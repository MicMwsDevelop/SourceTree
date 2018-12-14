//
// ScanImageDataSettings.cs
// 
// 文書インデックス管理 環境設定
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/12/13 勝呂)
//
using MwsLib.Common;
using System;
using System.Collections.Specialized;

namespace ScanImageData.Settings
{
	/// <summary>
	/// PC安心サポート管理 環境設定
	/// </summary>
	[Serializable]
	public class ScanImageDataSettings : ICloneable, IEquatable<ScanImageDataSettings>
	{
		/// <summary>
		/// スキャナーデータイメージパス名
		/// </summary>
		public string ScanImageDataPath;

		/// <summary>
		/// スキャナーデータイメージパス名（テスト用）
		/// </summary>
		public string TestScanImageDataPath;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ScanImageDataSettings()
        {
			ScanImageDataPath = string.Empty;
			TestScanImageDataPath = string.Empty;
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
		/// <param name="other">比較するScanImageDataSettings</param>
		/// <returns>判定</returns>
		public bool Equals(ScanImageDataSettings other)
		{
			if (other != null)
			{
				if (ScanImageDataPath == other.ScanImageDataPath
					&& TestScanImageDataPath == other.TestScanImageDataPath)
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
		/// <param name="obj">比較するScanImageDataSettingsオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is ScanImageDataSettings)
			{
				return this.Equals((ScanImageDataSettings)obj);
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
