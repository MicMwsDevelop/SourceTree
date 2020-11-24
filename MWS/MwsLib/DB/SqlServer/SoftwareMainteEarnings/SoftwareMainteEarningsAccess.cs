//
// SoftwareMainteEarningsAccess.cs
//
// ソフトウェア保守料売上データ作成 ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/10/09 勝呂)
// 
using MwsLib.BaseFactory.SoftwareMainteEarnings;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Charlie;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.SoftwareMainteEarnings
{
	/// <summary>
	/// ソフトウェア保守料売上データ作成 ファイルアクセスクラス
	/// </summary>
	public static class SoftwareMainteEarningsAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 顧客Noに売上データ必須情報の取得
		/// </summary>
		/// <param name="customerID">顧客No</param>
		/// <param name="ct">CT環境</param>
		/// <returns>受注情報リスト</returns>
		public static SoftwareMainteEarningsOut GetSoftwareMainteEarningsOut(int customerID, bool ct = false)
		{
			DataTable dt = SoftwareMainteEarningsGetIO.GetSoftwareMainteEarningsOut(customerID, ct);
			return SoftwareMainteEarningsController.ConvertSoftwareMainteEarningsOut(dt);
		}

		///// <summary>
		///// 顧客Noに対するソフトウェア保守料の受注情報の取得
		///// </summary>
		///// <param name="customerID">顧客No</param>
		///// <param name="ct">CT環境</param>
		///// <returns>受注情報リスト</returns>
		//public static OrderSlipSoftwareMainte GetSoftwareMainteOrderSlip(int customerID, bool ct = false)
		//{
		//	DataTable dt = SoftwareMainteEarningsGetIO.GetSoftwareMainteOrderSlip(customerID, ct);
		//	return SoftwareMainteEarningsController.ConvertOrderSlipSoftwareMainte(dt);
		//}


		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// ソフトウェア保守料１年 自動更新対象利用情報の取得
		/// 条件：ソフトウェア保守料１年の利用終了日が当月末日 and ソフトウェア保守料１年の利用終了日がpalette ESの利用終了日と違う
		/// </summary>
		/// <param name="today">当日</param>
		/// <param name="ct">CT環境</param>
		/// <returns>利用情報リスト</returns>
		public static List<CustomerUseInfoSoftwareMainte> GetCustomerUseInfoSoftwareMainte12(Date today, bool ct = false)
		{
			DataTable dt = SoftwareMainteEarningsGetIO.GetCustomerUseInfoSoftwareMainte12(today, ct);
			return SoftwareMainteEarningsController.ConvertCustomerUseInfoSoftwareMainte(dt);
		}

		/// <summary>
		/// ソフトウェア保守料１年 自動更新対象利用情報の更新
		/// </summary>
		/// <param name="data">更新利用情報</param>
		/// <param name="updateUser">更新者</param>
		/// <param name="ct">CT環境</param>
		/// <returns>結果行数</returns>
		public static int UpdateSetCustomerUseInfo(CustomerUseInfoSoftwareMainte cui, string updateUser, bool ct = false)
		{
			return CharlieDatabaseAccess.UpdateSetCharlieDatabase(cui.UpdateSetSqlString, cui.GetUpdateSetParameters(updateUser, cui.USE_END_DATE.Value.PlusYears(1).LastDayOfTheMonth()), ct);
		}
	}
}
