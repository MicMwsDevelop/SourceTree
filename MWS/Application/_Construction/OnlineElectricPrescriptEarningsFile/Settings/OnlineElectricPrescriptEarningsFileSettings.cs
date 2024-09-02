//
// OnlineElectricPrescriptEarningsFileSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/08/27 勝呂):新規作成
//
using MwsLib.Settings.Mail;
using MwsLib.Settings.SqlServer;
using System;
using System.IO;

namespace OnlineElectricPrescriptEarningsFile.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class OnlineElectricPrescriptEarningsFileSettings : ICloneable, IEquatable<OnlineElectricPrescriptEarningsFileSettings>
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
		/// SQL Server接続情報 CharliDB
		/// </summary>
		public SqlServerConnect ConnectCharlie { get; set; }

		/// <summary>
		/// SQL Server接続情報 JunpDB
		/// </summary>
		public SqlServerConnect ConnectJunp { get; set; }

		/// <summary>
		/// SQL Server接続情報 COUPLER
		/// </summary>
		public SqlServerConnect ConnectCoupler { get; set; }

		/// <summary>
		/// メール設定
		/// </summary>
		public MailSettings Mail { get; set; }

		/// <summary>
		/// 売上データ出力ファイル名
		/// オン資電子処方箋売上データ作成_202311251201.csv
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
		public OnlineElectricPrescriptEarningsFileSettings()
        {
			ExportDir = string.Empty;
			ExportFilename = string.Empty;
			PcaVersion = 7;
			SlipInitialNumber = 691001;
			ConnectCharlie = new SqlServerConnect();
			ConnectJunp = new SqlServerConnect();
			ConnectCoupler = new SqlServerConnect();
			Mail = new MailSettings();
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
		public bool Equals(OnlineElectricPrescriptEarningsFileSettings other)
		{
			if (other != null)
			{
				if (ExportDir == other.ExportDir
					&& ExportFilename == other.ExportFilename
					&& PcaVersion == other.PcaVersion
					&& SlipInitialNumber == other.SlipInitialNumber
					&& ConnectCharlie.Equals(other.ConnectCharlie)
					&& ConnectJunp.Equals(other.ConnectJunp)
					&& ConnectCoupler.Equals(other.ConnectCoupler)
					&& Mail.Equals(other.Mail))
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
			if (obj is OnlineElectricPrescriptEarningsFileSettings)
			{
				return this.Equals((OnlineElectricPrescriptEarningsFileSettings)obj);
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
