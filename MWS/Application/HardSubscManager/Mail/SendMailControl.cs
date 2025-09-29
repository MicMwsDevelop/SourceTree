//
// SendMailControl.cs
//
// メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// Ver1.00(2025/04/15 勝呂):新規作成
//
using CommonLib.Common;
using CommonLib.BaseFactory.HardSubsc;
using MwsLib.Settings.Mail;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using CommonLib.BaseFactory.Junp.Table;

namespace HardSubscManager.Mail
{
	/// <summary>
	/// メール送信クラス
	/// </summary>
	public static class SendMailControl
	{
		/// <summary>
		/// メール送信（業務課宛て）
		/// </summary>
		/// <param name="useNotifyDate">利用期限通知日</param>
		/// <param name="notifyList">ハードサブスク利用期限通知リスト</param>
		public static void SendMailToGyomu(Date useNotifyDate, List<HardSubscNotify> notifyList)
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

				string ymStr = string.Format("{0}年{1}月", useNotifyDate.Year, useNotifyDate.Month);

				// 件名
				msg.Subject = string.Format("{0}にハードサブスクの契約満了を迎える対象ユーザー連絡", ymStr);

				// 本文
				msg.Body += string.Format("<div>"
							+ @"<p>業務管理部 業務課各位</p>"
							+ @"<p>{0}にハードサブスクの契約満了を迎えるユーザーをご連絡します。<br>"
							+ @"<br>"
							+ @"<br>"
							+ @"<p>{0} 契約満了対象ユーザー<br>"
							+ @"</div>", ymStr);
				if (null != notifyList)
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>オフィス名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>ユーザー名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>電話番号</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>契約番号</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用期間</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>解約日</font></th>"
								+ @"</tr>";
					foreach (HardSubscNotify notify in notifyList)
					{
						msg.Body += string.Format(@"<tr>"
								+ @"<td><font size=2>{0}</font></td>"
								+ @"<td><font size=2>{1}</font></td>"
								+ @"<td><font size=2>{2}</font></td>"
								+ @"<td><font size=2>{3}</font></td>"
								+ @"<td><font size=2>{4}</font></td>"
								+ @"<td><font size=2>{5}</font></td>"
								+ @"</tr>"
								, notify.オフィス名
								, notify.顧客No
								, notify.顧客名
								, notify.電話番号
								, notify.契約番号
								, notify.利用期間
								, notify.解約日);
					}
					msg.Body += @"</table>";
				}
				else
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>オフィス名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>ユーザー名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>電話番号</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>契約番号</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用期間</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>解約日</font></th>"
								+ @"</tr>"
								+ @"</table>";
					msg.Body += string.Format(@"<br><p>{0}にハードサブスクの契約満了を迎えるユーザーはいませんでした。</p>", ymStr);
				}
				msg.Body += @"</div>"
							+ @"<div>"
							+ @"<p>以上、よろしくお願いいたします。<br>システム管理部</p>"
							+ @"</div>"
							+ @"</font>"
							+ @"</body>"
							+ @"</html>";

				// メール送信
				MailSettings.SendMail(msg, Program.gSettings.Mail);
			}
		}

		/// <summary>
		/// メール送信（各オフィス宛て）
		/// </summary>
		/// <param name="useNotifyDate">利用期限通知日</param>
		/// <param name="notifyList">ハードサブスク利用期限通知リスト</param>
		/// <param name="branch">支店情報</param>
		public static void SendMailToOffice(Date useNotifyDate, List<HardSubscNotify> notifyList, tMih支店情報 branch)
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

				string ymStr = string.Format("{0}年{1}月", useNotifyDate.Year, useNotifyDate.Month);

				// 件名
				msg.Subject = string.Format("【{0}】 {1}にハードサブスクの契約満了を迎える対象ユーザー連絡", branch.f支店名, ymStr);

				// 本文
				msg.Body += string.Format("<div>"
							+ @"<p>{0}</p>"
							+ @"<p>{1}にハードサブスクの契約満了を迎えるユーザーをご連絡します。<br>"
							+ @"<br>"
							+ @"<br>"
							+ @"<p>{1} 契約満了対象ユーザー<br>"
							+ @"</div>", branch.f支店名, ymStr);
				if (null != notifyList)
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>オフィス</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>ユーザー名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>電話番号</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>契約番号</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用期間</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>解約日</font></th>"
								+ @"</tr>";
					foreach (HardSubscNotify notify in notifyList)
					{
						msg.Body += string.Format(@"<tr>"
								+ @"<td><font size=2>{0}</font></td>"
								+ @"<td><font size=2>{1}</font></td>"
								+ @"<td><font size=2>{2}</font></td>"
								+ @"<td><font size=2>{3}</font></td>"
								+ @"<td><font size=2>{4}</font></td>"
								+ @"<td><font size=2>{5}</font></td>"
								+ @"</tr>"
								, notify.オフィス名
								, notify.顧客No
								, notify.顧客名
								, notify.電話番号
								, notify.契約番号
								, notify.利用期間
								, notify.解約日);
					}
					msg.Body += @"</table>";
				}
				else
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>オフィス</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>ユーザー名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>電話番号</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>契約番号</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用期間</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>解約日</font></th>"
								+ @"</tr>"
								+ @"</table>";
					msg.Body += string.Format(@"<br><p>{0}にハードサブスクの契約満了を迎えるユーザーはいませんでした。</p>", ymStr);
				}
				msg.Body += @"</div>"
							+ @"<div>"
							+ @"<p>以上、よろしくお願いいたします。<br>システム管理部</p>"
							+ @"</div>"
							+ @"</font>"
							+ @"</body>"
							+ @"</html>";

				// メール送信
				string from = "mainte_info_sys@mic.jp";

#if DEBUG
				string[] toArray = { "suguro@mic.jp" };
				string[] ccArray = { "suguro@mic.jp" };
#else
				string[] toArray = new string[1];
				toArray[0] = branch.fメールアドレス;
				string[] ccArray = { "gyomu@mic.jp", "mainte_info_sys@mic.jp" };
#endif
				MailSettings.SendMail(msg, Program.gSettings.Mail, from, toArray, ccArray);
			}
		}
	}
}
