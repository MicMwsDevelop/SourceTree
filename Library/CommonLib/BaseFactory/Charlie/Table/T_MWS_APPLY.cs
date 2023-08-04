//
// T_MWS_APPLY.cs
//
// [CharlieDB].[dbo].[T_MWS_APPLY]
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
	/// MWSサービス申込情報
	/// </summary>
	public class T_MWS_APPLY
	{
		/// <summary>
		/// 申込番号
		/// </summary>
		public int apply_id { get; set; }

		/// <summary>
		/// MWSID
		/// </summary>
		public string cp_id { get; set; }

		/// <summary>
		/// サービスID
		/// </summary>
		public int service_id { get; set; }

		/// <summary>
		/// 申込日時
		/// </summary>
		public DateTime? apply_date { get; set; }

		/// <summary>
		/// 申込種別（0:利用申込、1:解約申込）
		/// </summary>
		public string apply_type { get; set; }

		/// <summary>
		/// システム反映フラグ（0:未反映、1:反映済、2:取消）
		/// </summary>
		public string system_flg { get; set; }

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
		/// 顧客No
		/// </summary>
		public int customer_id { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_MWS_APPLY()
		{
			apply_id = 0;
			cp_id = string.Empty;
			service_id = 0;
			apply_date = null;
			apply_type = string.Empty;
			system_flg = string.Empty;
			create_date = null;
			create_user = string.Empty;
			update_date = null;
			update_user = string.Empty;
			customer_id = 0;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns>MWSコードマスタリスト</returns>
		public static List<T_MWS_APPLY> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_MWS_APPLY> result = new List<T_MWS_APPLY>();
				foreach (DataRow row in table.Rows)
				{
					T_MWS_APPLY data = new T_MWS_APPLY
					{
						apply_id = DataBaseValue.ConvObjectToInt(row["apply_id"]),
						cp_id = row["cp_id"].ToString().Trim(),
						service_id = DataBaseValue.ConvObjectToInt(row["service_id"]),
						apply_date = DataBaseValue.ConvObjectToDateTimeNull(row["apply_date"]),
						apply_type = row["apply_type"].ToString().Trim(),
						system_flg = row["system_flg"].ToString().Trim(),
						create_date = DataBaseValue.ConvObjectToDateTimeNull(row["create_date"]),
						create_user = row["create_user"].ToString().Trim(),
						update_date = DataBaseValue.ConvObjectToDateTimeNull(row["update_date"]),
						update_user = row["update_user"].ToString().Trim(),
						customer_id = DataBaseValue.ConvObjectToInt(row["customer_id"]),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
