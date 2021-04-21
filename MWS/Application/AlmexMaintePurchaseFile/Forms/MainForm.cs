//
// MainForm.cs
// 
// メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/01/20 勝呂)
//
using AlmexMaintePurchaseFile.Settings;
using MwsLib.Common;
using System;
using System.IO;
using System.Windows.Forms;

namespace AlmexMaintePurchaseFile.Forms
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

			dateTimePickerMonth.Value = Program.CollectDate.ToDateTime();
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
			Program.gSettings.ExportDir = textBoxFolder.Text;
			Program.gSettings.ExportFilename = textBoxFilename.Text;
			Program.gSettings.PcaVersion = textBoxPcaVer.ToInt();
			if (0 < Program.gSettings.Pathname.Length)
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 仕入データCSVファイルの出力
				string msg = Program.OutputCsvFile(new Date(dateTimePickerMonth.Value));

				// カーソルを元に戻す
				Cursor.Current = preCursor;
				if (0 == msg.Length)
				{
					MessageBox.Show(string.Format("{0}を出力しました。", Program.gSettings.Pathname), "出力成功", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					MessageBox.Show(string.Format("{0}\nERROR.LOGを確認してください。", msg), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				Program.gSettings.ExportDir = textBoxFolder.Text;
			}
			Program.gSettings.ExportFilename = textBoxFilename.Text;
			Program.gSettings.PcaVersion = textBoxPcaVer.ToInt();
			AlmexMaintePurchaseFileSettingsIF.SetSettings(Program.gSettings);
		}
	}
}
