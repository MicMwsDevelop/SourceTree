using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UserDataManager
{
	public class UserDataFile
	{
		/// <summary>
		/// パス名
		/// </summary>
		public string path { get; set; }

		/// <summary>
		/// ファイル名
		/// </summary>
		public string filename { get; set; }

		/// <summary>
		/// ファイルサイズ
		/// </summary>
		public long filesize { get; set; }

		/// <summary>
		/// フルパス名の取得
		/// </summary>
		public string Pathname
		{
			get
			{
				return Path.Combine(path, filename);
			}
		}

		/// <summary>
		/// ファイルサイズ文字列の取得
		/// </summary>
		public string FilesizeStr
		{
			get
			{
				if (1024 <= filesize)
				{
					return  string.Format("{0}KB", filesize / 1024);
				}
				return "1KB";
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public UserDataFile()
		{
			path = string.Empty;
			filename = string.Empty;
			filesize = 0;
		}

		/// <summary>
		/// データの格納
		/// </summary>
		/// <param name="pathname"></param>
		public void SetData(string pathname)
		{
			path = Path.GetDirectoryName(pathname);
			filename = Path.GetFileName(pathname);
			FileInfo fi = new FileInfo(pathname);
			filesize = fi.Length;
		}

		public string[] ToStringArry()
		{
			string[] ret = new string[2];
			ret[0] = Pathname;
			ret[1] = FilesizeStr;
			return ret;
		}

		public override string ToString()
		{
			return string.Format("{0},{1}", Pathname, FilesizeStr);
		}
	}
}
