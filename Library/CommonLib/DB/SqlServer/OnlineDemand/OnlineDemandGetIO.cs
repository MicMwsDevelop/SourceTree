//
// OnlineDemandGetIO.cs
//
// オンライン請求作業作業売上データ作成 データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/12/01 勝呂):新規作成
// 
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.Junp;
using System.Data;

namespace CommonLib.DB.SqlServer.OnlineDemand
{
	public static class OnlineDemandGetIO
	{
		/// <summary>
		/// オンライン請求作業済申請情報から先月分の情報を取得
		/// </summary>
		/// <param name="prevMonth">先月</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>結果</returns>
		public static DataTable GetOnlineDemandEarningsOut(YearMonth prevMonth, string connectStr)
		{
			string strSQL = string.Format(@"SELECT"
									+ " D.[ApplyNo] as 受付No"
									+ ", D.[ApplyDate] as 申請日時"
									+ ", D.[CustomerID] as 顧客No"
									+ ", U.[顧客名１] + U.[顧客名２] as 顧客名"
									+ ", U.[得意先No] as 得意先コード"
									+ ", U.[請求先コード] as 請求先コード"
									+ ", '月' as 更新単位"
									+ ", D.[GoodsID] as 商品コード"
									+ ", S.[sms_mei] as 商品名"
									+ ", convert(int, S.[sms_hyo]) as 標準価格"
									+ ", convert(int, S.[sms_gen]) as 原単価"
									+ ", S.[sms_tani] as 単位"
									+ ", B.[fPca部門コード] as PCA部門コード"
									+ ", B.[fPca倉庫コード] as PCA倉庫コード"
									+ ", B.[f担当者コード] as PCA担当者コード"
									+ " FROM {0} as D"
									+ " INNER JOIN {1} as U on U.[顧客No] = D.[CustomerID]"
									+ " LEFT JOIN {2} as S on S.[sms_scd] = D.[GoodsID]"
									+ " LEFT JOIN {3} as B on B.[fBshCode3] = U.[支店コード]"
									+ " WHERE U.[終了フラグ] = '0' AND (D.[DeleteFlag] is null OR D.[DeleteFlag] = '0') AND (convert(int, convert(nvarchar, D.[ApplyDate], 112)) BETWEEN {4} AND {5})"
#if !DEBUG
									+ " AND D.[SalesDate] is null"
#endif
									+ " ORDER BY 顧客No, 申請日時"
									, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_ONLINE_DEMAND]		// 0
									, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー2]	// 1
									, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]		// 2
									, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]		// 3
									, prevMonth.First.ToIntYMD()		// 4 先月初日
									, prevMonth.Last.ToIntYMD());		// 5 先月末日

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}
	}
}
