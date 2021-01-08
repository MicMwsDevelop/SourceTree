//
// SendMailControl.cs
//
// メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/10/09 勝呂)
// 
using MwsLib.BaseFactory.CloudBackup;
using MwsLib.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CloudBackupPurchaseFile.Mail
{
	/// <summary>
	/// メール送信クラス
	/// </summary>
	public static class SendMailControl
	{
		/// <summary>
		/// メール送信（営業管理部宛て）
		/// </summary>
		/// <param name="stockList">ユーザーリスト</param>
		public static void SoftwareMainteSendMail(List<CloudBackupEarningsData> stockList)
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

				string yearMonthStr = Date.Today.ToYearMonth().GetJapaneseString(false, '0', true, true);

				// 件名
				msg.Subject = "クラウドバックアップ仕入データ作成連絡";

				// 本文
				msg.Body += string.Format(@"<div>"
							+ @"<p>経理部各位</p>"
							+ @"<p>クラウドバックアップの仕入データを作成しました。<br>"
							+ @"<br>"
							+ @"{0}フォルダに{1}を格納しました。<br>"
							+ @"PCA読込作業をお願いします。<br></p>"
							+ @"</div>"
							, Program.gSettings.ExportDir
							, Program.gSettings.ExportFilename);
				if (0 < stockList.Count)
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>売上日</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>部門</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>商品コード</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>商品名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>単価</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>数量</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>金額</font></th>"
								+ @"</tr>";
					foreach (CloudBackupEarningsData stock in stockList)
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
								, stock.f売上日
								, stock.f部門コード
								, stock.f仕入商品コード
								, stock.f商品名
								, stock.f仕入価格
								, stock.f数量
								, stock.金額
								);
					}
					msg.Body += @"</table>";
				}
				else
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>売上日</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>部門</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>商品コード</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>商品名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>単価</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>数量</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>金額</font></th>"
								+ @"</tr>";
					msg.Body += @"<br><p>クラウドバックアップ仕入データはありませんでした。</p>";
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
		/// <param name="conf">App.config</param>
		/// <param name="to">宛先</param>
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
			msg.To.Add(new MailAddress(Program.gSettings.Mail.To));			// keiri@mic.jp
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
