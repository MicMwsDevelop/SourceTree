﻿//
// OnlineLicenseSubsidySettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2022/07/06 勝呂):新規作成
//
using MwsLib.Settings.HeadOffice;
using MwsLib.Settings.SqlServer;
using System;
using System.Collections.Generic;

namespace OnlineLicenseSubsidy.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class OnlineLicenseSubsidySettings : ICloneable, IEquatable<OnlineLicenseSubsidySettings>
	{
		/// <summary>
		/// 出力先フォルダ
		/// </summary>
		public HeadOfficeSettings HeadOffice { get; set; }

		/// <summary>
		/// SQL Server接続情報
		/// </summary>
		public SqlServerConnect Junp { get; set; }

		/// <summary>
		/// オンライン資格確認関連商品
		/// </summary>
		public List<OnlineGoods> OnlineGoodsList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OnlineLicenseSubsidySettings()
        {
			HeadOffice = new HeadOfficeSettings();
			Junp = new SqlServerConnect();
			OnlineGoodsList = new List<OnlineGoods>();
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
		public bool Equals(OnlineLicenseSubsidySettings other)
		{
			if (other != null)
			{
				if (HeadOffice.Equals(other.HeadOffice)
					&& Junp.Equals(other.Junp)
					&& OnlineGoodsList.Equals(other.OnlineGoodsList))
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
			if (obj is OnlineLicenseSubsidySettings)
			{
				return this.Equals((OnlineLicenseSubsidySettings)obj);
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