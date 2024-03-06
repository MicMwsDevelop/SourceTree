//
// CheckMwsServiceIllegalDataGetIO.cs
//
// 顧客利用情報 異常データ検出クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.Junp;
using System.Data;

namespace CommonLib.DB.SqlServer.CheckMwsServiceIllegalData
{
	public static class CheckMwsServiceIllegalDataGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// チェック用の顧客利用情報の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static DataTable GetCheckUseCustomerInfo(string connectStr)
		{
			string strSQL = string.Format(@"SELECT"
														+ " CUI.[CUSTOMER_ID] as CustomerID"
														+ ", CL.[fCliName] as CustomerName"
														+ ", CUI.[SERVICE_ID] as ServiceID"
														+ ", SV.[SERVICE_NAME] as ServiceName"
														+ ", CUI.[USE_START_DATE] as UseStartDate"
														+ ", CUI.[USE_END_DATE] as UseEndtDate"
														+ ", CUI.[PAUSE_END_STATUS] as PauseEndStatus"
														+ ", CUI.[CREATE_DATE] as CreateDate"
														+ ", CUI.[CREATE_PERSON] as CreatePerson"
														+ ", CUI.[UPDATE_DATE] as UpdateDate"
														+ ", CUI.[UPDATE_PERSON] as UpdatePerson"
														+ ", CUI.[PERIOD_END_DATE] as PeriodEndDate"
														+ " FROM {0} as CUI"
														+ " LEFT JOIN {1} as CL on CL.[fCliID] = CUI.[CUSTOMER_ID]"
														+ " LEFT JOIN {2} as SV on CUI.SERVICE_ID = SV.[SERVICE_ID]"
														+ " LEFT JOIN {3} as PC on PC.[CUSTOMER_ID] = CUI.[CUSTOMER_ID]"
														+ " WHERE CUI.[DELETE_FLG] = 0 AND CL.[fCliEnd] = 0 AND PC.[END_FLG] = 0 AND CUI.[SERVICE_ID] not in (1028120, 1030120)"
														+ " ORDER BY CUI.[CUSTOMER_ID], CUI.[SERVICE_ID]"
														, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]	// 0
														, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]	// 1
														, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_SERVICE]		// 2
														, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_PRODUCT_CONTROL]);   // 3
			return DatabaseAccess.SelectDatabaseTimeOut(strSQL, connectStr, 600);
		}

		/// <summary>
		/// チェック用の顧客利用情報の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static DataTable GetCuiCustomerIdList(string connectStr)
		{
			string strSQL = string.Format(@"SELECT"
														+ " CUI.[CUSTOMER_ID]"
														+ " FROM {0} as CUI"
														+ " LEFT JOIN {1} as CL on CL.[fCliID] = CUI.[CUSTOMER_ID]"
														+ " LEFT JOIN {2} as PC on PC.[CUSTOMER_ID] = CUI.[CUSTOMER_ID]"
														+ " WHERE CUI.[DELETE_FLG] = 0 AND CL.[fCliEnd] = 0 AND PC.[END_FLG] = 0"
														+ " GROUP BY CUI.[CUSTOMER_ID]"
														+ " ORDER BY CUI.[CUSTOMER_ID]"
														, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]   // 0
														, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]	// 1
														, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_PRODUCT_CONTROL]);   // 2
			return DatabaseAccess.SelectDatabaseTimeOut(strSQL, connectStr, 600);
		}
	}
}
