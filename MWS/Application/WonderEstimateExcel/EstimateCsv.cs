//
// EstimateCsv.cs
// 
// WonderWeb見積書CSVファイルクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/03/31 勝呂)
//
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

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
		/// １ページ行数
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
		public void WriteExcelFile(XLWorkbook wb)
		{
			IXLWorksheet wsOrg = wb.Worksheet("見積書原本");
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
				// 画像ファイルはコピーできないので、別途貼り付け
				ws.AddPicture(global::WonderEstimateExcel.Properties.Resources.MicLogo).MoveTo(ws.Cell(curRow + 4, 68));

				curRow += ExcelPageLineCount;
			}
			// ページ毎に見積書内容を設定
			curRow = 0;
			for (int i = 0; i < maxPage; i++)
			{
				ws.Cell(curRow + 2, 3).SetValue(Header.医院名);			// 顧客名
				ws.Cell(curRow + 2, 73).SetValue(Header.見積番号);		// 見積書No
				ws.Cell(curRow + 3, 66).SetValue(Header.発行日);		// 発行日
				ws.Cell(curRow + 4, 13).SetValue(Header.見積金額合計);	// お見積金額合計
				ws.Cell(curRow + 5, 13).SetValue(Header.消費税);		// 消費税
				ws.Cell(curRow + 6, 13).SetValue(Header.月額リース金額);// 月額リース金額
				ws.Cell(curRow + 8, 12).SetValue(Header.件名);			// 件名
				ws.Cell(curRow + 9, 12).SetValue(Header.納期);			// 納期
				ws.Cell(curRow + 10, 12).SetValue(Header.支払条件);		// 支払条件
				ws.Cell(curRow + 11, 12).SetValue(Header.納入場所);		// 納入場所
				ws.Cell(curRow + 12, 12).SetValue(Header.有効期限);		// 見積有効期限
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
			// シートの削除「印刷イメージ」「印刷フィールド」「印刷座標」「見積書原本」
			wb.Worksheet("印刷イメージ").Delete();
			wb.Worksheet("印刷フィールド").Delete();
			wb.Worksheet("印刷座標").Delete();
			wb.Worksheet("見積書原本").Delete();
		}
	}
}
