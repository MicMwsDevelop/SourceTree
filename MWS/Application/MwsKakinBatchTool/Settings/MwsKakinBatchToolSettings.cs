//
// MwsKakinBatchToolSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/08/23 勝呂):新規作成
//
using MwsLib.Settings.SqlServer;
using System;

namespace MwsKakinBatchTool.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class MwsKakinBatchToolSettings : ICloneable, IEquatable<MwsKakinBatchToolSettings>
	{
		/// <summary>
		/// <summary>
		/// SQL Server接続情報 CharlieDB
		/// </summary>
		public SqlServerConnect ConnectCharlie { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MwsKakinBatchToolSettings()
        {
			ConnectCharlie = new SqlServerConnect();
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
		public bool Equals(MwsKakinBatchToolSettings other)
		{
			if (other != null)
			{
				if (ConnectCharlie.Equals(other.ConnectCharlie))
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
			if (obj is MwsKakinBatchToolSettings)
			{
				return this.Equals((MwsKakinBatchToolSettings)obj);
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
