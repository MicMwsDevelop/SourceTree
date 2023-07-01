//
// PcaInvoiceDataConverterSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/27 勝呂):新規作成
//
using MwsLib.Settings.SqlServer;
using System;

namespace PcaInvoiceDataConverter.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class PcaInvoiceDataConverterSettings : ICloneable, IEquatable<PcaInvoiceDataConverterSettings>
	{
		/// <summary>
		/// SQL Server接続情報 CharliDB
		/// </summary>
		public SqlServerConnect ConnectCharlie { get; set; }

		/// <summary>
		/// SQL Server接続情報 JunpDB
		/// </summary>
		public SqlServerConnect ConnectJunp { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PcaInvoiceDataConverterSettings()
        {
			ConnectCharlie = new SqlServerConnect();
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
		public bool Equals(PcaInvoiceDataConverterSettings other)
		{
			if (other != null)
			{
				if (ConnectCharlie.Equals(other.ConnectCharlie) && ConnectJunp.Equals(other.ConnectJunp))
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
			if (obj is PcaInvoiceDataConverterSettings)
			{
				return this.Equals((PcaInvoiceDataConverterSettings)obj);
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
