//
// SoftwareMainteSaleDataOutputSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/03/16 勝呂)
//
using System;
using System.IO;

namespace SoftwareMainteSaleDataOutput
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class SoftwareMainteSaleDataOutputSettings : ICloneable, IEquatable<SoftwareMainteSaleDataOutputSettings>
	{
		/// <summary>
		/// 出力先フォルダ
		/// </summary>
		public string ExportDir;

		/// <summary>
		/// 売上データ出力ファイル名
		/// </summary>
		public string ExportFilename;

		/// <summary>
		/// 売上データ出力ファイルパス名
		/// </summary>
		public string Pathname
		{
			get
			{
				return Path.Combine(ExportDir, ExportFilename);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SoftwareMainteSaleDataOutputSettings()
        {
			ExportDir = string.Empty;
			ExportFilename = string.Empty;
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
		public bool Equals(SoftwareMainteSaleDataOutputSettings other)
		{
			if (other != null)
			{
				if (ExportDir == other.ExportDir
					&& ExportFilename == other.ExportFilename)
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
			if (obj is SoftwareMainteSaleDataOutputSettings)
			{
				return this.Equals((SoftwareMainteSaleDataOutputSettings)obj);
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
