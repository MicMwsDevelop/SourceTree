//
// MailExSettings..cs
// 
// メール環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/01/30 勝呂):Bccの設定を追加
//
using System;
using System.Net;
using System.Net.Mail;

namespace MwsLib.Settings.Mail
{
	public class MailExSettings : ICloneable, IEquatable<MailExSettings>
	{
		/// <summary>
		/// メール設定 送信元
		/// </summary>
		public string From { get; set; }

		/// <summary>
		/// メール設定 送信先
		/// </summary>
		public string To { get; set; }

		/// <summary>
		/// メール設定 CC
		/// </summary>
		public string CC { get; set; }

		/// <summary>
		/// メール設定 Bcc
		/// </summary>
		public string Bcc { get; set; }

		/// <summary>
		/// メール設定 送信先(Debug)
		/// </summary>
		public string TestTo { get; set; }

		/// <summary>
		/// メール設定 CC(Debug)
		/// </summary>
		public string TestCC { get; set; }

		/// <summary>
		/// メール設定 Bcc(Debug)
		/// </summary>
		public string TestBcc { get; set; }

		/// <summary>
		/// メール設定 smtp
		/// </summary>
		public string Smtp { get; set; }

		/// <summary>
		/// メール設定 port
		/// </summary>
		public int Port { get; set; }

		/// <summary>
		/// メール設定 user
		/// </summary>
		public string User { get; set; }

		/// <summary>
		/// メール設定 pass
		/// </summary>
		public string Pass { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MailExSettings()
		{
			From = string.Empty;
			To = string.Empty;
			CC = string.Empty;
			Bcc = string.Empty;
			TestTo = string.Empty;
			TestCC = string.Empty;
			TestBcc = string.Empty;
			Smtp = string.Empty;
			Port = 0;
			User = string.Empty;
			Pass = string.Empty;
		}

		/// <summary>
		/// メンバーのクローンを作成する
		/// （ICloneableの実装）
		/// </summary>
		/// <returns>クローンオブジェクト</returns>
		public Object Clone()
		{
			return MemberwiseClone();
		}


		/// <summary>
		/// Deep Copy
		/// </summary>
		/// <returns>チェック項目グループ</returns>
		public MailExSettings DeepCopy()
		{
			MailExSettings ret = new MailExSettings();
			ret.From = this.From;
			ret.To = this.To;
			ret.CC = this.CC;
			ret.Bcc = this.Bcc;
			ret.TestTo = this.TestTo;
			ret.TestCC = this.TestCC;
			ret.TestBcc = this.TestBcc;
			ret.Smtp = this.Smtp;
			ret.Port = this.Port;
			ret.User = this.User;
			ret.Pass = this.Pass;
			return ret;
		}

		/// <summary>
		/// このインスタンスと、指定した環境設定クラスの値が同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するオブジェクト</param>
		/// <returns>判定</returns>
		public bool Equals(MailExSettings other)
		{
			if (other != null)
			{
				if (From == other.From
					&& To == other.To
					&& CC == other.CC
					&& Bcc == other.Bcc
					&& TestTo == other.TestTo
					&& TestCC == other.TestCC
					&& TestBcc == other.TestBcc
					&& Smtp == other.Smtp
					&& Port == other.Port
					&& User == other.User
					&& Pass == other.Pass)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is MailExSettings)
			{
				return this.Equals((MailExSettings)obj);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		/// <summary>
		/// ハッシュコードを返す
		/// </summary>
		/// <returns>ハッシュコード</returns>
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		/// <summary>
		/// メール送信
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="settings"></param>
		// Ver1.01(2024/01/05 勝呂):メール送信機能を追加
		public static void SendMail(MailMessage msg, MailExSettings settings)
		{
			// 差出人（From）
			msg.From = new MailAddress(settings.From);           // sys_kanri@mic.jp

#if DEBUG
			string[] toArray = settings.TestTo.Split(';');    // suguro@mic.jp
#else
			string[] toArray = settings.To.Split(';');		// keiri@mic.jp
#endif
			// 宛先（To）を登録する
			foreach (string to in toArray)
			{
				msg.To.Add(new MailAddress(to));
			}
#if DEBUG
			string ccStr = settings.TestCC;       // suguro@mic.jp
#else
			string ccStr = settings.CC;			// sys_kanri@mic.jp;jigyo_plan_dx@mic.jp
#endif
			if (0 < ccStr.Length)
			{
				// CCを登録する
				string[] ccArray = ccStr.Split(';');
				foreach (string cc in ccArray)
				{
					msg.CC.Add(new MailAddress(cc));
				}
			}
#if DEBUG
			string bccStr = settings.TestBcc;       // suguro@mic.jp
#else
			string bccStr = settings.Bcc;			// sys_kanri@mic.jp;jigyo_plan_dx@mic.jp
#endif
			if (0 < bccStr.Length)
			{
				// Bccを登録する
				string[] bccArray = bccStr.Split(';');
				foreach (string bcc in bccArray)
				{
					msg.Bcc.Add(new MailAddress(bcc));
				}
			}
			// SMTPサーバの設定
			using (SmtpClient smtp = new SmtpClient())
			{
				smtp.Host = settings.Smtp;
				smtp.Port = settings.Port;

				// SMTP認証
				if (!String.IsNullOrEmpty(settings.User) && !String.IsNullOrEmpty(settings.Pass))
				{
					smtp.Credentials = new NetworkCredential(settings.User, settings.Pass);
				}
				// メール送信
				smtp.Send(msg);
			}
		}
	}
}
