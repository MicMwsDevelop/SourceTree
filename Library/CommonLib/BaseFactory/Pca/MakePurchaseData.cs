//
// MakePurchaseData.cs
//
// 仕入データ作成用クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2019/11/15 勝呂)
// Ver1.01 仕入データの17:区(0:通常仕入, 1:返品, 2:単価訂正)を2から1に変更(2021/01/08 勝呂)
// 
using CommonLib.Common;

namespace CommonLib.BaseFactory.Pca
{
	/// <summary>
	/// 仕入データ作成用クラス
	/// </summary>
	public class MakePurchaseData
	{
		/// <summary>
		/// 
		/// </summary>
		public string f仕入先コード { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string f仕入先名 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string f部門コード { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string f担当者コード { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string f仕入商品コード { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string f商品名 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int f数量 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string f単位 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int f仕入価格 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int f売上日 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public short f仕入フラグ { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public short f消費税率 { get; set; }

		/// <summary>
		/// 部門コードの取得
		/// </summary>
		public string 部門コード
		{
			get
			{
				return f部門コード.Substring(f部門コード.Length - 2, 2);
			}
		}

		/// <summary>
		/// 担当者コードの取得
		/// </summary>
		public string 担当者コード
		{
			get
			{
				return f担当者コード.Substring(f担当者コード.Length - 2, 2);
			}
		}

		/// <summary>
		/// 商品名の取得
		/// </summary>
		public string 商品名
		{
			get
			{
				return StringUtil.GetSubstringByByte(f商品名, 36);
			}
		}

		/// <summary>
		/// 金額の取得
		/// </summary>
		public int 金額
		{
			get
			{
				return f数量 * f仕入価格;
			}
		}

		/// <summary>
		/// 仕入先名の取得
		/// </summary>
		public string 仕入先名
		{
			get
			{
				return StringUtil.GetSubstringByByte(f仕入先名, 40);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MakePurchaseData()
		{
			f仕入先コード = string.Empty;
			f仕入先名 = string.Empty;
			f部門コード = string.Empty;
			f担当者コード = string.Empty;
			f仕入商品コード = string.Empty;
			f商品名 = string.Empty;
			f数量 = 0;
			f単位 = string.Empty;
			f仕入価格 = 0;
			f売上日 = 0;
			f仕入フラグ = 0;
			f消費税率 = 0;
		}

		/// <summary>
		/// 仕入データ作成
		/// ※PCA商魂・商管に[仕入明細データ]としてインポートCSVファイルの作成
		/// (PCA商魂・商管 > 随時 > 汎用データの受入 で[データの選択]欄で「仕入明細データ」を選択して読込
		/// </summary>
		/// <param name="no">伝票No</param>
		/// <returns>CSV文字列</returns>
		public string ToPurchase(int no, int pcaVer)
		{
			汎用データレイアウト仕入明細データ pca = new 汎用データレイアウト仕入明細データ();
			pca.科目区分 = f仕入フラグ;				// 2:科目区分
			pca.仕入日 = f売上日;					// 4:仕入年月日
			pca.精算日 = f売上日;					// 5:精算年月日
			pca.伝票No = no;						// 6:伝票番号
			pca.仕入先コード = f仕入先コード;		// 7:仕入先コード
			if (0 < f仕入先名.Length)
			{
				pca.仕入先名 = 仕入先名;			// 8:仕入先名
			}
			pca.部門コード = 部門コード;			// 10:部門コード
			pca.担当者コード = 担当者コード;		// 11:担当者コード
			pca.摘要コード = "0";					// 12:摘要コード
			pca.商品コード = f仕入商品コード;		// 14:商品コード
			pca.商品名 = 商品名;					// 16:品名(36)
			pca.区 = 0;								// 17:区(0:通常仕入, 1:返品, 2:単価訂正)
			pca.倉庫コード = "0";					// 18:倉庫コード
			pca.数量 = f数量;						// 21:数量
			pca.単価= f仕入価格;					// 23:単価
			pca.金額 = 金額;						// 24:金額
			pca.税区分 = 2;							// 27:税区分
			pca.備考 = "";							// 29:備考
			pca.税率 = f消費税率;					// 40:税率
			return pca.ToCsvString(pcaVer);
/*
			string[] ret = new string[45];
			ret[0] = "0";								// 1:入荷方法
			ret[1] = 仕入フラグ;						// 2:科目区分
			ret[2] = "0";								// 3:伝区(0:掛買)
			ret[3] = 売上日.ToString();					// 4:仕入年月日
			ret[4] = 売上日.ToString();					// 5:精算年月日
			ret[5] = no.ToString();						// 6:伝票番号
			ret[6] = 仕入先コード;						// 7:仕入先コード
			ret[7] = "";								// 8:仕入先名(40)
			ret[8] = "";								// 9:先方担当者名(30)
			ret[9] = 部門コード.Substring(部門コード.Length - 2, 2);	// 10:部門コード
			ret[10] = 担当者コード.Substring(担当者コード.Length - 2, 2);	// 11:担当者コード
			ret[11] = "0";								// 12:摘要コード
			ret[12] = "";								// 13:摘要名(30)
			ret[13] = 仕入商品コード;					// 14:商品コード
			ret[14] = "0";								// 15:マスタ区分  (0:一般商品)
			ret[15] = StringUtil.GetSubstringByByte(商品名, 36);	// 16:品名(36)
			ret[16] = "0";                              // 17:区(0:通常仕入, 1:返品, 2:単価訂正)
			ret[17] = "0";								// 18:倉庫コード
			ret[18] = "0";								// 19:入数
			ret[19] = "0";								// 20:箱数
			ret[20] = 数量.ToString();					// 21:数量
			ret[21] = "";								// 22:単位(4)
			ret[22] = 仕入価格.ToString();				// 23:単価
			ret[23] = (数量 * 仕入価格).ToString();		// 24:金額
			ret[24] = "0";								// 25:外税額
			ret[25] = "0";								// 26:内税額
			ret[26] = "2";								// 27:税区分
			ret[27] = "0";								// 28:税込区分
			ret[28] = "";								// 29:備考(20)
			ret[29] = "";								// 30:規格・型番(36)
			ret[30] = "";								// 31:色(7)
			ret[31] = "";								// 32:サイズ(5)
			ret[32] = "0";								// 33:計算式コード
			ret[33] = "0";								// 34:商品項目1
			ret[34] = "0";								// 35:商品項目2
			ret[35] = "0";								// 36:商品項目3
			ret[36] = "0";								// 37:仕入項目1
			ret[37] = "0";								// 38:仕入項目2
			ret[38] = "0";								// 39:仕入項目3
			ret[39] = 消費税率.ToString();				// 40:税率
			ret[40] = "0";								// 41:伝票消費税（外税）
			ret[41] = "";								// 42:プロジェクトコード
			ret[42] = "";								// 43:伝票No２
			ret[43] = "0";								// 44:データ区分
			ret[44] = "";								// 45:商品名２(256)
			string csv = string.Join(",", ret);
			if (8 == pcaVer)
			{
				// 汎用データレイアウト指定 7: Rev4.20、8: Rev4.50
				csv += ",0";							// 46:単位区分
				csv += ",";								// 47:ロットNo
				csv += ",0";							// 48:ロット有効期限
			}
			return csv;
*/
		}
	}
}
