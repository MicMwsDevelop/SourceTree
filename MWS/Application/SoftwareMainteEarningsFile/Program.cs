﻿//
// Program.cs
// 
// ソフトウェア保守料売上データ作成 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/10/16 勝呂)
// Ver1.01 売上日を前月末日から当月初日に変更(2020/11/30 勝呂)
//
using MwsLib.BaseFactory.SoftwareMainteEarnings;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Junp;
using MwsLib.DB.SqlServer.SoftwareMainteEarnings;
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
		/// データベース接続先
		/// </summary>
		private const bool DATABASE_ACCESS_CT = false;

		/// <summary>
		/// 環境設定
		/// </summary>
		public static SoftwareMainteEarningsFileSettings gSettings;

		/// <summary>
		/// プログラム名
		/// </summary>
		public const string PROC_NAME = "ソフトウェア保守料売上データ作成";

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
			CollectDate = new Date(2021, 9, 1);
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
					List<CustomerUseInfoSoftwareMainte> list = SoftwareMainteEarningsAccess.GetCustomerUseInfoSoftwareMainte12(collectDate, DATABASE_ACCESS_CT);
					if (0 < list.Count)
					{
						// 伝票番号
						int no = gSettings.SlipInitialNumber;

						List<string> csvList = new List<string>();
						foreach (CustomerUseInfoSoftwareMainte cui in list)
						{
							// 顧客Noに売上データ必須情報の取得
							SoftwareMainteEarningsOut sale = SoftwareMainteEarningsAccess.GetSoftwareMainteEarningsOut(cui.CUSTOMER_ID, DATABASE_ACCESS_CT);
							if (null != sale)
							{
								// 消費税
								int taxRate = JunpDatabaseAccess.GetTaxRate(cui.USE_START_DATE.Value, DATABASE_ACCESS_CT);

								// 利用期間を１年後に設定
								//sale.f利用期間 = new Span(cui.USE_START_DATE.Value, cui.USE_END_DATE.Value); 
								sale.Set利用期間(cui.USE_END_DATE.Value);

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
							// ソフトウェア保守料１年の利用終了日を１年更新
							foreach (CustomerUseInfoSoftwareMainte data in list)
							{
#if !DEBUG
								SoftwareMainteEarningsAccess.UpdateSetCustomerUseInfo(data, PROC_NAME, DATABASE_ACCESS_CT);
#endif
							}
						}
					}
#if !DEBUG
					// 営業管理部にメール送信
					SendMailControl.SoftwareMainteSendMail(saleList);
#endif
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