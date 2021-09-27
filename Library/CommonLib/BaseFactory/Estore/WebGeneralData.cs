using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MwsLib.DB;
using MwsLib.Common;

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
		/// コンストラクタ
		/// </summary>
		/// <param name="src"></param>
		public WebGeneralData(WebGeneralData src)
		{
			受注No = src.受注No;
			受注日 = src.受注日;
			納期 = src.納期;
			得意先No = src.得意先No;
			顧客名 = src.顧客名;
			直送先コード = src.直送先コード;
			先方担当者 = src.先方担当者;
			PCA部門No = src.PCA部門No;
			PCA主担当No = src.PCA主担当No;
			摘要コード = src.摘要コード;
			摘要名 = src.摘要名;
			goods_code = src.goods_code;
			マスター区分 = src.マスター区分;
			商品名 = src.商品名;
			倉庫コード = src.倉庫コード;
			入数 = src.入数;
			箱数 = src.箱数;
			数量 = src.数量;
			単位 = src.単位;
			単価 = src.単価;
			受注金額 = src.受注金額;
			原単価 = src.原単価;
			原価額 = src.原価額;
			粗利益 = src.粗利益;
			外税額 = src.外税額;
			内税額 = src.内税額;
			税区分 = src.税区分;
			税込区分 = src.税込区分;
			備考 = src.備考;
			標準価格 = src.標準価格;
			自動発注区分 = src.自動発注区分;
			売単価 = src.売単価;
			売価金額 = src.売価金額;
			規格型番 = src.規格型番;
			色 = src.色;
			サイズ = src.サイズ;
			計算式コード = src.計算式コード;
			商品項目1 = src.商品項目1;
			商品項目2 = src.商品項目2;
			商品項目3 = src.商品項目3;
			売上項目1 = src.売上項目1;
			売上項目2 = src.売上項目2;
			売上項目3 = src.売上項目3;
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
		/// DataRow → WebGeneralData
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static WebGeneralData DataRowToData(DataRow row)
		{
			WebGeneralData data = new WebGeneralData
			{
				受注No = DataBaseValue.ConvObjectToInt(row["受注No"]),
				受注日 = row["受注日"].ToString().Trim(),
				納期 = row["納期"].ToString().Trim(),
				得意先No = row["得意先No"].ToString().Trim(),
				顧客名 = row["顧客名"].ToString().Trim(),
				直送先コード = row["直送先コード"].ToString().Trim(),
				先方担当者 = row["先方担当者"].ToString().Trim(),
				PCA部門No = row["PCA部門No"].ToString().Trim(),
				PCA主担当No = row["PCA主担当No"].ToString().Trim(),
				摘要コード = row["摘要コード"].ToString().Trim(),
				摘要名 = row["摘要名"].ToString().Trim(),
				goods_code = row["goods_code"].ToString().Trim(),
				マスター区分 = row["マスター区分"].ToString().Trim(),
				商品名 = row["商品名"].ToString().Trim(),
				倉庫コード = row["倉庫コード"].ToString().Trim(),
				入数 = row["入数"].ToString().Trim(),
				箱数 = row["箱数"].ToString().Trim(),
				数量 = row["数量"].ToString().Trim(),
				単位 = row["単位"].ToString().Trim(),
				単価 = row["単価"].ToString().Trim(),
				受注金額 = row["受注金額"].ToString().Trim(),
				原単価 = row["原単価"].ToString().Trim(),
				原価額 = row["原価額"].ToString().Trim(),
				粗利益 = row["粗利益"].ToString().Trim(),
				外税額 = row["外税額"].ToString().Trim(),
				内税額 = row["内税額"].ToString().Trim(),
				税区分 = row["税区分"].ToString().Trim(),
				税込区分 = row["税込区分"].ToString().Trim(),
				備考 = row["備考"].ToString().Trim(),
				標準価格 = row["標準価格"].ToString().Trim(),
				自動発注区分 = row["自動発注区分"].ToString().Trim(),
				売単価 = row["売単価"].ToString().Trim(),
				売価金額 = row["売価金額"].ToString().Trim(),
				規格型番 = row["規格型番"].ToString().Trim(),
				色 = row["色"].ToString().Trim(),
				サイズ = row["サイズ"].ToString().Trim(),
				計算式コード = row["計算式コード"].ToString().Trim(),
				商品項目1 = row["商品項目1"].ToString().Trim(),
				商品項目2 = row["商品項目2"].ToString().Trim(),
				商品項目3 = row["商品項目3"].ToString().Trim(),
				売上項目1 = row["売上項目1"].ToString().Trim(),
				売上項目2 = row["売上項目2"].ToString().Trim(),
				売上項目3 = row["売上項目3"].ToString().Trim(),
			};
			return data;
		}

/*
		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<WebGeneralData> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<WebGeneralData> result = new List<WebGeneralData>();
				foreach (DataRow row in table.Rows)
				{
					WebGeneralData data = new WebGeneralData
					{
						受注No = DataBaseValue.ConvObjectToInt(row["受注No"]),
						受注日 = row["受注日"].ToString().Trim(),
						納期 = row["納期"].ToString().Trim(),
						得意先No = row["得意先No"].ToString().Trim(),
						顧客名 = row["顧客名"].ToString().Trim(),
						直送先コード = row["直送先コード"].ToString().Trim(),
						先方担当者 = row["先方担当者"].ToString().Trim(),
						PCA部門No = row["PCA部門No"].ToString().Trim(),
						PCA主担当No = row["PCA主担当No"].ToString().Trim(),
						摘要コード = row["摘要コード"].ToString().Trim(),
						摘要名 = row["摘要名"].ToString().Trim(),
						goods_code = row["goods_code"].ToString().Trim(),
						マスター区分 = row["マスター区分"].ToString().Trim(),
						商品名 = row["商品名"].ToString().Trim(),
						倉庫コード = row["倉庫コード"].ToString().Trim(),
						入数 = row["入数"].ToString().Trim(),
						箱数 = row["箱数"].ToString().Trim(),
						数量 = row["数量"].ToString().Trim(),
						単位 = row["単位"].ToString().Trim(),
						単価 = row["単価"].ToString().Trim(),
						受注金額 = row["受注金額"].ToString().Trim(),
						原単価 = row["原単価"].ToString().Trim(),
						原価額 = row["原価額"].ToString().Trim(),
						粗利益 = row["粗利益"].ToString().Trim(),
						外税額 = row["外税額"].ToString().Trim(),
						内税額 = row["内税額"].ToString().Trim(),
						税区分 = row["税区分"].ToString().Trim(),
						税込区分 = row["税込区分"].ToString().Trim(),
						備考 = row["備考"].ToString().Trim(),
						標準価格 = row["標準価格"].ToString().Trim(),
						自動発注区分 = row["自動発注区分"].ToString().Trim(),
						売単価 = row["売単価"].ToString().Trim(),
						売価金額 = row["売価金額"].ToString().Trim(),
						規格型番 = row["規格型番"].ToString().Trim(),
						色 = row["色"].ToString().Trim(),
						サイズ = row["サイズ"].ToString().Trim(),
						計算式コード = row["計算式コード"].ToString().Trim(),
						商品項目1 = row["商品項目1"].ToString().Trim(),
						商品項目2 = row["商品項目2"].ToString().Trim(),
						商品項目3 = row["商品項目3"].ToString().Trim(),
						売上項目1 = row["売上項目1"].ToString().Trim(),
						売上項目2 = row["売上項目2"].ToString().Trim(),
						売上項目3 = row["売上項目3"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
*/
	}
}
