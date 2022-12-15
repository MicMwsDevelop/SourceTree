//
// OnlineLicenseProgressEntrySettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/08/29 勝呂)
// Ver1.01 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
//
using MwsLib.Settings.SqlServer;
using System;

namespace OnlineLicenseProgressEntry.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class OnlineLicenseProgressEntrySettings : ICloneable, IEquatable<OnlineLicenseProgressEntrySettings>
	{
		/// <summary>
		/// SQL Server接続情報 SalesDB
		/// </summary>
		public SqlServerConnect ConnectSales { get; set; }

		/// <summary>
		/// SQL Server接続情報 JunpDB
		/// </summary>
		// Ver1.01 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
		public SqlServerConnect ConnectJunp { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OnlineLicenseProgressEntrySettings()
        {
			ConnectSales = new SqlServerConnect();
			ConnectJunp = new SqlServerConnect();
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
		public bool Equals(OnlineLicenseProgressEntrySettings other)
		{
			if (other != null)
			{
				if (ConnectSales.Equals(other.ConnectSales) && ConnectJunp.Equals(other.ConnectJunp))
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
			if (obj is OnlineLicenseProgressEntrySettings)
			{
				return this.Equals((OnlineLicenseProgressEntrySettings)obj);
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
