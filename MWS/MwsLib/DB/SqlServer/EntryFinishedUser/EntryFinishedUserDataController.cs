using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.BaseFactory.EntryFinishedUser;
using System.Data;
using MwsLib.Common;

namespace MwsLib.DB.SqlServer.EntryFinishedUser
{
	public static class EntryFinishedUserDataController
	{
		/// <summary>
		/// 顧客情報の詰め替え
		/// </summary>
		/// <param name="table">DataTable</param>
		/// <returns>顧客情報リスト</returns>
		public static List<EntryFinishedUserData> ConvertEntryFinishedUserDataList(DataTable table)
		{
			List<EntryFinishedUserData> result = null;
			if (null != table)
			{
				result = new List<EntryFinishedUserData>();
				foreach (DataRow row in table.Rows)
				{
					EntryFinishedUserData entry = new EntryFinishedUserData();
					entry.CostomerID = row["顧客No"].ToString();
					entry.TokuisakiNo = row["得意先No"].ToString();
					entry.UserName = row["顧客名"].ToString();
					entry.SystemName = row["レセコン名称"].ToString();
					entry.AreaCode = row["拠点コード"].ToString();
					entry.AreaName = row["拠点名"].ToString();
					entry.KenName = row["都道府県名"].ToString();
					entry.FinishedReason = row["終了事由"].ToString();
					entry.Replace = row["リプレース"].ToString();
					entry.Reason = row["理由"].ToString();
					entry.Comment = row["コメント"].ToString();
					entry.EnableUserFlag = DataBaseValue.ConvObjectToBool(row["有効ユーザーフラグ"]);
					entry.Expcet = row["除外"].ToString();
					entry.HanbaitenID = row["販売店ID"].ToString();
					entry.HanbaitenName = row["販売店名称"].ToString();
					YearMonth workYM = new YearMonth();
					if (YearMonth.TryParse(row["終了月"].ToString(), out workYM))
					{
						entry.FinishedYearMonth = workYM;
					}
					Date workDate = new Date();
					if (Date.TryParse(row["終了届受領日"].ToString(), out workDate))
					{
						entry.AcceptDate = workDate;
					}
					result.Add(entry);
				}
			}
			return result;
		}

		/// <summary>
		/// リプレース先メーカーの詰め替え
		/// </summary>
		/// <param name="table">DataTable</param>
		/// <returns>リプレース先メーカー</returns>
		public static List<string> ConvertReplaceMakerList(DataTable table)
		{
			List<string> result = null;
			if (null != table)
			{
				result = new List<string>();
				foreach (DataRow row in table.Rows)
				{
					result.Add(row["fcm名称"].ToString());
				}
			}
			return result;
		}
	}
}
