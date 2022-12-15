//
// SalesDatabaseAccess.cs
//
// SalesDB データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.01 NTT現調プランに対応(2022/08/31 勝呂)
// Ver1.02 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
// 
using CommonLib.BaseFactory.Sales.Table;
using CommonLib.BaseFactory.Sales.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.DB.SqlServer.Sales
{
	public static class SalesDatabaseAccess
	{
		////////////////////////////////////////////////////////////////
		// テーブル関連
		////////////////////////////////////////////////////////////////

		//////////////////////////////
		// SELECT

		/// <summary>
		/// [SalesDB].[dbo].[オン資格ヒアリングシート]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果リスト</returns>
		public static List<オン資格ヒアリングシート> Select_オン資格ヒアリングシート(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(SalesDatabaseDefine.TableName[SalesDatabaseDefine.TableType.オン資格ヒアリングシート], whereStr, orderStr, connectStr);
			return オン資格ヒアリングシート.DataTableToList(table);
		}

		/// <summary>
		/// [SalesDB].[dbo].[進捗管理表_作業情報]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果リスト</returns>
		public static List<進捗管理表_作業情報> Select_進捗管理表_作業情報(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(SalesDatabaseDefine.TableName[SalesDatabaseDefine.TableType.進捗管理表_作業情報], whereStr, orderStr, connectStr);
			return 進捗管理表_作業情報.DataTableToList(table);
		}

		/// <summary>
		/// [SalesDB].[dbo].[オンライン資格確認進捗管理情報]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果リスト</returns>
		public static List<オンライン資格確認進捗管理情報> Select_オンライン資格確認進捗管理情報(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(SalesDatabaseDefine.TableName[SalesDatabaseDefine.TableType.オンライン資格確認進捗管理情報], whereStr, orderStr, connectStr);
			return オンライン資格確認進捗管理情報.DataTableToList(table);
		}

		/// <summary>
		/// 顧客Noに対応する[SalesDB].[dbo].[オンライン資格確認進捗管理情報]に存在するか？
		/// </summary>
		/// <param name="connectStr">接続文字列</param>
		/// <param name="CustomerNo">顧客No</param>
		/// <returns>判定</returns>
		public static bool IsExistオンライン資格確認進捗管理情報(string connectStr, int CustomerNo)
		{
			object no = DatabaseAccess.ScalarDatabase(string.Format("SELECT 顧客No FROM {0} WHERE 顧客No = {1}", SalesDatabaseDefine.TableName[SalesDatabaseDefine.TableType.オンライン資格確認進捗管理情報], CustomerNo), connectStr);
			if (no is int)
			{
				return true;
			}
			return false;
		}


		//////////////////////////////
		// UPDATE SET

		/// <summary>
		/// [SalesDB].[dbo].[進捗管理表_作業情報]の更新
		/// </summary>
		/// <param name="data">進捗管理表_作業情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_進捗管理表_作業情報(進捗管理表_作業情報 data, string connectStr)
		{
			return DatabaseAccess.UpdateSetDatabase(data.UpdateSetSqlString, data.GetUpdateSetParameters(), connectStr);
		}

		/// <summary>
		/// [SalesDB].[dbo].[オンライン資格確認進捗管理情報]の更新
		/// </summary>
		/// <param name="data">オンライン資格確認進捗管理情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_オンライン資格確認進捗管理情報(オンライン資格確認進捗管理情報 data, string connectStr)
		{
			return DatabaseAccess.UpdateSetDatabase(data.UpdateSetSqlString, data.GetUpdateSetParameters(), connectStr);
		}

		// [SalesDB].[dbo].[オンライン資格確認進捗管理情報] 運用開始日の更新

		/// <summary>
		/// [SalesDB].[dbo].[オンライン資格確認進捗管理情報] 運用開始日の更新
		/// </summary>
		/// <param name="customerNo"></param>
		/// <param name="dt"></param>
		/// <param name="connectStr"></param>
		/// <returns></returns>
		// Ver1.01 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
		public static int UpdateSet_オンライン資格確認進捗管理情報_運用開始日(int customerNo, DateTime dt, string connectStr)
		{
			SqlParameter[] param = { new SqlParameter("@1", dt.ToString()) };
			return DatabaseAccess.UpdateSetDatabase(string.Format(@"UPDATE {0} SET オン資運用開始日 = @1 WHERE 顧客No = {1}", SalesDatabaseDefine.TableName[SalesDatabaseDefine.TableType.オンライン資格確認進捗管理情報], customerNo), param, connectStr);
		}


		//////////////////////////////
		// INSERT INTO

		/// <summary>
		/// [SalesDB].[dbo].[進捗管理表_作業情報]の新規追加
		/// </summary>
		/// <param name="data">進捗管理表_作業情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_進捗管理表_作業情報(進捗管理表_作業情報 data, string connectStr)
		{
			return DatabaseAccess.InsertIntoDatabase(進捗管理表_作業情報.InsertIntoSqlString, data.GetInsertIntoParameters(), connectStr);
		}

		/// <summary>
		/// [SalesDB].[dbo].[進捗管理表_作業情報]の複数新規追加
		/// </summary>
		/// <param name="list">進捗管理表_作業情報リスト</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoList_進捗管理表_作業情報(List<進捗管理表_作業情報> list, string connectStr)
		{
			List<SqlParameter[]> paramList = new List<SqlParameter[]>();
			foreach (進捗管理表_作業情報 data in list)
			{
				paramList.Add(data.GetInsertIntoParameters());
			}
			return DatabaseAccess.InsertIntoListDatabase(進捗管理表_作業情報.InsertIntoSqlString, paramList, connectStr);
		}

		/// <summary>
		/// [SalesDB].[dbo].[オンライン資格確認進捗管理情報]の複数新規追加
		/// </summary>
		/// <param name="data">オンライン資格確認進捗管理情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_オンライン資格確認進捗管理情報(オンライン資格確認進捗管理情報 data, string connectStr)
		{
			return DatabaseAccess.InsertIntoDatabase(オンライン資格確認進捗管理情報.InsertIntoSqlString, data.GetInsertIntoParameters(), connectStr);
		}

		/// <summary>
		/// [SalesDB].[dbo].[オンライン資格確認進捗管理情報]の複数新規追加
		/// </summary>
		/// <param name="list">オンライン資格確認進捗管理情報リスト</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoList_オンライン資格確認進捗管理情報(List<オンライン資格確認進捗管理情報> list, string connectStr)
		{
			List<SqlParameter[]> paramList = new List<SqlParameter[]>();
			foreach (オンライン資格確認進捗管理情報 data in list)
			{
				paramList.Add(data.GetInsertIntoParameters());
			}
			return DatabaseAccess.InsertIntoListDatabase(オンライン資格確認進捗管理情報.InsertIntoSqlString, paramList, connectStr);
		}


		//////////////////////////////
		// DELETE

		/// <summary>
		/// [SalesDB].[dbo].[オンライン資格確認進捗管理情報]の削除
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響レコード数</returns>
		public static int Delete_オンライン資格確認進捗管理情報(string whereStr, string connectStr)
		{
			string sqlStr = string.Format("DELETE FROM {0}", SalesDatabaseDefine.TableName[SalesDatabaseDefine.TableType.オンライン資格確認進捗管理情報]);
			if (0 < whereStr.Length)
			{
				sqlStr += " WHERE " + whereStr;
			}
			return DatabaseAccess.DeleteDatabase(sqlStr, connectStr);
		}


		////////////////////////////////////////////////////////////////
		// ビュー関連
		////////////////////////////////////////////////////////////////

		//////////////////////////////
		// SELECT

		/// <summary>
		/// [SalesDB].[dbo].[vオンライン資格確認ユーザー]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果リスト</returns>
		public static List<vオンライン資格確認ユーザー> Select_vオンライン資格確認ユーザー(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(SalesDatabaseDefine.ViewName[SalesDatabaseDefine.ViewType.vオンライン資格確認ユーザー], whereStr, orderStr, connectStr);
			return vオンライン資格確認ユーザー.DataTableToList(table);
		}

		/// <summary>
		/// [SalesDB].[dbo].[vオンライン資格確認進捗管理]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果リスト</returns>
		public static List<vオンライン資格確認進捗管理> Select_vオンライン資格確認進捗管理情報(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(SalesDatabaseDefine.ViewName[SalesDatabaseDefine.ViewType.vオンライン資格確認進捗管理情報], whereStr, orderStr, connectStr);
			return vオンライン資格確認進捗管理.DataTableToList(table);
		}
	}
}
