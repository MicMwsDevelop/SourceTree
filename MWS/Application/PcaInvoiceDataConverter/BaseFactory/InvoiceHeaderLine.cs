using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using CommonLib.Common;
using System.IO;

namespace PcaInvoiceDataConverter.BaseFactory
{
	/// <summary>
	/// ヘッダ行作業
	/// </summary>
	public class InvoiceHeaderLine
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
		/// 明細行作業リスト
		/// </summary>
		public List<InvoiceDetailLine> DetailLineList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public InvoiceHeaderLine()
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
				"請求明細行開始番号",
				"明細作業行開始番号"
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
		public static DataTable GetHeaderLineDataTable(List<InvoiceHeaderLine> list)
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
			table.Columns.Add("請求明細行開始番号", typeof(string));
			table.Columns.Add("明細作業行開始番号", typeof(string));

			foreach (InvoiceHeaderLine header in list)
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
				row["請求明細行開始番号"] = "";
				row["明細作業行開始番号"] = "";
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
				table.Rows.Add(detail.GetDataRow(table));
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
		/// WEB請求書ヘッダファイル（invoice_header.tsv）の出力
		/// </summary>
		/// <param name="pathname">WEB請求書ヘッダファイル名</param>
		/// <param name="headerLineList">ヘッダ行リスト</param>
		public static void FileOut(string pathname, List<InvoiceHeaderLine> headerLineList)
		{
			FileStream fs = null;
			try
			{
				fs = new FileStream(pathname, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("shift_jis")))
				{
					sw.WriteLine(InvoiceHeaderLine.GetHeaderLineTitle());
					foreach (InvoiceHeaderLine header in headerLineList)
					{
						sw.WriteLine(header.GetHeaderLineData());
					}
					sw.WriteLine(InvoiceHeaderLine.GetHeaderLineFooter());
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				if (null != fs)
				{
					fs.Close();
				}
			}
		}
	}
}
