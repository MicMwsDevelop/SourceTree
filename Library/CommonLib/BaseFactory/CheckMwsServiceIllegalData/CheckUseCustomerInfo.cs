//
// CheckUseCustomerInfo.cs
// 
// 顧客利用情報 異常データ検出クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/01/31 勝呂):新規作成
//
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.CheckMwsServiceIllegalData
{
	public class CheckUseCustomerInfo
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerID { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string CustomerName { get; set; }

		/// <summary>
		/// サービスID
		/// </summary>
		public int ServiceID { get; set; }

		/// <summary>
		/// サービス名
		/// </summary>
		public string ServiceName { get; set; }

		/// <summary>
		/// 利用開始日
		/// </summary>
		public DateTime? UseStartDate { get; set; }

		/// <summary>
		/// 課金終了日
		/// </summary>
		public DateTime? UseEndtDate { get; set; }

		/// <summary>
		/// 課金対象外フラグ
		/// </summary>
		public bool PauseEndStatus { get; set; }

		/// <summary>
		/// 作成日
		/// </summary>
		public DateTime? CreateDate { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string CreatePerson { get; set; }

		/// <summary>
		/// 更新日
		/// </summary>
		public DateTime? UpdateDate { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		public string UpdatePerson { get; set; }

		/// <summary>
		/// 利用期限日
		/// </summary>
		public DateTime? PeriodEndDate { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CheckUseCustomerInfo()
		{
			CustomerID = 0;
			CustomerName = string.Empty;
			ServiceID = 0;
			ServiceName = string.Empty;
			UseStartDate = null;
			UseEndtDate = null;
			PauseEndStatus = false;
			CreateDate = null;
			CreatePerson = string.Empty;
			UpdateDate = null;
			UpdatePerson = string.Empty;
			PeriodEndDate = null;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<CheckUseCustomerInfo> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<CheckUseCustomerInfo> result = new List<CheckUseCustomerInfo>();
				foreach (DataRow row in table.Rows)
				{
					CheckUseCustomerInfo data = new CheckUseCustomerInfo();
					data.CustomerID = DataBaseValue.ConvObjectToInt(row["CustomerID"]);
					data.CustomerName = row["CustomerName"].ToString().Trim();
					data.ServiceID = DataBaseValue.ConvObjectToInt(row["ServiceID"]);
					data.ServiceName = row["ServiceName"].ToString().Trim();
					data.UseStartDate = DataBaseValue.ConvObjectToDateTimeNull(row["UseStartDate"]);
					data.UseEndtDate = DataBaseValue.ConvObjectToDateTimeNull(row["UseEndtDate"]);
					data.PauseEndStatus = ("0" == row["PauseEndStatus"].ToString()) ? false : true;
					data.CreateDate = DataBaseValue.ConvObjectToDateTimeNull(row["CreateDate"]);
					data.CreatePerson = row["CreatePerson"].ToString().Trim();
					data.UpdateDate = DataBaseValue.ConvObjectToDateTimeNull(row["UpdateDate"]);
					data.UpdatePerson = row["UpdatePerson"].ToString().Trim();
					data.PeriodEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["PeriodEndDate"]);

					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
