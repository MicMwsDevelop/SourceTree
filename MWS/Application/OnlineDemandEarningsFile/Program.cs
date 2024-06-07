//
// Program.cs
// 
// オンライン請求作業売上データ作成 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// アプリ管理サイト：APPID636 オンライン請求作業売上データ作成
// 処理概要：オンライン請求作業情報（T_USE_ONLINE_DEMAND）から先月分以前の売上データを作成する
// 入力ファイル：無
// 出力ファイル：\\sqlsv\pcadata\オンライン請求作業売上データ_yyyyMMddHHmm.csv
// 印刷物：無
// メール送信：オンライン請求作業 売上連絡
/////////////////////////////////////////////////////////
// Ver1.00(2023/12/01 勝呂):新規作成
// Ver1.05(2024/01/05 勝呂):メール送信先が複数指定された時にアプリケーションエラー
// Ver1.06(2024/07/01 勝呂):オン資訪問診療連携の018426 ｵﾝﾗｲﾝ資格確認訪問診療連携環境設定費の売上データ作成に対応
//
using CommonLib.BaseFactory.OnlineDemand;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using CommonLib.DB.SqlServer.OnlineDemand;
using OnlineDemandEarningsFile.Mail;
using OnlineDemandEarningsFile.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace OnlineDemandEarningsFile
{
	internal static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string PROC_NAME = "オンライン請求作業売上データ作成";

		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string VersionStr = "Ver1.06(2024/07/01)";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static OnlineDemandEarningsFileSettings gSettings;

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
			gSettings = OnlineDemandEarningsFileSettingsIF.GetSettings();

#if DEBUG
			gBootDate = new Date(2024, 4, 1);
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
					OnlineDemandEarningsFileSettingsIF.SetSettings(gSettings);
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
		/// オンライン請求作業売上データ.csvの出力
		/// </summary>
		/// <param name="bootDate">実行日</param>
		/// <returns>エラーメッセージ</returns>
		public static string OutputCsvFile(Date bootDate)
		{
			// 伝票番号
			int no = gSettings.SlipInitialNumber;

			gFormalFilename = gSettings.FormalFilename;

			YearMonth prevMonth = bootDate.FirstDayOfLasMonth().ToYearMonth();   // 先月
			try
			{
				// オンライン請求作業情報から先月末日以前の情報を取得
				List<OnlineDemandEarningsOut> saleList = OnlineDemandAccess.GetOnlineDemandEarningsOut(prevMonth, gSettings.ConnectCharlie.ConnectionString);
				if (null != saleList && 0 < saleList.Count)
				{
					// 消費税
					int taxRate = JunpDatabaseAccess.GetTaxRate(prevMonth.First, gSettings.ConnectJunp.ConnectionString);

					// 中間ファイルの出力
					using (var sw = new StreamWriter(gSettings.TemporaryPathname, false, System.Text.Encoding.GetEncoding("shift_jis")))
					{
						// 売上日
						foreach (OnlineDemandEarningsOut sale in saleList)
						{
							Date requestDate = sale.申請日時.Value.ToDate();
							if (0 == sale.請求先コード.Length)
							{
								// 請求先がユーザーと同一
								sw.WriteLine(sale.ToEarnings(no, sale.得意先コード, requestDate, taxRate, gSettings.PcaVersion));
							}
							else
							{
								// 請求先がユーザーと異なる
								sw.WriteLine(sale.ToEarnings(no, sale.請求先コード, requestDate, taxRate, gSettings.PcaVersion));

								// ○○○○様分 を記事行１を追加
								sw.WriteLine(sale.ToArticle1(no, sale.請求先コード, requestDate, gSettings.PcaVersion));

								// 得意先No. を記事行２を追加
								sw.WriteLine(sale.ToArticle2(no, sale.請求先コード, requestDate, gSettings.PcaVersion));
							}
							no++;
						}
					}
					// 中間ファイルをリネームして出力ファルダにコピー
					File.Copy(gSettings.TemporaryPathname, gSettings.FormalPathname(gFormalFilename));

#if !DebugNoWrite
					foreach (OnlineDemandEarningsOut sale in saleList)
					{
						// オンライン請求作業情報の売上日時を設定
						OnlineDemandAccess.UpdateSetOnlineDemandSaleDate(sale, PROC_NAME, gSettings.ConnectCharlie.ConnectionString);
					}
#endif
				}
				else
				{
					// オンライン請求作業売上データ.csvの出力
					using (var sw = new StreamWriter(gSettings.FormalPathname(gFormalFilename), false, System.Text.Encoding.GetEncoding("shift_jis")))
					{
						// 出力物はないが、売上データファイルは出力する
						;
					}
				}
				// オンライン請求作業 売上連絡メール送信（経理課宛）
				SendMailControl.OnlineDemandSendMail(saleList, gFormalFilename);
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return string.Empty;
		}
	}
}
