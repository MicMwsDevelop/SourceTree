//
// T_COUPLER_APPLY.cs
//
// 申込情報クラス
// [COUPLER].[dbo].[T_COUPLER_APPLY]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.BaseFactory.Charlie.View;
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Coupler.Table
{
	public class T_COUPLER_APPLY
	{
		public int apply_id { get; set; }
		public string cp_id { get; set; }
		public int service_id { get; set; }
		public DateTime? apply_date { get; set; }
		public string apply_type { get; set; }
		public string system_flg { get; set; }
		public DateTime? create_date { get; set; }
		public string create_user { get; set; }
		public DateTime? update_date { get; set; }
		public string update_user { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_COUPLER_APPLY()
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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="data"></param>
		public void SetData(V_COUPLER_APPLY data)
		{
			apply_id = data.apply_id;
			cp_id = data.cp_id;
			service_id = data.service_id;
			apply_date = data.apply_date;
			apply_type = data.apply_type;
			system_flg = data.system_flg;
			create_date = data.create_date;
			create_user = data.create_user;
			update_date = data.update_date;
			update_user = data.update_user;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<T_COUPLER_APPLY> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_COUPLER_APPLY> result = new List<T_COUPLER_APPLY>();
				foreach (DataRow row in table.Rows)
				{
					T_COUPLER_APPLY data = new T_COUPLER_APPLY
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
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
