//
// PcaEarningsDetail.cs
//
// PCA汎用データ 売上明細データ
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/11/15 勝呂)
// 
using System;
using System.Collections.Generic;

namespace MwsLib.BaseFactory.Pca
{
	/// <summary>
	/// PCA汎用データ 売上明細データ
	/// </summary>
	[Serializable]
	public class PCA売上明細汎用データ
	{
		/// <summary>
		/// 記事コード
		/// </summary>
		public static readonly string ArticleCode = "000014";

		/// <summary>
		/// 0:掛売、1:現収、2:カード、3:そ の他、5:仮伝、6:契約
		/// </summary>
		public int 伝区 { get; set; }
		public int 売上日 { get; set; }
		public int 請求日 { get; set; }
		public int 伝票No { get; set; }
		public string 得意先コード { get; set; }
		public string 得意先名 { get; set; }
		public string 直送先コード { get; set; }
		public string 先方担当者名 { get; set; }
		public string 部門コード { get; set; }
		public string 担当者コード { get; set; }
		public string 摘要コード { get; set; }
		public string 摘要名 { get; set; }
		public string 分類コード { get; set; }
		public string 伝票区分 { get; set; }
		public string 商品コード { get; set; }
		/// <summary>
		/// 0:一般商品、1:雑商品、2:諸雑 費、3:値引、4:記事
		/// </summary>
		public int マスター区分 { get; set; }
		public string 商品名 { get; set; }
		/// <summary>
		/// 0:売上、1:返品、2:単価訂正、9: 一般商品以外を示す
		/// </summary>
		public int 区 { get; set; }
		public string 倉庫コード { get; set; }
		public int 入数 { get; set; }
		public int 箱数 { get; set; }
		public int 数量 { get; set; }
		public string 単位 { get; set; }
		public int 単価 { get; set; }
		public int 売上金額 { get; set; }
		public int 原単価 { get; set; }
		public int 原価金額 { get; set; }
		public int 粗利益 { get; set; }
		public int 外税額 { get; set; }
		public int 内税額 { get; set; }
		public int 税区分 { get; set; }
		/// <summary>
		/// 0:税抜き、1:税込み
		/// </summary>
		public int 税込区分 { get; set; }
		public string 備考 { get; set; }
		public int 標準価格 { get; set; }
		/// <summary>
		/// 0:自動入荷しない、1:自動入荷する
		/// </summary>
		public int 同時入荷区分 { get; set; }
		public int 売単価 { get; set; }
		public int 売価金額 { get; set; }
		public string 規格型番 { get; set; }
		public string 色 { get; set; }
		public string サイズ { get; set; }
		public int 計算式コード { get; set; }
		public int 商品項目１ { get; set; }
		public int 商品項目２ { get; set; }
		public int 商品項目３ { get; set; }
		public int 売上項目１ { get; set; }
		public int 売上項目２ { get; set; }
		public int 売上項目３ { get; set; }
		public int 税率 { get; set; }
		public int 伝票消費税額 { get; set; }
		public string ﾌﾟﾛｼﾞｪｸﾄコード { get; set; }
		public string 伝票No2 { get; set; }
		public int データ区分 { get; set; }
		public string 商品名２ { get; set; }
		public int 単位区分 { get; set; }
		public string ロットNo { get; set; }
		public int 売上税種別 { get; set; }
		public int 原価税込区分 { get; set; }
		public int 原価税率 { get; set; }
		public int 原価税種別 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PCA売上明細汎用データ()
		{
			伝区 = 0;
			売上日 = 0;
			請求日 = 0;
			伝票No = 0;
			得意先コード = string.Empty;
			得意先名 = string.Empty;
			直送先コード = string.Empty;
			先方担当者名 = string.Empty;
			部門コード = string.Empty;
			担当者コード = string.Empty;
			摘要コード = string.Empty;
			摘要名 = string.Empty;
			分類コード = string.Empty;
			伝票区分 = string.Empty;
			商品コード = string.Empty;
			マスター区分 = 0;
			商品名 = string.Empty;
			区 = 0;
			倉庫コード = string.Empty;
			入数 = 0;
			箱数 = 0;
			数量 = 0;
			単位 = string.Empty;
			単価 = 0;
			売上金額 = 0;
			原単価 = 0;
			原価金額 = 0;
			粗利益 = 0;
			外税額 = 0;
			内税額 = 0;
			税区分 = 0;
			税込区分 = 0;
			備考 = string.Empty;
			標準価格 = 0;
			同時入荷区分 = 0;
			売単価 = 0;
			売価金額 = 0;
			規格型番 = string.Empty;
			色 = string.Empty;
			サイズ = string.Empty;
			計算式コード = 0;
			商品項目１ = 0;
			商品項目２ = 0;
			商品項目３ = 0;
			売上項目１ = 0;
			売上項目２ = 0;
			売上項目３ = 0;
			税率 = 0;
			伝票消費税額 = 0;
			ﾌﾟﾛｼﾞｪｸﾄコード = string.Empty;
			伝票No2 = string.Empty;
			データ区分 = 0;
			商品名２ = string.Empty;
			単位区分 = 0;
			ロットNo = string.Empty;
			売上税種別 = 0;
			原価税込区分 = 0;
			原価税率 = 0;
			原価税種別 = 0;
		}

		/// <summary>
		/// CSV文字列の取得
		/// </summary>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>CSV文字列</returns>
		public string ToCsvString(int pcaVer)
		{
			List<string> list = new List<string>();
			list.Add(伝区.ToString());
			list.Add("\"" + 売上日.ToString() + "\"");
			list.Add("\"" + 請求日.ToString() + "\"");
			list.Add(伝票No.ToString());
			list.Add("\"" + 得意先コード + "\"");
			list.Add("\"" + 得意先名 + "\"");
			list.Add("\"" + 直送先コード + "\"");
			list.Add("\"" + 先方担当者名 + "\"");
			list.Add("\"" + 部門コード + "\"");
			list.Add("\"" + 担当者コード + "\"");
			list.Add(摘要コード);
			list.Add("\"" + 摘要名 + "\"");
			list.Add("\"" + 分類コード + "\"");
			list.Add("\"" + 伝票区分 + "\"");
			list.Add("\"" + 商品コード + "\"");
			list.Add(マスター区分.ToString());
			list.Add("\"" + 商品名 + "\"");
			list.Add(区.ToString());
			list.Add("\"" + 倉庫コード + "\"");
			list.Add(入数.ToString());
			list.Add(箱数.ToString());
			list.Add(数量.ToString());
			list.Add("\"" + 単位 + "\"");
			list.Add(単価.ToString());
			list.Add(売上金額.ToString());
			list.Add(原単価.ToString());
			list.Add(原価金額.ToString());
			list.Add(粗利益.ToString());
			list.Add(外税額.ToString());
			list.Add(内税額.ToString());
			list.Add(税区分.ToString());
			list.Add(税込区分.ToString());
			list.Add("\"" + 備考 + "\"");
			list.Add(標準価格.ToString());
			list.Add(同時入荷区分.ToString());
			list.Add(売単価.ToString());
			list.Add(売価金額.ToString());
			list.Add("\"" + 規格型番 + "\"");
			list.Add("\"" + 色 + "\"");
			list.Add("\"" + サイズ + "\"");
			list.Add(計算式コード.ToString());
			list.Add(商品項目１.ToString());
			list.Add(商品項目２.ToString());
			list.Add(商品項目３.ToString());
			list.Add(売上項目１.ToString());
			list.Add(売上項目２.ToString());
			list.Add(売上項目３.ToString());
			list.Add(税率.ToString());
			list.Add(伝票消費税額.ToString());
			list.Add("\"" + ﾌﾟﾛｼﾞｪｸﾄコード + "\"");
			list.Add("\"" + 伝票No2 + "\"");
			list.Add(データ区分.ToString());
			list.Add("\"" + 商品名２ + "\"");
			if (8 <= pcaVer)
			{
				list.Add(単位区分.ToString());
				list.Add(@""" + ロットNo + """);
				list.Add(売上税種別.ToString());
				list.Add(原価税込区分.ToString());
				list.Add(原価税率.ToString());
				list.Add(原価税種別.ToString());
			}
			return String.Join(",", list.ToArray());
		}
	}
}
