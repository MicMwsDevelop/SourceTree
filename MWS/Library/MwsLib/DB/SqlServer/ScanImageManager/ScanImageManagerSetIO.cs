//
// ScanerImageManagerSetIO.cs
//
// 文書インデックス管理 データ設定クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/06 勝呂)
// 
using MwsLib.BaseFactory.ScanImageManager;
using MwsLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.ScanImageManager
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
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoDocumentIndexList(int startID, List<ScanImageFile> list, bool ct)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SqlTransaction tran = con.BeginTransaction())
					{
						try
						{
							string sqlString = string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8)", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMic文書インデクス]);
							int i = startID;
							foreach (ScanImageFile data in list)
							{
								SqlParameter[] param = { new SqlParameter("@1", i),					// [ID]
													new SqlParameter("@2", data.CustomerNo),		// [顧客Ｎｏ]
													new SqlParameter("@3", data.TokuisakiNo),		// [得意先Ｎｏ]
													new SqlParameter("@4", (int)data.Document),		// [文書種別]
													new SqlParameter("@5", data.FolderName),		// [フォルダ名]
													new SqlParameter("@6", data.FileName),			// [ファイル名]
													new SqlParameter("@7", (data.FileDateTime.HasValue) ? data.FileDateTime.Value.ToString() : System.Data.SqlTypes.SqlString.Null),	// [ファイル更新日時]
													new SqlParameter("@8", DateTime.Now) };			// [更新日]

								// 実行
								rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
								if (rowCount <= -1)
								{
									throw new ApplicationException("InsertIntoDocumentIndexList() Error!");
								}
								i++;
							}
							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMic文書インデクス]の全削除
		/// </summary>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int DeleteDocumentIndex(bool ct)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SqlTransaction tran = con.BeginTransaction())
					{
						try
						{
							string sqlString = string.Format(@"DELETE FROM {0}", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMic文書インデクス]);

							// 実行
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString);
							if (rowCount <= -1)
							{
								throw new ApplicationException("DeleteDocumentIndex() Error!");
							}
							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}
	}
}
