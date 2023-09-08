//
// MwsCuiEditToolAccess.cs
// 
// 顧客利用情報編集ツール データベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/08/24 勝呂):新規作成
//
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Charlie.View;
using CommonLib.BuiData;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Sockets;
using CommonLib.BaseFactory.MwsCuiEditTool;

namespace CommonLib.DB.SqlServer.MwsCuiEditTool
{
	public static class MwsCuiEditToolAccess
	{
		/// <summary>
		/// 顧客利用情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>顧客利用情報</returns>
		public static DataTable GetDataTable_EditCustomerUseInformation(int customerNo, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
												+ " [CUSTOMER_ID]"
												+ ", CUI.[SERVICE_TYPE_ID]"
												+ ", CUI.[SERVICE_ID]"
												+ ", [SERVICE_NAME]"
												+	", [GOODS_ID]"
												+ ", [APPLICATION_NO]"
												+ ", [KAKIN_START_DATE]"
												+ ", [USE_START_DATE]"
												+ ", [USE_END_DATE]"
												+ ", [CANCELLATION_DAY]"
												+ ", [CANCELLATION_PROCESSING_DATE]"
												+ ", [PAUSE_END_STATUS]"
												+ ", CUI.[DELETE_FLG]"
												+ ", CUI.[CREATE_DATE]"
												+ ", CUI.[CREATE_PERSON]"
												+ ", CUI.[UPDATE_DATE]"
												+ ", CUI.[UPDATE_PERSON]"
												+ ", [PERIOD_END_DATE]"
												+ ", [RENEWAL_FLG]"
												+ "  FROM {0} as CUI"
												+ " LEFT JOIN {1} as CL on CL.[fCliID] = CUI.[CUSTOMER_ID]"
												+ " LEFT JOIN {2} as SV on SV.SERVICE_ID = CUI.[SERVICE_ID]"
												+ " WHERE CL.[fCliEnd] = 0 AND CUI.[CUSTOMER_ID] = {3}"
												+ " ORDER BY [SERVICE_ID]"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_SERVICE]
												, customerNo);

			return DatabaseAccess.SelectDatabase(sqlStr, connectStr);
		}
	}
}
