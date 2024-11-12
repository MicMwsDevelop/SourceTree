//
// SendMailControl.cs
//
// メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/08/27 勝呂):新規作成
//
using CommonLib.BaseFactory.Charlie.View;
using CommonLib.BaseFactory.OnlineElectricPrescript;
using CommonLib.Common;
using MwsLib.Settings.Mail;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace OnlineElectricPrescriptEarningsFile.Mail
{
	/// <summary>
	/// メール送信クラス
	/// </summary>
	public static class SendMailControl
	{
		/// <summary>
		/// オン歯電子処方箋連携費 売上連絡メール送信（経理課宛）
		/// </summary>
		/// <param name="saleList">オン資電子処方箋連携費売上情報リスト</param>
		/// <param name="formalFilename">出力ファイル名</param>
		public static void ExportEarningsFileSendMail(List<OnlineElectricPrescriptEarningsFileOut> saleList, string formalFilename)
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
				msg.Subject = "オン歯電子処方箋連携費 売上連絡";

				// 本文
				msg.Body += string.Format(@"<div>"
							+ @"<p>経理課各位</p>"
							+ @"<p>ｵﾝﾗｲﾝ資格確認電子処方箋連携費の売上データを作成しました。<br>"
							+ @"<br>"
							+ @"{0}フォルダに{1}を格納しました。<br>"
							+ @"PCA読込作業をお願いします。<br></p>"
							+ @"</div>"
							, Program.gSettings.ExportDir
							, formalFilename);

				if (null != saleList)
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>医院名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>商品コード</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>商品名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用期間</font></th>"
								+ @"</tr>";
					foreach (OnlineElectricPrescriptEarningsFileOut sale in saleList)
					{
						msg.Body += string.Format(@"<tr>"
								+ @"<td><font size=2>{0}</font></td>"
								+ @"<td><font size=2>{1}</font></td>"
								+ @"<td><font size=2>{2}</font></td>"
								+ @"<td><font size=2>{3}</font></td>"
								+ @"<td><font size=2>{4}</font></td>"
								+ @"</tr>"
								, sale.顧客No
								, sale.顧客名
								, sale.商品コード
								, sale.商品名
								, string.Format("{0}～{1}", sale.契約開始日.Value.ToDate().ToYearMonth().GetNormalString(), sale.契約終了日.Value.ToDate().ToYearMonth().GetNormalString()));
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
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>利用期間</font></th>"
								+ @"</tr>"
								+ @"</table>";
					msg.Body += @"<br><p>ｵﾝﾗｲﾝ資格確認電子処方箋連携費の売上データはありませんでした。</p>";
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
		/// [ｵﾝﾗｲﾝ資格確認電子処方箋連携] 利用申込更新 連絡メール送信（システム管理部宛）
		/// </summary>
		/// <param name="applyList">申込情報リスト</param>
		public static void UpdateCouplerApplySendMail(List<V_COUPLER_APPLY> applyList)
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
				msg.Subject = "[ｵﾝﾗｲﾝ資格確認電子処方箋連携] 利用申込更新";

				// 本文
				msg.Body += @"<div><p>システム管理部各位</p><p>ｵﾝﾗｲﾝ資格確認電子処方箋連携サービスの利用申込をシステム反映しました。<br></div>";

				if (null != applyList)
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>申込No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>サービスID</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>サービス名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>申込日時</font></th>"
								+ @"</tr>";
					foreach (V_COUPLER_APPLY apply in applyList)
					{
						msg.Body += string.Format(@"<tr>"
								+ @"<td><font size=2>{0}</font></td>"
								+ @"<td><font size=2>{1}</font></td>"
								+ @"<td><font size=2>{2}</font></td>"
								+ @"<td><font size=2>ｵﾝﾗｲﾝ資格確認電子処方箋連携</font></td>"
								+ @"<td><font size=2>{3}</font></td>"
								+ @"</tr>"
								, apply.apply_id	// 0
								, apply.customer_id	// 1
								, apply.service_id	// 2
								, apply.apply_date.Value.ToShortDateString());		// 3
					}
					msg.Body += @"</table>";
				}
				else
				{
					msg.Body += @"<table style=""BORDER-COLLAPSE: collapse"" bordercolor=""black"" border=1>"
								+ @"<tr>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>申込No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>顧客No</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>サービスID</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>サービス名</font></th>"
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>申込日時</font></th>"
								+ @"</tr>"
								+ @"</table>";
					msg.Body += @"<br><p>ｵﾝﾗｲﾝ資格確認電子処方箋連携の利用申込はありませんでした。</p>";
				}
				msg.Body += @"</div>"
							+ @"<div>"
							+ @"<p>以上、よろしくお願いいたします。<br>システム管理部</p>"
							+ @"</div>"
							+ @"</font>"
							+ @"</body>"
							+ @"</html>";

				// メール送信
				MailSettings settings = Program.gSettings.Mail.DeepCopy();
#if DEBUG
				settings.To = "suguro@mic.jp";
				settings.TestTo = "suguro@mic.jp";
#else
				settings.To = "sys_kanri@mic.jp";
				settings.TestTo = "sys_kanri@mic.jp";
#endif
				MailSettings.SendMail(msg, settings);
			}
		}
	}
}
