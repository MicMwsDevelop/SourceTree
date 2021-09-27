using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MwsLib.DB;
using System.Security.Policy;
using System.Windows.Forms.VisualStyles;

namespace MwsLib.BaseFactory.Estore
{
	public class WebJucu
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

		public int order_accept_id { get; set; }

		public DateTime? 希望着日 { get; set; }

		public int 変更前Web受注No { get; set; }

		/// <summary>
		/// WebJucu.txtファイルの取得
		/// </summary>
		public static string OutputFilename
		{
			get
			{
				return string.Format("WebJucu-{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}.txt", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public WebJucu()
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

			order_accept_id = 0;
			希望着日 = null;
			変更前Web受注No = 0;
		}

		/// <summary>
		/// 着日指定文字列の取得
		/// </summary>
		/// <returns></returns>
		public string GetChakubiString()
		{
			if (希望着日.HasValue)
			{
				return string.Format("着日指定：　{0}月{1}日", 希望着日.Value.Month, 希望着日.Value.Day);
			}
			return string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<WebJucu> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<WebJucu> result = new List<WebJucu>();
				foreach (DataRow row in table.Rows)
				{
					WebJucu data = new WebJucu
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
						order_accept_id = DataBaseValue.ConvObjectToInt(row["order_accept_id"]),
						希望着日 = DataBaseValue.ConvObjectToDateTimeNull(row["希望着日"]),
					};
					data.変更前Web受注No = data.受注No;

					result.Add(data);
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// 送料設定済みデータの取得
		/// </summary>
		/// <returns></returns>
		public static WebJucu SoryoData()
		{
			WebJucu ret = new WebJucu();
			ret.受注No = 0;
			ret.受注日 = string.Empty;
			ret.納期 = "0";
			ret.得意先No = string.Empty;
			ret.顧客名 = string.Empty;
			ret.直送先コード = string.Empty;
			ret.先方担当者 = string.Empty;
			ret.PCA部門No = string.Empty;
			ret.PCA主担当No = string.Empty;
			ret.摘要コード = "031";
			ret.摘要名 = "Web受注分";
			ret.goods_code = "000600";
			ret.マスター区分 = "1";
			ret.商品名 = "送料(980)";
			ret.倉庫コード = "0";
			ret.入数 = "0";
			ret.箱数 = "0";
			ret.数量 = "0";
			ret.単位 = "0";
			ret.単価 = "0";
			ret.受注金額 = "980";
			ret.原単価 = "0";
			ret.原価額 = "0";
			ret.粗利益 = "0";
			ret.外税額 = "0";
			ret.内税額 = "0";
			ret.税区分 = "2";
			ret.税込区分 = "0";
			ret.備考 = string.Empty;
			ret.標準価格 = "0";
			ret.自動発注区分 = "0";
			ret.売単価 = "0";
			ret.売価金額 = "0";
			ret.規格型番 = string.Empty;
			ret.色 = string.Empty;
			ret.サイズ = string.Empty;
			ret.計算式コード = "0";
			ret.商品項目1 = "0";
			ret.商品項目2 = "0";
			ret.商品項目3 = "0";
			ret.売上項目1 = "0";
			ret.売上項目2 = "0";
			ret.売上項目3 = "0";

			ret.order_accept_id = 0;
			ret.希望着日 = null;
			ret.変更前Web受注No = 0;

			return ret;
		}

		/// <summary>
		/// 着日指定設定済みデータの取得
		/// </summary>
		/// <returns></returns>
		public static WebJucu ChakubiData()
		{
			WebJucu ret = new WebJucu();

			ret.受注No = 0;
			ret.受注日 = string.Empty;
			ret.納期 = "0";
			ret.得意先No = string.Empty;
			ret.顧客名 = string.Empty;
			ret.直送先コード = string.Empty;
			ret.先方担当者 = string.Empty;
			ret.PCA部門No = string.Empty;
			ret.PCA主担当No = string.Empty;
			ret.摘要コード = "31";		// 031ではないのか？
			ret.摘要名 = "Web受注分";
			ret.goods_code = "000020";
			ret.マスター区分 = "4";
			ret.商品名 = string.Empty;
			ret.倉庫コード = "0";
			ret.入数 = "0";
			ret.箱数 = "0";
			ret.数量 = "0";
			ret.単位 = string.Empty;
			ret.単価 = "0";
			ret.受注金額 = "0";
			ret.原単価 = "0";
			ret.原価額 = "0";
			ret.粗利益 = "0";
			ret.外税額 = "0";
			ret.内税額 = "0";
			ret.税区分 = "2";
			ret.税込区分 = "0";
			ret.備考 = string.Empty;
			ret.標準価格 = "0";
			ret.自動発注区分 = "0";
			ret.売単価 = "0";
			ret.売価金額 = "0";
			ret.規格型番 = string.Empty;
			ret.色 = string.Empty;
			ret.サイズ = string.Empty;
			ret.計算式コード = "0";
			ret.商品項目1 = "0";
			ret.商品項目2 = "0";
			ret.商品項目3 = "0";
			ret.売上項目1 = "0";
			ret.売上項目2 = "0";
			ret.売上項目3 = "0";

			ret.order_accept_id = 0;
			ret.希望着日 = null;
			ret.変更前Web受注No = 0;

			return ret;
		}

		/// <summary>
		/// CSV文字列の取得
		/// </summary>
		/// <param name="taxRate">消費税率</param>
		/// <returns>CSV文字列</returns>
		public string ToCsvString(int taxRate)
		{
			List<string> list = new List<string>();
			list.Add(受注No.ToString());
			list.Add(受注日);
			list.Add(納期);
			list.Add(得意先No);
			list.Add(顧客名);
			list.Add(直送先コード);
			list.Add(先方担当者);
			list.Add(PCA部門No);
			list.Add(PCA主担当No);
			list.Add(摘要コード);
			list.Add(摘要名);
			list.Add(goods_code);
			list.Add(マスター区分);
			list.Add(商品名);
			list.Add(倉庫コード);
			list.Add(入数);
			list.Add(箱数);
			list.Add(数量);
			list.Add(単位);
			list.Add(単価);
			list.Add(受注金額);
			list.Add(原単価);
			list.Add(原価額);
			list.Add(粗利益);
			list.Add(外税額);
			list.Add(内税額);
			list.Add(税区分);
			list.Add(税込区分);
			list.Add(備考);
			list.Add(標準価格);
			list.Add(自動発注区分);
			list.Add(売単価);
			list.Add(売価金額);
			list.Add(規格型番);
			list.Add(色);
			list.Add(サイズ);
			list.Add(計算式コード);
			list.Add(商品項目1);
			list.Add(商品項目2);
			list.Add(商品項目3);
			list.Add(売上項目1);
			list.Add(売上項目2);
			list.Add(売上項目3);
			list.Add(taxRate.ToString());  // 税率
			list.Add("0");      // 伝票消費税
			list.Add("");       // プロジェクトコード
			list.Add("");       // 受注No2
			list.Add("");       // 明細納期
			list.Add("0");      // データ区分
			list.Add("");		// 商品名2
			return string.Join(",", list.ToArray());
		}
	}
}
