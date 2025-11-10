//
// MwsServiceCancelToolAccess.cs
// 
// MWSサービス利用申込取消ツール データベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2025/01/23 勝呂):新規作成
//
using CommonLib.BaseFactory.MwsServiceCancelTool;
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
		/// 契約ヘッダ情報の取得（おまとめプラン用）
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>契約ヘッダ情報</returns>
		public static DataTable DataTable_UseContractHeaderMatome(int customerNo, string connectStr)
		{
			string sqlStr = string.Format("SELECT TOP 1 fContractID, fCustomerID, fContractType, fMonths, fGoodsID, fApplyDate, fTotalAmount, fContractStartDate, fContractEndDate, fBillingStartDate, fBillingEndDate"
												+ "  FROM {0}"
												+ " WHERE fEndFlag = '0' AND fDeleteFlag = '0' AND fContractType = 'まとめ' AND fCustomerID = {1}"
												+ " ORDER BY fContractID DESC"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_HEADER]
												, customerNo);
			return DatabaseAccess.SelectDatabase(sqlStr, connectStr);
		}

		/// <summary>
		/// 契約ヘッダ情報の取得（セット割サービスプラン用）
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>契約ヘッダ情報</returns>
		public static DataTable DataTable_UseContractHeaderSetService(int customerNo, string connectStr)
		{
			string sqlStr = string.Format("SELECT fContractID, fCustomerID, fContractType, fMonths, fGoodsID, fApplyDate, fTotalAmount, fContractStartDate, fContractEndDate, fBillingStartDate, fBillingEndDate"
												+ "  FROM {0}"
												+ " WHERE fEndFlag = '0' AND fDeleteFlag = '0' AND fContractType = 'セット' AND fCustomerID = {1}"
												+ " ORDER BY fContractID DESC"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_HEADER]
												, customerNo);
			return DatabaseAccess.SelectDatabase(sqlStr, connectStr);
		}

		/// <summary>
		/// 契約詳細情報の取得
		/// </summary>
		/// <param name="fContractID">申込No</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>契約詳細情報</returns>
		public static DataTable DataTable_UseContractDetail(int fContractID, string connectStr)
		{
			string sqlStr = string.Format("SELECT fContractDetailID, fContractID, fSERVICE_ID, fSERVICE_NAME"
												+ "  FROM {0}"
												+ " WHERE fContractID = {1}"
												+ " ORDER BY fContractDetailID"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_DETAIL]
												, fContractID);
			return DatabaseAccess.SelectDatabase(sqlStr, connectStr);
		}

		/// <summary>
		/// PC安心サポート契約情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>契約ヘッダ情報</returns>
		public static DataTable DataTable_UseContractPcSupport(int customerNo, string connectStr)
		{
			string sqlStr = string.Format("SELECT TOP 1 fApplyNo, fCustomerID, fServiceId, fYears, fGoodsID, fApplyDate, fContractStartDate, fContractEndDate, fBillingStartDate, fBillingEndDate, fEndFlag"
												+ "  FROM {0}"
												+ " WHERE fEndFlag = '0' AND fDeleteFlag = '0' AND fCustomerID = {1}"
												+ " ORDER BY fApplyNo DESC"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_PCCSUPPORT]
												, customerNo);
			return DatabaseAccess.SelectDatabase(sqlStr, connectStr);
		}

		/// <summary>
		/// 顧客管理利用情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="serviceID">サービスID</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>顧客管理利用情報</returns>
		public static DataTable DataTable_CustomerUseInformation(int customerNo, int serviceID, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
												+ " [CUSTOMER_ID]"
												+ ", CUI.[SERVICE_ID]"
												+ ", [SERVICE_NAME]"
												+ ", [GOODS_ID]"
												+ ", [USE_START_DATE]"
												+ ", [USE_END_DATE]"
												+ ", [PAUSE_END_STATUS]"
												+ ", [PERIOD_END_DATE]"
												+ "  FROM {0} as CUI"
												+ " LEFT JOIN {1} as SV on SV.SERVICE_ID = CUI.[SERVICE_ID]"
												+ " WHERE CUI.[CUSTOMER_ID] = {2} AND CUI.[SERVICE_ID] = {3}"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_SERVICE]
												, customerNo
												, serviceID);
			return DatabaseAccess.SelectDatabase(sqlStr, connectStr);
		}

		/// <summary>
		/// 各種作業料作業済申請情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>各種作業料作業済申請情報</returns>
		public static DataTable DataTable_UseOnlineDemand(int customerNo, string connectStr)
		{
			string sqlStr = string.Format("SELECT [ApplyNo], [CustomerID], [RemoteFlag], [GoodsID], [sms_mei] as GoodsName, [ApplyDate], [SalesDate]"
												+ " FROM {0} as D"
												+ " LEFT JOIN {1} as G on G.[sms_scd] = D.[GoodsID]"
												+ " WHERE [CustomerID] = {2}"
												+ " ORDER BY [ApplyNo] DESC"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_ONLINE_DEMAND]
												, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]
												, customerNo);
			return DatabaseAccess.SelectDatabase(sqlStr, connectStr);
		}

		/// <summary>
		/// オンライン資格確認訪問診療連携契約情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>オンライン資格確認訪問診療連携契約情報</returns>
		public static DataTable DataTable_UseOnlineHomon(int customerNo, string connectStr)
		{
			string sqlStr = string.Format("SELECT TOP 1 [ApplyNo], [CustomerID], [GoodsID], [CouplerApplyID], [OrderReserveID], [sms_mei] as GoodsName, [ApplyDate], [SalesDate]"
												+ " FROM {0} as D"
												+ " LEFT JOIN {1} as G on G.[sms_scd] = D.[GoodsID]"
												+ " WHERE [CustomerID] = {2}"
												+ " ORDER BY [ApplyNo] DESC"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_ONLINE_HOMON]
												, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]
												, customerNo);
			return DatabaseAccess.SelectDatabase(sqlStr, connectStr);
		}

		/// <summary>
		/// オンライン資格電子処方箋連携費契約情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>オンライン資格電子処方箋連携費契約情報</returns>
		public static DataTable DataTable_UseOnlinePrescription(int customerNo, string connectStr)
		{
			string sqlStr = string.Format("SELECT TOP 1 [ApplyNo], [CustomerID], [GoodsID], [CouplerApplyID], [OrderReserveID], [sms_mei] as GoodsName, [ApplyDate], [SalesDate]"
												+ " FROM {0} as D"
												+ " LEFT JOIN {1} as G on G.[sms_scd] = D.[GoodsID]"
												+ " WHERE [CustomerID] = {2}"
												+ " ORDER BY [ApplyNo] DESC"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_ELECTRIC_PRESCRIPTION]
												, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]
												, customerNo);
			return DatabaseAccess.SelectDatabase(sqlStr, connectStr);
		}

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
		/// おまとめプラン契約情報の削除（契約ヘッダ情報、契約詳細情報を共に削除）
		/// </summary>
		/// <param name="contractID">契約番号</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		/// <exception cref="ApplicationException"></exception>
		public static int Delete_UseContractMatome(int contractID, string connectStr)
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
							string sql = string.Format("DELETE FROM {0} WHERE fContractID = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_DETAIL], contractID);
							rowCount = DatabaseController.SqlExecuteNonQueryTran(con, tran, sql);
							if (rowCount <= -1)
							{
								throw new ApplicationException(sql);
							}
							// T_USE_CONTRACT_HEADERの削除
							sql = string.Format("DELETE FROM {0} WHERE fContractID = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_HEADER], contractID);
							rowCount = DatabaseController.SqlExecuteNonQueryTran(con, tran, sql);
							if (rowCount <= -1)
							{
								throw new ApplicationException(sql);
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
					throw new ApplicationException(string.Format("Delete_UseContractMatome() Error!({0})", ex.Message));
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
		/// CustomerUseInformationの更新（顧客利用情報）
		/// </summary>
		/// <param name="data">顧客利用情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_CustomerUseInformation(CustomerUseInformation data, string updateUser, string connectStr)
		{
			string updateSQL = string.Format(@"UPDATE {0} SET GOODS_ID = @1, USE_END_DATE = @2, PAUSE_END_STATUS = @3"
								+ ", UPDATE_DATE = @4, UPDATE_PERSON = @5, PERIOD_END_DATE = @6, RENEWAL_FLG = @7"
								+ " WHERE CUSTOMER_ID = {1} AND SERVICE_ID = {2}"
								, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]
								, data.CUSTOMER_ID
								, data.SERVICE_ID);

			SqlParameter[] param = {
				new SqlParameter("@1", data.GOODS_ID ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@2", data.USE_END_DATE.HasValue ? data.USE_END_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@3", data.PAUSE_END_STATUS ? "1" : "0"),
				new SqlParameter("@4", DateTime.Now),
				new SqlParameter("@5", updateUser),
				new SqlParameter("@6", data.PERIOD_END_DATE.HasValue ? data.PERIOD_END_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@7", "1")
			};
			return DatabaseAccess.UpdateSetDatabase(updateSQL, param, connectStr);
		}

		/// <summary>
		/// UseContractPcSupportの更新（PC安心サポート契約情報）
		/// </summary>
		/// <param name="data">PC安心サポート契約情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_UseContractPcSupport(UseContractPcSupport data, string updateUser, string connectStr)
		{
			string updateSQL = string.Format(@"UPDATE {0} SET fGoodsID = @1, fContractEndDate = @2, fBillingEndDate = @3, fEndFlag = @4"
								+ ", fUpdateDate = @5, fUpdatePerson = @6"
								+ " WHERE fApplyNo = {1}"
								, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_PCCSUPPORT]
								, data.fApplyNo);

			SqlParameter[] param = {
				new SqlParameter("@1", data.fGoodsID ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@2", data.fContractEndDate.HasValue ? data.fContractEndDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@3", data.fBillingEndDate.HasValue ? data.fBillingEndDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@4", data.fEndFlag ? "1" : "0"),
				new SqlParameter("@5", DateTime.Now),
				new SqlParameter("@6", updateUser)
			};
			return DatabaseAccess.UpdateSetDatabase(updateSQL, param, connectStr);
		}
	}
}
