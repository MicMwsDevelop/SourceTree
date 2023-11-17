//
// T_COUPLER_PRODUCTUSER.cs
//
// カプラー顧客情報クラス
// [COUPLER].[dbo].[T_COUPLER_PRODUCTUSER]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/10/18 勝呂)
// 
using CommonLib.DB;
using CommonLib.DB.SqlServer.Coupler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.BaseFactory.Coupler.Table
{
	public class T_COUPLER_PRODUCTUSER
	{
		/// <summary>
		/// MWS-ID
		/// </summary>
		public string cp_id { get; set; }

		/// <summary>
		/// ユーザー種別
		/// 0:UNICORNユーザ
		/// 1:既存製品ユーザ
		/// 2:社員用ユーザー
		/// 3:デモ用ユーザー
		/// </summary>
		public int user_type { get; set; }

		/// <summary>
		/// 体験版 
		/// </summary>
		public bool trial_flg { get; set; }

		/// <summary>
		/// 終了フラグ
		/// </summary>
		public bool end_flg { get; set; }

		/// <summary>
		/// 顧客ID
		/// </summary>
		public int customer_id { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string customer_nm { get; set; }

		/// <summary>
		/// メールアドレス1
		/// </summary>
		public string email1 { get; set; }

		/// <summary>
		/// メールアドレス2
		/// </summary>
		public string email2 { get; set; }

		/// <summary>
		/// 利用開始日時
		/// </summary>
		public DateTime? login_start_date { get; set; }

		/// <summary>
		/// 利用終了日時
		/// </summary>
		public DateTime? login_end_date { get; set; }

		/// <summary>
		/// ログインパスワード
		/// </summary>
		public string login_paswd { get; set; }

		/// <summary>
		/// 初期パスワード
		/// </summary>
		public string default_paswd { get; set; }

		/// <summary>
		/// ログインパスワード更新日時
		/// </summary>
		public DateTime? paswd_update { get; set; }

		/// <summary>
		/// 同時接続クライアント数
		/// </summary>
		public int license_count { get; set; }

		/// <summary>
		/// paletteバージョン番号
		/// </summary>
		public int ver_id { get; set; }

		/// <summary>
		/// テストユーザフラグ
		/// </summary>
		public bool testuser_flg { get; set; }

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
				return string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20)", CouplerDatabaseDefine.TableName[CouplerDatabaseDefine.TableType.PRODUCTUSER]);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_COUPLER_PRODUCTUSER()
		{
			cp_id = string.Empty;
			user_type = 0;
			trial_flg = false;
			end_flg = false;
			customer_id = 0;
			customer_nm = string.Empty;
			email1 = string.Empty;
			email2 = string.Empty;
			login_start_date = null;
			login_end_date = null;
			login_paswd = string.Empty;
			default_paswd = string.Empty;
			paswd_update = null;
			license_count = 0;
			ver_id = 0;
			testuser_flg = false;
			create_date = null;
			create_user = string.Empty;
			update_date = null;
			update_user = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<T_COUPLER_PRODUCTUSER> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_COUPLER_PRODUCTUSER> result = new List<T_COUPLER_PRODUCTUSER>();
				foreach (DataRow row in table.Rows)
				{
					T_COUPLER_PRODUCTUSER data = new T_COUPLER_PRODUCTUSER
					{
						cp_id = row["cp_id"].ToString().Trim(),
						user_type = DataBaseValue.ConvObjectToInt(row["user_type"]),
						trial_flg = ("0" == row["trial_flg"].ToString()) ? false : true,
						end_flg = ("0" == row["end_flg"].ToString()) ? false : true,
						customer_id = DataBaseValue.ConvObjectToInt(row["customer_id"]),
						customer_nm = row["customer_nm"].ToString().Trim(),
						email1 = row["email1"].ToString().Trim(),
						email2 = row["email2"].ToString().Trim(),
						login_start_date = DataBaseValue.ConvObjectToDateTimeNull(row["login_start_date"]),
						login_end_date = DataBaseValue.ConvObjectToDateTimeNull(row["login_end_date"]),
						login_paswd = row["login_paswd"].ToString().Trim(),
						default_paswd = row["default_paswd"].ToString().Trim(),
						paswd_update = DataBaseValue.ConvObjectToDateTimeNull(row["paswd_update"]),
						license_count = DataBaseValue.ConvObjectToInt(row["license_count"]),
						ver_id = DataBaseValue.ConvObjectToInt(row["ver_id"]),
						testuser_flg = ("0" == row["testuser_flg"].ToString()) ? false : true,
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

		/// <summary>
		/// INSERT INTOパラメタの取得
		/// </summary>
		/// <returns></returns>
		public SqlParameter[] GetInsertIntoParameters()
		{
			SqlParameter[] param = {
				new SqlParameter("@1", cp_id),
				new SqlParameter("@2", user_type.ToString()),
				new SqlParameter("@3", trial_flg ? "1" : "0"),
				new SqlParameter("@4", end_flg ? "1" : "0"),
				new SqlParameter("@5", customer_id.ToString()),
				new SqlParameter("@6", customer_nm),
				new SqlParameter("@7", email1),
				new SqlParameter("@8", email2),
				new SqlParameter("@9", login_start_date.HasValue ? login_start_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@10", login_end_date.HasValue ? login_end_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@11", login_paswd),
				new SqlParameter("@12", default_paswd),
				new SqlParameter("@13", paswd_update.HasValue ? paswd_update.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@14", license_count.ToString()),
				new SqlParameter("@15", ver_id.ToString()),
				new SqlParameter("@16", testuser_flg ? "1" : "0"),
				new SqlParameter("@17", create_date.HasValue ? create_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@18", create_user),
				new SqlParameter("@19", update_date.HasValue ? update_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
				new SqlParameter("@20", update_user),
			};
			return param;
		}
	}
}
