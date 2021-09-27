//
// SqlServerConnectSettings..cs
// 
// SQL Server 接続情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/08/31 勝呂)
//
using System;

namespace MwsLib.Settings.SqlServer
{
	/// <summary>
	/// データベース接続情報
	/// </summary>
	public class SqlServerConnect : ICloneable, IEquatable<SqlServerConnect>
	{
		// 接続文字列 ///////////////////////////////////////

		/// <summary>
		/// DB接続文字列
		/// </summary>
		private const string DB_CONNECT_STRING = @"Server={0};Database={1};User ID={2};Password={3};Min Pool Size=1";

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
		/// 接続文字列の取得
		/// </summary>
		public string ConnectionString
		{
			get
			{
				return string.Format(DB_CONNECT_STRING, InstanceName, DatabaseName, UserID, Password);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SqlServerConnect()
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
		/// Deep Copy
		/// </summary>
		/// <returns>チェック項目グループ</returns>
		public SqlServerConnect DeepCopy()
		{
			SqlServerConnect ret = new SqlServerConnect();
			ret.InstanceName = this.InstanceName;
			ret.DatabaseName = this.DatabaseName;
			ret.UserID = this.UserID;
			ret.Password = this.Password;
			return ret;
		}

		/// <summary>
		/// このインスタンスと、指定した環境設定クラスの値が同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するオブジェクト</param>
		/// <returns>判定</returns>
		public bool Equals(SqlServerConnect other)
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
		/// <param name="obj">比較するオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is SqlServerConnect)
			{
				return this.Equals((SqlServerConnect)obj);
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

	/// <summary>
	/// SQL Server 接続XML情報
	/// </summary>
	public class SqlServerConnectSettings : ICloneable, IEquatable<SqlServerConnectSettings>
	{
		/// <summary>
		/// JunpDB
		/// </summary>
		public SqlServerConnect Junp { get; set; }

		/// <summary>
		/// CharlieDB
		/// </summary>
		public SqlServerConnect Charlie { get; set; }

		/// <summary>
		/// Coupler
		/// </summary>
		public SqlServerConnect Coupler { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SqlServerConnectSettings()
		{
			Junp = new SqlServerConnect();
			Charlie = new SqlServerConnect();
			Coupler = new SqlServerConnect();
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
		public SqlServerConnectSettings DeepCopy()
		{
			SqlServerConnectSettings ret = new SqlServerConnectSettings();
			ret.Junp = this.Junp.DeepCopy();
			ret.Charlie = this.Charlie.DeepCopy();
			ret.Coupler = this.Coupler.DeepCopy();
			return ret;
		}

		/// <summary>
		/// このインスタンスと、指定した環境設定クラスの値が同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するオブジェクト</param>
		/// <returns>判定</returns>
		public bool Equals(SqlServerConnectSettings other)
		{
			if (other != null)
			{
				if (Junp.Equals(other.Junp)
					&& Charlie.Equals(other.Charlie)
					&& Coupler.Equals(other.Coupler))
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
			return ToString().GetHashCode();
		}
	}
}
