//
// JunpDatabaseAccess.cs
//
// JunpDB データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2019/06/28 勝呂)
// Ver1.01 新規作成(2022/12/28 勝呂)
// 
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CommonLib.DB.SqlServer.Junp
{
	public static class JunpDatabaseAccess
	{
		////////////////////////////////////////////////////////////////
		// フィールド関連
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// [JunpDB].[dbo].[tMik基本情報]から顧客Noに対する得意先コードを取得
		/// </summary>
		/// <param name="CustomerNo">顧客No</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>得意先コード</returns>
		public static string GetTokuisakiCode(int CustomerNo, string connectStr)
		{
			string sql = string.Format("SELECT [fkj得意先情報] FROM {0} WHERE [fkjCliMicID] = {1}", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik基本情報], CustomerNo);
			DataTable table = DatabaseAccess.SelectDatabase(sql, connectStr);
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
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>請求先コード</returns>
		public static string GetSeikyusakiCode(int CustomerNo, string connectStr)
		{
			string sql = string.Format("SELECT [fus請求先コード] FROM {0} WHERE [fusCliMicID] = {1}", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ], CustomerNo);
			DataTable table = DatabaseAccess.SelectDatabase(sql, connectStr);
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
		/// [JunpDB].[dbo].[tMikユーザ]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>tMikユーザ</returns>
		public static List<tMikユーザ> Select_tMikユーザ(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ], whereStr, orderStr, connectStr);
			return tMikユーザ.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMikOS明細印字]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>tMikOS明細印字</returns>
		public static List<tMikOS明細印字> Select_tMikOS明細印字(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikOS明細印字], whereStr, orderStr, connectStr);
			return tMikOS明細印字.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMikコードマスタ]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>tMikコードマスタ</returns>
		public static List<tMikコードマスタ> Select_tMikコードマスタ(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikコードマスタ], whereStr, orderStr, connectStr);
			return tMikコードマスタ.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMik保守契約]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>tMik保守契約</returns>
		public static List<tMik保守契約> Select_tMik保守契約(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik保守契約], whereStr, orderStr, connectStr);
			return tMik保守契約.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tBusho]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>tMih支店情報</returns>
		public static List<tBusho> Select_tBusho(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tBusho], whereStr, orderStr, connectStr);
			return tBusho.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMih支店情報]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>tMih支店情報</returns>
		public static List<tMih支店情報> Select_tMih支店情報(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報], whereStr, orderStr, connectStr);
			return tMih支店情報.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMik基本情報]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>tMik基本情報</returns>
		public static List<tMik基本情報> Select_tMik基本情報(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik基本情報], whereStr, orderStr, connectStr);
			return tMik基本情報.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMikアプリケーション情報]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>tMik基本tMikアプリケーション情報</returns>
		public static List<tMikアプリケーション情報> Select_tMikアプリケーション情報(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikアプリケーション情報], whereStr, orderStr, connectStr);
			return tMikアプリケーション情報.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMic社内データ管理ヘッダ]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>tMic社内データ管理ヘッダ</returns>
		public static List<tMic社内データ管理ヘッダ> Select_tMic社内データ管理ヘッダ(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMic社内データ管理ヘッダ], whereStr, orderStr, connectStr);
			return tMic社内データ管理ヘッダ.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMic社内データ管理利用部署情報]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>tMic社内データ管理利用部署情報</returns>
		public static List<tMic社内データ管理利用部署情報> Select_tMic社内データ管理利用部署情報(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMic社内データ管理利用部署情報], whereStr, orderStr, connectStr);
			return tMic社内データ管理利用部署情報.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMic終了ユーザーリスト]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>tMic終了ユーザーリスト</returns>
		public static List<tMic終了ユーザーリスト> Select_tMic終了ユーザーリスト(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMic終了ユーザーリスト], whereStr, orderStr, connectStr);
			return tMic終了ユーザーリスト.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tUser]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>tUser</returns>
		public static List<tUser> Select_tUser(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tUser], whereStr, orderStr, connectStr);
			return tUser.DataTableToList(table);
		}


		//////////////////////////////
		// UPDATE SET

		/// <summary>
		/// [JunpDB].[dbo].[tMic終了ユーザーリスト]の更新
		/// </summary>
		/// <param name="data">tMic終了ユーザーリスト</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_tMic終了ユーザーリスト(tMic終了ユーザーリスト data, string connectStr)
		{
			return DatabaseAccess.UpdateSetDatabase(data.UpdateSetSqlString, data.GetUpdateSetParameters(), connectStr);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tClient]の更新
		/// </summary>
		/// <param name="customerID">顧客No</param>
		/// <param name="endFlag">終了フラグ</param>
		/// <param name="updateName">fCliUpdateMan</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_tClient(int customerID, bool endFlag, string updateName, string connectStr)
		{
			string sqlString = string.Format(@"UPDATE {0} SET fCliEnd = @1, fCliUpdate = @2, fCliUpdateMan = @3 WHERE fCliID = {1}"
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]
											, customerID);

			SqlParameter[] param = { new SqlParameter("@1", endFlag ? "1" : "0"),
									new SqlParameter("@2", DateTime.Now),
									new SqlParameter("@3", updateName ?? System.Data.SqlTypes.SqlString.Null) };

			return DatabaseAccess.UpdateSetDatabase(sqlString, param, connectStr);
		}


		//////////////////////////////
		// INSERT INTO

		/// <summary>
		/// [JunpDB].[dbo].[tMic終了ユーザーリスト]の新規追加
		/// </summary>
		/// <param name="data">tMic終了ユーザーリスト</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_tMic終了ユーザーリスト(tMic終了ユーザーリスト data, string connectStr)
		{
			return DatabaseAccess.InsertIntoDatabase(tMic終了ユーザーリスト.InsertIntoSqlString, data.GetInsertIntoParameters(), connectStr);
		}

		/// <summary>
		/// [JunpDB].[dbo].[tMemo]の新規追加
		/// </summary>
		/// <param name="data">tMemo</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_tMemo(tMemo data, string connectStr)
		{
			return DatabaseAccess.InsertIntoDatabase(tMemo.InsertIntoSqlString, data.GetInsertIntoParameters(), connectStr);
		}


		//////////////////////////////
		// DELETE

		/// <summary>
		/// [JunpDB].[dbo].[tMic終了ユーザーリスト]の削除
		/// </summary>
		/// <param name="user">tMic終了ユーザーリスト</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int Delete_tMic終了ユーザーリスト(tMic終了ユーザーリスト data, string connectStr)
		{
			return DatabaseAccess.DeleteDatabase(data.DeleteSqlString, connectStr);
		}


		////////////////////////////////////////////////////////////////
		// ビュー関連
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// [JunpDB].[dbo].[vMicPCA消費税率]から指定日の消費税率の取得
		/// </summary>
		/// <param name="date">当日</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>消費税率</returns>
		public static int GetTaxRate(Date date, string connectStr)
		{
			string sql = string.Format("SELECT CONVERT(int, t.tax_rate2) as 消費税率 FROM {0} as t INNER JOIN (SELECT MAX(r.tax_ymd) as ymd FROM {0} as r WHERE r.tax_ymd <= {1}) as s ON t.tax_ymd = s.ymd", JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA消費税率], date.ToIntYMD());
			DataTable table = DatabaseAccess.SelectDatabase(sql, connectStr);
			if (null != table && 1 == table.Rows.Count)
			{
				return DataBaseValue.ConvObjectToInt(table.Rows[0]["消費税率"]);
			}
			return 0;
		}

		/// <summary>
		/// 終了ユーザーリストの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<int> GetClientEnd(string connectStr)
		{
			string sql = string.Format("SELECT fCliID FROM {0} WHERE fCliEnd = '1' ORDER BY fCliID", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]);
			DataTable table = DatabaseAccess.SelectDatabase(sql, connectStr);
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
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns></returns>
		public static vMicPCA商品マスタ Select_vMicPCA商品マスタ(string code, string connectStr)
		{
			string sql = string.Format("SELECT * FROM {0} WHERE sms_scd = '{1}'", JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ], code);
			DataTable table = DatabaseAccess.SelectDatabase(sql, connectStr);
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
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns></returns>
		public static List<vSoftwareMainteLimit> Select_vSoftwareMainteLimit(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vSoftwareMainteLimit], whereStr, orderStr, connectStr);
			return vSoftwareMainteLimit.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[vMicPCA売上明細]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>vMicPCA売上明細</returns>
		public static List<vMicPCA売上明細> Select_vMicPCA売上明細(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細], whereStr, orderStr, connectStr);
			return vMicPCA売上明細.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[vMicPCA仕入先マスタ]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>vMicPCA仕入先マスタ</returns>
		public static List<vMicPCA仕入先マスタ> Select_vMicPCA仕入先マスタ(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA仕入先マスタ], whereStr, orderStr, connectStr);
			return vMicPCA仕入先マスタ.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[vMic全ユーザー2]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>vMic全ユーザー2</returns>
		public static List<vMic全ユーザー2> Select_vMic全ユーザー2(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー2], whereStr, orderStr, connectStr);
			return vMic全ユーザー2.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[vMic全ユーザー3]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>vMic全ユーザー3</returns>
		public static List<vMic全ユーザー3> Select_vMic全ユーザー3(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー3], whereStr, orderStr, connectStr);
			return vMic全ユーザー3.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[vMic当月売上予想]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>vMic当月売上予想</returns>
		public static List<vMic当月売上予想> Select_vMic当月売上予想(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic当月売上予想], whereStr, orderStr, connectStr);
			return vMic当月売上予想.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[vMic翌月売上予想]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>vMic翌月売上予想</returns>
		public static List<vMic翌月売上予想> Select_vMic翌月売上予想(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic翌月売上予想], whereStr, orderStr, connectStr);
			return vMic翌月売上予想.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[vMicソフトウェア保守料売上予測]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>vMicソフトウェア保守料売上予測</returns>
		public static List<vMicソフトウェア保守料売上予測> Select_vMicソフトウェア保守料売上予測(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicソフトウェア保守料売上予測], whereStr, orderStr, connectStr);
			return vMicソフトウェア保守料売上予測.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[vMicユーザーオン資用]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>vMicユーザーオン資用</returns>
		public static List<vMicユーザーオン資用> Select_vMicユーザーオン資用(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicユーザーオン資用], whereStr, orderStr, connectStr);
			return vMicユーザーオン資用.DataTableToList(table);
		}

		/// <summary>
		/// [JunpDB].[dbo].[vMicオンライン資格確認ソフト改修費]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>vMicオンライン資格確認ソフト改修費</returns>
		/// Ver1.01 新規作成(2022/12/28 勝呂)
		public static List<vMicオンライン資格確認ソフト改修費> Select_vMicオンライン資格確認ソフト改修費(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicオンライン資格確認ソフト改修費], whereStr, orderStr, connectStr);
			return vMicオンライン資格確認ソフト改修費.DataTableToList(table);
		}
	}
}
