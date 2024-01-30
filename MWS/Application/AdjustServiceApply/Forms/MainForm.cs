//
// MainForm.cs
//
// 申込情報更新処理 メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.DB.SqlServer.AdjustServiceApply;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AdjustServiceApply.Forms
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
		/// Load Form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			this.Text = string.Format("{0} Ver{1}", Program.gProcName, Program.gVersionStr);

			T_FILE_CREATEDATE customer = AdjustServiceApplyAccess.GetLastSynchroTimeForCustomer(Program.gSettings.ConnectCharlie.ConnectionString);
			T_FILE_CREATEDATE service = AdjustServiceApplyAccess.GetLastSynchroTimeForService(Program.gSettings.ConnectCharlie.ConnectionString);
			dateTimePickerCustomer.Value = customer.FILE_CREATEDATE;
			dateTimePickerApply.Value = service.FILE_CREATEDATE;

			string[] files = GetLogFiles("顧客情報更新_*.log");
			listBoxCustomerLog.Items.AddRange(files);

			files = GetLogFiles("申込情報更新_*.log");
			listBoxApplyLog.Items.AddRange(files);
		}

		/// <summary>
		/// ログファイルを最大20個まで降順に取得
		/// </summary>
		/// <param name="prefixName"></param>
		/// <returns></returns>
		private string[] GetLogFiles(string prefixName)
		{
			string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), prefixName, SearchOption.TopDirectoryOnly);
			Array.Reverse(files);

			string[] ret = new string[(files.Length > Program.LOGFILE_COUNT) ? Program.LOGFILE_COUNT : files.Length];
			for (int i = 0; i < files.Length; i++)
			{
				// ファイル名のみ取得
				ret[i] = Path.GetFileName(files[i]);
			}
			return ret;
		}

		/// <summary>
		/// 1. 顧客情報更新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonUpdateCustomerInfo_Click(object sender, EventArgs e)
		{
			if (DialogResult.No == MessageBox.Show("顧客情報更新を行います。よろしいですか？", "顧客情報更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				return;
			}
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			bool ret = Program.ExecCustomerInfo();

			// カーソルを元に戻す
			Cursor.Current = preCursor;

			if (false == ret)
			{
				MessageBox.Show("顧客情報更新 異常終了", "顧客情報更新", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			MessageBox.Show("顧客情報更新 正常終了", "顧客情報更新", MessageBoxButtons.OK, MessageBoxIcon.Information);

			string[] files = GetLogFiles("顧客情報更新_*.log");
			listBoxCustomerLog.Items.Clear();
			listBoxCustomerLog.Items.AddRange(files);

			// 顧客情報更新ログを開く
			using (Process process = new Process())
			{
				process.StartInfo.FileName = Program.gCustomerInfoLogPathname;
				process.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
				process.Start();
			}
		}

		/// <summary>
		/// 2. 申込情報更新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonUpdateApplyInfo_Click(object sender, EventArgs e)
		{
			if (DialogResult.No == MessageBox.Show("顧客情報更新後、申込情報更新を行います。よろしいですか？", "申込情報更新", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				return;
			}
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			// 顧客情報更新
			bool ret = Program.ExecCustomerInfo();
			if (false == ret)
			{
				MessageBox.Show("顧客情報更新 異常終了。申込情報更新は中止します。", "顧客情報更新", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			// 申込情報更新
			ret = Program.ExecApplyInfo();

			// カーソルを元に戻す
			Cursor.Current = preCursor;

			if (false == ret)
			{
				MessageBox.Show("申込情報更新 異常終了", "申込情報更新", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			MessageBox.Show("申込情報更新 正常終了", "申込情報更新", MessageBoxButtons.OK, MessageBoxIcon.Information);

			string[] files = GetLogFiles("申込情報更新_*.log");
			listBoxApplyLog.Items.Clear();
			listBoxApplyLog.Items.AddRange(files);

			// 申込情報更新ログを開く
			using (Process process = new Process())
			{
				process.StartInfo.FileName = Program.gApplyInfoLogPathname;
				process.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
				process.Start();
			}
		}

		/// <summary>
		/// 顧客情報更新ログ参照
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBoxCustomerLog_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (-1 != listBoxCustomerLog.SelectedIndex)
			{
				string pathname = Path.Combine(Directory.GetCurrentDirectory(), listBoxCustomerLog.SelectedItem.ToString());
				using (Process process = new Process())
				{
					process.StartInfo.FileName = pathname;
					process.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
					process.Start();
				}
			}
		}

		/// <summary>
		/// 申込情報更新ログ参照
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBoxApplyLog_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (-1 != listBoxApplyLog.SelectedIndex)
			{
				string pathname = Path.Combine(Directory.GetCurrentDirectory(), listBoxApplyLog.SelectedItem.ToString());
				using (Process process = new Process())
				{
					process.StartInfo.FileName = pathname;
					process.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
					process.Start();
				}
			}
		}

		/// <summary>
		/// 終了
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
