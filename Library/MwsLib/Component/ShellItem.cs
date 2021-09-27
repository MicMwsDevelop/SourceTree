using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MwsLib.Component
{
	public class ShellItem
	{
		[DllImport("kernel32.dll", SetLastError = true)]
		static extern bool QueryPerformanceCounter(out long lpPerformanceCount);


		private WindowsAPI.IShellFolder m_DesktopShellFolder = null;
		private WindowsAPI.IShellFolder m_SelfShellFolder = null;

		private IntPtr m_pIDL = IntPtr.Zero;
		private IntPtr m_pIDL_Relative = IntPtr.Zero;

		private string m_strDisplayName = "";
		private string m_strTypeName = "";
		private Int32 m_iIconIndex = -1;

		private bool m_bIsFolder = false;
		private bool m_bHasSubFolder = false;
		private bool m_bIsStream = false;
		private bool m_bIsDropTarget = false;
		private string m_strPath = "";
		private bool m_bIsZipFile = false;
		private bool m_bIsFileSystem = false;
		private bool m_bIsLink = false;
		private bool m_bIsSystem = false;
		private bool m_bIsNonEnumerated = false;

		private long m_FileSize = 0;
		private DateTime m_CreationTime;
		private DateTime m_LastAccessTime;
		private DateTime m_LastWriteTime;

		public WindowsAPI.IShellFolder DesktopShellFolder
		{
			get
			{
				return m_DesktopShellFolder;
			}
		}

		public WindowsAPI.IShellFolder ShellFolder
		{
			get
			{
				return m_SelfShellFolder;
			}
		}

		/// <summary>
		/// Gets or set the display name for this shell item.
		/// </summary>
		public string DisplayName
		{
			get { return m_strDisplayName; }
			set { m_strDisplayName = value; }
		}

		public string TypeName
		{
			get { return m_strTypeName; }
			set { m_strTypeName = value; }
		}

		/// <summary>
		/// Gets or sets the system image list icon index for this shell item.
		/// </summary>
		public Int32 IconIndex
		{
			get { return m_iIconIndex; }
			set { m_iIconIndex = value; }
		}

		/// <summary>
		/// Gets the fully qualified PIDL for this shell item.
		/// </summary>
		public IntPtr PIDL
		{
			get { return m_pIDL; }
		}

		/// <summary>
		/// Gets the fully qualified PIDL for this shell item.
		/// </summary>
		public IntPtr PIDL_Relative
		{
			get { return m_pIDL_Relative; }
		}

		/// <summary>
		/// Gets or sets a boolean indicating whether this shell item is a folder.
		/// </summary>
		public bool IsFolder
		{
			get { return m_bIsFolder; }
			set { m_bIsFolder = value; }
		}

		/// <summary>
		/// Gets or sets a boolean indicating whether this shell item has any sub-folders.
		/// </summary>
		public bool HasSubFolder
		{
			get { return m_bHasSubFolder; }
			set { m_bHasSubFolder = value; }
		}

		public bool IsFileSystem
		{
			get { return m_bIsFileSystem; }
			set { m_bIsFileSystem = value; }
		}

		public bool IsLink
		{
			get { return m_bIsLink; }
			set { m_bIsLink = value; }
		}

		public bool IsStream
		{
			get { return m_bIsStream; }
			set { m_bIsStream = value; }
		}

		public bool IsDropTarget
		{
			get { return m_bIsDropTarget; }
			set { m_bIsDropTarget = value; }
		}

		public bool IsZipFile
		{
			get { return m_bIsZipFile; }
		}

		public bool IsSystem
		{
			get { return m_bIsSystem; }
			set { m_bIsSystem = value; }
		}

		public bool IsNonEnumerated
		{
			get { return m_bIsNonEnumerated; }
			set { m_bIsNonEnumerated = value; }
		}


		/// <summary>
		/// Gets or sets the system path for this shell item.
		/// </summary>
		public string Path
		{
			get { return m_strPath; }
			set { m_strPath = value; }
		}

		public long FileSize
		{
			get { return m_FileSize; }
			set { m_FileSize = value; }
		}

		public DateTime CreationTime
		{
			get { return m_CreationTime; }
			set { m_CreationTime = value; }
		}

		public DateTime LastAccessTime
		{
			get { return m_LastAccessTime; }
			set { m_LastAccessTime = value; }
		}

		public DateTime LastWriteTime
		{
			get { return m_LastWriteTime; }
			set { m_LastWriteTime = value; }
		}


		/// <summary>
		/// Constructor. Creates the ShellItem object for the Desktop.
		/// </summary>
		public ShellItem(WindowsAPI.IShellFolder DesktopShellFolder)
		{
			if (DesktopShellFolder != null)
			{
				m_DesktopShellFolder = DesktopShellFolder;
				m_SelfShellFolder = DesktopShellFolder;

				// Now get the PIDL for the Desktop shell item.(ルートがデスクトップの場合)
				int hRes = WindowsAPI.SHGetSpecialFolderLocation(IntPtr.Zero, WindowsAPI.CSIDL.CSIDL_DESKTOP, ref m_pIDL);

				// Now retrieve some attributes for the root shell item.
				WindowsAPI.SHFILEINFO shInfo = new WindowsAPI.SHFILEINFO();
				WindowsAPI.SHGetFileInfo(m_pIDL, 0, out shInfo, (uint)Marshal.SizeOf(shInfo),
					WindowsAPI.SHGFI.SHGFI_DISPLAYNAME |
					WindowsAPI.SHGFI.SHGFI_PIDL |
					WindowsAPI.SHGFI.SHGFI_SMALLICON |
					WindowsAPI.SHGFI.SHGFI_SYSICONINDEX
				);

				// Set the arributes to object properties.
				DisplayName = shInfo.szDisplayName;
				IconIndex = shInfo.iIcon;
				IsFolder = true;
				HasSubFolder = true;
				Path = GetPath();
			}
			else
			{
				throw new Exception("DesktopShellFolder is NULL.");
			}
		}

		/// <summary>
		/// Constructor. Creates the ShellItem object for the Special Folder.
		/// </summary>
		public ShellItem(WindowsAPI.IShellFolder DesktopShellFolder, WindowsAPI.CSIDL ConstantSpecialIDList)
		{
			if (DesktopShellFolder != null)
			{
				m_DesktopShellFolder = DesktopShellFolder;

				//ルートがデスクトップ以外の場合
				int hRes = WindowsAPI.SHGetSpecialFolderLocation(IntPtr.Zero, ConstantSpecialIDList, ref m_pIDL);
				if (hRes != 0) Marshal.ThrowExceptionForHR(hRes);

				uint hRes2 = m_DesktopShellFolder.BindToObject(m_pIDL, IntPtr.Zero, ref WindowsAPI.IID_IShellFolder, out m_SelfShellFolder);

				// Now retrieve some attributes for the root shell item.
				WindowsAPI.SHFILEINFO shInfo = new WindowsAPI.SHFILEINFO();
				WindowsAPI.SHGetFileInfo(m_pIDL, 0, out shInfo, (uint)Marshal.SizeOf(shInfo),
					WindowsAPI.SHGFI.SHGFI_DISPLAYNAME |
					WindowsAPI.SHGFI.SHGFI_PIDL |
					WindowsAPI.SHGFI.SHGFI_SMALLICON |
					WindowsAPI.SHGFI.SHGFI_SYSICONINDEX
				);

				// Set the arributes to object properties.
				DisplayName = shInfo.szDisplayName;
				IconIndex = shInfo.iIcon;
				IsFolder = true;
				HasSubFolder = true;
				Path = GetPath();

			}
			else
			{
				throw new Exception("DesktopShellFolder is NULL.");
			}
		}

		//public ShellItem(WindowsAPI.IShellFolder DesktopShellFolder, WindowsAPI.IShellFolder ParentShellFolder, IntPtr pIDL, ShellItem shParent=null, bool ZipExclusion=false)
		public ShellItem(WindowsAPI.IShellFolder DesktopShellFolder, IntPtr pIDL, ShellItem shParent, bool ZipExclusion = false)
		{
			// We need the Desktop shell item to exist first.
			m_DesktopShellFolder = DesktopShellFolder;

			// Create the FQ PIDL for this new item.
			m_pIDL = WindowsAPI.ILCombine(shParent.PIDL, pIDL);
			m_pIDL_Relative = WindowsAPI.ILClone(pIDL);

			// Now we want to get extended attributes such as the icon index etc.
			WindowsAPI.SHFILEINFO shInfo = new WindowsAPI.SHFILEINFO();
			WindowsAPI.SHGFI vFlags = WindowsAPI.SHGFI.SHGFI_SMALLICON | WindowsAPI.SHGFI.SHGFI_SYSICONINDEX | WindowsAPI.SHGFI.SHGFI_PIDL | WindowsAPI.SHGFI.SHGFI_DISPLAYNAME | WindowsAPI.SHGFI.SHGFI_TYPENAME;

			WindowsAPI.SHGetFileInfo(m_pIDL, 0, out shInfo, (uint)Marshal.SizeOf(shInfo), vFlags);
			DisplayName = shInfo.szDisplayName;
			TypeName = shInfo.szTypeName;
			IconIndex = shInfo.iIcon;
			Path = GetPath();

			if (System.IO.File.Exists(Path) == true && System.IO.Path.GetExtension(Path).ToLower() == ".zip")
			{
				//ZIP
				m_bIsZipFile = true;
			}
			else
			{
				WindowsAPI.SFGAOF uFlags = WindowsAPI.SFGAOF.SFGAO_FOLDER | WindowsAPI.SFGAOF.SFGAO_HASSUBFOLDER
				  | WindowsAPI.SFGAOF.SFGAO_STREAM | WindowsAPI.SFGAOF.SFGAO_DROPTARGET | WindowsAPI.SFGAOF.SFGAO_FILESYSTEM | WindowsAPI.SFGAOF.SFGAO_LINK | WindowsAPI.SFGAOF.SFGAO_SYSTEM;
				shParent.m_SelfShellFolder.GetAttributesOf(1, new IntPtr[] { pIDL }, ref uFlags);

				//shDesktop.GetAttributesOf(1, new IntPtr[]{ m_pIDL}, ref uFlags);
				IsFolder = Convert.ToBoolean(uFlags & WindowsAPI.SFGAOF.SFGAO_FOLDER);
				HasSubFolder = Convert.ToBoolean(uFlags & WindowsAPI.SFGAOF.SFGAO_HASSUBFOLDER);
				IsDropTarget = Convert.ToBoolean(uFlags & WindowsAPI.SFGAOF.SFGAO_DROPTARGET);
				IsStream = Convert.ToBoolean(uFlags & WindowsAPI.SFGAOF.SFGAO_STREAM);
				IsFileSystem = Convert.ToBoolean(uFlags & WindowsAPI.SFGAOF.SFGAO_FILESYSTEM);
				IsLink = Convert.ToBoolean(uFlags & WindowsAPI.SFGAOF.SFGAO_LINK);
				IsSystem = Convert.ToBoolean(uFlags & WindowsAPI.SFGAOF.SFGAO_SYSTEM);
			}

			// Create the IShellFolder interface for this item.
			if (IsFolder)
			{
				uint hRes = shParent.m_SelfShellFolder.BindToObject(pIDL, IntPtr.Zero, ref WindowsAPI.IID_IShellFolder, out m_SelfShellFolder);
				//uint hRes = shParent.m_SelfShellFolder.BindToObject(pIDL, IntPtr.Zero, ref WindowsAPI.IID_IShellFolder, out m_SelfShellFolder);
				if (hRes != 0) Marshal.ThrowExceptionForHR((int)hRes);
			}
		}

		/// <summary>
		/// Constructor. Creates the ShellItem object for the File.
		/// </summary>
		public ShellItem(WindowsAPI.IShellFolder DesktopShellFolder, string PathString)
		{
			if (DesktopShellFolder != null)
			{
				m_DesktopShellFolder = DesktopShellFolder;

				uint pchEaten;
				IntPtr ppidl;
				WindowsAPI.SFGAOF attr = WindowsAPI.SFGAOF.SFGAO_FOLDER | WindowsAPI.SFGAOF.SFGAO_HASSUBFOLDER
				  | WindowsAPI.SFGAOF.SFGAO_STREAM | WindowsAPI.SFGAOF.SFGAO_DROPTARGET | WindowsAPI.SFGAOF.SFGAO_FILESYSTEM | WindowsAPI.SFGAOF.SFGAO_LINK | WindowsAPI.SFGAOF.SFGAO_SYSTEM | WindowsAPI.SFGAOF.SFGAO_NONENUMERATED;
				m_DesktopShellFolder.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, PathString, out pchEaten, out ppidl, ref attr);

				IsFolder = Convert.ToBoolean(attr & WindowsAPI.SFGAOF.SFGAO_FOLDER);
				HasSubFolder = Convert.ToBoolean(attr & WindowsAPI.SFGAOF.SFGAO_HASSUBFOLDER);
				IsDropTarget = Convert.ToBoolean(attr & WindowsAPI.SFGAOF.SFGAO_DROPTARGET);
				IsStream = Convert.ToBoolean(attr & WindowsAPI.SFGAOF.SFGAO_STREAM);
				IsFileSystem = Convert.ToBoolean(attr & WindowsAPI.SFGAOF.SFGAO_FILESYSTEM);
				IsLink = Convert.ToBoolean(attr & WindowsAPI.SFGAOF.SFGAO_LINK);
				IsSystem = Convert.ToBoolean(attr & WindowsAPI.SFGAOF.SFGAO_SYSTEM);
				IsNonEnumerated = Convert.ToBoolean(attr & WindowsAPI.SFGAOF.SFGAO_NONENUMERATED);

				m_pIDL = ppidl;

				// Now we want to get extended attributes such as the icon index etc.
				WindowsAPI.SHFILEINFO shInfo = new WindowsAPI.SHFILEINFO();
				WindowsAPI.SHGFI vFlags =
					WindowsAPI.SHGFI.SHGFI_SMALLICON |
					WindowsAPI.SHGFI.SHGFI_SYSICONINDEX |
					WindowsAPI.SHGFI.SHGFI_PIDL |
					WindowsAPI.SHGFI.SHGFI_DISPLAYNAME;
				WindowsAPI.SHGetFileInfo(m_pIDL, 0, out shInfo, (uint)Marshal.SizeOf(shInfo), vFlags);

				DisplayName = shInfo.szDisplayName;
				IconIndex = shInfo.iIcon;
				Path = GetPath();

				// Create the IShellFolder interface for this item.
				if (IsFolder)
				{
					uint hRes = m_DesktopShellFolder.BindToObject(ppidl, IntPtr.Zero, ref WindowsAPI.IID_IShellFolder, out m_SelfShellFolder);
					if (hRes != 0) Marshal.ThrowExceptionForHR((int)hRes);
				}
				else
				{
					m_SelfShellFolder = m_DesktopShellFolder;
				}

			}
			else
			{
				throw new Exception("DesktopShellFolder is NULL.");
			}
		}

		/// <summary>
		/// Constructor. Creates the ShellItem object for the File.
		/// </summary>
		public ShellItem(WindowsAPI.IShellFolder DesktopShellFolder, string PathString, WindowsAPI.IShellFolder ParentShellFolder)
		{
			if (DesktopShellFolder != null)
			{
				m_DesktopShellFolder = DesktopShellFolder;

				uint pchEaten;
				IntPtr ppidl;
				//uint attr=0;
				WindowsAPI.SFGAOF attr = WindowsAPI.SFGAOF.SFGAO_FOLDER | WindowsAPI.SFGAOF.SFGAO_HASSUBFOLDER
				  | WindowsAPI.SFGAOF.SFGAO_STREAM | WindowsAPI.SFGAOF.SFGAO_DROPTARGET | WindowsAPI.SFGAOF.SFGAO_FILESYSTEM | WindowsAPI.SFGAOF.SFGAO_LINK | WindowsAPI.SFGAOF.SFGAO_NONENUMERATED;
				ParentShellFolder.ParseDisplayName(IntPtr.Zero, IntPtr.Zero, PathString, out pchEaten, out ppidl, ref attr);

				IsFolder = Convert.ToBoolean(attr & WindowsAPI.SFGAOF.SFGAO_FOLDER);
				HasSubFolder = Convert.ToBoolean(attr & WindowsAPI.SFGAOF.SFGAO_HASSUBFOLDER);
				IsDropTarget = Convert.ToBoolean(attr & WindowsAPI.SFGAOF.SFGAO_DROPTARGET);
				IsStream = Convert.ToBoolean(attr & WindowsAPI.SFGAOF.SFGAO_STREAM);
				IsFileSystem = Convert.ToBoolean(attr & WindowsAPI.SFGAOF.SFGAO_FILESYSTEM);
				IsLink = Convert.ToBoolean(attr & WindowsAPI.SFGAOF.SFGAO_LINK);
				IsNonEnumerated = Convert.ToBoolean(attr & WindowsAPI.SFGAOF.SFGAO_NONENUMERATED);

				m_pIDL = ppidl;

				// Now we want to get extended attributes such as the icon index etc.
				WindowsAPI.SHFILEINFO shInfo = new WindowsAPI.SHFILEINFO();
				WindowsAPI.SHGFI vFlags =
					WindowsAPI.SHGFI.SHGFI_SMALLICON |
					WindowsAPI.SHGFI.SHGFI_SYSICONINDEX |
					WindowsAPI.SHGFI.SHGFI_PIDL |
					WindowsAPI.SHGFI.SHGFI_DISPLAYNAME;
				WindowsAPI.SHGetFileInfo(m_pIDL, 0, out shInfo, (uint)Marshal.SizeOf(shInfo), vFlags);

				DisplayName = shInfo.szDisplayName;
				IconIndex = shInfo.iIcon;
				Path = GetPath();

				// Create the IShellFolder interface for this item.
				if (IsFolder)
				{
					uint hRes = m_DesktopShellFolder.BindToObject(ppidl, IntPtr.Zero, ref WindowsAPI.IID_IShellFolder, out m_SelfShellFolder);
					if (hRes != 0) Marshal.ThrowExceptionForHR((int)hRes);
				}
				else
				{
					m_SelfShellFolder = m_DesktopShellFolder;
				}

			}
			else
			{
				throw new Exception("DesktopShellFolder is NULL.");
			}
		}

		/// <summary>
		/// Retrieves an array of ShellItem objects for sub-folders of this shell item.
		/// </summary>
		/// <returns>ArrayList of ShellItem objects.</returns>
		public List<ShellItem> GetSubFolders(bool ZipExclusion)
		{
			// Make sure we have a folder.
			if (IsFolder == false)
			{
				throw new Exception("Unable to retrieve sub-folders for a non-folder.");
			}

			List<ShellItem> arrChildren = new List<ShellItem>();
			try
			{
				// Get the IEnumIDList interface pointer.
				WindowsAPI.IEnumIDList pEnum = null;
				uint hRes = ShellFolder.EnumObjects(IntPtr.Zero, WindowsAPI.SHCONTF.SHCONTF_FOLDERS, out pEnum);
				if (hRes != 0) Marshal.ThrowExceptionForHR((int)hRes);

				IntPtr[] pIDL = new IntPtr[1];//IntPtr.Zero;
				Int32 iGot = 0;

				// Grab the first enumeration.
				pEnum.Next(1, pIDL, out iGot);

				// Then continue with all the rest.
				while (!pIDL.Equals(IntPtr.Zero) && iGot == 1)
				{
					// Create the new ShellItem object.

					ShellItem si;
					if (ZipExclusion == true)
					{
						//ZIP除外処理
						si = new ShellItem(m_DesktopShellFolder, pIDL[0], this, true);
					}
					else
					{
						//通常通りの処理
						si = new ShellItem(m_DesktopShellFolder, pIDL[0], this);
					}

					arrChildren.Add(si);

					// Free the PIDL and reset counters.
					Marshal.FreeCoTaskMem(pIDL[0]);

					iGot = 0;

					// Grab the next item.
					pEnum.Next(1, pIDL, out iGot);
				}

				// Free the interface pointer.
				if (pEnum != null) Marshal.ReleaseComObject(pEnum);
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message, "Error:",
					System.Windows.Forms.MessageBoxButtons.OK,
					System.Windows.Forms.MessageBoxIcon.Error
				);
			}

			return arrChildren;
		}

		/// <summary>
		/// Retrieves an array of ShellItem objects for sub-folders of this shell item.
		/// </summary>
		/// <returns>ArrayList of ShellItem objects.</returns>
		public List<ShellItem> GetSubItems(bool ZipExclusion)
		{
			// Make sure we have a folder.
			if (IsFolder == false)
			{
				throw new Exception("Unable to retrieve sub-folders for a non-folder.");
			}

			List<ShellItem> arrChildren = new List<ShellItem>();
			// Get the IEnumIDList interface pointer.
			WindowsAPI.IEnumIDList pEnum = null;
			WindowsAPI.SHCONTF flags = WindowsAPI.SHCONTF.SHCONTF_FOLDERS | WindowsAPI.SHCONTF.SHCONTF_NONFOLDERS | WindowsAPI.SHCONTF.SHCONTF_INCLUDEHIDDEN;
			uint hRes = ShellFolder.EnumObjects(IntPtr.Zero, flags, out pEnum);
			if (hRes != 0) Marshal.ThrowExceptionForHR((int)hRes);

			IntPtr[] pIDL = new IntPtr[1];//IntPtr.Zero;
			Int32 iGot = 0;

			// Grab the first enumeration.
			pEnum.Next(1, pIDL, out iGot);

			// Then continue with all the rest.
			while (!pIDL.Equals(IntPtr.Zero) && iGot == 1)
			{
				// Create the new ShellItem object.
				ZipExclusion = false;

				ShellItem si;
				if (ZipExclusion == true)
				{
					//ZIP除外処理
					si = new ShellItem(m_DesktopShellFolder, pIDL[0], this, true);
				}
				else
				{
					//通常通りの処理
					si = new ShellItem(m_DesktopShellFolder, pIDL[0], this);
				}

				arrChildren.Add(si);

				// Free the PIDL and reset counters.
				Marshal.FreeCoTaskMem(pIDL[0]);
				iGot = 0;

				// Grab the next item.
				pEnum.Next(1, pIDL, out iGot);
			}

			// Free the interface pointer.
			if (pEnum != null) Marshal.ReleaseComObject(pEnum);

			return arrChildren;
		}


		/// <summary>
		/// Gets the system path for this shell item.
		/// </summary>
		/// <returns>A path string.</returns>
		public string GetPath()
		{
			StringBuilder strBuffer = new StringBuilder(256);
			WindowsAPI.SHGetPathFromIDList(m_pIDL, strBuffer);
			return strBuffer.ToString();
		}

		public WindowsAPI.WIN32_FIND_DATA GetData(IntPtr pidl_relative)
		{
			WindowsAPI.WIN32_FIND_DATA pv;
			WindowsAPI.SHGetDataFromIDList(this.m_SelfShellFolder, pidl_relative, WindowsAPI.SHGDFIL.SHGDFIL_FINDDATA, out pv, Marshal.SizeOf(typeof(WindowsAPI.WIN32_FIND_DATA)));
			return pv;
		}

		public WindowsAPI.WIN32_FIND_DATA GetData(ShellItem si)
		{
			WindowsAPI.WIN32_FIND_DATA pv = new WindowsAPI.WIN32_FIND_DATA();
			IntPtr pidl_relative = si.m_pIDL_Relative;
			WindowsAPI.SHGetDataFromIDList(this.m_SelfShellFolder, pidl_relative, WindowsAPI.SHGDFIL.SHGDFIL_FINDDATA, out pv, Marshal.SizeOf(typeof(WindowsAPI.WIN32_FIND_DATA)));

			si.FileSize = (long)((pv.nFileSizeHigh * (2 ^ 32)) + pv.nFileSizeLow);

			//ローカル時刻に変換
			TimeZoneInfo tzi = TimeZoneInfo.Local;
			if (0 < FileTimeToLong(pv.ftCreationTime))
			{
				si.CreationTime = DateTime.SpecifyKind(DateTime.FromFileTime(FileTimeToLong(pv.ftCreationTime)), DateTimeKind.Utc);
				si.CreationTime = TimeZoneInfo.ConvertTimeFromUtc(si.CreationTime, tzi);
			}
			else
			{
				si.CreationTime = DateTime.MinValue;
			}

			if (0 < FileTimeToLong(pv.ftLastWriteTime))
			{
				si.LastWriteTime = DateTime.SpecifyKind(DateTime.FromFileTime(FileTimeToLong(pv.ftLastWriteTime)), DateTimeKind.Utc);
				si.LastWriteTime = TimeZoneInfo.ConvertTimeFromUtc(si.LastWriteTime, tzi);
			}
			else
			{
				si.LastWriteTime = DateTime.MinValue;
			}

			if (0 < FileTimeToLong(pv.ftLastAccessTime))
			{
				si.LastAccessTime = DateTime.SpecifyKind(DateTime.FromFileTime(FileTimeToLong(pv.ftLastAccessTime)), DateTimeKind.Utc);
				si.LastAccessTime = TimeZoneInfo.ConvertTimeFromUtc(si.LastAccessTime, tzi);
			}
			else
			{
				si.LastAccessTime = DateTime.MinValue;
			}

			return pv;
		}

		private long FileTimeToLong(System.Runtime.InteropServices.ComTypes.FILETIME ft)
		{
			return (((long)ft.dwHighDateTime) << 32) + ft.dwLowDateTime;
		}
	}
}
