﻿//
// CheckOrderSlipSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/04/17 勝呂)
//
using MwsLib.Settings.SqlServer;
using System;

namespace CheckOrderSlip.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class CheckOrderSlipSettings : ICloneable, IEquatable<CheckOrderSlipSettings>
	{
		/// <summary>
		/// SQL Server接続情報（JunpDB）
		/// </summary>
		public SqlServerConnect Junp { get; set; }

		/// <summary>
		/// SQL Server接続情報（CharlieDB）
		/// </summary>
		public SqlServerConnect Charlie { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CheckOrderSlipSettings()
		{
			Junp = new SqlServerConnect();
			Charlie = new SqlServerConnect();
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
		public bool Equals(CheckOrderSlipSettings other)
		{
			if (null != other)
			{
				if (Junp.Equals(other.Junp) && Charlie.Equals(other.Charlie))
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
			if (obj is CheckOrderSlipSettings)
			{
				return this.Equals((CheckOrderSlipSettings)obj);
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
