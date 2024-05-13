//
// SendMailControl.cs
//
// メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.03(2024/04/18 勝呂):受注伝票サービス利用期間不具合検出機能の追加
//
using CommonLib.BaseFactory.CheckMwsServiceIllegalData;
using MwsLib.Settings.Mail;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace CheckMwsServiceIllegalData.Mail
{
	/// <summary>
	/// メール送信クラス
	/// </summary>
	public static class SendMailControl
	{
		/// <summary>
		/// 受注伝票サービス利用期間不具合検出 連絡メール送信（社内システム維持管理宛）
		/// </summary>
		/// <param name="cuiList">顧客利用情報リスト</param>
		public static void SendMail(List<CheckUseCustomerInfo> cuiList)
		{
			if (null != cuiList && 0 < cuiList.Count)
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
					msg.Subject = "受注伝票サービス利用期間の不具合の検出";

					// 本文
					msg.Body += @"<div><p>システム管理部各位</p><p>受注伝票サービス利用期間の不具合を検出しました。<br></div>";

					if (null != cuiList)
					{
						msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
									+ @"<tr>"
									+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
									+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客名</font></th>"
									+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>サービスID</font></th>"
									+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>サービス名</font></th>"
									+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用開始日</font></th>"
									+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>課金終了日</font></th>"
									+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>作成日時</font></th>"
									+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>更新日時</font></th>"
									+ @"</tr>";
						foreach (CheckUseCustomerInfo cui in cuiList)
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
									, cui.CustomerID    // 0
									, cui.CustomerName  // 1
									, cui.ServiceID  // 2
									, cui.ServiceName  // 3
									, (cui.UseStartDate.HasValue) ? cui.UseStartDate.Value.ToShortDateString() : ""  // 4
									, (cui.UseEndDate.HasValue) ? cui.UseEndDate.Value.ToShortDateString() : ""  // 5
									, (cui.CreateDate.HasValue) ? cui.CreateDate.Value.ToShortDateString() : ""  // 6
									, (cui.UpdateDate.HasValue) ? cui.UpdateDate.Value.ToShortDateString() : "");  // 7
						}
						msg.Body += @"</table>";
						msg.Body += @"</div>"
									+ @"<div>"
									+ @"<p>以上、よろしくお願いいたします。<br>システム管理部</p>"
									+ @"</div>"
									+ @"</font>"
									+ @"</body>"
									+ @"</html>";

						// メール送信
						MailExSettings.SendMail(msg, Program.gSettings.Mail);
					}
				}
			}
		}
	}
}
