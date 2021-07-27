//
// OptechBui.cs
//
// オプテックコンバータ用XML 部位情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
using MwsLib.BuiData;
using MwsLib.Common;
using System.Collections.Generic;

namespace OptechConvert.XML.Karte
{
	/// <summary>
	/// 部位情報
	/// </summary>
	public class OptechBui
	{
		/// <summary>
		/// 順序番号
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("No")]
		public int No { get; set; }

		/// <summary>
		/// 部位
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Teeth")]
		public string Teeth { get; set; }

		/// <summary>
		/// 担当医
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Doctor")]
		public string Doctor { get; set; }

		/// <summary>
		/// 病名リスト
		/// </summary>
		[System.Xml.Serialization.XmlElement("Disease")]
		public List<OptechDisease> DiseaseList { get; set; }

		/// <summary>
		/// 処置リスト
		/// </summary>
		[System.Xml.Serialization.XmlElement("Treatment")]
		public List<OptechTreatment> TreatmentList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OptechBui()
		{
			No = 0;
			Teeth = string.Empty;
			Doctor = string.Empty;
			DiseaseList = new List<OptechDisease>();
			TreatmentList = new List<OptechTreatment>();
		}

		/// <summary>
		/// CONVERT_BUILISTの出力
		/// </summary>
		/// <param name="clinicCode">医療機関コード</param>
		/// <param name="pnumber">内部患者番号</param>
		/// <param name="ymd">診療日</param>
		/// <param name="daySeq">診療日シリアル番号</param>
		/// <param name="seqNo">部位シリアル番号</param>
		/// <returns></returns>
		public string OutputConvertBuiList(string clinicCode, int pnumber, Date ymd, int daySeq, int seqNo)
		{
			string[] ret = new string[13];
			// 医療機関コード
			ret[0] = clinicCode;
			// PNUMBER
			ret[1] = pnumber.ToString();
			// YMD
			ret[2] = ymd.ToIntYMD().ToString();
			// DAYSEQ
			ret[3] = daySeq.ToString();
			// SEQNO
			ret[4] = seqNo.ToString();
			// BUI_TYPE
			ret[5] = (0 == seqNo) ? "0" : "1";
			// BUI
			string bui = DecodingOptechTeeth.ConvertRezeptComputeBui(Teeth);
			ret[6] = RezeptComputeBui.ToBuiExpFromRezeptComputeString(bui).ToString();
			// BYOMIE_CODE1
			ret[7] = string.Empty;
			// BYOMEI_NAME1
			ret[8] = string.Empty;
			// BYOMIE_CODE2
			ret[9] = string.Empty;
			// BYOMEI_NAME2
			ret[10] = string.Empty;
			// BYOMIE_CODE3
			ret[11] = string.Empty;
			// BYOMEI_NAME3
			ret[12] = string.Empty;
			if (0 < DiseaseList.Count)
			{
				// BYOMIE_CODE1
				ret[7] = OptechDef.BYOMEI_FIXED_CODE;
				// BYOMEI_NAME1
				ret[8] = DiseaseList[0].Name;
				if (1 < DiseaseList.Count)
				{
					// BYOMIE_CODE2
					ret[9] = OptechDef.BYOMEI_FIXED_CODE;
					// BYOMEI_NAME2
					ret[10] = DiseaseList[1].Name;
					if (2 < DiseaseList.Count)
					{
						// BYOMIE_CODE3
						ret[11] = OptechDef.BYOMEI_FIXED_CODE;
						// BYOMEI_NAME3
						ret[12] = DiseaseList[2].Name;
					}
				}
			}
			return string.Join("\t", ret) + "|";
		}
	}
}
