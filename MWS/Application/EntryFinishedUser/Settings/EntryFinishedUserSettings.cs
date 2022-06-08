//
// EntryFinishedUserSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.03 XMLファイルの変更(2022/06/08 勝呂)
//
using MwsLib.Settings.SqlServer;
using System;

namespace EntryFinishedUser.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class EntryFinishedUserSettings : ICloneable, IEquatable<EntryFinishedUserSettings>
	{
		/// <summary>
		/// はなはなし月次リスト格納フォルダ
		/// </summary>
		public string HanahashiUserListFolder { get; set; }

		/// <summary>
		/// Curlineクラウド利用料請求リスト格納フォルダ
		/// </summary>
		public string CurlineCloudListFolder { get; set; }

		/// <summary>
		/// SQL Server接続情報 JunpDB
		/// </summary>
		public SqlServerConnect ConnectJunp { get; set; }

		/// <summary>
		/// SQL Server接続情報 CharlieDB
		/// </summary>
		public SqlServerConnect ConnectCharlie { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public EntryFinishedUserSettings()
        {
			HanahashiUserListFolder = string.Empty;
			CurlineCloudListFolder = string.Empty;
			ConnectJunp = new SqlServerConnect();
			ConnectCharlie = new SqlServerConnect();
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
		public bool Equals(EntryFinishedUserSettings other)
		{
			if (other != null)
			{
				if (HanahashiUserListFolder == other.HanahashiUserListFolder
					&& CurlineCloudListFolder == other.CurlineCloudListFolder
					&& ConnectJunp.Equals(other.ConnectJunp)
					&& ConnectCharlie.Equals(other.ConnectCharlie))
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
			if (obj is EntryFinishedUserSettings)
			{
				return this.Equals((EntryFinishedUserSettings)obj);
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
