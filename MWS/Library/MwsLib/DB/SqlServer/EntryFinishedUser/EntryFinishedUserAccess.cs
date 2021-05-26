//
// EntryFinishedUserAccess.cs
//
// 終了ユーザー管理 データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using MwsLib.BaseFactory.EntryFinishedUser;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.EntryFinishedUser
{
	public static class EntryFinishedUserAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 終了ユーザー情報リストの取得
		/// </summary>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>顧客情報リスト</returns>
		public static List<EntryFinishedUserData> GetEntryFinishedUserDataList(bool ct)
		{
			DataTable table = EntryFinishedUserGetIO.GetEntryFinishedUserList(ct);
			return EntryFinishedUserController.ConvertEntryFinishedUserList(table);
		}

		/// <summary>
		/// 得意先Noに該当する顧客情報を取得
		/// </summary>
		/// <param name="tokuisakiID">得意先No</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>顧客情報</returns>
		public static EntryFinishedUserData GetCustomerInfo(string tokuisakiID, bool ct)
		{
			DataTable table = EntryFinishedUserGetIO.GetCustomerInfo(tokuisakiID, ct);
			return EntryFinishedUserController.ConvertCustomerInfo(table);
		}
	}
}
