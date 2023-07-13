//
// AccountTransferHeaderLine.cs
// 
// 口座振替 ヘッダ行作業クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/27 勝呂):新規作成
//
using CommonLib.BaseFactory.PcaInvoiceDataConverter;
using CommonLib.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PcaInvoiceDataConverter.BaseFactory
{
	/// <summary>
	/// 口座振替ヘッダ行作業
	/// </summary>
	public class AccountTransferHeaderLine
	{
		public int 請求書No { get; set; }
		public int 顧客ID { get; set; }
		public string 得意先No { get; set; }
		public DateTime? 請求日付 { get; set; }
		public int 合計請求額税込 { get; set; }
		public int 消費税額 { get; set; }
		public int 明細行数 { get; set; }
		public int 消費税行数 { get; set; }
		public int 記事行数 { get; set; }
		public bool 紙請求書 { get; set; }

		/// <summary>
		/// 口座振替明細行作業リスト
		/// </summary>
		public List<InvoiceDetailLine> DetailLineList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public AccountTransferHeaderLine()
		{
			請求書No = 0;
			顧客ID = 0;
			得意先No = string.Empty;
			請求日付 = null;
			合計請求額税込 = 0;
			消費税額 = 0;
			明細行数 = 0;
			消費税行数 = 0;
			記事行数 = 0;
			紙請求書 = false;
			DetailLineList = new List<InvoiceDetailLine>();
		}

		/// <summary>
		/// タイトル行の取得
		/// </summary>
		/// <returns></returns>
		public static string GetTitle()
		{
			List<string> result = new List<string>
			{
				"請求書No",
				"顧客ID",
				"得意先No",
				"請求日付",
				"合計請求額（税込）",
				"消費税額",
				"明細行数",
				"消費税行数",
				"記事行数",
				"紙請求書",
			};
			return string.Join(",", result.ToArray());
		}


		/// <summary>
		/// 明細行数の取得
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public int GetInvoiceDetailBillCount()
		{
			return DetailLineList.FindAll(p => p.行タイプ == InvoiceDetailLine.TypeBill || p.行タイプ == InvoiceDetailLine.TypeShipping).Count;
		}

		/// <summary>
		/// 消費税行数の取得
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public int GetInvoiceDetailTaxCount()
		{
			return DetailLineList.FindAll(p => p.行タイプ == InvoiceDetailLine.TypeTax).Count;
		}

		/// <summary>
		/// 記事行数の取得
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public int GetInvoiceDetailCommentCount()
		{
			return DetailLineList.FindAll(p => p.行タイプ == InvoiceDetailLine.TypeComment).Count;
		}

		/// <summary>
		/// ヘッダ行作業のDataTableの作成
		/// </summary>
		/// <param name="list">ヘッダ行リスト</param>
		/// <returns>DataTable</returns>
		public static DataTable GetHeaderLineDataTable(List<AccountTransferHeaderLine> list)
		{
			DataTable table = new DataTable();
			table.Columns.Add("請求書No", typeof(int));
			table.Columns.Add("顧客ID", typeof(int));
			table.Columns.Add("得意先No", typeof(int));
			table.Columns.Add("請求日付", typeof(int));
			table.Columns.Add("合計請求額（税込）", typeof(int));
			table.Columns.Add("消費税額", typeof(int));
			table.Columns.Add("明細行数", typeof(int));
			table.Columns.Add("消費税行数", typeof(int));
			table.Columns.Add("記事行数", typeof(int));
			table.Columns.Add("紙請求書", typeof(string));

			foreach (AccountTransferHeaderLine header in list)
			{
				DataRow row = table.NewRow();
				row["請求書No"] = header.請求書No;
				row["顧客ID"] = header.顧客ID;
				row["得意先No"] = header.得意先No;
				row["請求日付"] = header.請求日付.Value.ToDate().ToIntYMD();
				row["合計請求額（税込）"] = header.合計請求額税込;
				row["消費税額"] = header.消費税額;
				row["明細行数"] = header.明細行数;
				row["消費税行数"] = header.消費税行数;
				row["記事行数"] = header.記事行数;
				row["紙請求書"] = (header.紙請求書) ? "TRUE" : "FALSE";
				table.Rows.Add(row);
			}
			return table;
		}

		/// <summary>
		/// 明細行のDataTableの作成
		/// </summary>
		/// <returns>DataTable</returns>
		public DataTable GetDetailLineDataTable()
		{
			DataTable table = new DataTable();
			table.Columns.AddRange(InvoiceDetailLine.GetDataColumn());
			foreach (InvoiceDetailLine detail in DetailLineList)
			{
				table.Rows.Add(detail.GetDataRow(table.NewRow()));
			}
			return table;
		}

		/// <summary>
		/// invoice_header.tsvのヘッダ行の出力
		/// </summary>
		/// <returns></returns>
		public static string GetHeaderLineTitle()
		{
			return "\"0\"\t\"0\"\t\"0\"\t\"\"\t\"\"\t\"0\"\t\"0\"\t\"0\"\t\"0\"\t\"0\"";
		}

		/// <summary>
		/// invoice_header.tsvのデータ行の出力
		/// </summary>
		/// <returns></returns>
		public string GetHeaderLineData()
		{
			return string.Format("\"1\"\t\"{0}\"\t\"{1}\"\t\"{2}\"\t\"{3}\"\t\"{4}\"\t\"{5}\"\t\"{6}\"\t\"{7}\"\t\"{8}\""
										, 請求書No, 顧客ID, 得意先No, 請求日付.Value.ToString("yyyy/MM/dd"), 合計請求額税込.CommaEdit(), 消費税額.CommaEdit(), 明細行数, 消費税行数, 記事行数);
		}

		/// <summary>
		/// invoice_header.tsvのフッター行の出力
		/// </summary>
		/// <returns></returns>
		public static string GetHeaderLineFooter()
		{
			return "\"9\"\t\"0\"\t\"0\"\t\"\"\t\"\"\t\"0\"\t\"0\"\t\"0\"\t\"0\"\t\"0\"";
		}


		/// <summary>
		/// AGREX口振通知書開始行の取得
		/// </summary>
		/// <param name="cust">顧客情報</param>
		/// <param name="detailData">請求明細データ</param>
		/// <param name="juchuCode">受注コード</param>
		/// <param name="口座振替請求日">口座振替請求日</param>
		/// <param name="WEB請求書発行開始日">WEB請求書発行開始日</param>
		/// <param name="WEB請求書発行終了日">WEB請求書発行終了日</param>
		/// <returns>AGREX口振通知書開始行</returns>
		public string GetAgrexStartLine(CustomerInfo cust, InvoiceDetailData detailData, string juchuCode, DateTime 口座振替請求日, DateTime WEB請求書発行開始日, DateTime WEB請求書発行終了日)
		{
			string kokyakuBuka, kokyakuShimeiKanji;
			cust.SplitCustomerName(out kokyakuBuka, out kokyakuShimeiKanji);

			string[] array =
			{
				"1",	// '1:レコード区分
				AgrexDefine.ContactCode,	// 2:契約先コード:132002
				juchuCode,	// 3:受注コード
				AgrexDefine.ConsignmentCode,	// 4:委託者コード（ミックは 206271 固定）
				AgrexDefine.ConsignmentKubun,	// 5:委託者区分
				StringUtil.Right(cust.APLUSコード, 14),	// 6:顧客コード（APLUSコードの下１４桁）
				detailData.請求先郵便番号数字のみ,	// 7:顧客郵便番号
				detailData.請求先住所,	// 8:顧客住所漢字（全角１７＋１７＋１６）
				"",	// 9:顧客住所カナ（省略）
				string.Format("お客様コード No.{0}", cust.得意先No),	// 10:顧客会社名（お客様コード）
				kokyakuBuka,	// 11:顧客部課名（得意先名１）
				kokyakuShimeiKanji,	// 12:顧客氏名漢字（得意先名２）
				"",	// 13:顧客氏名カナ（省略）
				detailData.請求先会社TEL,	// 14:顧客電話番号
				StringUtil.Right(cust.口座番号, 7),	// 15:顧客口座番号
				"",	// 16:案内書発行不要フラグ（なし）
				string.Format("{0} ～ {1} 分", WEB請求書発行開始日.ToString("yyyy/MM/dd"), WEB請求書発行終了日.ToString("yyyy/MM/dd")),	// 17:案内書通信文
				合計請求額税込.ToString(),	// 18:振替金額
				(合計請求額税込 - 消費税額).ToString(),	// 19:合計金額
				消費税額.ToString(),	// 20:消費税
				StringUtil.Right(cust.銀行コード, 4),	// 21:銀行コード
				StringUtil.Right(cust.支店コード, 3),	// 22:支店コード
				StringUtil.Right(cust.預金種別, 1),	// 23:預金種別
				StringUtil.ConvertNarrowForUnicode(cust.預金者名),	// 24:口座名義
				"",	// 25:新規コード（省略）
				口座振替請求日.ToString("yyyyMMdd")	// 26:振替日
			};
			return string.Join(",", array);
		}

		/// <summary>
		/// AGREX口振通知書明細行の取得
		/// </summary>
		/// <param name="juchuCode">受注コード</param>
		/// <param name="detailLine">明細行作業</param>
		/// <returns>AGREX口振通知書明細行</returns>
		public string GetAgrexDataLine(string juchuCode, InvoiceDetailLine detailLine)
		{
			string[] array = new string[10];
			array[0] = "2";
			array[1] = AgrexDefine.ContactCode;	// 契約先コード:132002
			array[2] = juchuCode;	// 受注コード
			array[3] = detailLine.枝番.ToString();
			array[4] = detailLine.売上日付.Value.ToString("yyyy/MM/dd");
			array[5] = detailLine.伝票No.ToString();
			array[6] = detailLine.商品名;
			switch (detailLine.行タイプ)
			{
				case InvoiceDetailLine.TypeBill:
					{
						array[7] = detailLine.数量.ToString();
						array[8] = detailLine.単価.ToString();
						array[9] = detailLine.金額.ToString();
					}
					break;
				case InvoiceDetailLine.TypeShipping:
				case InvoiceDetailLine.TypeTax:
					{
						array[7] = "";
						array[8] = "";
						array[9] = detailLine.金額.ToString();
					}
					break;
				default:
					array[7] = "";
					array[8] = "";
					array[9] = "";
					break;
			}
			return string.Join(",", array);
		}

		/// <summary>
		/// AGREX口振通知書終了行の取得
		/// </summary>
		/// <param name="juchuCode">受注コード</param>
		/// <returns>AGREX口振通知書終了行</returns>
		public string GetAgrexEndLine(string juchuCode)
		{
			return string.Format("9,{0}{1}", AgrexDefine.ContactCode, juchuCode);
		}
	}
}
