//
// PcSupportManagerSettings.cs
// 
// PC安心サポート管理 環境設定
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/11/19 勝呂)
//
using MwsLib.Common;
using System;
using System.Collections.Specialized;

namespace PcSupportManager.Settings
{
	/// <summary>
	/// PC安心サポート管理 環境設定
	/// </summary>
	[Serializable]
	public class PcSupportManagerSettings : ICloneable, IEquatable<PcSupportManagerSettings>
	{
		/// <summary>
		/// 実行日
		/// </summary>
		public int ExecDay;

		/// <summary>
		/// 前回実行日
		/// </summary>
		public DateTime? PrevExecDate;

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
		public PcSupportManagerSettings()
        {
			ExecDay = 0;
			PrevExecDate = null;
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
		/// <param name="other">比較するPcSupportManagerSettings</param>
		/// <returns>判定</returns>
		public bool Equals(PcSupportManagerSettings other)
		{
			if (other != null)
			{
				if (ExecDay == other.ExecDay
					&& PrevExecDate == other.PrevExecDate
					&& NationalHoliday.Equals(other.NationalHoliday)
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
		/// <param name="obj">比較するPcSupportManagerSettingsオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is PcSupportManagerSettings)
			{
				return this.Equals((PcSupportManagerSettings)obj);
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
		/// 実行可能かどうか？
		/// </summary>
		/// <param name="today">当日</param>
		/// <returns>判定</returns>
		public bool IsExec(Date today)
		{
			if (ExecDay <= today.Day)
			{
				if (PrevExecDate.HasValue)
				{
					if (new Date(PrevExecDate.Value).ToYearMonth() < today.ToYearMonth())
					{
						return true;
					}
				}
				else
				{
					return true;
				}
			}
			return false;
		}
	}
}
