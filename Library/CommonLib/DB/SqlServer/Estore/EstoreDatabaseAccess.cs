//
// EstoreDatabaseAccess.cs
//
// estoreDB データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/10/18 勝呂)
// 
using CommonLib.BaseFactory.Estore.Table;
using CommonLib.BaseFactory.Estore.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.DB.SqlServer.Estore
{
	public static class EstoreDatabaseAccess
	{
		////////////////////////////////////////////////////////////////
		// テーブル関連
		////////////////////////////////////////////////////////////////

		//////////////////////////////
		// SELECT

		/// <summary>
		/// estoreログの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<tMICestore_log> Select_tMicEstoreLog(string connectStr)
		{
			string sqlStr = string.Format("SELECT * FROM {0} ORDER BY ID", EstoreDatabaseDefine.TableName[EstoreDatabaseDefine.TableType.tMICestore_log]);
			DataTable table = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return tMICestore_log.DataTableToList(table);
		}


		//////////////////////////////
		// INSERT INTO

		/// <summary>
		/// tMICestore_logリストの新規追加
		/// </summary>
		/// <param name="list"></param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_tMICestore_log(List<tMICestore_log> list, string connectStr)
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
							foreach (tMICestore_log log in list)
							{
								// 実行
								rowCount = DatabaseController.SqlExecuteNonQueryTran(con, tran, tMICestore_log.InsertIntoSqlString, log.GetInsertIntoParameters());
								if (rowCount <= -1)
								{
									throw new ApplicationException("InsertInto_tMICestore_log() Error!");
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


		////////////////////////////////////////////////////////////////
		// ビュー関連
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// vMic受注最大番号の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static int Select_vMic受注最大番号(string connectStr)
		{
			string sqlStr = string.Format("SELECT * FROM {0}", EstoreDatabaseDefine.ViewName[EstoreDatabaseDefine.ViewType.vMic受注最大番号]);
			DataTable table = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			if (0 < table.Rows.Count)
			{
				DataRow row = table.Rows[0];
				return DataBaseValue.ConvObjectToInt(row["j_max"]);
			}
			return 0;
		}

		/// <summary>
		/// vMic部門コードの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<vMic部門コード> Select_vMic部門コード(string connectStr)
		{
			string sqlStr = string.Format("SELECT * FROM {0} ORDER BY 顧客Ｎｏ", EstoreDatabaseDefine.ViewName[EstoreDatabaseDefine.ViewType.vMic部門コード]);
			DataTable table = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return vMic部門コード.DataTableToList(table);
		}

		/// <summary>
		/// vMic顧客マスタの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<vMic顧客マスタ> Select_vMic顧客マスタ(string connectStr)
		{
			string sqlStr = string.Format("SELECT * FROM {0} ORDER BY 顧客No", EstoreDatabaseDefine.ViewName[EstoreDatabaseDefine.ViewType.vMic顧客マスタ]);
			DataTable table = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return vMic顧客マスタ.DataTableToList(table);
		}

		/// <summary>
		/// vMic商品マスタの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<vMic商品マスタ> Select_vMic商品マスタ(string connectStr)
		{
			string sqlStr = string.Format("SELECT * FROM {0} ORDER BY ID", EstoreDatabaseDefine.ViewName[EstoreDatabaseDefine.ViewType.vMic商品マスタ]);
			DataTable table = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return vMic商品マスタ.DataTableToList(table);
		}
	}
}
