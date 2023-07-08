//
// InvoiceDetailLine.cs
// 
// 口座振替/銀行振込 明細行作業クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/27 勝呂):新規作成
//
using CommonLib.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PcaInvoiceDataConverter.BaseFactory
{
	/// <summary>
	/// 口座振替/銀行振込 明細行作業
	/// </summary>
	public class InvoiceDetailLine
	{
		/// <summary>
		/// 伝票No最大値
		/// </summary>
		public const int DenNoMax = 999999;

		/// <summary>
		/// 商品名：前回御請求額
		/// </summary>
		public const string GoodsNameLastBill = "前回御請求額";

		/// <summary>
		/// 商品名：御入金額
		/// </summary>
		public const string GoodsNamePayment = "御入金額";

		/// <summary>
		/// 商品名：繰越金額
		/// </summary>
		public const string GoodsNameCarryForword = "繰越金額";

		/// <summary>
		/// 区切り線
		/// </summary>
		public const string SplitLine = "------------------------------------ ";

		/// <summary>
		/// 商品名：（消費税等）
		/// </summary>
		public const string GoodsNameTax = "（消費税等）";

		/// <summary>
		/// 商品名：伝票計
		/// </summary>
		public const string GoodsNameSubTotal = "　　　　　　　　　　　　　　伝票計";

		/// <summary>
		/// 明細行
		/// </summary>
		public const short TypeBill = 1;

		/// <summary>
		/// 送料行
		/// </summary>
		public const short TypeShipping = 2;

		/// <summary>
		/// 消費税行
		/// </summary>
		public const short TypeTax = 3;

		/// <summary>
		/// 記事行
		/// </summary>
		public const short TypeComment = 4;

		public int 請求書No { get; set; }
		public int 枝番 { get; set; }
		public DateTime? 売上日付 { get; set; }
		public int 伝票No { get; set; }
		public string 商品名 { get; set; }
		public int 数量 { get; set; }
		public int 単価 { get; set; }
		public int 金額 { get; set; }
		public short 行タイプ { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public InvoiceDetailLine()
		{
			請求書No = 0;
			枝番 = 0;
			売上日付 = null;
			伝票No = 0;
			商品名 = string.Empty;
			数量 = 0;
			単価 = 0;
			金額 = 0;
			行タイプ = 0;
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
				"枝番",
				"売上日付",
				"伝票No",
				"商品名",
				"数量",
				"単価",
				"金額",
				"行タイプ"
			};
			return string.Join(",", result.ToArray());
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static DataColumn[] GetDataColumn()
		{
			DataColumn[] columns = new DataColumn[9];
			columns[0].ColumnName = "請求書No";
			columns[1].ColumnName = "枝番";
			columns[2].ColumnName = "売上日付";
			columns[3].ColumnName = "伝票No";
			columns[4].ColumnName = "商品名";
			columns[5].ColumnName = "数量";
			columns[6].ColumnName = "単価";
			columns[7].ColumnName = "金額";
			columns[8].ColumnName = "行タイプ";
			return columns;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table">DataTable</param>
		/// <returns></returns>
		public DataRow GetDataRow(DataTable table)
		{
			DataRow row = table.NewRow();
			row["請求書No"] = 請求書No;
			row["枝番"] = 枝番;
			row["売上日付"] = 売上日付.Value.ToDate().ToIntYMD(); ;
			row["伝票No"] = 伝票No;
			row["商品名"] = 商品名;
			row["数量"] = 数量;
			row["単価"] = 単価;
			row["金額"] = 金額;
			row["行タイプ"] = 行タイプ;
			return row;
		}

		/// <summary>
		/// 明細行の取得
		/// </summary>
		/// <returns></returns>
		public string GetBillData()
		{
			return string.Format("\"{0}\"\t\"{1}\"\t\"{2}\"\t\"{3}\"\t\"{4}\"\t\"{5}\"\t\"{6}\""
													, 請求書No, 売上日付.Value.ToString("yyyy/MM/dd"), 伝票No, 枝番, 商品名, 数量, 金額.CommaEdit());
		}

		/// <summary>
		/// invoice_detail_bill.tsvの取得（明細行）
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static List<string> GetBillDataList(List<InvoiceDetailLine> list)
		{
			List<string> result = new List<string>();
			foreach (InvoiceDetailLine detail in list)
			{
				if (TypeBill == detail.行タイプ || TypeShipping == detail.行タイプ)
				{
					result.Add(detail.GetBillData());
				}
			}
			return result;
		}

		/// <summary>
		/// 消費税行の取得
		/// </summary>
		/// <returns></returns>
		public string GetTaxData()
		{
			return string.Format("\"{0}\"\t\"{1}\"\t\"{2}\"\t\"{3}\"\t\"{4}\"\t\"{5}\""
												, 請求書No, 売上日付.Value.ToString("yyyy/MM/dd"), 伝票No, 枝番, 商品名, 金額.CommaEdit());
		}

		/// <summary>
		/// invoice_detail_tax.tsvの取得（消費税行）
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static List<string> GetTaxDataList(List<InvoiceDetailLine> list)
		{
			List<string> result = new List<string>();
			foreach (InvoiceDetailLine detail in list)
			{
				if (TypeTax == detail.行タイプ)
				{
					result.Add(detail.GetTaxData());
				}
			}
			return result;
		}

		/// <summary>
		/// 記事行の取得
		/// </summary>
		/// <returns></returns>
		public string GetCommentData()
		{
			return string.Format("\"{0}\"\t\"{1}\"\t\"{2}\"\t\"{3}\"\t\"{4}\""
												, 請求書No, 売上日付.Value.ToString("yyyy/MM/dd"), 伝票No, 枝番, 商品名);
		}

		/// <summary>
		/// invoice_detail_comment.tsvの取得（記事行）
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public static List<string> GetCommentDataList(List<InvoiceDetailLine> list)
		{
			List<string> result = new List<string>();
			foreach (InvoiceDetailLine detail in list)
			{
				if (TypeComment == detail.行タイプ)
				{
					result.Add(detail.GetCommentData());
				}
			}
			return result;
		}
	}
}
