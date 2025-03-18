//
// MainForm.cs
//
// MWSサービス価格改定売上データ作成ツール メイン画面フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/10/16 勝呂):新規作成
//
using CommonLib.BaseFactory.Pca;
using CommonLib.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PriceUpEarningsFile.Forms
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// 起動日
		/// </summary>
		private Date BootDate;

		/// <summary>
		/// PCA商品マスタリスト（旧価格）
		/// </summary>
		private List<商品マスタ> GoodsList { get; set; }

		/// <summary>
		/// 汎用データレイアウト売上明細データリスト
		/// </summary>
		private List<汎用データレイアウト売上明細データ> ImportEarningsList { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			GoodsList = new List<商品マスタ>();
			ImportEarningsList = new List<汎用データレイアウト売上明細データ>();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
#if DEBUG
			BootDate = new Date(2025, 1, 1);
#else
			BootDate = Date.Today;
#endif
			this.Text = string.Format("{0} Ver {1}(2024/10/16)", Program.gProcName, Program.gVersionStr);

			dateTimePickerMonth.Value = BootDate.ToDateTime();
			textBoxFolder.Text = Program.gSettings.ExportDir;
			textBoxExportFilename.Text = Program.gSettings.ExportFilename;
			numericTextBoxPcaVerEarnings.Text = Program.gSettings.PcaVersionEarnings.ToString();
			numericTextBoxPcaVerGoods.Text = Program.gSettings.PcaVersionGoods.ToString();
		}

		/// <summary>
		/// 売上データファイルの選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonImportFile_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
				dlg.Title = "売上データファイルを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					textBoxImportPathname.Text = dlg.FileName;
				}
			}
		}

		/// <summary>
		/// PCA商品マスタの選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void buttonGoodsFile_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
				dlg.Title = "PCA商品マスタファイルを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					textBoxGoodsPathname.Text = dlg.FileName;
				}
			}
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			if (0 == textBoxImportPathname.Text.Trim().Length)
			{
				MessageBox.Show("売上データファイル名が指定されていません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (0 == textBoxGoodsPathname.Text.Trim().Length)
			{
				MessageBox.Show("商品マスタファイル名が指定されていません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (!Directory.Exists(textBoxFolder.Text))
			{
				MessageBox.Show("出力先が存在しません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (0 == textBoxExportFilename.Text.Trim().Length)
			{
				MessageBox.Show("出力ファイル名が指定されていません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (-1 != textBoxExportFilename.Text.IndexOf('.'))
			{
				MessageBox.Show("出力ファイル名に拡張子を指定しないでください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 課金データ作成バッチ 売上データファイルの読込み
				if (0 == ReadImportFile(textBoxImportPathname.Text.Trim(), dateTimePickerMonth.Value.ToDate()))
				{
					MessageBox.Show("月額課金サービス 新規利用申込分はありませんでした。\nファイルを確認してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				// PCA商品マスタファイル（旧価格）の読込み
				if (0 == ReadGoodsFile(textBoxGoodsPathname.Text.Trim()))
				{
					MessageBox.Show("PCA商品マスタファイルに商品が登録されていません。\nファイルを確認してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				// 売上データファイルの出力（赤伝、黒伝）
				WriteExportFile();

				// 中間ファイルをリネームして出力ファルダにコピー
				File.Copy(Program.gSettings.TemporaryPathname, Program.gSettings.FormalPathname(Program.gSettings.FormalFilename));

				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("エラー発生：{0}", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			MessageBox.Show("売上データファイルを出力しました。（赤伝、黒伝）", "終了", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// 課金データ作成バッチ 売上データファイルの読込み
		/// </summary>
		/// <param name="pathname">売上データファイルパス名</param>
		/// <param name="date">実行月</param>
		/// <returns>読込み行数</returns>
		private int ReadImportFile(string pathname, Date date)
		{
			ImportEarningsList.Clear();

			// 摘要名：2025年01月 月額利用料
			string targetTekiyo = string.Format("{0:D4}年{1:D2}月 月額利用料", date.Year, date.Month);

			// 売上データファイルの読込み（月額課金サービス 利用申込初月分）
			using (StreamReader sr = new StreamReader(pathname, Encoding.GetEncoding("shift_jis")))
			{
				while (!sr.EndOfStream)
				{
					string line = sr.ReadLine();
					if (';' == line[0])
					{
						// コメント行
						continue;
					}
					// データ中にカンマがあるので、ダブルクォーテーションでくくっているフィールドが存在するため、CSV区切り処理を正規な関数に変更
					//string[] csv = line.Split(',');
					List<string> work = SplitString.CSVSplitLine2(line);
					汎用データレイアウト売上明細データ data = new 汎用データレイアウト売上明細データ();
					if (data.SetCsvRecord(work.ToArray()))
					{
						if (targetTekiyo == data.摘要名.Trim())
						{
							// 月額課金サービス 利用申込初月分
							ImportEarningsList.Add(data);
						}
					}
				}
			}
			return ImportEarningsList.Count;
		}

		/// <summary>
		/// PCA商品マスタファイル（旧価格）の読込み
		/// </summary>
		/// <param name="pathname">CA商品マスタファイルパス名</param>
		/// <returns>読込み行数</returns>
		private int ReadGoodsFile(string pathname)
		{
			GoodsList.Clear();
			using (StreamReader sr = new StreamReader(pathname, Encoding.GetEncoding("shift_jis")))
			{
				while (!sr.EndOfStream)
				{
					string line = sr.ReadLine();
					if (';' == line[0])
					{
						// コメント行
						continue;
					}
					// データ中にカンマがあるので、ダブルクォーテーションでくくっているフィールドが存在するため、CSV区切り処理を正規な関数に変更
					//string[] csv = line.Split(',');
					List<string> work = SplitString.CSVSplitLine2(line);
					商品マスタ data = new 商品マスタ();
					if (data.SetCsvRecord(work.ToArray()))
					{
						GoodsList.Add(data);
					}
				}
				return GoodsList.Count;
			}
		}

		/// <summary>
		/// 売上データファイルの出力（赤伝、黒伝）
		/// </summary>
		private void WriteExportFile()
		{
			// 中間ファイルの出力
			using (var sw = new StreamWriter(Program.gSettings.TemporaryPathname, false, System.Text.Encoding.GetEncoding("shift_jis")))
			{
				// 伝票番号
				int no = Program.gSettings.SlipInitialNumber;

				// 赤伝を新価格で出力
				string tokuisakiNo = string.Empty;
				foreach (汎用データレイアウト売上明細データ data in ImportEarningsList)
				{
					if (tokuisakiNo != data.得意先コード)
					{
						tokuisakiNo = data.得意先コード;
						no++;
					}
					if (data.IsArticleRecord)
					{
						// 記事レコード
						汎用データレイアウト売上明細データ articleSlip = data.DeepCopy();
						articleSlip.伝票No = no;
						sw.WriteLine(articleSlip.ToCsvString(Program.gSettings.PcaVersionEarnings));
					}
                    else
                    {
						// 記事レコードでない
						// 赤伝出力
						// 22 数量：マイナス値
						// 24 単価：プラス値
						// 25 売上金額：マイナス値
						// 26 原単価：プラス値
						// 27 原価金額：マイナス値
						// 34 標準価格：プラス値
						汎用データレイアウト売上明細データ redSlip = data.DeepCopy();
						redSlip.伝票No = no;
						redSlip.備考 = string.Format("No.{0}の赤伝", data.伝票No);
						redSlip.数量 = -(data.数量);
						//redSlip.単価 = -(data.単価);
						redSlip.売上金額 = -(data.売上金額);
						//redSlip.原単価 = -(data.原単価);
						redSlip.原価金額 = -(data.原価金額);
						//redSlip.標準価格 = -(data.標準価格);
						sw.WriteLine(redSlip.ToCsvString(Program.gSettings.PcaVersionEarnings));
					}
				}
				// 黒伝を旧価格で出力
				tokuisakiNo = string.Empty;
				foreach (汎用データレイアウト売上明細データ data in ImportEarningsList)
				{
					if (tokuisakiNo != data.得意先コード)
					{
						tokuisakiNo = data.得意先コード;
						no++;
					}
					if (data.IsArticleRecord)
					{
						// 記事レコード
						汎用データレイアウト売上明細データ articleSlip = data.DeepCopy();
						articleSlip.伝票No = no;
						sw.WriteLine(articleSlip.ToCsvString(Program.gSettings.PcaVersionEarnings));
					}
					else
					{
						// 記事レコードでない
						商品マスタ goods = GoodsList.Find(p => p.商品コード == data.商品コード);
						if (null != goods)
						{
							// 黒伝を旧価格で出力
							汎用データレイアウト売上明細データ blackSlip = data.DeepCopy();
							blackSlip.伝票No = no;
							blackSlip.備考 = string.Format("No.{0}の黒伝", data.伝票No);
							blackSlip.単価 = goods.標準価格;
							blackSlip.売上金額 = goods.標準価格;
							blackSlip.原単価 = goods.原価;
							blackSlip.原価金額 = goods.原価;
							blackSlip.標準価格 = goods.標準価格;
							sw.WriteLine(blackSlip.ToCsvString(Program.gSettings.PcaVersionEarnings));
						}
					}
				}
			}
		}
	}
}
