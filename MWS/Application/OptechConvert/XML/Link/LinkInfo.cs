//
// LinkInfo.cs
//
// オプテック識別子→MIC項目コード定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
namespace OptechConvert.XML.Link
{
	/// <summary>
	/// オプテック識別子→MIC項目コード定義
	/// </summary>
	public class LinkInfo
	{
		/// <summary>
		/// オプテック処置識別子
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Type")]
		public string OptechType { get; set; }

		/// <summary>
		/// MIC項目コード
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("MicItem")]
		public string ItemCode { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public LinkInfo()
		{
			OptechType = string.Empty;
			ItemCode = string.Empty;
		}
	}
}
