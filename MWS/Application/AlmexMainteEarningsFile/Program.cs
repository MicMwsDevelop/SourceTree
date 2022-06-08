//
// Program.cs
// 
// アルメックス保守サービス売上データ作成 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// アプリ管理サイト：APPID612 アルメックス保守サービス売上データ作成
// 処理概要：アルメックスの自動精算機の保守サービスの自動更新分の売上データの作成
// 入力ファイル：無
// 出力ファイル：\\sqlsv\pcadata\アルメックス保守売上データ.csv
// 印刷物：無
// メール送信：アルメックス保守サービス自動更新 売上連絡
/////////////////////////////////////////////////////////
// Ver1.00 新規作成(2021/01/20 勝呂)
// Ver1.02 002189 アルメックス FIT-A 保守(ｸﾚｼﾞｯﾄ仕様)1ヶ月 削除の対応(2021/01/20 勝呂)
// Ver1.03 売上日が翌月初日になっていたのを当月初日に修正(2021/12/23 勝呂)
// Ver1.04 汎用データレイアウト 売上明細データ Version 11(DX-Rev3.00)に対応(2022/05/25 勝呂)
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
		public const string VersionStr = "Ver1.04(2022/05/25)";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static AlmexMainteEarningsFileSettings gSettings;

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

			gSettings = AlmexMainteEarningsFileSettingsIF.GetSettings();

#if DEBUG
			gBootDate = new Date(2022, 1, 1);
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
		/// <param name="bootDate">実行日</param>
		/// <returns>エラーメッセージ</returns>
		public static string OutputCsvFile(Date bootDate)
		{
			// 伝票番号
			int no = gSettings.SlipInitialNumber;

			gFormalFilename = gSettings.FormalFilename;

			// 保守終了月が翌月
			Date mainteDate = bootDate.FirstDayOfNextMonth();	// 翌月初日
			try
			{
				// アプリケーション情報からアルメックス保守サービスの更新対象医院の取得
				List<AlmexMainteEarningsOut> saleList = AlmexMainteAccess.GetAlmexMainteEarningsOut(mainteDate.ToYearMonth(), gSettings.Connect.Junp.ConnectionString);
				if (null != saleList && 0 < saleList.Count)
				{
					// 消費税
					int taxRate = JunpDatabaseAccess.GetTaxRate(mainteDate, gSettings.Connect.Junp.ConnectionString);

					// 中間ファイルの出力
					using (var sw = new StreamWriter(gSettings.TemporaryPathname, false, System.Text.Encoding.GetEncoding("shift_jis")))
					{
						// 売上日
						// Ver1.03 売上日が翌月初日になっていたのを当月初日に修正(2021/12/23 勝呂)
						Date saleDate = bootDate.FirstDayOfTheMonth();	// 当月初日
						foreach (AlmexMainteEarningsOut sale in saleList)
						{
							YearMonth? endYM = sale.最終保守終了月;
							if (endYM.HasValue && sale.f保守終了月.Value <= endYM.Value)
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
				SendMailControl.AlmexMainteSendMail(saleList, gFormalFilename, mainteDate);
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return string.Empty;
		}
	}
}
