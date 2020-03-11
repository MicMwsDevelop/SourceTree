//
// CloudDataBankSaleAccess.cs
//
// クラウドデータバンクPCA売上情報 データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/03/06 勝呂)
// 
using MwsLib.BaseFactory.CloudDataBankStockDataOutput;
using MwsLib.Common;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.CloudDataBankStockDataOutput
{
	public static class CloudDataBankSaleAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 先月のクラウドデータバンクPCA売上情報リストの取得
		/// </summary>
		/// <param name="date">当日</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>クラウドデータバンクPCA売上情報リスト</returns>
		public static List<CloudDataBankSaleData> GetCloudDataBankSaleList(Date date, bool sqlsv2)
		{
			DataTable table = CloudDataBankSaleGetIO.GetCloudDataBankSaleList(date, sqlsv2);
			return CloudDataBankSaleController.ConvertCloudDataBankSaleList(table);
		}
	}
}
