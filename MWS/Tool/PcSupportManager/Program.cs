using System;
using System.Windows.Forms;
using MwsLib.Common;
using MwsLib.BaseFactory.PcSupportManager;
using MwsLib.DB.SqlServer.PcSupportManager;
using System.Collections.Generic;
using System.Linq;
using PcSupportManager.Mail;
using System.IO;
using MwsLib.Log;

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
			/// 開始メール送信
			/// </summary>
			SendStartMail = 1,

			/// <summary>
			/// 月次処理
			/// ソフト保守メンテナンス情報更新
			/// 契約更新メール送信
			/// 契約更新案内メール送信
			/// </summary>
			Monthly = 2,
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
		public static bool DebugMode = true;

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
			Date date = Date.Today;
			if (2 <= cmds.Length)
			{
				if ("1" == cmds[1])
				{
					BootType = ProgramBootType.SendStartMail;
				}
				else if ("2" == cmds[1])
				{
					BootType = ProgramBootType.Monthly;
				}
				if (3 == cmds.Length)
				{
					date = Date.Parse(int.Parse(cmds[2]));
				}
			}
			switch (BootType)
			{
				// メイン画面起動
				case ProgramBootType.Menu:
					Application.Run(new Forms.MainForm());
					break;
				// 開始メール送信
				case ProgramBootType.SendStartMail:
					Program.SendStartMail(date);
					break;
				// 月次処理
				case ProgramBootType.Monthly:
					Program.Monthly(date);
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
		/// 開始メール送信
		/// </summary>
		/// <param name="date">当日</param>
		private static void SendStartMail(Date date)
		{
			string logPathname = Program.GetLogPathname();
			Logger.Out(logPathname, string.Format("{0} {1}:開始メール送信 開始", Program.PROGRAM_NAME, DateTime.Now.ToString()));

			// PC安心サポート管理情報リストの取得
			List<PcSupportControl> srcList = PcSupportManagerAccess.GetPcSupportControl();
			List<PcSupportControl> pcList = srcList.Where(p => true == p.IsSendStartMail(date)).ToList();
			if (0 < pcList.Count)
			{
				List<PcSupportMail> mailList = new List<PcSupportMail>();
				foreach (PcSupportControl pc in pcList)
				{
					// メール送信処理
					pc.StartMailDateTime = date.ToDateTime();
					pc.UpdateDateTime = date.ToDateTime();
					pc.UpdatePerson = Program.PROGRAM_NAME;
					PcSupportMail mail = new PcSupportMail(PcSupportMail.MailType.Start, pc, date);
					mailList.Add(mail);
					try
					{
						//SendMailControl.SendStartMail(mail, pc.ClinicName);
					}
					catch (Exception ex)
					{
						Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendStartMail ({0})", ex.Message));
						Logger.Out(logPathname, string.Format("{0} {1}:開始メール送信 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
						return;
					}
					Logger.Out(logPathname, mail.ToLog(pc));
					if (!DebugMode)
					{
						try
						{
							PcSupportManagerAccess.SetPcSupportControl(pc);
						}
						catch (Exception ex)
						{
							Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.SetPcSupportControl ({0})", ex.Message));
							Logger.Out(logPathname, string.Format("{0} {1}:開始メール送信 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
							return;
						}
					}
				}
				try
				{
					// 営業管理部にメール
					SendMailControl.SendEigyoKanriMail(PcSupportMail.MailType.Start, mailList, pcList);
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
						List<PcSupportMail> branchMailList = mailList.Where(p => p.BranchID == branch.BranchID).ToList();
						List<PcSupportControl> branchPcList = pcList.Where(p => p.BranchID == branch.BranchID).ToList();
						SendMailControl.SendBranchMail(PcSupportMail.MailType.Start, branch, branchMailList, branchPcList);
					}
				}
				catch (Exception ex)
				{
					// プログラムは止めない
					Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendBranchMail ({0})", ex.Message));
				}
				if (0 < mailList.Count)
				{
					if (!DebugMode)
					{
						try
						{
							PcSupportManagerAccess.InsertIntoPcSupportMailList(mailList);
						}
						catch (Exception e)
						{
							Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.InsertIntoPcSupportMailList ({0})", e.Message));
							Logger.Out(logPathname, string.Format("{0} {1}:開始メール送信 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
							return;
						}
					}
				}
			}
			else
			{
				// 営業管理部にメール
				try
				{
					SendMailControl.SendEigyoKanriMail(PcSupportMail.MailType.Start, new List<PcSupportMail>(), pcList);
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
						SendMailControl.SendBranchMail(PcSupportMail.MailType.Start, branch, new List<PcSupportMail>(), pcList);
					}
				}
				catch (Exception ex)
				{
					// プログラムは止めない
					Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendBranchMail ({0})", ex.Message));
				}
			}
			Logger.Out(logPathname, string.Format("{0},{1},開始メール送信 正常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
		}

		/// <summary>
		/// 月次処理
		/// ソフト保守メンテナンス情報更新
		/// 契約更新メール送信
		/// 契約更新案内メール送信
		/// </summary>
		/// <param name="date">当日</param>
		private static void Monthly(Date date)
		{
			string logPathname = Program.GetLogPathname();
			Logger.Out(logPathname, string.Format("{0} {1}:月次処理 開始", Program.PROGRAM_NAME, DateTime.Now.ToString()));


			/////////////////////////////////////////////
			// ソフト保守メンテナンス情報更新処理
			/////////////////////////////////////////////

			Logger.Out(logPathname, string.Format("{0}:ソフト保守メンテナンス情報更新処理 開始", DateTime.Now.ToString()));

			// PC安心サポート管理情報リストの取得
			List<PcSupportControl> pcList = PcSupportManagerAccess.GetPcSupportControl();

			// ソフト保守メンテナンス情報リストの取得
			List<SoftMaintenanceContract> softList = PcSupportManagerAccess.GetSoftMaintenanceContract();
			foreach (PcSupportControl pc in pcList)
			{
				SoftMaintenanceContract soft = softList.Find(p => p.CustomerNo == pc.CustomerNo);
				if (null != soft)
				{
					if (pc.DisableFlag)
					{
						// ソフト保守メンテナンス情報を初期化
						soft.Reset();
					}
					else
					{
						if (pc.IsOrderInfoCompleted(false))
						{
							if (pc.StartDate.Value <= date)
							{
								bool subscription = true;
								if (pc.PeriodEndDate.HasValue && pc.PeriodEndDate.Value < date)
								{
									// 保守→未保守
									subscription = false;
								}
								else if (pc.EndDate.Value < date)
								{
									// 保守→未保守
									subscription = false;
								}
								soft.SetPcSupportControl(pc, subscription);
								if (!DebugMode)
								{
									try
									{
										PcSupportManagerAccess.SetSoftMaintenanceContract(soft);
									}
									catch (Exception ex)
									{
										Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.SetSoftMaintenanceContract ({0})", ex.Message));
										Logger.Out(logPathname, string.Format("{0} {1}:月次処理 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
										return;
									}
								}
								Logger.Out(logPathname, soft.ToLog(pc));

								pc.WonderWebRenewalFlag = false;
								pc.UpdateDateTime = date.ToDateTime();
								pc.UpdatePerson = Program.PROGRAM_NAME;
								if (!DebugMode)
								{
									try
									{
										PcSupportManagerAccess.SetPcSupportControl(pc);
									}
									catch (Exception ex)
									{
										Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.SetPcSupportControl ({0})", ex.Message));
										Logger.Out(logPathname, string.Format("{0} {1}:月次処理 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
										return;
									}
								}
							}
						}
					}
				}
				else
				{
					if (false == pc.DisableFlag)
					{
						if (pc.IsOrderInfoCompleted(false))
						{
							if (pc.StartDate.Value <= date)
							{
								if (pc.PeriodEndDate.HasValue)
								{
									if (date <= pc.PeriodEndDate.Value)
									{
										soft = new SoftMaintenanceContract(pc, true);
									}
								}
								else
								{
									soft = new SoftMaintenanceContract(pc, true);
								}
							}
						}
					}
					if (null != soft)
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
								Logger.Out(logPathname, string.Format("{0} {1}:月次処理 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
								return;
							}
						}
						Logger.Out(logPathname, soft.ToLog(pc));

						pc.WonderWebRenewalFlag = false;
						pc.UpdateDateTime = date.ToDateTime();
						pc.UpdatePerson = Program.PROGRAM_NAME;
						if (!DebugMode)
						{
							try
							{
								PcSupportManagerAccess.SetPcSupportControl(pc);
							}
							catch (Exception ex)
							{
								Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.SetPcSupportControl ({0})", ex.Message));
								Logger.Out(logPathname, string.Format("{0} {1}:月次処理 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
								return;
							}
						}
					}
				}
			}
			Logger.Out(logPathname, string.Format("{0}:ソフト保守メンテナンス情報更新 正常終了", DateTime.Now.ToString()));


			/////////////////////////////////////////////
			// 契約更新案内メール送信処理
			/////////////////////////////////////////////

			Logger.Out(logPathname, string.Format("{0}:契約更新案内メール送信処理 開始", DateTime.Now.ToString()));

			List<PcSupportControl> guidePcList = pcList.Where(p => true == p.IsSendGuideMail(date)).ToList();
			List<PcSupportMail> mailList = new List<PcSupportMail>();
			foreach (PcSupportControl pc in guidePcList)
			{
				// メール送信前データ格納
				pc.GuideMailDateTime = date.ToDateTime();
				pc.UpdateDateTime = date.ToDateTime();
				pc.UpdatePerson = Program.PROGRAM_NAME;

				// メール送信処理
				PcSupportMail mail = new PcSupportMail(PcSupportMail.MailType.Start, pc, date);
				mailList.Add(mail);
				try
				{
					SendMailControl.SendGuideMail(mail, pc.ClinicName);
				}
				catch (Exception ex)
				{
					Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendGuideMail ({0})", ex.Message));
					Logger.Out(logPathname, string.Format("{0} {1}:月次処理 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
					return;
				}
				Logger.Out(logPathname, mail.ToLog(pc));

				// メール送信後データ格納
				pc.UpdateMailDateTime = null;
				if (!DebugMode)
				{
					try
					{
						PcSupportManagerAccess.SetPcSupportControl(pc);
					}
					catch (Exception ex)
					{
						Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.SetPcSupportControl ({0})", ex.Message));
						Logger.Out(logPathname, string.Format("{0} {1}:月次処理 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
						return;
					}
				}
			}
			if (0 < mailList.Count)
			{
				if (!DebugMode)
				{
					try
					{
						PcSupportManagerAccess.InsertIntoPcSupportMailList(mailList);
					}
					catch (Exception ex)
					{
						Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.InsertIntoPcSupportMailList ({0})", ex.Message));
						Logger.Out(logPathname, string.Format("{0} {1}:月次処理 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
						return;
					}
				}
			}
			Logger.Out(logPathname, string.Format("{0}:契約更新案内メール送信処理 正常終了", DateTime.Now.ToString()));


			/////////////////////////////////////////////
			// 契約更新メール送信処理
			/////////////////////////////////////////////

			Logger.Out(logPathname, string.Format("{0}:契約更新メール送信処理 開始", DateTime.Now.ToString()));

			mailList.Clear();
			List<PcSupportGoodsInfo> goodsList = PcSupportManagerAccess.GetPcSupportGoodsInfo();
			string goodsName = "PC安心ｻﾎﾟｰﾄ(1年契約)";
			PcSupportGoodsInfo goods = goodsList.Find(p => p.GoodsID == PcSupportGoodsInfo.PC_SUPPORT1_GOODS_ID);
			if (null != goods)
			{
				goodsName = goods.GoodsName;
			}
			List<PcSupportControl> updatePcList = pcList.Where(p => true == p.IsSendUpdateMail(date)).ToList();
			foreach (PcSupportControl pc in updatePcList)
			{
				// メール送信前データ格納
				pc.UpdateMailDateTime = date.ToDateTime();
				pc.UpdateDateTime = date.ToDateTime();
				pc.UpdatePerson = Program.PROGRAM_NAME;

				// メール送信処理
				PcSupportMail mail = new PcSupportMail(PcSupportMail.MailType.Start, pc, date);
				mailList.Add(mail);
				try
				{
					SendMailControl.SendUpdateMail(mail, pc.ClinicName);
				}
				catch (Exception ex)
				{
					Logger.Out(logPathname, string.Format("#ERROR:SendMailControl.SendUpdateMail ({0})", ex.Message));
					Logger.Out(logPathname, string.Format("{0} {1}:月次処理 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
					return;
				}
				Logger.Out(logPathname, mail.ToLog(pc));

				// メール送信後データ格納
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
						PcSupportManagerAccess.SetPcSupportControl(pc);
					}
					catch (Exception ex)
					{
						Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.SetPcSupportControl ({0})", ex.Message));
						Logger.Out(logPathname, string.Format("{0} {1}:月次処理 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
						return;
					}
				}
			}
			if (0 < mailList.Count)
			{
				if (!DebugMode)
				{
					try
					{
						PcSupportManagerAccess.InsertIntoPcSupportMailList(mailList);
					}
					catch (Exception ex)
					{
						Logger.Out(logPathname, string.Format("#ERROR:PcSupportManagerAccess.InsertIntoPcSupportMailList ({0})", ex.Message));
						Logger.Out(logPathname, string.Format("{0} {1}:月次処理 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
						return;
					}
				}
			}
			Logger.Out(logPathname, string.Format("{0}:契約更新メール送信処理 正常終了", DateTime.Now.ToString()));

			Logger.Out(logPathname, string.Format("{0} {1}:月次処理 正常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
		}
	}
}
