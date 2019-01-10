//
// Program.cs
//
// PC安心サポート管理
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/11/19 勝呂)
// Ver1.020 ソフト保守加入の条件を変更(2019/01/07 勝呂)
// 
using MwsLib.BaseFactory.PcSupportManager;
using MwsLib.Common;
using MwsLib.DB.SqlServer.PcSupportManager;
using MwsLib.Log;
using MwsLib.Settings;
using PcSupportManager.Mail;
using PcSupportManager.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PcSupportManager
{
	static class Program
	{
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
			/// 製品サポート情報ソフト保守自動更新
			/// </summary>
			SoftMainte = 1,

			/// <summary>
			/// PC安心サポート開始メール自動送信
			/// </summary>
			SendStartMail = 2,

			/// <summary>
			/// PC安心サポート契約更新案内/契約更新メール自動送信
			/// </summary>
			SendUpdateMail = 3,
		};

		/// <summary>
		/// プログラム名
		/// </summary>
		public static readonly string PROGRAM_NAME = "PC安心サポート管理";

		/// <summary>
		/// ログファイル名
		/// </summary>
		public static readonly string LOG_FILENAME = "PCSupportManager-{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}.log";

		/// <summary>
		/// 起動引数
		/// </summary>
		public static ProgramBootType BootType;

		/// <summary>
		/// システム日付の取得
		/// </summary>
		public static Date SystemDate;

		/// <summary>
		/// デバッグモード
		/// </summary>
		public static bool DebugMode = false;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			SystemDate = Date.Today;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// コマンドライン引数を配列で取得する
			BootType = ProgramBootType.Menu;
			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("1" == cmds[1])
				{
					BootType = ProgramBootType.SoftMainte;
				}
				else if ("2" == cmds[1])
				{
					BootType = ProgramBootType.SendStartMail;
				}
				else if ("3" == cmds[1])
				{
					BootType = ProgramBootType.SendUpdateMail;
				}
				if (3 == cmds.Length)
				{
					SystemDate = Date.Parse(int.Parse(cmds[2]));
				}
			}

			//// 休日テスト
			//PcSupportManagerSettings xml = PcSupportManagerSettingsIF.GetPcSupportManagerSettings();
			//CompanyHoliday.SetHoliday(xml.WeeklyHoliday, xml.NationalHoliday, xml.HappyMonday, xml.SpecialHoliday);
			//Span span = new Span(new Date(2019, 1, 1), new Date(2019, 12, 31));
			//Date date = span.Start;
			//for (int i = 0; i < 365; i++)
			//{
			//	if (!span.IsInside(date))
			//	{
			//		break;
			//	}
			//	if (CompanyHoliday.IsHoliday(date))
			//	{
			//		Console.WriteLine(string.Format(@"{0}:×", date.ToString()));
			//	}
			//	else
			//	{
			//		Console.WriteLine(string.Format(@"{0}:○", date.ToString()));
			//	}
			//	date++;
			//}

			switch (BootType)
			{
				// メイン画面起動
				case ProgramBootType.Menu:
					Application.Run(new Forms.MainForm());
					break;
				// 製品サポート情報ソフト保守自動更新
				case ProgramBootType.SoftMainte:
					Program.SoftMainte(SystemDate);
					break;
				// PC安心サポート開始メール自動送信
				case ProgramBootType.SendStartMail:
					{
						PcSupportManagerSettings xml = PcSupportManagerSettingsIF.GetPcSupportManagerSettings();
						if (xml.IsStartMailExec(SystemDate))
						{
							CompanyHoliday.SetHoliday(MicHolidaySettingsIF.GetMicHolidaySettings());
							if (false == CompanyHoliday.IsHoliday(SystemDate))
							{
								Program.SendStartMail(SystemDate);
								if (!DebugMode)
								{
									xml.StartMailPrevExecDate = SystemDate.ToDateTime();
									PcSupportManagerSettingsIF.SetPcSupportManagerSettings(xml);
								}
							}
						}
					}
					break;
				// PC安心サポート契約更新案内/契約更新メール自動送信
				case ProgramBootType.SendUpdateMail:
					{
						PcSupportManagerSettings xml = PcSupportManagerSettingsIF.GetPcSupportManagerSettings();
						if (xml.IsUpdateMailExec(SystemDate))
						{
							CompanyHoliday.SetHoliday(MicHolidaySettingsIF.GetMicHolidaySettings());
							if (false == CompanyHoliday.IsHoliday(SystemDate))
							{
								Program.SendUpdateMail(SystemDate);
								if (!DebugMode)
								{
									xml.UpdatteMailPrevExecDate = SystemDate.ToDateTime();
									PcSupportManagerSettingsIF.SetPcSupportManagerSettings(xml);
								}
							}
						}
					}
					break;
			}
		}

		/// <summary>
		/// ログファイルのパス名を取得
		/// </summary>
		/// <returns>ログファイルパス名</returns>
		private static string GetLogPathname()
		{
			return Path.Combine(Directory.GetCurrentDirectory(), string.Format(LOG_FILENAME, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute));
		}

		/// <summary>
		/// 製品サポート情報ソフト保守自動更新
		/// </summary>
		/// <param name="date">当日</param>
		private static void SoftMainte(Date date)
		{
			string logPathname = Program.GetLogPathname();
			Logger.Out(logPathname, string.Format("{0} {1}:製品サポート情報ソフト保守情報更新 開始", Program.PROGRAM_NAME, DateTime.Now.ToString()));

			/////////////////////////////////////////////
			// 受注情報の読込み
			/////////////////////////////////////////////

			Program.ReadOrderInfo(logPathname);


			/////////////////////////////////////////////
			// 製品サポート情報ソフト保守更新
			/////////////////////////////////////////////

			// PC安心サポート管理情報リストの取得
			List<PcSupportControl> pcList = PcSupportManagerAccess.GetPcSupportControl();

			// ソフト保守メンテナンス情報リストの取得
			List<SoftMaintenanceContract> softList = PcSupportManagerAccess.GetSoftMaintenanceContract();
			foreach (PcSupportControl pc in pcList)
			{
				if (false == pc.DisableFlag)
				{
					SoftMaintenanceContract soft = softList.Find(p => p.CustomerNo == pc.CustomerNo);
					if (null != soft)
					{
						if (pc.IsOrderInfoCompleted(false))
						{
							// Ver1.020 ソフト保守加入の条件を変更(2019/01/07 勝呂)
							if (soft.SetPcSupportControl(pc, date))
							{
								if (!DebugMode)
								{
									try
									{
										PcSupportManagerAccess.SetSoftMaintenanceContract(soft);
									}
									catch (Exception ex)
									{
										Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.SetSoftMaintenanceContract ({0})", ex.Message));
										Logger.Out(logPathname, string.Format("{0} {1}:製品サポート情報ソフト保守情報更新 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
										return;
									}
								}
								Logger.Out(logPathname, soft.ToLog(pc));
							}
						}
					}
					else
					{
						if (pc.IsOrderInfoCompleted(false))
						{
							// Ver1.020 ソフト保守加入の条件を変更(2019/01/07 勝呂)
							soft = new SoftMaintenanceContract(pc, date);
							if (!DebugMode)
							{
								try
								{
									PcSupportManagerAccess.SetSoftMaintenanceContract(soft);
								}
								catch (Exception ex)
								{
									Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.SetSoftMaintenanceContract ({0})", ex.Message));
									Logger.Out(logPathname, string.Format("{0} {1}:製品サポート情報ソフト保守情報更新 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
									return;
								}
							}
							Logger.Out(logPathname, soft.ToLog(pc));
						}
					}
				}
			}
			Logger.Out(logPathname, string.Format("{0} {1}:製品サポート情報ソフト保守情報更新 正常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
		}

		/// <summary>
		/// PC安心サポート開始メール自動送信
		/// </summary>
		/// <param name="date">当日</param>
		private static void SendStartMail(Date date)
		{
			string logPathname = Program.GetLogPathname();
			Logger.Out(logPathname, string.Format("{0} {1}:PC安心サポート開始メール送信 開始", Program.PROGRAM_NAME, DateTime.Now.ToString()));

			/////////////////////////////////////////////
			// 受注情報の読込み
			/////////////////////////////////////////////

			Program.ReadOrderInfo(logPathname);


			/////////////////////////////////////////////
			// 開始メール送信処理
			/////////////////////////////////////////////

			// PC安心サポート管理情報リストの取得
			List<PcSupportControl> pcList = PcSupportManagerAccess.GetPcSupportControl();
			List<PcSupportControl> startPcList = pcList.Where(p => true == p.IsSendStartMail(date)).ToList();
			if (0 < startPcList.Count)
			{
				List<PcSupportMail> startMailList = new List<PcSupportMail>();
				foreach (PcSupportControl pc in startPcList)
				{
					// 開始メール送信情報の格納
					pc.StartMailDateTime = date.ToDateTime();
					pc.UpdateDateTime = date.ToDateTime();
					pc.UpdatePerson = Program.PROGRAM_NAME;
					PcSupportMail mail = new PcSupportMail(PcSupportMail.MailType.Start, pc, date);
					startMailList.Add(mail);
					try
					{
						// 開始メール送信
						SendMailControl.SendStartMail(mail, pc.ClinicName);
					}
					catch (Exception ex)
					{
						Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendStartMail ({0})", ex.Message));
						Logger.Out(logPathname, string.Format("{0} {1}:PC安心サポート開始メール送信 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
						return;
					}
					Logger.Out(logPathname, mail.ToLog(pc));
					if (!DebugMode)
					{
						try
						{
							// PC安心サポート管理情報の更新
							PcSupportManagerAccess.SetPcSupportControl(pc);
						}
						catch (Exception ex)
						{
							Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.SetPcSupportControl ({0})", ex.Message));
							Logger.Out(logPathname, string.Format("{0} {1}:PC安心サポート開始メール送信 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
							return;
						}
					}
				}
				try
				{
					// 営業管理部にメール
					List<PcSupportControl> errPcList = pcList.Where(p => true == p.IsPastApprovalDate(date)).ToList();
					SendMailControl.SendEigyoKanriMail(PcSupportMail.MailType.Start, startMailList, startPcList, errPcList);
				}
				catch (Exception ex)
				{
					// プログラムは止めない
					Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendEigyoKanriMail ({0})", ex.Message));
				}
				try
				{
					// 各拠点にメール
					List<BranchInfo> branchInfoList = PcSupportManagerAccess.GetBranchInfo();
					foreach (BranchInfo branch in branchInfoList)
					{
						List<PcSupportMail> branchMailList = startMailList.Where(p => p.BranchID == branch.BranchID).ToList();
						if (0 < branchMailList.Count)
						{
							List<PcSupportControl> branchPcList = startPcList.Where(p => p.BranchID == branch.BranchID).ToList();
							SendMailControl.SendBranchMail(PcSupportMail.MailType.Start, branch, branchMailList, branchPcList);
						}
					}
				}
				catch (Exception ex)
				{
					// プログラムは止めない
					Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendBranchMail ({0})", ex.Message));
				}
				if (0 < startMailList.Count)
				{
					if (!DebugMode)
					{
						try
						{
							// PC安心サポート送信メール情報の登録
							PcSupportManagerAccess.InsertIntoPcSupportMailList(startMailList);
						}
						catch (Exception e)
						{
							Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.InsertIntoPcSupportMailList ({0})", e.Message));
							Logger.Out(logPathname, string.Format("{0} {1}:PC安心サポート開始メール送信 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
							return;
						}
					}
				}
			}
			//else
			//{
			//	// 営業管理部にメール
			//	try
			//	{
			//		SendMailControl.SendEigyoKanriMail(PcSupportMail.MailType.Start, new List<PcSupportMail>(), startPcList);
			//	}
			//	catch (Exception ex)
			//	{
			//		// プログラムは止めない
			//		Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendEigyoKanriMail ({0})", ex.Message));
			//	}
			//	try
			//	{
			//		// 各拠点にメール
			//		List<BranchInfo> branchInfoList = PcSupportManagerAccess.GetBranchInfo();
			//		foreach (BranchInfo branch in branchInfoList)
			//		{
			//			SendMailControl.SendBranchMail(PcSupportMail.MailType.Start, branch, new List<PcSupportMail>(), null);
			//		}
			//	}
			//	catch (Exception ex)
			//	{
			//		// プログラムは止めない
			//		Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendBranchMail ({0})", ex.Message));
			//	}
			//}
			Logger.Out(logPathname, string.Format("{0} {1}:PC安心サポート開始メール送信 正常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
		}

		/// <summary>
		/// PC安心サポート契約更新案内/契約更新メール自動送信
		/// </summary>
		/// <param name="date">当日</param>
		private static void SendUpdateMail(Date date)
		{
			string logPathname = Program.GetLogPathname();
			Logger.Out(logPathname, string.Format("{0} {1}:PC安心サポート契約更新案内/契約更新メール送信 開始", Program.PROGRAM_NAME, DateTime.Now.ToString()));

			/////////////////////////////////////////////
			// 受注情報の読込み
			/////////////////////////////////////////////

			Program.ReadOrderInfo(logPathname);


			/////////////////////////////////////////////
			// 契約更新案内メール送信処理
			/////////////////////////////////////////////

			Logger.Out(logPathname, string.Format("{0}:契約更新案内メール送信 開始", DateTime.Now.ToString()));

			List<PcSupportControl> pcList = PcSupportManagerAccess.GetPcSupportControl();
			List<PcSupportControl> guidePcList = pcList.Where(p => true == p.IsSendGuideMail(date)).ToList();
			if (0 < guidePcList.Count)
			{
				List<PcSupportMail> guideMailList = new List<PcSupportMail>();
				foreach (PcSupportControl pc in guidePcList)
				{
					// 契約更新案内メール送信情報の格納
					pc.GuideMailDateTime = date.ToDateTime();
					pc.UpdateDateTime = date.ToDateTime();
					pc.UpdatePerson = Program.PROGRAM_NAME;

					PcSupportMail mail = new PcSupportMail(PcSupportMail.MailType.Guide, pc, date);
					guideMailList.Add(mail);
					try
					{
						// 契約更新案内メール送信
						SendMailControl.SendGuideMail(mail, pc.ClinicName);
					}
					catch (Exception ex)
					{
						Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendGuideMail ({0})", ex.Message));
						Logger.Out(logPathname, string.Format("{0} {1}:PC安心サポート契約更新案内/契約更新メール送信 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
						return;
					}
					Logger.Out(logPathname, mail.ToLog(pc));

					// 契約更新メール送信情報をクリア
					pc.UpdateMailDateTime = null;
					if (!DebugMode)
					{
						try
						{
							// PC安心サポート管理情報の更新
							PcSupportManagerAccess.SetPcSupportControl(pc);
						}
						catch (Exception ex)
						{
							Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.SetPcSupportControl ({0})", ex.Message));
							Logger.Out(logPathname, string.Format("{0} {1}:PC安心サポート契約更新案内/契約更新メール送信 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
							return;
						}
					}
				}
				try
				{
					// 営業管理部にメール
					SendMailControl.SendEigyoKanriMail(PcSupportMail.MailType.Guide, guideMailList, guidePcList);
				}
				catch (Exception ex)
				{
					// プログラムは止めない
					Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendEigyoKanriMail ({0})", ex.Message));
				}
				try
				{
					// 各拠点にメール
					List<BranchInfo> branchInfoList = PcSupportManagerAccess.GetBranchInfo();
					foreach (BranchInfo branch in branchInfoList)
					{
						List<PcSupportMail> branchMailList = guideMailList.Where(p => p.BranchID == branch.BranchID).ToList();
						if (0 < branchMailList.Count)
						{
							List<PcSupportControl> branchPcList = guidePcList.Where(p => p.BranchID == branch.BranchID).ToList();
							SendMailControl.SendBranchMail(PcSupportMail.MailType.Update, branch, branchMailList, branchPcList);
						}
					}
				}
				catch (Exception ex)
				{
					// プログラムは止めない
					Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendBranchMail ({0})", ex.Message));
				}
				if (0 < guideMailList.Count)
				{
					if (!DebugMode)
					{
						try
						{
							// PC安心サポート送信メール情報の登録
							PcSupportManagerAccess.InsertIntoPcSupportMailList(guideMailList);
						}
						catch (Exception ex)
						{
							Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.InsertIntoPcSupportMailList ({0})", ex.Message));
							Logger.Out(logPathname, string.Format("{0} {1}:PC安心サポート契約更新案内/契約更新メール送信 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
							return;
						}
					}
				}
			}
			//else
			//{
			//	// 営業管理部にメール
			//	try
			//	{
			//		SendMailControl.SendEigyoKanriMail(PcSupportMail.MailType.Guide, new List<PcSupportMail>(), guidePcList);
			//	}
			//	catch (Exception ex)
			//	{
			//		// プログラムは止めない
			//		Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendEigyoKanriMail ({0})", ex.Message));
			//	}
			//	try
			//	{
			//		// 各拠点にメール
			//		List<BranchInfo> branchInfoList = PcSupportManagerAccess.GetBranchInfo();
			//		foreach (BranchInfo branch in branchInfoList)
			//		{
			//			SendMailControl.SendBranchMail(PcSupportMail.MailType.Guide, branch, new List<PcSupportMail>(), null);
			//		}
			//	}
			//	catch (Exception ex)
			//	{
			//		// プログラムは止めない
			//		Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendBranchMail ({0})", ex.Message));
			//	}
			//}
			Logger.Out(logPathname, string.Format("{0}:契約更新案内メール送信 正常終了", DateTime.Now.ToString()));


			/////////////////////////////////////////////
			// 契約更新メール送信処理
			/////////////////////////////////////////////

			Logger.Out(logPathname, string.Format("{0}:契約更新メール送信 開始", DateTime.Now.ToString()));

			List<PcSupportControl> updatePcList = pcList.Where(p => true == p.IsSendUpdateMail(date)).ToList();
			if (0 < updatePcList.Count)
			{
				List<PcSupportGoodsInfo> goodsList = PcSupportManagerAccess.GetPcSupportGoodsInfo();
				string goodsName = "PC安心ｻﾎﾟｰﾄ(1年契約)";
				PcSupportGoodsInfo goods = goodsList.Find(p => p.GoodsID == PcSupportGoodsInfo.PC_SUPPORT1_GOODS_ID);
				if (null != goods)
				{
					goodsName = goods.GoodsName;
				}
				List<PcSupportMail> updateMailList = new List<PcSupportMail>();
				foreach (PcSupportControl pc in updatePcList)
				{
					// 契約更新メール送信情報の格納
					pc.UpdateMailDateTime = date.ToDateTime();
					pc.UpdateDateTime = date.ToDateTime();
					pc.UpdatePerson = Program.PROGRAM_NAME;

					PcSupportMail mail = new PcSupportMail(PcSupportMail.MailType.Update, pc, date);
					updateMailList.Add(mail);
					try
					{
						// 契約更新メール送信
						SendMailControl.SendUpdateMail(mail, pc.ClinicName);
					}
					catch (Exception ex)
					{
						Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendUpdateMail ({0})", ex.Message));
						Logger.Out(logPathname, string.Format("{0} {1}:PC安心サポート契約更新案内/契約更新メール送信 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
						return;
					}
					Logger.Out(logPathname, mail.ToLog(pc));

					// 契約更新案内メール送信情報の格納
					pc.GuideMailDateTime = null;
					pc.GoodsID = PcSupportGoodsInfo.PC_SUPPORT1_GOODS_ID;
					pc.GoodsName = goodsName;
					pc.AgreeYear = 1;
					pc.StartDate = pc.EndDate.Value.PlusMonths(1).ToYearMonth().First;
					pc.EndDate = PcSupportControl.GetEndDate(pc.StartDate.Value, pc.AgreeYear);
					if (!DebugMode)
					{
						try
						{
							// PC安心サポート管理情報の更新
							PcSupportManagerAccess.SetPcSupportControl(pc);
						}
						catch (Exception ex)
						{
							Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.SetPcSupportControl ({0})", ex.Message));
							Logger.Out(logPathname, string.Format("{0} {1}:PC安心サポート契約更新案内/契約更新メール送信 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
							return;
						}
					}
				}
				try
				{
					// 営業管理部にメール
					SendMailControl.SendEigyoKanriMail(PcSupportMail.MailType.Update, updateMailList, updatePcList);
				}
				catch (Exception ex)
				{
					// プログラムは止めない
					Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendEigyoKanriMail ({0})", ex.Message));
				}
				try
				{
					// 各拠点にメール
					List<BranchInfo> branchInfoList = PcSupportManagerAccess.GetBranchInfo();
					foreach (BranchInfo branch in branchInfoList)
					{
						List<PcSupportMail> branchMailList = updateMailList.Where(p => p.BranchID == branch.BranchID).ToList();
						if (0 < branchMailList.Count)
						{
							List<PcSupportControl> branchPcList = updatePcList.Where(p => p.BranchID == branch.BranchID).ToList();
							SendMailControl.SendBranchMail(PcSupportMail.MailType.Update, branch, branchMailList, branchPcList);
						}
					}
				}
				catch (Exception ex)
				{
					// プログラムは止めない
					Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendBranchMail ({0})", ex.Message));
				}
				if (0 < updateMailList.Count)
				{
					if (!DebugMode)
					{
						try
						{
							// PC安心サポート送信メール情報の登録
							PcSupportManagerAccess.InsertIntoPcSupportMailList(updateMailList);
						}
						catch (Exception ex)
						{
							Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.InsertIntoPcSupportMailList ({0})", ex.Message));
							Logger.Out(logPathname, string.Format("{0} {1}:PC安心サポート契約更新案内/契約更新メール送信 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
							return;
						}
					}
				}
			}
			//else
			//{
			//	// 営業管理部にメール
			//	try
			//	{
			//		SendMailControl.SendEigyoKanriMail(PcSupportMail.MailType.Guide, new List<PcSupportMail>(), guidePcList);
			//	}
			//	catch (Exception ex)
			//	{
			//		// プログラムは止めない
			//		Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendEigyoKanriMail ({0})", ex.Message));
			//	}
			//	try
			//	{
			//		// 各拠点にメール
			//		List<BranchInfo> branchInfoList = PcSupportManagerAccess.GetBranchInfo();
			//		foreach (BranchInfo branch in branchInfoList)
			//		{
			//			SendMailControl.SendBranchMail(PcSupportMail.MailType.Update, branch, new List<PcSupportMail>(), null);
			//		}
			//	}
			//	catch (Exception ex)
			//	{
			//		// プログラムは止めない
			//		Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendBranchMail ({0})", ex.Message));
			//	}
			//}
			Logger.Out(logPathname, string.Format("{0}:契約更新メール送信 正常終了", DateTime.Now.ToString()));

			Logger.Out(logPathname, string.Format("{0} {1}:PC安心サポート契約更新案内/契約更新メール送信 正常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
		}

		/// <summary>
		/// 受注情報の読込み
		/// </summary>
		/// <param name="logPathname">ログファイルパス名</param>
		private static bool ReadOrderInfo(string logPathname)
		{
			try
			{
				List<Tuple<int, string>> mailAddressList = PcSupportManagerAccess.GetCustomerMailAddress();
				List<OrderInfo> orderInfoList = PcSupportManagerAccess.GetOrderInfoList();
				List<PcSupportControl> pcList = PcSupportManagerAccess.GetPcSupportControl();

				List<PcSupportControl> updatePcList = new List<PcSupportControl>();
				foreach (OrderInfo order in orderInfoList)
				{
					string mailAddress = string.Empty;
					Tuple<int, string> mail = mailAddressList.Find(p => p.Item1 == order.CustomerNo);
					if (null != mail)
					{
						mailAddress = mail.Item2;
					}
					PcSupportControl control = pcList.Find(p => p.OrderNo == order.OrderNo);
					if (null != control)
					{
						if (control.IsUpdateOrderData(order, mailAddress))
						{
							control.SetOrderInfo(order, mailAddress, Program.SystemDate);
							updatePcList.Add(control);
						}
					}
					else
					{
						control = new PcSupportControl(order, mailAddress, Program.SystemDate);
						updatePcList.Add(control);
					}
				}
				PcSupportManagerAccess.SetPcSupportControlList(updatePcList);
			}
			catch (Exception ex)
			{
				Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.SetPcSupportControlList ({0})", ex.Message));
				return false;
			}
			return true;
		}
	}
}
