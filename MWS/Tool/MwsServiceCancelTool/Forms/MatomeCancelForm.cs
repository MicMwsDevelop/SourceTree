//
// MatomeCancelForm.cs
// 
// おまとめプラン 利用申込取消画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2025/01/23 勝呂):新規作成
//
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
	/// おまとめプラン 利用申込取消画面クラス
	/// </summary>
	public partial class MatomeCancelForm : Form
	{
		/// <summary>
		/// 顧客情報
		/// </summary>
		public vMic顧客情報 CustomerInfo { get; set; }

		/// <summary>
		/// 契約ヘッダ情報
		/// </summary>
		private UseContractHeader Header { get; set; }

		/// <summary>
		/// 契約詳細情報
		/// </summary>
		private List<UseContractDetail> DetailList { get; set; }

		/// <summary>
		/// 契約ヘッダ情報 DataSource
		/// </summary>
		private BindingSource BindingSourceHeader;

		/// <summary>
		/// 契約詳細情報 DataSource
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

				DataTable table = MwsServiceCancelToolAccess.DataTable_UseContractHeaderMatome(CustomerInfo.顧客No, Program.gSettings.ConnectCharlie.ConnectionString);
				List<UseContractHeader> headerList = UseContractHeader.DataTableToList(table);
				if (null != headerList && 0 < headerList.Count)
				{
					// 契約ヘッダ情報の設定
					Header = headerList[0];
					BindingSourceHeader = new BindingSource(table, null);
					dataGridViewHeader.DataSource = BindingSourceHeader;

					table = MwsServiceCancelToolAccess.DataTable_UseContractDetail(Header.fContractID, Program.gSettings.ConnectCharlie.ConnectionString);
					DetailList = UseContractDetail.DataTableToList(table);
					if (null != DetailList && 0 < DetailList.Count)
					{
						// 契約詳細情報の設定
						BindingSourceDetail = new BindingSource(table, null);
						dataGridViewDetail.DataSource = BindingSourceDetail;
					}
					// おまとめプランに含まれるサービス群
					List<string> serviceIdList = new List<string>();
					foreach (UseContractDetail detail in DetailList)
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
						// 利用申込の取消が可能
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
		/// 利用申込取消
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (0 < dataGridViewApply.RowCount)
			{
				MessageBox.Show("申込情報があります。本取消処理を行う前に管理画面にて、サービスの利用申込の取消処理を行ってください。", "利用申込取消", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				return;
			}
			if (DialogResult.Yes == MessageBox.Show("おまとめプラン利用申込を取り消してよろしいですか？", "利用申込取消", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				try
				{
#if !DebugNoWrite
					// おまとめプラン契約情報の削除（契約ヘッダ情報、契約詳細情報を共に削除）
					int result = MwsServiceCancelToolAccess.Delete_UseContractMatome(Header.fContractID, Program.gSettings.ConnectCharlie.ConnectionString);
#endif
					MessageBox.Show("おまとめプランの利用申込取消処理が正常終了しました。", "利用申込取消", MessageBoxButtons.OK, MessageBoxIcon.Information);
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
				catch (Exception ex) 
				{
					MessageBox.Show(ex.Message, "利用申込取消", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
