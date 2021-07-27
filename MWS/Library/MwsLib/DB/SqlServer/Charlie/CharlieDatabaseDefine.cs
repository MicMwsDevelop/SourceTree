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
		/// データベース名
		/// </summary>
		static private string DatabaseName = "charlieDB.dbo";

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
			/// デモ用ID管理テーブル
			/// </summary>
			T_DEMO_USER = 4,

			/// <summary>
			/// ESETライセンス管理情報
			/// </summary>
			T_LICENSE_PRODUCT_CONTRACT = 5,

			/// <summary>
			/// サービス情報
			/// </summary>
			M_SERVICE = 6,

			/// <summary>
			/// まとめ契約ヘッダ情報
			/// </summary>
			T_USE_CONTRACT_HEADER = 7,

			/// <summary>
			/// まとめ契約詳細情報
			/// </summary>
			T_USE_CONTRACT_DETAIL = 8,

			/// <summary>
			/// 売上実績
			/// </summary>
			売上実績 = 9,
		}

		/// <summary>
		/// テーブル種別/テーブル文字列
		/// </summary>
		public static readonly EnumDictionary<TableType, string> TableName = new EnumDictionary<TableType, string>()
		{
			{ TableType.T_PRODUCT_CONTROL, string.Format("{0}.T_PRODUCT_CONTROL", DatabaseName) },
			{ TableType.T_CUSSTOMER_USE_INFOMATION, string.Format("{0}.T_CUSSTOMER_USE_INFOMATION", DatabaseName) },
			{ TableType.T_USE_PCCSUPPORT, string.Format("{0}.T_USE_PCCSUPPORT", DatabaseName) },
			{ TableType.T_DEMO_USER, string.Format("{0}.T_DEMO_USER", DatabaseName) },
			{ TableType.T_LICENSE_PRODUCT_CONTRACT, string.Format("{0}.T_LICENSE_PRODUCT_CONTRACT", DatabaseName) },
			{ TableType.M_SERVICE, string.Format("{0}.M_SERVICE", DatabaseName) },
			{ TableType.T_USE_CONTRACT_HEADER, string.Format("{0}.T_USE_CONTRACT_HEADER", DatabaseName) },
			{ TableType.T_USE_CONTRACT_DETAIL, string.Format("{0}.T_USE_CONTRACT_DETAIL", DatabaseName) },
			{ TableType.売上実績, string.Format("{0}.売上実績", DatabaseName) },
		};

		/// <summary>
		/// ビュー種別 
		/// </summary>
		public enum ViewType
		{
			支店情報参照ビュー = 1,
			V_COUPLER_APPLY = 2,
			V_CLIENT_INFO = 3,
			view_MWS顧客情報 = 4,
		}

		/// <summary>
		/// ビュー種別/ビュー文字列
		/// </summary>
		public static readonly EnumDictionary<ViewType, string> ViewName = new EnumDictionary<ViewType, string>()
		{
			{ ViewType.支店情報参照ビュー, string.Format("{0}.支店情報参照ビュー", DatabaseName) },
			{ ViewType.V_COUPLER_APPLY, string.Format("{0}.V_COUPLER_APPLY", DatabaseName) },
			{ ViewType.V_CLIENT_INFO, string.Format("{0}.V_CLIENT_INFO", DatabaseName) },
			{ ViewType.view_MWS顧客情報, string.Format("{0}.view_MWS顧客情報", DatabaseName) },
		};
	}
}
