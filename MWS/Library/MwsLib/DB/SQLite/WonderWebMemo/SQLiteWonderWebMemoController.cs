//
// SQLiteWonderWebMemoController.cs
// 
// WonderWebメモ情報 SQLiteデータベース詰め替えクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/08/09 勝呂)
//
using MwsLib.BaseFactory.WonderWebMemo;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SQLite.WonderWebMemo
{
	public static class SQLiteWonderWebMemoController
	{
		/// <summary>
		/// 銀行振込請求書発行先メモ情報の詰め替え
		/// INDEX_FILE
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>インデックスファイル情報</returns>
		public static List<MemoBankTransfer> ConvertBankTransfer(DataTable table)
		{
			List<MemoBankTransfer> result = null;
			if (null != table)
			{
				if (0 < table.Rows.Count)
				{
					result = new List<MemoBankTransfer>();
					foreach (DataRow row in table.Rows)
					{
						MemoBankTransfer info = new MemoBankTransfer();
						info.TokuisakiNo = row["TOKUISAKI_NO"].ToString();
						info.BillingAmount = DataBaseValue.ConvObjectToInt(row["BILLING_AMOUNT"]);
						result.Add(info);
					}
				}
			}
			return result;
		}
	}
}
