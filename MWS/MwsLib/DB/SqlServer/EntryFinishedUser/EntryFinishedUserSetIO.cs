//
// EntryFinishedUserSetIO.cs
//
// 終了ユーザー管理 データ格納クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using MwsLib.BaseFactory;
using MwsLib.BaseFactory.EntryFinishedUser;
using MwsLib.BaseFactory.Junp.Table;
using MwsLib.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MwsLib.DB.SqlServer.EntryFinishedUser
{
	public static class EntryFinishedUserSetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		///// <summary>
		///// 終了ユーザーリスト情報の追加
		///// [JunpDB].[dbo].[tMic終了ユーザーリスト]
		///// </summary>
		///// <param name="entry">終了ユーザー情報</param>
		///// <param name="sqlsv2">CT環境かどうか？</param>
		///// <returns>影響行数</returns>
		//public static int InsertIntoEntryFinishedUser(EntryFinishedUserData entry, bool sqlsv2)
		//{
		//	int rowCount = -1;
		//	using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(sqlsv2)))
		//	{
		//		try
		//		{
		//			// 接続
		//			con.Open();

		//			// トランザクション開始
		//			using (SqlTransaction tran = con.BeginTransaction())
		//			{
		//				try
		//				{
		//					string sqlString = @"INSERT INTO tMic終了ユーザーリスト VALUES (@1, @2, @3, @4, @5, @6, @7, @8)";
		//					SqlParameter[] param = { new SqlParameter("@1", entry.TokuisakiNo),
		//											new SqlParameter("@2", entry.FinishedYearMonth.Value.ToString()),
		//											new SqlParameter("@3", entry.AcceptDate.Value.ToString()),
		//											new SqlParameter("@4", entry.FinishedReason),
		//											new SqlParameter("@5", entry.Replace),
		//											new SqlParameter("@6", entry.Reason),
		//											new SqlParameter("@7", DateTime.Now),
		//											new SqlParameter("@8", (entry.NonPaletteUser) ? "1" : "0") };
		//					// 実行
		//					rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
		//					if (rowCount <= -1)
		//					{
		//						throw new ApplicationException("InsertIntoEntryFinishedUser() Error!");
		//					}
		//					// コミット
		//					tran.Commit();
		//				}
		//				catch
		//				{
		//					// ロールバック
		//					tran.Rollback();
		//					throw;
		//				}
		//			}
		//		}
		//		catch
		//		{
		//			throw;
		//		}
		//		finally
		//		{
		//			if (null != con)
		//			{
		//				// 切断
		//				con.Close();
		//			}
		//		}
		//	}
		//	return rowCount;
		//}

		///// <summary>
		///// 終了ユーザーリスト情報の更新
		///// [JunpDB].[dbo].[tMic終了ユーザーリスト]
		///// </summary>
		///// <param name="entry">終了ユーザー情報</param>
		///// <param name="sqlsv2">CT環境かどうか？</param>
		///// <returns>影響行数</returns>
		//public static int UpdateSetEntryFinishedUser(EntryFinishedUserData entry, bool sqlsv2)
		//{
		//	int rowCount = -1;
		//	using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(sqlsv2)))
		//	{
		//		try
		//		{
		//			// 接続
		//			con.Open();

		//			// トランザクション開始
		//			using (SqlTransaction tran = con.BeginTransaction())
		//			{
		//				try
		//				{
		//					string sqlString = string.Format(@"UPDATE tMic終了ユーザーリスト SET 得意先No = @1, 終了月 = @2, 終了届受領日 = @3, 終了事由 = @4, リプレース = @5, 理由 = @6, 更新日時 = @7, 非paletteユーザー = @8 WHERE 得意先No = {0}", entry.TokuisakiNo);
		//					SqlParameter[] param = { new SqlParameter("@1", entry.TokuisakiNo),
		//											new SqlParameter("@2", entry.FinishedYearMonth.Value.ToString()),
		//											new SqlParameter("@3", entry.AcceptDate.Value.ToString()),
		//											new SqlParameter("@4", entry.FinishedReason),
		//											new SqlParameter("@5", entry.Replace),
		//											new SqlParameter("@6", entry.Reason),
		//											new SqlParameter("@7", DateTime.Now),
		//											new SqlParameter("@8", (entry.NonPaletteUser) ? "1" : "0"), };
		//					// 実行
		//					rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
		//					if (rowCount <= -1)
		//					{
		//						throw new ApplicationException("UpdateEntryFinishedUser() Error!");
		//					}
		//					// コミット
		//					tran.Commit();
		//				}
		//				catch
		//				{
		//					// ロールバック
		//					tran.Rollback();
		//					throw;
		//				}
		//			}
		//		}
		//		catch
		//		{
		//			throw;
		//		}
		//		finally
		//		{
		//			if (null != con)
		//			{
		//				// 切断
		//				con.Close();
		//			}
		//		}
		//	}
		//	return rowCount;
		//}

		///// <summary>
		///// 終了ユーザーリスト情報の削除
		///// [JunpDB].[dbo].[tMic終了ユーザーリスト]
		///// </summary>
		///// <param name="tokuisakiNo">得意先No</param>
		///// <param name="sqlsv2">CT環境かどうか？</param>
		///// <returns>影響行数</returns>
		//public static int DeleteEntryFinishedUser(string tokuisakiNo, bool sqlsv2)
		//{
		//	int rowCount = -1;
		//	using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(sqlsv2)))
		//	{
		//		try
		//		{
		//			// 接続
		//			con.Open();

		//			// トランザクション開始
		//			using (SqlTransaction tran = con.BeginTransaction())
		//			{
		//				try
		//				{
		//					string sqlString = string.Format(@"DELETE FROM tMic終了ユーザーリスト WHERE 得意先No = '{0}'", tokuisakiNo);

		//					// 実行
		//					rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString);
		//					if (rowCount <= -1)
		//					{
		//						throw new ApplicationException("DeleteEntryFinishedUser() Error!");
		//					}
		//					// コミット
		//					tran.Commit();
		//				}
		//				catch
		//				{
		//					// ロールバック
		//					tran.Rollback();
		//					throw;
		//				}
		//			}
		//		}
		//		catch
		//		{
		//			throw;
		//		}
		//		finally
		//		{
		//			if (null != con)
		//			{
		//				// 切断
		//				con.Close();
		//			}
		//		}
		//	}
		//	return rowCount;
		//}

		///// <summary>
		///// メモ情報の新規追加
		///// [JunpDB].[dbo].[tMemo]
		///// </summary>
		///// <param name="entry">終了ユーザー情報</param>
		///// <param name="sqlsv2">CT環境かどうか？</param>
		///// <returns>影響行数</returns>
		//public static int InsertIntoMemo(EntryFinishedUserData entry, bool sqlsv2)
		//{
		//	int rowCount = -1;
		//	using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(sqlsv2)))
		//	{
		//		try
		//		{
		//			// 接続
		//			con.Open();

		//			// トランザクション開始
		//			using (SqlTransaction tran = con.BeginTransaction())
		//			{
		//				try
		//				{
		//					string sqlString = @"INSERT INTO tMemo VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18)";
		//					SqlParameter[] param = { new SqlParameter("@1", entry.CustomerID),
		//											new SqlParameter("@2", "tClient"),
		//											new SqlParameter("@3", string.Format("{0} {1:D2}:{2:D2} 営業管理部", new Date(DateTime.Now).ToString(), DateTime.Now.Hour, DateTime.Now.Minute)),
		//											new SqlParameter("@4", entry.GetMemoString()),
		//											new SqlParameter("@5", DateTime.Now),
		//											new SqlParameter("@6", "営業管理部"),
		//											new SqlParameter("@7", DBNull.Value),
		//											new SqlParameter("@8", DBNull.Value),
		//											new SqlParameter("@9", DBNull.Value),
		//											new SqlParameter("@10", DBNull.Value),
		//											new SqlParameter("@11", Convert.ToInt32(0)),
		//											new SqlParameter("@12", Convert.ToInt32(0)),
		//											new SqlParameter("@13", Convert.ToInt32(0)),
		//											new SqlParameter("@14", Convert.ToInt32(0)),
		//											new SqlParameter("@15", Convert.ToInt32(0)),
		//											new SqlParameter("@16", Convert.ToInt32(0)),
		//											new SqlParameter("@17", DBNull.Value),
		//											new SqlParameter("@18", DBNull.Value) };
		//					// 実行
		//					rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
		//					if (rowCount <= -1)
		//					{
		//						throw new ApplicationException("InsertIntoMemo() Error!");
		//					}
		//					// コミット
		//					tran.Commit();
		//				}
		//				catch
		//				{
		//					// ロールバック
		//					tran.Rollback();
		//					throw;
		//				}
		//			}
		//		}
		//		catch
		//		{
		//			throw;
		//		}
		//		finally
		//		{
		//			if (null != con)
		//			{
		//				// 切断
		//				con.Close();
		//			}
		//		}
		//	}
		//	return rowCount;
		//}

		/// <summary>
		/// 前月終了月終了ユーザー処理
		/// </summary>
		/// <param name="userList">終了ユーザー情報リスト</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSetPrevMonthFinishedUser(string productName, IEnumerable<tMikコードマスタ> codeMasterList, IEnumerable<EntryFinishedUserData> userList, bool sqlsv2)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(sqlsv2)))
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
							foreach (EntryFinishedUserData user in userList)
							{
								if (user.NonPaletteUser)
								{
									// palette → 非paletteユーザー
									string replace = string.Empty;
									if (0 < user.Replace.Length)
									{
										IEnumerable<tMikコードマスタ> list = codeMasterList.Where(p => p.fcm名称 == StringUtil.ConvertUpperCase(user.Replace));
										if (0 < list.Count())
										{
											replace = list.First().fcmコード;
										}
									}
									// [JunpDB].[dbo].[tMikユーザー].[fusユーザー] = 0（非ユーザー）
									// [JunpDB].[dbo].[tMikユーザー].[fus前ｼｽﾃﾑ名称] = [JunpDB].[dbo].[tMikユーザー].[fusｼｽﾃﾑ名]
									// [JunpDB].[dbo].[tMikユーザー].[fusｼｽﾃﾑ名] = '999'（その他）
									// [JunpDB].[dbo].[tMikユーザー].[fusメーカー名] = リプレース
									// [JunpDB].[dbo].[tMikユーザー].[fus更新日] = 現在
									// [JunpDB].[dbo].[tMikユーザー].[fus更新者] = プログラム名
									string sqlString = string.Format(@"UPDATE tMikユーザー SET fusユーザー = @1, fus前ｼｽﾃﾑ名称 = @2, fusｼｽﾃﾑ名 = @3,  fusメーカー名 = @4,  fus更新日 = @5, fus更新者 = @6 WHERE 顧客No = {0}", user.CustomerID);
									SqlParameter[] param = { new SqlParameter("@1", "0"),
															new SqlParameter("@2", user.SystemCode),
															new SqlParameter("@3", MwsDefine.SystemCodeEtc),
															new SqlParameter("@4", replace),
															new SqlParameter("@5", DateTime.Now),
															new SqlParameter("@6", productName) };

									// 実行
									rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
									if (rowCount <= -1)
									{
										throw new ApplicationException("UpdateSetPrevMonthFinishedUser() Error!");
									}
								}
								else
								{
									// palette → 終了 or 非paletteユーザー → 終了

									// [JunpDB].[dbo].[tClient].[fCliEnd] = 1（終了）
									// [JunpDB].[dbo].[tClient].[fCliUpdate] = 現在
									// [JunpDB].[dbo].[tClient].[fCliUpdateMan] = プログラム名
									string sqlString = string.Format(@"UPDATE tClient SET fCliEnd = @1, fCliUpdate = @2, fCliUpdateMan = @3 WHERE fCliID = {0} AND fCliEnd = '0'", user.CustomerID);
									SqlParameter[] param1 = { new SqlParameter("@1", "1"),
															new SqlParameter("@2", DateTime.Now),
															new SqlParameter("@3", productName) };

									// 実行
									rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param1);
									if (rowCount <= -1)
									{
										throw new ApplicationException("UpdateSetPrevMonthFinishedUser() Error!");
									}
									// [JunpDB].[dbo].[tMikユーザー].[fusユーザー] = 0（非ユーザー）
									// [JunpDB].[dbo].[tMikユーザー].[fus前ｼｽﾃﾑ名称] = [JunpDB].[dbo].[tMikユーザー].[fusｼｽﾃﾑ名]
									// [JunpDB].[dbo].[tMikユーザー].[fusｼｽﾃﾑ名] = ''（空白）
									// [JunpDB].[dbo].[tMikユーザー].[fusメーカー名] = ''（空白）
									// [JunpDB].[dbo].[tMikユーザー].[fus更新日] = 現在
									// [JunpDB].[dbo].[tMikユーザー].[fus更新者] = プログラム名
									sqlString = string.Format(@"UPDATE tMikユーザー SET fusユーザー = @1, fus前ｼｽﾃﾑ名称 = @2, fusｼｽﾃﾑ名 = @3,  fusメーカー名 = @4,  fus更新日 = @5, fus更新者 = @6 WHERE 顧客No = {0}", user.CustomerID);
									SqlParameter[] param2 = { new SqlParameter("@1", "0"),
															new SqlParameter("@2", user.SystemCode),
															new SqlParameter("@3", DBNull.Value),
															new SqlParameter("@4", DBNull.Value),
															new SqlParameter("@5", DateTime.Now),
															new SqlParameter("@6", productName) };

									// 実行
									rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param1);
									if (rowCount <= -1)
									{
										throw new ApplicationException("UpdateSetPrevMonthFinishedUser() Error!");
									}
								}
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


		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		///// <summary>
		///// 課金対象外フラグの更新
		///// [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]
		///// </summary>
		///// <param name="serviceList">顧客No,サービスコード</param>
		///// <param name="sqlsv2">CT環境かどうか？</param>
		///// <returns>影響行数</returns>
		//public static int UpdateSetPauseEndStatus(List<Tuple<int, int>> serviceList, bool sqlsv2)
		//{
		//	int rowCount = -1;
		//	using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieWebConnectionString(sqlsv2)))
		//	{
		//		try
		//		{
		//			// 接続
		//			con.Open();

		//			// トランザクション開始
		//			using (SqlTransaction tran = con.BeginTransaction())
		//			{
		//				try
		//				{
		//					SqlParameter[] param = { new SqlParameter("@1", "1"),
		//											new SqlParameter("@2", DateTime.Now),
		//											new SqlParameter("@3", "終了ユーザー管理") };
		//					foreach (Tuple<int, int> sv in serviceList)
		//					{
		//						string sqlString = string.Format(@"UPDATE T_CUSSTOMER_USE_INFOMATION SET PAUSE_END_STATUS = @1, UPDATE_DATE = @2, UPDATE_PERSON = @3 WHERE CUSTOMER_ID = {0} AND SERVICE_ID = {1} AND PAUSE_END_STATUS = '0'", sv.Item1, sv.Item2);

		//						// 実行
		//						rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
		//						if (rowCount <= -1)
		//						{
		//							throw new ApplicationException("UpdatePauseEndStatus() Error!");
		//						}
		//					}
		//					// コミット
		//					tran.Commit();
		//				}
		//				catch
		//				{
		//					// ロールバック
		//					tran.Rollback();
		//					throw;
		//				}
		//			}
		//		}
		//		catch
		//		{
		//			throw;
		//		}
		//		finally
		//		{
		//			if (null != con)
		//			{
		//				// 切断
		//				con.Close();
		//			}
		//		}
		//	}
		//	return rowCount;
		//}
	}
}
