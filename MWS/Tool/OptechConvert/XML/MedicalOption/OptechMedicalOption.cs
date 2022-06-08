//
// OptechMedicalOption.cs
//
// オプテックコンバータ用XML 治療情報クラス（MedicalOption.xml）
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
using CommonLib.Common;
using OptechConvert.XML.Link;
using System.Collections.Generic;
using System.Linq;

namespace OptechConvert.XML.MedicalOption
{
	/// <summary>
	/// 治療情報
	/// </summary>
	[System.Xml.Serialization.XmlRoot("MedicalOption")]
	public class OptechMedicalOption
	{
		/// <summary>
		/// 保険診療開始日
		/// </summary>
		[System.Xml.Serialization.XmlElement("保険診療開始日")]
		public MedicalDate HokenStartDate { get; set; }

		/// <summary>
		/// Treatment
		/// </summary>
		[System.Xml.Serialization.XmlElement("Treatment")]
		public List<MedicalTreatment> TreatmentList { get; set; }

		/// <summary>
		/// 最終来院日
		/// </summary>
		[System.Xml.Serialization.XmlElement("最終来院日")]
		public MedicalDate EndDate { get; set; }

		/// <summary>
		/// 保険最終来院日
		/// </summary>
		[System.Xml.Serialization.XmlElement("保険最終来院日")]
		public MedicalDate HokenEndDate { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OptechMedicalOption()
		{
			HokenStartDate = null;
			TreatmentList = new List<MedicalTreatment>();
			EndDate = null;
			HokenEndDate = null;
		}

		/// <summary>
		/// 初診期間リストの取得
		/// </summary>
		/// <param name="link">オプテック識別子→MIC項目コード定義</param>
		/// <returns>初診期間リスト</returns>
		private List<Span> GetShoshinList(LinkMicItem link)
		{
			List<Span> spanList = new List<Span>();
			foreach (MedicalTreatment tr in TreatmentList)
			{
				if (link.IsShoshin(tr.OptechType))
				{
					// 初診料
					spanList.Add(new Span(tr.GetYMD(), tr.GetYMD()));
				}
				else
				{
					if (0 == spanList.Count)
					{
						spanList.Add(new Span(tr.GetYMD(), tr.GetYMD()));
					}
					spanList[spanList.Count - 1] = new Span(spanList[spanList.Count - 1].Start, tr.GetYMD());
				}
			}
			return spanList;
		}

		/// <summary>
		/// インポートファイルの作成（初診情報、レセプト情報、レセプト病名欄情報）
		/// </summary>
		/// <param name="link">オプテック識別子→MIC項目コード定義</param>
		/// <param name="clinicCode">医療機関コード</param>
		/// <param name="pnumber">内部患者番号</param>
		/// <param name="buiStr">欠損歯情報(palette歯式形式)</param>
		/// <param name="shoshinInfList">初診情報リスト(palette歯式形式)</param>
		/// <param name="rezeptInfList">レセプト情報リスト(palette歯式形式)</param>
		/// <param name="rezeptByomeiranList">レセプト病名欄情報リスト(palette歯式形式)</param>
		/// <returns></returns>
		public void MakeConvertList(LinkMicItem link, string clinicCode, int pnumber, string buiStr, out List<string> shoshinInfList, out List<string> rezeptInfList, out List<string> rezeptByomeiranList)
		{
			shoshinInfList = new List<string>();
			rezeptInfList = new List<string>();
			rezeptByomeiranList = new List<string>();

			List<Span> spanList = GetShoshinList(link);
			if (0 < spanList.Count)
			{
				for (int i = 0; i < spanList.Count; i++)
				{
					Span span = spanList[i];

					// CONVERT_SHOSHIN_INF
					string[] ret1 = new string[7];
					// 医療機関コード
					ret1[0] = clinicCode;
					// PNUMBER
					ret1[1] = pnumber.ToString();
					// STARTYMD
					ret1[2] = span.Start.ToIntYMD().ToString();
					// ENDYMD
					ret1[3] = span.End.ToIntYMD().ToString();
					// KAISIYMD
					ret1[4] = span.Start.ToIntYMD().ToString();
					// LASTYMD
					ret1[5] = span.End.ToIntYMD().ToString();
					// KESSON_BUI
					if (i == spanList.Count - 1)
					{
						// 最終初診情報に欠損歯を格納
						ret1[6] = buiStr;
					}
					else
					{
						ret1[6] = string.Empty;
					}
					shoshinInfList.Add(string.Join("\t", ret1) + "|");
				}
			}
			// CONVERT_REZEPT_INF
			string[] ret2 = new string[9];
			// 医療機関コード
			ret2[0] = clinicCode;
			// PNUMBER
			ret2[1] = pnumber.ToString();
			// STARTYMD
			ret2[2] = spanList.First().Start.ToIntYMD().ToString();
			// ENDYMD
			ret2[3] = spanList.Last().End.ToIntYMD().ToString();
			// TENKI
			ret2[4] = "0";
			// KAISIYMD
			ret2[5] = spanList.First().Start.ToIntYMD().ToString();
			// POINT
			ret2[6] = "0";
			// MIRAIIN
			ret2[7] = "0";
			// DAYCOUNT
			ret2[8] = "0";
			rezeptInfList.Add(string.Join("\t", ret2) + "|");

			// CONVERT_REZEPT_BYOMEIRAN
			string[] ret3 = new string[8];
			// 医療機関コード
			ret3[0] = clinicCode;
			// PNUMBER
			ret3[1] = pnumber.ToString();
			// STARTYMD
			ret3[2] = spanList.First().Start.ToIntYMD().ToString();
			// SEQNO
			ret3[3] = "0";
			// BUI
			ret3[4] = string.Empty;
			// BYOMEI_NAME
			ret3[5] = string.Empty;
			// HEIKI_COUNT
			ret3[6] = "0";
			// BYOTAI_IKO
			ret3[7] = "0";
			rezeptByomeiranList.Add(string.Join("\t", ret3) + "|");
		}
	}
}
