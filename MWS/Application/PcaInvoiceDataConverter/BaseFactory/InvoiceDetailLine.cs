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
		public const string SplitLine = "------------------------------------";

		/// <summary>
		/// 商品名：（消費税等）
		/// </summary>
		public const string GoodsNameTax = "（消費税等）";

		/// <summary>
		/// 商品名：伝票計
		/// </summary>
		public const string GoodsNameSubTotal = "　　　　　　　　　　　　　　伝票計";

		/// <summary>
		/// 商品名：今回ご利用料金合計
		/// </summary>
		public const string GoodsNameThisUseTotal = "今回ご利用料金合計";

		/// <summary>
		/// 商品名：（内 消費税等）
		/// </summary>
		public const string GoodsNameIncludeTax = "（内 消費税等）";

		/// <summary>
		/// 商品名：10%対象額
		/// </summary>
		public const string GoodsNameConsumptionTax = "10%対象額";

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
		/// DataRowの設定
		/// </summary>
		/// <param name="table">DataTable</param>
		/// <returns>DataRow</returns>
		public DataRow GetDataRow(DataRow row)
		{
			string dateStr = " ";
			if (売上日付.HasValue)
			{
				dateStr = 売上日付.Value.ToString("yyyy/MM/dd");
			}
			string denNoStr = " ";
			if (0 < 伝票No && DenNoMax != 伝票No)
			{
				denNoStr = 伝票No.ToString();
			}
			row["請求書No"] = 請求書No.ToString();
			row["枝番"] = 枝番.ToString();
			row["売上日付"] = dateStr;
			row["伝票No"] = denNoStr;
			row["商品名"] = 商品名;
			row["数量"] = 数量.ToString();
			row["単価"] = 単価.ToString();
			row["金額"] = 金額.ToString();
			row["行タイプ"] = 行タイプ.ToString();
			return row;
		}

		/// <summary>
		/// 明細行の取得
		/// </summary>
		/// <returns></returns>
		public string GetBillData()
		{
			string dateStr = " ";
			if (売上日付.HasValue)
			{
				dateStr = 売上日付.Value.ToString("yyyy/MM/dd");
			}
			string denNoStr = " ";
			if (0 < 伝票No)
			{
				denNoStr = 伝票No.ToString();
			}
			return string.Format("\"{0}\"\t\"{1}\"\t\"{2}\"\t\"{3}\"\t\"{4}\"\t\"{5}\"\t\"{6}\""
													, 請求書No, dateStr, denNoStr, 枝番, 商品名, 数量, 金額.CommaEdit());
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
			string dateStr = " ";
			if (売上日付.HasValue)
			{
				dateStr = 売上日付.Value.ToString("yyyy/MM/dd");
			}
			string denNoStr = " ";
			if (0 < 伝票No && DenNoMax != 伝票No)
			{
				denNoStr = 伝票No.ToString();
			}
			return string.Format("\"{0}\"\t\"{1}\"\t\"{2}\"\t\"{3}\"\t\"{4}\"\t\"{5}\""
												, 請求書No, dateStr, denNoStr, 枝番, 商品名, 金額.CommaEdit());
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
			string dateStr = " ";
			if (売上日付.HasValue)
			{
				dateStr = 売上日付.Value.ToString("yyyy/MM/dd");
			}
			string denNoStr = " ";
			if (0 < 伝票No && DenNoMax != 伝票No)
			{
				denNoStr = 伝票No.ToString();
			}
			return string.Format("\"{0}\"\t\"{1}\"\t\"{2}\"\t\"{3}\"\t\"{4}\""
												, 請求書No, dateStr, denNoStr, 枝番, 商品名);
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

		/// <summary>
		/// カラムの設定
		/// </summary>
		/// <returns>DataTable</returns>
		public static DataTable SetColumns()
		{
			DataTable table = new DataTable();
			table.Columns.Add("請求書No", typeof(string));
			table.Columns.Add("枝番", typeof(string));
			table.Columns.Add("売上日付", typeof(string));
			table.Columns.Add("伝票No", typeof(string));
			table.Columns.Add("商品名", typeof(string));
			table.Columns.Add("数量", typeof(string));
			table.Columns.Add("単価", typeof(string));
			table.Columns.Add("金額", typeof(string));
			table.Columns.Add("行タイプ", typeof(string));
			return table;
		}
	}
}
