//
// SQLiteMwsSimulationDef.cs
// 
// MIC WEB SERVICE 課金シミュレーション SQLiteデータベース情報管理クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
//

namespace MwsLib.DB.SQLite.MwsSimulation
{
	/// <summary>
	/// MWS課金シミュレーションデータベース情報管理クラス
	/// </summary>
	public class SQLiteMwsSimulationDef
	{
		/////////////////////////////////////////////////////
		// マスターデータベース

		/// <summary>
		/// MWS課金シミュレーションマスターデータベース名
		/// </summary>
		public const string MWS_SIMULATION_MASTER_DATABASE_NAME = "MwsSimulationMaster.db";

		/// <summary>
		/// サービス情報テーブル名
		/// </summary>
		public const string SERVICE_INFO_TABLE_NAME = "SERVICE_INFO";

		/// <summary>
		/// おまとめプラン情報テーブル名
		/// </summary>
		public const string GROUP_PLAN_TABLE_NAME = "GROUP_PLAN";

		/// <summary>
		/// おススメセット情報テーブル名
		/// </summary>
		public const string INIT_GROUP_PLAN_TABLE_NAME = "INIT_GROUP_PLAN";

		/// <summary>
		/// おススメセットサービス情報テーブル名
		/// </summary>
		public const string INIT_GROUP_PLAN_ELEMENT_TABLE_NAME = "INIT_GROUP_PLAN_ELEMENT";

		/// <summary>
		/// セット割サービスヘッダ情報テーブル名
		/// </summary>
		public const string SET_PLAN_HEADER_TABLE_NAME = "SET_PLAN_HEADER";

		/// <summary>
		/// セット割サービス情報テーブル名
		/// </summary>
		public const string SET_PLAN_ELEMENT_TABLE_NAME = "SET_PLAN_ELEMENT";

		/// <summary>
		/// 消費税率情報テーブル名
		/// </summary>
		public const string TAXRATE_TABLE_NAME = "TAX_RATE";

		/// <summary>
		/// バージョン情報テーブル名
		/// </summary>
		public const string VERSION_INFO_TABLE_NAME = "VERSION_INFO";


		/////////////////////////////////////////////////////
		// ユーザーデータベース

		/// <summary>
		/// MWS課金シミュレーションユーザーデータベース名
		/// </summary>
		public const string MWS_SIMULATION_USER_DATABASE_NAME = "MwsSimulationUser.db";

		/// <summary>
		/// 見積書ヘッダ情報
		/// </summary>
		public const string ESTIMATE_HEADER_TABLE_NAME = "ESTIMATE_HEADER";

		/// <summary>
		/// 見積書サービス情報
		/// </summary>
		public const string ESTIMATE_SERVICE_TABLE_NAME = "ESTIMATE_SERVICE";

		/// <summary>
		/// 見積書おまとめプラン・セット割サービス情報
		/// </summary>
		public const string ESTIMATE_GROUP_ELEMENT_TABLE_NAME = "ESTIMATE_GROUP_ELEMENT";

		/// <summary>
		/// おまとめプラン対象サービス商品区分
		/// </summary>
		public const int GOODS_KUBUN_GROUP_PLAN_SERVICE = 201;

		/// <summary>
		/// ＭＩＣ ＷＥＢ ＳＥＲＶＩＣＥ 標準機能商品ID
		/// </summary>
		public const string MWS_STANDARD_GOODSID = "800001";

		/// <summary>
		/// おまとめプラン・セット割サービス種別
		/// </summary>
		public enum ServiceMode
		{
			/// <summary>None</summary>
			None = 0,

			/// <summary>おまとめプラン</summary>
			Group = 1,

			/// <summary>セット割サービス</summary>
			Set = 2,
		}
	}
}
