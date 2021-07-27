//
// ScanImageManagerSettings.cs
// 
// 文書インデックス管理 環境設定
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/06 勝呂)
//
using MwsLib.Common;
using System;
using System.Collections.Specialized;

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
		/// デフォルトコンストラクタ
		/// </summary>
		public ScanImageManagerSettings()
        {
			ImagePath = string.Empty;
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
				if (ImagePath == other.ImagePath)
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
