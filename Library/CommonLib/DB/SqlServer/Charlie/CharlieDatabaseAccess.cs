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
using CommonLib.BaseFactory.Coupler.Table;
using System;
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
		/// [charlieDB].[dbo].[M_SERVICE]の取得（サービスマスタ情報）
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>サービス情報リスト</returns>
		public static List<M_SERVICE> Select_M_SERVICE(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_SERVICE], whereStr, orderStr, connectStr);
			return M_SERVICE.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_PRODUCT_CONTROL]の取得（製品管理情報）
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
		/// [charlieDB].[dbo].[T_CUSTOMER_FOUNDATIONS]の取得（顧客管理基本情報）
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>顧客管理基本情報リスト</returns>
		public static List<T_CUSTOMER_FOUNDATIONS> Select_T_CUSTOMER_FOUNDATIONS(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSTOMER_FOUNDATIONS], whereStr, orderStr, connectStr);
			return T_CUSTOMER_FOUNDATIONS.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]の取得（顧客利用情報）
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
		/// [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]の取得（顧客利用情報）
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>顧客利用情報リスト</returns>
		public static DataTable GetDataTable_T_CUSSTOMER_USE_INFOMATION(string whereStr, string orderStr, string connectStr)
		{
			return DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION], whereStr, orderStr, connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_DEMO_USER]の取得（デモ用ID管理情報）
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>デモ用ID管理情報リスト</returns>
		public static List<T_DEMO_USER> Select_T_DEMO_USER(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_DEMO_USER], whereStr, orderStr, connectStr);
			return T_DEMO_USER.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_LICENSE_PRODUCT_CONTRACT]の取得（ESETライセンス管理情報）
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
		/// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の取得（PC安心サポート契約情報）
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
		/// [charlieDB].[dbo].[T_USE_CONTRACT_HEADER]の取得（契約ヘッダ管理情報）
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>契約ヘッダ管理情報リスト</returns>
		public static List<T_USE_CONTRACT_HEADER> Select_T_USE_CONTRACT_HEADER(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_HEADER], whereStr, orderStr, connectStr);
			return T_USE_CONTRACT_HEADER.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_CONTRACT_DETAIL]の取得（契約詳細管理情報）
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>契約詳細管理情報リスト</returns>
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

		/// <summary>
		/// [charlieDB].[dbo].[M_CODE]の取得（MWSコードマスタ管理）
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>MWSコードマスタ管理リスト</returns>
		public static List<M_CODE> Select_M_CODE(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_CODE], whereStr, orderStr, connectStr);
			return M_CODE.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[M_CODE]の取得（MWSコードマスタ管理）
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>MWSコードマスタ管理リスト</returns>
		public static List<M_CODE_EX> Select_M_CODE_EX(string whereStr, string orderStr, string connectStr)
		{
			string sqlStr = string.Format("SELECT [GOODS_ID]"
														+ ", MC.[SERVICE_TYPE_ID]"
														+ ", ST.[SERVICE_TYPE_NAME]"
														+ ", MC.[SERVICE_ID]"
														+ ", MS.[SERVICE_NAME]"
														+ ", MC.[SET_SALE]"
														+ ", MC.[REMARKS]"
														+ ", MC.[DELETE_FLG]"
														+ ", MC.[CREATE_DATE]"
														+ ", MC.[CREATE_PERSON]"
														+ ", MC.[UPDATE_DATE]"
														+ ", MC.[UPDATE_PERSON]"
														+ " FROM {0} as MC"
														+ " LEFT JOIN {1} as MS on MS.[SERVICE_ID] = MC.[SERVICE_ID]"
														+ " LEFT JOIN {2} as ST on ST.[SERVICE_TYPE_ID] = MC.[SERVICE_TYPE_ID]"
														, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_CODE]
														, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_SERVICE]
														, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_SERVICE_TYPE]);
			if (0 < whereStr.Length)
			{
				sqlStr += " WHERE " + whereStr;
			}
			if (0 < orderStr.Length)
			{
				sqlStr += " ORDER BY " + orderStr;
			}
			DataTable table = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return M_CODE_EX.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_APPLICATION_DATA]の取得（申込データ）
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>申込データリスト</returns>
		public static List<T_APPLICATION_DATA> Select_T_APPLICATION_DATA(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_APPLICATION_DATA], whereStr, orderStr, connectStr);
			return T_APPLICATION_DATA.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_ONLINE_HOMON]の取得（オン資訪問診療連携契約情報）
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>オン資訪問診療連携契約情報リスト</returns>
		public static List<T_USE_ONLINE_HOMON> Select_T_USE_ONLINE_HOMON(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_ONLINE_HOMON], whereStr, orderStr, connectStr);
			return T_USE_ONLINE_HOMON.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_ELECTRIC_PRESCRIPTION]の取得（電子処方箋管理契約情報）
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>電子処方箋管理契約情報リスト</returns>
		public static List<T_USE_ELECTRIC_PRESCRIPTION> Select_T_USE_ELECTRIC_PRESCRIPTION(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_ELECTRIC_PRESCRIPTION], whereStr, orderStr, connectStr);
			return T_USE_ELECTRIC_PRESCRIPTION.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_売上明細データ内訳]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>売上明細データ内訳情報リスト</returns>
		public static List<T_売上明細データ内訳> Select_T_売上明細データ内訳(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_売上明細データ内訳], whereStr, orderStr, connectStr);
			return T_売上明細データ内訳.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_HARD_SUBSC_HEADER]の取得（ハードレンタル管理 契約情報）
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>ハードサブスク契約情報リスト</returns>
		public static List<T_HARD_SUBSC_HEADER> Select_T_HARD_SUBSC_HEADER(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_HARD_SUBSC_HEADER], whereStr, orderStr, connectStr);
			return T_HARD_SUBSC_HEADER.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_HARD_SUBSC_DETAIL]の取得（ハードサブスク管理 機器情報）
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>ハードサブスク機器情報リスト</returns>
		public static List<T_HARD_SUBSC_DETAIL> Select_T_HARD_SUBSC_DETAIL(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_HARD_SUBSC_DETAIL], whereStr, orderStr, connectStr);
			return T_HARD_SUBSC_DETAIL.DataTableToList(table);
		}


		//////////////////////////////
		// UPDATE SET

		/// <summary>
		/// [charlieDB].[dbo].[T_PRODUCT_CONTROL]の更新（製品管理情報）
		/// </summary>
		/// <param name="data">製品管理情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_T_PRODUCT_CONTROL(T_PRODUCT_CONTROL data, string connectStr)
		{
			return DatabaseAccess.UpdateSetDatabase(data.UpdateSetSqlString, data.GetUpdateSetParameters(), connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]の更新（顧客利用情報）
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

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の更新（PC安心サポート契約情報）
		/// </summary>
		/// <param name="data">PC安心サポート契約情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_T_USE_PCCSUPPORT(T_USE_PCCSUPPORT data, string connectStr)
		{
			return DatabaseAccess.UpdateSetDatabase(data.UpdateSetSqlString, data.GetUpdateSetParameters(), connectStr);
		}



		//////////////////////////////
		// INSERT INTO

		/// <summary>
		/// [charlieDB].[dbo].[T_PRODUCT_CONTROL]の新規追加（製品管理情報）
		/// </summary>
		/// <param name="data">製品管理情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_T_PRODUCT_CONTROL(T_PRODUCT_CONTROL data, string connectStr)
		{
			return DatabaseAccess.InsertIntoDatabase(T_PRODUCT_CONTROL.InsertIntoSqlString, data.GetInsertIntoParameters(), connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]の新規追加（顧客利用情報）
		/// </summary>
		/// <param name="data">顧客利用情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_T_CUSSTOMER_USE_INFOMATION(T_CUSSTOMER_USE_INFOMATION data, string connectStr)
		{
			return DatabaseAccess.InsertIntoDatabase(T_CUSSTOMER_USE_INFOMATION.InsertIntoSqlString, data.GetInsertIntoParameters(), connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の新規追加（PC安心サポート契約情報）
		/// </summary>
		/// <param name="data">PC安心サポート契約情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_T_USE_PCCSUPPORT(T_USE_PCCSUPPORT data, string connectStr)
		{
			return DatabaseAccess.InsertIntoDatabase(T_USE_PCCSUPPORT.InsertIntoSqlString, data.GetInsertIntoParameters(), connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の複数新規追加（PC安心サポート契約情報）
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

		/// <summary>
		/// [charlieDB].[dbo].[T_CUSTOMER_FOUNDATIONS]の新規追加（顧客管理基本情報）
		/// </summary>
		/// <param name="data">顧客管理基本情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertInto_T_CUSTOMER_FOUNDATIONS(T_CUSTOMER_FOUNDATIONS data, string connectStr)
		{
			return DatabaseAccess.InsertIntoDatabase(T_CUSTOMER_FOUNDATIONS.InsertIntoSqlString, data.GetInsertIntoParameters(), connectStr);
		}



		//////////////////////////////
		// DELETE

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_CONTRACT_HEADER]の削除
		/// </summary>
		/// <param name="contractID">契約番号</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		/// <exception cref="ApplicationException"></exception>
		public static int Delete_T_USE_CONTRACT_HEADER(int contractID, string connectStr)
		{
			string sqlStr = string.Format("DELETE FROM {0} WHERE fContractID = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_HEADER], contractID);
			return DatabaseAccess.DeleteDatabase(sqlStr, connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_PCCSUPPORT]の削除
		/// </summary>
		/// <param name="applyNo">受付No</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns></returns>
		public static int Delete_T_USE_PCCSUPPORT(int applyNo, string connectStr)
		{
			string sqlStr = string.Format("DELETE FROM {0} WHERE fApplyNo = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_PCCSUPPORT], applyNo);
			return DatabaseAccess.DeleteDatabase(sqlStr, connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_ONLINE_DEMAND]の削除
		/// </summary>
		/// <param name="applyNo">受付No</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns></returns>
		public static int Delete_T_USE_ONLINE_DEMAND(int applyNo, string connectStr)
		{
			string sqlStr = string.Format("DELETE FROM {0} WHERE ApplyNo = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_ONLINE_DEMAND], applyNo);
			return DatabaseAccess.DeleteDatabase(sqlStr, connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_ONLINE_HOMON]の削除
		/// </summary>
		/// <param name="applyNo">受付No</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns></returns>
		public static int Delete_T_USE_ONLINE_HOMON(int applyNo, string connectStr)
		{
			string sqlStr = string.Format("DELETE FROM {0} WHERE ApplyNo = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_ONLINE_HOMON], applyNo);
			return DatabaseAccess.DeleteDatabase(sqlStr, connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_ELECTRIC_PRESCRIPTION]の削除
		/// </summary>
		/// <param name="applyNo">受付No</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns></returns>
		public static int Delete_T_USE_ELECTRIC_PRESCRIPTION(int applyNo, string connectStr)
		{
			string sqlStr = string.Format("DELETE FROM {0} WHERE ApplyNo = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_ELECTRIC_PRESCRIPTION], applyNo);
			return DatabaseAccess.DeleteDatabase(sqlStr, connectStr);
		}


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

		/// <summary>
		/// [charlieDB].[dbo].[V_CUSTOMER]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>V_CUSTOMERリスト</returns>
		public static List<V_CUSTOMER> Select_V_CUSTOMER(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.V_CUSTOMER], whereStr, orderStr, connectStr);
			return V_CUSTOMER.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[社員マスタ参照ビュー]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>社員マスタ参照ビュー</returns>
		public static List<社員マスタ参照ビュー> Select_社員マスタ参照ビュー(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.社員マスタ参照ビュー], whereStr, orderStr, connectStr);
			return 社員マスタ参照ビュー.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[販売店情報参照ビュー]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>販売店情報参照ビュー</returns>
		public static List<販売店情報参照ビュー> Select_販売店情報参照ビュー(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.販売店情報参照ビュー], whereStr, orderStr, connectStr);
			return 販売店情報参照ビュー.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[V_SERVICE]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>サービス利用情報参照ビュー</returns>
		public static List<V_SERVICE> Select_V_SERVICE(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.V_SERVICE], whereStr, orderStr, connectStr);
			return V_SERVICE.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[view_前月申込データ]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>前月申込データ参照ビュー</returns>
		public static List<view_前月申込データ> Select_view_前月申込データ(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.view_前月申込データ], whereStr, orderStr, connectStr);
			return view_前月申込データ.DataTableToList(table);
		}


		////////////////////////////////////////////////////////////////
		// シノニム関連
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// [charlieDB].[dbo].[T_COUPLER_PRODUCTUSER]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>カプラー顧客情報リスト</returns>
		public static List<T_COUPLER_PRODUCTUSER> Synonym_T_COUPLER_PRODUCTUSER(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.SynonymName[CharlieDatabaseDefine.SynonymType.T_COUPLER_PRODUCTUSER], whereStr, orderStr, connectStr);
			return T_COUPLER_PRODUCTUSER.DataTableToList(table);
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_COUPLER_APPLY]の取得
		/// </summary>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>申込情報リスト</returns>
		public static List<T_COUPLER_APPLY> Synonym_T_COUPLER_APPLY(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.SynonymName[CharlieDatabaseDefine.SynonymType.T_COUPLER_APPLY], whereStr, orderStr, connectStr);
			return T_COUPLER_APPLY.DataTableToList(table);
		}
	}
}
