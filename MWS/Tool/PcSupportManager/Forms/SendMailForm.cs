using DataGridViewAutoFilter;
using MwsLib.BaseFactory.PcSupportManager;
using MwsLib.DB.SqlServer.PcSupportManager;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;

namespace PcSupportManager.Forms
{
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

			try
			{
				DataTable dataTable = PcSupportManagerAccess.GetDataTablePcSupportControl();

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
				PcSupportControlList = PcSupportManagerController.ConvertPcSupportControl(dataTable);

				// カラム名の変更
				dataGridViewMail.Columns["SEND_MAIL"].HeaderText = "送信指示";
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
				dataGridViewMail.Columns["WW_RENEWAL_FLAG"].Visible = false;
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
				dataGridViewMail.ResumeLayout();
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("PcSupportManagerAccess.GetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
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
		/// 全て
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonAll_CheckedChanged(object sender, EventArgs e)
		{
			dataGridViewMailBindingSource.Filter = "DISABLE_FLAG = '0'";
			buttonSend.Enabled = false;
		}

		/// <summary>
		/// 開始メール
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonStartMail_CheckedChanged(object sender, EventArgs e)
		{
			dataGridViewMailBindingSource.Filter = "START_MAIL_DATE is null AND ORDER_APPROVAL_DATE is not null AND START_DATE is not null AND DISABLE_FLAG = '0' AND 0 < LEN(MAIL_ADDRESS)";
			buttonSend.Enabled = true;
		}

		/// <summary>
		/// 契約更新案内メール
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonMailGuide_CheckedChanged(object sender, EventArgs e)
		{
			buttonSend.Enabled = true;
		}

		/// <summary>
		/// 契約更新メール
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonMailUpdate_CheckedChanged(object sender, EventArgs e)
		{
			buttonSend.Enabled = true;
		}

		/// <summary>
		/// 送信
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSend_Click(object sender, EventArgs e)
		{
			List<PcSupportMail> list = new List<PcSupportMail>();
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
						pc.StartMailDateTime = DateTime.Now;
						list.Add(new PcSupportMail(PcSupportMail.MailType.Start, pc, DateTime.Now));
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
			if (0 < list.Count)
			{
				try
				{
					PcSupportManagerAccess.InsertIntoPcSupportMailList(list);
				}
				catch (Exception ex)
				{
					MessageBox.Show(string.Format("PcSupportManagerAccess.InsertIntoPcSupportMailList() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return;
				}
				MessageBox.Show("メール送信が終了しました。", "メール送信", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}
}
