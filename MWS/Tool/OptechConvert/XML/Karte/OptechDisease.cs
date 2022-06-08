//
// OptechDisease.cs
//
// オプテックコンバータ用XML 病名情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
namespace OptechConvert.XML.Karte
{
	/// <summary>
	/// 病名情報
	/// </summary>
	public class OptechDisease
	{
		/// <summary>
		/// 病名
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Name")]
		public string Name { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OptechDisease()
		{
			Name = string.Empty;
		}
	}
}
