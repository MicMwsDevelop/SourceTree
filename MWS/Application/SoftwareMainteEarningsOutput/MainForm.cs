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

namespace SoftwareMainteEarningsOutput
{
	/// <summary>
	/// メイン画面
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// 環境設定
		/// </summary>
		private SoftwareMainteEarningsOutputSettings Settings;

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
			Settings = SoftwareMainteEarningsOutputSettingsIF.GetSettings();
			textBoxFolder.Text = Settings.ExportDir;
			textBoxFilename.Text = Settings.ExportFilename;
			textBoxPcaVer.Text = Settings.PcaVersion.ToString();
			textBoxSlipNo.Text = Settings.SlipInitialNumber.ToString();
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
			Settings.PcaVersion = textBoxPcaVer.ToInt();
			Settings.SlipInitialNumber = textBoxSlipNo.ToInt();

			if (0 < Settings.Pathname.Length)
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 売上データCSVファイルの出力
				string msg = Program.OutputCsvFile(Settings);

				// カーソルを元に戻す
				Cursor.Current = preCursor;

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
			Settings.PcaVersion = textBoxPcaVer.ToInt();
			Settings.SlipInitialNumber = textBoxSlipNo.ToInt();
			SoftwareMainteEarningsOutputSettingsIF.SetSettings(Settings);
		}
	}
}
