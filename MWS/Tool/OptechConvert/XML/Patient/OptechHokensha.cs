//
// OptechHokensha.cs
//
// オプテックコンバータ用XML 保険者情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
using CommonLib.Common;

namespace OptechConvert.XML.Patient
{
	/// <summary>
	/// 保険者情報
	/// </summary>
	public class OptechHokensha
	{
		/// <summary>
		/// 保険者番号
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("保険者番号")]
		public string 保険者番号 { get; set; }

		/// <summary>
		/// 被保険者記号
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("被保険者記号")]
		public string 被保険者記号 { get; set; }

		/// <summary>
		/// 被保険者番号
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("被保険者番号")]
		public string 被保険者番号 { get; set; }

		/// <summary>
		/// 被保険者氏名
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("被保険者氏名")]
		public string 被保険者氏名 { get; set; }

		/// <summary>
		/// 続柄
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("続柄")]
		public string 続柄 { get; set; }

		/// <summary>
		/// 職務上の事由
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("職務上の事由")]
		public string 職務上の事由 { get; set; }

		/// <summary>
		/// 職業
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("職業")]
		public string 職業 { get; set; }

		/// <summary>
		/// 資格取得日
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("資格取得日")]
		public string 資格取得日 { get; set; }

		/// <summary>
		/// 保険有効期限
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("保険有効期限")]
		public string 保険有効期限 { get; set; }

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
		/// 有効期間開始日の取得
		/// </summary>
		public int LimitStartYMD
		{
			get
			{
				if (0 < 有効期間開始.Length)
				{
					return DateConversion.ToDate(有効期間開始).ToIntYMD();
				}
				return 0;
			}
		}

		/// <summary>
		/// 有効期間終了日の取得
		/// </summary>
		public int LimitEndYMD
		{
			get
			{
				if (0 < 有効期間終了.Length)
				{
					return DateConversion.ToDate(有効期間終了).ToIntYMD();
				}
				return 99991231;
			}
		}

		/// <summary>
		/// 家族かどうか？
		/// </summary>
		public int Kazoku
		{
			get
			{
				return ("本人" == 続柄) ? 0 : 1;
			}
		}

		/// <summary>
		/// 職務上の事由
		/// </summary>
		public int Shokumujo
		{
			get
			{
				switch (職務上の事由)
				{
					case "職務上": return 1;
					case "下船３ヶ月以内": return 2;
					case "通勤災害": return 3;
				}
				// 指定なし
				return 0;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OptechHokensha()
		{
			保険者番号 = string.Empty;
			被保険者記号 = string.Empty;
			被保険者番号 = string.Empty;
			被保険者氏名 = string.Empty;
			続柄 = string.Empty;
			職務上の事由 = string.Empty;
			職業 = string.Empty;
			資格取得日 = string.Empty;
			保険有効期限 = string.Empty;
			備考 = string.Empty;
			有効期間開始 = string.Empty;
			有効期間終了 = string.Empty;
		}
	}
}
