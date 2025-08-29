//
// HeaderDetailForm.cs
//
// 契約情報入力画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/04/03 勝呂)
// 
using CommonLib.Common;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.DB.SqlServer.HardRental;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HardRentalManager.Forms
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
		/// 契約情報（保存用）
		/// </summary>
		public T_HARD_RENTAL_HEADER SaveHeader = null;

		/// <summary>
		/// 機器情報リスト（保存用）
		/// </summary>
		private List<T_HARD_RENTAL_DETAIL> SaveDetailList = null;

		/// <summary>
		/// 内部契約番号の取得
		/// </summary>
		private int InternalRentalNo
		{
			get
			{
				return (null != SaveHeader) ? SaveHeader.InternalRentalNo : 0;
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
				// 新規申込
				// 入力可能：契約番号、受付日、利用月数、月額利用料、出荷日、納品日、機器情報
				this.Text = "契約情報の新規申込";
			}
			else
			{
				// 入力可能：全て
				this.Text = "契約情報の変更";
				dateTimePickerCancelDate.Enabled = true;

				textBoxRentalNo.Text = SaveHeader.RentalNo;
				if (SaveHeader.AcceptDate.HasValue)
				{
					dateTimePickerAcceptDate.Value = SaveHeader.AcceptDate.Value.Date;
				}
				numericTextBoxMonths.Text = SaveHeader.Months.ToString();
				numericTextBoxMonthlyAmount.Text = SaveHeader.MonthlyAmount.ToString();
				if (SaveHeader.ShippingDate.HasValue)
				{
					dateTimePickerShippingDate.Checked = true;
					dateTimePickerShippingDate.Value = SaveHeader.ShippingDate.Value.Date;
				}
				if (SaveHeader.DeliveryDate.HasValue)
				{
					dateTimePickerDeliveryDate.Checked = true;
					dateTimePickerDeliveryDate.Value = SaveHeader.DeliveryDate.Value.Date;
				}
				if (SaveHeader.UseStartDate.HasValue)
				{
					labelUseStartDate.Text = SaveHeader.UseStartDate.Value.ToShortDateString();
				}
				if (SaveHeader.UseEndDate.HasValue)
				{
					labelUseEndDate.Text = SaveHeader.UseEndDate.Value.ToShortDateString();
				}
				if (SaveHeader.BillingStartDate.HasValue)
				{
					labelBillingStartDate.Text = SaveHeader.BillingStartDate.Value.ToShortDateString();
				}
				if (SaveHeader.BillingEndDate.HasValue)
				{
					labelBillingEndDate.Text = SaveHeader.BillingEndDate.Value.ToShortDateString();
				}
				if (SaveHeader.CancelDate.HasValue)
				{
					dateTimePickerCancelDate.Checked = true;
					dateTimePickerCancelDate.Value = SaveHeader.CancelDate.Value.Date;
				}
				if (SaveHeader.BillingEndDate.HasValue)
				{
					// 課金が開始されているので、契約期間に関する情報は変更できない
					textBoxRentalNo.Enabled = false;
					dateTimePickerAcceptDate.Enabled = false;
					numericTextBoxMonths.Enabled = false;
					numericTextBoxMonthlyAmount.Enabled = false;
				}
				try
				{
					// 貸出番号に対応する機器情報の取得
					SaveDetailList = HardRentalAccess.GetHardRentalDetailList(InternalRentalNo, Program.gSettings.ConnectCharlie.ConnectionString);
					if (null != SaveDetailList)
					{
						if (0 == SaveDetailList.Count)
						{
							SaveDetailList = null;
						}
						else
						{
							int i = 1;
							foreach (T_HARD_RENTAL_DETAIL detail in SaveDetailList)
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
		/// 納品日の設定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dateTimePickerDeliveryDate_ValueChanged(object sender, EventArgs e)
		{
			if (dateTimePickerDeliveryDate.Checked)
			{
				short months = (short)numericTextBoxMonths.ToInt();
				if (false == T_HARD_RENTAL_HEADER.IsFormalMonths(months))
				{
					MessageBox.Show("利用月数が正しくありません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					dateTimePickerShippingDate.Checked = false;
					return;
				}
				labelUseStartDate.Enabled = true;
				labelUseEndDate.Enabled = true;
				DateTime? startDate = T_HARD_RENTAL_HEADER.GetUseStartDate(dateTimePickerDeliveryDate.Value);
				if (startDate.HasValue)
				{
					DateTime? endDate = T_HARD_RENTAL_HEADER.GetUseEndDate(startDate.Value, months);
					if (endDate.HasValue)
					{
						labelUseStartDate.Text = startDate.Value.ToShortDateString();
						labelUseEndDate.Text = endDate.Value.ToShortDateString();
					}
				}
			}
			else
			{
				labelUseStartDate.Enabled = false;
				labelUseEndDate.Enabled = false;
				dateTimePickerCancelDate.Checked = false;
			}
		}

		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSave_Click(object sender, EventArgs e)
		{
			// 機器情報の有無
			if (0 == listViewDetail.Items.Count)
			{
				MessageBox.Show("機器が登録されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// 契約番号の有無
			string rentalNo = textBoxRentalNo.Text.Trim();
			if (0 == rentalNo.Length)
			{
				MessageBox.Show("契約番号が入力されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// 利用月数の正当性
			short months = (short)numericTextBoxMonths.ToInt();
			if (false == T_HARD_RENTAL_HEADER.IsFormalMonths(months))
			{
				MessageBox.Show("利用月数が正しくありません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// 月額利用料の有無
			int amount = numericTextBoxMonthlyAmount.ToInt();
			if (0 == amount)
			{
				MessageBox.Show("月額利用料が入力されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			DateTime? shippingDate = null;
			DateTime? deliveryDate = null;
			DateTime? cancelDate = null;
			DateTime? useStartDate = null;
			DateTime? useEndDate = null;
			if (dateTimePickerShippingDate.Checked)
			{
				shippingDate = dateTimePickerShippingDate.Value;
			}
			if (dateTimePickerDeliveryDate.Checked)
			{
				deliveryDate = dateTimePickerDeliveryDate.Value;
			}
			if (dateTimePickerCancelDate.Checked)
			{
				cancelDate = dateTimePickerCancelDate.Value;
			}
			if (shippingDate.HasValue)
			{
				if (false == deliveryDate.HasValue)
				{
					MessageBox.Show("納品日が入力されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				// 利用期間
				useStartDate = T_HARD_RENTAL_HEADER.GetUseStartDate(shippingDate);
				useEndDate = T_HARD_RENTAL_HEADER.GetUseEndDate(useStartDate, months);
				Span useSpan = new Span(useStartDate.Value.ToDate(), useEndDate.Value.ToDate());
				if (cancelDate.HasValue)
				{
					// 解約日の正当性
					if (false == useSpan.IsInside(cancelDate.Value.ToDate()))
					{
						MessageBox.Show("解約日が利用期間内でありません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					if (cancelDate.Value.ToDate() != cancelDate.Value.ToDate().LastDayOfTheMonth())
					{
						MessageBox.Show("解約日は末日を設定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					if (SaveHeader.BillingEndDate.HasValue && cancelDate.Value <= SaveHeader.BillingEndDate.Value)
					{
						MessageBox.Show("解約日は課金終了日より未来を設定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
				}
			}
			// 機器情報を設定
			List<T_HARD_RENTAL_DETAIL> detailList = new List<T_HARD_RENTAL_DETAIL>();
			foreach (ListViewItem lvItem in listViewDetail.Items)
			{
				detailList.Add(lvItem.Tag as T_HARD_RENTAL_DETAIL);
			}
			try
			{
				bool saveFlag = false;
				if (null == SaveHeader)
				{
					// 新規入力
					saveFlag = true;
					T_HARD_RENTAL_HEADER header = new T_HARD_RENTAL_HEADER();
					header.CustomerID = CustomerNo;
					header.RentalNo = rentalNo;
					header.AcceptDate = dateTimePickerAcceptDate.Value.Date;
					header.Months = months;
					header.MonthlyAmount = amount;
					header.DeliveryDate = deliveryDate;
					header.ShippingDate = shippingDate;
					header.UseStartDate = useStartDate;
					header.UseEndDate = useEndDate;

					// 契約情報の追加（戻り値は内部契約番号）
					int internalRentalNo = HardRentalAccess.InsertIntoHardRentalHeader(header, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
					if (0 < internalRentalNo)
					{
						foreach (T_HARD_RENTAL_DETAIL detail in detailList)
						{
							detail.InternalRentalNo = internalRentalNo;
						}
						// 機器情報の追加
						HardRentalAccess.InsertIntoHardRentalDetailList(detailList, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
					}
				}
				else
				{
					// 更新
					T_HARD_RENTAL_HEADER header = SaveHeader.DeepCopy();
					header.RentalNo = rentalNo;
					header.AcceptDate = dateTimePickerAcceptDate.Value.Date;
					header.Months = months;
					header.MonthlyAmount = amount;
					header.DeliveryDate = deliveryDate;
					header.ShippingDate = shippingDate;
					header.UseStartDate = useStartDate;
					header.UseEndDate = useEndDate;
					if (false == SaveHeader.Equals(header))
					{
						// 契約情報の更新
						HardRentalAccess.UpdateSetHardRentalHeader(header, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
						saveFlag = true;
					}
					if (null == SaveDetailList)
					{
						// 新規追加
						if (0 < detailList.Count)
						{
							// 機器情報の追加
							HardRentalAccess.InsertIntoHardRentalDetailList(detailList, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
							saveFlag = true;
						}
					}
					else
					{
						// 更新
						if (false == T_HARD_RENTAL_DETAIL.EqualList(SaveDetailList, detailList))
						{
							// 機器情報の削除
							HardRentalAccess.DeleteHardRentalDetail(header.InternalRentalNo, Program.gSettings.ConnectCharlie.ConnectionString);

							// 機器情報の追加
							HardRentalAccess.InsertIntoHardRentalDetailList(detailList, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
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
				form.InternalRentalNo = InternalRentalNo;
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
				T_HARD_RENTAL_DETAIL detail = (T_HARD_RENTAL_DETAIL)lvItem.Tag;
				using (DetailForm form = new DetailForm())
				{
					form.InternalRentalNo = InternalRentalNo;
					form.SaveDetail = detail;
					if (DialogResult.OK == form.ShowDialog())
					{
						lvItem.Tag = form.Detail;
						lvItem.SubItems[1].Text = form.Detail.GoodsCode;
						lvItem.SubItems[2].Text = form.Detail.GoodsName;
						lvItem.SubItems[3].Text = detail.CategoryName;
						lvItem.SubItems[4].Text = form.Detail.Quantity.ToString();
						lvItem.SubItems[5].Text = form.Detail.SerialNo;
						lvItem.SubItems[6].Text = form.Detail.AssetsCode;
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
