//
// MainForm.cs
// 
// 文書インデックス管理 メイン画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/12/13 勝呂)
//
using System;
using System.IO;
using System.Windows.Forms;

namespace ScanImageData.Forms
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		private MainForm()
		{
			InitializeComponent();

		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="scanPath"></param>
		public MainForm(string scanPath)
		{
			InitializeComponent();
			textBoxScanImageDataPath.Text = scanPath;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
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

			int ret = Program.RemakeScanImageData(textBoxScanImageDataPath.Text);
			if (-1 == ret)
			{
				MessageBox.Show("スキャンデータ登録情報の再作成エラー", "再作成", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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

		private void buttonMakeIndexFile_Click(object sender, EventArgs e)
		{
			using (MakeIndexFileForm form = new MakeIndexFileForm())
			{
				form.ShowDialog();
			}
		}
	}
}
