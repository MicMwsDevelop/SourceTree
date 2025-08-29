//
// vMicPCA区分マスタ.cs
//
// [JunpDB].[dbo].[vMicPCA区分マスタ]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2025/06/23 勝呂):新規作成
//
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.View
{
	public class vMicPCA区分マスタ
	{
		public short ems_id { get; set; }
		public int ems_kbn { get; set; }
		public string ems_str { get; set; }
		public int ems_kosin { get; set; }
		public DateTime? ems_upddate { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public vMicPCA区分マスタ()
	  {
			ems_id = 0;
			ems_kbn = 0;
			ems_str = string.Empty;
			ems_kosin = 0;
			ems_upddate = null;
	  }

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<vMicPCA区分マスタ> DataTableToList(DataTable table)
		{
			List<vMicPCA区分マスタ> result = new List<vMicPCA区分マスタ>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					vMicPCA区分マスタ data = new vMicPCA区分マスタ
					{
						ems_id = DataBaseValue.ConvObjectToShort(row["ems_id"]),
						ems_kbn = DataBaseValue.ConvObjectToInt(row["ems_kbn"]),
						ems_str = row["ems_str"].ToString().Trim(),
						ems_kosin = DataBaseValue.ConvObjectToInt(row["ems_kosin"]),
						ems_upddate = DataBaseValue.ConvObjectToDateTimeNull(row["ems_upddate"])
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
