﻿//
// SendMailControl.cs
//
// メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/11/24 勝呂)
// Ver1.06(2024/05/10 勝呂):メール送信先が複数指定された時にアプリケーションエラー
//
using CommonLib.BaseFactory.AlmexMainte;
using CommonLib.Common;
using MwsLib.Settings.Mail;
using System.Collections.Generic;
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
		/// <param name="useDate">利用日</param>
		public static void AlmexMainteSendMail(List<AlmexMainteEarningsOut> userList, string formalFilename, Date useDate)
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

				YearMonth ym = useDate.ToYearMonth();
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
							+ @"<p>以上、よろしくお願いいたします。<br>システム管理部</p>"
							+ @"</div>"
							+ @"</font>"
							+ @"</body>"
							+ @"</html>";

				// メール送信
				// Ver1.06(2024/05/10 勝呂):メール送信先が複数指定された時にアプリケーションエラー
				//SendMailControl.SendMail(msg);
				MailSettings.SendMail(msg, Program.gSettings.Mail);
			}
		}
	}
}
