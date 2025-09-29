//
// HardSubscEarningsFileSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// Ver1.00(2025/04/15 勝呂):新規作成
//
using MwsLib.Settings.Mail;
using MwsLib.Settings.SqlServer;
using System;
using System.IO;

namespace HardSubscEarningsFile.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class HardSubscEarningsFileSettings : ICloneable, IEquatable<HardSubscEarningsFileSettings>
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
		/// PCAバージョン情報
		/// </summary>
		public int PcaVersion { get; set; }

		/// <summary>
		/// 売上伝票番号初期値
		/// </summary>
		public int SlipInitialNumber { get; set; }

		/// <summary>
		/// メール設定
		/// </summary>
		public MailSettings Mail { get; set; }

		/// <summary>
		/// SQL Server接続情報（Junp）
		/// </summary>
		public SqlServerConnect ConnectJunp { get; set; }

		/// <summary>
		/// SQL Server接続情報（Charlie）
		/// </summary>
		public SqlServerConnect ConnectCharlie { get; set; }

		/// <summary>
		/// 出力ファイル名
		/// アルメックス保守売上データ_202011251201.csv
		/// </summary>
		public string FormalFilename
		{
			get
			{
				return string.Format("{0}_{1}.csv", ExportFilename, DateTime.Now.ToString("yyyyMMddHHmm"));
			}
		}

		/// <summary>
		/// 中間ファイル名
		/// </summary>
		public string TemporaryFilename
		{
			get
			{
				return string.Format("{0}.csv", ExportFilename);
			}
		}

		/// <summary>
		/// 中間ファイルパス名
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
		public HardSubscEarningsFileSettings()
        {
			ExportDir = string.Empty;
			ExportFilename = string.Empty;
			PcaVersion = 7;
			SlipInitialNumber = 61001;
			Mail = new MailSettings();
			ConnectJunp = new SqlServerConnect();
			ConnectCharlie = new SqlServerConnect();
		}

		/// <summary>
		/// 出力ファイルパス名
		/// </summary>
		/// <param name="filename">ファイル名</param>
		public string FormalPathname(string filename)
		{
			return Path.Combine(ExportDir, filename);
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
		public bool Equals(HardSubscEarningsFileSettings other)
		{
			if (other != null)
			{
				if (ExportDir == other.ExportDir
					&& ExportFilename == other.ExportFilename
					&& PcaVersion == other.PcaVersion
					&& SlipInitialNumber == other.SlipInitialNumber
					&& Mail.Equals(other.Mail)
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
			if (obj is HardSubscEarningsFileSettings)
			{
				return this.Equals((HardSubscEarningsFileSettings)obj);
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
