using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MwsLib.BaseFactory.PcSupportTool;
using DataGridViewAutoFilter;
using MwsLib.DB.SqlServer.PcSupportTool;

namespace PcSupportTool.Forms
{
	public partial class ManagementForm : Form
	{
		/// <summary>
		/// PC安心サポート管理情報リスト DataSource
		/// </summary>
		private BindingSource dataGridViewManagerBindingSource;

		/// <summary>
		///  PC安心サポート管理情報リスト
		/// </summary>
		private List<PcSupportControl> PcSupportControlList;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ManagementForm()
		{
			InitializeComponent();

			dataGridViewManagerBindingSource = null;
			PcSupportControlList = null;
			dataGridViewManager.BindingContextChanged += new EventHandler(dataGridViewManager_BindingContextChanged);
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ManagementForm_Load(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			try
			{
				DataTable dataTable = PcSupportToolAccess.GetDataTablePcSupportControl();
				dataGridViewManagerBindingSource = new BindingSource(dataTable, null);
				dataGridViewManager.DataSource = dataGridViewManagerBindingSource;

				// カラム名の変更
				dataGridViewManager.Columns["ORDER_NO"].HeaderText = "受注No";
				dataGridViewManager.Columns["CUSTOMER_ID"].HeaderText = "顧客No";
				dataGridViewManager.Columns["CLINIC_NAME"].HeaderText = "医院名";
				dataGridViewManager.Columns["GOODS_ID"].HeaderText = "商品ID";
				dataGridViewManager.Columns["GOODS_NAME"].HeaderText = "商品名";
				dataGridViewManager.Columns["PRICE"].HeaderText = "料金";
				dataGridViewManager.Columns["AGREE_YEAR"].HeaderText = "契約年数";
				dataGridViewManager.Columns["START_DATE"].HeaderText = "契約開始日";
				dataGridViewManager.Columns["END_DATE"].HeaderText = "契約終了日";
				dataGridViewManager.Columns["PERIOD_END_DATE"].HeaderText = "利用期限日";
				dataGridViewManager.Columns["BRANCH_ID"].HeaderText = "拠店ID";
				dataGridViewManager.Columns["BRANCH_NAME"].HeaderText = "拠店名";
				dataGridViewManager.Columns["SALESMAN_ID"].HeaderText = "担当者ID";
				dataGridViewManager.Columns["SALESMAN_NAME"].HeaderText = "担当者名";
				dataGridViewManager.Columns["ORDER_DATE"].HeaderText = "受注日";
				dataGridViewManager.Columns["ORDER_REPORT_ACCEPT"].HeaderText = "申込用紙有無";
				dataGridViewManager.Columns["ORDER_APPROVAL_DATE"].HeaderText = "受注承認日";
				dataGridViewManager.Columns["MAIL_ADDRESS"].HeaderText = "メールアドレス";
				dataGridViewManager.Columns["REMARK"].HeaderText = "備考";
				dataGridViewManager.Columns["START_MAIL_DATE"].HeaderText = "開始メール送信日時";
				dataGridViewManager.Columns["GUIDE_MAIL_DATE"].HeaderText = "契約更新案内メール送信日時";
				dataGridViewManager.Columns["UPDATE_MAIL_DATE"].HeaderText = "契約更新メール送信日時";
				dataGridViewManager.Columns["CANCEL_DATE"].HeaderText = "解約日時";
				dataGridViewManager.Columns["CANCEL_REPORT_ACCEPT"].HeaderText = "解約届有無";
				dataGridViewManager.Columns["CANCEL_REASON"].HeaderText = "解約事由";
				dataGridViewManager.Columns["DISABLE_FLAG"].HeaderText = "無効フラグ";
				dataGridViewManager.Columns["WW_RENEWAL_FLAG"].HeaderText = "WonderWeb更新フラグ";
				dataGridViewManager.Columns["CREATE_DATE"].HeaderText = "作成日時";
				dataGridViewManager.Columns["CREATE_PERSON"].HeaderText = "作成者";
				dataGridViewManager.Columns["UPDATE_DATE"].HeaderText = "更新日時";
				dataGridViewManager.Columns["UPDATE_PERSON"].HeaderText = "更新者";

				// 非表示
				dataGridViewManager.Columns["GOODS_ID"].Visible = false;
				dataGridViewManager.Columns["PRICE"].Visible = false;
				dataGridViewManager.Columns["AGREE_YEAR"].Visible = false;
				dataGridViewManager.Columns["BRANCH_ID"].Visible = false;
				dataGridViewManager.Columns["SALESMAN_ID"].Visible = false;
				dataGridViewManager.Columns["START_MAIL_DATE"].Visible = false;
				dataGridViewManager.Columns["GUIDE_MAIL_DATE"].Visible = false;
				dataGridViewManager.Columns["UPDATE_MAIL_DATE"].Visible = false;
				dataGridViewManager.Columns["CANCEL_DATE"].Visible = false;
				dataGridViewManager.Columns["CANCEL_REPORT_ACCEPT"].Visible = false;
				dataGridViewManager.Columns["CANCEL_REASON"].Visible = false;
				dataGridViewManager.Columns["WW_RENEWAL_FLAG"].Visible = false;
				dataGridViewManager.Columns["CREATE_DATE"].Visible = false;
				dataGridViewManager.Columns["CREATE_PERSON"].Visible = false;
				dataGridViewManager.Columns["UPDATE_DATE"].Visible = false;
				dataGridViewManager.Columns["UPDATE_PERSON"].Visible = false;

				// 表示順の変更
				dataGridViewManager.Columns["ORDER_NO"].DisplayIndex = 0;
				dataGridViewManager.Columns["CUSTOMER_ID"].DisplayIndex = 1;
				dataGridViewManager.Columns["CLINIC_NAME"].DisplayIndex = 2;
				dataGridViewManager.Columns["GOODS_NAME"].DisplayIndex = 3;
				dataGridViewManager.Columns["START_DATE"].DisplayIndex = 4;
				dataGridViewManager.Columns["END_DATE"].DisplayIndex = 5;
				dataGridViewManager.Columns["PERIOD_END_DATE"].DisplayIndex = 6;
				dataGridViewManager.Columns["BRANCH_NAME"].DisplayIndex = 7;
				dataGridViewManager.Columns["SALESMAN_NAME"].DisplayIndex = 8;
				dataGridViewManager.Columns["ORDER_DATE"].DisplayIndex = 9;
				dataGridViewManager.Columns["ORDER_REPORT_ACCEPT"].DisplayIndex = 10;
				dataGridViewManager.Columns["ORDER_APPROVAL_DATE"].DisplayIndex = 11;
				dataGridViewManager.Columns["MAIL_ADDRESS"].DisplayIndex = 12;
				dataGridViewManager.Columns["REMARK"].DisplayIndex = 13;
				dataGridViewManager.Columns["DISABLE_FLAG"].DisplayIndex = 14;
				dataGridViewManager.ResumeLayout();

				PcSupportControlList = PcSupportToolController.ConvertPcSupportControl(dataTable);
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("PcySupporTooltAccess.GetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}

		/// <summary>
		/// Configures the autogenerated columns, replacing their header
		/// cells with AutoFilter header cells. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridViewManager_BindingContextChanged(object sender, EventArgs e)
		{
			// Continue only if the data source has been set.
			if (dataGridViewManager.DataSource == null)
			{
				return;
			}
			// Add the AutoFilter header cell to each column.
			foreach (DataGridViewColumn col in dataGridViewManager.Columns)
			{
				col.HeaderCell = new DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
			}
			// Resize the columns to fit their contents.
			//dataGridViewUser.AutoResizeColumns();
		}

		/// <summary>
		/// 受注情報からの読込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonReadOrderInfo_Click(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;
			try
			{
				List<Tuple<int, string>> mailAddressList = PcSupportToolAccess.GetMailAddress();

				List<OrderInfo> orderInfoList = PcSupportToolAccess.GetOrderInfo();
				PcSupportControlList = PcSupportToolAccess.GetPcSupportControl();

				foreach (OrderInfo order in orderInfoList)
				{
					string mailAddress = string.Empty;
					Tuple<int, string> mail = mailAddressList.Find(p => p.Item1 == order.CustomerNo);
					if (null != mail)
					{
						mailAddress = mail.Item2;
					}
					PcSupportControl control = PcSupportControlList.Find(p => p.OrderNo == order.OrderNo);
					if (null != control)
					{
						if (control.IsUpdateOrderData(order, mailAddress))
						{
							control.SetOrderInfo(order, mailAddress);
							PcSupportToolAccess.SetPcSupportControl(control);
						}
					}
					else
					{
						control = new PcSupportControl(order, mailAddress);
						PcSupportControlList.Add(control);
						PcSupportToolAccess.SetPcSupportControl(control);
					}
				}
				// DataSourceのクリア
				((DataTable)dataGridViewManagerBindingSource.DataSource).Clear();

				DataTable dataTable = PcSupportToolAccess.GetDataTablePcSupportControl();
				dataGridViewManagerBindingSource = new BindingSource(dataTable, null);
				dataGridViewManager.DataSource = dataGridViewManagerBindingSource;
				PcSupportControlList = PcSupportToolController.ConvertPcSupportControl(dataTable);
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("PcySupporTooltAccess.GetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}

		/// <summary>
		/// PC安心サポート管理情報の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridViewManager_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			string orderNo = (string)dataGridViewManager.CurrentRow.Cells[0].Value;
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
							// DataSourceのクリア
							((DataTable)dataGridViewManagerBindingSource.DataSource).Clear();

							DataTable dataTable = PcSupportToolAccess.GetDataTablePcSupportControl();
							dataGridViewManagerBindingSource = new BindingSource(dataTable, null);
							dataGridViewManager.DataSource = dataGridViewManagerBindingSource;
							PcSupportControlList = PcSupportToolController.ConvertPcSupportControl(dataTable);

							for (int i = 0; i < dataGridViewManager.Rows.Count; i++)
							{
								DataRowView drv = dataGridViewManager.Rows[i].DataBoundItem as DataRowView;
								DataRow dataRow = drv.Row as DataRow;
								if (control.OrderNo == (string)dataRow.ItemArray[0])
								{
									dataGridViewManager.Rows[i].Selected = true;

									// 先頭の行までスクロールする
									dataGridViewManager.FirstDisplayedScrollingRowIndex = i;
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
}
