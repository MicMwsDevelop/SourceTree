//
// Program.cs
// 
// ソフトウェア保守料売上データ出力 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/03/16 勝呂)
//
using MwsLib.BaseFactory.SoftwareMainteSaleData;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Charlie;
using MwsLib.DB.SqlServer.Junp;
using MwsLib.DB.SqlServer.SoftwareMainteSaleData;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SoftwareMainteSaleDataOutput
{
	static class Program
	{
		/// <summary>
		/// データベース接続先
		/// </summary>
		private const bool DATABASE_ACCESS_CT = false;

		/// <summary>
		/// プログラム名
		/// </summary>
		public const string PROC_NAME = "ソフトウェア保守料売上データ出力";

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static int Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					SoftwareMainteSaleDataOutputSettings settings = SoftwareMainteSaleDataOutputSettingsIF.GetSettings();
					string msg = OutputCsvFile(settings.Pathname, settings.PcaVersion);
					if (0 < msg.Length)
					{
						return 1;
					}
				}
			}
			Application.Run(new MainForm());
			return 0;
		}

		/// <summary>
		/// 売上データCSVファイルの出力
		/// </summary>
		/// <param name="pathname">出力ファイルパス名</param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>エラーメッセージ</returns>
		public static string OutputCsvFile(string pathname, int pcaVer)
		{
			try
			{
				List<CustomerUseInfoSoftwareMainte> list = SoftwareMainteSaleDataAccess.GetCustomerUseInfoSoftwareMainte1(Date.Today, DATABASE_ACCESS_CT);
				if (0 < list.Count)
				{
					List<string> csvList = new List<string>();
					
					// 伝票番号
					int no = 60000;
					
					// 売上日は先月末日
					Date uriageDate = Date.Today.LastDayOfLasMonth();

					// 売上日の消費税率
					int taxRate = JunpDatabaseAccess.GetTaxRate(uriageDate, DATABASE_ACCESS_CT);
					foreach (CustomerUseInfoSoftwareMainte data in list)
					{
						List<OrderSlipSoftwareMainte> order = SoftwareMainteSaleDataAccess.GetSoftwareMainteOrderSlip(data.CUSTOMER_ID, DATABASE_ACCESS_CT);
						if (0 < order.Count)
						{
							// f販売先コード（顧客No）から得意先コードを取得
							string tookuisakiCode = JunpDatabaseAccess.GetTokuisakiCode(order[0].f販売先コード.Value, DATABASE_ACCESS_CT);

							// 売上データCSV文字列の取得
							csvList.Add(order[0].ToSale(no, taxRate, tookuisakiCode, uriageDate, pcaVer));
							no++;
						}
					}
					if (0 < csvList.Count)
					{
						// 売上データ.csv
						using (var sw = new System.IO.StreamWriter(pathname, false))
						{
							foreach (string str in csvList)
							{
								sw.WriteLine(str);
							}
						}
						//// 利用情報の課金終了日を１年更新
						//foreach (CustomerUseInfoSoftwareMainte data in list)
						//{
						//	CharlieDatabaseAccess.UpdateSetCharlieDatabase(data.UpdateSetSqlString, data.GetUpdateSetParameters(Program.PROC_NAME, data.USE_END_DATE.Value.PlusYears(1)), DATABASE_ACCESS_CT);
						//}
					}
					else
					{
						return "ソフトウェア保守料売上対象なし";
					}
				}
				else
				{
					return "ソフトウェア保守料売上対象なし";
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
