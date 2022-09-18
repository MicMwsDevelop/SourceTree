//
// vオンライン資格確認進捗管理.cs
// 
// vオンライン資格確認進捗管理ビュークラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/08/29 勝呂)
//
using CommonLib.Common;
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Sales.View
{
	public class vオンライン資格確認進捗管理
	{
		public YearMonth 進捗年月 { get; set; }
		public int 顧客No { get; set; }
		public string 得意先No { get; set; }
		public string 拠点名 { get; set; }
		public string 顧客名 { get; set; }
		public string 郵便番号 { get; set; }
		public string 住所 { get; set; }
		public string 電話番号 { get; set; }
		public string FAX番号 { get; set; }
		public string オン資担当 { get; set; }
		public string 導入意思 { get; set; }
		public string 工事種別 { get; set; }
		public string ステータス { get; set; }
		public string 現調完了月 { get; set; }
		public string 導入月 { get; set; }
		public string 都道府県 { get; set; }
		public string 担当部署 { get; set; }
		public string 価格帯 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public vオンライン資格確認進捗管理()
		{
			顧客No = 0;
			得意先No = string.Empty;
			拠点名 = string.Empty;
			顧客名 = string.Empty;
			郵便番号 = string.Empty;
			住所 = string.Empty;
			電話番号 = string.Empty;
			FAX番号 = string.Empty;
			オン資担当 = string.Empty;
			導入意思 = string.Empty;
			工事種別 = string.Empty;
			ステータス = string.Empty;
			現調完了月 = string.Empty;
			導入月 = string.Empty;
			都道府県 = string.Empty;
			担当部署 = string.Empty;
			価格帯 = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<vオンライン資格確認進捗管理> DataTableToList(DataTable table)
		{
			List<vオンライン資格確認進捗管理> result = new List<vオンライン資格確認進捗管理>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					vオンライン資格確認進捗管理 data = new vオンライン資格確認進捗管理();
					int ym = DataBaseValue.ConvObjectToInt(row["進捗年月"]);
					if (0 < ym)
					{
						data.進捗年月 = YearMonth.Parse(ym);
					}
					data.顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]);
					data.得意先No = row["得意先No"].ToString().Trim();
					data.拠点名 = row["拠点名"].ToString().Trim();
					data.顧客名 = row["顧客名"].ToString().Trim();
					data.郵便番号 = row["郵便番号"].ToString().Trim();
					data.住所 = row["住所"].ToString().Trim();
					data.電話番号 = row["電話番号"].ToString().Trim();
					data.FAX番号 = row["FAX番号"].ToString().Trim();
					data.オン資担当 = row["オン資担当"].ToString().Trim();
					data.導入意思 = row["導入意思"].ToString().Trim();
					data.工事種別 = row["工事種別"].ToString().Trim();
					data.ステータス = row["ステータス"].ToString().Trim();
					data.現調完了月 = row["現調完了月"].ToString().Trim();
					data.導入月 = row["導入月"].ToString().Trim();
					data.都道府県 = row["都道府県"].ToString().Trim();
					data.担当部署 = row["担当部署"].ToString().Trim();
					data.価格帯 = row["価格帯"].ToString().Trim();
					result.Add(data);
				}
			}
			return result;
		}
	}
}
