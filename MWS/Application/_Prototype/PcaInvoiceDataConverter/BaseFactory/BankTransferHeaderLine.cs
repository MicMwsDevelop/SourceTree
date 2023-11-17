//
// BankTransferHeaderLine.cs
// 
// 銀行振込 ヘッダ行作業クラス
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
	/// 銀行振込 ヘッダ行作業
	/// </summary>
	public class BankTransferHeaderLine
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
		/// 銀行振込明細行作業リスト
		/// </summary>
		public List<InvoiceDetailLine> DetailLineList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public BankTransferHeaderLine()
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
		/// ヘッダ行のDataTableの作成
		/// </summary>
		/// <param name="list">ヘッダ行リスト</param>
		/// <returns>DataTable</returns>
		public static DataTable GetHeaderLineDataTable(List<BankTransferHeaderLine> list)
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

			foreach (BankTransferHeaderLine header in list)
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
		/// AGREX銀行振込請求書開始行の取得
		/// </summary>
		/// <param name="cust">顧客情報</param>
		/// <param name="detailData">請求明細データ</param>
		/// <param name="juchuCode">受注コード</param>
		/// <param name="入金期限日">入金期限日</param>
		/// <param name="銀行振込請求期間開始日">銀行振込請求期間開始日</param>
		/// <param name="銀行振込請求期間終了日">銀行振込請求期間終了日</param>
		/// <returns>AGREX銀行振込請求書開始行</returns>
		public string GetAgrexStartLine(CustomerInfo cust, InvoiceDetailData detailData, string juchuCode, DateTime 入金期限日, DateTime 銀行振込請求期間開始日, DateTime 銀行振込請求期間終了日)
		{
			string kokyakuBuka, kokyakuShimeiKanji;
			cust.SplitCustomerName(out kokyakuBuka, out kokyakuShimeiKanji);

			string[] array =
			{
				"1",	// '1:レコード区分
				AgrexDefine.BankTransferCode,	// 2:AGREX銀行振込請求書コード
				juchuCode,	// 3:受注コード
				string.Format("{0}-{1}", cust.顧客No, cust.得意先No),	// 4:顧客コード（顧客No-得意先No）
				detailData.請求先郵便番号数字のみ,	// 5:顧客郵便番号
				detailData.請求先住所,	// 6:顧客住所漢字（全角１７＋１７＋１６）
				"",	// 7:顧客住所カナ（省略）
				string.Format("お客様コード No.{0}", cust.得意先No),	// 8:顧客会社名（お客様コード）
				kokyakuBuka,	// 9:顧客部課名（得意先名１）
				kokyakuShimeiKanji,	// 10:顧客氏名漢字（得意先名２）
				"",	// 11:顧客氏名カナ（省略）
				detailData.請求先会社TEL,	// 12:顧客電話番号
				AgrexDefine.AccountNumber,	// 13:顧客指定口座番号（三菱UFJ）
				"",	// 14:請求書発行不要フラグ（なし）
				入金期限日.ToString("yyyyMMdd"),	// 15:入金期限日（西暦８桁）（「お支払期限日」）
				銀行振込請求期間終了日.ToString("yyyyMMdd"),	// 16:請求締め日（西暦８桁）
				"※お振込みの際は、振込人欄にお名前と共に６桁のお客様コードＮｏをご記入ください。",	// 17:請求書通信文
				合計請求額税込.ToString(),	// 18:請求金額（合計金額＋消費税）
				(合計請求額税込 - 消費税額).ToString(),	// 19:合計金額（消費税除く合計金額）
				消費税額.ToString(),	// 20:消費税
				"",	// 21:予備1
				"",	// 22:予備2
				"",	// 23:予備3
				"",	// 24:予備4
				""	// 25:予備5
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
			array[1] = AgrexDefine.BankTransferCode; // AGREX銀行振込請求書コード
			array[2] = juchuCode;   // 受注コード
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
		/// AGREX銀行振込請求書終了行の取得
		/// </summary>
		/// <param name="juchuCode">受注コード</param>
		/// <returns>AGREX銀行振込請求書終了行</returns>
		public string GetAgrexEndLine(string juchuCode)
		{
			return string.Format("9,{0},{1}", AgrexDefine.BankTransferCode, juchuCode);
		}
	}
}
