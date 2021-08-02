//
// OptechKarte.cs
//
// オプテックコンバータ用XML カルテ情報クラス（Karte.xml）
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
using MwsLib.Common;
using OptechConvert.XML.Link;
using OptechConvert.XML.MedicalOption;
using System.Collections.Generic;

namespace OptechConvert.XML.Karte
{
	/// <summary>
	/// カルテ情報
	/// </summary>
	[System.Xml.Serialization.XmlRoot("Karte")]
	public class OptechKarte
	{
		/// <summary>
		/// カルテ番号
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("KarteNo")]
		public int KarteNo { get; set; }

		/// <summary>
		/// 患者氏名
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Name")]
		public string Name { get; set; }

		/// <summary>
		/// 担当医
		/// </summary>
		[System.Xml.Serialization.XmlAttribute("Doctor")]
		public string Doctor { get; set; }

		/// <summary>
		/// 診療日リスト
		/// </summary>
		[System.Xml.Serialization.XmlElement("Day")]
		public List<OptechDay> DayList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OptechKarte()
		{
			KarteNo = 0;
			Name = string.Empty;
			Doctor = string.Empty;
			DayList = new List<OptechDay>();
		}

		/// <summary>
		/// 診察料を設定
		/// </summary>
		/// <param name="medicalList"></param>
		public void SetShinsatsuryo(LinkMicItem link, List<MedicalTreatment> medicalList)
		{
			foreach (MedicalTreatment medical in medicalList)
			{
				if (link.IsShoshin(medical.OptechType))
				{
					// 初診料
					List<OptechDay> dayList = DayList.FindAll(p => p.Date == medical.Date);
					if (null != dayList && 0 < dayList.Count)
					{
						dayList[0].Shoshin = true;
					}
				}
			}
		}

		/// <summary>
		/// インポートファイルの作成（診療日情報、部位情報、処置情報）
		/// </summary>
		/// <param name="exceptRezeptCheck">レセプトチェックデータを除外する</param>
		/// <param name="link">オプテック識別子→MIC項目コード定義</param>
		/// <param name="treatmentList">治療行為情報</param>
		/// <param name="clinicCode">医療機関コード</param>
		/// <param name="pnumber">内部患者番号</param>
		/// <param name="dayList">診療日情報</param>
		/// <param name="buiList">部位情報</param>
		/// <param name="scList">処置情報</param>
		public void MakeConvertList(bool exceptRezeptCheck, LinkMicItem link, List<MedicalTreatment> treatmentList, string clinicCode, int pnumber, out List<string> dayList, out List<string> buiList, out List<string> scList)
		{
			dayList = new List<string>();
			buiList = new List<string>();
			scList = new List<string>();

			Date prevYMD = Date.Today;
			int daySeq = 0;
			for (int i = 0; i < DayList.Count; i++)
			{
				OptechDay d = DayList[i];
				Date ymd = DateConversion.ToDate(d.Date);
				if (prevYMD != ymd)
				{
					prevYMD = ymd;
					daySeq = 0;
				}
				else
				{
					daySeq++;
				}
				string dayStr = d.OutputConvertDayList(clinicCode, pnumber, daySeq);
				dayList.Add(dayStr);
				for (int j = 0; j < d.BuiList.Count; j++)
				{
					OptechBui b = d.BuiList[j];
					string buiStr = b.OutputConvertBuiList(clinicCode, pnumber, ymd, daySeq, j);
					buiList.Add(buiStr);
					for (int k = 0; k < b.TreatmentList.Count; k++)
					{
						OptechTreatment t = b.TreatmentList[k];
						string scStr = t.OutputConvertScList(clinicCode, pnumber, ymd, daySeq, j, k);
						scList.Add(scStr);
					}
				}
				if (false == exceptRezeptCheck)
				{
					// レセプトチェックデータを追加
					// 治療行為情報から保険処置項目を設定
					List<MedicalTreatment> midicalList = treatmentList.FindAll(p => p.Date == d.Date);
					if (null != midicalList)
					{
						List<string> micList = new List<string>();
						foreach (MedicalTreatment medical in midicalList)
						{
							string micCode = link.GetMicItemCode(medical.OptechType);
							if (0 < micCode.Length)
							{
								micList.Add(medical.OutputConvertScList(clinicCode, pnumber, ymd, daySeq, d.BuiList.Count, micList.Count, micCode));
							}
						}
						if (0 < micList.Count)
						{
							// ダミー部位の追加
							buiList.Add(MedicalTreatment.OutputConvertBuiList(clinicCode, pnumber, ymd, daySeq, d.BuiList.Count));

							// オプテック識別子からMIC保険処置項目を追加
							scList.AddRange(micList);
						}
					}
				}
			}
		}
	}
}
