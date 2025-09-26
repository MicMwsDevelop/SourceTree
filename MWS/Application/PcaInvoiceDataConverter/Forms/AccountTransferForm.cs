//
// AccountTransferForm.cs
// 
// 口座振替データ作成画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/27 勝呂):新規作成
//
using ClosedXML.Excel;
using CommonLib.BaseFactory.PcaInvoiceDataConverter;
using CommonLib.Common;
using CommonLib.DB.SqlServer.PcaInvoiceDataConverter;
using PcaInvoiceDataConverter.BaseFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PcaInvoiceDataConverter.Forms
{
	public partial class AccountTransferForm : Form
	{
		/// <summary>
		/// Excelの起動プロセス
		/// </summary>
		private Process ProcessExcel = null;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public AccountTransferForm()
		{
			InitializeComponent();

			ProcessExcel = new Process();
			ProcessExcel.StartInfo.FileName = Program.ExcelPathname;
			ProcessExcel.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AccountTransferForm_Load(object sender, EventArgs e)
		{
			try
			{
				using (XLWorkbook book = new XLWorkbook(Program.ExcelPathname))
				{
					book.Style.Font.FontName = Program.SheetNameFontName;
					book.Style.Font.FontSize = Program.SheetNameFontSize;

					IXLWorksheet sheet基本データ = book.Worksheet(Program.SheetNameBasicData);

					/////////////////////////////////////////////////////////////////////
					// 「基本データ」 口座振替関連基本データ 初期値設定

					// 口座振替日=今月27日
					sheet基本データ.Cell(5, 3).Value = Program.gBasicSheetData.口座振替日;

					// PCA請求一覧読込みファイル
					sheet基本データ.Cell(6, 3).Value = Program.gBasicSheetData.PCA請求一覧10読込みファイル;

					// APLUS送信ファイル出力フォルダ
					sheet基本データ.Cell(7, 3).Value = Program.gBasicSheetData.APLUS送信ファイル出力フォルダ;

					// APLUS送信ファイル
					sheet基本データ.Cell(8, 3).Value = Program.gBasicSheetData.APLUS送信ファイル;


					/////////////////////////////////////////////////////////////////////
					// 「基本データ」 WEB請求書発行関連基本データ 初期値設定

					// 請求書番号基数
					sheet基本データ.Cell(16, 3).Value = Program.gBasicSheetData.WEB請求書番号基数;

					// 口座振替請求日=今月27日
					sheet基本データ.Cell(17, 3).Value = Program.gBasicSheetData.口座振替請求日;

					// 口座振替請求期間開始日=先月11日、口座振替請求期間終了日=今月10日
					sheet基本データ.Cell(18, 3).Value = Program.gBasicSheetData.口座振替請求期間開始日;
					sheet基本データ.Cell(18, 5).Value = Program.gBasicSheetData.口座振替請求期間終了日;

					// PCA請求明細読込みファイル
					sheet基本データ.Cell(19, 3).Value = Program.gBasicSheetData.PCA請求明細10読込みファイル;

					// WEB請求書ファイル出力フォルダ
					sheet基本データ.Cell(20, 3).Value = Program.gBasicSheetData.WEB請求書ファイル出力フォルダ;

					// WEB請求書ヘッダファイル
					sheet基本データ.Cell(21, 3).Value = Program.gBasicSheetData.WEB請求書ヘッダファイル;

					// WEB請求書明細売上行ファイル
					sheet基本データ.Cell(22, 3).Value = Program.gBasicSheetData.WEB請求書明細売上行ファイル;

					// WEB請求書明細消費税行ファイル
					sheet基本データ.Cell(23, 3).Value = Program.gBasicSheetData.WEB請求書明細消費税行ファイル;

					// WEB請求書明細記事行ファイル
					sheet基本データ.Cell(24, 3).Value = Program.gBasicSheetData.WEB請求書明細記事行ファイル;

					// AGREX口振通知書ファイル出力フォルダ
					sheet基本データ.Cell(25, 3).Value = Program.gBasicSheetData.AGREX口振通知書ファイル出力フォルダ;

					// AGREX口振通知書ファイル=本日
					sheet基本データ.Cell(26, 3).Value = Program.gBasicSheetData.AGREX口振通知書ファイル;

					// ワークブックの保存
					book.Save();

					// Excelの起動
					ProcessExcel.Start();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 顧客情報読込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonReadCustomerInfo_Click(object sender, EventArgs e)
		{
			try
			{
				// Excelを閉じる
				ProcessExcel.CloseMainWindow();

				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 顧客情報の読込
				DataTable dataTableCustomer = PcaInvoiceDataConverterGetIO.GetCustomerInfo(Program.gSettings.ConnectJunp.ConnectionString);
				if (0 == dataTableCustomer.Rows.Count)
				{
					MessageBox.Show("顧客情報の読込みに失敗しました。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				// 顧客情報の取得
				List<CustomerInfo> customerList = CustomerInfo.DataTableToList(dataTableCustomer);

				// WW顧客データ.csvの出力
				string pathname = Path.Combine(Directory.GetCurrentDirectory(), Program.CustomerDataFile);
				using (StreamWriter sw = new StreamWriter(pathname, false, Encoding.GetEncoding("Shift_JIS")))
				{
					// タイトル行出力
					sw.WriteLine(CustomerInfo.GetTitle(dataTableCustomer));

					// データ出力
					foreach (CustomerInfo customer in customerList)
					{
						sw.WriteLine(customer.GetData());
					}
				}
				using (XLWorkbook book = new XLWorkbook(Program.ExcelPathname))
				{
					// 「顧客情報」シートの出力
					Program.AddWorksheet(book, Program.SheetNameCustomer, dataTableCustomer);

					// ワークブックの保存
					book.Save();
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("顧客情報の読込みが終了しました。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Information);

				// Excelの起動
				ProcessExcel.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 請求一覧データ読込み（請求一覧10.txt）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonReadInvoiceHeaderData_Click(object sender, EventArgs e)
		{
			try
			{
				// Excelを閉じる
				ProcessExcel.CloseMainWindow();

				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				ProcessExcel.WaitForExit(Program.WaitTime);
				using (XLWorkbook book = new XLWorkbook(Program.ExcelPathname))
				{
					IXLWorksheet sheet基本データ = book.Worksheet(Program.SheetNameBasicData);

					// PCA請求一覧読込みファイル
					string pathname = sheet基本データ.Cell(6, 3).Value.GetText().Trim();
					if (0 == pathname.Length)
					{
						MessageBox.Show(" 「基本データ」 PCA請求一覧読込みファイルが設定されていません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					if (false == File.Exists(pathname))
					{
						MessageBox.Show(string.Format("{0}が存在しません。", pathname), Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					// 「顧客情報」シートの存在確認
					if (false == Program.IsExistWorksheet(book, Program.SheetNameCustomer))
					{
						MessageBox.Show("顧客情報を先に読込んでください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					Program.gBasicSheetData.PCA請求一覧10読込みファイル = pathname;
					Program.gBasicSheetData.口座振替請求一覧件数 = 0;
					Program.gBasicSheetData.口座振替請求一覧請求金額 = 0;
					Program.gBasicSheetData.口座振替不可件数 = 0;
					Program.gBasicSheetData.口座振替不可請求額 = 0;
					Program.gBasicSheetData.口座振替不要件数 = 0;
					Program.gBasicSheetData.口座振替不要請求額 = 0;

					// 「請求一覧10.txt」→「請求一覧」シートの作成
					IXLWorksheet sheet請求一覧 = Program.AddWorksheet(book, Program.SheetNameInvoiceHeader);
					List<InvoiceHeaderData> invoiceHeaderDataList = new List<InvoiceHeaderData>();
					using (StreamReader sr = new StreamReader(Program.gBasicSheetData.PCA請求一覧10読込みファイル, Encoding.GetEncoding("shift_jis")))
					{
						int rowIndex = 1;
						while (!sr.EndOfStream)
						{
							string line = sr.ReadLine();
							string[] values = line.Split(',');
							int columnIndex = 1;
							foreach (string str in values)
							{
								sheet請求一覧.Cell(rowIndex, columnIndex).Value = str;
								columnIndex++;
							}
							rowIndex++;
							if ("10" == values[3])
							{
								// 合計行以外
								InvoiceHeaderData invoiceHeader = new InvoiceHeaderData();
								invoiceHeader.SetData(line, values);
								invoiceHeaderDataList.Add(invoiceHeader);
							}
						}
					}
					// 請求一覧件数、請求一覧請求金額
					Program.gBasicSheetData.口座振替請求一覧件数 = invoiceHeaderDataList.Count;
					Program.gBasicSheetData.口座振替請求一覧請求金額 = invoiceHeaderDataList.Sum(p => p.請求残高);

					// 請求一覧件数
					sheet基本データ.Cell(9, 3).Value = Program.gBasicSheetData.口座振替請求一覧件数;

					// 請求一覧請求金額
					sheet基本データ.Cell(9, 6).Value = Program.gBasicSheetData.口座振替請求一覧請求金額;

					// ワークブックの保存
					book.Save();
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("請求一覧データ読込みが終了しました。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Information);

				// Excelの起動
				ProcessExcel.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 請求明細データ読込み（請求明細10.txt）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonReadInvoiceDetailData_Click(object sender, EventArgs e)
		{
			try
			{
				// Excelを閉じる
				ProcessExcel.CloseMainWindow();

				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				ProcessExcel.WaitForExit(Program.WaitTime);
				using (XLWorkbook book = new XLWorkbook(Program.ExcelPathname))
				{
					IXLWorksheet sheet基本データ = book.Worksheet(Program.SheetNameBasicData);

					// PCA請求明細読込みファイル
					string pathname = sheet基本データ.Cell(19, 3).Value.GetText().Trim();
					if (0 == pathname.Length)
					{
						MessageBox.Show(" 「基本データ」 PCA請求明細読込みファイルが設定されていません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					if (false == File.Exists(pathname))
					{
						MessageBox.Show(string.Format("{0}が存在しません。", pathname), Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					// 「顧客情報」シートの存在確認
					if (false == Program.IsExistWorksheet(book, Program.SheetNameCustomer))
					{
						MessageBox.Show("顧客情報を先に読込んでください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					// 「請求一覧」シートの存在確認
					if (false == Program.IsExistWorksheet(book, Program.SheetNameInvoiceHeader))
					{
						MessageBox.Show("請求一覧を先に読込んでください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					Program.gBasicSheetData.PCA請求明細10読込みファイル = pathname;

					// 「請求明細10.txt」→「請求明細」シートの出力
					IXLWorksheet sheet請求明細 = Program.AddWorksheet(book, Program.SheetNameInvoiceDetail);
					using (StreamReader sr = new StreamReader(Program.gBasicSheetData.PCA請求明細10読込みファイル, Encoding.GetEncoding("shift_jis")))
					{
						int rowIndex = 1;
						while (!sr.EndOfStream)
						{
							string line = sr.ReadLine();
							string[] values = line.Split(',');
							int columnIndex = 1;
							foreach (string str in values)
							{
								sheet請求明細.Cell(rowIndex, columnIndex).Value = str;
								columnIndex++;
							}
							rowIndex++;
						}
					}
					// ワークブックの保存
					book.Save();
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("請求明細データ読込みが終了しました。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Information);

				// Excelの起動
				ProcessExcel.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 口座振替データ作成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonMakeAccountTransfer_Click(object sender, EventArgs e)
		{
			try
			{
				// Excelを閉じる
				ProcessExcel.CloseMainWindow();

				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 現在時刻の取得
				DateTime start = DateTime.Now;

				DataTable dataTableCustomer = null;
				DataTable dataTableInvoiceHeader = null;
				DataTable dataTableInvoiceDetail = null;

				ProcessExcel.WaitForExit(Program.WaitTime);
				using (XLWorkbook book = new XLWorkbook(Program.ExcelPathname))
				{
					IXLWorksheet sheet基本データ = book.Worksheet(Program.SheetNameBasicData);
					if (0 == sheet基本データ.Cell(7, 3).Value.GetText().Trim().Length)
					{
						MessageBox.Show("APLUS送信ファイル出力フォルダを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					if (0 == sheet基本データ.Cell(8, 3).Value.GetText().Trim().Length)
					{
						MessageBox.Show("APLUS送信ファイルを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					// 「顧客情報」シートの読み込み
					dataTableCustomer = Program.ConvertExcelToDataTable(book, Program.SheetNameCustomer);
					if (null == dataTableCustomer || 0 == dataTableCustomer.Rows.Count)
					{
						MessageBox.Show("顧客情報を先に読込んでください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					// 「請求一覧」シートの読み込み
					dataTableInvoiceHeader = Program.ConvertExcelToDataTable(book, Program.SheetNameInvoiceHeader);
					if (null == dataTableInvoiceHeader || 0 == dataTableInvoiceHeader.Rows.Count)
					{
						MessageBox.Show("請求一覧を先に読込んでください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					// 「請求明細」シートの読み込み
					dataTableInvoiceDetail = Program.ConvertExcelToDataTable(book, Program.SheetNameInvoiceDetail);
					if (null == dataTableInvoiceDetail || 0 == dataTableInvoiceDetail.Rows.Count)
					{
						MessageBox.Show("請求明細を先に読込んでください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					Program.gBasicSheetData.APLUS送信ファイル出力フォルダ = sheet基本データ.Cell(7, 3).Value.GetText().Trim();
					Program.gBasicSheetData.APLUS送信ファイル = sheet基本データ.Cell(8, 3).Value.GetText().Trim();
					Program.gBasicSheetData.口座振替日 = Program.GetValueDateTime(sheet基本データ.Cell(5, 3).Value);

					// 顧客情報の取得
					List<CustomerInfo> customerList = CustomerInfo.DataTableToList(dataTableCustomer);

					// 請求一覧データ
					List<InvoiceHeaderData> invoiceList = new List<InvoiceHeaderData>();
					foreach (DataRow row1 in dataTableInvoiceHeader.Rows)
					{
						InvoiceHeaderData header = InvoiceHeaderData.DataTableToObject(row1);
						header.Customer = customerList.Find(p => p.得意先No == header.得意先コード);

						// 請求明細データ
						header.InvoiceDetailDataList = new List<InvoiceDetailData>();
						var rows = dataTableInvoiceDetail.AsEnumerable().Where(p => p.Field<string>("請求実績請求先コード") == header.得意先コード);
						foreach (DataRow row2 in rows)
						{
							header.InvoiceDetailDataList.Add(InvoiceDetailData.DataTableToObject(row2));
						}
						invoiceList.Add(header);
					}
					// 口座振替データ作成
					List<string> 送信データList, 口振不可List, 口振不要List;
					this.口座振替送信データ作成(invoiceList, out 送信データList, out 口振不可List, out 口振不要List);

					// 口座振替ファイル出力（SosinyyMMdd.txt）
					this.口座振替ファイル出力(送信データList);

					// 「送信データ」シートの出力
					IXLWorksheet sheet送信データ = Program.AddWorksheet(book, Program.SheetNameSendData);

					// 「送信データ」は固定長なのでフォントを"ＭＳ ゴシック"に設定する
					IXLRange range = sheet送信データ.Range("A1");
					range.Style.Font.FontName = "ＭＳ ゴシック";

					int rowIndex = 1;
					foreach (string line in 送信データList)
					{
						sheet送信データ.Cell(rowIndex, 1).Value = line;
						rowIndex++;
					}
					// 「口振不可」シートの出力
					IXLWorksheet sheet口振不可 = Program.AddWorksheet(book, Program.SheetNameImpossible);
					rowIndex = 1;
					foreach (string line in 口振不可List)
					{
						string[] values = line.Split(',');
						int columnIndex = 1;
						foreach (string str in values)
						{
							sheet口振不可.Cell(rowIndex, columnIndex).Value = str;
							columnIndex++;
						}
						rowIndex++;
					}
					// 「口振不要」シートの出力
					IXLWorksheet sheet口振不要 = Program.AddWorksheet(book, Program.SheetNameUnnecessary);
					rowIndex = 1;
					foreach (string line in 口振不要List)
					{
						string[] values = line.Split(',');
						int columnIndex = 1;
						foreach (string str in values)
						{
							sheet口振不要.Cell(rowIndex, columnIndex).Value = str;
							columnIndex++;
						}
						rowIndex++;
					}
					// 「基本データ」 口座振替関連基本データ
					sheet基本データ.Cell(10, 3).Value = Program.gBasicSheetData.口座振替不可件数;
					sheet基本データ.Cell(10, 6).Value = Program.gBasicSheetData.口座振替不可請求額;
					sheet基本データ.Cell(11, 3).Value = Program.gBasicSheetData.口座振替不要件数;
					sheet基本データ.Cell(11, 6).Value = Program.gBasicSheetData.口座振替不要請求額;

					// ワークブックの保存
					book.Save();
				}
				// 終了時刻の取得
				DateTime end = DateTime.Now;

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				TimeSpan sp = end - start;
				string msg = string.Format("口座振替データを作成し、口座振替ファイル\r\n"
															+ " {0} に {1} を出力しました。\r\n"
															+ "経過時間：{2}秒"
															, Program.gBasicSheetData.APLUS送信ファイル出力フォルダ
															, Program.gBasicSheetData.APLUS送信ファイル
															, Math.Floor(sp.TotalSeconds));
				MessageBox.Show(msg, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Information);

				// Excelの起動
				ProcessExcel.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 請求書データ作成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonMakeInvoice_Click(object sender, EventArgs e)
		{
			try
			{
				// Excelを閉じる
				ProcessExcel.CloseMainWindow();

				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 現在時刻の取得
				DateTime start = DateTime.Now;

				DataTable dataTableCustomer = null;
				DataTable dataTableInvoiceHeader = null;
				DataTable dataTableInvoiceDetail = null;

				ProcessExcel.WaitForExit(Program.WaitTime);
				using (XLWorkbook book = new XLWorkbook(Program.ExcelPathname))
				{
					// 「基本データ」
					IXLWorksheet sheet基本データ = book.Worksheet(Program.SheetNameBasicData);

					if (0 == sheet基本データ.Cell(20, 3).Value.GetText().Trim().Length)
					{
						MessageBox.Show("WEB請求書ファイル出力フォルダを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					if (0 == sheet基本データ.Cell(21, 3).Value.GetText().Trim().Length)
					{
						MessageBox.Show("WEB請求書ヘッダファイルを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					if (0 == sheet基本データ.Cell(22, 3).Value.GetText().Trim().Length)
					{
						MessageBox.Show("WEB請求書明細売上行ファイルを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					if (0 == sheet基本データ.Cell(23, 3).Value.GetText().Trim().Length)
					{
						MessageBox.Show("WEB請求書明細消費税行ファイルを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					if (0 == sheet基本データ.Cell(24, 3).Value.GetText().Trim().Length)
					{
						MessageBox.Show("WEB請求書明細記事行ファイルを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					if (0 == sheet基本データ.Cell(25, 3).Value.GetText().Trim().Length)
					{
						MessageBox.Show("AGREX口振通知書ファイル出力フォルダを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					if (0 == sheet基本データ.Cell(26, 3).Value.GetText().Trim().Length)
					{
						MessageBox.Show("AGREX口振通知書ファイルを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					// 「顧客情報」シートの読み込み
					dataTableCustomer = Program.ConvertExcelToDataTable(book, Program.SheetNameCustomer);
					if (null == dataTableCustomer || 0 == dataTableCustomer.Rows.Count)
					{
						MessageBox.Show("顧客情報を先に読込んでください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					// 「請求一覧」シートの読み込み
					dataTableInvoiceHeader = Program.ConvertExcelToDataTable(book, Program.SheetNameInvoiceHeader);
					if (null == dataTableInvoiceHeader || 0 == dataTableInvoiceHeader.Rows.Count)
					{
						MessageBox.Show("請求一覧を先に読込んでください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					// 最終行は【期間合計】の行で、必要ないので削除する
					dataTableInvoiceHeader.Rows.RemoveAt(dataTableInvoiceHeader.Rows.Count - 1);

					// 「請求明細」シートの読み込み
					dataTableInvoiceDetail = Program.ConvertExcelToDataTable(book, Program.SheetNameInvoiceDetail);
					if (null == dataTableInvoiceDetail || 0 == dataTableInvoiceDetail.Rows.Count)
					{
						MessageBox.Show("請求明細を先に読込んでください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					Program.gBasicSheetData.WEB請求書番号基数 = Program.GetValueDouble(sheet基本データ.Cell(16, 3).Value) + 1;
					Program.gBasicSheetData.口座振替請求日 = Program.GetValueDateTime(sheet基本データ.Cell(17, 3).Value);
					Program.gBasicSheetData.口座振替請求期間開始日 = Program.GetValueDateTime(sheet基本データ.Cell(18, 3).Value);
					Program.gBasicSheetData.口座振替請求期間終了日 = Program.GetValueDateTime(sheet基本データ.Cell(18, 5).Value);
					Program.gBasicSheetData.WEB請求書ファイル出力フォルダ = sheet基本データ.Cell(20, 3).Value.GetText().Trim();
					Program.gBasicSheetData.WEB請求書ヘッダファイル = sheet基本データ.Cell(21, 3).Value.GetText().Trim();
					Program.gBasicSheetData.WEB請求書明細売上行ファイル = sheet基本データ.Cell(22, 3).Value.GetText().Trim();
					Program.gBasicSheetData.WEB請求書明細消費税行ファイル = sheet基本データ.Cell(23, 3).Value.GetText().Trim();
					Program.gBasicSheetData.WEB請求書明細記事行ファイル = sheet基本データ.Cell(24, 3).Value.GetText().Trim();
					Program.gBasicSheetData.AGREX口振通知書ファイル出力フォルダ = sheet基本データ.Cell(25, 3).Value.GetText().Trim();
					Program.gBasicSheetData.AGREX口振通知書ファイル = sheet基本データ.Cell(26, 3).Value.GetText().Trim();

					// 顧客情報の取得
					List<CustomerInfo> customerList = CustomerInfo.DataTableToList(dataTableCustomer);

					// 請求一覧データ
					List<InvoiceHeaderData> invoiceList = new List<InvoiceHeaderData>();
					foreach (DataRow row1 in dataTableInvoiceHeader.Rows)
					{
						InvoiceHeaderData header = InvoiceHeaderData.DataTableToObject(row1);
						header.Customer = customerList.Find(p => p.得意先No == header.得意先コード);

						// 請求明細データ
						header.InvoiceDetailDataList = new List<InvoiceDetailData>();
						var rows = dataTableInvoiceDetail.AsEnumerable().Where(p => p.Field<string>("請求実績請求先コード") == header.得意先コード);
						foreach (DataRow row2 in rows)
						{
							header.InvoiceDetailDataList.Add(InvoiceDetailData.DataTableToObject(row2));
						}
						invoiceList.Add(header);
					}
					// 口座振替ヘッダ行および明細行の作成
					List<string> 口振請求なしList;
					List<AccountTransferHeaderLine> headerLineList = this.MakeHeaderLineAndDeatilLine(invoiceList, out 口振請求なしList);

					// 「口振請求なし」シートの出力
					IXLWorksheet sheet口振請求なし = Program.AddWorksheet(book, Program.SheetNameInvoiceNothing);
					int row = 1;
					foreach (string line in 口振請求なしList)
					{
						string[] values = line.Split(',');
						int column = 1;
						foreach (string str in values)
						{
							sheet口振請求なし.Cell(row, column).Value = str;
							column++;
						}
						row++;
					}
					// WEB請求書ファイル出力フォルダの取得
					if (false == Directory.Exists(Program.gBasicSheetData.WEB請求書ファイル出力フォルダ))
					{
						Directory.CreateDirectory(Program.gBasicSheetData.WEB請求書ファイル出力フォルダ);
					}
					string webFolder = Path.Combine(Program.gBasicSheetData.WEB請求書ファイル出力フォルダ, DateTime.Today.ToString("yyyyMMdd"));
					if (false == Directory.Exists(webFolder))
					{
						Directory.CreateDirectory(webFolder);
					}
					// WEB請求書ヘッダファイル（invoice_header.tsv）の出力
					this.FileOut(Path.Combine(webFolder, Program.gBasicSheetData.WEB請求書ヘッダファイル), headerLineList);

					// WEB請求書明細売上行ファイル（invoice_detail_bill.tsv）の出力
					this.FileOutBill(Path.Combine(webFolder, Program.gBasicSheetData.WEB請求書明細売上行ファイル), headerLineList);

					// WEB請求書明細消費税行ファイル（invoice_detail_tax.tsv）の出力
					this.FileOutTax(Path.Combine(webFolder, Program.gBasicSheetData.WEB請求書明細消費税行ファイル), headerLineList);

					// WEB請求書明細記事行ファイル（invoice_detail_comment.tsv）の出力
					this.FileOutComment(Path.Combine(webFolder, Program.gBasicSheetData.WEB請求書明細記事行ファイル), headerLineList);

					// AGREX口振通知書ファイル（132001yyyyMMddF.csv）の出力
					if (false == Directory.Exists(Program.gBasicSheetData.AGREX口振通知書ファイル出力フォルダ))
					{
						Directory.CreateDirectory(Program.gBasicSheetData.AGREX口振通知書ファイル出力フォルダ);
					}
					this.FileOutAgrexFile(Path.Combine(Program.gBasicSheetData.AGREX口振通知書ファイル出力フォルダ, Program.gBasicSheetData.AGREX口振通知書ファイル), headerLineList, invoiceList);

					// 「ヘッダ行作業」シートの出力
					DataTable dataTableHeaderLine = AccountTransferHeaderLine.GetHeaderLineDataTable(headerLineList);
					Program.AddWorksheet(book, Program.SheetNameHeaderLine, dataTableHeaderLine);

					// 「明細行作業」シートの出力
					DataTable dataTableDetailLine = InvoiceDetailLine.SetColumns();
					foreach (AccountTransferHeaderLine headerLine in headerLineList)
					{
						foreach (InvoiceDetailLine detailLine in headerLine.DetailLineList)
						{
							dataTableDetailLine.Rows.Add(detailLine.GetDataRow(dataTableDetailLine.NewRow()));
						}
					}
					Program.AddWorksheet(book, Program.SheetNameDetailLine, dataTableDetailLine);

					// 「基本データ」 WEB請求書発行関連基本データ
					sheet基本データ.Cell(16, 3).Value = Program.gBasicSheetData.WEB請求書番号基数;
					sheet基本データ.Cell(27, 3).Value = Program.gBasicSheetData.WEB請求書件数;
					sheet基本データ.Cell(27, 6).Value = Program.gBasicSheetData.AGREX口振通知書件数;
					sheet基本データ.Cell(28, 3).Value = Program.gBasicSheetData.口振請求なし件数;
					sheet基本データ.Cell(29, 3).Value = Program.gBasicSheetData.請求金額あり件数;
					sheet基本データ.Cell(29, 6).Value = Program.gBasicSheetData.WEB請求書請求金額;

					// ワークブックの保存
					book.Save();
				}
				// 終了時刻の取得
				DateTime end = DateTime.Now;

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				TimeSpan sp = end - start;
				string msg = string.Format("請求書送信データを作成し。\r\n"
															+ " 1.WEB請求書\r\n"
															+ "   {0} に、\r\n"
															+ "   {1}\r\n"
															+ "   {2}\r\n"
															+ "   {3}\r\n"
															+ "   {4}\r\n"
															+ " 2.口座振替通知書\r\n"
															+ " {5} に {6} を出力しました。\r\n"
															+ "経過時間：{7}秒"
															, Program.gBasicSheetData.WEB請求書ファイル出力フォルダ
															, Program.gBasicSheetData.WEB請求書ヘッダファイル
															, Program.gBasicSheetData.WEB請求書明細売上行ファイル
															, Program.gBasicSheetData.WEB請求書明細消費税行ファイル
															, Program.gBasicSheetData.WEB請求書明細記事行ファイル
															, Program.gBasicSheetData.AGREX口振通知書ファイル出力フォルダ
															, Program.gBasicSheetData.AGREX口振通知書ファイル
															, Math.Floor(sp.TotalSeconds));
				MessageBox.Show(msg, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Information);

				// Excelの起動
				ProcessExcel.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 口座振替送信データ作成
		/// </summary>
		/// <param name="book">ワークブック</param>
		private void 口座振替送信データ作成(List<InvoiceHeaderData> invoiceList, out List<string> 送信データList, out List<string> 口振不可List, out List<string> 口振不要List)
		{
			送信データList = new List<string>();
			口振不可List = new List<string>();
			口振不要List = new List<string>();

			// 「口振不可」「口振不要」の１行目に「請求一覧」のE:Mのタイトルを設定
			// 得意先コード、得意先名１、得意先名２、前回請求額、入金額、繰越金額、税込売上高、請求残高、回収予定日
			string record = string.Join(",", InvoiceHeaderData.GetInvoiceNothingTitle());
			口振不可List.Add(record);
			口振不要List.Add(record);

			// 送信データシートの１行目にヘッダーレコード（送信ヘッダ）を記録
			record = AgrexDefine.AplusHeaderRecord(Program.gBasicSheetData.口座振替日.Value);
			送信データList.Add(record);       // 送信データリストに追加

			// 送信データシートの２行目以降にデータレコード（振替データ）を記録
			Program.gBasicSheetData.口座振替不可件数 = 0;
			Program.gBasicSheetData.口座振替不可請求額 = 0;
			Program.gBasicSheetData.口座振替不要件数 = 0;
			Program.gBasicSheetData.口座振替不要請求額 = 0;

			foreach (InvoiceHeaderData headerData in invoiceList)
			{
				if (10 == headerData.データ区分)
				{
					if (0 < headerData.請求残高)
					{
						if (null != headerData.Customer)
						{
							if (20 == headerData.Customer.APLUSコード.Length)
							{
								// 「送信データ」シートに追記
								record = AgrexDefine.AplusSendDataRecord(headerData.請求残高, headerData.Customer);
								送信データList.Add(record);   // 送信データリストに追加
							}
							else
							{
								// 口座情報がないので「口振不可」シートに追記
								record = string.Join(",", headerData.GetInvoiceNothingRecord());
								口振不可List.Add(record);

								Program.gBasicSheetData.口座振替不可件数++;
								Program.gBasicSheetData.口座振替不可請求額 += headerData.請求残高;
							}
						}
					}
					else
					{
						// 請求金額が０以下なので「口振不要」シートに追記
						record = string.Join(",", headerData.GetInvoiceNothingRecord());
						口振不要List.Add(record);

						Program.gBasicSheetData.口座振替不要件数++;
						Program.gBasicSheetData.口座振替不要請求額 += headerData.請求残高;
					}
				}
			}
			// 送信データシートにトレーラレコード（合計データ）を記録
			record = AgrexDefine.AplusTotalRecord(Program.gBasicSheetData.口座振替請求件数, Program.gBasicSheetData.口座振替請求金額);
			送信データList.Add(record);   // 送信データリストに追加

			// 送信データシートにエンドレコード（終端データ）を記録
			record = AgrexDefine.AplusEndRecord();
			送信データList.Add(record);   // 送信データリストに追加
		}

		/// <summary>
		/// 口座振替ファイル出力（SosinyyMMdd.txt）
		/// </summary>
		private void 口座振替ファイル出力(List<string> 送信データList)
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), Program.gBasicSheetData.APLUS送信ファイル);
			FileStream fs = null;
			try
			{
				fs = new FileStream(pathname, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("shift_jis")))
				{
					foreach (string line in 送信データList)
					{
						sw.WriteLine(line);
					}
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
			// APLUS送信ファイル出力フォルダ
			if (false == Directory.Exists(Program.gBasicSheetData.APLUS送信ファイル出力フォルダ))
			{
				// APLUS送信ファイル出力フォルダの作成
				Directory.CreateDirectory(Program.gBasicSheetData.APLUS送信ファイル出力フォルダ);
			}
			// APLUS送信ファイルをAPLUS送信ファイル出力フォルダにコピー
			File.Copy(pathname, Path.Combine(Program.gBasicSheetData.APLUS送信ファイル出力フォルダ, Program.gBasicSheetData.APLUS送信ファイル), true);
		}

		/// <summary>
		/// WEB請求書ヘッダファイル（invoice_header.tsv）の出力
		/// </summary>
		/// <param name="pathname">WEB請求書ヘッダファイル名</param>
		/// <param name="headerLineList">ヘッダ行リスト</param>
		private void FileOut(string pathname, List<AccountTransferHeaderLine> headerLineList)
		{
			FileStream fs = null;
			try
			{
				fs = new FileStream(pathname, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("shift_jis")))
				{
					sw.WriteLine(AccountTransferHeaderLine.GetHeaderLineTitle());
					foreach (AccountTransferHeaderLine header in headerLineList)
					{
						sw.WriteLine(header.GetHeaderLineData());
					}
					sw.WriteLine(AccountTransferHeaderLine.GetHeaderLineFooter());
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

		/// <summary>
		/// WEB請求書明細売上行ファイル（invoice_detail_bill.tsv）の出力
		/// </summary>
		/// <param name="pathname">WEB請求書明細売上行ファイル名</param>
		/// <param name="headerLineList">ヘッダ行リスト</param>
		private void FileOutBill(string pathname, List<AccountTransferHeaderLine> headerLineList)
		{
			FileStream fs = null;
			try
			{
				fs = new FileStream(pathname, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("shift_jis")))
				{
					foreach (AccountTransferHeaderLine header in headerLineList)
					{
						List<string> billList = InvoiceDetailLine.GetBillDataList(header.DetailLineList);
						foreach (string line in billList)
						{
							sw.WriteLine(line);
						}
					}
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

		/// <summary>
		/// 口座振替ヘッダ行および明細行の作成
		/// </summary>
		/// <returns>口座振替ヘッダ行作業リスト</returns>
		private List<AccountTransferHeaderLine> MakeHeaderLineAndDeatilLine(List<InvoiceHeaderData> invoiceList, out List<string> 口振請求なしList)
		{
			// 「口振請求なし」シートの作成
			口振請求なしList = new List<string>();

			// 「口振請求なし」シートにタイトル行を設定
			string record = string.Join(",", InvoiceHeaderData.GetInvoiceNothingTitle());
			口振請求なしList.Add(record);

			Program.gBasicSheetData.WEB請求書件数 = invoiceList.Count;
			Program.gBasicSheetData.口振請求なし件数 = 0;
			Program.gBasicSheetData.AGREX口振通知書件数 = 0;
			Program.gBasicSheetData.請求金額あり件数 = 0;
			Program.gBasicSheetData.WEB請求書請求金額 = 0;

			List<AccountTransferHeaderLine> headerLineList = new List<AccountTransferHeaderLine>();
			foreach (InvoiceHeaderData headerData in invoiceList)
			{
				if (0 < headerData.請求残高)
				{
					Program.gBasicSheetData.請求金額あり件数++;
					Program.gBasicSheetData.WEB請求書請求金額 += headerData.請求残高;
					if (headerData.Customer.IsAGREX口振通知書())
					{
						Program.gBasicSheetData.AGREX口振通知書件数++;
					}
					////////////////////////////////////
					// 「ヘッダ行作業」作成

					AccountTransferHeaderLine headerLine = new AccountTransferHeaderLine();
					headerLine.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
					headerLine.顧客ID = headerData.Customer.顧客No;
					headerLine.得意先No = headerData.得意先コード;
					headerLine.請求日付 = Program.gBasicSheetData.口座振替請求日;
					headerLine.合計請求額税込 = headerData.請求残高;
					headerLine.消費税額 = headerData.InvoiceDetailDataList[0].期間外税額;
					headerLine.紙請求書 = headerData.Customer.IsAGREX口振通知書();
					headerLineList.Add(headerLine);

					////////////////////////////////////
					// 「明細行作業」作成

					// Ver.1.63(2023/07/27 勝呂):明細部冒頭の繰越金額の内訳の行の、「売上日」「伝票№」はブランクに変更
					// 前回御請求額
					InvoiceDetailLine line1 = new InvoiceDetailLine();
					line1.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
					line1.枝番 = 1;
					line1.売上日付 = null;
					line1.伝票No = InvoiceDetailLine.DenNoMax;
					line1.商品名 = InvoiceDetailLine.GoodsNameLastBill;
					line1.金額 = headerData.前回請求額;
					line1.行タイプ = InvoiceDetailLine.TypeTax;
					headerLine.DetailLineList.Add(line1);

					// 御入金額
					InvoiceDetailLine line2 = new InvoiceDetailLine();
					line2.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
					line2.枝番 = 2;
					line2.売上日付 = null;
					line2.伝票No = InvoiceDetailLine.DenNoMax;
					line2.商品名 = InvoiceDetailLine.GoodsNamePayment;
					line2.金額 = headerData.入金額;
					line2.行タイプ = InvoiceDetailLine.TypeTax;
					headerLine.DetailLineList.Add(line2);

					// 繰越金額
					InvoiceDetailLine line3 = new InvoiceDetailLine();
					line3.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
					line3.枝番 = 3;
					line3.売上日付 = null;
					line3.伝票No = InvoiceDetailLine.DenNoMax;
					line3.商品名 = InvoiceDetailLine.GoodsNameCarryForword;
					line3.金額 = headerData.繰越金額;
					line3.行タイプ = InvoiceDetailLine.TypeTax;
					headerLine.DetailLineList.Add(line3);

					// ------------------------------------
					InvoiceDetailLine line4 = new InvoiceDetailLine();
					line4.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
					line4.枝番 = 4;
					line4.売上日付 = null;
					line4.伝票No = InvoiceDetailLine.DenNoMax;
					line4.商品名 = InvoiceDetailLine.SplitLine;
					line4.行タイプ = InvoiceDetailLine.TypeComment;
					headerLine.DetailLineList.Add(line4);

					if (0 < headerData.InvoiceDetailDataList[0].期間売上額 && 0 < headerData.InvoiceDetailDataList[0].伝票No)
					{
						// 期間売上額が０円以上 && 伝票Noが存在する時のみ売上行を出力
						int 枝番 = 5;

						// 伝票No毎に（消費税等）行、摘要名行、伝票計行の３行を出力
						var queryDen = headerData.InvoiceDetailDataList.GroupBy(p => p.伝票No);
						foreach (var denNo in queryDen)
						{
							// 伝票Noが同じ請求明細のみ抽出
							List<InvoiceDetailData> detailDataList = headerData.InvoiceDetailDataList.FindAll(p => p.伝票No == denNo.Key);
							foreach (InvoiceDetailData detailData in detailDataList)
							{
								// 請求明細行
								InvoiceDetailLine line = new InvoiceDetailLine();
								line.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
								line.枝番 = 枝番;
								line.売上日付 = detailData.売上日;
								line.伝票No = detailData.伝票No;
								line.商品名 = detailData.商品名;
								line.単価 = detailData.単価;
								line.金額 = detailData.売上金額;
								if (detailData.IsShipping)
								{
									line.行タイプ = InvoiceDetailLine.TypeShipping;
									line.数量 = 1;
								}
								else if (detailData.IsArraivalDate || detailData.IsComment)
								{
									line.行タイプ = InvoiceDetailLine.TypeComment;
									line.数量 = detailData.数量;
								}
								else
								{
									line.行タイプ = InvoiceDetailLine.TypeBill;
									line.数量 = detailData.数量;
								}
								headerLine.DetailLineList.Add(line);
								枝番++;
							}
							// （消費税等）行
							InvoiceDetailLine line5 = new InvoiceDetailLine();
							line5.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
							line5.枝番 = 枝番;
							line5.売上日付 = detailDataList[0].売上日;
							line5.伝票No = detailDataList[0].伝票No;
							line5.商品名 = InvoiceDetailLine.GoodsNameTax;
							line5.金額 = detailDataList[0].外税合計;
							line5.行タイプ = InvoiceDetailLine.TypeTax;
							headerLine.DetailLineList.Add(line5);
							枝番++;

							// 摘要名行
							InvoiceDetailLine line6 = new InvoiceDetailLine();
							line6.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
							line6.枝番 = 枝番;
							line6.売上日付 = detailDataList[0].売上日;
							line6.伝票No = detailDataList[0].伝票No;
							line6.商品名 = detailDataList[0].摘要名文字列();
							line6.行タイプ = InvoiceDetailLine.TypeComment;
							headerLine.DetailLineList.Add(line6);
							枝番++;

							// 伝票計行
							InvoiceDetailLine line7 = new InvoiceDetailLine();
							line7.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
							line7.枝番 = 枝番;
							line7.売上日付 = detailDataList[0].売上日;
							line7.伝票No = detailDataList[0].伝票No;
							line7.商品名 = InvoiceDetailLine.GoodsNameSubTotal;
							line7.金額 = detailDataList[0].売上合計;
							line7.行タイプ = InvoiceDetailLine.TypeTax;
							headerLine.DetailLineList.Add(line7);
							枝番++;
						}
						// Ver.1.63(2023/07/27 勝呂):AGREX請求書インボイス対応。明細部の末尾に「今回ご利用金額合計」、「（内消費税等）」、「10％対象額」、「（内消費税等）」の４行を追加
						// 今回ご利用料金合計
						InvoiceDetailLine line8 = new InvoiceDetailLine();
						line8.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
						line8.枝番 = 枝番;
						line8.売上日付 = null;
						line8.伝票No = InvoiceDetailLine.DenNoMax;
						line8.商品名 = InvoiceDetailLine.GoodsNameThisUseTotal;
						line8.金額 = headerLine.合計請求額税込;  // ヘッダ行作業：合計請求額（税込）
						line8.行タイプ = InvoiceDetailLine.TypeTax;
						headerLine.DetailLineList.Add(line8);
						枝番++;

						// （内 消費税等）
						InvoiceDetailLine line9 = new InvoiceDetailLine();
						line9.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
						line9.枝番 = 枝番;
						line9.売上日付 = null;
						line9.伝票No = InvoiceDetailLine.DenNoMax;
						line9.商品名 = InvoiceDetailLine.GoodsNameIncludeTax;
						line9.金額 = headerLine.消費税額;  // ヘッダ行作業：消費税額
						line9.行タイプ = InvoiceDetailLine.TypeTax;
						headerLine.DetailLineList.Add(line9);
						枝番++;

						// 10%対象額
						InvoiceDetailLine line10 = new InvoiceDetailLine();
						line10.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
						line10.枝番 = 枝番;
						line10.売上日付 = null;
						line10.伝票No = InvoiceDetailLine.DenNoMax;
						line10.商品名 = InvoiceDetailLine.GoodsNameConsumptionTax;
						line10.金額 = headerLine.合計請求額税込;  // ヘッダ行作業：合計請求額（税込）
						line10.行タイプ = InvoiceDetailLine.TypeTax;
						headerLine.DetailLineList.Add(line10);
						枝番++;

						// （内 消費税等）
						InvoiceDetailLine line11 = new InvoiceDetailLine();
						line11.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
						line11.枝番 = 枝番;
						line11.売上日付 = null;
						line11.伝票No = InvoiceDetailLine.DenNoMax;
						line11.商品名 = InvoiceDetailLine.GoodsNameIncludeTax;
						line11.金額 = headerLine.消費税額;  // ヘッダ行作業：消費税額
						line11.行タイプ = InvoiceDetailLine.TypeTax;
						headerLine.DetailLineList.Add(line11);
						枝番++;
					}
					// ヘッダ行に各行数を設定
					headerLine.明細行数 = headerLine.GetInvoiceDetailBillCount();
					headerLine.消費税行数 = headerLine.GetInvoiceDetailTaxCount();
					headerLine.記事行数 = headerLine.GetInvoiceDetailCommentCount();

					Program.gBasicSheetData.WEB請求書番号基数++;
				}
				else
				{
					// 「口振請求なし」シートに追加
					record = string.Join(",", headerData.GetInvoiceNothingRecord());
					口振請求なしList.Add(record);

					Program.gBasicSheetData.口振請求なし件数++;
				}
			}
			return headerLineList;
		}

		/// <summary>
		/// WEB請求書明細消費税行ファイル（invoice_detail_tax.tsv）の出力
		/// </summary>
		/// <param name="pathname">WEB請求書明細消費税行ファイル名</param>
		/// <param name="headerLineList">ヘッダ行リスト</param>
		private void FileOutTax(string pathname, List<AccountTransferHeaderLine> headerLineList)
		{
			FileStream fs = null;
			try
			{
				fs = new FileStream(pathname, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("shift_jis")))
				{
					foreach (AccountTransferHeaderLine header in headerLineList)
					{
						List<string> billList = InvoiceDetailLine.GetTaxDataList(header.DetailLineList);
						foreach (string line in billList)
						{
							sw.WriteLine(line);
						}
					}
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

		/// <summary>
		/// WEB請求書明細記事行ファイル（invoice_detail_comment.tsv）の出力
		/// </summary>
		/// <param name="pathname">WEB請求書明細記事行ファイル名</param>
		/// <param name="headerLineList">ヘッダ行リスト</param>
		private void FileOutComment(string pathname, List<AccountTransferHeaderLine> headerLineList)
		{
			FileStream fs = null;
			try
			{
				fs = new FileStream(pathname, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("shift_jis")))
				{
					foreach (AccountTransferHeaderLine header in headerLineList)
					{
						List<string> billList = InvoiceDetailLine.GetCommentDataList(header.DetailLineList);
						foreach (string line in billList)
						{
							sw.WriteLine(line);
						}
					}
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

		/// <summary>
		/// AGREX口振通知書ファイルの出力（132001yyyyMMddF.csv）
		/// </summary>
		/// <param name="pathname">AGREX口振通知書ファイルパス名</param>
		/// <param name="headerLineList">ヘッダ行作業リスト</param>
		private void FileOutAgrexFile(string pathname, List<AccountTransferHeaderLine> headerLineList, List<InvoiceHeaderData> invoiceList)
		{
			FileStream fs = null;
			try
			{
				fs = new FileStream(pathname, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("shift_jis")))
				{
					foreach (AccountTransferHeaderLine headerLine in headerLineList)
					{
						InvoiceHeaderData headerData = invoiceList.Find(p => p.得意先コード == headerLine.得意先No);
						if (null != headerData && null != headerData.InvoiceDetailDataList && 0 < headerData.InvoiceDetailDataList.Count)
						{
							if (headerData.Customer.IsAGREX口振通知書())
							{
								// 受注コード
								string juchuCode = string.Format("{0}{1}{2}", headerLine.請求日付.Value.ToString("yyyyMMdd"), headerLine.得意先No.PadLeft(7, '0'), StringUtil.Right(headerLine.請求書No.ToString(), 5));

								// AGREX口振通知書開始行の取得
								string buf = headerLine.GetAgrexStartLine(headerData.Customer, headerData.InvoiceDetailDataList[0], juchuCode, Program.gBasicSheetData.口座振替請求日.Value, Program.gBasicSheetData.口座振替請求期間開始日.Value, Program.gBasicSheetData.口座振替請求期間終了日.Value);
								sw.WriteLine(buf);

								foreach (InvoiceDetailLine detailLine in headerLine.DetailLineList)
								{
									// AGREX口振通知書明細行の取得
									buf = headerLine.GetAgrexDataLine(juchuCode, detailLine);
									sw.WriteLine(buf);
								}
								// AGREX口振通知書終了行の取得
								buf = headerLine.GetAgrexEndLine(juchuCode);
								sw.WriteLine(buf);
							}
						}
					}
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
