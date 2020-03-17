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
		};

		/// <summary>
		/// ビュー種別 
		/// </summary>
		public enum ViewType
		{
			vMicPCA仕入先マスタ = 1,
			vMicPCA消費税率 = 2,
			vMicPCA商品マスタ = 3,
			vMicクラウドデータバンク商品売上 = 4,
			vMic全ユーザー３ = 5,
			vMic担当者 = 6,
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
		};
	}
}
