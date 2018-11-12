using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using MwsLib.Common;
using System.Diagnostics;
using MwsLib.BaseFactory.PcSupportManager;

namespace PcSupportManager.Mail
{
	public static class SendMailControl
	{
		public static void SendStartMail(ref PcSupportMail mail)
		{
			// Configファイルの読み込み
			var conf = ReadConfig();
			var enc = Encoding.GetEncoding("shift_jis");

			using (var smtp = new SmtpClient())
			using (var msg = new MailMessage())
			{
				msg.SubjectEncoding = enc;
				msg.BodyEncoding = enc;

				// 件名
				msg.Subject = conf["subject_start"];

				// 本文
				using (var sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), conf["body_start"]), enc))
				{
					// パラメータ（%～%）の置換
					msg.Body = sr.ReadToEnd().Replace("%CLINIC_NAME%", mail.CustomerNo.ToString()).Replace("%AGREE_YEAR%", mail.AgreeYear.ToString()).Replace("%START_YM%", mail.StartDate.Value.ToYearMonth().ToString()).Replace("%END_YM%", mail.EndDate.Value.ToYearMonth().ToString());
				}
				// 差出人（From）
				msg.From = new MailAddress(conf["from"]);

				// 宛先（To）を複数登録する
				msg.To.Add(new MailAddress("suguro@mic.jp"));
				//msg.To.Add(new MailAddress(mail.MailAddress));
				//msg.To.Add(new MailAddress("eigyo_kanri@mic.jp"));

				//foreach (var to in conf["to"].Split(';'))
				//{
				//	msg.To.Add(new MailAddress(to));
				//}
				// SMTPサーバの設定
				smtp.Host = conf["smtp"];
				smtp.Port = Convert.ToInt32(conf["port"]);

				// SMTP認証
				if (!String.IsNullOrEmpty(conf["user"])	&& !String.IsNullOrEmpty(conf["pass"]))
				{
					smtp.Credentials = new NetworkCredential(conf["user"], conf["pass"]);
				}
				// メール送信
				smtp.Send(msg);

				// PC安心サポート送信メール情報の設定
				mail.SendDateTime = DateTime.Now;
			}
		}

		private static Dictionary<string, string> ReadConfig()
		{
			var configs = new Dictionary<string, string>();
			ConfigurationManager.OpenExeConfiguration(@Process.GetCurrentProcess().MainModule.FileName);
			foreach (var key in ConfigurationManager.AppSettings.AllKeys)
			{
				configs[key] = ConfigurationManager.AppSettings[key];
			}
			return configs;
		}
	}
}
