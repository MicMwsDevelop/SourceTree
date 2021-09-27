//
// ScanImageManagerSettings.cs
// 
// 文書インデックス管理 環境設定
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/06 勝呂)
// Ver1.01 SQL Server接続情報を環境設定に移行(2021/09/07 勝呂)
//
using MwsLib.Settings.SqlServer;
using System;

namespace ScanImageManager.Settings
{
	/// <summary>
	/// PC安心サポート管理 環境設定
	/// </summary>
	[Serializable]
	public class ScanImageManagerSettings : ICloneable, IEquatable<ScanImageManagerSettings>
	{
		/// <summary>
		/// スキャナーデータイメージパス名
		/// </summary>
		public string ImagePath;

		/// <summary>
		/// SQL Server接続情報
		/// </summary>
		public SqlServerConnectSettings Connect { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ScanImageManagerSettings()
        {
			ImagePath = string.Empty;
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
		public bool Equals(ScanImageManagerSettings other)
		{
			if (other != null)
			{
				if (ImagePath == other.ImagePath && Connect.Equals(other.Connect))
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
			if (obj is ScanImageManagerSettings)
			{
				return this.Equals((ScanImageManagerSettings)obj);
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
