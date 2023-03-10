//
// SoftwareMainteEarningsGetIO.cs
//
// ソフトウェア保守料売上データ作成 データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/10/09 勝呂)
// 
using CommonLib.BaseFactory;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.Junp;
using System.Data;

namespace CommonLib.DB.SqlServer.SoftwareMainteEarnings
{
	/// <summary>
	/// ソフトウェア保守料売上データ作成  データ取得クラス
	/// </summary>
	public static class SoftwareMainteEarningsGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 顧客Noに売上データ必須情報の取得
		/// </summary>
		/// <param name="customerID">顧客No</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>レコード数</returns>
		public static DataTable GetSoftwareMainteEarningsOut(int customerID, string connectStr)
		{
			string strSQL = string.Format(@"SELECT"
										+ "  U.顧客No as f顧客No"
										+ ", (U.顧客名１ + U.顧客名２) as f顧客名"
										+ ", U.得意先No as f得意先コード"
										+ ", U.請求先コード as f請求先コード"
										+ ", B.fPca部門コード as fPca部門コード"
										+ ", B.fPca倉庫コード as fPca倉庫コード"
										+ ", B.f担当者コード as fPca担当者コード"
										+ ", S.sms_scd as f商品コード"
										+ ", S.sms_mei as f商品名"
										+ ", convert(int, S.sms_hyo) as f標準価格"
										+ ", convert(int, S.sms_gen) as f原単価"
										+ ", S.sms_tani as f単位"
										+ " FROM {0} as U"
										+ " INNER JOIN {1} as B on B.fBshCode3 = U.支店コード"
										+ " INNER JOIN {2} as S on S.sms_scd = '{3}'"
										+ " WHERE U.顧客No = {4}"
									, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー2]
									, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]
									, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]
									, PcaGoodsIDDefine.PaletteES_Mainte12
									, customerID);

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}


		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// ソフトウェア保守料１年 自動更新対象利用情報の取得
		/// 条件：ソフトウェア保守料１年の利用終了日が当月末日 and ソフトウェア保守料１年の利用終了日がpalette ESの利用終了日と違う
		/// </summary>
		/// <param name="today">当日</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>レコード数</returns>
		public static DataTable GetCustomerUseInfoSoftwareMainte12(Date today, string connectStr)
		{
			string strSQL = string.Format(@"SELECT"
										+ " SOFT.[CUSTOMER_ID]"
										+ ",SOFT.[SERVICE_ID]"
										+ ",[USE_START_DATE]"
										+ ",SOFT.[USE_END_DATE]"
										+ ",[CANCELLATION_DAY]"
										+ ",[PAUSE_END_STATUS]"
										+ ",[PERIOD_END_DATE]"
										+ ",ES.[USE_END_DATE] AS ES_USE_END_DATE"
										+ " FROM {0} AS SOFT"
										+ " LEFT JOIN"
										+ " ("
										+ " SELECT"
										+ " [CUSTOMER_ID]"
										+ ", [USE_END_DATE]"
										+ " FROM {0}"
										+ " WHERE [SERVICE_ID] = {1} AND convert(int, convert(nvarchar, [USE_END_DATE], 112)) > {2}"   // 当月末日
										+ ") AS ES ON SOFT.CUSTOMER_ID = ES.CUSTOMER_ID"
										+ " WHERE SOFT.[SERVICE_ID] = {3} AND SOFT.[USE_END_DATE] <> ES.[USE_END_DATE] AND convert(int, convert(nvarchar, SOFT.[USE_END_DATE], 112)) = {2}" // 当月末日
										+ " ORDER BY [CUSTOMER_ID] ASC"
										, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]
										, (int)ServiceCodeDefine.ServiceCode.PaletteES
										, today.LastDayOfTheMonth().ToIntYMD()
										, (int)ServiceCodeDefine.ServiceCode.SoftwareMainte1);

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}
	}
}
