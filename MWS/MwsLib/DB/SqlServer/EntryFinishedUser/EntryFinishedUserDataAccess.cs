using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MwsLib.BaseFactory.EntryFinishedUser;

namespace MwsLib.DB.SqlServer.EntryFinishedUser
{
	public static class EntryFinishedUserDataAccess
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>顧客情報リスト</returns>
		public static List<EntryFinishedUserData> GetEntryFinishedUserDataList(bool sqlsv2)
		{
			DataTable table = EntryFinishedUserDataGetIO.GetEntryFinishedUserDataList(sqlsv2);
			return EntryFinishedUserDataController.ConvertEntryFinishedUserDataList(table);
		}

		/// <summary>
		/// 得意先Noに該当する顧客情報を朱徳
		/// </summary>
		/// <param name="tokuisakiID">得意先No</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>顧客情報</returns>
		public static EntryFinishedUserData GetEntryFinishedUserData(string tokuisakiID, bool sqlsv2)
		{
			DataTable table = EntryFinishedUserDataGetIO.GetEntryFinishedUserDataList(sqlsv2, tokuisakiID);
			List<EntryFinishedUserData> list = EntryFinishedUserDataController.ConvertEntryFinishedUserDataList(table);
			if (0 < list.Count)
			{
				return list[0];
			}
			return null;
		}

		/// <summary>
		/// リプレース先メーカーリストの取得
		/// </summary>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>リプレース先メーカーリスト</returns>
		public static List<string> GetReplaceMakerList(bool sqlsv2)
		{
			DataTable table = EntryFinishedUserDataGetIO.GetReplaceMakerList(sqlsv2);
			return EntryFinishedUserDataController.ConvertReplaceMakerList(table);
		}

		/// <summary>
		/// 顧客情報の追加
		/// </summary>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <param name="data">顧客情報</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoEntryFinishedUserData(bool sqlsv2, EntryFinishedUserData data)
		{
			return EntryFinishedUserDataSetIO.InsertIntoEntryFinishedUserData(sqlsv2, data);
		}

		/// <summary>
		/// 顧客情報の更新
		/// </summary>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <param name="data">顧客情報</param>
		/// <returns>影響行数</returns>
		public static int UpdateEntryFinishedUserData(bool sqlsv2, EntryFinishedUserData data)
		{
			return EntryFinishedUserDataSetIO.UpdateEntryFinishedUserData(sqlsv2, data);
		}

		/// <summary>
		/// メモ情報の新規追加
		/// </summary>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <param name="data">顧客情報</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoMemo(bool sqlsv2, EntryFinishedUserData data)
		{
			return EntryFinishedUserDataSetIO.InsertIntoMemo(sqlsv2, data);
		}
	}
}
