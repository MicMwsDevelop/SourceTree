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
using MwsLib.BaseFactory.Junp.View;
using MwsLib.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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

		/// <summary>
		/// [JunpDB].[dbo].[tMikユーザ]から顧客Noに対する請求先コードを取得
		/// </summary>
		/// <param name="CustomerNo">顧客No</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>請求先コード</returns>
		public static string GetSeikyusakiCode(int CustomerNo, bool ct)
		{
			string sql = string.Format("SELECT [fus請求先コード] FROM {0} WHERE [fusCliMicID] = {1}", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ], CustomerNo);
			DataTable table = SelectJunpDatabase(sql, ct);
			if (null != table && 1 == table.Rows.Count)
			{
				return table.Rows[0]["fus請求先コード"].ToString().Trim();
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
		/// <returns>tMih支店情報</returns>
		public static List<tMih支店情報> Select_tMih支店情報(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectJunpDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報], whereStr, orderStr, ct);
			return tMih支店情報.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMik基本情報]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>tMik基本情報</returns>
		public static List<tMik基本情報> Select_tMik基本情報(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectJunpDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik基本情報], whereStr, orderStr, ct);
			return tMik基本情報.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMikアプリケーション情報]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>tMik基本tMikアプリケーション情報</returns>
		public static List<tMikアプリケーション情報> Select_tMikアプリケーション情報(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectJunpDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikアプリケーション情報], whereStr, orderStr, ct);
			return tMikアプリケーション情報.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMic社内データ管理ヘッダ]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>tMic社内データ管理ヘッダ</returns>
		public static List<tMic社内データ管理ヘッダ> Select_tMic社内データ管理ヘッダ(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectJunpDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMic社内データ管理ヘッダ], whereStr, orderStr, ct);
			return tMic社内データ管理ヘッダ.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMic社内データ管理利用部署情報]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>tMic社内データ管理利用部署情報</returns>
		public static List<tMic社内データ管理利用部署情報> Select_tMic社内データ管理利用部署情報(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectJunpDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMic社内データ管理利用部署情報], whereStr, orderStr, ct);
			return tMic社内データ管理利用部署情報.DataTableToList(table);
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
		public static int InsertInto_tMemo(tMemo data, bool ct)
		{
			return InsertIntoJunpDatabase(tMemo.InsertIntoSqlString, data.GetInsertIntoParameters(), ct);
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

		/// <summary>
		/// 終了ユーザーリストの取得
		/// </summary>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>結果</returns>
		public static List<int> GetClientEnd(bool ct)
		{
			string sql = string.Format("SELECT fCliID FROM {0} WHERE fCliEnd = '1' ORDER BY fCliID", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]);
			DataTable table = SelectJunpDatabase(sql, ct);
			if (null != table && 0 < table.Rows.Count)
			{
				List<int> result = new List<int>();
				foreach (DataRow row in table.Rows)
				{
					result.Add(DataBaseValue.ConvObjectToInt(row["fCliID"]));
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// vMicPCA商品マスタの取得
		/// </summary>
		/// <param name="code"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		public static vMicPCA商品マスタ Select_vMicPCA商品マスタ(string code, bool ct)
		{
			string sql = string.Format("SELECT * FROM {0} WHERE sms_scd = '{1}'", JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ], code);
			DataTable table = SelectJunpDatabase(sql, ct);
			List<vMicPCA商品マスタ> list = vMicPCA商品マスタ.DataTableToList(table);
			if (null != list)
			{
				return list.First();
			}
			return null;
		}

		/// <summary>
		/// vSoftwareMainteLimitの取得
		/// </summary>
		/// <param name="whereStr"></param>
		/// <param name="orderStr"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		public static List<vSoftwareMainteLimit> Select_vSoftwareMainteLimit(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectJunpDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vSoftwareMainteLimit], whereStr, orderStr, ct);
			return vSoftwareMainteLimit.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[vMicPCA売上明細]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>vMicPCA売上明細</returns>
		public static List<vMicPCA売上明細> Select_vMicPCA売上明細(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectJunpDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細], whereStr, orderStr, ct);
			return vMicPCA売上明細.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[vMicPCA仕入先マスタ]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>vMicPCA仕入先マスタ</returns>
		public static List<vMicPCA仕入先マスタ> Select_vMicPCA仕入先マスタ(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectJunpDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA仕入先マスタ], whereStr, orderStr, ct);
			return vMicPCA仕入先マスタ.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[vMic全ユーザー2]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>vMic全ユーザー2</returns>
		public static List<vMic全ユーザー2> Select_vMic全ユーザー2(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectJunpDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー2], whereStr, orderStr, ct);
			return vMic全ユーザー2.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[vMic当月売上予想]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>vMic当月売上予想</returns>
		public static List<vMic当月売上予想> Select_vMic当月売上予想(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectJunpDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic当月売上予想], whereStr, orderStr, ct);
			return vMic当月売上予想.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[vMic翌月売上予想]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>vMic翌月売上予想</returns>
		public static List<vMic翌月売上予想> Select_vMic翌月売上予想(string whereStr, string orderStr, bool ct)
		{
			DataTable table = SelectJunpDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic翌月売上予想], whereStr, orderStr, ct);
			return vMic翌月売上予想.DataTableToList(table);
		}



	}
}
