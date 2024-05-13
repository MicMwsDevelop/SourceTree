//
// OnlineLicenseHomonGetIO.cs
//
// オン資格訪問診療サービス データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/03/21 勝呂):新規作成
// 
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.Junp;
using System.Data;

namespace CommonLib.DB.SqlServer.OnlineLicenseHomon
{
	static public class OnlineLicenseHomonGetIO
	{
		/// <summary>
		/// オン資格訪問診療契約情報からオン資格訪問診療売上情報の取得
		/// </summary>
		/// <param name="applyDate">申込日時</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>レコード数</returns>
		public static DataTable GetOnlineLicenseHomonEarningsOut(Date applyDate, string connectStr)
		{
			string strSQL = string.Format(@"SELECT"
									+ " H.[ApplyNo] as 受付No"
									+ ",H.[CustomerID] as 顧客No"
									+ ",U.[顧客名１] + U.[顧客名２] as 顧客名"
									+ ",U.[得意先No] as 得意先コード"
									+ ",U.[請求先コード] as 請求先コード"
									+ ",U.[請求先名] as 請求先名"
									+ ",H.[GoodsID] as 商品コード"
									+ ",S.[sms_mei] as 商品名"
									+ ",CONVERT(int, S.[sms_hyo]) as 標準価格"
									+ ",CONVERT(int, S.[sms_gen]) as 原単価"
									+ ",S.[sms_tani] as 単位"
									+ ",B.[fPca部門コード] as PCA部門コード"
									+ ",B.[fPca倉庫コード] as PCA倉庫コード"
									+ ",B.[f担当者コード] as PCA担当者コード"
									+ ",H.[ApplyDate] as 申込日時"
									+ ",H.[ContractStartDate] as 契約開始日"
									+ ",H.[ContractEndDate] as 契約終了日"
									+ ",H.[SalesDate] as 売上日時"
									+ " FROM {0} as H"	// 0
									+ " INNER JOIN {1} as U on U.[顧客No] = H.[CustomerID]"	// 1
									+ " LEFT JOIN {2} as S on S.[sms_scd] = H.[GoodsID]"	// 2
									+ " LEFT JOIN {3} as B on B.[fBshCode3] = U.[支店コード]"	// 3
									+ " WHERE H.[DeleteFlag] = '0' AND U.[終了フラグ] = '0' AND CONVERT(int, CONVERT(NVARCHAR, H.[ApplyDate], 112)) between {4} AND {5}"
									+ " ORDER BY H.[ApplyNo]"
									, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_ONLINE_HOMON]	// 0
									, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー2]	// 1
									, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]		// 2
									, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]	// 3
									, applyDate.FirstDayOfTheMonth().ToIntYMD()	// 4 当月初日
									, applyDate.LastDayOfTheMonth().ToIntYMD());	// 5 当月末日

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}
	}
}
