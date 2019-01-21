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

namespace PcSupportManager.Settings
{
	/// <summary>
	/// PC安心サポート管理 環境設定
	/// </summary>
	[Serializable]
	public class PcSupportManagerSettings : ICloneable, IEquatable<PcSupportManagerSettings>
	{
		/// <summary>
		/// 開始メール送信実行日
		/// </summary>
		public int StartMailExecDay;

		/// <summary>
		/// 開始メール送信前回実行日
		/// </summary>
		public DateTime? StartMailPrevExecDate;

		/// <summary>
		/// 契約更新案内/契約更新メール送信実行日
		/// </summary>
		public int UpdateMailExecDay;

		/// <summary>
		/// 契約更新案内/契約更新メール送信前回実行日
		/// </summary>
		public DateTime? UpdatteMailPrevExecDate;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PcSupportManagerSettings()
        {
			StartMailExecDay = 0;
			StartMailPrevExecDate = null;
			UpdateMailExecDay = 0;
			UpdatteMailPrevExecDate = null;
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
				if (StartMailExecDay == other.StartMailExecDay
					&& StartMailPrevExecDate == other.StartMailPrevExecDate
					&& UpdateMailExecDay == other.UpdateMailExecDay
					&& UpdatteMailPrevExecDate == other.UpdatteMailPrevExecDate)
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
		/// 開始メール送信実行可能かどうか？
		/// </summary>
		/// <param name="today">当日</param>
		/// <returns>判定</returns>
		public bool IsStartMailExec(Date today)
		{
			if (StartMailExecDay <= today.Day)
			{
				if (StartMailPrevExecDate.HasValue)
				{
					if (new Date(StartMailPrevExecDate.Value).ToYearMonth() < today.ToYearMonth())
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

		/// <summary>
		/// 契約更新案内/契約更新メール送信実行可能かどうか？
		/// </summary>
		/// <param name="today">当日</param>
		/// <returns>判定</returns>
		public bool IsUpdateMailExec(Date today)
		{
			if (UpdateMailExecDay <= today.Day)
			{
				if (UpdatteMailPrevExecDate.HasValue)
				{
					if (new Date(UpdatteMailPrevExecDate.Value).ToYearMonth() < today.ToYearMonth())
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
