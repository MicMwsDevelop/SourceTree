//
// EntryFinishedUserData.cs
//
// 終了ユーザー管理情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/12/12 勝呂)
// 
using MwsLib.Common;

namespace MwsLib.BaseFactory.EntryFinishedUser
{
	/// <summary>
	/// 終了ユーザーデータ
	/// </summary>
	public class EntryFinishedUserData
	{
		/// <summary>
		/// 得意先No
		/// </summary>
		public string TokuisakiNo { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerID { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// レセコン名称
		/// </summary>
		public string SystemName { get; set; }

		/// <summary>
		/// 拠点コード
		/// </summary>
		public string AreaCode { get; set; }

		/// <summary>
		/// 拠点名
		/// </summary>
		public string AreaName { get; set; }

		/// <summary>
		/// 都道府県名
		/// </summary>
		public string KenName { get; set; }

		/// <summary>
		/// 終了事由
		/// </summary>
		public string FinishedReason { get; set; }

		/// <summary>
		/// リプレース
		/// </summary>
		public string Replace { get; set; }

		/// <summary>
		/// 理由
		/// </summary>
		public string Reason { get; set; }

		/// <summary>
		/// コメント
		/// </summary>
		public string Comment { get; set; }

		/// <summary>
		/// 有効ユーザーフラグ
		/// </summary>
		public bool EnableUserFlag { get; set; }

		/// <summary>
		/// 除外
		/// </summary>
		public string Expcet { get; set; }

		/// <summary>
		/// 販売店ID
		/// </summary>
		public string HanbaitenID { get; set; }

		/// <summary>
		/// 販売店名称
		/// </summary>
		public string HanbaitenName { get; set; }

		/// <summary>
		/// 終了月
		/// </summary>
		public YearMonth? FinishedYearMonth { get; set; }

		/// <summary>
		/// 終了届受領日
		/// </summary>
		public Date? AcceptDate { get; set; }

		/// <summary>
		/// 非paletteユーザー
		/// </summary>
		public bool NonPaletteUser { get; set; }

		/// <summary>
		/// 終了フラグユーザー
		/// </summary>
		public bool FinishedUser { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public EntryFinishedUserData()
		{
			CustomerID = 0;
			TokuisakiNo = string.Empty;
			UserName = string.Empty;
			SystemName = string.Empty;
			AreaCode = string.Empty;
			AreaName = string.Empty;
			KenName = string.Empty;
			FinishedReason = string.Empty;
			Replace = string.Empty;
			Reason = string.Empty;
			Comment = string.Empty;
			EnableUserFlag = false;
			Expcet = string.Empty;
			HanbaitenID = string.Empty;
			HanbaitenName = string.Empty;
			FinishedYearMonth = null;
			AcceptDate = null;
			NonPaletteUser = false;
			FinishedUser = false;
		}

		/// <summary>
		/// 翌月終了ユーザーかどうか？（palette）
		/// </summary>
		/// <param name="ym">当月</param>
		/// <returns>判定</returns>
		public bool IsNextMonthFinishedUserByPalette(YearMonth ym)
		{
			if (false == FinishedUser)
			{
				if (FinishedYearMonth.HasValue)
				{
					if (FinishedYearMonth.Value == ym + 1)
					{
						// 翌月終了ユーザー
						if (-1 != SystemName.IndexOf("palette"))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		/// <summary>
		/// 翌月終了ユーザーかどうか？（旧システム）
		/// </summary>
		/// <param name="ym">当月</param>
		/// <returns>判定</returns>
		public bool IsNextMonthFinishedUserByOldSystem(YearMonth ym)
		{
			if (false == FinishedUser)
			{
				if (FinishedYearMonth.HasValue)
				{
					if (FinishedYearMonth.Value == ym + 1)
					{
						// 翌月終了ユーザー
						if (-1 == SystemName.IndexOf("palette"))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		/// <summary>
		/// 前月終了ユーザーかどうか？（palette）
		/// 非paletteユーザー及び旧システムを除く
		/// </summary>
		/// <param name="ym">当月</param>
		/// <returns>判定</returns>
		public bool IsPrevMonthFinishedUserByPalette(YearMonth ym)
		{
			if (false == FinishedUser && false == NonPaletteUser)
			{
				if (FinishedYearMonth.HasValue)
				{
					if (FinishedYearMonth.Value == ym - 1)
					{
						// 前月終了ユーザー
						if (-1 != SystemName.IndexOf("palette"))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		///// <summary>
		///// 翌月非paletteユーザーかどうか？
		///// </summary>
		///// <param name="ym">当月</param>
		///// <returns>判定</returns>
		//public bool IsNextMonthNonPaletteUser(YearMonth ym)
		//{
		//	if (false == FinishedUser && true == NonPaletteUser)
		//	{
		//		if (FinishedYearMonth.HasValue)
		//		{
		//			if (FinishedYearMonth.Value == ym + 1)
		//			{
		//				// 翌月非paletteユーザー
		//				return true;
		//			}
		//		}
		//	}
		//	return false;
		//}

		///// <summary>
		///// 前月非paletteユーザーかどうか？
		///// </summary>
		///// <param name="ym">当月</param>
		///// <returns>判定</returns>
		//public bool IsPrevMonthNonPaletteUser(YearMonth ym)
		//{
		//	if (false == FinishedUser && true == NonPaletteUser)
		//	{
		//		if (FinishedYearMonth.HasValue)
		//		{
		//			if (FinishedYearMonth.Value == ym - 1)
		//			{
		//				// 前月非paletteユーザー
		//				return true;
		//			}
		//		}
		//	}
		//	return false;
		//}
	}
}
