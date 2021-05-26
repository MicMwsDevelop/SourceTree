//
// HeadOfficeSettings.cs
// 
// 本社情報定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/04/22 勝呂)
//
using System;

namespace MicLib.HeadOffice
{
	/// <summary>
	/// 本社情報
	/// </summary>
	public class HeadOfficeSettings : ICloneable, IEquatable<HeadOfficeSettings>
	{
		/// <summary>
		/// 社名
		/// </summary>
		public string CompanyName { get; set; }

		/// <summary>
		/// 郵便番号
		/// </summary>
		public string Zipcode { get; set; }

		/// <summary>
		/// 住所１
		/// </summary>
		public string Address1 { get; set; }

		/// <summary>
		/// 住所２
		/// </summary>
		public string Address2 { get; set; }

		/// <summary>
		/// 電話番号
		/// </summary>
		public string Tel { get; set; }

		/// <summary>
		/// FAX番号
		/// </summary>
		public string Fax { get; set; }

		/// <summary>
		/// email
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// URL
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// 住所
		/// </summary>
		public string 住所
		{
			get
			{
				if (0 < Address2.Length)
				{
					return string.Format("{0} {1}", Address1, Address2);
				}
				return Address1;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public HeadOfficeSettings()
        {
			CompanyName = string.Empty;
			Zipcode = string.Empty;
			Address1 = string.Empty;
			Address2 = string.Empty;
			Tel = string.Empty;
			Fax = string.Empty;
			Email = string.Empty;
			Url = string.Empty;
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
		/// このインスタンスと、指定したクラスの値が同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するオブジェクト</param>
		/// <returns>判定</returns>
		public bool Equals(HeadOfficeSettings other)
		{
			if (other != null)
			{
				if (CompanyName == other.CompanyName
					&& Zipcode == other.Zipcode
					&& Address1 == other.Address1
					&& Address2 == other.Address2
					&& Tel == other.Tel
					&& Fax == other.Fax
					&& Email == other.Email
					&& Url == other.Url)
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
			if (obj is HeadOfficeSettings)
			{
				return this.Equals((HeadOfficeSettings)obj);
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
