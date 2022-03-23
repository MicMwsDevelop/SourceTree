//
// SalesDatabaseDefine.cs
//
// SalesDB データベース定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2022/03/04 勝呂)
// 
using CommonLib.Common;

namespace CommonLib.DB.SqlServer.Sales
{
	/// <summary>
	/// Sales データベース定義
	/// </summary>
	public static class SalesDatabaseDefine
	{
		/// <summary>
		/// データベース名
		/// </summary>
		static private string DatabaseName = "SalesDB.dbo";

		/// <summary>
		/// テーブル種別 
		/// </summary>
		public enum TableType
		{
			/// <summary>
			/// オン資格ヒアリングシート
			/// </summary>
			オン資格ヒアリングシート = 1,

			/// <summary>
			/// 進捗管理表_作業情報
			/// </summary>
			進捗管理表_作業情報 = 2,
		}

		/// <summary>
		/// テーブル種別/テーブル文字列
		/// </summary>
		public static readonly EnumDictionary<TableType, string> TableName = new EnumDictionary<TableType, string>()
		{
			{ TableType.オン資格ヒアリングシート, string.Format("{0}.オン資格ヒアリングシート", DatabaseName) },
			{ TableType.進捗管理表_作業情報, string.Format("{0}.進捗管理表_作業情報", DatabaseName) },
		};

		/// <summary>
		/// ビュー種別 
		/// </summary>
		public enum ViewType
		{
			vオンライン資格確認ユーザー = 1,
		}

		/// <summary>
		/// ビュー種別/ビュー文字列
		/// </summary>
		public static readonly EnumDictionary<ViewType, string> ViewName = new EnumDictionary<ViewType, string>()
		{
			{ ViewType.vオンライン資格確認ユーザー, string.Format("{0}.vオンライン資格確認ユーザー", DatabaseName) },
		};
	}
}
