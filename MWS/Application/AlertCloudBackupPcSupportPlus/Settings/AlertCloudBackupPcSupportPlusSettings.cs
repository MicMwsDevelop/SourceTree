//
// AlertCloudBackupPcSupportPlusSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/02/05 勝呂)
// Ver1.01 アラートに引っかかったユーザーがしばらくの間、毎日、アラートさせるので、チェックから除外するユーザーを登録できるように改修(2021/10/03 勝呂)
//
using MwsLib.Settings.Mail;
using MwsLib.Settings.SqlServer;
using System;
using System.Collections.Generic;

namespace AlertCloudBackupPcSupportPlus.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class AlertCloudBackupPcSupportPlusSettings : ICloneable, IEquatable<AlertCloudBackupPcSupportPlusSettings>
	{
		/// <summary>
		/// チェック除外ユーザー
		/// </summary>
		// Ver1.01 アラートに引っかかったユーザーがしばらくの間、毎日、アラートさせるので、チェックから除外するユーザーを登録できるように改修(2021/10/03 勝呂)
		public string ExcludeUser { get; set; }

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
				if (ExcludeUser== other.ExcludeUser
					&& Mail.Equals(other.Mail)
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

		/// <summary>
		/// 除外ユーザーの取得
		/// </summary>
		/// <returns></returns>
		public List<string> GetExcludeUserList()
		{
			if (0 < ExcludeUser.Length)
			{
				string[] users = ExcludeUser.Split(',');
				List<string> ret = new List<string>();
				ret.AddRange(users);
				return ret;
			}
			return null;
		}
	}
}
