//
// VariousDocumentOutSettings.cs
// 
// 環境設定定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2021/04/22):新規作成
//
using MwsLib.Settings.HeadOffice;
using MwsLib.Settings.SqlServer;
using System;

namespace VariousDocumentOut.Settings
{
	/// <summary>
	/// 環境設定
	/// </summary>
	public class VariousDocumentOutSettings : ICloneable, IEquatable<VariousDocumentOutSettings>
	{
		/// <summary>
		/// 出力先フォルダ
		/// </summary>
		public HeadOfficeSettings HeadOffice { get; set; }

		/// <summary>
		/// SQL Server接続情報
		/// </summary>
		public SqlServerConnectSettings Connect { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public VariousDocumentOutSettings()
        {
			HeadOffice = new HeadOfficeSettings();
			Connect = new SqlServerConnectSettings();
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
		public bool Equals(VariousDocumentOutSettings other)
		{
			if (other != null)
			{
				if (HeadOffice.Equals(other.HeadOffice)
					&& Connect.Equals(other.Connect))
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
			if (obj is VariousDocumentOutSettings)
			{
				return this.Equals((VariousDocumentOutSettings)obj);
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
