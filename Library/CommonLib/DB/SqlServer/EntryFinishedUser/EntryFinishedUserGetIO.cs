﻿//
// EntryFinishedUserGetIO.cs
//
// 終了ユーザー管理 データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using CommonLib.DB.SqlServer.Junp;
using System.Data;

namespace CommonLib.DB.SqlServer.EntryFinishedUser
{
	public static class EntryFinishedUserGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 終了ユーザー情報リストの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static DataTable GetEntryFinishedUserList(string connectStr)
		{
			string strSQL = string.Format(@"SELECT"
							+ " U.得意先No"
							+ ", U.顧客No"
							+ ", U.顧客名１ + U.顧客名２ AS 顧客名"
							+ ", E.終了月"
							+ ", E.終了届受領日"
							+ ", E.終了事由"
							+ ", E.リプレース"
							+ ", E.理由"
							+ ", E.非paletteユーザー"
							+ ", U.レセコン名称 AS システム名"
							+ ", U.拠点名"
							+ ", U.レセコンシステムコード AS システムコード"
							+ ", U.拠点コード"
							+ ", U.都道府県名"
							+ ", U.販売店ID"
							+ ", U.販売店名称"
							+ ", U.有効ユーザーフラグ"
							+ ", U.終了フラグ"
							+ ", '' AS 除外"
							+ ", '' AS コメント"
							+ " FROM {0} AS E"
							+ " INNER JOIN {1} AS U ON E.得意先No = U.得意先No"
							+ " ORDER BY U.得意先No ASC"
							, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMic終了ユーザーリスト]
							, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー３]);

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// 顧客情報の取得
		/// </summary>
		/// <param name="tokuisakiNo">得意先No</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static DataTable GetCustomerInfo(string tokuisakiNo, string connectStr)
		{
			string strSQL = string.Format(@"SELECT"
										+ " 得意先No"
										+ ", 顧客No"
										+ ", 顧客名１ + 顧客名２ AS 顧客名"
										+ ", レセコン名称"
										+ ", 拠点名"
										+ ", レセコンシステムコード AS システムコード"
										+ ", 拠点コード"
										+ ", 都道府県名"
										+ ", 有効ユーザーフラグ"
										+ ", 販売店ID"
										+ ", 販売店名称"
										+ " FROM {0}"
										+ " WHERE 得意先No = '{1}'"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー３]
										, tokuisakiNo);

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}
	}
}
