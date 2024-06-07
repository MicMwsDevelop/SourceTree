//
// CharlieDatabaseDefine.cs
//
// CharlieDB データベース定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2019/06/28 勝呂)
// Ver1.01 経理部の要請により、Microsoft365仕入データを部門毎の集計を止めて、得意先に関する記事データを追加(2023/02/10 勝呂)
// Ver1.02(2024/03/25 勝呂):オン資格訪問診療 補助金パックに対応
// 
using CommonLib.Common;

namespace CommonLib.DB.SqlServer.Charlie
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
			/// MWSコードマスタ
			/// </summary>
			M_CODE = 6,

			/// <summary>
			/// サービス種別情報
			/// </summary>
			M_SERVICE_TYPE = 7,

			/// <summary>
			/// サービス情報
			/// </summary>
			M_SERVICE = 8,

			/// <summary>
			/// まとめ契約ヘッダ情報
			/// </summary>
			T_USE_CONTRACT_HEADER = 9,

			/// <summary>
			/// まとめ契約詳細情報
			/// </summary>
			T_USE_CONTRACT_DETAIL = 10,

			/// <summary>
			/// 売上実績
			/// </summary>
			売上実績 = 11,

			/// <summary>
			/// 電子処方箋契約ヘッダ情報
			/// </summary>
			T_USE_PRESCRIPTION_HEADER = 12,

			/// <summary>
			/// 顧客管理基本
			/// </summary>
			T_CUSTOMER_FOUNDATIONS = 13,

			/// <summary>
			/// 申込データ
			/// </summary>
			T_APPLICATION_DATA = 14,

			/// <summary>
			/// 同期日時管理テーブル
			/// </summary>
			T_FILE_CREATEDATE = 15,

			/// <summary>
			/// デモ用サービス利用情報
			/// </summary>
			T_DEMO_SERVICE = 16,

			/// <summary>
			/// メールアドレス（kikaku@mic.jp）
			/// </summary>
			M_MAIL = 17,

			/// <summary>
			/// オンライン請求作業情報
			/// </summary>
			T_USE_ONLINE_DEMAND = 18,

			/// <summary>
			/// オン資格確認訪問診療連携契約情報
			/// </summary>
			T_USE_ONLINE_HOMON = 19,

			/// <summary>
			/// 電子処方箋管理契約情報
			/// </summary>
			T_USE_ELECTRIC_PRESCRIPTION = 20,
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
			{ TableType.M_CODE, string.Format("{0}.M_CODE", DatabaseName) },
			{ TableType.M_SERVICE_TYPE, string.Format("{0}.M_SERVICE_TYPE", DatabaseName) },
			{ TableType.M_SERVICE, string.Format("{0}.M_SERVICE", DatabaseName) },
			{ TableType.T_USE_CONTRACT_HEADER, string.Format("{0}.T_USE_CONTRACT_HEADER", DatabaseName) },
			{ TableType.T_USE_CONTRACT_DETAIL, string.Format("{0}.T_USE_CONTRACT_DETAIL", DatabaseName) },
			{ TableType.売上実績, string.Format("{0}.売上実績", DatabaseName) },
			{ TableType.T_USE_PRESCRIPTION_HEADER, string.Format("{0}.T_USE_PRESCRIPTION_HEADER", DatabaseName) },
			{ TableType.T_CUSTOMER_FOUNDATIONS, string.Format("{0}.T_CUSTOMER_FOUNDATIONS", DatabaseName) },
			{ TableType.T_APPLICATION_DATA, string.Format("{0}.T_APPLICATION_DATA", DatabaseName) },
			{ TableType.T_FILE_CREATEDATE, string.Format("{0}.T_FILE_CREATEDATE", DatabaseName) },
			{ TableType.T_DEMO_SERVICE, string.Format("{0}.T_DEMO_SERVICE", DatabaseName) },
			{ TableType.M_MAIL, string.Format("{0}.M_MAIL", DatabaseName) },
			{ TableType.T_USE_ONLINE_DEMAND, string.Format("{0}.T_USE_ONLINE_DEMAND", DatabaseName) },
			{ TableType.T_USE_ONLINE_HOMON, string.Format("{0}.T_USE_ONLINE_HOMON", DatabaseName) },
			{ TableType.T_USE_ELECTRIC_PRESCRIPTION, string.Format("{0}.T_USE_ELECTRIC_PRESCRIPTION", DatabaseName) },
		};

		/// <summary>
		/// ビュー種別 
		/// </summary>
		public enum ViewType
		{
			V_COUPLER_APPLY = 1,
			V_CLIENT_INFO = 2,
			V_PCA_GOODS = 3,
			支店情報参照ビュー = 4,
			view_MWS顧客情報 = 5,
			顧客マスタ参照ビュー = 6,
			WW伝票参照ビュー = 7,
			V_CUSTOMER = 8,
			社員マスタ参照ビュー = 9,
			販売店区分参照ビュー = 10,
			販売店情報参照ビュー = 11,
			V_SERVICE = 12,
			view_前月申込データ = 13,
		}

		/// <summary>
		/// ビュー種別/ビュー文字列
		/// </summary>
		public static readonly EnumDictionary<ViewType, string> ViewName = new EnumDictionary<ViewType, string>()
		{
			{ ViewType.V_COUPLER_APPLY, string.Format("{0}.V_COUPLER_APPLY", DatabaseName) },
			{ ViewType.V_CLIENT_INFO, string.Format("{0}.V_CLIENT_INFO", DatabaseName) },
			{ ViewType.V_PCA_GOODS, string.Format("{0}.V_PCA_GOODS", DatabaseName) },
			{ ViewType.支店情報参照ビュー, string.Format("{0}.支店情報参照ビュー", DatabaseName) },
			{ ViewType.view_MWS顧客情報, string.Format("{0}.view_MWS顧客情報", DatabaseName) },
			{ ViewType.顧客マスタ参照ビュー, string.Format("{0}.顧客マスタ参照ビュー", DatabaseName) },
			{ ViewType.WW伝票参照ビュー, string.Format("{0}.WW伝票参照ビュー", DatabaseName) },
			{ ViewType.V_CUSTOMER, string.Format("{0}.V_CUSTOMER", DatabaseName) },
			{ ViewType.社員マスタ参照ビュー, string.Format("{0}.社員マスタ参照ビュー", DatabaseName) },
			{ ViewType.販売店区分参照ビュー, string.Format("{0}.販売店区分参照ビュー", DatabaseName) },
			{ ViewType.販売店情報参照ビュー, string.Format("{0}.販売店情報参照ビュー", DatabaseName) },
			{ ViewType.V_SERVICE, string.Format("{0}.V_SERVICE", DatabaseName) },
			{ ViewType.view_前月申込データ, string.Format("{0}.V_SERVICE", DatabaseName) },
		};


		/// <summary>
		/// シノニム種別 
		/// </summary>
		public enum SynonymType
		{
			/// <summary>
			/// オススメセット情報
			/// </summary>
			M_SET = 1,

			/// <summary>
			/// おまとめプラン情報
			/// </summary>
			T_COUPLER_GROUP_PLAN = 2,

			/// <summary>
			/// 申込情報
			/// </summary>
			T_COUPLER_APPLY = 3,

			/// <summary>
			/// 製品顧客管理情報
			/// </summary>
			T_COUPLER_PRODUCTUSER = 4,
		}

		/// <summary>
		/// シノニム種別/シノニム文字列
		/// </summary>
		public static readonly EnumDictionary<SynonymType, string> SynonymName = new EnumDictionary<SynonymType, string>()
		{
			{  SynonymType.M_SET, string.Format("{0}.M_SET", DatabaseName) },
			{  SynonymType.T_COUPLER_GROUP_PLAN, string.Format("{0}.T_COUPLER_GROUP_PLAN", DatabaseName) },
			{  SynonymType.T_COUPLER_APPLY, string.Format("{0}.T_COUPLER_APPLY", DatabaseName) },
			{  SynonymType.T_COUPLER_PRODUCTUSER, string.Format("{0}.T_COUPLER_PRODUCTUSER", DatabaseName) },
		};
	}
}
