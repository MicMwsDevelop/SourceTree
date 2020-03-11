//
// MicHolidaySettings.cs
// 
// MIC休日 環境設定
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/01/10 勝呂)
//
using System;
using System.Collections.Specialized;

namespace MicHoliday.Settings
{
	/// <summary>
	/// PC安心サポート管理 環境設定
	/// </summary>
	[Serializable]
	public class MicHolidaySettings : ICloneable, IEquatable<MicHolidaySettings>
	{
		/// <summary>
		/// 祝日
		/// </summary>
		public StringCollection NationalHoliday;

		/// <summary>
		/// ハッピーマンデー
		/// </summary>
		public StringCollection HappyMonday;

		/// <summary>
		/// 臨時休日
		/// </summary>
		public StringCollection SpecialHoliday;

		/// <summary>
		/// 休日
		/// </summary>
		public string WeeklyHoliday;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MicHolidaySettings()
        {
			NationalHoliday = new StringCollection();
			HappyMonday = new StringCollection();
			SpecialHoliday = new StringCollection();
			WeeklyHoliday = string.Empty;
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
		/// <param name="other">比較するMicHolidaySettings</param>
		/// <returns>判定</returns>
		public bool Equals(MicHolidaySettings other)
		{
			if (other != null)
			{
				if (NationalHoliday.Equals(other.NationalHoliday)
					&& HappyMonday.Equals(other.HappyMonday)
					&& SpecialHoliday.Equals(other.SpecialHoliday)
					&& WeeklyHoliday == other.WeeklyHoliday)
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
		/// <param name="obj">比較するMicHolidaySettingsオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is MicHolidaySettings)
			{
				return this.Equals((MicHolidaySettings)obj);
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
