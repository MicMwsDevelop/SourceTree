using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MwsLib.BaseFactory.Estore
{
	/// <summary>
	/// 汎用データクラス
	/// </summary>
	public class WebGeneralData
	{
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

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public WebGeneralData()
		{
			this.Clear();
		}

		/// <summary>
		/// クリア
		/// </summary>
		public virtual void Clear()
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
		}

		/// <summary>
		/// Create Table 定義文字列の取得
		/// </summary>
		/// <returns>定義文字列</returns>
		public static string CreateTableWebGeneralData()
		{
			return "受注No int,"
					+ "受注日 nvarchar(255),"
					+ "納期 nvarchar(255),"
					+ "得意先No nvarchar(255),"
					+ "顧客名 nvarchar(255),"
					+ "直送先コード nvarchar(255),"
					+ "先方担当者 nvarchar(255),"
					+ "PCA部門No nvarchar(255),"
					+ "PCA主担当No nvarchar(255),"
					+ "摘要コード nvarchar(255),"
					+ "摘要名 nvarchar(255),"
					+ "goods_code nvarchar(255),"
					+ "マスター区分 nvarchar(255),"
					+ "商品名 nvarchar(255),"
					+ "倉庫コード nvarchar(255),"
					+ "入数 nvarchar(255),"
					+ "箱数 nvarchar(255),"
					+ "数量 nvarchar(255),"
					+ "単位 nvarchar(255),"
					+ "単価 nvarchar(255),"
					+ "受注金額 nvarchar(255),"
					+ "原単価 nvarchar(255),"
					+ "原価額 nvarchar(255),"
					+ "粗利益 nvarchar(255),"
					+ "外税額 nvarchar(255),"
					+ "内税額 nvarchar(255),"
					+ "税区分 nvarchar(255),"
					+ "税込区分 nvarchar(255),"
					+ "備考 nvarchar(255),"
					+ "標準価格 nvarchar(255),"
					+ "自動発注区分 nvarchar(255),"
					+ "売単価 nvarchar(255),"
					+ "売価金額 nvarchar(255),"
					+ "規格・型番 nvarchar(255),"
					+ "色 nvarchar(255),"
					+ "サイズ nvarchar(255),"
					+ "計算式コード nvarchar(255),"
					+ "商品項目1 nvarchar(255),"
					+ "商品項目2 nvarchar(255),"
					+ "商品項目3 nvarchar(255),"
					+ "売上項目1 nvarchar(255),"
					+ "売上項目2 nvarchar(255),"
					+ "売上項目3 nvarchar(255)";
		}
	}
}
