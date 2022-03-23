//
// NoticeOnlineLicenseConfirmSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/03/10 勝呂)
//
using MwsLib.Settings.Mail;
using MwsLib.Settings.SqlServer;
using System;

namespace NoticeOnlineLicenseConfirm.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class NoticeOnlineLicenseConfirmSettings : ICloneable, IEquatable<NoticeOnlineLicenseConfirmSettings>
	{
		/// <summary>
		/// メール設定
		/// </summary>
		public MailSettings Mail { get; set; }

		/// <summary>
		/// SQL Server接続情報 JunpDB
		/// </summary>
		public SqlServerConnect ConnectJunp { get; set; }

		/// <summary>
		/// SQL Server接続情報 SalesDB
		/// </summary>
		public SqlServerConnect ConnectSales { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public NoticeOnlineLicenseConfirmSettings()
        {
			Mail = new MailSettings();
			ConnectJunp = new SqlServerConnect();
			ConnectSales = new SqlServerConnect();
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
		/// このインスタンスと、指定した環境設定クラスの値が同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するオブジェクト</param>
		/// <returns>判定</returns>
		public bool Equals(NoticeOnlineLicenseConfirmSettings other)
		{
			if (other != null)
			{
				if (Mail.Equals(other.Mail)
					&& ConnectJunp.Equals(other.ConnectJunp)
					&& ConnectSales.Equals(other.ConnectSales))
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
			if (obj is NoticeOnlineLicenseConfirmSettings)
			{
				return this.Equals((NoticeOnlineLicenseConfirmSettings)obj);
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
