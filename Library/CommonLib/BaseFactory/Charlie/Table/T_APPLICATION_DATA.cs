//
// T_APPLICATION_DATA.cs
//
// [CharlieDB].[dbo].[T_APPLICATION_DATA]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.Common;
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
		/// <summary>
		/// 申込No
		/// </summary>
		public int APPLICATION_NO { get; set; }

		/// <summary>
		/// 顧客ID
		/// </summary>
		public int CUSTOMER_ID { get; set; }

		/// <summary>
		/// サービス種別ID
		/// </summary>
		public int SERVICE_TYPE_ID { get; set; }

		/// <summary>
		/// サービスID
		/// </summary>
		public int SERVICE_ID { get; set; }

		/// <summary>
		/// Coupler申込No
		/// </summary>
		public int COUPLER_APPLICATION_NO { get; set; }

		/// <summary>
		/// 申込年月日
		/// </summary>
		public DateTime? APPLICATION_DATE { get; set; }

		/// <summary>
		/// 申込解約フラグ 0:申込 1:解約
		/// </summary>
		public bool APPLICATION_CANCELLATION_FLG { get; set; }

		/// <summary>
		/// チェックステータス 0:未処理 1:伝票起票済 2:営業確認中 3:キャンセル
		/// </summary>
		public int CHECK_STATUS { get; set; }

		/// <summary>
		/// PCA汎用データ作成済フラグ 0:未作成 1:作成済
		/// </summary>
		public bool PCA_FINISHING_FLG { get; set; }

		/// <summary>
		/// 削除フラグ
		/// </summary>
		public bool DELETE_FLG { get; set; }

		/// <summary>
		/// 作成日時
		/// </summary>
		public DateTime? CREATE_DATE { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string CREATE_PERSON { get; set; }

		/// <summary>
		/// 更新日時
		/// </summary>
		public DateTime? UPDATE_DATE { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
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
			CHECK_STATUS = 0;
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
						CHECK_STATUS = row["CHECK_STATUS"].ToString().Trim().ToInt(),
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
