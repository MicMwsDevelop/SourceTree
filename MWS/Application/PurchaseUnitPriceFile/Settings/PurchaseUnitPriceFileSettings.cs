//
// PurchaseUnitPriceFileSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/01/14 勝呂)
//
using MwsLib.Settings.SqlServer;
using System;
using System.IO;

namespace PurchaseUnitPriceFile.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class PurchaseUnitPriceFileSettings : ICloneable, IEquatable<PurchaseUnitPriceFileSettings>
	{
		/// <summary>
		/// 在庫一覧表パス初期値
		/// </summary>
		public static readonly string 在庫一覧表パス初期値 = @"\\storage\公開データ\業務部公開用\経理共有\仕入振替\在庫一覧表";

		/// <summary>
		/// 在庫一覧表入力パス名
		/// </summary>
		public string 在庫一覧表入力パス名 { get; set; }

		/// <summary>
		/// 出力先フォルダ
		/// </summary>
		public string 出力先フォルダ { get; set; }

		/// <summary>
		/// 仕入振替出力ファイル名
		/// </summary>
		public string 仕入振替出力ファイル名 { get; set; }

		/// <summary>
		/// 社内使用分振替出力ファイル名
		/// </summary>
		public string 社内使用分振替出力ファイル名 { get; set; }

		/// <summary>
		/// 貯蔵品社内使用分振替出力ファイル名
		/// </summary>
		public string 貯蔵品社内使用分振替出力ファイル名 { get; set; }

		/// <summary>
		/// SQL Server接続情報
		/// </summary>
		public SqlServerConnectSettings Connect { get; set; }

		/// <summary>
		/// 社内使用分振替出力パス名
		/// </summary>
		public string 社内使用分振替出力パス名
		{
			get
			{
				return Path.Combine(出力先フォルダ, 社内使用分振替出力ファイル名);
			}
		}

		/// <summary>
		/// 貯蔵品社内使用分振替出力パス名
		/// </summary>
		public string 貯蔵品社内使用分振替出力パス名
		{
			get
			{
				return Path.Combine(出力先フォルダ, 貯蔵品社内使用分振替出力ファイル名);
			}
		}


		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PurchaseUnitPriceFileSettings()
        {
			在庫一覧表入力パス名 = string.Empty;
			出力先フォルダ = string.Empty;
			仕入振替出力ファイル名 = string.Empty;
			社内使用分振替出力ファイル名 = string.Empty;
			貯蔵品社内使用分振替出力ファイル名 = string.Empty;
			Connect = new SqlServerConnectSettings();
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
		public bool Equals(PurchaseUnitPriceFileSettings other)
		{
			if (other != null)
			{
				if (在庫一覧表入力パス名 == other.在庫一覧表入力パス名
					&& 出力先フォルダ == other.出力先フォルダ
					&& 仕入振替出力ファイル名 == other.仕入振替出力ファイル名
					&& 社内使用分振替出力ファイル名 == other.社内使用分振替出力ファイル名
					&& 貯蔵品社内使用分振替出力ファイル名 == other.貯蔵品社内使用分振替出力ファイル名
					&& Connect.Equals(other.Connect))
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
			if (obj is PurchaseUnitPriceFileSettings)
			{
				return this.Equals((PurchaseUnitPriceFileSettings)obj);
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
