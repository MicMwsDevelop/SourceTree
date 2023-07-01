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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
				Program.PcaWorkbook = new XLWorkbook(Program.ExcelPathname);
				Program.SheetBasic = Program.PcaWorkbook.Worksheet(Program.SheetNameBasicData);

				// 初期設定

				// 今月27日
				DateTime transferDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 27);
				if (DayOfWeek.Sunday == transferDate.DayOfWeek)
				{
					// 日曜日は翌日に移動（意味あるのか？）
					transferDate.AddDays(1);
				}
				// 今月末日
				DateTime thisLastday = DateTime.Today.EndOfMonth();

				// 先月11日
				Date prevDate = Date.Today.PlusMonths(-1);
				DateTime prev11 = new DateTime(prevDate.Year, prevDate.Month, 11);

				// 今月10日
				DateTime this10 = new DateTime(Date.Today.Year, Date.Today.Month, 10);

				// 口座振替日=今月27日
				Program.SheetBasic.Cell(5, 3).Value = transferDate;

				// 口座振替請求日=今月27日
				Program.SheetBasic.Cell(17, 3).Value = transferDate;

				// 口座振替請求期間開始日=先月11日、口座振替請求期間終了日=今月10日
				Program.SheetBasic.Cell(18, 3).Value = prev11;
				Program.SheetBasic.Cell(18, 5).Value = this10;

				// 銀行振込請求書請求日=本日
				Program.SheetBasic.Cell(34, 3).Value = DateTime.Today;

				// 銀行振込請求期間開始日=先月11日、銀行振込請求期間終了日=今月10日
				Program.SheetBasic.Cell(35, 3).Value = prev11;
				Program.SheetBasic.Cell(35, 5).Value = this10;

				// 銀行振込入金期限日=今月末日
				Program.SheetBasic.Cell(36, 3).Value = thisLastday;

				// APLUS送信ファイル=本日
				Program.SheetBasic.Cell(8, 3).Value = Program.GetAplusSendDataFilename;

				// AGREX口振通知書ファイル=本日
				Program.SheetBasic.Cell(26, 3).Value = Program.GetAccountTransferFilename;

				// AGREX請求書ファイル=本日
				Program.SheetBasic.Cell(40, 3).Value = Program.GetBankTransferFilename;

				// ワークブックの保存
				Program.PcaWorkbook.Save();

				
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
			AccountTransferForm form = new AccountTransferForm();
			form.ShowDialog();
		}

		/// <summary>
		/// 銀行振込請求書データ作成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonBankTransfer_Click(object sender, EventArgs e)
		{

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
		}
	}
}
