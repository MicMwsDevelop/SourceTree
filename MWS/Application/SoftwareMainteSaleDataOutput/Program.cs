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
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					SoftwareMainteSaleDataOutputSettings settings = SoftwareMainteSaleDataOutputSettingsIF.GetSettings();
					OutputCsvFile(settings.Pathname);
					return;
				}
			}
			Application.Run(new MainForm());
		}

		/// <summary>
		/// 売上データCSVファイルの出力
		/// </summary>
		/// <param name="pathname">出力ファイルパス名</param>
		/// <returns>エラーメッセージ</returns>
		public static string OutputCsvFile(string pathname)
		{
			try
			{
				List<CustomerUseInfoSoftwareMainte> list = SoftwareMainteSaleDataAccess.GetCustomerUseInfoSoftwareMainte1(Date.Today, DATABASE_ACCESS_CT);
				if (0 < list.Count)
				{
					List<string> csvList = new List<string>();
					int no = 60000;
					Date lastMonth = Date.Today.LastDayOfLasMonth();
					int taxRate = JunpDatabaseAccess.GetTaxRate(lastMonth, DATABASE_ACCESS_CT);
					foreach (CustomerUseInfoSoftwareMainte data in list)
					{
						List<OrderSlipSoftwareMainte> order = SoftwareMainteSaleDataAccess.GetSoftwareMainteOrderSlip(data.CUSTOMER_ID, DATABASE_ACCESS_CT);
						if (0 < order.Count)
						{
							csvList.Add(order[0].ToSale(no, taxRate, lastMonth));
							no++;
						}
					}
					if (0 < csvList.Count)
					{
						// 売上データ.txtの出力
						using (var sw = new System.IO.StreamWriter(pathname, false))
						{
							foreach (string str in csvList)
							{
								sw.WriteLine(str);
							}
						}
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
