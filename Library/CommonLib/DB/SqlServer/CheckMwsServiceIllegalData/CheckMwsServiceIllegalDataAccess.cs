//
// CheckMwsServiceIllegalDataAccess.cs
//
// サービス申込情報更新処理 データベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// Ver1.02(2024/01/24 勝呂):販売店情報参照ビューから販売店コードを取得処理で例外エラー
// 
using CommonLib.BaseFactory.CheckMwsServiceIllegalData;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Charlie.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.DB.SqlServer.CheckMwsServiceIllegalData
{
	public static class CheckMwsServiceIllegalDataAccess
	{
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		//////////////////////////////
		// SELECT

		/// <summary>
		/// チェック用の顧客利用情報の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>顧客利用情報異常データ検出クラスリスト</returns>
		public static List<CheckUseCustomerInfo> GetCheckUseCustomerInfo(string connectStr)
		{
			DataTable table = CheckMwsServiceIllegalDataGetIO.GetCheckUseCustomerInfo(connectStr);
			return CheckUseCustomerInfo.DataTableToList(table);
		}

		/// <summary>
		/// チェック用の顧客利用情報の顧客IDリストの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>顧客IDリスト</returns>
		public static List<int> GetCuiCustomerIdList(string connectStr)
		{
			DataTable table = CheckMwsServiceIllegalDataGetIO.GetCuiCustomerIdList(connectStr);
			if (null != table && 0 < table.Rows.Count)
			{
				List<int> result = new List<int>();
				foreach (DataRow row in table.Rows)
				{
					result.Add(DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]));
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// サービス利用期間が正しくない顧客利用情報の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>顧客利用情報異常データ検出クラスリスト</returns>
		public static List<CheckUseCustomerInfo> GetIllegalCuiServiceTerm(string connectStr)
		{
			DataTable table = CheckMwsServiceIllegalDataGetIO.GetIllegalCuiServiceTerm(connectStr);
			return CheckUseCustomerInfo.DataTableToList(table);
		}
	}
}
