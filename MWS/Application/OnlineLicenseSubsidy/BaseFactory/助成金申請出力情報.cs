using CommonLib.BaseFactory;
using System;
using System.Collections.Generic;

namespace OnlineLicenseSubsidy.BaseFactory
{
	/// <summary>
	/// 補助金申請出力情報
	/// </summary>
	public class 助成金申請出力情報
	{
		public string 受付通番 { get; set; }
		public string 得意先番号 { get; set; }
		public int 顧客No { get; set; }
		public string 顧客名 { get; set; }
		public string 郵便番号 { get; set; }
		public string 住所 { get; set; }
		public string 電話番号 { get; set; }
		public string 開設者 { get; set; }
		public string 医療機関コード { get; set; }
		public DateTime? 工事完了日 { get; set; }
		public List<領収内訳情報> 領収内訳情報List { get; set; }

		/// <summary>
		/// ファイル名称の取得
		/// </summary>
		public string GetFilename
		{
			get
			{
				return string.Format("{0}_{1}", 受付通番, 顧客名);
			}
		}

		/// <summary>
		/// Excelファイル名称の取得
		/// </summary>
		public string GetExcelFilename
		{
			get
			{
				return string.Format("{0}.xlsx", GetFilename);
			}
		}

		/// <summary>
		/// PDFファイル名称の取得
		/// </summary>
		public string GetPdfFilename
		{
			get
			{
				return string.Format("{0}.pdf", GetFilename);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 助成金申請出力情報()
		{
			受付通番 = string.Empty;
			得意先番号 = string.Empty;
			顧客No = 0;
			顧客名 = string.Empty;
			郵便番号 = string.Empty;
			住所 = string.Empty;
			電話番号 = string.Empty;
			開設者 = string.Empty;
			医療機関コード = string.Empty;
			工事完了日 = null;
			領収内訳情報List = new List<領収内訳情報>();
		}

		/// <summary>
		/// 県番号文字列の取得
		/// </summary>
		/// <returns></returns>
		public string GetKenNumberString()
		{
			if (0 == 住所.Length)
			{
				return string.Empty;
			}
			foreach (var ken in KenNumDef.KenString)
			{
				if (KenNumDef.KenNumber.None != ken.Key)
				{
					if (-1 != 住所.IndexOf(ken.Value[0]))
					{
						return ((int)(ken.Key)).ToString();
					}
				}
			}
			return string.Empty;
		}
	}
}
