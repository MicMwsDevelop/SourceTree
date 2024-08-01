//
// OnlineLicenseHomonAccess.cs
//
// オン資格訪問診療サービス ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/07/01 勝呂):新規作成
// Ver1.02(2024/08/01 勝呂):オン資訪問診療契約情報の売上日時の更新時の障害対応
// 
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Charlie.View;
using CommonLib.BaseFactory.OnlineLicenseHomon;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.Coupler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.DB.SqlServer.OnlineLicenseHomon
{
	static public class OnlineLicenseHomonAccess
	{
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// オン資訪問診療連携契約情報の取得
		/// </summary>
		/// <param name="applyDate">申込日</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>オン資訪問診療売上情報リスト</returns>
		public static List<OnlineLicenseHomonEarningsOut> GetOnlineLicenseHomonEarningsOut(Date applyDate, string connectStr)
		{
			DataTable dt = OnlineLicenseHomonGetIO.GetOnlineLicenseHomonEarningsOut(applyDate, connectStr);
			return OnlineLicenseHomonEarningsOut.DataTableToList(dt);
		}

		/// <summary>
		/// オン資訪問診療連携契約情報 売上日時の設定
		/// </summary>
		/// <param name="sale">オン資訪問診療連携契約情報</param>
		/// <param name="procName">アプリ名</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果行数</returns>
		public static int UpdateSetOnlineLicenseHomonSaleDate(OnlineLicenseHomonEarningsOut sale, string procName, string connectStr)
		{
			string updateStr = string.Format(@"UPDATE {0} SET [SalesDate] = @1, [UpdateDate] = @2, [UpdatePerson] = @3 WHERE [ApplyNo] = {1}"
								, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_ONLINE_HOMON]   // 0
								, sale.受付No);   // 1

			// Ver1.02(2024/08/01 勝呂):オン資訪問診療契約情報の売上日時の更新時の障害対応
			SqlParameter[] param = {
				new SqlParameter("@1", sale.売上日時.Value),		// SalesDate
				new SqlParameter("@2", DateTime.Now),		// UpdateDate
				new SqlParameter("@3", procName)			// UpdatePerson
            };
			return DatabaseAccess.UpdateSetDatabase(updateStr, param, connectStr);
		}

		/// <summary>
		/// 顧客管理利用情報にオンライン資格確認訪問診療連携費サービスを追加
		/// </summary>
		/// <param name="sale">オン資訪問診療売上情報</param>
		/// <param name="procName">アプリ名</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果行数</returns>
		public static int SetCustomerUseInformation(OnlineLicenseHomonEarningsOut sale, string procName, string connectStr)
		{
			string whereStr = string.Format("CUSTOMER_ID = {0} AND SERVICE_ID = {1}", sale.顧客No, (int)ServiceCodeDefine.ServiceCode.OnlineLicenseHomon);
			List<T_CUSSTOMER_USE_INFOMATION> cuiList = CharlieDatabaseAccess.Select_T_CUSSTOMER_USE_INFOMATION(whereStr, "", connectStr);
			if (null != cuiList && 0 < cuiList.Count)
			{
				// 変更
				T_CUSSTOMER_USE_INFOMATION cui = cuiList[0];
				string updateStr = string.Format(@"UPDATE {0} SET [USE_END_DATE] = @1, [CANCELLATION_DAY] = @2"
												+ ", [CANCELLATION_PROCESSING_DATE] = @3, [PAUSE_END_STATUS] = @4, [DELETE_FLG] = @5"
												+ ", [UPDATE_DATE] = @6, [UPDATE_PERSON] = @7, [PERIOD_END_DATE] = @8, [RENEWAL_FLG] = @9"
												+ " WHERE CUSTOMER_ID = {1} AND SERVICE_ID = {2}"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]	// 0
												, cui.CUSTOMER_ID	// 1
												, cui.SERVICE_ID);		// 2
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
				T_CUSSTOMER_USE_INFOMATION cui =  new T_CUSSTOMER_USE_INFOMATION();
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

		//////////////////////////////////////////////////////////////////
		/// COUPLER
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 申込情報の利用申込のシステム反映済フラグをシステム反映に設定
		/// </summary>
		/// <param name="applyList">申込情報リスト</param>
		/// <param name="procName">プロシージャ名</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <param name="databaseName">データベース名</param>
		/// <returns>影響行数</returns>
		public static int UpdateSetCouplerApplySystemFlg(List<V_COUPLER_APPLY> applyList, string procName, string connectStr, string databaseName)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(connectStr))
			{
				try
				{
					// 接続
					con.Open();

					foreach (V_COUPLER_APPLY apply in applyList)
					{
						string sqlStr = string.Format(@"UPDATE {0} SET system_flg = '1', update_date = getdate(), update_user = '{1}' WHERE apply_id = {2}"
																	, string.Format("{0}.[dbo].{1}", databaseName, CouplerDatabaseDefine.TableName[CouplerDatabaseDefine.TableType.APPLY])	// 0
																	, procName			// 1
																	, apply.apply_id);	// 2
						// 実行
						rowCount = DatabaseController.SqlExecuteNonQuery(con, sqlStr);
						if (rowCount <= -1)
						{
							throw new ApplicationException("SqlExecuteNonQuery");
						}
					}
				}
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("UpdateSetCouplerApplySystemFlg 申込情報更新エラー ({0})", ex.Message));
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
	}
}
