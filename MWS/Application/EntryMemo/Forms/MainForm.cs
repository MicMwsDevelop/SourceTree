//
// MainForm.cs
// 
// メイン画面 フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/03/17 勝呂)
//
using EntryMemo.Settings;
using System;
using System.Windows.Forms;

namespace EntryMemo.Forms
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
			// 環境設定の読込
			Program.gSettings = EntryMemoSettingsIF.GetSettings();

			// バージョン情報設定
			labelVersion.Text = Program.ProgramVersion;
		}

		/// <summary>
		/// 銀行振込請求書発行メモ追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonBank_Click(object sender, EventArgs e)
		{
			using (BankForm dlg = new BankForm())
			{
				dlg.ShowDialog();
			}
		}

		/// <summary>
		/// オン資格補助金申請書類メモ追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOnline_Click(object sender, EventArgs e)
		{
			using (OnlineForm dlg = new OnlineForm())
			{
				dlg.ShowDialog();
			}
		}
	}
}
