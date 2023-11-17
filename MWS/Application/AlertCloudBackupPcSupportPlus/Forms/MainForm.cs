//
// MainForm.cs
//
// メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/02/05 勝呂)
//
using System;
using System.Windows.Forms;

namespace AlertCloudBackupPcSupportPlus.Forms
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
			this.Text = string.Format("{0} ({1})", Program.PROC_NAME, Program.VersionStr);
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			string msg;
			int ret = Program.AlartCheck(out msg);
			if (-1 == ret)
			{
				MessageBox.Show(msg, Program.PROC_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else if (0 < ret)
			{
				MessageBox.Show("アラートメールを送信しました。", Program.PROC_NAME, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBox.Show("アラートはありませんでした。", Program.PROC_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}
}
