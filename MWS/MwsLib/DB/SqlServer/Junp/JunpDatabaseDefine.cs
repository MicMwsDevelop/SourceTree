//
// JunpDatabaseDefine.cs
//
// JunpDB データベース定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using MwsLib.Common;

namespace MwsLib.DB.SqlServer.Junp
{
	/// <summary>
	/// JumpDB データベース定義
	/// </summary>
	public static class JunpDatabaseDefine
	{
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
		}

		/// <summary>
		/// テーブル種別/テーブル文字列
		/// </summary>
		public static readonly EnumDictionary<TableType, string> TableName = new EnumDictionary<TableType, string>()
		{
			{ TableType.tMikコードマスタ, "tMikコードマスタ" },
			{ TableType.tMic終了ユーザーリスト, "tMic終了ユーザーリスト" },
			{ TableType.tMemo, "tMemo" },
			{ TableType.tMik保守契約, "tMik保守契約" },
			{ TableType.tMikユーザ, "tMikユーザ" },
			{ TableType.tClient, "tClient" },
			{ TableType.tMih送料商品コード, "tMih送料商品コード" },
			{ TableType.tMihPca在庫引当表J, "tMihPca在庫引当表J" },
			{ TableType.tMic離島, "tMic離島" },
			{ TableType.tMih支店情報, "tMih支店情報" },
			{ TableType.tMih受注ヘッダ, "tMih受注ヘッダ" },
			{ TableType.tMih受注詳細, "tMih受注詳細" },
			{ TableType.tMik基本情報, "tMik基本情報" },
			{ TableType.tMic出荷代行トップ印刷休業日, "tMic出荷代行トップ印刷休業日" },
			{ TableType.tBusho, "tBusho" },
			{ TableType.t_MicSyukkashiji, "t_MicSyukkashiji" },
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
			vMicPCA売上明細 = 6,
			vMicPCA受注明細 = 7,
			vMic全ユーザー２ = 8,
			vMicPCA担当者マスタ = 9,
			vMicPCA出荷データ = 10,
			vMicPCA仕入データ = 11,
		}

		/// <summary>
		/// ビュー種別/ビュー文字列
		/// </summary>
		public static readonly EnumDictionary<ViewType, string> ViewName = new EnumDictionary<ViewType, string>()
		{
			{ ViewType.vMicPCA仕入先マスタ, "vMicPCA仕入先マスタ" },
			{ ViewType.vMicPCA消費税率, "vMicPCA消費税率" },
			{ ViewType.vMicPCA商品マスタ, "vMicPCA商品マスタ" },
			{ ViewType.vMic全ユーザー３, "vMic全ユーザー３" },
			{ ViewType.vMic担当者, "vMic担当者" },
			{ ViewType.vMicPCA売上明細, "vMicPCA売上明細" },
			{ ViewType.vMicPCA受注明細, "vMicPCA受注明細" },
			{ ViewType.vMic全ユーザー２, "vMic全ユーザー２" },
			{ ViewType.vMicPCA担当者マスタ, "vMicPCA担当者マスタ" },
			{ ViewType.vMicPCA出荷データ, "vMicPCA出荷データ" },
			{ ViewType.vMicPCA仕入データ, "vMicPCA仕入データ" },
		};
	}
}
