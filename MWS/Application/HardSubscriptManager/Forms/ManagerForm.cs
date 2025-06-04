//
// ManagerForm.cs
//
// ハードサブスク契約管理画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/04/03 勝呂)
// 
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.DB.SqlServer.HardSubscript;
using HardSubscriptManager.Forms;
using HardSubscriptManager.Settings;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HardSubscriptManager
{
	/// <summary>
	/// メイン画面
	/// </summary>
	public partial class ManagerForm : Form
	{
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ManagerForm()
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
			// バージョン情報設定
			labelVersion.Text = Program.ProgramVersion;

			// ログインユーザー情報の取得
			tUser user = HardSubscriptAccess.GetLoginUser(Environment.UserName, Program.gSettings.ConnectJunp.ConnectionString);
			if (null != user)
			{
				Program.UserInfo = user;
			}
		}

		/// <summary>
		/// 顧客Noの入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSearch_Click(object sender, EventArgs e)
		{
			// クリア
			labelOffice.Text = string.Empty;
			labelClinicName.Text = string.Empty;
			labelClinickKana.Text = string.Empty;
			labelAddress.Text = string.Empty;
			labelTel.Text = string.Empty;
			labelEndFlag.Text = string.Empty;
			listViewHeader.Items.Clear();

			try
			{
				int customerNo = numericTextBoxCustomerID.ToInt();
				vMic全ユーザー2 user = HardSubscriptAccess.GetClinicInfo(customerNo, Program.gSettings.ConnectJunp.ConnectionString);
				if (null != user)
				{
					// 顧客情報の設定
					labelOffice.Text = user.支店名;
					labelClinicName.Text = user.顧客名;
					labelClinickKana.Text = user.フリガナ;
					labelAddress.Text = user.住所;
					labelTel.Text = user.電話番号;
					labelEndFlag.Text = (user.終了フラグ) ? "終了" : "";

					// 契約情報ListViewの設定
					this.SetHeaderListView(customerNo);
				}
				else
				{
					MessageBox.Show("顧客Noに該当する医院が見つかりません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 新規申込
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAddNew_Click(object sender, EventArgs e)
		{
			if (0 < labelClinicName.Text.Length) 
			{
				using (HeaderDetailForm form = new HeaderDetailForm())
				{
					form.InputMode = HeaderDetailForm.TInputMode.AddNew;
					form.CustomerNo = numericTextBoxCustomerID.ToInt();
					if (DialogResult.OK == form.ShowDialog())
					{
						// 契約情報ListViewの設定
						this.SetHeaderListView(form.CustomerNo);
					}
				}
			}
			else
			{
				MessageBox.Show("医院が指定されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		/// <summary>
		/// 契約情報修正
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewHeader_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			buttonModify.PerformClick();
		}

		/// <summary>
		/// 契約情報修正
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
					T_HARD_SUBSCRIPT_HEADER header = (T_HARD_SUBSCRIPT_HEADER)lvItem.Tag;
					if (header.IsModify)
					{
						using (HeaderDetailForm form = new HeaderDetailForm())
						{
							form.InputMode = HeaderDetailForm.TInputMode.Modify;
							form.CustomerNo = header.CustomerID;
							form.SaveHeader = header;
							if (DialogResult.OK == form.ShowDialog())
							{
								// 契約情報ListViewの設定
								this.SetHeaderListView(header.CustomerID);
							}
						}
					}
					else
					{
						MessageBox.Show("既に利用されているので契約情報の修正はできません。\nどうしても修正が必要な場合には、システム管理部にお問い合わせください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
		}

		/// <summary>
		/// 利用開始日入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInputUseStartDate_Click(object sender, EventArgs e)
		{
			if (0 < listViewHeader.SelectedIndices.Count)
			{
				ListViewItem lvItem = listViewHeader.SelectedItems[0];
				if (null != lvItem.Tag)
				{
					T_HARD_SUBSCRIPT_HEADER header = (T_HARD_SUBSCRIPT_HEADER)lvItem.Tag;
					if (header.IsEnableContractStartDate)
					{
						using (HeaderDetailForm form = new HeaderDetailForm())
						{
							form.InputMode = HeaderDetailForm.TInputMode.UseStartDate;
							form.CustomerNo = header.CustomerID;
							form.SaveHeader = header;
							if (DialogResult.OK == form.ShowDialog())
							{
								// 契約情報ListViewの設定
								this.SetHeaderListView(header.CustomerID);
							}
						}
					}
					else
					{
						MessageBox.Show("既に課金が開始されているので利用開始日の修正はできません。\nどうしても修正が必要な場合には、システム管理部にお問い合わせください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
		}

		/// <summary>
		/// 解約日入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInputCancelDate_Click(object sender, EventArgs e)
		{
			if (0 < listViewHeader.SelectedIndices.Count)
			{
				ListViewItem lvItem = listViewHeader.SelectedItems[0];
				if (null != lvItem.Tag)
				{
					T_HARD_SUBSCRIPT_HEADER header = (T_HARD_SUBSCRIPT_HEADER)lvItem.Tag;
					if (header.IsEnableCancelDate)
					{
						using (HeaderDetailForm form = new HeaderDetailForm())
						{
							form.InputMode = HeaderDetailForm.TInputMode.CancelDate;
							form.CustomerNo = header.CustomerID;
							form.SaveHeader = header;
							if (DialogResult.OK == form.ShowDialog())
							{
								// 契約情報ListViewの設定
								this.SetHeaderListView(header.CustomerID);
							}
						}
					}
					else
					{
						MessageBox.Show("利用開始日が設定されていない。または既にサービスが終了しているので解約日は入力できません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
		}

		/// <summary>
		/// 契約情報削除
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
					T_HARD_SUBSCRIPT_HEADER header = (T_HARD_SUBSCRIPT_HEADER)lvItem.Tag;
					if (header.IsDelete)
					{
						if (DialogResult.Yes == MessageBox.Show("契約情報を削除してよろしいですか？", Program.ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
						{
							try
							{
								List<T_HARD_SUBSCRIPT_DETAIL> detailList = HardSubscriptAccess.GetHardSubscriptDetailList(header.InternalRentalNo, Program.gSettings.ConnectCharlie.ConnectionString);
								if (null != detailList && 0 < detailList.Count)
								{
									// 機器情報の削除
									HardSubscriptAccess.DeleteHardSubscriptDetail(header.InternalRentalNo, Program.gSettings.ConnectCharlie.ConnectionString);
								}
								// 契約情報の削除
								HardSubscriptAccess.DeleteHardSubscriptHeader(header.InternalRentalNo, Program.gSettings.ConnectCharlie.ConnectionString);

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
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				listViewHeader.Items.Clear();

				// 契約情報リストの取得
				List<T_HARD_SUBSCRIPT_HEADER> headerList = HardSubscriptAccess.GetHardSubscriptHeaderList(customerID, Program.gSettings.ConnectCharlie.ConnectionString);
				if (null != headerList && 0 < headerList.Count)
				{
					foreach (T_HARD_SUBSCRIPT_HEADER header in headerList)
					{
						ListViewItem item = new ListViewItem(Program.GetHeaderListViewItem(header));
						item.Tag = header;
						listViewHeader.Items.Add(item);
					}
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
