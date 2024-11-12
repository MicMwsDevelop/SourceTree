//
// PriceUpEarningsFileSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/10/16 勝呂):新規作成
//
using System;
using System.IO;

namespace PriceUpEarningsFile.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class PriceUpEarningsFileSettings : ICloneable, IEquatable<PriceUpEarningsFileSettings>
	{
		/// <summary>
		/// 出力先フォルダ
		/// </summary>
		public string ExportDir { get; set; }

		/// <summary>
		/// 売上データ出力ファイル名(拡張子なし)
		/// </summary>
		public string ExportFilename { get; set; }

		/// <summary>
		/// 売上明細データ PCAバージョン情報
		/// </summary>
		public int PcaVersionEarnings { get; set; }

		/// <summary>
		/// 商品マスタ PCAバージョン情報
		/// </summary>
		public int PcaVersionGoods { get; set; }

		/// <summary>
		/// 売上伝票番号初期値
		/// </summary>
		public int SlipInitialNumber { get; set; }

		/// <summary>
		/// 売上データ出力ファイル名
		/// MWS値上対応売上データ_202412010900.csv
		/// </summary>
		public string FormalFilename
		{
			get
			{
				return string.Format("{0}_{1}.csv", ExportFilename, DateTime.Now.ToString("yyyyMMddHHmm"));
			}
		}

		/// <summary>
		/// 売上データ中間ファイル名
		/// </summary>
		public string TemporaryFilename
		{
			get
			{
				return string.Format("{0}.csv", ExportFilename);
			}
		}

		/// <summary>
		/// 売上データ中間ファイルパス名
		/// </summary>
		public string TemporaryPathname
		{
			get
			{
				return Path.Combine(Directory.GetCurrentDirectory(), TemporaryFilename);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PriceUpEarningsFileSettings()
        {
			ExportDir = string.Empty;
			ExportFilename = string.Empty;
			PcaVersionEarnings = 7;
			PcaVersionGoods = 10;
			SlipInitialNumber = 850001;
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
		public bool Equals(PriceUpEarningsFileSettings other)
		{
			if (other != null)
			{
				if (ExportDir == other.ExportDir
					&& ExportFilename == other.ExportFilename
					&& PcaVersionEarnings == other.PcaVersionEarnings
					&& PcaVersionGoods == other.PcaVersionGoods
					&& SlipInitialNumber == other.SlipInitialNumber)
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
			if (obj is PriceUpEarningsFileSettings)
			{
				return this.Equals((PriceUpEarningsFileSettings)obj);
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
		/// 出力ファイルパス名
		/// </summary>
		/// <param name="filename">ファイル名</param>
		public string FormalPathname(string filename)
		{
			return Path.Combine(ExportDir, filename);
		}
	}
}
