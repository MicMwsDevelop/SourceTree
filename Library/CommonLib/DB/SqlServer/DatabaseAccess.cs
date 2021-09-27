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
		/// レコードの取得
		/// </summary>
		/// <param name="strSQL">SQL文</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>レコード数</returns>
		public static DataTable SelectDatabase(string strSQL, string connectStr)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(connectStr))
			{
				try
				{
					// 接続
					con.Open();

					using (SqlCommand cmd = new SqlCommand(strSQL, con))
					{
						using (SqlDataAdapter da = new SqlDataAdapter(cmd))
						{
							result = new DataTable();
							da.Fill(result);
						}
					}
				}
				catch
				{
					throw new ApplicationException("SelectDatabase() Error!");
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
		/// <returns>レコード数</returns>
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
							rowCount = DatabaseController.SqlExecuteCommand(con, tran, sqlStr, param);
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
				catch
				{
					throw new ApplicationException("InsertIntoDatabase() Error!");
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
								rowCount = DatabaseController.SqlExecuteCommand(con, tran, sqlStr, param);
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
							rowCount = DatabaseController.SqlExecuteCommand(con, tran, sqlStr, param);
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
							rowCount = DatabaseController.SqlExecuteCommand(con, tran, sqlStr);
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