//
// MainForm.cs
// 
// メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/03/10 勝呂)
//
using ClosedXML.Excel;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.BaseFactory.Sales.Table;
using CommonLib.BaseFactory.Sales.View;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using CommonLib.DB.SqlServer.Sales;
using NoticeOnlineLicenseConfirm.BaseFactory;
using NoticeOnlineLicenseConfirm.Mail;
using NoticeOnlineLicenseConfirm.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NoticeOnlineLicenseConfirm.Forms
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// オンライン資格確認通知結果「通知1-東日本」
		/// </summary>
		private const string SheetNameNotice1East = "通知1-東日本";

		/// <summary>
		/// オンライン資格確認通知結果「通知1-西日本」
		/// </summary>
		private const string SheetNameNotice1West = "通知1-西日本";

		/// <summary>
		/// オンライン資格確認通知結果「通知2-ヒアリングシート」
		/// </summary>
		private const string SheetNameNotice2 = "通知2-ヒアリングシート";

		/// <summary>
		/// オンライン資格確認通知結果「通知3-東日本」
		/// </summary>
		private const string SheetNameNotice3East = "通知3-東日本";

		/// <summary>
		/// オンライン資格確認通知結果「通知4-西日本」
		/// </summary>
		private const string SheetNameNotice4West = "通知4-西日本";

		/// <summary>
		/// オンライン資格確認通知結果「通知5-東日本」
		/// </summary>
		private const string SheetNameNotice5East = "通知5-東日本";

		/// <summary>
		/// オンライン資格確認通知結果「通知5-西日本」
		/// </summary>
		private const string SheetNameNotice5West = "通知5-西日本";

		/// <summary>
		/// SalesDB接続文字列
		/// </summary>
		//private const string SalesConnectionString = @"Server=SQLSV;Database=SalesDB;User ID=web;Password=02035612;Min Pool Size=1";

		/// <summary>
		/// JunpDB接続文字列
		/// </summary>
		//private const string JunpConnectionString = @"Server=SQLSV;Database=JunpDB;User ID=web;Password=02035612;Min Pool Size=1";

		/// <summary>
		/// 環境設定
		/// </summary>
		public NoticeOnlineLicenseConfirmSettings Settings { get; set; }

		/// <summary>
		/// vオンライン資格確認ユーザーリスト
		/// </summary>
		public List<vオンライン資格確認ユーザー> HearingSheetList { get; set; }

		/// <summary>
		/// 進捗管理表_作業情報リスト
		/// </summary>
		public List<進捗管理表_作業情報> ProgressList { get; set; }

		/// <summary>
		/// NTT東日本 進捗管理表ファイル名
		/// </summary>
		public string ProgerssEastFilename { get; set; }

		/// <summary>
		/// NTT西日本 進捗管理表ファイル名
		/// </summary>
		public string ProgerssWestFilename { get; set; }

		/// <summary>
		/// NTT東日本 進捗管理表パス名
		/// </summary>
		public string ProgerssEastPathname { get; set; }

		/// <summary>
		/// NTT西日本 進捗管理表パス名
		/// </summary>
		public string ProgerssWestPathname { get; set; }

		/// <summary>
		/// NTT西日本 連絡票パス名
		/// </summary>
		public string ContactWestPathname { get; set; }

		/// <summary>
		/// NTT東日本 進捗管理表ファイル作成日
		/// </summary>
		public Date? EastFileDate { get; set; }

		/// <summary>
		/// NTT西日本 進捗管理表ファイル作成日
		/// </summary>
		public Date? WestFileDate { get; set; }

		/// <summary>
		/// オンライン資格確認通知結果ファイル名
		/// </summary>
		public string ConfirmFilename { get; set; }

		/// <summary>
		/// オンライン資格確認通知結果パス名
		/// </summary>
		public string ConfirmPathname { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			HearingSheetList = null;
			ProgressList = null;
			ProgerssEastFilename = string.Empty;
			ProgerssWestFilename = string.Empty;
			ProgerssEastPathname = string.Empty;
			ProgerssWestPathname = string.Empty;
			ConfirmFilename = string.Empty;
			EastFileDate = null;
			WestFileDate = null;
			ConfirmFilename = string.Empty;
			ConfirmPathname = string.Empty;
			ContactWestPathname = string.Empty;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			// 環境設定の読込
			Settings = NoticeOnlineLicenseConfirmSettingsIF.GetSettings();

			// バージョン情報の設定
			labelVersion.Text = Program.ProgramVersion;

			try
			{
				// ヒアリングシートの取得
				HearingSheetList = SalesDatabaseAccess.Select_vオンライン資格確認ユーザー("[送信履歴] is not null", "[顧客番号]", Settings.ConnectSales.ConnectionString);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			string xlsFilename = string.Format("オンライン資格確認通知結果_{0}.xlsx", Date.Today.GetNumeralString());
			string xlsPathname = Path.Combine(Directory.GetCurrentDirectory(), xlsFilename);
			if (File.Exists(xlsPathname))
			{
				// オンライン資格確認通知結果ファイルの設定
				ConfirmFilename = xlsFilename;
				ConfirmPathname = xlsPathname;
				textBoxConfirmFile.Text = ConfirmFilename;
			}
		}

		/// <summary>
		/// NTT東日本進捗管理表の設定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonProgressEast_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "EXCELファイル(*.xlsx)|*.xlsx|すべてのファイル(*.*)|*.*";
				dlg.Title = "NTT東日本進捗管理表を選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					ProgerssEastFilename = Path.GetFileName(dlg.FileName);
					textBoxProgressEast.Text = ProgerssEastFilename;
					ProgerssEastPathname = dlg.FileName;
					string dateStr = Regex.Replace(ProgerssEastFilename, @"[^0-9]", "");
					int dateNum = int.Parse(dateStr);

					Date work;
					if (Date.TryParse(dateNum, out work))
					{
						dateTimePickerEast.Value = work.ToDateTime();
						EastFileDate = work;
					}
					else
					{
						dateTimePickerEast.Value = DateTime.Today;
						EastFileDate = null;
					}
				}
			}
		}

		/// <summary>
		/// NTT西日本進捗管理表の設定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonProgressWest_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "EXCELファイル(*.xlsx)|*.xlsx|すべてのファイル(*.*)|*.*";
				dlg.Title = "NTT西日本進捗管理表を選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					ProgerssWestFilename = Path.GetFileName(dlg.FileName);
					textBoxProgressWest.Text = ProgerssWestFilename;
					ProgerssWestPathname = dlg.FileName;
					string dateStr = Regex.Replace(ProgerssWestFilename, @"[^0-9]", "");
					int dateNum = int.Parse(dateStr);

					Date work;
					if (Date.TryParse(dateNum, out work))
					{
						dateTimePickerWest.Value = work.ToDateTime();
						WestFileDate = work;
					}
					else
					{
						dateTimePickerWest.Value = DateTime.Today;
						WestFileDate = null;
					}
				}
			}
		}

		/// <summary>
		/// NTT西日本連絡表の設定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonContactWest_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "EXCELファイル(*.xlsx)|*.xlsx|すべてのファイル(*.*)|*.*";
				dlg.Title = "NTT西日本進捗管理表を選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					textBoxContactWest.Text = Path.GetFileName(dlg.FileName);
					ContactWestPathname = dlg.FileName;
				}
			}
		}

		/// <summary>
		/// チェック
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCheck_Click(object sender, EventArgs e)
		{
			bool eastOK = false;
			bool westOK = false;
			if (0 < ProgerssEastFilename.Length)
			{
				eastOK = true;
			}
			if (0 < ProgerssWestFilename.Length)
			{
				westOK = true;
			}
			if (false == eastOK && false == westOK)
			{
				MessageBox.Show("進捗管理表を設定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (false == eastOK && true == westOK)
			{
				if (DialogResult.No == MessageBox.Show("NTT東日本 進捗管理表が設定されていません。処理を続けますか？", Program.ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					return;
				}
			}
			else if (true == eastOK && false == westOK)
			{
				if (DialogResult.No == MessageBox.Show("NTT西日本 進捗管理表が設定されていません。処理を続けますか？", Program.ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					return;
				}
			}
			if (eastOK)
			{
				if (EastFileDate.HasValue && EastFileDate.Value.ToDateTime() != dateTimePickerEast.Value)
				{
					if (DialogResult.No == MessageBox.Show("NTT東日本 進捗管理表の更新日とファイル作成日に相違がありますが、よろしいですか？", Program.ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					{
						return;
					}
				}
			}
			if (westOK)
			{
				if (WestFileDate.HasValue && WestFileDate.Value.ToDateTime() != dateTimePickerWest.Value)
				{
					if (DialogResult.No == MessageBox.Show("NTT西日本 進捗管理表の更新日とファイル作成日に相違がありますが、よろしいですか？", Program.ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					{
						return;
					}
				}
				if (0 == ContactWestPathname.Length)
				{
					if (DialogResult.No == MessageBox.Show("NTT西日本 連絡票が設定されていませんが、よろしいですか？\n\n通知４と通知５の連絡項目と連絡内容は未出力となります。", Program.ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					{
						return;
					}
				}
			}
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// orgファイル→Excelファイルをコピー
				string orgPpathname = Path.Combine(Directory.GetCurrentDirectory(), "オンライン資格確認通知結果.xlsx.org");
				ConfirmFilename = string.Format("オンライン資格確認通知結果_{0}.xlsx", Date.Today.GetNumeralString());
				ConfirmPathname = Path.Combine(Directory.GetCurrentDirectory(), ConfirmFilename);
				File.Copy(orgPpathname, ConfirmPathname, true);

				// オンライン資格確認通知結果ファイルの設定
				textBoxConfirmFile.Text = ConfirmFilename;

				// 通知数を設定
				labelNotice1.Text = "通知１：0件";
				labelNotice2.Text = "通知２：0件";
				labelNotice3.Text = "通知３：0件";
				labelNotice4.Text = "通知４：0件";
				labelNotice5.Text = "通知５：0件";
				int notice1 = 0;
				int notice2 = 0;
				int notice3 = 0;
				int notice4 = 0;
				int notice5 = 0;
				checkBoxNotice1.Checked = false;
				checkBoxNotice3.Checked = false;
				checkBoxNotice4.Checked = false;
				checkBoxNotice5.Checked = false;

				using (XLWorkbook wb = new XLWorkbook(ConfirmPathname, XLEventTracking.Disabled))
				{
					if (0 < ProgerssEastFilename.Length && 0 < ProgerssWestFilename.Length)
					{
						// NTT東日本とNTT西日本の通知
						List<進捗管理表_NTT東日本> eastList = Read進捗管理表_NTT東日本();
						List<進捗管理表_NTT西日本> westList = Read進捗管理表_NTT西日本();
						List<連絡票_NTT西日本> contractList = Read連絡票_NTT西日本();

						// 通知１：作業日決定を担当者へ通知
						notice1 = Notice1(eastList, westList, wb);

						// 通知２：提出漏れ通知
						notice2 = Notice2(eastList, westList, wb);

						// 通知３：NTT東日本 ヒアリングシート修正アップデート通知
						notice3 = Notice3(eastList, wb);

						// 通知４：NTT西日本 ヒアリングシート修正アップデート通知
						notice4 = Notice4(westList, contractList, wb);

						// 通知５：工事日14日前でヒアリングシート未完成通知
						notice5 = Notice5(eastList, westList, contractList, wb);

						// 進捗管理表_作業情報の更新
						UpdateProgress(eastList, westList);
					}
					else if (0 < ProgerssEastFilename.Length && 0 == ProgerssWestFilename.Length)
					{
						// NTT東日本の通知
						List<進捗管理表_NTT東日本> eastList = Read進捗管理表_NTT東日本();

						// 通知１：作業日決定を担当者へ通知
						notice1 = Notice1(eastList, null, wb);

						// 通知２：提出漏れ通知
						notice2 = Notice2(eastList, null, wb);

						// 通知３：NTT東日本 ヒアリングシート修正アップデート通知
						notice3 = Notice3(eastList, wb);

						// 通知５：工事日14日前でヒアリングシート未完成通知
						notice5 = Notice5(eastList, null, null, wb);

						// 進捗管理表_作業情報の更新
						UpdateProgress(eastList, null);
					}
					else
					{
						// NTT西日本の通知
						List<進捗管理表_NTT西日本> westList = Read進捗管理表_NTT西日本();
						List<連絡票_NTT西日本> contractList = Read連絡票_NTT西日本();

						// 通知１：作業日決定を担当者へ通知
						notice1 = Notice1(null, westList, wb);

						// 通知２：提出漏れ通知
						notice2 = Notice2(null, westList, wb);

						// 通知４：NTT西日本 ヒアリングシート修正アップデート通知
						notice4 = Notice4(westList, contractList, wb);

						// 通知５：工事日14日前でヒアリングシート未完成通知
						notice5 = Notice5(null, westList, contractList, wb);

						// 進捗管理表_作業情報の更新
						UpdateProgress(null, westList);
					}
					// カーソルを元に戻す
					Cursor.Current = preCursor;

					if (0 < notice1 + notice2 + notice3 + notice4 + notice5)
					{
						// Excelファイルの保存
						wb.Save();

						labelNotice1.Text = string.Format("通知１：{0}件", notice1);
						labelNotice2.Text = string.Format("通知２：{0}件", notice2);
						labelNotice3.Text = string.Format("通知３：{0}件", notice3);
						labelNotice4.Text = string.Format("通知４：{0}件", notice4);
						labelNotice5.Text = string.Format("通知５：{0}件", notice5);
						if (0 < notice1)
						{
							checkBoxNotice1.Checked = true;
						}
						if (0 < notice3)
						{
							checkBoxNotice3.Checked = true;
						}
						if (0 < notice4)
						{
							checkBoxNotice4.Checked = true;
						}
						if (0 < notice5)
						{
							checkBoxNotice5.Checked = true;
						}
						MessageBox.Show(string.Format("通知１：{0}件\n通知２：{1}件\n通知３：{2}件\n通知４：{3}件\n通知５：{4}件\n\n{5}を確認してください。", notice1, notice2, notice3, notice4, notice5, ConfirmFilename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
					else
					{
						MessageBox.Show("通知はありませんでした。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// オンライン資格確認通知結果ファイルの設定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void buttonConfirmFile_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "EXCELファイル(*.xlsx)|*.xlsx|すべてのファイル(*.*)|*.*";
				dlg.Title = "オンライン資格確認通知結果ファイルを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					ConfirmFilename = Path.GetFileName(dlg.FileName);
					ConfirmPathname = dlg.FileName;
					textBoxConfirmFile.Text = ConfirmFilename;
				}
			}
		}

		/// <summary>
		/// メール送信
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSendMail_Click(object sender, EventArgs e)
		{
			if (0 == ConfirmPathname.Length || false == File.Exists(ConfirmPathname))
			{
				MessageBox.Show("オンライン資格確認通知結果ファイルが存在しません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			try
			{
				using (XLWorkbook wb = new XLWorkbook(ConfirmPathname, XLEventTracking.Disabled))
				{
					// 元のカーソルを保持
					Cursor preCursor = Cursor.Current;

					// カーソルを待機カーソルに変更
					Cursor.Current = Cursors.WaitCursor;

					List<進捗管理表_NTT東日本> notice1EastList = new List<進捗管理表_NTT東日本>();
					List<進捗管理表_NTT西日本> notice1WestList = new List<進捗管理表_NTT西日本>();
					List<進捗管理表_NTT東日本> notice3List = new List<進捗管理表_NTT東日本>();
					List<進捗管理表_NTT西日本> notice4List = new List<進捗管理表_NTT西日本>();
					List<進捗管理表_NTT東日本> notice5EastList = new List<進捗管理表_NTT東日本>();
					List<進捗管理表_NTT西日本> notice5WestList = new List<進捗管理表_NTT西日本>();

					// 通知１：作業日決定を担当者へ通知
					if (checkBoxNotice1.Checked)
					{
						IXLWorksheet ws1_east = wb.Worksheet(SheetNameNotice1East);
						for (int i = 6; ; i++)
						{
							if ("" == ws1_east.Cell(i, 5).GetString())
							{
								break;
							}
							進捗管理表_NTT東日本 data = new 進捗管理表_NTT東日本();
							data.ReadWorksheetByオンライン資格確認通知結果(ws1_east, i, 進捗管理表_NTT東日本.Verion);
							notice1EastList.Add(data);
						}
						IXLWorksheet ws1_west = wb.Worksheet(SheetNameNotice1West);
						for (int i = 7; ; i++)
						{
							if ("" == ws1_west.Cell(i, 5).GetString())
							{
								break;
							}
							進捗管理表_NTT西日本 data = new 進捗管理表_NTT西日本();
							data.SetWorksheetByオンライン資格確認通知結果(ws1_west, i, 進捗管理表_NTT西日本.Verion);
							notice1WestList.Add(data);
						}
					}
					// 通知３：NTT東日本 ヒアリングシート修正アップデート通知
					if (checkBoxNotice3.Checked)
					{
						IXLWorksheet ws3 = wb.Worksheet(SheetNameNotice3East);
						for (int i = 6; ; i++)
						{
							if ("" == ws3.Cell(i, 5).GetString())
							{
								break;
							}
							進捗管理表_NTT東日本 data = new 進捗管理表_NTT東日本();
							data.ReadWorksheetByオンライン資格確認通知結果(ws3, i, 進捗管理表_NTT東日本.Verion);
							notice3List.Add(data);
						}
					}
					// 通知４：NTT西日本 ヒアリングシート修正アップデート通知
					if (checkBoxNotice4.Checked)
					{
						IXLWorksheet ws4 = wb.Worksheet(SheetNameNotice4West);
						for (int i = 7; ; i++)
						{
							if ("" == ws4.Cell(i, 5).GetString())
							{
								break;
							}
							進捗管理表_NTT西日本 data = new 進捗管理表_NTT西日本();
							data.SetWorksheetByオンライン資格確認通知結果(ws4, i, 進捗管理表_NTT西日本.Verion);
							notice4List.Add(data);
						}
					}
					// 通知５：工事日14日前でヒアリングシート未完成通知
					if (checkBoxNotice5.Checked)
					{
						IXLWorksheet ws5_east = wb.Worksheet(SheetNameNotice5East);
						for (int i = 6; ; i++)
						{
							if ("" == ws5_east.Cell(i, 5).GetString())
							{
								break;
							}
							進捗管理表_NTT東日本 data = new 進捗管理表_NTT東日本();
							data.ReadWorksheetByオンライン資格確認通知結果(ws5_east, i, 進捗管理表_NTT東日本.Verion);
							notice5EastList.Add(data);
						}
						IXLWorksheet ws5_west = wb.Worksheet(SheetNameNotice5West);
						for (int i = 7; ; i++)
						{
							if ("" == ws5_west.Cell(i, 5).GetString())
							{
								break;
							}
							進捗管理表_NTT西日本 data = new 進捗管理表_NTT西日本();
							data.SetWorksheetByオンライン資格確認通知結果(ws5_west, i, 進捗管理表_NTT西日本.Verion);
							notice5WestList.Add(data);
						}
					}
					if (0 < notice1EastList.Count + notice1WestList.Count + notice3List.Count + notice4List.Count + notice5EastList.Count + notice5WestList.Count)
					{
						// 通知１-NTT東日本
						foreach (進捗管理表_NTT東日本 east in notice1EastList)
						{
							if (east.Notice.IsEnableSendMail)
							{
								SendMailControl.Notice1East(Settings.Mail, east);
							}
						}
						// 通知１-NTT西日本
						foreach (進捗管理表_NTT西日本 west in notice1WestList)
						{
							if (west.Notice.IsEnableSendMail)
							{
								SendMailControl.Notice1West(Settings.Mail, west);
							}
						}
						// 通知３-NTT東日本
						foreach (進捗管理表_NTT東日本 east in notice3List)
						{
							if (east.Notice.IsEnableSendMail)
							{
								SendMailControl.Notice3(Settings.Mail, east);
							}
						}
						// 通知４-NTT西日本
						foreach (進捗管理表_NTT西日本 west in notice4List)
						{
							if (west.Notice.IsEnableSendMail)
							{
								SendMailControl.Notice4(Settings.Mail, west);
							}
						}
						// 通知５-NTT東日本
						foreach (進捗管理表_NTT東日本 east in notice5EastList)
						{
							if (east.Notice.IsEnableSendMail)
							{
								SendMailControl.Notice5East(Settings.Mail, east);
							}
						}
						// 通知５-NTT西日本
						foreach (進捗管理表_NTT西日本 west in notice5WestList)
						{
							if (west.Notice.IsEnableSendMail)
							{
								SendMailControl.Notice5West(Settings.Mail, west);
							}
						}
						MessageBox.Show("メール送信が終了しました。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else
					{
						MessageBox.Show("メールの送信対象はありませんでした。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					// カーソルを元に戻す
					Cursor.Current = preCursor;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// MIC連絡担当者の通知情報を取得
		/// </summary>
		/// <param name="病院ID">病院ID</param>
		/// <returns>通知情報</returns>
		private NoticeInfo GetNoticeInfo(int 病院ID)
		{
			vオンライン資格確認ユーザー hs = HearingSheetList.Find(p => p.顧客No == 病院ID);
			if (null != hs)
			{
				if (0 < hs.MIC連絡担当者.Length)
				{
					string name = hs.MIC連絡担当者.Replace(" ", "").Replace("　", "");
					List<tUser> userList = JunpDatabaseAccess.Select_tUser(string.Format("[fUsrBusho1] = '40' AND REPLACE([fUsrName], ' ', '') LIKE '{0}%'", name), "", Settings.ConnectJunp.ConnectionString);
					if (null != userList && 0 < userList.Count)
					{
						NoticeInfo notice = new NoticeInfo();
						notice.メール送信指示 = "●";
						notice.社員番号 = userList[0].fUsrID;
						notice.MIC連絡担当者 = userList[0].fUsrName;
						notice.MailAddress = userList[0].fUsrEmail;
						return notice;
					}
					else
					{
						NoticeInfo notice = new NoticeInfo();
						notice.MIC連絡担当者 = hs.MIC連絡担当者;
						return notice;
					}
				}
				else
				{
					NoticeInfo notice = new NoticeInfo();
					notice.MIC連絡担当者 = hs.更新者;
					return notice;
				}
			}
			return null;
		}

		/// <summary>
		/// NTT東日本 進捗管理表の読込
		/// </summary>
		/// <returns></returns>
		private List<進捗管理表_NTT東日本> Read進捗管理表_NTT東日本()
		{
			List<進捗管理表_NTT東日本> eastList = new List<進捗管理表_NTT東日本>();
			try
			{
				using (XLWorkbook wb = new XLWorkbook(ProgerssEastPathname, XLEventTracking.Disabled))
				{
					IXLWorksheet ws = wb.Worksheet("申込書兼進捗管理表");
					for (int i = 7; ; i++)
					{
						if ("" == ws.Cell(i, 1).GetString())
						{
							break;
						}
						進捗管理表_NTT東日本 data = new 進捗管理表_NTT東日本();
						data.SetWorksheetBy進捗管理表(ws, i, 進捗管理表_NTT東日本.Verion);
						eastList.Add(data);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return eastList;
		}

		/// <summary>
		/// NTT西日本 進捗管理表の読込
		/// </summary>
		/// <returns></returns>
		private List<進捗管理表_NTT西日本> Read進捗管理表_NTT西日本()
		{
			List<進捗管理表_NTT西日本> westList = new List<進捗管理表_NTT西日本>();
			try
			{
				using (XLWorkbook wbWest = new XLWorkbook(ProgerssWestPathname, XLEventTracking.Disabled))
				{
					IXLWorksheet ws = wbWest.Worksheet("申込書兼進捗管理表 (BS案)");
					for (int i = 8; ; i++)
					{
						if ("" == ws.Cell(i, 3).GetString())
						{
							break;
						}
						進捗管理表_NTT西日本 data = new 進捗管理表_NTT西日本();
						data.SetWorksheetBy進捗管理表(ws, i, 進捗管理表_NTT西日本.Verion);
						westList.Add(data);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return westList;
		}

		/// <summary>
		/// NTT西日本 連絡票の読込
		/// </summary>
		/// <returns></returns>
		private List<連絡票_NTT西日本> Read連絡票_NTT西日本()
		{
			if (File.Exists(ContactWestPathname))
			{
				List<連絡票_NTT西日本> contractList = new List<連絡票_NTT西日本>();
				try
				{
					using (XLWorkbook wb = new XLWorkbook(ContactWestPathname, XLEventTracking.Disabled))
					{
						IXLWorksheet ws = wb.Worksheet("連絡票一覧");
						for (int i = 5; ; i++)
						{
							if ("" == ws.Cell(i, 3).GetString())
							{
								break;
							}
							連絡票_NTT西日本 data = new 連絡票_NTT西日本();
							data.ReadWorksheet(ws, i);
							contractList.Add(data);
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				// NTT西日本進捗管理表のヒアリングシート修正依頼日とNTT西日本連絡票の依頼日が違う場合があり、NTT通番がユニークでないため、正しくマッチングできない
				// 連絡票を逆順にして最新の内容を検索するにする
				// ※ただし、依頼日に対し複数の連絡内容がある場合があるが、仕様上対応できていない
				contractList.Reverse();

				return contractList;
			}
			return null;
		}

		/// <summary>
		/// 通知１：作業日決定を担当者へ通知
		/// 東西進捗表の作業日列が埋まったら担当者へ通知
		/// 抽出条件
		/// 1. 進捗管理表の工事確定日(NTT東日本進捗管理表S列、NTT西日本進捗管理表I列)が設定
		/// 2. [SalesDB].[dbo].[進捗管理表_作業情報]に存在しない。または[SalesDB].[dbo].[進捗管理表_作業情報].[工事確定日]より未来の日付
		/// </summary>
		/// <param name="eastList">NTT東日本 進捗管理表</param>
		/// <param name="westList">NTT西日本 進捗管理表</param>
		/// <param name="wbOutput">オンライン資格確認通知結果.xlsx</param>
		private int Notice1(List<進捗管理表_NTT東日本> eastList, List<進捗管理表_NTT西日本> westList, XLWorkbook wbOutput)
		{
			// 進捗管理表_作業情報の取得
			List<進捗管理表_作業情報> progressList = SalesDatabaseAccess.Select_進捗管理表_作業情報("", "[顧客No]", Settings.ConnectSales.ConnectionString);

			int ret = 0;
			if (null != eastList)
			{
				// NTT東日本の通知
				IXLWorksheet ws = wbOutput.Worksheet(SheetNameNotice1East);
				int row = 6;
				foreach (進捗管理表_NTT東日本 east in eastList)
				{
					if (east.工事確定日付.HasValue)
					{
						進捗管理表_作業情報 data = progressList.Find(p => p.顧客No == east.病院ID);
						if (null == data)
						{
							// MIC連絡担当者の通知情報を取得
							NoticeInfo notice = GetNoticeInfo(east.病院ID);
							if (null != notice)
							{
								east.Notice = notice;
							}
							// シートに追加
							string[] record = east.GetData();
							for (int i = 0; i < record.Length; i++)
							{
								ws.Cell(row, i + 1).SetValue(record[i]);
							}
							row++;
							ret++;
						}
						else if (data.工事確定日.Value.ToDate() < east.工事確定日付.Value)
						{
							// MIC連絡担当者の通知情報を取得
							NoticeInfo notice = GetNoticeInfo(east.病院ID);
							if (null != notice)
							{
								east.Notice = notice;
							}
							// シートに追加
							string[] record = east.GetData();
							for (int i = 0; i < record.Length; i++)
							{
								ws.Cell(row, i + 1).SetValue(record[i]);
							}
							row++;
							ret++;
						}
					}
				}
			}
			if (null != westList)
			{
				// NTT西日本の通知
				IXLWorksheet ws = wbOutput.Worksheet(SheetNameNotice1West);
				int row = 7;
				foreach (進捗管理表_NTT西日本 west in westList)
				{
					if (west.工事確定日付.HasValue)
					{
						進捗管理表_作業情報 data = progressList.Find(p => p.顧客No == west.病院ID);
						if (null == data)
						{
							// MIC連絡担当者の通知情報を取得
							NoticeInfo notice = GetNoticeInfo(west.病院ID);
							if (null != notice)
							{
								west.Notice = notice;
							}
							// シートに追加
							string[] record = west.GetData();
							for (int i = 0; i < record.Length; i++)
							{
								ws.Cell(row, i + 1).SetValue(record[i]);
							}
							row++;
							ret++;
						}
						else if (data.工事確定日.Value.ToDate() < west.工事確定日付.Value)
						{
							// MIC連絡担当者の通知情報を取得
							NoticeInfo notice = GetNoticeInfo(west.病院ID);
							if (null != notice)
							{
								west.Notice = notice;
							}
							// シートに追加
							string[] record = west.GetData();
							for (int i = 0; i < record.Length; i++)
							{
								ws.Cell(row, i + 1).SetValue(record[i]);
							}
							row++;
							ret++;
						}
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// 通知２：提出漏れ通知
		/// 営業管理部に提出された案件が進捗表にない場合、NTTへ提出漏れとして営業管理部へ通知
		/// 抽出条件
		/// 1. [SalesDB].[dbo].[オン資格ヒアリングシート].[送信履歴]に記録がある
		/// 2. [SalesDB].[dbo].[オン資格ヒアリングシート].[顧客番号]に該当する番号が進捗管理表の病院ID(NTT東日本進捗管理表C列、NTT西日本進捗管理表G列)
		///    存在しない
		/// </summary>
		/// <param name="eastList">NTT東日本 進捗管理表</param>
		/// <param name="westList">NTT西日本 進捗管理表</param>
		/// <param name="wbOutput">オンライン資格確認通知結果.xlsx</param>
		private int Notice2(List<進捗管理表_NTT東日本> eastList, List<進捗管理表_NTT西日本> westList, XLWorkbook wbOutput)
		{
			int ret = 0;
			IXLWorksheet ws = wbOutput.Worksheet(SheetNameNotice2);
			int row = 4;

			if (null != eastList)
			{
				// NTT東日本の通知
				foreach (vオンライン資格確認ユーザー hs in HearingSheetList)
				{
					if (hs.IsNTT東日本管轄)
					{
						if (-1 == eastList.FindIndex(p => p.病院ID == hs.顧客No))
						{
							string[] record = hs.GetData();
							for (int i = 0; i < record.Length; i++)
							{
								ws.Cell(row, i + 1).SetValue(record[i]);
							}
							row++;
							ret++;
						}
					}
				}
			}
			if (null != westList)
			{
				// NTT西日本の通知
				foreach (vオンライン資格確認ユーザー hs in HearingSheetList)
				{
					if (false == hs.IsNTT東日本管轄)
					{
						if (-1 == westList.FindIndex(p => p.病院ID == hs.顧客No))
						{
							string[] record = hs.GetData();
							for (int i = 0; i < record.Length; i++)
							{
								ws.Cell(row, i + 1).SetValue(record[i]);
							}
							row++;
							ret++;
						}
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// 通知３：NTT東日本 ヒアリングシート修正アップデート通知
		/// NTTからヒアリングシートに不備があった場合、NTT東日本進捗管理表の本日の更新列に値が入っていて、かつBJ列or BL列（回答結果）
		/// がNGを検知してその内容を担当者へ通知
		/// 抽出条件
		/// 1. NTT東日本進捗管理表の本日の更新分(BI列)に進捗管理表の日付と同じ日付が設定
		/// 2. 回答結果(BJ列、BL列)にいずれかにNGが設定
		/// </summary>
		/// <param name="eastList">NTT東日本 進捗管理表</param>
		/// <param name="wbOutput">オンライン資格確認通知結果.xlsx</param>
		private int Notice3(List<進捗管理表_NTT東日本> eastList, XLWorkbook wbOutput)
		{
			int ret = 0;
			IXLWorksheet ws = wbOutput.Worksheet(SheetNameNotice3East);
			int row = 6;
			foreach (進捗管理表_NTT東日本 east in eastList)
			{
				if (east.本日の更新分日付.HasValue)
				{
					if (EastFileDate.Value == east.本日の更新分日付.Value)
					{
						if (east.回答結果1_NG || east.回答結果2_NG)
						{
							// MIC連絡担当者の通知情報を取得
							NoticeInfo notice = GetNoticeInfo(east.病院ID);
							if (null != notice)
							{
								east.Notice = notice;
							}
							// シートに追加
							string[] record = east.GetData();
							for (int i = 0; i < record.Length; i++)
							{
								ws.Cell(row, i + 1).SetValue(record[i]);
							}
							row++;
							ret++;
						}
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// 通知４：NTT西日本 ヒアリングシート修正アップデート通知
		/// NTT西日本進捗管理表にNTTから修正要請の行が追加されたら担当者に通知
		/// 抽出条件
		/// 1. NTT西日本進捗管理表のヒアリングシート修正依頼日(AP列)に進捗管理表の日付と同じ日付が設定
		/// </summary>
		/// <param name="westList">NTT西日本 進捗管理表</param>
		/// <param name="contractList">NTT西日本 連絡票</param>
		/// <param name="wbOutput">オンライン資格確認通知結果.xlsx</param>
		private int Notice4(List<進捗管理表_NTT西日本> westList, List<連絡票_NTT西日本> contractList, XLWorkbook wbOutput)
		{
			int ret = 0;
			IXLWorksheet ws = wbOutput.Worksheet(SheetNameNotice4West);
			int row = 7;
			foreach (進捗管理表_NTT西日本 west in westList)
			{
				if (west.ヒアリングシート修正依頼日付.HasValue)
				{
					if (WestFileDate.Value == west.ヒアリングシート修正依頼日付.Value)
					{
						// MIC連絡担当者の通知情報を取得
						NoticeInfo notice = GetNoticeInfo(west.病院ID);
						if (null != notice)
						{
							west.Notice = notice;
						}
						// シートに追加
						string[] record = west.GetData();
						for (int i = 0; i < record.Length; i++)
						{
							ws.Cell(row, i + 1).SetValue(record[i]);
						}
						if (null != contractList)
						{
							// NTT西日本進捗管理表のヒアリングシート修正依頼日とNTT西日本連絡票の依頼日が違う場合があり、NTT通番がユニークでないため、正しくマッチングできない
							//連絡票_NTT西日本 contract = contractList.Find(p => p.NTT通番 == west.受付通番 && p.依頼日付.Value == west.ヒアリングシート修正依頼日付.Value);
							連絡票_NTT西日本 contract = contractList.Find(p => p.NTT通番 == west.受付通番);
							if (null != contract)
							{
								ws.Cell(row, record.Length + 1).SetValue(contract.連絡項目);
								ws.Cell(row, record.Length + 2).SetValue(contract.連絡内容);
							}
						}
						row++;
						ret++;
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// 通知５：工事日14日前でヒアリングシート未完成通知
		/// 工事作業日14日前になってもヒアリングシートが完成していない場合担当者に通知
		/// 抽出条件
		/// 1. 工事確定日(NTT東日本：S列、NTT西日本：I列)の日が設定
		/// 2. ヒアリングシートが未完成(NTT東日本：BJ列とBL列のいずれかがNG、NTT西日本：AO列が空白)
		/// 3. 工事確定日が14日以内
		/// </summary>
		/// <param name="eastList">NTT東日本 進捗管理表</param>
		/// <param name="westList">NTT西日本 進捗管理表</param>
		/// <param name="contractList">NTT西日本 連絡票</param>
		/// <param name="wbOutput">オンライン資格確認通知結果.xlsx</param>
		private int Notice5(List<進捗管理表_NTT東日本> eastList, List<進捗管理表_NTT西日本> westList, List<連絡票_NTT西日本> contractList, XLWorkbook wbOutput)
		{
			int ret = 0;
			if (null != eastList)
			{
				// NTT東日本進捗管理表
				IXLWorksheet ws = wbOutput.Worksheet(SheetNameNotice5East);
				int row = 6;
				foreach (進捗管理表_NTT東日本 east in eastList)
				{
					Date? date = east.工事確定日付;
					if (date.HasValue)
					{
						if (east.回答結果1_NG || east.回答結果2_NG)
						{
							int a = date.Value - Date.Today;
							if (14 >= (date.Value - Date.Today))
							{
								// MIC連絡担当者の通知情報を取得
								NoticeInfo notice = GetNoticeInfo(east.病院ID);
								if (null != notice)
								{
									east.Notice = notice;
								}
								// シートに追加
								string[] record = east.GetData();
								for (int i = 0; i < record.Length; i++)
								{
									ws.Cell(row, i + 1).SetValue(record[i]);
								}
								row++;
								ret++;
							}
						}
					}
				}
			}
			if (null != westList)
			{
				// NTT西日本進捗管理表
				IXLWorksheet ws = wbOutput.Worksheet(SheetNameNotice5West);
				int row = 7;
				foreach (進捗管理表_NTT西日本 west in westList)
				{
					Date? date = west.工事確定日付;
					if (date.HasValue)
					{
						if (west.ヒアリングシートチェック結果_NG)
						{
							if (14 >= (date.Value - Date.Today))
							{
								// MIC連絡担当者の通知情報を取得
								NoticeInfo notice = GetNoticeInfo(west.病院ID);
								if (null != notice)
								{
									west.Notice = notice;
								}
								// シートに追加
								string[] record = west.GetData();
								for (int i = 0; i < record.Length; i++)
								{
									ws.Cell(row, i + 1).SetValue(record[i]);
								}
								if (null != contractList)
								{
									// NTT西日本進捗管理表のヒアリングシート修正依頼日とNTT西日本連絡票の依頼日が違う場合があり、NTT通番がユニークでないため、正しくマッチングできない
									//連絡票_NTT西日本 contract = contractList.Find(p => p.NTT通番 == west.受付通番 && p.依頼日付.Value == west.ヒアリングシート修正依頼日付.Value);
									連絡票_NTT西日本 contract = contractList.Find(p => p.NTT通番 == west.受付通番);
									if (null != contract)
									{
										ws.Cell(row, record.Length + 1).SetValue(contract.連絡項目);
										ws.Cell(row, record.Length + 2).SetValue(contract.連絡内容);
									}
								}
								row++;
								ret++;
							}
						}
					}
				}
			}
			return ret;
		}

		/// <summary>
		/// 進捗管理表_作業情報の更新
		/// </summary>
		/// <param name="eastList">進捗管理表_NTT東日本</param>
		/// <param name="westList">進捗管理表_NTT西日本</param>
		private void UpdateProgress(List<進捗管理表_NTT東日本> eastList, List<進捗管理表_NTT西日本> westList)
		{
			List<進捗管理表_作業情報> list = new List<進捗管理表_作業情報>();
			if (null != eastList)
			{
				// NTT東日本進捗管理表
				foreach (進捗管理表_NTT東日本 east in eastList)
				{
					Date? date = east.工事確定日付;
					if (date.HasValue)
					{
						進捗管理表_作業情報 data = new 進捗管理表_作業情報();
						data.顧客No = east.病院ID;
						data.工事確定日 = date.Value.ToDateTime();
						data.進捗管理表ファイル名 = ProgerssEastFilename;
						data.更新日 = DateTime.Now;
						list.Add(data);
					}
				}
			}
			if (null != westList)
			{
				// NTT西日本進捗管理表
				foreach (進捗管理表_NTT西日本 west in westList)
				{
					Date? date = west.工事確定日付;
					if (date.HasValue)
					{
						進捗管理表_作業情報 data = new 進捗管理表_作業情報();
						data.顧客No = west.病院ID;
						data.工事確定日 = date.Value.ToDateTime();
						data.進捗管理表ファイル名 = ProgerssWestFilename;
						data.更新日 = DateTime.Now;
						list.Add(data);
					}
				}
			}
#if false == DEBUG
			if (0 < list.Count)
			{
				// 進捗管理表_作業情報の取得
				List<進捗管理表_作業情報> progressList = SalesDatabaseAccess.Select_進捗管理表_作業情報("", "[顧客No]", Settings.ConnectSales.ConnectionString);
				if (0 < progressList.Count)
				{
					foreach (進捗管理表_作業情報 newData in list)
					{
						進捗管理表_作業情報 orgData = progressList.Find(p => p.顧客No == newData.顧客No);
						if (null == orgData)
						{
							try
							{
								SalesDatabaseAccess.InsertInto_進捗管理表_作業情報(newData, Settings.ConnectSales.ConnectionString);
							}
							catch (Exception ex)
							{
								throw new ApplicationException(string.Format("{0} 顧客No:{1}", ex.Message, newData.顧客No));
							}
						}
						else if (orgData.工事確定日.Value.ToDate() < newData.工事確定日.Value.ToDate())
						{
							try
							{
								SalesDatabaseAccess.UpdateSet_進捗管理表_作業情報(newData, Settings.ConnectSales.ConnectionString);
							}
							catch (Exception ex)
							{
								throw new ApplicationException(string.Format("{0} 顧客No:{1}", ex.Message, newData.顧客No));
							}
						}
					}
				}
				else
				{
					SalesDatabaseAccess.InsertIntoList_進捗管理表_作業情報(list, Settings.ConnectSales.ConnectionString);
				}
			}
#endif
		}
	}
}
