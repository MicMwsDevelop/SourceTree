//
// T_CUSTOMER_FOUNDATIONS.cs
//
// [CharlieDB].[dbo].[T_CUSTOMER_FOUNDATIONS]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.DB;
using CommonLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommonLib.BaseFactory.MwsDefine;

namespace CommonLib.BaseFactory.Charlie.Table
{
	/// <summary>
	/// 顧客管理基本情報
	/// </summary>
	public class T_CUSTOMER_FOUNDATIONS
	{
		/// <summary>
		/// 顧客ID
		/// </summary>
		public int CUSTOMER_ID { get; set; }

		/// <summary>
		/// 営業担当者ID
		/// </summary>
		public string MARKETING_SPECIALIST_ID { get; set; }

		/// <summary>
		/// 販売店(使用料請求先）コード
		/// </summary>
		public int STORE_BILLING_ADDRESS_CODE { get; set; }

		/// <summary>
		/// 販売店(拠点）コード
		/// </summary>
		public int STORE_CODE { get; set; }

		/// <summary>
		/// ライセンス発行可能フラグ
		/// 0:登録カード未回収 1:登録カード回収済
		/// </summary>
		public bool LICENSE_FLG { get; set; }

		/// <summary>
		/// 申込書回収日
		/// </summary>
		public DateTime? APPLY_RECOVERY_DAY { get; set; }

		/// <summary>
		/// 販売種別
		/// 1:直接 2:販売店
		/// </summary>
		public char SALE_TYPE { get; set; }

		/// <summary>
		/// 削除フラグ
		/// 1:削除
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
		/// 申込種別
		/// 0:その他 1:VP 2:UG 3:月額 4:まとめ
		/// </summary>
		public char APPLY_TYPE { get; set; }

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
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14)", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSTOMER_FOUNDATIONS]);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_CUSTOMER_FOUNDATIONS()
		{
			CUSTOMER_ID = 0;
			MARKETING_SPECIALIST_ID = string.Empty;
			STORE_BILLING_ADDRESS_CODE = 0;
			STORE_CODE = 0;
			LICENSE_FLG = false;
			APPLY_RECOVERY_DAY = null;
			SALE_TYPE = '0';
			DELETE_FLG = false;
			CREATE_DATE = null;
			CREATE_PERSON = string.Empty;
			UPDATE_DATE = null;
			UPDATE_PERSON = string.Empty;
			APPLY_TYPE = '0';
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
				new SqlParameter("@2", MARKETING_SPECIALIST_ID ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@3", STORE_BILLING_ADDRESS_CODE.ToString()),
				new SqlParameter("@4", STORE_CODE.ToString()),
				new SqlParameter("@5", LICENSE_FLG ? "1" : "0"),
				new SqlParameter("@6", APPLY_RECOVERY_DAY.HasValue ? APPLY_RECOVERY_DAY.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@7", SALE_TYPE.ToString()),
				new SqlParameter("@8", DELETE_FLG ? "1" : "0"),
				new SqlParameter("@9", CREATE_DATE.HasValue ? CREATE_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@10", CREATE_PERSON ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@11", UPDATE_DATE.HasValue ? UPDATE_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@12", UPDATE_PERSON ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@13", APPLY_TYPE.ToString()),
				new SqlParameter("@14", RENEWAL_FLG ? "1" : "0")
			};
			return param;
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_CUSTOMER_FOUNDATIONS]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>顧客管理基本情報リスト</returns>
		public static List<T_CUSTOMER_FOUNDATIONS> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_CUSTOMER_FOUNDATIONS> result = new List<T_CUSTOMER_FOUNDATIONS>();
				foreach (DataRow row in table.Rows)
				{
					T_CUSTOMER_FOUNDATIONS data = new T_CUSTOMER_FOUNDATIONS
					{
						CUSTOMER_ID = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]),
						MARKETING_SPECIALIST_ID = row["MARKETING_SPECIALIST_ID"].ToString().Trim(),
						STORE_BILLING_ADDRESS_CODE = DataBaseValue.ConvObjectToInt(row["STORE_BILLING_ADDRESS_CODE"]),
						STORE_CODE = DataBaseValue.ConvObjectToInt(row["STORE_CODE"]),
						LICENSE_FLG = DataBaseValue.ConvObjectToBool(row["LICENSE_FLG"]),
						APPLY_RECOVERY_DAY = DataBaseValue.ConvObjectToDateTimeNull(row["APPLY_RECOVERY_DAY"]),
						SALE_TYPE = row["SALE_TYPE"].ToString().Trim()[0],
						DELETE_FLG = DataBaseValue.ConvObjectToBool(row["DELETE_FLG"]),
						CREATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["CREATE_DATE"]),
						CREATE_PERSON = row["CREATE_PERSON"].ToString().Trim(),
						UPDATE_DATE = DataBaseValue.ConvObjectToDateTimeNull(row["UPDATE_DATE"]),
						UPDATE_PERSON = row["UPDATE_PERSON"].ToString().Trim(),
						APPLY_TYPE = row["APPLY_TYPE"].ToString().Trim()[0],
						RENEWAL_FLG = DataBaseValue.ConvObjectToBool(row["RENEWAL_FLG"]),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
