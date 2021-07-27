//
// OptechOralInformation.cs
//
// オプテックコンバータ用XML 口腔内情報クラス（OralInformation.xml）
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
namespace OptechConvert.XML.OralInformation
{
	/// <summary>
	/// 口腔内情報
	/// </summary>
	[System.Xml.Serialization.XmlRoot("OralInformation")]
	public class OptechOralInformation
	{
		/// <summary>
		/// 欠損歯情報
		/// </summary>
		[System.Xml.Serialization.XmlElement("MissingTeeth")]
		public MissingTeeth Missing { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OptechOralInformation()
		{
			Missing = null;
		}
	}
}
