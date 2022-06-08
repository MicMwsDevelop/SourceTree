//
// LedgerValue.cs
//
// オプテックコンバータ用XML 未収金情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
namespace OptechConvert.XML.Ledger
{
	/// <summary>
	/// 未収金情報
	/// </summary>
	public class LedgerValue
	{
		/// <summary>
		/// 預り金
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Value")]
		public int Value { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public LedgerValue()
		{
			Value = 0;
		}
	}
}
