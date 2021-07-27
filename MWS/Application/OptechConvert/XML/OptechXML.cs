using OptechConvert.XML.Karte;
using OptechConvert.XML.Ledger;
using OptechConvert.XML.Link;
using OptechConvert.XML.MedicalOption;
using OptechConvert.XML.OralInformation;
using OptechConvert.XML.Patient;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace OptechConvert.XML
{
	public class OptechXML
	{
		/// <summary>
		/// 患者情報
		/// </summary>
		public OptechPatient PatientXML { get; set; }

		/// <summary>
		/// 会計情報
		/// </summary>
		public OptechLedger LedgerXML { get; set; }

		/// <summary>
		/// カルテ情報
		/// </summary>
		public OptechKarte KarteXML { get; set; }

		/// <summary>
		/// 口腔内情報
		/// </summary>
		public OptechOralInformation OralXML { get; set; }

		/// <summary>
		/// 治療情報
		/// </summary>
		public OptechMedicalOption MedicalXML { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OptechXML()
		{
			PatientXML = null;
			LedgerXML = null;
			KarteXML = null;
			OralXML = null;
			MedicalXML = null;
		}

		/// <summary>
		/// XMLコンバートファイルの読込
		/// </summary>
		/// <param name="path">読込先フォルダ名</param>
		public void ReadXmlFile(string path)
		{
			try
			{
				using (FileStream fs = new FileStream(Path.Combine(path, OptechDef.XML_PATIENT_FILENAME), FileMode.Open))
				{
					XmlSerializer s = new XmlSerializer(typeof(OptechPatient));
					PatientXML = (OptechPatient)s.Deserialize(fs);
				}
				using (FileStream fs = new FileStream(Path.Combine(path, OptechDef.XML_LEDGER_FILENAME), FileMode.Open))
				{
					XmlSerializer s = new XmlSerializer(typeof(OptechLedger));
					LedgerXML = (OptechLedger)s.Deserialize(fs);
				}
				using (FileStream fs = new FileStream(Path.Combine(path, OptechDef.XML_KARTE_FILENAME), FileMode.Open))
				{
					XmlSerializer s = new XmlSerializer(typeof(OptechKarte));
					KarteXML = (OptechKarte)s.Deserialize(fs);
					KarteXML.DayList.Sort(OptechConvert.XML.Karte.OptechDay.CompareByDate);
				}
				using (FileStream fs = new FileStream(Path.Combine(path, OptechDef.XML_ORAL_FILENAME), FileMode.Open))
				{
					XmlSerializer s = new XmlSerializer(typeof(OptechOralInformation));
					OralXML = (OptechOralInformation)s.Deserialize(fs);
				}
				using (FileStream fs = new FileStream(Path.Combine(path, OptechDef.XML_MIDICAL_FILENAME), FileMode.Open))
				{
					XmlSerializer s = new XmlSerializer(typeof(OptechMedicalOption));
					MedicalXML = (OptechMedicalOption)s.Deserialize(fs);
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// インポートファイルの作成
		/// </summary>
		/// <param name="link">オプテック識別子→MIC項目コード定義</param>
		/// <param name="clinicCode">医療機関コード</param>
		/// <param name="pnumber">内部患者番号</param>
		/// <param name="patient">患者情報</param>
		/// <param name="hokenInfList">保険情報リスト</param>
		/// <param name="shoshinInfList">初診情報リスト</param>
		/// <param name="rezeptInfList">レセプト情報リスト</param>
		/// <param name="rezeptByomeiranList">レセプト病名欄情報リスト</param>
		/// <param name="dayList">診療日リスト</param>
		/// <param name="buiList">部位リスト</param>
		/// <param name="scList">処置リスト</param>
		public void MakeConvertList(LinkMicItem link, string clinicCode, int pnumber, out string patient, out List<string> hokenInfList, out List<string> shoshinInfList, out List<string> rezeptInfList, out List<string> rezeptByomeiranList, out List<string> dayList, out List<string> buiList, out List<string> scList)
		{
			// 口腔内情報から欠損歯を取得
			string missingTeethStr = string.Empty;
			if (OralXML.Missing.IsNormalTeeth())
			{
				missingTeethStr = OralXML.Missing.ToBuiExpString();
			}
			// 診察料を設定
			KarteXML.SetShinsatsuryo(link, MedicalXML.TreatmentList);

			// インポートファイルの作成（患者情報）
			patient = PatientXML.MakeUserPatient(clinicCode, pnumber);

			// インポートファイルの作成（保険情報）
			hokenInfList = PatientXML.MakeUserHokenInfList(clinicCode, pnumber);

			//インポートファイルの作成（初診情報、レセプト情報、レセプト病名欄情報）
			MedicalXML.MakeConvertList(link, clinicCode, pnumber, missingTeethStr, out shoshinInfList, out rezeptInfList, out rezeptByomeiranList);

			// インポートファイルの作成（診療日情報、部位情報、処置情報）
			KarteXML.MakeConvertList(link, MedicalXML.TreatmentList, clinicCode, pnumber, out dayList, out buiList, out scList);
		}
	}
}
