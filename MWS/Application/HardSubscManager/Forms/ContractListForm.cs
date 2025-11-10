//
// ContractListForm.cs
//
// ハードサブスク契約情報画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/10/20 勝呂)
// 
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.DB.SqlServer.HardSubsc;
using HardSubscManager.Forms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HardSubscManager
{
	/// <summary>
	/// ハードサブスク契約情報画面
	/// </summary>
	public partial class ContractListForm : Form
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ContractListForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ContractListForm_Load(object sender, EventArgs e)
		{
			try
			{
				// 顧客情報の設定
				vMic全ユーザー2 clinicInfo = HardSubscAccess.GetClinicInfo(CustomerNo, Program.gSettings.ConnectJunp.ConnectionString);
				if (null != clinicInfo)
				{
					labelCustomerNo.Text = CustomerNo.ToString();
					labelOffice.Text = clinicInfo.支店名;
					labelClinicName.Text = clinicInfo.顧客名;
					labelClinickKana.Text = clinicInfo.フリガナ;
					labelAddress.Text = clinicInfo.住所;
					labelTel.Text = clinicInfo.電話番号;
					labelEndFlag.Text = (clinicInfo.終了フラグ) ? "終了" : "";

					// 契約情報ListViewの設定
					SetHeaderListView(CustomerNo);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 契約情報変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewHeader_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			buttonModify.PerformClick();
		}

		/// <summary>
		/// 契約情報変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonModify_Click(object sender, EventArgs e)
		{
			if (0 < listViewHeader.SelectedIndices.Count)
			{
				ListViewItem lvItem = listViewHeader.SelectedItems[0];
				if (null != lvItem.Tag)
				{
					T_HARD_SUBSC_HEADER header = (T_HARD_SUBSC_HEADER)lvItem.Tag;
					using (ContractForm form = new ContractForm())
					{
						try
						{
							form.ModofyFlag = true;
							form.OrgHeader = header;
							form.OrgDetailList = HardSubscAccess.GetHardSubscDetailList(header.InternalContractNo, Program.gSettings.ConnectCharlie.ConnectionString);
							if (DialogResult.OK == form.ShowDialog())
							{
								this.DialogResult = DialogResult.OK;
								this.Close();
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
		}

		/// <summary>
		/// 契約情報削除
		/// 現機能は安全運用を考慮してマスク中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonDeleteHeader_Click(object sender, EventArgs e)
		{
			if (0 < listViewHeader.SelectedIndices.Count)
			{
				ListViewItem lvItem = listViewHeader.SelectedItems[0];
				if (null != lvItem.Tag)
				{
					T_HARD_SUBSC_HEADER header = (T_HARD_SUBSC_HEADER)lvItem.Tag;
					if (header.IsDelete)
					{
						if (DialogResult.Yes == MessageBox.Show("契約情報を削除してよろしいですか？", Program.ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
						{
							try
							{
								List<T_HARD_SUBSC_DETAIL> detailList = HardSubscAccess.GetHardSubscDetailList(header.InternalContractNo, Program.gSettings.ConnectCharlie.ConnectionString);
								if (null != detailList && 0 < detailList.Count)
								{
									// 機器情報の削除
									HardSubscAccess.DeleteHardSubscDetail(header.InternalContractNo, Program.gSettings.ConnectCharlie.ConnectionString);
								}
								// 契約情報の削除
								HardSubscAccess.DeleteHardSubscHeader(header.InternalContractNo, Program.gSettings.ConnectCharlie.ConnectionString);

								// 契約情報ListViewの設定
								this.SetHeaderListView(header.CustomerID);
							}
							catch (Exception ex)
							{
								MessageBox.Show(ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
					}
					else
					{
						MessageBox.Show("既に課金が開始されているので契約情報は削除できません。\nどうしても削除が必要な場合には、システム管理部にお問い合わせください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
			else
			{
				MessageBox.Show("契約情報を選択してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		/// <summary>
		/// 閉じる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 契約情報ListViewの設定
		/// </summary>
		/// <param name="customerID"></param>
		private void SetHeaderListView(int customerID)
		{
			try
			{
				listViewHeader.Items.Clear();

				// 契約情報リストの取得
				List<T_HARD_SUBSC_HEADER> headerList = HardSubscAccess.GetHardSubscHeaderList(customerID, Program.gSettings.ConnectCharlie.ConnectionString);
				if (null != headerList && 0 < headerList.Count)
				{
					foreach (T_HARD_SUBSC_HEADER header in headerList)
					{
						ListViewItem item = new ListViewItem(Program.GetHeaderListViewItem(header));

						// 解約日は協調表示
						item.UseItemStyleForSubItems = false;
						item.SubItems[8].ForeColor = System.Drawing.Color.Red;

						item.Tag = header;
						listViewHeader.Items.Add(item);
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
