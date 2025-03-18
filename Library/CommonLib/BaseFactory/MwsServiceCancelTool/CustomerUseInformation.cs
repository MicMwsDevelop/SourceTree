//
// CustomerUseInformation.cs
// 
// 顧客管理利用情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2025/01/23 勝呂):新規作成
//
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.MwsServiceCancelTool
{
	public class CustomerUseInformation
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int CUSTOMER_ID { get; set; }

		/// <summary>
		/// サービス番号
		/// </summary>
		public int SERVICE_ID { get; set; }

		/// <summary>
		/// サービス名称
		/// </summary>
		public string SERVICE_NAME { get; set; }

		/// <summary>
		/// 商品ID
		/// </summary>
		public string GOODS_ID { get; set; }

		/// <summary>
		/// 利用開始年月日
		/// </summary>
		public DateTime? USE_START_DATE { get; set; }

		/// <summary>
		/// 利用終了年月日
		/// </summary>
		public DateTime? USE_END_DATE { get; set; }

		/// <summary>
		/// 課金対象外フラグ 利用中=0､終了＝1 
		/// </summary>
		public bool PAUSE_END_STATUS { get; set; }

		/// <summary>
		/// 利用期限日
		/// </summary>
		public DateTime? PERIOD_END_DATE { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CustomerUseInformation() : base()
		{
			CUSTOMER_ID = 0;
			SERVICE_ID = 0;
			SERVICE_NAME = string.Empty;
			GOODS_ID = null;
			USE_START_DATE = null;
			USE_END_DATE = null;
			PAUSE_END_STATUS = false;
			PERIOD_END_DATE = null;
		}

		/// <summary>
		/// [CustomerUseInformation]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>利用情報</returns>
		public static List<CustomerUseInformation> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<CustomerUseInformation> result = new List<CustomerUseInformation>();
				foreach (DataRow row in table.Rows)
				{
					CustomerUseInformation data = new CustomerUseInformation
					{
						CUSTOMER_ID = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]),
						SERVICE_ID = DataBaseValue.ConvObjectToInt(row["SERVICE_ID"]),
						SERVICE_NAME = row["SERVICE_NAME"].ToString().Trim(),
						GOODS_ID = row["GOODS_ID"].ToString().Trim(),
						USE_START_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["USE_START_DATE"]),
						USE_END_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["USE_END_DATE"]),
						PAUSE_END_STATUS = ("0" == row["PAUSE_END_STATUS"].ToString()) ? false : true,
						PERIOD_END_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["PERIOD_END_DATE"]),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
