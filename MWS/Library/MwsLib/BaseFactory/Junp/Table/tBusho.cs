//
// tBusho.cs
//
// 部署情報クラス
// [JunpDB].[dbo].[tBusho]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//

namespace MwsLib.BaseFactory.Junp.Table
{
	/// <summary>
	/// 部署情報
	/// </summary>
	public class tBusho
	{
		public string fBshCode1 { get; set; }
		
		/// <summary>
		/// 部コード
		/// </summary>
		public string fBshCode2 { get; set; }
		
		/// <summary>
		/// 拠点コード
		/// </summary>
		public string fBshCode3 { get; set; }
		public string fBshName1 { get; set; }

		/// <summary>
		/// 部署名
		/// </summary>
		public string fBshName2 { get; set; }

		/// <summary>
		/// 拠点名
		/// </summary>
		public string fBshName3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string fBshBumon { get; set; }

		/// <summary>
		/// 部署種別
		/// </summary>
		public string fBshType { get; set; }

		/// <summary>
		/// 部署略称
		/// </summary>
		public string fBshNameRyaku { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tBusho()
		{
			fBshCode1 = string.Empty;
			fBshCode2 = string.Empty;
			fBshCode3 = string.Empty;
			fBshName1 = string.Empty;
			fBshName2 = string.Empty;
			fBshName3 = string.Empty;
			fBshBumon = string.Empty;
			fBshType = string.Empty;
			fBshNameRyaku = string.Empty;
		}
	}
}
