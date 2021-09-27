//
// EntryFinishedUserController.cs
//
// 終了ユーザー管理 データ詰め替えクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using CommonLib.BaseFactory.EntryFinishedUser;
using CommonLib.Common;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.DB.SqlServer.EntryFinishedUser
{
	public static class EntryFinishedUserController
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 終了ユーザー情報リストの詰め替え
		/// </summary>
		/// <param name="table">DataTable</param>
		/// <returns>終了ユーザー情報リスト</returns>
		public static List<EntryFinishedUserData> ConvertEntryFinishedUserList(DataTable table)
		{
			List<EntryFinishedUserData> result = null;
			if (null != table)
			{
				result = new List<EntryFinishedUserData>();
				foreach (DataRow row in table.Rows)
				{
					EntryFinishedUserData entry = new EntryFinishedUserData
					{
						TokuisakiNo = row["得意先No"].ToString(),
						CustomerID = DataBaseValue.ConvObjectToInt(row["顧客No"]),
						UserName = row["顧客名"].ToString(),
						SystemCode = row["システムコード"].ToString(),
						SystemName = row["システム名"].ToString(),
						AreaCode = row["拠点コード"].ToString(),
						AreaName = row["拠点名"].ToString(),
						KenName = row["都道府県名"].ToString(),
						FinishedReason = row["終了事由"].ToString(),
						Replace = row["リプレース"].ToString(),
						Reason = row["理由"].ToString(),
						Comment = row["コメント"].ToString(),
						EnableUserFlag = DataBaseValue.ConvObjectToBool(row["有効ユーザーフラグ"]),
						Expcet = row["除外"].ToString(),
						HanbaitenID = row["販売店ID"].ToString(),
						HanbaitenName = row["販売店名称"].ToString(),
						NonPaletteUser = ("0" == row["非paletteユーザー"].ToString()) ? false : true,
						FinishedUser = ("0" == row["終了フラグ"].ToString()) ? false : true
					};
					if (YearMonth.TryParse(row["終了月"].ToString(), out YearMonth workYM))
					{
						entry.FinishedYearMonth = workYM;
					}
					if (Date.TryParse(row["終了届受領日"].ToString(), out Date workDate))
					{
						entry.AcceptDate = workDate;
					}
					result.Add(entry);
				}
			}
			return result;
		}

		///// <summary>
		///// 終了ユーザー情報の詰め替え
		///// </summary>
		///// <param name="table">DataTable</param>
		///// <returns>終了ユーザー情報リスト</returns>
		//public static EntryFinishedUserData ConvertEntryFinishedUser(DataTable table)
		//{
		//	if (null != table)
		//	{
		//		if (0 < table.Rows.Count)
		//		{
		//			EntryFinishedUserData entry = new BaseFactory.EntryFinishedUser.EntryFinishedUserData();
		//			entry.TokuisakiNo = table.Rows[0]["得意先No"].ToString();
		//			entry.CustomerID = DataBaseValue.ConvObjectToInt(table.Rows[0]["顧客No"]);
		//			entry.UserName = table.Rows[0]["顧客名"].ToString();
		//			entry.SystemCode = table.Rows[0]["システムコード"].ToString();
		//			entry.SystemName = table.Rows[0]["システム名"].ToString();
		//			entry.AreaCode = table.Rows[0]["拠点コード"].ToString();
		//			entry.AreaName = table.Rows[0]["拠点名"].ToString();
		//			entry.KenName = table.Rows[0]["都道府県名"].ToString();
		//			entry.FinishedReason = table.Rows[0]["終了事由"].ToString();
		//			entry.Replace = table.Rows[0]["リプレース"].ToString();
		//			entry.Reason = table.Rows[0]["理由"].ToString();
		//			entry.Comment = table.Rows[0]["コメント"].ToString();
		//			entry.EnableUserFlag = DataBaseValue.ConvObjectToBool(table.Rows[0]["有効ユーザーフラグ"]);
		//			entry.Expcet = table.Rows[0]["除外"].ToString();
		//			entry.HanbaitenID = table.Rows[0]["販売店ID"].ToString();
		//			entry.HanbaitenName = table.Rows[0]["販売店名称"].ToString();
		//			YearMonth workYM = new YearMonth();
		//			if (YearMonth.TryParse(table.Rows[0]["終了月"].ToString(), out workYM))
		//			{
		//				entry.FinishedYearMonth = workYM;
		//			}
		//			Date workDate;
		//			if (Date.TryParse(table.Rows[0]["終了届受領日"].ToString(), out workDate))
		//			{
		//				entry.AcceptDate = workDate;
		//			}
		//			entry.NonPaletteUser = ("0" == table.Rows[0]["非paletteユーザー"].ToString()) ? false : true;
		//			entry.FinishedUser = ("0" == table.Rows[0]["終了フラグ"].ToString()) ? false : true;
		//			return entry;
		//		}
		//	}
		//	return null;
		//}

		/// <summary>
		/// 顧客情報の詰め替え
		/// </summary>
		/// <param name="table">DataTable</param>
		/// <returns>顧客情報</returns>
		public static EntryFinishedUserData ConvertCustomerInfo(DataTable table)
		{
			if (null != table)
			{
				if (0 < table.Rows.Count)
				{
					EntryFinishedUserData entry = new EntryFinishedUserData
					{
						TokuisakiNo = table.Rows[0]["得意先No"].ToString(),
						CustomerID = DataBaseValue.ConvObjectToInt(table.Rows[0]["顧客No"]),
						UserName = table.Rows[0]["顧客名"].ToString(),
						SystemCode = table.Rows[0]["システムコード"].ToString(),
						SystemName = table.Rows[0]["レセコン名称"].ToString(),
						AreaCode = table.Rows[0]["拠点コード"].ToString(),
						AreaName = table.Rows[0]["拠点名"].ToString(),
						KenName = table.Rows[0]["都道府県名"].ToString(),
						EnableUserFlag = DataBaseValue.ConvObjectToBool(table.Rows[0]["有効ユーザーフラグ"]),
						HanbaitenID = table.Rows[0]["販売店ID"].ToString(),
						HanbaitenName = table.Rows[0]["販売店名称"].ToString()
					};
					return entry;
				}
			}
			return null;
		}

		///// <summary>
		///// リプレース先メーカーの詰め替え
		///// </summary>
		///// <param name="table">DataTable</param>
		///// <returns>リプレース先メーカー</returns>
		//public static List<string> ConvertReplaceMakerList(DataTable table)
		//{
		//	List<string> result = null;
		//	if (null != table)
		//	{
		//		result = new List<string>();
		//		foreach (DataRow row in table.Rows)
		//		{
		//			result.Add(row["fcm名称"].ToString());
		//		}
		//	}
		//	return result;
		//}


		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		///// <summary>
		///// 課金対象外フラグの取得
		///// </summary>
		///// <param name="table">DataTable</param>
		///// <returns>リプレース先メーカーリスト</returns>
		//public static List<int> ConvertPauseEndStatus(DataTable table)
		//{
		//	List<int> result = null;
		//	if (null != table)
		//	{
		//		result = new List<int>();
		//		foreach (DataRow row in table.Rows)
		//		{
		//			result.Add(DataBaseValue.ConvObjectToInt(row["SERVICE_ID"]));
		//		}
		//	}
		//	return result;
		//}
	}
}
