//
// MainForm.cs
// 
// WonderWeb見積書CSVファイル EXCEL出力 メイン画面フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/03/31 勝呂)
//
using ClosedXML.Excel;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WonderEstimateExcel
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// WonderWeb見積書CSVファイルパス名
		/// </summary>
		private string CsvPathname { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			CsvPathname = string.Empty;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			pictureBoxDropZone.AllowDrop = true;
		}

		/// <summary>
		/// ファイル追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSelectFile_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = @"C:\";
				dlg.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
				dlg.Title = "WonderWeb見積書CSVファイルを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					labelFilename.Text = dlg.FileName;
					CsvPathname = dlg.FileName;
					pictureBoxDropZone.Image = global::WonderEstimateExcel.Properties.Resources.CsvFile;
				}
			}
		}

		/// <summary>
		/// DragEnter
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBoxDropZone_DragEnter(object sender, DragEventArgs e)
		{
			//コントロール内にドラッグされたとき実行される
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				//ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
				e.Effect = DragDropEffects.Copy;
			else
				//ファイル以外は受け付けない
				e.Effect = DragDropEffects.None;
		}

		/// <summary>
		/// Drag&Drop
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBoxDropZone_DragDrop(object sender, DragEventArgs e)
		{
			//コントロール内にドロップされたとき実行される
			//ドロップされたすべてのファイル名を取得する
			string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			if (0 < fileName.Length)
			{
				labelFilename.Text = CsvPathname = fileName[0];
				pictureBoxDropZone.Image = global::WonderEstimateExcel.Properties.Resources.CsvFile;
			}
		}

		/// <summary>
		/// 見積書Excelファイル出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExcelOut_Click(object sender, EventArgs e)
		{
			if (0 == CsvPathname.Length)
			{
				MessageBox.Show(this, "見積書CSVファイルを指定してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (!File.Exists(CsvPathname))
			{
				MessageBox.Show(this, string.Format("{0}が見つかりません。", CsvPathname), "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			Cursor preCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;

			EstimateCsv estimate = new EstimateCsv();
			try
			{
				// WonderWeb見積書CSVファイルの読込み
				using (StreamReader sr = new StreamReader(CsvPathname, Encoding.GetEncoding("shift-jis")))
				{
					estimate.ReadCsvFile(sr);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "CSVファイル読込エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			try
			{
				// WonderWeb見積書.xlsx.orgを件名.xlmsにコピー
				string srcPathname = Path.Combine(Directory.GetCurrentDirectory(), "WonderWeb見積書.xlsx.org");
				string dstPathname = Path.Combine(Directory.GetCurrentDirectory(), estimate.Header.GetFilename);
				File.Copy(srcPathname, dstPathname, true);

				using (XLWorkbook wb = new XLWorkbook(dstPathname, XLEventTracking.Disabled))
				{
					// Excelファイルの出力
					estimate.WriteExcelFile(wb);

					// Excelファイルの保存
					wb.SaveAs(dstPathname);

					// 終了メッセージ
					//MessageBox.Show(this, string.Format("{0}を保存しました。", dstPathname), "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Information);

					// Excelの起動
					ProcessStartInfo pInfo = new ProcessStartInfo();
					pInfo.FileName = dstPathname;
					Process.Start(pInfo);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Excelファイル書込エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			Cursor.Current = preCursor;
		}
	}
}
