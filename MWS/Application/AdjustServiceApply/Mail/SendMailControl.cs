//
// SendMailControl.cs
//
// メール送信クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// Ver1.10(2024/05/10 勝呂):メール送信先が複数指定された時にアプリケーションエラー
// 
using AdjustServiceApply.Log;
using MwsLib.Settings.Mail;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AdjustServiceApply.Mail
{
	/// <summary>
	/// メール送信クラス
	/// </summary>
	public static class SendMailControl
	{
		/// <summary>
		/// 申込情報更新 メール送信（システム管理部宛て）
		/// </summary>
		/// <param name="instanceName">インスタンス名</param>
		/// <param name="result">結果</param>
		/// <param name="logList">ログ出力</param>
		public static void AdjustServiceApplySendMail(string instanceName, bool result, MailLogOut logList)
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
				if (result)
				{
					msg.Subject = string.Format("【{0}】申込情報更新→正常終了", instanceName);
				}
				else
				{
					msg.Subject = string.Format("【{0}】申込情報更新→異常終了", instanceName);
				}
				// 本文
				foreach (string log in logList)
				{
					msg.Body += string.Format("{0}<br>", log);
				}
				// メール送信
				// Ver1.10(2024/05/10 勝呂):メール送信先が複数指定された時にアプリケーションエラー
				//SendMailControl.SendMail(msg);
				MailSettings.SendMail(msg, Program.gSettings.Mail);
			}
		}
	}
}
