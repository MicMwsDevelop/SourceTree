using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace UserDataManager
{
	static class Program
	{
		/// <summary>
		/// データベース接続先 CT環境
		/// </summary>
		public static bool DATABACE_ACCEPT_CT = false;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Forms.MainForm());
		}

		/// <summary>
		/// ファイルサイズの取得
		/// ※再帰あり
		/// </summary>
		/// <param name="pathname">パス名</param>
		/// <returns>ファイルサイズ</returns>
		static public long GetFileSize(string pathname)
		{
			try
			{
				if (Directory.Exists(pathname))
				{
					long size = 0;
					DirectoryInfo folder = new DirectoryInfo(pathname);
					foreach (FileInfo info in folder.GetFiles())
					{
						size += info.Length;
					}
					foreach (DirectoryInfo dir in folder.GetDirectories())
					{
						size += Program.GetFileSize(dir.FullName);
					}
					return size;
				}
				FileInfo fi = new FileInfo(pathname);
				return fi.Length;
			}
			catch (UnauthorizedAccessException)
			{
				throw;
			}
		}

		static public int SetUserDataList(string pathname, List<UserDataFile> list)
		{
			try
			{
				if (Directory.Exists(pathname))
				{
					long size = 0;
					DirectoryInfo folder = new DirectoryInfo(pathname);
					foreach (FileInfo info in folder.GetFiles())
					{
						UserDataFile data2 = new UserDataFile();
						data2.SetData(info.FullName);
						list.Add(data2);
					}
					foreach (DirectoryInfo dir in folder.GetDirectories())
					{
						Program.SetUserDataList(dir.FullName, list);
					}
					return list.Count();
				}
				UserDataFile data = new UserDataFile();
				data.SetData(pathname);
				list.Add(data);
				return list.Count();
			}
			catch (UnauthorizedAccessException)
			{
				throw;
			}
		}
	}
}
