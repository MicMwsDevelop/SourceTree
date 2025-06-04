//
// Program.cs
//
// ハードサブスク管理プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/04/03 勝呂)
// 
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.HardSubscript;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.Common;
using CommonLib.DB.SqlServer.HardSubscript;
using HardSubscriptManager.Settings;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HardSubscriptManager.Mail;
using CommonLib.DB.SqlServer.Junp;

namespace HardSubscriptManager
{
	internal static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string ProgramName = "ハードサブスク管理";

		/// <summary>
		/// プログラムバージョン
		/// </summary>
		public const string ProgramVersion = "Ver1.00 2025/05/20";

		/// <summary>
		/// [JunpDB].[dbo].[tUser]
		/// </summary>
		public static tUser UserInfo { get; set; }

		/// <summary>
		/// 環境設定
		/// </summary>
		public static HardSubscriptManagerSettings gSettings { get; set; }

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static int Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			UserInfo = null;

			// 環境設定の読込
			gSettings = HardSubscriptManagerSettingsIF.GetSettings();

			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					// サイレントモード
					string msg = NotifyMail();
					if (0 < msg.Length)
					{
						return 1;
					}
					return 0;
				}
			}
			Application.Run(new ManagerForm());
			return 0;
		}

		/// <summary>
		/// ハードサブスク利用期限通知メール送信
		/// </summary>
		/// <returns>エラーメッセージ</returns>
		public static string NotifyMail()
		{
			try
			{
#if DEBUG
				Date useEndDate = new Date(2025, 9, 30).PlusMonths(6);
#else
				Date useEndDate = Date.Today.PlusMonths(6).LastDayOfTheMonth();		// 当日から半年後の末日
#endif
				// ハードサブスク契約情報から利用期限通知医院の取得
				List<HardSubscriptNotify> notifyList = HardSubscriptAccess.GetHardSubscriptNotify(useEndDate, gSettings.ConnectCharlie.ConnectionString);

				// 業務課にメール送信
				SendMailControl.SendMailToGyomu(useEndDate, notifyList);

				// 各オフィスにメール送信
				List<tMih支店情報> branchList = JunpDatabaseAccess.Select_tMih支店情報("[fBshCode2] >= '50' AND [fBshCode2] < '98'", "[fBshCode2], [fBshCode3] ASC", gSettings.ConnectJunp.ConnectionString);
				foreach (tMih支店情報 branch in branchList)
				{
					List<HardSubscriptNotify> userList = notifyList.FindAll(p => p.支店コード == branch.fBshCode3);
					SendMailControl.SendMailToOffice(useEndDate, userList, branch);
				}
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			return string.Empty;
		}

		/// <summary>
		/// 使用者の取得
		/// </summary>
		/// <returns>使用者</returns>
		public static string GetPerson()
		{
			if (null != UserInfo)
			{
				return UserInfo.fUsrName;
			}
			return ProgramVersion;
		}

		/// <summary>
		/// 契約情報ListView設定値の取得
		/// </summary>
		/// <returns>ListView設定値</returns>
		public static string[] GetHeaderListViewItem(T_HARD_SUBSCRIPT_HEADER header)
		{
			string[] item = new string[11];
			item[0] = header.RentalNo;
			item[1] = (header.ContractDate.HasValue) ? header.ContractDate.Value.ToShortDateString() : "";
			item[2] = header.MonthlyAmount.ToString();
			item[3] = header.Months.ToString();
			item[4] = (header.UseStartDate.HasValue) ? header.UseStartDate.Value.ToShortDateString() : "";
			item[5] = (header.UseEndDate.HasValue) ? header.UseEndDate.Value.ToShortDateString() : "";
			item[6] = (header.BillingStartDate.HasValue) ? header.BillingStartDate.Value.ToShortDateString() : "";
			item[7] = (header.BillingEndDate.HasValue) ? header.BillingEndDate.Value.ToShortDateString() : "";
			item[8] = (header.CancelDate.HasValue) ? header.CancelDate.Value.ToShortDateString() : "";
			item[9] = (header.CancelApplyDate.HasValue) ? header.CancelApplyDate.Value.ToShortDateString() : "";
			item[10] = (header.ServiceEndFlag) ? "終了" : "";
			return item;
		}

		/// <summary>
		/// 機器情報ListView設定値の取得
		/// </summary>
		/// <param name="line">行番号</param>
		/// <param name="detail">機器情報</param>
		/// <returns>ListView設定値</returns>
		public static string[] GetDetailListViewItem(int line, T_HARD_SUBSCRIPT_DETAIL detail)
		{
			string[] item = new string[7];
			item[0] = line.ToString();
			item[1] = detail.GoodsCode;
			item[2] = detail.GoodsName;
			item[3] = detail.GetCategoryName();
			item[4] = detail.Quantity.ToString();
			item[5] = detail.AssetsCode;
			item[6] = detail.SerialNo;
			return item;
		}
	}
}
