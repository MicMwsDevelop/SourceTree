//
// OnlineLicenseSubsidySettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2022/07/06 勝呂):新規作成
//
using MwsLib.Settings.SqlServer;
using System;

namespace OnlineLicenseSubsidy.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class OnlineLicenseSubsidySettings : ICloneable, IEquatable<OnlineLicenseSubsidySettings>
	{
		/// <summary>
		/// 補助金額資料_NTT東日本_入力フォルダ
		/// </summary>
		public string 補助金額資料_NTT東日本_入力フォルダ { get; set; }

		/// </summary>
		/// 補助金額資料_NTT西日本_入力フォルダ
		/// </summary>
		public string 補助金額資料_NTT西日本_入力フォルダ { get; set; }

		/// <summary>
		/// 助成金申請書類出力フォルダ
		/// </summary>
		public string 助成金申請書類出力フォルダ { get; set; }
		/// <summary>
		/// SQL Server接続情報
		/// </summary>
		public SqlServerConnect Junp { get; set; }
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OnlineLicenseSubsidySettings()
        {
			補助金額資料_NTT東日本_入力フォルダ = string.Empty;
			補助金額資料_NTT西日本_入力フォルダ = string.Empty;
			助成金申請書類出力フォルダ = string.Empty;
			Junp = new SqlServerConnect();
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
				if (補助金額資料_NTT東日本_入力フォルダ == other.補助金額資料_NTT東日本_入力フォルダ
					&& 補助金額資料_NTT東日本_入力フォルダ == other.補助金額資料_NTT東日本_入力フォルダ
					&& 助成金申請書類出力フォルダ == other.助成金申請書類出力フォルダ
					&& Junp.Equals(other.Junp))
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
