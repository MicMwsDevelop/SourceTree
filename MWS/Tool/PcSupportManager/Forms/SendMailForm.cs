﻿//
// SendMailForm.cs
//
// PC安心サポート管理 メール送信画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/11/19 勝呂)
// 
using DataGridViewAutoFilter;
using MwsLib.BaseFactory.PcSupportManager;
using MwsLib.Common;
using MwsLib.DB.SqlServer.PcSupportManager;
using PcSupportManager.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PcSupportManager.Forms
{
	/// <summary>
	/// メール送信画面
	/// </summary>
	public partial class SendMailForm : Form
	{
		/// <summary>
		/// PC安心サポート管理情報リスト DataSource
		/// </summary>
		private BindingSource dataGridViewMailBindingSource;

		/// <summary>
		///  PC安心サポート管理情報リスト
		/// </summary>
		private List<PcSupportControl> PcSupportControlList;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SendMailForm()
		{
			InitializeComponent();

			dataGridViewMailBindingSource = null;
			PcSupportControlList = null;
			dataGridViewMail.BindingContextChanged += new EventHandler(dataGridViewMail_BindingContextChanged);
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SendMailForm_Load(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			DataTable dataTable = PcSupportManagerGetIO.GetPcSupportControl();
			PcSupportControlList = PcSupportManagerController.ConvertPcSupportControl(dataTable);

			// チェックボックス列を作成
			DataColumn column = new DataColumn("SEND_MAIL", typeof(Boolean));

			// DataTableにチェックボックス列を追加
			dataTable.Columns.Add(column);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				// 値を予め格納していないと参照時にエラーになる
				dataTable.Rows[i]["SEND_MAIL"] = false;
			}
			dataGridViewMailBindingSource = new BindingSource(dataTable, null);
			dataGridViewMail.DataSource = dataGridViewMailBindingSource;

			// カラム名の変更
			dataGridViewMail.Columns["SEND_MAIL"].HeaderText = "送信対象";
			dataGridViewMail.Columns["ORDER_NO"].HeaderText = "受注No";
			dataGridViewMail.Columns["CUSTOMER_ID"].HeaderText = "顧客No";
			dataGridViewMail.Columns["CLINIC_NAME"].HeaderText = "医院名";
			dataGridViewMail.Columns["GOODS_NAME"].HeaderText = "商品名";
			dataGridViewMail.Columns["START_DATE"].HeaderText = "契約開始日";
			dataGridViewMail.Columns["END_DATE"].HeaderText = "契約終了日";
			dataGridViewMail.Columns["BRANCH_NAME"].HeaderText = "拠店名";
			dataGridViewMail.Columns["SALESMAN_NAME"].HeaderText = "担当者名";
			dataGridViewMail.Columns["ORDER_DATE"].HeaderText = "受注日";
			dataGridViewMail.Columns["ORDER_APPROVAL_DATE"].HeaderText = "受注承認日";
			dataGridViewMail.Columns["MAIL_ADDRESS"].HeaderText = "メールアドレス";
			dataGridViewMail.Columns["START_MAIL_DATE"].HeaderText = "開始メール送信日時";
			dataGridViewMail.Columns["GUIDE_MAIL_DATE"].HeaderText = "契約更新案内メール送信日時";
			dataGridViewMail.Columns["UPDATE_MAIL_DATE"].HeaderText = "契約更新メール送信日時";

			// 非表示
			dataGridViewMail.Columns["GOODS_ID"].Visible = false;
			dataGridViewMail.Columns["PRICE"].Visible = false;
			dataGridViewMail.Columns["AGREE_YEAR"].Visible = false;
			dataGridViewMail.Columns["PERIOD_END_DATE"].Visible = false;
			dataGridViewMail.Columns["BRANCH_ID"].Visible = false;
			dataGridViewMail.Columns["SALESMAN_ID"].Visible = false;
			dataGridViewMail.Columns["ORDER_REPORT_ACCEPT"].Visible = false;
			dataGridViewMail.Columns["REMARK"].Visible = false;
			dataGridViewMail.Columns["CANCEL_DATE"].Visible = false;
			dataGridViewMail.Columns["CANCEL_REPORT_ACCEPT"].Visible = false;
			dataGridViewMail.Columns["CANCEL_REASON"].Visible = false;
			dataGridViewMail.Columns["DISABLE_FLAG"].Visible = false;
			dataGridViewMail.Columns["CREATE_DATE"].Visible = false;
			dataGridViewMail.Columns["CREATE_PERSON"].Visible = false;
			dataGridViewMail.Columns["UPDATE_DATE"].Visible = false;
			dataGridViewMail.Columns["UPDATE_PERSON"].Visible = false;

			// 表示順の変更
			dataGridViewMail.Columns["SEND_MAIL"].DisplayIndex = 0;
			dataGridViewMail.Columns["ORDER_NO"].DisplayIndex = 1;
			dataGridViewMail.Columns["CUSTOMER_ID"].DisplayIndex = 2;
			dataGridViewMail.Columns["CLINIC_NAME"].DisplayIndex = 3;
			dataGridViewMail.Columns["GOODS_NAME"].DisplayIndex = 4;
			dataGridViewMail.Columns["START_DATE"].DisplayIndex = 5;
			dataGridViewMail.Columns["END_DATE"].DisplayIndex = 6;
			dataGridViewMail.Columns["BRANCH_NAME"].DisplayIndex = 7;
			dataGridViewMail.Columns["SALESMAN_NAME"].DisplayIndex = 8;
			dataGridViewMail.Columns["ORDER_DATE"].DisplayIndex = 9;
			dataGridViewMail.Columns["ORDER_APPROVAL_DATE"].DisplayIndex = 10;
			dataGridViewMail.Columns["MAIL_ADDRESS"].DisplayIndex = 11;
			dataGridViewMail.Columns["START_MAIL_DATE"].DisplayIndex = 12;
			dataGridViewMail.Columns["GUIDE_MAIL_DATE"].DisplayIndex = 13;
			dataGridViewMail.Columns["UPDATE_MAIL_DATE"].DisplayIndex = 14;

			// 編集不可
			dataGridViewMail.Columns["ORDER_NO"].ReadOnly = true;
			dataGridViewMail.Columns["CUSTOMER_ID"].ReadOnly = true;
			dataGridViewMail.Columns["CLINIC_NAME"].ReadOnly = true;
			dataGridViewMail.Columns["GOODS_NAME"].ReadOnly = true;
			dataGridViewMail.Columns["START_DATE"].ReadOnly = true;
			dataGridViewMail.Columns["END_DATE"].ReadOnly = true;
			dataGridViewMail.Columns["BRANCH_NAME"].ReadOnly = true;
			dataGridViewMail.Columns["SALESMAN_NAME"].ReadOnly = true;
			dataGridViewMail.Columns["ORDER_DATE"].ReadOnly = true;
			dataGridViewMail.Columns["ORDER_APPROVAL_DATE"].ReadOnly = true;
			dataGridViewMail.Columns["MAIL_ADDRESS"].ReadOnly = true;
			dataGridViewMail.Columns["START_MAIL_DATE"].ReadOnly = true;
			dataGridViewMail.Columns["GUIDE_MAIL_DATE"].ReadOnly = true;
			dataGridViewMail.Columns["UPDATE_MAIL_DATE"].ReadOnly = true;

			dataGridViewMail.ResumeLayout();

			// メール送信種別 すべて
			radioButtonAll.Checked = true;

			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}

		/// <summary>
		/// Configures the autogenerated columns, replacing their header
		/// cells with AutoFilter header cells. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridViewMail_BindingContextChanged(object sender, EventArgs e)
		{
			// Continue only if the data source has been set.
			if (dataGridViewMail.DataSource == null)
			{
				return;
			}
			// Add the AutoFilter header cell to each column.
			foreach (DataGridViewColumn col in dataGridViewMail.Columns)
			{
				col.HeaderCell = new DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
			}
			// Resize the columns to fit their contents.
			//dataGridViewUser.AutoResizeColumns();
		}

		/// <summary>
		/// メール送信種別 すべて
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonAll_CheckedChanged(object sender, EventArgs e)
		{
			dataGridViewMailBindingSource.Filter = @"DISABLE_FLAG = '0'";

			// 送信対象
			textBoxSpan.Text = string.Empty;

			// 送信ボタン無効
			buttonSend.Enabled = false;

			// レコード件数の表示
			textBoxCount.Text = string.Format("{0}/{1}", dataGridViewMailBindingSource.Count, PcSupportControlList.Count);
		}

		/// <summary>
		/// メール送信種別 開始メール
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonStartMail_CheckedChanged(object sender, EventArgs e)
		{
			// 契約開始日が当月末以前
			Date limit = Program.SystemDate.ToYearMonth().Last;
			dataGridViewMailBindingSource.Filter = string.Format(@"DISABLE_FLAG = '0' AND 0 < LEN(MAIL_ADDRESS) AND START_MAIL_DATE is null AND ORDER_APPROVAL_DATE is not null AND START_DATE is not null AND START_DATE <= '{0}'", limit.ToSqlDateTimeString());

			// 送信対象
			textBoxSpan.Text = string.Format("契約開始日が{0}以前", limit.ToString());

			// 送信ボタン有効
			buttonSend.Enabled = true;

			// レコード件数の表示
			textBoxCount.Text = string.Format("{0}/{1}", dataGridViewMailBindingSource.Count, PcSupportControlList.Count);
		}

		/// <summary>
		/// メール送信種別 契約更新案内メール
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonMailGuide_CheckedChanged(object sender, EventArgs e)
		{
			// 契約終了月が翌月
			Date limit = Program.SystemDate.PlusMonths(1).ToYearMonth().Last;
			dataGridViewMailBindingSource.Filter = string.Format(@"DISABLE_FLAG = '0' AND 0 < LEN(MAIL_ADDRESS) AND GUIDE_MAIL_DATE is null AND ORDER_APPROVAL_DATE is not null AND END_DATE is not null AND END_DATE = '{0}'", limit.ToSqlDateTimeString());

			// 送信対象
			textBoxSpan.Text = string.Format("契約終了月が{0}", limit.ToString());

			// 送信ボタン有効
			buttonSend.Enabled = true;

			// レコード件数の表示
			textBoxCount.Text = string.Format("{0}/{1}", dataGridViewMailBindingSource.Count, PcSupportControlList.Count);
		}

		/// <summary>
		/// メール送信種別 契約更新メール
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonMailUpdate_CheckedChanged(object sender, EventArgs e)
		{
			// 契約終了月が先月
			Date limit = Program.SystemDate.PlusMonths(-1).ToYearMonth().Last;
			dataGridViewMailBindingSource.Filter = string.Format(@"DISABLE_FLAG = '0' AND 0 < LEN(MAIL_ADDRESS) AND GUIDE_MAIL_DATE is null AND ORDER_APPROVAL_DATE is not null AND END_DATE is not null AND END_DATE = '{0}'", limit.ToSqlDateTimeString());

			// 送信対象
			textBoxSpan.Text = string.Format("契約終了月が{0}", limit.ToString());

			// 送信ボタン有効
			buttonSend.Enabled = true;

			// レコード件数の表示
			textBoxCount.Text = string.Format("{0}/{1}", dataGridViewMailBindingSource.Count, PcSupportControlList.Count);
		}

		/// <summary>
		/// メール送信
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSend_Click(object sender, EventArgs e)
		{
			List<PcSupportMail> mailList = new List<PcSupportMail>();
			DateTime today = Program.SystemDate.ToDateTime();
			for (int i = 0; i < dataGridViewMail.Rows.Count; i++)
			{
				// SEND_MAIL
				if (true == (bool)dataGridViewMail.Rows[i].Cells["SEND_MAIL"].Value)
				{
					// ORDER_NO
					string orderNo = (string)dataGridViewMail.Rows[i].Cells["ORDER_NO"].Value;
					PcSupportControl pc = PcSupportControlList.Find(p => p.OrderNo == orderNo);
					if (null != pc)
					{
						if (radioButtonStartMail.Checked)
						{
							// 開始メール
							pc.StartMailDateTime = today;
							pc.UpdateDateTime = today;
							pc.UpdatePerson = Program.PROGRAM_NAME;
							PcSupportMail mail = new PcSupportMail(PcSupportMail.MailType.Start, pc, Program.SystemDate);
							mailList.Add(mail);
							try
							{
								// 開始メール送信処理
								SendMailControl.SendStartMail(mail, pc.ClinicName);
							}
							catch (Exception ex)
							{
								MessageBox.Show(string.Format("SendMailControl.SendStartMail() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
								return;
							}
							try
							{
								PcSupportManagerAccess.SetPcSupportControl(pc);
							}
							catch (Exception ex)
							{
								MessageBox.Show(string.Format("PcSupportManagerAccess.SetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
								return;
							}
						}
						else if (radioButtonMailGuide.Checked)
						{
							// 契約更新案内メール

							// メール送信前データ格納
							pc.GuideMailDateTime = today;
							pc.UpdateDateTime = today;
							pc.UpdatePerson = Program.PROGRAM_NAME;

							// メール送信処理
							PcSupportMail mail = new PcSupportMail(PcSupportMail.MailType.Start, pc, Program.SystemDate);
							mailList.Add(mail);
							try
							{
								SendMailControl.SendGuideMail(mail, pc.ClinicName);
							}
							catch (Exception ex)
							{
								MessageBox.Show(string.Format("SendMailControl.SendStartMail() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
								return;
							}
							// メール送信後データ格納
							pc.UpdateMailDateTime = null;
							try
							{
								PcSupportManagerAccess.SetPcSupportControl(pc);
							}
							catch (Exception ex)
							{
								MessageBox.Show(string.Format("PcSupportManagerAccess.SetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
								return;
							}
						}
						else
						{
							// 契約更新メール
							string goodsName = "PC安心ｻﾎﾟｰﾄ(1年契約)";
							PcSupportGoodsInfo goods = MainForm.gPcSupportGoodsList.Find(p => p.GoodsID == PcSupportGoodsInfo.PC_SUPPORT1_GOODS_ID);
							if (null != goods)
							{
								goodsName = goods.GoodsName;
							}
							// メール送信前データ格納
							pc.UpdateMailDateTime = today;
							pc.UpdateDateTime = today;
							pc.UpdatePerson = Program.PROGRAM_NAME;

							// メール送信処理
							PcSupportMail mail = new PcSupportMail(PcSupportMail.MailType.Start, pc, Program.SystemDate);
							mailList.Add(mail);
							try
							{
								SendMailControl.SendUpdateMail(mail, pc.ClinicName);
							}
							catch (Exception ex)
							{
								MessageBox.Show(string.Format("SendMailControl.SendStartMail() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
								return;
							}
							// メール送信後データ格納
							pc.GuideMailDateTime = null;
							pc.GoodsID = PcSupportGoodsInfo.PC_SUPPORT1_GOODS_ID;
							pc.GoodsName = goodsName;
							pc.AgreeYear = 1;
							pc.StartDate = pc.EndDate.Value.PlusMonths(1).ToYearMonth().First;
							pc.EndDate = PcSupportControl.GetEndDate(pc.StartDate.Value, pc.AgreeYear);
							try
							{
								PcSupportManagerAccess.SetPcSupportControl(pc);
							}
							catch (Exception ex)
							{
								MessageBox.Show(string.Format("PcSupportManagerAccess.SetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
								return;
							}
						}
					}
				}
			}
			if (0 < mailList.Count)
			{
				try
				{
					PcSupportManagerAccess.InsertIntoPcSupportMailList(mailList);
				}
				catch (Exception ex)
				{
					MessageBox.Show(string.Format("PcSupportManagerAccess.InsertIntoPcSupportMailList() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return;
				}
				MessageBox.Show(string.Format("{0}件のメールを送信しました。", mailList.Count), "メール送信", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("メール送信対象はありませんでした。", "メール送信", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}
}