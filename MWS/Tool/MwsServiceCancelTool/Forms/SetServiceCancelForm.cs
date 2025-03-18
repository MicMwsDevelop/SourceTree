//
// SetServiceCancelForm.cs
// 
// セット割サービス 利用申込取消画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2025/01/23 勝呂):新規作成
//
using CommonLib.BaseFactory.Charlie.Table;
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
	/// セット割サービス 利用申込取消画面クラス
	/// </summary>
	public partial class SetServiceCancelForm : Form
	{
		/// <summary>
		/// 顧客情報
		/// </summary>
		public vMic顧客情報 CustomerInfo { get; set; }

		/// <summary>
		/// 契約ヘッダ情報リスト
		/// </summary>
		private List<UseContractHeader> HeaderList { get; set; }

		/// <summary>
		/// 契約ヘッダ情報 DataSource
		/// </summary>
		private BindingSource BindingSourceHeader;

		/// <summary>
		/// 申込情報 DataSource
		/// </summary>
		private BindingSource BindingSourceApply;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SetServiceCancelForm()
		{
			InitializeComponent();

			CustomerInfo = null;
			HeaderList = null;
			BindingSourceHeader = null;
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
		private void SetServiceCancelForm_Load(object sender, EventArgs e)
		{
			if (null != CustomerInfo)
			{
				labelCustomerNo.Text = CustomerInfo.顧客No.ToString();
				labelCustomerName.Text = CustomerInfo.顧客名;

				DataTable table = MwsServiceCancelToolAccess.DataTable_UseContractHeaderSetService(CustomerInfo.顧客No, Program.gSettings.ConnectCharlie.ConnectionString);
				HeaderList = UseContractHeader.DataTableToList(table);
				if (null != HeaderList && 0 < HeaderList.Count)
				{
					// 契約ヘッダ情報の設定
					BindingSourceHeader = new BindingSource(table, null);
					dataGridViewHeader.DataSource = BindingSourceHeader;

					// カプラー申込情報の設定
					for (int i  = 0; i < HeaderList.Count; i++)
					{
						string whereStr = string.Format("GOODS_ID = {0}", HeaderList[i].fGoodsID);
						List<M_CODE> mcodeList = CharlieDatabaseAccess.Select_M_CODE(whereStr, "SERVICE_ID", Program.gSettings.ConnectCharlie.ConnectionString);
						if (null != mcodeList && 0 < mcodeList.Count)
						{
							List<string> serviceIdList = new List<string>();
							foreach (M_CODE mcode in mcodeList)
							{
								serviceIdList.Add(mcode.SERVICE_ID.ToString());
							}
							table = MwsServiceCancelToolAccess.DataTable_V_COUPLER_APPLY(HeaderList[i].fCustomerID, serviceIdList.ToArray(), HeaderList[i].fApplyDate.Value.ToDate(), Program.gSettings.ConnectCharlie.ConnectionString);
							dataGridViewHeader.Rows[i].Tag = table;
						}
					}
					dataGridViewHeader.Rows[0].Selected = true;
				}
			}
		}

		/// <summary>
		/// 契約ヘッダ情報グリッドビューの選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridViewHeader_SelectionChanged(object sender, EventArgs e)
		{
			// ヘッダーを残しデータをクリア
			//if (null != dataGridViewApply.DataSource)
			//{
			//	DataTable table = (DataTable)dataGridViewApply.DataSource;
			//	table.Clear();
			//}
			foreach (DataGridViewRow row in dataGridViewHeader.SelectedRows)
			{
				if (null != row.Tag)
				{
					DataTable table = row.Tag as DataTable;
					BindingSourceApply = new BindingSource(table, null);
					dataGridViewApply.DataSource = BindingSourceApply;
					break;
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
			try
			{
				foreach (DataGridViewRow row in dataGridViewHeader.SelectedRows)
				{
					UseContractHeader header = HeaderList[row.Index];
					if (header.IsEnableCancel)
					{
						if (DialogResult.Yes == MessageBox.Show("セット割サービスの利用申込を取り消してよろしいですか？", "利用申込取消", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
						{
#if !DebugNoWrite
							// [charlieDB].[dbo].[T_USE_CONTRACT_HEADER]の削除
							int result = CharlieDatabaseAccess.Delete_T_USE_CONTRACT_HEADER(header.fContractID, Program.gSettings.ConnectCharlie.ConnectionString);
#endif
							MessageBox.Show("セット割サービスの利用申込取消処理が正常終了しました。", "利用申込取消", MessageBoxButtons.OK, MessageBoxIcon.Information);
							this.DialogResult = DialogResult.OK;
							this.Close();
						}
					}
					else
					{
						MessageBox.Show("既に契約済みのため、利用申込の取消はできません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
					break;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "利用申込取消", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
