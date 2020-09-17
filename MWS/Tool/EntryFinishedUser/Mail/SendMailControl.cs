//
// SendMailControl.cs
//
// 終了ユーザー管理 メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using MwsLib.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace EntryFinishedUser.Mail
{
	/// <summary>
	/// メール送信クラス
	/// </summary>
	public static class SendMailControl
	{
		/// <summary>
		/// 翌月終了ユーザー サービス契約中リスト メール送信（営業管理部宛て）
		/// </summary>
		/// <param name="userList">ユーザーリスト</param>
		public static void SendContractServiceMailNextMonth(List<ContractServiceUser> userList)
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

				// 件名
				msg.Subject = string.Format(@"{0} 翌月終了ユーザー サービス契約中リスト", yearMonthStr);

				// 本文
				msg.Body += string.Format(@"<div>"
							+ @"<p>営業管理部</p>"
							+ @"<p>{0} 翌月終了ユーザー サービス契約中リスト<br>"
							+ @"</div>", yearMonthStr);
				if (0 < userList.Count)
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>終了月</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>拠店名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>サービスID</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>サービス名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用開始日</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用終了日</font></th>"
								+ @"</tr>";
					foreach (ContractServiceUser user in userList)
					{
						msg.Body += string.Format(@"<tr>"
								+ @"<td><font size=2>{0}</font></td>"
								+ @"<td><font size=2>{1}</font></td>"
								+ @"<td><font size=2>{2}</font></td>"
								+ @"<td><font size=2>{3}</font></td>"
								+ @"<td><font size=2>{4}</font></td>"
								+ @"<td><font size=2>{5}</font></td>"
								+ @"<td><font size=2>{6}</font></td>"
								+ @"<td><font size=2>{7}</font></td>"
								+ @"</tr>"
								, user.CustomerID
								, user.UserName
								, user.FinishedYearMonth.Value.ToString()
								, user.AreaName
								, user.ServiceID
								, user.ServiceName
								, user.StartDate.HasValue ? user.StartDate.Value.ToShortDateString() : ""
								, user.EndDate.HasValue ? user.EndDate.Value.ToShortDateString() : "");
					}
					msg.Body += @"</table>";
					msg.Body += @"<div><br><p>来月終了ユーザーの契約中サービスをご確認ください。</p>";
				}
				else
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>終了月</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>拠店名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>サービスID</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>サービス名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用開始日</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用終了日</font></th>"
								+ @"</tr>";
					msg.Body += @"</table>";
					msg.Body += @"<br><p>来月終了ユーザーでサービスの契約中はありませでした。</p>";
				}
				msg.Body += @"</div>"
							+ @"<div>"
							+ @"<p>以上、よろしくお願いいたします。<br>営業管理部</p>"
							+ @"</div>"
							+ @"</font>"
							+ @"</body>"
							+ @"</html>";

				// メール送信
				SendMailControl.SendMail(msg, conf);
			}
		}

		/// <summary>
		/// 前月終了済ユーザー サービス契約中リスト メール送信（営業管理部宛て）
		/// </summary>
		/// <param name="userList">ユーザーリスト</param>
		public static void SendContractServiceMailPrevMonth(List<ContractServiceUser> userList)
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

				// 件名
				msg.Subject = string.Format(@"{0} 前月終了済ユーザー サービス契約中リスト", yearMonthStr);

				// 本文
				msg.Body += string.Format(@"<div>"
							+ @"<p>営業管理部</p>"
							+ @"<p>{0} 前月終了済ユーザー サービス契約中リスト<br>"
							+ @"</div>", yearMonthStr);
				if (0 < userList.Count)
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>終了月</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>拠店名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>サービスID</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>サービス名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用開始日</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用終了日</font></th>"
								+ @"</tr>";
					foreach (ContractServiceUser user in userList)
					{
						msg.Body += string.Format(@"<tr>"
								+ @"<td><font size=2>{0}</font></td>"
								+ @"<td><font size=2>{1}</font></td>"
								+ @"<td><font size=2>{2}</font></td>"
								+ @"<td><font size=2>{3}</font></td>"
								+ @"<td><font size=2>{4}</font></td>"
								+ @"<td><font size=2>{5}</font></td>"
								+ @"<td><font size=2>{6}</font></td>"
								+ @"<td><font size=2>{7}</font></td>"
								+ @"</tr>"
								, user.CustomerID
								, user.UserName
								, user.FinishedYearMonth.Value.ToString()
								, user.AreaName
								, user.ServiceID
								, user.ServiceName
								, user.StartDate.HasValue ? user.StartDate.Value.ToShortDateString() : ""
								, user.EndDate.HasValue ? user.EndDate.Value.ToShortDateString() : "");
					}
					msg.Body += @"</table>";
					msg.Body += @"<div><br><p>前月終了済ユーザーの契約中サービスをご確認ください。</p>";
				}
				else
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>終了月</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>拠店名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>サービスID</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>サービス名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用開始日</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用終了日</font></th>"
								+ @"</tr>";
					msg.Body += @"</table>";
					msg.Body += @"<br><p>前月終了済ユーザーでサービスの契約中はありませでした。</p>";
				}
				msg.Body += @"</div>"
							+ @"<div>"
							+ @"<p>以上、よろしくお願いいたします。<br>営業管理部</p>"
							+ @"</div>"
							+ @"</font>"
							+ @"</body>"
							+ @"</html>";

				// メール送信
				SendMailControl.SendMail(msg, conf);
			}
		}
	
/*
				/// <summary>
				/// 終了ユーザー連絡メール送信（営業管理部宛て）
				/// </summary>
				/// <param name="userList">ユーザーリスト</param>
				/// <param name="userCount">ユーザー数</param>
				public static void SendFinishedUserMail(IEnumerable<EntryFinishedUserData> userList, int userCount)
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

						// 件名
						msg.Subject = string.Format(@"{0} 終了ユーザーリスト", yearMonthStr);

						// 本文
						msg.Body += string.Format(@"<div>"
									+ @"<p>営業管理部</p>"
									+ @"<p>{0} 終了ユーザーリスト<br>"
									+ @"</div>", yearMonthStr);
						if (0 < userCount)
						{
							msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
										+ @"<tr>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>システム名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>拠店名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>終了月</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>終了届受領日</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>非paletteユーザー</font></th>"
										+ @"</tr>";
							foreach (EntryFinishedUserData user in userList)
							{
								msg.Body += string.Format(@"<tr>"
										+ @"<td><font size=2>{0}</font></td>"
										+ @"<td><font size=2>{1}</font></td>"
										+ @"<td><font size=2>{2}</font></td>"
										+ @"<td><font size=2>{3}</font></td>"
										+ @"<td><font size=2>{4}</font></td>"
										+ @"<td><font size=2>{5}</font></td>"
										+ @"<td><font size=2>{6}</font></td>"
										+ @"</tr>", user.CustomerID, user.UserName, user.SystemName, user.AreaName, user.FinishedYearMonth.Value.ToString(), user.AcceptDate.Value.ToString(), (user.NonPaletteUser) ? "○" : "×");
							}
							msg.Body += @"</table>";
							msg.Body += @"<div><br><p>上記のユーザーの終了処理を実行しました。</p>";
						}
						else
						{
							msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
										+ @"<tr>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>システム名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>拠店名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>終了月</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>終了届受領日</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>非paletteユーザー</font></th>"
										+ @"</tr>";
							msg.Body += @"</table>";
							msg.Body += @"<br><p>終了ユーザーはいませんでした。</p>";
						}
						msg.Body += @"</div>"
									+ @"<div>"
									+ @"<p>以上、よろしくお願いいたします。<br>営業管理部</p>"
									+ @"</div>"
									+ @"</font>"
									+ @"</body>"
									+ @"</html>";

						// メール送信
						SendMailControl.SendMail(msg, conf);
					}
				}

				/// <summary>
				/// 非paletteユーザー連絡メール送信（営業管理部宛て）
				/// </summary>
				/// <param name="userList">ユーザーリスト</param>
				/// <param name="userCount">ユーザー数</param>
				public static void SendNonPaletteUserMail(IEnumerable<EntryFinishedUserData> userList, int userCount)
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

						// 件名
						msg.Subject = string.Format(@"{0} 非paletteユーザーリスト", yearMonthStr);

						// 本文
						msg.Body += string.Format(@"<div>"
									+ @"<p>営業管理部</p>"
									+ @"<p>{0} 非paletteユーザーリスト<br>"
									+ @"</div>", yearMonthStr);
						if (0 < userCount)
						{
							msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
										+ @"<tr>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>システム名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>拠店名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>終了月</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>終了届受領日</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>非paletteユーザー</font></th>"
										+ @"</tr>";
							foreach (EntryFinishedUserData user in userList)
							{
								msg.Body += string.Format(@"<tr>"
										+ @"<td><font size=2>{0}</font></td>"
										+ @"<td><font size=2>{1}</font></td>"
										+ @"<td><font size=2>{2}</font></td>"
										+ @"<td><font size=2>{3}</font></td>"
										+ @"<td><font size=2>{4}</font></td>"
										+ @"<td><font size=2>{5}</font></td>"
										+ @"<td><font size=2>{6}</font></td>"
										+ @"</tr>", user.CustomerID, user.UserName, user.SystemName, user.AreaName, user.FinishedYearMonth.Value.ToString(), user.AcceptDate.Value.ToString(), (user.NonPaletteUser) ? "○" : "×");
							}
							msg.Body += @"</table>";
							msg.Body += @"<div><br><p>上記のユーザーの非paletteへの切替処理を実行しました。</p>";
						}
						else
						{
							msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
										+ @"<tr>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>システム名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>拠店名</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>終了月</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>終了届受領日</font></th>"
										+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>非paletteユーザー</font></th>"
										+ @"</tr>";
							msg.Body += @"</table>";
							msg.Body += @"<br><p>非paletteユーザーはいませんでした。</p>";
						}
						msg.Body += @"</div>"
									+ @"<div>"
									+ @"<p>以上、よろしくお願いいたします。<br>営業管理部</p>"
									+ @"</div>"
									+ @"</font>"
									+ @"</body>"
									+ @"</html>";

						// メール送信
						SendMailControl.SendMail(msg, conf);
					}
				}
		*/
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
		/// <param name="conf">App.config</param>
		/// <param name="to">宛先</param>
		private static void SendMail(MailMessage msg, Dictionary<string, string> conf)
		{
#if DEBUG
			// 差出人（From）
			msg.From = new MailAddress("suguro@mic.jp");		// suguro@mic.jp

			// 宛先（To）を登録する
			msg.To.Add(new MailAddress("suguro@mic.jp"));		// suguro@mic.jp
#else
			// 差出人（From）
			msg.From = new MailAddress(conf["from"]);			// eigyo_kanri@mic.jp

			// 宛先（To）を登録する
			msg.To.Add(new MailAddress(conf["to"]));			// eigyo_kanri@mic.jp

#endif
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
