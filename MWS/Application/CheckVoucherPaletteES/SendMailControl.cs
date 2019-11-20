//
// SendMailControl.cs
//
// メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2019/11/15 勝呂)
// 
using MwsLib.BaseFactory.CheckVoucherPaletteES;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CheckVoucherPaletteES
{
	/// <summary>
	/// メール送信クラス
	/// </summary>
	public static class SendMailControl
	{
		/// <summary>
		/// palette ES 起票不備確認連絡メール送信（営業管理部宛て）
		/// </summary>
		/// <param name="userList">ユーザーリスト</param>
		/// <param name="userCount">ユーザー数</param>
		public static void SendMail(List<OrderVoucher> list)
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

				// 件名
				msg.Subject = @"palette ES 起票不備の確認連絡";

				// 本文
				msg.Body += @"<div>"
							+ @"<p>各位</p>"
							+ @"<p>palette ES の伝票起票に不備を検出しました。<br>"
							+ @"<p>伝票内容の確認をお願いします。<br>"
							+ @"<div>";
				msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
							+ @"<tr>"
							+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>拠点名</font></th>"
							+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>担当者</font></th>"
							+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>受注番号</font></th>"
							+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
							+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
							+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>受注日</font></th>"
							+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>受注承認日</font></th>"
							+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>売上承認日</font></th>"
							+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>不備内容</font></th>"
							+ @"</tr>";
				foreach (OrderVoucher es in list)
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
							+ @"<td><font size=2>{8}</font></td>"
							+ @"</tr>"
							, es.担当支店名
							, es.担当者名
							, es.受注番号.ToString()
							, es.顧客No.ToString()
							, es.顧客名
							, es.受注日Str
							, es.受注承認日Str
							, es.売上承認日Str
							, string.Join("<br>", es.ErrorList.ToArray()));
				}
				msg.Body += @"</table>";

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
			// 差出人（From）
			msg.From = new MailAddress(conf["from"]);			// suguro@mic.jp

			if (Program.TEST_MAIL_SEND)
			{
				// 宛先（To）を登録する
				msg.To.Add(new MailAddress(conf["test_to"]));	// suguro@mic.jp
			}
			else
			{
				// 宛先（To）を登録する
				msg.To.Add(new MailAddress(conf["to"]));		// eigyo_kanri@mic.jp
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
