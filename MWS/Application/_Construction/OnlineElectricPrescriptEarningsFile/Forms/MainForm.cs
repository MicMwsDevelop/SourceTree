//
// MainForm.cs
//
// オン資電子処方箋売上データ作成 メイン画面フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/08/27 勝呂):新規作成
//
using CommonLib.Common;
using System;
using System.IO;
using System.Windows.Forms;

namespace OnlineElectricPrescriptEarningsFile.Forms
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
			this.Text = string.Format("{0}  {1} {2}", Program.gProcName, Program.gVersionStr, Program.gSettings.ConnectJunp.InstanceName);

			dateTimePickerMonth.Value = Program.gBootDate.ToDateTime();
			textBoxFolder.Text = Program.gSettings.ExportDir;
			textBoxExportFilename.Text = Program.gSettings.ExportFilename;
			textBoxPcaVer.Text = Program.gSettings.PcaVersion.ToString();
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
			if (0 == textBoxExportFilename.Text.Trim().Length)
			{
				MessageBox.Show("売上データファイル名が指定されていません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (-1 != textBoxExportFilename.Text.IndexOf('.'))
			{
				MessageBox.Show("売上データファイル名に拡張子を指定しないでください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			Program.gSettings.ExportDir = textBoxFolder.Text;
			Program.gSettings.ExportFilename = textBoxExportFilename.Text.Trim();
			Program.gSettings.PcaVersion = textBoxPcaVer.ToInt();

			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			// 売上データファイルの出力
			string msg = Program.Exec(new Date(dateTimePickerMonth.Value));

			// カーソルを元に戻す
			Cursor.Current = preCursor;

			if (0 == msg.Length)
			{
				MessageBox.Show("売上データファイルを出力しました。", "出力", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show(msg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			this.Close();
		}
	}
}
