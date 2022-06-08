//
// MedicalTreatment.cs
//
// オプテックコンバータ用XML 治療行為情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
using CommonLib.BuiData;
using CommonLib.Common;

namespace OptechConvert.XML.MedicalOption
{
	/// <summary>
	/// 治療行為情報
	/// </summary>
	public class MedicalTreatment
	{
		/// <summary>
		/// 治療行為
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Type")]
		public string OptechType { get; set; }

		/// <summary>
		/// 歯式情報
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Teeth")]
		public string Teeth { get; set; }

		/// <summary>
		/// 算定日
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Date")]
		public string Date { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MedicalTreatment()
		{
			OptechType = string.Empty;
			Teeth = string.Empty;
			Date = string.Empty;
		}

		/// <summary>
		/// 算定日の取得
		/// </summary>
		/// <returns></returns>
		public Date GetYMD()
		{
			return DateConversion.ToDate(this.Date);
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
		public static string OutputConvertBuiList(string clinicCode, int pnumber, Date ymd, int daySeq, int seqNo)
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
			ret[5] = "1";
			// BUI
			ret[6] = "";
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
			return string.Join("\t", ret) + "|";
		}

		/// <summary>
		/// CONVERT_SCLISTの出力
		/// </summary>
		/// <param name="clinicCode">医療機関コード</param>
		/// <param name="pnumber">内部患者番号</param>
		/// <param name="ymd">診療日</param>
		/// <param name="daySeq">診療日シリアル番号</param>
		/// <param name="buiSeq">部位シリアル番号</param>
		/// <param name="seqNo">処置シリアル番号</param>
		/// <param name="micCode">MIC保険処置項目コード</param>
		/// <returns></returns>
		public string OutputConvertScList(string clinicCode, int pnumber, Date ymd, int daySeq, int buiSeq, int seqNo, string micCode)
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
			// BUISEQ
			ret[4] = buiSeq.ToString();
			// SEQNO
			ret[5] = seqNo.ToString();
			// CODE
			ret[6] = micCode;
			// NAME
			ret[7] = OptechType;
			// GROUP_FLAG
			ret[8] = "0";
			// CHILDNUM
			ret[9] = "0";
			// SUBBUI
			if (DecodingOptechTeeth.IsNormalTeeth(Teeth))
			{
				string bui = DecodingOptechTeeth.ConvertRezeptComputeBui(Teeth);
				ret[10] = RezeptComputeBui.ToBuiExpFromRezeptComputeString(bui).ToString();
			}
			else
			{
				ret[10] = string.Empty;
			}
			// POINT
			ret[11] = "0";
			// TIMES
			ret[12] = "1";
			return string.Join("\t", ret) + "|";
		}
	}
}
