//
// CouplerDatabaseDefine.cs
//
// カプラーデータベース定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using MwsLib.Common;

namespace MwsLib.DB.SqlServer.Coupler
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
			/// サービス利用情報
			/// </summary>
			T_COUPLER_SERVICE = 1,

			/// <summary>
			/// 製品顧客管理情報
			/// </summary>
			T_COUPLER_PRODUCTUSER = 2,
		}

		/// <summary>
		/// テーブル種別/テーブル文字列
		/// </summary>
		public static readonly EnumDictionary<TableType, string> TableName = new EnumDictionary<TableType, string>()
		{
			{ TableType.T_COUPLER_SERVICE, "SERVICE" },
			{ TableType.T_COUPLER_PRODUCTUSER, "PRODUCTUSER" },
			//{ TableType.T_COUPLER_SERVICE, "T_COUPLER_SERVICE" },
			//{ TableType.T_COUPLER_PRODUCTUSER, "T_COUPLER_PRODUCTUSER" },
		};

		///// <summary>
		///// ビュー種別 
		///// </summary>
		//public enum ViewType
		//{
		//	vMicPCA仕入先マスタ = 1,
		//	vMicPCA消費税率 = 2,
		//	vMicPCA商品マスタ = 3,
		//}

		///// <summary>
		///// ビュー種別/ビュー文字列
		///// </summary>
		//public static readonly EnumDictionary<ViewType, string> ViewName = new EnumDictionary<ViewType, string>()
		//{
		//	{ ViewType.vMicPCA仕入先マスタ, "vMicPCA仕入先マスタ" },
		//	{ ViewType.vMicPCA消費税率, "vMicPCA消費税率" },
		//	{ ViewType.vMicPCA商品マスタ, "vMicPCA商品マスタ" },
		//};
	}
}
