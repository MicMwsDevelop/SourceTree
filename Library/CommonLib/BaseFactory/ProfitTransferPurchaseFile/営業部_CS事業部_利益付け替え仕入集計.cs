//
// 営業部_CS事業部_利益付け替え仕入集計.cs
// 
// 部署間利益付け替え仕入データ作成 営業部_CS事業部_利益付け替え仕入集計
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/09/22 越田)
//
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.BaseFactory.ProfitTransferPurchaseFile
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// ・PRO営業部→CS事業部 51期からの役務利益付け替えで利用
	/// ・SOL営業部→CS事業部 51期からの役務利益付け替えで利用
	/// </remarks>
	public class 営業部_CS事業部_利益付け替え仕入集計
	{
		public int 売上日 { get; set; }

		public string 委託元_部門コード { get; set; }

		public string 委託元_担当者コード { get; set; }

		public string 商品コード { get; set; }

		public string 商品名 { get; set; }

		public int 数量 { get; set; }

		public int 金額 { get; set; }

		public int 単価 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>
		/// ※PRO営業部→CS事業部 51期役務利益付け替え では原価金額で利益付け替え
		/// ※SOL営業部→CS事業部 51期役務利益付け替え では売上金額で利益付け替え
		/// </remarks>
		public int 売上金額 { get; set; }

		public int 原単価 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>
		/// ※PRO営業部→CS事業部 51期役務利益付け替え では原価金額で利益付け替え
		/// ※SOL営業部→CS事業部 51期役務利益付け替え では売上金額で利益付け替え
		/// </remarks>
		public int 原価金額 { get; set; }

		public short 税区分 { get; set; }

		public short 税込区分 { get; set; }

		public int 売上伝票No { get; set; }

		public int 顧客No { get; set; }

		public string 顧客名 { get; set; }

		public int 税率 { get; set; }


		public string 委託先_部門コード { get; set; }

		public string 委託先_担当者コード { get; set; }


		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 営業部_CS事業部_利益付け替え仕入集計()
		{
			売上日 = 0;
			委託元_部門コード = string.Empty;
			委託元_担当者コード = string.Empty;
			商品コード = string.Empty;
			商品名 = string.Empty;
			数量 = 0;
			単価 = 0;
			売上金額 = 0;
			原単価 = 0;
			原価金額 = 0;
			税区分 = 0;
			税込区分 = 0;
			売上伝票No = 0;
			顧客No = 0;
			顧客名 = string.Empty;
			税率 = 0;
			委託先_部門コード = string.Empty;
			委託先_担当者コード = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<営業部_CS事業部_利益付け替え仕入集計> DataTableToList(DataTable table)
		{
			var result = new List<営業部_CS事業部_利益付け替え仕入集計>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					var data = new 営業部_CS事業部_利益付け替え仕入集計
					{
						売上日 = DataBaseValue.ConvObjectToInt(row["売上日"]),
						委託元_部門コード = row["委託元_部門コード"].ToString().Trim(),
						委託元_担当者コード = row["委託元_担当者コード"].ToString().Trim(),
						商品コード = row["商品コード"].ToString().Trim(),
						商品名 = row["商品名"].ToString().Trim(),
						数量 = DataBaseValue.ConvObjectToInt(row["数量"]),
						単価 = DataBaseValue.ConvObjectToInt(row["単価"]),
						売上金額 = DataBaseValue.ConvObjectToInt(row["売上金額"]),
						原単価 = DataBaseValue.ConvObjectToInt(row["原単価"]),
						原価金額 = DataBaseValue.ConvObjectToInt(row["原価金額"]),
						税区分 = DataBaseValue.ConvObjectToShort(row["税区分"]),
						税込区分 = DataBaseValue.ConvObjectToShort(row["税込区分"]),
						売上伝票No = DataBaseValue.ConvObjectToInt(row["売上伝票No"]),
						顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]),
						顧客名 = row["顧客名"].ToString().Trim(),
						税率 = DataBaseValue.ConvObjectToInt(row["税率"]),
						委託先_部門コード = row["委託先_部門コード"].ToString().Trim(),
						委託先_担当者コード = row["委託先_担当者コード"].ToString().Trim()
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
