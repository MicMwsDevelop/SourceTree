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

namespace PurchaseTransfer.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class PurchaseTransferSettings : ICloneable, IEquatable<PurchaseTransferSettings>
	{
		/// <summary>
		/// 在庫一覧表入力ファイル名
		/// </summary>
		public string 在庫一覧表入力ファイル名 { get; set; }

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
		/// りすとん振替出力ファイル名
		/// </summary>
		public string りすとん振替出力ファイル名 { get; set; }

		/// <summary>
		/// りすとん出荷ファイル名
		/// </summary>
		public string りすとん出荷ファイル名 { get; set; }

		/// <summary>
		/// りすとん月額振替出力ファイル名
		/// </summary>
		public string りすとん月額振替出力ファイル名 { get; set; }

		/// <summary>
		/// りすとん月額出荷ファイル名
		/// </summary>
		public string りすとん月額出荷ファイル名 { get; set; }

		/// <summary>
		/// Office365振替出力ファイル名
		/// </summary>
		public string Office365振替出力ファイル名 { get; set; }

		/// <summary>
		/// Office365出荷ファイル名
		/// </summary>
		public string Office365出荷ファイル名 { get; set; }

		/// <summary>
		/// 問心伝振替出力ファイル名
		/// </summary>
		public string 問心伝振替出力ファイル名 { get; set; }

		/// <summary>
		/// 問心伝出荷ファイル名
		/// </summary>
		public string 問心伝出荷ファイル名 { get; set; }

		/// <summary>
		/// 問心伝月額振替出力ファイル名
		/// </summary>
		public string 問心伝月額振替出力ファイル名 { get; set; }

		/// <summary>
		/// ソフトバンク振替出力ファイル名
		/// </summary>
		public string ソフトバンク振替出力ファイル名 { get; set; }

		/// <summary>
		/// Curline本体アプリ出力ファイル名
		/// </summary>
		public string Curline本体アプリ出力ファイル名 { get; set; }

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
		/// SQL Server接続情報
		/// </summary>
		public SqlServerConnectSettings Connect { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PurchaseTransferSettings()
        {
			在庫一覧表入力ファイル名 = string.Empty;
			仕入振替出力ファイル名 = string.Empty;
			社内使用分振替出力ファイル名 = string.Empty;
			貯蔵品社内使用分振替出力ファイル名 = string.Empty;
			りすとん振替出力ファイル名 = string.Empty;
			りすとん出荷ファイル名 = string.Empty;
			りすとん月額振替出力ファイル名 = string.Empty;
			りすとん月額出荷ファイル名 = string.Empty;
			Office365振替出力ファイル名 = string.Empty;
			Office365出荷ファイル名 = string.Empty;
			問心伝振替出力ファイル名 = string.Empty;
			問心伝出荷ファイル名 = string.Empty;
			問心伝月額振替出力ファイル名 = string.Empty;
			ソフトバンク振替出力ファイル名 = string.Empty;
			Curline本体アプリ出力ファイル名 = string.Empty;
			対象年月 = string.Empty;
			対象年月開始日 = 0;
			対象年月終了日 = 0;
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
				if (在庫一覧表入力ファイル名 == other.在庫一覧表入力ファイル名
					&& 仕入振替出力ファイル名 == other.仕入振替出力ファイル名
					&& 社内使用分振替出力ファイル名 == other.社内使用分振替出力ファイル名
					&& 貯蔵品社内使用分振替出力ファイル名 == other.貯蔵品社内使用分振替出力ファイル名
					&& りすとん振替出力ファイル名 == other.りすとん振替出力ファイル名
					&& りすとん出荷ファイル名 == other.りすとん出荷ファイル名
					&& りすとん月額振替出力ファイル名 == other.りすとん月額振替出力ファイル名
					&& りすとん月額出荷ファイル名 == other.りすとん月額出荷ファイル名
					&& Office365振替出力ファイル名 == other.Office365振替出力ファイル名
					&& Office365出荷ファイル名 == other.Office365出荷ファイル名
					&& 問心伝振替出力ファイル名 == other.問心伝振替出力ファイル名
					&& 問心伝出荷ファイル名 == other.問心伝出荷ファイル名
					&& 問心伝月額振替出力ファイル名 == other.問心伝月額振替出力ファイル名
					&& ソフトバンク振替出力ファイル名 == other.ソフトバンク振替出力ファイル名
					&& Curline本体アプリ出力ファイル名 == other.Curline本体アプリ出力ファイル名
					&& 対象年月 == other.対象年月
					&& 対象年月開始日 == other.対象年月開始日
					&& 対象年月終了日 == other.対象年月終了日
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
