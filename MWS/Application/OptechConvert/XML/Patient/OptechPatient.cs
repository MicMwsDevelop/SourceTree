//
// OptechPatient.cs
//
// オプテックコンバータ用XML 患者情報クラス（Patient.xml）
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
using MwsLib.Common;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OptechConvert.XML.Patient
{
	/// <summary>
	/// 患者情報
	/// </summary>
	[System.Xml.Serialization.XmlRoot("Patient")]
	public class OptechPatient
	{
		/// <summary>
		/// カルテ番号
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("KarteNo")]
		public string KarteNo { get; set; }

		/// <summary>
		/// カナ氏名
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Name")]
		public string Name { get; set; }

		/// <summary>
		/// 漢字氏名
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("KanjiName")]
		public string KanjiName { get; set; }

		/// <summary>
		/// 誕生日
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Birthday")]
		public string Birthday { get; set; }

		/// <summary>
		/// 性別
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Sex")]
		public string Sex { get; set; }

		/// <summary>
		/// 郵便番号
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("郵便番号")]
		public string Zipcode { get; set; }

		/// <summary>
		/// 住所
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("住所")]
		public string Address { get; set; }

		/// <summary>
		/// 連絡先電話番号
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("連絡先電話番号")]
		public string Tel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("自宅電話番号")]
		public string HomeTel { get; set; }

		/// <summary>
		/// 主担当医
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("主担当医")]
		public string Doctor { get; set; }

		/// <summary>
		/// 備考
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("備考")]
		public string Marks { get; set; }

		/// <summary>
		/// 統計
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("統計")]
		public string Statistics { get; set; }

		/// <summary>
		/// 保険者リスト
		/// </summary>
		[System.Xml.Serialization.XmlElement("保険者")]
		public List<OptechHokensha> HokehshaList { get; set; }

		/// <summary>
		/// 公費リスト
		/// </summary>
		[System.Xml.Serialization.XmlElement("公費")]
		public List<OptechKohi> KohiList { get; set; }

		/// <summary>
		/// 老人保健リスト
		/// </summary>
		[System.Xml.Serialization.XmlElement("老人保健")]
		public List<OptechRojin> RojinList { get; set; }

		/// <summary>
		/// 性別の取得
		/// </summary>
		public int SexToInt
		{
			get
			{
				if ("Male" == Sex) return 1;
				if ("Female" == Sex) return 2;
				return 0;
			}
		}

		/// <summary>
		/// 担当医番号の取得
		/// </summary>
		public int DoctorNo
		{
			get
			{
				int index = Doctor.IndexOf('_');
				if (-1 != index)
				{
					// 院長_0→0を取り出す
					string drNo = Doctor.Substring(index + 1);
					drNo = Regex.Replace(drNo, @"[^0-9]", "");
					return int.Parse(drNo);
				}
				return 0;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OptechPatient()
		{
			KarteNo = string.Empty;
			Name = string.Empty;
			KanjiName = string.Empty;
			Birthday = string.Empty;
			Sex = string.Empty;
			Zipcode = string.Empty;
			Address = string.Empty;
			Tel = string.Empty;
			HomeTel = string.Empty;
			Doctor = string.Empty;
			Marks = string.Empty;
			Statistics = string.Empty;
			HokehshaList = new List<OptechHokensha>();
			KohiList = new List<OptechKohi>();
			RojinList = new List<OptechRojin>();
		}

		/// <summary>
		/// 漢字氏名の取得
		/// </summary>
		/// <returns></returns>
		public Tuple<string, string> GetName()
		{
			string[] name = KanjiName.Split(' ');
			return new Tuple<string, string>(name[0], name[1]);
		}

		/// <summary>
		/// フリガナの取得
		/// </summary>
		/// <returns></returns>
		public Tuple<string, string> GetPS()
		{
			string[] name = Name.Split(' ');
			return new Tuple<string, string>(name[0], name[1]);
		}

		/// <summary>
		/// 住所の取得
		/// </summary>
		/// <returns></returns>
		public Tuple<string, string> GetAddress()
		{
			string buf1 = string.Empty;
			string buf2 = string.Empty;
			if (OptechDef.HOME_ADDRESS_LENGTH < Address.Length)
			{
				buf1 = StringUtil.Left(Address, OptechDef.HOME_ADDRESS_LENGTH);
				buf2 = StringUtil.Mid(Address, OptechDef.HOME_ADDRESS_LENGTH);
			}
			else
			{
				buf1 = Address;
			}
			return new Tuple<string, string>(buf1, buf2);
		}

		/// <summary>
		/// USER_PATIENTの作成
		/// </summary>
		/// <param name="clinicCode">医療機関コード</param>
		/// <param name="pnumber">内部患者番号</param>
		/// <returns></returns>
		public string MakeUserPatient(string clinicCode, int pnumber)
		{
			string[] ret = new string[55];
			// 医療機関コード
			ret[0] = clinicCode;
			// PNUMBER
			ret[1] = pnumber.ToString();
			// PID
			ret[2] = KarteNo;
			// SEX
			ret[3] = SexToInt.ToString();
			// BIRTHDAY
			ret[4] = (0 < Birthday.Length) ? DateConversion.ToDate(Birthday).ToIntYMD().ToString() : "0";
			// TEL
			ret[5] = Tel;
			// HOME_ZIPCODE
			ret[6] = Zipcode.Replace("-", "");
			// HOME_ADDRESS1
			Tuple<string, string> add = GetAddress();
			ret[7] = add.Item1;
			// HOME_ADDRESS2
			ret[8] = add.Item2;
			// HOME_TEL
			ret[9] = HomeTel;
			// OCCUPATION
			ret[10] = string.Empty;
			// NOTE1
			ret[11] = Marks;
			// NOTE2
			ret[12] = string.Empty;
			// KAKUNINYM
			ret[13] = "0";
			// HALFWAY_STARTDATE
			ret[14] = "0";
			// HALFWAY_LASTDATE
			ret[15] = "0";
			// HALFWAY_KAKARITUKE
			ret[16] = "0";
			// HALFWAY_KESSON
			ret[17] = string.Empty;
			// PRCH_MODIFYDATE
			ret[18] = "0";
			// PRCH_MODIFYDATE_SEQ
			ret[19] = "0";
			// PRCH_LASTDATE
			ret[20] = "0";
			// PRCH_LASTDATE_SEQ
			ret[21] = "0";
			// PRCH_STARTPAGE
			ret[22] = "0";
			// PRCH_STARTLINE
			ret[23] = "0";
			// PRCH_STARTFACE
			ret[24] = "0";
			// PRCH_LAST_TUKISIME
			ret[25] = "0";
			// PRCH_LAST_TUKISIME_SEQ
			ret[26] = "0";
			// PRJIHI_LASTDATE
			ret[27] = "0";
			// PRJIHI_LASTDATE_SEQ
			ret[28] = "0";
			// PRJIHI_STARTPAGE
			ret[29] = "0";
			// PRJIHI_STARTFACE
			ret[30] = "0";
			// PRJIHI_STARTLINE
			ret[31] = "0";
			// KEY_PS
			ret[32] = Name;
			// KEY_LASTDATE
			ret[33] = "0";
			// DISP_PS
			ret[34] = KanjiName;
			// DISP_NAME
			ret[35] = KanjiName;
			// DISP_HOKENTYPE
			ret[36] = "0";
			// RECALL_MONTH
			ret[37] = "0";
			// RECALL_JOKEN_STR1
			ret[38] = string.Empty;
			// RECALL_JOKEN_STR2
			ret[39] = string.Empty;
			// RECALL_JOKEN_STR3
			ret[40] = string.Empty;
			// RECALL_JOKEN_STR4
			ret[41] = string.Empty;
			// RECALL_JOKEN_STR5
			ret[42] = string.Empty;
			// LAST_DOCTOR
			ret[43] = DoctorNo.ToString();
			// CONVERTED
			ret[44] = "0";
			// RECALL_JOGAI
			ret[45] = "0";
			// RECALL_JOKEN_TYPE
			ret[46] = "0";
			// KENSHIN_INTERVAL
			ret[47] = "0";
			// SHOSHIN_SANTEI_TYPE
			ret[48] = "0";
			// HOKEN_LASTDATE
			ret[49] = "0";
			// JIHI_LASTDATE
			ret[50] = "0";
			// USE_KAIGO
			ret[51] = "0";
			// MONSHIN_PID
			ret[52] = "0";
			// NOTPRINT_RECCHART
			ret[53] = "0";
			// LAST_DH
			ret[54] = "0";
			return string.Join("\t", ret) + "|";
		}

		/// <summary>
		/// USER_HOKENINFの作成
		/// </summary>
		/// <param name="clinicCode">医療機関コード</param>
		/// <param name="pnumber">内部患者番号</param>
		/// <returns></returns>
		public List<string> MakeUserHokenInfList(string clinicCode, int pnumber)
		{
			List<string> list = new List<string>();
			foreach (OptechHokensha hokensha in HokehshaList)
			{
				int start = hokensha.LimitStartYMD;
				int end = hokensha.LimitEndYMD;
				OptechKohi kohi = null;
				foreach (OptechKohi kh in KohiList)
				{
					if (kh.IsInclude(start, end))
					{
						kohi = kh;
						break;
					}
				}
				OptechRojin rojin = null;
				foreach (OptechRojin rj in RojinList)
				{
					if (rj.IsInclude(start, end))
					{
						rojin = rj;
						break;
					}
				}
				string str = OutputConvertUserHokenInf(clinicCode, pnumber, hokensha, kohi, rojin);
				list.Add(str);
			}
			return list;
		}

		/// <summary>
		/// USER_HOKENINFの出力
		/// </summary>
		/// <param name="clinicCode">医療機関コード</param>
		/// <param name="pnumber">内部患者番号</param>
		/// <param name="hokensha">保険証情報</param>
		/// <param name="kohi">公費情報</param>
		/// <param name="rojin">老人保健</param>
		/// <returns></returns>
		private string OutputConvertUserHokenInf(string clinicCode, int pnumber, OptechHokensha hokensha, OptechKohi kohi, OptechRojin rojin)
		{
			string[] ret = new string[38];
			// 医療機関コード
			ret[0] = clinicCode;
			// PNUMBER
			ret[1] = pnumber.ToString();
			// STARTYMD
			ret[2] = hokensha.LimitStartYMD.ToString();
			// ENDYMD
			ret[3] = hokensha.LimitEndYMD.ToString();
			// SYSTEM_INSERT
			ret[4] = "0";
			// FAMILY_NM
			Tuple<string, string> name = GetName();
			ret[5] = name.Item1;
			// FIRST_NM
			ret[6] = name.Item2;
			// FAMILY_PS
			Tuple<string, string> ps = GetPS();
			ret[7] = ps.Item1;
			// FIRST_PS
			ret[8] = ps.Item2;
			if (null != hokensha)
			{
				// H_HBANGO
				ret[9] = hokensha.保険者番号;
				// H_KIGO
				ret[10] = hokensha.被保険者記号;
				// JIGYOSHO_SEQNO
				ret[11] = "0";
				// H_BANGO
				ret[12] = hokensha.被保険者番号;
				// KAZOKU
				ret[13] = hokensha.Kazoku.ToString();
				// TOKURYO
				ret[14] = string.Empty;
				// ZOKUGARA
				ret[15] = hokensha.続柄;
				// HIHOKENSHA_PS
				ret[16] = Name;
				// HIHOKENSHA_NAME
				ret[17] = KanjiName;
				// USR_FUTAN
				ret[18] = "-1";
				// SONOTA
				ret[19] = hokensha.Shokumujo.ToString();
				// H_SHIKAKUDATE
				ret[20] = (0 < hokensha.資格取得日.Length) ? DateConversion.ToDate(hokensha.資格取得日).ToIntYMD().ToString() : "0";
				// H_LIMITDATE
				ret[21] = (0 < hokensha.保険有効期限.Length) ? DateConversion.ToDate(hokensha.保険有効期限).ToIntYMD().ToString() : "0";
			}
			else
			{
				// H_HBANGO
				ret[9] = string.Empty;
				// H_KIGO
				ret[10] = string.Empty;
				// JIGYOSHO_SEQNO
				ret[11] = "0";
				// H_BANGO
				ret[12] = string.Empty;
				// KAZOKU
				ret[13] = "0";
				// TOKURYO
				ret[14] = string.Empty;
				// ZOKUGARA
				ret[15] = string.Empty;
				// HIHOKENSHA_PS
				ret[16] = Name;
				// HIHOKENSHA_NAME
				ret[17] = KanjiName;
				// USR_FUTAN
				ret[18] = "-1";
				// SONOTA
				ret[19] = "0";
				// H_SHIKAKUDATE
				ret[20] = "0";
				// H_LIMITDATE
				ret[21] = "0";
			}
			if (null != rojin)
			{
				// R_BANGO
				ret[22] = rojin.市町村番号;
				// R_JUKYU
				ret[23] = rojin.老人受給者番号;
				// R_LIMITDATE
				ret[24] = (0 < rojin.有効期間終了.Length) ? DateConversion.ToDate(rojin.有効期間終了).ToIntYMD().ToString() : "0";
			}
			else
			{
				// R_BANGO
				ret[22] = string.Empty;
				// R_JUKYU
				ret[23] = string.Empty;
				// R_LIMITDATE
				ret[24] = "0";
			}
			if (null != kohi)
			{
				// K_BANGO
				ret[25] = kohi.公費負担者番号;
				// K_JUKYU
				ret[26] = kohi.公費受給者番号;
				// K_LIMITDATE
				ret[27] = (0 < kohi.有効期間終了.Length) ? DateConversion.ToDate(kohi.有効期間終了).ToIntYMD().ToString() : "0";
			}
			else
			{
				// K_BANGO
				ret[25] = string.Empty;
				// K_JUKYU
				ret[26] = string.Empty;
				// K_LIMITDATE
				ret[27] = "0";
			}
			// T_BANGO
			ret[28] = string.Empty;
			// T_LEVY
			ret[29] = "0";
			// T_JOGEN
			ret[30] = "0";
			// T_SJOGEN
			ret[31] = "0";
			// T_YLEVY
			ret[32] = "0";
			// T_YJOGEN
			ret[33] = "0";
			// TOKKI
			ret[34] = "0";
			// ROJIN_KOGAKU
			ret[35] = "0";
			// KOGAKU_TEKIYO_KUBUN
			ret[36] = "0";
			// H_EDABAN
			ret[37] = string.Empty;
			return string.Join("\t", ret) + "|";
		}
	}
}
