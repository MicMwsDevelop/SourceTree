using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MwsLib.Component
{
	public class SystemImageList
	{
		public enum SystemIconSize { Small, Large, ExtraLarge, Jumbo };
		public enum ListViewIconSetMode { Normal, Small }

		// Used to store the handle of the system image list.
		private static IntPtr m_pImgHandle = IntPtr.Zero;

		// Flags whether we've already retrieved the image list handle or not.
		//private static Boolean m_bImgInit = false;

		// TreeView message constants.
		private const UInt32 TVSIL_NORMAL = 0x0000;
		private const UInt32 TVSIL_STATE = 0x0002;
		private const UInt32 TVM_SETIMAGELIST = 0x1109; //4361;

		// ListView message constants.
		private const UInt32 LVSIL_NORMAL = 0x0000;
		private const UInt32 LVSIL_SMALL = 0x0001;
		private const UInt32 LVSIL_STATE = 0x0002;
		private const UInt32 LVSIL_GROUPHEADER = 0x0003;
		private const UInt32 LVM_SETIMAGELIST = 0x1003;

		public SystemImageList(bool SelectedImage, SystemIconSize iconSize)
		{
			InitImageList(SelectedImage, iconSize);
		}

		private void InitImageList(bool selected, SystemIconSize iconSize)
		{
			IntPtr ppidl = IntPtr.Zero;
			WindowsAPI.SHGetSpecialFolderLocation(IntPtr.Zero, WindowsAPI.CSIDL.CSIDL_DESKTOP, ref ppidl);

			WindowsAPI.SHFILEINFO shInfo = new WindowsAPI.SHFILEINFO();
			WindowsAPI.SHGFI dwAttribs;


			dwAttribs = WindowsAPI.SHGFI.SHGFI_USEFILEATTRIBUTES | WindowsAPI.SHGFI.SHGFI_SYSICONINDEX;

			switch (iconSize)
			{
				case SystemIconSize.Small:
					dwAttribs = dwAttribs | WindowsAPI.SHGFI.SHGFI_SMALLICON;
					break;
				case SystemIconSize.Large:
					dwAttribs = dwAttribs | WindowsAPI.SHGFI.SHGFI_LARGEICON;
					break;
			}

			if (selected == true)
			{
				dwAttribs = dwAttribs | WindowsAPI.SHGFI.SHGFI_SELECTED;
			}

			switch (iconSize)
			{
				case SystemIconSize.Small:
				case SystemIconSize.Large:
					m_pImgHandle = WindowsAPI.SHGetFileInfo(ppidl, WindowsAPI.FILE_ATTRIBUTE_NORMAL, out shInfo, (uint)Marshal.SizeOf(shInfo), dwAttribs);
					break;
				case SystemIconSize.ExtraLarge:
					WindowsAPI.SHGetImageList(WindowsAPI.SHIL.SHIL_EXTRALARGE, ref WindowsAPI.IID_IImageList, out m_pImgHandle);
					break;
				case SystemIconSize.Jumbo:
					WindowsAPI.SHGetImageList(WindowsAPI.SHIL.SHIL_JUMBO, ref WindowsAPI.IID_IImageList, out m_pImgHandle);
					break;
			}

			// Make sure we got the handle.
			if (m_pImgHandle.Equals(IntPtr.Zero)) throw new Exception("Unable to retrieve system image list handle.");
		}


		/// <summary>
		/// Sets the image list for the TreeView to the system image list.
		/// </summary>
		/// <param name="tvwHandle">The window handle of the TreeView control</param>
		public void TreeViewSetImageList(TreeView targetTreeView)
		{
			//InitImageList();
			Int32 hRes = WindowsAPI.SendMessage(targetTreeView.Handle, TVM_SETIMAGELIST, TVSIL_NORMAL, m_pImgHandle);
			if (hRes != 0) Marshal.ThrowExceptionForHR(hRes);
		}

		public void TreeViewSetImageList(IntPtr targetHandle)
		{
			//InitImageList();
			Int32 hRes = WindowsAPI.SendMessage(targetHandle, TVM_SETIMAGELIST, TVSIL_NORMAL, m_pImgHandle);
			if (hRes != 0) Marshal.ThrowExceptionForHR(hRes);
		}

		/// <summary>
		/// Sets the image list for the ListView to the system image list.
		/// </summary>
		/// <param name="tvwHandle">The window handle of the TreeView control</param>
		public void ListViewSetImageList(ListView targetListView, ListViewIconSetMode mode)
		{
			Int32 hRes = 0;

			//InitImageList();
			switch (mode)
			{
				case ListViewIconSetMode.Normal:
					hRes = WindowsAPI.SendMessage(targetListView.Handle, LVM_SETIMAGELIST, LVSIL_NORMAL, m_pImgHandle);
					break;
				case ListViewIconSetMode.Small:
					hRes = WindowsAPI.SendMessage(targetListView.Handle, LVM_SETIMAGELIST, LVSIL_SMALL, m_pImgHandle);
					break;

			}

			//Int32 hRes = WindowsAPI.SendMessage(targetListView.Handle, LVM_SETIMAGELIST, LVSIL_NORMAL, m_pImgHandle);
			//Int32 hRes = WindowsAPI.SendMessage(targetListView.Handle, LVM_SETIMAGELIST, LVSIL_SMALL, m_pImgHandle);
			if (hRes != 0) Marshal.ThrowExceptionForHR(hRes);
		}

		public void ListViewSetImageList(IntPtr TargetHandle, ListViewIconSetMode mode)
		{
			Int32 hRes = 0;

			//InitImageList();
			switch (mode)
			{
				case ListViewIconSetMode.Normal:
					hRes = WindowsAPI.SendMessage(TargetHandle, LVM_SETIMAGELIST, LVSIL_NORMAL, m_pImgHandle);
					break;
				case ListViewIconSetMode.Small:
					hRes = WindowsAPI.SendMessage(TargetHandle, LVM_SETIMAGELIST, LVSIL_SMALL, m_pImgHandle);
					break;

			}
			if (hRes != 0) Marshal.ThrowExceptionForHR(hRes);
		}
	}
}
