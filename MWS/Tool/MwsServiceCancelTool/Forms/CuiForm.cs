//
// CuiForm.cs
// 
// 顧客管理利用情報編集ツール メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/11/01 勝呂):新規作成
//
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.BaseFactory.MwsServiceCancelTool;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using CommonLib.DB.SqlServer.MwsServiceCancelTool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace MwsServiceCancelTool.Forms
{
	public partial class CuiForm : Form
	{
		/// <summary>
		/// 顧客管理利用情報リスト DataSource
		/// </summary>
		private BindingSource dataGridViewCuiBindingSource;

		/// <summary>
		/// 顧客管理利用情報リスト
		/// </summary>
		private List<EditCustomerUseInformation> CuiList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CuiForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// 検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSearch_Click(object sender, EventArgs e)
		{
			int customerNo = textBoxCustomerNo.Text.ToInt();
			if (MwsDefine.CustomerNoLength != customerNo.ToString().Length)
			{
				return;
			}
			// 顧客名の取得
			List<vMic顧客情報> userList = JunpDatabaseAccess.Select_vMic顧客情報(string.Format("顧客No = {0}", customerNo), "", Program.gSettings.ConnectJunp.ConnectionString);
			if (null != userList && 0 < userList.Count)
			{
				labelCustomerName.Text = userList[0].顧客名;
			}
			// 顧客管理利用情報の設定
			DataTable table = MwsServiceCancelToolAccess.DataTable_EditCustomerUseInformation(customerNo, Program.gSettings.ConnectCharlie.ConnectionString);
			dataGridViewCuiBindingSource = new BindingSource(table, null);
			dataGridViewCui.DataSource = dataGridViewCuiBindingSource;
			CuiList = EditCustomerUseInformation.DataTableToList(table);

			// 非表示カラムの設定
			dataGridViewCui.Columns[1].Visible = false;     // SERVICE_TYPE_ID
			dataGridViewCui.Columns[4].Visible = false;     // GOODS_ID
			dataGridViewCui.Columns[5].Visible = false;     // APPLICATION_NO
			dataGridViewCui.Columns[6].Visible = false;     // KAKIN_START_DATE
			dataGridViewCui.Columns[12].Visible = false;    // DELETE_FLG
			dataGridViewCui.ResumeLayout();
		}

		/// <summary>
		/// dataGridViewCui MouseDoubleClick
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridViewCui_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			int serviceID = (int)dataGridViewCui.CurrentRow.Cells[2].Value;
			EditCustomerUseInformation cui = CuiList.Find(p => p.SERVICE_ID == serviceID);
			if (null != cui)
			{
				using (CuiEditForm dlg = new CuiEditForm())
				{
					dlg.SetCUI(cui);
					if (DialogResult.OK == dlg.ShowDialog())
					{
						//CharlieDatabaseAccess.UpdateSet_T_CUSSTOMER_USE_INFOMATION(dlg.CUI, Program.gSettings.ConnectCharlie.ConnectionString);

						// DataSourceのクリア
						((DataTable)dataGridViewCuiBindingSource.DataSource).Clear();

						// 顧客管理利用情報の設定
						int customerNo = textBoxCustomerNo.Text.ToInt();
						DataTable table = MwsServiceCancelToolAccess.DataTable_EditCustomerUseInformation(customerNo, Program.gSettings.ConnectCharlie.ConnectionString);
						dataGridViewCuiBindingSource = new BindingSource(table, null);
						dataGridViewCui.DataSource = dataGridViewCuiBindingSource;
						CuiList = EditCustomerUseInformation.DataTableToList(table);

						// サービスの選択
						int index = dataGridViewCuiBindingSource.Find("SERVICE_ID", serviceID);
						dataGridViewCui.Rows[index].Selected = true;
						dataGridViewCui.FirstDisplayedScrollingRowIndex = index;
					}
				}
			}
		}
	}
}
