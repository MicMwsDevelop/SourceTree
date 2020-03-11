//
// CloudDataBankSaleController.cs
//
// クラウドデータバンクPCA売上情報 データ詰め替えクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/03/06 勝呂)
// 
using MwsLib.BaseFactory.CloudDataBankStockDataOutput;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.CloudDataBankStockDataOutput
{
	public static class CloudDataBankSaleController
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// クラウドデータバンクPCA売上情報リストの詰め替え
		/// </summary>
		/// <param name="table">DataTable</param>
		/// <returns>クラウドデータバンクPCA売上情報リスト</returns>
		public static List<CloudDataBankSaleData> ConvertCloudDataBankSaleList(DataTable table)
		{
			List<CloudDataBankSaleData> result = null;
			if (null != table)
			{
				result = new List<CloudDataBankSaleData>();
				foreach (DataRow row in table.Rows)
				{
					CloudDataBankSaleData data = new CloudDataBankSaleData
					{
						仕入先コード = row["仕入先コード"].ToString().Trim(),
						sykd_jbmn = row["sykd_jbmn"].ToString().Trim(),
						sykd_jtan = row["sykd_jtan"].ToString().Trim(),
						仕入商品コード = row["仕入商品コード"].ToString().Trim(),
						sykd_mkbn = row["sykd_mkbn"].ToString().Trim(),
						商品名 = row["商品名"].ToString().Trim(),
						数量 = DataBaseValue.ConvObjectToInt(row["数量"]),
						sykd_tani = row["sykd_tani"].ToString().Trim(),
						仕入価格 = DataBaseValue.ConvObjectToInt(row["仕入価格"]),
						売上日 = DataBaseValue.ConvObjectToInt(row["売上日"]),
						仕入フラグ = row["仕入フラグ"].ToString().Trim(),
						消費税率 = (short)DataBaseValue.ConvObjectToInt(row["消費税率"])
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
