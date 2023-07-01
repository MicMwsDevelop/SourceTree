//
// PcaInvoiceDataConverterGetIO.cs
//
// PCA請求データコンバータ データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/27 勝呂):新規作成
// 
using CommonLib.DB.SqlServer.Junp;
using System.Data;

namespace CommonLib.DB.SqlServer.PcaInvoiceDataConverter
{
	public static class PcaInvoiceDataConverterGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 顧客情報の抽出
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static DataTable GetCustomerInfo(string connectStr)
		{
			string strSQL = string.Format(@"SELECT"
												+ " B.fkj得意先情報 as 得意先No"
												+ ", C.fCliID as 顧客No"
												+ ", C.fCliName as 顧客名１"
												+ ", ISNULL(B.fkj顧客名２,'') as 顧客名２"
												+ ", ISNULL(D.fdaAPLUSコード,'') as APLUSコード"
												+ ", ISNULL(D.fda銀行コード,'') as 銀行コード"
												+ ", ISNULL(D.fda銀行名カナ,'') as 銀行名カナ"
												+ ", ISNULL(D.fda支店コード,'') as 支店コード"
												+ ", ISNULL(D.fda支店名カナ,'') as 支店名カナ"
												+ ", RTRIM(isnull(D.fda預金種別,'')) as 預金種別"
												+ ", ISNULL(D.fda口座番号,'') as 口座番号"
												+ ", ISNULL(D.fda預金者名,'') as 預金者名"
												+ ", U.fusオプション1 as レセコン区分"
												+ " FROM {0} as U"
												+ " INNER JOIN {1} as C on C.fCliID = U.fusCliMicID"
												+ " INNER JOIN {2} as B on B.fkjCliMicID = U.fusCliMicID"
												+ " INNER JOIN {3} as T on T.fptCliMicID = U.fusCliMicID"
												+ " LEFT JOIN {4} as D on D.fdaCliMicID = U.fusCliMicID AND D.fda銀行コード <> '' AND D.fda状態 = '継続'"
												+ " WHERE B.fkj削除フラグ = '0' AND (B.fkj顧客区分 = 2 OR B.fkj顧客区分 = 18) AND ISNULL(B.fkj得意先情報,'') <> ''"
												+ " ORDER BY B.fkj得意先情報"
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ]
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik基本情報]
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikPca得意先]
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik代行回収]);
			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}
		/*
				//////////////////////////////////////////////////////////////////
				/// CharlieDB
				//////////////////////////////////////////////////////////////////

				/// <summary>
				/// 販売店情報参照ビューから販売店コードを取得
				/// </summary>
				/// <param name="storeCode">販売店コード</param>
				/// <param name="connectStr">SQL Server接続文字列</param>
				/// <returns>DataTable</returns>
				public static DataTable GetStoreCode(int storeCode, string connectStr)
				{
					string strSQL = string.Format(@"SELECT SI.[販売店コード]"
																+ " FROM {0} as VR"
																+ " INNER JOIN {1} as SI ON VR.[区分コード] = CONVERT(int, SI.[販売店ランクコード]) AND VR.[ランク] is not null"
																+ " WHERE SI.[販売店コード] = {2}"
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
														+ " INNER JOIN {1} as B on A.GOODS_ID = B.GOODS_ID AND B.[BRAND_CLASSIFICATION] = 200"
														+ " WHERE A.DELETE_FLG = '0' AND SET_SALE = '1'"
														+ " ORDER BY A.[GOODS_ID]"
														, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_CODE]
														, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.V_PCA_GOODS]	);
					return DatabaseAccess.SelectDatabase(strSQL, connectStr);
				}

				/// <summary>
				/// 顧客利用情報から利用申込サービスの取得
				/// </summary>
				/// <param name="compDateTime">前回取得日時</param>
				/// <param name="connectStr">SQL Server接続文字列</param>
				/// <returns>DataTable</returns>
				public static DataTable GetCustomerUseInformationUseService(DateTime compDateTime, string connectStr)
				{
					string strSQL = string.Format(@"SELECT * FROM {0}"
														+ " WHERE [SERVICE_TYPE_ID] <> 1"	// 基本サービス以外
														+ " AND [PAUSE_END_STATUS] = 0"		// 課金対象外フラグがOFF
														+ " AND [USE_START_DATE] is not null"	// 利用開始日がNULLでない
														+ " AND [PERIOD_END_DATE] is null"		// 利用終了日がNULL
														+ " AND [USE_START_DATE] <= EOMONTH(getdate(), 1) AND [USE_END_DATE] >= EOMONTH(getdate(), 1)	"	// 翌月末日のサービスが利用可能
														+ " AND [UPDATE_DATE] > '{1}'"
														+ " ORDER BY [CUSTOMER_ID], [SERVICE_ID]"
														, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]
														, compDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
					return DatabaseAccess.SelectDatabase(strSQL, connectStr);
				}

				/// <summary>
				/// 顧客利用情報から解約申込サービスの取得
				/// </summary>
				/// <param name="compDateTime">前回取得日時</param>
				/// <param name="connectStr">SQL Server接続文字列</param>
				/// <returns>DataTable</returns>
				public static DataTable GetCustomerUseInformationCancelService(DateTime compDateTime, string connectStr)
				{
					string strSQL = string.Format(@"SELECT * FROM {0}"
														+ " WHERE [SERVICE_TYPE_ID] <> 1"	// 基本サービス以外
														+ " AND [PAUSE_END_STATUS] = 1"		// 課金対象外フラグがON
														+ " AND [USE_END_DATE] is not null"		// 課金終了日がNULLでない
														+ " AND [PERIOD_END_DATE] = EOMONTH(getdate())"	// 利用終了日が今月末日
														+ " AND [UPDATE_DATE] > '{1}'"
														+ " ORDER BY [CUSTOMER_ID], [SERVICE_ID]"
														, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]
														, compDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
					return DatabaseAccess.SelectDatabase(strSQL, connectStr);
				}

				/// <summary>
				/// 最終同期日時の取得
				/// </summary>
				/// <param name="connectStr">SQL Server接続文字列</param>
				/// <returns>DataTable</returns>
				public static DataTable GetLastSynchroTime(string connectStr)
				{
					string strSQL = string.Format(@"SELECT TOP 1 * FROM {0} WHERE [FILE_TYPE] = '2' ORDER BY [FILE_CREATEDATE] DESC"
														, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_FILE_CREATEDATE]);
					return DatabaseAccess.SelectDatabase(strSQL, connectStr);
				}

				/// <summary>
				/// サービス申込情報から利用申込サービスの取得
				/// </summary>
				/// <param name="connectStr">SQL Server接続文字列</param>
				/// <returns>DataTable</returns>
				public static DataTable GetMwsApplyUserService(string connectStr)
				{
					string strSQL = string.Format(@"SELECT * FROM {0}"
														+ " WHERE [system_flg] = '0' AND [apply_type] = '0' AND [customer_id] < 30000000"
														+ " ORDER BY [customer_id], [service_id]"
														, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_MWS_APPLY]);
					return DatabaseAccess.SelectDatabase(strSQL, connectStr);
				}

				/// <summary>
				/// サービス申込情報から解約申込サービスの取得
				/// </summary>
				/// <param name="connectStr">SQL Server接続文字列</param>
				/// <returns>DataTable</returns>
				public static DataTable GetMwsApplyCancelService(string connectStr)
				{
					string strSQL = string.Format(@"SELECT * FROM {0}"
														+ " WHERE [system_flg] = '0' AND [apply_type] = '1' AND [customer_id] < 30000000"
														+ " ORDER BY [customer_id], [service_id]"
														, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_MWS_APPLY]);
					return DatabaseAccess.SelectDatabase(strSQL, connectStr);
				}
		*/
	}
}
