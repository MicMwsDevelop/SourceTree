//
// MainForm.cs
// 
// オンライン資格確認導入状況 メイン画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/08/29 勝呂)
//
using CommonLib.BaseFactory.Sales.Table;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Sales;
using OnlineLicenseIntroductionStatus.BaseFactory;
using OnlineLicenseIntroductionStatus.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace OnlineLicenseIntroductionStatus.Forms
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// オンライン資格確認進捗管理ファイル名
		/// </summary>
		private const string ProgressManagementFilename = "オンライン資格確認進捗管理.xlsx";

		/// <summary>
		/// オンライン資格確認導入状況ファイル名
		/// </summary>
		private const string IntroductionStatusFilename = "オンライン資格確認導入状況";

		/// <summary>
		/// 環境設定
		/// </summary>
		public OnlineLicenseIntroductionStatusSettings Settings { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			// プログラムタイトル設定
			this.Text = string.Format("{0} ({1})", Program.ProgramName, Program.ProgramVersion);

			// 環境設定の読込
			Settings = OnlineLicenseIntroductionStatusSettingsIF.GetSettings();

#if DEBUG
			// 進捗管理ファイル名の設定
			textBoxProgress.Text = Path.Combine(Directory.GetCurrentDirectory(), ProgressManagementFilename);
#endif
		}

		/// <summary>
		/// 進捗管理ファイルの指定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonProgress_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "EXCELファイル(*.xlsx)|*.xlsx|すべてのファイル(*.*)|*.*";
				dlg.Title = "進捗管理ファイルを選択してください";
				if (DialogResult.OK == dlg.ShowDialog())
				{
					textBoxProgress.Text = dlg.FileName;
				}
			}
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonFileOut_Click(object sender, EventArgs e)
		{
			if (0 == textBoxProgress.Text.Length)
			{
				MessageBox.Show("進捗管理ファイルを設定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (false == checkBoxDatabase.Checked)
			{
				if (DialogResult.No == MessageBox.Show("進捗管理情報をデータベースに登録しません。よろしいですか？", Program.ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
				{
					return;
				}
			}
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			// 進捗管理ファイルから全顧客のリストを取得する
			string msg;
			List<ClinicProgress> clinicList = ClinicProgress.ReadProgressExcelFile(textBoxProgress.Text, out msg);
			if (null == clinicList)
			{
				MessageBox.Show(msg, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// オフィス毎の導入状況を集計する
			StatusJudgement judge = new StatusJudgement();
			judge.SetStatusJudgement(Settings);

			// 各オフィス毎の導入状況総計の取得
			List<ProgressResult> progressList = ClinicProgress.GetProgressResult(clinicList, judge);

			// 設定ミスリストの取得
			List<ClinicProgress> mistakeList = ClinicProgress.GetMistakeList(clinicList, judge);

			try
			{
				// オンライン資格確認導入状況_YYYYMMDD.xlsxの作成
				string orgPathname = Path.Combine(Directory.GetCurrentDirectory(), string.Format("{0}.xlsx.org", IntroductionStatusFilename));
				string outputFilename = string.Format("{0}_{1}.xlsx", IntroductionStatusFilename, Date.Today.GetNumeralString());
				string outputPathname = Path.Combine(Directory.GetCurrentDirectory(), outputFilename);
				File.Copy(orgPathname, outputPathname, true);

				// 導入状況ファイルへの書き込み
				ProgressResult.WriteProgressResult(outputPathname, progressList, clinicList, mistakeList);
				if (checkBoxDatabase.Checked)
				{
					// 進捗管理情報をデータベースに登録する
					List<オンライン資格確認進捗管理> onlineList = ClinicProgress.GetOnlineIntroductionStatusList(clinicList);

					// [SalesDB].[dbo].[オンライン資格確認進捗管理]に登録
#if true
					// 全削除、全追加方式
					SalesDatabaseAccess.Delete_オンライン資格確認進捗管理("", Settings.ConnectSales.ConnectionString);
					SalesDatabaseAccess.InsertIntoList_オンライン資格確認進捗管理(onlineList, Settings.ConnectSales.ConnectionString);
#else
					// 更新及び追加方式（非常に時間がかかる）
					foreach (オンライン資格確認進捗管理 online in onlineList)
					{
						List<オンライン資格確認進捗管理> work = SalesDatabaseAccess.Select_オンライン資格確認進捗管理(string.Format("顧客No = {0}", online.顧客No), "", Settings.ConnectSales.ConnectionString);
						if (null == work || 0 == work.Count)
						{
							SalesDatabaseAccess.InsertInto_オンライン資格確認進捗管理(online, Settings.ConnectSales.ConnectionString);
						}
						else if (false == online.Equals(work[0]))
						{
							SalesDatabaseAccess.UpdateSet_オンライン資格確認進捗管理(online, Settings.ConnectSales.ConnectionString);
						}
					}
#endif
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show(string.Format("{0}を出力しました。", outputFilename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);

				// Excelの起動
				using (Process process = new Process())
				{
					process.StartInfo.FileName = outputPathname;
					process.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
					process.Start();
				}
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
