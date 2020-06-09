//
// SoftwareMainteEarningsController.cs
//
// ソフトウェア保守料売上データ作成 データテーブル詰め替えクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/03/16 勝呂)
// 
using MwsLib.BaseFactory.SoftwareMainteEarnings;
using MwsLib.Common;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.SoftwareMainteEarnings
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
		/// ソフトウェア保守料の受注情報のの詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>ソフト保守情報リスト</returns>
		public static List<OrderSlipSoftwareMainte> ConvertOrderSlipSoftwareMainte(DataTable table)
		{
			List<OrderSlipSoftwareMainte> result = null;
			if (null != table)
			{
				result = new List<OrderSlipSoftwareMainte>();
				foreach (DataRow row in table.Rows)
				{
					OrderSlipSoftwareMainte order = new OrderSlipSoftwareMainte();
					order.f受注番号 = DataBaseValue.ConvObjectToInt(row["f受注番号"]);
					order.f受注日 = DataBaseValue.ConvObjectToDateNullByDate(row["f受注日"]);
					order.f受注承認日 = DataBaseValue.ConvObjectToDateNullByDate(row["f受注承認日"]);
					order.f売上承認日 = DataBaseValue.ConvObjectToDateNullByDate(row["f売上承認日"]);
					order.f販売種別 = DataBaseValue.ConvObjectToIntNull(row["f販売種別"]);
					order.f販売先コード = DataBaseValue.ConvObjectToIntNull(row["f販売先コード"]);
					order.f販売先 = row["f販売先"].ToString().Trim();
					order.fユーザーコード = DataBaseValue.ConvObjectToInt(row["fユーザーコード"]);
					order.fユーザー = row["fユーザー"].ToString().Trim();
					if (YearMonth.TryParse(row["fSV利用開始年月"].ToString(), out YearMonth workYM1))
					{
						order.fSV利用開始年月 = workYM1;
					}
					if (YearMonth.TryParse(row["fSV利用終了年月"].ToString(), out YearMonth workYM2))
					{
						order.fSV利用終了年月 = workYM2;
					}
					order.fBshCode3 = row["fBshCode3"].ToString().Trim();
					order.f担当支店名 = row["f担当支店名"].ToString().Trim();
					order.f件名 = row["f件名"].ToString().Trim();
					order.f商品コード = row["f商品コード"].ToString().Trim();
					order.f商品名 = row["f商品名"].ToString().Trim();
					order.f数量 = DataBaseValue.ConvObjectToIntNull(row["f数量"]);
					order.f標準価格 = DataBaseValue.ConvObjectToIntNull(row["f標準価格"]);
					order.f金額 = DataBaseValue.ConvObjectToIntNull(row["f金額"]);
					order.f提供価格 = DataBaseValue.ConvObjectToIntNull(row["f提供価格"]);
					order.f税区分 = row["f税区分"].ToString().Trim();
					order.f税率 = DataBaseValue.ConvObjectToIntNull(row["f税率"]);
					order.f税込区分 = row["f税込区分"].ToString().Trim();
					order.f売上原価 = DataBaseValue.ConvObjectToIntNull(row["f売上原価"]);
					order.fPca部門コード = (short)DataBaseValue.ConvObjectToIntNull(row["fPca部門コード"]);
					order.fPca担当者コード = row["fPca担当者コード"].ToString().Trim();
					order.fPca倉庫コード = (short)DataBaseValue.ConvObjectToIntNull(row["fPca倉庫コード"]);
					result.Add(order);
				}
			}
			return result;
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
