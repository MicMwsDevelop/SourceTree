//
// MainForm.cs
// 
// メイン画面フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/01/14 勝呂)
//
using ClosedXML.Excel;
using CommonLib.BaseFactory.PurchaseUnitPriceFile;
using CommonLib.Common;
using CommonLib.DB.SqlServer;
using CommonLib.DB.SqlServer.PurchaseUnitPriceFile;
using PurchaseUnitPriceFile.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace PurchaseUnitPriceFile
{
	public partial class MainForm : Form
	{
		// pinvoke:
		private const int DTM_GETMONTHCAL = 0x1000 + 8;
		private const int MCM_SETCURRENTVIEW = 0x1000 + 32;

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

		/// <summary>
		/// 環境設定
		/// </summary>
		public static PurchaseUnitPriceFileSettings Settings;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			// 環境設定の読込
			Settings = PurchaseUnitPriceFileSettingsIF.GetSettings();

			// 先月初日を設定
			dateTimePickerTarget.Value = Date.Today.FirstDayOfLasMonth().ToDateTime();

			// 環境設定内容の表示
			textBoxZaikoFilename.Text = Settings.在庫一覧表入力パス名;
			textBoxOutputFolder.Text = Settings.出力先フォルダ;

			if (!DropTable())
			{
				return;
			}
			if (!CreateTable())
			{
				return;
			}
			/////////////////////////////////////
			// 1.在庫一覧テーブルリンク接続
			// 環境設定.在庫一覧表入力ファイル名：\\storage\公開データ\業務部公開用\経理共有\仕入振替\在庫一覧表\在庫一覧表GG年MM月末.txt

			string msg;
			List<在庫一覧表> zaikoList = ReadZaikoListCsvFile(Settings.在庫一覧表入力パス名, out msg);
			if (0 == zaikoList.Count)
			{
				MessageBox.Show(msg, "在庫一覧表の読込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			try
			{
				DataTable dt = new DataTable();
				在庫一覧表.SetDataColumn(dt);
				foreach (在庫一覧表 zaiko in zaikoList)
				{
					dt.Rows.Add(zaiko.GetDataRow(dt));
				}
				DatabaseAccess.BulkInsert(Settings.Connect.Charlie.ConnectionString, "TMP_在庫一覧表", dt);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "在庫一覧表のレコード追加エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Drop Down
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dateTimePickerTarget_DropDown(object sender, EventArgs e)
		{
			DateTimePicker myDt = (DateTimePicker)sender;

			IntPtr cal = SendMessage(dateTimePickerTarget.Handle, DTM_GETMONTHCAL, IntPtr.Zero, IntPtr.Zero);
			SendMessage(cal, MCM_SETCURRENTVIEW, IntPtr.Zero, (IntPtr)1);
		}

		/// <summary>
		/// 在庫一覧表入力ファイル名の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInputZaikoFile_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				string folder = PurchaseUnitPriceFileSettings.在庫一覧表パス初期値;
				string filename = string.Empty;
				if (0 < Settings.在庫一覧表入力パス名.Length)
				{
					folder = Path.GetDirectoryName(Settings.在庫一覧表入力パス名);
					filename = Path.GetFileName(Settings.在庫一覧表入力パス名);
				}
				dlg.InitialDirectory = folder;
				dlg.FileName = filename;
				dlg.Filter = "TEXTファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
				dlg.Title = "在庫一覧表ファイルを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					Settings.在庫一覧表入力パス名 = dlg.FileName;

					// 環境設定の書込み
					PurchaseUnitPriceFileSettingsIF.SetSettings(Settings);
				}
			}
		}

		/// <summary>
		/// 振替出力ファイル 出力先フォルダの変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonChangeFolder_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dlg = new FolderBrowserDialog())
			{
				dlg.Description = "出力先フォルダを指定してください。";
				dlg.SelectedPath = Settings.出力先フォルダ;
				dlg.ShowNewFolderButton = false;
				if (DialogResult.OK == dlg.ShowDialog())
				{
					Settings.出力先フォルダ = dlg.SelectedPath;
					textBoxOutputFolder.Text = Settings.出力先フォルダ;

					// 環境設定の書込み
					PurchaseUnitPriceFileSettingsIF.SetSettings(Settings);
				}
			}
		}

		/// <summary>
		/// START
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonStart_Click(object sender, EventArgs e)
		{
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 対象年月
				YearMonth collectMonth = new YearMonth(dateTimePickerTarget.Value.Year, dateTimePickerTarget.Value.Month);

				// (2)在庫評価単価 選択クエリの実行：2-2 在庫評価単価.sql
				List<在庫評価単価> 在庫評価単価_List = PurchaseUnitPriceFileAccess.Select_在庫評価単価(Settings.Connect.Charlie.ConnectionString);

				// (3)当月仕入単価 選択クエリの実行：2-3 当月仕入単価.sql
				List<当月仕入単価> 当月仕入単価_List = PurchaseUnitPriceFileAccess.Select_当月仕入単価(collectMonth, Settings.Connect.Junp.ConnectionString);

				仕入振替出力用ファイル作成(collectMonth, 在庫評価単価_List, 当月仕入単価_List);

				/////////////////////////////////////
				// 3.貯蔵品社内使用分出荷振替データ作成

				貯蔵品振替出力用ファイル作成(collectMonth, 在庫評価単価_List);

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("振替出力ファイルを出力しました。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "振替出力ファイル出力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 社内使用分振替出力ファイルを開く
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOpenExcel_Click(object sender, EventArgs e)
		{
			if (File.Exists(Settings.社内使用分振替出力パス名))
			{
				// Excelの起動
				using (Process process = new Process())
				{
					process.StartInfo.FileName = Settings.社内使用分振替出力パス名;
					process.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
					process.Start();
				}
			}
			else
			{
				MessageBox.Show(string.Format("{0} が見つかりません。", Settings.社内使用分振替出力ファイル名), "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		/// <summary>
		/// 終了
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// テーブルの削除
		/// </summary>
		public bool DropTable()
		{
			try
			{
				// TMP_在庫一覧表
				DatabaseAccess.DropTable(在庫一覧表.DropTableString, Settings.Connect.Charlie.ConnectionString);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "DropTable実行エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		/// <summary>
		/// テーブルの新規作成
		/// </summary>
		public bool CreateTable()
		{
			try
			{
				// TMP_在庫一覧表
				DatabaseAccess.CreateTable(在庫一覧表.CreateTableString, Settings.Connect.Charlie.ConnectionString);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "CreateTable実行エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		/// <summary>
		///在庫一覧表GG年MM月.CSVの読み込み
		/// </summary>
		/// <param name="pathname">パラメタファイルパス名</param>
		/// <param name="msg">エラーメッセージ</param>
		/// <returns>在庫一覧表リスト</returns>
		public List<在庫一覧表> ReadZaikoListCsvFile(string pathname, out string msg)
		{
			msg = string.Empty;

			List<在庫一覧表> dataList = new List<在庫一覧表>();
			if (File.Exists(pathname))
			{
				try
				{
					// テキストファイルの読み込み
					using (StreamReader textfile = new StreamReader(pathname, System.Text.Encoding.GetEncoding("Shift_JIS")))
					{
						string line = textfile.ReadLine();
						bool firstLine = true;
						while (null != line)
						{
							if (0 < line.Length)
							{
								if (!firstLine)
								{
									line = line.Trim(StringUtil.DefalutTrimCharSet);
									if (';' != line[0])
									{
										// コメント行以外
										List<string> split = SplitString.CSVSplitLine(line);
										在庫一覧表 data = new 在庫一覧表();
										data.SetCsvRecord(split);
										dataList.Add(data);
									}
								}
								else
								{
									// １行目はタイトル行なのでスキップ
									firstLine = false;
								}
							}
							line = textfile.ReadLine();
						}
					}
				}
				catch (Exception ex)
				{
					msg = ex.ToString();
					return null;
				}
			}
			else
			{
				msg = pathname + "が存在しません。";
				return null;
			}
			return dataList;
		}

		/// <summary>
		/// 単価の取得
		/// 在庫がない時は仕入単価を使用
		/// </summary>
		/// <param name="商品コード"></param>
		/// <param name="在庫単価"></param>
		/// <param name="仕入単価"></param>
		/// <returns>単価</returns>
		private decimal GetUnitPrice(string 商品コード, List<在庫評価単価> 在庫単価, List<当月仕入単価> 仕入単価)
		{
			在庫評価単価 zaiko = 在庫単価.Find(p => p.商品コード == 商品コード);
			if (null != zaiko)
			{
				return zaiko.評価単価;
			}
			当月仕入単価 shiire = 仕入単価.Find(p => p.商品コード == 商品コード);
			if (null != shiire)
			{
				return shiire.単価;
			}
			return 0;
		}

		/// <summary>
		/// 2.社内使用分出荷振替データ作成
		/// </summary>
		/// <param name="collectMonth">対象月</param>
		/// <param name="在庫単価"></param>
		/// <param name="仕入単価"></param>
		public void 仕入振替出力用ファイル作成(YearMonth collectMonth, List<在庫評価単価> 在庫単価, List<当月仕入単価> 仕入単価)
		{
			try
			{
				// (1)社内使用分出荷明細 選択クエリの実行：2-1 社内使用分出荷明細.sql
				List<PCA出荷明細> 社内使用分出荷明細_List = PurchaseUnitPriceFileAccess.Select_社内使用分出荷明細(collectMonth, Settings.Connect.Junp.ConnectionString);

				// (6)社内仕入振替データテーブルをエクセルファイルで出力
				// 環境設定.社内使用分振替出力パス名：\\storage\公開データ\業務部公開用\経理共有\仕入振替\仕入振替出力用ﾌｧｲﾙ.xlsx
				using (XLWorkbook wb = new XLWorkbook())
				{
					IXLWorksheet ws = wb.Worksheets.Add("社内仕入振替データ");
					ws.Cell(1, 1).SetValue("日付");
					ws.Cell(1, 2).SetValue("部署名");
					ws.Cell(1, 3).SetValue("科目");
					ws.Cell(1, 4).SetValue("摘要");
					ws.Cell(1, 5).SetValue("商品コード");
					ws.Cell(1, 6).SetValue("品名");
					ws.Cell(1, 7).SetValue("数量");
					ws.Cell(1, 8).SetValue("単価");
					ws.Cell(1, 9).SetValue("金額");
					for (int i = 0; i < 社内使用分出荷明細_List.Count; i++)
					{
						PCA出荷明細 data = 社内使用分出荷明細_List[i];
						ws.Cell(i + 2, 1).SetValue(data.出荷日.ToString());
						ws.Cell(i + 2, 2).SetValue(data.出荷先名.Replace("ミック", "").Trim());
						ws.Cell(i + 2, 3).SetValue("");
						ws.Cell(i + 2, 4).SetValue(data.先方担当者名.Trim());
						ws.Cell(i + 2, 5).SetValue(data.商品コード.Trim());
						ws.Cell(i + 2, 6).SetValue(data.品名.Trim());
						ws.Cell(i + 2, 7).SetValue(data.数量);
						decimal unitPrice = GetUnitPrice(data.商品コード, 在庫単価, 仕入単価);
						ws.Cell(i + 2, 8).SetValue(unitPrice);
						ws.Cell(i + 2, 9).SetValue(data.数量 * unitPrice);
					}
					// Excelファイルの保存
					wb.SaveAs(Settings.社内使用分振替出力パス名);
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// 3.貯蔵品社内使用分出荷振替データ作成
		/// </summary>
		/// <param name="collectMonth">対象月</param>
		/// <param name="在庫単価"></param>
		public void 貯蔵品振替出力用ファイル作成(YearMonth collectMonth, List<在庫評価単価> 在庫単価)
		{
			try
			{
				// (1) 貯蔵品社内使用分出荷明細 選択クエリの実行：3-1 貯蔵品社内使用分出荷明細.sql
				List<PCA出荷明細貯蔵品> 貯蔵品社内使用分出荷明細_List = PurchaseUnitPriceFileAccess.Select_貯蔵品社内使用分出荷明細(collectMonth, Settings.Connect.Junp.ConnectionString);

				// (3)当月仕入単価貯蔵品 選択クエリの実行：3-2 当月仕入単価貯蔵品.sql
				List<当月仕入単価> 当月仕入単価貯蔵品_List = PurchaseUnitPriceFileAccess.Select_当月仕入単価貯蔵品(collectMonth, Settings.Connect.Junp.ConnectionString);

				// (6)社内仕入振替データテーブルをエクセルファイルで出力
				// 環境設定.社内使用分振替出力パス名：\\storage\公開データ\業務部公開用\経理共有\仕入振替\仕入振替出力用ﾌｧｲﾙ.xlsx
				using (XLWorkbook wb = new XLWorkbook())
				{
					IXLWorksheet ws = wb.Worksheets.Add("貯蔵品社内仕入振替データ");
					ws.Cell(1, 1).SetValue("商品コード");
					ws.Cell(1, 2).SetValue("品名");
					ws.Cell(1, 3).SetValue("日付");
					ws.Cell(1, 4).SetValue("伝票No");
					ws.Cell(1, 5).SetValue("部署名");
					ws.Cell(1, 6).SetValue("数量");
					ws.Cell(1, 7).SetValue("単価");
					ws.Cell(1, 8).SetValue("金額");
					ws.Cell(1, 9).SetValue("摘要");
					for (int i = 0; i < 貯蔵品社内使用分出荷明細_List.Count; i++)
					{
						PCA出荷明細貯蔵品 data = 貯蔵品社内使用分出荷明細_List[i];
						ws.Cell(i + 2, 1).SetValue(data.商品コード.Trim());
						ws.Cell(i + 2, 2).SetValue(data.品名.Trim());
						ws.Cell(i + 2, 3).SetValue(data.出荷日.ToString());
						ws.Cell(i + 2, 4).SetValue(data.伝票No);
						ws.Cell(i + 2, 5).SetValue(data.出荷先名.Replace("ミック", "").Trim());
						ws.Cell(i + 2, 6).SetValue(data.数量);
						decimal unitPrice = GetUnitPrice(data.商品コード, 在庫単価, 当月仕入単価貯蔵品_List);
						ws.Cell(i + 2, 7).SetValue(unitPrice);
						ws.Cell(i + 2, 8).SetValue(data.数量 * unitPrice);
						ws.Cell(i + 2, 9).SetValue(data.先方担当者名.Trim());
					}
					// Excelファイルの保存
					wb.SaveAs(Settings.貯蔵品社内使用分振替出力パス名);
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
