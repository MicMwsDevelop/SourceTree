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
		///////////////////////////////////////////////////////
		// 口座振替関連基本データ

		public string PCA請求一覧10読込みファイル { get; set; }
		public string APLUS送信ファイル出力フォルダ { get; set; }

		///////////////////////////////////////////////////////
		// WEB請求書発行関連基本データ

		public int WEB請求書番号基数 { get; set; }
		public string PCA請求明細10読込みファイル { get; set; }	
		public string WEB請求書ファイル出力フォルダ { get; set; }	
		public string WEB請求書ヘッダファイル { get; set; }
		public string WEB請求書明細売上行ファイル { get; set; }
		public string WEB請求書明細消費税行ファイル { get; set; }
		public string WEB請求書明細記事行ファイル { get; set; }
		public string AGREX口振通知書ファイル出力フォルダ { get; set; }

		///////////////////////////////////////////////////////
		// 銀行振込請求書発行関連基本データ

		public int 請求書番号基数 { get; set; }
		public string PCA請求一覧11読込みファイル { get; set; }
		public string PCA請求明細11読込みファイル { get; set; }
		public string AGREX請求書ファイル出力フォルダ { get; set; }

		///////////////////////////////////////////////////////
		// その他

		public string 結果データシートファイル出力フォルダ { get; set; }

		///////////////////////////////////////////////////////
		// データベースアクセス情報

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
			PCA請求一覧10読込みファイル = string.Empty;
			APLUS送信ファイル出力フォルダ = string.Empty;
			WEB請求書番号基数 = 0;
			PCA請求明細10読込みファイル = string.Empty;
			WEB請求書ファイル出力フォルダ = string.Empty;
			WEB請求書ヘッダファイル = string.Empty;
			WEB請求書明細売上行ファイル = string.Empty;
			WEB請求書明細消費税行ファイル = string.Empty;
			WEB請求書明細記事行ファイル = string.Empty;
			AGREX口振通知書ファイル出力フォルダ = string.Empty;
			請求書番号基数 = 0;
			PCA請求一覧11読込みファイル = string.Empty;
			PCA請求明細11読込みファイル = string.Empty;
			AGREX請求書ファイル出力フォルダ = string.Empty;
			結果データシートファイル出力フォルダ = string.Empty;
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
				if (PCA請求一覧10読込みファイル != other.PCA請求一覧10読込みファイル) return false;
				if (APLUS送信ファイル出力フォルダ != other.APLUS送信ファイル出力フォルダ) return false;
				if (WEB請求書番号基数 != other.WEB請求書番号基数) return false;
				if (PCA請求明細10読込みファイル != other.PCA請求明細10読込みファイル) return false;
				if (WEB請求書ファイル出力フォルダ != other.WEB請求書ファイル出力フォルダ) return false;
				if (WEB請求書ヘッダファイル != other.WEB請求書ヘッダファイル) return false;
				if (WEB請求書明細売上行ファイル != other.WEB請求書明細売上行ファイル) return false;
				if (WEB請求書明細消費税行ファイル != other.WEB請求書明細消費税行ファイル) return false;
				if (WEB請求書明細記事行ファイル != other.WEB請求書明細記事行ファイル) return false;
				if (AGREX口振通知書ファイル出力フォルダ != other.AGREX口振通知書ファイル出力フォルダ) return false;
				if (請求書番号基数 != other.請求書番号基数) return false;
				if (PCA請求一覧11読込みファイル != other.PCA請求一覧11読込みファイル) return false;
				if (PCA請求明細11読込みファイル != other.PCA請求明細11読込みファイル) return false;
				if (AGREX請求書ファイル出力フォルダ != other.AGREX請求書ファイル出力フォルダ) return false;
				if (結果データシートファイル出力フォルダ != other.結果データシートファイル出力フォルダ) return false;
				if (false == ConnectCharlie.Equals(other.ConnectCharlie)) return false;
				if (false == ConnectJunp.Equals(other.ConnectJunp)) return false;
				return true;
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
