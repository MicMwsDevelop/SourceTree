//
// PrintNouhinSettings.cs
// 
// MIC 納品書印刷環境設定
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/10/24 勝呂)
//
using System;
using System.Drawing;

namespace PrintNouhin
{
	/// <summary>
	/// MIC 納品書印刷環境設定
	/// </summary>
	public class PrintNouhinSettings : ICloneable, IEquatable<PrintNouhinSettings>
	{
		/// <summary>
		/// 印刷オフセット
		/// </summary>
		public Point PaperOffset;

		/// <summary>
		/// 納品書用データファイル用フォルダ
		/// </summary>
		public string NouhinDir;

		/// <summary>
		/// 納品書用データファイル名
		/// </summary>
		public string NouhinFile;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PrintNouhinSettings()
        {
			PaperOffset = new Point(0, 0);
			NouhinDir = string.Empty;
			NouhinFile = string.Empty;
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
		public bool Equals(PrintNouhinSettings other)
		{
			if (other != null)
			{
				if (PaperOffset == other.PaperOffset
					&& NouhinDir == other.NouhinDir
					&& NouhinFile == other.NouhinFile)
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
			if (obj is PrintNouhinSettings)
			{
				return this.Equals((PrintNouhinSettings)obj);
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
