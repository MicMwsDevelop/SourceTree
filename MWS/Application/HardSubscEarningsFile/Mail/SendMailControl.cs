//
// SendMailControl.cs
//
// メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// Ver1.00(2025/10/21 勝呂):新規作成
//
using CommonLib.BaseFactory.HardSubsc;
using CommonLib.Common;
using MwsLib.Settings.Mail;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace HardSubscEarningsFile.Mail
{
	/// <summary>
	/// メール送信クラス
	/// </summary>
	public static class SendMailControl
	{
		/// <summary>
		/// メール送信（業務課宛て）
		/// </summary>
		/// <param name="saleList">ハードサブスク売上情報リスト</param>
		/// <param name="formalFilename">出力ファイル名</param>
		/// <param name="useDate">利用日</param>
		public static void SendMail(List<HardSubscEarningsOut> saleList, string formalFilename, Date useDate)
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
				msg.Subject = "ハードサブスク自動更新 売上連絡";

				// 本文
				msg.Body += string.Format(@"<div>"
							+ @"<p>業務管理部 業務課各位</p>"
							+ @"<p>ハードサブスクの課金終了日の更新と売上データを作成しました。<br>"
							+ @"<br>"
							+ @"{0}フォルダに{1}を格納しました。<br>"
							+ @"PCA読込作業をお願いします。<br></p>"
							+ @"</div>"
							, Program.gSettings.ExportDir
							, formalFilename);

				YearMonth ym = useDate.ToYearMonth();
				if (null != saleList)
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>契約番号</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>契約期間</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用月</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>解約日</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>サービス終了</font></th>"
								+ @"</tr>";
					foreach (HardSubscEarningsOut sale in saleList)
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
								, sale.顧客No
								, sale.顧客名
								, sale.契約番号
								, sale.契約期間
								, sale.摘要名(ym)
								, (sale.解約日.HasValue) ? sale.解約日.Value.ToShortDateString() : ""
								, (sale.サービス終了フラグ) ? "終了" : "");
					}
					msg.Body += @"</table>";
				}
				else
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>契約番号</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>契約期間</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用月</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>解約日</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>サービス終了</font></th>"
								+ @"</tr>"
								+ @"</table>";
					msg.Body += @"<br><p>ハードサブスク更新対象医院はありませんでした。</p>";
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
	}
}
