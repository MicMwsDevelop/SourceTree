//
// MailSettings..cs
// 
// メール環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/10/27 勝呂)
//
using System;

namespace MwsLib.BaseFactory.Mail
{
	public class MailSettings : ICloneable, IEquatable<MailSettings>
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
		/// メール設定 送信先(Debug)
		/// </summary>
		public string TestTo { get; set; }

		/// <summary>
		/// メール設定 CC(Debug)
		/// </summary>
		public string TestCC { get; set; }

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
		public MailSettings()
		{
			From = string.Empty;
			To = string.Empty;
			CC = string.Empty;
			TestTo = string.Empty;
			TestCC = string.Empty;
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
		public MailSettings DeepCopy()
		{
			MailSettings ret = new MailSettings();
			ret.From = this.From;
			ret.To = this.To;
			ret.CC = this.CC;
			ret.TestTo = this.TestTo;
			ret.TestCC = this.TestCC;
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
		public bool Equals(MailSettings other)
		{
			if (other != null)
			{
				if (From == other.From
					&& To == other.To
					&& CC == other.CC
					&& TestTo == other.TestTo
					&& TestCC == other.TestCC
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
			if (obj is MailSettings)
			{
				return this.Equals((MailSettings)obj);
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
	}
}
