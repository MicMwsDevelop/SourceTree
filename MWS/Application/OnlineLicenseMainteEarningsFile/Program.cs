//
// Program.cs
// 
// オン資格保守サービス売上データ作成 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// アプリ管理サイト：APPID637 オン資格保守サービス売上データ作成
// 処理概要：リコー及び菱洋エレクトロのオン資格保守サービスの売上データファイルを出力する
// 入力ファイル：無
// 出力ファイル：\\sqlsv\pcadata\オン資格保守サービス売上データ_yyyyMMddHHmm.csv
// 印刷物：無
// メール送信：オン資格保守サービス自動更新 売上連絡
/////////////////////////////////////////////////////////
// Ver1.00(2024/01/23 勝呂):新規作成
// Ver1.01(2024/01/30 勝呂):メール送信設定にBCCを追加
//
using CommonLib.BaseFactory.OnlineLicenseMainte;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using CommonLib.DB.SqlServer.OnlineLicenseMainte;
using OnlineLicenseMainteEarningsFile.Mail;
using OnlineLicenseMainteEarningsFile.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace OnlineLicenseMainteEarningsFile
{
	internal static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string PROC_NAME = "オン資格保守サービス売上データ作成";

		/// <summary>
		/// プログラム名（更新者名）
		/// </summary>
		public const string UPDATE_PERSON = "ｵﾝ資格保守ｻｰﾋﾞｽ売上ﾃﾞｰﾀ作成";

		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string VersionStr = "Ver1.01(2024/01/30)";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static OnlineLicenseMainteEarningsFileSettings gSettings;

		/// <summary>
		/// 実行日
		/// </summary>
		public static Date gBootDate;

		/// <summary>
		/// 出力ファイル名
		/// </summary>
		public static string gFormalFilename;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static int Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			gSettings = OnlineLicenseMainteEarningsFileSettingsIF.GetSettings();

#if DEBUG
			gBootDate = new Date(2024, 1, 1);
#else
			gBootDate = Date.Today;
#endif

			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					// サイレントモード
					string msg = OutputCsvFile(gBootDate);
					if (0 < msg.Length)
					{
						return 1;
					}
					return 0;
				}
			}
			Application.Run(new Forms.MainForm());
			return 0;
		}

		/// <summary>
		/// オン資格保守サービス売上データ.csvの出力
		/// </summary>
		/// <param name="bootDate">実行日</param>
		/// <returns>エラーメッセージ</returns>
		public static string OutputCsvFile(Date bootDate)
		{
			// 伝票番号
			int no = gSettings.SlipInitialNumber;

			gFormalFilename = gSettings.FormalFilename;

			// 保守終了月が翌月
			Date mainteDate = bootDate.FirstDayOfNextMonth();   // 翌月初日
			try
			{
				// アプリケーション情報からオン資格保守サービス売上情報の取得
				List<OnlineLicenseMainteEarningsOut> saleList = OnlineLicenseMainteAccess.GetOnlineLicenseMainteEarningsOut(mainteDate.ToYearMonth(), gSettings.ConnectJunp.ConnectionString);
				if (null != saleList && 0 < saleList.Count)
				{
					// 消費税
					int taxRate = JunpDatabaseAccess.GetTaxRate(mainteDate, gSettings.ConnectJunp.ConnectionString);

					// 中間ファイルの出力
					using (var sw = new StreamWriter(gSettings.TemporaryPathname, false, System.Text.Encoding.GetEncoding("shift_jis")))
					{
						// 売上日
						Date saleDate = bootDate.FirstDayOfTheMonth();  // 当月初日
						foreach (OnlineLicenseMainteEarningsOut sale in saleList)
						{
							if (sale.f保守終了月.HasValue && saleDate.ToYearMonth() <= sale.f保守終了月.Value)
							{
								// 最終保守終了月以前 売上データ追加
								if (0 == sale.f請求先コード.Length)
								{
									// 請求先がユーザーと同一
									sw.WriteLine(sale.ToEarnings(no, sale.f得意先コード, saleDate, mainteDate, taxRate, gSettings.PcaVersion));
								}
								else
								{
									// 請求先がユーザーと異なる
									sw.WriteLine(sale.ToEarnings(no, sale.f請求先コード, saleDate, mainteDate, taxRate, gSettings.PcaVersion));

									// ○○○○様分 を記事行１を追加
									sw.WriteLine(sale.ToArticle1(no, sale.f請求先コード, saleDate, gSettings.PcaVersion));

									// 得意先No. を記事行２を追加
									// ○○○○様分 を記事行１を追加
									sw.WriteLine(sale.ToArticle2(no, sale.f請求先コード, saleDate, gSettings.PcaVersion));
								}
								no++;
							}
						}
					}
					// 中間ファイルをリネームして出力ファルダにコピー
					File.Copy(gSettings.TemporaryPathname, gSettings.FormalPathname(gFormalFilename));

					foreach (OnlineLicenseMainteEarningsOut sale in saleList)
					{
						if (sale.Is最終徴収年月)
						{
							// 2019/12～2020/11→2020/12
							// 2020/12～2021/11→2021/12
							// 2021/12～2022/11→2022/12
							// 2022/12～2023/11→2023/12
							// 2023/12～2024/11→2024/11●
							sale.f保守終了月 = sale.f保守終了月.Value + 11;

							// 最終徴収年月なので終了フラグをONにする
							sale.f終了フラグ = true;

							// アプリケーション情報 終了フラグの設定
							OnlineLicenseMainteAccess.UpdateSetApplicationInfoEndFlag(sale, UPDATE_PERSON, gSettings.ConnectJunp.ConnectionString);
						}
						else
						{
							if (sale.f保守終了月.HasValue)
							{
								// 保守終了月を１年後に更新
								// 2023/12～2024/11→2024/12●
								// 2024/12～2025/11→2025/12
								// 2025/12～2026/11→2026/12
								// 2026/12～2027/11→2027/12
								// 2027/12～2028/11→2028/11
								sale.f保守終了月 = sale.f保守終了月.Value.PlusYears(1);

								// アプリケーション情報の更新
								OnlineLicenseMainteAccess.UpdateSetApplicationInfo(sale, UPDATE_PERSON, gSettings.ConnectJunp.ConnectionString);
							}
						}
					}
				}
				else
				{
					// オン資格保守サービス売上データ.csvの出力
					using (var sw = new StreamWriter(gSettings.FormalPathname(gFormalFilename), false, System.Text.Encoding.GetEncoding("shift_jis")))
					{
						// 出力物はないが、売上データファイルは出力する
						;
					}
				}
				// 経理部にメール送信
				SendMailControl.OnlineLicenseMainteSendMail(saleList, gFormalFilename, mainteDate);
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return string.Empty;
		}
	}
}
