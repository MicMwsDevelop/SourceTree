//
// Program.cs
// 
// アルメックス保守サービス売上データ作成 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/01/20 勝呂)
//
using AlmexMainteEarningsFile.Mail;
using AlmexMainteEarningsFile.Settings;
using CommonLib.BaseFactory.AlmexMainte;
using CommonLib.Common;
using CommonLib.DB.SqlServer.AlmexMainte;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AlmexMainteEarningsFile
{
	static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string PROC_NAME = "アルメックス保守売上データ作成";

		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string VersionStr = "Ver1.00(2021/01/20)";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static AlmexMainteEarningsFileSettings gSettings;

		/// <summary>
		/// 売上日
		/// </summary>
		public static Date gSaleDate;

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

			gSettings = AlmexMainteEarningsFileSettingsIF.GetSettings();

#if DEBUG
			gSaleDate = new Date(2021, 10, 1);
#else
			gSaleDate = Date.Today;
#endif

			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					// サイレントモード
					string msg = OutputCsvFile(gSaleDate);
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
		/// アルメックス保守売上データ.csvの出力
		/// </summary>
		/// <param name="saleDate">売上日</param>
		/// <returns>エラーメッセージ</returns>
		public static string OutputCsvFile(Date saleDate)
		{
			// 伝票番号
			int no = gSettings.SlipInitialNumber;

			gFormalFilename = gSettings.FormalFilename;

			// 保守終了月が当月
			Date mainteEndDate = saleDate.LastDayOfTheMonth();
			try
			{
				// アプリケーション情報からアルメックス保守サービスの更新対象医院の取得
				List<AlmexMainteEarningsOut> saleList = AlmexMainteAccess.GetAlmexMainteEarningsOut(mainteEndDate.ToYearMonth(), gSettings.Connect.Junp.ConnectionString);
				if (null != saleList && 0 < saleList.Count)
				{
					// 消費税
					int taxRate = JunpDatabaseAccess.GetTaxRate(saleDate, gSettings.Connect.Junp.ConnectionString);

					// 中間ファイルの出力
					using (var sw = new StreamWriter(gSettings.TemporaryPathname, false, System.Text.Encoding.GetEncoding("shift_jis")))
					{
						foreach (AlmexMainteEarningsOut sale in saleList)
						{
							YearMonth? endYM = sale.最終保守終了月;
							if (endYM.HasValue && sale.f保守終了月.Value <= endYM.Value)
							{
								// 最終保守終了月以前 売上データ追加
								if (0 == sale.f請求先コード.Length)
								{
									// 請求先がユーザーと同一
									sw.WriteLine(sale.ToEarnings(no, sale.f得意先コード, saleDate, taxRate, gSettings.PcaVersion));
								}
								else
								{
									// 請求先がユーザーと異なる
									sw.WriteLine(sale.ToEarnings(no, sale.f請求先コード, saleDate, taxRate, gSettings.PcaVersion));

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

					foreach (AlmexMainteEarningsOut sale in saleList)
					{
						if (sale.Is最終保守終了月)
						{
							// 最終保守終了月なので終了フラグをONにする
							sale.f終了フラグ = true;
#if !DEBUG
							// アプリケーション情報 終了フラグの設定
							AlmexMainteAccess.UpdateSetApplicationInfo(sale, PROC_NAME, gSettings.Connect.Junp.ConnectionString);
#endif
						}
						else
						{
							if (sale.f保守終了月.HasValue)
							{
								// 保守終了月を１か月更新
								sale.f保守終了月 = sale.f保守終了月.Value + 1;
#if !DEBUG
								// アプリケーション情報の更新
								AlmexMainteAccess.UpdateSetApplicationInfo(sale, PROC_NAME, gSettings.Connect.Junp.ConnectionString);
#endif
							}
						}
					}
				}
				else
				{
					// アルメックス保守売上データ.csvの出力
					using (var sw = new StreamWriter(gSettings.FormalPathname(gFormalFilename), false, System.Text.Encoding.GetEncoding("shift_jis")))
					{
						// 出力物はないが、売上データファイルは出力する
						;
					}
				}
				// 営業管理部にメール送信
				SendMailControl.AlmexMainteSendMail(saleList, gFormalFilename, saleDate);
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return string.Empty;
		}
	}
}
