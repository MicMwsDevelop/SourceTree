//
// OptechLedger.cs
//
// オプテックコンバータ用XML 会計情報クラス（Ledger.xml）
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
namespace OptechConvert.XML.Ledger
{
	/// <summary>
	/// 会計情報
	/// </summary>
	[System.Xml.Serialization.XmlRoot("Ledger")]
	public class OptechLedger
	{
		/// <summary>
		/// 保険未収金
		/// </summary>
		[System.Xml.Serialization.XmlElement("InsuranceOutstandings")]
		public LedgerValue Insurance { get; set; }

		/// <summary>
		/// 自費未収金
		/// </summary>
		[System.Xml.Serialization.XmlElement("JihiOutstandings")]
		public LedgerValue Jihi { get; set; }

		/// <summary>
		/// その他未収金
		/// </summary>
		[System.Xml.Serialization.XmlElement("CommodityOutstandings")]
		public LedgerValue Commodity { get; set; }

		/// <summary>
		/// 自費及びその他未収金
		/// </summary>
		[System.Xml.Serialization.XmlElement("AnotherOutstandings")]
		public LedgerValue Another { get; set; }

		/// <summary>
		/// 預り金
		/// </summary>
		[System.Xml.Serialization.XmlElement("Deposit")]
		public LedgerValue Deposit { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OptechLedger()
		{
			Insurance = null;
			Jihi = null;
			Commodity = null;
			Another = null;
			Deposit = null;
		}
	}
}
