//
// Programs.cs
// 
// PCA請求データコンバータ プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/27 勝呂):新規作成
//
using ClosedXML.Excel;
using CommonLib.DB.SqlServer.PcaInvoiceDataConverter;
using PcaInvoiceDataConverter.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLib.BaseFactory.PcaInvoiceDataConverter;
using System.Data;
using PcaInvoiceDataConverter.BaseFactory;

namespace PcaInvoiceDataConverter
{
	internal static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string ProcName = "PCA請求データコンバータ";

		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string VersionStr = "Ver 1.00（2023/06/27）";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static PcaInvoiceDataConverterSettings gSettings = null;

		/// <summary>
		/// 顧客情報リスト
		/// </summary>
		public static List<CustomerInfo> Customers = null;

		/// <summary>
		/// 「基本データ」設定値
		/// </summary>
		public static BasicSheetData gBasicSheetData = null;

		/// <summary>
		/// PCA請求データコンバータ.xlsx
		/// </summary>
		public const string ExcelFilename = "PCA請求データコンバータ.xlsx";

		/// <summary>
		/// WW顧客データ.csv
		/// </summary>
		public const string CustomerDataFile = "WW顧客データ.csv";

		/// <summary>
		/// シート名「基本データ」
		/// </summary>
		public const string SheetNameBasicData = "基本データ";

		/// <summary>
		/// シート名「顧客情報」
		/// </summary>
		public const string SheetNameCustomer = "顧客情報";

		/// <summary>
		/// シート名「請求一覧」
		/// </summary>
		public const string SheetNameInvoiceHeader = "請求一覧";

		/// <summary>
		/// シート名「請求明細」
		/// </summary>
		public const string SheetNameInvoiceDetail = "請求明細";

		/// <summary>
		/// シート名「送信データ」
		/// </summary>
		public const string SheetNameSendData = "送信データ";

		/// <summary>
		/// シート名「口振不要」
		/// </summary>
		public const string SheetNameUnnecessary = "口振不要";

		/// <summary>
		/// シート名「口振不可」
		/// </summary>
		public const string SheetNameImpossible = "口振不可";

		/// <summary>
		/// シート名「口振請求なし」
		/// </summary>
		public const string SheetNameInvoiceNothing = "口振請求なし";

		/// <summary>
		/// シート名「銀行振込０円請求」
		/// </summary>
		public const string SheetNameBankTransferZeroInvoice = "銀行振込０円請求";

		/// <summary>
		/// シート名「銀行振込マイナス請求」
		/// </summary>
		public const string SheetNameBankTransferMinusInvoice = "銀行振込マイナス請求";

		/// <summary>
		/// シート名「ヘッダ行作業」
		/// </summary>
		public const string SheetNameHeaderLine = "ヘッダ行作業";

		/// <summary>
		/// シート名「明細行作業」
		/// </summary>
		public const string SheetNameDetailLine = "明細行作業";

		/// <summary>
		/// PCA請求データコンバータ.xlsx
		/// </summary>
		public static XLWorkbook PcaWorkbook = null;

		/// <summary>
		/// 「基本データ」シート
		/// </summary>
		public static IXLWorksheet WS基本データ = null;

		/// <summary>
		/// PCA請求データコンバータ.xlsxパス名の取得
		/// </summary>
		public static string ExcelPathname
		{
			get
			{
				return Path.Combine(Directory.GetCurrentDirectory(), ExcelFilename);
			}
		}

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// 環境設定の読込
			gSettings = PcaInvoiceDataConverterSettingsIF.GetSettings();
			gBasicSheetData = new BasicSheetData();

			Application.Run(new Forms.MainForm());
		}

		/// <summary>
		/// 顧客情報読込み
		/// </summary>
		public static void ReadCustomerInfo()
		{
			try
			{
				DataTable table = PcaInvoiceDataConverterGetIO.GetCustomerInfo(gSettings.ConnectJunp.ConnectionString);
				if (0 < table.Rows.Count)
				{
					if (IsExistWorksheet(SheetNameCustomer))
					{
						// シート「顧客情報」の削除
						PcaWorkbook.Worksheets.Delete(SheetNameCustomer);
					}
					// シート「顧客情報」を追加して、顧客情報の書き込み
					IXLWorksheet wsCust = PcaWorkbook.Worksheets.Add(table, SheetNameCustomer);

					// 表全体の列、カラムの幅を自動調整
					//wsCust.ColumnsUsed().AdjustToContents();

					// ワークブックの保存
					PcaWorkbook.Save();

					// 顧客情報の取得
					Customers = CustomerInfo.DataTableToList(table);

					// WW顧客データ.csvの出力
					string csvPathname = Path.Combine(Directory.GetCurrentDirectory(), CustomerDataFile);
					using (StreamWriter sw = new StreamWriter(csvPathname, false, Encoding.GetEncoding("Shift_JIS")))
					{
						// タイトル行出力
						sw.WriteLine(CustomerInfo.GetTitle());

						// データ出力
						List<CustomerInfo> list = CustomerInfo.DataTableToList(table);
						foreach (CustomerInfo data in list)
						{
							sw.WriteLine(data.GetData());
						}
					}
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 請求一覧データファイル読込み
		/// </summary>
		/// <param name="pathname">請求一覧データファイルパス名</param>
		/// <param name="InvoiceHeaderDataList">請求一覧データリスト</param>
		public static void ReadInvoiceHeaderDataFile(string pathname, List<InvoiceHeaderData> InvoiceHeaderDataList)
		{
			try
			{
				InvoiceHeaderDataList.Clear();

				// PCA請求一覧読込みファイル
				using (StreamReader sr = new StreamReader(pathname, Encoding.GetEncoding("shift_jis")))
				{
					// 「請求一覧」シートの作成
					IXLWorksheet wsList = AddWorksheet(SheetNameInvoiceHeader);
					int row = 1;
					while (!sr.EndOfStream)
					{
						string line = sr.ReadLine();
						string[] values = line.Split(',');
						for (int i = 0, j = 1; i < values.Length; i++, j++)
						{
							wsList.Cell(row, j).Value = values[i];
						}
						row++;
						if ("10" == values[3])
						{
							// 合計行以外
							InvoiceHeaderData invoiceHeader = new InvoiceHeaderData();
							invoiceHeader.SetData(line, values);
							InvoiceHeaderDataList.Add(invoiceHeader);

							// 請求一覧表に顧客情報を紐づけ
							invoiceHeader.Customer = Customers.Find(p => p.得意先No == invoiceHeader.得意先コード);
						}
						// 表全体の列、カラムの幅を自動調整
						//wsList.ColumnsUsed().AdjustToContents();
					}
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 請求明細データファイル読込み
		/// </summary>
		/// <param name="pathname">請求一覧データファイルパス名</param>
		/// <param name="InvoiceHeaderDataList">請求一覧データリスト</param>
		public static void ReadInvoiceDetailDataFile(string pathname, List<InvoiceHeaderData> InvoiceHeaderDataList)
		{
			try
			{
				// csvファイルを開く
				using (StreamReader sr = new StreamReader(pathname, Encoding.GetEncoding("shift_jis")))
				{
					List<InvoiceDetailData> invoiceDetailDataList = new List<InvoiceDetailData>();

					// 「請求明細」シートに出力
					IXLWorksheet wsDetail = AddWorksheet(SheetNameInvoiceDetail);
					int row = 1;
					while (!sr.EndOfStream)
					{
						string line = sr.ReadLine();
						string[] values = line.Split(',');
						for (int i = 0, j = 1; i < values.Length; i++, j++)
						{
							wsDetail.Cell(row, j).Value = values[i];
						}
						row++;

						InvoiceDetailData detailData = new InvoiceDetailData();
						detailData.SetData(values);
						invoiceDetailDataList.Add(detailData);
					}
					// 請求一覧表クラスに請求明細データリストをを紐づけ
					foreach (InvoiceHeaderData headerData in InvoiceHeaderDataList)
					{
						headerData.InvoiceDetailDataList = invoiceDetailDataList.FindAll(p => p.得意先コード == headerData.得意先コード);
					}
					// 表全体の列、カラムの幅を自動調整
					//wsDetail.ColumnsUsed().AdjustToContents();
				}
			}
			catch
			{
				throw;
			}
		}


		/// <summary>
		/// WorkSheetが存在するかどうか？
		/// </summary>
		/// <param name="sheetName">シート名</param>
		/// <returns>判定</returns>
		public static bool IsExistWorksheet(string sheetName)
		{
			var wss = PcaWorkbook.Worksheets.Where(x => x.Name == sheetName);
			return (0 < wss.Count()) ? true: false;
		}

		/// <summary>
		/// WorkSheetの追加
		/// </summary>
		/// <param name="sheetName">シート名</param>
		/// <returns>WorkSheet</returns>
		public static IXLWorksheet AddWorksheet(string sheetName)
		{
			if (IsExistWorksheet(sheetName))
			{
				PcaWorkbook.Worksheets.Delete(sheetName);
			}
			return PcaWorkbook.Worksheets.Add(sheetName);
		}

		/// <summary>
		/// WorkSheetの削除
		/// </summary>
		/// <param name="sheetName">シート名</param>
		public static void DeleteWorksheet(string sheetName)
		{
			if (IsExistWorksheet(sheetName))
			{
				PcaWorkbook.Worksheets.Delete(sheetName);
			}
		}

		/// <summary>
		/// 数値の取得
		/// </summary>
		/// <param name="value">設定値</param>
		/// <returns>数値</returns>
		public static double GetValueDouble(XLCellValue value)
		{
			if (value.IsNumber)
			{
				return value.GetNumber();
			}
			return 0;
		}

		/// <summary>
		/// 日付の取得
		/// </summary>
		/// <param name="value">設定値</param>
		/// <returns>日付</returns>
		public static DateTime? GetValueDateTime(XLCellValue value)
		{
			if (value.IsDateTime)
			{
				return value.GetDateTime();
			}
			return null;
		}
	}
}
