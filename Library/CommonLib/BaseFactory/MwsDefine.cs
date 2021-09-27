//
// MWS関連定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/02/08 勝呂)
//
using CommonLib.Common;

namespace CommonLib.BaseFactory
{
	/// <summary>
	/// MWS関連定義クラス
	/// </summary>
	public static class MwsDefine
	{
		//////////////////////////////////////////////////////////
		/// レングス長定義

		/// <summary>
		/// 顧客No
		/// </summary>
		public const int CustomerNoLength = 8;

		/// <summary>
		/// 得意先No
		/// </summary>
		public const int TokuisakiNoLength = 6;


		//////////////////////////////////////////////////////////
		/// システムコード

		/// <summary>
		/// palette ネットワーク版
		/// </summary>
		public const string SystemCodePaletteNetwork = "101";

		/// <summary>
		/// palette 単体版
		/// </summary>
		public const string SystemCodePaletteStandalone = "102";

		/// <summary>
		/// その他
		/// </summary>
		public const string SystemCodeEtc = "999";


		//////////////////////////////////////////////////////////
		/// 商品ID

		/// <summary>
		/// 申込種別/販売種別
		/// [charlieDB].[dbo].[T_CUSTOMER_FOUNDATIONS].[APPLY_TYPE]
		/// </summary>
		public enum ApplyType
		{
			/// <summary>
			/// その他
			/// </summary>
			Etc = 0,

			/// <summary>
			/// バリューパック
			/// </summary>
			ValuePack = 1,

			/// <summary>
			/// アップグレード
			/// </summary>
			Upgrade = 2,

			/// <summary>
			/// 月額課金
			/// </summary>
			Monthly = 3,

			/// <summary>
			/// まとめ
			/// </summary>
			Matome = 4,

			/// <summary>
			/// PC安心サポート
			/// </summary>
			PcSupport = 5,
		}

		/// <summary>
		/// 申込種別/販売種別文字列
		/// </summary>
		public static readonly EnumDictionary<ApplyType, string> ApplyTypeString = new EnumDictionary<ApplyType, string>()
		{
			{ ApplyType.Etc, "その他" },
			{ ApplyType.ValuePack, "ＶＰ" },
			{ ApplyType.Upgrade, "ＵＧ" },
			{ ApplyType.Monthly, "月額" },
			{ ApplyType.Matome, "まとめ" },
			{ ApplyType.PcSupport, "PC安心" },
		};

		/// <summary>
		/// ユーザー種別
		/// [CharlieDB].[dbo].[T_PRODUCT_CONTROL].[USER_CLASSIFICATION]
		/// </summary>
		public enum UserClassification
		{
			/// <summary>
			/// paletteユーザー
			/// </summary>
			PaletteUser = 0,

			/// <summary>
			/// 非paletteユーザー
			/// </summary>
			NonPaletteUser = 1,

			/// <summary>
			/// MIC社員
			/// </summary>
			MicEmployeeUser = 2,

			/// <summary>
			/// デモユーザー
			/// </summary>
			DemoUser = 3,
		}
	}
}
