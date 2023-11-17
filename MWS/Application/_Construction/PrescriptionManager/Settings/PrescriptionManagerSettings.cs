//
// PrescriptionManagerSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/02/14 勝呂):新規作成
//
using MwsLib.Settings.Mail;
using MwsLib.Settings.SqlServer;
using System;
using System.Collections.Generic;
using System.IO;

namespace PrescriptionManager.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class PrescriptionManagerSettings : ICloneable, IEquatable<PrescriptionManagerSettings>
	{
		/// <summary>
		/// 売上ファイル出力先フォルダ
		/// </summary>
		public string ExportFolder { get; set; }

		/// <summary>
		/// 売上明細データ出力ファイル名(拡張子なし)
		/// </summary>
		public string ExportFilename { get; set; }

		/// <summary>
		/// 売上明細 PCAバージョン情報
		/// </summary>
		public int PcaVersion { get; set; }

		/// <summary>
		/// 売上明細 伝票番号初期値
		/// </summary>
		public int SlipInitialNumber { get; set; }

		/// <summary>
		/// 800705 電子処方箋管理サービス（院外処方）導入パック
		/// </summary>
		public string IntroductionPackOutside { get; set; }

		/// <summary>
		/// 800701 電子処方箋管理サービス（院外処方）
		/// </summary>
		public string OutsideHospitalService { get; set; }

		/// <summary>
		/// 800706 電子処方箋管理サービス（院内処方）導入パック
		/// </summary>
		public string IntroductionPackInside { get; set; }

		/// <summary>
		/// 800702 電子処方箋管理サービス（院内処方）
		/// </summary>
		public string InsideHospitalService { get; set; }

		/// <summary>
		/// メール設定
		/// </summary>
		public MailSettings Mail { get; set; }

		/// <summary>
		/// SQL Server接続情報
		/// </summary>
		public SqlServerConnect ConnectCharlie { get; set; }

		/// <summary>
		/// 売上明細ファイル名
		/// 電子処方箋売上_20220214124500.csv
		/// </summary>
		public string FormalFilename
		{
			get
			{
				return string.Format("{0}_{1}.csv", ExportFilename, DateTime.Now.ToString("yyyyMMddHHmmss"));
			}
		}

		/// <summary>
		/// 売上明細中間ファイル名
		/// </summary>
		public string TemporaryFilename
		{
			get
			{
				return string.Format("{0}.csv", ExportFilename);
			}
		}

		/// <summary>
		/// 売上明細中間ファイルパス名
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
		public PrescriptionManagerSettings()
        {
			ExportFolder = string.Empty;
			ExportFilename = string.Empty;
			PcaVersion = 7;
			SlipInitialNumber = 610001;
			IntroductionPackOutside = string.Empty;
			OutsideHospitalService = string.Empty;
			IntroductionPackInside = string.Empty;
			InsideHospitalService = string.Empty;
			Mail = new MailSettings();
			ConnectCharlie = new SqlServerConnect();
		}

		/// <summary>
		/// 出力ファイルパス名
		/// </summary>
		/// <param name="filename">ファイル名</param>
		public string FormalPathname(string filename)
		{
			return Path.Combine(ExportFolder, filename);
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
		public bool Equals(PrescriptionManagerSettings other)
		{
			if (other != null)
			{
				if (ExportFolder == other.ExportFolder
					&& ExportFilename == other.ExportFilename
					&& PcaVersion == other.PcaVersion
					&& SlipInitialNumber == other.SlipInitialNumber
					&& IntroductionPackOutside == other.IntroductionPackOutside
					&& OutsideHospitalService == other.OutsideHospitalService
					&& IntroductionPackInside == other.IntroductionPackInside
					&& InsideHospitalService == other.IntroductionPackOutside
					&& Mail.Equals(other.Mail)
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
			if (obj is PrescriptionManagerSettings)
			{
				return this.Equals((PrescriptionManagerSettings)obj);
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
