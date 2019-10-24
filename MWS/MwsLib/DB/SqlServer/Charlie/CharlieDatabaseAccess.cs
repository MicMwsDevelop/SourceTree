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
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		private static DataTable SelectDirectCharlieDatabase(string selectSql, string whereStr, string orderStr, bool sqlsv2)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(sqlsv2)))
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
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		private static DataTable SelectCharlieDatabase(string tableName, string whereStr, string orderStr, bool sqlsv2)
		{
			return SelectDirectCharlieDatabase(string.Format(@"SELECT * FROM {0}", tableName), whereStr, orderStr, sqlsv2);
		}

		/// <summary>
		/// [CharlieDB] レコードの新規追加
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="param">パラメータ</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		private static int InsertIntoCharlieDatabase(string sqlStr, SqlParameter[] param, bool sqlsv2)
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
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		private static int InsertIntoListCharlieDatabase(string sqlStr, IEnumerable<SqlParameter[]> paramList, bool sqlsv2)
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
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		private static int UpdateSetCharlieDatabase(string sqlStr, SqlParameter[] param, bool sqlsv2)
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
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		private static int DeleteCharlieDatabase(string sqlStr, bool sqlsv2)
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
		// テーブル関連
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// [charlieDB].[dbo].[T_PRODUCT_CONTROL]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>製品管理情報リスト</returns>
		public static List<T_PRODUCT_CONTROL> Select_T_PRODUCT_CONTROL(string whereStr, string orderStr, bool sqlsv2)
		{
			DataTable table = SelectCharlieDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_PRODUCT_CONTROL], whereStr, orderStr, sqlsv2);
			return T_PRODUCT_CONTROL.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_PRODUCT_CONTROL]の新規追加
		/// </summary>
		/// <param name="data">製品管理情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_T_PRODUCT_CONTROL(T_PRODUCT_CONTROL data, bool sqlsv2)
		{
			return InsertIntoCharlieDatabase(T_PRODUCT_CONTROL.InsertIntoSqlString, data.GetInsertIntoParameters(), sqlsv2);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_PRODUCT_CONTROL]の更新
		/// </summary>
		/// <param name="data">製品管理情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_T_PRODUCT_CONTROL(T_PRODUCT_CONTROL data, bool sqlsv2)
		{
			return UpdateSetCharlieDatabase(data.UpdateSetSqlString, data.GetUpdateSetParameters(), sqlsv2);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>顧客利用情報リスト</returns>
		public static List<T_CUSSTOMER_USE_INFOMATION> Select_T_CUSSTOMER_USE_INFOMATION(string whereStr, string orderStr, bool sqlsv2)
		{
			DataTable table = SelectCharlieDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION], whereStr, orderStr, sqlsv2);
			return T_CUSSTOMER_USE_INFOMATION.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]の新規追加
		/// </summary>
		/// <param name="data">顧客利用情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_T_CUSSTOMER_USE_INFOMATION(T_CUSSTOMER_USE_INFOMATION data, bool sqlsv2)
		{
			return InsertIntoCharlieDatabase(T_CUSSTOMER_USE_INFOMATION.InsertIntoSqlString, data.GetInsertIntoParameters(), sqlsv2);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]の更新
		/// </summary>
		/// <param name="data">顧客利用情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_T_CUSSTOMER_USE_INFOMATION(T_CUSSTOMER_USE_INFOMATION data, bool sqlsv2)
		{
			return UpdateSetCharlieDatabase(data.UpdateSetSqlString, data.GetUpdateSetParameters(), sqlsv2);
		}

		/// <summary>
		/// ナルコーム申込情報リストの取得
		/// </summary>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>ナルコーム申込情報リスト</returns>
		public static List<T_NARCOHM_APPLICATE_HEADER> Select_T_NARCOHM_APPLICATE_HEADER(bool sqlsv2)
		{
			DataTable tableHeader = SelectCharlieDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_NARCOHM_APPLICATE_HEADER], "", "ApplicateID ASC", sqlsv2);
			if (null != tableHeader)
			{
				if (0 < tableHeader.Rows.Count)
				{
					DataTable tableDetail = SelectCharlieDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_NARCOHM_APPLICATE_DETAIL], "", "ApplicateID ASC, SeqNo ASC", sqlsv2);
					if (0 < tableDetail.Rows.Count)
					{
						List<T_NARCOHM_APPLICATE_HEADER> headerList = T_NARCOHM_APPLICATE_HEADER.DataTableToList(tableHeader);
						List<T_NARCOHM_APPLICATE_DETAIL> detailList = T_NARCOHM_APPLICATE_DETAIL.DataTableToList(tableDetail);
						foreach (T_NARCOHM_APPLICATE_HEADER header in headerList)
						{
							header.DetailList = detailList.FindAll(p => header.ApplicateID == p.ApplicateID);
						}
						return headerList;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// ナルコーム申込情報の追加
		/// [Charlie].[dbo].[T_NARCOHM_APPLICATE_HEADER]
		/// [Charlie].[dbo].[T_NARCOHM_APPLICATE_DETAIL]
		/// </summary>
		/// <param name="data">ナルコーム申込情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_T_NARCOHM_APPLICATE_HEADER(T_NARCOHM_APPLICATE_HEADER data, bool sqlsv2)
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
							// 実行
							object iNewRowIdentity = DataBaseController.SqlExecuteScalar(con, tran, T_NARCOHM_APPLICATE_HEADER.InsertIntoSqlString, data.GetInsertIntoParameters());
							if (null == iNewRowIdentity)
							{
								throw new ApplicationException("InsertInto_T_NARCOHM_APPLICATE_HEADER() Error!");
							}
							// オートナンバーの取得
							data.ApplicateID = Convert.ToInt32(iNewRowIdentity);

							// ナルコーム製品申込詳細情報の追加
							int i = 0;
							foreach (T_NARCOHM_APPLICATE_DETAIL detail in data.DetailList)
							{
								detail.ApplicateID = data.ApplicateID;

								// 実行
								rowCount = DataBaseController.SqlExecuteCommand(con, tran, T_NARCOHM_APPLICATE_DETAIL.InsertIntoSqlString, detail.GetInsertIntoParameters(i));
								if (rowCount <= -1)
								{
									throw new ApplicationException("InsertInto_T_NARCOHM_APPLICATE_HEADER() Error!");
								}
								i++;
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
		/// ナルコーム製品申込情報の更新
		/// [Charlie].[dbo].[T_NARCOHM_APPLICATE_HEADER]
		/// [Charlie].[dbo].[T_NARCOHM_APPLICATE_DETAIL]
		/// </summary>
		/// <param name="data">ナルコーム製品申込ヘッダ情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_T_NARCOHM_APPLICATE_HEADER(T_NARCOHM_APPLICATE_HEADER data, bool sqlsv2)
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
							// ナルコーム製品申込情報の更新
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, data.UpdateSetSqlString, data.GetUpdateSetParameters());
							if (rowCount <= -1)
							{
								throw new ApplicationException("UpdateSet_T_NARCOHM_APPLICATE_HEADER() Error!");
							}
							// ナルコーム製品申込詳細情報の削除
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, T_NARCOHM_APPLICATE_DETAIL.DeleteSqlString(data.ApplicateID));
							if (rowCount <= -1)
							{
								throw new ApplicationException("UpdateSet_T_NARCOHM_APPLICATE_HEADER() Error!");
							}
							// ナルコーム製品申込詳細情報の追加
							int i = 0;
							foreach (T_NARCOHM_APPLICATE_DETAIL detail in data.DetailList)
							{
								rowCount = DataBaseController.SqlExecuteCommand(con, tran, T_NARCOHM_APPLICATE_DETAIL.InsertIntoSqlString, detail.GetInsertIntoParameters(i));
								if (rowCount <= -1)
								{
									throw new ApplicationException("UpdateSet_T_NARCOHM_APPLICATE_HEADER() Error!");
								}
								i++;
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
		/// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の新規追加
		/// </summary>
		/// <param name="data">PC安心サポート契約情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_T_USE_PCCSUPPORT(T_USE_PCCSUPPORT data, bool sqlsv2)
		{
			return InsertIntoCharlieDatabase(T_USE_PCCSUPPORT.InsertIntoSqlString, data.GetInsertIntoParameters(), sqlsv2);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の複数新規追加
		/// </summary>
		/// <param name="list">PC安心サポート契約情報リスト</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoList_T_USE_PCCSUPPORT(IEnumerable<T_USE_PCCSUPPORT> list, bool sqlsv2)
		{
			List<SqlParameter[]> paramList = new List<SqlParameter[]>();
			foreach (T_USE_PCCSUPPORT data in list)
			{
				paramList.Add(data.GetInsertIntoParameters());
			}
			return InsertIntoListCharlieDatabase(T_USE_PCCSUPPORT.InsertIntoSqlString, paramList, sqlsv2);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_DEMO_USER]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>デモ用ID管理テーブルリスト</returns>
		public static List<T_DEMO_USER> Select_T_DEMO_USER(string whereStr, string orderStr, bool sqlsv2)
		{
			DataTable table = SelectCharlieDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_DEMO_USER], whereStr, orderStr, sqlsv2);
			return T_DEMO_USER.DataTableToList(table);
		}
	}
}
