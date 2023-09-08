//
// AdjustServiceApplyGetIO.cs
//
// サービス申込情報更新処理 データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.DB.SqlServer.Charlie;
using System.Data;

namespace CommonLib.DB.SqlServer.AdjustServiceApply
{
	public static class AdjustServiceApplyGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// WW伝票参照ビュー抽出
		/// WW伝票view.受注承認日が締日の範囲内で、数量>0の、伝票番号が最小の伝票データの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static DataTable GetWonderWebSlip(string connectStr)
		{
			string strSQL = string.Format(@"SELECT Z.伝票No"
														+ ", Z.販売先顧客ID"
														+ ", Z.ユーザー顧客ID"
														+ ", Z.担当者ID"
														+ ", Z.担当者名"
														+ ", Z.担当支店ID"
														+ ", Z.担当支店名"
														+ ", Z.受注年月日"
														+ ", Z.受注承認日"
														+ ", Z.売上承認日"
														+ ", Z.商品コード"
														+ ", Z.商品名"
														+ ", Z.商品区分"
														+ ", Z.数量"
														+ ", Z.販売価格"
														+ ", Z.申込種別"
														+ ", Z.システム略称"
														+ ", Z.最終出力日時"
														+ " FROM {0} as Z"
														+ " INNER HASH JOIN"
														+ " ("
														+ " SELECT *"
														+ " FROM"
														+ " (SELECT"
														+ "  Y.ユーザー顧客ID"
														+ ", Y.商品コード"
														+ ", SUM(Y.数量) AS sumQUANTITY"
														+ ", COUNT(Y.数量) AS CNT"
														+ ", MIN(伝票No) AS minCHECK_NO"
														+ " FROM {0} as Y"
														+ " WHERE 受注承認日 is not null"
														+ " GROUP BY ユーザー顧客ID, 商品コード"
														+ ") as tblA"
														+ " WHERE sumQUANTITY > 0"
														+ ") as X ON X.minCHECK_NO = Z.伝票No AND X.ユーザー顧客ID = Z.ユーザー顧客ID AND X.商品コード = Z.商品コード"
														+ " WHERE Z.受注承認日 is not null"
														+ " ORDER BY 伝票No, ユーザー顧客ID, 商品コード"
														, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.WW伝票参照ビュー]);
			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// 販売店情報参照ビューから販売店コードを取得
		/// </summary>
		/// <param name="storeCode">販売店コード</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static DataTable GetStoreCode(int storeCode, string connectStr)
		{
			string strSQL = string.Format(@"SELECT SI.販売店コード"
														+ " FROM {0} as VR"
														+ " INNER JOIN {1} as SI ON VR.区分コード = CONVERT(int, SI.販売店ランクコード) AND VR.ランク is not null"
														+ " WHERE SI.販売店コード = {2}"
														, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.販売店区分参照ビュー]
														, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.販売店情報参照ビュー]
														, storeCode);
			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// 基本機能パック 商品コード、サービス種別ID、サービスIDの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static DataTable GetKihonPack(string connectStr)
		{
			string strSQL = string.Format(@"SELECT TOP 1 A.*"
												+ " FROM {0} as A"
												+ " INNER JOIN {1} as B on A.GOODS_ID = B.GOODS_ID AND B.BRAND_CLASSIFICATION = 200"
												+ " WHERE A.DELETE_FLG = '0' AND SET_SALE = '1'"
												+ " ORDER BY A.GOODS_ID"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_CODE]
												, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.V_PCA_GOODS]	);
			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// 前回同期日時の取得（サービス情報）
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static DataTable GetLastSynchroTime(string connectStr)
		{
			string strSQL = string.Format(@"SELECT TOP 1 * FROM {0} WHERE FILE_TYPE = '2' ORDER BY FILE_CREATEDATE DESC"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_FILE_CREATEDATE]);
			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// 前回同期日時以降の顧客管理利用情報の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static DataTable GetCustomerUseInformationAfterSynchroTime(string connectStr)
		{
			string strSQL = string.Format(@"SELECT"
													+ " CUSTOMER_ID"
													+ ", SERVICE_TYPE_ID"
													+ ", SERVICE_ID"
													+ ", GOODS_ID"
													+ ", APPLICATION_NO"
													+ ", KAKIN_START_DATE"
													+ ", USE_START_DATE"
													+ ", USE_END_DATE"
													+ ", CANCELLATION_DAY"
													+ ", CANCELLATION_PROCESSING_DATE"
													+ ", PAUSE_END_STATUS"
													+ ", DELETE_FLG"
													+ ", CREATE_DATE"
													+ ", CREATE_PERSON"
													+ ", UPDATE_DATE"
													+ ", UPDATE_PERSON"
													+ ", PERIOD_END_DATE"
													+ ", RENEWAL_FLG"
													+ " FROM {0} as CUI"
													+ " CROSS JOIN"
													+ " ("
													+ " SELECT max(FILE_CREATEDATE) as 最終出力日時"
													+ " FROM {1}"
													+ " WHERE FILE_TYPE = 2"
													+ " GROUP BY FILE_TYPE"
													+ ") as L"
													+ " WHERE SERVICE_TYPE_ID <> 1"
													+ " AND CUI.USE_START_DATE is not null"
													+ " AND CUI.USE_END_DATE is not null"
													+ " AND (CUI.CREATE_DATE > 最終出力日時 OR CUI.UPDATE_DATE > 最終出力日時)"
													+ " ORDER BY CUSTOMER_ID, SERVICE_ID"
													, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]
													, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_FILE_CREATEDATE]);
			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}






















			///// <summary>
			///// 顧客利用情報から利用申込サービスの取得
			///// </summary>
			///// <param name="compDateTime">前回取得日時</param>
			///// <param name="connectStr">SQL Server接続文字列</param>
			///// <returns>DataTable</returns>
			//public static DataTable GetCustomerUseInformationUseService(DateTime compDateTime, string connectStr)
			//{
			//	string strSQL = string.Format(@"SELECT * FROM {0}"
			//										+ " WHERE [SERVICE_TYPE_ID] <> 1"	// 基本サービス以外
			//										+ " AND [PAUSE_END_STATUS] = 0"		// 課金対象外フラグがOFF
			//										+ " AND [USE_START_DATE] is not null"	// 利用開始日がNULLでない
			//										+ " AND [PERIOD_END_DATE] is null"		// 利用終了日がNULL
			//										+ " AND [USE_START_DATE] <= EOMONTH(getdate(), 1) AND [USE_END_DATE] >= EOMONTH(getdate(), 1)	"	// 翌月末日のサービスが利用可能
			//										+ " AND [UPDATE_DATE] > '{1}'"
			//										+ " ORDER BY [CUSTOMER_ID], [SERVICE_ID]"
			//										, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]
			//										, compDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
			//	return DatabaseAccess.SelectDatabase(strSQL, connectStr);
			//}

			///// <summary>
			///// 顧客利用情報から解約申込サービスの取得
			///// </summary>
			///// <param name="compDateTime">前回取得日時</param>
			///// <param name="connectStr">SQL Server接続文字列</param>
			///// <returns>DataTable</returns>
			//public static DataTable GetCustomerUseInformationCancelService(DateTime compDateTime, string connectStr)
			//{
			//	string strSQL = string.Format(@"SELECT * FROM {0}"
			//										+ " WHERE [SERVICE_TYPE_ID] <> 1"	// 基本サービス以外
			//										+ " AND [PAUSE_END_STATUS] = 1"		// 課金対象外フラグがON
			//										+ " AND [USE_END_DATE] is not null"		// 課金終了日がNULLでない
			//										+ " AND [PERIOD_END_DATE] = EOMONTH(getdate())"	// 利用終了日が今月末日
			//										+ " AND [UPDATE_DATE] > '{1}'"
			//										+ " ORDER BY [CUSTOMER_ID], [SERVICE_ID]"
			//										, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]
			//										, compDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
			//	return DatabaseAccess.SelectDatabase(strSQL, connectStr);
			//}

			///// <summary>
			///// サービス申込情報から利用申込サービスの取得
			///// </summary>
			///// <param name="connectStr">SQL Server接続文字列</param>
			///// <returns>DataTable</returns>
			//public static DataTable GetMwsApplyUserService(string connectStr)
			//{
			//	string strSQL = string.Format(@"SELECT * FROM {0}"
			//										+ " WHERE [system_flg] = '0' AND [apply_type] = '0' AND [customer_id] < 30000000"
			//										+ " ORDER BY [customer_id], [service_id]"
			//										, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_MWS_APPLY]);
			//	return DatabaseAccess.SelectDatabase(strSQL, connectStr);
			//}

			///// <summary>
			///// サービス申込情報から解約申込サービスの取得
			///// </summary>
			///// <param name="connectStr">SQL Server接続文字列</param>
			///// <returns>DataTable</returns>
			//public static DataTable GetMwsApplyCancelService(string connectStr)
			//{
			//	string strSQL = string.Format(@"SELECT * FROM {0}"
			//										+ " WHERE [system_flg] = '0' AND [apply_type] = '1' AND [customer_id] < 30000000"
			//										+ " ORDER BY [customer_id], [service_id]"
			//										, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_MWS_APPLY]);
			//	return DatabaseAccess.SelectDatabase(strSQL, connectStr);
			//}
		}
	}
