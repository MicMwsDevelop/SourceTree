//
// SendMailControl.cs
//
// メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/02/05 勝呂)
// Ver1.02(2023/09/14 勝呂):組織変更対応 営業管理部→システム管理部
//
using CommonLib.BaseFactory.AlertCloudBackupPcSupportPlus;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AlertCloudBackupPcSupportPlus.Mail
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
		/// <param name="formalFilename">出力ファイル名</param>
		public static void AlartSendMail(List<CloudBackupPcSupportPlus> list1, List<CloudBackupPcSupportPlus> list2, List<CloudBackupPcSupportPlus> list3)
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
				msg.Subject = @"ｸﾗｳﾄﾞﾊﾞｯｸｱｯﾌﾟPC安心ｻﾎﾟｰﾄPlus申込ｱﾗｰﾄ";

				// 本文
				msg.Body += @"<div>"
						+ @"<p>システム管理部 各位</p>"
						+ @"<p>クラウドバックアップとPC安心サポートPlusが申し込まれている可能性があるためご確認ください。</p>"
						+ @"<br>"
						+ @"</div>";

				msg.Body += @"1. PC安心サポートPlus契約期間中にクラウドバックアップサービスも契約している医院</p>"
						+ @"<table style="" BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
						+ @"<tr>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>顧客名</font></th>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>PC安心ｻﾎﾟｰﾄPlus利用期間</font></th>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>ｸﾗｳﾄﾞﾊﾞｯｸｱｯﾌﾟ利用期間</font></th>"
						+ @"</tr>";
				if (0 < list1.Count)
				{
					foreach (CloudBackupPcSupportPlus user in list1)
					{
						msg.Body += string.Format(@"<tr>"
								+ @"<td><font size=2>{0}</font></td>"
								+ @"<td><font size=2>{1}</font></td>"
								+ @"<td><font size=2>{2}</font></td>"
								+ @"<td><font size=2>{3}</font></td>"
								+ @"</tr>"
								, user.CustomerNo
								, user.ClinicName
								, user.PcSupportPlusSpanString
								, user.CloudBackupSpanString);
					}
				}
				msg.Body += @"</table><br>";

				msg.Body += "2. PC安心サポートPlus契約期間中にMWSサイトでクラウドバックアップサービスの申込をした医院</p>"
						+@"<table style="" BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
						+ @"<tr>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>顧客名</font></th>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>PC安心ｻﾎﾟｰﾄPlus利用期間</font></th>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>ｸﾗｳﾄﾞﾊﾞｯｸｱｯﾌﾟ利用期間</font></th>"
						+ @"</tr>";
				if (0 < list2.Count)
				{
					foreach (CloudBackupPcSupportPlus user in list2)
					{
						msg.Body += string.Format(@"<tr>"
								+ @"<td><font size=2>{0}</font></td>"
								+ @"<td><font size=2>{1}</font></td>"
								+ @"<td><font size=2>{2}</font></td>"
								+ @"<td><font size=2>{3}</font></td>"
								+ @"</tr>"
								, user.CustomerNo
								, user.ClinicName
								, user.PcSupportPlusSpanString
								, user.CloudBackupSpanString);
					}
				}
				msg.Body += @"</table><br>";

				msg.Body += "3. クラウドバックアップサービス利用中にMWSサイトでPC安心サポートPlusの申込をした医院</p>"
						+ @"<table style="" BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
						+ @"<tr>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>顧客名</font></th>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>PC安心ｻﾎﾟｰﾄPlus利用期間</font></th>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>ｸﾗｳﾄﾞﾊﾞｯｸｱｯﾌﾟ利用期間</font></th>"
						+ @"</tr>";
				if (0 < list3.Count)
				{
					foreach (CloudBackupPcSupportPlus user in list3)
					{
						msg.Body += string.Format(@"<tr>"
								+ @"<td><font size=2>{0}</font></td>"
								+ @"<td><font size=2>{1}</font></td>"
								+ @"<td><font size=2>{2}</font></td>"
								+ @"<td><font size=2>{3}</font></td>"
								+ @"</tr>"
								, user.CustomerNo
								, user.ClinicName
								, user.PcSupportPlusSpanString
								, user.CloudBackupSpanString);
					}
				}
				msg.Body += @"</table>";

				msg.Body += @"</div>"
							+ @"<div>"
							+ @"<p>以上、よろしくお願いいたします。<br><br>システム管理部</p>"
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
