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
//
// 売上作成対象
// 保守終了月：当月
// 売上日：当月初日
// 摘要：翌月（yyyy年MM月更新分）
/////////////////////////////////////////////////////////
// Ver1.00(2024/01/23 勝呂):新規作成
// Ver1.01(2024/01/30 勝呂):メール送信設定にBCCを追加
// Ver1.02(2024/02/01 勝呂):売上作成対象は保守終了月が翌月でなく、当月が正しい
// Ver1.03(2024/02/05 勝呂):売上データの利用年月分の表記と年月が正しくない
// Ver1.04(2024/05/17 勝呂):fai保守契約終了の更新時に条件文の不具合を修正
// Ver1.05(2024/05/29 勝呂):SHINKO ｵﾝ資･ｵﾝｻｲﾄ保守、MIC ｵﾝﾗｲﾝ資格確認保守ｻｰﾋﾞｽに対応
// Ver1.05(2024/05/29 勝呂):売上データの更新単位を月から空白に修正
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
		public const string VersionStr = "Ver1.05(2024/05/29)";

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
			gBootDate = new Date(2024, 6, 1);
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

			// Ver1.02(2024/02/01 勝呂):売上作成対象は保守終了月が翌月でなく、当月が正しい
			// 保守終了月が今月
			//Date mainteDate = bootDate.FirstDayOfNextMonth();   // 翌月初日
			Date targetDate = bootDate.FirstDayOfTheMonth();        // 当月初日

			// 摘要利用年月日（ yyyy年MM月更新分）
			Date tekiyoDate = bootDate.FirstDayOfNextMonth();   // 翌月初日

			try
			{
#if false
				// 緊急用
				string customerNoStr = "10019664,10002822,20005135,20006802,20007938,20010480,10048260,10048254,10000210,10030884,10026081,20002673,20007564,20005812,10068679"
												+ ",20003035,10030527,10017040,20005217,20015175,20008437,10048481,10092238,20010670,10037935,10002334,10010059,10000106,10000638,10064592"
												+ ",20008548,10075037,10023681,10046155,10032334,10044597,20021497,10048953,10031926,10075922,10068387,10006798,10005618,10047604,10064469"
												+ ",10000083,10041774,20003018,20002066,20017312,20002356,10086810,20005708,20014877";
				List<OnlineLicenseMainteEarningsOut> saleList = OnlineLicenseMainteAccess.GetOnlineLicenseMainteEarningsOutEmergency(customerNoStr, gSettings.ConnectJunp.ConnectionString);
#else
				// アプリケーション情報からオン資格保守サービス売上情報の取得
				List<OnlineLicenseMainteEarningsOut> saleList = OnlineLicenseMainteAccess.GetOnlineLicenseMainteEarningsOut(targetDate.ToYearMonth(), gSettings.ConnectJunp.ConnectionString);
#endif
				if (null != saleList && 0 < saleList.Count)
				{
					// 消費税
					int taxRate = JunpDatabaseAccess.GetTaxRate(targetDate, gSettings.ConnectJunp.ConnectionString);

					// 中間ファイルの出力
					using (var sw = new StreamWriter(gSettings.TemporaryPathname, false, System.Text.Encoding.GetEncoding("shift_jis")))
					{
						// 売上日
						// Ver1.03(2024/02/05 勝呂):売上データの利用年月分の表記と年月が正しくない
						//Date saleDate = bootDate.FirstDayOfTheMonth();  // 当月初日
						foreach (OnlineLicenseMainteEarningsOut sale in saleList)
						{
							if (sale.f保守終了月.HasValue && targetDate.ToYearMonth() <= sale.f保守終了月.Value)
							{
								// 最終保守終了月以前 売上データ追加
								if (0 == sale.f請求先コード.Length)
								{
									// 請求先がユーザーと同一
									// Ver1.03(2024/02/05 勝呂):売上データの利用年月分の表記と年月が正しくない
									//sw.WriteLine(sale.ToEarnings(no, sale.f得意先コード, saleDate, mainteDate, taxRate, gSettings.PcaVersion));
									sw.WriteLine(sale.ToEarnings(no, sale.f得意先コード, targetDate, tekiyoDate, taxRate, gSettings.PcaVersion));
								}
								else
								{
									// 請求先がユーザーと異なる
									// Ver1.03(2024/02/05 勝呂):売上データの利用年月分の表記と年月が正しくない
									//sw.WriteLine(sale.ToEarnings(no, sale.f請求先コード, saleDate, mainteDate, taxRate, gSettings.PcaVersion));
									sw.WriteLine(sale.ToEarnings(no, sale.f請求先コード, targetDate, tekiyoDate, taxRate, gSettings.PcaVersion));

									// ○○○○様分 を記事行１を追加
									// Ver1.03(2024/02/05 勝呂):売上データの利用年月分の表記と年月が正しくない
									//sw.WriteLine(sale.ToArticle1(no, sale.f請求先コード, saleDate, gSettings.PcaVersion));
									sw.WriteLine(sale.ToArticle1(no, sale.f請求先コード, targetDate, tekiyoDate, gSettings.PcaVersion));

									// 得意先No. を記事行２を追加
									// Ver1.03(2024/02/05 勝呂):売上データの利用年月分の表記と年月が正しくない
									//sw.WriteLine(sale.ToArticle2(no, sale.f請求先コード, saleDate, gSettings.PcaVersion));
									sw.WriteLine(sale.ToArticle2(no, sale.f請求先コード, targetDate, tekiyoDate, gSettings.PcaVersion));
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
							// 保守終了月を１年後に更新。５年後に終了
							// 2023/12～2024/11→2024/12：更新
							// 2024/12～2025/11→2025/12：更新
							// 2025/12～2026/11→2026/12：更新
							// 2026/12～2027/11→2027/12：更新
							// ●2027/12～2028/11→2028/11：終了
							sale.f保守終了月 = sale.f保守終了月.Value + 11;

							// 最終徴収年月なので終了フラグをONにする
							sale.f終了フラグ = true;

#if DebugNoWrite == false
							// アプリケーション情報 終了フラグの設定
							OnlineLicenseMainteAccess.UpdateSetApplicationInfoEndFlag(sale, UPDATE_PERSON, gSettings.ConnectJunp.ConnectionString);
#endif
						}
						else
						{
							if (sale.f保守終了月.HasValue)
							{
								// 保守終了月を１年後に更新。５年後に終了
								// ●2023/12～2024/11→2024/12：更新
								// ●2024/12～2025/11→2025/12：更新
								// ●2025/12～2026/11→2026/12：更新
								// ●2026/12～2027/11→2027/12：更新
								// 2027/12～2028/11→2028/11：終了
								sale.f保守終了月 = sale.f保守終了月.Value.PlusYears(1);

#if !DebugNoWrite
								// アプリケーション情報の更新
								OnlineLicenseMainteAccess.UpdateSetApplicationInfo(sale, UPDATE_PERSON, gSettings.ConnectJunp.ConnectionString);
#endif
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
				SendMailControl.OnlineLicenseMainteSendMail(saleList, gFormalFilename, tekiyoDate);
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return string.Empty;
		}
	}
}
