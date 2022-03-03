//
// 当月仕入単価.cs
// 
// 当月仕入単価クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/01/14 勝呂)
//
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.PurchaseUnitPriceFile
{
	public class 当月仕入単価
	{
		public string 商品コード { get; set; }
		public decimal 単価 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 当月仕入単価()
		{
			商品コード = string.Empty;
			単価 = 0;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<当月仕入単価> DataTableToList(DataTable table)
		{
			List<当月仕入単価> result = new List<当月仕入単価>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					当月仕入単価 data = new 当月仕入単価
					{
						商品コード = row["商品コード"].ToString().Trim(),
						単価 = DataBaseValue.ConvObjectToDecimal(row["単価"]),
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
