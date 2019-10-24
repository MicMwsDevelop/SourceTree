//
// CharlieDatabaseDefine.cs
//
// CharlieDB データベース定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using MwsLib.Common;

namespace MwsLib.DB.SqlServer.Charlie
{
	/// <summary>
	/// Charlie データベース定義
	/// </summary>
	public static class CharlieDatabaseDefine
	{
		/// <summary>
		/// テーブル種別 
		/// </summary>
		public enum TableType
		{
			/// <summary>
			/// 製品管理情報
			/// </summary>
			T_PRODUCT_CONTROL = 1,

			/// <summary>
			/// 顧客利用情報
			/// </summary>
			T_CUSSTOMER_USE_INFOMATION = 2,

			/// <summary>
			/// PC安心サポート契約情報
			/// </summary>
			T_USE_PCCSUPPORT = 3,

			/// <summary>
			/// ナルコーム製品申込ヘッダ情報
			/// </summary>
			T_NARCOHM_APPLICATE_HEADER = 4,

			/// <summary>
			/// ナルコーム製品申込詳細情報
			/// </summary>
			T_NARCOHM_APPLICATE_DETAIL = 5,

			/// <summary>
			/// デモ用ID管理テーブル
			/// </summary>
			T_DEMO_USER = 6,
		}

		/// <summary>
		/// テーブル種別/テーブル文字列
		/// </summary>
		public static readonly EnumDictionary<TableType, string> TableName = new EnumDictionary<TableType, string>()
		{
			{ TableType.T_PRODUCT_CONTROL, "T_PRODUCT_CONTROL" },
			{ TableType.T_CUSSTOMER_USE_INFOMATION, "T_CUSSTOMER_USE_INFOMATION" },
			{ TableType.T_USE_PCCSUPPORT, "T_USE_PCCSUPPORT" },
			{ TableType.T_NARCOHM_APPLICATE_HEADER, "T_NARCOHM_APPLICATE_HEADER" },
			{ TableType.T_NARCOHM_APPLICATE_DETAIL, "T_NARCOHM_APPLICATE_DETAIL" },
			{ TableType.T_DEMO_USER, "T_DEMO_USER" },
		};

		/// <summary>
		/// ビュー種別 
		/// </summary>
		public enum ViewType
		{
			支店情報参照ビュー = 1,
		}

		/// <summary>
		/// ビュー種別/ビュー文字列
		/// </summary>
		public static readonly EnumDictionary<ViewType, string> ViewName = new EnumDictionary<ViewType, string>()
		{
			{ ViewType.支店情報参照ビュー, "支店情報参照ビュー" },
		};
	}
}
