//
// MainForm.cs
//
// MWSサービス異常データ検出 メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/01/29 勝呂):新規作成
// Ver1.02(2024/04/03 勝呂):課金データ作成バッチにならい、申込情報から申込データに参照先を変更
// Ver1.03(2024/04/18 勝呂):受注伝票サービス利用期間不具合検出機能の追加
// Ver1.04(2024/04/26 勝呂):受注伝票サービス利用期間不具合検出機能に検索条件漏れがあったので修正
// 
using CommonLib.BaseFactory.CheckMwsServiceIllegalData;
using CommonLib.Common;
using System;
using System.IO;
using System.Windows.Forms;

namespace CheckMwsServiceIllegalData.Forms
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
			this.Text = string.Format("{0} Ver{1} {2}", Program.gProcName, Program.gVersionStr, Program.gSettings.ConnectCharlie.InstanceName);
		}

		/// <summary>
		/// MWSサービス異常データ出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCheckAbnormalData_Click(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			string xlsPathname = Path.Combine(Directory.GetCurrentDirectory(), string.Format(CheckUseCustomerInfo.ExcelFilename, Date.Today.ToIntYMD()));
			string msg = Program.CheckAbnormalData(xlsPathname, false);

			// カーソルを元に戻す
			Cursor.Current = preCursor;

			if (0 < msg.Length)
			{
				MessageBox.Show(msg, "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			// アプリケーションの終了
			this.Close();
		}

		/// <summary>
		/// 受注伝票サービス利用期間不具合検出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		// Ver1.03(2024/04/18 勝呂):受注伝票サービス利用期間不具合検出機能の追加
		private void buttonCheckIllegalCuiServiceTerm_Click(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			// サービス利用期間が正しくない顧客利用情報の取得
			// Ver1.04(2024/04/26 勝呂):受注伝票サービス利用期間不具合検出機能に検索条件漏れがあったので修正
			int findCount;
			string msg = Program.CheckIllegalCuiServiceTerm(out findCount);
			// カーソルを元に戻す
			Cursor.Current = preCursor;

			if (0 == msg.Length)
			{
				if (0 < findCount)
				{
					MessageBox.Show(string.Format("サービス利用期間が正しくない顧客利用情報が{0}件ありました。メールをご確認ください。", findCount), "報告", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					MessageBox.Show("受注伝票サービス利用期間の不具合はありませんでした。", "正常終了", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			else
			{
				MessageBox.Show(msg, "異常終了", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			// アプリケーションの終了
			this.Close();
		}
	}
}
