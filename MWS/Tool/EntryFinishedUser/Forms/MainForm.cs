﻿using DataGridViewAutoFilter;
using MwsLib.BaseFactory.EntryFinishedUser;
using MwsLib.DB.SqlServer.EntryFinishedUser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace EntryFinishedUser.Forms
{
	/// <summary>
	/// 終了ユーザー管理画面
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// 終了ユーザー情報リスト DataSource
		/// </summary>
		private BindingSource dataGridViewFinishedUserBindingSource;

		/// <summary>
		/// 終了ユーザーリスト
		/// </summary>
		private List<EntryFinishedUserData> FinishedUserList { get; set; }

		/// <summary>
		/// リプレース先リスト
		/// </summary>
		public static List<string> gReplaceList;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			dataGridViewFinishedUser.BindingContextChanged += new EventHandler(dataGridViewUser_BindingContextChanged);
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowListForm_Load(object sender, EventArgs e)
		{
			// リプレース先リストの取得
			gReplaceList = EntryFinishedUserAccess.GetReplaceMakerList(Program.DATABACE_ACCEPT_CT);

			// 終了ユーザーリストの設定
			dataGridViewFinishedUser.DataSource = null;
			dataGridViewFinishedUser.Rows.Clear();
			dataGridViewFinishedUser.Columns.Clear();

			DataTable table = EntryFinishedUserGetIO.GetEntryFinishedUserList(Program.DATABACE_ACCEPT_CT);
			dataGridViewFinishedUserBindingSource = new BindingSource(table, null);
			dataGridViewFinishedUser.DataSource = dataGridViewFinishedUserBindingSource;
			FinishedUserList = EntryFinishedUserController.ConvertEntryFinishedUserList(table);

			//dataGridViewUser.DataSource = UserList;
			//string[] title = EntryFinishedUserData.ToTitleArray();
			//for (int i = 0; i < title.Length; i++)
			//{
			//	dataGridViewUser.Columns[i].HeaderText = title[i];
			//}

			// 拠店コードを非表示
			dataGridViewFinishedUser.Columns[4].Visible = false;

			// 終了届受領日を非表示
			dataGridViewFinishedUser.Columns[16].Visible = false;
			dataGridViewFinishedUser.ResumeLayout();

			// レコード件数の表示
			textBoxCount.Text = string.Format("{0}/{1}", dataGridViewFinishedUserBindingSource.Count, FinishedUserList.Count);
		}

		/// <summary>
		/// Configures the autogenerated columns, replacing their header
		/// cells with AutoFilter header cells. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridViewUser_BindingContextChanged(object sender, EventArgs e)
		{
			// Continue only if the data source has been set.
			if (dataGridViewFinishedUser.DataSource == null)
			{
				return;
			}
			// Add the AutoFilter header cell to each column.
			foreach (DataGridViewColumn col in dataGridViewFinishedUser.Columns)
			{
				col.HeaderCell = new DataGridViewAutoFilterColumnHeaderCell(col.HeaderCell);
			}
			// Resize the columns to fit their contents.
			//dataGridViewUser.AutoResizeColumns();
		}

		/// <summary>
		/// 得意先No 入力制限
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxTokuisakiNo_KeyPress(object sender, KeyPressEventArgs e)
		{
			// 0～9と、バックスペース以外の時は、イベントをキャンセルする
			if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		/// <summary>
		/// 得意先Noによる検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSearch_Click(object sender, EventArgs e)
		{
			string tokuisakiNo = textBoxTokuisakiNo.Text.Trim();
			if (6 == tokuisakiNo.Length)
			{
				bool modify = false;
				EntryFinishedUserData user = FinishedUserList.Find(p => p.TokuisakiNo == tokuisakiNo);
				if (null != user)
				{
					modify = true;
				}
				else
				{
					try
					{
						user = EntryFinishedUserAccess.GetCustomerInfo(tokuisakiNo, Program.DATABACE_ACCEPT_CT);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "顧客情報取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					}
				}
				using (EntryFinishedUserForm form = new EntryFinishedUserForm(user, modify))
				{
					if (DialogResult.OK == form.ShowDialog())
					{
						// DataSourceのクリア
						((DataTable)dataGridViewFinishedUserBindingSource.DataSource).Clear();

						// 終了ユーザーリストの設定
						DataTable table = EntryFinishedUserGetIO.GetEntryFinishedUserList(Program.DATABACE_ACCEPT_CT);
						dataGridViewFinishedUserBindingSource = new BindingSource(table, null);
						dataGridViewFinishedUser.DataSource = dataGridViewFinishedUserBindingSource;
						FinishedUserList = EntryFinishedUserController.ConvertEntryFinishedUserList(table);

						// レコード件数の表示
						textBoxCount.Text = string.Format("{0}/{1}", dataGridViewFinishedUserBindingSource.Count, FinishedUserList.Count);
					}
				}
			}
		}

		/// <summary>
		/// 終了ユーザー情報の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dataGridViewFinishedUser_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			string tokuisakiNo = (string)dataGridViewFinishedUser.CurrentRow.Cells[0].Value;
			EntryFinishedUserData user = FinishedUserList.Find(p => p.TokuisakiNo == tokuisakiNo);
			if (null != user)
			{
				using (EntryFinishedUserForm form = new EntryFinishedUserForm(user, true))
				{
					if (DialogResult.OK == form.ShowDialog())
					{
						// DataSourceのクリア
						((DataTable)dataGridViewFinishedUserBindingSource.DataSource).Clear();

						// 終了ユーザーリストの設定
						DataTable table = EntryFinishedUserGetIO.GetEntryFinishedUserList(Program.DATABACE_ACCEPT_CT);
						dataGridViewFinishedUserBindingSource = new BindingSource(table, null);
						dataGridViewFinishedUser.DataSource = dataGridViewFinishedUserBindingSource;
						FinishedUserList = EntryFinishedUserController.ConvertEntryFinishedUserList(table);
					}
				}
			}
		}
	}
}