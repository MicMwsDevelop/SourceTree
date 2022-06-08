//
// OptechTreatment.cs
//
// オプテックコンバータ用XML 診療行為情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
using CommonLib.BuiData;
using CommonLib.Common;

namespace OptechConvert.XML.Karte
{
	/// <summary>
	/// 診療行為情報
	/// </summary>
	public class OptechTreatment
	{
		/// <summary>
		/// 点数
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Point")]
		public string Point { get; set; }

		/// <summary>
		/// 算定回数
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Count")]
		public string Count { get; set; }

		/// <summary>
		/// 算定部位
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("InnerBui")]
		public string InnerBui { get; set; }

		/// <summary>
		/// 処置名称
		/// </summary>
		[System.Xml.Serialization.XmlText()]
		public string Shochi { get; set; }

		/// <summary>
		/// 処置項目コード
		/// </summary>
		public string ShochiCode
		{
			get
			{
				if (0 == Count.Length)
				{
					// 摘要項目
					return OptechDef.COMMENT_FIXED_NUMBER;
				}
				// 保険処置
				return OptechDef.SHOCHI_FIXED_CODE;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OptechTreatment()
		{
			Point = string.Empty;
			Count = string.Empty;
			InnerBui = string.Empty;
			Shochi = string.Empty;
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
		/// <returns></returns>
		public string OutputConvertScList(string clinicCode, int pnumber, Date ymd, int daySeq, int buiSeq, int seqNo)
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
			ret[6] = ShochiCode;
			// NAME
			ret[7] = Shochi;
			// GROUP_FLAG
			ret[8] = "0";
			// CHILDNUM
			ret[9] = "0";
			// SUBBUI
			if (DecodingOptechTeeth.IsNormalTeeth(InnerBui))
			{
				string bui = DecodingOptechTeeth.ConvertRezeptComputeBui(InnerBui);
				ret[10] = RezeptComputeBui.ToBuiExpFromRezeptComputeString(bui).ToString();
			}
			else
			{
				ret[10] = string.Empty;
			}
			// POINT
			ret[11] = (0 == Point.Length) ? "0" : Point;
			// TIMES
			ret[12] = (0 == Count.Length) ? "0" : Count;
			return string.Join("\t", ret) + "|";
		}
	}
}
