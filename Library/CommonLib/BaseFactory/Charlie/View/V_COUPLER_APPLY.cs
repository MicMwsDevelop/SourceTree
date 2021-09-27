//
// V_COUPLER_APPLY.cs
//
// カプラー申込情報ビュークラス
// [CharlieDB].[dbo].[V_COUPLER_APPLY]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/12/22 勝呂)
// 
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.View
{
	public class V_COUPLER_APPLY
	{
        public int apply_id { get; set; }
        public string cp_id { get; set; }
        public int customer_id { get; set; }
        public int service_id { get; set; }
        public DateTime? apply_date { get; set; }
        public string apply_type { get; set; }
        public string system_flg { get; set; }
        public DateTime? create_date { get; set; }
        public string create_user { get; set; }
        public DateTime? update_date { get; set; }
        public string update_user { get; set; }

        public V_COUPLER_APPLY()
        {
            apply_id = 0;
            cp_id = string.Empty;
            customer_id = 0;
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
		/// [charlieDB].[dbo].[V_COUPLER_APPLY]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>V_COUPLER_APPLY</returns>
		public static List<V_COUPLER_APPLY> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<V_COUPLER_APPLY> result = new List<V_COUPLER_APPLY>();
				foreach (DataRow row in table.Rows)
				{
					V_COUPLER_APPLY data = new V_COUPLER_APPLY
					{
                        apply_id = DataBaseValue.ConvObjectToInt(row["apply_id"]),
                        cp_id = row["cp_id"].ToString().Trim(),
                        customer_id = DataBaseValue.ConvObjectToInt(row["customer_id"]),
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
