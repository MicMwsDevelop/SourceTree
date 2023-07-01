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
using CommonLib.DB.SqlServer.PcaInvoiceDataConverter;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using PcaInvoiceDataConverter.BaseFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace PcaInvoiceDataConverter.Forms
{
	public partial class AccountTransferForm : Form
	{
		/// <summary>
		/// 顧客情報リスト
		/// </summary>
		private List<CustomerInfo> Customers { get; set; }

		/// <summary>
		/// 請求一覧表リスト
		/// </summary>
		private List<InvoiceHeaderData> InvoiceHeaderDataList { get; set; }

		/// <summary>
		/// 請求明細データリスト
		/// </summary>
		private List<InvoiceDetailData> InvoiceDetailDataList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public AccountTransferForm()
		{
			InitializeComponent();

			Customers = new List<CustomerInfo>();
			InvoiceHeaderDataList = new List<InvoiceHeaderData>();
			InvoiceDetailDataList = new List<InvoiceDetailData>();
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
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				DataTable table = PcaInvoiceDataConverterGetIO.GetCustomerInfo(Program.gSettings.ConnectJunp.ConnectionString);
				if (0 < table.Rows.Count)
				{
					// PCA請求データコンバータ.xlsx
					//using (XLWorkbook wb = new XLWorkbook(Program.ExcelPathname))
					{
						if (Program.IsExistWorksheet(Program.PcaWorkbook, Program.SheetNameCustomer))
						{
							// シート「顧客情報」の削除
							Program.PcaWorkbook.Worksheets.Delete(Program.SheetNameCustomer);
						}
						// シート「顧客情報」を追加して、顧客情報の書き込み
						IXLWorksheet wsCust = Program.PcaWorkbook.Worksheets.Add(table, Program.SheetNameCustomer);

						// ワークブックの保存
						Program.PcaWorkbook.Save();

						// 顧客情報の取得
						Customers = CustomerInfo.DataTableToList(table);
					}
					// WW顧客データ.csvの出力
					string csvPathname = Path.Combine(Directory.GetCurrentDirectory(), Program.CustomerDataFile);
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
				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 請求一覧データ読込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonReadList_Click(object sender, EventArgs e)
		{
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				InvoiceHeaderDataList.Clear();

				// PCA請求データコンバータ.xlsxの読込
				//using (XLWorkbook wb = new XLWorkbook(Program.ExcelPathname))
				{
					// 「基本データ」
					//IXLWorksheet wsBasic = Program.PcaWorkbook.Worksheet(Program.SheetNameBasicData);

					// PCA請求一覧読込みファイル
					string filename = Program.SheetBasic.Cell(6, 3).GetString();
					if (0 == filename.Length)
					{
						MessageBox.Show(" 「基本データ」 PCA請求一覧読込みファイルが設定されていません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
					if (false == File.Exists(pathname))
					{
						MessageBox.Show(string.Format("{0}が存在しません。", pathname), Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					// PCA請求一覧読込みファイルを開く
					using (StreamReader sr = new StreamReader(pathname, Encoding.GetEncoding("shift_jis")))
					{
						// 「請求一覧」シートの作成
						IXLWorksheet wsList = Program.AddWorksheet(Program.PcaWorkbook, Program.SheetNameInvoiceList);
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
							}
						}
						// 請求一覧件数
						Program.SheetBasic.Cell(9, 3).Value = InvoiceHeaderDataList.Count;

						// 請求一覧請求金額
						Program.SheetBasic.Cell(9, 6).Value = InvoiceHeaderDataList.Sum(p =>p.請求残高);

						// ワークブックの保存
						Program.PcaWorkbook.Save();
					}
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 請求明細データ読込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonReadDetail_Click(object sender, EventArgs e)
		{
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				InvoiceDetailDataList.Clear();

				// PCA請求データコンバータ.xlsxの読込
				//using (XLWorkbook wb = new XLWorkbook(Program.ExcelPathname))
				{
					// 「基本データ」
					//IXLWorksheet wsBasic = Program.PcaWorkbook.Worksheet(Program.SheetNameBasicData);

					// PCA請求明細読込みファイル
					string filename = Program.SheetBasic.Cell(19, 3).GetString();
					if (0 == filename.Length)
					{
						MessageBox.Show("PCA請求明細読込みファイルが指定されていません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
					if (false == File.Exists(pathname))
					{
						MessageBox.Show(string.Format("{0}が存在しません。", pathname), Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					// csvファイルを開く
					using (StreamReader sr = new StreamReader(pathname, Encoding.GetEncoding("shift_jis")))
					{
						// 「請求明細」シートに出力
						IXLWorksheet wsDetail = Program.AddWorksheet(Program.PcaWorkbook, Program.SheetNameInvoiceDetail);
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
							detailData.SetData(line, values);
							InvoiceDetailDataList.Add(detailData);
						}
						// ワークブックの保存
						Program.PcaWorkbook.Save();
					}
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;
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
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// PCA請求データコンバータ.xlsxの読込
				//using (XLWorkbook wb = new XLWorkbook(Program.ExcelPathname))
				{
					bool ret1 = Program.IsExistWorksheet(Program.PcaWorkbook, Program.SheetNameInvoiceList);
					bool ret2 = Program.IsExistWorksheet(Program.PcaWorkbook, Program.SheetNameCustomer);
					if (false == ret1 || false == ret2)
					{
						MessageBox.Show("先に、請求一覧データ および 顧客情報 の読込みをしてください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					// 現在時刻の取得
					DateTime start = DateTime.Now;

					// 振替日の取得
					//IXLWorksheet wsBasic = wb.Worksheet(Program.SheetNameBasicData);
					DateTime? transferDate = Program.GetValueDateTime(Program.SheetBasic.Cell(5, 3).Value);
					if (false == transferDate.HasValue)
					{
						MessageBox.Show("「基本データ」振替日が設定されていません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					// 口座振替データ作成
					List<string> sendDataList;
					口座振替送信データ作成(Program.PcaWorkbook, Program.SheetBasic, transferDate.Value, out sendDataList);

					// 口座振替ファイル出力
					口座振替ファイル出力(Program.SheetBasic, transferDate.Value, sendDataList);

					// ワークブックの保存
					Program.PcaWorkbook.Save();

					// 終了時刻の取得
					DateTime end = DateTime.Now;
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;
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
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				bool ret1 = Program.IsExistWorksheet(Program.PcaWorkbook, Program.SheetNameInvoiceDetail);
				bool ret2 = Program.IsExistWorksheet(Program.PcaWorkbook, Program.SheetNameCustomer);
				if (false == ret1 || false == ret2)
				{
					MessageBox.Show("先に、請求明細データ および 顧客情報 の読込みをしてください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				// 現在時刻の取得
				DateTime start = DateTime.Now;

				// 「ヘッダ行作業」シートの削除
				Program.DeleteWorksheet(Program.PcaWorkbook, Program.SheetNameHeaderLine);

				// 「明細行作業」シートの削除
				Program.DeleteWorksheet(Program.PcaWorkbook, Program.SheetNameDetailLine);

				// 「口振請求なし」シートの作成
				IXLWorksheet wsInvoiceNothing = Program.AddWorksheet(Program.PcaWorkbook, Program.SheetNameInvoiceNothing);

				// Web請求書番号基数の取得
				int 請求書No = (int)Program.SheetBasic.Cell(16, 3).GetDouble() + 1;

				// 口座振替請求日の取得
				DateTime 口座振替請求日 = Program.SheetBasic.Cell(17, 3).GetDateTime();

				// 口振請求なし件数
				int 口振請求なし件数 = 0;

				List<InvoiceHeaderLine> headerLineList = new List<InvoiceHeaderLine>();
				foreach (InvoiceHeaderData headerData in InvoiceHeaderDataList)
				{
					if (0 < headerData.請求残高)
					{
						CustomerInfo cust = Customers.Find(p =>p.得意先No == headerData.得意先コード);
						List<InvoiceDetailData> custDetailDataList = InvoiceDetailDataList.FindAll(p =>p.請求実績請求先コード == headerData.得意先コード);
						if (null != cust && 0 < custDetailDataList.Count)
						{
							// ヘッダ行作成
							InvoiceHeaderLine headerLine = new InvoiceHeaderLine();
							headerLine.請求書No = 請求書No;
							headerLine.顧客ID = cust.顧客No;
							headerLine.得意先No = headerData.得意先コード;
							headerLine.請求日付 = 口座振替請求日;
							headerLine.合計請求額税込 = headerData.請求残高;
							headerLine.消費税額 = custDetailDataList[0].期間外税額;
							headerLine.紙請求書 = cust.IsAGREX口振通知書();
							headerLineList.Add(headerLine);

							// 明細行作成
							InvoiceDetailLine line1 = new InvoiceDetailLine();
							line1.請求書No = 請求書No;
							line1.枝番 = 1;
							line1.売上日付 = headerData.前回請求締日付();
							line1.伝票No = InvoiceDetailLine.DenNoMax;
							line1.商品名 = InvoiceDetailLine.GoodsNameLastBill;
							line1.金額 = headerData.前回請求額;
							line1.行タイプ = InvoiceDetailLine.TypeTax;
							headerLine.DetailLineList.Add(line1);

							InvoiceDetailLine line2 = new InvoiceDetailLine();
							line2.請求書No = 請求書No;
							line2.枝番 = 2;
							line2.売上日付 = line1.売上日付;
							line2.伝票No = InvoiceDetailLine.DenNoMax;
							line2.商品名 = InvoiceDetailLine.GoodsNamePayment;
							line2.金額 = headerData.入金額;
							line2.行タイプ = InvoiceDetailLine.TypeTax;
							headerLine.DetailLineList.Add(line2);

							InvoiceDetailLine line3 = new InvoiceDetailLine();
							line3.請求書No = 請求書No;
							line3.枝番 = 3;
							line3.売上日付 = line1.売上日付;
							line3.伝票No = InvoiceDetailLine.DenNoMax;
							line3.商品名 = InvoiceDetailLine.GoodsNameCarryForword;
							line3.金額 = headerData.繰越金額;
							line3.行タイプ = InvoiceDetailLine.TypeTax;
							headerLine.DetailLineList.Add(line3);

							InvoiceDetailLine line4 = new InvoiceDetailLine();
							line4.請求書No = 請求書No;
							line4.枝番 = 4;
							line4.売上日付 = line1.売上日付;
							line4.伝票No = InvoiceDetailLine.DenNoMax;
							line4.商品名 = InvoiceDetailLine.SplitLine;
							line4.行タイプ = InvoiceDetailLine.TypeComment;
							headerLine.DetailLineList.Add(line4);

							if (0 < custDetailDataList[0].期間売上額)
							{
								// 期間売上額が０円以上の時のみ売上行を出力
								int 枝番 = 5;

								// 伝票No毎に（消費税等）行、摘要名行、伝票計行の３行を出力
								var queryDen = custDetailDataList.OrderBy(p => p.伝票No).GroupBy(p => p.伝票No);
								foreach (var denNo in queryDen)
								{
									// 伝票Noが同じ請求明細のみ抽出
									List<InvoiceDetailData> detailDataList = custDetailDataList.FindAll(p =>p.伝票No == denNo.Key);
									foreach (InvoiceDetailData detailData in detailDataList)
									{
										// 請求明細行
										InvoiceDetailLine line = new InvoiceDetailLine();
										line.請求書No = 請求書No;
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
									line5.請求書No = 請求書No;
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
									line6.請求書No = 請求書No;
									line6.枝番 = 枝番;
									line6.売上日付 = detailDataList[0].売上日;
									line6.伝票No = detailDataList[0].伝票No;
									line6.商品名 = detailDataList[0].摘要名文字列();
									line6.行タイプ = InvoiceDetailLine.TypeComment;
									headerLine.DetailLineList.Add(line6);
									枝番++;

									// 伝票計行
									InvoiceDetailLine line7 = new InvoiceDetailLine();
									line7.請求書No = 請求書No;
									line7.枝番 = 枝番;
									line7.売上日付 = detailDataList[0].売上日;
									line7.伝票No = detailDataList[0].伝票No;
									line7.商品名 = InvoiceDetailLine.GoodsNameSubTotal;
									line7.金額 = detailDataList[0].売上合計;
									line7.行タイプ = InvoiceDetailLine.TypeTax;
									headerLine.DetailLineList.Add(line7);
									枝番++;
								}
							}
							請求書No++;

							// ヘッダ行に各行数を設定
							headerLine.明細行数 = headerLine.GetInvoiceDetailBillCount();
							headerLine.消費税行数 = headerLine.GetInvoiceDetailTaxCount();
							headerLine.記事行数 = headerLine.GetInvoiceDetailCommentCount();
						}
					}
					else
					{
						// 「口振請求なし」シートに追加
						int column = 1;
						foreach (string data in headerData.GetCopyRecord())
						{
							wsInvoiceNothing.Cell(口振請求なし件数 + 2, column).Value = data;
							column++;
						}
						口振請求なし件数++;
					}
				}
				// 「ヘッダ行作業」の出力
				DataTable headerLineDataTable = InvoiceHeaderLine.GetHeaderLineDataTable(headerLineList);
				IXLWorksheet wsHeader = Program.PcaWorkbook.Worksheets.Add(headerLineDataTable, Program.SheetNameHeaderLine);

				// 「明細行作業」の出力
				DataTable detailLineDataTable = new DataTable();
				detailLineDataTable.Columns.Add("請求書No", typeof(int));
				detailLineDataTable.Columns.Add("枝番", typeof(int));
				detailLineDataTable.Columns.Add("売上日付", typeof(int));
				detailLineDataTable.Columns.Add("伝票No", typeof(int));
				detailLineDataTable.Columns.Add("商品名", typeof(string));
				detailLineDataTable.Columns.Add("数量", typeof(int));
				detailLineDataTable.Columns.Add("単価", typeof(int));
				detailLineDataTable.Columns.Add("金額", typeof(int));
				detailLineDataTable.Columns.Add("行タイプ", typeof(int));
				foreach (InvoiceHeaderLine headerLine in headerLineList)
				{
					foreach (InvoiceDetailLine detailLine in headerLine.DetailLineList) 
					{
						detailLineDataTable.Rows.Add(detailLine.GetDataRow(detailLineDataTable));
					}
				}
				IXLWorksheet wsDetail = Program.PcaWorkbook.Worksheets.Add(detailLineDataTable, Program.SheetNameDetailLine);

				// ワークブックの保存
				Program.PcaWorkbook.Save();



				// 終了時刻の取得
				DateTime end = DateTime.Now;

				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 口座振替送信データ作成
		/// </summary>
		/// <param name="wb"></param>
		/// <param name="wsBasic"></param>
		/// <param name="transferDate"></param>
		/// <param name="sendDataList"></param>
		private void 口座振替送信データ作成(XLWorkbook wb, IXLWorksheet wsBasic, DateTime transferDate, out List<string> sendDataList)
		{
			sendDataList = new List<string>();

			// シートの追加
			IXLWorksheet wsImpossible = Program.AddWorksheet(wb, Program.SheetNameImpossible);   // 「口振不可」
			IXLWorksheet wsUnnecessary = Program.AddWorksheet(wb, Program.SheetNameUnnecessary);  // 「口振不要」
			IXLWorksheet wsSendData = Program.AddWorksheet(wb, Program.SheetNameSendData);    // 「送信データ」

			// 「口振不可」「口振不要」の１行目に「請求一覧」のE:Mのタイトルを設定
			// 得意先コード、得意先名１、得意先名２、前回請求額、入金額	、繰越金額、税込売上高、請求残高、回収予定日
			IXLWorksheet wsList = wb.Worksheet(Program.SheetNameInvoiceList);
			wsImpossible.Cell(1, 1).Value =wsList.Cell(1, 5).Value.ToString();
			wsUnnecessary.Cell(1, 1).Value = wsList.Cell(1, 5).Value.ToString();
			wsImpossible.Cell(1, 2).Value = wsList.Cell(1, 6).Value.ToString();
			wsUnnecessary.Cell(1, 2).Value = wsList.Cell(1, 6).Value.ToString();
			wsImpossible.Cell(1, 3).Value = wsList.Cell(1, 7).Value.ToString();
			wsUnnecessary.Cell(1, 3).Value = wsList.Cell(1, 7).Value.ToString();
			wsImpossible.Cell(1, 4).Value = wsList.Cell(1, 8).Value.ToString();
			wsUnnecessary.Cell(1, 4).Value = wsList.Cell(1, 8).Value.ToString();
			wsImpossible.Cell(1, 5).Value = wsList.Cell(1, 9).Value.ToString();
			wsUnnecessary.Cell(1, 5).Value = wsList.Cell(1, 9).Value.ToString();
			wsImpossible.Cell(1, 6).Value = wsList.Cell(1, 10).Value.ToString();
			wsUnnecessary.Cell(1, 6).Value = wsList.Cell(1, 10).Value.ToString();
			wsImpossible.Cell(1, 7).Value = wsList.Cell(1, 11).Value.ToString();
			wsUnnecessary.Cell(1, 7).Value = wsList.Cell(1, 11).Value.ToString();
			wsImpossible.Cell(1, 8).Value = wsList.Cell(1, 12).Value.ToString();
			wsUnnecessary.Cell(1, 8).Value = wsList.Cell(1, 12).Value.ToString();
			wsImpossible.Cell(1, 9).Value = wsList.Cell(1, 13).Value.ToString();
			wsUnnecessary.Cell(1, 9).Value = wsList.Cell(1, 13).Value.ToString();

			// 送信データシートの１行目にヘッダーレコード（送信ヘッダ）を記録
			string record = CustomerInfo.AplusHeaderRecord(transferDate);
			wsSendData.Cell(1, 1).Value = record;
			sendDataList.Add(record);   // 送信データリストに追加

			// 送信データシートの２行目以降にデータレコード（振替データ）を記録
			int invoiceCount = 0;			// 請求一覧件数
			int invoiceTotal = 0;			// 請求一覧請求金額
			int impossibleCount = 0;		// 口座振替不可件数
			int impossibleTotal = 0;		// 口座振替不可請求額
			int unnecessaryCount = 0;	// 口座振替不要件数
			int unnecessaryTotal = 0;	// 口座振替不要請求額
			foreach (InvoiceHeaderData headerData in InvoiceHeaderDataList)
			{
				if (10 == headerData.データ区分)
				{
					if (0 < headerData.請求残高)
					{
						CustomerInfo cust = Customers.Find(p =>p.得意先No == headerData.得意先コード);
						if (null != cust)
						{
							if (20 == cust.APLUSコード.Length)
							{
								// 「送信データ」シートに追記
								record = cust.AplusSendDataRecord(headerData.請求残高);
								wsSendData.Cell(invoiceCount + 2, 1).Value = record;
								sendDataList.Add(record);   // 送信データリストに追加

								invoiceCount++;
								invoiceTotal += headerData.請求残高;
							}
							else
							{
								// 口座情報がないので「口振不可」シートに追記
								int column = 1;
								foreach (string data in headerData.GetCopyRecord())
								{
									wsImpossible.Cell(impossibleCount + 2, column).Value = data;
									column++;
								}
								impossibleCount++;
								impossibleTotal += headerData.請求残高;
							}
						}
					}
					else
					{
						// 請求金額が０以下なので「口振不要」シートに追記
						int column = 1;
						foreach (string data in headerData.GetCopyRecord())
						{
							wsUnnecessary.Cell(unnecessaryCount + 2, column).Value = data;
							column++;
						}
						unnecessaryCount++;
						unnecessaryTotal += headerData.請求残高;
					}
				}
			}
			// 送信データシートにトレーラレコード（合計データ）を記録
			record = CustomerInfo.AplusTotalRecord(invoiceCount, invoiceTotal);
			wsSendData.Cell(invoiceCount + 1, 1).Value = record;
			sendDataList.Add(record);   // 送信データリストに追加

			// 送信データシートにエンドレコード（終端データ）を記録
			record = CustomerInfo.AplusEndRecord();
			wsSendData.Cell(invoiceCount + 2, 1).Value = record;
			sendDataList.Add(record);   // 送信データリストに追加

			// 口座振替不可件数、口座振替不可請求額
			wsBasic.Cell(10, 3).Value = impossibleCount;
			wsBasic.Cell(10, 6).Value = impossibleTotal;

			// 口座振替不要件数、口座振替不要請求額
			wsBasic.Cell(11, 3).Value = unnecessaryCount;
			wsBasic.Cell(11, 6).Value = unnecessaryTotal;
		}

		/// <summary>
		/// 口座振替ファイル出力
		/// </summary>
		/// <param name="wsBasic"></param>
		/// <param name="transferDate"></param>
		/// <param name="sendDataList"></param>
		private void 口座振替ファイル出力(IXLWorksheet wsBasic, DateTime transferDate, List<string> sendDataList)
		{
			// APLUS送信ファイルの作成
			string filename = wsBasic.Cell(8, 3).GetString();
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), filename);
			using (StreamWriter sw = new StreamWriter(pathname, false, Encoding.GetEncoding("shift_jis")))
			{
				foreach (string line in sendDataList)
				{
					sw.WriteLine(line);
				}
			}
			// APLUS送信ファイル出力フォルダ
			string folder = wsBasic.Cell(7, 3).GetString();
			if (false == Directory.Exists(folder))
			{
				// APLUS送信ファイル出力フォルダの作成
				Directory.CreateDirectory(folder);
			}
			// APLUS送信ファイルをAPLUS送信ファイル出力フォルダにコピー
			File.Copy(pathname, Path.Combine(folder, filename), true);
		}
	}
}
