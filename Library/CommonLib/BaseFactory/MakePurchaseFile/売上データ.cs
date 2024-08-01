//
// 売上データ.cs
// 
// 売上データクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// VerX.XX(2024/06/25 勝呂):課金データ作成売上データ圧縮対応
//
using CommonLib.Common;
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.MakePurchaseFile
{
	/// <summary>
	/// vMicPCA売上明細からPCA売上明細データを作成
	/// </summary>
	public class 売上データ
	{
		public int 伝票No { get; set; }
		public int 売上日 { get; set; }
		public int 部門コード { get; set; }
		public int 担当者コード { get; set; }
		public int マスター区分 { get; set; }
		public string 摘要名 { get; set; }
		public string 商品コード { get; set; }
		public string 商品名 { get; set; }
		public int 数量 { get; set; }
		public string 単位 { get; set; }
		public int 税率 { get; set; }
		public int 枝番 { get; set; }
		public string 得意先コード { get; set; }
		public string 得意先名 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 売上データ()
		{
			伝票No = 0;
			売上日 = 0;
			部門コード = 0;
			担当者コード = 0;
			マスター区分 = 0;
			摘要名 = string.Empty;
			商品コード = string.Empty;
			商品名 = string.Empty;
			数量 = 0;
			単位 = string.Empty;
			税率 = 0;
			枝番 = 0;
			得意先コード = string.Empty;
			得意先名 = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<売上データ> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<売上データ> dataList = new List<売上データ>();
				foreach (DataRow row in table.Rows)
				{
					売上データ data = new 売上データ
					{
						伝票No = DataBaseValue.ConvObjectToInt(row["伝票No"]),
						売上日 = DataBaseValue.ConvObjectToInt(row["売上日"]),
						部門コード = DataBaseValue.ConvObjectToInt(row["部門コード"]),
						担当者コード = DataBaseValue.ConvObjectToInt(row["担当者コード"]),
						マスター区分 = DataBaseValue.ConvObjectToInt(row["マスター区分"]),
						摘要名 = row["摘要名"].ToString().Trim(),
						商品コード = row["商品コード"].ToString().Trim(),
						商品名 = row["商品名"].ToString().Trim(),
						数量 = DataBaseValue.ConvObjectToInt(row["数量"]),
						単位 = row["単位"].ToString().Trim(),
						税率 = DataBaseValue.ConvObjectToInt(row["税率"]),
						枝番 = DataBaseValue.ConvObjectToInt(row["枝番"]),
						得意先コード = row["得意先コード"].ToString().Trim(),
						得意先名 = row["得意先名"].ToString().Trim(),
					};
					dataList.Add(data);
				}
				return dataList;
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

		/// <summary>
		/// 商品名から得意先Noを取得
		/// ex. 得意先No. 010490
		/// </summary>
		/// <returns></returns>
		public string GetTokuisakiCodeInGoodsName()
		{
			return 商品名.Replace("得意先No. ", "").Trim();
		}
	}
}

