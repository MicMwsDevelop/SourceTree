//
// JunpDatabaseAccess.cs
//
// JunpDB データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using MwsLib.BaseFactory.Junp.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MwsLib.Common;

namespace MwsLib.DB.SqlServer.Junp
{
	public static class JunpDatabaseAccess
	{
		/// <summary>
		/// JunpDB レコードの取得
		/// </summary>
		/// <param name="tableName">テーブル/ビュー名</param>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable SelectJunpDatabase(string tableName, string whereStr, string orderStr, bool sqlsv2)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT * FROM {0}", tableName);
					if (0 < whereStr.Length)
					{
						strSQL += " WHERE " + whereStr;
					}
					if (0 < orderStr.Length)
					{
						strSQL += " ORDER BY " + orderStr;
					}
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
			return result;
		}

		/// <summary>
		/// JunpDB レコードの取得
		/// </summary>
		/// <param name="strSQL">SQL文</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable SelectJunpDatabase(string strSQL, bool sqlsv2)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(sqlsv2)))
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
			return result;
		}

		///// <summary>
		///// JunpDB レコード数の取得
		///// </summary>
		///// <param name="strSQL">SQL文</param>
		///// <param name="sqlsv2">CT環境</param>
		///// <returns>レコード数</returns>
		//public static DataTable GetRecordCountJunpDatabase(string strSQL, bool sqlsv2)
		//{
		//	DataTable result = null;
		//	using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(sqlsv2)))
		//	{
		//		try
		//		{
		//			// 接続
		//			con.Open();

		//			using (SqlCommand cmd = new SqlCommand(strSQL, con))
		//			{
		//				using (SqlDataAdapter da = new SqlDataAdapter(cmd))
		//				{
		//					result = new DataTable();
		//					da.Fill(result);
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
		//	return result;
		//}

		/// <summary>
		/// [JunpDB] レコードの新規追加
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="param">パラメータ</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoJunpDatabase(string sqlStr, SqlParameter[] param, bool sqlsv2)
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
							// 実行
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlStr, param);
							if (rowCount <= -1)
							{
								throw new ApplicationException("InsertIntoJunpDatabase() Error!");
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
		/// [JunpDB] レコードの更新
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="param">パラメータ</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSetJunpDatabase(string sqlStr, SqlParameter[] param, bool sqlsv2)
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
							// 実行
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlStr, param);
							if (rowCount <= -1)
							{
								throw new ApplicationException("UpdateSetJunpDatabase() Error!");
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
		/// [JunpDB] レコードの削除
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int DeleteJunpDatabase(string sqlStr, bool sqlsv2)
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
							// 実行
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlStr);
							if (rowCount <= -1)
							{
								throw new ApplicationException("DeleteJunpDatabase() Error!");
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


		////////////////////////////////////////////////////////////////
		// テーブル関連
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// [JunpDB].[dbo].[tMikコードマスタ]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>tMikコードマスタ</returns>
		public static List<tMikコードマスタ> Select_tMikコードマスタ(string whereStr, string orderStr, bool sqlsv2)
		{
			DataTable table = SelectJunpDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikコードマスタ], whereStr, orderStr, sqlsv2);
			return tMikコードマスタ.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMic終了ユーザーリスト]の新規追加
		/// </summary>
		/// <param name="user">tMic終了ユーザーリスト</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_tMic終了ユーザーリスト(tMic終了ユーザーリスト data, bool sqlsv2)
		{
			return InsertIntoJunpDatabase(tMic終了ユーザーリスト.InsertIntoSqlString, data.GetInsertIntoParameters(), sqlsv2);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMic終了ユーザーリスト]の更新
		/// </summary>
		/// <param name="user">tMic終了ユーザーリスト</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_tMic終了ユーザーリスト(tMic終了ユーザーリスト data, bool sqlsv2)
		{
			return UpdateSetJunpDatabase(data.UpdateSetSqlString, data.GetUpdateSetParameters(), sqlsv2);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMic終了ユーザーリスト]の削除
		/// </summary>
		/// <param name="user">tMic終了ユーザーリスト</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int Delete_tMic終了ユーザーリスト(tMic終了ユーザーリスト data, bool sqlsv2)
		{
			return DeleteJunpDatabase(data.DeleteSqlString, sqlsv2);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMic終了ユーザーリスト]の新規追加
		/// </summary>
		/// <param name="user">tMic終了ユーザーリスト</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_tMemo(tMemo data, bool sqlsv2)
		{
			return InsertIntoJunpDatabase(tMemo.InsertIntoSqlString, data.GetInsertIntoParameters(), sqlsv2);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMik保守契約]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>tMik保守契約</returns>
		public static List<tMik保守契約> Select_tMik保守契約(string whereStr, string orderStr, bool sqlsv2)
		{
			DataTable table = SelectJunpDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik保守契約], whereStr, orderStr, sqlsv2);
			return tMik保守契約.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tClient]の更新
		/// </summary>
		/// <param name="customerID">顧客No</param>
		/// <param name="endFlag">終了フラグ</param>
		/// <param name="updateName">fCliUpdateMan</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_tClient(int customerID, bool endFlag, string updateName, bool sqlsv2)
		{
			string sqlString = string.Format(@"UPDATE {0} SET fCliEnd = @1, fCliUpdate = @2, fCliUpdateMan = @3 WHERE fCliID = {1}", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient], customerID);
			SqlParameter[] param = { new SqlParameter("@1", endFlag ? "1" : "0"),
									new SqlParameter("@2", DateTime.Now),
									new SqlParameter("@3", updateName ?? System.Data.SqlTypes.SqlString.Null) };
			return UpdateSetJunpDatabase(sqlString, param, sqlsv2);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMih支店情報]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>tMikコードマスタ</returns>
		public static List<tMih支店情報> Select_tMih支店情報(string whereStr, string orderStr, bool sqlsv2)
		{
			DataTable table = SelectJunpDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報], whereStr, orderStr, sqlsv2);
			return tMih支店情報.DataTableToList(table);
		}



		////////////////////////////////////////////////////////////////
		// ビュー関連
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// [JunpDB].[dbo].[vMicPCA消費税率]から指定日の消費税率の取得
		/// </summary>
		/// <param name="date">当日</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>消費税率</returns>
		public static int GetTaxRate(Date date, bool sqlsv2)
		{
			string sql = string.Format("SELECT CONVERT(int, t.tax_rate2) as 消費税率 FROM {0} as t INNER JOIN (SELECT MAX(r.tax_ymd) as ymd FROM {0} as r WHERE r.tax_ymd <= {1}) as s ON t.tax_ymd = s.ymd", JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA消費税率], date.ToIntYMD());
			DataTable table = SelectJunpDatabase(sql, sqlsv2);
			if (null != table && 1 == table.Rows.Count)
			{
				return DataBaseValue.ConvObjectToInt(table.Rows[0]["消費税率"]);
			}
			return 0;
		}
	}
}
