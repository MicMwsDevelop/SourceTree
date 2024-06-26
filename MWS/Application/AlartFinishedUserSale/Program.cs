﻿/////////////////////////////////////////////////////////
//
// Program.cs
// 
// 終了ユーザー課金アラート プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// アプリ管理サイト：APPID622 終了ユーザー課金アラート
// 処理概要：PCA売上データから先月終了ユーザーの課金を検出し、営業管理部にアラートメールを送信する
// 入力ファイル：\\sqlsv\pcadata\PCA売上yyyyMMddhhmmss.csv
// 出力ファイル：無
// 印刷物：無
// メール送信：終了ユーザー課金アラート
/////////////////////////////////////////////////////////
// Ver1.00(2021/08/18):新規作成(勝呂)
// Ver1.01(2024/02/06):2023/08組織変更対応 メール「終了ユーザー課金アラート」の宛先・送信元など変更(メールアドレス複数指定対応含む)(越田)
using AlartFinishedUserSale.Mail;
using AlartFinishedUserSale.Settings;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.BaseFactory.Pca;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AlartFinishedUserSale
{
	static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public static readonly string gProgramName = "終了ユーザー課金アラート";

		/// <summary>
		/// バージョン情報
		/// </summary>
		public static readonly string gVersionStr = "Ver1.01(2024/02/06)";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static AlartFinishedUserSaleSettings gSettings;

		/// <summary>
		/// 起動引数
		/// </summary>
		public enum ProgramBootType
		{
			/// <summary>
			/// メイン画面起動
			/// </summary>
			Menu = 0,

			/// <summary>
			///// サイレント実行
			/// </summary>
			Silent = 1,
		}

		/// <summary>
		/// タイトル名
		/// </summary>
		public static string Title
		{
			get
			{
				return string.Format("{0} {1}", gProgramName, gVersionStr);
			}
		}

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			gSettings = AlartFinishedUserSaleSettingsIF.GetSettings();

			// コマンドライン引数を配列で取得する
			ProgramBootType bootType = ProgramBootType.Menu;
			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					bootType = ProgramBootType.Silent;
				}
			}
			switch (bootType)
			{
				// メイン画面起動
				case ProgramBootType.Menu:
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new Forms.MainForm());
					break;
				// サイレント実行
				case ProgramBootType.Silent:
					{
						Date firstDay = Date.Today.FirstDayOfTheMonth(); // 当月初日
						string filename = string.Format("PCA売上データ{0}??????.csv", firstDay.ToIntYMD());
						string[] filelist = Directory.GetFiles(gSettings.FilePath, filename);
						if (0 < filelist.Length)
						{
							string msg;
							Program.Alart(firstDay.PlusMonths(-1), filelist[0], out msg);
						}
					}
					break;
			}
		}

		/// <summary>
		/// アラート
		/// </summary>
		/// <param name="FinishedDate">終了月</param>
		/// <param name="pathname">売上データファイルパス名</param>
		/// <param name="msg">エラーメッセージ</param>
		/// <returns>判定</returns>
		public static int Alart(Date FinishedDate, string pathname, out string msg)
		{
			msg = string.Empty;

			try
			{
				// (1) 先月の終了ユーザーの取得
				List<tMic終了ユーザーリスト> userList = JunpDatabaseAccess.Select_tMic終了ユーザーリスト(string.Format("終了月 = '{0:D4}/{1:D2}'", FinishedDate.Year, FinishedDate.Month), "得意先No ASC", gSettings.Connect.Junp.ConnectionString);
				if (0 == userList.Count)
				{
					msg = "当月終了ユーザーはいませんでした。";
					return 1;
				}
				// (2) 今月１日に課金データ作成が出力した売上データファイルの読込
				List<汎用データレイアウト売上明細データ> saleList = new List<汎用データレイアウト売上明細データ>();
				using (StreamReader sr = new StreamReader(pathname, System.Text.Encoding.GetEncoding("shift_jis")))
				{
					while (!sr.EndOfStream)
					{
						string line = sr.ReadLine();
						string[] csv = line.Split(',');
						汎用データレイアウト売上明細データ data = new 汎用データレイアウト売上明細データ();
						if (data.SetCsvRecord(csv))
						{
							saleList.Add(data);
						}
					}
				}
				if (0 == saleList.Count())
				{
					msg = "売上データがありませんでした。";
					return 1;
				}
				// (3) 先月終了ユーザーの伝票を抽出
				List<FinishedUserSale> resultList = new List<FinishedUserSale>();
				foreach (tMic終了ユーザーリスト user in userList)
				{
					List<汎用データレイアウト売上明細データ> work = saleList.FindAll(p => p.得意先コード == user.得意先No);
					if (0 < work.Count)
					{
						FinishedUserSale result = resultList.Find(p => p.TokuisakiNo == user.得意先No);
						if (null == result)
						{
							result = new FinishedUserSale();
							result.TokuisakiNo = user.得意先No;
							result.SaleList.AddRange(work);
							resultList.Add(result);
						}
						else
						{
							result.SaleList.AddRange(work);
						}
					}
					// 請求先が違う医院の伝票の抽出
					汎用データレイアウト売上明細データ seikyusaki = saleList.Find(p => p.IsDifferentSeikyusaki(user.得意先No));
					if (null != seikyusaki)
					{
						List<汎用データレイアウト売上明細データ> work2 = saleList.FindAll(p => p.伝票No == seikyusaki.伝票No);
						if (0 < work2.Count)
						{
							FinishedUserSale result = resultList.Find(p => p.TokuisakiNo == user.得意先No);
							if (null == result)
							{
								result = new FinishedUserSale();
								result.TokuisakiNo = user.得意先No;
								result.SaleList.AddRange(work2);
								resultList.Add(result);
							}
							else
							{
								result.SaleList.AddRange(work);
							}
						}
					}
				}
				if (0 == resultList.Count)
				{
					msg = "売上データに終了ユーザーの伝票はありませんでした。";
					return 1;
				}
				// 顧客Noと顧客名を格納
				foreach (FinishedUserSale result in resultList)
				{
					List<vMic全ユーザー2> users = JunpDatabaseAccess.Select_vMic全ユーザー2(string.Format("[得意先No] = {0}", result.TokuisakiNo), "", gSettings.Connect.Junp.ConnectionString);
					if (null != users && 0 < users.Count)
					{
						result.CustomerNo = users.First().顧客No;
						result.CustomerName = users.First().顧客名;
					}
					// 記事レコードの削除
					result.SaleList.RemoveAll(p => p.IsArticleRecord);
				}
				// 営業管理部にアラートを送信
				SendMailControl.AlartSendMail(resultList);
			}
			catch (Exception e)
			{
				msg = e.Message;
				return 1;
			}
			return 0;
		}
	}
}
