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
		/// 補助金額資料フォルダ_NTT東日本
		/// </summary>
		public string 補助金額資料フォルダ_NTT東日本 { get; set; }

		/// </summary>
		/// 助成金額資料フォルダ_NTT西日本
		/// </summary>
		public string 補助金額資料フォルダ_NTT西日本 { get; set; }

		/// <summary>
		/// 作業リストフォルダ
		/// </summary>
		public string 作業リストフォルダ { get; set; }

		/// <summary>
		/// 補助金申請書類フォルダ
		/// </summary>
		public string 補助金申請書類フォルダ { get; set; }

		/// <summary>
		/// 補助金申請書類エクセルフォルダ
		/// </summary>
		public string 補助金申請書類エクセルフォルダ { get; set; }

		/// <summary>
		/// 補助金申請書類PDFフォルダ
		/// </summary>
		public string 補助金申請書類PDFフォルダ { get; set; }

		/// <summary>
		/// SQL Server接続情報
		/// </summary>
		public SqlServerConnect Junp { get; set; }
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OnlineLicenseSubsidySettings()
        {
			補助金額資料フォルダ_NTT東日本 = string.Empty;
			補助金額資料フォルダ_NTT西日本 = string.Empty;
			作業リストフォルダ = string.Empty;
			補助金申請書類フォルダ = string.Empty;
			補助金申請書類エクセルフォルダ = string.Empty;
			補助金申請書類PDFフォルダ = string.Empty;
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
				if (補助金額資料フォルダ_NTT東日本 == other.補助金額資料フォルダ_NTT東日本
					&& 補助金額資料フォルダ_NTT西日本 == other.補助金額資料フォルダ_NTT西日本
					&& 作業リストフォルダ == other.作業リストフォルダ
					&& 補助金申請書類フォルダ == other.補助金申請書類フォルダ
					&& 補助金申請書類エクセルフォルダ == other.補助金申請書類エクセルフォルダ
					&& 補助金申請書類PDFフォルダ == other.補助金申請書類PDFフォルダ
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
