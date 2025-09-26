//
// Programs.cs
// 
// PCA請求データコンバータ プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
///////////////////////////////////////////////////////////////////////////////////////////////////////
// 移植前修正履歴
// Ver.1.63(2023/07/27 勝呂):明細部冒頭の繰越金額の内訳の行の、「売上日」「伝票№」はブランクに変更
// Ver.1.63(2023/07/27 勝呂):AGREX請求書インボイス対応。明細部の末尾に「今回ご利用金額合計」、「（内消費税等）」、「10％対象額」、「（内消費税等）」の４行を追加
///////////////////////////////////////////////////////////////////////////////////////////////////////
// Ver2.00(2023/06/27 勝呂):新規作成
//
using ClosedXML.Excel;
using PcaInvoiceDataConverter.BaseFactory;
using PcaInvoiceDataConverter.Settings;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
		public const string VersionStr = "Ver 2.00（2025/09/11）";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static PcaInvoiceDataConverterSettings gSettings = null;

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
		/// シートフォント名
		/// </summary>
		public const string SheetNameFontName = "メイリオ";

		/// <summary>
		/// シートフォントサイズ
		/// </summary>
		public const int SheetNameFontSize = 9;

		/// <summary>
		/// 待ち時間３秒
		/// Excelの終了後、直後にExcelを再オープンすると、Excelが終了しておらず、オープンエラーとなる。よって、しばらく待機する必要がある
		/// </summary>
		public const int WaitTime = 3000;

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

			gBasicSheetData.PCA請求一覧10読込みファイル = gSettings.PCA請求一覧10読込みファイル;
			gBasicSheetData.APLUS送信ファイル出力フォルダ = gSettings.APLUS送信ファイル出力フォルダ;
			gBasicSheetData.WEB請求書番号基数 = gSettings.WEB請求書番号基数;
			gBasicSheetData.PCA請求明細10読込みファイル = gSettings.PCA請求明細10読込みファイル;
			gBasicSheetData.WEB請求書ファイル出力フォルダ = gSettings.WEB請求書ファイル出力フォルダ;
			gBasicSheetData.WEB請求書ヘッダファイル = gSettings.WEB請求書ヘッダファイル;
			gBasicSheetData.WEB請求書明細売上行ファイル = gSettings.WEB請求書明細売上行ファイル;
			gBasicSheetData.WEB請求書明細消費税行ファイル = gSettings.WEB請求書明細消費税行ファイル;
			gBasicSheetData.WEB請求書明細記事行ファイル = gSettings.WEB請求書明細記事行ファイル;
			gBasicSheetData.AGREX口振通知書ファイル出力フォルダ = gSettings.AGREX口振通知書ファイル出力フォルダ;
			gBasicSheetData.請求書番号基数 = gSettings.請求書番号基数;
			gBasicSheetData.PCA請求一覧11読込みファイル = gSettings.PCA請求一覧11読込みファイル;
			gBasicSheetData.PCA請求明細11読込みファイル = gSettings.PCA請求明細11読込みファイル;
			gBasicSheetData.AGREX請求書ファイル出力フォルダ = gSettings.AGREX請求書ファイル出力フォルダ;

			Application.Run(new Forms.MainForm());
		}

		/// <summary>
		/// ワークシートが存在するかどうか？
		/// </summary>
		/// <param name="sheetName">シート名</param>
		/// <returns>判定</returns>
		public static bool IsExistWorksheet(XLWorkbook book, string sheetName)
		{
			var wss = book.Worksheets.Where(x => x.Name == sheetName);
			return (0 < wss.Count()) ? true: false;
		}

		/// <summary>
		/// ワークシートの追加
		/// </summary>
		/// <param name="sheetName">シート名</param>
		/// <param name="table">DataTable</param>
		/// <returns>ワークシート</returns>
		public static IXLWorksheet AddWorksheet(XLWorkbook book, string sheetName, DataTable table = null)
		{
			if (IsExistWorksheet(book, sheetName))
			{
				book.Worksheets.Delete(sheetName);
			}
			IXLWorksheet sheet = null;
			if (null == table)
			{
				sheet = book.Worksheets.Add(sheetName);
			}
			else
			{
				sheet = book.Worksheets.Add(table, sheetName);
			}
			sheet.Style.Font.FontName = SheetNameFontName;
			sheet.Style.Font.FontSize = SheetNameFontSize;
			return sheet;
		}

		/// <summary>
		/// 数値の取得
		/// </summary>
		/// <param name="value">設定値</param>
		/// <returns>数値</returns>
		public static int GetValueDouble(XLCellValue value)
		{
			if (value.IsNumber)
			{
				return (int)value.GetNumber();
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

		/// <summary>
		/// 指定されたシートをDataTableに変換する
		/// </summary>
		/// <param name="book">ワークブック</param>
		/// <param name="shhetName">シート名</param>
		/// <returns>DataTable</returns>
		public static DataTable ConvertExcelToDataTable(XLWorkbook book, string shhetName)
		{
			IXLWorksheet sheet = book.Worksheet(shhetName);
			if (null != sheet)
			{
				DataTable dt = new DataTable();
				int lastRow = sheet.LastRowUsed().RowNumber();
				int lastCol = sheet.LastColumnUsed().ColumnNumber();
				for (int i = 0; i < lastRow; i++)
				{
					IXLRow row = sheet.Row(i + 1);
					if (0 == i)
					{
						// 最初の行をヘッダーとしてDataTableの列を定義する
						for (int j = 0; j < lastCol; j++)
						{
							dt.Columns.Add(row.Cell(j + 1).GetString(), typeof(string));
						}
					}
					else
					{
						// データ行
						DataRow newRow = dt.NewRow();
						for (int j = 0; j < lastCol; j++)
						{
							newRow[j] = row.Cell(j + 1).Value.ToString();
						}
						dt.Rows.Add(newRow);
					}
				}
				return dt;
			}
			return null;
		}
	}
}
