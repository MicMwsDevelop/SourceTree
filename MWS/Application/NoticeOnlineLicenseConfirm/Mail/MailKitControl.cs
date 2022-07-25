//
// MailKitControl.cs
//
// メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.07 通知１、通知５のメール本文に工事確定時間を追加。東日本SGからの要望(2022/07/21 勝呂)
// Ver1.07 メール文字化け対応のため、メール送信方式とSmtpClientからMailKitに変更(2022/07/22 勝呂)
//
using CommonLib.Common;
using MwsLib.Settings.Mail;
using NoticeOnlineLicenseConfirm.BaseFactory;
using System.Text;

namespace NoticeOnlineLicenseConfirm.Mail
{
	/// <summary>
	/// メール送信クラス
	/// </summary>
	public static class MailKitControl
	{
		/// <summary>
		/// 通知１-NTT東日本
		/// </summary>
		/// <param name="mail">メール送信情報</param>
		/// <param name="east">NTT東日本進捗管理表</param>
		/// <param name="testMail">テストメール</param>
		public static void Notice1East(MailSettings mail, 進捗管理表_NTT東日本 east, bool testMail)
		{
			// 件名
			// Ver1.03 メール件名に顧客Noを追加(2022/04/05 勝呂)
			string subject = string.Format("【オンライン資格確認通知】 NTT東日本より工事確定日の連絡(顧客No:{0})", east.病院ID);

			// 本文
			// Ver1.07 通知１、通知５のメール本文に工事確定時間を追加。東日本SGからの要望(2022/07/21 勝呂)
			string body = string.Format(@"<div>"
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
						+ @"工事確定時間：{4}<br>"
						+ @"保存先：\\wwsv\ons-pics\{0}<br></ p>"
						+ @"</div>"
						, east.病院ID
						, east.医療機関名
						, east.受付通番
						, east.工事確定日付.Value.GetNormalString()
						, east.工事確定時間);
			body += @"</div>"
						+ @"<div>"
						+ @"<p>以上、よろしくお願いいたします。<br><br>営業管理部</p>"
						+ @"</div>"
						+ @"</font>"
						+ @"</body>"
						+ @"</html>";

			// メール送信
			MailKitControl.SendMail(subject, body, mail, east.Notice.MailAddress, testMail);
		}

		/// <summary>
		/// 通知１-NTT西日本
		/// </summary>
		/// <param name="mail">メール送信情報</param>
		/// <param name="west">NTT西日本進捗管理表</param>
		/// <param name="testMail">テストメール</param>
		public static void Notice1West(MailSettings mail, 進捗管理表_NTT西日本 west, bool testMail)
		{
			// 件名
			// Ver1.03 メール件名に顧客Noを追加(2022/04/05 勝呂)
			string subject = string.Format("【オンライン資格確認通知】 NTT西日本より工事確定日の連絡(顧客No:{0})", west.病院ID);

			// 本文
			// Ver1.07 通知１、通知５のメール本文に工事確定時間を追加。東日本SGからの要望(2022/07/21 勝呂)
			string body = string.Format(@"<div>"
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
						+ @"工事確定時間：{4}<br>"
						+ @"保存先：\\wwsv\ons-pics\{0}<br></ p>"
						+ @"</div>"
						, west.病院ID
						, west.医療機関名
						, west.受付通番
						, west.工事確定日付.Value.GetNormalString()
						, west.工事確定時間);
			body += @"</div>"
						+ @"<div>"
						+ @"<p>以上、よろしくお願いいたします。<br><br>営業管理部</p>"
						+ @"</div>"
						+ @"</font>"
						+ @"</body>"
						+ @"</html>";

			// メール送信
			MailKitControl.SendMail(subject, body, mail, west.Notice.MailAddress, testMail);
		}

		/// <summary>
		/// 通知３-NTT東日本
		/// </summary>
		/// <param name="mail">メール送信情報</param>
		/// <param name="east">NTT東日本進捗管理表</param>
		/// <param name="testMail">テストメール</param>
		public static void Notice3(MailSettings mail, 進捗管理表_NTT東日本 east, bool testMail)
		{
			// 件名
			string subject = string.Format("【オンライン資格確認通知】 NTT東日本よりヒアリングシート不備の連絡(顧客No:{0})", east.病院ID);

			// 本文
			string body = string.Format(@"<div>"
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
			body += @"</div>"
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
			MailKitControl.SendMail(subject, body, mail, east.Notice.MailAddress, testMail);
		}

		/// <summary>
		/// 通知４-NTT西日本
		/// </summary>
		/// <param name="mail">メール送信情報</param>
		/// <param name="west">NTT西日本進捗管理表</param>
		/// <param name="testMail">テストメール</param>
		public static void Notice4(MailSettings mail, 進捗管理表_NTT西日本 west, bool testMail)
		{
			// 件名
			string subject = string.Format("【オンライン資格確認通知】 NTT西日本よりヒアリングシート不備の連絡(顧客No:{0})", west.病院ID);

			// 本文
			string body = string.Format(@"<div>"
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
			body += @"</div>"
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
			MailKitControl.SendMail(subject, body, mail, west.Notice.MailAddress, testMail);
		}

		/// <summary>
		/// 通知５-NTT東日本
		/// </summary>
		/// <param name="mail">メール送信情報</param>
		/// <param name="east">NTT東日本進捗管理表</param>
		/// <param name="testMail">テストメール</param>
		public static void Notice5East(MailSettings mail, 進捗管理表_NTT東日本 east, bool testMail)
		{
			// 件名
			string subject = string.Format("【オンライン資格確認通知】 工事確定日14日前ヒアリングシート未完成通知（NTT東日本）(顧客No:{0})", east.病院ID);

			// 本文
			// Ver1.07 通知１、通知５のメール本文に工事確定時間を追加。東日本SGからの要望(2022/07/21 勝呂)
			string body = string.Format(@"<div>"
						+ @"<p>ご担当者様</p>"
						+ @"<p>工事確定日の14日前になりましたが、未修正箇所があります。<br>"
						+ @"下記内容をご確認ください。<br>"
						+ @"<br>"
						+ @"【連絡内容】<br>"
						+ @"顧客No：{0}<br>"
						+ @"医院名：{1}<br>"
						+ @"工事確定日：{2}<br>"
						+ @"工事確定時間：{3}<br>"
						+ @"回答結果１：{4}<br>"
						+ @"修正箇所１：{5}<br>"
						+ @"回答結果２：{6}<br>"
						+ @"修正箇所２：{7}<br>"
						+ @"保存先：\\wwsv\ons-pics\{0}<br></ p>"
						+ @"</div>"
						, east.病院ID
						, east.医療機関名
						, east.工事確定日付.Value.GetNormalString()
						, east.工事確定時間
						, east.回答結果1
						, east.修正箇所1
						, east.回答結果2
						, east.修正箇所2);
			body += @"</div>"
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
			MailKitControl.SendMail(subject, body, mail, east.Notice.MailAddress, testMail);
		}

		/// <summary>
		/// 通知５-NTT西日本
		/// </summary>
		/// <param name="mail">メール送信情報</param>
		/// <param name="west">NTT西日本進捗管理表</param>
		/// <param name="testMail">テストメール</param>
		public static void Notice5West(MailSettings mail, 進捗管理表_NTT西日本 west, bool testMail)
		{
			// 件名
			string subject = string.Format("【オンライン資格確認通知】 工事確定日14日前ヒアリングシート未完成通知（NTT西日本）(顧客No:{0})", west.病院ID);

			// 本文
			// Ver1.07 通知１、通知５のメール本文に工事確定時間を追加。東日本SGからの要望(2022/07/21 勝呂)
			string body = string.Format(@"<div>"
						+ @"<p>ご担当者様</p>"
						+ @"<p>工事確定日の14日前になりましたが、未修正箇所があります。<br>"
						+ @"下記内容をご確認ください。<br>"
						+ @"<br>"
						+ @"【連絡内容】<br>"
						+ @"顧客No：{0}<br>"
						+ @"医院名：{1}<br>"
						+ @"工事確定日：{2}<br>"
						+ @"工事確定時間：{3}<br>"
						+ @"ヒアリングシート修正依頼日：{4}<br>"
						+ @"連絡票-連絡項目：{5}<br>"
						+ @"連絡票-連絡内容：{6}<br>"
						+ @"保存先：\\wwsv\ons-pics\{0}<br></ p>"
						+ @"</div>"
						, west.病院ID
						, west.医療機関名
						, west.工事確定日付.Value.GetNormalString()
						, west.工事確定時間
						, west.ヒアリングシート修正依頼日付.Value.GetNormalString()
						, west.連絡項目
						, west.連絡内容);
			body += @"</div>"
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
			MailKitControl.SendMail(subject, body, mail, west.Notice.MailAddress, testMail);
		}

		/// <summary>
		/// メール送信
		/// </summary>
		/// <param name="subject">件名</param>
		/// <param name="body">本文</param>
		/// <param name="mail">メール送信情報</param>
		/// <param name="to">宛先</param>
		/// <param name="testMail">テストメール</param>
		private static void SendMail(string subject, string body, MailSettings mail, string to, bool testMail)
		{
			// Ver1.07 メール文字化け対応のため、メール送信方式とSmtpClientからMailKitに変更(2022/07/22 勝呂)
			using (MimeKit.MimeMessage msg = new MimeKit.MimeMessage())
			{
				Encoding enc = Encoding.GetEncoding("iso-2022-jp");

				// 件名
				msg.Headers.Replace(MimeKit.HeaderId.Subject, enc, subject);

				// 本文
				MimeKit.TextPart textPart = new MimeKit.TextPart(MimeKit.Text.TextFormat.Html);
				textPart.SetText(enc, body);

				// "iso-2022-jp"で送るので、"Content-Transfer-Encoding"に"7bit"を指定
				textPart.ContentTransferEncoding = MimeKit.ContentEncoding.SevenBit;
				msg.Body = textPart;

				// 差出人（From）
				//msg.From.Add(new MimeKit.MailboxAddress("営業管理部", mail.From));   // eigyo_kanri@mic.jp
				msg.From.Add(MimeKit.MailboxAddress.Parse(mail.From));   // eigyo_kanri@mic.jp

				if (testMail)
				{
					// テストメールの送信
					//msg.To.Add(new MimeKit.MailboxAddress("勝呂 幹雄", mail.TestTo));   // suguro@mic.jp
					msg.To.Add(MimeKit.MailboxAddress.Parse(mail.TestTo));   // suguro@mic.jp
					if (0 < mail.TestCC.Length)
					{
						//msg.Cc.Add(new MimeKit.MailboxAddress("勝呂 幹雄", mail.TestCC));   // suguro@mic.jp
						msg.Cc.Add(MimeKit.MailboxAddress.Parse(mail.TestCC));   // suguro@mic.jp
					}
				}
				else
				{
					// 宛先（To）を登録する
					//msg.To.Add(new MimeKit.MailboxAddress("", to));     // 拠点担当者
					msg.To.Add(MimeKit.MailboxAddress.Parse(to));     // 拠点担当者
					if (0 < mail.CC.Length)
					{
						// CCを登録する
						//msg.Cc.Add(new MimeKit.MailboxAddress("営業管理部", mail.CC));   // eigyo_kanri@mic.jp
						msg.Cc.Add(MimeKit.MailboxAddress.Parse(mail.CC));   // eigyo_kanri@mic.jp
					}
				}
				// SMTPサーバの設定
				using (MailKit.Net.Smtp.SmtpClient smtp = new MailKit.Net.Smtp.SmtpClient())
				{
					smtp.Connect(mail.Smtp, mail.Port, MailKit.Security.SecureSocketOptions.None);
					if (!string.IsNullOrEmpty(mail.User) && !string.IsNullOrEmpty(mail.Pass))
					{
						// SMTPサーバ認証
						smtp.Authenticate(mail.User, mail.Pass);
					}
					// メール送信
					smtp.Send(msg);

					// 切断
					smtp.Disconnect(true);
				}
			}
		}
	}
}
