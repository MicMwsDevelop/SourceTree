﻿//
// MainForm.cs
// 
// ハードサブスク売上データ作成 メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// Ver1.00(2025/04/15 勝呂):新規作成
//
using CommonLib.Common;
using HardSubscriptEarningsFile.Settings;
using System;
using System.IO;
using System.Windows.Forms;

namespace HardSubscriptEarningsFile.Forms
{
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
			this.Text = string.Format("{0}  {1}", Program.PROC_NAME, Program.VersionStr);

			textBoxFolder.Text = Program.gSettings.ExportDir;
			textBoxFilename.Text = Program.gSettings.ExportFilename;
			textBoxPcaVer.Text = Program.gSettings.PcaVersion.ToString();
			dateTimePickerMonth.Value = Program.gBootDate.ToDateTime();
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
			HardSubscriptEarningsFileSettingsIF.SetSettings(Program.gSettings);
		}

		private void label6_Click(object sender, EventArgs e)
		{

		}
	}
}
