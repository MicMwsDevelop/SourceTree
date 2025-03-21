﻿//
// Program.cs
// 
// ソフトウェア保守料売上データ作成 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// アプリ管理サイト：APPID607 ソフトウェア保守料売上データ作成
// 処理概要：ｿﾌﾄｳｪｱ保守料12ヶ月の契約者は毎年、利用終了月の初日に契約延長の自動更新とｿﾌﾄｳｪｱ保守料12ヶ月の売上データを作成
// 入力ファイル：無
// 出力ファイル：ソフトウェア保守料売上データ.csv
// 印刷物：無
// メール送信：ソフトウェア保守料自動更新 売上連絡
/////////////////////////////////////////////////////////
// Ver1.00 新規作成(2020/10/16 勝呂)
// Ver1.01 売上日を前月末日から当月初日に変更(2020/11/30 勝呂)
// Ver1.02 SQL Server接続情報を環境設定に移行(2021/09/07 勝呂)
// Ver1.03 奇数月の時、摘要の利用期間の開始月が正しくない(2022/01/05 勝呂)
// Ver1.04 汎用データレイアウト 売上明細データ Version 11(DX-Rev3.00)に対応(2022/05/25 勝呂)
// Ver1.05 2023/08組織変更対応 メール「ソフトウェア保守料自動更新 売上連絡」の宛先・送信元など変更(メールアドレス複数指定対応含む)(2024/02/06 越田)
// Ver1.06 2025/02/19 勝呂:ソフトウェア保守料１年更新対象の抽出条件に課金対象外フラグがOFFの条件を追加
//
using CommonLib.BaseFactory.SoftwareMainteEarnings;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using CommonLib.DB.SqlServer.SoftwareMainteEarnings;
using SoftwareMainteEarningsFile.Mail;
using SoftwareMainteEarningsFile.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SoftwareMainteEarningsFile
{
	static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string PROC_NAME = "ソフトウェア保守料売上データ作成";

		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string VersionStr = "Ver1.06(2025/02/19)";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static SoftwareMainteEarningsFileSettings gSettings;

		/// <summary>
		/// 集計日
		/// </summary>
		public static Date CollectDate;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static int Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			gSettings = SoftwareMainteEarningsFileSettingsIF.GetSettings();

#if DEBUG
			CollectDate = new Date(2025, 3, 1);
#else
			// 集計日を当月初日に設定
			CollectDate = Date.Today.FirstDayOfTheMonth();
#endif

			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					// サイレントモード
					string msg = OutputCsvFile(Date.Today);
					SoftwareMainteEarningsFileSettingsIF.SetSettings(gSettings);
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
		/// ソフトウェア保守料売上データ.txtの出力
		/// </summary>
		/// <param name="collectDate">集計日</param>
		/// <returns>エラーメッセージ</returns>
		public static string OutputCsvFile(Date collectDate)
		{
			try
			{
				// ソフトウェア保守料売上データ.txtの出力
				using (var sw = new StreamWriter(gSettings.Pathname, false, System.Text.Encoding.GetEncoding("shift_jis")))
				{
					List<SoftwareMainteEarningsOut> saleList = new List<SoftwareMainteEarningsOut>();

					// ソフトウェア保守料１年 自動更新対象利用情報の取得
					List<CustomerUseInfoSoftwareMainte> list = SoftwareMainteEarningsAccess.GetCustomerUseInfoSoftwareMainte12(collectDate, gSettings.Connect.Charlie.ConnectionString);
					if (0 < list.Count)
					{
						// 伝票番号
						int no = gSettings.SlipInitialNumber;

						List<string> csvList = new List<string>();
						foreach (CustomerUseInfoSoftwareMainte cui in list)
						{
							// 顧客Noに売上データ必須情報の取得
							SoftwareMainteEarningsOut sale = SoftwareMainteEarningsAccess.GetSoftwareMainteEarningsOut(cui.CUSTOMER_ID, gSettings.Connect.Junp.ConnectionString);
							if (null != sale)
							{
								// 消費税
								int taxRate = JunpDatabaseAccess.GetTaxRate(cui.USE_START_DATE.Value, gSettings.Connect.Junp.ConnectionString);

								// 利用期間を１年後に設定
								//sale.f利用期間 = new Span(cui.USE_START_DATE.Value, cui.USE_END_DATE.Value); 
								// Ver1.03 奇数月の時、摘要の利用期間の開始月が正しくない(2022/01/05 勝呂)
								//sale.Set利用期間(cui.USE_END_DATE.Value);
								sale.Set利用期間(collectDate);

								// 売上データ追加
								if (0 == sale.f請求先コード.Length)
								{
									// 請求先がユーザーと同一
									csvList.Add(sale.ToEarnings(no, sale.f得意先コード, collectDate, taxRate, gSettings.PcaVersion));
								}
								else
								{
									// 請求先がユーザーと異なる
									csvList.Add(sale.ToEarnings(no, sale.f請求先コード, collectDate, taxRate, gSettings.PcaVersion));

									// ○○○○様分 を記事行１を追加
									csvList.Add(sale.ToArticle1(no, sale.f請求先コード, collectDate, gSettings.PcaVersion));

									// 得意先No. を記事行２を追加
									// ○○○○様分 を記事行１を追加
									csvList.Add(sale.ToArticle2(no, sale.f請求先コード, collectDate, gSettings.PcaVersion));
								}
								no++;
								saleList.Add(sale);
							}
						}
						if (0 < csvList.Count)
						{
							foreach (string str in csvList)
							{
								sw.WriteLine(str);
							}
							// ソフトウェア保守料１年の課金終了日を１年更新
							foreach (CustomerUseInfoSoftwareMainte data in list)
							{
#if !DEBUG
								SoftwareMainteEarningsAccess.UpdateSetCustomerUseInfo(data, PROC_NAME, gSettings.Connect.Charlie.ConnectionString);
#endif
							}
						}
					}
//#if !DEBUG
					// システム管理部にメール送信
					SendMailControl.SoftwareMainteSendMail(saleList);
//#endif
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
