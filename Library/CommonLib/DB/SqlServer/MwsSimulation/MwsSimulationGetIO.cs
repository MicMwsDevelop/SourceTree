//
// MwsSimulationGetIO.cs
// 
// MIC WEB SERVICE 課金シミュレーション SQL SERVER データベース読込みI/Oクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
//
using CommonLib.DB.SqlServer.Charlie;
using System;
using System.Data;

namespace CommonLib.DB.SqlServer.MwsSimulation
{
	public static class MwsSimulationGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// サービス情報の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>サービス情報レコード数</returns>
		public static DataTable GetServiceInfo(string connectStr)
		{
			string strSQL = string.Format(@"SELECT  M_SERVICE.SERVICE_ID AS ServiceCode"
									  + ", M_SERVICE.[SERVICE_NAME] AS ServiceName"
									  + ", M_SERVICE.PARENTS_SERVICE_ID AS ParentServiceCode"
									  + ", M_SERVICE_TYPE.SERVICE_TYPE_ID AS ServiceType"
									  + ", M_SERVICE_TYPE.SERVICE_TYPE_NAME AS ServiceTypeName"
									  + ", V_PCA_GOODS.PRICE AS Price"
									  + ", M_CODE.GOODS_ID AS GoodsID"
									  + ", V_PCA_GOODS.GOODS_NAME AS GoodsName"
									  + ", V_PCA_GOODS.BRAND_CLASSIFICATION AS GoodsKubun"
									  + " FROM {0} AS M_SERVICE"
									  + " INNER JOIN {1} AS M_SERVICE_TYPE ON M_SERVICE.SERVICE_TYPE_ID = M_SERVICE_TYPE.SERVICE_TYPE_ID"
									  + " INNER JOIN {2} AS M_CODE ON M_SERVICE.SERVICE_ID = M_CODE.SERVICE_ID"
									  + " INNER JOIN {3} AS V_PCA_GOODS ON M_CODE.GOODS_ID = V_PCA_GOODS.GOODS_ID"
									  + " WHERE (M_SERVICE.DELETE_FLG = N'0') AND (M_SERVICE.UMU_FLG = N'0') AND (M_CODE.DELETE_FLG = N'0') AND (M_CODE.SET_SALE = 1) AND (M_SERVICE.SERVICE_ID <> '1099')"
									  + " AND (V_PCA_GOODS.BRAND_CLASSIFICATION = 200 OR V_PCA_GOODS.BRAND_CLASSIFICATION = 201 OR V_PCA_GOODS.BRAND_CLASSIFICATION = 211 OR V_PCA_GOODS.BRAND_CLASSIFICATION = 212 OR V_PCA_GOODS.BRAND_CLASSIFICATION = 214)"
									  + " ORDER BY M_SERVICE_TYPE.SERVICE_TYPE_ID ASC, M_SERVICE.SERVICE_ID ASC"
									  , CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_SERVICE]
									  , CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_SERVICE_TYPE]
									  , CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_CODE]
									  , CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.V_PCA_GOODS]);

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// セット割サービス情報の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>セット割サービス情報の取得レコード数</returns>
		public static DataTable GetSetPlan(string connectStr)
		{
			string strSQL = string.Format(@"SELECT GOODS.GOODS_ID AS GoodsID"
									+ ", GOODS.GOODS_NAME AS GoodsName"
									+ ", GOODS.PRICE AS Price"
									+ ", MCODE.SERVICE_ID AS ServiceCode"
									+ ", MSERVICE.SERVICE_NAME AS ServiceName"
									+ " FROM {0} AS GOODS"
									+ " LEFT JOIN {1} AS MCODE ON MCODE.GOODS_ID = GOODS.GOODS_ID"
									+ " LEFT JOIN {2} AS MSERVICE ON MSERVICE.SERVICE_ID = MCODE.SERVICE_ID"
									+ " WHERE BRAND_CLASSIFICATION = 206"
									+ " ORDER BY GOODS.GOODS_ID ASC, MCODE.SERVICE_ID ASC"
									, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.V_PCA_GOODS]
									, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_CODE]
									, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_SERVICE]);

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// セット割サービス情報の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>セット割サービス情報の取得レコード数</returns>
		public static DataTable GetSetPlanElement(string connectStr)
		{
			string strSQL = string.Format(@"SELECT SERVICE_ID AS ServiceCode, GOODS_ID AS GoodsID"
							+ " FROM {0}"
							+ " WHERE SET_SALE = 1"
							+ " ORDER BY SERVICE_ID ASC"
							, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_CODE]);

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// おまとめプラン情報の取得
		/// </summary>
		/// <returns>おまとめプラン情報レコード数</returns>
		/// <param name="connectStr">SQL Server接続文字列</param>
		public static DataTable GetGroupPlan(string connectStr)
		{
			string strSQL = string.Format(@"SELECT goods_id AS GoodsID"
										+ ", plan_nm AS GoodsName"
										+ ", keiyaku_month_cnt AS KeiyakuMonth"
										+ ", free_use_month AS FreeMonth"
										+ ", min_amount AS MinAmmount"
										+ ", max_amount AS MaxAmmount"
										+ " FROM {0}"
										+ " WHERE del_flg = N'0' AND goods_kbn = 204"
										+ " AND ((close_date is null) OR (open_date <= '{1}' AND close_date >= '{1}'))"
										+ " ORDER BY goods_id ASC, keiyaku_month_cnt ASC"
										, CharlieDatabaseDefine.SynonymName[CharlieDatabaseDefine.SynonymType.T_COUPLER_GROUP_PLAN]
										, DateTime.Now);

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// おススメセット情報の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>おススメセット情報レコード数</returns>
		public static DataTable GetInitGroupPlan(string connectStr)
		{
			string strSQL = string.Format(@"SELECT MSET.SET_ID AS GroupID"
										+ ", MSET.SET_NM AS GroupName"
										+ ", SET_SERVICE.SERVICE_ID AS ServiceCode"
										+ " FROM {0} AS MSET"
										+ " LEFT JOIN dbo.M_SET_SERVICE AS SET_SERVICE ON SET_SERVICE.SET_ID = MSET.SET_ID"
										+ " WHERE MSET.DEL_FLG = N'0' AND (MSET.SET_START_DATE <= '{1}' AND MSET.SET_END_DATE >= '{1}')"
										+ " ORDER BY MSET.SET_ID ASC, SET_SERVICE.SERVICE_ID ASC"
										, CharlieDatabaseDefine.SynonymName[CharlieDatabaseDefine.SynonymType.M_SET]
										, DateTime.Now);

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}
	}
}
