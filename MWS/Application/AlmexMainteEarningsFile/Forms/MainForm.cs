//
// MainForm.cs
// 
// メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/11/24 勝呂)
//
using MwsLib.Common;
using AlmexMainteEarningsFile.Settings;
using System;
using System.IO;
using System.Windows.Forms;

namespace AlmexMainteEarningsFile.Forms
{
	/// <summary>
	/// メイン画面
	/// </summary>
	public partial class MainForm : Form
	{
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
			textBoxFolder.Text = Program.gSettings.ExportDir;
			textBoxFilename.Text = Program.gSettings.ExportFilename;
			textBoxPcaVer.Text = Program.gSettings.PcaVersion.ToString();

			dateTimePickerMonth.Value = Program.gSaleDate.ToDateTime();
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
			if (-1 != textBoxFilename.Text.IndexOf('.'))
			{
				MessageBox.Show("売上データファイル名に拡張子を指定しないでください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			Program.gSettings.ExportDir = textBoxFolder.Text;
			Program.gSettings.ExportFilename = textBoxFilename.Text;
			Program.gSettings.PcaVersion = textBoxPcaVer.ToInt();

			if (0 < Program.gSettings.ExportFilename.Length)
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 売上データCSVファイルの出力
				string msg = Program.OutputCsvFile(new Date(dateTimePickerMonth.Value));

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				if (0 == msg.Length)
				{
					MessageBox.Show(string.Format("{0}を出力しました。", Program.gSettings.FormalPathname(Program.gFormalFilename)), "出力成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					MessageBox.Show(msg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				MessageBox.Show("売上データファイル名が設定されていません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
				Program.gSettings.ExportDir = textBoxFolder.Text;
			}
			Program.gSettings.ExportFilename = textBoxFilename.Text;
			Program.gSettings.PcaVersion = textBoxPcaVer.ToInt();
			AlmexMainteEarningsFileSettingsIF.SetSettings(Program.gSettings);
		}
	}
}
