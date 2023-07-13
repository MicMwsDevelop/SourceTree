//
// MainForm.cs
// 
// PCA請求データコンバータ メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/27 勝呂):新規作成
//
using ClosedXML.Excel;
using CommonLib.Common;
using PcaInvoiceDataConverter.BaseFactory;
using PcaInvoiceDataConverter.Settings;
using System;
using System.Windows.Forms;

namespace PcaInvoiceDataConverter.Forms
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
			labelVersion.Text = Program.VersionStr;

			try
			{
				//Program.WorkbookPca = new XLWorkbook(Program.ExcelPathname);
				//Program.WorkbookPca.Style.Font.FontName = "メイリオ";
				//Program.WorkbookPca.Style.Font.FontSize = 9;
				//Program.WorkSheetBasic = Program.WorkbookPca.Worksheet(Program.SheetNameBasicData);

				// 今月27日（日曜日は翌日に移動）
				DateTime transferDate = Program.gBasicSheetData.振替日();

				// 今月末日
				DateTime thisLastday = DateTime.Today.EndOfMonth();

				// 先月11日
				Date prevDate = Date.Today.PlusMonths(-1);
				DateTime prev11 = new DateTime(prevDate.Year, prevDate.Month, 11);

				// 今月10日
				DateTime this10 = new DateTime(Date.Today.Year, Date.Today.Month, 10);

				/////////////////////////////////////////////////////////////////////
				// 「基本データ」 口座振替関連基本データ 初期値設定

				// 口座振替日=今月27日
				Program.gBasicSheetData.口座振替日 = transferDate;
				//Program.WS基本データ.Cell(5, 3).Value = transferDate;

				//Program.gBasicSheetData.PCA請求一覧10読込みファイル = Program.WorkSheetBasic.Cell(6, 3).GetString().Trim();
				//Program.gBasicSheetData.APLUS送信ファイル出力フォルダ = Program.WorkSheetBasic.Cell(7, 3).GetString().Trim();

				// APLUS送信ファイル=本日
				Program.gBasicSheetData.APLUS送信ファイル = AgrexDefine.GetAplusSendDataFilename;
				//Program.WS基本データ.Cell(8, 3).Value = AgrexDefine.GetAplusSendDataFilename;


				/////////////////////////////////////////////////////////////////////
				// 「基本データ」 WEB請求書発行関連基本データ 初期値設定

				// WEB請求書番号基数
				//Program.gBasicSheetData.WEB請求書番号基数 = (int)Program.WorkSheetBasic.Cell(16, 3).GetDouble() + 1;

				// 口座振替請求日=今月27日
				Program.gBasicSheetData.口座振替請求日 = transferDate;
				//Program.WS基本データ.Cell(17, 3).Value = Program.gBasicSheetData.口座振替請求日;

				// 口座振替請求期間開始日=先月11日、口座振替請求期間終了日=今月10日
				Program.gBasicSheetData.口座振替請求期間開始日 = prev11;
				Program.gBasicSheetData.口座振替請求期間終了日 = this10;
				//Program.WS基本データ.Cell(18, 3).Value = Program.gBasicSheetData.口座振替請求期間開始日;
				//Program.WS基本データ.Cell(18, 5).Value = Program.gBasicSheetData.口座振替請求期間終了日;

				//Program.gBasicSheetData.PCA請求明細10読込みファイル = Program.WorkSheetBasic.Cell(19, 3).GetString().Trim();
				//Program.gBasicSheetData.WEB請求書ファイル出力フォルダ = Program.WorkSheetBasic.Cell(20, 3).GetString().Trim();
				//Program.gBasicSheetData.WEB請求書ヘッダファイル = Program.WorkSheetBasic.Cell(21, 3).GetString().Trim();
				//Program.gBasicSheetData.WEB請求書明細売上行ファイル = Program.WorkSheetBasic.Cell(22, 3).GetString().Trim();
				//Program.gBasicSheetData.WEB請求書明細消費税行ファイル = Program.WorkSheetBasic.Cell(23, 3).GetString().Trim();
				//Program.gBasicSheetData.WEB請求書明細記事行ファイル = Program.WorkSheetBasic.Cell(24, 3).GetString().Trim();
				//Program.gBasicSheetData.AGREX口振通知書ファイル出力フォルダ = Program.WorkSheetBasic.Cell(25, 3).GetString().Trim();

				// AGREX口振通知書ファイル=本日
				Program.gBasicSheetData.AGREX口振通知書ファイル = AgrexDefine.GetAccountTransferFilename;
				//Program.WS基本データ.Cell(26, 3).Value = Program.gBasicSheetData.AGREX口振通知書ファイル;


				/////////////////////////////////////////////////////////////////////
				// 「基本データ」 銀行振込請求書発行関連基本データ 初期値設定

				// 請求書番号基数
				//Program.gBasicSheetData.請求書番号基数 = (int)Program.WorkSheetBasic.Cell(33, 3).GetDouble() + 1;

				// 銀行振込請求書請求日=本日
				Program.gBasicSheetData.銀行振込請求書請求日 = DateTime.Today;
				//Program.WS基本データ.Cell(34, 3).Value = Program.gBasicSheetData.銀行振込請求書請求日;

				// 銀行振込請求期間開始日=先月11日、銀行振込請求期間終了日=今月10日
				Program.gBasicSheetData.銀行振込請求期間開始日 = prev11;
				Program.gBasicSheetData.銀行振込請求期間終了日 = this10;
				//Program.WS基本データ.Cell(35, 3).Value = Program.gBasicSheetData.銀行振込請求期間開始日;
				//Program.WS基本データ.Cell(35, 5).Value = Program.gBasicSheetData.銀行振込請求期間終了日;

				// 銀行振込入金期限日=今月末日
				Program.gBasicSheetData.銀行振込入金期限日 = thisLastday;
				//Program.WS基本データ.Cell(36, 3).Value = Program.gBasicSheetData.銀行振込入金期限日;

				//Program.gBasicSheetData.PCA請求一覧11読込みファイル = Program.WorkSheetBasic.Cell(37, 3).GetString().Trim();
				//Program.gBasicSheetData.PCA請求明細11読込みファイル = Program.WorkSheetBasic.Cell(38, 3).GetString().Trim();
				//Program.gBasicSheetData.AGREX請求書ファイル出力フォルダ = Program.WorkSheetBasic.Cell(39, 3).GetString().Trim();

				// AGREX請求書ファイル=本日
				Program.gBasicSheetData.AGREX請求書ファイル = AgrexDefine.GetBankTransferFilename;
				//Program.WS基本データ.Cell(40, 3).Value = Program.gBasicSheetData.AGREX請求書ファイル;

				// ワークブックの保存
				//Program.PcaWorkbook.Save();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 口座振替データ作成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAccountTransfer_Click(object sender, EventArgs e)
		{
			using (AccountTransferForm form = new AccountTransferForm())
			{
				form.ShowDialog();
			}
		}

		/// <summary>
		/// 銀行振込請求書データ作成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonBankTransfer_Click(object sender, EventArgs e)
		{
			using (BankTransferForm form = new BankTransferForm())
			{
				form.ShowDialog();
			}
		}

		/// <summary>
		/// 結果データシート保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSaveResultSheet_Click(object sender, EventArgs e)
		{

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

		/// <summary>
		/// Form Closed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Program.gSettings.PCA請求一覧10読込みファイル = Program.gBasicSheetData.PCA請求一覧10読込みファイル;
			Program.gSettings.APLUS送信ファイル出力フォルダ = Program.gBasicSheetData.APLUS送信ファイル出力フォルダ;
			Program.gSettings.WEB請求書番号基数 = Program.gBasicSheetData.WEB請求書番号基数;
			Program.gSettings.PCA請求明細10読込みファイル = Program.gBasicSheetData.PCA請求明細10読込みファイル;
			Program.gSettings.WEB請求書ファイル出力フォルダ = Program.gBasicSheetData.WEB請求書ファイル出力フォルダ;
			Program.gSettings.WEB請求書ヘッダファイル = Program.gBasicSheetData.WEB請求書ヘッダファイル;
			Program.gSettings.WEB請求書明細売上行ファイル = Program.gBasicSheetData.WEB請求書明細売上行ファイル;
			Program.gSettings.WEB請求書明細消費税行ファイル = Program.gBasicSheetData.WEB請求書明細消費税行ファイル;
			Program.gSettings.WEB請求書明細記事行ファイル = Program.gBasicSheetData.WEB請求書明細記事行ファイル;
			Program.gSettings.AGREX口振通知書ファイル出力フォルダ = Program.gBasicSheetData.AGREX口振通知書ファイル出力フォルダ;
			Program.gSettings.請求書番号基数 = Program.gBasicSheetData.請求書番号基数;
			Program.gSettings.PCA請求一覧11読込みファイル = Program.gBasicSheetData.PCA請求一覧11読込みファイル;
			Program.gSettings.PCA請求明細11読込みファイル = Program.gBasicSheetData.PCA請求明細11読込みファイル;
			Program.gSettings.AGREX請求書ファイル出力フォルダ = Program.gBasicSheetData.AGREX請求書ファイル出力フォルダ;
			PcaInvoiceDataConverterSettingsIF.SetSettings(Program.gSettings);
		}
	}
}
