﻿//
// SendMailControl.cs
//
// メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2021/08/18):新規作成(勝呂)
//
using MwsLib.BaseFactory.Pca;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AlartFinishedUserSale.Mail
{
	/// <summary>
	/// メール送信クラス
	/// </summary>
	public static class SendMailControl
	{
		/// <summary>
		/// メール送信（営業管理部宛て）
		/// </summary>
		/// <param name="list">終了ユーザー課金情報リスト</param>
		public static void AlartSendMail(List<FinishedUserSale> list)
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
				msg.Subject = @"終了ユーザー課金アラート";

				// 本文
				msg.Body += @"<div>"
						+ @"<p>営業管理部 各位</p>"
						+ @"<p>先月終了ユーザーに課金が発生してます。課金データ作成の売上データをご確認ください。</p>"
						+ @"</div>";

				msg.Body += @"<table style="" BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
						+ @"<tr>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>顧客名</font></th>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>伝票No</font></th>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>商品コード</font></th>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>商品名</font></th>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>単価</font></th>"
						+ @"<th style="" BACKGROUND-COLOR: silver""><font size=2>数量</font></th>"
						+ @"</tr>";
				if (0 < list.Count)
				{
					foreach (FinishedUserSale result in list)
					{
						foreach (PCA売上明細汎用データ sale in result.SaleList)
						{
							msg.Body += string.Format(@"<tr>"
									+ @"<td><font size=2>{0}</font></td>"
									+ @"<td><font size=2>{1}</font></td>"
									+ @"<td><font size=2>{2}</font></td>"
									+ @"<td><font size=2>{3}</font></td>"
									+ @"<td><font size=2>{4}</font></td>"
									+ @"<td><font size=2>{5}</font></td>"
									+ @"<td><font size=2>{6}</font></td>"
									+ @"</tr>"
									, result.CustomerNo
									, result.CustomerName
									, sale.伝票No
									, sale.商品コード
									, sale.商品名
									, sale.単価
									, sale.数量);
						}
					}
				}
				msg.Body += @"</table><br>";
				msg.Body += @"</div>"
							+ @"<p>以上、よろしくお願いいたします。<br><br>営業管理部</p>"
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
			msg.From = new MailAddress(Program.gSettings.Mail.From);           // tasksv@mic.jp

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