//
// SendMailControl.cs
//
// メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/03/10 勝呂)
// Ver1.03 メール件名に顧客Noを追加(2022/04/05 勝呂)
// Ver1.03 メール本文に保存先を追加(2022/04/05 勝呂)
// Ver1.05 メール本文にファイル名記載例を追加(2022/05/12 勝呂)
//
using CommonLib.Common;
using MwsLib.Settings.Mail;
using NoticeOnlineLicenseConfirm.BaseFactory;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace NoticeOnlineLicenseConfirm.Mail
{
	/// <summary>
	/// メール送信クラス
	/// </summary>
	public static class SendMailControl
	{
		/// <summary>
		/// 通知１-NTT東日本
		/// </summary>
		/// <param name="mail">メール送信情報</param>
		/// <param name="east">NTT東日本進捗管理表</param>
		/// <param name="testMail">テストメール</param>
		public static void Notice1East(MailSettings mail, 進捗管理表_NTT東日本 east, bool testMail)
		{
			using (MailMessage msg = new MailMessage())
			{
				Encoding enc = Encoding.GetEncoding("shift_jis");
				msg.SubjectEncoding = enc;
				msg.BodyEncoding = enc;
				msg.IsBodyHtml = true;
				msg.Body = @"<html>"
							+ @"<head><meta http-equiv=Content-Type content=""text/html; charset=utf-8""></head>"
							+ @"<body>"
							+ @"<font face=""MS UI Gothic"" size=""3"">";

				// 件名
				// Ver1.03 メール件名に顧客Noを追加(2022/04/05 勝呂)
				msg.Subject = string.Format("【オンライン資格確認通知】 NTT東日本より工事確定日の連絡(顧客No:{0})", east.病院ID);

				// 本文
				// Ver1.03 メール本文に保存先を追加(2022/04/05 勝呂)
				msg.Body += string.Format(@"<div>"
							+ @"<p>ご担当者様</p>"
							+ @"<p>NTT東日本より工事確定日の連絡が来ましたので通知します。<br>"
							+ @"既に確定していた場合は、日程変更となります。<br>"
							+ @"詳しくは進捗管理表をご確認ください。<br>"
							+ @"<br>"
							+ @"【連絡内容】<br>"
							+ @"顧客No：{0}<br>"
							+ @"医院名：{1}<br>"
							+ @"受付通番：{2}<br>"
							+ @"工事確定日：{3}<br>"
							+ @"保存先：\\wwsv\ons-pics\{0}<br></ p>"
							+ @"</div>"
							, east.病院ID
							, east.医療機関名
							, east.受付通番
							, east.工事確定日付.Value.GetNormalString());
				msg.Body += @"</div>"
							+ @"<div>"
							+ @"<p>以上、よろしくお願いいたします。<br><br>営業管理部</p>"
							+ @"</div>"
							+ @"</font>"
							+ @"</body>"
							+ @"</html>";

				// メール送信
				SendMailControl.SendMail(mail, msg, east.Notice.MailAddress, testMail);
			}
		}

		/// <summary>
		/// 通知１-NTT西日本
		/// </summary>
		/// <param name="mail">メール送信情報</param>
		/// <param name="west">NTT西日本進捗管理表</param>
		/// <param name="testMail">テストメール</param>
		public static void Notice1West(MailSettings mail, 進捗管理表_NTT西日本 west, bool testMail)
		{
			using (MailMessage msg = new MailMessage())
			{
				Encoding enc = Encoding.GetEncoding("shift_jis");
				msg.SubjectEncoding = enc;
				msg.BodyEncoding = enc;
				msg.IsBodyHtml = true;
				msg.Body = @"<html>"
							+ @"<head><meta http-equiv=Content-Type content=""text/html; charset=utf-8""></head>"
							+ @"<body>"
							+ @"<font face=""MS UI Gothic"" size=""3"">";

				// 件名
				// Ver1.03 メール件名に顧客Noを追加(2022/04/05 勝呂)
				msg.Subject = string.Format("【オンライン資格確認通知】 NTT西日本より工事確定日の連絡(顧客No:{0})", west.病院ID);

				// 本文
				// Ver1.03 メール本文に保存先を追加(2022/04/05 勝呂)
				msg.Body += string.Format(@"<div>"
							+ @"<p>ご担当者様</p>"
							+ @"<p>NTT西日本より工事確定日の連絡が来ましたので通知します。<br>"
							+ @"既に確定していた場合は、日程変更となります。<br>"
							+ @"詳しくは進捗管理表をご確認ください。<br>"
							+ @"<br>"
							+ @"【連絡内容】<br>"
							+ @"顧客No：{0}<br>"
							+ @"医院名：{1}<br>"
							+ @"受付通番：{2}<br>"
							+ @"工事確定日：{3}<br>"
							+ @"保存先：\\wwsv\ons-pics\{0}<br></ p>"
							+ @"</div>"
							, west.病院ID
							, west.医療機関名
							, west.受付通番
							, west.工事確定日付.Value.GetNormalString());
				msg.Body += @"</div>"
							+ @"<div>"
							+ @"<p>以上、よろしくお願いいたします。<br><br>営業管理部</p>"
							+ @"</div>"
							+ @"</font>"
							+ @"</body>"
							+ @"</html>";

				// メール送信
				SendMailControl.SendMail(mail, msg, west.Notice.MailAddress, testMail);
			}
		}

		/// <summary>
		/// 通知３-NTT東日本
		/// </summary>
		/// <param name="mail">メール送信情報</param>
		/// <param name="east">NTT東日本進捗管理表</param>
		/// <param name="testMail">テストメール</param>
		public static void Notice3(MailSettings mail, 進捗管理表_NTT東日本 east, bool testMail)
		{
			using (MailMessage msg = new MailMessage())
			{
				Encoding enc = Encoding.GetEncoding("shift_jis");
				msg.SubjectEncoding = enc;
				msg.BodyEncoding = enc;
				msg.IsBodyHtml = true;
				msg.Body = @"<html>"
							+ @"<head><meta http-equiv=Content-Type content=""text/html; charset=utf-8""></head>"
							+ @"<body>"
							+ @"<font face=""MS UI Gothic"" size=""3"">";

				// 件名
				// Ver1.03 メール件名に顧客Noを追加(2022/04/05 勝呂)
				msg.Subject = string.Format("【オンライン資格確認通知】 NTT東日本よりヒアリングシート不備の連絡(顧客No:{0})", east.病院ID);

				// 本文
				// Ver1.03 メール本文に保存先を追加(2022/04/05 勝呂)
				// Ver1.05 メール本文にファイル名記載例を追加(2022/05/12 勝呂)
				msg.Body += string.Format(@"<div>"
							+ @"<p>ご担当者様</p>"
							+ @"<p>NTT東日本よりヒアリングシートの不備の連絡が来ましたので通知します。<br>"
							+ @"【修正箇所１と修正箇所2の最新日付の内容】をご確認ください。<br>"
							+ @"<br>"
							+ @"【連絡内容】<br>"
							+ @"顧客No：{0}<br>"
							+ @"医院名：{1}<br>"
							+ @"受付通番：{2}<br>"
							+ @"本日の更新分：{3}<br>"
							+ @"回答結果１：{4}<br>"
							+ @"修正箇所１：{5}<br>"
							+ @"回答結果２：{6}<br>"
							+ @"修正箇所２：{7}<br>"
							+ @"保存先：\\wwsv\ons-pics\{0}<br></ p>"
							+ @"</div>"
							, east.病院ID
							, east.医療機関名
							, east.受付通番
							, east.本日の更新分日付.Value.GetNormalString()
							, east.回答結果1
							, east.修正箇所1
							, east.回答結果2
							, east.修正箇所2);
				msg.Body += @"</div>"
							+ @"<div>"
							+ @"<p>最新のヒアリングシートにて修正後、こちらのメールに添付し返信してください。<br>"
							+ @"<br>"
							+ @"ファイル名は以下の形式でお願いいたします。<br>"
							+ @"「受付通番_ヒアリングシート_医院名_バージョン_提出日.xlsx」<br>"
							+ @"「例：東0123_ヒアリングシート_サンプル歯科医院_r26_20220501.xlsx」<br>"
							+ @"<br>"
							+ @"以上、よろしくお願いいたします。<br><br>営業管理部</p>"
							+ @"</div>"
							+ @"</font>"
							+ @"</body>"
							+ @"</html>";

				// メール送信
				SendMailControl.SendMail(mail, msg, east.Notice.MailAddress, testMail);
			}
		}

		/// <summary>
		/// 通知４-NTT西日本
		/// </summary>
		/// <param name="mail">メール送信情報</param>
		/// <param name="west">NTT西日本進捗管理表</param>
		/// <param name="testMail">テストメール</param>
		public static void Notice4(MailSettings mail, 進捗管理表_NTT西日本 west, bool testMail)
		{
			using (MailMessage msg = new MailMessage())
			{
				Encoding enc = Encoding.GetEncoding("shift_jis");
				msg.SubjectEncoding = enc;
				msg.BodyEncoding = enc;
				msg.IsBodyHtml = true;
				msg.Body = @"<html>"
							+ @"<head><meta http-equiv=Content-Type content=""text/html; charset=utf-8""></head>"
							+ @"<body>"
							+ @"<font face=""MS UI Gothic"" size=""3"">";

				// 件名
				// Ver1.03 メール件名に顧客Noを追加(2022/04/05 勝呂)
				msg.Subject = string.Format("【オンライン資格確認通知】 NTT西日本よりヒアリングシート不備の連絡(顧客No:{0})", west.病院ID);

				// 本文
				// Ver1.03 メール本文に保存先を追加(2022/04/05 勝呂)
				// Ver1.05 メール本文にファイル名記載例を追加(2022/05/12 勝呂)
				msg.Body += string.Format(@"<div>"
							+ @"<p>ご担当者様</p>"
							+ @"<p>NTT西日本よりヒアリングシートの不備の連絡が来ましたので通知します。<br>"
							+ @"ヒアリングシートをご確認ください。<br>"
							+ @"<br>"
							+ @"【連絡内容】<br>"
							+ @"顧客No：{0}<br>"
							+ @"医院名：{1}<br>"
							+ @"受付通番：{2}<br>"
							+ @"ヒアリングシート修正依頼日：{3}<br>"
							+ @"連絡票-連絡項目：{4}<br>"
							+ @"連絡票-連絡内容：{5}<br>"
							+ @"保存先：\\wwsv\ons-pics\{0}<br></ p>"
							+ @"</div>"
							, west.病院ID
							, west.医療機関名
							, west.受付通番
							, west.ヒアリングシート修正依頼日付.Value.GetNormalString()
							, west.連絡項目
							, west.連絡内容);
				msg.Body += @"</div>"
							+ @"<div>"
							+ @"<p>最新のヒアリングシートにて修正後、こちらのメールに添付し返信してください。<br>"
							+ @"<br>"
							+ @"ファイル名は以下の形式でお願いいたします。<br>"
							+ @"「受付通番_ヒアリングシート_医院名_バージョン_提出日.xlsx」<br>"
							+ @"「例：西0123_ヒアリングシート_サンプル歯科医院_r8_20220501.xlsx」<br>"
							+ @"<br>"
							+ @"以上、よろしくお願いいたします。<br><br>営業管理部</p>"
							+ @"</div>"
							+ @"</font>"
							+ @"</body>"
							+ @"</html>";

				// メール送信
				SendMailControl.SendMail(mail, msg, west.Notice.MailAddress, testMail);
			}
		}

		/// <summary>
		/// 通知５-NTT東日本
		/// </summary>
		/// <param name="mail">メール送信情報</param>
		/// <param name="east">NTT東日本進捗管理表</param>
		/// <param name="testMail">テストメール</param>
		public static void Notice5East(MailSettings mail, 進捗管理表_NTT東日本 east, bool testMail)
		{
			using (MailMessage msg = new MailMessage())
			{
				Encoding enc = Encoding.GetEncoding("shift_jis");
				msg.SubjectEncoding = enc;
				msg.BodyEncoding = enc;
				msg.IsBodyHtml = true;
				msg.Body = @"<html>"
							+ @"<head><meta http-equiv=Content-Type content=""text/html; charset=utf-8""></head>"
							+ @"<body>"
							+ @"<font face=""MS UI Gothic"" size=""3"">";

				// 件名
				// Ver1.03 メール件名に顧客Noを追加(2022/04/05 勝呂)
				msg.Subject = string.Format("【オンライン資格確認通知】 工事確定日14日前ヒアリングシート未完成通知（NTT東日本）(顧客No:{0})", east.病院ID);

				// 本文
				// Ver1.03 メール本文に保存先を追加(2022/04/05 勝呂)
				// Ver1.05 メール本文にファイル名記載例を追加(2022/05/12 勝呂)
				msg.Body += string.Format(@"<div>"
							+ @"<p>ご担当者様</p>"
							+ @"<p>工事確定日の14日前になりましたが、未修正箇所があります。<br>"
							+ @"下記内容をご確認ください。<br>"
							+ @"<br>"
							+ @"【連絡内容】<br>"
							+ @"顧客No：{0}<br>"
							+ @"医院名：{1}<br>"
							+ @"工事確定日：{2}<br>"
							+ @"回答結果１：{3}<br>"
							+ @"修正箇所１：{4}<br>"
							+ @"回答結果２：{5}<br>"
							+ @"修正箇所２：{6}<br>"
							+ @"保存先：\\wwsv\ons-pics\{0}<br></ p>"
							+ @"</div>"
							, east.病院ID
							, east.医療機関名
							, east.工事確定日付.Value.GetNormalString()
							, east.回答結果1
							, east.修正箇所1
							, east.回答結果2
							, east.修正箇所2);
				msg.Body += @"</div>"
							+ @"<div>"
							+ @"<p>最新のヒアリングシートにて修正後、こちらのメールに添付し返信してください。<br>"
							+ @"<br>"
							+ @"ファイル名は以下の形式でお願いいたします。<br>"
							+ @"「受付通番_ヒアリングシート_医院名_バージョン_提出日.xlsx」<br>"
							+ @"「例：東0123_ヒアリングシート_サンプル歯科医院_r26_20220501.xlsx」<br>"
							+ @"<br>"
							+ @"以上、よろしくお願いいたします。<br><br>営業管理部</p>"
							+ @"</div>"
							+ @"</font>"
							+ @"</body>"
							+ @"</html>";

				// メール送信
				SendMailControl.SendMail(mail, msg, east.Notice.MailAddress, testMail);
			}
		}

		/// <summary>
		/// 通知５-NTT西日本
		/// </summary>
		/// <param name="mail">メール送信情報</param>
		/// <param name="west">NTT西日本進捗管理表</param>
		/// <param name="testMail">テストメール</param>
		public static void Notice5West(MailSettings mail, 進捗管理表_NTT西日本 west, bool testMail)
		{
			using (MailMessage msg = new MailMessage())
			{
				Encoding enc = Encoding.GetEncoding("shift_jis");
				msg.SubjectEncoding = enc;
				msg.BodyEncoding = enc;
				msg.IsBodyHtml = true;
				msg.Body = @"<html>"
							+ @"<head><meta http-equiv=Content-Type content=""text/html; charset=utf-8""></head>"
							+ @"<body>"
							+ @"<font face=""MS UI Gothic"" size=""3"">";

				// 件名
				// Ver1.03 メール件名に顧客Noを追加(2022/04/05 勝呂)
				msg.Subject = string.Format("【オンライン資格確認通知】 工事確定日14日前ヒアリングシート未完成通知（NTT西日本）(顧客No:{0})", west.病院ID);

				// 本文
				// Ver1.03 メール本文に保存先を追加(2022/04/05 勝呂)
				// Ver1.05 メール本文にファイル名記載例を追加(2022/05/12 勝呂)
				msg.Body += string.Format(@"<div>"
							+ @"<p>ご担当者様</p>"
							+ @"<p>工事確定日の14日前になりましたが、未修正箇所があります。<br>"
							+ @"下記内容をご確認ください。<br>"
							+ @"<br>"
							+ @"【連絡内容】<br>"
							+ @"顧客No：{0}<br>"
							+ @"医院名：{1}<br>"
							+ @"工事確定日：{2}<br>"
							+ @"ヒアリングシート修正依頼日：{3}<br>"
							+ @"連絡票-連絡項目：{4}<br>"
							+ @"連絡票-連絡内容：{5}<br>"
							+ @"保存先：\\wwsv\ons-pics\{0}<br></ p>"
							+ @"</div>"
							, west.病院ID
							, west.医療機関名
							, west.工事確定日付.Value.GetNormalString()
							, west.ヒアリングシート修正依頼日付.Value.GetNormalString()
							, west.連絡項目
							, west.連絡内容);
				msg.Body += @"</div>"
							+ @"<div>"
							+ @"<p>最新のヒアリングシートにて修正後、こちらのメールに添付し返信してください。<br>"
							+ @"<br>"
							+ @"ファイル名は以下の形式でお願いいたします。<br>"
							+ @"「受付通番_ヒアリングシート_医院名_バージョン_提出日.xlsx」<br>"
							+ @"「例：西0123_ヒアリングシート_サンプル歯科医院_r8_20220501.xlsx」<br>"
							+ @"<br>"
							+ @"以上、よろしくお願いいたします。<br><br>営業管理部</p>"
							+ @"</div>"
							+ @"</font>"
							+ @"</body>"
							+ @"</html>";

				// メール送信
				SendMailControl.SendMail(mail, msg, west.Notice.MailAddress, testMail);
			}
		}

		/// <summary>
		/// メール送信
		/// </summary>
		/// <param name="mail">メール送信情報</param>
		/// <param name="msg"></param>
		/// <param name="to">宛先</param>
		/// <param name="testMail">テストメール</param>
		private static void SendMail(MailSettings mail, MailMessage msg, string to, bool testMail)
		{
			// 差出人（From）
			msg.From = new MailAddress(mail.From);           // eigyo_kanri@mic.jp

			if (testMail)
			{
				// テストメールの送信
				msg.To.Add(new MailAddress(mail.TestTo));      // suguro@mic.jp
				if (0 < mail.TestCC.Length)
				{
					msg.CC.Add(new MailAddress(mail.TestCC));      // suguro@mic.jp
				}
			}
			else
			{
				// 宛先（To）を登録する
				msg.To.Add(new MailAddress(to));            // 拠点担当者
				if (0 < mail.CC.Length)
				{
					// CCを登録する
					msg.CC.Add(new MailAddress(mail.CC));           // eigyo_kanri@mic.jp
				}
			}
			// SMTPサーバの設定
			using (SmtpClient smtp = new SmtpClient())
			{
				smtp.Host = mail.Smtp;
				smtp.Port = mail.Port;

				// SMTP認証
				if (!String.IsNullOrEmpty(mail.User) && !String.IsNullOrEmpty(mail.Pass))
				{
					smtp.Credentials = new NetworkCredential(mail.User, mail.Pass);
				}
				// メール送信
				smtp.Send(msg);
			}
		}
	}
}
