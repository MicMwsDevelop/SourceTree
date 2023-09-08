//
// CouplerDatabaseDefine.cs
//
// カプラーデータベース定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.Common;

namespace CommonLib.DB.SqlServer.Coupler
{
	/// <summary>
	/// カプラーデータベース定義
	/// </summary>
	public static class CouplerDatabaseDefine
	{
		/// <summary>
		/// テーブル種別 
		/// </summary>
		public enum TableType
		{
			/// <summary>
			/// サービス情報
			/// </summary>
			SERVICE = 1,

			/// <summary>
			/// 製品顧客管理情報
			/// </summary>
			PRODUCTUSER = 2,

			/// <summary>
			/// 申込情報
			/// </summary>
			APPLY = 3,
		}

		/// <summary>
		/// テーブル種別/テーブル文字列
		/// </summary>
		public static readonly EnumDictionary<TableType, string> TableName = new EnumDictionary<TableType, string>()
		{
			{ TableType.SERVICE, "[SERVICE]" },
			{ TableType.PRODUCTUSER, "[PRODUCTUSER]" },
			{ TableType.APPLY, "[APPLY]" },
		};
	}
}
