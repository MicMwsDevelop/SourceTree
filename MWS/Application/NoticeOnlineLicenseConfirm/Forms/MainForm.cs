//
// MainForm.cs
// 
// メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/03/10 勝呂)
// Ver1.06 通知５の判定を本日以降の工事確定日付のみ検索するように抽出条件を変更(2022/05/17 勝呂)
// Ver1.11 NTT現調プランに対応(2022/08/29 勝呂)
// Ver1.12 進捗管理表_作業情報に受付通番、工事結果、工事結果格納日時のフィールド追加に対応(2022/09/13 勝呂)
// Ver1.14 現調及び工事の通知チェック後に設定する連絡用チェックボックスの制御が一部正しくなかった(2022/12/07 勝呂)
//
using ClosedXML.Excel;
using CommonLib.BaseFactory.Sales.View;
using CommonLib.Common;
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
		/// 環境設定
		/// </summary>
		public NoticeOnlineLicenseConfirmSettings Settings { get; set; }

		/// <summary>
		/// vオンライン資格確認ユーザーリスト
		/// </summary>
		public List<vオンライン資格確認ユーザー> WebHearingSheet { get; set; }

		/// <summary>
		/// NTT東日本 進捗管理表ファイル名
		/// </summary>
		public string ProgEastFname { get; set; }

		/// <summary>
		/// NTT西日本 進捗管理表ファイル名
		/// </summary>
		public string ProgWestFname { get; set; }

		/// <summary>
		/// NTT東日本 進捗管理表パス名
		/// </summary>
		public string ProgEastPname { get; set; }

		/// <summary>
		/// NTT西日本 進捗管理表パス名
		/// </summary>
		public string ProgWestPname { get; set; }

		/// <summary>
		/// NTT西日本 連絡票パス名
		/// </summary>
		public string ContactWestPname { get; set; }

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
		public string ConfirmFname { get; set; }

		/// <summary>
		/// オンライン資格確認通知結果パス名
		/// </summary>
		public string ConfirmPname { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			WebHearingSheet = null;
			ProgEastFname = string.Empty;
			ProgWestFname = string.Empty;
			ProgEastPname = string.Empty;
			ProgWestPname = string.Empty;
			EastFileDate = null;
			WestFileDate = null;
			ConfirmFname = string.Empty;
			ConfirmPname = string.Empty;
			ContactWestPname = string.Empty;
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

#if DEBUG
			this.Text = this.Text + " " + Settings.ConnectJunp.InstanceName;
#endif

			try
			{
				// Webヒアリングシートの取得
				// Ver1.10 Webヒアリングシート現調対応(2022/08/03 勝呂)
				//HearingSheet = SalesDatabaseAccess.Select_vオンライン資格確認ユーザー("[送信履歴] is not null", "[顧客番号]", Settings.ConnectSales.ConnectionString);
				WebHearingSheet = SalesDatabaseAccess.Select_vオンライン資格確認ユーザー("", "[顧客番号]", Settings.ConnectSales.ConnectionString);
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
				ConfirmFname = xlsFilename;
				ConfirmPname = xlsPathname;
				textBoxConfirmFile.Text = ConfirmFname;
			}
#if DEBUG
			checkBoxTestMail.Checked = true;
#endif
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
					ProgEastFname = Path.GetFileName(dlg.FileName);
					textBoxProgressEast.Text = ProgEastFname;
					ProgEastPname = dlg.FileName;
					string dateStr = Regex.Replace(ProgEastFname, @"[^0-9]", "");
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
					ProgWestFname = Path.GetFileName(dlg.FileName);
					textBoxProgressWest.Text = ProgWestFname;
					ProgWestPname = dlg.FileName;
					string dateStr = Regex.Replace(ProgWestFname, @"[^0-9]", "");
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
					ContactWestPname = dlg.FileName;
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
			if (0 < ProgEastFname.Length)
			{
				eastOK = true;
			}
			if (0 < ProgWestFname.Length)
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
				if (false == EastFileDate.HasValue)
				{
					MessageBox.Show("NTT東日本の進捗管理表のファイル名の日付が正しくありません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
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
				if (false == WestFileDate.HasValue)
				{
					MessageBox.Show("NTT西日本の進捗管理表のファイル名の日付が正しくありません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (WestFileDate.HasValue && WestFileDate.Value.ToDateTime() != dateTimePickerWest.Value)
				{
					if (DialogResult.No == MessageBox.Show("NTT西日本 進捗管理表の更新日とファイル作成日に相違がありますが、よろしいですか？", Program.ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					{
						return;
					}
				}
				if (0 == ContactWestPname.Length)
				{
					if (DialogResult.No == MessageBox.Show("NTT西日本 連絡票が設定されていませんが、よろしいですか？\n\n通知３と通知４の連絡項目と連絡内容は未出力となります。", Program.ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
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
				ConfirmFname = string.Format("オンライン資格確認通知結果_{0}.xlsx", Date.Today.GetNumeralString());
				ConfirmPname = Path.Combine(Directory.GetCurrentDirectory(), ConfirmFname);
				File.Copy(orgPpathname, ConfirmPname, true);

				// オンライン資格確認通知結果ファイルの設定
				textBoxConfirmFile.Text = ConfirmFname;

				///////////////////////////////////////////////
				/// オン資現調

				// 通知数を設定
				labelResearch1.Text = "通知１：0件";
				labelResearch2.Text = "通知２：0件";
				labelResearch3.Text = "通知３：0件";
				labelResearch4.Text = "通知４：0件";
				int research1East = 0;
				int research1West = 0;
				int research2 = 0;
				int research3East = 0;
				int research3West = 0;
				int research4East = 0;
				int research4West = 0;
				checkBoxResearch1East.Checked = false;
				checkBoxResearch1West.Checked = false;
				checkBoxResearch3East.Checked = false;
				checkBoxResearch3West.Checked = false;
				checkBoxResearch4East.Checked = false;
				checkBoxResearch4West.Checked = false;


				///////////////////////////////////////////////
				/// オン資工事

				// 通知数を設定
				labelConstruct1.Text = "通知１：0件";
				labelConstruct2.Text = "通知２：0件";
				labelConstruct3.Text = "通知３：0件";
				labelConstruct4.Text = "通知４：0件";
				int constrct1East = 0;
				int constrct1West = 0;
				int constrct2 = 0;
				int constrct3East = 0;
				int constrct3West = 0;
				int constrct4East = 0;
				int constrct4West = 0;
				checkBoxConstruct1East.Checked = false;
				checkBoxConstruct1West.Checked = false;
				checkBoxConstruct3East.Checked = false;
				checkBoxConstruct3West.Checked = false;
				checkBoxConstruct4East.Checked = false;
				checkBoxConstruct4West.Checked = false;

				using (XLWorkbook wb = new XLWorkbook(ConfirmPname, XLEventTracking.Disabled))
				{
					string msg;
					if (0 < ProgEastFname.Length && 0 < ProgWestFname.Length)
					{
						// NTT東日本とNTT西日本の通知
						List<進捗管理表_NTT東日本> eastList = 進捗管理表_NTT東日本.ReadProgressExcelFile(ProgEastPname, out msg);
						if (null == eastList)
						{
							MessageBox.Show(msg, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}
						List<進捗管理表_NTT西日本> westList = 進捗管理表_NTT西日本.ReadProgressExcelFile(ProgWestPname, out msg);
						if (null == westList)
						{
							MessageBox.Show(msg, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}
						List<連絡票_NTT西日本> contractList = 連絡票_NTT西日本.ReadContactExcelFile(ContactWestPname, out msg);
						if (null == contractList && 0 < msg.Length)
						{
							MessageBox.Show(msg, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}

						///////////////////////////////////////////////
						/// オン資現調

						// 現調通知１：現地調査確定日の連絡（NTT東日本）
						research1East = NoticeResearch.Notice1East(ProgEastFname, WebHearingSheet, eastList, wb, Settings.ConnectSales.ConnectionString);

						// 現調通知１：現地調査確定日の連絡（NTT西日本）
						research1West = NoticeResearch.Notice1West(ProgWestFname, WebHearingSheet, westList, wb, Settings.ConnectSales.ConnectionString);

						// 工事通知２：提出漏れ通知
						research2 = NoticeResearch.Notice2(WebHearingSheet, eastList, westList, wb);

						// 現調通知３：現調結果の連絡（NTT東日本）
						research3East = NoticeResearch.Notice3East(ProgEastFname, WebHearingSheet, eastList, wb, Settings.ConnectSales.ConnectionString);

						// 現調通知３：現調結果の連絡（NTT西日本）
						research3West = NoticeResearch.Notice3West(ProgWestFname, WebHearingSheet, westList, wb, Settings.ConnectSales.ConnectionString);

						// 現調通知４：新規案件出し忘れの連絡（NTT東日本）
						research4East = NoticeResearch.Notice4East(WebHearingSheet, eastList, wb, Settings.ConnectSales.ConnectionString);

						// 現調通知４：新規案件出し忘れの連絡（NTT西日本）
						research4West = NoticeResearch.Notice4West(WebHearingSheet, westList, wb, Settings.ConnectSales.ConnectionString);


						///////////////////////////////////////////////
						/// オン資工事

						// 工事通知１：工事確定日の連絡（NTT東日本）
						constrct1East = NoticeConstruct.Notice1East(ProgEastFname, WebHearingSheet, eastList, wb, Settings.ConnectSales.ConnectionString);

						// 工事通知１：工事確定日の連絡（NTT西日本）
						constrct1West = NoticeConstruct.Notice1West(ProgWestFname, WebHearingSheet, westList, wb, Settings.ConnectSales.ConnectionString);

						// 工事通知２：提出漏れ通知
						constrct2 = NoticeConstruct.Notice2(WebHearingSheet, eastList, westList, wb);

						// 工事通知３：ヒアリングシート不備の連絡（NTT東日本）
						constrct3East = NoticeConstruct.Notice3East(WebHearingSheet, eastList, EastFileDate, wb, Settings.ConnectSales.ConnectionString);

						// 工事通知３：ヒアリングシート不備の連絡（NTT西日本）
						constrct3West = NoticeConstruct.Notice3West(WebHearingSheet, westList, WestFileDate, contractList, wb, Settings.ConnectSales.ConnectionString);

						// 工事通知４：工事確定日14日前ヒアリングシート未完成の連絡（NTT東日本）
						constrct4East = NoticeConstruct.Notice4East(WebHearingSheet, eastList, wb, Settings.ConnectSales.ConnectionString);

						// 工事通知４：工事確定日14日前ヒアリングシート未完成の連絡（NTT西日本）
						constrct4West = NoticeConstruct.Notice4West(WebHearingSheet, westList, contractList, wb, Settings.ConnectSales.ConnectionString);

						// NTT東日本 工事結果の設定
						// Ver1.12 進捗管理表_作業情報に受付通番、工事結果、工事結果格納日時のフィールド追加に対応(2022/09/13 勝呂)
						NoticeConstruct.SetEastConstrctionResult(ProgEastFname, eastList, Settings.ConnectSales.ConnectionString);

						// NTT西日本 工事結果の設定
						// Ver1.12 進捗管理表_作業情報に受付通番、工事結果、工事結果格納日時のフィールド追加に対応(2022/09/13 勝呂)
						NoticeConstruct.SetWestConstrctionResult(ProgWestFname, westList, Settings.ConnectSales.ConnectionString);
					}
					else if (0 < ProgEastFname.Length && 0 == ProgWestFname.Length)
					{
						// NTT東日本の通知
						List<進捗管理表_NTT東日本> eastList = 進捗管理表_NTT東日本.ReadProgressExcelFile(ProgEastPname, out msg);
						if (null == eastList)
						{
							MessageBox.Show(msg, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}
						///////////////////////////////////////////////
						/// オン資現調

						// 現調通知１：現地調査確定日の連絡（NTT東日本）
						research1East = NoticeResearch.Notice1East(ProgEastFname, WebHearingSheet, eastList, wb, Settings.ConnectSales.ConnectionString);

						// 工事通知２：提出漏れ通知
						research2 = NoticeResearch.Notice2(WebHearingSheet, eastList, null, wb);

						// 現調通知３：現調結果の連絡（NTT東日本）
						research3East = NoticeResearch.Notice3East(ProgEastFname, WebHearingSheet, eastList, wb, Settings.ConnectSales.ConnectionString);

						// 現調通知４：新規案件出し忘れの連絡（NTT東日本）
						research4East = NoticeResearch.Notice4East(WebHearingSheet, eastList, wb, Settings.ConnectSales.ConnectionString);

						///////////////////////////////////////////////
						/// オン資工事

						// 工事通知１：工事確定日の連絡（NTT東日本）
						constrct1East = NoticeConstruct.Notice1East(ProgEastFname, WebHearingSheet, eastList, wb, Settings.ConnectSales.ConnectionString);

						// 工事通知２：提出漏れ通知
						constrct2 = NoticeConstruct.Notice2(WebHearingSheet, eastList, null, wb);

						// 工事通知３：ヒアリングシート不備の連絡（NTT東日本）
						constrct3East = NoticeConstruct.Notice3East(WebHearingSheet, eastList, EastFileDate, wb, Settings.ConnectSales.ConnectionString);

						// 工事通知４：工事確定日14日前ヒアリングシート未完成の連絡（NTT東日本）
						constrct4East = NoticeConstruct.Notice4East(WebHearingSheet, eastList, wb, Settings.ConnectSales.ConnectionString);

						// NTT東日本 工事結果の設定
						// Ver1.12 進捗管理表_作業情報に受付通番、工事結果、工事結果格納日時のフィールド追加に対応(2022/09/13 勝呂)
						NoticeConstruct.SetEastConstrctionResult(ProgEastFname, eastList, Settings.ConnectSales.ConnectionString);
					}
					else
					{
						// NTT西日本の通知
						List<進捗管理表_NTT西日本> westList = 進捗管理表_NTT西日本.ReadProgressExcelFile(ProgWestPname, out msg);
						if (null == westList)
						{
							MessageBox.Show(msg, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}
						List<連絡票_NTT西日本> contractList = 連絡票_NTT西日本.ReadContactExcelFile(ContactWestPname, out msg);
						if (null == contractList && 0 < msg.Length)
						{
							MessageBox.Show(msg, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
							return;
						}

						///////////////////////////////////////////////
						/// オン資現調

						// 現調通知１：現地調査確定日の連絡（NTT西日本）
						research1West += NoticeResearch.Notice1West(ProgWestFname, WebHearingSheet, westList, wb, Settings.ConnectSales.ConnectionString);

						// 現調通知２：提出漏れ通知
						research2 = NoticeResearch.Notice2(WebHearingSheet, null, westList, wb);

						// 現調通知３：現調結果の連絡（NTT西日本）
						research3West = NoticeResearch.Notice3West(ProgWestFname, WebHearingSheet, westList, wb, Settings.ConnectSales.ConnectionString);

						// 現調通知４：新規案件出し忘れの連絡（NTT西日本）
						research4West = NoticeResearch.Notice4West(WebHearingSheet, westList, wb, Settings.ConnectSales.ConnectionString);

						///////////////////////////////////////////////
						/// オン資工事

						// 工事通知１：工事確定日の連絡（NTT西日本）
						constrct1West = NoticeConstruct.Notice1West(ProgWestFname, WebHearingSheet, westList, wb, Settings.ConnectSales.ConnectionString);

						// 工事通知２：提出漏れ通知
						constrct2 = NoticeConstruct.Notice2(WebHearingSheet, null, westList, wb);

						// 工事通知３：ヒアリングシート不備の連絡（NTT西日本）
						constrct3West = NoticeConstruct.Notice3West(WebHearingSheet, westList, WestFileDate, contractList, wb, Settings.ConnectSales.ConnectionString);

						// 工事通知４：工事確定日14日前ヒアリングシート未完成の連絡（NTT西日本）
						constrct4West = NoticeConstruct.Notice4West(WebHearingSheet, westList, contractList, wb, Settings.ConnectSales.ConnectionString);

						// NTT西日本 工事結果の設定
						// Ver1.12 進捗管理表_作業情報に受付通番、工事結果、工事結果格納日時のフィールド追加に対応(2022/09/13 勝呂)
						NoticeConstruct.SetWestConstrctionResult(ProgWestFname, westList, Settings.ConnectSales.ConnectionString);
					}
					// カーソルを元に戻す
					Cursor.Current = preCursor;

					// Ver1.14 現調及び工事の通知チェック後に設定する連絡用チェックボックスの制御が一部正しくなかった(2022/12/07 勝呂)
					if (0 < research1East + research1West + research2 + research3East + research3West + research4East + research4West + constrct1East + constrct1West + constrct2 + constrct3East + constrct3West + constrct4East + constrct4West)
					{
						// Excelファイルの保存
						wb.Save();

						///////////////////////////////////////////////
						/// オン資現調

						labelResearch1.Text = string.Format("通知１：{0}件", research1East + research1West);
						labelResearch2.Text = string.Format("通知２：{0}件", research2);
						labelResearch3.Text = string.Format("通知３：{0}件", research3East + research3West);
						labelResearch4.Text = string.Format("通知４：{0}件", research4East + research4West);
						if (0 < research1East)
						{
							checkBoxResearch1East.Checked = true;
						}
						if (0 < research1West)
						{
							checkBoxResearch1West.Checked = true;
						}
						if (0 < research3East)
						{
							checkBoxResearch3East.Checked = true;
						}
						if (0 < research3West)
						{
							checkBoxResearch3West.Checked = true;
						}
						if (0 < research4East)
						{
							checkBoxResearch4East.Checked = true;
						}
						if (0 < research4West)
						{
							checkBoxResearch4West.Checked = true;
						}

						///////////////////////////////////////////////
						/// オン資工事

						labelConstruct1.Text = string.Format("通知１：{0}件", constrct1East + constrct1West);
						labelConstruct2.Text = string.Format("通知２：{0}件", constrct2);
						labelConstruct3.Text = string.Format("通知３：{0}件", constrct3East + constrct3West);
						labelConstruct4.Text = string.Format("通知４：{0}件", constrct4East + constrct4West);
						if (0 < constrct1East)
						{
							checkBoxConstruct1East.Checked = true;
						}
						if (0 < constrct1West)
						{
							checkBoxConstruct1West.Checked = true;
						}
						if (0 < constrct3East)
						{
							checkBoxConstruct3East.Checked = true;
						}
						if (0 < constrct3West)
						{
							checkBoxConstruct3West.Checked = true;
						}
						if (0 < constrct4East)
						{
							checkBoxConstruct4East.Checked = true;
						}
						if (0 < constrct4West)
						{
							checkBoxConstruct4West.Checked = true;
						}
						MessageBox.Show(string.Format("【現調】\n通知１：{0}件\n通知２：{1}件\n通知３：{2}件\n通知４：{3}件\n\n【工事】\n通知１：{4}件\n通知２：{5}件\n通知３：{6}件\n通知４：{7}件\n\n{8}を確認してください。", research1East + research1West, research2, research3East + research3West, research4East + research4West, constrct1East + constrct1West, constrct2, constrct3East + constrct3West, constrct4East + constrct4West, ConfirmFname), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
					ConfirmFname = Path.GetFileName(dlg.FileName);
					ConfirmPname = dlg.FileName;
					textBoxConfirmFile.Text = ConfirmFname;
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
			if (0 == ConfirmPname.Length || false == File.Exists(ConfirmPname))
			{
				MessageBox.Show("オンライン資格確認通知結果ファイルが存在しません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			try
			{
				using (XLWorkbook wb = new XLWorkbook(ConfirmPname, XLEventTracking.Disabled))
				{
					// 元のカーソルを保持
					Cursor preCursor = Cursor.Current;

					// カーソルを待機カーソルに変更
					Cursor.Current = Cursors.WaitCursor;

					///////////////////////////////////////////////
					/// オン資現調

					// 現調通知１：現地調査確定日の連絡（NTT東日本）
					List<進捗管理表_NTT東日本> research1EastList = new List<進捗管理表_NTT東日本>();
					if (checkBoxResearch1East.Checked)
					{
						NoticeResearch.GetEastOutputRecordList(wb, NoticeResearch.SheetNameResearch1East, research1EastList);
					}
					// 現調通知１：現地調査確定日の連絡（NTT西日本）
					List<進捗管理表_NTT西日本> research1WestList = new List<進捗管理表_NTT西日本>();
					if (checkBoxResearch1West.Checked)
					{
						NoticeResearch.GetWestOutputRecordList(wb, NoticeResearch.SheetNameResearch1West, research1WestList);
					}
					// 現調通知３：現調結果の連絡（NTT東日本）
					List<進捗管理表_NTT東日本> research3EastList = new List<進捗管理表_NTT東日本>();
					if (checkBoxResearch3East.Checked)
					{
						NoticeResearch.GetEastOutputRecordList(wb, NoticeResearch.SheetNameResearch3East, research3EastList);
					}
					// 現調通知３：現調結果の連絡（NTT西日本）
					List<進捗管理表_NTT西日本> research3WestList = new List<進捗管理表_NTT西日本>();
					if (checkBoxResearch3West.Checked)
					{
						NoticeResearch.GetWestOutputRecordList(wb, NoticeResearch.SheetNameResearch3West, research3WestList);
					}
					// 現調通知４：新規案件出し忘れの連絡（NTT東日本）
					List<進捗管理表_NTT東日本> research4EastList = new List<進捗管理表_NTT東日本>();
					if (checkBoxResearch4East.Checked)
					{
						NoticeResearch.GetEastOutputRecordList(wb, NoticeResearch.SheetNameResearch4East, research4EastList);
					}
					// 現調通知４：新規案件出し忘れの連絡（NTT西日本）
					List<進捗管理表_NTT西日本> research4WestList = new List<進捗管理表_NTT西日本>();
					if (checkBoxResearch4West.Checked)
					{
						NoticeResearch.GetWestOutputRecordList(wb, NoticeResearch.SheetNameResearch4West, research4WestList);
					}

					///////////////////////////////////////////////
					/// オン資工事

					// 工事通知１：工事確定日の連絡（NTT東日本）
					List<進捗管理表_NTT東日本> construct1EastList = new List<進捗管理表_NTT東日本>();
					if (checkBoxConstruct1East.Checked)
					{
						NoticeConstruct.GetEastOutputRecordList(wb, NoticeConstruct.SheetNameConstruct1East, construct1EastList);
					}
					// 工事通知１：工事確定日の連絡（NTT西日本）
					List<進捗管理表_NTT西日本> construct1WestList = new List<進捗管理表_NTT西日本>();
					if (checkBoxConstruct1West.Checked)
					{
						NoticeConstruct.GetWestOutputRecordList(wb, NoticeConstruct.SheetNameConstruct1West, construct1WestList);
					}
					// 工事通知３：ヒアリングシート不備の連絡（NTT東日本）
					List<進捗管理表_NTT東日本> construct3EastList = new List<進捗管理表_NTT東日本>();
					if (checkBoxConstruct3East.Checked)
					{
						NoticeConstruct.GetEastOutputRecordList(wb, NoticeConstruct.SheetNameConstruct3East, construct3EastList);
					}
					// 工事通知３：ヒアリングシート不備の連絡（NTT西日本）
					List<進捗管理表_NTT西日本> construct3WestList = new List<進捗管理表_NTT西日本>();
					if (checkBoxConstruct3West.Checked)
					{
						NoticeConstruct.GetWestOutputRecordList(wb, NoticeConstruct.SheetNameConstruct3West, construct3WestList);
					}
					// 工事通知４：工事確定日14日前ヒアリングシート未完成の連絡（NTT東日本）
					List<進捗管理表_NTT東日本> construct4EastList = new List<進捗管理表_NTT東日本>();
					if (checkBoxConstruct4East.Checked)
					{
						NoticeConstruct.GetEastOutputRecordList(wb, NoticeConstruct.SheetNameConstruct4East, construct4EastList);
					}
					// 工事通知４：工事確定日14日前ヒアリングシート未完成の連絡（NTT西日本）
					List<進捗管理表_NTT西日本> construct4WestList = new List<進捗管理表_NTT西日本>();
					if (checkBoxConstruct4West.Checked)
					{
						NoticeConstruct.GetWestOutputRecordList(wb, NoticeConstruct.SheetNameConstruct4West, construct4WestList);
					}
					if (0 < research1EastList.Count + research1WestList.Count + research3EastList.Count + research3WestList.Count + research4EastList.Count + research4WestList.Count + construct1EastList.Count + construct1WestList.Count + construct3EastList.Count + construct3WestList.Count + construct4EastList.Count + construct4WestList.Count)
					{
						///////////////////////////////////////////////
						/// オン資現調

						// 現調通知１：現地調査確定日の連絡（NTT東日本）
						foreach (進捗管理表_NTT東日本 east in research1EastList)
						{
							if (east.Notice.IsEnableSendMail)
							{
								MailKitControl.Research1East(Settings.Mail, east, checkBoxTestMail.Checked);
							}
						}
						// 現調通知１：現地調査確定日の連絡（NTT西日本）
						foreach (進捗管理表_NTT西日本 west in research1WestList)
						{
							if (west.Notice.IsEnableSendMail)
							{
								MailKitControl.Research1West(Settings.Mail, west, checkBoxTestMail.Checked);
							}
						}
						// 現調通知３：現調結果の連絡（NTT東日本）
						foreach (進捗管理表_NTT東日本 east in research3EastList)
						{
							if (east.Notice.IsEnableSendMail)
							{
								MailKitControl.Research3East(Settings.Mail, east, checkBoxTestMail.Checked);
							}
						}
						// 現調通知３：現調結果の連絡（NTT西日本）
						foreach (進捗管理表_NTT西日本 west in research3WestList)
						{
							if (west.Notice.IsEnableSendMail)
							{
								MailKitControl.Research3West(Settings.Mail, west, checkBoxTestMail.Checked);
							}
						}
						// 現調通知４：新規案件出し忘れの連絡（NTT東日本）
						foreach (進捗管理表_NTT東日本 east in research4EastList)
						{
							if (east.Notice.IsEnableSendMail)
							{
								MailKitControl.Research4East(Settings.Mail, east, checkBoxTestMail.Checked);
							}
						}
						// 現調通知４：新規案件出し忘れの連絡（NTT西日本）
						foreach (進捗管理表_NTT西日本 west in research4WestList)
						{
							if (west.Notice.IsEnableSendMail)
							{
								MailKitControl.Research4West(Settings.Mail, west, checkBoxTestMail.Checked);
							}
						}

						///////////////////////////////////////////////
						/// オン資工事

						// 工事通知１：工事確定日の連絡（NTT東日本）
						foreach (進捗管理表_NTT東日本 east in construct1EastList)
						{
							if (east.Notice.IsEnableSendMail)
							{
								MailKitControl.Construct1East(Settings.Mail, east, checkBoxTestMail.Checked);
							}
						}
						// 工事通知１：工事確定日の連絡（NTT西日本）
						foreach (進捗管理表_NTT西日本 west in construct1WestList)
						{
							if (west.Notice.IsEnableSendMail)
							{
								MailKitControl.Construct1West(Settings.Mail, west, checkBoxTestMail.Checked);
							}
						}
						// 工事通知３：ヒアリングシート不備の連絡（NTT東日本）
						foreach (進捗管理表_NTT東日本 east in construct3EastList)
						{
							if (east.Notice.IsEnableSendMail)
							{
								MailKitControl.Construct3East(Settings.Mail, east, checkBoxTestMail.Checked);
							}
						}
						// 工事通知３：ヒアリングシート不備の連絡（NTT西日本）
						foreach (進捗管理表_NTT西日本 west in construct3WestList)
						{
							if (west.Notice.IsEnableSendMail)
							{
								MailKitControl.Construct3West(Settings.Mail, west, checkBoxTestMail.Checked);
							}
						}
						// 工事通知４：工事確定日14日前ヒアリングシート未完成の連絡（NTT東日本）
						foreach (進捗管理表_NTT東日本 east in construct4EastList)
						{
							if (east.Notice.IsEnableSendMail)
							{
								MailKitControl.Construct4East(Settings.Mail, east, checkBoxTestMail.Checked);
							}
						}
						// 工事通知４：工事確定日14日前ヒアリングシート未完成の連絡（NTT西日本）
						foreach (進捗管理表_NTT西日本 west in construct4WestList)
						{
							if (west.Notice.IsEnableSendMail)
							{
								MailKitControl.Construct4West(Settings.Mail, west, checkBoxTestMail.Checked);
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
	}
}
