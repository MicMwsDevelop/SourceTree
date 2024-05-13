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
using CommonLib.DB;
using CommonLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.BaseFactory.Charlie.Table
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
		/// 課金開始年月日
		/// </summary>
		public DateTime? KAKIN_START_DATE { get; set; }

		/// <summary>
		/// 利用開始年月日
		/// </summary>
		public DateTime? USE_START_DATE { get; set; }

		/// <summary>
		/// 利用終了年月日
		/// </summary>
		public DateTime? USE_END_DATE { get; set; }

		/// <summary>
		/// 解約申込日
		/// </summary>
		public DateTime? CANCELLATION_DAY { get; set; }

		/// <summary>
		/// 解約処理日
		/// </summary>
		public DateTime? CANCELLATION_PROCESSING_DATE { get; set; }

		/// <summary>
		/// 課金対象外フラグ 利用中=0､終了＝1 
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
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18)", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]);
			}
		}

		/// <summary>
		/// UPDATE SET SQL文字列の取得
		/// </summary>
		public string UpdateSetSqlString
		{
			get
			{
				return string.Format(@"UPDATE {0} SET GOODS_ID = @1, APPLICATION_NO = @2, KAKIN_START_DATE = @3, USE_START_DATE = @4, USE_END_DATE = @5, CANCELLATION_DAY = @6"
									+ ", CANCELLATION_PROCESSING_DATE = @7, PAUSE_END_STATUS = @8, DELETE_FLG = @9, CREATE_DATE = @10, CREATE_PERSON = @11"
									+ ", UPDATE_DATE = @12, UPDATE_PERSON = @13, PERIOD_END_DATE = @14, RENEWAL_FLG = @15"
									+ " WHERE CUSTOMER_ID = {1} AND SERVICE_TYPE_ID = {2} AND SERVICE_ID = {3}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION], CUSTOMER_ID, SERVICE_TYPE_ID, SERVICE_ID);
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
			CREATE_PERSON = null;
			UPDATE_DATE = null;
			UPDATE_PERSON = null;
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
				new SqlParameter("@4", GOODS_ID ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@5", APPLICATION_NO ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@6", KAKIN_START_DATE.HasValue ? KAKIN_START_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@7", USE_START_DATE.HasValue ? USE_START_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@8", USE_END_DATE.HasValue ? USE_END_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@9", CANCELLATION_DAY.HasValue ? CANCELLATION_DAY.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@10", CANCELLATION_PROCESSING_DATE.HasValue ? CANCELLATION_PROCESSING_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@11", PAUSE_END_STATUS ? "1" : "0"),
				new SqlParameter("@12", DELETE_FLG ? "1" : "0"),
				new SqlParameter("@13", CREATE_DATE.HasValue ? CREATE_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@14", CREATE_PERSON ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@15", UPDATE_DATE.HasValue ? UPDATE_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@16", UPDATE_PERSON ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@17", PERIOD_END_DATE.HasValue ? PERIOD_END_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@18", RENEWAL_FLG ? "1" : "0")
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
				new SqlParameter("@1", GOODS_ID ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@2", APPLICATION_NO ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@3", KAKIN_START_DATE.HasValue ? KAKIN_START_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@4", USE_START_DATE.HasValue ? USE_START_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@5", USE_END_DATE.HasValue ? USE_END_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@6", CANCELLATION_DAY.HasValue ? CANCELLATION_DAY.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@7", CANCELLATION_PROCESSING_DATE.HasValue ? CANCELLATION_PROCESSING_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@8", PAUSE_END_STATUS ? "1" : "0"),
				new SqlParameter("@9", DELETE_FLG ? "1" : "0"),
				new SqlParameter("@10", CREATE_DATE.HasValue ? CREATE_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@11", CREATE_PERSON ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@12", UPDATE_DATE.HasValue ? UPDATE_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@13", UPDATE_PERSON ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@14", PERIOD_END_DATE.HasValue ? PERIOD_END_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@15", RENEWAL_FLG ? "1" : "0")
			};
			return param;
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>利用情報</returns>
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
						PAUSE_END_STATUS = ("0" == row["PAUSE_END_STATUS"].ToString()) ? false : true,
						DELETE_FLG = ("0" == row["DELETE_FLG"].ToString()) ? false : true,
						CREATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["CREATE_DATE"]),
						CREATE_PERSON = row["CREATE_PERSON"].ToString().Trim(),
						UPDATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["UPDATE_DATE"]),
						UPDATE_PERSON = row["UPDATE_PERSON"].ToString().Trim(),
						PERIOD_END_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["PERIOD_END_DATE"]),
						RENEWAL_FLG = ("0" == row["RENEWAL_FLG"].ToString()) ? false : true
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}