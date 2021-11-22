//
// NouhinGoods.cs
// 
// 配送センター納品物情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/10/25 勝呂)
//
using CommonLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryCenterPrintNouhin.Print
{
	/// <summary>
	/// 納品物情報
	/// </summary>
	public class NouhinGoods
	{
		/// <summary>
		/// 品名
		/// </summary>
		public string GoodsName { get; set; }

		/// <summary>
		/// 数量
		/// </summary>
		public string Count { get; set; }

		/// <summary>
		/// 単位
		/// </summary>
		public string Unit { get; set; }

		/// <summary>
		/// 単価
		/// </summary>
		public string Tanka { get; set; }

		/// <summary>
		/// 金額
		/// </summary>
		public string Price { get; set; }

		/// <summary>
		/// 備考
		/// </summary>
		public string Biko { get; set; }

		/// <summary>
		/// 合計
		/// </summary>
		public string Total { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public NouhinGoods()
		{
			GoodsName = string.Empty;
			Count = string.Empty;
			Unit = string.Empty;
			Tanka = string.Empty;
			Price = string.Empty;
			Biko = string.Empty;
			Total = string.Empty;
		}

		/// <summary>
		/// CSVレコードの設定
		/// </summary>
		/// <param name="split">CSVレコード</param>
		public void SetGoodsData(List<string> split)
		{
			if (NouhinData.FIELD_COUNT == split.Count)
			{
				GoodsName = split[14];
				Count = split[15];
				Unit = split[16];
				Tanka = split[17];
				Price = split[18];
				Biko = split[19];
				Total = split[20];
			}
		}

		/// <summary>
		/// テスト印刷
		/// </summary>
		/// <returns></returns>
		public static NouhinGoods GetTestData()
		{
			NouhinGoods test = new NouhinGoods();
			test.GoodsName = "ⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩⅩ";
			test.Count = "9,999,999";
			test.Unit = "ⅩⅩ";
			test.Tanka = "999999999";
			test.Price = "999999999";
			test.Biko = "999999999";
			test.Total = "999999999";
			return test;
		}
	}
}
