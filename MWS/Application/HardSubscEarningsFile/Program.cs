//
// Program.cs
// 
// ハードサブスク売上データ作成 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// アプリ管理サイト：APPIDXXX ハードサブスク売上データ作成
// 処理概要：ハードサブスクの売上データの作成
// 入力ファイル：無
// 出力ファイル：\\sqlsv\pcadata\ハードサブスク売上データ.csv
// 印刷物：無
// メール送信：ハードサブスク売上連絡
/////////////////////////////////////////////////////////
// 注意事項
// 本アプリは終了ユーザーの売上を作成しないため、課金データ作成バッチの後に実行する必要がある
/////////////////////////////////////////////////////////
// Ver1.00(2025/04/15 勝呂):新規作成
//
using CommonLib.BaseFactory.HardSubsc;
using CommonLib.Common;
using CommonLib.DB.SqlServer.HardSubsc;
using CommonLib.DB.SqlServer.Junp;
using HardSubscEarningsFile.Mail;
using HardSubscEarningsFile.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace HardSubscEarningsFile
{
	internal static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string PROC_NAME = "ハードサブスク売上データ作成";

		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string VersionStr = "Ver1.00(2025/04/15)";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static HardSubscEarningsFileSettings gSettings;

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

			gSettings = HardSubscEarningsFileSettingsIF.GetSettings();

#if DEBUG
			gBootDate = new Date(2025, 6, 1);
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
					HardSubscEarningsFileSettingsIF.SetSettings(gSettings);
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
		/// ハードサブスク売上データ.csvの出力
		/// </summary>
		/// <param name="bootDate">実行日</param>
		/// <returns>エラーメッセージ</returns>
		public static string OutputCsvFile(Date bootDate)
		{
			// 伝票番号
			int no = gSettings.SlipInitialNumber;

			gFormalFilename = gSettings.FormalFilename;

			// 当月初日
			Date firstDate = bootDate.FirstDayOfTheMonth();

			// 当月末日
			Date lastDate = bootDate.LastDayOfTheMonth();

			try
			{
				// ハードサブスク管理契約情報から売上対象医院の取得
				List<HardSubscEarningsOut> saleList = HardSubscAccess.GetHardSubscEarningsOut(firstDate, gSettings.ConnectCharlie.ConnectionString);
				if (null != saleList && 0 < saleList.Count)
				{
					// 消費税
					int taxRate = JunpDatabaseAccess.GetTaxRate(firstDate, gSettings.ConnectJunp.ConnectionString);

					// 中間ファイルの出力
					using (var sw = new StreamWriter(gSettings.TemporaryPathname, false, System.Text.Encoding.GetEncoding("shift_jis")))
					{
						foreach (HardSubscEarningsOut sale in saleList)
						{
							if (0 == sale.請求先コード.Length)
							{
								// 請求先がユーザーと同一
								sw.WriteLine(sale.ToEarnings(no, sale.得意先コード, firstDate, firstDate, firstDate, taxRate, gSettings.PcaVersion));
							}
							else
							{
								// 請求先がユーザーと異なる
								sw.WriteLine(sale.ToEarnings(no, sale.請求先コード, firstDate, firstDate, firstDate, taxRate, gSettings.PcaVersion));

								// ○○○○様分 を記事行１を追加
								sw.WriteLine(sale.ToArticle1(no, sale.請求先コード, firstDate, firstDate, firstDate, gSettings.PcaVersion));

								// 得意先No. を記事行２を追加
								// ○○○○様分 を記事行１を追加
								sw.WriteLine(sale.ToArticle2(no, sale.請求先コード, firstDate, firstDate, firstDate, gSettings.PcaVersion));
							}
							no++;
						}
					}
					// 中間ファイルをリネームして出力ファルダにコピー
					File.Copy(gSettings.TemporaryPathname, gSettings.FormalPathname(gFormalFilename));

					foreach (HardSubscEarningsOut sale in saleList)
					{
						if (false == sale.課金開始日.HasValue)
						{
							// 当月初日
							sale.課金開始日 = firstDate.ToDateTime();
						}
						// 当月末日
						sale.課金終了日 = lastDate.ToDateTime();

						bool serviceEndFlag = false;
						if (sale.課金終了日 == sale.利用終了日)
						{
							// 利用終了日を迎えたので、サービス終了
							serviceEndFlag = true;
						}
						else if (sale.解約日.HasValue && sale.解約日 == sale.課金終了日)
						{
							// 解約日を迎えたので、サービス終了
							serviceEndFlag = true;
						}
#if !DebugNoWrite
						// ハードレンタル管理契約情報 課金終了日、終了フラグの設定
						HardSubscAccess.UpdateSetHardSubscHeader(sale, serviceEndFlag, gSettings.ConnectCharlie.ConnectionString, PROC_NAME);
#endif
					}
				}
				else
				{
					// ハードレンタル売上データ.csvの出力
					using (var sw = new StreamWriter(gSettings.FormalPathname(gFormalFilename), false, System.Text.Encoding.GetEncoding("shift_jis")))
					{
						// 出力物はないが、売上データファイルは出力する
						;
					}
				}
#if !DebugNoWrite
				// 業務課にメール送信
				//SendMailControl.SendMail(saleList, gFormalFilename, firstDate);
#endif
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return string.Empty;
		}
	}
}
