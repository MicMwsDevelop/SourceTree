//
// 販売店区分参照ビュー.cs
//
// [CharlieDB].[dbo].[販売店区分参照ビュー]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.BaseFactory.Charlie.View
{
	public class 販売店区分参照ビュー
	{
		public int 区分コード { get; set; }
		public string 区分名 { get; set; }
		public string ランク { get; set; }
		public string PCA区分名称 { get; set; }
		public int 手数料率 { get; set; }
		public string 開始報奨 { get; set; }
		public int 月額区分 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 販売店区分参照ビュー()
		{
			区分コード = 0;
			区分名 = string.Empty;
			ランク = string.Empty;
			PCA区分名称 = string.Empty;
			手数料率 = 0;
			開始報奨 = string.Empty;
			月額区分 = 0;
		}

		/// <summary>
		/// [charlieDB].[dbo].[販売店区分参照ビュー]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>販売店区分参照ビュー</returns>
		public static List<販売店区分参照ビュー> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<販売店区分参照ビュー> result = new List<販売店区分参照ビュー>();
				foreach (DataRow row in table.Rows)
				{
					販売店区分参照ビュー data = new 販売店区分参照ビュー
					{
						区分コード = DataBaseValue.ConvObjectToInt(row["区分コード"]),
						区分名 = row["区分名"].ToString().Trim(),
						ランク = row["ランク"].ToString().Trim(),
						PCA区分名称 = row["PCA区分名称"].ToString().Trim(),
						手数料率 = DataBaseValue.ConvObjectToInt(row["手数料率"]),
						開始報奨 = row["開始報奨"].ToString().Trim(),
						月額区分 = DataBaseValue.ConvObjectToInt(row["月額区分"]),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
