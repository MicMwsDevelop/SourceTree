using MwsLib.BaseFactory.Junp.View;
using System;
using System.Collections.Generic;

namespace MwsLib.BaseFactory.Pca
{
	/// <summary>
	/// PCA汎用データ 受注明細データ
	/// </summary>
	[Serializable]
	public class PCA受注明細汎用データ
	{
		/// <summary>
		/// Web受注分 摘要名
		/// </summary>
		public static readonly string WebJucuTekimei = "Web受注分";

		/// <summary>
		/// 送料 PCA商品コード
		/// </summary>
		public static readonly string ShippingGoodsCode = "000600";

		/// <summary>
		/// 着日指定 PCA商品コード
		/// </summary>
		public static readonly string ArrivalDateGoodsCode = "000020";

		public int 受注No { get; set; }
		public string 受注日 { get; set; }
		public string 納期 { get; set; }
		public string 得意先No { get; set; }
		public string 顧客名 { get; set; }
		public string 直送先コード { get; set; }
		public string 先方担当者 { get; set; }
		public string PCA部門No { get; set; }
		public string PCA主担当No { get; set; }
		public string 摘要コード { get; set; }
		public string 摘要名 { get; set; }
		public string goods_code { get; set; }
		public string マスター区分 { get; set; }
		public string 商品名 { get; set; }
		public string 倉庫コード { get; set; }
		public string 入数 { get; set; }
		public string 箱数 { get; set; }
		public string 数量 { get; set; }
		public string 単位 { get; set; }
		public string 単価 { get; set; }
		public string 受注金額 { get; set; }
		public string 原単価 { get; set; }
		public string 原価額 { get; set; }
		public string 粗利益 { get; set; }
		public string 外税額 { get; set; }
		public string 内税額 { get; set; }
		public string 税区分 { get; set; }
		public string 税込区分 { get; set; }
		public string 備考 { get; set; }
		public string 標準価格 { get; set; }
		public string 自動発注区分 { get; set; }
		public string 売単価 { get; set; }
		public string 売価金額 { get; set; }
		public string 規格型番 { get; set; }
		public string 色 { get; set; }
		public string サイズ { get; set; }
		public string 計算式コード { get; set; }
		public string 商品項目1 { get; set; }
		public string 商品項目2 { get; set; }
		public string 商品項目3 { get; set; }
		public string 売上項目1 { get; set; }
		public string 売上項目2 { get; set; }
		public string 売上項目3 { get; set; }
		public int 税率 { get; set; }
		public int 伝票消費税 { get; set; }
		public string プロジェクトコード { get; set; }
		public string 受注No2 { get; set; }
		public string 明細納期 { get; set; }
		public int データ区分 { get; set; }
		public string 商品名2 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PCA受注明細汎用データ()
		{
			受注No = 0;
			受注日 = string.Empty;
			納期 = string.Empty;
			得意先No = string.Empty;
			顧客名 = string.Empty;
			直送先コード = string.Empty;
			先方担当者 = string.Empty;
			PCA部門No = string.Empty;
			PCA主担当No = string.Empty;
			摘要コード = string.Empty;
			摘要名 = string.Empty;
			goods_code = string.Empty;
			マスター区分 = string.Empty;
			商品名 = string.Empty;
			倉庫コード = string.Empty;
			入数 = string.Empty;
			箱数 = string.Empty;
			数量 = string.Empty;
			単位 = string.Empty;
			単価 = string.Empty;
			受注金額 = string.Empty;
			原単価 = string.Empty;
			原価額 = string.Empty;
			粗利益 = string.Empty;
			外税額 = string.Empty;
			内税額 = string.Empty;
			税区分 = string.Empty;
			税込区分 = string.Empty;
			備考 = string.Empty;
			標準価格 = string.Empty;
			自動発注区分 = string.Empty;
			売単価 = string.Empty;
			売価金額 = string.Empty;
			規格型番 = string.Empty;
			色 = string.Empty;
			サイズ = string.Empty;
			計算式コード = string.Empty;
			商品項目1 = string.Empty;
			商品項目2 = string.Empty;
			商品項目3 = string.Empty;
			売上項目1 = string.Empty;
			売上項目2 = string.Empty;
			売上項目3 = string.Empty;
			税率 = 0;
			伝票消費税 = 0;
			プロジェクトコード = string.Empty;
			受注No2 = string.Empty;
			明細納期 = string.Empty;
			データ区分 = 0;
			商品名2 = string.Empty;
		}

		/// <summary>
		/// 初期値設定
		/// </summary>
		public void Initial()
		{
			納期 = "0";
			倉庫コード = "0";
			入数 = "0";
			箱数 = "0";
			数量 = "0";
			原単価 = "0";
			原価額 = "0";
			粗利益 = "0";
			外税額 = "0";
			内税額 = "0";
			税込区分 = "0";
			標準価格 = "0";
			自動発注区分 = "0";
			売単価 = "0";
			売価金額 = "0";
			計算式コード = "0";
			商品項目1 = "0";
			商品項目2 = "0";
			商品項目3 = "0";
			売上項目1 = "0";
			売上項目2 = "0";
			売上項目3 = "0";
		}

		/// <summary>
		/// 送料設定済みデータの取得
		/// </summary>
		/// <returns></returns>
		public static PCA受注明細汎用データ ShippingData(PCA受注明細汎用データ jucu, vMicPCA商品マスタ shipping)
		{
			PCA受注明細汎用データ ret = new PCA受注明細汎用データ();
			ret.Initial();

			ret.受注No = jucu.受注No;
			ret.受注日 = jucu.受注日;
			ret.得意先No = jucu.得意先No;
			ret.顧客名 = jucu.顧客名;
			ret.PCA部門No = jucu.PCA部門No;
			ret.PCA主担当No = jucu.PCA主担当No;
			ret.税率 = jucu.税率;
			ret.納期 = "0";
			ret.摘要コード = "031";
			ret.摘要名 = WebJucuTekimei;
			ret.goods_code = shipping.sms_scd;
			ret.マスター区分 = "1";
			ret.商品名 = shipping.sms_mei; // 送料(980)
			ret.単価 = "0";
			ret.単位 = "0";
			ret.受注金額 = shipping.sms_hyo.ToString();
			ret.税区分 = "2";
			return ret;
		}

		/// <summary>
		/// 着日指定設定済みデータの取得
		/// </summary>
		/// <returns></returns>
		public static PCA受注明細汎用データ ArrivalDateData(PCA受注明細汎用データ jucu, DateTime pref_arrival_date)
		{
			PCA受注明細汎用データ ret = new PCA受注明細汎用データ();
			ret.Initial();

			ret.受注No = jucu.受注No;
			ret.受注日 = jucu.受注日;
			ret.得意先No = jucu.得意先No;
			ret.顧客名 = jucu.顧客名;
			ret.PCA部門No = jucu.PCA部門No;
			ret.PCA主担当No = jucu.PCA主担当No;
			ret.税率 = jucu.税率;
			ret.摘要コード = "031";       // 031ではないのか？
			ret.摘要名 = WebJucuTekimei;
			ret.goods_code = PCA受注明細汎用データ.ArrivalDateGoodsCode;
			ret.マスター区分 = "4";
			ret.商品名 = PCA受注明細汎用データ.ArrivalDateGoodsName(pref_arrival_date);
			ret.単価 = "0";
			ret.単位 = "0";
			ret.税区分 = "2";
			return ret;
		}

		/// <summary>
		/// CSV文字列の取得
		/// </summary>
		/// <returns>CSV文字列</returns>
		public string ToCsvString()
		{
			List<string> list = new List<string>();
			list.Add(受注No.ToString());
			list.Add("\"" + 受注日 + "\"");
			list.Add("\"" + 納期 + "\"");
			list.Add("\"" + 得意先No + "\"");
			list.Add("\"\"");
			list.Add("\"" + 直送先コード + "\"");
			list.Add("\"" + 先方担当者 + "\"");
			list.Add("\"" + PCA部門No + "\"");
			list.Add("\"" + PCA主担当No + "\"");
			list.Add("\"" + 摘要コード + "\"");
			list.Add("\"" + 摘要名 + "\"");
			list.Add("\"" + goods_code + "\"");
			list.Add("\"" + マスター区分 + "\"");
			list.Add("\"" + 商品名 + "\"");
			list.Add("\"" + 倉庫コード + "\"");
			list.Add("\"" + 入数 + "\"");
			list.Add("\"" + 箱数 + "\"");
			list.Add("\"" + 数量 + "\"");
			list.Add("\"" + 単位 + "\"");
			list.Add("\"" + 単価 + "\"");
			list.Add("\"" + 受注金額 + "\"");
			list.Add("\"" + 原単価 + "\"");
			list.Add("\"" + 原価額 + "\"");
			list.Add("\"" + 粗利益 + "\"");
			list.Add("\"" + 外税額 + "\"");
			list.Add("\"" + 内税額 + "\"");
			list.Add("\"" + 税区分 + "\"");
			list.Add("\"" + 税込区分 + "\"");
			list.Add("\"" + 備考 + "\"");
			list.Add("\"" + 標準価格 + "\"");
			list.Add("\"" + 自動発注区分 + "\"");
			list.Add("\"" + 売単価 + "\"");
			list.Add("\"" + 売価金額 + "\"");
			list.Add("\"" + 規格型番 + "\"");
			list.Add("\"" + 色 + "\"");
			list.Add("\"" + サイズ + "\"");
			list.Add("\"" + 計算式コード + "\"");
			list.Add("\"" + 商品項目1 + "\"");
			list.Add("\"" + 商品項目2 + "\"");
			list.Add("\"" + 商品項目3 + "\"");
			list.Add("\"" + 売上項目1 + "\"");
			list.Add("\"" + 売上項目2 + "\"");
			list.Add("\"" + 売上項目3 + "\"");
			list.Add("\"" + 税率.ToString() + "\"");
			list.Add("\"" + 伝票消費税.ToString() + "\"");
			list.Add("\"" + プロジェクトコード + "\"");
			list.Add("\"" + 受注No2 + "\"");
			list.Add("\"" + 明細納期 + "\"");
			list.Add("\"" + データ区分.ToString() + "\"");
			list.Add("\"" + 商品名2 + "\"");
			return string.Join(",", list.ToArray());
		}

		/// <summary>
		/// 着日指定商品名文字列の取得
		/// </summary>
		/// <returns></returns>
		public static string ArrivalDateGoodsName(DateTime pref_arrival_date)
		{
			return string.Format("着日指定：　{0}月{1}日", pref_arrival_date.Month, pref_arrival_date.Day);
		}
	}
}
