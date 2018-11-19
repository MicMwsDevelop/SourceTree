//
// SendMailControl.cs
//
// PC安心サポート管理 メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/11/19 勝呂)
// 
using MwsLib.BaseFactory.PcSupportManager;
using MwsLib.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace PcSupportManager.Mail
{
	/// <summary>
	/// メール送信クラス
	/// </summary>
	public static class SendMailControl
	{
		/// <summary>
		/// 開始メールの送信
		/// </summary>
		/// <param name="mail">PC安心サポート送信メール情報</param>
		/// <param name="clinicName">医院名</param>
		public static void SendStartMail(PcSupportMail mail, string clinicName)
		{
			using (MailMessage msg = new MailMessage())
			{
				Encoding enc = Encoding.GetEncoding("shift_jis");
				msg.SubjectEncoding = enc;
				msg.BodyEncoding = enc;

				// App.configの読込み
				Dictionary<string, string> conf = SendMailControl.ReadConfig();

				// 件名
				msg.Subject = conf["subject_start"];

				// 本文
				using (var sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), conf["body_start"]), enc))
				{
					// パラメータ（%～%）の置換
					msg.Body = sr.ReadToEnd().Replace("%CLINIC_NAME%", clinicName).Replace("%AGREE_YEAR%", mail.AgreeYear.ToString()).Replace("%START_YM%", mail.StartDate.Value.ToYearMonth().ToString()).Replace("%END_YM%", mail.EndDate.Value.ToYearMonth().ToString());
				}
				// メール送信
				SendMailControl.SendMail(msg, conf, mail.MailAddress);
			}
		}

		/// <summary>
		/// 契約更新案内メールの送信
		/// </summary>
		/// <param name="mail">PC安心サポート送信メール情報</param>
		/// <param name="clinicName">医院名</param>
		public static void SendGuideMail(PcSupportMail mail, string clinicName)
		{
			using (MailMessage msg = new MailMessage())
			{
				Encoding enc = Encoding.GetEncoding("shift_jis");
				msg.SubjectEncoding = enc;
				msg.BodyEncoding = enc;

				// App.configの読込み
				Dictionary<string, string> conf = SendMailControl.ReadConfig();

				// 件名
				msg.Subject = conf["subject_guide"];

				// 本文
				using (var sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), conf["body_guide"]), enc))
				{
					// パラメータ（%～%）の置換
					msg.Body = sr.ReadToEnd().Replace("%CLINIC_NAME%", clinicName).Replace("%AGREE_YEAR%", mail.AgreeYear.ToString()).Replace("%END_YM%", mail.EndDate.Value.ToYearMonth().ToString());
				}
				// メール送信
				SendMailControl.SendMail(msg, conf, mail.MailAddress);
			}
		}

		/// <summary>
		/// 契約更新メールの送信
		/// </summary>
		/// <param name="mail">PC安心サポート送信メール情報</param>
		/// <param name="clinicName">医院名</param>
		public static void SendUpdateMail(PcSupportMail mail, string clinicName)
		{
			using (MailMessage msg = new MailMessage())
			{
				Encoding enc = Encoding.GetEncoding("shift_jis");
				msg.SubjectEncoding = enc;
				msg.BodyEncoding = enc;

				// App.configの読込み
				Dictionary<string, string> conf = SendMailControl.ReadConfig();

				// 件名
				msg.Subject = conf["subject_update"];

				// 本文
				using (var sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), conf["body_update"]), enc))
				{
					// パラメータ（%～%）の置換
					msg.Body = sr.ReadToEnd().Replace("%CLINIC_NAME%", clinicName).Replace("%AGREE_YEAR%", mail.AgreeYear.ToString()).Replace("%START_YM%", mail.StartDate.Value.ToYearMonth().ToString()).Replace("%END_YM%", mail.EndDate.Value.ToYearMonth().ToString());
				}
				// メール送信
				SendMailControl.SendMail(msg, conf, mail.MailAddress);
			}
		}

		/// <summary>
		/// メール送信（営業管理部宛て）
		/// </summary>
		/// <param name="mailType">メール種別</param>
		/// <param name="mailList">送信メール情報</param>
		/// <param name="pcList">PC安心サポート管理情報</param>
		public static void SendEigyoKanriMail(PcSupportMail.MailType mailType, List<PcSupportMail> mailList, List<PcSupportControl> pcList)
		{
			using (MailMessage msg = new MailMessage())
			{
				Encoding enc = Encoding.GetEncoding("shift_jis");
				msg.SubjectEncoding = enc;
				msg.BodyEncoding = enc;

				// App.configの読込み
				Dictionary<string, string> conf = SendMailControl.ReadConfig();

				msg.IsBodyHtml = true;
				msg.Body = @"<html>"
							+ @"<head><meta http-equiv=Content-Type content=""text/html; charset=iso-2022-jp""></head>"
							+ @"<body>"
							+ @"<font face=""MS UI Gothic"" size=""2"">";

				string yearMonthStr = Date.Today.ToYearMonth().GetJapaneseString(false, '0', true, true);
				switch (mailType)
				{
					// 開始メール
					case PcSupportMail.MailType.Start:
						// 件名
						msg.Subject = string.Format(@"{0} PC安心サポート 開始メールを送信しました", yearMonthStr);

						// 本文
						msg.Body += string.Format(@"<div>"
									+ @"<p>営業管理部</p>"
									+ @"<p>{0} PC安心サポート 開始メールを送信対象ユーザーに送信しました。<br>"
									+ @"</div>", yearMonthStr);
						if (0 < mailList.Count)
						{
							msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
										+ @"<tr>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>拠店名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>商品名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>契約開始日</font></th>"
										+ @"</tr>";
							foreach (PcSupportMail mail in mailList)
							{
								PcSupportControl pc = pcList.Find(p => p.CustomerNo == mail.CustomerNo);
								if (null != pc)
								{
									msg.Body += string.Format(@"<tr>"
												+ @"<td><font size=2>{0}</font></td>"
												+ @"<td><font size=2>{1}</font></td>"
												+ @"<td><font size=2>{2}</font></td>"
												+ @"<td><font size=2>{3}</font></td>"
												+ @"<td><font size=2>{4}</font></td>"
												+ @"</tr>", pc.BranchName, pc.CustomerNo, pc.ClinicName, pc.GoodsName, pc.StartDate.Value.ToString());
								}
							}
							msg.Body += @"</table>";
						}
						else
						{
							msg.Body += @"<br><p>PC安心サポート 開始メール送信対象ユーザーはいませんでした。</p>";
						}
						break;
					// 契約更新案内メール
					case PcSupportMail.MailType.Guide:
						// 件名
						msg.Subject = string.Format(@"{0} PC安心サポート 契約更新案内メールを送信しました", yearMonthStr);

						// 本文
						msg.Body += string.Format(@"<div>"
									+ @"<p>営業管理部</p>"
									+ @"<p>{0} PC安心サポート 契約更新案内メールを送信対象ユーザーに送信しました。<br>"
									+ @"</div>", yearMonthStr);
						if (0 < mailList.Count)
						{
							msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
										+ @"<tr>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>拠店名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>商品名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>契約開始日</font></th>"
										+ @"</tr>";
							foreach (PcSupportMail mail in mailList)
							{
								PcSupportControl pc = pcList.Find(p => p.CustomerNo == mail.CustomerNo);
								if (null != pc)
								{
									msg.Body += string.Format(@"<tr>"
												+ @"<td><font size=2>{0}</font></td>"
												+ @"<td><font size=2>{1}</font></td>"
												+ @"<td><font size=2>{2}</font></td>"
												+ @"<td><font size=2>{3}</font></td>"
												+ @"<td><font size=2>{4}</font></td>"
												+ @"</tr>", pc.BranchName, pc.CustomerNo, pc.ClinicName, pc.GoodsName, pc.StartDate.Value.ToString());
								}
							}
							msg.Body += @"</table>";
						}
						else
						{
							msg.Body += @"<br><p>PC安心サポート 契約更新案内メール送信対象ユーザーはいませんでした。</p>";
						}
						break;
					// 契約更新メール
					case PcSupportMail.MailType.Update:
						// 件名
						msg.Subject = string.Format(@"{0} PC安心サポート 契約更新メールを送信しました", yearMonthStr);

						// 本文
						msg.Body += string.Format(@"<div>"
									+ @"<p>営業管理部</p>"
									+ @"<p>{0} PC安心サポート 契約更新メールを送信対象ユーザーに送信しました。<br>"
									+ @"</div>", yearMonthStr);
						if (0 < mailList.Count)
						{
							msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
										+ @"<tr>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>拠店名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>商品名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>契約開始日</font></th>"
										+ @"</tr>";
							foreach (PcSupportMail mail in mailList)
							{
								PcSupportControl pc = pcList.Find(p => p.CustomerNo == mail.CustomerNo);
								if (null != pc)
								{
									msg.Body += string.Format(@"<tr>"
												+ @"<td><font size=2>{0}</font></td>"
												+ @"<td><font size=2>{1}</font></td>"
												+ @"<td><font size=2>{2}</font></td>"
												+ @"<td><font size=2>{3}</font></td>"
												+ @"<td><font size=2>{4}</font></td>"
												+ @"</tr>", pc.BranchName, pc.CustomerNo, pc.ClinicName, pc.GoodsName, pc.StartDate.Value.ToString());
								}
							}
							msg.Body += @"</table>";
						}
						else
						{
							msg.Body += @"<br><p>PC安心サポート 契約更新メール送信対象ユーザーはいませんでした。</p>";
						}
						break;
				}
				msg.Body += @"</div>"
							+ @"<div>"
							+ @"<p>以上、よろしくお願いいたします。<br>営業管理部</p>"
							+ @"</div>"
							+ @"</font>"
							+ @"</body>"
							+ @"</html>";

				// メール送信
				SendMailControl.SendMail(msg, conf, conf["to"]);
			}
		}

		/// <summary>
		/// メール送信（拠店宛て）
		/// </summary>
		/// <param name="mailType">メール種別</param>
		/// <param name="branch">拠店情報</param>
		/// <param name="mailList">送信メール情報</param>
		/// <param name="pcList">PC安心サポート管理情報</param>
		public static void SendBranchMail(PcSupportMail.MailType mailType, BranchInfo branch, List<PcSupportMail> mailList, List<PcSupportControl> pcList)
		{
			using (MailMessage msg = new MailMessage())
			{
				Encoding enc = Encoding.GetEncoding("shift_jis");
				msg.SubjectEncoding = enc;
				msg.BodyEncoding = enc;

				// App.configの読込み
				Dictionary<string, string> conf = SendMailControl.ReadConfig();

				msg.IsBodyHtml = true;
				msg.Body = @"<html>"
							+ @"<head><meta http-equiv=Content-Type content=""text/html; charset=iso-2022-jp""></head>"
							+ @"<body>"
							+ @"<font face=""MS UI Gothic"" size=""2"">";

				string yearMonthStr = Date.Today.ToYearMonth().GetJapaneseString(false, '0', true, true);
				switch (mailType)
				{
					// 開始メール
					case PcSupportMail.MailType.Start:
						// 件名
						msg.Subject = string.Format(@"【{0}】{1} PC安心サポート 開始対象ユーザー", branch.BranchName, yearMonthStr);

						// 本文
						msg.Body += string.Format(@"<div>"
									+ @"<p>{0}</p>"
									+ @"<p>{1} PC安心サポート 開始対象ユーザーをご連絡いたします。<br>"
									+ @"</div>", branch.BranchName, yearMonthStr);
						if (0 < mailList.Count)
						{
							msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
										+ @"<tr>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>拠店名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>商品名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>契約開始日</font></th>"
										+ @"</tr>";
							foreach (PcSupportMail mail in mailList)
							{
								PcSupportControl pc = pcList.Find(p => p.CustomerNo == mail.CustomerNo);
								if (null != pc)
								{
									msg.Body += string.Format(@"<tr>"
												+ @"<td><font size=2>{0}</font></td>"
												+ @"<td><font size=2>{1}</font></td>"
												+ @"<td><font size=2>{2}</font></td>"
												+ @"<td><font size=2>{3}</font></td>"
												+ @"<td><font size=2>{4}</font></td>"
												+ @"</tr>", branch.BranchName, pc.CustomerNo, pc.ClinicName, pc.GoodsName, pc.StartDate.Value.ToString());
								}
							}
							msg.Body += @"</table>";
						}
						else
						{
							msg.Body += @"<br><p>PC安心サポート 開始対象ユーザーはいませんでした。</p>";
						}
						break;
					// 契約更新案内メール
					case PcSupportMail.MailType.Guide:
						// 件名
						msg.Subject = string.Format(@"【{0}】{1} PC安心サポート 契約更新案内対象ユーザー", branch.BranchName, yearMonthStr);

						// 本文
						msg.Body += string.Format(@"<div>"
									+ @"<p>{0}</p>"
									+ @"<p>{1} PC安心サポート 契約更新案内対象ユーザーをご連絡いたします。<br>"
									+ @"</div>", branch.BranchName, yearMonthStr);
						if (0 < mailList.Count)
						{
							msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
										+ @"<tr>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>拠店名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>商品名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>契約開始日</font></th>"
										+ @"</tr>";
							foreach (PcSupportMail mail in mailList)
							{
								PcSupportControl pc = pcList.Find(p => p.CustomerNo == mail.CustomerNo);
								if (null != pc)
								{
									msg.Body += string.Format(@"<tr>"
												+ @"<td><font size=2>{0}</font></td>"
												+ @"<td><font size=2>{1}</font></td>"
												+ @"<td><font size=2>{2}</font></td>"
												+ @"<td><font size=2>{3}</font></td>"
												+ @"<td><font size=2>{4}</font></td>"
												+ @"</tr>", branch.BranchName, pc.CustomerNo, pc.ClinicName, pc.GoodsName, pc.StartDate.Value.ToString());
								}
							}
							msg.Body += @"</table>";
						}
						else
						{
							msg.Body += @"<br><p>PC安心サポート 契約更新案内対象ユーザーはいませんでした。</p>";
						}
						break;
					// 契約更新メール
					case PcSupportMail.MailType.Update:
						// 件名
						msg.Subject = string.Format(@"【{0}】{1} PC安心サポート 契約更新対象ユーザー", branch.BranchName, yearMonthStr);

						// 本文
						msg.Body += string.Format(@"<div>"
									+ @"<p>{0}</p>"
									+ @"<p>{1} PC安心サポート 契約更新対象ユーザーをご連絡いたします。<br>"
									+ @"</div>", branch.BranchName, yearMonthStr);
						if (0 < mailList.Count)
						{
							msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
										+ @"<tr>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>拠店名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>商品名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>契約開始日</font></th>"
										+ @"</tr>";
							foreach (PcSupportMail mail in mailList)
							{
								PcSupportControl pc = pcList.Find(p => p.CustomerNo == mail.CustomerNo);
								if (null != pc)
								{
									msg.Body += string.Format(@"<tr>"
												+ @"<td><font size=2>{0}</font></td>"
												+ @"<td><font size=2>{1}</font></td>"
												+ @"<td><font size=2>{2}</font></td>"
												+ @"<td><font size=2>{3}</font></td>"
												+ @"<td><font size=2>{4}</font></td>"
												+ @"</tr>", branch.BranchName, pc.CustomerNo, pc.ClinicName, pc.GoodsName, pc.StartDate.Value.ToString());
								}
							}
							msg.Body += @"</table>";
						}
						else
						{
							msg.Body += @"<br><p>PC安心サポート 契約更新対象ユーザーはいませんでした。</p>";
						}
						break;
				}
				msg.Body += @"</div>"
							+ @"<div>"
							+ @"<p>以上、よろしくお願いいたします。<br>営業管理部</p>"
							+ @"</div>"
							+ @"</font>"
							+ @"</body>"
							+ @"</html>";

				// メール送信
				SendMailControl.SendMail(msg, conf, branch.MailAddress, conf["to"]);
			}
		}

		/// <summary>
		/// App.configの読込み
		/// </summary>
		/// <returns>App.config</returns>
		private static Dictionary<string, string> ReadConfig()
		{
			var configs = new Dictionary<string, string>();
			ConfigurationManager.OpenExeConfiguration(@Process.GetCurrentProcess().MainModule.FileName);
			foreach (var key in ConfigurationManager.AppSettings.AllKeys)
			{
				configs[key] = ConfigurationManager.AppSettings[key];
			}
			return configs;
		}

		/// <summary>
		/// メール送信
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="to">送信先メールアドレス</param>
		/// <param name="cc">CCメールアドレス</param>
		/// <param name="conf">App.config</param>
		private static void SendMail(MailMessage msg, Dictionary<string, string> conf, string to, string cc = "")
		{
			if (Program.DebugMode)
			{
				// 差出人（From）
				msg.From = new MailAddress(conf["test_from"]);		// suguro@mic.jp

				// 宛先（To）を登録する
				msg.To.Add(new MailAddress(conf["test_to"]));       // suguro@mic.jp
			}
			else
			{
				// 差出人（From）
				msg.From = new MailAddress(conf["from"]);           // 営業管理部

				// 宛先（To）を登録する
				msg.To.Add(new MailAddress(to));

				if (0 < cc.Length)
				{
					// CCを登録する
					msg.CC.Add(new MailAddress(cc));
				}
			}
			// SMTPサーバの設定
			using (SmtpClient smtp = new SmtpClient())
			{
				smtp.Host = conf["smtp"];
				smtp.Port = Convert.ToInt32(conf["port"]);

				// SMTP認証
				if (!String.IsNullOrEmpty(conf["user"]) && !String.IsNullOrEmpty(conf["pass"]))
				{
					smtp.Credentials = new NetworkCredential(conf["user"], conf["pass"]);
				}
				// メール送信
				smtp.Send(msg);
			}
		}
	}
}
