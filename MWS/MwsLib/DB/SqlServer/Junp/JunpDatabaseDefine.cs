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
		};

		/// <summary>
		/// ビュー種別 
		/// </summary>
		public enum ViewType
		{
			vMicPCA仕入先マスタ = 1,
		}

		/// <summary>
		/// ビュー種別/ビュー文字列
		/// </summary>
		public static readonly EnumDictionary<ViewType, string> ViewName = new EnumDictionary<ViewType, string>()
		{
			{ ViewType.vMicPCA仕入先マスタ, "vMicPCA仕入先マスタ" },
		};
	}
}
