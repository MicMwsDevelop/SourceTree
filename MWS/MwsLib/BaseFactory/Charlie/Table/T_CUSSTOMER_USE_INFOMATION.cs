//
// T_CUSSTOMER_USE_INFOMATION.cs
//
// 顧客利用情報クラス
// [CharlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using MwsLib.DB;
using MwsLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.BaseFactory.Charlie.Table
{
	/// <summary>
	/// [CharlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]
	/// 顧客利用情報
	/// </summary>
	public class T_CUSSTOMER_USE_INFOMATION
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int CUSTOMER_ID { get; set; }

		/// <summary>
		/// サービス種別
		/// </summary>
		public int SERVICE_TYPE_ID { get; set; }

		/// <summary>
		/// サービス番号
		/// </summary>
		public int SERVICE_ID { get; set; }

		/// <summary>
		/// 商品ID
		/// </summary>
		public string GOODS_ID { get; set; }

		/// <summary>
		/// 申込番号
		/// </summary>
		public string APPLICATION_NO { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? KAKIN_START_DATE { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? USE_START_DATE { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? USE_END_DATE { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CANCELLATION_DAY { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CANCELLATION_PROCESSING_DATE { get; set; }

		/// <summary>
		/// 課金対象外フラグ
		/// </summary>
		public bool PAUSE_END_STATUS { get; set; }

		/// <summary>
		/// 削除フラグ
		/// </summary>
		public bool DELETE_FLG { get; set; }

		/// <summary>
		/// 作成日付
		/// </summary>
		public DateTime? CREATE_DATE { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string CREATE_PERSON { get; set; }

		/// <summary>
		/// 更新日付
		/// </summary>
		public DateTime? UPDATE_DATE { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		public string UPDATE_PERSON { get; set; }

		/// <summary>
		/// 利用期限日
		/// </summary>
		public DateTime? PERIOD_END_DATE { get; set; }

		/// <summary>
		/// 顧客差分フラグ
		/// </summary>
		public bool RENEWAL_FLG { get; set; }

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16. @17, @18)", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]);
			}
		}

		/// <summary>
		/// UPDATE SET SQL文字列の取得
		/// </summary>
		public string UpdateSetSqlString
		{
			get
			{
				return string.Format(@"UPDATE {0} SET SERVICE_TYPE_ID = @1, SERVICE_ID = @2, GOODS_ID = @3, APPLICATION_NO = @4"
									+ ", KAKIN_START_DATE = @5, USE_START_DATE = @6, USE_END_DATE = @7, CANCELLATION_DAY = @8"
									+ ", CANCELLATION_PROCESSING_DATE = @9, PAUSE_END_STATUS = @10, DELETE_FLG = @11"
									+ ", UPDATE_DATE = @12, UPDATE_PERSON = @13, PERIOD_END_DATE = @14, RENEWAL_FLG = @15"
									+ " WHERE CUSTOMER_ID = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION], CUSTOMER_ID);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_CUSSTOMER_USE_INFOMATION()
		{
			CUSTOMER_ID = 0;
			SERVICE_TYPE_ID = 0;
			SERVICE_ID = 0;
			GOODS_ID = null;
			APPLICATION_NO = null;
			KAKIN_START_DATE = null;
			USE_START_DATE = null;
			USE_END_DATE = null;
			CANCELLATION_DAY = null;
			CANCELLATION_PROCESSING_DATE = null;
			PAUSE_END_STATUS = false;
			DELETE_FLG = false;
			CREATE_DATE = null;
			CREATE_PERSON = string.Empty;
			UPDATE_DATE = null;
			UPDATE_PERSON = string.Empty;
			PERIOD_END_DATE = null;
			RENEWAL_FLG = false;
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", CUSTOMER_ID.ToString()),
				new SqlParameter("@2", SERVICE_TYPE_ID.ToString()),
				new SqlParameter("@3", SERVICE_ID.ToString()),
				new SqlParameter("@4", GOODS_ID),
				new SqlParameter("@5", APPLICATION_NO),
				new SqlParameter("@6", KAKIN_START_DATE),
				new SqlParameter("@7", USE_START_DATE),
				new SqlParameter("@8", USE_END_DATE),
				new SqlParameter("@9", CANCELLATION_DAY),
				new SqlParameter("@10", CANCELLATION_PROCESSING_DATE),
				new SqlParameter("@11", (PAUSE_END_STATUS) ? "1" : "0"),
				new SqlParameter("@12", (DELETE_FLG) ? "1" : "0"),
				new SqlParameter("@13", CREATE_DATE),
				new SqlParameter("@14", CREATE_PERSON),
				new SqlParameter("@15", UPDATE_DATE),
				new SqlParameter("@16", UPDATE_PERSON),
				new SqlParameter("@17", PERIOD_END_DATE),
				new SqlParameter("@18", (RENEWAL_FLG) ? "1" : "0")
			};
			return param;
		}

		/// <summary>
		/// UPDATE SETパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetUpdateSetParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", SERVICE_TYPE_ID.ToString()),
				new SqlParameter("@2", SERVICE_ID.ToString()),
				new SqlParameter("@3", GOODS_ID),
				new SqlParameter("@4", APPLICATION_NO),
				new SqlParameter("@5", KAKIN_START_DATE),
				new SqlParameter("@6", USE_START_DATE),
				new SqlParameter("@7", USE_END_DATE),
				new SqlParameter("@8", CANCELLATION_DAY),
				new SqlParameter("@9", CANCELLATION_PROCESSING_DATE),
				new SqlParameter("@10", (PAUSE_END_STATUS) ? "1" : "0"),
				new SqlParameter("@11", (DELETE_FLG) ? "1" : "0"),
				new SqlParameter("@12", UPDATE_DATE),
				new SqlParameter("@13", UPDATE_PERSON),
				new SqlParameter("@14", PERIOD_END_DATE),
				new SqlParameter("@15", (RENEWAL_FLG) ? "1" : "0")
			};
			return param;
		}

		/// <summary>
		/// [charlieDB].[dbo].[支店情報参照ビュー]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>支店情報</returns>
		public static List<T_CUSSTOMER_USE_INFOMATION> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_CUSSTOMER_USE_INFOMATION> result = new List<T_CUSSTOMER_USE_INFOMATION>();
				foreach (DataRow row in table.Rows)
				{
					T_CUSSTOMER_USE_INFOMATION data = new T_CUSSTOMER_USE_INFOMATION
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
						PAUSE_END_STATUS = DataBaseValue.ConvObjectToBool(row["PAUSE_END_STATUS"]),
						DELETE_FLG = DataBaseValue.ConvObjectToBool(row["DELETE_FLG"]),
						CREATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["CREATE_DATE"]),
						CREATE_PERSON = row["CREATE_PERSON"].ToString().Trim(),
						UPDATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["UPDATE_DATE"]),
						UPDATE_PERSON = row["UPDATE_PERSON"].ToString().Trim(),
						PERIOD_END_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["PERIOD_END_DATE"]),
						RENEWAL_FLG = DataBaseValue.ConvObjectToBool(row["RENEWAL_FLG"])
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}