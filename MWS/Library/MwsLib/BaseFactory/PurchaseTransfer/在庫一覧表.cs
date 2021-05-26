//
// 在庫一覧表.cs
// 
// 在庫一覧表クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/05/26 勝呂)
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MwsLib.Common;

namespace MwsLib.BaseFactory.PurchaseTransfer
{
	public class 在庫一覧表
	{
		public string 倉庫ｺｰﾄﾞ { get; set; }
		public string 倉庫名 { get; set; }
		public short ﾃﾞｰﾀ区分 { get; set; }
		public string 商品ｺｰﾄﾞ { get; set; }
		public string 品名 { get; set; }
		public int 繰越在庫 { get; set; }
		public int 入荷数 { get; set; }
		public int 出荷数 { get; set; }
		public int 現品数 { get; set; }
		public int 在庫数 { get; set; }
		public int 評価単価 { get; set; }
		public int 在庫金額 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 在庫一覧表()
		{
			倉庫ｺｰﾄﾞ = string.Empty;
			倉庫名 = string.Empty;
			ﾃﾞｰﾀ区分 = 0;
			商品ｺｰﾄﾞ = string.Empty;
			品名 = string.Empty;
			繰越在庫 = 0;
			入荷数 = 0;
			出荷数 = 0;
			現品数 = 0;
			在庫数 = 0;
			評価単価 = 0;
			在庫金額 = 0;
		}

		public 在庫一覧表(string[] buf)
		{
			倉庫ｺｰﾄﾞ = buf[0];
			倉庫名 = buf[1];
			ﾃﾞｰﾀ区分 = (short)buf[2].ToInt();
			商品ｺｰﾄﾞ = buf[3];
			品名 = buf[4];
			繰越在庫 = buf[5].ToInt();
			入荷数 = buf[6].ToInt();
			出荷数 = buf[7].ToInt();
			現品数 = buf[8].ToInt();
			在庫数 = buf[9].ToInt();
			評価単価 = buf[10].ToInt();
			在庫金額 = buf[11].ToInt();
		}

		public static List<Tuple<string, int>> 在庫評価単価(List<在庫一覧表> list)
		{
/*
			return list.GroupBy(g => new { g.商品ｺｰﾄﾞ, g.評価単価 })
				.OrderBy(o => o.Key.商品ｺｰﾄﾞ)
				.Where(w => w.Key.商品ｺｰﾄﾞ != "" && w.Key.評価単価 != 0)
				.Select(s => new {商品ｺｰﾄﾞ = s.Key.商品ｺｰﾄﾞ, 評価単価 = s.Key.評価単価 })
				.AsEnumerable()
				.Select(s => new Tuple<string, int>(s.商品ｺｰﾄﾞ, s.評価単価))
				.ToList();
*/
			var query = from 在庫一覧表 in list
						orderby 在庫一覧表.商品ｺｰﾄﾞ
						where 在庫一覧表.商品ｺｰﾄﾞ != "" && 在庫一覧表.評価単価 != 0
						group 在庫一覧表 by new { 在庫一覧表.商品ｺｰﾄﾞ, 在庫一覧表.評価単価 } into X
						select new { X.Key.商品ｺｰﾄﾞ, X.Key.評価単価 };
			List<Tuple<string, int>> result = new List<Tuple<string, int>>();
			foreach (var a in query)
			{
				result.Add(new Tuple<string, int>(a.商品ｺｰﾄﾞ, a.評価単価));
			}
			return result;
		}
	}
}
