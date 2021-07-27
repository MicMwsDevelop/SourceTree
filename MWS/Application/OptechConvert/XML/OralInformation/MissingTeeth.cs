//
// MissingTeeth.cs
//
// オプテックコンバータ用XML 欠損歯情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
using MwsLib.BuiData;

namespace OptechConvert.XML.OralInformation
{
	public class MissingTeeth
	{
		/// <summary>
		/// 欠損歯情報
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Teeth")]
		public string Teeth { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MissingTeeth()
		{
			Teeth = string.Empty;
		}

		/// <summary>
		/// 通常歯式かどうか？
		/// </summary>
		/// <returns></returns>
		public bool IsNormalTeeth()
		{
			return DecodingOptechTeeth.IsNormalTeeth(Teeth);
		}

		/// <summary>
		/// 欠損歯をpalette歯式文字列で取得
		/// </summary>
		/// <returns>palette歯式文字列</returns>
		public string ToBuiExpString()
		{
			if (IsNormalTeeth())
			{
				string bui = DecodingOptechTeeth.ConvertRezeptComputeBui(Teeth);
				return RezeptComputeBui.ToBuiExpFromRezeptComputeString(bui).ToString();
			}
			return string.Empty;
		}
	}
}
