//
// MainForm.cs
// 
// メイン画面 フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/03/17 勝呂)
// Ver1.03(2023/08/21 勝呂):厚生局データメモ追加機能の追加
// Ver1.04(2024/06/18 勝呂):CSVファイルによるメモ追加機能の追加（企画推進部で使用）
//
using WonderWebEntryMemo.Settings;
using System;
using System.Windows.Forms;

namespace WonderWebEntryMemo.Forms
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
			Program.gSettings = WonderWebEntryMemoSettingsIF.GetSettings();

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

		/// <summary>
		/// 厚生局データメモ追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		// Ver1.03(2023/08/21 勝呂):厚生局データメモ追加機能の追加
		private void buttonWelfare_Click(object sender, EventArgs e)
		{
			using (WelfareForm dlg = new WelfareForm())
			{
				dlg.ShowDialog();
			}
		}

		/// <summary>
		/// CSVファイルインポート
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		// Ver1.04(2024/06/18 勝呂):CSVファイルによるメモ追加機能の追加（企画推進部で使用）
		private void buttonImportFile_Click(object sender, EventArgs e)
		{
			using (ImportCsvFileForm dlg = new ImportCsvFileForm())
			{
				dlg.ShowDialog();
			}
		}
	}
}
