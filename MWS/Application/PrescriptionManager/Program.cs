//
// Program.cs
// 
// 電子処方箋契約情報管理 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/02/14 勝呂):新規作成
//
using CommonLib.BaseFactory.PrescriptionManager;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using CommonLib.DB.SqlServer.PrescriptionManager;
using PrescriptionManager.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PrescriptionManager
{
	internal static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string PROC_NAME = "電子処方箋契約情報管理";

		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string VersionStr = "Ver1.00(2023/02/14)";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static PrescriptionManagerSettings gSettings;

		/// <summary>
		/// 実行日
		/// </summary>
		public static Date gBootDate;

		/// <summary>
		/// 売上明細ファイル名
		/// </summary>
		public static string gEarningsFilename;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static int Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			gSettings = PrescriptionManagerSettingsIF.GetSettings();

#if DEBUG
			gBootDate = new Date(2023, 3, 1);
#else
			gBootDate = Date.Today;
#endif

			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					// サイレントモード
					try
					{
						// 当月初日
						Date firstDate = gBootDate.FirstDayOfTheMonth();

						// 電子処方箋契約情報から運用開始日が先月の医院を取得
						List<PrescriptionEarnings> pcaList = PrescriptionManagerAccess.GetPrescriptionEarnings(firstDate, gSettings.ConnectCharlie.ConnectionString);

						// 売上明細ファイルの出力
						ExportEarningsFile(firstDate, pcaList);

						// 電子処方箋契約情報の課金期間の設定
						UpdateUsePrescription(firstDate, pcaList);

						PrescriptionManagerSettingsIF.SetSettings(gSettings);
					}
					catch
					{
						return 1;
					}
					return 0;
				}
			}
			//Application.Run(new Forms.MainForm());
			Application.Run(new Forms.ManagerForm());
			return 0;
		}

		/// <summary>
		/// 売上明細ファイルの出力
		/// </summary>
		/// <param name="firstDate">当月初日</param>
		/// <param name="pcaList">電子処方箋契約情報から運用開始日が先月の医院のリスト</param>
		public static void ExportEarningsFile(Date firstDate, List<PrescriptionEarnings> pcaList)
		{
			// 伝票番号
			int no = gSettings.SlipInitialNumber;

			// 売上明細ファイル名の設定
			gEarningsFilename = gSettings.FormalFilename;

			try
			{
				if (null != pcaList && 0 < pcaList.Count)
				{
					// 消費税
					int taxRate = JunpDatabaseAccess.GetTaxRate(firstDate, gSettings.ConnectCharlie.ConnectionString);

					// 中間ファイルの出力
					using (var sw = new StreamWriter(gSettings.TemporaryPathname, false, System.Text.Encoding.GetEncoding("shift_jis")))
					{
						foreach (PrescriptionEarnings pca in pcaList)
						{
							if (0 == pca.請求先コード.Length)
							{
								// 販売先がユーザーと同一
								sw.WriteLine(pca.ToEarnings(no, pca.得意先コード, pca.顧客名, firstDate, pca.契約期間, taxRate, gSettings.PcaVersion));
							}
							else
							{
								// 販売先が得意先でなく請求先
								sw.WriteLine(pca.ToEarnings(no, pca.請求先コード, pca.請求先名, firstDate, pca.契約期間, taxRate, gSettings.PcaVersion));

								// {顧客名}様分 を記事行１を追加
								sw.WriteLine(pca.ToEarningsArticle1(no, firstDate, pca.契約期間, gSettings.PcaVersion));

								// No.{得意先コード} を記事行２を追加
								sw.WriteLine(pca.ToEarningsArticle2(no, firstDate, pca.契約期間, gSettings.PcaVersion));
							}
							no++;
						}
					}
					// 中間ファイルをリネームして出力ファルダにコピー
					File.Copy(gSettings.TemporaryPathname, gSettings.FormalPathname(gEarningsFilename));
				}
				else
				{
					// 売上明細ファイルの出力
					using (var sw = new StreamWriter(gSettings.FormalPathname(gEarningsFilename), false, System.Text.Encoding.GetEncoding("shift_jis")))
					{
						// 売上がなくても売上明細ファイルは出力する
						;
					}
				}
				// 営業管理部にメール送信
				//SendMailControl.AlmexMainteSendMail(saleList, gEarningsFilename, firstDate);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("ExportEarningsFile() Error : {0}", ex.Message));
			}
		}

		/// <summary>
		/// 電子処方箋契約情報の課金期間の設定
		/// </summary>
		/// <param name="firstDate"></param>
		/// <param name="pcaList"></param>
		/// <returns></returns>
		public static void UpdateUsePrescription(Date firstDate, List<PrescriptionEarnings> pcaList)
		{
			if (null != pcaList && 0 < pcaList.Count)
			{
				try
				{
					string where = string.Empty;
					foreach (PrescriptionEarnings pca in pcaList)
					{
						if (0 < where.Length)
						{
							where += ", ";
						}
						where += pca.申込No.ToString();
					}
					List<UsePrescription> useList = PrescriptionManagerAccess.GetUsePrescription(gSettings.ConnectCharlie.ConnectionString, "WHERE [fContractID] IN (" + where + ")");
					foreach (UsePrescription use in useList)
					{
						// 課金期間に契約期間を設定
						use.BillingStartDate = use.ContractStartDate;
						use.BillingEndDate = use.ContractEndDate;

						// 電子処方箋契約情報の課金期間の設定
						PrescriptionManagerAccess.UpdateSetUsePrescriptionBilling(use, Program.PROC_NAME, gSettings.ConnectCharlie.ConnectionString);
					}
				}
				catch (Exception ex) 
				{
					throw new Exception(string.Format("UpdateUsePrescription() Error : {0}", ex.Message));
				}
			}
		}
	}
}
