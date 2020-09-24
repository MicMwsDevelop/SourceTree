//
// MainForm.cs
// 
// メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/09/23 勝呂)
//
using ThermometerOrderOutput.Settings;
using MwsLib.Common;
using System;
using System.IO;
using System.Windows.Forms;

namespace ThermometerOrderOutput.Forms
{
	/// <summary>
	/// メイン画面
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// 環境設定
		/// </summary>
		private ThermometerOrderOutputSettings Settings;

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
			Settings = ThermometerOrderOutputSettingsIF.GetSettings();
			textBoxFolder.Text = Settings.ExportDir;

			if (Settings.OrderDate.HasValue)
			{
				dateTimePickerOrderDate.Value = Date.Parse(Settings.OrderDate.Value).ToDateTime();
			}
			else
			{
				dateTimePickerOrderDate.Value = DateTime.Today;
			}
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
			Settings.OrderDate = new Date(dateTimePickerOrderDate.Value).ToIntYMD();

			if (0 < Settings.Pathname.Length)
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 皮膚赤外線体温計受注明細ファイルの出力
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
			Settings.ExportDir = textBoxFolder.Text;
			Settings.OrderDate = new Date(dateTimePickerOrderDate.Value).ToIntYMD();
		}
	}
}
