//
// tMik基本情報.cs
//
// 基本情報クラス
// [JunpDB].[dbo].[tMik基本情報]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using System;

namespace MwsLib.BaseFactory.Junp.Table
{
	/// <summary>
	/// [JunpDB].[dbo].[tMik基本情報]
	/// </summary>
	public class tMik基本情報
	{
		/// <summary>
		/// fkjCliMicID
		/// </summary>
		public int fkjCliMicID { get; set; }

		/// <summary>
		/// fkj顧客区分
		/// </summary>
		public int fkj顧客区分 { get; set; }

		/// <summary>
		/// fkj顧客名２
		/// </summary>
		public string fkj顧客名２ { get; set; }

		/// <summary>
		/// fkj郵便番号
		/// </summary>
		public string fkj郵便番号 { get; set; }

		/// <summary>
		/// fkj住所１
		/// </summary>
		public string fkj住所１ { get; set; }

		/// <summary>
		/// fkj住所２
		/// </summary>
		public string fkj住所２ { get; set; }

		/// <summary>
		/// fkj住所フリガナ
		/// </summary>
		public string fkj住所フリガナ { get; set; }

		/// <summary>
		/// fkj電話番号
		/// </summary>
		public string fkj電話番号 { get; set; }

		/// <summary>
		/// fkjファックス番号
		/// </summary>
		public string fkjファックス番号 { get; set; }

		/// <summary>
		/// fkj削除フラグ
		/// </summary>
		public bool fkj削除フラグ { get; set; }

		/// <summary>
		/// fkj得意先情報
		/// </summary>
		public string fkj得意先情報 { get; set; }

		/// <summary>
		/// fkj仕入先情報
		/// </summary>
		public string fkj仕入先情報 { get; set; }

		/// <summary>
		/// fkj状態1
		/// </summary>
		public int fkj状態1 { get; set; }

		/// <summary>
		/// fkj状態2
		/// </summary>
		public int fkj状態2 { get; set; }

		/// <summary>
		/// fkj更新日
		/// </summary>
		public DateTime? fkj更新日 { get; set; }

		/// <summary>
		/// fkj更新者
		/// </summary>
		public string fkj更新者 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMik基本情報()
		{
			fkjCliMicID = 0;
			fkj顧客区分 = 0;
			fkj顧客名２ = string.Empty;
			fkj郵便番号 = string.Empty;
			fkj住所１ = string.Empty;
			fkj住所２ = string.Empty;
			fkj住所フリガナ = string.Empty;
			fkj電話番号 = string.Empty;
			fkjファックス番号 = string.Empty;
			fkj削除フラグ = false;
			fkj得意先情報 = string.Empty;
			fkj仕入先情報 = string.Empty;
			fkj状態1 = 0;
			fkj状態2 = 0;
			fkj更新日 = null;
			fkj更新者 = string.Empty;
		}
	}
}
