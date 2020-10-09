//
// Program.cs
// 
// ソフトウェア保守料売上データ作成 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/10/09 勝呂)
//
using MwsLib.BaseFactory.SoftwareMainteEarnings;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Junp;
using MwsLib.DB.SqlServer.SoftwareMainteEarnings;
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

			// 集計日を先月初日に設定
			//CollectDate = new Date(2020, 11, 1);
			CollectDate = Date.Today.FirstDayOfLasMonth();

			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
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
				using (var sw = new StreamWriter(gSettings.Pathname, false))
				{
					// ソフトウェア保守料１年 自動更新対象利用情報の取得
					List<CustomerUseInfoSoftwareMainte> list = SoftwareMainteEarningsAccess.GetCustomerUseInfoSoftwareMainte12(collectDate, DATABASE_ACCESS_CT);
					if (0 < list.Count)
					{
						// 伝票番号
						int no = gSettings.SlipInitialNumber;

						// 売上日は先月末日
						Date uriageDate = collectDate.LastDayOfLasMonth();

						// 売上日の消費税率
						int taxRate = JunpDatabaseAccess.GetTaxRate(uriageDate, DATABASE_ACCESS_CT);

						List<string> csvList = new List<string>();
						foreach (CustomerUseInfoSoftwareMainte data in list)
						{
							// 顧客Noに対するソフトウェア保守料の受注情報の取得
							List<OrderSlipSoftwareMainte> order = SoftwareMainteEarningsAccess.GetSoftwareMainteOrderSlip(data.CUSTOMER_ID, DATABASE_ACCESS_CT);
							if (0 < order.Count)
							{
								// f販売先コード（顧客No）から得意先コードを取得
								string tookuisakiCode = JunpDatabaseAccess.GetTokuisakiCode(order[0].f販売先コード.Value, DATABASE_ACCESS_CT);

								// 売上データCSV文字列の取得
								csvList.Add(order[0].ToEarnings(no, taxRate, tookuisakiCode, uriageDate, gSettings.PcaVersion));
								no++;
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
								//SoftwareMainteEarningsAccess.UpdateSetCustomerUseInfo(data, PROC_NAME, DATABASE_ACCESS_CT);
							}
						}
					}
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
