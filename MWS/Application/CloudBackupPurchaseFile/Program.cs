//
// Program.cs
// 
// クラウドバックアップ仕入データ作成 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/10/06 勝呂)
// Ver1.01 仕入データの17:区(0:通常仕入, 1:返品, 2:単価訂正)を2から1に変更(2021/01/08 勝呂)
//
using CloudBackupPurchaseFile.Forms;
using CloudBackupPurchaseFile.Mail;
using CloudBackupPurchaseFile.Settings;
using MwsLib.BaseFactory.CloudBackup;
using MwsLib.BaseFactory.Junp.View;
using MwsLib.Common;
using MwsLib.DB.SqlServer.CloudBackup;
using MwsLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CloudBackupPurchaseFile
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
		public static CloudBackupPurchaseFileSettings gSettings;

		/// <summary>
		/// プログラム名
		/// </summary>
		public const string PROC_NAME = "クラウドバックアップ仕入データ作成";

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

			gSettings = CloudBackupPurchaseFileSettingsIF.GetSettings();

#if DEBUG
			CollectDate = new Date(2020, 12, 1);
#else
			// 集計日を先月初日に設定
			CollectDate = Date.Today.FirstDayOfLasMonth();
#endif

			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					string msg = OutputCsvFile(CollectDate);
					CloudBackupPurchaseFileSettingsIF.SetSettings(gSettings);
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
		/// クラウドバックアップ仕入データ.csv
		/// </summary>
		/// <param name="settings">出力ファイルパス名</param>
		/// <param name="collectDate">集計日</param>
		/// <returns>エラーメッセージ</returns>
		public static string OutputCsvFile(Date collectDate)
		{
			try
			{
				// クラウドバックアップ仕入データ.csvの出力
				using (var sw = new System.IO.StreamWriter(gSettings.Pathname, false, System.Text.Encoding.GetEncoding("shift_jis")))
				{
					List<CloudBackupEarningsData> stockList = new List<CloudBackupEarningsData>();

					List<GroupMicPCA売上明細> pcaList = CloudBackupAccess.GetCloudBackupEarningsList(gSettings.GetCloudBackupGoods(), collectDate.ToYearMonth().ToSpan(), DATABASE_ACCESS_CT);
					if (0 < pcaList.Count)
					{
						//var query = from PCA売上明細 in pcaList
						//			orderby PCA売上明細.sykd_jbmn, PCA売上明細.sykd_uribi, PCA売上明細.sykd_scd
						//			group PCA売上明細 by new { PCA売上明細.sykd_jbmn, PCA売上明細.sykd_jtan, PCA売上明細.sykd_scd, PCA売上明細.sykd_mkbn, PCA売上明細.sykd_tani, PCA売上明細.sykd_uribi, PCA売上明細.sykd_rate } into X
						//			select new { X.Key.sykd_jbmn, X.Key.sykd_jtan, X.Key.sykd_scd, X.Key.sykd_mkbn, X.Key.sykd_tani, X.Key.sykd_uribi, X.Key.sykd_rate, 数量 = X.Sum(x => (int)x.sykd_suryo) };

						foreach (GroupMicPCA売上明細 pca in pcaList)
						{
							if (0 != pca.数量)
							{
								CloudBackupGoods goods = gSettings.CloudBackupGoodsList.Find(p => p.商品コード == pca.sykd_scd);
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
									sale.f消費税率 = (short)pca.消費税率;

									// PC安心サポート Plus3年契約 仕入数36
									// PC安心サポート Plus1年契約 仕入数12
									// PC安心サポート Plus1年更新 仕入数12
									// MWS ｸﾗｳﾄﾞﾊﾞｯｸｱｯﾌﾟ(PC安心ｻﾎﾟｰﾄ Plus) 仕入数1*数量
									// MWS ｸﾗｳﾄﾞﾊﾞｯｸｱｯﾌﾟ(月額) 仕入数1*数量
									sale.f数量 = goods.仕入数 * pca.数量;

									vMicPCA商品マスタ mst = JunpDatabaseAccess.Select_vMicPCA商品マスタ(goods.仕入商品コード, DATABASE_ACCESS_CT);
									if (null != mst)
									{
										sale.f商品名 = mst.sms_mei;
									}
									stockList.Add(sale);
								}
							}
						}
						int plusNo = gSettings.InitDenNo; // '20500番台（りすとん=20 office365=40）
						foreach (CloudBackupEarningsData stock in stockList)
						{
							string str = stock.ToPurchase(plusNo, gSettings.PcaVersion);
							sw.WriteLine(str);
							plusNo++;
						}
					}
					// 営業管理部にメール送信
					SendMailControl.SoftwareMainteSendMail(stockList);
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
