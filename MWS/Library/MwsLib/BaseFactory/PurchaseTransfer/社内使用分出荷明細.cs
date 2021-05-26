//
// 社内使用分出荷明細.cs
// 
// 社内使用分出荷明細クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/05/26 勝呂)
//
using MwsLib.BaseFactory.Junp.View;

namespace MwsLib.BaseFactory.PurchaseTransfer
{
	public class 社内使用分出荷明細
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
		public int 税区分 { get; set; }
		public short 税込区分 { get; set; }
		public string 備考 { get; set; }
		public string 規格型番 { get; set; }
		public string 色 { get; set; }
		public string サイズ { get; set; }
		public decimal 税率 { get; set; }
		public string プロジェクトコード { get; set; }
		public string 商品名2 { get; set; }

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
		public 社内使用分出荷明細()
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
			商品名2 = string.Empty;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="pca">vMicPCA出荷データ</param>
		public 社内使用分出荷明細(vMicPCA出荷データ pca)
		{
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
			税率 = pca.urid_rate;
		}
	}
}
