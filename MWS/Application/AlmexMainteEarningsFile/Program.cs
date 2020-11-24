//
// Program.cs
// 
// アルメックス保守更新売上データ作成 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/11/24 勝呂)
//
using AlmexMainteEarningsFile.Mail;
using AlmexMainteEarningsFile.Settings;
using MwsLib.BaseFactory.AlmexMainteEarnings;
using MwsLib.Common;
using MwsLib.DB.SqlServer.AlmexMainteEarnings;
using MwsLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AlmexMainteEarningsFile
{
	static class Program
	{
		/// <summary>
		/// データベース接続先
		/// </summary>
		private const bool DATABASE_ACCESS_CT = false;

		/// <summary>
		/// 売上日
		/// </summary>
		public static Date SaleDate;

		/// <summary>
		/// 環境設定
		/// </summary>
		public static AlmexMainteEarningsFileSettings gSettings;

		/// <summary>
		/// プログラム名
		/// </summary>
		public const string PROC_NAME = "アルメックス保守更新売上データ作成";

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static int Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			gSettings = AlmexMainteEarningsFileSettingsIF.GetSettings();

#if DEBUG
			SaleDate = new Date(2020, 12, 1);
#else
			// 集計日を当月初日に設定
			SaleDate = Date.Today.FirstDayOfTheMonth();
#endif

			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					// サイレントモード
					string msg = OutputCsvFile(SaleDate);
					AlmexMainteEarningsFileSettingsIF.SetSettings(gSettings);
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
		/// アルメックス保守料売上データ.csvの出力
		/// </summary>
		/// <param name="saleDate">売上日</param>
		/// <returns>エラーメッセージ</returns>
		public static string OutputCsvFile(Date saleDate)
		{
			// 消費税
			int taxRate = JunpDatabaseAccess.GetTaxRate(saleDate, DATABASE_ACCESS_CT);

			try
			{
				// アルメックス保守料売上データ.csvの出力
				using (var sw = new StreamWriter(gSettings.Pathname, false, System.Text.Encoding.GetEncoding("shift_jis")))
				{
					// ソフトウェア保守料１年 自動更新対象利用情報の取得
					List<AlmexMainteEarningsOut> saleList = AlmexMainteEarningsAccess.GetAlmexMainteEarningsOut(saleDate, DATABASE_ACCESS_CT);
					if (0 < saleList.Count)
					{
						// 伝票番号
						int no = gSettings.SlipInitialNumber;

						// 請求日は先月末日
						Date billingDate = saleDate.LastDayOfLasMonth();

						foreach (AlmexMainteEarningsOut sale in saleList)
						{
							// 売上データ追加
							if (0 == sale.f請求先コード.Length)
							{
								// 請求先がユーザーと同一
								sw.WriteLine(sale.ToEarnings(no, sale.f得意先コード, saleDate, billingDate, taxRate, gSettings.PcaVersion));
							}
							else
							{
								// 請求先がユーザーと異なる
								sw.WriteLine(sale.ToEarnings(no, sale.f請求先コード, saleDate, billingDate, taxRate, gSettings.PcaVersion));

								// ○○○○様分 を記事行１を追加
								sw.WriteLine(sale.ToArticle1(no, sale.f請求先コード, saleDate, billingDate, gSettings.PcaVersion));

								// 得意先No. を記事行２を追加
								// ○○○○様分 を記事行１を追加
								sw.WriteLine(sale.ToArticle2(no, sale.f請求先コード, saleDate, billingDate, gSettings.PcaVersion));
							}
							no++;
						}
						// 保守終了月を１年更新
						foreach (AlmexMainteEarningsOut sale in saleList)
						{
							if (sale.f保守終了月.HasValue)
							{
								sale.f保守終了月 = sale.f保守終了月.Value.PlusYears(1);
#if !DEBUG
								// アプリケーション情報の更新
								AlmexMainteEarningsAccess.UpdateSetApplicationInfo(sale, PROC_NAME, DATABASE_ACCESS_CT);
#endif
							}
						}
					}
					// 営業管理部にメール送信
					SendMailControl.AlmexMainteSendMail(saleList);
				}
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return string.Empty;
		}
	}
}
