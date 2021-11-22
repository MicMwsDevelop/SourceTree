//
// SendMailControl.cs
//
// メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/11/24 勝呂)
//
using CommonLib.BaseFactory.AlmexMainte;
using CommonLib.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AlmexMainteEarningsFile.Mail
{
	/// <summary>
	/// メール送信クラス
	/// </summary>
	public static class SendMailControl
	{
		/// <summary>
		/// メール送信（経理部宛て）
		/// </summary>
		/// <param name="userList">ユーザーリスト</param>
		/// <param name="formalFilename">出力ファイル名</param>
		/// <param name="saleDate">売上日</param>
		public static void AlmexMainteSendMail(List<AlmexMainteEarningsOut> userList, string formalFilename, Date saleDate)
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
				msg.Subject = "アルメックス保守サービス自動更新 売上連絡";

				// 本文
				msg.Body += string.Format(@"<div>"
							+ @"<p>経理部各位</p>"
							+ @"<p>アルメックス保守サービスの期間更新と売上データを作成しました。<br>"
							+ @"<br>"
							+ @"{0}フォルダに{1}を格納しました。<br>"
							+ @"PCA読込作業をお願いします。<br></p>"
							+ @"</div>"
							, Program.gSettings.ExportDir
							, formalFilename);

				YearMonth ym = saleDate.ToYearMonth();
				if (null != userList)
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>保守サービス</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用年月</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>終了</font></th>"
								+ @"</tr>";
					foreach (AlmexMainteEarningsOut user in userList)
					{
						msg.Body += string.Format(@"<tr>"
								+ @"<td><font size=2>{0}</font></td>"
								+ @"<td><font size=2>{1}</font></td>"
								+ @"<td><font size=2>{2}</font></td>"
								+ @"<td><font size=2>{3}</font></td>"
								+ @"<td><font size=2>{4}</font></td>"
								+ @"</tr>"
								, user.f顧客No
								, user.f顧客名
								, user.fcm名称
								, user.摘要名(ym)
								, (user.f終了フラグ) ? "1" : "0");
					}
					msg.Body += @"</table>";
				}
				else
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>保守サービス</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用年月</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>終了</font></th>"
								+ @"</tr>"
								+ @"</table>";
					msg.Body += @"<br><p>アルメックス保守サービス更新対象医院はありませんでした。</p>";
				}
				msg.Body += @"</div>"
							+ @"<div>"
							+ @"<p>以上、よろしくお願いいたします。<br>営業管理部</p>"
							+ @"</div>"
							+ @"</font>"
							+ @"</body>"
							+ @"</html>";

				// メール送信
				SendMailControl.SendMail(msg);
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
