//
// Program.cs
//
// オン資訪問診療売上データ作成 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
//////////////////////////////////////////////////////////////////
// アプリ管理サイト：APPID640 オン資訪問診療売上データ作成
//
// 【処理内容】
// 1. 「オン資訪問診療連携契約情報」から先月利用申込分を抽出する
// 2. 売上明細データを作成する
// 3. 出力先フォルダにオン資訪問診療売上データ.csvを出力
// 4. 「オン資訪問診療連携契約情報」の売上作成日時に売上データの出力日時を設定する
// 5. 顧客管理利用情報にオンライン資格確認訪問診療連携サービスを60ヵ月の利用期間で登録する
// 6. 経理課に対し、「オン資訪問診療連携費 売上連絡」メールを送信する
// 7. カプラーDB「申込情報」から「オン資訪問診療連携契約情報」のカプラー申込IDに該当する情報を抽出する
// 8. システム反映済フラグをシステム反映済にして情報を更新する
// 9. システム管理部に対し、「[ｵﾝﾗｲﾝ資格確認訪問診療連携] 利用申込更新」メールを送信する
//////////////////////////////////////////////////////////////////
// Ver1.00(2024/07/09 勝呂):新規作成
// Ver1.01(2024/07/29 勝呂):オン資訪問診療連携費 売上連絡メールの申込日時を利用期間に変更
// Ver1.02(2024/08/01 勝呂):オン資訪問診療契約情報の売上日時の更新時の障害対応
//
using CommonLib.BaseFactory.Charlie.View;
using CommonLib.BaseFactory.OnlineLicenseHomon;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.Junp;
using CommonLib.DB.SqlServer.OnlineLicenseHomon;
using OnlineLicenseHomonEarningsFile.Log;
using OnlineLicenseHomonEarningsFile.Mail;
using OnlineLicenseHomonEarningsFile.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace OnlineLicenseHomonEarningsFile
{
	internal static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string gProcName = "オン資訪問診療売上データ作成";

		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string gVersionStr = "1.02";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static OnlineLicenseHomonEarningsFileSettings gSettings { get; set; }

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

				gSettings = OnlineLicenseHomonEarningsFileSettingsIF.GetSettings();
#if DEBUG
				gBootDate = new Date(2024, 8, 1);
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
						if (0  < msg.Length)
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
			// オン資格訪問診療売上データ.csvの出力
			string msg;
			List<OnlineLicenseHomonEarningsOut> homonList = ExportEarningsFile(gBootDate, out msg);
			if (0 == msg.Length)
			{
				// 利用申込情報のシステム反映済フラグを反映済に更新
				msg = UpdateCouplerApply(homonList);
			}
			return msg;
		}

		/// <summary>
		/// オン資格訪問診療売上データ.csvの出力
		/// </summary>
		/// <param name="bootDate">当日</param>
		/// <param name="msg">エラーメッセージ</param>
		/// <returns>オン資訪問診療連携契約情報リスト</returns>
		private static List<OnlineLicenseHomonEarningsOut> ExportEarningsFile(Date bootDate, out string msg)
		{
			msg = string.Empty;

			// 利用申込月：先月初日
			Date applyDate = bootDate.FirstDayOfLasMonth();

			try
			{
				// 出力ファイル名
				gFormalFilename = gSettings.FormalFilename;

				// オン資訪問診療連携契約情報の取得
				List<OnlineLicenseHomonEarningsOut> homonList = OnlineLicenseHomonAccess.GetOnlineLicenseHomonEarningsOut(applyDate, gSettings.ConnectCharlie.ConnectionString);
				if (null != homonList && 0 < homonList.Count)
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

						foreach (OnlineLicenseHomonEarningsOut homon in homonList)
						{
							// Ver1.02(2024/08/01 勝呂):オン資訪問診療契約情報の売上日時の更新時の障害対応
							homon.売上日時 = saleDate.ToDateTime();

							if (0 == homon.請求先コード.Length)
							{
								// 請求先がユーザーと同一
								sw.WriteLine(homon.ToEarnings(no, homon.得意先コード, saleDate, homon.契約開始日, homon.契約終了日, taxRate, gSettings.PcaVersion));
							}
							else
							{
								// 請求先がユーザーと異なる
								sw.WriteLine(homon.ToEarnings(no, homon.請求先コード, saleDate, homon.契約開始日, homon.契約終了日, taxRate, gSettings.PcaVersion));

								// ○○○○様分 を記事行１を追加
								sw.WriteLine(homon.ToArticle1(no, homon.請求先コード, saleDate, homon.契約開始日, homon.契約終了日, gSettings.PcaVersion));

								// 得意先No. を記事行２を追加
								sw.WriteLine(homon.ToArticle2(no, homon.請求先コード, saleDate, homon.契約開始日, homon.契約終了日, gSettings.PcaVersion));
							}
							no++;
						}
					}
					// 中間ファイルをリネームして出力ファルダにコピー
					File.Copy(gSettings.TemporaryPathname, gSettings.FormalPathname(gFormalFilename));

#if !DebugNoWrite
					foreach (OnlineLicenseHomonEarningsOut homon in homonList)
					{
						// オン資訪問診療契約情報の売上日時の設定
						OnlineLicenseHomonAccess.UpdateSetOnlineLicenseHomonSaleDate(homon, gProcName, gSettings.ConnectCharlie.ConnectionString);

						// 顧客管理利用情報にサービスの登録
						OnlineLicenseHomonAccess.SetCustomerUseInformation(homon, gProcName, gSettings.ConnectCharlie.ConnectionString);
					}
#endif
				}
				else
				{
					// オン資格訪問診療売上データ.csvの出力
					using (var sw = new StreamWriter(gSettings.FormalPathname(gFormalFilename), false, System.Text.Encoding.GetEncoding("shift_jis")))
					{
						// 売上データファイルを空で出力する
						;
					}
				}
				// オン資格訪問診療 売上連絡メール送信（経理課宛）
				SendMailControl.ExportEarningsFileSendMail(homonList, gFormalFilename);

				return homonList;
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
		/// <param name="homonList">オン資訪問診療連携契約情報リスト</param>
		/// <returns>エラーメッセージ</returns>
		private static string UpdateCouplerApply(List<OnlineLicenseHomonEarningsOut> homonList)
		{
			try
			{
				List<V_COUPLER_APPLY> applyList = new List<V_COUPLER_APPLY>();
				if (null != homonList && 0 < homonList.Count)
				{
					// 申込情報から先月分のオン資訪問診療連携費を取得
					foreach (OnlineLicenseHomonEarningsOut homon in homonList)
					{
						if (0 < homon.CouplerApplyID)
						{
							string whereStr = string.Format("apply_id = {0}", homon.CouplerApplyID);   // カプラー申込ID
							List<V_COUPLER_APPLY> list = CharlieDatabaseAccess.Select_V_COUPLER_APPLY(whereStr, "", gSettings.ConnectCharlie.ConnectionString);
							if (null != list && 0 < list.Count)
							{
								applyList.AddRange(list);
							}
						}
						else
						{
							// カプラー申込IDが未格納
							ErrorLogOut.Out(string.Format("カプラー申込IDが未格納。顧客No：{0}、 申込日時：{1}", homon.顧客No, homon.申込日時.ToString()));
						}
					}
					if (0 < applyList.Count)
					{
#if !DebugNoWrite
						// 申込情報の利用申込のシステム反映済フラグをシステム反映に設定
						OnlineLicenseHomonAccess.UpdateSetCouplerApplySystemFlg(applyList, gProcName, gSettings.ConnectCharlie.ConnectionString, gSettings.ConnectCoupler.DatabaseName);
#endif
					}
				}
				// [オン資訪問診療連携費] 利用申込更新 連絡メール送信（社内システム維持管理宛）
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
