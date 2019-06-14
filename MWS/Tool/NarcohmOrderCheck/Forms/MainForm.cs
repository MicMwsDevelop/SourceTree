using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MwsLib.DB.SqlServer.NarcohmOrderCheck;
using MwsLib.BaseFactory.NarcohmOrderCheck;

namespace NarcohmOrderCheck.Forms
{
	public partial class MainForm : Form
	{
		//private List<NarcohmApplicate> ApplicateList;


		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			List<NarcohmApplicate> list = NarcohmOrderCheckAccess.GetNarcohmApplicateList(true);
			foreach (NarcohmApplicate header in list)
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
					form.ApplicateInfo = lvItem.Tag as NarcohmApplicate;
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

		}
	}
}
