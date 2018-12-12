//
// EntryFinishedUserSetIO.cs
//
// 終了ユーザー管理 データ格納クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/12/12 勝呂)
// 
using MwsLib.BaseFactory.EntryFinishedUser;
using MwsLib.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.EntryFinishedUser
{
	public static class EntryFinishedUserSetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 終了ユーザー情報の追加
		/// [JunpDB].[dbo].[tMic終了ユーザーリスト]
		/// </summary>
		/// <param name="entry">終了ユーザー情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoEntryFinishedUser(EntryFinishedUserData entry, bool sqlsv2)
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
							string sqlString = @"INSERT INTO tMic終了ユーザーリスト VALUES (@1, @2, @3, @4, @5, @6, @7, @8)";
							SqlParameter[] param = { new SqlParameter("@1", entry.TokuisakiNo),
													new SqlParameter("@2", entry.FinishedYearMonth.Value.ToString()),
													new SqlParameter("@3", entry.AcceptDate.Value.ToString()),
													new SqlParameter("@4", entry.FinishedReason),
													new SqlParameter("@5", entry.Replace),
													new SqlParameter("@6", entry.Reason),
													new SqlParameter("@7", DateTime.Now),
													new SqlParameter("@8", (entry.NonPaletteUser) ? "1" : "0") };
							// 実行
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
							if (rowCount <= -1)
							{
								throw new ApplicationException("InsertIntoEntryFinishedUser() Error!");
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
		/// 終了ユーザー情報の更新
		/// [JunpDB].[dbo].[tMic終了ユーザーリスト]
		/// </summary>
		/// <param name="entry">終了ユーザー情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateEntryFinishedUser(EntryFinishedUserData entry, bool sqlsv2)
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
							string sqlString = string.Format(@"UPDATE tMic終了ユーザーリスト SET 得意先No = @1, 終了月 = @2, 終了届受領日 = @3, 終了事由 = @4, リプレース = @5, 理由 = @6, 更新日時 = @7, 非paletteユーザー = @8 WHERE 得意先No = {0}", entry.TokuisakiNo);
							SqlParameter[] param = { new SqlParameter("@1", entry.TokuisakiNo),
													new SqlParameter("@2", entry.FinishedYearMonth.Value.ToString()),
													new SqlParameter("@3", entry.AcceptDate.Value.ToString()),
													new SqlParameter("@4", entry.FinishedReason),
													new SqlParameter("@5", entry.Replace),
													new SqlParameter("@6", entry.Reason),
													new SqlParameter("@7", DateTime.Now),
													new SqlParameter("@8", (entry.NonPaletteUser) ? "1" : "0"), };
							// 実行
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
							if (rowCount <= -1)
							{
								throw new ApplicationException("UpdateEntryFinishedUser() Error!");
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
		/// メモ情報の新規追加
		/// [JunpDB].[dbo].[tMemo]
		/// </summary>
		/// <param name="entry">終了ユーザー情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoMemo(EntryFinishedUserData entry, bool sqlsv2)
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
							string param4 = string.Empty;
							if (0 < entry.Replace.Length)
							{
								param4 = string.Format("【使用終了予定】\r\nユーザー登録情報変更届（終了）受理\r\n【終了事由】\r\n{0}({1})\r\n終了月:{2}\r\n\r\n理由:{3}", entry.FinishedReason, entry.Replace, entry.FinishedYearMonth.ToString(), entry.Reason);
							}
							else
							{
								param4 = string.Format("【使用終了予定】\r\nユーザー登録情報変更届（終了）受理\r\n【終了事由】\r\n{0}\r\n終了月:{1}\r\n\r\n理由:{2}", entry.FinishedReason, entry.FinishedYearMonth.ToString(), entry.Reason);
							}
							string sqlString = @"INSERT INTO tMemo VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18)";
							SqlParameter[] param = { new SqlParameter("@1", entry.CustomerID),
													new SqlParameter("@2", "tClient"),
													new SqlParameter("@3", string.Format("{0} {1:D2}:{2:D2} 営業管理部", new Date(DateTime.Now).ToString(), DateTime.Now.Hour, DateTime.Now.Minute)),
													new SqlParameter("@4", param4),
													new SqlParameter("@5", DateTime.Now),
													new SqlParameter("@6", "営業管理部"),
													new SqlParameter("@7", DBNull.Value),
													new SqlParameter("@8", DBNull.Value),
													new SqlParameter("@9", DBNull.Value),
													new SqlParameter("@10", DBNull.Value),
													new SqlParameter("@11", Convert.ToInt32(0)),
													new SqlParameter("@12", Convert.ToInt32(0)),
													new SqlParameter("@13", Convert.ToInt32(0)),
													new SqlParameter("@14", Convert.ToInt32(0)),
													new SqlParameter("@15", Convert.ToInt32(0)),
													new SqlParameter("@16", Convert.ToInt32(0)),
													new SqlParameter("@17", DBNull.Value),
													new SqlParameter("@18", DBNull.Value) };
							// 実行
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
							if (rowCount <= -1)
							{
								throw new ApplicationException("InsertIntoMemo() Error!");
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
		/// 終了ユーザーフラグの更新
		/// [JunpDB].[dbo].[tClient]
		/// </summary>
		/// <param name="userList">終了ユーザー情報リスト</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateClientEndFlag(List<EntryFinishedUserData> userList, bool sqlsv2)
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
							SqlParameter[] param = { new SqlParameter("@1", "1"),
													new SqlParameter("@2", DateTime.Now),
													new SqlParameter("@3", "終了ユーザー管理") };
							foreach (EntryFinishedUserData user in userList)
							{
								string sqlString = string.Format(@"UPDATE tClient SET fCliEnd = @1, fCliUpdate = @2, fCliUpdateMan = @3 WHERE fCliID = {0} AND fCliEnd = '0'", user.CustomerID);

								// 実行
								rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
								if (rowCount <= -1)
								{
									throw new ApplicationException("UpdateClientEndFlag() Error!");
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

		/// <summary>
		/// 課金対象外フラグの更新
		/// [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]
		/// </summary>
		/// <param name="serviceList">顧客No,サービスコード</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdatePauseEndStatus(List<Tuple<int, int>> serviceList, bool sqlsv2)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieWebConnectionString(sqlsv2)))
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
							SqlParameter[] param = { new SqlParameter("@1", "1"),
													new SqlParameter("@2", DateTime.Now),
													new SqlParameter("@3", "終了ユーザー管理") };
							foreach (Tuple<int, int> sv in serviceList)
							{
								string sqlString = string.Format(@"UPDATE T_CUSSTOMER_USE_INFOMATION SET PAUSE_END_STATUS = @1, UPDATE_DATE = @2, UPDATE_PERSON = @3 WHERE CUSTOMER_ID = {0} AND SERVICE_ID = {1} AND PAUSE_END_STATUS = '0'", sv.Item1, sv.Item2);

								// 実行
								rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
								if (rowCount <= -1)
								{
									throw new ApplicationException("UpdatePauseEndStatus() Error!");
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
	}
}
