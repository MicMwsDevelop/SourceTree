//
// PCA仕入明細貯蔵品.cs
// 
// PCA仕入明細貯蔵品クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/05/28 勝呂)
//
using MwsLib.DB;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.BaseFactory.PurchaseTransfer
{
	public class PCA仕入明細貯蔵品
	{
		public short 入荷方法 { get; set; }
		public short 科目区分 { get; set; }
		public string 伝区 { get; set; }
		public int 仕入日 { get; set; }
		public int 精算日 { get; set; }
		public int 伝票No { get; set; }
		public string 仕入先コード { get; set; }
		public string 仕入先名 { get; set; }
		public string 先方担当者名 { get; set; }
		public string 部門コード { get; set; }
		public string 担当者コード { get; set; }
		public string 摘要コード { get; set; }
		public string 摘要名 { get; set; }
		public string 商品コード { get; set; }
		public short マスター区分 { get; set; }
		public string 品名 { get; set; }
		public short 区 { get; set; }
		public string 倉庫コード { get; set; }
		public decimal 入数 { get; set; }
		public decimal 箱数 { get; set; }
		public decimal 数量 { get; set; }
		public string 単位 { get; set; }
		public decimal 単価 { get; set; }
		public decimal 金額 { get; set; }
		public decimal 外税額 { get; set; }
		public decimal 内税額 { get; set; }
		public short 税区分 { get; set; }
		public short 税込区分 { get; set; }
		public string 備考 { get; set; }
		public string 規格型番 { get; set; }
		public string 色 { get; set; }
		public string サイズ { get; set; }
		public int 計算式コード { get; set; }
		public int 商品項目1 { get; set; }
		public int 商品項目2 { get; set; }
		public int 商品項目3 { get; set; }
		public int 仕入項目1 { get; set; }
		public int 仕入項目2 { get; set; }
		public int 仕入項目3 { get; set; }
		public int 税率 { get; set; }
		public int 伝票消費税外税 { get; set; }
		public string プロジェクトコード { get; set; }
		public string 伝票No2 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PCA仕入明細貯蔵品()
		{
			this.Clear();
		}

		/// <summary>
		/// クリア
		/// </summary>
		protected void Clear()
		{
			入荷方法 = 0;
			科目区分 = 0;
			伝区 = string.Empty;
			仕入日 = 0;
			精算日 = 0;
			伝票No = 0;
			仕入先コード = string.Empty;
			仕入先名 = string.Empty;
			先方担当者名 = string.Empty;
			部門コード = string.Empty;
			担当者コード = string.Empty;
			摘要コード = string.Empty;
			摘要名 = string.Empty;
			商品コード = string.Empty;
			マスター区分 = 0;
			品名 = string.Empty;
			区 = 0;
			倉庫コード = string.Empty;
			入数 = 0;
			箱数 = 0;
			数量 = 0;
			単位 = string.Empty;
			単価 = 0;
			金額 = 0;
			外税額 = 0;
			内税額 = 0;
			税区分 = 0;
			税込区分 = 0;
			備考 = string.Empty;
			規格型番 = string.Empty;
			色 = string.Empty;
			サイズ = string.Empty;
			計算式コード = 0;
			商品項目1 = 0;
			商品項目2 = 0;
			商品項目3 = 0;
			仕入項目1 = 0;
			仕入項目2 = 0;
			仕入項目3 = 0;
			税率 = 0;
			伝票消費税外税 = 0;
			プロジェクトコード = string.Empty;
			伝票No2 = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<PCA仕入明細貯蔵品> DataTableToList(DataTable table)
		{
			List<PCA仕入明細貯蔵品> result = new List<PCA仕入明細貯蔵品>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					PCA仕入明細貯蔵品 data = new PCA仕入明細貯蔵品
					{
						入荷方法 = DataBaseValue.ConvObjectToShort(row["nykd_hoho"]),
						科目区分 = DataBaseValue.ConvObjectToShort(row["nykd_flid"]),
						伝区 = row["nykd_denku"].ToString().Trim(),
						仕入日 = DataBaseValue.ConvObjectToInt(row["nykd_uribi"]),
						精算日 = DataBaseValue.ConvObjectToInt(row["nykd_seibi"]),
						伝票No = DataBaseValue.ConvObjectToInt(row["nykd_denno"]),
						仕入先コード = row["nykd_tcd"].ToString().Trim(),
						仕入先名 = row["rms_mei1"].ToString().Trim(),
						先方担当者名 = row["rms_tanmei"].ToString().Trim(),
						部門コード = row["nykd_jbmn"].ToString().Trim(),
						担当者コード = row["nykd_jtan"].ToString().Trim(),
						摘要コード = row["nykd_tekcd"].ToString().Trim(),
						摘要名 = row["nykd_tekmei"].ToString().Trim(),
						商品コード = row["nykd_scd"].ToString().Trim(),
						マスター区分 = DataBaseValue.ConvObjectToShort(row["nykd_mkbn"]),
						品名 = row["nykd_mei"].ToString().Trim(),
						区 = DataBaseValue.ConvObjectToShort(row["nykd_ku"]),
						倉庫コード = row["nykd_souko"].ToString().Trim(),
						入数 = DataBaseValue.ConvObjectToDecimal(row["nykd_iri"]),
						箱数 = DataBaseValue.ConvObjectToDecimal(row["nykd_hako"]),
						数量 = DataBaseValue.ConvObjectToDecimal(row["nykd_suryo"]),
						単位 = row["nykd_tani"].ToString().Trim(),
						単価 = DataBaseValue.ConvObjectToDecimal(row["nykd_tanka"]),
						金額 = DataBaseValue.ConvObjectToDecimal(row["nykd_kingaku"]),
						外税額 = DataBaseValue.ConvObjectToDecimal(row["nykd_zei"]),
						内税額 = DataBaseValue.ConvObjectToDecimal(row["nykd_uchi"]),
						税区分 = DataBaseValue.ConvObjectToShort(row["nykd_tax"]),
						税込区分 = DataBaseValue.ConvObjectToShort(row["nykd_komi"]),
						備考 = row["nykd_biko"].ToString().Trim(),
						税率 = DataBaseValue.ConvObjectToInt(row["nykd_rate"]),
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
