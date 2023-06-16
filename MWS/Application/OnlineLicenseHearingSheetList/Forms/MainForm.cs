using ClosedXML.Excel;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace OnlineLicenseHearingSheetList.Forms
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// オンライン資格確認資料格納フォルダ
		/// </summary>
		private const string OnsPicsFolder  = @"\\wwsv\ons-pics";

		/// <summary>
		/// ヒアリングシートリストエクセルファイル名
		/// </summary>
		private const string ListFilename = "ヒアリングシートリスト.xlsx";

		/// <summary>
		/// ヒアリングシート一覧シート名
		/// </summary>
		private const string ListFileSheetname = "ヒアリングシート一覧";

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// ヒアリングシートリスト作成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonHearingSheetList_Click(object sender, EventArgs e)
		{
			try
			{
				string pathname = Path.Combine(Directory.GetCurrentDirectory(), ListFilename);
				using (XLWorkbook wb = new XLWorkbook())
				{
					// 元のカーソルを保持
					Cursor preCursor = Cursor.Current;

					// カーソルを待機カーソルに変更
					Cursor.Current = Cursors.WaitCursor;

					// シート「ヒアリングシート一覧」の追加
					IXLWorksheet ws = wb.AddWorksheet(ListFileSheetname);

					int row = 2;
					ws.Cell(1, 1).SetValue("No");
					ws.Cell(1, 2).SetValue("顧客No");
					ws.Cell(1, 3).SetValue("ヒアリングシート名");
					ws.Cell(1, 4).SetValue("更新日時");

					DirectoryInfo ons_pics = new DirectoryInfo(OnsPicsFolder);
					foreach (DirectoryInfo folder in ons_pics.EnumerateDirectories())
					{
						if (8 == folder.Name.Length)
						{
							int numericValue;
							if (int.TryParse(folder.Name, out numericValue))
							{
								ws.Cell(row, 1).SetValue(row - 1);
								ws.Cell(row, 2).SetValue(numericValue);

								// 数値なので顧客Noと判断する
								int column = 3;
								foreach (FileInfo file in folder.EnumerateFiles("*.xlsx"))
								{
									ws.Cell(row, column).SetValue(file.Name);
									ws.Cell(row, column + 1).SetValue(file.LastWriteTime);		// 更新日時
									column = column + 2;
								}
							}
							row++;
						}
					}
					// Excelファイルの保存
					wb.SaveAs(pathname);

					// カーソルを元に戻す
					Cursor.Current = preCursor;

					//MessageBox.Show(this, "ヒアリングシートリストを出力しました。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);

					// Excelの起動
					using (Process process = new Process())
					{
						process.StartInfo.FileName = pathname;
						process.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
						process.Start();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, string.Format("エクセルファイル更新エラー：{0}", ex.Message), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
