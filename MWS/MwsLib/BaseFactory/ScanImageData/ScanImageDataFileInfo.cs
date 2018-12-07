using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MwsLib.BaseFactory.ScanImageData
{
	/// <summary>
	/// スキャンデータ情報
	/// </summary>
	public class ScanImageDataFileInfo
	{
		/// <summary>
		/// 登録種別
		/// </summary>
		public enum MethodType
		{
			/// <summary>
			/// 追加
			/// </summary>
			Add = 0,

			/// <summary>
			/// 登録済み
			/// </summary>
			Registed = 1,

			/// <summary>
			/// 更新
			/// </summary>
			Update = 2,

			/// <summary>
			/// 削除
			/// </summary>
			Delete = 3,
		}

		/// <summary>
		/// 登録ID
		/// </summary>
		public int ScanID { get; set; }

		/// <summary>
		/// 文書種別
		/// </summary>
		public ScanImageDataDef.ScanDocumentType Document { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo { get; set; }

		/// <summary>
		/// 得意先No
		/// </summary>
		public string TokuisakiNo { get; set; }

		/// <summary>
		/// 医院名
		/// </summary>
		public string ClinicName { get; set; }

		/// <summary>
		/// フォルダ名
		/// </summary>
		public string FolderName { get; private set; }

		/// <summary>
		/// スキャンデータファイル名
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// スキャンデータファイル更新日時
		/// </summary>
		public DateTime? FileDateTime { get; set; }

		/// <summary>
		/// 登録種別
		/// </summary>
		public MethodType Method { get; set; }

		///// <summary>
		///// スキャンデータファイルパス名の取得
		///// </summary>
		//public string FilePathname
		//{
		//	get
		//	{
		//		return Path.Combine(FolderName, FileName);
		//	}
		//}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ScanImageDataFileInfo()
		{
			ScanID = 0;
			Document = ScanImageDataDef.ScanDocumentType.None;
			CustomerNo = 0;
			TokuisakiNo = string.Empty;
			ClinicName = string.Empty;
			FolderName = string.Empty;
			FileName = string.Empty;
			FileDateTime = null;
			Method = MethodType.Add;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="other">ユーザー登録情報</param>
		public ScanImageDataFileInfo(ScanImageDataFileInfo other)
		{
			ScanID = other.ScanID;
			Document = other.Document;
			CustomerNo = other.CustomerNo;
			TokuisakiNo = other.TokuisakiNo;
			ClinicName = other.ClinicName;
			FolderName = other.FolderName;
			FileName = other.FileName;
			FileDateTime = other.FileDateTime;
			Method = other.Method;
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMic文書インデクス]格納用フォルダ名の取得
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public string GetFolderName(string path)
		{
			if (0 < FolderName.Length)
			{
				return FolderName.Replace(path + @"\", "");
			}
			return string.Empty;
		}

		public void SetFolderName(string rootPath, string folder)
		{
			FolderName = folder.Replace(rootPath + @"\", "");
		}
	}
}
