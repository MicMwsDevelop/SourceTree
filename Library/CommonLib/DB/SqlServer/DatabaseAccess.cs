//
// DatabaseAccess.cs
// 
// SQL SERVER データベース接続基本情報
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.DB.SqlServer
{
	/// <summary>
	/// SQL SERVER DB接続基本情報
	/// </summary>
	public static class DatabaseAccess
    {
		/// <summary>
		/// SQLコマンドの実行
		/// </summary>
		/// <param name="strSQL">SQL文</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>実行結果</returns>
		public static int ExecuteNonQuery(string strSQL, string connectStr)
		{
			int result = -1;
			using (SqlConnection con = new SqlConnection(connectStr))
			{
				try
				{
					// 接続
					con.Open();

					result = DatabaseController.SqlExecuteNonQuery(con, strSQL);
				}
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("ExecuteNonQuery() Error!({0})", ex.Message));
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
			return result;
		}

		/// <summary>
		/// ストアドプロシージャの実行
		/// </summary>
		/// <param name="strSQL">SQL文</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <param name="param">パラメータ</param>
		/// <returns>実行結果</returns>
		public static int ExecuteStoredProcedure(string strSQL, string connectStr, SqlParameter[] param = null)
		{
			int result = -1;
			using (SqlConnection con = new SqlConnection(connectStr))
			{
				try
				{
					// 接続
					con.Open();

					result = DatabaseController.SqlExecuteStoredProcedure(con, strSQL, param);
				}
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("ExecuteStoredProcedure() Error!({0})", ex.Message));
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
			return result;
		}

		/// <summary>
		/// テーブル新規作成
		/// </summary>
		/// <param name="strSQL">SQL文</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>実行結果</returns>
		public static int CreateTable(string strSQL, string connectStr)
		{
			int result = 0;
			using (SqlConnection con = new SqlConnection(connectStr))
			{
				try
				{
					// 接続
					con.Open();

					result = DatabaseController.SqlExecuteNonQuery(con, strSQL);
				}
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("CreateTable() Error!({0})", ex.Message));
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
			return result;
		}

		/// <summary>
		/// テーブル削除
		/// </summary>
		/// <param name="strSQL">SQL文</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>実行結果</returns>
		public static int DropTable(string strSQL, string connectStr)
		{
			int result = 0;
			using (SqlConnection con = new SqlConnection(connectStr))
			{
				try
				{
					// 接続
					con.Open();

					result = DatabaseController.SqlExecuteNonQuery(con, strSQL);
				}
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("DropTable() Error!({0})", ex.Message));
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
			return result;
		}

		/// <summary>
		/// 単一の値の取得(SqlExecuteScalar)
		/// クエリが単一の値を返すときに使用。結果は最初の行の最初の列
		/// </summary>
		/// <param name="strSQL">SQL文</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>オブジェクト</returns>
		public static object ScalarDatabase(string strSQL, string connectStr)
		{
			object result = null;
			using (SqlConnection con = new SqlConnection(connectStr))
			{
				try
				{
					// 接続
					con.Open();

					result = DatabaseController.SqlExecuteScalar(con, strSQL);
				}
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("ScalarDatabase() Error!({0})", ex.Message));
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
			return result;
		}

		/// <summary>
		/// 単一の値の取得(SqlExecuteScalar)
		/// クエリが単一の値を返すときに使用。結果は最初の行の最初の列
		/// </summary>
		/// <param name="strSQL">SQL文</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>オブジェクト</returns>
		public static object ScalarDatabaseScopeIdentity(string strSQL, string connectStr, SqlParameter[] param)
		{
			object result = null;
			using (SqlConnection con = new SqlConnection(connectStr))
			{
				try
				{
					// 接続
					con.Open();

					result = DatabaseController.SqlExecuteScalar(con, strSQL);
				}
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("ScalarDatabase() Error!({0})", ex.Message));
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
			return result;
		}

		/// <summary>
		/// レコードの取得(SqlExcuteDataAdapter)
		/// </summary>
		/// <param name="strSQL">SQL文</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果セット</returns>
		public static DataTable SelectDatabase(string strSQL, string connectStr)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(connectStr))
			{
				try
				{
					// 接続
					con.Open();

					result = DatabaseController.SqlExcuteDataAdapter(con, strSQL);
				}
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("SelectDatabase() Error!({0})", ex.Message));
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
			return result;
		}

		/// <summary>
		/// レコードの取得(SqlExcuteDataAdapter)
		/// </summary>
		/// <param name="strSQL">SQL文</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <param name="timeOutSecond">タイムアウト(秒数)</param>
		/// <returns>結果セット</returns>
		public static DataTable SelectDatabaseTimeOut(string strSQL, string connectStr, int timeOutSecond)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(connectStr))
			{
				try
				{
					// 接続
					con.Open();

					result = DatabaseController.SqlExcuteDataAdapterTimeOut(con, strSQL, timeOutSecond);
				}
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("SelectDatabaseTimeOut() Error!({0})", ex.Message));
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
			return result;
		}

		/// <summary>
		/// レコードの取得
		/// </summary>
		/// <param name="tableName">テーブル/ビュー名</param>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果セット</returns>
		public static DataTable SelectDatabase(string tableName, string whereStr, string orderStr, string connectStr)
		{
			string strSQL = string.Format(@"SELECT * FROM {0}", tableName);
			if (0 < whereStr.Length)
			{
				strSQL += " WHERE " + whereStr;
			}
			if (0 < orderStr.Length)
			{
				strSQL += " ORDER BY " + orderStr;
			}
			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// レコードの新規追加
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="param">パラメータ</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoDatabase(string sqlStr, SqlParameter[] param, string connectStr)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(connectStr))
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
							// 実行
							rowCount = DatabaseController.SqlExecuteNonQueryTran(con, tran, sqlStr, param);
							if (rowCount <= -1)
							{
								throw new ApplicationException("InsertIntoDatabase() Error!");
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
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("InsertIntoDatabase() Error!({0})", ex.Message));
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
		/// レコードの新規追加 - SCOPE_IDENTITY()
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="param">パラメータ</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>SCOPE_IDENTITY()</returns>
		public static object InsertIntoDatabaseScopeIdentity(string sqlStr, SqlParameter[] param, string connectStr)
		{
			object iNewRowIdentity = null;
			using (SqlConnection con = new SqlConnection(connectStr))
			{
				try
				{
					// 接続
					con.Open();

					// 実行
					iNewRowIdentity = DatabaseController.SqlExecuteScalar(con, sqlStr, param);
					if (null == iNewRowIdentity)
					{
						throw new ApplicationException("InsertIntoDatabaseScopeIdentity() Error!");
					}
				}
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("InsertIntoDatabaseScopeIdentity() Error!({0})", ex.Message));
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
			return iNewRowIdentity;
		}

		/// <summary>
		/// 複数レコードの新規追加
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="paramList">パラメータ</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoListDatabase(string sqlStr, IEnumerable<SqlParameter[]> paramList, string connectStr)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(connectStr))
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
							// 実行
							foreach (SqlParameter[] param in paramList)
							{
								rowCount = DatabaseController.SqlExecuteNonQueryTran(con, tran, sqlStr, param);
								if (rowCount <= -1)
								{
									throw new ApplicationException("InsertIntoListDatabase() Error!");
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
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("InsertIntoListDatabase() Error!({0})", ex.Message));
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
		/// Bulk Insert
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <param name="tableName">テーブル名</param>
		/// <param name="dt"></param>
		public static void BulkInsert(string connectStr, string tableName, DataTable dt)
		{
			using (SqlConnection con = new SqlConnection(connectStr))
			{
				SqlBulkCopy bc = new SqlBulkCopy(con);
				bc.DestinationTableName = tableName;
				try
				{
					con.Open();
					bc.WriteToServer(dt);
				}
				finally
				{
					con.Close();
				}
			}
		}

		/// <summary>
		/// レコードの更新
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="param">パラメータ</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int UpdateSetDatabase(string sqlStr, SqlParameter[] param, string connectStr)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(connectStr))
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
							// 実行
							rowCount = DatabaseController.SqlExecuteNonQueryTran(con, tran, sqlStr, param);
							if (rowCount <= -1)
							{
								throw new ApplicationException("UpdateSetDatabase() Error!");
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
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("UpdateSetDatabase() Error!({0})", ex.Message));
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
		/// レコードの削除
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int DeleteDatabase(string sqlStr, string connectStr)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(connectStr))
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
							// 実行
							rowCount = DatabaseController.SqlExecuteNonQueryTran(con, tran, sqlStr);
							if (rowCount <= -1)
							{
								throw new ApplicationException("DeleteDatabase() Error!");
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
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("DeleteDatabase() Error!({0})", ex.Message));
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