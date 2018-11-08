using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MwsLib.DB.SqlServer.PcSafetySupport;
using DataGridViewAutoFilter;
using MwsLib.BaseFactory.PcSafetySupport;

namespace PcSafetySupport.Forms
{
	/// <summary>
	/// PC安心サポート管理情報登録画面
	/// </summary>
	public partial class PcSupportControlListForm : Form
	{
		private BindingSource DataGridViewControlBindingSource;

		/// <summary>
		/// PC安心サポート管理情報リスト
		/// </summary>
		private List<PcSupportControl> PcSupportControlList;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PcSupportControlListForm()
		{
			InitializeComponent();

			DataGridViewControlBindingSource = null;
			PcSupportControlList = null;
			dataGridViewControl.BindingContextChanged += new EventHandler(dataGridViewControl_BindingContextChanged);
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PcSupportControlListForm_Load(object sender, EventArgs e)
		{
			dataGridViewControl.DataSource = null;
			dataGridViewControl.Rows.Clear();
			dataGridViewControl.Columns.Clear();
			dataGridViewControl.ReadOnly = true;

			try
			{
				DataTable dataTable = PcSafetySupportAccess.GetPcSupportControl("", Program.SQLSV2);
				DataGridViewControlBindingSource = new BindingSource(dataTable, null);
				dataGridViewControl.DataSource = DataGridViewControlBindingSource;

				// カラム名の変更
				dataGridViewControl.Columns["ORDER_NO"].HeaderText = "受注No";
				dataGridViewControl.Columns["CUSTOMER_ID"].HeaderText = "顧客No";
				dataGridViewControl.Columns["CLINIC_NAME"].HeaderText = "医院名";
				dataGridViewControl.Columns["GOODS_ID"].HeaderText = "商品ID";
				dataGridViewControl.Columns["START_DATE"].HeaderText = "契約開始日";
				dataGridViewControl.Columns["END_DATE"].HeaderText = "契約終了日";
				dataGridViewControl.Columns["PERIOD_END_DATE"].HeaderText = "利用期限日";
				dataGridViewControl.Columns["AGREE_YEAR"].HeaderText = "契約年数";
				dataGridViewControl.Columns["PRICE"].HeaderText = "料金";
				dataGridViewControl.Columns["MARKETING_SPECIALIST_ID"].HeaderText = "営業担当者ID";
				dataGridViewControl.Columns["BRANCH_ID"].HeaderText = "支店ID";
				dataGridViewControl.Columns["ORDER_DATE"].HeaderText = "受注日";
				dataGridViewControl.Columns["ORDER_REPORT_ACCEPT"].HeaderText = "申込用紙有無";
				dataGridViewControl.Columns["ORDER_APPROVAL_DATE"].HeaderText = "受注承認日";
				dataGridViewControl.Columns["MAIL_ADDRESS"].HeaderText = "メールアドレス";
				dataGridViewControl.Columns["REMARK1"].HeaderText = "備考１";
				dataGridViewControl.Columns["REMARK2"].HeaderText = "備考２";
				dataGridViewControl.Columns["START_MAIL_DATE"].HeaderText = "開始メール送信日時";
				dataGridViewControl.Columns["GUIDE_MAIL_DATE"].HeaderText = "契約更新案内メール送信日時";
				dataGridViewControl.Columns["UPDATE_MAIL_DATE"].HeaderText = "契約更新メール送信日時";
				dataGridViewControl.Columns["CANCEL_DATE"].HeaderText = "解約日時";
				dataGridViewControl.Columns["CANCEL_REPORT_ACCEPT"].HeaderText = "解約届有無";
				dataGridViewControl.Columns["DISABLE_FLAG"].HeaderText = "無効フラグ";
				dataGridViewControl.Columns["CANCEL_REASON"].HeaderText = "解約事由";
				dataGridViewControl.Columns["WW_RENEWAL_FLAG"].HeaderText = "WonderWeb更新フラグ";
				dataGridViewControl.Columns["CREATE_DATE"].HeaderText = "作成日時";
				dataGridViewControl.Columns["CREATE_PERSON"].HeaderText = "作成者";
				dataGridViewControl.Columns["UPDATE_DATE"].HeaderText = "更新日時";
				dataGridViewControl.Columns["UPDATE_PERSON"].HeaderText = "更新者";

				// 表示順の変更
				dataGridViewControl.Columns["ORDER_NO"].DisplayIndex = 0;
				dataGridViewControl.Columns["CUSTOMER_ID"].DisplayIndex = 1;
				dataGridViewControl.Columns["CLINIC_NAME"].DisplayIndex = 2;
				dataGridViewControl.Columns["GOODS_ID"].DisplayIndex = 3;
				dataGridViewControl.Columns["START_DATE"].DisplayIndex = 4;
				dataGridViewControl.Columns["END_DATE"].DisplayIndex = 5;
				dataGridViewControl.Columns["PERIOD_END_DATE"].DisplayIndex = 6;
				dataGridViewControl.Columns["AGREE_YEAR"].DisplayIndex = 7;
				dataGridViewControl.Columns["PRICE"].DisplayIndex = 8;
				dataGridViewControl.Columns["MARKETING_SPECIALIST_ID"].DisplayIndex = 9;
				dataGridViewControl.Columns["BRANCH_ID"].DisplayIndex = 10;
				dataGridViewControl.Columns["ORDER_DATE"].DisplayIndex = 11;
				dataGridViewControl.Columns["ORDER_REPORT_ACCEPT"].DisplayIndex = 12;
				dataGridViewControl.Columns["ORDER_APPROVAL_DATE"].DisplayIndex = 13;
				dataGridViewControl.Columns["MAIL_ADDRESS"].DisplayIndex = 14;
				dataGridViewControl.Columns["REMARK1"].DisplayIndex = 15;
				dataGridViewControl.Columns["REMARK2"].DisplayIndex = 16;
				dataGridViewControl.Columns["START_MAIL_DATE"].DisplayIndex = 17;
				dataGridViewControl.Columns["GUIDE_MAIL_DATE"].DisplayIndex = 18;
				dataGridViewControl.Columns["UPDATE_MAIL_DATE"].DisplayIndex = 19;
				dataGridViewControl.Columns["CANCEL_DATE"].DisplayIndex = 20;
				dataGridViewControl.Columns["CANCEL_REPORT_ACCEPT"].DisplayIndex = 21;
				dataGridViewControl.Columns["DISABLE_FLAG"].DisplayIndex = 22;
				dataGridViewControl.Columns["CANCEL_REASON"].DisplayIndex = 23;
				dataGridViewControl.Columns["WW_RENEWAL_FLAG"].DisplayIndex = 24;
				dataGridViewControl.Columns["CREATE_DATE"].DisplayIndex = 25;
				dataGridViewControl.Columns["CREATE_PERSON"].DisplayIndex = 26;
				dataGridViewControl.Columns["UPDATE_DATE"].DisplayIndex = 27;
				dataGridViewControl.Columns["UPDATE_PERSON"].DisplayIndex = 28;
				dataGridViewControl.ResumeLayout();

				PcSupportControlList = PcSafetySupportController.ConvertPcSupportControl(dataTable);
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("PcSafetySupportAccess.GetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}

		/// <summary>
		/// Configures the autogenerated columns, replacing their header
		/// cells with AutoFilter header cells. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridViewControl_BindingContextChanged(object sender, EventArgs e)
		{
			// Continue only if the data source has been set.
			if (dataGridViewControl.DataSource == null)
			{
				return;
			}
			// Add the AutoFilter header cell to each column.
			foreach (DataGridViewColumn col in dataGridViewControl.Columns)
			{
				col.HeaderCell = new DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
			}
			// Resize the columns to fit their contents.
			//dataGridViewUser.AutoResizeColumns();
		}

		/// <summary>
		/// 受注Noによる検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void buttonSearch_Click(object sender, EventArgs e)
		{
			if (6 == textBoxOrderNo.Text.Length)
			{
				PcSupportControl control = PcSupportControlList.Find(p => p.OrderNo == textBoxOrderNo.Text);
				if (null != control)
				{
					// 変更
					for (int i = 0; i < dataGridViewControl.Rows.Count; i++)
					{
						DataRowView drv = dataGridViewControl.Rows[i].DataBoundItem as DataRowView;
						DataRow dataRow = drv.Row as DataRow;
						if (control.OrderNo == (string)dataRow.ItemArray[0])
						{
							dataGridViewControl.Rows[i].Selected = true;
							break;
						}
					}
					using (PcSupportControlForm form = new PcSupportControlForm(control))
					{
						if (DialogResult.OK == form.ShowDialog())
						{
							try
							{
								DataTable dataTable = PcSafetySupportAccess.GetPcSupportControl("", Program.SQLSV2);
								DataGridViewControlBindingSource = new BindingSource(dataTable, null);
								dataGridViewControl.DataSource = DataGridViewControlBindingSource;
								PcSupportControlList = PcSafetySupportController.ConvertPcSupportControl(dataTable);
							}
							catch (Exception ex)
							{
								MessageBox.Show(string.Format("PcSafetySupportAccess.GetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
							}
						}
					}
				}
				else
				{
					// 追加
					using (PcSupportControlForm form = new PcSupportControlForm(textBoxOrderNo.Text))
					{
						if (DialogResult.OK == form.ShowDialog())
						{
							try
							{
								DataTable dataTable = PcSafetySupportAccess.GetPcSupportControl("", Program.SQLSV2);
								DataGridViewControlBindingSource = new BindingSource(dataTable, null);
								dataGridViewControl.DataSource = DataGridViewControlBindingSource;
								PcSupportControlList = PcSafetySupportController.ConvertPcSupportControl(dataTable);

								for (int i = 0; i < dataGridViewControl.Rows.Count; i++)
								{
									DataRowView drv = dataGridViewControl.Rows[i].DataBoundItem as DataRowView;
									DataRow dataRow = drv.Row as DataRow;
									if (textBoxOrderNo.Text == (string)dataRow.ItemArray[0])
									{
										dataGridViewControl.Rows[i].Selected = true;
										break;
									}
								}
							}
							catch (Exception ex)
							{
								MessageBox.Show(string.Format("PcSafetySupportAccess.GetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// PC安心サポート管理情報の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridViewControl_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			string orderNo = (string)dataGridViewControl.CurrentRow.Cells[0].Value;
			PcSupportControl control = PcSupportControlList.Find(p => p.OrderNo == orderNo);
			if (null != control)
			{
				// 変更
				using (PcSupportControlForm form = new PcSupportControlForm(control))
				{
					if (DialogResult.OK == form.ShowDialog())
					{
						try
						{
							DataTable dataTable = PcSafetySupportAccess.GetPcSupportControl("", Program.SQLSV2);
							DataGridViewControlBindingSource = new BindingSource(dataTable, null);
							dataGridViewControl.DataSource = DataGridViewControlBindingSource;
							PcSupportControlList = PcSafetySupportController.ConvertPcSupportControl(dataTable);
						}
						catch (Exception ex)
						{
							MessageBox.Show(string.Format("PcSafetySupportAccess.GetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						}
					}
				}
			}
		}
	}
}
