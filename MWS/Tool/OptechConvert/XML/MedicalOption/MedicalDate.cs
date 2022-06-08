//
// MedicalDate.cs
//
// オプテックコンバータ用XML 保険診療日情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
namespace OptechConvert.XML.MedicalOption
{
	/// <summary>
	/// 保険診療日情報
	/// </summary>
	public class MedicalDate
	{
		/// <summary>
		/// 保険診療日
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Value")]
		public string Value { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MedicalDate()
		{
			Value = string.Empty;
		}
	}
}
