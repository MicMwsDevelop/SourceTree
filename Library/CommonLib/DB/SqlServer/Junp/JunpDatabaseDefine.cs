//
// JunpDatabaseDefine.cs
//
// JunpDB データベース定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2019/06/28 勝呂)
// Ver1.01 新規作成(2022/12/28 勝呂)
// 
using CommonLib.Common;

namespace CommonLib.DB.SqlServer.Junp
{
	/// <summary>
	/// JumpDB データベース定義
	/// </summary>
	public static class JunpDatabaseDefine
	{
		/// <summary>
		/// データベース名
		/// </summary>
		static private string DatabaseName = "JunpDB.dbo";

		/// <summary>
		/// テーブル種別 
		/// </summary>
		public enum TableType
		{
			tMikコードマスタ = 1,
			tMic終了ユーザーリスト = 2,
			tMemo = 3,
			tMik保守契約 = 4,
			tMikユーザ = 5,
			tClient = 6,
			tMih送料商品コード = 7,
			tMihPca在庫引当表J = 8,
			tMic離島 = 9,
			tMih支店情報 = 10,
			tMih受注ヘッダ = 11,
			tMih受注詳細 = 12,
			tMik基本情報 = 13,
			tMic出荷代行トップ印刷休業日 = 14,
			tBusho = 15,
			t_MicSyukkashiji = 16,
			tMikアプリケーション情報 = 17,
			tMic社内データ管理利用部署情報 = 18,
			tMic社内データ管理ヘッダ = 19,
			tMic社内データ管理詳細 = 20,
			tMic文書インデクス = 21,
			tMikOS明細印字 = 22,
			tUser = 23,
			tMik代行回収 = 24,
			tMik県番号 = 25,
			tMikPca得意先 = 26,
		}

		/// <summary>
		/// テーブル種別/テーブル文字列
		/// </summary>
		public static readonly EnumDictionary<TableType, string> TableName = new EnumDictionary<TableType, string>()
		{
			{ TableType.tMikコードマスタ, string.Format("{0}.tMikコードマスタ", DatabaseName) },
			{ TableType.tMic終了ユーザーリスト, string.Format("{0}.tMic終了ユーザーリスト", DatabaseName) },
			{ TableType.tMemo, string.Format("{0}.tMemo", DatabaseName) },
			{ TableType.tMik保守契約, string.Format("{0}.tMik保守契約", DatabaseName) },
			{ TableType.tMikユーザ, string.Format("{0}.tMikユーザ", DatabaseName) },
			{ TableType.tClient, string.Format("{0}.[tClient]", DatabaseName) },
			{ TableType.tMih送料商品コード, string.Format("{0}.tMih送料商品コード", DatabaseName) },
			{ TableType.tMihPca在庫引当表J, string.Format("{0}.tMihPca在庫引当表J", DatabaseName) },
			{ TableType.tMic離島, string.Format("{0}.tMic離島", DatabaseName) },
			{ TableType.tMih支店情報, string.Format("{0}.tMih支店情報", DatabaseName) },
			{ TableType.tMih受注ヘッダ, string.Format("{0}.tMih受注ヘッダ", DatabaseName) },
			{ TableType.tMih受注詳細, string.Format("{0}.tMih受注詳細", DatabaseName) },
			{ TableType.tMik基本情報, string.Format("{0}.tMik基本情報", DatabaseName) },
			{ TableType.tMic出荷代行トップ印刷休業日, string.Format("{0}.tMic出荷代行トップ印刷休業日", DatabaseName) },
			{ TableType.tBusho, string.Format("{0}.tBusho", DatabaseName) },
			{ TableType.t_MicSyukkashiji, string.Format("{0}.t_MicSyukkashiji", DatabaseName) },
			{ TableType.tMikアプリケーション情報, string.Format("{0}.tMikアプリケーション情報", DatabaseName) },
			{ TableType.tMic社内データ管理利用部署情報, string.Format("{0}.tMic社内データ管理利用部署情報", DatabaseName) },
			{ TableType.tMic社内データ管理ヘッダ, string.Format("{0}.tMic社内データ管理ヘッダ", DatabaseName) },
			{ TableType.tMic社内データ管理詳細, string.Format("{0}.tMic社内データ管理詳細", DatabaseName) },
			{ TableType.tMic文書インデクス, string.Format("{0}.tMic文書インデクス", DatabaseName) },
			{ TableType.tMikOS明細印字, string.Format("{0}.tMikOS明細印字", DatabaseName) },
			{ TableType.tUser, string.Format("{0}.tUser", DatabaseName) },
			{ TableType.tMik代行回収, string.Format("{0}.tMik代行回収", DatabaseName) },
			{ TableType.tMik県番号, string.Format("{0}.tMik県番号", DatabaseName) },
			{ TableType.tMikPca得意先, string.Format("{0}.tMikPca得意先", DatabaseName) },
		};

		/// <summary>
		/// ビュー種別 
		/// </summary>
		public enum ViewType
		{
			vMicPCA仕入先マスタ = 1,
			vMicPCA消費税率 = 2,
			vMicPCA商品マスタ = 3,
			vMic全ユーザー３ = 4,
			vMic担当者 = 5,
			vMicPCA売上ヘッダ = 6,
			vMicPCA売上明細 = 7,
			vMicPCA受注明細 = 8,
			vMic全ユーザー2 = 9,
			vMic全ユーザー3 = 10,
			vMic全ユーザー4 = 11,
			vMicPCA担当者マスタ = 12,
			vMicPCA出荷データ = 13,
			vMicPCA仕入データ = 14,
			vSoftwareMainteLimit = 15,
			vMic当月売上予想 = 16,
			vMic翌月売上予想 = 17,
			vMicPCA部門マスタ = 18,
			vMicPCA区分マスタ = 19,
			vMicES保守売上予測 = 20,
			vMicユーザー基本 = 21,
			vMicソフトウェア保守料売上予測 = 22,
			vMicユーザーオン資用 = 23,
			vMicオンライン資格確認ソフト改修費 = 24,
			vMih担当者 = 25,
			vMic営業担当 = 26,
			vMic全販売店 = 27,
		}

		/// <summary>
		/// ビュー種別/ビュー文字列
		/// </summary>
		public static readonly EnumDictionary<ViewType, string> ViewName = new EnumDictionary<ViewType, string>()
		{
			{ ViewType.vMicPCA仕入先マスタ, string.Format("{0}.vMicPCA仕入先マスタ", DatabaseName) },
			{ ViewType.vMicPCA消費税率, string.Format("{0}.vMicPCA消費税率", DatabaseName) },
			{ ViewType.vMicPCA商品マスタ, string.Format("{0}.vMicPCA商品マスタ", DatabaseName) },
			{ ViewType.vMic全ユーザー３, string.Format("{0}.vMic全ユーザー３", DatabaseName) },
			{ ViewType.vMic担当者, string.Format("{0}.vMic担当者", DatabaseName) },
			{ ViewType.vMicPCA売上ヘッダ, string.Format("{0}.vMicPCA売上ヘッダ", DatabaseName) },
			{ ViewType.vMicPCA売上明細, string.Format("{0}.vMicPCA売上明細", DatabaseName) },
			{ ViewType.vMicPCA受注明細, string.Format("{0}.vMicPCA受注明細", DatabaseName) },
			{ ViewType.vMic全ユーザー2, string.Format("{0}.vMic全ユーザー2", DatabaseName) },
			{ ViewType.vMic全ユーザー3, string.Format("{0}.vMic全ユーザー3", DatabaseName) },
			{ ViewType.vMic全ユーザー4, string.Format("{0}.vMic全ユーザー4", DatabaseName) },
			{ ViewType.vMicPCA担当者マスタ, string.Format("{0}.vMicPCA担当者マスタ", DatabaseName) },
			{ ViewType.vMicPCA出荷データ, string.Format("{0}.vMicPCA出荷データ", DatabaseName) },
			{ ViewType.vMicPCA仕入データ, string.Format("{0}.vMicPCA仕入データ", DatabaseName) },
			{ ViewType.vSoftwareMainteLimit, string.Format("{0}.vSoftwareMainteLimit", DatabaseName) },
			{ ViewType.vMic当月売上予想, string.Format("{0}.vMic当月売上予想", DatabaseName) },
			{ ViewType.vMic翌月売上予想, string.Format("{0}.vMic翌月売上予想", DatabaseName) },
			{ ViewType.vMicPCA部門マスタ, string.Format("{0}.vMicPCA部門マスタ", DatabaseName) },
			{ ViewType.vMicPCA区分マスタ, string.Format("{0}.vMicPCA区分マスタ", DatabaseName) },
			{ ViewType.vMicES保守売上予測, string.Format("{0}.vMicES保守売上予測", DatabaseName) },
			{ ViewType.vMicユーザー基本, string.Format("{0}.vMicユーザー基本", DatabaseName) },
			{ ViewType.vMicソフトウェア保守料売上予測, string.Format("{0}.vMicソフトウェア保守料売上予測", DatabaseName) },
			{ ViewType.vMicユーザーオン資用, string.Format("{0}.vMicユーザーオン資用", DatabaseName) },
			{ ViewType.vMicオンライン資格確認ソフト改修費, string.Format("{0}.vMicオンライン資格確認ソフト改修費", DatabaseName) },
			{ ViewType.vMih担当者, string.Format("{0}.vMih担当者", DatabaseName) },
			{ ViewType.vMic営業担当, string.Format("{0}.vMic営業担当", DatabaseName) },
			{ ViewType.vMic全販売店, string.Format("{0}.vMic全販売店", DatabaseName) },
		};
	}
}
