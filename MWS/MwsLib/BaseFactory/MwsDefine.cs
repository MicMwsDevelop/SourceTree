//
// MWS関連定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/02/08 勝呂)
//
using MwsLib.Common;

namespace MwsLib.BaseFactory
{
	/// <summary>
	/// MWS関連定義クラス
	/// [Charlie].[dbo].[T_CUSTOMER_FOUNDATIONS].APPLY_TYPE
	/// </summary>
	public static class MwsDefine
	{
		/// <summary>
		/// 申込種別
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
		}

		/// <summary>
		/// 申込種別文字列
		/// </summary>
		public static readonly EnumDictionary<ApplyType, string> ApplyTypeString = new EnumDictionary<ApplyType, string>()
		{
			{ ApplyType.Etc, "その他" },
			{ ApplyType.ValuePack, "ＶＰ" },
			{ ApplyType.Upgrade, "ＵＧ" },
			{ ApplyType.Monthly, "月額" },
			{ ApplyType.Matome, "まとめ" },
		};
	}
}
