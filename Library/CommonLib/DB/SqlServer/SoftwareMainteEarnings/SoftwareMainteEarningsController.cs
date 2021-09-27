//
// SoftwareMainteEarningsController.cs
//
// ソフトウェア保守料売上データ作成 データテーブル詰め替えクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/10/09 勝呂)
// 
using CommonLib.DB;
using CommonLib.BaseFactory.SoftwareMainteEarnings;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.DB.SqlServer.SoftwareMainteEarnings
{
	/// <summary>
	/// ソフトウェア保守料売上データ作成 データテーブル詰め替えクラス
	/// </summary>
	public static class SoftwareMainteEarningsController
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// ソフトウェア保守料の売上必須情報の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>ソフト保守料情報</returns>
		public static SoftwareMainteEarningsOut ConvertSoftwareMainteEarningsOut(DataTable table)
		{
			if (null != table)
			{
				if (0 < table.Rows.Count)
				{
					DataRow row = table.Rows[0];
					SoftwareMainteEarningsOut ret = new SoftwareMainteEarningsOut
					{
						f顧客No = DataBaseValue.ConvObjectToInt(row["f顧客No"]),
						f顧客名 = row["f顧客名"].ToString().Trim(),
						f得意先コード = row["f得意先コード"].ToString().Trim(),
						f請求先コード = row["f請求先コード"].ToString().Trim(),
						fPca部門コード = (short)DataBaseValue.ConvObjectToIntNull(row["fPca部門コード"]),
						fPca担当者コード = row["fPca担当者コード"].ToString().Trim(),
						fPca倉庫コード = (short)DataBaseValue.ConvObjectToIntNull(row["fPca倉庫コード"]),
						f商品コード = row["f商品コード"].ToString().Trim(),
						f商品名 = row["f商品名"].ToString().Trim(),
						f標準価格 = DataBaseValue.ConvObjectToInt(row["f標準価格"]),
						f原単価 = DataBaseValue.ConvObjectToInt(row["f原単価"]),
						f単位 = row["f単位"].ToString().Trim(),
					};
					return ret;
				}
			}
			return null;
		}

		
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// ソフトウェア保守料利用情報の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>ソフトウェア保守料利用情報リスト</returns>
		public static List<CustomerUseInfoSoftwareMainte> ConvertCustomerUseInfoSoftwareMainte(DataTable table)
		{
			List<CustomerUseInfoSoftwareMainte> result = null;
			if (null != table)
			{
				result = new List<CustomerUseInfoSoftwareMainte>();
				foreach (DataRow row in table.Rows)
				{
					CustomerUseInfoSoftwareMainte mainte = new CustomerUseInfoSoftwareMainte();
					mainte.CUSTOMER_ID = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]);
					mainte.SERVICE_ID = DataBaseValue.ConvObjectToInt(row["SERVICE_ID"]);
					mainte.USE_START_DATE = DataBaseValue.ConvObjectToDateNullByDate(row["USE_START_DATE"]);
					mainte.USE_END_DATE = DataBaseValue.ConvObjectToDateNullByDate(row["USE_END_DATE"]);
					mainte.CANCELLATION_DAY = DataBaseValue.ConvObjectToDateNullByDate(row["CANCELLATION_DAY"]);
					mainte.PAUSE_END_STATUS = DataBaseValue.ConvObjectToIntNull(row["PAUSE_END_STATUS"]);
					mainte.PERIOD_END_DATE = DataBaseValue.ConvObjectToDateNullByDate(row["PERIOD_END_DATE"]);
					mainte.ES_USE_END_DATE = DataBaseValue.ConvObjectToDateNullByDate(row["ES_USE_END_DATE"]);
					result.Add(mainte);
				}
			}
			return result;
		}
	}
}
