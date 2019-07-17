using MwsLib.BaseFactory.Charlie.Table;
using MwsLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NarcohmOrderCheck.Forms
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
			List<T_NARCOHM_APPLICATE_HEADER> list = CharlieDatabaseAccess.Select_T_NARCOHM_APPLICATE_HEADER(Program.DATABACE_ACCEPT_CT);
			foreach (T_NARCOHM_APPLICATE_HEADER header in list)
			{
				ListViewItem lvItem = new ListViewItem(header.GetListViewData());
				lvItem.Tag = header;
				listViewApplicate.Items.Add(lvItem);
			}
		}

		/// <summary>
		/// 追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAdd_Click(object sender, EventArgs e)
		{
			using (NarcohmApplicateForm form = new NarcohmApplicateForm())
			{
				if (DialogResult.OK == form.ShowDialog())
				{
					ListViewItem lvItem = new ListViewItem(form.ApplicateInfo.GetListViewData());
					lvItem.Tag = form.ApplicateInfo;
					listViewApplicate.Items.Add(lvItem);
					listViewApplicate.Items[listViewApplicate.Items.Count - 1].Selected = true;
				}
			}
		}

		/// <summary>
		/// 変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonModify_Click(object sender, EventArgs e)
		{
			if (0 < listViewApplicate.SelectedIndices.Count)
			{
				ListViewItem lvItem = listViewApplicate.SelectedItems[0];
				using (NarcohmApplicateForm form = new NarcohmApplicateForm())
				{
					form.ApplicateInfo = lvItem.Tag as T_NARCOHM_APPLICATE_HEADER;
					if (DialogResult.OK == form.ShowDialog())
					{
						lvItem.Tag = form.ApplicateInfo;
						string[] data = form.ApplicateInfo.GetListViewData();
						lvItem.Text = data[0];
						lvItem.SubItems[1].Text = data[1];
						lvItem.SubItems[2].Text = data[2];
						lvItem.SubItems[3].Text = data[3];
						lvItem.SubItems[4].Text = data[4];
						lvItem.SubItems[5].Text = data[5];
						lvItem.SubItems[6].Text = data[6];
						lvItem.SubItems[7].Text = data[7];
						lvItem.SubItems[8].Text = data[8];
						lvItem.SubItems[9].Text = data[9];
						lvItem.SubItems[10].Text = data[10];
						lvItem.SubItems[11].Text = data[11];
						lvItem.SubItems[12].Text = data[12];
						lvItem.SubItems[13].Text = data[13];
					}
				}
			}
		}

		/// <summary>
		/// 変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewApplicate_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			buttonModify.PerformClick();
		}

		/// <summary>
		/// 削除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonRemove_Click(object sender, EventArgs e)
		{
			if (0 < listViewApplicate.SelectedIndices.Count)
			{
				if (DialogResult.Yes == MessageBox.Show("本当に削除してもよろしいですか", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					listViewApplicate.Items.Remove(listViewApplicate.SelectedItems[0]);
				}
			}
		}
	}
}
