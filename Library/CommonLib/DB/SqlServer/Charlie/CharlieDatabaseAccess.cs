//
// CharlieDatabaseAccess.cs
//
// CharlieDB データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Charlie.View;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.DB.SqlServer.Charlie
{
	public static class CharlieDatabaseAccess
	{
		////////////////////////////////////////////////////////////////
		// フィールド関連
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// サービスIDに対応するサービス名の取得
		/// </summary>
		/// <param name="serviceID">サービスID</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>サービス名</returns>
		public static string GetServiceName(int serviceID, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_SERVICE], string.Format("SERVICE_ID = {0}", serviceID), "", connectStr);
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
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>製品管理情報リスト</returns>
		public static List<T_PRODUCT_CONTROL> Select_T_PRODUCT_CONTROL(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_PRODUCT_CONTROL], whereStr, orderStr, connectStr);
			return T_PRODUCT_CONTROL.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>顧客利用情報リスト</returns>
		public static List<T_CUSSTOMER_USE_INFOMATION> Select_T_CUSSTOMER_USE_INFOMATION(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION], whereStr, orderStr, connectStr);
			return T_CUSSTOMER_USE_INFOMATION.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_DEMO_USER]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>デモ用ID管理テーブルリスト</returns>
		public static List<T_DEMO_USER> Select_T_DEMO_USER(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_DEMO_USER], whereStr, orderStr, connectStr);
			return T_DEMO_USER.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_LICENSE_PRODUCT_CONTRACT]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>ESETライセンス管理情報リスト</returns>
		public static List<T_LICENSE_PRODUCT_CONTRACT> Select_T_LICENSE_PRODUCT_CONTRACT(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_LICENSE_PRODUCT_CONTRACT], whereStr, orderStr, connectStr);
			return T_LICENSE_PRODUCT_CONTRACT.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>PC安心サポート契約情報リスト</returns>
		public static List<T_USE_PCCSUPPORT> Select_T_USE_PCCSUPPORT(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_PCCSUPPORT], whereStr, orderStr, connectStr);
			return T_USE_PCCSUPPORT.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_CONTRACT_HEADER]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>PC安心サポート契約情報リスト</returns>
		public static List<T_USE_CONTRACT_HEADER> Select_T_USE_CONTRACT_HEADER(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_HEADER], whereStr, orderStr, connectStr);
			return T_USE_CONTRACT_HEADER.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_CONTRACT_DETAIL]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>PC安心サポート契約情報リスト</returns>
		public static List<T_USE_CONTRACT_DETAIL> Select_T_USE_CONTRACT_DETAIL(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_DETAIL], whereStr, orderStr, connectStr);
			return T_USE_CONTRACT_DETAIL.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[売上実績]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns売上実績リスト</returns>
		public static List<売上実績> Select_売上実績(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.売上実績], whereStr, orderStr, connectStr);
			return 売上実績.DataTableToList(table);
		}


		//////////////////////////////
		// UPDATE SET

		/// <summary>
		/// [charlieDB].[dbo].[T_PRODUCT_CONTROL]の更新
		/// </summary>
		/// <param name="data">製品管理情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_T_PRODUCT_CONTROL(T_PRODUCT_CONTROL data, string connectStr)
		{
			return DatabaseAccess.UpdateSetDatabase(data.UpdateSetSqlString, data.GetUpdateSetParameters(), connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]の更新
		/// </summary>
		/// <param name="data">顧客利用情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_T_CUSSTOMER_USE_INFOMATION(T_CUSSTOMER_USE_INFOMATION data, string connectStr)
		{
			return DatabaseAccess.UpdateSetDatabase(data.UpdateSetSqlString, data.GetUpdateSetParameters(), connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[売上実績]の更新
		/// </summary>
		/// <param name="data">売上実績</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_売上実績(売上実績 data, string connectStr)
		{
			return DatabaseAccess.UpdateSetDatabase(data.UpdateSetSqlString, data.GetUpdateSetParameters(), connectStr);
		}


		//////////////////////////////
		// INSERT INTO

		/// <summary>
		/// [charlieDB].[dbo].[T_PRODUCT_CONTROL]の新規追加
		/// </summary>
		/// <param name="data">製品管理情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_T_PRODUCT_CONTROL(T_PRODUCT_CONTROL data, string connectStr)
		{
			return DatabaseAccess.InsertIntoDatabase(T_PRODUCT_CONTROL.InsertIntoSqlString, data.GetInsertIntoParameters(), connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]の新規追加
		/// </summary>
		/// <param name="data">顧客利用情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_T_CUSSTOMER_USE_INFOMATION(T_CUSSTOMER_USE_INFOMATION data, string connectStr)
		{
			return DatabaseAccess.InsertIntoDatabase(T_CUSSTOMER_USE_INFOMATION.InsertIntoSqlString, data.GetInsertIntoParameters(), connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の新規追加
		/// </summary>
		/// <param name="data">PC安心サポート契約情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_T_USE_PCCSUPPORT(T_USE_PCCSUPPORT data, string connectStr)
		{
			return DatabaseAccess.InsertIntoDatabase(T_USE_PCCSUPPORT.InsertIntoSqlString, data.GetInsertIntoParameters(), connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の複数新規追加
		/// </summary>
		/// <param name="list">PC安心サポート契約情報リスト</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoList_T_USE_PCCSUPPORT(IEnumerable<T_USE_PCCSUPPORT> list, string connectStr)
		{
			List<SqlParameter[]> paramList = new List<SqlParameter[]>();
			foreach (T_USE_PCCSUPPORT data in list)
			{
				paramList.Add(data.GetInsertIntoParameters());
			}
			return DatabaseAccess.InsertIntoListDatabase(T_USE_PCCSUPPORT.InsertIntoSqlString, paramList, connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[売上実績]の新規追加
		/// </summary>
		/// <param name="data">売上実績</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_売上実績(売上実績 data, string connectStr)
		{
			return DatabaseAccess.InsertIntoDatabase(売上実績.InsertIntoSqlString, data.GetInsertIntoParameters(), connectStr);
		}


		//////////////////////////////
		// DELETE


		////////////////////////////////////////////////////////////////
		// ビュー関連
		////////////////////////////////////////////////////////////////

		//////////////////////////////
		// SELECT

		/// <summary>
		/// [charlieDB].[dbo].[V_COUPLER_APPLY]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>製品管理情報リスト</returns>
		public static List<V_COUPLER_APPLY> Select_V_COUPLER_APPLY(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.V_COUPLER_APPLY], whereStr, orderStr, connectStr);
			return V_COUPLER_APPLY.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[view_MWS顧客情報]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>製品管理情報リスト</returns>
		public static List<view_MWS顧客情報> Select_view_MWS顧客情報(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.view_MWS顧客情報], whereStr, orderStr, connectStr);
			return view_MWS顧客情報.DataTableToList(table);
		}
	}
}
