using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MwsLib.Component;
using System.IO;
using MwsLib.BaseFactory.ScanImageData;

namespace ScanImageData.Forms
{
	public partial class RegistScanImageForm : Form
	{
		public string ScanDataImagePath { get; set; }

		private string ScanDataImageFolder { get; set; }


		private RegistScanImageForm()
		{
			InitializeComponent();
		}

		public RegistScanImageForm(string path)
		{
			InitializeComponent();

			ScanDataImagePath = path;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RegistScanImageForm_Load(object sender, EventArgs e)
		{
			// 登録・変更を選択
			radioButtonUser.Checked = true;
		}

		/// <summary>
		/// スキャンデータファイルダブルクリック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void explorerListViewScanImage_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (null == explorerTreeViewScanImage.SelectedNode)
			{
				return;
			}
			ExplorerListView targetItem = (ExplorerListView)sender;
			if (0 < targetItem.SelectedItems.Count)
			{
				ListViewItem item = explorerListViewScanImage.SelectedItems[0];
				ShellItem si = item.Tag as ShellItem;
				string ext = Path.GetExtension(si.Path);
				if (0 <= ext.Length)
				{
					using (DisplayScanImageForm form = new DisplayScanImageForm(si.Path))
					{
						form.ShowDialog();
					}
				}
			}
		}

		/// <summary>
		/// 登録・変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonUser_CheckedChanged(object sender, EventArgs e)
		{
			ScanDataImageFolder = Path.Combine(ScanDataImagePath, ScanImageDataDef.FolderUser);
			explorerTreeViewScanImage.UIInit(ScanDataImageFolder);
			explorerListViewScanImage.UIInit(ScanDataImageFolder);

			//explorerTreeViewScanImage.SelectedNode = explorerTreeViewScanImage.TopNode;
		}

		/// <summary>
		/// 保守契約
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonMainte_CheckedChanged(object sender, EventArgs e)
		{
			ScanDataImageFolder = Path.Combine(ScanDataImagePath, ScanImageDataDef.FolderMainte);
			explorerTreeViewScanImage.UIInit(ScanDataImageFolder);
			explorerListViewScanImage.UIInit(ScanDataImageFolder);

			//explorerTreeViewScanImage.SelectedNode = explorerTreeViewScanImage.TopNode;
		}

		/// <summary>
		/// 口座振替
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonAccountTransfer_CheckedChanged(object sender, EventArgs e)
		{
			ScanDataImageFolder = Path.Combine(ScanDataImagePath, ScanImageDataDef.FolderAccountTransfer);
			explorerTreeViewScanImage.UIInit(ScanDataImageFolder);
			explorerListViewScanImage.UIInit(ScanDataImageFolder);

			//explorerTreeViewScanImage.SelectedNode = explorerTreeViewScanImage.TopNode;
		}

		/// <summary>
		/// 取引条件確認書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonTransaction_CheckedChanged(object sender, EventArgs e)
		{
			ScanDataImageFolder = Path.Combine(ScanDataImagePath, ScanImageDataDef.FolderTransaction);
			explorerTreeViewScanImage.UIInit(ScanDataImageFolder);
			explorerListViewScanImage.UIInit(ScanDataImageFolder);

			//explorerTreeViewScanImage.SelectedNode = explorerTreeViewScanImage.TopNode;
		}

		/// <summary>
		/// リモートサービス利用規約同意書
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonConsent_CheckedChanged(object sender, EventArgs e)
		{
			ScanDataImageFolder = Path.Combine(ScanDataImagePath, ScanImageDataDef.FolderConsent);
			explorerTreeViewScanImage.UIInit(ScanDataImageFolder);
			explorerListViewScanImage.UIInit(ScanDataImageFolder);

			//explorerTreeViewScanImage.SelectedNode = explorerTreeViewScanImage.TopNode;
		}
	}
}
