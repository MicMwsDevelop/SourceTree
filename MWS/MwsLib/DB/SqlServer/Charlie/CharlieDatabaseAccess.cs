//
// CharlieDatabaseAccess.cs
//
// CharlieDB データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using MwsLib.BaseFactory.Charlie.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.Charlie
{
	public static class CharlieDatabaseAccess
	{
		/// <summary>
		/// [charlieDB] レコードの取得
		/// </summary>
		/// <param name="selectSql">SQL文</param>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable SelectDirectCharlieDatabase(string selectSql, string whereStr, string orderStr, bool ct)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = selectSql;
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
		/// [charlieDB] レコードの取得
		/// </summary>
		/// <param name="tableName">テーブル/ビュー名</param>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable SelectCharlieDatabase(string tableName, string whereStr, string orderStr, bool ct)
		{
			return SelectDirectCharlieDatabase(string.Format(@"SELECT * FROM {0}", tableName), whereStr, orderStr, ct);
		}

		/// <summary>
		/// [CharlieDB] レコードの新規追加
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="param">パラメータ</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoCharlieDatabase(string sqlStr, SqlParameter[] param, bool ct)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieWebConnectionString(ct)))
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
								throw new ApplicationException("InsertIntoCharlieDatabase() Error!");
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
		/// [CharlieDB] 複数レコードの新規追加
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="paramList">パラメータ</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoListCharlieDatabase(string sqlStr, IEnumerable<SqlParameter[]> paramList, bool ct)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieWebConnectionString(ct)))
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
								rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlStr, param);
								if (rowCount <= -1)
								{
									throw new ApplicationException("InsertIntoListCharlieDatabase() Error!");
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
		/// [CharlieDB] レコードの更新
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="param">パラメータ</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSetCharlieDatabase(string sqlStr, SqlParameter[] param, bool ct)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieWebConnectionString(ct)))
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
								throw new ApplicationException("UpdateSetCharlieDatabase() Error!");
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
		/// [CharlieDB] レコードの削除
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int DeleteCharlieDatabase(string sqlStr, bool ct)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieWebConnectionString(ct)))
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
								throw new ApplicationException("DeleteCharlieDatabase() Error!");
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
		/// サービスIDに対応するサービス名の取得
		/// </summary>
		/// <param name="serviceID">サービスID</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>サービス名</returns>
		public static string GetServiceName(int serviceID, bool ct)
		{
			DataTable table = SelectCharlieDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_SERVICE], string.Format("SERVICE_ID = {0}", serviceID), "", ct);
			if (null != table && 0 < table.Rows.Count)
			{
				return table.Rows[0]["SERVICE_NAME"].ToString().Trim();
			}
			return string.Empty;
		}


		////////////////////////////////////////////////////////////////
		// テーブル関連
		////////////////////////////////////////////////////////////////

		//////////////////////////////
		// SELECT

		/// <summary>
		/// [charlieDB].[dbo].[T_PRODUCT_CONTROL]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>製品管理情報リスト</returns>
		public static List<T_PRODUCT_CONTROL> Select_T_PRODUCT_CONTROL(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectCharlieDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_PRODUCT_CONTROL], whereStr, orderStr, ct);
			return T_PRODUCT_CONTROL.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>顧客利用情報リスト</returns>
		public static List<T_CUSSTOMER_USE_INFOMATION> Select_T_CUSSTOMER_USE_INFOMATION(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectCharlieDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION], whereStr, orderStr, ct);
			return T_CUSSTOMER_USE_INFOMATION.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_DEMO_USER]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>デモ用ID管理テーブルリスト</returns>
		public static List<T_DEMO_USER> Select_T_DEMO_USER(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectCharlieDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_DEMO_USER], whereStr, orderStr, ct);
			return T_DEMO_USER.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_LICENSE_PRODUCT_CONTRACT]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>ESETライセンス管理情報リスト</returns>
		public static List<T_LICENSE_PRODUCT_CONTRACT> Select_T_LICENSE_PRODUCT_CONTRACT(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectCharlieDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_LICENSE_PRODUCT_CONTRACT], whereStr, orderStr, ct);
			return T_LICENSE_PRODUCT_CONTRACT.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>PC安心サポート契約情報リスト</returns>
		public static List<T_USE_PCCSUPPORT> Select_T_USE_PCCSUPPORT(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectCharlieDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_PCCSUPPORT], whereStr, orderStr, ct);
			return T_USE_PCCSUPPORT.DataTableToList(table);
		}

		//////////////////////////////
		// UPDATE SET

		/// <summary>
		/// [charlieDB].[dbo].[T_PRODUCT_CONTROL]の更新
		/// </summary>
		/// <param name="data">製品管理情報</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_T_PRODUCT_CONTROL(T_PRODUCT_CONTROL data, bool ct)
		{
			return UpdateSetCharlieDatabase(data.UpdateSetSqlString, data.GetUpdateSetParameters(), ct);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]の更新
		/// </summary>
		/// <param name="data">顧客利用情報</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_T_CUSSTOMER_USE_INFOMATION(T_CUSSTOMER_USE_INFOMATION data, bool ct)
		{
			return UpdateSetCharlieDatabase(data.UpdateSetSqlString, data.GetUpdateSetParameters(), ct);
		}

		//////////////////////////////
		// INSERT INTO

		/// <summary>
		/// [charlieDB].[dbo].[T_PRODUCT_CONTROL]の新規追加
		/// </summary>
		/// <param name="data">製品管理情報</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_T_PRODUCT_CONTROL(T_PRODUCT_CONTROL data, bool ct)
		{
			return InsertIntoCharlieDatabase(T_PRODUCT_CONTROL.InsertIntoSqlString, data.GetInsertIntoParameters(), ct);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]の新規追加
		/// </summary>
		/// <param name="data">顧客利用情報</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_T_CUSSTOMER_USE_INFOMATION(T_CUSSTOMER_USE_INFOMATION data, bool ct)
		{
			return InsertIntoCharlieDatabase(T_CUSSTOMER_USE_INFOMATION.InsertIntoSqlString, data.GetInsertIntoParameters(), ct);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の新規追加
		/// </summary>
		/// <param name="data">PC安心サポート契約情報</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_T_USE_PCCSUPPORT(T_USE_PCCSUPPORT data, bool ct)
		{
			return InsertIntoCharlieDatabase(T_USE_PCCSUPPORT.InsertIntoSqlString, data.GetInsertIntoParameters(), ct);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の複数新規追加
		/// </summary>
		/// <param name="list">PC安心サポート契約情報リスト</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoList_T_USE_PCCSUPPORT(IEnumerable<T_USE_PCCSUPPORT> list, bool ct)
		{
			List<SqlParameter[]> paramList = new List<SqlParameter[]>();
			foreach (T_USE_PCCSUPPORT data in list)
			{
				paramList.Add(data.GetInsertIntoParameters());
			}
			return InsertIntoListCharlieDatabase(T_USE_PCCSUPPORT.InsertIntoSqlString, paramList, ct);
		}

		//////////////////////////////
		// DELETE


	}
}
