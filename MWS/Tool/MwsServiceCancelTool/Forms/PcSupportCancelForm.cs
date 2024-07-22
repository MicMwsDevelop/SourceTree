//
// PcSupportCancelForm.cs
// 
// PC安心サポート 利用申込取消画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/06/11 勝呂):新規作成
//
using CommonLib.BaseFactory;
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
		private T_USE_PCCSUPPORT PcSupport { get; set; }

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

				string whereStr = string.Format("fEndFlag = '0' AND fDeleteFlag = '0' AND fCustomerID = {0}", CustomerInfo.顧客No);
				DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_PCCSUPPORT], whereStr, "fApplyNo DESC", Program.gSettings.ConnectCharlie.ConnectionString);
				List<T_USE_PCCSUPPORT> list = T_USE_PCCSUPPORT.DataTableToList(table);
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
		/// PC安心サポート削除
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
					// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の削除
					int result = CharlieDatabaseAccess.Delete_T_USE_PCCSUPPORT(PcSupport.fApplyNo, Program.gSettings.ConnectCharlie.ConnectionString);
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
