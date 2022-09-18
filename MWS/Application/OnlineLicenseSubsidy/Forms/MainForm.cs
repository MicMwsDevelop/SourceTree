//
// MainForm.cs
// 
// メイン画面 フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2022/07/06 勝呂):新規作成
//
using ClosedXML.Excel;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.BaseFactory.VariousDocumentOut;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using OnlineLicenseSubsidy.BaseFactory;
using OnlineLicenseSubsidy.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OnlineLicenseSubsidy.Forms
{
	public partial class MainForm : Form
	{
		private const string OutputExcelName = "オン資補助金申請書類";

		/// <summary>
		/// 各種書類出力 共通情報
		/// </summary>
		private DocumentCommon Common { get; set; }

		public MainForm()
		{
			InitializeComponent();

			Common = new DocumentCommon();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			// ウィンドウタイトルにバージョン情報を表示
			this.Text = string.Format("{0} {1}", Program.ProgramName, Program.VersionStr);

			// 出力対象月コンボボックスの設定
			List<Tuple<string, string>> list = new List<Tuple<string, string>>();
			string[] folders = System.IO.Directory.GetDirectories(@"C:\ons-pics\_補助金申請書類\NTT東日本_提出用", "*", SearchOption.TopDirectoryOnly);
			for (int i = 0; i < folders.Length; i++)
			{
				list.Add(new Tuple<string, string>(Path.GetFileName(folders[i]), folders[i]));
			}
			comboBoxYearMonth.DataSource = list;
			comboBoxYearMonth.DisplayMember = "Item1";
			comboBoxYearMonth.ValueMember = "Item2";
			comboBoxYearMonth.SelectedIndex = 0;

			//try
			//{
			//	List<SatelliteOffice> headOfficeList = VariousDocumentOutAccess.Select_SatelliteOfficeInfo(Environment.UserName, Program.gSettings.Junp.ConnectionString);
			//	Common.Satellite = headOfficeList.First();
			//}
			//catch (Exception ex)
			//{
			//	MessageBox.Show(this, string.Format("サーバー通信エラー：{0}", ex.Message), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			//}
		}

		/// <summary>
		/// 作業リスト出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonWorkbook_Click(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			// orgファイル→Excelファイルをコピー
			string orgWorkPathname = Path.Combine(Directory.GetCurrentDirectory(), "オン資補助金申請書類出力作業リスト.xlsx.org");
			string workFilename = string.Format("オン資補助金申請書類出力作業リスト_{0}.xlsx", Date.Today.GetNumeralString());
			string workPathname = Path.Combine(Directory.GetCurrentDirectory(), workFilename);
			File.Copy(orgWorkPathname, workPathname, true);

			// \\wwsv\ons-pics\管理用_営業管理部\NTT東日本_提出用\05_補助金額資料\202207
			Tuple<string, string> folder = (Tuple<string, string>)comboBoxYearMonth.SelectedItem;
			string[] clinicFolders = System.IO.Directory.GetDirectories(folder.Item2, "*", SearchOption.TopDirectoryOnly);

			List<補助金申請情報> dataList = new List<補助金申請情報>();
			foreach (string folderName in clinicFolders)
			{
				// 最後のフォルダ名の取得
				// ex. \\wwsv\ons-pics\管理用_営業管理部\NTT東日本_提出用\05_補助金額資料\202207\010251_東0591_浅沼歯科医院 → 010251_東0591_浅沼歯科医院
				string targetfolderName = Path.GetFileName(folderName);

				// フォルダ名から得意先Noの取得 
				string tokuisakiNo = string.Empty;
				if ('_' == targetfolderName[6])
				{
					tokuisakiNo = targetfolderName.Substring(0, 6);
				}
				if (6 != tokuisakiNo.Length)
				{
					continue;
				}
				// フォルダ名から受付通番の取得 ex.東0339
				string acceptNo = targetfolderName.Substring(7, 5);
				try
				{
					string whereStr = string.Format("得意先No = '{0}'", tokuisakiNo);
					List<vMicユーザーオン資用> work = JunpDatabaseAccess.Select_vMicユーザーオン資用(whereStr, "", Program.gSettings.Junp.ConnectionString);
					if (null != work)
					{
						vMicユーザーオン資用 userInfo = work.First();
						string[] files = Directory.GetFiles(folderName, "*.xlsx", SearchOption.TopDirectoryOnly);
						if (2 == files.Length)
						{
							string reportPathname, receiptPathname;
							if (-1 != files[0].IndexOf("完了報告書"))
							{
								reportPathname = files[0];
								receiptPathname = files[1];
							}
							else
							{
								reportPathname = files[1];
								receiptPathname = files[0];
							}
							// 完了報告書
							string msg;
							補助金申請情報 data = ReadExcel完了報告書(userInfo, acceptNo, reportPathname, out msg);
							if (null == data)
							{
								MessageBox.Show(this, msg, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								continue;
							}
							// 領収証内訳書
							data.領収内訳情報List = ReadExcel領収内訳書(receiptPathname, out msg);
							if (null != data.領収内訳情報List)
							{
								dataList.Add(data);
							}
							else
							{
								MessageBox.Show(this, msg, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, string.Format("サーバー通信エラー：{0}", ex.Message), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
			if (0 < dataList.Count)
			{
				// オン資補助金申請書類出力作業リスト.xlsxの出力
				WriteExcelオン資補助金申請書類出力作業リスト(dataList, workPathname);
				MessageBox.Show(this, string.Format("{0} を出力しました。", workPathname), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);

				// 作業リストの設定
				textBoxWorkbook.Text = workPathname;
				buttonExport.Enabled = true;

				try
				{
					// Excelの起動
					using (Process process = new Process())
					{
						process.StartInfo.FileName = workPathname;
						process.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
						process.Start();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}

		/// <summary>
		/// 完了報告書の読込
		/// </summary>
		/// <param name="ww">vMicユーザーオン資用</param>
		/// <param name="acceptNo">受付通番</param>
		/// <param name="pathname">完了報告書パス名</param>
		/// <param name="msg">エラーメッセージ</param>
		/// <returns>補助金申請情報</returns>
		public static 補助金申請情報 ReadExcel完了報告書(vMicユーザーオン資用 ww, string acceptNo, string pathname, out string msg)
		{
			msg = string.Empty;
			if (File.Exists(pathname))
			{
				try
				{
					using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
					{
						// シート名が固定でないので検索する
						IXLWorksheet ws = null;
						foreach (var sheet in wb.Worksheets)
						{
							if (-1 != sheet.Name.IndexOf("別紙様式3"))
							{
								ws = sheet;
								break;
							}
						}
						補助金申請情報 data = new 補助金申請情報();
						data.顧客情報WW = ww;
						data.受付通番 = acceptNo;
						string yearStr = ws.Cell(2, 20).GetString();
						string monthStr = ws.Cell(2, 23).GetString();
						string dayStr = ws.Cell(2, 25).GetString();
						int year, month, day;
						int.TryParse(yearStr, out year);
						int.TryParse(monthStr, out month);
						int.TryParse(dayStr, out day);
						if (0 < year && 0 < month && 0 < day)
						{
							data.工事完了日 = new DateTime(year, month, day);
						}
						data.医療機関コード = ws.Cell(8, 19).GetString();
						data.医療機関コード += ws.Cell(8, 20).GetString();
						data.医療機関コード += ws.Cell(8, 21).GetString();
						data.医療機関コード += ws.Cell(8, 22).GetString();
						data.医療機関コード += ws.Cell(8, 23).GetString();
						data.医療機関コード += ws.Cell(8, 24).GetString();
						data.医療機関コード += ws.Cell(8, 25).GetString();
						data.顧客名 = ws.Cell(9, 19).GetString();
						data.開設者 = ws.Cell(10, 19).GetString();
						data.郵便番号 = ws.Cell(11, 19).GetString();
						data.住所 = ws.Cell(12, 15).GetString();
						data.電話番号 = ws.Cell(13, 19).GetString();
						return data;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(pathname);
					msg = ex.Message;
				}
			}
			else
			{
				msg = string.Format("{0}が見つかりません。", pathname);
			}
			return null;
		}

		/// <summary>
		/// 領収内訳書の読込
		/// </summary>
		/// <param name="pathname">領収内訳書パス名</param>
		/// <param name="msg">エラーメッセージ</param>
		/// <returns>領収内訳情報リスト</returns>
		public static List<領収内訳情報> ReadExcel領収内訳書(string pathname, out string msg)
		{
			msg = string.Empty;
			if (File.Exists(pathname))
			{
				try
				{
					using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
					{
						// シート名が固定でないので検索する
						IXLWorksheet ws = null;
						foreach (var sheet in wb.Worksheets)
						{
							if (-1 != sheet.Name.IndexOf("別紙様式2"))
							{
								ws = sheet;
								break;
							}
						}
						List<領収内訳情報> list = new List<領収内訳情報>();
						for (int i = 0, j = 17; i < 15; i++, j++)
						{
							if ("" == ws.Cell(j, 4).GetString())
							{
								break;
							}
							領収内訳情報 data = new 領収内訳情報();
							data.項目 = ws.Cell(j, 4).GetString();
							data.内訳 = ws.Cell(j, 14).GetString();
							data.補助対象金額 = Program.GetDoubleData(ws.Cell(j, 36));
							data.補助対象外金額 = Program.GetDoubleData(ws.Cell(j, 41));
							list.Add(data);
						}
						return list;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(pathname);
					msg = ex.Message;
				}
			}
			else
			{
				msg = string.Format("{0}が見つかりません。", pathname);
			}
			return null;
		}

		/// <summary>
		/// オン資補助金申請書類出力作業リスト.xlsxの出力
		/// </summary>
		/// <param name="dataList">補助金申請情報リスト</param>
		/// <param name="pathname">オン資補助金申請書類出力作業リストパス名</param>
		private void WriteExcelオン資補助金申請書類出力作業リスト(List<補助金申請情報> dataList, string pathname)
		{
			using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
			{
				// 顧客情報
				IXLWorksheet ws1 = wb.Worksheet("顧客情報");
				IXLWorksheet ws2 = wb.Worksheet("出力内容");
				for (int i = 0, j = 3; i < dataList.Count; i++, j++)
				{
					補助金申請情報 data = dataList[i];

					// 「顧客情報」
					// 基本情報
					ws1.Cell(j, 1).SetValue(data.受付通番);
					ws1.Cell(j, 2).SetValue(data.顧客情報WW.得意先No);
					ws1.Cell(j, 3).SetValue(data.顧客情報WW.顧客No);

					// NTT顧客情報
					ws1.Cell(j, 4).SetValue(data.医療機関コード);
					ws1.Cell(j, 5).SetValue(data.開設者);
					ws1.Cell(j, 6).SetValue(data.郵便番号);
					ws1.Cell(j, 7).SetValue(data.住所);
					ws1.Cell(j, 8).SetValue(data.電話番号);

					// MIC顧客情報
					ws1.Cell(j, 9).SetValue(data.顧客情報WW.GetClinicCodeNumeric());
					ws1.Cell(j, 10).SetValue(data.顧客情報WW.開設者);
					ws1.Cell(j, 11).SetValue(data.顧客情報WW.郵便番号);
					ws1.Cell(j, 12).SetValue(data.顧客情報WW.住所);
					ws1.Cell(j, 13).SetValue(data.顧客情報WW.電話番号);

					// 出力する顧客情報  ※NTT顧客情報を設定
					ws1.Cell(j, 14).SetValue(data.顧客情報WW.顧客名);
					ws1.Cell(j, 15).SetValue(data.医療機関コード);
					ws1.Cell(j, 16).SetValue(data.開設者);
					ws1.Cell(j, 17).SetValue(data.郵便番号);
					ws1.Cell(j, 18).SetValue(data.住所);
					ws1.Cell(j, 19).SetValue(data.電話番号);
					ws1.Cell(j, 20).SetValue(data.工事完了日);

					// 相違点は赤で表示する
					if (data.医療機関コード != data.顧客情報WW.GetClinicCodeNumeric())
					{
						ws1.Cell(j, 15).Style.Font.SetFontColor(XLColor.Red);
					}
					if (data.開設者 != data.顧客情報WW.開設者)
					{
						ws1.Cell(j, 16).Style.Font.SetFontColor(XLColor.Red);
					}
					if (data.郵便番号 != data.顧客情報WW.郵便番号)
					{
						ws1.Cell(j, 17).Style.Font.SetFontColor(XLColor.Red);
					}
					if (data.住所 != data.顧客情報WW.住所)
					{
						ws1.Cell(j, 18).Style.Font.SetFontColor(XLColor.Red);
					}
					if (data.電話番号 != data.顧客情報WW.電話番号)
					{
						ws1.Cell(j, 19).Style.Font.SetFontColor(XLColor.Red);
					}
					// 「出力内容」
					ws2.Cell(j, 1).SetValue(data.顧客情報WW.得意先No);
					for (int k = 0, c = 2; k < data.領収内訳情報List.Count; k++, c += 4)
					{
						領収内訳情報 item = data.領収内訳情報List[k];
						ws2.Cell(j, c).SetValue(item.項目);
						ws2.Cell(j, c + 1).SetValue(item.内訳);
						ws2.Cell(j, c + 2).SetValue(item.補助対象金額);
						ws2.Cell(j, c + 3).SetValue(item.補助対象外金額);
					}
				}
				// Excelファイルの保存
				wb.Save();
			}
		}

		/// <summary>
		/// 作業リストの設定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInputWorkbook_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "EXCELファイル(*.xlsx)|*.xlsx|すべてのファイル(*.*)|*.*";
				dlg.Title = "オン資補助金申請書類出力作業リストファイルを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					textBoxWorkbook.Text = dlg.FileName;
					buttonExport.Enabled = true;
				}
			}
		}

		/// <summary>
		/// 作業リストファイルの読込
		/// </summary>
		/// <param name="pathname"></param>
		/// <param name="msg"></param>
		/// <returns></returns>
		private List<補助金申請出力情報> ReadExcel作業リスト(string pathname, out string msg)
		{
			msg = string.Empty;
			if (File.Exists(pathname))
			{
				try
				{
					using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
					{
						List<補助金申請出力情報> list = new List<補助金申請出力情報>();
						IXLWorksheet ws1 = wb.Worksheet("顧客情報");
						IXLWorksheet ws2 = wb.Worksheet("出力内容");
						for (int i = 3; ; i++)
						{
							if ("" == ws1.Cell(i, 1).GetString())
							{
								break;
							}
							補助金申請出力情報 data = new 補助金申請出力情報();
							data.受付通番 = ws1.Cell(i, 1).GetString();
							data.得意先番号 = ws1.Cell(i, 2).GetString();
							data.顧客No = int.Parse(ws1.Cell(i, 3).GetString());
							data.顧客名 = ws1.Cell(i, 14).GetString();
							data.医療機関コード = ws1.Cell(i, 15).GetString();
							data.開設者 = ws1.Cell(i, 16).GetString();
							data.郵便番号 = ws1.Cell(i, 17).GetString();
							data.住所 = ws1.Cell(i, 18).GetString();
							data.電話番号 = ws1.Cell(i, 19).GetString();
							data.工事完了日 = Program.GetDateTimeData(ws1.Cell(i, 20));
							list.Add(data);
							for (int j = 3; ; j++)
							{
								if ("" == ws2.Cell(j, 1).GetString())
								{
									break;
								}
								if (data.得意先番号 == ws2.Cell(j, 1).GetString())
								{
									for (int k = 2; k < 62; k += 4)
									{
										if ("" == ws2.Cell(j, k).GetString())
										{
											break;
										}
										領収内訳情報 meisai = new 領収内訳情報();
										meisai.項目 = ws2.Cell(j, k).GetString();
										meisai.内訳 = ws2.Cell(j, k + 1).GetString();
										meisai.補助対象金額 = Program.GetDoubleData(ws2.Cell(j, k + 2));
										meisai.補助対象外金額 = Program.GetDoubleData(ws2.Cell(j, k + 3));
										data.領収内訳情報List.Add(meisai);
									}
									break;
								}
							}
						}
						return list;
					}
				}
				catch (Exception ex)
				{
					msg = ex.Message;
				}
			}
			else
			{
				msg = string.Format("{0}が見つかりません。", pathname);
			}
			return null;
		}

		/// <summary>
		/// 書類出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExport_Click(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			string msg;
			List<補助金申請出力情報> dataList = ReadExcel作業リスト(textBoxWorkbook.Text, out msg);
			if (0 < msg.Length)
			{
				MessageBox.Show(this, msg, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			string orgPathname = Path.Combine(Directory.GetCurrentDirectory(), "オン資補助金申請書類.xlsx.org");
			foreach (補助金申請出力情報 data in dataList)
			{
				string outputPath = string.Format(@"C:\ons-pics\{0}", data.顧客No.ToString());
				if (false == Directory.Exists(outputPath))
				{
					Directory.CreateDirectory(outputPath);
				}
				outputPath = string.Format(@"{0}\助成金申請書類", outputPath);
				if (false == Directory.Exists(outputPath))
				{
					Directory.CreateDirectory(outputPath);
				}
				string excelPathname = Path.Combine(outputPath, data.GetExcelFilename);
				string pdfPathname = Path.Combine(outputPath, data.GetPdfFilename);
				try
				{
					// Excelファイル出力
					WriteExcelFile(orgPathname, excelPathname, data);

					// PDFファイル出力
					using (ExcelManager manage = new ExcelManager())
					{
						manage.Open(excelPathname);
						manage.SaveAsPDF(pdfPathname);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, string.Format("{0} 顧客No:{1}", ex.Message, data.顧客No), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
			// カーソルを元に戻す
			Cursor.Current = preCursor;

			MessageBox.Show(this, string.Format("補助金申請書類を出力しました。{0}", @"C:\ons-pics"), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// 書類出力
		/// </summary>
		/// <param name="common">各種書類出力 共通情報</param>
		/// <param name="inputPathname">入力Excelファイルパス名</param>
		/// <param name="outputPathname">出力Excelファイルパス名(org)</param>
		/// <param name="goodsList">オンライン資格確認対象商品売上明細</param>
		/// Ver1.07(2021/12/24):経理部専用 オンライン資格確認等事業完了報告書の対応
		/// Ver1.12(2022/02/22):経理部専用 オンライン資格確認等事業完了報告書 修正依頼対応
		private void WriteExcelFile(string orgPathname, string outputPathname, 補助金申請出力情報 data)
		{
			try
			{
				File.Copy(orgPathname, outputPathname, true);
				using (XLWorkbook wb = new XLWorkbook(outputPathname, XLEventTracking.Disabled))
				{
					// 領収書内訳書
					IXLWorksheet ws1 = wb.Worksheet("領収書内訳書");
					if (data.工事完了日.HasValue)
					{
						ws1.Cell(3, 42).SetValue(string.Format("{0,4}", data.工事完了日.Value.Year));
						ws1.Cell(3, 45).SetValue(string.Format("{0,2}", data.工事完了日.Value.Month));
						ws1.Cell(3, 47).SetValue(string.Format("{0,2}", data.工事完了日.Value.Day));
					}
					string ken = data.GetKenNumberString();
					ws1.Cell(6, 9).SetValue(ken.Substring(0, 1));
					ws1.Cell(6, 10).SetValue(ken.Substring(1, 1));
					ws1.Cell(8, 9).SetValue(data.医療機関コード.Substring(0, 1));
					ws1.Cell(8, 10).SetValue(data.医療機関コード.Substring(1, 1));
					ws1.Cell(8, 11).SetValue(data.医療機関コード.Substring(2, 1));
					ws1.Cell(8, 12).SetValue(data.医療機関コード.Substring(3, 1));
					ws1.Cell(8, 13).SetValue(data.医療機関コード.Substring(4, 1));
					ws1.Cell(8, 14).SetValue(data.医療機関コード.Substring(5, 1));
					ws1.Cell(8, 16).SetValue(data.医療機関コード.Substring(6, 1));
					ws1.Cell(10, 9).SetValue(string.Format("{0}  様", data.顧客名));

					//ws1.Cell(8, 40).SetValue(common.社名);
					//ws1.Cell(10, 40).SetValue(common.住所1);
					//ws1.Cell(12, 40).SetValue(common.電話番号);

					for (int i = 0, j = 16; i < data.領収内訳情報List.Count; i++, j++)
					{
						// 項目
						ws1.Cell(j, 4).SetValue(data.領収内訳情報List[i].項目);

						// 内訳
						ws1.Cell(j, 16).SetValue(data.領収内訳情報List[i].内訳);

						// ①補助対象金額
						if (0 < data.領収内訳情報List[i].補助対象金額)
						{
							ws1.Cell(j, 38).SetValue(data.領収内訳情報List[i].補助対象金額);
						}
						if (0 < data.領収内訳情報List[i].補助対象外金額)
						{
							// ②補助対象外金額
							ws1.Cell(j, 44).SetValue(data.領収内訳情報List[i].補助対象外金額);
						}
					}
					// ①小計
					//ws1.Cell(31, 42).SetValue(total);

					// ②小計
					//;

					// 総額（①＋②）
					//ws1.Cell(12, 9).SetValue(total);

					// 作業完了報告書
					IXLWorksheet ws2 = wb.Worksheet("作業完了報告書");
					if (data.工事完了日.HasValue)
					{
						ws2.Cell(2, 20).SetValue(string.Format("{0,4}", data.工事完了日.Value.Year));
						ws2.Cell(2, 23).SetValue(string.Format("{0,2}", data.工事完了日.Value.Month));
						ws2.Cell(2, 25).SetValue(string.Format("{0,2}", data.工事完了日.Value.Day));
					}
					ws2.Cell(7, 19).SetValue(ken.Substring(0, 1));
					ws2.Cell(7, 20).SetValue(ken.Substring(1, 1));
					ws2.Cell(8, 19).SetValue(data.医療機関コード.Substring(0, 1));
					ws2.Cell(8, 20).SetValue(data.医療機関コード.Substring(1, 1));
					ws2.Cell(8, 21).SetValue(data.医療機関コード.Substring(2, 1));
					ws2.Cell(8, 22).SetValue(data.医療機関コード.Substring(3, 1));
					ws2.Cell(8, 23).SetValue(data.医療機関コード.Substring(4, 1));
					ws2.Cell(8, 24).SetValue(data.医療機関コード.Substring(5, 1));
					ws2.Cell(8, 25).SetValue(data.医療機関コード.Substring(6, 1));
					ws2.Cell(9, 19).SetValue(data.顧客名);
					ws2.Cell(11, 18).SetValue(data.郵便番号);
					ws2.Cell(12, 15).SetValue(data.住所);
					ws2.Cell(13, 19).SetValue(data.電話番号);
					ws2.Cell(10, 19).SetValue(data.開設者);

					// Excelファイルの保存
					wb.Save();
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
