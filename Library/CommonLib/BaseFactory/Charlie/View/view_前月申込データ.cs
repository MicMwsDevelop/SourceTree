//
// view_前月申込データ.cs
//
// [CharlieDB].[dbo].[view_前月申込データ]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/08/23 勝呂):新規作成
// 
using CommonLib.Common;
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.View
{
	public class view_前月申込データ
	{
		/// <summary>
		/// 申込No
		/// </summary>
		public int apply_id { get; set; }

		/// <summary>
		/// MWSID
		/// </summary>
		public string cp_id { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int customer_id { get; set; }

		/// <summary>
		/// サービス種別ID
		/// </summary>
		public int service_type_id { get; set; }

		/// <summary>
		/// サービスID
		/// </summary>
		public int service_id { get; set; }

		/// <summary>
		/// 申込種別 0:利用申込、1:解約申込
		/// </summary>
		public int apply_type { get; set; }

		/// <summary>
		/// 申込日時
		/// </summary>
		public DateTime? apply_date { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public view_前月申込データ()
		{
			apply_id = 0;
			cp_id = string.Empty;
			customer_id = 0;
			service_type_id = 0;
			service_id = 0;
			apply_type = 0;
			apply_date = null;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns>サービス利用情報リスト</returns>
		public static List<view_前月申込データ> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<view_前月申込データ> result = new List<view_前月申込データ>();
				foreach (DataRow row in table.Rows)
				{
					view_前月申込データ data = new view_前月申込データ
					{
						apply_id = DataBaseValue.ConvObjectToInt(row["apply_id"]),
						cp_id = row["cp_id"].ToString().Trim(),
						customer_id = DataBaseValue.ConvObjectToInt(row["customer_id"]),
						service_type_id = DataBaseValue.ConvObjectToInt(row["service_type_id"]),
						service_id = DataBaseValue.ConvObjectToInt(row["service_id"]),
						apply_type = row["apply_type"].ToString().Trim().ToInt(),
						apply_date = DataBaseValue.ConvObjectToDateTimeNull(row["apply_date"]),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
