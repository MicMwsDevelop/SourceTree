﻿//
// SendMailControl.cs
//
// メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/10/09 勝呂)
// Ver1.05 2023/08組織変更対応 メール「ソフトウェア保守料自動更新 売上連絡」の宛先・送信元など変更(メールアドレス複数指定対応含む)(2024/02/06 越田)

using CommonLib.BaseFactory.SoftwareMainteEarnings;
using MwsLib.Settings.Mail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SoftwareMainteEarningsFile.Mail
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
		public static void SoftwareMainteSendMail(List<SoftwareMainteEarningsOut> userList)
		{
			using (MailMessage msg = new MailMessage())
			{
				Encoding enc = Encoding.GetEncoding("shift_jis");
				msg.SubjectEncoding = enc;
				msg.BodyEncoding = enc;
				msg.IsBodyHtml = true;
				msg.Body = @"<html>"
							+ @"<head><meta http-equiv=Content-Type content=""text/html; charset=iso-2022-jp""></head>"
							+ @"<body>"
							+ @"<font face=""MS UI Gothic"" size=""2"">";

				// 件名
				msg.Subject = "ソフトウェア保守料自動更新 売上連絡";

				// 本文
				// Ver1.05 2023/08組織変更対応 メール「ソフトウェア保守料自動更新 売上連絡」の宛先・送信元など変更(メールアドレス複数指定対応含む)(2024/02/06 越田)
				msg.Body += string.Format(@"<div>"
							+ @"<p>宛先部署 担当者様</p>"
							+ @"<p>palette ES ソフトウェア保守料１年の期間更新と売上データを作成しました。<br>"
							+ @"<br>"
							+ @"{0}フォルダに{1}を格納しました。<br>"
							+ @"PCA読込作業をお願いします。<br></p>"
							+ @"</div>"
							, Program.gSettings.ExportDir
							, Program.gSettings.ExportFilename);
				if (0 < userList.Count)
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>更新期間</font></th>"
								+ @"</tr>";
					foreach (SoftwareMainteEarningsOut user in userList)
					{
						msg.Body += string.Format(@"<tr>"
								+ @"<td><font size=2>{0}</font></td>"
								+ @"<td><font size=2>{1}</font></td>"
								+ @"<td><font size=2>{2}</font></td>"
								+ @"</tr>"
								, user.f顧客No
								, user.f顧客名
								, user.摘要名);
					}
					msg.Body += @"</table>";
				}
				else
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>更新期間</font></th>"
								+ @"</tr>"
								+ @"</table>";
					msg.Body += @"<br><p>ソフトウェア保守料更新対象ユーザーはありませんでした。</p>";
				}
				// Ver1.05 2023/08組織変更対応 メール「ソフトウェア保守料自動更新 売上連絡」の宛先・送信元など変更(メールアドレス複数指定対応含む)(2024/02/06 越田)
				msg.Body += @"</div>"
							+ @"<div>"
							+ @"<p>以上、よろしくお願いいたします。</p>"
							+ @"</div>"
							+ @"</font>"
							+ @"</body>"
							+ @"</html>";

				// メール送信
				// Ver1.05 2023/08組織変更対応 メール「ソフトウェア保守料自動更新 売上連絡」の宛先・送信元など変更(メールアドレス複数指定対応含む)(2024/02/06 越田)
				//SendMailControl.SendMail(msg);
				MailSettings.SendMail(msg, Program.gSettings.Mail);
			}
		}

		/// <summary>
		/// メール送信
		/// </summary>
		/// <param name="msg"></param>
		private static void SendMail(MailMessage msg)
		{
			// 差出人（From）
			msg.From = new MailAddress(Program.gSettings.Mail.From);           // eigyo_kanri@mic.jp

#if DEBUG
			// 宛先（To）を登録する
			msg.To.Add(new MailAddress(Program.gSettings.Mail.TestTo));      // suguro@mic.jp
			if (0 < Program.gSettings.Mail.TestCC.Length)
			{
				msg.CC.Add(new MailAddress(Program.gSettings.Mail.TestCC));      // suguro@mic.jp
			}
#else
			// 宛先（To）を登録する
			msg.To.Add(new MailAddress(Program.gSettings.Mail.To));			// eigyo_kanri@mic.jp
			if (0 < Program.gSettings.Mail.CC.Length)
			{
				msg.CC.Add(new MailAddress(Program.gSettings.Mail.CC));			// eigyo_kanri@mic.jp
			}
#endif

			// SMTPサーバの設定
			using (SmtpClient smtp = new SmtpClient())
			{
				smtp.Host = Program.gSettings.Mail.Smtp;
				smtp.Port = Program.gSettings.Mail.Port;

				// SMTP認証
				if (!String.IsNullOrEmpty(Program.gSettings.Mail.User) && !String.IsNullOrEmpty(Program.gSettings.Mail.Pass))
				{
					smtp.Credentials = new NetworkCredential(Program.gSettings.Mail.User, Program.gSettings.Mail.Pass);
				}
				// メール送信
				smtp.Send(msg);
			}
		}
	}
}
