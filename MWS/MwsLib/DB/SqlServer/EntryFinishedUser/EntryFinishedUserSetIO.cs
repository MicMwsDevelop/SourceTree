using MwsLib.BaseFactory.EntryFinishedUser;
using MwsLib.Common;
using System;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.EntryFinishedUser
{
	public static class EntryFinishedUserSetIO
	{
		/// <summary>
		/// 終了ユーザー情報の追加
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
							SqlParameter[] param = { new SqlParameter("@1", entry.CostomerID),
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
	}
}
