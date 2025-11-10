//
// Program.cs
//
// ハードサブスク通知プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/10/28 勝呂)
// 
using CommonLib.BaseFactory.HardSubsc;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.Common;
using CommonLib.DB.SqlServer.HardSubsc;
using CommonLib.DB.SqlServer.Junp;
using HardSubscNotifyMail.Mail;
using HardSubscNotifyMail.Settings;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HardSubscNotifyMail
{
	internal static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string ProgramName = "ハードサブスク通知";

		/// <summary>
		/// プログラムバージョン
		/// </summary>
		public const string ProgramVersion = "Ver1.00 2025/10/28";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static HardSubscNotifyMailSettings gSettings { get; set; }

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static int Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// 環境設定の読込
			gSettings = HardSubscNotifyMailSettingsIF.GetSettings();

			string[] cmds = Environment.GetCommandLineArgs();
			if (3 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					// サイレントモード
					if ("1" == cmds[2].ToUpper())
					{
						// 利用期限通知
						string msg = Program.NotifyLimitMail();
						if (0 < msg.Length)
						{
							return 1;
						}
					}
					else if ("2" == cmds[2].ToUpper())
					{
						// 利用終了通知
						string msg = Program.NotifyFinishedMail();
						if (0 < msg.Length)
						{
							return 1;
						}
					}
					return 0;
				}
			}
			Application.Run(new Forms.MainForm());
			return 0;
		}

		/// <summary>
		/// 利用期限通知
		/// </summary>
		/// <returns>エラーメッセージ</returns>
		public static string NotifyLimitMail()
		{
			try
			{
#if DEBUG
				Date endDate = HardSubscNotify.GetLimitDate(new Date(2027, 12, 1));
#else
				Date endDate = HardSubscNotify.GetLimitDate(Date.Today);		// 当日から半年後の末日
#endif
				// ハードサブスク契約情報から通知医院の取得
				List<HardSubscNotify> notifyList = HardSubscAccess.GetHardSubscNotify(endDate, gSettings.ConnectCharlie.ConnectionString);

				// 業務課にメール送信
				SendMailControl.SendLimitMailToGyomu(endDate, notifyList);

				// 各オフィスにメール送信
				List<tMih支店情報> branchList = JunpDatabaseAccess.Select_tMih支店情報("[fBshCode2] >= '50' AND [fBshCode2] < '98'", "[fBshCode2], [fBshCode3] ASC", gSettings.ConnectJunp.ConnectionString);
				foreach (tMih支店情報 branch in branchList)
				{
					List<HardSubscNotify> userList = notifyList.FindAll(p => p.支店コード == branch.fBshCode3);
					SendMailControl.SendLimitMailToOffice(endDate, userList, branch);
				}
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return string.Empty;
		}

		/// <summary>
		/// 利用終了通知
		/// </summary>
		/// <returns>エラーメッセージ</returns>
		public static string NotifyFinishedMail()
		{
			try
			{
#if DEBUG
				Date endDate = new Date(2028, 5, 1).LastDayOfTheMonth();
#else
				Date endDate = Date.Today.LastDayOfTheMonth();		// 当月末日
#endif
				// ハードサブスク契約情報から通知医院の取得
				List<HardSubscNotify> notifyList = HardSubscAccess.GetHardSubscNotify(endDate, gSettings.ConnectCharlie.ConnectionString);

				// 業務課にメール送信
				SendMailControl.SendFinishedMailToGyomu(notifyList);

				// 各オフィスにメール送信
				List<tMih支店情報> branchList = JunpDatabaseAccess.Select_tMih支店情報("[fBshCode2] >= '50' AND [fBshCode2] < '98'", "[fBshCode2], [fBshCode3] ASC", gSettings.ConnectJunp.ConnectionString);
				foreach (tMih支店情報 branch in branchList)
				{
					List<HardSubscNotify> userList = notifyList.FindAll(p => p.支店コード == branch.fBshCode3);
					SendMailControl.SendFinishedMailToOffice(userList, branch);
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
