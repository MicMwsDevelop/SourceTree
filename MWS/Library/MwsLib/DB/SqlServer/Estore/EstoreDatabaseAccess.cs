using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MwsLib.BaseFactory.Estore.Table;
using MwsLib.BaseFactory.Estore.View;

namespace MwsLib.DB.SqlServer.Estore
{
	public static class EstoreDatabaseAccess
	{
		/// <summary>
		/// [estoreDB] レコードの取得
		/// </summary>
		/// <param name="strSQL">SQL文</param>
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable EstoreDatabaseDataAdpter(string strSQL, bool ct)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateEstoreWebConnectionString(ct)))
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
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("EstoreDatabaseDataAdpter() Error!({0})", ex.Message));
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
		/// [estoreDB] レコードの更新
		/// </summary>
		/// <param name="tableName">テーブル/ビュー名</param>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static int EstoreDatabaseExcuteCommand(string sqlStr, bool ct)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateEstoreWebConnectionString(ct)))
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
								throw new ApplicationException("EstoreDatabaseExcuteCommand() Error!");
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
		/// [estoreDB] レコードの更新
		/// </summary>
		/// <param name="sqlStr">SQL文</param>
		/// <param name="param">パラメータ</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSetEstoreDatabase(string sqlStr, SqlParameter[] param, bool ct)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateEstoreWebConnectionString(ct)))
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
								throw new ApplicationException("UpdateSetEstoreDatabase() Error!");
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

		//////////////////////////////
		// SELECT

		/// <summary>
		/// estoreログの取得
		/// </summary>
		/// <param name="ct">CT環境</param>
		/// <returns>結果</returns>
		public static List<tMICestore_log> Select_tMicEstoreLog(bool ct)
		{
			string sqlStr = string.Format("SELECT * FROM {0} ORDER BY ID", EstoreDatabaseDefine.TableName[EstoreDatabaseDefine.TableType.tMICestore_log]);
			DataTable table = EstoreDatabaseAccess.EstoreDatabaseDataAdpter(sqlStr, ct);
			return tMICestore_log.DataTableToList(table);
		}

		//////////////////////////////
		// INSERT INTO

		/// <summary>
		/// tMICestore_logリストの新規追加
		/// </summary>
		/// <param name="list"></param>
		/// <param name="ct">CT環境</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_tMICestore_log(List<tMICestore_log> list, bool ct)
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
							foreach (tMICestore_log log in list)
							{
								// 実行
								rowCount = DataBaseController.SqlExecuteCommand(con, tran, tMICestore_log.InsertIntoSqlString, log.GetInsertIntoParameters());
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
		/// <param name="ct">CT環境</param>
		/// <returns>結果</returns>
		public static int Select_vMic受注最大番号(bool ct)
		{
			string sqlStr = string.Format("SELECT * FROM {0}", EstoreDatabaseDefine.ViewName[EstoreDatabaseDefine.ViewType.vMic受注最大番号]);
			DataTable table = EstoreDatabaseAccess.EstoreDatabaseDataAdpter(sqlStr, ct);
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
		/// <param name="ct">CT環境</param>
		/// <returns>結果</returns>
		public static List<vMic部門コード> Select_vMic部門コード(bool ct)
		{
			string sqlStr = string.Format("SELECT * FROM {0} ORDER BY 顧客Ｎｏ", EstoreDatabaseDefine.ViewName[EstoreDatabaseDefine.ViewType.vMic部門コード]);
			DataTable table = EstoreDatabaseAccess.EstoreDatabaseDataAdpter(sqlStr, ct);
			return vMic部門コード.DataTableToList(table);
		}

		/// <summary>
		/// vMic顧客マスタの取得
		/// </summary>
		/// <param name="ct">CT環境</param>
		/// <returns>結果</returns>
		public static List<vMic顧客マスタ> Select_vMic顧客マスタ(bool ct)
		{
			string sqlStr = string.Format("SELECT * FROM {0} ORDER BY 顧客No", EstoreDatabaseDefine.ViewName[EstoreDatabaseDefine.ViewType.vMic顧客マスタ]);
			DataTable table = EstoreDatabaseAccess.EstoreDatabaseDataAdpter(sqlStr, ct);
			return vMic顧客マスタ.DataTableToList(table);
		}

		/// <summary>
		/// vMic商品マスタの取得
		/// </summary>
		/// <param name="ct">CT環境</param>
		/// <returns>結果</returns>
		public static List<vMic商品マスタ> Select_vMic商品マスタ(bool ct)
		{
			string sqlStr = string.Format("SELECT * FROM {0} ORDER BY ID", EstoreDatabaseDefine.ViewName[EstoreDatabaseDefine.ViewType.vMic商品マスタ]);
			DataTable table = EstoreDatabaseAccess.EstoreDatabaseDataAdpter(sqlStr, ct);
			return vMic商品マスタ.DataTableToList(table);
		}
	}
}
