//
// Program.cs
// 
// クラウドデータバンク仕入データ出力 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/03/06 勝呂)
//
using MwsLib.BaseFactory.CloudDataBankStockDataOutput;
using MwsLib.Common;
using MwsLib.DB.SqlServer.CloudDataBankStockDataOutput;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CloudDataBankStockDataOutput
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
					CloudDataBankStockDataOutputSettings settings = CloudDataBankStockDataOutputSettingsIF.GetSettings();
					OutputCsvFile(settings.Pathname);
					return;
				}
			}
			Application.Run(new MainForm());
		}

		/// <summary>
		/// 仕入データCSVファイルの出力
		/// </summary>
		/// <param name="pathname">出力ファイルパス名</param>
		/// <returns>エラーメッセージ</returns>
		public static string OutputCsvFile(string pathname)
		{
			try
			{
				List<CloudDataBankSaleData> list = CloudDataBankSaleAccess.GetCloudDataBankSaleList(Date.Today, DATABASE_ACCESS_CT);
				if (0 < list.Count)
				{
					List<string> outputList = new List<string>();
					int plusNo = 20060; // '20060番台　（りすとん=20 office365=40）
					foreach (CloudDataBankSaleData data in list)
					{
						outputList.Add(data.ToStock(plusNo));
						plusNo++;
					}
					// ナルコーム商品仕入データ.txtの出力
					using (var sw = new System.IO.StreamWriter(pathname, false))
					{
						foreach (string str in outputList)
						{
							sw.WriteLine(str);
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
