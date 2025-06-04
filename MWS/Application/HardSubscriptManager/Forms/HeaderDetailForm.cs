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
using CommonLib.DB.SqlServer.HardSubscript;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HardSubscriptManager.Forms
{
	/// <summary>
	/// 契約情報入力画面クラス
	/// </summary>
	public partial class HeaderDetailForm : Form
	{
		/// <summary>
		/// 入力モード
		/// </summary>
		public enum TInputMode
		{
			AddNew = 0,		// 新規申込
			Modify,					// 変更
			UseStartDate,		// 利用開始日入力
			CancelDate,			// 解約日入力
		}

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo = 0;

		/// <summary>
		/// 契約情報（保存用）
		/// </summary>
		public T_HARD_SUBSCRIPT_HEADER SaveHeader = null;

		/// <summary>
		/// 機器情報リスト（保存用）
		/// </summary>
		private List<T_HARD_SUBSCRIPT_DETAIL> SaveDetailList = null;

		/// <summary>
		/// 入力モード
		/// </summary>
		public TInputMode InputMode { get; set; }

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

			InputMode = TInputMode.AddNew;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HeaderDetailForm_Load(object sender, EventArgs e)
		{
			if (TInputMode.AddNew == InputMode)
			{
				// 新規申込
				// 入力可能：契約番号、契約日、月額利用料、利用月数、機器情報
				this.Text = "契約情報の新規申込";
				textBoxRentalNo.Enabled = true;
				dateTimePickerContractDate.Enabled = true;
				numericTextBoxMonthlyAmount.Enabled = true;
				numericTextBoxMonths.Enabled = true;
			}
			else
			{
				switch (InputMode)
				{
					// 契約情報変更
					case TInputMode.Modify:
					{
							// 入力可能：契約番号、契約日、月額利用料、利用月数、機器情報
							// 条件：利用開始日未設定
							this.Text = "契約情報の変更";
							textBoxRentalNo.Enabled = true;
							dateTimePickerContractDate.Enabled = true;
							numericTextBoxMonthlyAmount.Enabled = true;
							numericTextBoxMonths.Enabled = true;
						}
						break;
					// 利用開始日入力
					case TInputMode.UseStartDate:
					{
							// 入力可能：利用開始日、機器情報
							// 条件：課金終了日未設定
							this.Text = "利用開始日の入力";
							dateTimePickerUseStartDate.Enabled = true;
						}
						break;
					// 解約日入力
					case TInputMode.CancelDate:
					{
							// 入力可能：解約日、解約受付日、機器情報
							// 条件：利用開始日設定済
							this.Text = "解約日の入力";
							dateTimePickerCancelDate.Enabled = true;
							dateTimePickerCancelApplyDate.Enabled = true;
						}
						break;
				}
				textBoxRentalNo.Text = SaveHeader.RentalNo;
				numericTextBoxMonthlyAmount.Text = SaveHeader.MonthlyAmount.ToString();
				numericTextBoxMonths.Text = SaveHeader.Months.ToString();
				if (SaveHeader.ContractDate.HasValue)
				{
					dateTimePickerContractDate.Value = SaveHeader.ContractDate.Value.Date;
				}
				if (SaveHeader.UseStartDate.HasValue)
				{
					dateTimePickerUseStartDate.Value = SaveHeader.UseStartDate.Value.Date;
				}
				if (SaveHeader.UseEndDate.HasValue)
				{
					labelUseEndDate.Text = SaveHeader.UseEndDate.Value.ToShortDateString();
				}
				if (SaveHeader.CancelDate.HasValue)
				{
					dateTimePickerCancelDate.Value = SaveHeader.CancelDate.Value.Date;
				}
				if (SaveHeader.CancelApplyDate.HasValue)
				{
					dateTimePickerCancelApplyDate.Value = SaveHeader.CancelApplyDate.Value.Date;
				}
				try
				{
					// 貸出番号に対応する機器情報の取得
					SaveDetailList = HardSubscriptAccess.GetHardSubscriptDetailList(InternalRentalNo, Program.gSettings.ConnectCharlie.ConnectionString);
					if (null != SaveDetailList)
					{
						if (0 == SaveDetailList.Count)
						{
							SaveDetailList = null;
						}
						else
						{
							int i = 1;
							foreach (T_HARD_SUBSCRIPT_DETAIL detail in SaveDetailList)
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
		/// 利用開始日の設定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dateTimePickerContractStartDate_ValueChanged(object sender, EventArgs e)
		{
			if (dateTimePickerUseStartDate.Checked)
			{
				int months = numericTextBoxMonths.ToInt();
				Date? endDate = T_HARD_SUBSCRIPT_HEADER.GetUseEndDate(dateTimePickerUseStartDate.Value.ToDate(), months);
				if (endDate.HasValue)
				{
					labelUseEndDate.Text = endDate.Value.ToString();
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
			// 月額利用料の有無
			int amount = numericTextBoxMonthlyAmount.ToInt();
			if (0 == amount)
			{
				MessageBox.Show("月額利用料が入力されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// 利用月数の有無
			short months = (short)numericTextBoxMonths.ToInt();
			if (0 == months)
			{
				MessageBox.Show("利用月数が入力されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// 利用月数の正当性
			if (months < T_HARD_SUBSCRIPT_HEADER.ContractMonthMix || T_HARD_SUBSCRIPT_HEADER.ContractMonthMax < months)
			{
				MessageBox.Show("利用月数が正しくありません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (dateTimePickerUseStartDate.Checked)
			{
				// 利用開始日の正当性
				if (dateTimePickerUseStartDate.Value.ToDate() < SaveHeader.ContractDate.Value.ToDate())
				{
					MessageBox.Show("利用開始日が契約日時より過去の日付が設定されています。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			if (dateTimePickerCancelDate.Checked)
			{
				// 解約日の正当性
				Date cancelDate = dateTimePickerCancelDate.Value.ToDate();
				if (cancelDate != cancelDate.LastDayOfTheMonth())
				{
					MessageBox.Show("解約日は末日を設定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				// 解約受付日の正当性
				Span contractSpan = new Span(SaveHeader.UseStartDate.Value.ToDate(), SaveHeader.UseEndDate.Value.ToDate());
				if (false == contractSpan.IsInside(dateTimePickerCancelDate.Value.ToDate()))
				{
					MessageBox.Show("解約日が利用期間内でありません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (SaveHeader.BillingEndDate.HasValue && cancelDate <= SaveHeader.BillingEndDate.Value.ToDate())
				{
					MessageBox.Show("解約日は課金終了日より未来を設定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				Date cancelApplyDate = dateTimePickerCancelApplyDate.Value.ToDate();
				if (cancelDate < cancelApplyDate)
				{
					MessageBox.Show("解約受付日が解約日より過去の日付が設定されています。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			// 機器情報を設定
			List<T_HARD_SUBSCRIPT_DETAIL> detailList = new List<T_HARD_SUBSCRIPT_DETAIL>();
			foreach (ListViewItem lvItem in listViewDetail.Items)
			{
				detailList.Add(lvItem.Tag as T_HARD_SUBSCRIPT_DETAIL);
			}
			try
			{
				bool saveFlag = false;
				if (null == SaveHeader)
				{
					// 新規入力
					saveFlag = true;
					T_HARD_SUBSCRIPT_HEADER header = new T_HARD_SUBSCRIPT_HEADER();
					header.CustomerID = CustomerNo;
					header.RentalNo = rentalNo;
					header.ContractDate = dateTimePickerContractDate.Value.Date;
					header.MonthlyAmount = amount;
					header.Months = months;

					// 契約情報の追加（戻り値は内部契約番号）
					int internalRentalNo = HardSubscriptAccess.InsertIntoHardSubscriptHeader(header, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
					if (0 < internalRentalNo)
					{
						foreach (T_HARD_SUBSCRIPT_DETAIL detail in detailList)
						{
							detail.InternalRentalNo = internalRentalNo;
						}
						// 機器情報の追加
						HardSubscriptAccess.InsertIntoHardSubscriptDetailList(detailList, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
					}
				}
				else
				{
					// 更新
					T_HARD_SUBSCRIPT_HEADER header = SaveHeader.DeepCopy();
					header.RentalNo = rentalNo;
					header.ContractDate = dateTimePickerContractDate.Value.Date;
					header.MonthlyAmount = amount;
					header.Months = months;

					// 利用開始日と利用終了日の設定
					if (dateTimePickerUseStartDate.Checked)
					{
						Date? endDate = T_HARD_SUBSCRIPT_HEADER.GetUseEndDate(dateTimePickerUseStartDate.Value.ToDate(), header.Months);
						if (endDate.HasValue)
						{
							header.UseStartDate = dateTimePickerUseStartDate.Value.Date;
							header.UseEndDate = endDate.Value.ToDateTime();
						}
						else
						{
							header.UseStartDate = null;
							header.UseEndDate = null;
						}
					}
					else
					{
						header.UseStartDate = null;
						header.UseEndDate = null;
					}
					// 解約日の設定
					if (dateTimePickerCancelDate.Checked)
					{
						header.CancelDate = dateTimePickerCancelDate.Value.Date;
						header.CancelApplyDate = dateTimePickerCancelApplyDate.Value.Date;
					}
					else
					{
						header.CancelDate = null;
						header.CancelApplyDate = null;
					}
					if (false == SaveHeader.Equals(header))
					{
						// 契約情報の更新
						HardSubscriptAccess.UpdateSetHardSubscriptHeader(header, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
						saveFlag = true;
					}
					if (null == SaveDetailList)
					{
						// 新規追加
						if (0 < detailList.Count)
						{
							// 機器情報の追加
							HardSubscriptAccess.InsertIntoHardSubscriptDetailList(detailList, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
							saveFlag = true;
						}
					}
					else
					{
						// 更新
						if (false == T_HARD_SUBSCRIPT_DETAIL.EqualList(SaveDetailList, detailList))
						{
							// 機器情報の削除
							HardSubscriptAccess.DeleteHardSubscriptDetail(header.InternalRentalNo, Program.gSettings.ConnectCharlie.ConnectionString);

							// 機器情報の追加
							HardSubscriptAccess.InsertIntoHardSubscriptDetailList(detailList, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
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
				T_HARD_SUBSCRIPT_DETAIL detail = (T_HARD_SUBSCRIPT_DETAIL)lvItem.Tag;
				using (DetailForm form = new DetailForm())
				{
					form.InternalRentalNo = InternalRentalNo;
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
