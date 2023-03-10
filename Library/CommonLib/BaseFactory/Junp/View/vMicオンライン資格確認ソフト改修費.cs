//
// vMicオンライン資格確認ソフト改修費.cs
//
// vMicオンライン資格確認ソフト改修費
// [JunpDB].[dbo].[vMicオンライン資格確認ソフト改修費]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/12/28 勝呂)
//
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.View
{
	public class vMicオンライン資格確認ソフト改修費
	{
		public int 受注番号 { get; set; }
		public DateTime? 受注日 { get; set; }
		public DateTime? 受注承認日 { get; set; }
		public DateTime? 売上承認日 { get; set; }
		public int 顧客No { get; set; }
		public string 顧客名 { get; set; }
		public string 商品コード { get; set; }
		public string 商品名 { get; set; }
		public int 受注金額 { get; set; }
		public int 販売先コード { get; set; }
		public string 販売先 { get; set; }
		public string 拠点コード { get; set; }
		public string 拠点名 { get; set; }
		public string 担当者コード { get; set; }
		public string 担当者名 { get; set; }
		public string 件名 { get; set; }

		/// <summary>
		/// 受注金額（税込）の取得
		/// 消費税率10%固定
		/// </summary>
		public double 受注金額税込
		{
			get
			{
				if (0 < 受注金額)
				{
					return 受注金額 * 1.1;
				}
				return 0;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public vMicオンライン資格確認ソフト改修費()
		{
			受注番号 = 0;
			受注日 = null;
			受注承認日 = null;
			売上承認日 = null;
			顧客No = 0;
			顧客名 = string.Empty;
			商品コード = string.Empty;
			商品名 = string.Empty;
			受注金額 = 0;
			販売先コード = 0;
			販売先 = string.Empty;
			拠点コード = string.Empty;
			拠点名 = string.Empty;
			担当者コード = string.Empty;
			担当者名 = string.Empty;
			件名 = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<vMicオンライン資格確認ソフト改修費> DataTableToList(DataTable table)
		{
			List<vMicオンライン資格確認ソフト改修費> result = new List<vMicオンライン資格確認ソフト改修費>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					vMicオンライン資格確認ソフト改修費 data = new vMicオンライン資格確認ソフト改修費
					{
						受注番号 = DataBaseValue.ConvObjectToInt(row["受注番号"]),
						受注日 = DataBaseValue.ConvObjectToDateTime(row["受注日"]),
						受注承認日 = DataBaseValue.ConvObjectToDateTime(row["受注承認日"]),
						売上承認日 = DataBaseValue.ConvObjectToDateTime(row["売上承認日"]),
						顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]),
						顧客名 = row["顧客名"].ToString().Trim(),
						商品コード = row["商品コード"].ToString().Trim(),
						商品名 = row["商品名"].ToString().Trim(),
						受注金額 = DataBaseValue.ConvObjectToInt(row["受注金額"]),
						販売先コード = DataBaseValue.ConvObjectToInt(row["販売先コード"]),
						販売先 = row["販売先"].ToString().Trim(),
						拠点コード = row["拠点コード"].ToString().Trim(),
						拠点名 = row["拠点名"].ToString().Trim(),
						担当者コード = row["担当者コード"].ToString().Trim(),
						担当者名 = row["担当者名"].ToString().Trim(),
						件名 = row["件名"].ToString().Trim(),
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
