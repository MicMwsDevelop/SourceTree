//
// CouplerDatabaseAccess.cs
//
// CouplerDB データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/10/18 勝呂)
// 
using MwsLib.BaseFactory.Coupler.Table;
using MwsLib.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.Coupler
{
	public static class CouplerDatabaseAccess
	{
		/// <summary>
		/// SQLコマンドの実行
		/// </summary>
		/// <param name="con">SqlConnection</param>
		/// <param name="tran">SqlTransaction</param>
		/// <param name="table">DataTable</param>
		/// <param name="sqlString">クエリ</param>
		/// <returns>実行結果</returns>
		private static int SqlExecuteCommand(SqlConnection con, string sqlString, SqlParameter[] param = null)
		{
			int result = -1;
			{
				using (SqlCommand cmd = new SqlCommand(sqlString, con))
				{
					cmd.Parameters.Clear();
					if (null != param)
					{
						cmd.Parameters.AddRange(param);
					}
					try
					{
						//実行
						result = cmd.ExecuteNonQuery();
					}
					catch
					{
						throw;
					}
				}
			}
			return result;
		}

		/// <summary>
		/// [Coupler] レコードの取得
		/// </summary>
		/// <param name="dbConnect">DB接続情報</param>
		/// <param name="tableName">テーブル/ビュー名</param>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <returns>レコード数</returns>
		public static DataTable SelectCouplerDatabase(DatabaseConnect dbConnect, string tableName, string whereStr, string orderStr)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateConnectionString(dbConnect)))
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

		///// <summary>
		///// [Coupler] レコード数の取得
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
		/// [Coupler] レコードの新規追加
		/// </summary>
		/// <param name="dbConnect">DB接続情報</param>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="param">パラメータ</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoCouplerDatabase(DatabaseConnect dbConnect, string sqlStr, SqlParameter[] param)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateConnectionString(dbConnect)))
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
							//rowCount = SqlExecuteCommand(con, sqlStr, param);
							if (rowCount <= -1)
							{
								throw new ApplicationException("InsertIntoCouplerDatabase() Error!");
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
		/// [Coupler] レコードの更新
		/// </summary>
		/// <param name="dbConnect">DB接続情報</param>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="param">パラメータ</param>
		/// <returns>影響行数</returns>
		public static int UpdateSetCouplerDatabase(DatabaseConnect dbConnect, string sqlStr, SqlParameter[] param)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateConnectionString(dbConnect)))
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
							//rowCount = SqlExecuteCommand(con, sqlStr, param);
							if (rowCount <= -1)
							{
								throw new ApplicationException("UpdateSetCouplerDatabase() Error!");
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

		///// <summary>
		///// [Coupler] レコードの削除
		///// </summary>
		///// <param name="dbConnect">DB接続情報</param>
		///// <param name="sqlStr">SQL文</param>
		///// <returns>影響行数</returns>
		//public static int DeleteCouplerDatabase(DatabaseConnect dbConnect, string sqlStr)
		//{
		//	int rowCount = -1;
		//	using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateConnectionString(dbConnect)))
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
		//					// 実行
		//					rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlStr);
		//					if (rowCount <= -1)
		//					{
		//						throw new ApplicationException("DeleteCouplerDatabase() Error!");
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


		////////////////////////////////////////////////////////////////
		// テーブル関連
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// [Coupler].[dbo].[SERVICE]の取得
		/// </summary>
		/// <param name="dbConnect">DB接続情報</param>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <returns>サービス利用情報</returns>
		public static List<T_COUPLER_SERVICE> Select_T_COUPLER_SERVICE(DatabaseConnect dbConnect, string whereStr = "", string orderStr = "")
		{
			DataTable table = SelectCouplerDatabase(dbConnect, CouplerDatabaseDefine.TableName[CouplerDatabaseDefine.TableType.T_COUPLER_SERVICE], whereStr, orderStr);
			return T_COUPLER_SERVICE.DataTableToList(table);
		}

		/// <summary>
		/// [Coupler].[dbo].[SERVICE]の新規追加
		/// </summary>
		/// <param name="dbConnect">DB接続情報</param>
		/// <param name="user">tMic終了ユーザーリスト</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_T_COUPLER_SERVICE(DatabaseConnect dbConnect, T_COUPLER_SERVICE data)
		{
			return InsertIntoCouplerDatabase(dbConnect, T_COUPLER_SERVICE.InsertIntoSqlString, data.GetInsertIntoParameters());
		}

		/// <summary>
		/// [Coupler].[dbo].[SERVICE]の更新
		/// </summary>
		/// <param name="dbConnect">DB接続情報</param>
		/// <param name="user">tMic終了ユーザーリスト</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_T_COUPLER_SERVICE(DatabaseConnect dbConnect, T_COUPLER_SERVICE data)
		{
			return UpdateSetCouplerDatabase(dbConnect, data.UpdateSetSqlString, data.GetUpdateSetParameters());
		}

		///// <summary>
		///// [Coupler].[dbo].[SERVICE]の削除
		///// </summary>
		///// <param name="dbConnect">DB接続情報</param>
		///// <param name="user">tMic終了ユーザーリスト</param>
		///// <returns>影響行数</returns>
		//public static int Delete_T_COUPLER_SERVICE(DatabaseConnect dbConnect, T_COUPLER_SERVICE data)
		//{
		//	return DeleteCouplerDatabase(dbConnect, data.DeleteSqlString);
		//}

		/// <summary>
		/// [Coupler].[dbo].[PRODUCTUSER]の取得
		/// </summary>
		/// <param name="dbConnect">DB接続情報</param>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <returns>製品顧客管理情報</returns>
		public static List<T_COUPLER_PRODUCTUSER> Select_T_COUPLER_PRODUCTUSER(DatabaseConnect dbConnect, string whereStr = "", string orderStr = "")
		{
			DataTable table = SelectCouplerDatabase(dbConnect, CouplerDatabaseDefine.TableName[CouplerDatabaseDefine.TableType.T_COUPLER_PRODUCTUSER], whereStr, orderStr);
			return T_COUPLER_PRODUCTUSER.DataTableToList(table);
		}


		////////////////////////////////////////////////////////////////
		// ビュー関連
		////////////////////////////////////////////////////////////////

		///// <summary>
		///// [Coupler].[dbo].[vMicPCA消費税率]から指定日の消費税率の取得
		///// </summary>
		///// <param name="date">当日</param>
		///// <param name="sqlsv2">CT環境かどうか？</param>
		///// <returns>消費税率</returns>
		//public static int GetTaxRate(Date date, bool sqlsv2)
		//{
		//	string sql = string.Format("SELECT CONVERT(int, t.tax_rate2) as 消費税率 FROM {0} as t INNER JOIN (SELECT MAX(r.tax_ymd) as ymd FROM {0} as r WHERE r.tax_ymd <= {1}) as s ON t.tax_ymd = s.ymd", JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA消費税率], date.ToIntYMD());
		//	DataTable table = SelectJunpDatabase(sql, sqlsv2);
		//	if (null != table && 1 == table.Rows.Count)
		//	{
		//		return DataBaseValue.ConvObjectToInt(table.Rows[0]["消費税率"]);
		//	}
		//	return 0;
		//}
	}
}
