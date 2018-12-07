using MwsLib.BaseFactory.EntryFinishedUser;
using MwsLib.Common;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.EntryFinishedUser
{
	public static class EntryFinishedUserController
	{
		/// <summary>
		/// 終了ユーザー情報の詰め替え
		/// </summary>
		/// <param name="table">DataTable</param>
		/// <returns>終了ユーザー情報リスト</returns>
		public static List<EntryFinishedUserData> ConvertEntryFinishedUserList(DataTable table)
		{
			List<BaseFactory.EntryFinishedUser.EntryFinishedUserData> result = null;
			if (null != table)
			{
				result = new List<BaseFactory.EntryFinishedUser.EntryFinishedUserData>();
				foreach (DataRow row in table.Rows)
				{
					EntryFinishedUserData entry = new BaseFactory.EntryFinishedUser.EntryFinishedUserData();
					entry.TokuisakiNo = row["得意先No"].ToString();
					entry.CostomerID = row["顧客No"].ToString();
					entry.UserName = row["顧客名"].ToString();
					entry.SystemName = row["システム名"].ToString();
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
					Date workDate;
					if (Date.TryParse(row["終了届受領日"].ToString(), out workDate))
					{
						entry.AcceptDate = workDate;
					}
					entry.NonPaletteUser = DataBaseValue.ConvObjectToBool(row["非paletteユーザー"]);
					entry.FinishedUser = ("0" == row["終了フラグ"].ToString()) ? false : true;

					result.Add(entry);
				}
			}
			return result;
		}

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
					EntryFinishedUserData entry = new EntryFinishedUserData();
					entry.TokuisakiNo = table.Rows[0]["得意先No"].ToString();
					entry.CostomerID = table.Rows[0]["顧客No"].ToString();
					entry.UserName = table.Rows[0]["顧客名"].ToString();
					entry.SystemName = table.Rows[0]["レセコン名称"].ToString();
					entry.AreaCode = table.Rows[0]["拠点コード"].ToString();
					entry.AreaName = table.Rows[0]["拠点名"].ToString();
					entry.KenName = table.Rows[0]["都道府県名"].ToString();
					entry.EnableUserFlag = DataBaseValue.ConvObjectToBool(table.Rows[0]["有効ユーザーフラグ"]);
					entry.HanbaitenID = table.Rows[0]["販売店ID"].ToString();
					entry.HanbaitenName = table.Rows[0]["販売店名称"].ToString();
					return entry;
				}
			}
			return null;
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
