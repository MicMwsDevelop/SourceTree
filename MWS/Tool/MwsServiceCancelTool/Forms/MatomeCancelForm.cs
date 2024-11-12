//
// MatomeCancelForm.cs
// 
// おまとめプラン 利用申込取消画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/11/01 勝呂):新規作成
//
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.Common;
using CommonLib.DB.SqlServer;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.MwsServiceCancelTool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace MwsServiceCancelTool.Forms
{
	/// <summary>
	/// おまとめプラン 利用申込取消画面クラス
	/// </summary>
	public partial class MatomeCancelForm : Form
	{
		/// <summary>
		/// 顧客情報
		/// </summary>
		public vMic顧客情報 CustomerInfo { get; set; }

		/// <summary>
		/// おまとめプラン契約ヘッダ情報
		/// </summary>
		private T_USE_CONTRACT_HEADER Header { get; set; }

		/// <summary>
		/// おまとめプラン契約詳細情報
		/// </summary>
		private List<T_USE_CONTRACT_DETAIL> DetailList { get; set; }

		/// <summary>
		/// おまとめプラン契約ヘッダ情報 DataSource
		/// </summary>
		private BindingSource BindingSourceHeader;

		/// <summary>
		/// おまとめプラン契約詳細情報 DataSource
		/// </summary>
		private BindingSource BindingSourceDetail;

		/// <summary>
		/// 申込情報 DataSource
		/// </summary>
		private BindingSource BindingSourceApply;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MatomeCancelForm()
		{
			InitializeComponent();

			CustomerInfo = null;
			Header = null;
			DetailList = null;
			BindingSourceHeader = null;
			BindingSourceDetail = null;
			BindingSourceApply = null;
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
		private void MatomeCancelForm_Load(object sender, EventArgs e)
		{
			if (null != CustomerInfo)
			{
				labelCustomerNo.Text = CustomerInfo.顧客No.ToString();
				labelCustomerName.Text = CustomerInfo.顧客名;

				string whereStr = string.Format("fEndFlag = '0' AND fDeleteFlag = '0' AND fContractType = 'まとめ' AND fCustomerID = {0}", CustomerInfo.顧客No);
				DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_HEADER], whereStr, "fContractID DESC", Program.gSettings.ConnectCharlie.ConnectionString);
				List<T_USE_CONTRACT_HEADER> headerList = T_USE_CONTRACT_HEADER.DataTableToList(table);
				if (null != headerList && 0 < headerList.Count)
				{
					// おまとめプラン契約ヘッダ情報の設定
					Header = headerList[0];
					BindingSourceHeader = new BindingSource(table, null);
					dataGridViewHeader.DataSource = BindingSourceHeader;

					whereStr = string.Format("fContractID = {0}", Header.fContractID);
					table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_DETAIL], whereStr, "fContractDetailID", Program.gSettings.ConnectCharlie.ConnectionString);
					DetailList = T_USE_CONTRACT_DETAIL.DataTableToList(table);
					if (null != DetailList && 0 < DetailList.Count)
					{
						// おまとめプラン契約詳細情報の設定
						BindingSourceDetail = new BindingSource(table, null);
						dataGridViewDetail.DataSource = BindingSourceDetail;
					}
					// おまとめプランに含まれるサービス群
					List<string> serviceIdList = new List<string>();
					foreach (T_USE_CONTRACT_DETAIL detail in DetailList)
					{
						serviceIdList.Add(detail.fSERVICE_ID.ToString());
					}
					table = MwsServiceCancelToolAccess.DataTable_V_COUPLER_APPLY(Header.fCustomerID, serviceIdList.ToArray(), Header.fApplyDate.Value.ToDate(), Program.gSettings.ConnectCharlie.ConnectionString);
					if (null != table && 0 < table.Rows.Count)
					{
						// カプラー申込情報の設定
						BindingSourceApply = new BindingSource(table, null);
						dataGridViewApply.DataSource = BindingSourceApply;
					}
					if (Header.IsEnableCancel)
					{
						buttonOK.Enabled = true;
					}
					else
					{
						labelMessage.Visible = true;
					}
				}
			}
		}

		/// <summary>
		/// おまとめプラン削除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == MessageBox.Show("本当に削除してもよろしいですか？", "削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				try
				{
#if !DebugNoWrite
					// おまとめプラン契約情報の削除（ヘッダ情報、詳細情報共に削除）
					int result = MwsServiceCancelToolAccess.Delete_Matome(Header, Program.gSettings.ConnectCharlie.ConnectionString);
#endif
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
				catch (Exception ex) 
				{
					MessageBox.Show(ex.Message, "削除", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
