//
// MainForm.cs
//
// 申込情報更新処理 メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using System;
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
		}

		/// <summary>
		/// 1. 顧客情報更新処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonUpdateCustomerInfo_Click(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			Program.ExecCustomerInfo();

			// カーソルを元に戻す
			Cursor.Current = preCursor;

			MessageBox.Show("顧客情報更新処理が終了しました。", "顧客情報更新処理", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// 2. 申込情報更新処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonUpdateApplyInfo_Click(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			Program.ExecApplyInfo();

			// カーソルを元に戻す
			Cursor.Current = preCursor;

			MessageBox.Show("申込情報更新処理が終了しました。", "申込情報更新処理", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
