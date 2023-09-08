//
// EditCustomerUseInformation.cs
// 
// 顧客利用情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/08/24 勝呂):新規作成
//
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.MwsCuiEditTool
{
	public class EditCustomerUseInformation : T_CUSSTOMER_USE_INFOMATION
	{
		/// <summary>
		/// サービス名称
		/// </summary>
		public string ServiceName { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public EditCustomerUseInformation() : base()
		{
			ServiceName = string.Empty;
		}

		/// <summary>
		/// Deep Copy
		/// </summary>
		/// <returns>チェック項目グループ</returns>
		public EditCustomerUseInformation DeepCopy()
		{
			EditCustomerUseInformation ret = new EditCustomerUseInformation();
			ret.CUSTOMER_ID = CUSTOMER_ID;
			ret.SERVICE_TYPE_ID = SERVICE_TYPE_ID;
			ret.SERVICE_ID = SERVICE_ID;
			ret.GOODS_ID = GOODS_ID;
			ret.APPLICATION_NO = APPLICATION_NO;
			ret.KAKIN_START_DATE = KAKIN_START_DATE;
			ret.USE_START_DATE = USE_START_DATE;
			ret.USE_END_DATE = USE_END_DATE;
			ret.CANCELLATION_DAY = CANCELLATION_DAY;
			ret.CANCELLATION_PROCESSING_DATE = CANCELLATION_PROCESSING_DATE;
			ret.PAUSE_END_STATUS = PAUSE_END_STATUS;
			ret.DELETE_FLG = DELETE_FLG;
			ret.CREATE_DATE = CREATE_DATE;
			ret.CREATE_PERSON = CREATE_PERSON;
			ret.UPDATE_DATE = UPDATE_DATE;
			ret.UPDATE_PERSON = UPDATE_PERSON;
			ret.PERIOD_END_DATE = PERIOD_END_DATE;
			ret.RENEWAL_FLG = RENEWAL_FLG;
			ret.ServiceName = ServiceName;
			return ret;
		}

		/// <summary>
		/// [EditCustomerUseInformation]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>利用情報</returns>
		public static new List<EditCustomerUseInformation> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<EditCustomerUseInformation> result = new List<EditCustomerUseInformation>();
				foreach (DataRow row in table.Rows)
				{
					EditCustomerUseInformation data = new EditCustomerUseInformation
					{
						CUSTOMER_ID = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]),
						SERVICE_TYPE_ID = DataBaseValue.ConvObjectToInt(row["SERVICE_TYPE_ID"]),
						SERVICE_ID = DataBaseValue.ConvObjectToInt(row["SERVICE_ID"]),
						GOODS_ID = row["GOODS_ID"].ToString().Trim(),
						APPLICATION_NO = row["APPLICATION_NO"].ToString().Trim(),
						KAKIN_START_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["KAKIN_START_DATE"]),
						USE_START_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["USE_START_DATE"]),
						USE_END_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["USE_END_DATE"]),
						CANCELLATION_DAY = DataBaseValue.ConvObjectToDateTimeNull(row["CANCELLATION_DAY"]),
						CANCELLATION_PROCESSING_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["CANCELLATION_PROCESSING_DATE"]),
						PAUSE_END_STATUS = ("0" == row["PAUSE_END_STATUS"].ToString()) ? false : true,
						DELETE_FLG = ("0" == row["DELETE_FLG"].ToString()) ? false : true,
						CREATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["CREATE_DATE"]),
						CREATE_PERSON = row["CREATE_PERSON"].ToString().Trim(),
						UPDATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["UPDATE_DATE"]),
						UPDATE_PERSON = row["UPDATE_PERSON"].ToString().Trim(),
						PERIOD_END_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["PERIOD_END_DATE"]),
						RENEWAL_FLG = ("0" == row["RENEWAL_FLG"].ToString()) ? false : true,
						ServiceName = row["SERVICE_NAME"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
