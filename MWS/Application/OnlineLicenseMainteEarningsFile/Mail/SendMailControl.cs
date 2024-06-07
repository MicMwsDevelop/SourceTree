//
// SendMailControl.cs
//
// メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/01/23 勝呂):新規作成
// Ver1.03(2024/02/05 勝呂):売上データの利用年月分の表記と年月が正しくない
//
using CommonLib.BaseFactory.OnlineLicenseMainte;
using CommonLib.Common;
using MwsLib.Settings.Mail;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace OnlineLicenseMainteEarningsFile.Mail
{
	/// <summary>
	/// メール送信クラス
	/// </summary>
	public static class SendMailControl
	{
		/// <summary>
		/// メール送信（経理課宛て）
		/// </summary>
		/// <param name="userList">ユーザーリスト</param>
		/// <param name="formalFilename">出力ファイル名</param>
		/// <param name="tekiyoDate">摘要利用年月日（ yyyy年MM月更新分）</param>
		public static void OnlineLicenseMainteSendMail(List<OnlineLicenseMainteEarningsOut> userList, string formalFilename, Date tekiyoDate)
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
				msg.Subject = "オン資格保守サービス自動更新 売上連絡";

				// 本文
				msg.Body += string.Format(@"<div>"
							+ @"<p>経理課各位</p>"
							+ @"<p>オン資格保守サービスの期間更新と売上データを作成しました。<br>"
							+ @"<br>"
							+ @"{0}フォルダに{1}を格納しました。<br>"
							+ @"PCA読込作業をお願いします。<br></p>"
							+ @"</div>"
							, Program.gSettings.ExportDir
							, formalFilename);

				YearMonth ym = tekiyoDate.ToYearMonth();
				if (null != userList)
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>保守サービス</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>更新月</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>終了</font></th>"
								+ @"</tr>";
					foreach (OnlineLicenseMainteEarningsOut user in userList)
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
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>更新月</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>終了</font></th>"
								+ @"</tr>"
								+ @"</table>";
					msg.Body += @"<br><p>オン資格保守サービス更新対象医院はありませんでした。</p>";
				}
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
