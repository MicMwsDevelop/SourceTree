//
// T_APPLICATION_DATA.cs
//
// [CharlieDB].[dbo].[T_APPLICATION_DATA]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.Table
{
	/// <summary>
	/// 申込データ
	/// </summary>
	public class T_APPLICATION_DATA
	{
		public int APPLICATION_NO { get; set; }
		public int CUSTOMER_ID { get; set; }
		public int SERVICE_TYPE_ID { get; set; }
		public int SERVICE_ID { get; set; }
		public int COUPLER_APPLICATION_NO { get; set; }
		public DateTime? APPLICATION_DATE { get; set; }
		public bool APPLICATION_CANCELLATION_FLG { get; set; }
		public bool CHECK_STATUS { get; set; }
		public bool PCA_FINISHING_FLG { get; set; }
		public bool DELETE_FLG { get; set; }
		public DateTime? CREATE_DATE { get; set; }
		public string CREATE_PERSON { get; set; }
		public DateTime? UPDATE_DATE { get; set; }
		public string UPDATE_PERSON { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_APPLICATION_DATA()
		{
			APPLICATION_NO = 0;
			CUSTOMER_ID = 0;
			SERVICE_TYPE_ID = 0;
			SERVICE_ID = 0;
			COUPLER_APPLICATION_NO = 0;
			APPLICATION_DATE = null;
			APPLICATION_CANCELLATION_FLG = false;
			CHECK_STATUS = false;
			PCA_FINISHING_FLG = false;
			DELETE_FLG = false;
			CREATE_DATE = null;
			CREATE_PERSON = string.Empty;
			UPDATE_DATE = null;
			UPDATE_PERSON = string.Empty;
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_APPLICATION_DATA]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>T_APPLICATION_DATA</returns>
		public static List<T_APPLICATION_DATA> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_APPLICATION_DATA> result = new List<T_APPLICATION_DATA>();
				foreach (DataRow row in table.Rows)
				{
					T_APPLICATION_DATA data = new T_APPLICATION_DATA
					{
						APPLICATION_NO = DataBaseValue.ConvObjectToInt(row["APPLICATION_NO"]),
						CUSTOMER_ID = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]),
						SERVICE_TYPE_ID = DataBaseValue.ConvObjectToInt(row["SERVICE_TYPE_ID"]),
						SERVICE_ID = DataBaseValue.ConvObjectToInt(row["SERVICE_ID"]),
						COUPLER_APPLICATION_NO = DataBaseValue.ConvObjectToInt(row["COUPLER_APPLICATION_NO"]),
						APPLICATION_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["APPLICATION_DATE"]),
						APPLICATION_CANCELLATION_FLG = DataBaseValue.ConvObjectToBool(row["APPLICATION_CANCELLATION_FLG"]),
						CHECK_STATUS = DataBaseValue.ConvObjectToBool(row["CHECK_STATUS"]),
						PCA_FINISHING_FLG = DataBaseValue.ConvObjectToBool(row["PCA_FINISHING_FLG"]),
						DELETE_FLG = DataBaseValue.ConvObjectToBool(row["DELETE_FLG"]),
						CREATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["CREATE_DATE"]),
						CREATE_PERSON = row["CREATE_PERSON"].ToString().Trim(),
						UPDATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["UPDATE_DATE"]),
						UPDATE_PERSON = row["UPDATE_PERSON"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
