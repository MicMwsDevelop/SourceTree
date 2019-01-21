//
// ManagementForm.cs
//
// PC安心サポート管理情報登録画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/11/19 勝呂)
// Ver1.030 PC安心サポート管理情報に登録されている受注情報が存在しない時の処理を追加(2019/01/21 勝呂)
// 
using DataGridViewAutoFilter;
using MwsLib.BaseFactory.PcSupportManager;
using MwsLib.DB.SqlServer.PcSupportManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PcSupportManager.Forms
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
				DataTable dataTable = PcSupportManagerGetIO.GetPcSupportControl();
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

				PcSupportControlList = PcSupportManagerController.ConvertPcSupportControl(dataTable);

				// 入力状態絞り込み すべて
				radioButtonInputAll.Checked = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("PcSupportManagerAccess.GetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
		/// 入力状態絞り込み 全て
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonInputAll_CheckedChanged(object sender, EventArgs e)
		{
			dataGridViewManagerBindingSource.Filter = null;

			// レコード件数の表示
			//textBoxCount.Text = string.Format("{0}/{1}", dataGridViewManagerBindingSource.Count, PcSupportControlList.Count);

			// 背景色の設定
			this.SetDataGridViewManagerCellBackColor();
		}

		/// <summary>
		/// 入力状態絞り込み 入力途中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonNotInput_CheckedChanged(object sender, EventArgs e)
		{
			dataGridViewManagerBindingSource.Filter = "START_DATE is null OR 0 = LEN(MAIL_ADDRESS) OR ORDER_REPORT_ACCEPT = '0'";

			// レコード件数の表示
			//textBoxCount.Text = string.Format("{0}/{1}", dataGridViewManagerBindingSource.Count, PcSupportControlList.Count);

			// 背景色の設定
			this.SetDataGridViewManagerCellBackColor();
		}

		/// <summary>
		/// 入力状態絞り込み 入力済み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonInputed_CheckedChanged(object sender, EventArgs e)
		{
			dataGridViewManagerBindingSource.Filter = "START_DATE is not null AND 0 < LEN(MAIL_ADDRESS) AND ORDER_REPORT_ACCEPT <> '0' AND DISABLE_FLAG = '0'";

			// レコード件数の表示
			//textBoxCount.Text = string.Format("{0}/{1}", dataGridViewManagerBindingSource.Count, PcSupportControlList.Count);

			// 背景色の設定
			this.SetDataGridViewManagerCellBackColor();
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
				List<OrderInfo> orderInfoList = PcSupportManagerAccess.GetOrderInfoList();
				PcSupportControlList = PcSupportManagerAccess.GetPcSupportControl();

				int insertIntoCount = 0;
				int updateCount = 0;
				List<PcSupportControl> updatePcList = new List<PcSupportControl>();
				foreach (OrderInfo order in orderInfoList)
				{
					string mailAddress = MainForm.GetCustomerMailAddress(order.CustomerNo); ;
					PcSupportControl control = PcSupportControlList.Find(p => p.OrderNo == order.OrderNo);
					if (null != control)
					{
						if (control.IsUpdateOrderData(order, mailAddress))
						{
							control.SetOrderInfo(order, mailAddress, Program.SystemDate);
							updatePcList.Add(control);
							updateCount++;
						}
					}
					else
					{
						control = new PcSupportControl(order, mailAddress, Program.SystemDate);
						updatePcList.Add(control);
						insertIntoCount++;
					}
				}
				if (0 < updatePcList.Count)
				{
					try
					{
						PcSupportManagerAccess.SetPcSupportControlList(updatePcList);
					}
					catch (Exception ex)
					{
						MessageBox.Show(string.Format("PcSupportManagerAccess.SetPcSupportControlList() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						return;
					}
					// DataSourceのクリア
					((DataTable)dataGridViewManagerBindingSource.DataSource).Clear();

					DataTable dataTable = PcSupportManagerGetIO.GetPcSupportControl();
					dataGridViewManagerBindingSource = new BindingSource(dataTable, null);
					dataGridViewManager.DataSource = dataGridViewManagerBindingSource;
					PcSupportControlList = PcSupportManagerController.ConvertPcSupportControl(dataTable);

					// 背景色の設定
					this.SetDataGridViewManagerCellBackColor();

					MessageBox.Show(string.Format("受注情報に変更がありましたので、再読込を行いました。(追加:{0} 変更:{1})", insertIntoCount, updateCount), "受注情報からの読込み", MessageBoxButtons.OK, MessageBoxIcon.Information);

					// レコード件数の表示
					//textBoxCount.Text = string.Format("{0}/{1}", dataGridViewManagerBindingSource.Count, PcSupportControlList.Count);
				}
				else
				{
					MessageBox.Show("受注情報に変更はありません。", "受注情報からの読込み", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("PcSupportManagerAccess.GetOrderInfoList() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
				int customerNo = (int)dataGridViewManager.CurrentRow.Cells[1].Value;
				OrderInfo orderInfo = PcSupportManagerAccess.GetOrderInfo(customerNo);

				// Ver1.030 PC安心サポート管理情報に登録されている受注情報が存在しない時の処理を追加(2019/01/21 勝呂)
				if (null == orderInfo)
				{
					MessageBox.Show("登録されている受注情報が存在しません。", "受注情報読込エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				string mailAddress = MainForm.GetCustomerMailAddress(customerNo);
				bool modify = false;
				if (control.IsUpdateOrderData(orderInfo, mailAddress))
				{
					control.SetOrderInfo(orderInfo, mailAddress, Program.SystemDate);
					try
					{
						PcSupportManagerAccess.SetPcSupportControl(control);
						modify = true;
					}
					catch (Exception ex)
					{
						MessageBox.Show(string.Format("PcSupportManagerAccess.SetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					}
				}
				using (PcSupportControlForm form = new PcSupportControlForm(control))
				{
					DialogResult ret = form.ShowDialog();
					if (modify || DialogResult.OK == ret)
					{
						try
						{
							// DataSourceのクリア
							((DataTable)dataGridViewManagerBindingSource.DataSource).Clear();

							DataTable dataTable = PcSupportManagerGetIO.GetPcSupportControl();
							PcSupportControlList = PcSupportManagerController.ConvertPcSupportControl(dataTable);
							dataGridViewManagerBindingSource = new BindingSource(dataTable, null);
							dataGridViewManager.DataSource = dataGridViewManagerBindingSource;

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
							// 背景色の設定
							this.SetDataGridViewManagerCellBackColor();
						}
						catch (Exception ex)
						{
							MessageBox.Show(string.Format("PcSupportManagerAccess.GetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						}
					}
				}
			}
		}

		/// <summary>
		/// 顧客Noによる検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSearch_Click(object sender, EventArgs e)
		{
			if (8 == textBoxCustomerNo.Text.Length)
			{
				int customerNo;
				if (int.TryParse(textBoxCustomerNo.Text, out customerNo))
				{
					OrderInfo orderInfo = PcSupportManagerAccess.GetOrderInfo(customerNo);
					if (null != orderInfo)
					{
						string mailAddress = MainForm.GetCustomerMailAddress(customerNo);
						PcSupportControl control = PcSupportControlList.Find(p => p.OrderNo == orderInfo.OrderNo);
						bool modify = false;
						try
						{
							if (null != control)
							{
								if (control.IsUpdateOrderData(orderInfo, mailAddress))
								{
									control.SetOrderInfo(orderInfo, mailAddress, Program.SystemDate);
									PcSupportManagerAccess.SetPcSupportControl(control);
									modify = true;
								}
							}
							else
							{
								control = new PcSupportControl(orderInfo, mailAddress, Program.SystemDate);
								PcSupportManagerAccess.SetPcSupportControl(control);
								modify = true;
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(string.Format("PcSupportManagerAccess.SetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
							return;
						}
						using (PcSupportControlForm form = new PcSupportControlForm(control))
						{
							DialogResult ret = form.ShowDialog();
							if (modify || DialogResult.OK == ret)
							{
								try
								{
									// DataSourceのクリア
									((DataTable)dataGridViewManagerBindingSource.DataSource).Clear();

									DataTable dataTable = PcSupportManagerGetIO.GetPcSupportControl();
									PcSupportControlList = PcSupportManagerController.ConvertPcSupportControl(dataTable);
									dataGridViewManagerBindingSource = new BindingSource(dataTable, null);
									dataGridViewManager.DataSource = dataGridViewManagerBindingSource;

									// 背景色の設定
									this.SetDataGridViewManagerCellBackColor();
								}
								catch (Exception ex)
								{
									MessageBox.Show(string.Format("PcSupportManagerAccess.GetPcSupportControl() Error({0})", ex.Message), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
								}
							}
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
					}
					else
					{
						MessageBox.Show("顧客Noに該当する受注情報はありませんでした。", "検索", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			}
			else
			{
				MessageBox.Show("顧客Noを正しく入力してください。", "検索", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		/// <summary>
		/// 背景色の設定
		/// </summary>
		private void SetDataGridViewManagerCellBackColor()
		{
			for (int i = 0; i < dataGridViewManager.Rows.Count; i++)
			{
				DateTime? startDateTime = dataGridViewManager.Rows[i].Cells[7].Value as DateTime?;
				if (false == startDateTime.HasValue)
				{
					dataGridViewManager.Rows[i].Cells[7].Style.BackColor = Color.Pink;
					dataGridViewManager.Rows[i].Cells[8].Style.BackColor = Color.Pink;
				}
				string accept = dataGridViewManager.Rows[i].Cells[15].Value as string;
				if ("0" == accept)
				{
					dataGridViewManager.Rows[i].Cells[15].Style.BackColor = Color.Pink;
				}
				string mailAddress = dataGridViewManager.Rows[i].Cells[17].Value as string;
				if (0 == mailAddress.Length)
				{
					dataGridViewManager.Rows[i].Cells[17].Style.BackColor = Color.Pink;
				}
				string disable = dataGridViewManager.Rows[i].Cells[25].Value as string;
				if ("1" == disable)
				{
					dataGridViewManager.Rows[i].Cells[25].Style.BackColor = Color.DarkGray;
				}
			}
		}

		/// <summary>
		/// DataBindingComplete
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridViewManager_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			if (null != PcSupportControlList)
			{
				// レコード件数の表示
				textBoxCount.Text = string.Format("{0}/{1}", dataGridViewManagerBindingSource.Count, PcSupportControlList.Count);
			}
		}
	}
}
