//
// Program.cs
//
// 終了ユーザー管理プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/12/12 勝呂)
// 
using EntryFinishedUser.Mail;
using MwsLib.BaseFactory.EntryFinishedUser;
using MwsLib.Common;
using MwsLib.DB.SqlServer.EntryFinishedUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EntryFinishedUser
{
	static class Program
	{
		/// <summary>
		/// 起動引数
		/// </summary>
		private enum ProgramBootType
		{
			/// <summary>
			/// メイン画面起動
			/// </summary>
			Menu = 0,

			/// <summary>
			/// 月末処理
			/// タイミング：翌月初日のMWS課金データ作成実行前に行う
			/// ①課金対象外フラグＯＦＦ
			/// ②終了予定ユーザーリストメール送信
			/// </summary>
			EndMonth = 1,

			/// <summary>
			/// 月初処理
			/// タイミング：当月初日のMWS課金データ作成実行後に行う
			/// ①終了ユーザー設定
			/// ②終了ユーザーリストメール送信
			/// </summary>
			BeginMonth = 2,
		}

		/// <summary>
		/// 起動引数
		/// </summary>
		private static ProgramBootType BootType;

		/// <summary>
		/// データベース接続先 CT環境
		/// </summary>
		public static bool DATABACE_ACCEPT_CT = true;

		/// <summary>
		/// リプレース先リスト
		/// </summary>
		public static List<string> gReplaceList;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// コマンドライン引数を配列で取得する
			BootType = ProgramBootType.Menu;
			string[] cmds = Environment.GetCommandLineArgs();
			Date today = Date.Today;
			if (2 <= cmds.Length)
			{
				if ("1" == cmds[1])
				{
					BootType = ProgramBootType.EndMonth;
				}
				else if ("2" == cmds[1])
				{
					BootType = ProgramBootType.BeginMonth;
				}
				if (3 == cmds.Length)
				{
					today = Date.Parse(int.Parse(cmds[2]));
				}
			}
			switch (BootType)
			{
				// メイン画面起動
				case ProgramBootType.Menu:
					// リプレース先リストの取得
					gReplaceList = EntryFinishedUserAccess.GetReplaceMakerList(DATABACE_ACCEPT_CT);

					Application.Run(new Forms.EntryFinishedUserForm());
					break;
				// 月末処理
				case ProgramBootType.EndMonth:
					Program.EndMonth(today);
					break;
				// 月初処理
				case ProgramBootType.BeginMonth:
					Program.BeginMonth(today);
					break;
			}
		}

		/// <summary>
		/// 月末処理
		/// ①課金対象外フラグＯＦＦ
		/// ②終了予定ユーザーリストメール送信
		/// </summary>
		/// <param name="date"></param>
		private static void EndMonth(Date date)
		{
			List<EntryFinishedUserData> work = EntryFinishedUserAccess.GetEntryFinishedUserDataList(DATABACE_ACCEPT_CT);

			YearMonth thisMonth = date.ToYearMonth();
			List<EntryFinishedUserData> paletteFinishedList = work.Where(p => true == p.IsNextMonthFinishedUserByPalette(thisMonth)).ToList();
			if (0 < paletteFinishedList.Count)
			{
				// 翌月終了ユーザー（palette）
				// ①課金対象外フラグＯＦＦ
				List<Tuple<int, int>> list = new List<Tuple<int, int>>();
				foreach (EntryFinishedUserData user in paletteFinishedList)
				{
					List<int> svList = EntryFinishedUserAccess.GetPauseEndStatus(user.CustomerID, DATABACE_ACCEPT_CT);
					foreach (int sv in svList)
					{
						list.Add(new Tuple<int, int>(user.CustomerID, sv));
					}
				}
				if (!DATABACE_ACCEPT_CT)
				{
					EntryFinishedUserSetIO.UpdatePauseEndStatus(list, DATABACE_ACCEPT_CT);
				}
				if (0 < list.Count)
				{
					// ②終了予定ユーザーリストメール送信
					SendMailControl.SendEigyoKanriMail(paletteFinishedList, false);
				}
			}
			List<EntryFinishedUserData> oldSystemFinishedList = work.Where(p => true == p.IsNextMonthFinishedUserByOldSystem(thisMonth)).ToList();
			if (0 < oldSystemFinishedList.Count)
			{
				// 翌月終了ユーザー（旧システム）
				// ①終了ユーザー設定
				if (!DATABACE_ACCEPT_CT)
				{
					EntryFinishedUserSetIO.UpdateClientEndFlag(oldSystemFinishedList, DATABACE_ACCEPT_CT);
				}
				// ②終了ユーザーリストメール送信
				SendMailControl.SendEigyoKanriMail(oldSystemFinishedList, true);
			}
		}

		/// <summary>
		/// 月初処理
		/// ①終了ユーザー設定
		/// ②終了ユーザーリストメール送信
		/// </summary>
		/// <param name="date"></param>
		private static void BeginMonth(Date date)
		{
			List<EntryFinishedUserData> work = EntryFinishedUserAccess.GetEntryFinishedUserDataList(DATABACE_ACCEPT_CT);

			YearMonth thisMonth = date.ToYearMonth();
			List<EntryFinishedUserData> finishedList = work.Where(p => true == p.IsPrevMonthFinishedUserByPalette(thisMonth)).ToList();
			if (0 < finishedList.Count)
			{
				// 前月終了ユーザー
				if (!DATABACE_ACCEPT_CT)
				{
					EntryFinishedUserSetIO.UpdateClientEndFlag(finishedList, DATABACE_ACCEPT_CT);
				}
				// ②終了ユーザーリストメール送信
				SendMailControl.SendEigyoKanriMail(finishedList, true);
			}
		}
	}
}
