//
// OnlineDemandEarningsFileSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/12/01 勝呂):新規作成
//
using MwsLib.Settings.Mail;
using MwsLib.Settings.SqlServer;
using System;
using System.IO;

namespace OnlineDemandEarningsFile.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class OnlineDemandEarningsFileSettings : ICloneable, IEquatable<OnlineDemandEarningsFileSettings>
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
		/// 044029 ｵﾝﾗｲﾝ請求 設定作業(訪問)
		/// </summary>
		//public string OnlineDemandHomonGoodsID { get; set; }

		/// <summary>
		/// 044030 ｵﾝﾗｲﾝ請求 設定作業(リモート)
		/// </summary>
		//public string OnlineDemandRemoteGoodsID { get; set; }

		/// <summary>
		/// 018426 ｵﾝﾗｲﾝ資格確認訪問診療連携環境設定費
		/// </summary>
		//public string OnlineHomonGoodsID { get; set; }

		/// <summary>
		/// 出力ファイル名
		/// オンライン請求作業売上データ_202404011201.csv
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
		public OnlineDemandEarningsFileSettings()
        {
			ExportDir = string.Empty;
			ExportFilename = string.Empty;
			PcaVersion = 7;
			SlipInitialNumber = 61001;
			Mail = new MailSettings();
			ConnectJunp = new SqlServerConnect();
			ConnectCharlie = new SqlServerConnect();
			//OnlineDemandHomonGoodsID = string.Empty;
			//OnlineDemandRemoteGoodsID = string.Empty;
			//OnlineHomonGoodsID = string.Empty;
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
		public bool Equals(OnlineDemandEarningsFileSettings other)
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
					//&& OnlineDemandHomonGoodsID == other.OnlineDemandHomonGoodsID
					//&& OnlineDemandRemoteGoodsID == other.OnlineDemandRemoteGoodsID
					//&& OnlineHomonGoodsID == other.OnlineHomonGoodsID)
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
			if (obj is OnlineDemandEarningsFileSettings)
			{
				return this.Equals((OnlineDemandEarningsFileSettings)obj);
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
