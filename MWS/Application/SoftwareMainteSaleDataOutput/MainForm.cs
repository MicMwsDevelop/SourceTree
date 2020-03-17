//
// MainForm.cs
// 
// メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/03/16 勝呂)
//
using System;
using System.IO;
using System.Windows.Forms;

namespace SoftwareMainteSaleDataOutput
{
	/// <summary>
	/// メイン画面
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// 環境設定
		/// </summary>
		private SoftwareMainteSaleDataOutputSettings Settings;

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
			Settings = SoftwareMainteSaleDataOutputSettingsIF.GetSettings();
			textBoxFolder.Text = Settings.ExportDir;
			textBoxFilename.Text = Settings.ExportFilename;
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			if (!Directory.Exists(textBoxFolder.Text))
			{
				MessageBox.Show("出力先が存在しません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			Settings.ExportDir = textBoxFolder.Text;
			Settings.ExportFilename = textBoxFilename.Text;
			if (0 < Settings.Pathname.Length)
			{
				string msg = Program.OutputCsvFile(Settings.Pathname);
				if (0 == msg.Length)
				{
					MessageBox.Show(string.Format("{0}を出力しました。", Settings.Pathname), "出力成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					MessageBox.Show(msg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show("出力先が設定されていません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		/// <summary>
		/// Form Closing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Directory.Exists(textBoxFolder.Text))
			{
				Settings.ExportDir = textBoxFolder.Text;
			}
			Settings.ExportFilename = textBoxFilename.Text;
			SoftwareMainteSaleDataOutputSettingsIF.SetSettings(Settings);
		}
	}
}
