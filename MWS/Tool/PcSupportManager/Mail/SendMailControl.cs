using MwsLib.BaseFactory.PcSupportManager;
using MwsLib.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace PcSupportManager.Mail
{
	/// <summary>
	/// メール送信クラス
	/// </summary>
	public static class SendMailControl
	{
		/// <summary>
		/// 開始メールの送信
		/// </summary>
		/// <param name="mail">PC安心サポート送信メール情報</param>
		/// <param name="clinicName">医院名</param>
		/// <param name="branchMailAddress">拠店メールアドレス</param>
		public static void SendStartMail(PcSupportMail mail, string clinicName, string branchMailAddress)
		{
			using (MailMessage msg = new MailMessage())
			{
				Encoding enc = Encoding.GetEncoding("shift_jis");
				msg.SubjectEncoding = enc;
				msg.BodyEncoding = enc;

				// App.configの読込み
				Dictionary<string, string> conf = SendMailControl.ReadConfig();

				// 件名
				msg.Subject = conf["subject_start"];

				// 本文
				using (var sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), conf["body_start"]), enc))
				{
					// パラメータ（%～%）の置換
					msg.Body = sr.ReadToEnd().Replace("%CLINIC_NAME%", clinicName).Replace("%AGREE_YEAR%", mail.AgreeYear.ToString()).Replace("%START_YM%", mail.StartDate.Value.ToYearMonth().ToString()).Replace("%END_YM%", mail.EndDate.Value.ToYearMonth().ToString());
				}
				// メール送信
				SendMailControl.SendMail(msg, mail.MailAddress, branchMailAddress, conf);
			}
		}

		/// <summary>
		/// 契約更新案内メールの送信
		/// </summary>
		/// <param name="mail">PC安心サポート送信メール情報</param>
		/// <param name="clinicName">医院名</param>
		/// <param name="branchMailAddress">拠店メールアドレス</param>
		public static void SendGuideMail(PcSupportMail mail, string clinicName, string branchMailAddress)
		{
			using (MailMessage msg = new MailMessage())
			{
				Encoding enc = Encoding.GetEncoding("shift_jis");
				msg.SubjectEncoding = enc;
				msg.BodyEncoding = enc;

				// App.configの読込み
				Dictionary<string, string> conf = SendMailControl.ReadConfig();

				// 件名
				msg.Subject = conf["subject_guide"];

				// 本文
				using (var sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), conf["body_guide"]), enc))
				{
					// パラメータ（%～%）の置換
					msg.Body = sr.ReadToEnd().Replace("%CLINIC_NAME%", clinicName).Replace("%AGREE_YEAR%", mail.AgreeYear.ToString()).Replace("%END_YM%", mail.EndDate.Value.ToYearMonth().ToString());
				}
				// メール送信
				SendMailControl.SendMail(msg, mail.MailAddress, branchMailAddress, conf);
			}
		}

		/// <summary>
		/// 契約更新メールの送信
		/// </summary>
		/// <param name="mail">PC安心サポート送信メール情報</param>
		/// <param name="clinicName">医院名</param>
		/// <param name="branchMailAddress">拠店メールアドレス</param>
		public static void SendUpdateMail(PcSupportMail mail, string clinicName, string branchMailAddress)
		{
			using (MailMessage msg = new MailMessage())
			{
				Encoding enc = Encoding.GetEncoding("shift_jis");
				msg.SubjectEncoding = enc;
				msg.BodyEncoding = enc;

				// App.configの読込み
				Dictionary<string, string> conf = SendMailControl.ReadConfig();

				// 件名
				msg.Subject = conf["subject_update"];

				// 本文
				using (var sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), conf["body_update"]), enc))
				{
					// パラメータ（%～%）の置換
					msg.Body = sr.ReadToEnd().Replace("%CLINIC_NAME%", clinicName).Replace("%AGREE_YEAR%", mail.AgreeYear.ToString()).Replace("%START_YM%", mail.StartDate.Value.ToYearMonth().ToString()).Replace("%END_YM%", mail.EndDate.Value.ToYearMonth().ToString());
				}
				// メール送信
				SendMailControl.SendMail(msg, mail.MailAddress, branchMailAddress, conf);
			}
		}

		/// <summary>
		/// App.configの読込み
		/// </summary>
		/// <returns>App.config</returns>
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

		/// <summary>
		/// メール送信
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="customerMailAddress">顧客メールアドレス</param>
		/// <param name="branchMailAddress">拠店メールアドレス</param>
		/// <param name="conf">App.config</param>
		private static void SendMail(MailMessage msg, string customerMailAddress, string branchMailAddress, Dictionary<string, string> conf)
		{
			if (Program.DebugMode)
			{
				// 差出人（From）
				msg.From = new MailAddress(conf["test_from"]);		// suguro@mic.jp

				// 宛先（To）を複数登録する
				msg.To.Add(new MailAddress(conf["test_to"]));       // suguro@mic.jp
			}
			else
			{
				// 差出人（From）
				msg.From = new MailAddress(conf["from"]);           // 営業管理部

				// 宛先（To）を複数登録する
				msg.To.Add(new MailAddress(customerMailAddress));	// 医院
				msg.To.Add(new MailAddress(branchMailAddress));		// 拠店
				msg.To.Add(new MailAddress(conf["to"]));			// 営業管理部
			}
			// SMTPサーバの設定
			using (SmtpClient smtp = new SmtpClient())
			{
				smtp.Host = conf["smtp"];
				smtp.Port = Convert.ToInt32(conf["port"]);

				// SMTP認証
				if (!String.IsNullOrEmpty(conf["user"]) && !String.IsNullOrEmpty(conf["pass"]))
				{
					smtp.Credentials = new NetworkCredential(conf["user"], conf["pass"]);
				}
				// メール送信
				smtp.Send(msg);
			}
		}
	}
}
