//
// OnlineLicenseIntroductionStatusSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/08/29 勝呂)
//
using MwsLib.Settings.Mail;
using MwsLib.Settings.SqlServer;
using System;

namespace OnlineLicenseIntroductionStatus.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class OnlineLicenseIntroductionStatusSettings : ICloneable, IEquatable<OnlineLicenseIntroductionStatusSettings>
	{
		/// <summary>
		/// メール設定
		/// </summary>
		public MailSettings Mail { get; set; }

		/// <summary>
		/// SQL Server接続情報 JunpDB
		/// </summary>
		public SqlServerConnect ConnectJunp { get; set; }

		/// <summary>
		/// SQL Server接続情報 SalesDB
		/// </summary>
		public SqlServerConnect ConnectSales { get; set; }

		public string 導入意志 { get; set; }
		public string 工事種別 { get; set; }
		public string ステータス { get; set; }
		public string 導入意志あり_導入意思 { get; set; }
		public string 未確認_反応無し_導入意思 { get; set; }
		public string NTT_外注_依頼数_工事種別 { get; set; }
		public string IPSEC依頼提出数_工事種別 { get; set; }
		public string ヒアリングシート提出数_ステータス { get; set; }
		public string NTT案件納品数_工事種別 { get; set; }
		public string NTT案件納品数_ステータス { get; set; }
		public string IPSEC納品数_工事種別 { get; set; }
		public string IPSEC納品数_ステータス { get; set; }
		public string MIC自力_その他納品数_工事種別 { get; set; }
		public string MIC自力_その他納品数_ステータス { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OnlineLicenseIntroductionStatusSettings()
        {
			Mail = new MailSettings();
			ConnectJunp = new SqlServerConnect();
			ConnectSales = new SqlServerConnect();
			導入意志 = string.Empty;
			工事種別 = string.Empty;
			ステータス = string.Empty;
			導入意志あり_導入意思 = string.Empty;
			未確認_反応無し_導入意思 = string.Empty;
			NTT_外注_依頼数_工事種別 = string.Empty;
			IPSEC依頼提出数_工事種別 = string.Empty;
			ヒアリングシート提出数_ステータス = string.Empty;
			NTT案件納品数_工事種別 = string.Empty;
			NTT案件納品数_ステータス = string.Empty;
			IPSEC納品数_工事種別 = string.Empty;
			IPSEC納品数_ステータス = string.Empty;
			MIC自力_その他納品数_工事種別 = string.Empty;
			MIC自力_その他納品数_ステータス = string.Empty;
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
		public bool Equals(OnlineLicenseIntroductionStatusSettings other)
		{
			if (other != null)
			{
				if (Mail.Equals(other.Mail)
					&& ConnectJunp.Equals(other.ConnectJunp)
					&& ConnectSales.Equals(other.ConnectSales))
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
			if (obj is OnlineLicenseIntroductionStatusSettings)
			{
				return this.Equals((OnlineLicenseIntroductionStatusSettings)obj);
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
