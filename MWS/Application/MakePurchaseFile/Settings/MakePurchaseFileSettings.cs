//
// MakePurchaseFileSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/01/07 勝呂)
// Ver1.07(2023/12/01 勝呂):オン資格保守サービスの仕入データ作成に対応
//
using MwsLib.Settings.SqlServer;
using System;
using System.Collections.Generic;
using CommonLib.BaseFactory.MakePurchaseFile;
using System.IO;

namespace MakePurchaseFile.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class MakePurchaseFileSettings : ICloneable, IEquatable<MakePurchaseFileSettings>
	{
		/// <summary>
		/// 出力先フォルダ
		/// </summary>
		public string 出力先フォルダ { get; set; }

		/// <summary>
		/// PCAバージョン
		/// </summary>
		public short PcaVersion { get; set; }

		/// <summary>
		/// りすとん月額仕入データファイル名
		/// </summary>
		public string りすとん月額仕入データファイル名 { get; set; }

		/// <summary>
		/// Microsoft365仕入データファイル名
		/// </summary>
		public string Microsoft365仕入データファイル名 { get; set; }

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
		/// クラウドバックアップ仕入データファイル名
		/// </summary>
		public string クラウドバックアップ仕入データファイル名 { get; set; }

		/// <summary>
		/// アルメックス保守仕入データファイル名
		/// </summary>
		public string アルメックス保守仕入データファイル名 { get; set; }

		/// <summary>
		/// オン資格保守サービス仕入データファイル名
		/// </summary>
		// Ver1.07(2023/12/01 勝呂):オン資格保守サービスの仕入データ作成に対応
		public string オン資格保守サービス仕入データファイル名 { get; set; }

		/// <summary>
		/// りすとん月額開始伝票番号：20020
		/// </summary>
		public int りすとん月額開始伝票番号 { get; set; }

		/// <summary>
		/// Microsoft365開始伝票番号：20040
		/// </summary>
		public int Microsoft365開始伝票番号 { get; set; }

		/// <summary>
		/// 問心伝月額開始伝票番号：20080
		/// </summary>
		public int 問心伝月額開始伝票番号 { get; set; }

		/// <summary>
		/// Curline本体アプリ開始伝票番号：20120
		/// </summary>
		public int Curline本体アプリ開始伝票番号 { get; set; }

		/// <summary>
		/// ナルコーム開始伝票番号：20060
		/// </summary>
		public int ナルコーム開始伝票番号 { get; set; }

		/// <summary>
		/// クラウドバックアップ開始伝票番号：20501
		/// </summary>
		public int クラウドバックアップ開始伝票番号 { get; set; }

		/// <summary>
		/// アルメックス開始伝票番号：20801
		/// </summary>
		public int アルメックス開始伝票番号 { get; set; }

		/// <summary>
		/// オン資格保守サービス開始伝票番号：20901
		/// </summary>
		// Ver1.07(2023/12/01 勝呂):オン資格保守サービスの仕入データ作成に対応
		public int オン資格保守サービス開始伝票番号 { get; set; }

		/// <summary>
		/// Curline本体アプリ商品
		/// </summary>
		public List<仕入商品情報> Curline本体アプリ商品 { get; set; }

		/// <summary>
		/// Microsoft365商品
		/// </summary>
		public List<仕入商品情報> Microsoft365商品 { get; set; }

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
		/// クラウドバックアップ商品
		/// </summary>
		public List<クラウドバックアップ仕入商品情報> クラウドバックアップ商品 { get; set; }

		/// <summary>
		/// アルメックス商品
		/// </summary>
		public List<アルメックス仕入商品情報> アルメックス商品 { get; set; }

		/// <summary>
		/// オン資格保守サービス仕入商品
		/// </summary>
		// Ver1.07(2023/12/01 勝呂):オン資格保守サービスの仕入データ作成に対応
		public List<オン資格保守サービス仕入商品情報> オン資格保守サービス商品 { get; set; }

		/// <summary>
		/// SQL Server接続情報
		/// </summary>
		public SqlServerConnectSettings Connect { get; set; }

		/// <summary>
		/// りすとん月額仕入データパス名
		/// </summary>
		public string りすとん月額仕入データパス名
		{
			get
			{
				return Path.Combine(出力先フォルダ, りすとん月額仕入データファイル名);
			}
		}

		/// <summary>
		/// Microsoft365仕入データパス名
		/// </summary>
		public string Microsoft365仕入データパス名
		{
			get
			{
				return Path.Combine(出力先フォルダ, Microsoft365仕入データファイル名);
			}
		}

		/// <summary>
		/// 問心伝月額仕入データパス名
		/// </summary>
		public string 問心伝月額仕入データパス名
		{
			get
			{
				return Path.Combine(出力先フォルダ, 問心伝月額仕入データファイル名);
			}
		}

		/// <summary>
		/// Curline本体アプリ仕入データパス名
		/// </summary>
		public string Curline本体アプリ仕入データパス名
		{
			get
			{
				return Path.Combine(出力先フォルダ, Curline本体アプリ仕入データファイル名);
			}
		}

		/// <summary>
		/// ナルコーム仕入データパス名
		/// </summary>
		public string ナルコーム仕入データパス名
		{
			get
			{
				return Path.Combine(出力先フォルダ, ナルコーム仕入データファイル名);
			}
		}

		/// <summary>
		/// クラウドバックアップ仕入データパス名
		/// </summary>
		public string クラウドバックアップ仕入データパス名
		{
			get
			{
				return Path.Combine(出力先フォルダ, クラウドバックアップ仕入データファイル名);
			}
		}

		/// <summary>
		/// アルメックス仕入データパス名
		/// </summary>
		public string アルメックス保守仕入データパス名
		{
			get
			{
				return Path.Combine(出力先フォルダ, アルメックス保守仕入データファイル名);
			}
		}

		/// <summary>
		/// オン資格保守サービス仕入データパス名
		/// </summary>
		// Ver1.07(2023/12/01 勝呂):オン資格保守サービスの仕入データ作成に対応
		public string オン資格保守サービス仕入データパス名
		{
			get
			{
				return Path.Combine(出力先フォルダ, オン資格保守サービス仕入データファイル名);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MakePurchaseFileSettings()
        {
			出力先フォルダ = string.Empty;
			PcaVersion = 0;
			りすとん月額仕入データファイル名 = string.Empty;
			Microsoft365仕入データファイル名 = string.Empty;
			問心伝月額仕入データファイル名 = string.Empty;
			Curline本体アプリ仕入データファイル名 = string.Empty;
			ナルコーム仕入データファイル名 = string.Empty;
			クラウドバックアップ仕入データファイル名 = string.Empty;
			アルメックス保守仕入データファイル名 = string.Empty;
			りすとん月額開始伝票番号 = 0;
			Microsoft365開始伝票番号 = 0;
			問心伝月額開始伝票番号 = 0;
			Curline本体アプリ開始伝票番号 = 0;
			ナルコーム開始伝票番号 = 0;
			クラウドバックアップ開始伝票番号 = 0;
			アルメックス開始伝票番号 = 0;
			Curline本体アプリ商品 = new List<仕入商品情報>();
			Microsoft365商品 = new List<仕入商品情報>();
			りすとん月額商品 = new List<仕入商品情報>();
			問心伝月額商品 = new List<仕入商品情報>();
			ナルコーム商品 = new List<ナルコーム仕入商品情報>();
			クラウドバックアップ商品 = new List<クラウドバックアップ仕入商品情報>();
			アルメックス商品 = new List<アルメックス仕入商品情報>();
			Connect = new SqlServerConnectSettings();

			// Ver1.07(2023/12/01 勝呂):オン資格保守サービスの仕入データ作成に対応
			オン資格保守サービス仕入データファイル名 = string.Empty;
			オン資格保守サービス開始伝票番号 = 0;
			オン資格保守サービス商品 = new List<オン資格保守サービス仕入商品情報>();
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
		public bool Equals(MakePurchaseFileSettings other)
		{
			if (other != null)
			{
				if (出力先フォルダ == other.出力先フォルダ
					&& PcaVersion == other.PcaVersion
					&& りすとん月額仕入データファイル名 == other.りすとん月額仕入データファイル名
					&& Microsoft365仕入データファイル名 == other.Microsoft365仕入データファイル名
					&& 問心伝月額仕入データファイル名 == other.問心伝月額仕入データファイル名
					&& Curline本体アプリ仕入データファイル名 == other.Curline本体アプリ仕入データファイル名
					&& ナルコーム仕入データファイル名 == other.ナルコーム仕入データファイル名
					&& クラウドバックアップ仕入データファイル名 == other.クラウドバックアップ仕入データファイル名
					&& アルメックス保守仕入データファイル名 == other.アルメックス保守仕入データファイル名
					&& オン資格保守サービス仕入データファイル名 == other.オン資格保守サービス仕入データファイル名
					&& りすとん月額開始伝票番号 == other.りすとん月額開始伝票番号
					&& Microsoft365開始伝票番号 == other.Microsoft365開始伝票番号
					&& 問心伝月額開始伝票番号 == other.問心伝月額開始伝票番号
					&& Curline本体アプリ開始伝票番号 == other.Curline本体アプリ開始伝票番号
					&& ナルコーム開始伝票番号 == other.ナルコーム開始伝票番号
					&& クラウドバックアップ開始伝票番号 == other.クラウドバックアップ開始伝票番号
					&& アルメックス開始伝票番号 == other.アルメックス開始伝票番号
					&& オン資格保守サービス開始伝票番号 == other.オン資格保守サービス開始伝票番号
					&& Curline本体アプリ商品.Equals(other.Curline本体アプリ商品)
					&& Microsoft365商品.Equals(other.Microsoft365商品)
					&& りすとん月額商品.Equals(other.りすとん月額商品)
					&& 問心伝月額商品.Equals(other.問心伝月額商品)
					&& ナルコーム商品.Equals(other.ナルコーム商品)
					&& クラウドバックアップ商品.Equals(other.ナルコーム商品)
					&& アルメックス商品.Equals(other.アルメックス商品)
					&& オン資格保守サービス商品.Equals(other.オン資格保守サービス商品)
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
			if (obj is MakePurchaseFileSettings)
			{
				return this.Equals((MakePurchaseFileSettings)obj);
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
		/// りすとん月額商品コード群の取得
		/// </summary>
		/// <returns></returns>
		public string GetListonGoods()
		{
			string str = string.Empty;
			foreach (仕入商品情報 goods in りすとん月額商品)
			{
				if (0 < str.Length)
				{
					str += ",";
				}
				str += "'" + goods.商品コード + "'";
			}
			return str;
		}

		/// <summary>
		/// Microsoft365商品コード群の取得
		/// </summary>
		/// <returns></returns>
		public string GetMicrosoft365Goods()
		{
			string str = string.Empty;
			foreach (仕入商品情報 goods in Microsoft365商品)
			{
				if (0 < str.Length)
				{
					str += ",";
				}
				str += "'" + goods.商品コード + "'";
			}
			return str;
		}

		/// <summary>
		/// 問心伝月額商品コード群の取得
		/// </summary>
		/// <returns></returns>
		public string GetMonshindenGoods()
		{
			string str = string.Empty;
			foreach (仕入商品情報 goods in 問心伝月額商品)
			{
				if (0 < str.Length)
				{
					str += ",";
				}
				str += "'" + goods.商品コード + "'";
			}
			return str;
		}

		/// <summary>
		/// ナルコーム月額商品コード群の取得
		/// </summary>
		/// <returns></returns>
		public string GetNarcohrmGoods()
		{
			string str = string.Empty;
			foreach (ナルコーム仕入商品情報 goods in ナルコーム商品)
			{
				if (0 < str.Length)
				{
					str += ",";
				}
				str += "'" + goods.商品コード + "'";
			}
			return str;
		}

		/// <summary>
		/// クラウドバックアップ商品コード群の取得
		/// </summary>
		/// <returns></returns>
		public string GetCloudBackupGoods()
		{
			string str = string.Empty;
			foreach (クラウドバックアップ仕入商品情報 goods in クラウドバックアップ商品)
			{
				if (0 < str.Length)
				{
					str += ",";
				}
				str += "'" + goods.商品コード + "'";
			}
			return str;
		}

		/// <summary>
		/// アルメックス商品コード群の取得
		/// </summary>
		/// <returns></returns>
		public string GetAlmexMainteGoods()
		{
			string str = string.Empty;
			foreach (アルメックス仕入商品情報 goods in アルメックス商品)
			{
				if (0 < str.Length)
				{
					str += ",";
				}
				str += "'" + goods.商品コード + "'";
			}
			return str;
		}

		/// <summary>
		/// オン資格保守サービス商品コード群の取得
		/// </summary>
		/// <returns></returns>
		// Ver1.07(2023/12/01 勝呂):オン資格保守サービスの仕入データ作成に対応
		public string GetOnlineLicenseMaineGoods()
		{
			string str = string.Empty;
			foreach (オン資格保守サービス仕入商品情報 goods in オン資格保守サービス商品)
			{
				if (0 < str.Length)
				{
					str += ",";
				}
				str += "'" + goods.商品コード + "'";
			}
			return str;
		}

		/// <summary>
		/// オン資格保守サービス仕入先の取得
		/// </summary>
		/// <returns></returns>
		// Ver1.07(2023/12/01 勝呂):オン資格保守サービスの仕入データ作成に対応
		public List<string> GetOnlineLicenseMainePurchaseCode()
		{
			List<string> ret = new List<string>();
			foreach (オン資格保守サービス仕入商品情報 goods in オン資格保守サービス商品)
			{
				if (0 < goods.仕入先.Length)
				{
					ret.Add(goods.仕入先);
				}
			}
			return ret;
		}
	}
}
