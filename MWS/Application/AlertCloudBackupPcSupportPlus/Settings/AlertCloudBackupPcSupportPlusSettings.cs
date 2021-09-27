//
// AlertCloudBackupPcSupportPlusSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/02/05 勝呂)
//
using MwsLib.Settings.Mail;
using MwsLib.Settings.SqlServer;
using System;

namespace AlertCloudBackupPcSupportPlus.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class AlertCloudBackupPcSupportPlusSettings : ICloneable, IEquatable<AlertCloudBackupPcSupportPlusSettings>
	{
		/// <summary>
		/// メール設定
		/// </summary>
		public MailSettings Mail { get; set; }

		/// <summary>
		/// SQL Server接続情報
		/// </summary>
		public SqlServerConnectSettings Connect { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public AlertCloudBackupPcSupportPlusSettings()
        {
			Mail = new MailSettings();
			Connect = new SqlServerConnectSettings();
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
		public bool Equals(AlertCloudBackupPcSupportPlusSettings other)
		{
			if (other != null)
			{
				if (Mail.Equals(other.Mail)
					&& Connect.Equals(other.Connect))
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
			if (obj is AlertCloudBackupPcSupportPlusSettings)
			{
				return this.Equals((AlertCloudBackupPcSupportPlusSettings)obj);
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
