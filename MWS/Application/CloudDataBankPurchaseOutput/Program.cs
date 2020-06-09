//
// Program.cs
// 
// クラウドデータバンク仕入データ作成 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/03/06 勝呂)
//
using CloudDataBankPurchaseOutput.Forms;
using CloudDataBankPurchaseOutput.Settings;
using MwsLib.BaseFactory.CloudDataBank;
using MwsLib.BaseFactory.Junp.View;
using MwsLib.Common;
using MwsLib.DB.SqlServer.CloudDataBank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CloudDataBankPurchaseOutput
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
		public const string PROC_NAME = "クラウドデータバンク仕入データ作成";

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
					Date date = new Date(2020, 2, 1);
					string msg = OutputCsvFile(CloudDataBankPurchaseOutputSettingsIF.GetSettings(), date);
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
		/// <param name="settings">出力ファイルパス名</param>
		/// <param name="date">検索対象日</param>
		/// <returns>エラーメッセージ</returns>
		public static string OutputCsvFile(CloudDataBankPurchaseOutputSettings settings, Date date)
		{
			try
			{
				// クラウドデータバンク仕入データ.csvの出力
				using (var sw = new System.IO.StreamWriter(settings.Pathname, false))
				{
					List<vMicPCA売上明細> pcaList = CloudDataBankAccess.GetCloudDataBankEarningsList(settings.GetCloudDataBankGoods(), date.ToYearMonth().ToSpan(), DATABASE_ACCESS_CT);
					if (0 < pcaList.Count)
					{
						var query = from PCA売上明細 in pcaList
									orderby PCA売上明細.sykd_jbmn, PCA売上明細.sykd_uribi, PCA売上明細.sykd_scd
									group PCA売上明細 by new { PCA売上明細.sykd_jbmn, PCA売上明細.sykd_jtan, PCA売上明細.sykd_scd, PCA売上明細.sykd_mkbn, PCA売上明細.sykd_tani, PCA売上明細.sykd_uribi, PCA売上明細.sykd_rate } into X
									select new { X.Key.sykd_jbmn, X.Key.sykd_jtan, X.Key.sykd_scd, X.Key.sykd_mkbn, X.Key.sykd_tani, X.Key.sykd_uribi, X.Key.sykd_rate, 数量 = X.Sum(x => (int)x.sykd_suryo) };

						List<CloudDataBankEarningsData> list = new List<CloudDataBankEarningsData>();
						foreach (var pca in query)
						{
							CloudDataBankGoods goods = settings.CloudDataBankGoodsList.Find(p => p.商品コード == pca.sykd_scd);
							if (null != goods)
							{
								CloudDataBankEarningsData data = new CloudDataBankEarningsData();
								data.仕入先コード = goods.仕入先;
								data.部門コード = pca.sykd_jbmn;
								data.担当者コード = pca.sykd_jtan;
								data.仕入商品コード = goods.仕入商品コード;
								data.数量 = pca.数量;
								data.単位 = pca.sykd_tani;
								data.仕入価格 = goods.仕入価格;
								data.売上日 = pca.sykd_uribi;
								data.仕入フラグ = goods.仕入フラグ;
								data.消費税率 = (short)pca.sykd_rate;
								List<vMicPCA商品マスタ> mst = CloudDataBankAccess.GetPCA商品マスタ(goods.仕入商品コード, DATABASE_ACCESS_CT);
								if (0 < mst.Count)
								{
									data.商品名 = mst.First().sms_mei;
								}
								list.Add(data);
							}
						}
						int plusNo = 20060; // '20060番台（りすとん=20 office365=40）
						foreach (CloudDataBankEarningsData data in list)
						{
							string str = data.ToPurchase(plusNo, settings.PcaVersion);
							sw.WriteLine(str);
							plusNo++;
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
