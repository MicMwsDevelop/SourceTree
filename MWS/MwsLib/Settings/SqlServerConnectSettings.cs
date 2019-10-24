//
// SqlServerAccessSettings.cs
// 
// SQL Server データベース接続情報環境設定
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/10/24 勝呂)
//
using System;

namespace MwsLib.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	[Serializable]
	public class SqlServerConnectSettings : ICloneable, IEquatable<SqlServerConnectSettings>
	{
		/// <summary>
		/// Coupler 本番環境接続情報
		/// </summary>
		public DatabaseConnect Coupler { get; set; }

		/// <summary>
		/// Coupler ＣＴ環境接続情報
		/// </summary>
		public DatabaseConnect CouplerCT { get; set; }

		/// <summary>
		/// Charlie 本番環境接続情報
		/// </summary>
		public DatabaseConnect Charlie { get; set; }

		/// <summary>
		/// Charlie ＣＴ環境接続情報
		/// </summary>
		public DatabaseConnect CharlieCT { get; set; }

		/// <summary>
		/// Junp 本番環境接続情報
		/// </summary>
		public DatabaseConnect Junp { get; set; }

		/// <summary>
		/// Junp ＣＴ環境接続情報
		/// </summary>
		public DatabaseConnect JunpCT { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SqlServerConnectSettings()
        {
			Coupler = new DatabaseConnect();
			CouplerCT = new DatabaseConnect();
			Charlie = new DatabaseConnect();
			CharlieCT = new DatabaseConnect();
			Junp = new DatabaseConnect();
			JunpCT = new DatabaseConnect();
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
		/// <param name="other">比較するSqlServerConnectSettings</param>
		/// <returns>判定</returns>
		public bool Equals(SqlServerConnectSettings other)
		{
			if (other != null)
			{
				if (Coupler.Equals(other.Coupler)
					&& CouplerCT.Equals(other.CouplerCT)
					&& Charlie.Equals(other.Charlie)
					&& CharlieCT.Equals(other.CharlieCT)
					&& Junp.Equals(other.Junp)
					&& JunpCT.Equals(other.JunpCT))
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
		/// <param name="obj">比較するSqlServerConnectSettingsオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is SqlServerConnectSettings)
			{
				return this.Equals((SqlServerConnectSettings)obj);
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
			return Coupler.GetHashCode() + CouplerCT.GetHashCode() + Charlie.GetHashCode() + CharlieCT.GetHashCode() + Junp.GetHashCode() + JunpCT.GetHashCode();
		}
	}

	/// <summary>
	/// データベース接続情報
	/// </summary>
	[Serializable]
	public class DatabaseConnect : ICloneable, IEquatable<DatabaseConnect>
	{
		/// <summary>
		/// インスタンス名
		/// </summary>
		public string InstanceName { get; set; }

		/// <summary>
		/// データベース名
		/// </summary>
		public string DatabaseName { get; set; }

		/// <summary>
		/// ユーザーID
		/// </summary>
		public string UserID { get; set; }

		/// <summary>
		/// パスワード
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public DatabaseConnect()
		{
			InstanceName = string.Empty;
			DatabaseName = string.Empty;
			UserID = string.Empty;
			Password = string.Empty;
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
		/// <param name="other">比較するDatabaseConnect</param>
		/// <returns>判定</returns>
		public bool Equals(DatabaseConnect other)
		{
			if (other != null)
			{
				if (InstanceName == other.InstanceName
					&& DatabaseName == other.DatabaseName
					&& UserID == other.UserID
					&& Password == other.Password)
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
		/// <param name="obj">比較するDatabaseConnectオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is DatabaseConnect)
			{
				return this.Equals((DatabaseConnect)obj);
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
			return (InstanceName + DatabaseName + UserID + Password).GetHashCode();
		}
	}
}
