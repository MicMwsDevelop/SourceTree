//
// PCA出荷明細貯蔵品.cs
// 
// PCA出荷明細貯蔵品クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/05/28 勝呂)
//
using MwsLib.BaseFactory.Junp.View;

namespace MwsLib.BaseFactory.PurchaseTransfer
{
	public class PCA出荷明細貯蔵品
	{
		public short 出荷方法 { get; set; }
		public int 出荷日 { get; set; }
		public int 伝票No { get; set; }
		public string 出荷先コード { get; set; }
		public string 出荷先名 { get; set; }
		public string 先方担当者名 { get; set; }
		public string 部門コード { get; set; }
		public string 担当者コード { get; set; }
		public string 商品コード { get; set; }
		public string 品名 { get; set; }
		public string 倉庫コード { get; set; }
		public decimal 入数 { get; set; }
		public decimal 箱数 { get; set; }
		public decimal 数量 { get; set; }
		public string 単位 { get; set; }
		public decimal 単価 { get; set; }
		public decimal 出荷金額 { get; set; }
		public short 税区分 { get; set; }
		public short 税込区分 { get; set; }
		public string 備考 { get; set; }
		public string 規格型番 { get; set; }
		public string 色 { get; set; }
		public string サイズ { get; set; }
		public decimal 税率 { get; set; }
		public string プロジェクトコード { get; set; }

		public string 部署名
		{
			get
			{
				return 出荷先名.Replace("ミック", "");
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PCA出荷明細貯蔵品()
		{
			this.Clear();
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="pca"></param>
		public PCA出荷明細貯蔵品(vMicPCA出荷データ pca)
		{
			this.Clear();
			出荷方法 = pca.urid_dkbn;
			出荷日 = pca.urid_uribi;
			伝票No = pca.urid_denno;
			出荷先コード = pca.urid_tcd;
			出荷先名 = pca.urid_mei1;
			先方担当者名 = pca.URID_tanmei;
			部門コード = pca.urid_jbmn;
			担当者コード = pca.urid_jtan;
			商品コード = pca.urid_scd;
			品名 = pca.urid_mei;
			倉庫コード = pca.urid_souko;
			入数 = pca.urid_iri;
			箱数 = pca.urid_hako;
			数量 = pca.urid_suryo;
			単位 = pca.urid_tani;
			単価 = pca.urid_tanka;
			出荷金額 = pca.urid_kingaku;
			税区分 = pca.urid_tax;
			税込区分 = pca.urid_komi;
			備考 = pca.urid_biko;
		}

		/// <summary>
		/// クリア
		/// </summary>
		protected void Clear()
		{
			出荷方法 = 0;
			出荷日 = 0;
			伝票No = 0;
			出荷先コード = string.Empty;
			出荷先名 = string.Empty;
			先方担当者名 = string.Empty;
			部門コード = string.Empty;
			担当者コード = string.Empty;
			商品コード = string.Empty;
			品名 = string.Empty;
			倉庫コード = string.Empty;
			入数 = 0;
			箱数 = 0;
			数量 = 0;
			単位 = string.Empty;
			単価 = 0;
			出荷金額 = 0;
			税区分 = 0;
			税込区分 = 0;
			備考 = string.Empty;
			規格型番 = string.Empty;
			色 = string.Empty;
			サイズ = string.Empty;
			税率 = 0;
			プロジェクトコード = string.Empty;
		}
/*
		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<PCA出荷明細貯蔵品> DataTableToList(DataTable table)
		{
			List<PCA出荷明細貯蔵品> result = new List<PCA出荷明細貯蔵品>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					PCA出荷明細貯蔵品 data = new PCA出荷明細貯蔵品
					{
						出荷方法 = DataBaseValue.ConvObjectToShort(row["urid_dkbn"]),
						出荷日 = DataBaseValue.ConvObjectToInt(row["urid_uribi"]),
						伝票No = DataBaseValue.ConvObjectToInt(row["urid_denno"]),
						出荷先コード = row["urid_tcd"].ToString().Trim(),
						出荷先名 = row["urid_mei1"].ToString().Trim(),
						先方担当者名 = row["URID_tanmei"].ToString().Trim(),
						部門コード = row["urid_jbmn"].ToString().Trim(),
						担当者コード = row["urid_jtan"].ToString().Trim(),
						商品コード = row["urid_scd"].ToString().Trim(),
						品名 = row["urid_mei"].ToString().Trim(),
						倉庫コード = row["urid_souko"].ToString().Trim(),
						入数 = DataBaseValue.ConvObjectToDecimal(row["urid_iri"]),
						箱数 = DataBaseValue.ConvObjectToDecimal(row["urid_hako"]),
						数量 = DataBaseValue.ConvObjectToDecimal(row["urid_suryo"]),
						単位 = row["urid_tani"].ToString().Trim(),
						単価 = DataBaseValue.ConvObjectToDecimal(row["urid_tanka"]),
						出荷金額 = DataBaseValue.ConvObjectToDecimal(row["urid_kingaku"]),
						税区分 = DataBaseValue.ConvObjectToShort(row["urid_tax"]),
						税込区分 = DataBaseValue.ConvObjectToShort(row["urid_komi"]),
						備考 = row["urid_biko"].ToString().Trim(),
					};
					result.Add(data);
				}
			}
			return result;
		}
*/
	}
}
