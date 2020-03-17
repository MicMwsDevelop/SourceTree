//
// SoftwareMainteSaleDataAccess.cs
//
// ソフトウェア保守料売上データ作成 ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/11/19 勝呂)
// 
using MwsLib.BaseFactory.SoftwareMainteSaleData;
using MwsLib.Common;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.SoftwareMainteSaleData
{
	/// <summary>
	/// ソフトウェア保守料売上データ作成 ファイルアクセスクラス
	/// </summary>
	public static class SoftwareMainteSaleDataAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 顧客Noに対するソフトウェア保守料の受注情報の取得
		/// </summary>
		/// <param name="customerID">顧客No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>受注情報リスト</returns>
		public static List<OrderSlipSoftwareMainte> GetSoftwareMainteOrderSlip(int customerID, bool sqlsv2 = false)
		{
			DataTable dt = SoftwareMainteSaleDataGetIO.GetSoftwareMainteOrderSlip(customerID, sqlsv2);
			return SoftwareMainteSaleDataController.ConvertOrderSlipSoftwareMainte(dt);
		}


		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// ソフトウェア保守料１年 自動更新対象利用情報の取得
		/// </summary>
		/// <param name="today">当日</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>PC安心サポート管理情報</returns>
		public static List<CustomerUseInfoSoftwareMainte> GetCustomerUseInfoSoftwareMainte1(Date today, bool sqlsv2 = false)
		{
			DataTable dt = SoftwareMainteSaleDataGetIO.GetCustomerUseInfoSoftwareMainte1(today, sqlsv2);
			return SoftwareMainteSaleDataController.ConvertCustomerUseInfoSoftwareMainte(dt);
		}
	}
}
