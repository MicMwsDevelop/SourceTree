using MwsLib.Common;
using System;
using System.Collections.Generic;
using System.Data;
using MwsLib.BaseFactory.StockGoodsMaster;
using System.IO;

namespace MwsLib.DB.SQLite.StockGoodsMaster
{
	public static class SQLiteStockGoodsMasterAccess
	{
		/// <summary>
		/// 仕入商品マスタデータベース名
		/// </summary>
		public const string DATABASE_NAME = "仕入商品マスタ.db";

		/// <summary>
		/// OSクラウドデータバンクテーブル名
		/// </summary>
		public const string AOS_CLOUDDATABANK_TABLE_NAME = "AOS_CLOUDDATABANK";

		/// <summary>
		/// AOSクラウドデータバンク仕入商品マスタの取得
		/// </summary>
		/// <returns>仕入マスタ</returns>
		public static List<StockGoodsMasterData> GetStockGoodsMasterCloudDataBank()
		{
			DataTable table = SQLiteStockGoodsMasterGetIO.GetStockGoodsMasterCloudDataBank(Directory.GetCurrentDirectory());
			return SQLiteStockGoodsMasterController.ConvertStockGoodsMaster(table);
		}
	}
}
