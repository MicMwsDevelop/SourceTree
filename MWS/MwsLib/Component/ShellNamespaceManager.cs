using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MwsLib.Component
{
	public class ShellNamespaceManager
	{
		private WindowsAPI.IShellFolder m_DesktopShellFolder = null;

		public WindowsAPI.IShellFolder DesktopShellFolder
		{
			get
			{
				return m_DesktopShellFolder;
			}
		}

		public ShellItem GetDesktopShellItem()
		{
			return new ShellItem(m_DesktopShellFolder);
		}

		public ShellItem GetSpecialFolderShellItem(WindowsAPI.CSIDL ConstantSprecialFolderIDList)
		{
			return new ShellItem(m_DesktopShellFolder, ConstantSprecialFolderIDList);
		}

		public ShellItem GetShellItemFromFilePath(string FilePath)
		{
			return new ShellItem(m_DesktopShellFolder, FilePath);
		}

		public ShellItem GetShellItemFromFilePath(string FilePath, WindowsAPI.IShellFolder ParentIShellFoloder)
		{
			return new ShellItem(m_DesktopShellFolder, FilePath, ParentIShellFoloder);
		}


		public ShellNamespaceManager()
		{
			int hRes = WindowsAPI.SHGetDesktopFolder(ref m_DesktopShellFolder);
			if (hRes != 0)
			{
				Marshal.ThrowExceptionForHR(hRes);
			}
		}

		~ShellNamespaceManager()
		{
			Marshal.ReleaseComObject(m_DesktopShellFolder);
		}
	}
}
