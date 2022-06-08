//
// OptechDay.cs
//
// オプテックコンバータ用XML 診療日情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
using CommonLib.Common;
using System.Collections.Generic;

namespace OptechConvert.XML.Karte
{
	/// <summary>
	/// 診療日情報
	/// </summary>
	public class OptechDay
	{
		/// <summary>
		/// 診療日
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Date")]
		public string Date { get; set; }

		/// <summary>
		/// 順序番号
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("DateNo")]
		public int DateNo { get; set; }

		/// <summary>
		/// 担当医
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Doctor")]
		public string Doctor { get; set; }

		/// <summary>
		/// 部位リスト
		/// </summary>
		[System.Xml.Serialization.XmlElement("Bui")]
		public List<OptechBui> BuiList { get; set; }

		/// <summary>
		/// 初診かどうか？
		/// </summary>
		public bool Shoshin { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OptechDay()
		{
			Date = string.Empty;
			DateNo = 0;
			Doctor = string.Empty;
			BuiList = new List<OptechBui>();
			Shoshin = false;
		}

		/// <summary>
		/// 日付順で比較
		/// </summary>
		public static int CompareByDate(OptechDay a, OptechDay b)
		{
			int ans = DateConversion.ToDate(a.Date).ToIntYMD() - DateConversion.ToDate(b.Date).ToIntYMD();
			if (0 == ans)
			{
				return a.DateNo - b.DateNo;
			}
			return ans;
		}

		/// <summary>
		/// CONVERT_DAYLISTの出力
		/// </summary>
		/// <param name="clinicCode">医療機関コード</param>
		/// <param name="pnumber">内部患者番号</param>
		/// <param name="seqNo">診療日シリアル番号</param>
		/// <returns></returns>
		public string OutputConvertDayList(string clinicCode, int pnumber, int seqNo)
		{
			string[] ret = new string[8];
			// 医療機関コード
			ret[0] = clinicCode;
			// PNUMBER
			ret[1] = pnumber.ToString();
			// YMD
			ret[2] = DateConversion.ToDate(Date).ToIntYMD().ToString();
			// SEQNO
			ret[3] = seqNo.ToString();
			// DAY_TYPE
			ret[4] = (Shoshin) ? "1" : "2";
			// EM_TYPE
			ret[5] = "0";
			// HOMON_TYPE
			ret[6] = "0";
			// HOMON_EM_TYPE
			ret[7] = "0";
			return string.Join("\t", ret) + "|";
		}
	}
}
