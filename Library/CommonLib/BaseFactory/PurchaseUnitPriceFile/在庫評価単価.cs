//
// 在庫評価単価.cs
// 
// 在庫評価単価クラス
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
	/// <summary>
	/// 2-2 在庫評価単価
	/// </summary>
	public class 在庫評価単価
	{
		public string 商品コード { get; set; }
		public decimal 評価単価 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 在庫評価単価()
		{
			商品コード = string.Empty;
			評価単価 = 0;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<在庫評価単価> DataTableToList(DataTable table)
		{
			List<在庫評価単価> result = new List<在庫評価単価>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					在庫評価単価 data = new 在庫評価単価
					{
						商品コード = row["商品コード"].ToString().Trim(),
						評価単価 = DataBaseValue.ConvObjectToDecimal(row["評価単価"]),
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
