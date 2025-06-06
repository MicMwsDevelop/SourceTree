﻿//
// PcSupportEndForm.cs
// 
// PC安心サポート 自動更新後の終了処理画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2025/01/23 勝呂):新規作成
//
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.BaseFactory.MwsServiceCancelTool;
using CommonLib.Common;
using CommonLib.DB.SqlServer.MwsServiceCancelTool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace MwsServiceCancelTool.Forms
{
	/// <summary>
	/// PC安心サポート 自動更新後の終了処理画面クラス
	/// </summary>
	public partial class PcSupportEndForm : Form
	{
		/// <summary>
		/// 顧客情報
		/// </summary>
		public vMic顧客情報 CustomerInfo { get; set; }

		/// <summary>
		/// PC安心サポート契約情報
		/// </summary>
		private UseContractPcSupport PcSupport { get; set; }

		/// <summary>
		/// 顧客管理利用情報
		/// </summary>
		private CustomerUseInformation Cui { get; set; }

		/// <summary>
		/// PC安心サポート契約情報 DataSource
		/// </summary>
		private BindingSource BindingSourcePcSupport;

		/// <summary>
		/// 顧客管理利用情報 DataSource
		/// </summary>
		private BindingSource BindingSourceCui;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PcSupportEndForm()
		{
			InitializeComponent();

			CustomerInfo = null;
			Cui = null;
			PcSupport = null;
			BindingSourcePcSupport = null;
			BindingSourceCui = null;
		}

		/// <summary>
		/// 顧客情報を設定
		/// </summary>
		/// <param name="cu">顧客情報</param>
		public void SetCustomerInfo(vMic顧客情報 cu)
		{
			CustomerInfo = cu;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PcSupportCancelForm_Load(object sender, EventArgs e)
		{
			if (null != CustomerInfo)
			{
				labelCustomerNo.Text = CustomerInfo.顧客No.ToString();
				labelCustomerName.Text = CustomerInfo.顧客名;

				DataTable table = MwsServiceCancelToolAccess.DataTable_UseContractPcSupport(CustomerInfo.顧客No, Program.gSettings.ConnectCharlie.ConnectionString);
				List<UseContractPcSupport> pcList = UseContractPcSupport.DataTableToList(table);
				if (null != pcList && 0 < pcList.Count)
				{
					// PC安心サポート契約情報の設定
					PcSupport = pcList[0];
					BindingSourcePcSupport = new BindingSource(table, null);
					dataGridViewPcSupport.DataSource = BindingSourcePcSupport;

					if (PcSupport.IsContinueGoods)
					{
						// PC安心サポート１年契約（更新用） or PC安心サポートPlus１年契約（更新用）
						buttonOK.Enabled = true;

						// 処理後の契約終了日、課金終了日
						Date endDate = PcSupport.fContractEndDate.Value.ToDateTime().ToDate().PlusYears(-1);
						labelContractEndDate.Text = endDate.ToString();
						labelContractEndDate.Tag = endDate;
						labelBillingEndDate.Text = endDate.ToString();
						labelBillingEndDate.Tag = endDate;

						// 処理後の商品コード
						int termYear = Date.GetPassageYear(PcSupport.fContractStartDate.Value, PcSupport.fContractEndDate.Value + 1);
						if (PcSupport.IsThreeYearService)
						{
							// サービスコードがPC安心サポート3年契約またはPC安心サポートPlus3年契約
							if (5 <= termYear)
							{
								// 期間が5年以上なので、更新用のまま
								labelGoodsID.Text = PcSupport.fGoodsID;
								labelGoodsID.Tag = PcSupport.fGoodsID;
							}
							else
							{
								// 期間が4年以内なので、新規に戻す
								if (PcSupport.IsPcSupportPlusGoods)
								{
									labelGoodsID.Text = PcaGoodsIDDefine.PcSupportPlus3;
									labelGoodsID.Tag = PcaGoodsIDDefine.PcSupportPlus3;
								}
								else
								{
									labelGoodsID.Text = PcaGoodsIDDefine.PcSupport3;
									labelGoodsID.Tag = PcaGoodsIDDefine.PcSupport3;
								}
							}
						}
						else
						{
							// サービスコードがPC安心サポート1年契約またはPC安心サポートPlus1年契約
							if (3 <= termYear)
							{
								// 期間が3年以上なので、更新用のまま
								labelGoodsID.Text = PcSupport.fGoodsID;
								labelGoodsID.Tag = PcSupport.fGoodsID;
							}
							else
							{
								// 期間が2年以内なので、新規に戻す
								if (PcSupport.IsPcSupportPlusGoods)
								{
									labelGoodsID.Text = PcaGoodsIDDefine.PcSupportPlus1;
									labelGoodsID.Tag = PcaGoodsIDDefine.PcSupportPlus1;
								}
								else
								{
									labelGoodsID.Text = PcaGoodsIDDefine.PcSupport1;
									labelGoodsID.Tag = PcaGoodsIDDefine.PcSupport1;
								}
							}
						}
					}
					else
					{
						// 「PC安心サポートが更新されていないので、終了処理はできません」メッセージ表示
						label1Warning.Visible = true;
					}
					if (PcSupport.IsPcSupportPlusGoods)
					{
						// PC安心サポートPlusなので、クラウドバックアップ（PC安心サポートPlus）の顧客管理利用情報を表示
						table = MwsServiceCancelToolAccess.DataTable_CustomerUseInformation(CustomerInfo.顧客No, (int)ServiceCodeDefine.ServiceCode.ExCloudBackupPcSupportPlus, Program.gSettings.ConnectCharlie.ConnectionString);
						if (null != table && 0 < table.Rows.Count)
						{
							// 顧客管理利用情報の設定
							List<CustomerUseInformation> cuiList = CustomerUseInformation.DataTableToList(table);
							if (null != cuiList && 0 < cuiList.Count)
							{
								Cui = cuiList[0];
								BindingSourceCui = new BindingSource(table, null);
								dataGridViewCui.DataSource = BindingSourceCui;
								if (PcSupport.IsContinueGoods)
								{
									// PC安心サポートPlus１年契約（更新用）
									DateTime endDateTime = Cui.USE_END_DATE.Value.ToDate().PlusYears(-1).ToDateTime();
									labelUseEndDate.Text = endDateTime.ToDate().ToString();
									labelUseEndDate.Tag = endDateTime;
									labelPeriodEndDate.Text = endDateTime.ToDate().ToString();
									labelPeriodEndDate.Tag = endDateTime;
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// 終了処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == MessageBox.Show("PC安心サポートの終了処理を行います。よろしいですか？", "終了処理", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				try
				{
					if (PcSupport.IsPcSupportPlusGoods)
					{
						// クラウドバックアップ（PC安心サポートPlus）の課金終了日と利用期限日を１年前に設定し、解約状態にする
						Cui.PAUSE_END_STATUS = true;
						Cui.USE_END_DATE = (DateTime)labelUseEndDate.Tag;
						Cui.PERIOD_END_DATE = (DateTime)labelPeriodEndDate.Tag;
					}
					// PC安心サポートの契約終了日と課金終了日を１年前に設定し、終了フラグをONにする
					PcSupport.fGoodsID = (string)labelGoodsID.Tag;
					PcSupport.fContractEndDate = (Date)labelContractEndDate.Tag;
					PcSupport.fBillingEndDate = (Date)labelBillingEndDate.Tag;
					PcSupport.fEndFlag = true;

#if !DebugNoWrite
					if (PcSupport.IsPcSupportPlusGoods)
					{
						// クラウドバックアップ（PC安心サポートPlus）
						// [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]の更新
						int result1 = MwsServiceCancelToolAccess.UpdateSet_CustomerUseInformation(Cui, Program.ProcName, Program.gSettings.ConnectCharlie.ConnectionString);
					}
					// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の更新
					int result2 = MwsServiceCancelToolAccess.UpdateSet_UseContractPcSupport(PcSupport, Program.ProcName, Program.gSettings.ConnectCharlie.ConnectionString);
#endif
					MessageBox.Show("PC安心サポートの終了処理が正常終了しました。", "終了処理", MessageBoxButtons.OK, MessageBoxIcon.Information);
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
				catch (Exception ex) 
				{
					MessageBox.Show(ex.Message, "終了処理", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
