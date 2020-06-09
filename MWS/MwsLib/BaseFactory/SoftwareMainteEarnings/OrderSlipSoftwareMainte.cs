//
// OrderSlipSoftwareMainte.cs
//
// ソフトウェア保守料受注伝票情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/03/16 勝呂)
// 
using MwsLib.Common;
using MwsLib.BaseFactory.Pca;

namespace MwsLib.BaseFactory.SoftwareMainteEarnings
{
	/// <summary>
	/// ソフトウェア保守料受注伝票情報
	/// </summary>
	public class OrderSlipSoftwareMainte
	{
		/// <summary>
		/// tMih受注ヘッダ f受注番号
		/// </summary>
		public int f受注番号 { get; set; }

		/// <summary>
		/// tMih受注ヘッダ f受注日
		/// </summary>
		public Date? f受注日 { get; set; }

		/// <summary>
		/// tMih受注ヘッダ f受注承認日
		/// </summary>
		public Date? f受注承認日 { get; set; }

		/// <summary>
		/// tMih受注ヘッダ f売上承認日
		/// </summary>
		public Date? f売上承認日 { get; set; }

		/// <summary>
		/// tMih受注ヘッダ f販売種別
		/// </summary>
		public int? f販売種別 { get; set; }

		/// <summary>
		/// tMih受注ヘッダ f販売先コード
		/// </summary>
		public int? f販売先コード { get; set; }

		/// <summary>
		/// tMih受注ヘッダ f販売先
		/// </summary>
		public string f販売先 { get; set; }

		/// <summary>
		/// tMih受注ヘッダ fユーザーコード
		/// </summary>
		public int? fユーザーコード { get; set; }

		/// <summary>
		/// tMih受注ヘッダ fユーザー
		/// </summary>
		public string fユーザー { get; set; }

		/// <summary>
		/// tMih受注ヘッダ fSV利用開始年月
		/// </summary>
		public YearMonth? fSV利用開始年月 { get; set; }

		/// <summary>
		/// tMih受注ヘッダ fSV利用終了年月
		/// </summary>
		public YearMonth? fSV利用終了年月 { get; set; }

		/// <summary>
		/// tMih受注ヘッダ fBshCode3
		/// </summary>
		public string fBshCode3 { get; set; }

		/// <summary>
		/// tMih受注ヘッダ f担当支店名
		/// </summary>
		public string f担当支店名 { get; set; }

		/// <summary>
		/// tMih受注ヘッダ f件名
		/// </summary>
		public string f件名 { get; set; }

		/// <summary>
		/// tMih受注詳細 f商品コード
		/// </summary>
		public string f商品コード { get; set; }

		/// <summary>
		/// tMih受注詳細 f商品名
		/// </summary>
		public string f商品名 { get; set; }

		/// <summary>
		/// tMih受注詳細 f数量
		/// </summary>
		public int? f数量 { get; set; }

		/// <summary>
		/// tMih受注詳細 f標準価格
		/// </summary>
		public int? f標準価格 { get; set; }

		/// <summary>
		/// tMih受注詳細 f金額
		/// </summary>
		public int? f金額 { get; set; }

		/// <summary>
		/// tMih受注詳細 f提供価格
		/// </summary>
		public int? f提供価格 { get; set; }

		/// <summary>
		/// tMih受注詳細 f税区分
		/// </summary>
		public string f税区分 { get; set; }

		/// <summary>
		/// tMih受注詳細 f税率
		/// </summary>
		public int? f税率 { get; set; }

		/// <summary>
		/// tMih受注詳細 f税込区分
		/// </summary>
		public string f税込区分 { get; set; }

		/// <summary>
		/// tMih受注詳細 f売上原価
		/// </summary>
		public int? f売上原価 { get; set; }

		/// <summary>
		/// tMih支店情報 fPca部門コード
		/// </summary>
		public short? fPca部門コード { get; set; }

		/// <summary>
		/// tMih支店情報 fPca担当者コード
		/// </summary>
		public string fPca担当者コード { get; set; }

		/// <summary>
		/// tMih支店情報 fPca倉庫コード
		/// </summary>
		public short? fPca倉庫コード { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OrderSlipSoftwareMainte()
		{
			f受注番号 = 0;
			f受注日 = null;
			f受注承認日 = null;
			f売上承認日 = null;
			f販売種別 = null;
			f販売先コード = null;
			f販売先 = string.Empty;
			fユーザーコード = null;
			fユーザー = string.Empty;
			fSV利用開始年月 = null;
			fSV利用終了年月 = null;
			fBshCode3 = string.Empty;
			f担当支店名 = string.Empty;
			f件名 = string.Empty;
			f商品コード = string.Empty;
			f商品名 = string.Empty;
			f数量 = null;
			f標準価格 = null;
			f金額 = null;
			f提供価格 = null;
			f税区分 = string.Empty;
			f税率 = null;
			f税込区分 = string.Empty;
			f売上原価 = null;
			fPca部門コード = null;
			fPca担当者コード = string.Empty;
			fPca倉庫コード = null;
		}

		/// <summary>
		/// ソフトウェア保守料売上データCSV文字列の取得
		/// </summary>
		/// <param name="no">伝票No</param>
		/// <param name="tokuisakiCode">販売先得意先コード</param>
		/// <param name="taxRate">伝票No</param>
		/// <param name="saleDate">売上日 </param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToSale(int no, int taxRate, string tokuisakiCode, Date saleDate, int pcaVer)
		{
			PCA売上明細 pca = new PCA売上明細();
			pca.売上日 = saleDate.ToIntYMD();// 2:売上年月日
			pca.請求日 = saleDate.ToIntYMD();// 3:請求年月日
			pca.伝票No = no;// 4:伝票番号
			pca.得意先コード = tokuisakiCode;// 5:得意先コード(13)
			pca.得意先名 = StringUtil.GetSubstringByByte(f販売先, 40);// 6:得意先名(40)
			pca.部門コード = fPca部門コード.Value.ToString();// 9:部門コード(6)
			pca.担当者コード = fPca担当者コード;// 10:担当者コード(13)
			pca.摘要名 = StringUtil.GetSubstringByByte(fユーザー, 30);//12:摘要名(30)
			pca.商品コード = f商品コード;// 15:商品コード(13)
			pca.商品名= StringUtil.GetSubstringByByte(f商品名, 36);// 17:品名(36)
			pca.倉庫コード = fPca倉庫コード.Value.ToString();// 19:倉庫コード(6)
			pca.数量 = 1;// 22:数量
			pca.売上金額 = f提供価格.Value;// 25:売上金額
			pca.原単価 = f売上原価.Value;// 26:原単価
			pca.原価金額 = f売上原価.Value;// 27:原価額
			pca.税区分 = f税区分.ToInt();// 31:税区分
			pca.税込区分 = f税込区分.ToInt();// 32:税込区分
			pca.標準価格 = f標準価格.Value;// 34:標準価格
			pca.税率 = taxRate;// 48:税率
			return pca.ToCsvString(pcaVer);
/*
			string[] ret = new string[53];
			ret[0] = "0";											// 1:伝区
			ret[1] = saleDate.ToIntYMD().ToString();				// 2:売上年月日
			ret[2] = saleDate.ToIntYMD().ToString();				// 3:請求年月日
			ret[3] = no.ToString();									// 4:伝票番号
			ret[4] = tokuisakiCode;									// 5:得意先コード(13)
			ret[5] = StringUtil.GetSubstringByByte(f販売先, 40);	// 6:得意先名(40)
			ret[6] = "";											// 7:直送先コード(13)
			ret[7] = "";											// 8:先方担当者名(30)
			ret[8] = fPca部門コード.Value.ToString();				// 9:部門コード(6)
			ret[9] = fPca担当者コード;								// 10:担当者コード(13)
			ret[10] = "0";											// 11:摘要コード(6)
			ret[11] = StringUtil.GetSubstringByByte(fユーザー, 30);	// 12:摘要名(30)
			ret[12] = "";											// 13:分類コード(4)
			ret[13] = "";											// 14:伝票区分コード(2)
			ret[14] = f商品コード;									// 15:商品コード(13)
			ret[15] = "0";											// 16:マスタ区分  (0:一般商品)
			ret[16] = StringUtil.GetSubstringByByte(f商品名, 36);	// 17:品名(36)
			ret[17] = "0";											// 18:区
			ret[18] = fPca倉庫コード.Value.ToString();				// 19:倉庫コード(6)
			ret[19] = "0";											// 20:入数
			ret[20] = "0";											// 21:箱数
			ret[21] = "1";											// 22:数量
			ret[22] = "";											// 23:単位(4)
			ret[23] = "0";											// 24:単価
			ret[24] = f提供価格.Value.ToString();					// 25:売上金額
			ret[25] = f売上原価.Value.ToString();					// 26:原単価
			ret[26] = f売上原価.Value.ToString();					// 27:原価額
			ret[27] = "0";											// 28:粗利益
			ret[28] = "0";											// 29:外税額
			ret[29] = "0";											// 30:内税額
			ret[30] = f税区分;										// 31:税区分
			ret[31] = f税込区分;									// 32:税込区分
			ret[32] = "";											// 33:備考(20)
			ret[33] = f標準価格.Value.ToString();					// 34:標準価格
			ret[34] = "0";											// 35:同時入荷区分
			ret[35] = "0";											// 36:売単価コード
			ret[36] = "0";											// 37:売価金額コード
			ret[37] = "";											// 38:規格・型番(36)
			ret[38] = "";											// 39:色(7)
			ret[39] = "";											// 40:サイズ(5)
			ret[40] = "0";											// 41:計算式コード
			ret[41] = "0";											// 42:商品項目1
			ret[42] = "0";											// 43:商品項目2
			ret[43] = "0";											// 44:商品項目3
			ret[44] = "0";											// 45:売上項目1
			ret[45] = "0";											// 46:売上項目2
			ret[46] = "0";											// 47:売上項目3
			ret[47] = taxRate.ToString();							// 48:税率
			ret[48] = "0";											// 49:伝票消費税（外税）
			ret[49] = "";											// 50:プロジェクトコード(16)
			ret[50] = "";											// 51:伝票No２(16)
			ret[51] = "0";											// 52:データ区分
			ret[52] = "";											// 53:商品名２(256)
			string csv = string.Join(",", ret);
			if (8 == pcaVer)
			{
				// 汎用データレイアウト指定 7: Rev4.20、8: Rev4.50
				csv += ",0";										// 54:単位区分
				csv += ",";											// 55:ロットNo
			}
			return csv;
*/
		}
	}
}
