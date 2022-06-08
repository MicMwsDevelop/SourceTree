//
// LinkMicItem.cs
//
// オプテック識別子→MIC項目コード定義クラス（LinkMicItem.xml）
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
using System.Collections.Generic;

namespace OptechConvert.XML.Link
{
	/// <summary>
	/// オプテック識別子→MIC項目コード定義
	/// </summary>
	[System.Xml.Serialization.XmlRoot("LinkMicItem")]
	public class LinkMicItem
	{
		/// <summary>
		/// 初診料識別子
		/// </summary>
		[System.Xml.Serialization.XmlElement("初診")]
		public string TypeShoshin { get; set; }

		/// <summary>
		/// 再診料識別子
		/// </summary>
		[System.Xml.Serialization.XmlElement("再診")]
		public string TypeSaishin { get; set; }

		/// <summary>
		/// オプテック識別子→MIC項目コード定義
		/// </summary>
		[System.Xml.Serialization.XmlElement("LinkInfo")]
		public List<LinkInfo> Link { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public LinkMicItem()
		{
			TypeShoshin = string.Empty;
			TypeSaishin = string.Empty;
			Link = new List<LinkInfo>();
		}

		/// <summary>
		/// 識別子が初診かどうか？
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public bool IsShoshin(string type)
		{
			return (TypeShoshin == type) ? true : false;
		}

		/// <summary>
		/// オプテック識別子からMIC保険処置項目コードの取得
		/// </summary>
		/// <param name="type">オプテック識別子</param>
		/// <returns>MIC保険処置項目コード</returns>
		public string GetMicItemCode(string type)
		{
			LinkInfo info = Link.Find(p => p.OptechType == type);
			if (null != info && 0 < info.ItemCode.Length)
			{
				return info.ItemCode;
			}
			return string.Empty;
		}
	}
}
