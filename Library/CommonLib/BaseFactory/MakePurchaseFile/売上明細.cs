//
// 売上明細.cs
// 
// 売上明細クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2023/02/2 勝呂)
// Ver1.03 経理部の要請により、Microsoft365仕入データを部門毎の集計を止めて、得意先に関する記事データを追加(2023/02/10 勝呂)
//
using CommonLib.Common;
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.MakePurchaseFile
{
	/// <summary>
	/// PCA売上明細データ
	/// </summary>
	public class 売上明細
	{
		public int 伝票No { get; set; }
		public string 仕入先 { get; set; }
		public string 得意先番号 { get; set; }
		public string 得意先名 { get; set; }
		public int 売上日 { get; set; }
		public string sykd_jbmn { get; set; }
		public string sykd_jtan { get; set; }
		public string 摘要 { get; set; }
		public string 商品コード { get; set; }
		public short sykd_mkbn { get; set; }
		public string 商品名 { get; set; }
		public int 数量 { get; set; }
		public string 単位 { get; set; }
		public int 単価 { get; set; }
		public int 金額 { get; set; }
		public int 消費税率 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 売上明細()
		{
			伝票No = 0;
			仕入先 = string.Empty;
			得意先番号 = string.Empty;
			得意先名 = string.Empty;
			売上日 = 0;
			sykd_jbmn = string.Empty;
			sykd_jtan = string.Empty;
			摘要 = string.Empty;
			商品コード = string.Empty;
			sykd_mkbn = 0;
			商品名 = string.Empty;
			数量 = 0;
			単位 = string.Empty;
			単価 = 0;
			金額 = 0;
			消費税率 = 0;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<売上明細> DataTableToList(DataTable table)
		{
			List<売上明細> result = new List<売上明細>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					売上明細 data = new 売上明細
					{
						伝票No = DataBaseValue.ConvObjectToInt(row["伝票No"]),
						仕入先 = row["仕入先"].ToString().Trim(),
						得意先番号 = row["得意先番号"].ToString().Trim(),
						得意先名 = row["得意先名"].ToString().Trim(),
						売上日 = DataBaseValue.ConvObjectToInt(row["売上日"]),
						sykd_jbmn = row["sykd_jbmn"].ToString().Trim(),
						sykd_jtan = row["sykd_jtan"].ToString().Trim(),
						摘要 = row["摘要"].ToString().Trim(),
						商品コード = row["商品コード"].ToString().Trim(),
						sykd_mkbn = DataBaseValue.ConvObjectToShort(row["sykd_mkbn"]),
						商品名 = row["商品名"].ToString().Trim(),
						数量 = DataBaseValue.ConvObjectToInt(row["数量"]),
						単位 = row["単位"].ToString().Trim(),
						単価 = DataBaseValue.ConvObjectToInt(row["単価"]),
						金額 = DataBaseValue.ConvObjectToInt(row["金額"]),
						消費税率 = DataBaseValue.ConvObjectToInt(row["消費税率"])
					};
					result.Add(data);
				}
			}
			return result;
		}

		/// <summary>
		/// 得意先名を36byteで出力する
		/// </summary>
		/// <returns>得意先名</returns>
		public string GetCustmerName()
		{
			return MultiByteStrings.CutByMultiByteLength(得意先名, 36);
		}
	}

	/// <summary>
	/// PCA売上明細記事データ
	/// </summary>
	/// Ver1.03 経理部の要請により、Microsoft365仕入データを部門毎の集計を止めて、得意先に関する記事データを追加(2023/02/10 勝呂)
	public class 売上明細記事データ
	{
		public int 伝票No { get; set; }
		public string 請求先 { get; set; }
		public int 売上日 { get; set; }
		public string 商品コード { get; set; }
		public string 商品名 { get; set; }
		public string 得意先番号 { get; set; }
		public string 得意先名 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 売上明細記事データ()
		{
			伝票No = 0;
			請求先 = string.Empty;
			売上日 = 0;
			商品コード = string.Empty;
			商品名 = string.Empty;
			得意先番号 = string.Empty;
			得意先名 = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static 売上明細記事データ DataTableToData(DataTable table)
		{
			if (null != table && 1 == table.Rows.Count)
			{
				DataRow row = table.Rows[0];
				売上明細記事データ data = new 売上明細記事データ
				{
					伝票No = DataBaseValue.ConvObjectToInt(row["伝票No"]),
					請求先 = row["請求先"].ToString().Trim(),
					売上日 = DataBaseValue.ConvObjectToInt(row["売上日"]),
					商品コード = row["商品コード"].ToString().Trim(),
					商品名 = row["商品名"].ToString().Trim(),
					得意先番号 = row["得意先番号"].ToString().Trim(),
					得意先名 = row["得意先名"].ToString().Trim()
				};
				return data;
			}
			return null;
		}

		/// <summary>
		/// 得意先名を36byteで出力する
		/// </summary>
		/// <returns>得意先名</returns>
		public string GetCustmerName()
		{
			return MultiByteStrings.CutByMultiByteLength(得意先名, 36);
		}
	}
}
