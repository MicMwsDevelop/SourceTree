using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.BaseFactory.EntryFinishedUser;
using System.Data;

namespace MwsLib.DB.SqlServer.EntryFinishedUser
{
	public static class EntryFinishedUserDataController
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
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
					entry.FinishedYearMonth = DataBaseValue.ConvObjectToYearMonthNull(row["終了月"]);
					entry.AcceptDate = DataBaseValue.ConvObjectToDateNull(row["終了届受領日"]);
					result.Add(entry);
				}
			}
			return result;
		}
	}
}
