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
using MwsLib.BaseFactory.Junp.View;

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
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable SelectJunpDatabase(string tableName, string whereStr, string orderStr, bool ct)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(ct)))
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
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable SelectJunpDatabase(string strSQL, bool ct)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(ct)))
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
		///// <param name="ct">CT環境</param>
		///// <returns>レコード数</returns>
		//public static DataTable GetRecordCountJunpDatabase(string strSQL, bool ct)
		//{
		//	DataTable result = null;
		//	using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(ct)))
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
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoJunpDatabase(string sqlStr, SqlParameter[] param, bool ct)
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
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSetJunpDatabase(string sqlStr, SqlParameter[] param, bool ct)
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
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int DeleteJunpDatabase(string sqlStr, bool ct)
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
		// フィールド関連
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// [JunpDB].[dbo].[tMik基本情報]から顧客Noに対する得意先コードを取得
		/// </summary>
		/// <param name="CustomerNo">顧客No</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>得意先コード</returns>
		public static string GetTokuisakiCode(int CustomerNo, bool ct)
		{
			string sql = string.Format("SELECT [fkj得意先情報] FROM {0} WHERE [fkjCliMicID] = {1}", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik基本情報], CustomerNo);
			DataTable table = SelectJunpDatabase(sql, ct);
			if (null != table && 1 == table.Rows.Count)
			{
				return table.Rows[0]["fkj得意先情報"].ToString().Trim();
			}
			return string.Empty;
		}


		////////////////////////////////////////////////////////////////
		// テーブル関連
		////////////////////////////////////////////////////////////////

		//////////////////////////////
		// SELECT

		/// <summary>
		/// [JunpDB].[dbo].[tMikコードマスタ]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>tMikコードマスタ</returns>
		public static List<tMikコードマスタ> Select_tMikコードマスタ(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectJunpDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikコードマスタ], whereStr, orderStr, ct);
			return tMikコードマスタ.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMik保守契約]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>tMik保守契約</returns>
		public static List<tMik保守契約> Select_tMik保守契約(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectJunpDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik保守契約], whereStr, orderStr, ct);
			return tMik保守契約.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMih支店情報]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>tMikコードマスタ</returns>
		public static List<tMih支店情報> Select_tMih支店情報(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectJunpDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報], whereStr, orderStr, ct);
			return tMih支店情報.DataTableToList(table);
		}

		//////////////////////////////
		// UPDATE SET

		/// <summary>
		/// [JunpDB].[dbo].[tMic終了ユーザーリスト]の更新
		/// </summary>
		/// <param name="data">tMic終了ユーザーリスト</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_tMic終了ユーザーリスト(tMic終了ユーザーリスト data, bool ct)
		{
			return UpdateSetJunpDatabase(data.UpdateSetSqlString, data.GetUpdateSetParameters(), ct);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tClient]の更新
		/// </summary>
		/// <param name="customerID">顧客No</param>
		/// <param name="endFlag">終了フラグ</param>
		/// <param name="updateName">fCliUpdateMan</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_tClient(int customerID, bool endFlag, string updateName, bool ct)
		{
			string sqlString = string.Format(@"UPDATE {0} SET fCliEnd = @1, fCliUpdate = @2, fCliUpdateMan = @3 WHERE fCliID = {1}", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient], customerID);
			SqlParameter[] param = { new SqlParameter("@1", endFlag ? "1" : "0"),
									new SqlParameter("@2", DateTime.Now),
									new SqlParameter("@3", updateName ?? System.Data.SqlTypes.SqlString.Null) };
			return UpdateSetJunpDatabase(sqlString, param, ct);
		}

		//////////////////////////////
		// INSERT INTO

		/// <summary>
		/// [JunpDB].[dbo].[tMic終了ユーザーリスト]の新規追加
		/// </summary>
		/// <param name="data">tMic終了ユーザーリスト</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_tMic終了ユーザーリスト(tMic終了ユーザーリスト data, bool ct)
		{
			return InsertIntoJunpDatabase(tMic終了ユーザーリスト.InsertIntoSqlString, data.GetInsertIntoParameters(), ct);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMemo]の新規追加
		/// </summary>
		/// <param name="data">tMemo</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_tMemo(tMemo data, bool sqlsv2)
		{
			return InsertIntoJunpDatabase(tMemo.InsertIntoSqlString, data.GetInsertIntoParameters(), sqlsv2);
		}

		//////////////////////////////
		// DELETE

		/// <summary>
		/// [JunpDB].[dbo].[tMic終了ユーザーリスト]の削除
		/// </summary>
		/// <param name="user">tMic終了ユーザーリスト</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int Delete_tMic終了ユーザーリスト(tMic終了ユーザーリスト data, bool ct)
		{
			return DeleteJunpDatabase(data.DeleteSqlString, ct);
		}


		////////////////////////////////////////////////////////////////
		// ビュー関連
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// [JunpDB].[dbo].[vMicPCA消費税率]から指定日の消費税率の取得
		/// </summary>
		/// <param name="date">当日</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>消費税率</returns>
		public static int GetTaxRate(Date date, bool ct)
		{
			string sql = string.Format("SELECT CONVERT(int, t.tax_rate2) as 消費税率 FROM {0} as t INNER JOIN (SELECT MAX(r.tax_ymd) as ymd FROM {0} as r WHERE r.tax_ymd <= {1}) as s ON t.tax_ymd = s.ymd", JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA消費税率], date.ToIntYMD());
			DataTable table = SelectJunpDatabase(sql, ct);
			if (null != table && 1 == table.Rows.Count)
			{
				return DataBaseValue.ConvObjectToInt(table.Rows[0]["消費税率"]);
			}
			return 0;
		}
	}
}
