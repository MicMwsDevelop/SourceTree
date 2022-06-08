//
// OptechRojin.cs
//
// オプテックコンバータ用XML 老人保健情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
using CommonLib.Common;

namespace OptechConvert.XML.Patient
{
	/// <summary>
	/// 老人保健情報
	/// </summary>
	public class OptechRojin
	{
		/// <summary>
		/// 市町村番号
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("市町村番号")]
		public string 市町村番号 { get; set; }

		/// <summary>
		/// 老人受給者番号
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("老人受給者番号")]
		public string 老人受給者番号 { get; set; }

		/// <summary>
		/// 備考
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("備考")]
		public string 備考 { get; set; }

		/// <summary>
		/// 有効期間開始
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("有効期間開始")]
		public string 有効期間開始 { get; set; }

		/// <summary>
		/// 有効期間終了
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("有効期間終了")]
		public string 有効期間終了 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OptechRojin()
		{
			市町村番号 = string.Empty;
			老人受給者番号 = string.Empty;
			備考 = string.Empty;
			有効期間開始 = string.Empty;
			有効期間終了 = string.Empty;
		}

		/// <summary>
		/// 保険証情報に含まれるか？
		/// </summary>
		/// <param name="startYMD">保険証有効期間開始日</param>
		/// <param name="endYMD">保険証有効期間終了日</param>
		/// <returns>判定</returns>
		public bool IsInclude(int startYMD, int endYMD)
		{
			int start = 0;
			if (0 < 有効期間開始.Length)
			{
				start = DateConversion.ToDate(有効期間開始).ToIntYMD();
			}
			int end = 0;
			if (0 < 有効期間終了.Length)
			{
				end = DateConversion.ToDate(有効期間終了).ToIntYMD();
			}
			if (0 == start && 0 == end)
			{
				return true;
			}
			if (0 == start)
			{
				if (end <= endYMD)
				{
					return true;
				}
			}
			else if (start <= startYMD && endYMD <= end)
			{
				return true;
			}
			return false;
		}
	}
}
