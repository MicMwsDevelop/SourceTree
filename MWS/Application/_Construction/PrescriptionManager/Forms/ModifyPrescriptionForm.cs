//
// ModifyPrescriptionForm.cs
// 
// 電子処方箋契約情報変更画面フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/02/14 勝呂):新規作成
//
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.PrescriptionManager;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.PrescriptionManager;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PrescriptionManager.Forms
{
	public partial class ModifyPrescriptionForm : Form
	{
		/// <summary>
		/// 電子処方箋契約情報
		/// </summary>
		public UsePrescription Data { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ModifyPrescriptionForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ModifyPrescription_Load(object sender, EventArgs e)
		{
			textBoxCustomerNo.Text = Data.CustomerID.ToString();
			textBoxCustomerName.Text = Data.CustomerName;
			textBoxApplyDate.Text = Data.ApplyDate.ToString();
			if (Data.OperationDate.HasValue)
			{
				dateTimePickerOperationDate.Checked = true;
				dateTimePickerOperationDate.Value = Data.OperationDate.Value;
			}
			if (Data.ContractStartDate.HasValue)
			{
				textBoxContractStartDate.Text = Data.ContractStartDate.Value.ToShortDateString();
				textBoxContractStartDate.Tag = Data.ContractStartDate.Value;
			}
			if (Data.ContractEndDate.HasValue)
			{
				textBoxContractEndDate.Text = Data.ContractEndDate.Value.ToShortDateString();
				textBoxContractEndDate.Tag = Data.ContractEndDate.Value;
			}
		}

		/// <summary>
		/// 運用開始日の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dateTimePickerOperationDate_ValueChanged(object sender, EventArgs e)
		{
			if (dateTimePickerOperationDate.Checked)
			{
				Date start = dateTimePickerOperationDate.Value.ToDate().FirstDayOfNextMonth();	// 翌月初日
				Date end = start.PlusMonths(60).LastDayOfTheMonth();
				textBoxContractStartDate.Text = start.ToString();
				textBoxContractStartDate.Tag = start;
				textBoxContractEndDate.Text = end.ToString();
				textBoxContractEndDate.Tag = end;
			}
			else
			{
				textBoxContractStartDate.Text = string.Empty;
				textBoxContractStartDate.Tag = null;
				textBoxContractEndDate.Text = string.Empty;
				textBoxContractEndDate.Tag = null;
			}
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (dateTimePickerOperationDate.Checked)
			{
				Data.OperationDate = dateTimePickerOperationDate.Value.Date;
				Data.ContractStartDate = ((Date)textBoxContractStartDate.Tag).ToDateTime();
				Data.ContractEndDate = ((Date)textBoxContractEndDate.Tag).ToDateTime();
			}
			else
			{
				Data.OperationDate = null;
				Data.ContractStartDate = null;
				Data.ContractEndDate = null;
			}
			try
			{
				// 電子処方箋契約情報の運用開始日の更新
				PrescriptionManagerAccess.UpdateSetUsePrescriptionOperationDate(Data, Program.PROC_NAME, Program.gSettings.ConnectCharlie.ConnectionString);

				// 顧客利用情報の利用期間の設定
				List<M_CODE> codeList = null;
				string goodsID = string.Empty;
				if (Data.GoodsID == Program.gSettings.IntroductionPackOutside)
				{
					// 800705 電子処方箋管理サービス（院外）導入パック
					// 800701 電子処方箋管理サービス（院外）のMWSコードマスタの設定を取得
					//      1048100 電子処方箋管理
　					//      1046160 薬剤情報等閲覧
　					//      1022100 処方箋発行
　					//      1036240 TABLET ビューワ
					string whereStr = string.Format("GOODS_ID = '{0}'", Program.gSettings.OutsideHospitalService);
					codeList = CharlieDatabaseAccess.Select_M_CODE(whereStr, "SERVICE_ID ASC", Program.gSettings.ConnectCharlie.ConnectionString);
					goodsID = Program.gSettings.OutsideHospitalService;
				}
				else if (Data.GoodsID == Program.gSettings.IntroductionPackInside)
				{
					// 800706 電子処方箋管理サービス（院内）導入パック
					// 800702 電子処方箋管理サービス（院内）のMWSコードマスタの設定を取得
					//      1048120 院内処方チェック
　					//      1046160 薬剤情報等閲覧
　					//      1036240 TABLET ビューワ
					string whereStr = string.Format("GOODS_ID = '{0}'", Program.gSettings.InsideHospitalService);
					codeList = CharlieDatabaseAccess.Select_M_CODE(whereStr, "SERVICE_ID ASC", Program.gSettings.ConnectCharlie.ConnectionString);
					goodsID = Program.gSettings.InsideHospitalService;
				}
				if (null != codeList && 0 < codeList.Count)
				{
					foreach (M_CODE code in codeList)
					{
						string whereStr = string.Format("CUSTOMER_ID = {0} AND SERVICE_ID = {1}", Data.CustomerID, code.SERVICE_ID);
						List<T_CUSSTOMER_USE_INFOMATION> cuiList = CharlieDatabaseAccess.Select_T_CUSSTOMER_USE_INFOMATION(whereStr, "", Program.gSettings.ConnectCharlie.ConnectionString);
						if (null != cuiList && 0 < cuiList.Count)
						{
							T_CUSSTOMER_USE_INFOMATION cui = cuiList[0];
							if (false == cui.USE_END_DATE.HasValue || cui.USE_END_DATE < Data.ContractEndDate)
							{
								cui.GOODS_ID = goodsID;
								cui.USE_END_DATE = Data.ContractEndDate;
								cui.PAUSE_END_STATUS = false;
								cui.PERIOD_END_DATE = null;
								cui.UPDATE_DATE = DateTime.Now;
								cui.UPDATE_PERSON = Program.PROC_NAME;
								cui.RENEWAL_FLG = true;
								CharlieDatabaseAccess.UpdateSet_T_CUSSTOMER_USE_INFOMATION(cui, Program.gSettings.ConnectCharlie.ConnectionString);
							}
						}
						else
						{
							T_CUSSTOMER_USE_INFOMATION cui = new T_CUSSTOMER_USE_INFOMATION();
							cui.CUSTOMER_ID = Data.CustomerID;
							cui.SERVICE_TYPE_ID = code.SERVICE_TYPE_ID;
							cui.SERVICE_ID = code.SERVICE_ID;
							cui.GOODS_ID = goodsID;
							cui.APPLICATION_NO = Data.ContractID.ToString();
							//cui.KAKIN_START_DATE = null;
							cui.USE_START_DATE = Data.ContractStartDate;
							cui.USE_END_DATE = Data.ContractEndDate;
							//cui.CANCELLATION_DAY = null;
							//cui.CANCELLATION_PROCESSING_DATE = null;
							//cui.PAUSE_END_STATUS = false;
							//cui.DELETE_FLG = false;
							cui.CREATE_DATE = DateTime.Now;
							cui.CREATE_PERSON = Program.PROC_NAME;
							//cui.UPDATE_DATE = null;
							//cui.UPDATE_PERSON = null;
							//cui.PERIOD_END_DATE = null;
							//cui.RENEWAL_FLG = true;
							CharlieDatabaseAccess.InsertInto_T_CUSSTOMER_USE_INFOMATION(cui, Program.gSettings.ConnectCharlie.ConnectionString);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "電子処方箋契約情報更新エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}
			DialogResult = DialogResult.OK;
		}
	}
}
