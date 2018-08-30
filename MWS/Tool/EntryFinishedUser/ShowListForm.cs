using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MwsLib.BaseFactory.EntryFinishedUser;

namespace EntryFinishedUser
{
	public partial class ShowListForm : Form
	{
		/// <summary>
		/// リスト参照データ
		/// </summary>
		public List<EntryFinishedUserData> UserList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		private ShowListForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="list">リスト参照データ</param>
		public ShowListForm(List<EntryFinishedUserData> list)
		{
			InitializeComponent();

			UserList = list;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowListForm_Load(object sender, EventArgs e)
		{
			dataGridViewUser.DataSource = null;
			dataGridViewUser.Rows.Clear();
			dataGridViewUser.Columns.Clear();
			dataGridViewUser.ReadOnly = true;

			dataGridViewUser.DataSource = UserList;
			string[] title = EntryFinishedUserData.ToTitleArray();
			for (int i = 0; i < title.Length; i++)
			{
				dataGridViewUser.Columns[i].HeaderText = title[i];
			}
			// 終了届受領日を非表示
			dataGridViewUser.Columns[title.Length].Visible = false;

			dataGridViewUser.ResumeLayout();
		}
	}
}
