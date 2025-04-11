//
// HeaderDetailForm.cs
//
// 契約情報入力画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/04/03 勝呂)
// 
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.DB.SqlServer.HardSubscManager;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HardSubscManager.Forms
{
	/// <summary>
	/// 契約情報入力画面クラス
	/// </summary>
	public partial class HeaderDetailForm : Form
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo = 0;

		/// <summary>
		/// ハードサブスク管理契約情報（保存用）
		/// </summary>
		public T_HARDSUBSC_HEADER SaveHeader = null;

		/// <summary>
		/// 機器情報リスト（保存用）
		/// </summary>
		private List<T_HARDSUBSC_DETAIL> SaveDetailList = null;

		/// <summary>
		/// 貸出番号の取得
		/// </summary>
		private int RentalNo
		{
			get
			{
				return (null != SaveHeader) ? SaveHeader.RentalNo : 0;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public HeaderDetailForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HeaderDetailForm_Load(object sender, EventArgs e)
		{
			if (null == SaveHeader)
			{
				// 新規入力
				dateTimePickerAcceptDate.Value = DateTime.Now;
				numericTextBoxMonths.Text = "0";
				numericTextBoxTotalAmount.Text = "0";
				numericTextBoxMonthlyAmount.Text = "0";
			}
			else
			{
				// 更新
				labelRentalNo.Text = SaveHeader.RentalNo.ToString();
				dateTimePickerAcceptDate.Value = SaveHeader.ApplyDate.Value;
				dateTimePickerAcceptDate.Enabled = false;
				numericTextBoxMonths.Text = SaveHeader.Months.ToString();
				numericTextBoxTotalAmount.Text = SaveHeader.TotalAmount.ToString();
				numericTextBoxMonthlyAmount.Text = SaveHeader.MonthlyAmount.ToString();
				if (SaveHeader.ContractStartDate.HasValue)
				{
					labelContractStartDate.Text = SaveHeader.ContractStartDate.Value.ToShortDateString();
				}
				if (SaveHeader.ContractEndDate.HasValue)
				{
					labelContractEndDate.Text = SaveHeader.ContractEndDate.Value.ToShortDateString();
				}
				if (SaveHeader.IsEnableCancelDate)
				{
					// 解約日が設定可能
					dateTimePickerCancelDate.Enabled = true;
					dateTimePickerCancelDate.MinDate = SaveHeader.ContractStartDate.Value;
					dateTimePickerCancelDate.MaxDate = SaveHeader.ContractEndDate.Value;
					dateTimePickerCancelDate.Checked = false;
					if (SaveHeader.CancelDate.HasValue)
					{
						dateTimePickerCancelDate.Checked = true;
						dateTimePickerCancelDate.Value = SaveHeader.CancelDate.Value;
					}
				}
				try
				{
					// 貸出番号に対応する機器情報の取得
					SaveDetailList = HardSubscManagerAccess.GetHardSubscDetailList(RentalNo, Program.gSettings.ConnectCharlie.ConnectionString);
					if (null != SaveDetailList)
					{
						if (0 == SaveDetailList.Count)
						{
							SaveDetailList = null;
						}
						else
						{
							int i = 1;
							foreach (T_HARDSUBSC_DETAIL detail in SaveDetailList)
							{
								ListViewItem item = new ListViewItem(Program.GetDetailListViewItem(i, detail));
								item.Tag = detail;
								listViewDetail.Items.Add(item);
								i++;
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (0 == numericTextBoxMonths.ToInt())
			{
				MessageBox.Show("契約月数が入力されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (0 == numericTextBoxTotalAmount.ToInt())
			{
				MessageBox.Show("金額が入力されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (0 == numericTextBoxMonthlyAmount.ToInt())
			{
				MessageBox.Show("月額が入力されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			bool saveFlag = false;

			// 機器情報を設定
			List<T_HARDSUBSC_DETAIL> detailList = new List<T_HARDSUBSC_DETAIL>();
			foreach (ListViewItem lvItem in listViewDetail.Items)
			{
				detailList.Add(lvItem.Tag as T_HARDSUBSC_DETAIL);
			}
			try
			{
				if (null == SaveHeader)
				{
					// 新規入力
					saveFlag = true;
					T_HARDSUBSC_HEADER header = new T_HARDSUBSC_HEADER();
					header.CustomerID = CustomerNo;
					header.ApplyDate = dateTimePickerAcceptDate.Value;
					header.Months = (short)numericTextBoxMonths.ToInt();
					header.TotalAmount = numericTextBoxTotalAmount.ToInt();
					header.MonthlyAmount = numericTextBoxMonthlyAmount.ToInt();

					// 契約情報の追加（戻り値は貸出番号）
					int rentaNo = HardSubscManagerAccess.InsertIntoHardSubscHeader(header, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
					if (0 < rentaNo)
					{
						foreach (T_HARDSUBSC_DETAIL detail in detailList)
						{
							detail.RentalNo = rentaNo;
						}
						// 機器情報の追加
						HardSubscManagerAccess.InsertIntoHardSubscDetailList(detailList, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
					}
				}
				else
				{
					// 更新
					T_HARDSUBSC_HEADER header = SaveHeader.DeepCopy();
					header.Months = (short)numericTextBoxMonths.ToInt();
					header.TotalAmount = numericTextBoxTotalAmount.ToInt();
					header.MonthlyAmount = numericTextBoxMonthlyAmount.ToInt();
					if (SaveHeader.CancelDate.HasValue)
					{
						if (dateTimePickerCancelDate.Checked)
						{
							if (SaveHeader.CancelDate != dateTimePickerCancelDate.Value)
							{
								// 解約日の設定
								header.CancelDate = dateTimePickerCancelDate.Value;
								header.CancelApplyDate = DateTime.Now;
							}
						}
						else
						{
							// 解約日の解除
							header.CancelDate = null;
							header.CancelApplyDate = null;
						}
					}
					else
					{
						if (dateTimePickerCancelDate.Checked)
						{
							// 解約日の設定
							header.CancelDate = dateTimePickerCancelDate.Value;
							header.CancelApplyDate = DateTime.Now;
						}
					}
					if (false == SaveHeader.Equals(header))
					{
						// 契約情報の更新
						HardSubscManagerAccess.UpdateSetHardSubscHeader(header, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
						saveFlag = true;
					}
					if (null == SaveDetailList)
					{
						if (0 < detailList.Count)
						{
							// 機器情報の追加
							HardSubscManagerAccess.InsertIntoHardSubscDetailList(detailList, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
							saveFlag = true;
						}
					}
					else
					{
						if (false == T_HARDSUBSC_DETAIL.EqualList(SaveDetailList, detailList))
						{
							// 機器情報の削除
							HardSubscManagerAccess.DeleteHardSubscDetail(header.RentalNo, Program.gSettings.ConnectCharlie.ConnectionString);

							// 機器情報の追加
							HardSubscManagerAccess.InsertIntoHardSubscDetailList(detailList, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
							saveFlag = true;
						}
					}
				}
				if (saveFlag)
				{
					this.DialogResult = DialogResult.OK;
				}
				else
				{
					this.DialogResult = DialogResult.Cancel;
				}
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// キャンセル
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();	
		}

		/// <summary>
		/// 機器の追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAdd_Click(object sender, EventArgs e)
		{
			using (DetailForm form = new DetailForm())
			{
				form.RentalNo = RentalNo;
				if (DialogResult.OK == form.ShowDialog())
				{
					ListViewItem item = new ListViewItem(Program.GetDetailListViewItem(listViewDetail.Items.Count + 1, form.Detail));
					item.Tag = form.Detail;
					listViewDetail.Items.Add(item);
				}
			}
		}

		/// <summary>
		/// 機器の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewDetail_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			ListViewItem lvItem = listViewDetail.SelectedItems[0];
			if (null != lvItem.Tag)
			{
				T_HARDSUBSC_DETAIL detail = (T_HARDSUBSC_DETAIL)lvItem.Tag;
				using (DetailForm form = new DetailForm())
				{
					form.RentalNo = RentalNo;
					form.SaveDetail = detail;
					if (DialogResult.OK == form.ShowDialog())
					{
						lvItem.Tag = form.Detail;
						lvItem.SubItems[1].Text = form.Detail.GoodsCode;
						lvItem.SubItems[2].Text = form.Detail.GoodsName;
						lvItem.SubItems[3].Text = form.Detail.GetCategoryName();
						lvItem.SubItems[4].Text = form.Detail.Quantity.ToString();
						lvItem.SubItems[5].Text = form.Detail.AssetsCode;
						lvItem.SubItems[6].Text = form.Detail.SerialNo;
					}
				}
			}
		}

		/// <summary>
		/// 機器の削除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (0 < listViewDetail.SelectedItems.Count)
			{
				ListViewItem lvItem = listViewDetail.SelectedItems[0];
				if (null != lvItem)
				{
					if (DialogResult.Yes == MessageBox.Show("機器情報を削除してよろしいですか？", Program.ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					{
						listViewDetail.Items.Remove(lvItem);
					}
				}
			}
		}
	}
}
