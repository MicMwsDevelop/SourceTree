//
// BankTransferForm.cs
// 
// 銀行振替請求書データ作成画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/27 勝呂):新規作成
//
using ClosedXML.Excel;
using CommonLib.BaseFactory.PcaInvoiceDataConverter;
using CommonLib.Common;
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
	public partial class BankTransferForm : Form
	{
		/// <summary>
		/// 請求一覧表リスト
		/// </summary>
		private List<InvoiceHeaderData> InvoiceHeaderDataList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public BankTransferForm()
		{
			InitializeComponent();

			InvoiceHeaderDataList = new List<InvoiceHeaderData>();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BankTransferForm_Load(object sender, EventArgs e)
		{
			///////////////////////////////////////////////////////
			// 銀行振込請求書発行関連基本データ

			textBox請求書番号基数.Text = Program.gBasicSheetData.請求書番号基数.ToString();
			dateTimePicker銀行振込請求書請求日.Value = Program.gBasicSheetData.銀行振込請求書請求日.Value;
			dateTimePicker銀行振込請求期間開始日.Value = Program.gBasicSheetData.銀行振込請求期間開始日.Value;
			dateTimePicker銀行振込請求期間終了日.Value = Program.gBasicSheetData.銀行振込請求期間終了日.Value;
			dateTimePicker銀行振込入金期限日.Value = Program.gBasicSheetData.銀行振込入金期限日.Value;
			textBoxPCA請求一覧11読込みファイル.Text = Program.gBasicSheetData.PCA請求一覧11読込みファイル;
			textBoxPCA請求明細11読込みファイル.Text = Program.gBasicSheetData.PCA請求明細11読込みファイル;
			labelAGREX請求書ファイル出力フォルダ.Text = Program.gBasicSheetData.AGREX請求書ファイル出力フォルダ;
			textBoxAGREX請求書ファイル.Text = Program.gBasicSheetData.AGREX請求書ファイル;
			label銀行振込請求一覧件数.Text =  string.Format("{0} 件 ", Program.gBasicSheetData.銀行振込請求一覧件数.CommaEdit());
			label銀行振込請求一覧請求金額.Text = string.Format("{0} 円 ", Program.gBasicSheetData.銀行振込請求一覧請求金額.CommaEdit());
			label銀行振込請求書件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.銀行振込請求書件数.CommaEdit());
			label銀行振込請求金額.Text = string.Format("{0} 円 ", Program.gBasicSheetData.銀行振込請求金額.CommaEdit());
			label銀行振込マイナス請求件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.銀行振込マイナス請求件数.CommaEdit());
			label銀行振込マイナス請求金額.Text = string.Format("{0} 円 ", Program.gBasicSheetData.銀行振込マイナス請求金額.CommaEdit());
			label銀行振込0円請求件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.銀行振込0円請求件数.CommaEdit());
			label銀行振込請求書発行件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.銀行振込請求書発行件数.CommaEdit());
			label銀行振込請求書発行金額.Text = string.Format("{0} 円 ", Program.gBasicSheetData.銀行振込請求書発行金額.CommaEdit());
		}

		/// <summary>
		/// AGREX請求書ファイル出力フォルダの設定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAGREX請求書ファイル出力フォルダ_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog fbd = new FolderBrowserDialog())
			{
				fbd.Description = "APLUS送信ファイル出力フォルダを指定してください。";
				fbd.RootFolder = Environment.SpecialFolder.Desktop;
				fbd.SelectedPath = Program.gBasicSheetData.APLUS送信ファイル出力フォルダ;
				fbd.ShowNewFolderButton = true;
				if (DialogResult.OK == fbd.ShowDialog(this))
				{
					labelAGREX請求書ファイル出力フォルダ.Text = Program.gBasicSheetData.AGREX請求書ファイル出力フォルダ = fbd.SelectedPath;
					//Program.WS基本データ.Cell(39, 3).Value = Program.gBasicSheetData.AGREX請求書ファイル出力フォルダ;
				}
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
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 顧客情報読込
				Program.ReadCustomerInfo();

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("顧客情報読込みが終了しました。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 請求一覧データ読込み（請求一覧11.txt）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonReadInvoiceHeaderData_Click(object sender, EventArgs e)
		{
			try
			{
				// PCA請求一覧読込みファイル
				string filename = Program.WS基本データ.Cell(37, 3).GetString();
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
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 請求一覧データファイル読込み
				Program.ReadInvoiceHeaderDataFile(pathname, InvoiceHeaderDataList);

				// 請求一覧件数、請求一覧請求金額
				//Program.WS基本データ.Cell(41, 3).Value = InvoiceHeaderDataList.Count;
				//Program.WS基本データ.Cell(41, 6).Value = InvoiceHeaderDataList.Sum(p => p.請求残高);

				// ワークブックの保存
				Program.PcaWorkbook.Save();

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("請求一覧データ読込みが終了しました。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 請求明細データ読込み（請求明細11.txt）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonReadInvoiceDetailData_Click(object sender, EventArgs e)
		{
			try
			{
				// PCA請求明細読込みファイル
				string filename = Program.WS基本データ.Cell(38, 3).GetString();
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
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 請求明細データファイル読込み
				Program.ReadInvoiceDetailDataFile(pathname, InvoiceHeaderDataList);

				// ワークブックの保存
				Program.PcaWorkbook.Save();

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("請求明細データ読込みが終了しました。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
				// 「請求明細」シートと「顧客一覧」シートの存在確認
				bool ret1 = Program.IsExistWorksheet(Program.SheetNameInvoiceDetail);
				bool ret2 = Program.IsExistWorksheet(Program.SheetNameCustomer);
				if (false == ret1 || false == ret2)
				{
					MessageBox.Show("先に、請求明細 および 顧客情報 の読込みをしてください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 現在時刻の取得
				DateTime start = DateTime.Now;

				// 「ヘッダ行作業」シートの削除
				Program.DeleteWorksheet(Program.SheetNameHeaderLine);

				// 「明細行作業」シートの削除
				Program.DeleteWorksheet(Program.SheetNameDetailLine);

				// 「ヘッダ行作業」「明細行作業」シートの出力
				List<BankTransferHeaderLine> headerLineList = this.MakeHeaderAndDeatilSheet();

				// ワークブックの保存
				Program.PcaWorkbook.Save();

				string AGREX請求書ファイル出力フォルダ = Program.WS基本データ.Cell(39, 3).GetString();
				string AGREX請求書ファイル = Program.WS基本データ.Cell(40, 3).GetString();
				if (false == Directory.Exists(AGREX請求書ファイル出力フォルダ))
				{
					Directory.CreateDirectory(AGREX請求書ファイル出力フォルダ);
				}
				// 銀行振込請求書ファイル出力
				this.FileOutAgrexFile(Path.Combine(AGREX請求書ファイル出力フォルダ, AGREX請求書ファイル), headerLineList);

				// 終了時刻の取得
				DateTime end = DateTime.Now;

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				TimeSpan sp = end - start;
				string msg = string.Format("銀行振込請求書データ を作成し\r\n"
														+ "{0} に、\r\n"
														+ "{1} を出力しました。\r\n"
														+ "経過時間：{2}秒"
														, AGREX請求書ファイル出力フォルダ
														, AGREX請求書ファイル
														, Math.Floor(sp.TotalSeconds));
				MessageBox.Show(msg, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Information);

				// PCA請求データコンバータ.xlsxの表示
				BootExcel();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 「ヘッダ行作業」「明細行作業」シートの出力
		/// </summary>
		/// <returns>銀行振込ヘッダ行作業リスト</returns>
		private List<BankTransferHeaderLine> MakeHeaderAndDeatilSheet()
		{
			List<BankTransferHeaderLine> headerLineList = new List<BankTransferHeaderLine>();

			// 「銀行振込０円請求」シートの作成
			IXLWorksheet wsZero = Program.AddWorksheet(Program.SheetNameBankTransferZeroInvoice);

			// 「銀行振込マイナス請求」シートの作成
			IXLWorksheet wsMinus = Program.AddWorksheet(Program.SheetNameBankTransferMinusInvoice);

			// 「銀行振込０円請求」シート、「銀行振込マイナス請求」シートにタイトル行を設定
			string[] titles = InvoiceHeaderData.GetInvoiceNothingTitle();
			int column = 1;
			foreach (string title in titles)
			{
				wsZero.Cell(1, column).Value = title;
				wsMinus.Cell(1, column).Value = title;
				column++;
			}
			int 請求書No = (int)Program.GetValueDouble(Program.WS基本データ.Cell(33, 3).Value) + 1; // 請求書番号基数
			DateTime? 請求日 = Program.GetValueDateTime(Program.WS基本データ.Cell(34, 3).Value);    // 請求日
			DateTime? 請求繰越日 = Program.GetValueDateTime(Program.WS基本データ.Cell(35, 3).Value);  // 請求期間開始日
			if (請求繰越日.HasValue)
			{
				請求繰越日 = 請求繰越日.Value.AddDays(-1);
			}
			Program.WS基本データ.Cell(42, 3).Value = ""; // 銀行振込請求書件数
			Program.WS基本データ.Cell(42, 6).Value = ""; // 銀行振込請求金額
			Program.WS基本データ.Cell(43, 3).Value = ""; // マイナス請求件数
			Program.WS基本データ.Cell(43, 6).Value = ""; // マイナス請求金額
			Program.WS基本データ.Cell(44, 3).Value = ""; // ０円請求件数

			int 銀行振込０円請求件数 = 0;
			int マイナス請求件数 = 0;
			int マイナス請求金額 = 0;
			int 銀行振込請求書件数 = 0;
			int 銀行振込請求金額 = 0;
			foreach (InvoiceHeaderData headerData in InvoiceHeaderDataList)
			{
				if (0 == headerData.請求残高)
				{
					// 「銀行振込０円請求」シートに追加
					column = 1;
					foreach (string data in headerData.GetInvoiceNothingRecord())
					{
						wsZero.Cell(銀行振込０円請求件数 + 2, column).Value = data;
						column++;
					}
					銀行振込０円請求件数++;
				}
				else if (0 > headerData.請求残高)
				{
					// 「銀行振込マイナス請求」シートに追加
					column = 1;
					foreach (string data in headerData.GetInvoiceNothingRecord())
					{
						wsZero.Cell(マイナス請求件数 + 2, column).Value = data;
						column++;
					}
					マイナス請求件数++;
					マイナス請求金額 += headerData.請求残高;
				}
				else
				{
					銀行振込請求書件数++;
					銀行振込請求金額 += headerData.請求残高;

					// ヘッダ行作成
					BankTransferHeaderLine headerLine = new BankTransferHeaderLine();
					headerLine.請求書No = 請求書No;
					headerLine.顧客ID = headerData.Customer.顧客No;
					headerLine.得意先No = headerData.得意先コード;
					headerLine.請求日付 = 請求日;
					headerLine.合計請求額税込 = headerData.請求残高;
					headerLine.消費税額 = headerData.InvoiceDetailDataList[0].期間外税額;
					headerLine.紙請求書 = headerData.Is銀行振込請求書送付();
					headerLineList.Add(headerLine);

					// 明細行作成
					InvoiceDetailLine line1 = new InvoiceDetailLine();
					line1.請求書No = 請求書No;
					line1.枝番 = 1;
					line1.売上日付 = 請求繰越日;
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

					if (0 < headerData.InvoiceDetailDataList[0].期間売上額)
					{
						// 期間売上額が０円以上の時のみ売上行を出力
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
					// ヘッダ行に各行数を設定
					headerLine.明細行数 = headerLine.GetInvoiceDetailBillCount();
					headerLine.消費税行数 = headerLine.GetInvoiceDetailTaxCount();
					headerLine.記事行数 = headerLine.GetInvoiceDetailCommentCount();

					請求書No++;
				}
			}
			// 「ヘッダ行作業」の出力
			DataTable headerLineDataTable = BankTransferHeaderLine.GetHeaderLineDataTable(headerLineList);
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
			foreach (BankTransferHeaderLine headerLine in headerLineList)
			{
				foreach (InvoiceDetailLine detailLine in headerLine.DetailLineList)
				{
					detailLineDataTable.Rows.Add(detailLine.GetDataRow(detailLineDataTable));
				}
			}
			IXLWorksheet wsDetail = Program.PcaWorkbook.Worksheets.Add(detailLineDataTable, Program.SheetNameDetailLine);

			// 表全体の列、カラムの幅を自動調整
			//wsHeader.ColumnsUsed().AdjustToContents();
			//wsDetail.ColumnsUsed().AdjustToContents();

			//Program.WS基本データ.Cell(33, 3).Value = 請求書No;                  // 請求書番号基数
			//Program.WS基本データ.Cell(42, 3).Value = 銀行振込請求書件数;  // 銀行振込請求書件数
			//Program.WS基本データ.Cell(42, 6).Value = 銀行振込請求金額;       // 銀行振込請求金額
			//Program.WS基本データ.Cell(43, 3).Value = マイナス請求件数;           // マイナス請求件数
			//Program.WS基本データ.Cell(43, 6).Value = マイナス請求金額;           // マイナス請求金額
			//Program.WS基本データ.Cell(44, 3).Value = 銀行振込０円請求件数; // ０円請求件数

			return headerLineList;
		}

		/// <summary>
		/// 銀行振込請求書ファイル出力（296001yyMMdd.csv.csv）
		/// </summary>
		/// <param name="pathname">AGREX口振通知書ファイルパス名</param>
		/// <param name="headerLineList">銀行振込ヘッダ行作業リスト</param>
		private void FileOutAgrexFile(string pathname, List<BankTransferHeaderLine> headerLineList)
		{
			FileStream fs = null;
			try
			{
				DateTime? 銀行振込請求期間開始日 = Program.GetValueDateTime(Program.WS基本データ.Cell(35, 3).Value);
				DateTime? 銀行振込請求期間終了日 = Program.GetValueDateTime(Program.WS基本データ.Cell(35, 5).Value);
				DateTime? 入金期限日 = Program.GetValueDateTime(Program.WS基本データ.Cell(36, 3).Value);

				fs = new FileStream(pathname, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("shift_jis")))
				{
					foreach (BankTransferHeaderLine headerLine in headerLineList)
					{
						if (headerLine.紙請求書)
						{
							InvoiceHeaderData headerData = InvoiceHeaderDataList.Find(p => p.得意先コード == headerLine.得意先No);
							if (null != headerData && null != headerData.InvoiceDetailDataList && 0 < headerData.InvoiceDetailDataList.Count)
							{
								// 受注コード
								string juchuCode = string.Format("{0}{1}{2}", headerLine.請求日付.Value.ToString("yyyyMMdd"), headerLine.得意先No.PadLeft(7, '0'), StringUtil.Right(headerLine.請求書No.ToString(), 5));

								// AGREX銀行振込請求書開始行の取得
								string buf = headerLine.GetAgrexStartLine(headerData.Customer, headerData.InvoiceDetailDataList[0], juchuCode, 入金期限日.Value, 銀行振込請求期間開始日.Value, 銀行振込請求期間終了日.Value);
								sw.WriteLine(buf);
								foreach (InvoiceDetailLine detailLine in headerLine.DetailLineList)
								{
									// AGREX銀行振込請求書明細行の取得
									buf = headerLine.GetAgrexDataLine(juchuCode, detailLine);
									sw.WriteLine(buf);
								}
								// AGREX銀行振込請求書終了行の取得
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

		/// <summary>
		/// Excelの起動
		/// </summary>
		private void BootExcel()
		{
			///////////////////////////////////////////////////////
			// 銀行振込請求書発行関連基本データ

			Program.WS基本データ.Cell(33, 3).Value = Program.gBasicSheetData.請求書番号基数;
			Program.WS基本データ.Cell(34, 3).Value = Program.gBasicSheetData.銀行振込請求書請求日.Value;
			Program.WS基本データ.Cell(35, 3).Value = Program.gBasicSheetData.銀行振込請求期間開始日.Value;
			Program.WS基本データ.Cell(35, 6).Value = Program.gBasicSheetData.銀行振込請求期間終了日.Value;
			Program.WS基本データ.Cell(36, 3).Value = Program.gBasicSheetData.銀行振込入金期限日.Value;
			Program.WS基本データ.Cell(37, 3).Value = Program.gBasicSheetData.PCA請求一覧11読込みファイル;
			Program.WS基本データ.Cell(38, 3).Value = Program.gBasicSheetData.PCA請求明細11読込みファイル;
			Program.WS基本データ.Cell(39, 3).Value = Program.gBasicSheetData.AGREX請求書ファイル出力フォルダ;
			Program.WS基本データ.Cell(40, 3).Value = Program.gBasicSheetData.AGREX請求書ファイル;
			Program.WS基本データ.Cell(41, 3).Value = Program.gBasicSheetData.銀行振込請求一覧件数;
			Program.WS基本データ.Cell(41, 6).Value = Program.gBasicSheetData.銀行振込請求一覧請求金額;
			Program.WS基本データ.Cell(42, 3).Value = Program.gBasicSheetData.銀行振込請求書件数;
			Program.WS基本データ.Cell(42, 6).Value = Program.gBasicSheetData.銀行振込請求金額;
			Program.WS基本データ.Cell(43, 3).Value = Program.gBasicSheetData.銀行振込マイナス請求件数;
			Program.WS基本データ.Cell(43, 6).Value = Program.gBasicSheetData.銀行振込マイナス請求金額;
			Program.WS基本データ.Cell(44, 3).Value = Program.gBasicSheetData.銀行振込0円請求件数;

			// ワークブックの保存
			Program.PcaWorkbook.Save();

			// Excelの起動
			using (Process process = new Process())
			{
				process.StartInfo.FileName = Program.ExcelPathname;
				process.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
				process.Start();
			}
		}
	}
}
