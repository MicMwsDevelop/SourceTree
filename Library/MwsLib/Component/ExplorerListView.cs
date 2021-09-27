using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MwsLib.Component
{
	public partial class ExplorerListView : ListView
	{
		private ShellNamespaceManager shellNamespaceManager;
		private SystemImageList systemImageList_Normal;
		private SystemImageList systemImageList_Small;

		public ExplorerListView()
		{
			InitializeComponent();
			init();
		}

		public ExplorerListView(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
			init();
		}

		private void init()
		{
			systemImageList_Normal = new SystemImageList(false, SystemImageList.SystemIconSize.ExtraLarge);
			systemImageList_Normal.ListViewSetImageList(base.Handle, SystemImageList.ListViewIconSetMode.Normal);
			systemImageList_Small = new SystemImageList(false, SystemImageList.SystemIconSize.Small);
			systemImageList_Small.ListViewSetImageList(base.Handle, SystemImageList.ListViewIconSetMode.Small);
			shellNamespaceManager = new ShellNamespaceManager();
			CreateDetailsColumn();
		}

		public void UIInit(string rootPath)
		{
			if (DesignMode == false)
			{
				LoadDesktopFolder(rootPath);
			}
		}

		private void LoadDesktopFolder(string rootPath)
		{
			//ShellItem m_shDesktop = shellNamespaceManager.GetDesktopShellItem();
			ShellItem m_shDesktop = shellNamespaceManager.GetShellItemFromFilePath(rootPath);

			List<ShellItem> itemList = m_shDesktop.GetSubItems(true);

			base.Items.Clear();
			foreach (ShellItem si in itemList)
			{
				ListViewItem lvItem = new ListViewItem();
				lvItem.Text = si.DisplayName;
				lvItem.ImageIndex = si.IconIndex;
				lvItem.Tag = si;
				m_shDesktop.GetData(si);

				if ((si.IsStream == true && si.IsFileSystem == true) || (si.IsFolder == true && si.IsFileSystem == true))
				{
					if (si.LastAccessTime == DateTime.MinValue)
					{
						lvItem.SubItems.Add("");
					}
					else
					{
						lvItem.SubItems.Add(si.LastWriteTime.ToString("yyyy-MM-dd HH:mm"));
					}
				}
				else
				{
					lvItem.SubItems.Add("");
				}
				lvItem.SubItems.Add(si.TypeName);
				lvItem.SubItems.Add(FileSizeToString(si.FileSize));
				base.Items.Add(lvItem);
			}
		}

		protected override void OnDoubleClick(EventArgs e)
		{
			base.OnDoubleClick(e);

			if (SelectedItems.Count > 0)
			{
				ChangeCurentDirectory((ShellItem)SelectedItems[0].Tag);
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (e.KeyCode == Keys.Enter)
			{
				if (SelectedItems.Count > 0)
				{
					ChangeCurentDirectory((ShellItem)SelectedItems[0].Tag);
				}
			}
		}

		public void ChangeCurentDirectory(ShellItem ssi)
		{
			if (ssi.IsFolder == true)
			{
				List<ShellItem> itemList;
				try
				{
					itemList = ssi.GetSubItems(false);
				}
				catch (System.IO.FileNotFoundException exc)
				{
					System.Windows.Forms.MessageBox.Show(exc.Message);
					return;
				}
				catch (Exception exc)
				{
					System.Windows.Forms.MessageBox.Show(exc.Message);
					return;
				}
				FillItem(itemList, ssi);
			}
		}

		private void FillItem(List<ShellItem> itemList, ShellItem parentShellItem)
		{
			base.Items.Clear();

			foreach (ShellItem si in itemList)
			{
				ListViewItem lvItem = new ListViewItem();
				lvItem.Text = si.DisplayName;
				lvItem.ImageIndex = si.IconIndex;
				lvItem.Tag = si;
				parentShellItem.GetData(si);
				if (si.IsStream == true || si.IsFolder == true)
				{
					if (si.LastAccessTime == DateTime.MinValue)
					{
						lvItem.SubItems.Add("");
					}
					else
					{
						lvItem.SubItems.Add(si.LastWriteTime.ToString("yyyy-MM-dd HH:mm"));
					}
				}
				else
				{
					lvItem.SubItems.Add("");
				}
				lvItem.SubItems.Add(si.TypeName);
				lvItem.SubItems.Add(FileSizeToString(si.FileSize));
				base.Items.Add(lvItem);
			}
		}

		protected virtual void CreateDetailsColumn()
		{
			base.Columns.Add("名前");
			base.Columns.Add("更新日時");
			base.Columns.Add("種類");
			base.Columns.Add("サイズ");
		}

		private string FileSizeToString(long FileSize)
		{
			long TB = 1000L * 1000L * 1000L * 1000L;
			long GB = 1000L * 1000L * 1000L;
			long MB = 1000L * 1000L;
			long KB = 1000L;

			if (FileSize <= 0)
			{
				return "";
			}
			else if (TB <= FileSize)
			{
				return string.Format("{0:#,#} TB", FileSize / TB);
			}
			else if (GB <= FileSize)
			{
				return string.Format("{0:#,#} GB", FileSize / GB);
			}
			else if (MB <= FileSize)
			{
				return string.Format("{0:#,#} MB", FileSize / MB);
			}
			else if (KB <= FileSize)
			{
				return string.Format("{0:#,#} KB", FileSize / KB);
			}
			else
			{
				return string.Format("{0:#,#} Byte", FileSize);
			}
		}
	}
}
