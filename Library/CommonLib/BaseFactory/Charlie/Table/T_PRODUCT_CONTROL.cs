//
// T_PRODUCT_CONTROL.cs
//
// 製品管理情報クラス
// [CharlieDB].[dbo].[T_PRODUCT_CONTROL]
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
	/// 製品管理情報
	/// [CharlieDB].[dbo].[T_PRODUCT_CONTROL]
	/// </summary>
	public class T_PRODUCT_CONTROL
	{
		/// <summary>
		/// Coupler ID
		/// </summary>
		public string PRODUCT_ID { get; set; }

		/// <summary>
		/// パスワード
		/// </summary>
		public string PASSWORD { get; set; }

		/// <summary>
		/// パスワード読み
		/// </summary>
		public string PASSWORD_READING { get; set; }

		/// <summary>
		/// ユーザー種別
		/// </summary>
		public MwsDefine.UserClassification USER_CLASSIFICATION { get; set; }

		/// <summary>
		/// 顧客ID
		/// </summary>
		public int CUSTOMER_ID { get; set; }

		/// <summary>
		/// 体験版
		/// </summary>
		public bool TRIAL_FLG { get; set; }

		/// <summary>
		/// 休止・終了ステータス
		/// </summary>
		public bool END_FLG { get; set; }

		/// <summary>
		/// 体験利用開始日
		/// </summary>
		public DateTime? TRIAL_START_DATE { get; set; }

		/// <summary>
		/// 体験利用終了日
		/// </summary>
		public DateTime? TRIAL_END_DATE { get; set; }

		/// <summary>
		/// 委託先
		/// </summary>
		public string COMMISSION_PLACE { get; set; }

		/// <summary>
		/// 備考
		/// </summary>
		public string REMARKS { get; set; }

		/// <summary>
		/// 解約事由
		/// </summary>
		public string REASON_CANCELLATION { get; set; }

		/// <summary>
		/// 解約日
		/// </summary>
		public DateTime? CANCELLATION_DATE { get; set; }

		/// <summary>
		/// 課金対象終了フラグ
		/// </summary>
		public bool PAUSE_END_STATUS { get; set; }

		/// <summary>
		/// 作成日
		/// </summary>
		public DateTime? CREATE_DATE { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string CREATE_PERSON { get; set; }

		/// <summary>
		/// 更新日
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
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19)", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_PRODUCT_CONTROL]);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_PRODUCT_CONTROL()
		{
			PRODUCT_ID = null;
			PASSWORD = null;
			PASSWORD_READING = null;
			USER_CLASSIFICATION = MwsDefine.UserClassification.PaletteUser;
			CUSTOMER_ID = 0;
			TRIAL_FLG = false;
			END_FLG = false;
			TRIAL_START_DATE = null;
			TRIAL_END_DATE = null;
			COMMISSION_PLACE = null;
			REMARKS = null;
			REASON_CANCELLATION = null;
			CANCELLATION_DATE = null;
			PAUSE_END_STATUS = false;
			CREATE_DATE = null;
			CREATE_PERSON = null;
			UPDATE_DATE = null;
			UPDATE_PERSON = null;
			PERIOD_END_DATE = null;
		}

		/// <summary>
		/// UPDATE SET SQL文字列の取得
		/// </summary>
		public string UpdateSetSqlString
		{
			get
			{
				return string.Format(@"UPDATE {0} SET PASSWORD = @1, PASSWORD_READING = @2, USER_CLASSIFICATION = @3, CUSTOMER_ID = @4, TRIAL_FLG = @5, END_FLG = @6"
									+ ", TRIAL_START_DATE = @7, TRIAL_END_DATE = @8, COMMISSION_PLACE = @9, REMARKS = @10, REASON_CANCELLATION = @11, CANCELLATION_DATE = @12"
									+ ", PAUSE_END_STATUS = @13, CREATE_DATE = @14, CREATE_PERSON = @15, UPDATE_DATE = @16, UPDATE_PERSON = @17, PERIOD_END_DATE = @18"
									+ " WHERE CUSTOMER_ID = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_PRODUCT_CONTROL], CUSTOMER_ID);
			}
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			SqlParameter[] param = {
			new SqlParameter("@1", PRODUCT_ID ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@2", PASSWORD ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@3", PASSWORD_READING ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@4", (int)USER_CLASSIFICATION),
				new SqlParameter("@5", CUSTOMER_ID.ToString()),
				new SqlParameter("@6", TRIAL_FLG ? "1" : "0"),
				new SqlParameter("@7", END_FLG ? "1" : "0"),
				new SqlParameter("@8", TRIAL_START_DATE.HasValue ? TRIAL_START_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@9", TRIAL_END_DATE.HasValue ? TRIAL_END_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@10", COMMISSION_PLACE ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@11", REMARKS ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@12", REASON_CANCELLATION ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@13", CANCELLATION_DATE.HasValue ? CANCELLATION_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@14", PAUSE_END_STATUS ? "1" : "0"),
				new SqlParameter("@15", CREATE_DATE.HasValue ? CREATE_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@16", CREATE_PERSON ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@17", UPDATE_DATE.HasValue ? UPDATE_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@18", UPDATE_PERSON ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@19", PERIOD_END_DATE.HasValue ? PERIOD_END_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null)
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
				new SqlParameter("@1", PASSWORD ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@2", PASSWORD_READING ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@3", (int)USER_CLASSIFICATION),
				new SqlParameter("@4", CUSTOMER_ID.ToString()),
				new SqlParameter("@5", TRIAL_FLG ? "1" : "0"),
				new SqlParameter("@6", END_FLG ? "1" : "0"),
				new SqlParameter("@7", TRIAL_START_DATE.HasValue ? TRIAL_START_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@8", TRIAL_END_DATE.HasValue ? TRIAL_END_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@9", COMMISSION_PLACE ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@10", REMARKS ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@11", REASON_CANCELLATION ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@12", CANCELLATION_DATE.HasValue ? CANCELLATION_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@13", PAUSE_END_STATUS ? "1" : "0"),
				new SqlParameter("@14", CREATE_DATE.HasValue ? CREATE_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@15", CREATE_PERSON ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@16", UPDATE_DATE.HasValue ? UPDATE_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@17", UPDATE_PERSON ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@18", PERIOD_END_DATE.HasValue ? PERIOD_END_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null)
			};
			return param;
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_PRODUCT_CONTROL]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>製品管理情報</returns>
		public static List<T_PRODUCT_CONTROL> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_PRODUCT_CONTROL> result = new List<T_PRODUCT_CONTROL>();
				foreach (DataRow row in table.Rows)
				{
					T_PRODUCT_CONTROL data = new T_PRODUCT_CONTROL
					{
						PRODUCT_ID = row["PRODUCT_ID"].ToString().Trim(),
						PASSWORD = row["PASSWORD"].ToString().Trim(),
						PASSWORD_READING = row["PASSWORD_READING"].ToString().Trim(),
						USER_CLASSIFICATION = (MwsDefine.UserClassification)DataBaseValue.ConvObjectToInt(row["USER_CLASSIFICATION"]),
						CUSTOMER_ID = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]),
						TRIAL_FLG = ("0" == row["TRIAL_FLG"].ToString()) ? false : true,
						END_FLG = ("0" == row["END_FLG"].ToString()) ? false : true,
						TRIAL_START_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["TRIAL_START_DATE"]),
						TRIAL_END_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["TRIAL_END_DATE"]),
						COMMISSION_PLACE = row["COMMISSION_PLACE"].ToString().Trim(),
						REMARKS = row["REMARKS"].ToString().Trim(),
						REASON_CANCELLATION = row["REASON_CANCELLATION"].ToString().Trim(),
						CANCELLATION_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["CANCELLATION_DATE"]),
						PAUSE_END_STATUS = ("0" == row["PAUSE_END_STATUS"].ToString()) ? false : true,
						CREATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["CREATE_DATE"]),
						CREATE_PERSON = row["CREATE_PERSON"].ToString().Trim(),
						UPDATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["UPDATE_DATE"]),
						UPDATE_PERSON = row["UPDATE_PERSON"].ToString().Trim(),
						PERIOD_END_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["PERIOD_END_DATE"])
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
