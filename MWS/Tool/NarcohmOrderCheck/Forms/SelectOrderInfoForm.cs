using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MwsLib.BaseFactory.NarcohmOrderCheck;
using MwsLib.Common;

namespace NarcohmOrderCheck.Forms
{
	public partial class SelectOrderInfoForm : Form
	{
		public List<NarcohmOrderInfo> OrderInfoList;

		public List<NarcohmOrderInfo> SelectOrderInfoList;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SelectOrderInfoForm()
		{
			InitializeComponent();

			OrderInfoList = null;
			SelectOrderInfoList = null;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectOrderInfoForm_Load(object sender, EventArgs e)
		{
			foreach (NarcohmOrderInfo info in OrderInfoList)
			{
				ListViewItem lvItem = new ListViewItem(info.GetListViewData());
				lvItem.Tag = info;
				listViewOrderInfo.Items.Add(lvItem);
			}
		}

		/// <summary>
		/// 受注伝票の選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewOrderInfo_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			buttonOK.PerformClick();
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (0 < listViewOrderInfo.SelectedIndices.Count)
			{
				SelectOrderInfoList = new List<NarcohmOrderInfo>();
				foreach (ListViewItem item in listViewOrderInfo.SelectedItems)
				{
					NarcohmOrderInfo order = item.Tag as NarcohmOrderInfo;
					SelectOrderInfoList.Add(order.CloneDeep());
				}
				this.DialogResult = DialogResult.OK;
			}
			else
			{
				this.DialogResult = DialogResult.Cancel;
			}
			this.Close();
		}
	}
}
