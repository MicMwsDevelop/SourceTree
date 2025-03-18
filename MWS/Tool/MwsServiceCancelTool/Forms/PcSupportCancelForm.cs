//
// PcSupportCancelForm.cs
// 
// PC安心サポート 利用申込取消画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2025/01/23 勝呂):新規作成
//
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.BaseFactory.MwsServiceCancelTool;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.MwsServiceCancelTool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace MwsServiceCancelTool.Forms
{
	/// <summary>
	/// PC安心サポート 利用申込取消画面クラス
	/// </summary>
	public partial class PcSupportCancelForm : Form
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
		/// PC安心サポート契約情報 DataSource
		/// </summary>
		private BindingSource BindingSourcePcSupport;

		/// <summary>
		/// 申込情報 DataSource
		/// </summary>
		private BindingSource BindingSourceApply;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PcSupportCancelForm()
		{
			InitializeComponent();

			CustomerInfo = null;
			PcSupport = null;
			BindingSourcePcSupport = null;
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
		private void PcSupportCancelForm_Load(object sender, EventArgs e)
		{
			if (null != CustomerInfo)
			{
				labelCustomerNo.Text = CustomerInfo.顧客No.ToString();
				labelCustomerName.Text = CustomerInfo.顧客名;

				DataTable table = MwsServiceCancelToolAccess.DataTable_UseContractPcSupport(CustomerInfo.顧客No, Program.gSettings.ConnectCharlie.ConnectionString);
				List<UseContractPcSupport> list = UseContractPcSupport.DataTableToList(table);
				if (null != list && 0 < list.Count)
				{
					// PC安心サポート契約情報の設定
					PcSupport = list[0];
					BindingSourcePcSupport = new BindingSource(table, null);
					dataGridViewPcSupport.DataSource = BindingSourcePcSupport;

					// クラウドバックアップ（PC安心サポートPlus）
					int serviceID = (int)ServiceCodeDefine.ServiceCode.ExCloudBackupPcSupportPlus;
					string[] array = new string[1] { serviceID.ToString() };
					table = MwsServiceCancelToolAccess.DataTable_V_COUPLER_APPLY(PcSupport.fCustomerID, array, PcSupport.fApplyDate.Value.ToDate(), Program.gSettings.ConnectCharlie.ConnectionString);
					if (null != table && 0 < table.Rows.Count)
					{
						// カプラー申込情報の設定
						BindingSourceApply = new BindingSource(table, null);
						dataGridViewApply.DataSource = BindingSourceApply;
					}
					if (PcSupport.IsEnableCancel)
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
				MessageBox.Show("カプラー申込情報があります。本取消処理を行う前に管理画面にて、クラウドバックアップの利用申込の取消処理を行ってください。", "利用申込取消", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				return;
			}
			if (DialogResult.Yes == MessageBox.Show("PC安心サポートの利用申込を取り消してよろしいですか？", "利用申込取消", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				try
				{
#if !DebugNoWrite
					// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の削除
					int result = CharlieDatabaseAccess.Delete_T_USE_PCCSUPPORT(PcSupport.fApplyNo, Program.gSettings.ConnectCharlie.ConnectionString);
#endif
					MessageBox.Show("PC安心サポートの利用申込取消処理が正常終了しました。", "利用申込取消", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
