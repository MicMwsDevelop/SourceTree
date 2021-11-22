//
// DeliveryCenterPrintNouhinSettings.cs
// 
// MIC納品書印刷環境設定
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/10/25 勝呂)
//
using System;
using System.IO;
using System.Drawing;

namespace DeliveryCenterPrintNouhin.Settings
{
	/// <summary>
	/// MIC 納品書印刷環境設定
	/// </summary>
	public class DeliveryCenterPrintNouhinSettings : ICloneable, IEquatable<DeliveryCenterPrintNouhinSettings>
	{
		/// <summary>
		/// 印刷オフセット
		/// </summary>
		public Point PaperOffset;

		/// <summary>
		/// 納品書用ファイルフォルダ
		/// </summary>
		public string NouhinDir;

		/// <summary>
		/// 納品書用データファイル名
		/// </summary>
		public string NouhinFile;

		/// <summary>
		/// パラメタファイルファイル名
		/// </summary>
		public string ParamFile;

		/// <summary>
		/// 矩形印刷
		/// </summary>
		public int PrintRectangle;

		/// <summary>
		/// 印刷プレビュー
		/// </summary>
		public int PrintPreview;

		/// <summary>
		/// 納品書ファイルパス名の取得
		/// </summary>
		public string NouhinFilePathname
		{
			get => Path.Combine(NouhinDir, NouhinFile);
		}

		/// <summary>
		/// 矩形印刷
		/// </summary>
		public bool 矩形印刷
		{
			get => (0 == PrintRectangle) ? false : true;
		}

		/// <summary>
		/// 印刷プレビュー
		/// </summary>
		public bool 印刷プレビュー
		{
			get => (0 == PrintPreview) ? false : true;
		}

		/// <summary>
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public DeliveryCenterPrintNouhinSettings()
        {
			PaperOffset = new Point(0, 0);
			NouhinDir = string.Empty;
			NouhinFile = string.Empty;
			ParamFile = string.Empty;
			PrintRectangle = 0;
			PrintPreview = 0;
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
		/// <returns></returns>
		public DeliveryCenterPrintNouhinSettings DeepCopy()
		{
			DeliveryCenterPrintNouhinSettings ret = new DeliveryCenterPrintNouhinSettings();
			ret.PaperOffset = this.PaperOffset;
			ret.NouhinDir = this.NouhinDir;
			ret.NouhinFile = this.NouhinFile;
			ret.ParamFile = this.ParamFile;
			ret.PrintRectangle = this.PrintRectangle;
			ret.PrintPreview = this.PrintPreview;
			return ret;
		}

		/// <summary>
		/// このインスタンスと、指定した環境設定クラスの値が同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するオブジェクト</param>
		/// <returns>判定</returns>
		public bool Equals(DeliveryCenterPrintNouhinSettings other)
		{
			if (other != null)
			{
				if (PaperOffset == other.PaperOffset
					&& NouhinDir == other.NouhinDir
					&& NouhinFile == other.NouhinFile
					&& ParamFile == other.ParamFile
					&& PrintRectangle == other.PrintRectangle
					&& PrintPreview == other.PrintPreview)
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
			if (obj is DeliveryCenterPrintNouhinSettings)
			{
				return this.Equals((DeliveryCenterPrintNouhinSettings)obj);
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
