//
// WW伝票参照ビュー.cs
//
// [CharlieDB].[dbo].[WW伝票参照ビュー]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.View
{
	public class WW伝票参照ビュー
	{
		public int 伝票No { get; set; }
		public int 販売先顧客ID { get; set; }
		public int ユーザー顧客ID { get; set; }
		public string 担当者ID { get; set; }
		public string 担当者名 { get; set; }
		public string 担当支店ID { get; set; }
		public string 担当支店名 { get; set; }
		public DateTime? 受注年月日 { get; set; }
		public DateTime? 受注承認日 { get; set; }
		public DateTime? 売上承認日 { get; set; }
		public string 商品コード { get; set; }
		public string 商品名 { get; set; }
		public short 商品区分 { get; set; }
		public int 数量 { get; set; }
		public int 販売価格 { get; set; }
		public int 申込種別 { get; set; }
		public string システム略称 { get; set; }
		public DateTime? 最終出力日時 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public WW伝票参照ビュー()
		{
			伝票No = 0;
			販売先顧客ID = 0;
			ユーザー顧客ID = 0;
			担当者ID = string.Empty;
			担当者名 = string.Empty;
			担当支店ID = string.Empty;
			担当支店名 = string.Empty;
			受注年月日 = null;
			受注承認日 = null;
			売上承認日 = null;
			商品コード = string.Empty;
			商品名 = string.Empty;
			商品区分 = 0;
			数量 = 0;
			販売価格 = 0;
			申込種別 = 0;
			システム略称 = string.Empty;
			最終出力日時 = null;
		}

		/// <summary>
		/// [charlieDB].[dbo].[WW伝票参照ビュー]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>WW伝票参照ビュー</returns>
		public static List<WW伝票参照ビュー> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<WW伝票参照ビュー> result = new List<WW伝票参照ビュー>();
				foreach (DataRow row in table.Rows)
				{
					WW伝票参照ビュー data = new WW伝票参照ビュー
					{
						伝票No = DataBaseValue.ConvObjectToInt(row["伝票No"]),
						販売先顧客ID = DataBaseValue.ConvObjectToInt(row["販売先顧客ID"]),
						ユーザー顧客ID = DataBaseValue.ConvObjectToInt(row["ユーザー顧客ID"]),
						担当者ID = row["担当者ID"].ToString().Trim(),
						担当者名 = row["担当者名"].ToString().Trim(),
						担当支店ID = row["担当支店ID"].ToString().Trim(),
						担当支店名 = row["担当支店名"].ToString().Trim(),
						受注年月日 = DataBaseValue.ConvObjectToDateTimeNull(row["受注年月日"]),
						受注承認日 = DataBaseValue.ConvObjectToDateTimeNull(row["受注承認日"]),
						売上承認日 = DataBaseValue.ConvObjectToDateTimeNull(row["売上承認日"]),
						商品コード = row["商品コード"].ToString().Trim(),
						商品名 = row["商品名"].ToString().Trim(),
						商品区分 = DataBaseValue.ConvObjectToShort(row["商品区分"]),
						数量 = DataBaseValue.ConvObjectToInt(row["数量"]),
						販売価格 = DataBaseValue.ConvObjectToInt(row["販売価格"]),
						申込種別 = DataBaseValue.ConvObjectToInt(row["申込種別"]),
						システム略称 = row["システム略称"].ToString().Trim(),
						最終出力日時 = DataBaseValue.ConvObjectToDateTimeNull(row["最終出力日時"]),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
