//
// EditForm.cs
// 
// 顧客利用情報編集画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/08/24 勝呂):新規作成
//
using CommonLib.BaseFactory.MwsCuiEditTool;
using System;
using System.Windows.Forms;

namespace MwsCuiEditTool.Forms
{
	public partial class EditForm : Form
	{
		/// <summary>
		/// 顧客利用情報
		/// </summary>
		public EditCustomerUseInformation CUI { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public EditForm()
		{
			InitializeComponent();

			CUI = null;
		}

		/// <summary>
		/// 顧客利用情報を設定
		/// </summary>
		/// <param name="cui"></param>
		public void SetCUI(EditCustomerUseInformation cui)
		{
			CUI = cui.DeepCopy();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EditForm_Load(object sender, EventArgs e)
		{
			if (null != CUI)
			{
				labelServiceID.Text = CUI.SERVICE_ID.ToString();
				labelServiceName.Text = CUI.ServiceName;
				dateTimePickerUseStartDate.SetDateTime(CUI.USE_START_DATE);
				dateTimePickerUseEndDate.SetDateTime(CUI.USE_END_DATE);
				dateTimePickerCancelationDay.SetDateTime(CUI.CANCELLATION_DAY);
				dateTimePickerCancerationProcessingDate.SetDateTime(CUI.CANCELLATION_PROCESSING_DATE);
				checkBoxPauseEndStatus.Checked = CUI.PAUSE_END_STATUS;
				dateTimePickerCreateDate.SetDateTime(CUI.CREATE_DATE);
				textBoxCreatePerson.Text = CUI.CREATE_PERSON;
				dateTimePickerUpdateDate.SetDateTime(CUI.UPDATE_DATE);
				textBoxUpdatePerson.Text = CUI.UPDATE_PERSON;
				dateTimePickerPeriodEndDate.SetDateTime(CUI.PERIOD_END_DATE);
				checkBoxRenewalFlg.Checked = CUI.RENEWAL_FLG;
			}
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			CUI.USE_START_DATE = dateTimePickerUseStartDate.ToDateTime();
			CUI.USE_END_DATE = dateTimePickerUseEndDate.ToDateTime();
			CUI.CANCELLATION_DAY = dateTimePickerCancelationDay.ToDateTime();
			CUI.CANCELLATION_PROCESSING_DATE = dateTimePickerCancerationProcessingDate.ToDateTime();
			CUI.PAUSE_END_STATUS = checkBoxPauseEndStatus.Checked;
			CUI.CREATE_DATE = dateTimePickerCreateDate.ToDateTime();
			CUI.CREATE_PERSON = textBoxCreatePerson.Text.Trim();
			CUI.UPDATE_DATE = dateTimePickerUpdateDate.ToDateTime();
			CUI.UPDATE_PERSON = textBoxUpdatePerson.Text.Trim();
			CUI.PERIOD_END_DATE = dateTimePickerPeriodEndDate.ToDateTime();
			CUI.RENEWAL_FLG = checkBoxRenewalFlg.Checked;
		}
	}
}
