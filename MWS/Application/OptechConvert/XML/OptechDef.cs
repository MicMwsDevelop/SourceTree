//
// OptechDef.cs
//
// オプテックカルテコンバータ定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/20 勝呂)
// 
namespace OptechConvert.XML
{
	public static class OptechDef
	{
		/// <summary>
		/// オプテック識別子→MIC項目コード定義 XMLファイル名
		/// </summary>
		public static readonly string XML_LINKINFO_FILENAME = "LinkMicItem.xml";

		/// <summary>
		/// 患者情報 XMLファイル名
		/// </summary>
		public static readonly string XML_PATIENT_FILENAME = "Patient.xml";

		/// <summary>
		/// カルテ情報 XMLファイル名
		/// </summary>
		public static readonly string XML_KARTE_FILENAME = "Karte.xml";

		/// <summary>
		/// 口腔内情報 XMLファイル名
		/// </summary>
		public static readonly string XML_ORAL_FILENAME = "OralInformation.xml";

		/// <summary>
		/// 治療情報 XMLファイル名
		/// </summary>
		public static readonly string XML_MIDICAL_FILENAME = "MedicalOption.xml";

		/// <summary>
		/// 未収金情報 XMLファイル名
		/// </summary>
		public static readonly string XML_LEDGER_FILENAME = "Ledger.xml";

		/// <summary>
		/// 患者情報 インポートファイル名
		/// </summary>
		public static readonly string USER_PATIENT_FILENAME = "USER_PATIENT.TXT";

		/// <summary>
		/// 保険情報 インポートファイル名
		/// </summary>
		public static readonly string USER_HOKNEINF_FILENAME = "USER_HOKENINF.TXT";

		/// <summary>
		/// 初診情報 インポートファイル名
		/// </summary>
		public static readonly string CONVERT_SHOSHIN_INF_FILENAME = "CONVERT_SHOSHIN_INF.TXT";

		/// <summary>
		/// レセプト情報 インポートファイル名
		/// </summary>
		public static readonly string CONVERT_REZEPT_INF_FILENAME = "CONVERT_REZEPT_INF.TXT";

		/// <summary>
		/// レセプト病名欄情報 インポートファイル名
		/// </summary>
		public static readonly string CONVERT_REZEPT_BYOMEIRAN_INF_FILENAME = "CONVERT_REZEPT_BYOMEIRAN.TXT";

		/// <summary>
		/// 診療日情報 インポートファイル名
		/// </summary>
		public static readonly string CONVERT_DAYLIST_FILENAME = "CONVERT_DAYLIST.TXT";

		/// <summary>
		/// 部位情報 インポートファイル名
		/// </summary>
		public static readonly string CONVERT_BUILIST_FILENAME = "CONVERT_BUILIST.TXT";

		/// <summary>
		/// 処置情報 インポートファイル名
		/// </summary>
		public static readonly string CONVERT_SCLIST_FILENAME = "CONVERT_SCLIST.TXT";

		/// <summary>
		/// オプテックカルテコンバート用 傷病名項目の項目番号
		/// </summary>
		public static readonly string BYOMEI_FIXED_CODE = "B49998";

		/// <summary>
		/// オプテックカルテコンバート用 診療行為情報項目の項目番号
		/// </summary>
		public static readonly string SHOCHI_FIXED_CODE = "H49999";

		/// <summary>
		/// オプテックカルテコンバート用 摘要項目の項目番号
		/// </summary>
		public static readonly string COMMENT_FIXED_NUMBER = "T49999";
		
		/// <summary>
		/// 住所桁数(50文字)
		/// </summary>
		public static readonly int HOME_ADDRESS_LENGTH = 50;

	}
}
