//
// SQLiteScanImageDataController.cs
// 
// 文書インデックスファイル管理 SQLiteデータベース詰め替えクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/05/16 勝呂)
//
using MwsLib.BaseFactory.ScanImageData;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SQLite.ScanImageData
{
	public static class SQLiteScanImageDataController
	{
		/// <summary>
		/// インデックスファイル情報の詰め替え
		/// INDEX_FILE
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>インデックスファイル情報</returns>
		public static List<IndexFileInfo> ConvertIndexFileInfo(DataTable table)
		{
			List<IndexFileInfo> result = null;
			if (null != table)
			{
				if (0 < table.Rows.Count)
				{
					result = new List<IndexFileInfo>();
					foreach (DataRow row in table.Rows)
					{
						IndexFileInfo info = new IndexFileInfo();
						info.TokuisakiNo = row["TOKUISAKI_NO"].ToString();
						info.CustomerNo = DataBaseValue.ConvObjectToInt(row["CUSTOMER_NO"]);
						info.ClinicName = row["CLINIC_NAME"].ToString();
						result.Add(info);
					}
				}
			}
			return result;
		}
	}
}
