//
// AlertCloudBackupPcSupportPlusSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/02/05 勝呂)
//
using MwsLib.Settings.SqlServer;
using System;
using System.Collections.Generic;
using CommonLib.BaseFactory.PurchaseTransfer;
using System.IO;

namespace PurchaseTransfer.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class PurchaseTransferSettings : ICloneable, IEquatable<PurchaseTransferSettings>
	{
		/// <summary>
		/// 在庫一覧表入力パス名
		/// </summary>
		public string 在庫一覧表入力パス名 { get; set; }

		/// <summary>
		/// 出力フォルダ
		/// </summary>
		public string 出力フォルダ { get; set; }

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
		/// りすとん月額仕入データファイル名
		/// </summary>
		public string りすとん月額仕入データファイル名 { get; set; }

		/// <summary>
		/// Office365仕入データファイル名
		/// </summary>
		public string Office365仕入データファイル名 { get; set; }

		/// <summary>
		/// 問心伝月額仕入データファイル名
		/// </summary>
		public string 問心伝月額仕入データファイル名 { get; set; }

		/// <summary>
		/// Curline本体アプリ仕入データファイル名
		/// </summary>
		public string Curline本体アプリ仕入データファイル名 { get; set; }

		/// <summary>
		/// ナルコーム仕入データファイル名
		/// </summary>
		public string ナルコーム仕入データファイル名 { get; set; }

		/// <summary>
		/// 対象年月
		/// </summary>
		public string 対象年月 { get; set; }

		/// <summary>
		/// 対象年月開始日
		/// </summary>
		public int 対象年月開始日 { get; set; }

		/// <summary>
		/// 対象年月終了日
		/// </summary>
		public int 対象年月終了日 { get; set; }

		/// <summary>
		/// Curline本体アプリ商品
		/// </summary>
		public List<仕入商品情報> Curline本体アプリ商品 { get; set; }

		/// <summary>
		/// Office365商品
		/// </summary>
		public List<仕入商品情報> Office365商品 { get; set; }

		/// <summary>
		/// りすとん月額商品
		/// </summary>
		public List<仕入商品情報> りすとん月額商品 { get; set; }

		/// <summary>
		/// 問心伝月額商品
		/// </summary>
		public List<仕入商品情報> 問心伝月額商品 { get; set; }

		/// <summary>
		/// ナルコーム商品
		/// </summary>
		public List<ナルコーム仕入商品情報> ナルコーム商品 { get; set; }

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
				return Path.Combine(出力フォルダ, 社内使用分振替出力ファイル名);
			}
		}

		/// <summary>
		/// 貯蔵品社内使用分振替出力パス名
		/// </summary>
		public string 貯蔵品社内使用分振替出力パス名
		{
			get
			{
				return Path.Combine(出力フォルダ, 貯蔵品社内使用分振替出力ファイル名);
			}
		}

		/// <summary>
		/// 仕入振替出力パス名
		/// </summary>
		public string 仕入振替出力パス名
		{
			get
			{
				return Path.Combine(出力フォルダ, 仕入振替出力ファイル名);
			}
		}

		/// <summary>
		/// りすとん月額仕入データパス名
		/// </summary>
		public string りすとん月額仕入データパス名
		{
			get
			{
				return Path.Combine(出力フォルダ, りすとん月額仕入データファイル名);
			}
		}

		/// <summary>
		/// Office365仕入データパス名
		/// </summary>
		public string Office365仕入データパス名
		{
			get
			{
				return Path.Combine(出力フォルダ, Office365仕入データファイル名);
			}
		}

		/// <summary>
		/// 問心伝月額仕入データパス名
		/// </summary>
		public string 問心伝月額仕入データパス名
		{
			get
			{
				return Path.Combine(出力フォルダ, 問心伝月額仕入データファイル名);
			}
		}

		/// <summary>
		/// Curline本体アプリ仕入データパス名
		/// </summary>
		public string Curline本体アプリ仕入データパス名
		{
			get
			{
				return Path.Combine(出力フォルダ, Curline本体アプリ仕入データファイル名);
			}
		}

		/// <summary>
		/// ナルコーム仕入データパス名
		/// </summary>
		public string ナルコーム仕入データパス名
		{
			get
			{
				return Path.Combine(出力フォルダ, ナルコーム仕入データファイル名);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PurchaseTransferSettings()
        {
			在庫一覧表入力パス名 = string.Empty;
			出力フォルダ = string.Empty;
			仕入振替出力ファイル名 = string.Empty;
			社内使用分振替出力ファイル名 = string.Empty;
			貯蔵品社内使用分振替出力ファイル名 = string.Empty;
			りすとん月額仕入データファイル名 = string.Empty;
			Office365仕入データファイル名 = string.Empty;
			問心伝月額仕入データファイル名 = string.Empty;
			Curline本体アプリ仕入データファイル名 = string.Empty;
			ナルコーム仕入データファイル名 = string.Empty;
			対象年月 = string.Empty;
			対象年月開始日 = 0;
			対象年月終了日 = 0;
			Curline本体アプリ商品 = new List<仕入商品情報>();
			Office365商品 = new List<仕入商品情報>();
			りすとん月額商品 = new List<仕入商品情報>();
			問心伝月額商品 = new List<仕入商品情報>();
			ナルコーム商品 = new List<ナルコーム仕入商品情報>();
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
		public bool Equals(PurchaseTransferSettings other)
		{
			if (other != null)
			{
				if (在庫一覧表入力パス名 == other.在庫一覧表入力パス名
					&& 出力フォルダ == other.出力フォルダ
					&& 仕入振替出力ファイル名 == other.仕入振替出力ファイル名
					&& 社内使用分振替出力ファイル名 == other.社内使用分振替出力ファイル名
					&& 貯蔵品社内使用分振替出力ファイル名 == other.貯蔵品社内使用分振替出力ファイル名
					&& りすとん月額仕入データファイル名 == other.りすとん月額仕入データファイル名
					&& Office365仕入データファイル名 == other.Office365仕入データファイル名
					&& 問心伝月額仕入データファイル名 == other.問心伝月額仕入データファイル名
					&& Curline本体アプリ仕入データファイル名 == other.Curline本体アプリ仕入データファイル名
					&& ナルコーム仕入データファイル名 == other.ナルコーム仕入データファイル名
					&& 対象年月 == other.対象年月
					&& 対象年月開始日 == other.対象年月開始日
					&& 対象年月終了日 == other.対象年月終了日
					&& Curline本体アプリ商品.Equals(other.Curline本体アプリ商品)
					&& Office365商品.Equals(other.Office365商品)
					&& りすとん月額商品.Equals(other.りすとん月額商品)
					&& 問心伝月額商品.Equals(other.問心伝月額商品)
					&& ナルコーム商品.Equals(other.ナルコーム商品)
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
			if (obj is PurchaseTransferSettings)
			{
				return this.Equals((PurchaseTransferSettings)obj);
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
