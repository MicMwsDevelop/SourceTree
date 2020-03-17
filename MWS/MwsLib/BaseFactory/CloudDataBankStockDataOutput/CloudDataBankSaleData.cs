//
// CloudDataBankSaleData.cs
//
// クラウドデータバンクPCA売上情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/11/15 勝呂)
// 

namespace MwsLib.BaseFactory.CloudDataBankStockDataOutput
{
	/// <summary>
	/// クラウドデータバンクPCA売上情報
	/// </summary>
	public class CloudDataBankSaleData
	{
		/// <summary>
		/// 
		/// </summary>
		public string 仕入先コード { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string sykd_jbmn { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string sykd_jtan { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string 仕入商品コード { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string sykd_mkbn { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string 商品名 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int 数量 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string sykd_tani { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int 仕入価格 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int 売上日 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string 仕入フラグ { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public short 消費税率 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CloudDataBankSaleData()
		{
			仕入先コード = string.Empty;
			sykd_jbmn = string.Empty;
			sykd_jtan = string.Empty;
			仕入商品コード = string.Empty;
			sykd_mkbn = string.Empty;
			商品名 = string.Empty;
			数量 = 0;
			sykd_tani = string.Empty;
			仕入価格 = 0;
			売上日 = 0;
			仕入フラグ = string.Empty;
			消費税率 = 0;
		}

		/// <summary>
		/// クラウドデータバンク商品仕入データ作成
		/// ※PCA商魂・商管に[仕入明細データ]としてインポートCSVファイルの作成
		/// (PCA商魂・商管 > 随時 > 汎用データの受入 で[データの選択]欄で「仕入明細データ」を選択して読込
		/// </summary>
		/// <param name="no">伝票No</param>
		/// <returns>CSV文字列</returns>
		public string ToStock(int no)
		{
			string[] ret = new string[45];
			ret[0] = "0";
			ret[1] = 仕入フラグ;						// 仕入または仕入以外
			ret[2] = "0";
			ret[3] = 売上日.ToString();
			ret[4] = 売上日.ToString();
			ret[5] = no.ToString();						// 伝票Ｎｏ
			ret[6] = 仕入先コード;						// 仕入先コード
			ret[7] = "";								// 仕入先名は空白
			ret[8] = "";
			ret[9] = sykd_jbmn.Substring(sykd_jbmn.Length - 2, 2);	// 部門コード
			ret[10] = sykd_jtan.Substring(sykd_jtan.Length - 2, 2);	// 担当者コード
			ret[11] = "0";
			ret[12] = "";
			ret[13] = 仕入商品コード;
			ret[14] = "0";
			ret[15] = 商品名;
			ret[16] = "2";								// 区 '2'は 単価修正
			ret[17] = "0";
			ret[18] = "0";
			ret[19] = "0";
			ret[20] = 数量.ToString();					// 数量
			ret[21] = "";								// sykd_tani;
			ret[22] = 仕入価格.ToString();				// 単価
			ret[23] = (数量 * 仕入価格).ToString();		// 金額
			ret[24] = "0";
			ret[25] = "0";
			ret[26] = "2";
			ret[27] = "0";								// 税込区分
			ret[28] = "0";
			ret[29] = "";
			ret[30] = "";
			ret[31] = "";
			ret[32] = "0";
			ret[33] = "0";
			ret[34] = "0";
			ret[35] = "0";
			ret[36] = "0";
			ret[37] = "0";
			ret[38] = "0";
			ret[39] = 消費税率.ToString();				// 税率
			ret[40] = "0";
			ret[41] = "";
			ret[42] = "";
			ret[43] = "0";
			ret[44] = "";
			return string.Join(",", ret);
		}
	}
}
