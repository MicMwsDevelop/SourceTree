using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MwsLib.DB.SqlServer.PcSafetySupport;
using MwsLib.BaseFactory.PcSafetySupport;
using PcSafetySupport.Mail;
using MwsLib.Common;

namespace PcSafetySupport.Forms
{
	public partial class SendMailForm : Form
	{
		/// <summary>
		/// PC安心サポート管理情報リスト
		/// </summary>
		private List<PcSupportControl> PcSupportControlList;

		private BindingSource DataGridViewMailBindingSource;

		public SendMailForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SendMailForm_Load(object sender, EventArgs e)
		{
			radioButtonStartMail.Checked = true;
		}

		/// <summary>
		/// 開始メール
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonStartMail_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				dataGridViewMail.DataSource = null;
				dataGridViewMail.Rows.Clear();
				dataGridViewMail.Columns.Clear();
				dataGridViewMail.ReadOnly = true;

				DataTable dataTable = PcSafetySupportAccess.GetPcSupportControl("", Program.SQLSV2);
				DataGridViewMailBindingSource = new BindingSource(dataTable, null);
				DataGridViewMailBindingSource.Filter = "START_MAIL_DATE is null";
				dataGridViewMail.DataSource = DataGridViewMailBindingSource;

				// カラム名の変更
				dataGridViewMail.Columns["ORDER_NO"].HeaderText = "受注No";
				dataGridViewMail.Columns["CUSTOMER_ID"].HeaderText = "顧客No";
				dataGridViewMail.Columns["CLINIC_NAME"].HeaderText = "医院名";
				dataGridViewMail.Columns["GOODS_ID"].HeaderText = "商品ID";
				dataGridViewMail.Columns["START_DATE"].HeaderText = "契約開始日";
				dataGridViewMail.Columns["END_DATE"].HeaderText = "契約終了日";
				dataGridViewMail.Columns["PERIOD_END_DATE"].HeaderText = "利用期限日";
				dataGridViewMail.Columns["AGREE_YEAR"].HeaderText = "契約年数";
				dataGridViewMail.Columns["PRICE"].HeaderText = "料金";
				dataGridViewMail.Columns["MARKETING_SPECIALIST_ID"].HeaderText = "営業担当者ID";
				dataGridViewMail.Columns["BRANCH_ID"].HeaderText = "支店ID";
				dataGridViewMail.Columns["ORDER_DATE"].HeaderText = "受注日";
				dataGridViewMail.Columns["ORDER_REPORT_ACCEPT"].HeaderText = "申込用紙有無";
				dataGridViewMail.Columns["ORDER_APPROVAL_DATE"].HeaderText = "受注承認日";
				dataGridViewMail.Columns["MAIL_ADDRESS"].HeaderText = "メールアドレス";
				dataGridViewMail.Columns["REMARK1"].HeaderText = "備考１";
				dataGridViewMail.Columns["REMARK2"].HeaderText = "備考２";
				dataGridViewMail.Columns["START_MAIL_DATE"].HeaderText = "開始メール送信日時";
				dataGridViewMail.Columns["GUIDE_MAIL_DATE"].HeaderText = "契約更新案内メール送信日時";
				dataGridViewMail.Columns["UPDATE_MAIL_DATE"].HeaderText = "契約更新メール送信日時";
				dataGridViewMail.Columns["CANCEL_DATE"].HeaderText = "解約日時";
				dataGridViewMail.Columns["CANCEL_REPORT_ACCEPT"].HeaderText = "解約届有無";
				dataGridViewMail.Columns["DISABLE_FLAG"].HeaderText = "無効フラグ";
				dataGridViewMail.Columns["CANCEL_REASON"].HeaderText = "解約事由";
				dataGridViewMail.Columns["WW_RENEWAL_FLAG"].HeaderText = "WonderWeb更新フラグ";
				dataGridViewMail.Columns["CREATE_DATE"].HeaderText = "作成日時";
				dataGridViewMail.Columns["CREATE_PERSON"].HeaderText = "作成者";
				dataGridViewMail.Columns["UPDATE_DATE"].HeaderText = "更新日時";
				dataGridViewMail.Columns["UPDATE_PERSON"].HeaderText = "更新者";

				// 表示順の変更
				dataGridViewMail.Columns["ORDER_NO"].DisplayIndex = 0;
				dataGridViewMail.Columns["CUSTOMER_ID"].DisplayIndex = 1;
				dataGridViewMail.Columns["CLINIC_NAME"].DisplayIndex = 2;
				dataGridViewMail.Columns["GOODS_ID"].DisplayIndex = 3;
				dataGridViewMail.Columns["START_DATE"].DisplayIndex = 4;
				dataGridViewMail.Columns["END_DATE"].DisplayIndex = 5;
				dataGridViewMail.Columns["PERIOD_END_DATE"].DisplayIndex = 6;
				dataGridViewMail.Columns["AGREE_YEAR"].DisplayIndex = 7;
				dataGridViewMail.Columns["PRICE"].DisplayIndex = 8;
				dataGridViewMail.Columns["MARKETING_SPECIALIST_ID"].DisplayIndex = 9;
				dataGridViewMail.Columns["BRANCH_ID"].DisplayIndex = 10;
				dataGridViewMail.Columns["ORDER_DATE"].DisplayIndex = 11;
				dataGridViewMail.Columns["ORDER_REPORT_ACCEPT"].DisplayIndex = 12;
				dataGridViewMail.Columns["ORDER_APPROVAL_DATE"].DisplayIndex = 13;
				dataGridViewMail.Columns["MAIL_ADDRESS"].DisplayIndex = 14;
				dataGridViewMail.Columns["REMARK1"].DisplayIndex = 15;
				dataGridViewMail.Columns["REMARK2"].DisplayIndex = 16;
				dataGridViewMail.Columns["START_MAIL_DATE"].DisplayIndex = 17;
				dataGridViewMail.Columns["GUIDE_MAIL_DATE"].DisplayIndex = 18;
				dataGridViewMail.Columns["UPDATE_MAIL_DATE"].DisplayIndex = 19;
				dataGridViewMail.Columns["CANCEL_DATE"].DisplayIndex = 20;
				dataGridViewMail.Columns["CANCEL_REPORT_ACCEPT"].DisplayIndex = 21;
				dataGridViewMail.Columns["DISABLE_FLAG"].DisplayIndex = 22;
				dataGridViewMail.Columns["CANCEL_REASON"].DisplayIndex = 23;
				dataGridViewMail.Columns["WW_RENEWAL_FLAG"].DisplayIndex = 24;
				dataGridViewMail.Columns["CREATE_DATE"].DisplayIndex = 25;
				dataGridViewMail.Columns["CREATE_PERSON"].DisplayIndex = 26;
				dataGridViewMail.Columns["UPDATE_DATE"].DisplayIndex = 27;
				dataGridViewMail.Columns["UPDATE_PERSON"].DisplayIndex = 28;
				dataGridViewMail.ResumeLayout();

				PcSupportControlList = PcSafetySupportController.ConvertPcSupportControl(dataTable);

				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("PcSafetySupportAccess.GetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}

		/// <summary>
		/// 契約更新案内メール
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void radioButtonGuideMail_CheckedChanged(object sender, EventArgs e)
		{
			DataGridViewMailBindingSource.Filter = "GUIDE_MAIL_DATE is null";
			dataGridViewMail.DataSource = DataGridViewMailBindingSource;
		}

		/// <summary>
		/// 契約更新メール
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonUpdateMail_CheckedChanged(object sender, EventArgs e)
		{
			DataGridViewMailBindingSource.Filter = "UPDATE_MAIL_DATE is null";
			dataGridViewMail.DataSource = DataGridViewMailBindingSource;
		}

		/// <summary>
		/// メール送信
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSendMail_Click(object sender, EventArgs e)
		{
			if (0 < dataGridViewMail.Rows.Count)
			{
				try
				{
					// 元のカーソルを保持
					Cursor preCursor = Cursor.Current;

					// カーソルを待機カーソルに変更
					Cursor.Current = Cursors.WaitCursor;

					List<PcSupportMail> mailList = new List<PcSupportMail>();
					foreach (DataGridViewRow row in dataGridViewMail.Rows)
					{
						DataRowView drv = row.DataBoundItem as DataRowView;
						DataRow dataRow = drv.Row as DataRow;
						PcSupportControl pc = PcSupportControlList.Find(p => p.OrderNo == (string)dataRow.ItemArray[0]);
						if (null != pc)
						{
							PcSupportMail mail = new PcSupportMail(PcSupportMail.MailType.Start, pc);
							SendMailControl.SendStartMail(ref mail);
							mailList.Add(mail);
						}
					}
					PcSafetySupportAccess.SetPcSupportMail(mailList, Program.SQLSV2);
					foreach (PcSupportMail mail in mailList)
					{
						PcSupportControl pc = PcSupportControlList.Find(p => p.OrderNo == mail.OrderNo);
						if (null != pc)
						{
							pc.StartMailDateTime = mail.SendDateTime;
							PcSafetySupportAccess.SetPcSupportControl(pc, Program.SQLSV2);
						}
					}
					// カーソルを元に戻す
					Cursor.Current = preCursor;

					MessageBox.Show(string.Format("開始メールを{0}件送信しました。", mailList.Count), "開始メール送信", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					MessageBox.Show(string.Format("PcSafetySupportAccess.SetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
			}
		}
	}
}
