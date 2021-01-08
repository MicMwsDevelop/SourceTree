//
// Program.cs
// 
// アルメックス保守サービス仕入データ作成 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/01/06 勝呂)
//
using AlmexMaintePurchaseFile.Forms;
using AlmexMaintePurchaseFile.Mail;
using AlmexMaintePurchaseFile.Settings;
using MwsLib.BaseFactory.CloudBackup;
using MwsLib.BaseFactory.Junp.View;
using MwsLib.Common;
using MwsLib.DB.SqlServer.AlmexMainte;
using MwsLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AlmexMaintePurchaseFile
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
		public static AlmexMaintePurchaseFileSettings gSettings;

		/// <summary>
		/// プログラム名
		/// </summary>
		public const string PROC_NAME = "ｱﾙﾒｯｸｽ保守仕入ﾃﾞｰﾀ作成";

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

			gSettings = AlmexMaintePurchaseFileSettingsIF.GetSettings();

#if DEBUG
			CollectDate = new Date(2021, 1, 1);
#else
			// 集計日を当月初日に設定
			CollectDate = Date.Today.FirstDayOfTheMonth();
#endif

			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					string msg = OutputCsvFile(CollectDate);
					AlmexMaintePurchaseFileSettingsIF.SetSettings(gSettings);
					if (0 < msg.Length)
					{
						return 1;
					}
					return 0;
				}
			}
			Application.Run(new MainForm());
			return 0;
		}

		/// <summary>
		/// アルメックス保守仕入データ.csv
		/// </summary>
		/// <param name="settings">出力ファイルパス名</param>
		/// <param name="collectDate">集計日</param>
		/// <returns>エラーメッセージ</returns>
		public static string OutputCsvFile(Date collectDate)
		{
			try
			{
				// アルメックス保守仕入データ.csvの出力
				using (var sw = new System.IO.StreamWriter(gSettings.Pathname, false, System.Text.Encoding.GetEncoding("shift_jis")))
				{
					List<CloudBackupEarningsData> stockList = new List<CloudBackupEarningsData>();

					List<vMicPCA売上明細> pcaList = AlmexMainteAccess.GetAlmexMainteEarningsList(gSettings.GetAlmexMainteGoods(), collectDate.ToYearMonth().ToSpan(), DATABASE_ACCESS_CT);
					if (0 < pcaList.Count)
					{
						foreach (vMicPCA売上明細 pca in pcaList)
						{
							if (0 != pca.数量)
							{
								AlmexMainteGoods goods = gSettings.AlmexMainteGoodsList.Find(p => p.商品コード == pca.sykd_scd);
								if (null != goods)
								{
									CloudBackupEarningsData sale = new CloudBackupEarningsData();
									sale.f仕入先コード = goods.仕入先;
									sale.f部門コード = pca.sykd_jbmn;
									sale.f担当者コード = pca.sykd_jtan;
									sale.f仕入商品コード = goods.仕入商品コード;
									sale.f単位 = pca.sykd_tani;
									sale.f仕入価格 = goods.仕入価格;
									sale.f売上日 = pca.sykd_uribi;
									sale.f仕入フラグ = goods.仕入フラグ;
									sale.f消費税率 = pca.消費税;
									sale.f数量 = pca.数量;

									vMicPCA商品マスタ mst = JunpDatabaseAccess.Select_vMicPCA商品マスタ(goods.仕入商品コード, DATABASE_ACCESS_CT);
									if (null != mst)
									{
										sale.f商品名 = mst.sms_mei;
									}
									stockList.Add(sale);
								}
							}
						}
						int plusNo = gSettings.InitDenNo; // '20801番台
						foreach (CloudBackupEarningsData stock in stockList)
						{
							string str = stock.ToPurchase(plusNo, gSettings.PcaVersion);
							sw.WriteLine(str);
							plusNo++;
						}
					}
					// 経理部にメール送信
					//SendMailControl.AlmexMainteSendMail(stockList);
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
