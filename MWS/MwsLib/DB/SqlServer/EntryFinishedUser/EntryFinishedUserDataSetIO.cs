using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.BaseFactory.EntryFinishedUser;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Collections;

namespace MwsLib.DB.SqlServer.EntryFinishedUser
{
	public static class EntryFinishedUserDataSetIO
	{
		/// <summary>
		/// 顧客情報の追加
		/// </summary>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <param name="data">顧客情報</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoEntryFinishedUserData(bool sqlsv2, EntryFinishedUserData data)
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
							string sqlString = @"INSERT INTO tMic終了ユーザーリスト VALUES (@1, @2, @3, @4, @5, @6, @7)";
							SqlParameter[] param = { new SqlParameter("@1", data.TokuisakiNo),
													new SqlParameter("@2", data.FinishedYearMonth.Value.ToString()),
													new SqlParameter("@3", data.AcceptDate.Value.ToString()),
													new SqlParameter("@4", data.FinishedReason),
													new SqlParameter("@5", data.Replace),
													new SqlParameter("@6", data.Reason),
													new SqlParameter("@7", DateTime.Now) };
							// 実行
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
							if (rowCount <= -1)
							{
								throw new ApplicationException("");
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
		/// 顧客情報の更新
		/// </summary>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <param name="data">顧客情報</param>
		/// <returns>影響行数</returns>
		public static int UpdateEntryFinishedUserData(bool sqlsv2, EntryFinishedUserData data)
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
							string sqlString = string.Format(@"UPDATE tMic終了ユーザーリスト SET 得意先No = @1, 終了月 = @2, 終了届受領日 = @3, 終了事由 = @4, リプレース = @5, 理由 = @6, 更新日時 = @7 WHERE 得意先No = {0}", data.TokuisakiNo);
							SqlParameter[] param = { new SqlParameter("@1", data.TokuisakiNo),
													new SqlParameter("@2", data.FinishedYearMonth.Value.ToString()),
													new SqlParameter("@3", data.AcceptDate.Value.ToString()),
													new SqlParameter("@4", data.FinishedReason),
													new SqlParameter("@5", data.Replace),
													new SqlParameter("@6", data.Reason),
													new SqlParameter("@7", DateTime.Now) };
							// 実行
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
							if (rowCount <= -1)
							{
								throw new ApplicationException("");
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
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <param name="data">顧客情報</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoMemo(bool sqlsv2, EntryFinishedUserData data)
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
							if (0 < data.Replace.Length)
							{
								param4 = string.Format("【使用終了予定】\r\nユーザー登録情報変更届（終了）受理\r\n【終了事由】\r\n{0}({1})\r\n終了月:{2}\r\n\r\n理由:{3}", data.FinishedReason, data.Replace, data.FinishedYearMonth.ToString(), data.Reason);
							}
							else
							{
								param4 = string.Format("【使用終了予定】\r\nユーザー登録情報変更届（終了）受理\r\n【終了事由】\r\n{0}\r\n終了月:{1}\r\n\r\n理由:{2}", data.FinishedReason, data.FinishedYearMonth.ToString(), data.Reason);
							}
							string sqlString = @"INSERT INTO tMemo VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18)";
							SqlParameter[] param = { new SqlParameter("@1", data.CostomerID),
													new SqlParameter("@2", "tClient"),
													new SqlParameter("@3", string.Format("{0} 営業管理部", DateTime.Now.ToString())),
													new SqlParameter("@4", param4),
													new SqlParameter("@5", DateTime.Now),
													new SqlParameter("@6", "営業管理部"),
													new SqlParameter("@7", null),
													new SqlParameter("@8", null),
													new SqlParameter("@9", null),
													new SqlParameter("@10", null),
													new SqlParameter("@11", 0),
													new SqlParameter("@12", 0),
													new SqlParameter("@13", 0),
													new SqlParameter("@14", 0),
													new SqlParameter("@15", 0),
													new SqlParameter("@16", 0),
													new SqlParameter("@17", null),
													new SqlParameter("@18", null) };
							// 実行
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
							if (rowCount <= -1)
							{
								throw new ApplicationException("");
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
