//
// SendMailControl.cs
//
// メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/12/01 勝呂):新規作成
// Ver1.05(2024/01/05 勝呂):メール送信先が複数指定された時にアプリケーションエラー
//
using CommonLib.BaseFactory.OnlineDemand;
using MwsLib.Settings.Mail;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace OnlineDemandEarningsFile.Mail
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
		public static void OnlineDemandSendMail(List<OnlineDemandEarningsOut> userList, string formalFilename)
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
				msg.Subject = "オンライン請求作業 売上連絡";

				// 本文
				msg.Body += string.Format(@"<div>"
							+ @"<p>経理課各位</p>"
							+ @"<p>オンライン請求作業の売上データを作成しました。<br>"
							+ @"<br>"
							+ @"{0}フォルダに{1}を格納しました。<br>"
							+ @"PCA読込作業をお願いします。<br></p>"
							+ @"</div>"
							, Program.gSettings.ExportDir
							, formalFilename);

				if (null != userList)
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>商品コード</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>商品名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>申請日時</font></th>"
								+ @"</tr>";
					foreach (OnlineDemandEarningsOut user in userList)
					{
						msg.Body += string.Format(@"<tr>"
								+ @"<td><font size=2>{0}</font></td>"
								+ @"<td><font size=2>{1}</font></td>"
								+ @"<td><font size=2>{2}</font></td>"
								+ @"<td><font size=2>{3}</font></td>"
								+ @"<td><font size=2>{4}</font></td>"
								+ @"</tr>"
								, user.顧客No
								, user.顧客名
								, user.商品コード
								, user.商品名
								, user.申請日時.Value.ToShortDateString());
					}
					msg.Body += @"</table>";
				}
				else
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>商品コード</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>商品名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>申請日時</font></th>"
								+ @"</tr>"
								+ @"</table>";
					msg.Body += @"<br><p>オンライン請求作業の売上データはありませんでした。</p>";
				}
				msg.Body += @"</div>"
							+ @"<div>"
							+ @"<p>以上、よろしくお願いいたします。<br>システム管理部</p>"
							+ @"</div>"
							+ @"</font>"
							+ @"</body>"
							+ @"</html>";

				// メール送信
				// Ver1.05(2024/01/05 勝呂):メール送信先が複数指定された時にアプリケーションエラー
				MailSettings.SendMail(msg, Program.gSettings.Mail);
			}
		}
	}
}
