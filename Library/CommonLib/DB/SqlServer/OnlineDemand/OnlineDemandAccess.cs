//
// OnlineDemandAccess.cs
//
// オンライン請求作業作業売上データ作成 ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/12/01 勝呂):新規作成
// Ver1.07(2024/07/29 勝呂):売上日を申請月の末日に変更する
// Ver1.08(2024/08/01 勝呂):各種作業料作業済申請情報の更新時の障害対応
// 
using CommonLib.BaseFactory.OnlineDemand;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.DB.SqlServer.OnlineDemand
{
	public static class OnlineDemandAccess
	{
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 各種作業料作業済申請情報から先月分の情報を取得
		/// </summary>
		/// <param name="prevMonth">先月</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>各種作業料作業済申請情報リスト</returns>
		public static List<OnlineDemandEarningsOut> GetOnlineDemandEarningsOut(YearMonth prevMonth, string connectStr)
		{
			DataTable dt = OnlineDemandGetIO.GetOnlineDemandEarningsOut(prevMonth, connectStr);
			return OnlineDemandEarningsOut.DataTableToList(dt);
		}

		/// <summary>
		/// 各種作業料作業済申請情報 売上日時の設定
		/// </summary>
		/// <param name="sale">各種作業料作業済申請情報</param>
		/// <param name="saleDate">売上日</param>
		/// <param name="procName">アプリ名</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果行数</returns>
		// Ver1.07(2024/07/29 勝呂):売上日を申請月の末日に変更する
		// Ver1.08(2024/08/01 勝呂):各種作業料作業済申請情報の更新時の障害対応
		//public static int UpdateSetOnlineDemandSaleDate(OnlineDemandEarningsOut sale, string procName, string connectStr)
		public static int UpdateSetOnlineDemandSaleDate(OnlineDemandEarningsOut sale, Date saleDate, string procName, string connectStr)
		{
			string updateStr = string.Format(@"UPDATE {0} SET [SalesDate] = @1, [UpdateDate] = @2, [UpdatePerson] = @3"
								+ " WHERE [ApplyNo] = {1}"
								, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_ONLINE_DEMAND]
								, sale.受付No);

			SqlParameter[] param = {
				new SqlParameter("@1", saleDate.ToDateTime()),		// SalesDate
				new SqlParameter("@2", DateTime.Now),		// UpdateDate
				new SqlParameter("@3", procName)			// UpdatePerson
            };
			return DatabaseAccess.UpdateSetDatabase(updateStr, param, connectStr);
		}
	}
}
