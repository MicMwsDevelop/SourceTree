//
// ElectricPrescriptionAccess.cs
//
// 電子処方箋管理契約情報 ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/07/01 勝呂):新規作成
// 
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.ElectricPrescription;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.DB.SqlServer.ElectricPrescription
{
	public static class ElectricPrescriptionAccess
	{
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 電子処方箋管理契約情報の取得
		/// </summary>
		/// <param name="applyDate">申込日時</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>電子処方箋管理契約情報リスト</returns>
		public static List<ElectricPrescriptionEarningsOut> GetElectricPrescriptionEarningsOut(Date applyDate, string connectStr)
		{
			DataTable dt = ElectricPrescriptionGetIO.GetElectricPrescriptionEarningsOut(applyDate, connectStr);
			return ElectricPrescriptionEarningsOut.DataTableToList(dt);
		}

		/// <summary>
		/// 電子処方箋管理契約情報 売上日時の設定
		/// </summary>
		/// <param name="sale">電子処方箋管理契約情報</param>
		/// <param name="procName">アプリ名</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果行数</returns>
		public static int UpdateSetElectricPrescriptionSaleDate(ElectricPrescriptionEarningsOut sale, string procName, string connectStr)
		{
			string updateStr = string.Format(@"UPDATE {0} SET [SalesDate] = @1, [UpdateDate] = @2, [UpdatePerson] = @3 WHERE [ApplyNo] = {1}"
								, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_ONLINE_HOMON]   // 0
								, sale.受付番号);   // 1

			SqlParameter[] param = {
				new SqlParameter("@1", DateTime.Now),		// SalesDate
				new SqlParameter("@2", DateTime.Now),		// UpdateDate
				new SqlParameter("@3", procName)			// UpdatePerson
            };
			return DatabaseAccess.UpdateSetDatabase(updateStr, param, connectStr);
		}

		/// <summary>
		/// 顧客管理利用情報に電子処方箋管理サービスを追加
		/// </summary>
		/// <param name="sale">オン資格訪問診療売上情報</param>
		/// <param name="procName">アプリ名</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果行数</returns>
		public static int InsertIntoCustomerUseInformation(ElectricPrescriptionEarningsOut sale, string procName, string connectStr)
		{
			string whereStr = string.Format("CUSTOMER_ID = {0} AND SERVICE_ID = {1}", sale.顧客No, (int)sale.GetServiceCode);
			List<T_CUSSTOMER_USE_INFOMATION> cuiList = CharlieDatabaseAccess.Select_T_CUSSTOMER_USE_INFOMATION(whereStr, "", connectStr);
			if (null != cuiList && 0 < cuiList.Count)
			{
				// 変更
				T_CUSSTOMER_USE_INFOMATION cui = cuiList[0];
				string updateStr = string.Format(@"UPDATE {0} SET [USE_END_DATE] = @1, [CANCELLATION_DAY] = @2"
												+ ", [CANCELLATION_PROCESSING_DATE] = @3, [PAUSE_END_STATUS] = @4, [DELETE_FLG] = @5"
												+ ", [UPDATE_DATE] = @6, [UPDATE_PERSON] = @7, [PERIOD_END_DATE] = @8, [RENEWAL_FLG] = @9"
												+ " WHERE CUSTOMER_ID = {1} AND SERVICE_ID = {2}"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]   // 0
												, cui.CUSTOMER_ID   // 1
												, cui.SERVICE_ID);      // 2

				SqlParameter[] param = {
					new SqlParameter("@1", sale.契約終了日.Value),		// USE_END_DATE
					new SqlParameter("@2", System.Data.SqlTypes.SqlString.Null),		// CANCELLATION_DAY
					new SqlParameter("@3", System.Data.SqlTypes.SqlString.Null),		// CANCELLATION_PROCESSING_DATE
					new SqlParameter("@4", "0"),		// PAUSE_END_STATUS
					new SqlParameter("@5", "0"),		// DELETE_FLG
					new SqlParameter("@6", DateTime.Now),		// UPDATE_DATE
					new SqlParameter("@7", procName),			// UPDATE_PERSON
					new SqlParameter("@8", System.Data.SqlTypes.SqlString.Null),		// PERIOD_END_DATE
					new SqlParameter("@9", "1"),		// RENEWAL_FLG
				};
				return DatabaseAccess.UpdateSetDatabase(updateStr, param, connectStr);
			}
			else
			{
				// 新規追加
				T_CUSSTOMER_USE_INFOMATION cui = new T_CUSSTOMER_USE_INFOMATION();
				cui.CUSTOMER_ID = sale.顧客No;
				cui.SERVICE_TYPE_ID = (int)ServiceCodeDefine.ServiceType.OnlineLicense;
				cui.SERVICE_ID = (int)ServiceCodeDefine.ServiceCode.OnlineLicenseHomon;
				cui.USE_START_DATE = sale.申込日時;
				cui.USE_END_DATE = sale.契約終了日;
				cui.CREATE_DATE = DateTime.Now;
				cui.CREATE_PERSON = procName;
				cui.RENEWAL_FLG = true;
				SqlParameter[] param = cui.GetInsertIntoParameters();
				return DatabaseAccess.InsertIntoDatabase(T_CUSSTOMER_USE_INFOMATION.InsertIntoSqlString, param, connectStr);
			}
		}
	}
}
