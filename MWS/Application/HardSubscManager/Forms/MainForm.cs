//
// MainForm.cs
//
// ハードサブスク情報管理 メイン画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/04/03 勝呂)
// 
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.DB.SqlServer.HardSubscManager;
using CommonLib.DB.SqlServer.Junp;
using HardSubscManager.Forms;
using HardSubscManager.Settings;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HardSubscManager
{
	public partial class MainForm : Form
	{

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
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
			// 環境設定の読込
			Program.gSettings = HardSubscManagerSettingsIF.GetSettings();

			// バージョン情報設定
			labelVersion.Text = Program.ProgramVersion;

			// ログインユーザー情報の取得
			string whereStr = string.Format("[fUsrLoginID] = '{0}'", Environment.UserName);
			List<tUser> userList = JunpDatabaseAccess.Select_tUser(whereStr, "", Program.gSettings.ConnectJunp.ConnectionString);
			if (null != userList && 0 < userList.Count)
			{
				Program.UserInfo = userList[0];
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
				vMic全ユーザー2 user = HardSubscManagerAccess.GetUserInfo(customerNo, Program.gSettings.ConnectJunp.ConnectionString);
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
		/// 契約情報追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAddHeader_Click(object sender, EventArgs e)
		{
			if (0 < labelClinicName.Text.Length) 
			{
				using (HeaderDetailForm form = new HeaderDetailForm())
				{
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
		/// 契約情報の修正
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewHeader_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (0 < listViewHeader.SelectedIndices.Count)
			{
				ListViewItem lvItem = listViewHeader.SelectedItems[0];
				if (null != lvItem.Tag)
				{
					T_HARDSUBSC_HEADER header = (T_HARDSUBSC_HEADER)lvItem.Tag;
					using (HeaderDetailForm form = new HeaderDetailForm())
					{
						form.CustomerNo = header.CustomerID;
						form.SaveHeader = header;
						if (DialogResult.OK == form.ShowDialog())
						{
							// 契約情報ListViewの設定
							this.SetHeaderListView(header.CustomerID);
						}
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
				if (DialogResult.Yes == MessageBox.Show("契約情報を削除してよろしいですか？", Program.ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					ListViewItem lvItem = listViewHeader.SelectedItems[0];
					if (null != lvItem.Tag)
					{
						T_HARDSUBSC_HEADER header = (T_HARDSUBSC_HEADER)lvItem.Tag;
						if (false == header.IsDelete)
						{
							try
							{
								List<T_HARDSUBSC_DETAIL> detailList = HardSubscManagerAccess.GetHardSubscDetailList(header.RentalNo, Program.gSettings.ConnectCharlie.ConnectionString);
								if (null != detailList && 0 < detailList.Count)
								{
									// 機器情報の削除
									HardSubscManagerAccess.DeleteHardSubscDetail(header.RentalNo, Program.gSettings.ConnectCharlie.ConnectionString);
								}
								// 契約情報の削除
								HardSubscManagerAccess.DeleteHardSubscHeader(header.RentalNo, Program.gSettings.ConnectCharlie.ConnectionString);

								// 契約情報ListViewの設定
								this.SetHeaderListView(header.CustomerID);
							}
							catch (Exception ex)
							{
								MessageBox.Show(ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
						else
						{
							MessageBox.Show("既に課金されている契約情報は削除できません。\nどうしても削除したい場合には、システム管理部にお問い合わせください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
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
				List<T_HARDSUBSC_HEADER> headerList = HardSubscManagerAccess.GetHardSubscHeaderList(customerID, Program.gSettings.ConnectCharlie.ConnectionString);
				if (null != headerList && 0 < headerList.Count)
				{
					foreach (T_HARDSUBSC_HEADER header in headerList)
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
