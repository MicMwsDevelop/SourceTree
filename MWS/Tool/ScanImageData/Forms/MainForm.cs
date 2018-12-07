using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MwsLib.BaseFactory.ScanImageData;
using System.IO;
using MwsLib.DB.SqlServer.ScanImageData;

namespace ScanImageData.Forms
{
	public partial class MainForm : Form
	{
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
			textBoxScanImageDataPath.Text = @"D:\_●営業管理部\●文書インデックス\ScanImageData";
			//textBoxScanImageDataPath.Text = @"\\sqlsv\data保存\ScanImageData";

		}

		/// <summary>
		/// スキャンデータ登録パスの指定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInputPath_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog form = new FolderBrowserDialog())
			{
				form.Description = "フォルダを指定してください。";
				form.RootFolder = Environment.SpecialFolder.Desktop;
				form.SelectedPath = @"C:\Windows";
				form.ShowNewFolderButton = true;
				if (DialogResult.OK == form.ShowDialog(this))
				{
					textBoxScanImageDataPath.Text = form.SelectedPath;
				}
			}
		}

		/// <summary>
		/// スキャンデータ登録情報の再作成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonRemakeScanData_Click(object sender, EventArgs e)
		{
			if (0 == textBoxScanImageDataPath.Text.Length)
			{
				return;
			}
			if (false == Directory.Exists(textBoxScanImageDataPath.Text))
			{
				return;
			}
			if (DialogResult.No == MessageBox.Show("スキャンデータ登録情報を再作成します。よろしいですか？", "再作成", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				return;
			}
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			string msg;
			int ret = Program.RemakeScanImageData(textBoxScanImageDataPath.Text, out msg);
			if (-1 == ret)
			{
				MessageBox.Show(msg, "再作成", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			else if (0 < ret)
			{
				MessageBox.Show("スキャンデータ登録情報の再作成が終了しました。", "再作成", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("登録するスキャンデータはありませんでした。", "再作成", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}
	}
}
