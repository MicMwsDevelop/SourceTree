//
// MainForm.cs
// 
// オンライン資格確認進捗管理情報登録 メイン画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/09/28 勝呂)
// Ver1.01 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
//
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.BaseFactory.Sales.Table;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using CommonLib.DB.SqlServer.Sales;
using OnlineLicenseProgressEntry.BaseFactory;
using OnlineLicenseProgressEntry.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace OnlineLicenseProgressEntry.Forms
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// オン資進捗管理表ファイル名
		/// </summary>
		private const string ProgressFilename = "オン資進捗管理表";

		/// <summary>
		/// 環境設定
		/// </summary>
		public OnlineLicenseProgressEntrySettings Settings { get; set; }

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
			// 環境設定の読込
			Settings = OnlineLicenseProgressEntrySettingsIF.GetSettings();

			// プログラムタイトル設定
#if DEBUG
			this.Text = string.Format("{0} ({1}) {2}", Program.ProgramName, Program.ProgramVersion, Settings.ConnectSales.InstanceName);
#else
			this.Text = string.Format("{0} ({1})", Program.ProgramName, Program.ProgramVersion);
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
		/// マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）フォルダの選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		// Ver1.01 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
		private void buttonClinicList_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dlg = new FolderBrowserDialog())
			{
				//上部に表示する説明テキストを指定する
				dlg.Description = "フォルダを指定してください。";
				dlg.SelectedPath = @"D:\SourceTree\MWS\Application\OnlineLicenseProgressEntry\Doc\マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）対応";
				dlg.ShowNewFolderButton = false;
				if (DialogResult.OK == dlg.ShowDialog(this))
				{
					textBoxClinicList.Text = dlg.SelectedPath;
				}
			}
		}

		/// <summary>
		/// 進捗管理情報の登録
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExecProgress_Click(object sender, EventArgs e)
		{
			if (0 == textBoxProgress.Text.Length)
			{
				MessageBox.Show("進捗管理ファイルを設定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (false == File.Exists(textBoxProgress.Text))
			{
				MessageBox.Show(string.Format("{0}のファイルが見つかりません。", textBoxProgress.Text), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (0 == textBoxClinicList.Text.Length)
			{
				MessageBox.Show("マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）フォルダを設定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (false == Directory.Exists(textBoxClinicList.Text))
			{
				MessageBox.Show(string.Format("{0}のフォルダが見つかりません。", textBoxClinicList.Text), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			try
			{
				// 進捗管理ファイルから全顧客情報を取得する
				List<ProgressExcelRecord> progressList = ProgressExcelRecord.ReadProgressExcelFile(textBoxProgress.Text);
				if (null != progressList && 0 < progressList.Count)
				{
					// オンライン資格確認進捗管理表_YYYYMMDD.xlsxの作成
					// オン資進捗管理表.xlsx.orgをカレントフォルダにコピー
					// オン資進捗管理表.xlsx.orgをオンライン資格確認進捗管理表_YYYYMMDD.xlsxにリネーム
					string orgPathname = Path.Combine(Directory.GetCurrentDirectory(), string.Format("{0}.xlsx.org", ProgressFilename));
					string outputFilename = string.Format("{0}_{1}.xlsx", ProgressFilename, Date.Today.GetNumeralString());
					string outputPathname = Path.Combine(Directory.GetCurrentDirectory(), outputFilename);
					File.Copy(orgPathname, outputPathname, true);

					// 進捗管理全顧客情報を[SalesDB].[dbo].[オンライン資格確認進捗管理情報]に変換する
					List<オンライン資格確認進捗管理情報> onlineList = ProgressExcelRecord.GetOnlineIntroductionStatusList(progressList);

					// マイナンバーカードの健康保険証利用対応の医療機関リストから医療機関情報を抽出
					List<MyNumberCardClinic> clinicList = new List<MyNumberCardClinic>();
					string[] files = Directory.GetFiles(textBoxClinicList.Text, "*.xlsx", SearchOption.AllDirectories);
					foreach (string pathName in files)
					{
						MyNumberCardClinic.ReadMyNumberCardClinicListExcelFile(pathName, clinicList);
					}
					if (0 < clinicList.Count)
					{
						// 医療機関情報の電話番号からWWの顧客情報を抽出し、医療機関情報に顧客Noを設定
						List<tMik基本情報> basicList = JunpDatabaseAccess.Select_tMik基本情報("Len([fkj電話番号]) > 1", "[fkjCliMicID] ASC", Settings.ConnectJunp.ConnectionString);
						if (null != basicList && 0 < basicList.Count)
						{
							foreach (MyNumberCardClinic clinic in clinicList)
							{
								if (0 < clinic.TelNumber.Length)
								{
									tMik基本情報 basic = basicList.Find(p => p.fkj電話番号 == clinic.TelNumber);
									if (null != basic)
									{
										オンライン資格確認進捗管理情報 online = onlineList.Find(p => p.顧客No == basic.fkjCliMicID);
										if (null != online)
										{
											online.オン資運用開始日 = clinic.OperationStartDate;
										}
									}
								}
							}
						}
						// [SalesDB].[dbo].[オンライン資格確認進捗管理情報]に登録
#if true
						// 全削除、全追加方式
						SalesDatabaseAccess.Delete_オンライン資格確認進捗管理情報("", Settings.ConnectSales.ConnectionString);
						SalesDatabaseAccess.InsertIntoList_オンライン資格確認進捗管理情報(onlineList, Settings.ConnectSales.ConnectionString);
#else
						// 更新及び追加方式（非常に時間がかかる）
						foreach (オンライン資格確認進捗管理情報 online in onlineList)
						{
							if (SalesDatabaseAccess.IsExistオンライン資格確認進捗管理情報(Settings.ConnectSales.ConnectionString, online.顧客No))
							{
								SalesDatabaseAccess.UpdateSet_オンライン資格確認進捗管理情報(online, Settings.ConnectSales.ConnectionString);
							}
							else
							{
								SalesDatabaseAccess.InsertInto_オンライン資格確認進捗管理情報(online, Settings.ConnectSales.ConnectionString);
							}
						}
#endif
						// カーソルを元に戻す
						Cursor.Current = preCursor;

						MessageBox.Show("進捗管理情報を登録しました。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);

						// Excelの起動
						using (Process process = new Process())
						{
							process.StartInfo.FileName = outputPathname;
							process.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
							process.Start();
						}
						this.Close();
					}
				}
				else
				{
					MessageBox.Show("データがありません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

/*
		/// <summary>
		/// 運用開始日の登録
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		// Ver1.01 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
		private void buttonExecClinicList_Click(object sender, EventArgs e)
		{
			if (0 == textBoxClinicList.Text.Length)
			{
				MessageBox.Show("マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）フォルダを設定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (false == Directory.Exists(textBoxClinicList.Text))
			{
				MessageBox.Show("マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）フォルダが存在しません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// マイナンバーカードの健康保険証利用対応の医療機関リストから医療機関情報を抽出
				List<MyNumberCardClinic> clinicList = new List<MyNumberCardClinic>();
				string[] files = Directory.GetFiles(textBoxClinicList.Text, "*.xlsx", SearchOption.AllDirectories);
				foreach (string pathName in files)
				{
					MyNumberCardClinic.ReadMyNumberCardClinicListExcelFile(pathName, clinicList);
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;

				if (0 < clinicList.Count)
				{
					// 医療機関情報の電話番号からWWの顧客情報を抽出し、医療機関情報に顧客Noを設定
					List<tMik基本情報> basicList = JunpDatabaseAccess.Select_tMik基本情報("Len([fkj電話番号]) > 1", "[fkjCliMicID] ASC", Settings.ConnectJunp.ConnectionString);
					if (null != basicList && 0 < basicList.Count)
					{
						// 元のカーソルを保持
						preCursor = Cursor.Current;

						// カーソルを待機カーソルに変更
						Cursor.Current = Cursors.WaitCursor;

						int setCount = 0;
						foreach (MyNumberCardClinic clinic in clinicList)
						{
							if (0 < clinic.TelNumber.Length)
							{
								tMik基本情報 basic = basicList.Find(p => p.fkj電話番号 == clinic.TelNumber);
								if (null != basic)
								{
									clinic.CustomerNo = basic.fkjCliMicID;
								}
							}
						}
						// オンライン資格確認進捗管理情報に運用開始日を設定
						foreach (MyNumberCardClinic clinic in clinicList)
						{
							if (0 < clinic.CustomerNo && true == clinic.OperationStartDate.HasValue)
							{
								SalesDatabaseAccess.UpdateSet_オンライン資格確認進捗管理情報_運用開始日(clinic.CustomerNo, clinic.OperationStartDate.Value, Settings.ConnectSales.ConnectionString);
								setCount++;
							}
						}
						MessageBox.Show(string.Format("運用開始日を {0} 件 登録しました。", setCount), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);

						// カーソルを元に戻す
						Cursor.Current = preCursor;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
*/
	}
}
