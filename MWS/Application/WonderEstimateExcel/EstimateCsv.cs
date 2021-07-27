//
// EstimateCsv.cs
// 
// WonderWeb見積書CSVファイルクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2021/03/31):新規作成(勝呂)
// Ver1.04(2021/05/26):備考内にカンマがあるときにエラー発生(勝呂)
// Ver1.05(2021/06/11):注文書と注文請書の出力機能を追加(勝呂)
//
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace WonderEstimateExcel
{
	/// <summary>
	/// Wonder Web見積書CSV
	/// </summary>
	public class EstimateCsv
	{
		/// <summary>
		/// 明細行数
		/// </summary>
		public const short DetailLineCount = 20;

		/// <summary>
		/// 見積書１ページ行数
		/// </summary>
		public const short ExcelPageLineCount = 42;

		/// <summary>
		/// 消費税率
		/// </summary>
		public const short TaxRate = 10;

		/// <summary>
		/// WonderWeb見積書ヘッダ行情報
		/// </summary>
		public EstimateHeader Header{ get; set; }

		/// <summary>
		/// 見積書明細行リスト
		/// </summary>
		public List<EstimateDetail> DetailList { get; set; }

		/// <summary>
		/// 標準価格計合計
		/// </summary>
		public int 標準価格計合計
		{
			get
			{
				int total = 0;
				foreach (EstimateDetail detail in DetailList)
				{
					total += detail.標準価格計;
				}
				return total;
			}
		}

		/// <summary>
		/// 提供価格合計
		/// </summary>
		public int 提供価格合計
		{
			get
			{
				int total = 0;
				foreach (EstimateDetail detail in DetailList)
				{
					total += detail.提供価格;
				}
				return total;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public EstimateCsv()
		{
			Header = new EstimateHeader();
			DetailList = new List<EstimateDetail>();
		}

		/// <summary>
		/// カンマ文字列の取得
		/// </summary>
		/// <param name="price"></param>
		/// <returns></returns>
		public static string CommaEdit(int price)
		{
			return string.Format(@"\{0:#,0}", price);
		}

		/// <summary>
		/// ダブルコーテーションを取り除く
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string Trim(string str)
		{
			return str.Replace("\"", "");
		}

		/// <summary>
		/// 消費税額の取得
		/// </summary>
		/// <param name="price">価格</param>
		/// <returns>消費税額</returns>
		public static int GetTax(int price)
		{
			if (0 < price)
			{
				return ((price * TaxRate) + 50) / 100;
			}
			return 0;
		}

		/// <summary>
		/// 税込価格の取得
		/// </summary>
		/// <param name="price">価格</param>
		/// <returns>税込価格</returns>
		public static int GetOutsideTaxPrice(int price)
		{
			if (0 < price)
			{
				return price + GetTax(price);
			}
			return 0;
		}

		/// <summary>
		/// クリア
		/// </summary>
		public void Clear()
		{
			DetailList.Clear();
		}

		/// <summary>
		/// 最大ページ数の取得
		/// </summary>
		/// <returns>最大ページ数</returns>
		public int GetMaxPage()
		{
			if (DetailLineCount < DetailList.Count)
			{
				return (DetailList.Count / DetailLineCount) + 1;
			}
			return 1;
		}

		/// <summary>
		/// ページに該当する明細行リストの取得
		/// </summary>
		/// <param name="page">カレントページ</param>
		/// <returns>明細行リスト</returns>
		public List<EstimateDetail> GetDetailListByPage(int page)
		{
			if (0 < DetailList.Count)
			{
				if (DetailLineCount < DetailList.Count)
				{
					List<EstimateDetail> ret = new List<EstimateDetail>();
					int startIndex = DetailLineCount * (page - 1);
					for (int i = startIndex, j = 0; i < DetailList.Count; i++, j++)
					{
						if (j == DetailLineCount)
						{
							break;
						}
						ret.Add(DetailList[i]);
					}
					return ret;
				}
				return DetailList;
			}
			return null;
		}

		/// <summary>
		/// カンマ区切り（ダブルクォーテーション、フィールド内のカンマ含む）
		/// </summary>
		/// <param name="csv"></param>
		/// <returns></returns>
		public static string[] Split(string csv)
		{
			Regex reg = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
			string[] split = reg.Split(csv); // ""で囲まれたものは分割しない
			for (int i = 0; i < split.Length; i++)
			{
				split[i] = split[i].Trim('"'); // 先頭と最後尾の '"' を削除
			}
			return split;
		}

		/// <summary>
		/// WonderWeb見積書CSVファイルの読込み
		/// </summary>
		/// <param name="sr">ファイルストリーム</param>
		public void ReadCsvFile(StreamReader sr)
		{
			Clear();

			string prevCsv = string.Empty;
			int firstLine = 0;
			while (!sr.EndOfStream)
			{
				string csv = sr.ReadLine();
				if (0 == firstLine)
				{
					// 先頭行スキップ
					firstLine = 1;
					continue;
				}
				if (1 == firstLine)
				{
					// ヘッダ部
					if (0 < csv.Length)
					{
						// Ver1.04(2021/05/26):備考内にカンマがあるときにエラー発生(勝呂)
						if ('\"' != csv[0])
							// 先頭にダブルクォーテーションがない
							// ■paletteと連動するには、自動精算機連携￥880-/月のご契約が必要になります。","" → "■paletteと連動するには、自動精算機連携￥880-/月のご契約が必要になります。",""
							csv = "\"" + csv;
						if ('\"' != csv[csv.Length - 1])
							// 末尾にダブルクォーテーションがない
							// ■保守形態はｵﾝｻｲﾄﾌﾙﾒﾝﾃﾅﾝｽ保守(月曜～金曜9：00～18：00) → "■保守形態はｵﾝｻｲﾄﾌﾙﾒﾝﾃﾅﾝｽ保守(月曜～金曜9：00～18：00)"
							csv += "\"";

						string[] split = Split(csv);
						if (EstimateHeader.FieldCount == split.Length)
						{
							// 備考に改行が含まない
							if (false == Header.SetRecord(csv))
							{
								throw new Exception("WonderWeb見積書CSVではありません。");
							}
							firstLine++;
						}
						// 備考に改行が含まれる時の処理
						// ex."139405","2021/04/09","本社","東京都新宿区新宿1-8-5",～,"045-784-2000","あ
						// い
						// う
						// え","5年/60ヶ月"
						else if (1 == split.Length)
						{
							// 備考に改行が許可されているので、改行コードを付加して連結する
							prevCsv += "\r\n" + csv;
						}
						else if (2 == split.Length)
						{
							// 備考の最後の行
							if (false == Header.SetRecord(prevCsv + "\r\n" + csv))
							{
								throw new Exception("WonderWeb見積書CSVではありません。");
							}
							firstLine++;
						}
						else
						{
							// １行目
							prevCsv = csv;
						}
					}
				}
				else
				{
					// 明細部
					EstimateDetail detail = new EstimateDetail();
					if (detail.SetRecord(csv))
					{
						DetailList.Add(detail);
					}
					else
					{
						throw new Exception("WonderWeb見積書CSVではありません。");
					}
				}
			}
		}

		/// <summary>
		/// 見積書エクセルファイルの出力
		/// </summary>
		/// <param name="wb"></param>
		public void WriteEstimateExcelFile(XLWorkbook wb)
		{
			IXLWorksheet wsOrg = wb.Worksheet("原本");
			IXLWorksheet ws = wsOrg.CopyTo("見積書");
			int maxPage = GetMaxPage();

			// フォームを印刷ページ分コピー
			int curRow = ExcelPageLineCount;
			for (int i = 1; i < maxPage; i++)
			{
				ws.Cell(curRow + 1, 1).Value = wsOrg.Range("A1:CB42");

				// 書式の設定
				ws.Row(curRow + 1).Height = 16;
				for (int j = 2; j < 7; j++)
				{
					ws.Row(curRow + j).Height = 15;
				}
				for (int j = 7; j < 43; j++)
				{
					ws.Row(curRow + j).Height = 12;
				}
				// MICロゴの貼り付け
				ws.AddPicture(global::WonderEstimateExcel.Properties.Resources.MicLogo).MoveTo(ws.Cell(curRow + 4, 68));

				curRow += ExcelPageLineCount;
			}
			// ページ毎に見積書内容を設定
			curRow = 0;
			for (int i = 0; i < maxPage; i++)
			{
				ws.Cell(curRow + 2, 3).SetValue(Header.顧客名);			// 顧客名
				ws.Cell(curRow + 2, 73).SetValue(Header.見積番号);		// 見積書No
				ws.Cell(curRow + 3, 66).SetValue(Header.発行日);		// 発行日
				ws.Cell(curRow + 4, 13).SetValue(Header.見積金額合計);	// お見積金額合計
				ws.Cell(curRow + 5, 13).SetValue(Header.消費税);		// 消費税
				ws.Cell(curRow + 6, 13).SetValue(Header.月額リース金額);// 月額リース金額
				ws.Cell(curRow + 8, 11).SetValue(Header.件名);			// 件名
				ws.Cell(curRow + 9, 11).SetValue(Header.納期);			// 納期
				ws.Cell(curRow + 10, 11).SetValue(Header.支払条件);		// 支払条件
				ws.Cell(curRow + 11, 11).SetValue(Header.納入場所);		// 納入場所
				ws.Cell(curRow + 12, 11).SetValue(Header.有効期限);		// 見積有効期限
				ws.Cell(curRow + 6, 58).SetValue(Header.担当支店名);	// 担当支店名
				ws.Cell(curRow + 7, 58).SetValue(Header.担当支店住所1);	// 担当支店住所1
				ws.Cell(curRow + 8, 58).SetValue(Header.担当支店住所2);	// 担当支店住所2
				ws.Cell(curRow + 9, 61).SetValue(Header.担当支店TEL);	// 担当支店TEL
				ws.Cell(curRow + 9, 71).SetValue(Header.担当支店FAX);	// 担当支店FAX
				ws.Cell(curRow + 10, 69).SetValue(Header.担当者名);		// 担当者名
				ws.Cell(curRow + 14, 76).SetValue(string.Format("{0}/{1}", i + 1, maxPage));	// ページ数
				ws.Cell(curRow + 38, 5).SetValue(Header.備考);			// 備考

				// ページに対応した明細行リストの取得して設定
				List<EstimateDetail> detailList = this.GetDetailListByPage(i + 1);
				int row = curRow + 16;
				foreach (EstimateDetail detail in detailList)
				{
					ws.Cell(row, 3).SetValue(detail.区分名);
					ws.Cell(row, 11).SetValue(detail.商品名);
					ws.Cell(row, 29).SetValue(detail.数量);
					ws.Cell(row, 32).SetValue(detail.標準価格);
					ws.Cell(row, 39).SetValue(detail.標準価格計);
					ws.Cell(row, 46).SetValue(detail.提供価格);
					ws.Cell(row, 53).SetValue(detail.商品備考);
					row++;
				}
				if (i + 1 == maxPage)
				{
					// 最終ページ
					ws.Cell(curRow + 37, 39).SetValue(標準価格計合計);
					ws.Cell(curRow + 37, 46).SetValue(提供価格合計);
				}
				curRow += ExcelPageLineCount;
			}
			// シートの削除「印刷イメージ」「印刷フィールド」「印刷座標」「原本」
			wb.Worksheet("印刷イメージ").Delete();
			wb.Worksheet("印刷フィールド").Delete();
			wb.Worksheet("印刷座標").Delete();
			wb.Worksheet("原本").Delete();
		}

		/// <summary>
		/// 注文書エクセルファイルの出力
		/// </summary>
		/// <param name="wb"></param>
		// Ver1.05(2021/06/11):注文書と注文請書の出力機能を追加(勝呂)
		public void WriteOrderSheetExcelFile(XLWorkbook wb)
		{
			IXLWorksheet wsOrg = wb.Worksheet("原本");
			IXLWorksheet ws = wsOrg.CopyTo("注文書");

			CultureInfo ci = new CultureInfo("ja-JP") { DateTimeFormat = { Calendar = new JapaneseCalendar() } };	// 令和対応

			ws.Cell(2, 19).SetValue(DateTime.Today.ToString("gg", ci));	// 元号
			ws.Cell(18, 2).SetValue(Header.件名);					// 商品名
			ws.Cell(18, 13).SetValue("1");							// 数量
			ws.Cell(18, 19).SetValue(Header.見積金額合計2);			// 金額（税込）
			ws.Cell(20, 2).SetValue(string.Format("詳細は別紙見積No:{0}", Header.見積番号));	// 見積書No
			ws.Cell(24, 13).SetValue(Header.顧客住所);				// 納品先住所
			ws.Cell(25, 13).SetValue(Header.顧客TEL);				// 電話番号
			ws.Cell(26, 13).SetValue(Header.顧客名);				// 名前
			ws.Cell(28, 3).SetValue(Header.納期);					// 納品予定日
			ws.Cell(29, 3).SetValue(Header.支払条件);				// お支払条件
			ws.Cell(32, 3).SetValue(Header.備考);					// 備考
			ws.Cell(40, 18).SetValue(string.Format("担当支店名：{0}", Header.担当支店名));		// 担当支店名
			ws.Cell(41, 18).SetValue(string.Format("担当：{0}", Header.担当者名));				// 担当者名

			// 印鑑マークの画像ファイルはコピーできないので、別途貼り付け
			ws.AddPicture(global::WonderEstimateExcel.Properties.Resources.Seal).MoveTo(ws.Cell(15, 24), 0, 10);

			// シートの削除「印刷イメージ」「印刷フィールド」「印刷座標」「原本」
			wb.Worksheet("印刷イメージ").Delete();
			wb.Worksheet("印刷フィールド").Delete();
			wb.Worksheet("印刷座標").Delete();
			wb.Worksheet("原本").Delete();
		}

		/// <summary>
		/// 注文請書エクセルファイルの出力
		/// </summary>
		/// <param name="wb"></param>
		// Ver1.05(2021/06/11):注文書と注文請書の出力機能を追加(勝呂)
		public void WriteOrderConfirmExcelFile(XLWorkbook wb)
		{
			IXLWorksheet wsOrg = wb.Worksheet("原本");
			IXLWorksheet ws = wsOrg.CopyTo("注文請書");

			CultureInfo ci = new CultureInfo("ja-JP") { DateTimeFormat = { Calendar = new JapaneseCalendar() } };   // 令和対応

			ws.Cell(2, 6).SetValue(DateTime.Today.ToString("gg", ci)); // 元号
			ws.Cell(6, 2).SetValue(Header.顧客名);               // 名前
			ws.Cell(11, 7).SetValue(Header.担当支店名);     // 担当支店名
			ws.Cell(12, 7).SetValue(Header.担当支店住所1);   // 担当支店住所1
			ws.Cell(13, 7).SetValue(Header.担当支店住所2);   // 担当支店住所2
			ws.Cell(14, 7).SetValue(string.Format("TEL：{0}", Header.担当支店TEL));   // 担当支店TEL
			ws.Cell(14, 10).SetValue(string.Format("FAX：{0}", Header.担当支店FAX));   // 担当支店FAX
			ws.Cell(15, 11).SetValue(Header.担当者名);     // 担当者名

			ws.Cell(18, 2).SetValue(Header.件名);                 // 商品名
			ws.Cell(18, 5).SetValue("1");                          // 数量
			ws.Cell(18, 6).SetValue(Header.見積金額合計2);            // 金額（税込）

			ws.Cell(24, 5).SetValue(Header.顧客住所);              // 納品先住所
			ws.Cell(25, 5).SetValue(Header.顧客TEL);             // 電話番号
			ws.Cell(26, 5).SetValue(Header.顧客名);               // 名前
			ws.Cell(28, 3).SetValue(Header.納期);                 // 納品予定日
			ws.Cell(29, 3).SetValue(Header.支払条件);               // お支払条件
			ws.Cell(32, 3).SetValue(Header.備考);                 // 備考

			// シートの削除「印刷イメージ」「印刷フィールド」「印刷座標」「原本」
			wb.Worksheet("印刷イメージ").Delete();
			wb.Worksheet("印刷フィールド").Delete();
			wb.Worksheet("印刷座標").Delete();
			wb.Worksheet("原本").Delete();
		}
	}
}
