//
// MainForm.cs
//
// メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/10/28 勝呂)
// 
using System;
using System.Windows.Forms;

namespace HardSubscNotifyMail.Forms
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 利用期限通知
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonNotifyLimitMail_Click(object sender, EventArgs e)
		{
			string msg = Program.NotifyLimitMail();
			if (0 < msg.Length)
			{
				MessageBox.Show(msg, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 利用終了通知
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonNotifyFinishedMail_Click(object sender, EventArgs e)
		{
			string msg = Program.NotifyFinishedMail();
			if (0 < msg.Length)
			{
				MessageBox.Show(msg, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
