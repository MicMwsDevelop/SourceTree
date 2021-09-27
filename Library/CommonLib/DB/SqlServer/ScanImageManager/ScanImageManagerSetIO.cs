//
// ScanerImageManagerSetIO.cs
//
// 文書インデックス管理 データ設定クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/06 勝呂)
// Ver1.01 SQL Server接続情報を環境設定に移行(2021/09/07 勝呂)
// 
using CommonLib.BaseFactory.ScanImageManager;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CommonLib.DB.SqlServer.ScanImageManager
{
	public static class ScanImageManagerSetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		///  [JunpDB].[dbo].[tMic文書インデクス]の追加
		/// </summary>
		/// <param name="startID">開始ID</param>
		/// <param name="list">文書インデクス情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoDocumentIndexList(int startID, List<ScanImageFile> list, string connectStr)
		{
			string sqlString = string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8)", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMic文書インデクス]);
			List<SqlParameter[]> paramList = new List<SqlParameter[]>();
			for (int i = 0; i < list.Count; i++)
			{
				ScanImageFile data = list[i];
				SqlParameter[] param = { new SqlParameter("@1", i),					// [ID]
													new SqlParameter("@2", data.CustomerNo),		// [顧客Ｎｏ]
													new SqlParameter("@3", data.TokuisakiNo),		// [得意先Ｎｏ]
													new SqlParameter("@4", (int)data.Document),		// [文書種別]
													new SqlParameter("@5", data.FolderName),		// [フォルダ名]
													new SqlParameter("@6", data.FileName),			// [ファイル名]
													new SqlParameter("@7", (data.FileDateTime.HasValue) ? data.FileDateTime.Value.ToString() : System.Data.SqlTypes.SqlString.Null),	// [ファイル更新日時]
													new SqlParameter("@8", DateTime.Now) };         // [更新日]
				paramList.Add(param);
			}
			return DatabaseAccess.InsertIntoListDatabase(sqlString, paramList, connectStr);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMic文書インデクス]の全削除
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int DeleteDocumentIndex(string connectStr)
		{
			string sqlString = string.Format(@"DELETE FROM {0}", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMic文書インデクス]);
			return DatabaseAccess.DeleteDatabase(sqlString, connectStr);
		}
	}
}
