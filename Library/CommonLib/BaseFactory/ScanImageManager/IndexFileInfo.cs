//
// IndexFileInfo.cs
//
// 文書インデックス管理 インデックスファイル情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/06 勝呂)
// 
namespace CommonLib.BaseFactory.ScanImageManager
{
	/// <summary>
	/// インデックスファイル情報
	/// </summary>
	public class IndexFileInfo
	{
		/// <summary>
		/// 得意先No
		/// </summary>
		public string TokuisakiNo { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo { get; set; }

		/// <summary>
		/// 医院名
		/// </summary>
		public string ClinicName { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public IndexFileInfo()
		{
			TokuisakiNo = string.Empty;
			CustomerNo = 0;
			ClinicName = string.Empty;
		}

		public string ToTitle()
		{
			return "\"ＷＷ顧客Ｎｏ\",\"得意先No\",\"医院名\"";
		}

		public string ToCSV()
		{
			return string.Format("{0},\"{1}\",\"{2}\"", CustomerNo, TokuisakiNo, ClinicName);
		}
	}
}
