//
// SQLiteStockGoodsMasterController.cs
// 
// 仕入商品マスタ情報 SQLiteデータベース詰め替えクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/03/19 勝呂)
//
using MwsLib.BaseFactory.StockGoodsMaster;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SQLite.StockGoodsMaster
{
	public static class SQLiteStockGoodsMasterController
	{
		/// <summary>
		/// 仕入商品マスタ情報の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>仕入商品マスタ情報</returns>
		public static List<StockGoodsMasterData> ConvertStockGoodsMaster(DataTable table)
		{
			List<StockGoodsMasterData> result = null;
			if (null != table)
			{
				if (0 < table.Rows.Count)
				{
					result = new List<StockGoodsMasterData>();
					foreach (DataRow row in table.Rows)
					{
						StockGoodsMasterData data = new StockGoodsMasterData();
						data.商品コード = row["商品コード"].ToString();
						data.仕入商品コード = row["仕入商品コード"].ToString();
						data.仕入価格 = DataBaseValue.ConvObjectToInt(row["仕入価格"]);
						data.仕入先 = row["仕入先"].ToString();
						data.商品名 = row["商品名"].ToString();
						data.仕入フラグ = DataBaseValue.ConvObjectToBool(row["仕入フラグ"]);
						result.Add(data);
					}
				}
			}
			return result;
		}
	}
}
