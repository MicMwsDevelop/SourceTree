//
// SalesDatabaseAccess.cs
//
// SalesDB データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2022/03/04 勝呂)
// 
using CommonLib.BaseFactory.Sales.Table;
using CommonLib.BaseFactory.Sales.View;
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
		/// <param name="list">進捗管理表_作業情報</param>
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
	}
}
