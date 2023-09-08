//
// MwsKakinBatchToolAccess.cs
// 
// MWS課金バッチツール データベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/08/23 勝呂):新規作成
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

namespace CommonLib.DB.SqlServer.MwsKakinBatchTool
{
	public static class MwsKakinBatchToolAccess
	{
		/// <summary>
		/// 先月分のサービス申込情報の取得
		/// </summary>
		/// <param name="today">当日</param>
		/// <param name="cancelFlag">解約申込かどうか？</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>view_前月申込データ</returns>
		public static List<view_前月申込データ> Select_サービス申込情報(DateTime today, bool cancelFlag, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
													+ " A.apply_id"
													+ ", A.cp_id"
													+ ", U.customer_id"
													+ ", S.service_type_id"
													+ ", A.service_id"
													+ ", A.apply_type"
													+ ", A.apply_date"
													+ " FROM {0} as A"
													+ " LEFT JOIN {1} as S ON A.service_id = S.service_id"
													+ " LEFT JOIN {2} as U ON A.cp_id = U.cp_id"
													+ " WHERE A.apply_date >= cast(dateadd(day, 1, eomonth('{3}', -2)) as datetime)"
													+ " AND A.apply_date < cast(dateadd(day, 1, eomonth('{3}', -1)) as datetime)"
													+ " AND U.user_type = '0'"
													+ " AND A.system_flg<> '2'"
													+ " AND U.testuser_flg is null"
													+ " AND A.apply_type = '{4}'"
													+ " ORDER BY U.customer_id, A.service_id"
													, CharlieDatabaseDefine.SynonymName[CharlieDatabaseDefine.SynonymType.T_COUPLER_APPLY]
													, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_SERVICE]
													, CharlieDatabaseDefine.SynonymName[CharlieDatabaseDefine.SynonymType.T_COUPLER_PRODUCTUSER]
													, today.ToString(), (cancelFlag)? '1' : '0');

			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return view_前月申込データ.DataTableToList(dt);
		}

		/// <summary>
		/// 月額利用分の顧客利用情報の取得
		/// </summary>
		/// <param name="endDate">利用終了日</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>顧客利用情報</returns>
		public static List<T_CUSSTOMER_USE_INFOMATION> Select_T_CUSSTOMER_USE_INFOMATION_月額利用(DateTime endDate, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
												+ " [CUSTOMER_ID]"
												+ ", [SERVICE_TYPE_ID]"
												+ ", [SERVICE_ID]"
												+	", [GOODS_ID]"
												+ ", [APPLICATION_NO]"
												+ ", [KAKIN_START_DATE]"
												+ ", [USE_START_DATE]"
												+ ", [USE_END_DATE]"
												+ ", [CANCELLATION_DAY]"
												+ ", [CANCELLATION_PROCESSING_DATE]"
												+ ", [PAUSE_END_STATUS]"
												+ ", [DELETE_FLG]"
												+ ", [CREATE_DATE]"
												+ ", [CREATE_PERSON]"
												+ ", [UPDATE_DATE]"
												+ ", [UPDATE_PERSON]"
												+ ", [PERIOD_END_DATE]"
												+ ", [RENEWAL_FLG]"
												+ "  FROM {0} as CUI"
												+ " LEFT JOIN {1} as CL on CL.[fCliID] = CUI.[CUSTOMER_ID]"
												+ " WHERE CUI.[PAUSE_END_STATUS] = 0 AND CL.[fCliEnd] = 0 AND CUI.[USE_END_DATE] = '{2}'"
												+ " ORDER BY [CUSTOMER_ID], [SERVICE_ID]"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]
												, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]
												, endDate.ToString());

			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return T_CUSSTOMER_USE_INFOMATION.DataTableToList(dt);
		}
	}
}
