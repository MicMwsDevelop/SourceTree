//
// T_COUPLER_SERVICE.cs
//
// カプラーサービス利用情報クラス
// [COUPLER].[dbo].[T_COUPLER_SERVICE]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/10/18 勝呂)
// 
using MwsLib.DB;
using MwsLib.DB.SqlServer.Coupler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.BaseFactory.Coupler.Table
{
	/// <summary>
	/// サービス利用情報
	/// </summary>
	public class T_COUPLER_SERVICE
	{
		/// <summary>
		/// MWSID
		/// </summary>
		public string cp_id { get; set; }

		/// <summary>
		/// サービスコード
		/// </summary>
		public int service_id { get; set; }

		/// <summary>
		/// 利用開始日
		/// </summary>
		public DateTime? start_date { get; set; }

		/// <summary>
		/// 利用終了日
		/// </summary>
		public DateTime? end_date { get; set; }

		/// <summary>
		/// 契約種別
		/// 0:契約、1:解約
		/// </summary>
		public int contrac_type { get; set; }

		/// <summary>
		/// 支払い方法
		/// 0:月額以外、1:月額
		/// ※現状は使用していない。Charlieからは0固定でインポートされる
		/// </summary>
		public int payment_type { get; set; }

		/// <summary>
		/// 作成日時
		/// </summary>
		public DateTime? create_date { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string create_user { get; set; }

		/// <summary>
		/// 更新日時
		/// </summary>
		public DateTime? update_date { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		public string update_user { get; set; }

		/// <summary>
		/// INSERT INTO SQL文字列の取得
		/// </summary>
		public static string InsertIntoSqlString
		{
			get
			{
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10)", CouplerDatabaseDefine.TableName[CouplerDatabaseDefine.TableType.T_COUPLER_SERVICE]);
			}
		}

		/// <summary>
		/// DELETE SQL文字列の取得
		/// </summary>
		/// <returns>SQL文字列</returns>
		public string DeleteSqlString
		{
			get
			{
				return string.Format(@"DELETE FROM {0} WHERE cp_id = '{1}' AND service_id = {2}", CouplerDatabaseDefine.TableName[CouplerDatabaseDefine.TableType.T_COUPLER_SERVICE], cp_id, service_id);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_COUPLER_SERVICE()
		{
			cp_id = null;
			service_id = 0;
			start_date = null;
			end_date = null;
			contrac_type = 0;
			payment_type = 0;
			create_date = null;
			create_user = null;
			update_date = null;
			update_user = null;
		}

		/// <summary>
		/// UPDATE SET SQL文字列の取得
		/// </summary>
		public string UpdateSetSqlString
		{
			get
			{
				return string.Format(@"UPDATE {0} SET start_date = @1, end_date = @2, contrac_type = @3, payment_type = @4, create_date = @5, create_user = @6, update_date = @7, update_user = @8"
									+ " WHERE cp_id = '{1}' AND service_id = {2}", CouplerDatabaseDefine.TableName[CouplerDatabaseDefine.TableType.T_COUPLER_SERVICE], cp_id, service_id);
			}
		}

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", cp_id ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@2", service_id),
				new SqlParameter("@3", start_date.HasValue ? start_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@4", end_date.HasValue ? end_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@5", contrac_type),
				new SqlParameter("@6", payment_type),
				new SqlParameter("@7", create_date.HasValue ? create_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@8", create_user ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@9", update_date.HasValue ? update_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@10", update_user ?? System.Data.SqlTypes.SqlString.Null)
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
				new SqlParameter("@1", start_date.HasValue ? start_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@2", end_date.HasValue ? end_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@3", contrac_type),
				new SqlParameter("@4", payment_type),
				new SqlParameter("@5", create_date.HasValue ? create_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@6", create_user ?? System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@7", update_date.HasValue ? update_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@8", update_user ?? System.Data.SqlTypes.SqlString.Null)
			};
			return param;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<T_COUPLER_SERVICE> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_COUPLER_SERVICE> result = new List<T_COUPLER_SERVICE>();
				foreach (DataRow row in table.Rows)
				{
					T_COUPLER_SERVICE data = new T_COUPLER_SERVICE
					{
						cp_id = row["cp_id"].ToString().Trim(),
						service_id = DataBaseValue.ConvObjectToInt(row["service_id"]),
						start_date = DataBaseValue.ConvObjectToDateTimeNull(row["start_date"]),
						end_date = DataBaseValue.ConvObjectToDateTimeNull(row["end_date"]),
						contrac_type = DataBaseValue.ConvObjectToInt(row["contrac_type"]),
						payment_type = DataBaseValue.ConvObjectToInt(row["payment_type"]),
						create_date = DataBaseValue.ConvObjectToDateTimeNull(row["create_date"]),
						create_user = row["create_user"].ToString().Trim(),
						update_date = DataBaseValue.ConvObjectToDateTimeNull(row["update_date"]),
						update_user = row["update_user"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
