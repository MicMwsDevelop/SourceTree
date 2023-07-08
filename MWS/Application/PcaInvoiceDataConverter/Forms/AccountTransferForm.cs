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
		/// 請求一覧表リスト
		/// </summary>
		private List<InvoiceHeaderData> InvoiceHeaderDataList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public AccountTransferForm()
		{
			InitializeComponent();

			InvoiceHeaderDataList = new List<InvoiceHeaderData>();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AccountTransferForm_Load(object sender, EventArgs e)
		{
			///////////////////////////////////////////////////////
			// 口座振替関連基本データ

			dateTimePicker口座振替日.Value = Program.gBasicSheetData.口座振替日.Value;
			textBoxPCA請求一覧10読込みファイル.Text = Program.gBasicSheetData.PCA請求一覧10読込みファイル;
			labelAPLUS送信ファイル出力フォルダ.Text = Program.gBasicSheetData.APLUS送信ファイル出力フォルダ;
			textBoxAPLUS送信ファイル.Text = Program.gBasicSheetData.APLUS送信ファイル;
			label口座振替請求一覧件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.口座振替請求一覧件数.CommaEdit());
			label口座振替請求一覧請求金額.Text =  string.Format("{0} 円 ", Program.gBasicSheetData.口座振替請求一覧請求金額.CommaEdit());
			label口座振替不可件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.口座振替不可件数.CommaEdit());
			label口座振替不可請求額.Text = string.Format("{0} 円 ", Program.gBasicSheetData.口座振替不可請求額.CommaEdit());
			label口座振替不要件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.口座振替不要件数.CommaEdit());
			label口座振替不要請求額.Text = string.Format("{0} 円 ", Program.gBasicSheetData.口座振替不要請求額.CommaEdit());
			label口座振替請求件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.口座振替請求件数.CommaEdit());
			label口座振替請求金額.Text = string.Format("{0} 円 ", Program.gBasicSheetData.口座振替請求金額.CommaEdit());


			///////////////////////////////////////////////////////
			// WEB請求書発行関連基本データ

			textBoxWEB請求書番号基数.Text = Program.gBasicSheetData.WEB請求書番号基数.ToString();
			dateTimePicker口座振替請求日.Value = Program.gBasicSheetData.口座振替請求日.Value;
			dateTimePicker口座振替請求期間開始日.Value = Program.gBasicSheetData.口座振替請求期間開始日.Value;
			dateTimePicker口座振替請求期間終了日.Value = Program.gBasicSheetData.口座振替請求期間終了日.Value;
			textBoxPCA請求明細10読込みファイル.Text = Program.gBasicSheetData.PCA請求明細10読込みファイル;
			labelWEB請求書ファイル出力フォルダ.Text = Program.gBasicSheetData.WEB請求書ファイル出力フォルダ;
			textBoxWEB請求書ヘッダファイル.Text = Program.gBasicSheetData.WEB請求書ヘッダファイル;
			textBoxWEB請求書明細売上行ファイル.Text = Program.gBasicSheetData.WEB請求書明細売上行ファイル;
			textBoxWEB請求書明細消費税行ファイル.Text = Program.gBasicSheetData.WEB請求書明細消費税行ファイル;
			textBoxWEB請求書明細記事行ファイル.Text = Program.gBasicSheetData.WEB請求書明細記事行ファイル;
			textBoxAGREX口振通知書ファイル.Text = Program.gBasicSheetData.AGREX口振通知書ファイル;
			labelWEB請求書件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.WEB請求書件数.CommaEdit());
			labelAGREX口振通知書件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.AGREX口振通知書件数.CommaEdit());
			label口振請求なし件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.口振請求なし件数.CommaEdit());
			label請求金額あり件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.請求金額あり件数.CommaEdit());
			labelWEB請求書請求金額.Text = string.Format("{0} 円 ", Program.gBasicSheetData.WEB請求書請求金額.CommaEdit());
		}

		/// <summary>
		/// APLUS送信ファイル出力フォルダの変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAPLUS送信ファイル出力フォルダ_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog fbd = new FolderBrowserDialog())
			{
				fbd.Description = "APLUS送信ファイル出力フォルダを指定してください。";
				fbd.RootFolder = Environment.SpecialFolder.Desktop;
				fbd.SelectedPath = Program.gBasicSheetData.APLUS送信ファイル出力フォルダ;
				fbd.ShowNewFolderButton = true;
				if (DialogResult.OK == fbd.ShowDialog(this))
				{
					labelAPLUS送信ファイル出力フォルダ.Text = Program.gBasicSheetData.APLUS送信ファイル出力フォルダ = fbd.SelectedPath;
					//Program.WS基本データ.Cell(10, 3).Value = Program.gBasicSheetData.APLUS送信ファイル出力フォルダ;
				}
			}
		}

		/// <summary>
		/// WEB請求書ファイル出力フォルダの変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonWEB請求書ファイル出力フォルダ_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog fbd = new FolderBrowserDialog())
			{
				fbd.Description = "WEB請求書ファイル出力フォルダを指定してください。";
				fbd.RootFolder = Environment.SpecialFolder.Desktop;
				fbd.SelectedPath = Program.gBasicSheetData.WEB請求書ファイル出力フォルダ;
				fbd.ShowNewFolderButton = true;
				if (DialogResult.OK == fbd.ShowDialog(this))
				{
					labelWEB請求書ファイル出力フォルダ.Text = Program.gBasicSheetData.WEB請求書ファイル出力フォルダ = fbd.SelectedPath;
					//Program.WS基本データ.Cell(23, 3).Value = Program.gBasicSheetData.WEB請求書ファイル出力フォルダ;
				}
			}
		}

		/// <summary>
		/// AGREX口振通知書ファイル出力フォルダの変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAGREX口振通知書ファイル出力フォルダ_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog fbd = new FolderBrowserDialog())
			{
				fbd.Description = "AGREX口振通知書ファイル出力フォルダを指定してください。";
				fbd.RootFolder = Environment.SpecialFolder.Desktop;
				fbd.SelectedPath = Program.gBasicSheetData.AGREX口振通知書ファイル出力フォルダ;
				fbd.ShowNewFolderButton = true;
				if (DialogResult.OK == fbd.ShowDialog(this))
				{
					labelAGREX口振通知書ファイル出力フォルダ.Text = Program.gBasicSheetData.AGREX口振通知書ファイル出力フォルダ = fbd.SelectedPath;
					//Program.WS基本データ.Cell(28, 3).Value = Program.gBasicSheetData.AGREX口振通知書ファイル出力フォルダ;
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
		/// 請求一覧データ読込み（請求一覧10.txt）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonReadInvoiceHeaderData_Click(object sender, EventArgs e)
		{
			try
			{
				// PCA請求一覧読込みファイル
				string filename = Program.WS基本データ.Cell(6, 3).GetString();
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

				Program.gBasicSheetData.口座振替請求一覧件数 = 0;
				Program.gBasicSheetData.口座振替請求一覧請求金額 = 0;
				Program.gBasicSheetData.口座振替不可件数 = 0;
				Program.gBasicSheetData.口座振替不可請求額 = 0;
				Program.gBasicSheetData.口座振替不要件数 = 0;
				Program.gBasicSheetData.口座振替不要請求額 = 0;
				label口座振替請求一覧件数.Text = "0 件 ";
				label口座振替請求一覧請求金額.Text = "0 円 ";
				label口座振替不可件数.Text = "0 件 ";
				label口座振替不可請求額.Text = "0 円 ";
				label口座振替不要件数.Text = "0 件 ";
				label口座振替不要請求額.Text = "0 円 ";
				label口座振替請求件数.Text = "0 件 ";
				label口座振替請求金額.Text = "0 円 ";

				// 請求一覧データファイル読込み
				Program.ReadInvoiceHeaderDataFile(pathname, InvoiceHeaderDataList);

				// 請求一覧件数、請求一覧請求金額
				Program.gBasicSheetData.口座振替請求一覧件数 = InvoiceHeaderDataList.Count;
				Program.gBasicSheetData.口座振替請求一覧請求金額 = InvoiceHeaderDataList.Sum(p => p.請求残高);
				label口座振替請求一覧件数.Text = string.Format("{0} 件 ", Program.gBasicSheetData.口座振替請求一覧件数.CommaEdit());
				label口座振替請求一覧請求金額.Text = string.Format("{0} 円 ", Program.gBasicSheetData.口座振替請求一覧請求金額.CommaEdit());
				Program.WS基本データ.Cell(9, 3).Value = Program.gBasicSheetData.口座振替請求一覧件数;
				Program.WS基本データ.Cell(9, 6).Value = InvoiceHeaderDataList.Sum(p => p.請求残高);

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
		/// 請求明細データ読込み（請求明細10.txt）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonReadInvoiceDetailData_Click(object sender, EventArgs e)
		{
			try
			{
				// PCA請求明細読込みファイル
				string filename = Program.WS基本データ.Cell(19, 3).GetString();
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
		/// 口座振替データ作成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonMakeAccountTransfer_Click(object sender, EventArgs e)
		{
			try
			{
				// 「請求一覧」シートと「顧客一覧」シートの存在確認
				bool ret1 = Program.IsExistWorksheet(Program.SheetNameInvoiceHeader);
				bool ret2 = Program.IsExistWorksheet(Program.SheetNameCustomer);
				if (false == ret1 || false == ret2)
				{
					MessageBox.Show("先に、請求明細 および 顧客情報 の読込みをしてください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (0 == Program.gBasicSheetData.APLUS送信ファイル出力フォルダ.Length)
				{
					MessageBox.Show("APLUS送信ファイル出力フォルダを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (0 == textBoxAPLUS送信ファイル.Text.Trim().Length)
				{
					MessageBox.Show("APLUS送信ファイルを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 現在時刻の取得
				DateTime start = DateTime.Now;

				// 口座振替日の取得
				Program.gBasicSheetData.口座振替日 = dateTimePicker口座振替日.Value;

				// 口座振替データ作成
				List<string> sendDataList;
				this.口座振替送信データ作成(out sendDataList);

				// 口座振替ファイル出力（SosinyyMMdd.txt）
				Program.gBasicSheetData.APLUS送信ファイル = textBoxAPLUS送信ファイル.Text.Trim();
				//Program.WS基本データ.Cell(7, 3).Value = Program.gBasicSheetData.APLUS送信ファイル出力フォルダ;
				//Program.WS基本データ.Cell(8, 3).Value = Program.gBasicSheetData.APLUS送信ファイル;
				this.口座振替ファイル出力(sendDataList);

				// ワークブックの保存
				Program.PcaWorkbook.Save();

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

				// 「請求明細」シートと「顧客一覧」シートの存在確認
				bool ret1 = Program.IsExistWorksheet(Program.SheetNameInvoiceDetail);
				bool ret2 = Program.IsExistWorksheet(Program.SheetNameCustomer);
				if (false == ret1 || false == ret2)
				{
					MessageBox.Show("先に、請求明細 および 顧客情報 の読込みをしてください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (0 == Program.gBasicSheetData.WEB請求書ファイル出力フォルダ.Length)
				{
					MessageBox.Show("WEB請求書ファイル出力フォルダを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (0 == textBoxWEB請求書ヘッダファイル.Text.Trim().Length)
				{
					MessageBox.Show("WEB請求書ヘッダファイルを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (0 == textBoxWEB請求書明細売上行ファイル.Text.Trim().Length)
				{
					MessageBox.Show("WEB請求書明細売上行ファイルを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (0 == textBoxWEB請求書明細消費税行ファイル.Text.Trim().Length)
				{
					MessageBox.Show("WEB請求書明細消費税行ファイルを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (0 == textBoxWEB請求書明細記事行ファイル.Text.Trim().Length)
				{
					MessageBox.Show("WEB請求書明細記事行ファイルを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (0 == Program.gBasicSheetData.AGREX口振通知書ファイル出力フォルダ.Length)
				{
					MessageBox.Show("AGREX口振通知書ファイル出力フォルダを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (0 == textBoxAGREX口振通知書ファイル.Text.Trim().Length)
				{
					MessageBox.Show("AGREX口振通知書ファイルを指定してください。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (dateTimePicker口座振替請求期間終了日.Value <= dateTimePicker口座振替請求期間開始日.Value)
				{
					MessageBox.Show("口座振替請求期間開始日または口座振替請求期間終了日が正しくありません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				Program.gBasicSheetData.WEB請求書番号基数 = textBoxWEB請求書番号基数.ToInt();

				// 現在時刻の取得
				DateTime start = DateTime.Now;

				// 口座振替請求日の取得
				Program.gBasicSheetData.口座振替請求日 = dateTimePicker口座振替請求日.Value;

				// 「ヘッダ行作業」シートの削除
				Program.DeleteWorksheet(Program.SheetNameHeaderLine);

				// 「明細行作業」シートの削除
				Program.DeleteWorksheet(Program.SheetNameDetailLine);

				// 「ヘッダ行作業」「明細行作業」シートの出力
				List<AccountTransferHeaderLine> headerLineList = this.MakeHeaderAndDeatilSheet();

				// ワークブックの保存
				Program.PcaWorkbook.Save();

				// WEB請求書ファイル出力フォルダの取得
				if (false == Directory.Exists(Program.gBasicSheetData.WEB請求書ファイル出力フォルダ))
				{
					Directory.CreateDirectory(Program.gBasicSheetData.WEB請求書ファイル出力フォルダ);
				}
				string webFolder = Path.Combine(Program.gBasicSheetData.WEB請求書ファイル出力フォルダ,	DateTime.Today.ToString("yyyyMMdd"));
				if (false == Directory.Exists(webFolder))
				{
					Directory.CreateDirectory(webFolder);
				}
				// WEB請求書ヘッダファイル（invoice_header.tsv）の出力
				Program.gBasicSheetData.WEB請求書ヘッダファイル = textBoxWEB請求書ヘッダファイル.Text.Trim();
				//Program.WS基本データ.Cell(21, 3).Value = Program.gBasicSheetData.WEB請求書ヘッダファイル;
				this.FileOut(Path.Combine(webFolder, Program.gBasicSheetData.WEB請求書ヘッダファイル), headerLineList);

				// WEB請求書明細売上行ファイル（invoice_detail_bill.tsv）の出力
				Program.gBasicSheetData.WEB請求書明細売上行ファイル = textBoxWEB請求書明細売上行ファイル.Text.Trim();
				//Program.WS基本データ.Cell(22, 3).Value = Program.gBasicSheetData.WEB請求書明細売上行ファイル;
				this.FileOutBill(Path.Combine(webFolder, Program.gBasicSheetData.WEB請求書明細売上行ファイル), headerLineList);

				// WEB請求書明細消費税行ファイル（invoice_detail_tax.tsv）の出力
				Program.gBasicSheetData.WEB請求書明細消費税行ファイル = textBoxWEB請求書明細消費税行ファイル.Text.Trim();
				//Program.WS基本データ.Cell(23, 3).Value = Program.gBasicSheetData.WEB請求書明細消費税行ファイル;
				this.FileOutTax(Path.Combine(webFolder, Program.gBasicSheetData.WEB請求書明細消費税行ファイル), headerLineList);

				// WEB請求書明細記事行ファイル（invoice_detail_comment.tsv）の出力
				Program.gBasicSheetData.WEB請求書明細記事行ファイル = textBoxWEB請求書明細記事行ファイル.Text.Trim();
				//Program.WS基本データ.Cell(24, 3).Value = Program.gBasicSheetData.WEB請求書明細記事行ファイル;
				this.FileOutComment(Path.Combine(webFolder, Program.gBasicSheetData.WEB請求書明細記事行ファイル), headerLineList);

				// AGREX口振通知書ファイル（132001yyyyMMddF.csv）の出力
				if (false == Directory.Exists(Program.gBasicSheetData.AGREX口振通知書ファイル出力フォルダ))
				{
					Directory.CreateDirectory(Program.gBasicSheetData.AGREX口振通知書ファイル出力フォルダ);
				}
				Program.gBasicSheetData.AGREX口振通知書ファイル = textBoxAGREX口振通知書ファイル.Text.Trim();
				//Program.WS基本データ.Cell(26, 3).Value = Program.gBasicSheetData.AGREX口振通知書ファイル;

				Program.gBasicSheetData.口座振替請求期間開始日 = dateTimePicker口座振替請求期間開始日.Value;
				Program.gBasicSheetData.口座振替請求期間終了日 = dateTimePicker口座振替請求期間終了日.Value;
				//Program.WS基本データ.Cell(18, 3).Value = Program.gBasicSheetData.口座振替請求期間開始日.Value;
				//Program.WS基本データ.Cell(18, 5).Value = Program.gBasicSheetData.口座振替請求期間終了日.Value;

				this.FileOutAgrexFile(Path.Combine(Program.gBasicSheetData.AGREX口振通知書ファイル出力フォルダ, Program.gBasicSheetData.AGREX口振通知書ファイル), headerLineList);

				// 終了時刻の取得
				DateTime end = DateTime.Now;

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				TimeSpan sp = end - start;
				string msg = string.Format("請求書送信データを作成し。\r\n"
															+ " 1.ＷＥＢ請求書\r\n"
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

				// PCA請求データコンバータ.xlsxの表示
				this.BootExcel();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 口座振替送信データ作成
		/// </summary>
		/// <param name="transferDate">口座振替日</param>
		/// <param name="sendDataList">送信データリスト</param>
		private void 口座振替送信データ作成(out List<string> sendDataList)
		{
			sendDataList = new List<string>();

			// シートの追加
			IXLWorksheet ws口振不可 = Program.AddWorksheet(Program.SheetNameImpossible);   // 「口振不可」
			IXLWorksheet ws口振不要 = Program.AddWorksheet(Program.SheetNameUnnecessary);  // 「口振不要」
			IXLWorksheet ws送信データ = Program.AddWorksheet(Program.SheetNameSendData);    // 「送信データ」

			// ws送信データは固定長なのでフォントを"ＭＳ ゴシック"に設定する
			ws送信データ.Style.Font.FontName = "ＭＳ ゴシック";
			ws送信データ.Style.Font.FontSize = 9;

			// 「口振不可」「口振不要」の１行目に「請求一覧」のE:Mのタイトルを設定
			// 得意先コード、得意先名１、得意先名２、前回請求額、入金額	、繰越金額、税込売上高、請求残高、回収予定日
			IXLWorksheet wsList = Program.PcaWorkbook.Worksheet(Program.SheetNameInvoiceHeader);
			ws口振不可.Cell(1, 1).Value =wsList.Cell(1, 5).Value.ToString();
			ws口振不要.Cell(1, 1).Value = wsList.Cell(1, 5).Value.ToString();
			ws口振不可.Cell(1, 2).Value = wsList.Cell(1, 6).Value.ToString();
			ws口振不要.Cell(1, 2).Value = wsList.Cell(1, 6).Value.ToString();
			ws口振不可.Cell(1, 3).Value = wsList.Cell(1, 7).Value.ToString();
			ws口振不要.Cell(1, 3).Value = wsList.Cell(1, 7).Value.ToString();
			ws口振不可.Cell(1, 4).Value = wsList.Cell(1, 8).Value.ToString();
			ws口振不要.Cell(1, 4).Value = wsList.Cell(1, 8).Value.ToString();
			ws口振不可.Cell(1, 5).Value = wsList.Cell(1, 9).Value.ToString();
			ws口振不要.Cell(1, 5).Value = wsList.Cell(1, 9).Value.ToString();
			ws口振不可.Cell(1, 6).Value = wsList.Cell(1, 10).Value.ToString();
			ws口振不要.Cell(1, 6).Value = wsList.Cell(1, 10).Value.ToString();
			ws口振不可.Cell(1, 7).Value = wsList.Cell(1, 11).Value.ToString();
			ws口振不要.Cell(1, 7).Value = wsList.Cell(1, 11).Value.ToString();
			ws口振不可.Cell(1, 8).Value = wsList.Cell(1, 12).Value.ToString();
			ws口振不要.Cell(1, 8).Value = wsList.Cell(1, 12).Value.ToString();
			ws口振不可.Cell(1, 9).Value = wsList.Cell(1, 13).Value.ToString();
			ws口振不要.Cell(1, 9).Value = wsList.Cell(1, 13).Value.ToString();

			// 送信データシートの１行目にヘッダーレコード（送信ヘッダ）を記録
			string record = AgrexDefine.AplusHeaderRecord(Program.gBasicSheetData.口座振替日.Value);
			ws送信データ.Cell(1, 1).Value = record;
			sendDataList.Add(record);       // 送信データリストに追加

			// 送信データシートの２行目以降にデータレコード（振替データ）を記録
			Program.gBasicSheetData.口座振替不可件数 = 0;
			Program.gBasicSheetData.口座振替不可請求額 = 0;
			Program.gBasicSheetData.口座振替不要件数 = 0;
			Program.gBasicSheetData.口座振替不要請求額 = 0;
			label口座振替不可件数.Text = "0 件 ";
			label口座振替不可請求額.Text = "0 円 ";
			label口座振替不要件数.Text = "0 件 ";
			label口座振替不要請求額.Text = "0 円 ";
			label口座振替請求件数.Text = "0 件 ";
			label口座振替請求金額.Text = "0 円 ";

			int 口座振替請求件数 = 0;
			int 口座振替請求金額 = 0;
			foreach (InvoiceHeaderData headerData in InvoiceHeaderDataList)
			{
				if (10 == headerData.データ区分)
				{
					if (0 < headerData.請求残高)
					{
						CustomerInfo cust = Program.Customers.Find(p =>p.得意先No == headerData.得意先コード);
						if (null != cust)
						{
							if (20 == cust.APLUSコード.Length)
							{
								// 「送信データ」シートに追記
								record = AgrexDefine.AplusSendDataRecord(headerData.請求残高, cust);
								ws送信データ.Cell(Program.gBasicSheetData.口座振替請求件数 + 2, 1).Value = record;
								sendDataList.Add(record);   // 送信データリストに追加

								口座振替請求件数++;
								口座振替請求金額 += headerData.請求残高;
							}
							else
							{
								// 口座情報がないので「口振不可」シートに追記
								int column = 1;
								foreach (string data in headerData.GetInvoiceNothingRecord())
								{
									ws口振不可.Cell(Program.gBasicSheetData.口座振替不可件数 + 2, column).Value = data;
									column++;
								}
								Program.gBasicSheetData.口座振替不可件数++;
								Program.gBasicSheetData.口座振替不可請求額 += headerData.請求残高;
							}
						}
					}
					else
					{
						// 請求金額が０以下なので「口振不要」シートに追記
						int column = 1;
						foreach (string data in headerData.GetInvoiceNothingRecord())
						{
							ws口振不要.Cell(Program.gBasicSheetData.口座振替不要件数 + 2, column).Value = data;
							column++;
						}
						Program.gBasicSheetData.口座振替不要件数++;
						Program.gBasicSheetData.口座振替不要請求額 += headerData.請求残高;
					}
				}
			}
			// 送信データシートにトレーラレコード（合計データ）を記録
			record = AgrexDefine.AplusTotalRecord(Program.gBasicSheetData.口座振替請求件数, Program.gBasicSheetData.口座振替請求金額);
			ws送信データ.Cell(Program.gBasicSheetData.口座振替請求件数 + 1, 1).Value = record;
			sendDataList.Add(record);   // 送信データリストに追加

			// 送信データシートにエンドレコード（終端データ）を記録
			record = AgrexDefine.AplusEndRecord();
			ws送信データ.Cell(Program.gBasicSheetData.口座振替請求件数 + 2, 1).Value = record;
			sendDataList.Add(record);   // 送信データリストに追加

			// 表全体の列、カラムの幅を自動調整
			//ws送信データ.ColumnsUsed().AdjustToContents();
			//ws口振不要.ColumnsUsed().AdjustToContents();
			//ws口振不可.ColumnsUsed().AdjustToContents();

			// 口座振替不可件数、口座振替不可請求額
			label口座振替不可件数.Text = string.Format(" {0} 件 ", Program.gBasicSheetData.口座振替不可件数.CommaEdit());
			label口座振替不可請求額.Text = string.Format(" {0} 円 ", Program.gBasicSheetData.口座振替不可請求額.CommaEdit());

			// 口座振替不要件数、口座振替不要請求額
			label口座振替不要件数.Text = string.Format(" {0} 件 ", Program.gBasicSheetData.口座振替不要件数.CommaEdit());
			label口座振替不要請求額.Text = string.Format(" {0} 円 ", Program.gBasicSheetData.口座振替不要請求額.CommaEdit());

			// 口座振替請求件数、口座振替請求金額
			label口座振替請求件数.Text = string.Format(" {0} 件 ", Program.gBasicSheetData.口座振替請求件数.CommaEdit());
			label口座振替請求金額.Text = string.Format(" {0} 円 ", Program.gBasicSheetData.口座振替請求金額.CommaEdit());
		}

		/// <summary>
		/// 口座振替ファイル出力（SosinyyMMdd.txt）
		/// </summary>
		/// <param name="sendDataList">送信データリスト</param>
		private void 口座振替ファイル出力(List<string> sendDataList)
		{
			string pathname = Path.Combine(Directory.GetCurrentDirectory(), Program.gBasicSheetData.APLUS送信ファイル);
			FileStream fs = null;
			try
			{
				fs = new FileStream(pathname, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("shift_jis")))
				{
					foreach (string line in sendDataList)
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
		/// 「ヘッダ行作業」「明細行作業」シートの出力
		/// </summary>
		/// <returns>口座振替ヘッダ行作業リスト</returns>
		private List<AccountTransferHeaderLine> MakeHeaderAndDeatilSheet()
		{
			List<AccountTransferHeaderLine> headerLineList = new List<AccountTransferHeaderLine>();

			// 「口振請求なし」シートの作成
			IXLWorksheet wsInvoiceNothing = Program.AddWorksheet(Program.SheetNameInvoiceNothing);

			// 「口振請求なし」シートにタイトル行を設定
			string[] titles = InvoiceHeaderData.GetInvoiceNothingTitle();
			int column = 1;
			foreach (string title in titles)
			{
				wsInvoiceNothing.Cell(1, column).Value = title;
				column++;
			}
			Program.gBasicSheetData.口振請求なし件数 = 0;
			Program.gBasicSheetData.AGREX口振通知書件数 = 0;
			Program.gBasicSheetData.請求金額あり件数 = 0;
			Program.gBasicSheetData.WEB請求書請求金額 = 0;
			foreach (InvoiceHeaderData headerData in InvoiceHeaderDataList)
			{
				if (0 < headerData.請求残高)
				{
					Program.gBasicSheetData.請求金額あり件数++;
					Program.gBasicSheetData.WEB請求書請求金額 += headerData.請求残高;
					if (headerData.Customer.IsAGREX口振通知書())
					{
						Program.gBasicSheetData.AGREX口振通知書件数++;
					}
					// ヘッダ行作成
					AccountTransferHeaderLine headerLine = new AccountTransferHeaderLine();
					headerLine.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
					headerLine.顧客ID = headerData.Customer.顧客No;
					headerLine.得意先No = headerData.得意先コード;
					headerLine.請求日付 = Program.gBasicSheetData.口座振替請求日;
					headerLine.合計請求額税込 = headerData.請求残高;
					headerLine.消費税額 = headerData.InvoiceDetailDataList[0].期間外税額;
					headerLine.紙請求書 = headerData.Customer.IsAGREX口振通知書();
					headerLineList.Add(headerLine);

					// 明細行作成
					InvoiceDetailLine line1 = new InvoiceDetailLine();
					line1.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
					line1.枝番 = 1;
					line1.売上日付 = headerData.前回請求締日付();
					line1.伝票No = InvoiceDetailLine.DenNoMax;
					line1.商品名 = InvoiceDetailLine.GoodsNameLastBill;
					line1.金額 = headerData.前回請求額;
					line1.行タイプ = InvoiceDetailLine.TypeTax;
					headerLine.DetailLineList.Add(line1);

					InvoiceDetailLine line2 = new InvoiceDetailLine();
					line2.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
					line2.枝番 = 2;
					line2.売上日付 = line1.売上日付;
					line2.伝票No = InvoiceDetailLine.DenNoMax;
					line2.商品名 = InvoiceDetailLine.GoodsNamePayment;
					line2.金額 = headerData.入金額;
					line2.行タイプ = InvoiceDetailLine.TypeTax;
					headerLine.DetailLineList.Add(line2);

					InvoiceDetailLine line3 = new InvoiceDetailLine();
					line3.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
					line3.枝番 = 3;
					line3.売上日付 = line1.売上日付;
					line3.伝票No = InvoiceDetailLine.DenNoMax;
					line3.商品名 = InvoiceDetailLine.GoodsNameCarryForword;
					line3.金額 = headerData.繰越金額;
					line3.行タイプ = InvoiceDetailLine.TypeTax;
					headerLine.DetailLineList.Add(line3);

					InvoiceDetailLine line4 = new InvoiceDetailLine();
					line4.請求書No = Program.gBasicSheetData.WEB請求書番号基数;
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
					column = 1;
					foreach (string data in headerData.GetInvoiceNothingRecord())
					{
						wsInvoiceNothing.Cell(Program.gBasicSheetData.口振請求なし件数 + 2, column).Value = data;
						column++;
					}
					Program.gBasicSheetData.口振請求なし件数++;
				}
			}
			// 「ヘッダ行作業」の出力
			DataTable headerLineDataTable = AccountTransferHeaderLine.GetHeaderLineDataTable(headerLineList);
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
			foreach (AccountTransferHeaderLine headerLine in headerLineList)
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

			textBoxWEB請求書番号基数.Text = Program.gBasicSheetData.WEB請求書番号基数.ToString();
			labelWEB請求書件数.Text = string.Format(" {0} 件", InvoiceHeaderDataList.Count.CommaEdit());
			labelAGREX口振通知書件数.Text = string.Format(" {0} 件", Program.gBasicSheetData.AGREX口振通知書件数.CommaEdit());
			label口振請求なし件数.Text = string.Format(" {0} 件", Program.gBasicSheetData.口振請求なし件数.CommaEdit());
			label請求金額あり件数.Text = string.Format(" {0} 件", Program.gBasicSheetData.請求金額あり件数.CommaEdit());
			labelWEB請求書請求金額.Text = string.Format(" {0} 円", Program.gBasicSheetData.WEB請求書請求金額.CommaEdit());

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
		private void FileOutAgrexFile(string pathname, List<AccountTransferHeaderLine> headerLineList)
		{
			FileStream fs = null;
			try
			{
				fs = new FileStream(pathname, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("shift_jis")))
				{
					foreach (AccountTransferHeaderLine headerLine in headerLineList)
					{
						InvoiceHeaderData headerData = InvoiceHeaderDataList.Find(p => p.得意先コード == headerLine.得意先No);
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

		/// <summary>
		/// Excelの起動
		/// </summary>
		private void BootExcel()
		{
			///////////////////////////////////////////////////////
			// 口座振替関連基本データ

			Program.WS基本データ.Cell(5, 3).Value = Program.gBasicSheetData.口座振替日.Value;
			Program.WS基本データ.Cell(6, 3).Value = Program.gBasicSheetData.PCA請求一覧10読込みファイル;
			Program.WS基本データ.Cell(7, 3).Value = Program.gBasicSheetData.APLUS送信ファイル出力フォルダ;
			Program.WS基本データ.Cell(8, 3).Value = Program.gBasicSheetData.APLUS送信ファイル;
			Program.WS基本データ.Cell(9, 3).Value = Program.gBasicSheetData.口座振替請求一覧件数;
			Program.WS基本データ.Cell(9, 6).Value = Program.gBasicSheetData.口座振替請求一覧請求金額;
			Program.WS基本データ.Cell(10, 3).Value = Program.gBasicSheetData.口座振替不可件数;
			Program.WS基本データ.Cell(10, 6).Value = Program.gBasicSheetData.口座振替不可請求額;
			Program.WS基本データ.Cell(11, 3).Value = Program.gBasicSheetData.口座振替不要件数;
			Program.WS基本データ.Cell(11, 6).Value = Program.gBasicSheetData.口座振替不要請求額;


			///////////////////////////////////////////////////////
			// WEB請求書発行関連基本データ

			Program.WS基本データ.Cell(16, 3).Value = Program.gBasicSheetData.WEB請求書番号基数;
			Program.WS基本データ.Cell(17, 3).Value = Program.gBasicSheetData.口座振替請求日.Value;
			Program.WS基本データ.Cell(18, 3).Value = Program.gBasicSheetData.口座振替請求期間開始日.Value;
			Program.WS基本データ.Cell(18, 6).Value = Program.gBasicSheetData.口座振替請求期間終了日.Value;
			Program.WS基本データ.Cell(19, 3).Value = Program.gBasicSheetData.PCA請求明細10読込みファイル;
			Program.WS基本データ.Cell(20, 3).Value = Program.gBasicSheetData.WEB請求書ファイル出力フォルダ;
			Program.WS基本データ.Cell(21, 3).Value = Program.gBasicSheetData.WEB請求書ヘッダファイル;
			Program.WS基本データ.Cell(22, 3).Value = Program.gBasicSheetData.WEB請求書明細売上行ファイル;
			Program.WS基本データ.Cell(23, 3).Value = Program.gBasicSheetData.WEB請求書明細消費税行ファイル;
			Program.WS基本データ.Cell(24, 3).Value = Program.gBasicSheetData.WEB請求書明細記事行ファイル;
			Program.WS基本データ.Cell(25, 3).Value = Program.gBasicSheetData.AGREX口振通知書ファイル出力フォルダ;
			Program.WS基本データ.Cell(26, 3).Value = Program.gBasicSheetData.AGREX口振通知書ファイル;
			Program.WS基本データ.Cell(27, 3).Value = Program.gBasicSheetData.WEB請求書件数;
			Program.WS基本データ.Cell(27, 6).Value = Program.gBasicSheetData.AGREX口振通知書件数;
			Program.WS基本データ.Cell(28, 3).Value = Program.gBasicSheetData.口振請求なし件数;
			Program.WS基本データ.Cell(29, 3).Value = Program.gBasicSheetData.請求金額あり件数;
			Program.WS基本データ.Cell(29, 6).Value = Program.gBasicSheetData.WEB請求書請求金額;

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
