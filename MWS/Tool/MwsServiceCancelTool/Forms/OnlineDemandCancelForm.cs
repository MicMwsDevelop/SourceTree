//
// OnlineDemandCancelForm.cs
// 
// 各種作業料 作業済申請取消画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2025/01/23 勝呂):新規作成
//
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.BaseFactory.MwsServiceCancelTool;
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
	/// 各種作業料 作業済申請取消画面クラス
	/// </summary>
	public partial class OnlineDemandCancelForm : Form
	{
		/// <summary>
		/// 顧客情報
		/// </summary>
		public vMic顧客情報 CustomerInfo { get; set; }

		/// <summary>
		/// 各種作業料作業済申請情報
		/// </summary>
		private List<UseOnlineDemand> OnlineDemandList { get; set; }

		/// <summary>
		/// 各種作業料作業済申請情報 DataSource
		/// </summary>
		private BindingSource BindingSourceOnlineDemand;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OnlineDemandCancelForm()
		{
			InitializeComponent();

			CustomerInfo = null;
			OnlineDemandList = null;
			BindingSourceOnlineDemand = null;
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
		private void OnlineDemandCancelForm_Load(object sender, EventArgs e)
		{
			if (null != CustomerInfo)
			{
				labelCustomerNo.Text = CustomerInfo.顧客No.ToString();
				labelCustomerName.Text = CustomerInfo.顧客名;

				string whereStr = string.Format("DeleteFlag = '0' AND CustomerID = {0}", CustomerInfo.顧客No);
				DataTable table = MwsServiceCancelToolAccess.DataTable_UseOnlineDemand(CustomerInfo.顧客No, Program.gSettings.ConnectCharlie.ConnectionString);
				OnlineDemandList = UseOnlineDemand.DataTableToList(table);
				if (null != OnlineDemandList && 0 < OnlineDemandList.Count)
				{
					// 各種作業料作業済申請情報の設定
					BindingSourceOnlineDemand = new BindingSource(table, null);
					dataGridViewOnlineDemand.DataSource = BindingSourceOnlineDemand;
				}
			}
		}

		/// <summary>
		/// 作業済申請取消
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (DataGridViewRow row in dataGridViewOnlineDemand.SelectedRows)
				{
					UseOnlineDemand demand = OnlineDemandList[row.Index];
					if (demand.IsEnableCancel)
					{
						if (DialogResult.Yes == MessageBox.Show("作業済申請を取消してもよろしいですか？", "作業済申請取消", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
						{
#if !DebugNoWrite
							// [charlieDB].[dbo].[T_USE_ONLINE_DEMAND]の削除
							int result = CharlieDatabaseAccess.Delete_T_USE_ONLINE_DEMAND(demand.ApplyNo, Program.gSettings.ConnectCharlie.ConnectionString);
#endif
							MessageBox.Show("作業済申請の取消処理が正常終了しました。", "作業済申請取消", MessageBoxButtons.OK, MessageBoxIcon.Information);
							this.DialogResult = DialogResult.OK;
							this.Close();
						}
					}
					else
					{
						MessageBox.Show("既に売上データが作成されているため取消できません。\n\n[charlieDB].[dbo].[T_USE_ONLINE_DEMAND]のレコードを物理的に削除する必要があります。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
					return;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "作業済申請取消", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
