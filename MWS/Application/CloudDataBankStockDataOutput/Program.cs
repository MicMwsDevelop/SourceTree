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
		/// プログラム名
		/// </summary>
		public const string PROC_NAME = "クラウドデータバンク仕入データ出力";

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
					CloudDataBankStockDataOutputSettings settings = CloudDataBankStockDataOutputSettingsIF.GetSettings();
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
		/// 仕入データCSVファイルの出力
		/// </summary>
		/// <param name="pathname">出力ファイルパス名</param>
		/// <param name="pcaVer">PCAバージョン情報 </param>
		/// <returns>エラーメッセージ</returns>
		public static string OutputCsvFile(string pathname, int pcaVer)
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
						outputList.Add(data.ToStock(plusNo, pcaVer));
						plusNo++;
					}
					// クラウドデータバンク仕入データ.csvの出力
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
