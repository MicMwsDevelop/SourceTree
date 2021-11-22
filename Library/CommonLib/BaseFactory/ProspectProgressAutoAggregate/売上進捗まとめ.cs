//
// 売上進捗まとめ.cs
//
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/08/04 勝呂)
//
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.ProspectProgressAutoAggregate
{
	/// <summary>
	/// 売上進捗まとめ
	/// </summary>
	public class 売上進捗まとめ
	{
		public string 売上月 { get; set; }
		public string 営業部コード { get; set; }
		public string 営業部名 { get; set; }
		public string 拠点コード { get; set; }
		public string 拠点名 { get; set; }
		public string 担当者コード { get; set; }
		public string 担当者 { get; set; }
		public int 受注番号 { get; set; }
		public int 顧客No { get; set; }
		public string 顧客名 { get; set; }
		public string 商品コード { get; set; }
		public int 数量 { get; set; }
		public string 課金開始日 { get; set; }
		public string 課金終了日 { get; set; }
		public int 金額 { get; set; }
		
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 売上進捗まとめ()
		{
			売上月 = string.Empty;
			営業部コード = string.Empty;
			営業部名 = string.Empty;
			拠点コード = string.Empty;
			拠点名 = string.Empty;
			担当者コード = string.Empty;
			担当者 = string.Empty;
			受注番号 = 0;
			顧客No = 0;
			顧客名 = string.Empty;
			商品コード = string.Empty;
			数量 = 0;
			課金開始日 = string.Empty;
			課金終了日 = string.Empty;
			金額 = 0;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<売上進捗まとめ> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<売上進捗まとめ> result = new List<売上進捗まとめ>();
				foreach (DataRow row in table.Rows)
				{
					売上進捗まとめ data = new 売上進捗まとめ
					{
						売上月 = row["売上月"].ToString().Trim(),
						営業部コード = row["営業部コード"].ToString().Trim(),
						営業部名 = row["営業部名"].ToString().Trim(),
						拠点コード = row["拠点コード"].ToString().Trim(),
						拠点名 = row["拠点名"].ToString().Trim(),
						担当者コード = row["担当者コード"].ToString().Trim(),
						担当者 = row["担当者"].ToString().Trim(),
						受注番号 = DataBaseValue.ConvObjectToInt(row["受注番号"]),
						顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]),
						顧客名 = row["顧客名"].ToString().Trim(),
						商品コード = row["商品コード"].ToString().Trim(),
						数量 = DataBaseValue.ConvObjectToInt(row["数量"]),
						課金開始日 = row["課金開始日"].ToString().Trim(),
						課金終了日 = row["課金終了日"].ToString().Trim(),
						金額 = DataBaseValue.ConvObjectToInt(row["金額"]),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
