//
// SendMailControl.cs
//
// 終了ユーザー管理 メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/12/12 勝呂)
// 
using MwsLib.BaseFactory.EntryFinishedUser;
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
		/// メール送信（営業管理部宛て）
		/// </summary>
		/// <param name="userList">ユーザーリスト</param>
		/// <param name="finished">終了ユーザーリストかどうか？</param>
		public static void SendEigyoKanriMail(List<EntryFinishedUserData> userList, bool finished)
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
				if (false == finished)
				{
					// 終了予定ユーザーリスト

					// 件名
					msg.Subject = string.Format(@"{0} 終了予定ユーザーリスト", yearMonthStr);

					// 本文
					msg.Body += string.Format(@"<div>"
								+ @"<p>営業管理部</p>"
								+ @"<p>{0} 終了予定ユーザーリスト<br>"
								+ @"</div>", yearMonthStr);
					if (0 < userList.Count)
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
						msg.Body += @"<div><br><p>終了予定ユーザーに課金対象外フラグを設定しました。</p>";
					}
					else
					{
						msg.Body += @"<br><p>終了予定ユーザーはいませんでした。</p>";
					}
				}
				else
				{
					// 終了ユーザーリスト

					// 件名
					msg.Subject = string.Format(@"{0} 終了ユーザーリスト", yearMonthStr);

					// 本文
					msg.Body += string.Format(@"<div>"
								+ @"<p>営業管理部</p>"
								+ @"<p>{0} 終了ユーザーリスト<br>"
								+ @"</div>", yearMonthStr);
					if (0 < userList.Count)
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
						msg.Body += @"<div><br><p>終了ユーザーに終了フラグを設定しました。</p>";
					}
					else
					{
						msg.Body += @"<br><p>終了ユーザーはいませんでした。</p>";
					}
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
		private static void SendMail(MailMessage msg, Dictionary<string, string> conf, string to)
		{
			if (Program.DATABACE_ACCEPT_CT)
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
