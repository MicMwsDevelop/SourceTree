//
// FaxOrderSheetGoods.cs
// 
// 消耗品FAXオーダーシート商品情報
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.03(2021/09/02):消耗品FAXオーダーシートの新規追加
//
using CommonLib.Common;

namespace VariousDocumentOut
{
	/// <summary>
	/// 消耗品FAXオーダーシート商品情報
	/// </summary>
	public class FaxOrderSheetGoods
	{
		/// <summary>
		/// 商品コード
		/// </summary>
		public string GoodsCode { get; set; }

		/// <summary>
		/// 商品名
		/// </summary>
		public string GoodsName { get; set; }

		/// <summary>
		/// 単価
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// 入数
		/// </summary>
		public string Unit { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public FaxOrderSheetGoods()
		{
			GoodsCode = string.Empty;
			GoodsCode = string.Empty;
			Price = 0;
			Unit = string.Empty;
		}

		/// <summary>
		/// 税込単価の取得
		/// </summary>
		/// <param name="tax"></param>
		/// <returns>税込単価</returns>
		public int 税込単価(int taxRate)
		{
			int tax = CalcTax.GetTax(taxRate, CalcTax.RoundFraction.Round, Price);
			return Price + tax;
		}
	}
}
