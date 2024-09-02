//
// Program.cs
//
// オン資電子処方箋売上データ作成 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
//////////////////////////////////////////////////////////////////
// 【処理内容】
// 1. 「オン資電子処方箋連携契約情報」から先月利用申込分を抽出する
// 2. 売上明細データを作成する
// 3. 出力先フォルダにオン資電子処方箋売上データ.csv を出力
// 4. 「オン資電子処方箋連携契約情報」の売上作成日時に売上データの出力日時を設定する
// 5. 顧客管理利用情報にｵﾝﾗｲﾝ資格確認電子処方箋連携を60ヵ月の利用期間で登録する
// 6. 経理課に対し、「オン歯電子処方箋連携費 売上連絡」メールを送信する
// 7. カプラーDB「申込情報」からカプラー申込ID に該当する情報を抽出する
// 8. カプラーDB「申込情報」のシステム反映済フラグをシステム反映に更新する
// 9. システム管理部に対し、「[ｵﾝﾗｲﾝ資格確認電子処方箋連携] 利用申込更新」メールを送信する
//////////////////////////////////////////////////////////////////
// Ver1.00(2024/08/27 勝呂):新規作成
//
using CommonLib.BaseFactory.Charlie.View;
using CommonLib.BaseFactory.OnlineElectricPrescript;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.Junp;
using CommonLib.DB.SqlServer.OnlineElectricPrescript;
using OnlineElectricPrescriptEarningsFile.Log;
using OnlineElectricPrescriptEarningsFile.Mail;
using OnlineElectricPrescriptEarningsFile.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace OnlineElectricPrescriptEarningsFile
{
	internal static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string gProcName = "オン資電子処方箋売上データ作成";

		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string gVersionStr = "1.00";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static OnlineElectricPrescriptEarningsFileSettings gSettings { get; set; }

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

				gSettings = OnlineElectricPrescriptEarningsFileSettingsIF.GetSettings();
#if DEBUG
				gBootDate = new Date(2024, 9, 1);
#else
				gBootDate = Date.Today;
#endif
				string[] cmds = Environment.GetCommandLineArgs();
				if (2 <= cmds.Length)
				{
					if ("AUTO" == cmds[1].ToUpper())
					{
						// サイレントモード
						string msg = Exec(gBootDate);
						if (0 < msg.Length)
						{
							ret = 1;
						}
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
		/// 実行
		/// </summary>
		/// <param name="bootDate">当日</param>
		/// <returns>エラーメッセージ</returns>
		public static string Exec(Date bootDate)
		{
			// オン資電子処方箋売上データ.csvの出力
			string msg;
			List<OnlineElectricPrescriptEarningsFileOut> saleList = ExportEarningsFile(bootDate, out msg);
			if (0 == msg.Length)
			{
				// 利用申込情報のシステム反映済フラグを反映済に更新
				msg = UpdateCouplerApply(saleList);
			}
			return msg;
		}

		/// <summary>
		/// オン資電子処方箋売上データ作成.csvの出力
		/// </summary>
		/// <param name="bootDate">当日</param>
		/// <param name="msg">エラーメッセージ</param>
		/// <returns>オン資電子処方箋連携契約情報リスト</returns>
		public static List<OnlineElectricPrescriptEarningsFileOut> ExportEarningsFile(Date bootDate, out string msg)
		{
			msg = string.Empty;

			// 申込月：先月初日
			Date applyDate = bootDate.FirstDayOfLasMonth();
			try
			{
				// 出力ファイル名
				gFormalFilename = gSettings.FormalFilename;

				// オン資電子処方箋連携契約情報の取得
				List<OnlineElectricPrescriptEarningsFileOut> saleList = OnlineElectricPrescriptAccess.GetElectricPrescriptionEarningsOut(applyDate, gSettings.ConnectCharlie.ConnectionString);
				if (null != saleList && 0 < saleList.Count)
				{
					// 売上日：当月初日
					Date saleDate = bootDate.FirstDayOfTheMonth();

					// 消費税
					int taxRate = JunpDatabaseAccess.GetTaxRate(saleDate, gSettings.ConnectJunp.ConnectionString);

					// 中間ファイルの出力
					using (var sw = new StreamWriter(gSettings.TemporaryPathname, false, System.Text.Encoding.GetEncoding("shift_jis")))
					{
						// 伝票番号
						int no = gSettings.SlipInitialNumber;

						foreach (OnlineElectricPrescriptEarningsFileOut sale in saleList)
						{
							sale.売上日時 = saleDate.ToDateTime();

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
					foreach (OnlineElectricPrescriptEarningsFileOut sale in saleList)
					{
						// オン資電子処方箋連携契約情報の売上日時の設定
						OnlineElectricPrescriptAccess.UpdateSetOnlineElectricPrescriptSaleDate(sale, gProcName, gSettings.ConnectCharlie.ConnectionString);

						// 顧客管理利用情報にｵﾝﾗｲﾝ資格確認電子処方箋連携サービスの登録
						OnlineElectricPrescriptAccess.SetCustomerUseInformation(sale, gProcName, gSettings.ConnectCharlie.ConnectionString);
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
				// オン歯電子処方箋連携費 売上連絡メール送信（経理課宛）
				SendMailControl.ExportEarningsFileSendMail(saleList, gFormalFilename);

				return saleList;
			}
			catch (Exception ex)
			{
				ErrorLogOut.Out(ex.Message);
				msg = ex.Message;
			}
			return null;
		}

		/// <summary>
		/// 利用申込情報のシステム反映済フラグを反映済に更新
		/// </summary>
		/// <param name="saleList">オン資電子処方箋連携費売上情報リスト</param>
		public static string UpdateCouplerApply(List<OnlineElectricPrescriptEarningsFileOut> saleList)
		{
			try
			{
				List<V_COUPLER_APPLY> applyList = new List<V_COUPLER_APPLY>();
				if (null != saleList && 0 < saleList.Count)
				{
					// 申込情報から先月分のオン歯電子処方箋連携費を取得
					foreach (OnlineElectricPrescriptEarningsFileOut sale in saleList)
					{
						if (0 < sale.CouplerApplyID)
						{
							string whereStr = string.Format("apply_id = {0}", sale.CouplerApplyID);   // カプラー申込ID
							List<V_COUPLER_APPLY> list = CharlieDatabaseAccess.Select_V_COUPLER_APPLY(whereStr, "", gSettings.ConnectCharlie.ConnectionString);
							if (null != list && 0 < list.Count)
							{
								applyList.AddRange(list);
							}
						}
						else
						{
							// カプラー申込IDが未格納
							ErrorLogOut.Out(string.Format("カプラー申込IDが未格納。顧客No：{0}、 申込日時：{1}", sale.顧客No, sale.申込日時.ToString()));
						}
					}
					if (0 < applyList.Count)
					{
#if !DebugNoWrite
						// 申込情報の利用申込のシステム反映済フラグをシステム反映に設定
						OnlineElectricPrescriptAccess.UpdateSetCouplerApplySystemFlg(applyList, gProcName, gSettings.ConnectCharlie.ConnectionString, gSettings.ConnectCoupler.DatabaseName);
#endif
					}
				}
				// [ｵﾝﾗｲﾝ資格確認電子処方箋連携] 利用申込更新 連絡メール送信（社内システム維持管理宛）
				SendMailControl.UpdateCouplerApplySendMail(applyList);
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
