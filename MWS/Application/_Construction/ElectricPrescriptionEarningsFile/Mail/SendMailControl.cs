//
// SendMailControl.cs
//
// メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/07/01 勝呂):新規作成
//
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Charlie.View;
using CommonLib.BaseFactory.ElectricPrescription;
using MwsLib.Settings.Mail;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace ElectricPrescriptionEarningsFile.Mail
{
	/// <summary>
	/// メール送信クラス
	/// </summary>
	public static class SendMailControl
	{
		/// <summary>
		/// 電子処方箋管理サービス 売上連絡メール送信（経理課宛）
		/// </summary>
		/// <param name="userList">ユーザーリスト</param>
		/// <param name="formalFilename">出力ファイル名</param>
		public static void ExportEarningsFileSendMail(List<ElectricPrescriptionEarningsOut> userList, string formalFilename)
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
				msg.Subject = "電子処方箋管理サービス 売上連絡";

				// 本文
				msg.Body += string.Format(@"<div>"
							+ @"<p>経理課各位</p>"
							+ @"<p>電子処方箋管理サービスの売上データを作成しました。<br>"
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
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>申込日時</font></th>"
								+ @"</tr>";
					foreach (ElectricPrescriptionEarningsOut user in userList)
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
								, user.申込日時.Value.ToShortDateString());
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
								+ @"<th style=""BACKGROUND-COLOR: silver""><font size=2>申込日時</font></th>"
								+ @"</tr>"
								+ @"</table>";
					msg.Body += @"<br><p>電子処方箋管理サービスの売上データはありませんでした。</p>";
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
		/// 電子処方箋管理サービス 利用申込取消 連絡メール送信（システム管理部宛）
		/// </summary>
		/// <param name="applyList">申込情報リスト</param>
		/// <param name="svList">サービス情報リスト</param>
		public static void CancelUseApplyInfoSendMail(List<V_COUPLER_APPLY> applyList, List<M_SERVICE> svList)
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
				msg.Subject = "電子処方箋管理サービス 利用申込取消";

				// 本文
				msg.Body += @"<div><p>システム管理部各位</p><p>電子処方箋管理サービスの利用申込を取り消しました。<br></div>";

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
						M_SERVICE sv = svList.Find(p => p.SERVICE_ID == apply.service_id);
						string svName = string.Empty;
						if (null != sv)
						{
							svName = sv.SERVICE_NAME;
						}
						msg.Body += string.Format(@"<tr>"
								+ @"<td><font size=2>{0}</font></td>"
								+ @"<td><font size=2>{1}</font></td>"
								+ @"<td><font size=2>{2}</font></td>"
								+ @"<td><font size=2>{3}</font></td>"
								+ @"<td><font size=2>{4}</font></td>"
								+ @"</tr>"
								, apply.apply_id	// 0
								, apply.customer_id	// 1
								, apply.service_id	// 2
								, svName	// 3
								, apply.apply_date.Value.ToShortDateString());		// 4
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
					msg.Body += @"<br><p>電子処方箋管理サービスの利用申込はありませんでした。</p>";
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
