using MwsLib.BaseFactory.EntryFinishedUser;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.EntryFinishedUser
{
	public static class EntryFinishedUserAccess
	{
		/// <summary>
		/// 終了ユーザー情報リストの取得
		/// </summary>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>顧客情報リスト</returns>
		public static List<EntryFinishedUserData> GetEntryFinishedUserDataList(bool sqlsv2)
		{
			DataTable table = EntryFinishedUserGetIO.GetEntryFinishedUserList(sqlsv2);
			return EntryFinishedUserController.ConvertEntryFinishedUserList(table);
		}

		/// <summary>
		/// 得意先Noに該当する顧客情報を取得
		/// </summary>
		/// <param name="tokuisakiID">得意先No</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>顧客情報</returns>
		public static EntryFinishedUserData GetCustomerInfo(string tokuisakiID, bool sqlsv2)
		{
			DataTable table = EntryFinishedUserGetIO.GetCustomerInfo(tokuisakiID, sqlsv2);
			return EntryFinishedUserController.ConvertCustomerInfo(table);
		}

		/// <summary>
		/// リプレース先メーカーリストの取得
		/// </summary>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>リプレース先メーカーリスト</returns>
		public static List<string> GetReplaceMakerList(bool sqlsv2)
		{
			DataTable table = EntryFinishedUserGetIO.GetReplaceMakerList(sqlsv2);
			return EntryFinishedUserController.ConvertReplaceMakerList(table);
		}

		/// <summary>
		/// 終了ユーザー情報の追加
		/// </summary>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <param name="entry">顧客情報</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoEntryFinishedUser(EntryFinishedUserData entry, bool sqlsv2)
		{
			return EntryFinishedUserSetIO.InsertIntoEntryFinishedUser(entry, sqlsv2);
		}

		/// <summary>
		/// 終了ユーザー情報の更新
		/// </summary>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <param name="entry">終了ユーザー情報</param>
		/// <returns>影響行数</returns>
		public static int UpdateEntryFinishedUser(EntryFinishedUserData entry, bool sqlsv2)
		{
			return EntryFinishedUserSetIO.UpdateEntryFinishedUser(entry, sqlsv2);
		}

		/// <summary>
		/// メモ情報の新規追加
		/// </summary>
		/// <param name="entry">終了ユーザー情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoMemo(EntryFinishedUserData entry, bool sqlsv2)
		{
			return EntryFinishedUserSetIO.InsertIntoMemo(entry, sqlsv2);
		}
	}
}
