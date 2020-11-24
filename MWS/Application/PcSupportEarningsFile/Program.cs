//
// Program.cs
// 
// PC安心サポート継続売上データ作成 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/11/02 勝呂)
//
using MwsLib.BaseFactory;
using MwsLib.BaseFactory.Charlie.Table;
using MwsLib.BaseFactory.PcSupportEarnings;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Charlie;
using MwsLib.DB.SqlServer.Junp;
using MwsLib.DB.SqlServer.PcSupportEarnings;
using PcSupportEarningsFile.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PcSupportEarningsFile
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
		public static PcSupportEarningsFileSettings gSettings;

		/// <summary>
		/// プログラム名
		/// </summary>
		public const string PROC_NAME = "PC安心サポート継続売上データ作成";

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

			gSettings = PcSupportEarningsFileSettingsIF.GetSettings();

#if DEBUG
			CollectDate = new Date(2020, 11, 1);
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
					PcSupportEarningsFileSettingsIF.SetSettings(gSettings);
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
				// PC安心サポート継続売上データ.csvの出力
				using (var sw = new StreamWriter(gSettings.Pathname, false, System.Text.Encoding.GetEncoding("shift_jis")))
				{
					List<PcSupportEarningsOut> saleList = new List<PcSupportEarningsOut>();

					// PC安心サポート契約情報から自動更新対象の取得
					List<T_USE_PCCSUPPORT> pcList = CharlieDatabaseAccess.Select_T_USE_PCCSUPPORT(string.Format("fEndFlag = '0' AND convert(int, convert(nvarchar, fContractEndDate, 112)) = {0}", CollectDate.LastDayOfTheMonth().ToIntYMD()), "fCustomerID ASC", DATABASE_ACCESS_CT);
					if (0 < pcList.Count)
					{
						// 伝票番号
						int no = gSettings.SlipInitialNumber;

						// 売上日は先月末日
						Date saleDate = collectDate.LastDayOfLasMonth();

						// 先月末日の消費税
						int taxRate = JunpDatabaseAccess.GetTaxRate(saleDate, DATABASE_ACCESS_CT);

						List<string> csvList = new List<string>();
						foreach (T_USE_PCCSUPPORT pc in pcList)
						{
							// 顧客Noに売上データ必須情報の取得
							PcSupportEarningsOut sale = PcSupportEarningsAccess.GetPcSupportEarningsOut(pc.fCustomerID, DATABASE_ACCESS_CT);
							if (null != sale)
							{
								if (pc.fContractEndDate.HasValue && pc.fBillingEndDate.HasValue)
								{
									// 利用期間を１年後に設定
									sale.f利用期間 = new Span(pc.fBillingStartDate.Value, pc.fBillingEndDate.Value);
									sale.Set利用期間(pc.fContractEndDate.Value);

									// 売上データ追加
									if (0 == sale.f請求先コード.Length)
									{
										// 請求先がユーザーと同一
										csvList.Add(sale.ToEarnings(no, sale.f得意先コード, saleDate, taxRate, gSettings.PcaVersion));
									}
									else
									{
										// 請求先がユーザーと異なる
										csvList.Add(sale.ToEarnings(no, sale.f請求先コード, saleDate, taxRate, gSettings.PcaVersion));

										// ○○○○様分 を記事行１を追加
										csvList.Add(sale.ToArticle1(no, sale.f請求先コード, saleDate, gSettings.PcaVersion));

										// 得意先No. を記事行２を追加
										// ○○○○様分 を記事行１を追加
										csvList.Add(sale.ToArticle2(no, sale.f請求先コード, saleDate, gSettings.PcaVersion));
									}
									no++;
									saleList.Add(sale);

									// PC安心サポート契約情報の更新
									// 利用終了日を１年間延長
									pc.fContractEndDate = pc.fBillingEndDate = sale.f利用期間.End;
									pc.fContractEndDate.Value.PlusYears(1).LastDayOfTheMonth();

									// 商品IDをPC安心サポート１年契約（更新用）
									if (PcaGoodsIDDefine.PcSupport1Continue != pc.fGoodsID)
									{
										pc.fGoodsID = PcaGoodsIDDefine.PcSupport1Continue;
									}
								}
							}
						}
						if (0 < csvList.Count)
						{
							foreach (string str in csvList)
							{
								sw.WriteLine(str);
							}
							// PC安心サポート契約情報の更新
							// 利用終了日を１年更新
							// 商品IDの更新
							foreach (T_USE_PCCSUPPORT pc in pcList)
							{
#if !DEBUG
								PcSupportEarningsAccess.UpdatePcSupportInfo(pc, PROC_NAME, DATABASE_ACCESS_CT);
#endif
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
