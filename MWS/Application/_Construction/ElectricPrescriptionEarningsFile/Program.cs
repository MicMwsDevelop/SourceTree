//
// Program.cs
//
// 電子処方箋管理売上データ作成 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
//////////////////////////////////////////////////////////////////
// 【処理内容】
// 1:売上データ作成処理
// ・当月初日に電子処方箋管理契約情報から先月申込分を抽出
// ・電子処方箋管理サービスの売上データを作成
// ・顧客管理利用情報に月額課金のサービスを登録
// ・処理終了後、経理部宛に売上データファイルの作成をメールで連絡
//
// 2:利用申込取消処理
// ・当月末日に電子処方箋管理契約情報から今月申込分を抽出
// ・申込情報の電子処方箋管理サービスの利用申込を抽出
// ・申込情報の利用申込を取り消して更新する
// ・処理終了後、システム管理部宛に利用申込の取り消しをメールで連絡
//////////////////////////////////////////////////////////////////
// Ver1.00(2024/07/01 勝呂):新規作成
//
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Charlie.View;
using CommonLib.BaseFactory.ElectricPrescription;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.ElectricPrescription;
using CommonLib.DB.SqlServer.Junp;
using CommonLib.DB.SqlServer.OnlineLicenseHomon;
using ElectricPrescriptionEarningsFile.Log;
using ElectricPrescriptionEarningsFile.Mail;
using ElectricPrescriptionEarningsFile.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ElectricPrescriptionEarningsFile
{
	internal static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string gProcName = "電子処方箋管理売上データ作成";

		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string gVersionStr = "1.00";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static ElectricPrescriptionEarningsFileSettings gSettings { get; set; }

		/// <summary>
		/// 起動日
		/// </summary>
		public static Date gBootDate;

		/// <summary>
		/// 出力ファイル名
		/// </summary>
		public static string gFormalFilename;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static int Main()
		{
			int ret = 0;

			// Mutexオブジェクトを作成する ※多重起動禁止
			Mutex mutex = new Mutex(false, gProcName);

			bool hasHandle = false;
			try
			{
				try
				{
					// ミューテックスの所有権を要求する
					hasHandle = mutex.WaitOne(0, false);
				}
				//.NET Framework 2.0以降の場合
				catch (AbandonedMutexException)
				{
					// 別のアプリケーションがミューテックスを解放しないで終了した時
					hasHandle = true;
				}
				// ミューテックスを得られたか調べる
				if (hasHandle == false)
				{
					// 得られなかった場合は、すでに起動していると判断して終了
					//MessageBox.Show("多重起動はできません。");
					return 0;
				}
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				gSettings = ElectricPrescriptionEarningsFileSettingsIF.GetSettings();
#if DEBUG
				gBootDate = new Date(2024, 6, 1);
#else
				gBootDate = Date.Today;
#endif
				string[] cmds = Environment.GetCommandLineArgs();
				if (2 <= cmds.Length)
				{
					string msg = string.Empty;
					switch (cmds[1])
					{
						// 売上データ作成処理
						case "1":
							msg = ExportEarningsFile(gBootDate);
							break;
						// 利用申込取消処理
						case "2":
							msg = CancelUseApplyInfo(gBootDate);
							break;
					}
					if (0 < msg.Length)
					{
						ret = 1;
					}
				}
				else
				{
					Application.Run(new Forms.MainForm());
				}
			}
			finally
			{
				if (hasHandle)
				{
					// ミューテックスを解放する
					mutex.ReleaseMutex();
				}
				mutex.Close();
			}
			return ret;
		}

		/// <summary>
		/// 電子処方箋管理売上データ作成.csvの出力
		/// </summary>
		/// <param name="exeDate">実行日</param>
		/// <returns>エラーメッセージ</returns>
		public static string ExportEarningsFile(Date exeDate)
		{
			// 申込月：先月初日
			Date applyDate = exeDate.FirstDayOfLasMonth();
			try
			{
				// 出力ファイル名
				gFormalFilename = gSettings.FormalFilename;

				// 電子処方箋管理契約情報の取得
				List<ElectricPrescriptionEarningsOut> saleList = ElectricPrescriptionAccess.GetElectricPrescriptionEarningsOut(applyDate, gSettings.ConnectCharlie.ConnectionString);
				if (null != saleList && 0 < saleList.Count)
				{
					// 売上日：当月初日
					Date saleDate = exeDate.FirstDayOfTheMonth();

					// 消費税
					int taxRate = JunpDatabaseAccess.GetTaxRate(saleDate, gSettings.ConnectJunp.ConnectionString);

					// 中間ファイルの出力
					using (var sw = new StreamWriter(gSettings.TemporaryPathname, false, System.Text.Encoding.GetEncoding("shift_jis")))
					{
						// 伝票番号
						int no = gSettings.SlipInitialNumber;

						foreach (ElectricPrescriptionEarningsOut sale in saleList)
						{
							if (0 == sale.請求先コード.Length)
							{
								// 請求先がユーザーと同一
								sw.WriteLine(sale.ToEarnings(no, sale.得意先コード, saleDate, sale.契約開始日, sale.契約終了日, taxRate, gSettings.PcaVersion));
							}
							else
							{
								// 請求先がユーザーと異なる
								sw.WriteLine(sale.ToEarnings(no, sale.請求先コード, saleDate, sale.契約開始日, sale.契約終了日, taxRate, gSettings.PcaVersion));

								// ○○○○様分 を記事行１を追加
								sw.WriteLine(sale.ToArticle1(no, sale.請求先コード, saleDate, sale.契約開始日, sale.契約終了日, gSettings.PcaVersion));

								// 得意先No. を記事行２を追加
								sw.WriteLine(sale.ToArticle2(no, sale.請求先コード, saleDate, sale.契約開始日, sale.契約終了日, gSettings.PcaVersion));
							}
							no++;
						}
					}
					// 中間ファイルをリネームして出力ファルダにコピー
					File.Copy(gSettings.TemporaryPathname, gSettings.FormalPathname(gFormalFilename));

#if !DebugNoWrite
					foreach (ElectricPrescriptionEarningsOut sale in saleList)
					{
						// 電子処方箋管理契約情報の売上日時の設定
						ElectricPrescriptionAccess.UpdateSetElectricPrescriptionSaleDate(sale, gProcName, gSettings.ConnectCharlie.ConnectionString);

						// 顧客管理利用情報に電子処方箋管理サービスの登録
						ElectricPrescriptionAccess.InsertIntoCustomerUseInformation(sale, gProcName, gSettings.ConnectCharlie.ConnectionString);
					}
#endif
				}
				else
				{
					// 電子処方箋管理売上データ.csvの出力
					using (var sw = new StreamWriter(gSettings.FormalPathname(gFormalFilename), false, System.Text.Encoding.GetEncoding("shift_jis")))
					{
						// 売上データファイルを空で出力する
						;
					}
				}
				// 電子処方箋管理サービス 売上連絡メール送信（経理課宛）
				SendMailControl.ExportEarningsFileSendMail(saleList, gFormalFilename);
			}
			catch (Exception ex)
			{
				ErrorLogOut.Out(ex.Message);
				return ex.Message;
			}
			return string.Empty;
		}

		/// <summary>
		/// 利用申込の取消
		/// </summary>
		/// <param name="exeDate">実行日</param>
		public static string CancelUseApplyInfo(Date exeDate)
		{
			// 申込月：当月初日
			Date applyDate = exeDate.FirstDayOfTheMonth();
			try
			{
				// 当月分の電子処方箋管理契約情報の取得
				string whereStr = string.Format("(CONVERT(int, CONVERT(NVARCHAR, ApplyDate, 112)) BETWEEN {0} AND {1})"
																, applyDate.FirstDayOfTheMonth().ToIntYMD()     // 0 当月初日
																, applyDate.LastDayOfTheMonth().ToIntYMD());    // 1 当月末日

				List<V_COUPLER_APPLY> applyList = new List<V_COUPLER_APPLY>();
				List<T_USE_ELECTRIC_PRESCRIPTION> prescriptionList = CharlieDatabaseAccess.Select_T_USE_ELECTRIC_PRESCRIPTION(whereStr, "ApplyNo", gSettings.ConnectCharlie.ConnectionString);
				if (null != prescriptionList && 0 < prescriptionList.Count)
				{
					// 申込情報から当月分の電子処方箋管理サービスを取得
					foreach (T_USE_ELECTRIC_PRESCRIPTION prescription in prescriptionList)
					{
						if (0 < prescription.CouplerApplyID)
						{
							whereStr = string.Format("apply_id = {0}", prescription.CouplerApplyID);   // カプラー申込ID
							List<V_COUPLER_APPLY> list = CharlieDatabaseAccess.Select_V_COUPLER_APPLY(whereStr, "", gSettings.ConnectCharlie.ConnectionString);
							if (null != list && 0 < list.Count)
							{
								applyList.AddRange(list);
							}
						}
						else
						{
							// カプラー申込IDが未格納
							ErrorLogOut.Out(string.Format("カプラー申込IDが未格納。顧客No：{0}、 申込日時：{1}", prescription.CustomerID, prescription.ApplyDate.ToString()));
						}
					}
					if (0 < applyList.Count)
					{
#if !DebugNoWrite
						// 申込情報の利用申込のシステム反映済フラグを取消に設定
						OnlineLicenseHomonAccess.UpdateSetCouplerApplySystemFlg(applyList, gProcName, gSettings.ConnectCharlie.ConnectionString, gSettings.ConnectCoupler.DatabaseName);
#endif
					}
				}
				// サービス名の取得
				whereStr = string.Format("SERVICE_ID IN ({0}, {1})", (int)ServiceCodeDefine.ServiceCode.ElectricPrescriptionOutside, (int)ServiceCodeDefine.ServiceCode.ElectricPrescriptionInside);
				List<M_SERVICE> svList = CharlieDatabaseAccess.Select_M_SERVICE(whereStr, "SERVICE_ID", gSettings.ConnectCharlie.ConnectionString);

				// 電子処方箋管理サービス 利用申込取消 利用申込取消 連絡メール送信（社内システム維持管理宛）
				SendMailControl.CancelUseApplyInfoSendMail(applyList, svList);
			}
			catch (Exception ex)
			{
				ErrorLogOut.Out(ex.Message);
				return ex.Message;
			}
			return string.Empty;
		}
	}
}
