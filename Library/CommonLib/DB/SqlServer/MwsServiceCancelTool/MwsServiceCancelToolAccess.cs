//
// MwsServiceCancelToolAccess.cs
// 
// MWSサービス利用申込取消ツール データベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/06/11 勝呂):新規作成
//
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.DB.SqlServer.MwsServiceCancelTool
{
	public static class MwsServiceCancelToolAccess
	{
		/// <summary>
		/// 顧客管理利用情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>顧客管理利用情報</returns>
		public static DataTable DataTable_EditCustomerUseInformation(int customerNo, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
												+ " [CUSTOMER_ID] as 顧客No"
												+ ", CUI.[SERVICE_TYPE_ID] as サービス種別"
												+ ", CUI.[SERVICE_ID] as サービスID"
												+ ", [SERVICE_NAME] as サービス名"
												+	", [GOODS_ID] as 商品ID"
												+ ", [APPLICATION_NO] as 申込ID"
												+ ", [KAKIN_START_DATE] as 課金開始日"
												+ ", [USE_START_DATE] as 利用開始日"
												+ ", [USE_END_DATE] as 課金終了日"
												+ ", [CANCELLATION_DAY] as 解約日"
												+ ", [CANCELLATION_PROCESSING_DATE]"
												+ ", [PAUSE_END_STATUS] as 課金対象外フラグ"
												+ ", CUI.[DELETE_FLG] as 削除フラグ"
												+ ", CUI.[CREATE_DATE] as 作成日時"
												+ ", CUI.[CREATE_PERSON] as 作成者"
												+ ", CUI.[UPDATE_DATE] as 更新日時"
												+ ", CUI.[UPDATE_PERSON] as 更新者"
												+ ", [PERIOD_END_DATE] as 利用期限日"
												+ ", [RENEWAL_FLG] as 顧客差分フラグ"
												+ "  FROM {0} as CUI"
												+ " LEFT JOIN {1} as CL on CL.[fCliID] = CUI.[CUSTOMER_ID]"
												+ " LEFT JOIN {2} as SV on SV.SERVICE_ID = CUI.[SERVICE_ID]"
												+ " WHERE CL.[fCliEnd] = 0 AND CUI.[CUSTOMER_ID] = {3}"
												+ " ORDER BY サービスID"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_SERVICE]
												, customerNo);

			return DatabaseAccess.SelectDatabase(sqlStr, connectStr);
		}

		/// <summary>
		/// カプラー申込情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="serviceIdArray">サービスID群</param>
		/// <param name="applyDate">申込日</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns></returns>
		public static DataTable DataTable_V_COUPLER_APPLY(int customerNo, string[] serviceIdArray, Date applyDate, string connectStr) 
		{
			string sqlStr = string.Format("SELECT"
													+ " [apply_id] as 申込No"
													+ ",[cp_id] as MWSID"
													+ ",[customer_id] as 顧客No"
													+ ",AP.[service_id] as サービスID"
													+ ",SV.[SERVICE_NAME] as サービス名"
													+ ",[apply_date] as 申込日時"
													+ ",[apply_type] as 申込種別"
													+ ",[system_flg] as システム反映済フラグ"
													+ ",AP.[create_date] as 作成日時"
													+ ",[create_user] as 作成者"
													+ ",AP.[update_date] as 更新日時"
													+ ",[update_user] as 更新者"
													+ " FROM {0} as AP"
													+ " LEFT JOIN {1} as SV on SV.[SERVICE_ID] = AP.[service_id]"
													+ " WHERE [customer_id] = {2} AND [apply_type] = '0' AND [system_flg] = '0' AND CONVERT(date, [apply_date]) = '{3}' AND AP.[service_id] IN ({4})"
													+ " ORDER BY サービスID"
													, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.V_COUPLER_APPLY]	// 0
													, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_SERVICE]				// 1
													, customerNo		// 2
													, applyDate.ToString()	// 3
													, string.Join(",", serviceIdArray));    // 4

			return DatabaseAccess.SelectDatabase(sqlStr, connectStr);
		}

		/// <summary>
		/// おまとめプラン契約情報の削除（ヘッダ情報、詳細情報共に削除）
		/// </summary>
		/// <param name="header">おまとめプラン契約ヘッダ情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		/// <exception cref="ApplicationException"></exception>
		public static int Delete_Matome(T_USE_CONTRACT_HEADER header, string connectStr)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(connectStr))
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
							// T_USE_CONTRACT_DETAILの削除
							string sqlDetail = string.Format("DELETE FROM {0} WHERE fContractID = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_DETAIL], header.fContractID);
							rowCount = DatabaseController.SqlExecuteNonQueryTran(con, tran, sqlDetail);
							if (rowCount <= -1)
							{
								throw new ApplicationException("Delete_Matome() Error!");
							}
							// T_USE_CONTRACT_HEADERの削除
							string sqlHeader = string.Format("DELETE FROM {0} WHERE fContractID = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_HEADER], header.fContractID);
							rowCount = DatabaseController.SqlExecuteNonQueryTran(con, tran, sqlDetail);
							if (rowCount <= -1)
							{
								throw new ApplicationException("Delete_Matome() Error!");
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
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("Delete_Matome() Error!({0})", ex.Message));
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
