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
using CommonLib.DB.SqlServer.PcaInvoiceDataConverter;
using DocumentFormat.OpenXml.Spreadsheet;
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
		/// 顧客情報リスト
		/// </summary>
		public List<CustomerInfo> Customers { get; set; }

		/// <summary>
		/// 「顧客情報」シート
		/// </summary>
		public DataTable DataTableCustomers { get; set; }

		/// <summary>
		/// 請求一覧表リスト
		/// </summary>
		private List<InvoiceHeaderData> InvoiceHeaderDataList { get; set; }

		/// <summary>
		/// 「銀行振込０円請求」シート
		/// </summary>
		private List<string> 銀行振込０円請求List { get; set; }

		/// <summary>
		/// 「銀行振込マイナス請求」シート
		/// </summary>
		private List<string> 銀行振込マイナス請求List { get; set; }

		/// <summary>
		/// PCA請求データコンバータ.xlsx
		/// </summary>
		private XLWorkbook WorkbookPca = null;

		/// <summary>
		/// Excelの起動プロセス
		/// </summary>
		private Process ProcessExcel = null;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public BankTransferForm()
		{
			InitializeComponent();

			//Customers = null;
			//DataTableCustomers = null;
			//InvoiceHeaderDataList = new List<InvoiceHeaderData>();
			//銀行振込０円請求List = new List<string>();
			//銀行振込マイナス請求List = new List<string>();

			ProcessExcel = new Process();
			ProcessExcel.StartInfo.FileName = Program.ExcelPathname;
			ProcessExcel.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BankTransferForm_Load(object sender, EventArgs e)
		{
			try
			{
				///////////////////////////////////////////////////////
				// PCA請求データコンバータ.xlsx 更新処理

				using (XLWorkbook book = new XLWorkbook(Program.ExcelPathname))
				{
					book.Style.Font.FontName = Program.SheetNameFontName;
					book.Style.Font.FontSize = Program.SheetNameFontSize;

					/////////////////////////////////////////////////////////////////////
					// 「基本データ」 銀行振込請求書発行関連基本データ 初期値設定

					IXLWorksheet sheet基本データ = book.Worksheet(Program.SheetNameBasicData);

					// 請求書番号基数
					sheet基本データ.Cell(33, 3).Value = Program.gBasicSheetData.請求書番号基数;

					// 銀行振込請求書請求日=本日
					sheet基本データ.Cell(34, 3).Value = Program.gBasicSheetData.銀行振込請求書請求日;

					// 銀行振込請求期間開始日=先月11日、銀行振込請求期間終了日=今月10日
					sheet基本データ.Cell(35, 3).Value = Program.gBasicSheetData.銀行振込請求期間開始日;
					sheet基本データ.Cell(35, 5).Value = Program.gBasicSheetData.銀行振込請求期間終了日;

					// 銀行振込入金期限日=今月末日
					sheet基本データ.Cell(36, 3).Value = Program.gBasicSheetData.銀行振込入金期限日;

					// PCA請求一覧読込みファイル
					sheet基本データ.Cell(37, 3).Value = Program.gBasicSheetData.PCA請求一覧11読込みファイル;

					// PCA請求明細読込みファイル
					sheet基本データ.Cell(38, 3).Value = Program.gBasicSheetData.PCA請求明細11読込みファイル;

					// AGREX請求書ファイル出力フォルダ
					sheet基本データ.Cell(39, 3).Value = Program.gBasicSheetData.AGREX請求書ファイル出力フォルダ;

					// AGREX請求書ファイル=本日
					sheet基本データ.Cell(40, 3).Value = Program.gBasicSheetData.AGREX請求書ファイル;

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
			///////////////////////////////////////////////////////
			// 銀行振込請求書発行関連基本データ

			//textBox請求書番号基数.Text = Program.gBasicSheetData.請求書番号基数.ToString();
			//dateTimePicker銀行振込請求書請求日.Value = Program.gBasicSheetData.銀行振込請求書請求日.Value;
			//dateTimePicker銀行振込請求期間開始日.Value = Program.gBasicSheetData.銀行振込請求期間開始日.Value;
			//dateTimePicker銀行振込請求期間終了日.Value = Program.gBasicSheetData.銀行振込請求期間終了日.Value;
			//dateTimePicker銀行振込入金期限日.Value = Program.gBasicSheetData.銀行振込入金期限日.Value;
			//textBoxPCA請求一覧11読込みファイル.Text = Program.gBasicSheetData.PCA請求一覧11読込みファイル;
			//textBoxPCA請求明細11読込みファイル.Text = Program.gBasicSheetData.PCA請求明細11読込みファイル;
			//labelAGREX請求書ファイル出力フォルダ.Text = Program.gBasicSheetData.AGREX請求書ファイル出力フォルダ;
			//textBoxAGREX請求書ファイル.Text = Program.gBasicSheetData.AGREX請求書ファイル;
			//label銀行振込請求一覧件数.Text =  string.Format("{0} 件 ", Program.gBasicSheetData.銀行振込請求一覧件数.CommaEdit());
			//label銀行振込請求一覧請求金額.Text = string.Format("{0} 円 ", Program.gBasicSheetData.銀行振込請求一覧請求金額.CommaEdit());
			//label銀行振込請求書件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.銀行振込請求書件数.CommaEdit());
			//label銀行振込請求金額.Text = string.Format("{0} 円 ", Program.gBasicSheetData.銀行振込請求金額.CommaEdit());
			//label銀行振込マイナス請求件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.銀行振込マイナス請求件数.CommaEdit());
			//label銀行振込マイナス請求金額.Text = string.Format("{0} 円 ", Program.gBasicSheetData.銀行振込マイナス請求金額.CommaEdit());
			//label銀行振込0円請求件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.銀行振込0円請求件数.CommaEdit());
			//label銀行振込請求書発行件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.銀行振込請求書発行件数.CommaEdit());
			//label銀行振込請求書発行金額.Text = string.Format("{0} 円 ", Program.gBasicSheetData.銀行振込請求書発行金額.CommaEdit());
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
					labelAGREX請求書ファイル出力フォルダ.Text = fbd.SelectedPath;
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
				DataTableCustomers = PcaInvoiceDataConverterGetIO.GetCustomerInfo(Program.gSettings.ConnectJunp.ConnectionString);
				if (0 < DataTableCustomers.Rows.Count)
				{
					// 顧客情報の取得
					Customers = CustomerInfo.DataTableToList(DataTableCustomers);

					// WW顧客データ.csvの出力
					string pathname = Path.Combine(Directory.GetCurrentDirectory(), Program.CustomerDataFile);
					using (StreamWriter sw = new StreamWriter(pathname, false, Encoding.GetEncoding("Shift_JIS")))
					{
						// タイトル行出力
						sw.WriteLine(CustomerInfo.GetTitle(DataTableCustomers));

						// データ出力
						foreach (CustomerInfo customer in Customers)
						{
							sw.WriteLine(customer.GetData());
						}
					}
				}
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
				if (0 == textBoxPCA請求一覧11読込みファイル.Text.Trim().Length)
				{
					MessageBox.Show(" 「基本データ」 PCA請求一覧読込みファイルが設定されていません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				string pathname = Path.Combine(Directory.GetCurrentDirectory(), textBoxPCA請求一覧11読込みファイル.Text.Trim());
				if (false == File.Exists(pathname))
				{
					MessageBox.Show(string.Format("{0}が存在しません。", pathname), Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				Program.gBasicSheetData.PCA請求一覧11読込みファイル = textBoxPCA請求一覧11読込みファイル.Text.Trim();

				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				Program.gBasicSheetData.銀行振込請求一覧件数 = 0;
				Program.gBasicSheetData.銀行振込請求一覧請求金額 = 0;
				Program.gBasicSheetData.銀行振込請求書件数 = 0;
				Program.gBasicSheetData.銀行振込請求金額 = 0;
				Program.gBasicSheetData.銀行振込マイナス請求件数 = 0;
				Program.gBasicSheetData.銀行振込マイナス請求金額 = 0;
				Program.gBasicSheetData.銀行振込0円請求件数 = 0;
				label銀行振込請求一覧件数.Text = "0 件 ";
				label銀行振込請求一覧請求金額.Text = "0 円 ";
				label銀行振込請求書件数.Text = "0 件 ";
				label銀行振込請求金額.Text = "0 円 ";
				label銀行振込マイナス請求件数.Text = "0 件 ";
				label銀行振込マイナス請求件数.Text = "0 円 ";
				label銀行振込0円請求件数.Text = "0 件 ";
				label銀行振込請求一覧件数.Update();
				label銀行振込請求一覧請求金額.Update();
				label銀行振込請求書件数.Update();
				label銀行振込請求金額.Update();
				label銀行振込マイナス請求件数.Update();
				label銀行振込マイナス請求件数.Update();
				label銀行振込0円請求件数.Update();

				// PCA請求一覧読込みファイル
				InvoiceHeaderDataList.Clear();
				using (StreamReader sr = new StreamReader(pathname, Encoding.GetEncoding("shift_jis")))
				{
					// 「請求一覧」シートの作成
					while (!sr.EndOfStream)
					{
						string line = sr.ReadLine();
						string[] values = line.Split(',');
						if ("10" == values[3])
						{
							// 合計行以外
							InvoiceHeaderData invoiceHeader = new InvoiceHeaderData();
							invoiceHeader.SetData(line, values);
							InvoiceHeaderDataList.Add(invoiceHeader);

							// 請求一覧表に顧客情報を紐づけ
							invoiceHeader.Customer = Customers.Find(p => p.得意先No == invoiceHeader.得意先コード);

							// 請求一覧件数、請求一覧請求金額
							Program.gBasicSheetData.銀行振込請求一覧件数 = InvoiceHeaderDataList.Count;
							Program.gBasicSheetData.銀行振込請求一覧請求金額 = InvoiceHeaderDataList.Sum(p => p.請求残高);
							label銀行振込請求一覧件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.銀行振込請求一覧件数.CommaEdit());
							label銀行振込請求一覧請求金額.Text = string.Format("{0} 円 ", Program.gBasicSheetData.銀行振込請求一覧請求金額.CommaEdit());
							label銀行振込請求一覧件数.Update();
							label銀行振込請求一覧請求金額.Update();
						}
					}
				}
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
				if (0 == textBoxPCA請求明細11読込みファイル.Text.Trim().Length)
				{
					MessageBox.Show("PCA請求明細読込みファイルが指定されていません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				string pathname = Path.Combine(Directory.GetCurrentDirectory(), textBoxPCA請求明細11読込みファイル.Text.Trim());
				if (false == File.Exists(pathname))
				{
					MessageBox.Show(string.Format("{0}が存在しません。", pathname), Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				Program.gBasicSheetData.PCA請求明細11読込みファイル = textBoxPCA請求明細11読込みファイル.Text.Trim();

				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 請求明細データファイル読込み
				using (StreamReader sr = new StreamReader(pathname, Encoding.GetEncoding("shift_jis")))
				{
					List<InvoiceDetailData> invoiceDetailDataList = new List<InvoiceDetailData>();

					while (!sr.EndOfStream)
					{
						string line = sr.ReadLine();
						string[] values = line.Split(',');
						InvoiceDetailData detailData = new InvoiceDetailData();
						detailData.SetData(values);
						invoiceDetailDataList.Add(detailData);
					}
					// 請求一覧表クラスに請求明細データリストを紐づけ
					foreach (InvoiceHeaderData headerData in InvoiceHeaderDataList)
					{
						headerData.InvoiceDetailDataList = invoiceDetailDataList.FindAll(p => p.得意先コード == headerData.得意先コード);
					}
					// カーソルを元に戻す
					Cursor.Current = preCursor;

					MessageBox.Show("請求明細データ読込みが終了しました。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
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
				if (0 == InvoiceHeaderDataList.Count || 0 == InvoiceHeaderDataList[0].InvoiceDetailDataList.Count || 0 == Customers.Count)
				{
					MessageBox.Show("先に、請求明細 および 顧客情報 の読込みをしてください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (0 == labelAGREX請求書ファイル出力フォルダ.Text.Length)
				{
					MessageBox.Show("AGREX請求書ファイル出力フォルダが指定されていません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				if (0 == textBoxAGREX請求書ファイル.Text.Trim().Length)
				{
					MessageBox.Show("AGREX請求書ファイルが指定されていません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				///////////////////////////////////////////////////////
				// 銀行振込請求書発行関連基本データ

				Program.gBasicSheetData.請求書番号基数 = textBox請求書番号基数.ToInt() + 1;
				Program.gBasicSheetData.銀行振込請求書請求日 = dateTimePicker銀行振込請求書請求日.Value;
				Program.gBasicSheetData.銀行振込請求期間開始日 = dateTimePicker銀行振込請求期間開始日.Value;
				Program.gBasicSheetData.銀行振込請求期間終了日 = dateTimePicker銀行振込請求期間終了日.Value;
				Program.gBasicSheetData.銀行振込入金期限日 = dateTimePicker銀行振込入金期限日.Value;
				Program.gBasicSheetData.PCA請求一覧11読込みファイル = textBoxPCA請求一覧11読込みファイル.Text.Trim();
				Program.gBasicSheetData.PCA請求明細11読込みファイル = textBoxPCA請求明細11読込みファイル.Text.Trim();
				Program.gBasicSheetData.AGREX請求書ファイル出力フォルダ = labelAGREX請求書ファイル出力フォルダ.Text;
				Program.gBasicSheetData.AGREX請求書ファイル = textBoxAGREX請求書ファイル.Text.Trim();

				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 現在時刻の取得
				DateTime start = DateTime.Now;

				// 銀行振込ヘッダ行および明細行の作成
				List<BankTransferHeaderLine> headerLineList = this.MakeHeaderLineAndDeatilLine();

				if (false == Directory.Exists(Program.gBasicSheetData.AGREX請求書ファイル出力フォルダ))
				{
					Directory.CreateDirectory(Program.gBasicSheetData.AGREX請求書ファイル出力フォルダ);
				}
				// 銀行振込請求書ファイル出力
				this.FileOutAgrexFile(Path.Combine(Program.gBasicSheetData.AGREX請求書ファイル出力フォルダ, Program.gBasicSheetData.AGREX請求書ファイル), headerLineList);

				// 終了時刻の取得
				DateTime end = DateTime.Now;

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				TimeSpan sp = end - start;
				string msg = string.Format("銀行振込請求書データ を作成し\r\n"
														+ "{0} に、\r\n"
														+ "{1} を出力しました。\r\n"
														+ "経過時間：{2}秒"
														, Program.gBasicSheetData.AGREX請求書ファイル出力フォルダ
														, Program.gBasicSheetData.AGREX請求書ファイル
														, Math.Floor(sp.TotalSeconds));
				MessageBox.Show(msg, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Information);

				// PCA請求データコンバータ.xlsxの表示
				//BootExcel(headerLineList);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 銀行振込ヘッダ行および明細行の作成
		/// </summary>
		/// <returns>銀行振込ヘッダ行作業リスト</returns>
		private List<BankTransferHeaderLine> MakeHeaderLineAndDeatilLine()
		{
			銀行振込０円請求List.Clear();
			銀行振込マイナス請求List.Clear();

			// 「銀行振込０円請求」「銀行振込マイナス請求」の１行目に「請求一覧」のE:Mのタイトルを設定
			// 得意先コード、得意先名１、得意先名２、前回請求額、入金額	、繰越金額、税込売上高、請求残高、回収予定日
			string record = string.Join(",", InvoiceHeaderData.GetInvoiceNothingTitle());
			銀行振込０円請求List.Add(record);
			銀行振込マイナス請求List.Add(record);

			DateTime? 請求繰越日 = Program.gBasicSheetData.銀行振込請求期間開始日;  // 請求期間開始日
			if (請求繰越日.HasValue)
			{
				請求繰越日 = 請求繰越日.Value.AddDays(-1);
			}
			Program.gBasicSheetData.銀行振込0円請求件数 = 0;
			Program.gBasicSheetData.銀行振込マイナス請求件数 = 0;
			Program.gBasicSheetData.銀行振込マイナス請求金額 = 0;
			Program.gBasicSheetData.銀行振込請求書件数 = 0;
			Program.gBasicSheetData.銀行振込請求金額 = 0;
			label銀行振込0円請求件数.Text = "0 件 ";
			label銀行振込マイナス請求件数.Text = "0 件 ";
			label銀行振込マイナス請求金額.Text = "0 円 ";
			label銀行振込請求書件数.Text =  "0 件 ";
			label銀行振込請求金額.Text = "0 円 ";
			label銀行振込0円請求件数.Update();
			label銀行振込マイナス請求件数.Update();
			label銀行振込マイナス請求金額.Update();
			label銀行振込請求書件数.Update();
			label銀行振込請求金額.Update();

			List<BankTransferHeaderLine> headerLineList = new List<BankTransferHeaderLine>();
			foreach (InvoiceHeaderData headerData in InvoiceHeaderDataList)
			{
				if (0 == headerData.請求残高)
				{
					// 「銀行振込０円請求」シートに追加
					record = string.Join(",", headerData.GetInvoiceNothingRecord());
					銀行振込０円請求List.Add(record);
					Program.gBasicSheetData.銀行振込0円請求件数++;

					label銀行振込0円請求件数.Text = string.Format("{0} 件", Program.gBasicSheetData.銀行振込0円請求件数.CommaEdit());
					label銀行振込0円請求件数.Update();
				}
				else if (0 > headerData.請求残高)
				{
					// 「銀行振込マイナス請求」シートに追加
					record = string.Join(",", headerData.GetInvoiceNothingRecord());
					銀行振込マイナス請求List.Add(record);

					Program.gBasicSheetData.銀行振込マイナス請求件数++;
					Program.gBasicSheetData.銀行振込マイナス請求金額 += headerData.請求残高;
					label銀行振込マイナス請求件数.Text = string.Format("{0} 件", Program.gBasicSheetData.銀行振込マイナス請求件数.CommaEdit());
					label銀行振込マイナス請求金額.Text = string.Format("{0} 円", Program.gBasicSheetData.銀行振込マイナス請求金額.CommaEdit());
					label銀行振込マイナス請求件数.Update();
					label銀行振込マイナス請求金額.Update();
				}
				else
				{
					Program.gBasicSheetData.銀行振込請求書件数++;
					Program.gBasicSheetData.銀行振込請求金額 += headerData.請求残高;
					label銀行振込請求書件数.Text = string.Format("{0} 件", Program.gBasicSheetData.銀行振込請求書件数.CommaEdit());
					label銀行振込請求金額.Text = string.Format("{0} 円", Program.gBasicSheetData.銀行振込請求金額.CommaEdit());
					label銀行振込請求書件数.Update();
					label銀行振込請求金額.Update();

					// ヘッダ行作成
					BankTransferHeaderLine headerLine = new BankTransferHeaderLine();
					headerLine.請求書No = Program.gBasicSheetData.請求書番号基数;
					headerLine.顧客ID = headerData.Customer.顧客No;
					headerLine.得意先No = headerData.得意先コード;
					headerLine.請求日付 = Program.gBasicSheetData.銀行振込請求書請求日;
					headerLine.合計請求額税込 = headerData.請求残高;
					headerLine.消費税額 = headerData.InvoiceDetailDataList[0].期間外税額;
					headerLine.紙請求書 = headerData.Is銀行振込請求書送付();
					headerLineList.Add(headerLine);

					// 明細行作成
					InvoiceDetailLine line1 = new InvoiceDetailLine();
					line1.請求書No = Program.gBasicSheetData.請求書番号基数;
					line1.枝番 = 1;
					line1.売上日付 = 請求繰越日;
					line1.伝票No = InvoiceDetailLine.DenNoMax;
					line1.商品名 = InvoiceDetailLine.GoodsNameLastBill;
					line1.金額 = headerData.前回請求額;
					line1.行タイプ = InvoiceDetailLine.TypeTax;
					headerLine.DetailLineList.Add(line1);

					InvoiceDetailLine line2 = new InvoiceDetailLine();
					line2.請求書No = Program.gBasicSheetData.請求書番号基数;
					line2.枝番 = 2;
					line2.売上日付 = line1.売上日付;
					line2.伝票No = InvoiceDetailLine.DenNoMax;
					line2.商品名 = InvoiceDetailLine.GoodsNamePayment;
					line2.金額 = headerData.入金額;
					line2.行タイプ = InvoiceDetailLine.TypeTax;
					headerLine.DetailLineList.Add(line2);

					InvoiceDetailLine line3 = new InvoiceDetailLine();
					line3.請求書No = Program.gBasicSheetData.請求書番号基数;
					line3.枝番 = 3;
					line3.売上日付 = line1.売上日付;
					line3.伝票No = InvoiceDetailLine.DenNoMax;
					line3.商品名 = InvoiceDetailLine.GoodsNameCarryForword;
					line3.金額 = headerData.繰越金額;
					line3.行タイプ = InvoiceDetailLine.TypeTax;
					headerLine.DetailLineList.Add(line3);

					InvoiceDetailLine line4 = new InvoiceDetailLine();
					line4.請求書No = Program.gBasicSheetData.請求書番号基数;
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
								line.請求書No = Program.gBasicSheetData.請求書番号基数;
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
							line5.請求書No = Program.gBasicSheetData.請求書番号基数;
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
							line6.請求書No = Program.gBasicSheetData.請求書番号基数;
							line6.枝番 = 枝番;
							line6.売上日付 = detailDataList[0].売上日;
							line6.伝票No = detailDataList[0].伝票No;
							line6.商品名 = detailDataList[0].摘要名文字列();
							line6.行タイプ = InvoiceDetailLine.TypeComment;
							headerLine.DetailLineList.Add(line6);
							枝番++;

							// 伝票計行
							InvoiceDetailLine line7 = new InvoiceDetailLine();
							line7.請求書No = Program.gBasicSheetData.請求書番号基数;
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

					Program.gBasicSheetData.請求書番号基数++;
					textBox請求書番号基数.Text = Program.gBasicSheetData.請求書番号基数.ToString();
					textBox請求書番号基数.Update();
				}
			}
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
								string buf = headerLine.GetAgrexStartLine(headerData.Customer, headerData.InvoiceDetailDataList[0], juchuCode, Program.gBasicSheetData.銀行振込入金期限日.Value, Program.gBasicSheetData.銀行振込請求期間開始日.Value, Program.gBasicSheetData.銀行振込請求期間終了日.Value);
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

		///// <summary>
		///// Excelの起動
		///// </summary>
		///// <param name="headerLineList">銀行振込ヘッダ行リスト</param>
		//private void BootExcel(List<BankTransferHeaderLine> headerLineList)
		//{
		//	try
		//	{
		//		// 元のカーソルを保持
		//		Cursor preCursor = Cursor.Current;

		//		// カーソルを待機カーソルに変更
		//		Cursor.Current = Cursors.WaitCursor;

		//		XLWorkbook wb = new XLWorkbook(Program.ExcelPathname);
		//		wb.Style.Font.FontName = "メイリオ";
		//		wb.Style.Font.FontSize = 9;
		//		IXLWorksheet wsBasic = wb.Worksheet(Program.SheetNameBasicData);

		//		///////////////////////////////////////////////////////
		//		// 銀行振込請求書発行関連基本データ

		//		wsBasic.Cell(33, 3).Value = Program.gBasicSheetData.請求書番号基数;
		//		wsBasic.Cell(34, 3).Value = Program.gBasicSheetData.銀行振込請求書請求日.Value;
		//		wsBasic.Cell(35, 3).Value = Program.gBasicSheetData.銀行振込請求期間開始日.Value;
		//		wsBasic.Cell(35, 6).Value = Program.gBasicSheetData.銀行振込請求期間終了日.Value;
		//		wsBasic.Cell(36, 3).Value = Program.gBasicSheetData.銀行振込入金期限日.Value;
		//		wsBasic.Cell(37, 3).Value = Program.gBasicSheetData.PCA請求一覧11読込みファイル;
		//		wsBasic.Cell(38, 3).Value = Program.gBasicSheetData.PCA請求明細11読込みファイル;
		//		wsBasic.Cell(39, 3).Value = Program.gBasicSheetData.AGREX請求書ファイル出力フォルダ;
		//		wsBasic.Cell(40, 3).Value = Program.gBasicSheetData.AGREX請求書ファイル;
		//		wsBasic.Cell(41, 3).Value = Program.gBasicSheetData.銀行振込請求一覧件数;
		//		wsBasic.Cell(41, 6).Value = Program.gBasicSheetData.銀行振込請求一覧請求金額;
		//		wsBasic.Cell(42, 3).Value = Program.gBasicSheetData.銀行振込請求書件数;
		//		wsBasic.Cell(42, 6).Value = Program.gBasicSheetData.銀行振込請求金額;
		//		wsBasic.Cell(43, 3).Value = Program.gBasicSheetData.銀行振込マイナス請求件数;
		//		wsBasic.Cell(43, 6).Value = Program.gBasicSheetData.銀行振込マイナス請求金額;
		//		wsBasic.Cell(44, 3).Value = Program.gBasicSheetData.銀行振込0円請求件数;

		//		// 「顧客情報」シートの出力
		//		Program.AddWorksheet(book, Program.SheetNameCustomer, dataTableCustomer);

		//		// 「請求一覧」シートの出力
		//		DataTable tableHeaderData = InvoiceHeaderData.GetHeaderDataDataTable(InvoiceHeaderDataList);
		//		Program.AddWorksheet(book, Program.SheetNameInvoiceHeader, tableHeaderData);

		//		// 「請求明細」シートの出力
		//		DataTable tableDetailData = InvoiceDetailData.SetColumns();
		//		foreach (InvoiceHeaderData header in InvoiceHeaderDataList)
		//		{
		//			foreach (InvoiceDetailData detail in header.InvoiceDetailDataList)
		//			{
		//				tableDetailData.Rows.Add(detail.GetDataRow(tableDetailData.NewRow()));
		//			}
		//		}
		//		Program.AddWorksheet(book, Program.SheetNameInvoiceDetail, tableDetailData);

		//		// 「銀行振込０円請求」シートの出力
		//		IXLWorksheet ws銀行振込０円請求 = Program.AddWorksheet(wb, Program.SheetNameBankTransferZeroInvoice);
		//		int row = 1;
		//		foreach (string line in 銀行振込０円請求List)
		//		{
		//			string[] values = line.Split(',');
		//			int column = 1;
		//			foreach (string str in values)
		//			{
		//				ws銀行振込０円請求.Cell(row, column).Value = str;
		//				column++;
		//			}
		//			row++;
		//		}
		//		// 「銀行振込マイナス請求」シートの出力
		//		IXLWorksheet ws銀行振込マイナス請求 = Program.AddWorksheet(wb, Program.SheetNameBankTransferMinusInvoice);
		//		row = 1;
		//		foreach (string line in 銀行振込マイナス請求List)
		//		{
		//			string[] values = line.Split(',');
		//			int column = 1;
		//			foreach (string str in values)
		//			{
		//				ws銀行振込マイナス請求.Cell(row, column).Value = str;
		//				column++;
		//			}
		//			row++;
		//		}
		//		// 「ヘッダ行作業」の出力
		//		Program.DeleteWorksheet(wb, Program.SheetNameHeaderLine);
		//		DataTable headerLineDataTable = BankTransferHeaderLine.GetHeaderLineDataTable(headerLineList);
		//		IXLWorksheet wsHeader = wb.Worksheets.Add(headerLineDataTable, Program.SheetNameHeaderLine);

		//		// 「明細行作業」の出力
		//		Program.DeleteWorksheet(wb, Program.SheetNameDetailLine);
		//		DataTable detailLineDataTable = InvoiceDetailLine.SetColumns();
		//		foreach (BankTransferHeaderLine headerLine in headerLineList)
		//		{
		//			foreach (InvoiceDetailLine detailLine in headerLine.DetailLineList)
		//			{
		//				detailLineDataTable.Rows.Add(detailLine.GetDataRow(detailLineDataTable.NewRow()));
		//			}
		//		}
		//		IXLWorksheet wsDetail = wb.Worksheets.Add(detailLineDataTable, Program.SheetNameDetailLine);

		//		// ワークブックの保存
		//		wb.Save();

		//		// カーソルを元に戻す
		//		Cursor.Current = preCursor;

		//		// Excelの起動
		//		using (Process process = new Process())
		//		{
		//			process.StartInfo.FileName = Program.ExcelPathname;
		//			process.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
		//			process.Start();
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show(ex.Message, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
		//	}
		//}

		/// <summary>
		/// Form Closed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BankTransferForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			// ワークブックの保存
			//WorkbookPca.Save();
		}
	}
}
