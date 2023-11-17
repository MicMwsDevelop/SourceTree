//
// AdjustServiceApplyGetIO.cs
//
// サービス申込情報更新処理 データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.Junp;
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
														, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.WW伝票参照ビュー]);	// 0
			return DatabaseAccess.SelectDatabaseTimeOut(strSQL, connectStr, 600);
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
														, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.販売店区分参照ビュー]	// 0
														, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.販売店情報参照ビュー]	// 1
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
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_CODE]				// 0
												, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.V_PCA_GOODS]	);	// 1
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
													+ " SELECT max([FILE_CREATEDATE]) as 最終出力日時"
													+ " FROM {1}"
													+ " WHERE FILE_TYPE = 2"
													+ " GROUP BY FILE_TYPE"
													+ ") as L"
													+ " WHERE CUI.[SERVICE_TYPE_ID] <> 1"
													+ " AND CUI.[USE_START_DATE] is not null"
													+ " AND CUI.[USE_END_DATE] is not null"
													+ " AND (CUI.[CREATE_DATE] > 最終出力日時 OR CUI.[UPDATE_DATE] > 最終出力日時)"
													+ " ORDER BY CUSTOMER_ID, SERVICE_ID"
													, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]	// 0
													, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_FILE_CREATEDATE]);					// 1
			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// 前回同期日時以降の顧客情報の取得（MWSユーザー）
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static DataTable GetMwsUserAfterSynchroTime(string connectStr)
		{
			string strSQL = string.Format(@"SELECT"
												+ " PC.[PRODUCT_ID] as cp_id"
												+ ", PC.[USER_CLASSIFICATION] as user_type"
												+ ", PC.[TRIAL_FLG] as trial_flg"
												+ ", CASE PC.[END_FLG] WHEN '2' THEN '1' ELSE PC.[END_FLG] END as end_flg"
												+ ", PC.[CUSTOMER_ID] as customer_id"
												+ ", LEFT(CL.[fCliName] + B.[fkj顧客名２], 40) as customer_nm"
												+ ", BR.[fメールアドレス] as email1"
												+ ", (SELECT TOP 1 [MAIL_ADDRESS] FROM {0}) as email2"
												+ ", PC.[TRIAL_START_DATE] as login_start_date"
												+ ", iif(PC.[PERIOD_END_DATE] is null, '2999-12-31 23:59:59.000', PC.[PERIOD_END_DATE]) as login_end_date"
												+ ", PC.[PASSWORD] as default_paswd"
												+ ", iif(MU.[fus同時接続ｸﾗｲｱﾝﾄ数] is null, 0, MU.[fus同時接続ｸﾗｲｱﾝﾄ数]) as license_count"
												+ ", iif(LTRIM(RTRIM(MU.[fusシステム名])) is null, 999, iif(LTRIM(RTRIM(MU.[fusシステム名])) = '', 999, convert(int, LTRIM(RTRIM(MU.[fusシステム名]))))) as system_code"
												+ ", CF.[DELETE_FLG] as delete_flg"
												+ " FROM {1} as PC"
												+ " LEFT JOIN {2} as CL on CL.[fCliID] = PC.[CUSTOMER_ID]"
												+ " LEFT JOIN {3} as B on B.[fkjCliMicID] = PC.[CUSTOMER_ID]"
												+ " LEFT JOIN {4} as MU on MU.[fusCliMicID] = PC.[CUSTOMER_ID]"
												+ " LEFT JOIN {5} as U on U.[fUsrID] = CL.[fCliFirstcMan]"
												+ " LEFT JOIN {6} as BR on BR.[fBshCode1] = U.[fUsrBusho1] AND BR.[fBshCode2] = U.[fUsrBusho2] AND BR.[fBshCode3] = U.[fUsrBusho3]"
												+ " LEFT JOIN {7} as CF on PC.[CUSTOMER_ID] = CF.[CUSTOMER_ID]"
												+ " CROSS JOIN"
												+ " ("
												+ "SELECT max([FILE_CREATEDATE]) as 最終出力日時"
												+ " FROM {8}"
												+ " WHERE [FILE_TYPE] = 1"
												+ " GROUP BY [FILE_TYPE]"
												+ ") as L"
												+ " WHERE PC.[TRIAL_FLG] = 0 AND PC.[CUSTOMER_ID] is not null AND PC.[USER_CLASSIFICATION] <= 1 AND PC.[TRIAL_START_DATE] is not null"
												+ " AND (PC.[UPDATE_DATE] > 最終出力日時 OR CL.[fCliUpdate] > 最終出力日時 OR B.[fkj更新日] > 最終出力日時 OR MU.[fus更新日] > 最終出力日時)"
												+ " ORDER BY customer_id"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_MAIL]	// 0
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_PRODUCT_CONTROL]	// 1
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]	// 2
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik基本情報]	// 3
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ]	// 4
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tUser]	// 5
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]   // 6
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSTOMER_FOUNDATIONS]    // 7
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_FILE_CREATEDATE]);    // 8

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// 前回同期日時以降の顧客情報の取得（体験版ユーザー）
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static DataTable GetTrialUserAfterSynchroTime(string connectStr)
		{
			string strSQL = string.Format(@"SELECT"
												+ " PC.[PRODUCT_ID] as cp_id"
												+ ", PC.[USER_CLASSIFICATION] as user_type"
												+ ", PC.[TRIAL_FLG] as trial_flg"
												+ ", CASE PC.[END_FLG] WHEN '2' THEN '1' ELSE PC.[END_FLG] END as end_flg"
												+ ", PC.[CUSTOMER_ID] as customer_id"
												+ ", LEFT(CL.[fCliName] + B.[fkj顧客名２], 40) as customer_nm"
												+ ", BR.[fメールアドレス] as email1"
												+ ", (SELECT TOP 1 [MAIL_ADDRESS] FROM {0}) as email2"
												+ ", PC.[TRIAL_START_DATE] as login_start_date"
												+ ", iif(PC.[PERIOD_END_DATE] is null, '2999-12-31 23:59:59.000', PC.[PERIOD_END_DATE]) as login_end_date"
												+ ", PC.[PASSWORD] as default_paswd"
												+ ", iif(MU.[fus同時接続ｸﾗｲｱﾝﾄ数] is null, 0, MU.[fus同時接続ｸﾗｲｱﾝﾄ数]) as license_count"
												+ ", iif(LTRIM(RTRIM(MU.[fusシステム名])) is null, 999, iif(LTRIM(RTRIM(MU.[fusシステム名])) = '', 999, convert(int, LTRIM(RTRIM(MU.[fusシステム名]))))) as system_code"
												+ ", CF.[DELETE_FLG] as delete_flg"
												+ " FROM {1} as PC"
												+ " LEFT JOIN {2} as CL on CL.[fCliID] = PC.[CUSTOMER_ID]"
												+ " LEFT JOIN {3} as B on B.[fkjCliMicID] = PC.[CUSTOMER_ID]"
												+ " LEFT JOIN {4} as MU on MU.[fusCliMicID] = PC.[CUSTOMER_ID]"
												+ " LEFT JOIN {5} as U on U.[fUsrID] = CL.[fCliFirstcMan]"
												+ " LEFT JOIN {6} as BR on BR.[fBshCode1] = U.[fUsrBusho1] AND BR.[fBshCode2] = U.[fUsrBusho2] AND BR.[fBshCode3] = U.[fUsrBusho3]"
												+ " LEFT JOIN {7} as CF on PC.[CUSTOMER_ID] = CF.[CUSTOMER_ID]"
												+ " CROSS JOIN"
												+ " ("
												+ "SELECT max([FILE_CREATEDATE]) as 最終出力日時"
												+ " FROM {8}"
												+ " WHERE [FILE_TYPE] = 1"
												+ " GROUP BY [FILE_TYPE]"
												+ ") as L"
												+ " WHERE PC.[TRIAL_FLG] = 1 AND PC.[USER_CLASSIFICATION] <= 1 AND PC.[TRIAL_START_DATE] is not null"
												+ " AND (PC.[UPDATE_DATE] > 最終出力日時 OR CL.[fCliUpdate] > 最終出力日時 OR B.[fkj更新日] > 最終出力日時 OR MU.[fus更新日] > 最終出力日時)"
												+ " ORDER BY customer_id"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_MAIL]   // 0
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_PRODUCT_CONTROL]    // 1
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]    // 2
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik基本情報]   // 3
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ]    // 4
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tUser]  // 5
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]   // 6
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSTOMER_FOUNDATIONS]    // 7
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_FILE_CREATEDATE]);    // 8

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// 前回同期日時以降の顧客情報の取得（社員用ユーザーAAA、デモ用ユーザーADM）
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static DataTable GetDemoUserAfterSynchroTime(string connectStr)
		{
			string strSQL = string.Format(@"SELECT"
												+ " PC.[PRODUCT_ID] as cp_id"
												+ ", PC.[USER_CLASSIFICATION] as user_type"
												+ ", PC.[TRIAL_FLG] as trial_flg"
												+ ", CASE PC.[END_FLG] WHEN '2' THEN '1' ELSE PC.[END_FLG] END as end_flg"
												+ ", PC.[CUSTOMER_ID] as customer_id"
												+ ", LEFT(DU.[NAME], 40) as customer_nm"
												+ ", DU.[MAILADDR1] as email1"
												+ ", DU.[MAILADDR2] as email2"
												+ ", PC.[TRIAL_START_DATE] as login_start_date"
												+ ", iif(PC.[PERIOD_END_DATE] is null, '2999-12-31 23:59:59.000', PC.[PERIOD_END_DATE]) as login_end_date"
												+ ", PC.[PASSWORD] as default_paswd"
												+ ",'10' as license_count"
												+ ",'100' as system_code"
												+ ", DU.DELETE_FLG as delete_flg"
												+ " FROM {0} as PC"
												+ " LEFT JOIN {1} as DU on DU.[CUSTOMER_ID] = PC.[CUSTOMER_ID]"
												+ " CROSS JOIN"
												+ " ("
												+ "SELECT max([FILE_CREATEDATE]) as 最終出力日時"
												+ " FROM {2}"
												+ " WHERE [FILE_TYPE] = 1"
												+ " GROUP BY [FILE_TYPE]"
												+ ") as L"
												+ " WHERE PC.[CUSTOMER_ID] is not null AND PC.[USER_CLASSIFICATION] >= 2 AND PC.[TRIAL_START_DATE] is not null"
												+ " AND DU.[END_FLG] = 0 AND DU.[DELETE_FLG] = 0"
												+ " AND (PC.[UPDATE_DATE] > 最終出力日時 OR DU.[UPDATE_DATE] > 最終出力日時)"
												+ " ORDER BY customer_id"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_PRODUCT_CONTROL]	// 0
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_DEMO_USER]				// 1
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_FILE_CREATEDATE]);	// 2

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}

		/// <summary>
		/// 利用情報利用日確認情報の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static DataTable GetCheckCuiUseDate(string connectStr)
		{
			string strSQL = string.Format(@"SELECT CUI.[CUSTOMER_ID] as customer_id"
												+ ", PC.[PRODUCT_ID] as cp_id"
												+ ", CL.[fCliName] as customer_name"
												+ ", CUI.[SERVICE_ID] as service_id"
												+ ", MS.[SERVICE_NAME] as service_name"
												+ ", CUI.[PAUSE_END_STATUS] as pause_end_status"
												+ ", CUI.[USE_START_DATE] as use_start_date"
												+ ", CUI.[USE_END_DATE] as use_end_date"
												+ ", CUI.[DELETE_FLG] as delete_flg"
												+ ", '0' as set_sale"
												+ " FROM {0} as CUI"	// 0
												+ " LEFT JOIN {1} as PC ON CUI.[CUSTOMER_ID] = PC.[CUSTOMER_ID] AND PC.[USER_CLASSIFICATION] IN ('0', '1')"	// 1
												+ " LEFT JOIN {2} as MS ON CUI.[SERVICE_ID] = MS.[SERVICE_ID]"	// 2
												+ " LEFT JOIN {3} as CL on CL.[fCliID] = CUI.[CUSTOMER_ID]"	// 3
												+ " WHERE CUI.[DELETE_FLG] = 0"
												+ " AND MS.[UMU_FLG] = 0"
												+ " AND PC.[USER_CLASSIFICATION] IN ('0', '1')"
												+ " AND PC.[PRODUCT_ID] is not null"
												+ " AND (CUI.[USE_START_DATE] is null OR CUI.[USE_END_DATE] is null)"
												+ " ORDER BY customer_id, service_id"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]    // 0
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_PRODUCT_CONTROL]              // 1
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_SERVICE]  // 2
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]);  // 3

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}
	}
}
