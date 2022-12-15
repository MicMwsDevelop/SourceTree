//
// MyNumberCardClinic.cs
// 
// マイナンバーカードの健康保険証利用参加医療機関情報
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.01 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
//
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;

namespace OnlineLicenseProgressEntry.BaseFactory
{
	/// <summary>
	/// マイナンバーカードの健康保険証利用参加医療機関
	/// </summary>
	public class MyNumberCardClinic
	{
		/// <summary>
		/// オンライン資格確認進捗管理ファイル「全顧客」データ開始行
		/// </summary>
		public const int StartRow = 6;

		/// <summary>
		/// 医療機関区分 歯科（病院）
		/// </summary>
		public const string ShikaHospital = "歯科（病院）";

		/// <summary>
		/// 医療機関区分 歯科（診療所）
		/// </summary>
		public const string ShikaClinic = "歯科（診療所）";

		/// <summary>
		/// Excelファイル名称
		/// </summary>
		public string ExcelFileName { get; set; }

		/// <summary>
		/// 医療機関名称
		/// </summary>
		public string ClinicName { get; set; }
		
		/// <summary>
		/// 医療機関コード
		/// </summary>
		public string ClinicCode { get; set; }

		/// <summary>
		/// 電話番号
		/// </summary>
		public string TelNumber { get; set; }

		/// <summary>
		/// オン資運用開始日
		/// </summary>
		public DateTime? OperationStartDate { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MyNumberCardClinic()
		{
			ExcelFileName = string.Empty;
			ClinicName = string.Empty;
			ClinicCode = string.Empty;
			TelNumber = string.Empty;
			OperationStartDate = null;
			CustomerNo = 0;
		}

		/// <summary>
		/// 医療機関区分が歯科かどうか？
		/// </summary>
		/// <param name="kubun">医療機関区分</param>
		/// <returns>判定</returns>
		private static bool IsShika(string kubun)
		{
			return (ShikaHospital == kubun || ShikaClinic == kubun) ? true : false;
		}

		/// <summary>
		/// データの格納
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		public void SetData(IXLWorksheet ws, int row)
		{
			ClinicName = ws.Cell(row, 6).GetString().Trim();
			ClinicCode = ws.Cell(row, 3).GetString().Trim();
			if (6 == ClinicCode.Length)
			{
				ClinicCode = "0" + ClinicCode;
			}
			TelNumber = ws.Cell(row, 8).GetString().Trim();
			string dateStr = Program.GetDateString(ws.Cell(row, 4));
			if (0 < dateStr.Length)
			{
				DateTime work;
				if (DateTime.TryParse(dateStr, out work))
				{
					OperationStartDate = work;
				}
			}
		}

		/// <summary>
		/// マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）エクセルファイルの読込み
		/// </summary>
		/// <param name="pathName"></param>
		/// <param name="clinicList"></param>
		public static void ReadMyNumberCardClinicListExcelFile(string pathName, List<MyNumberCardClinic> clinicList)
		{
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathName, XLEventTracking.Disabled))
				{
					string filename = Path.GetFileName(pathName);

					IXLWorksheet ws = wb.Worksheet(1);
					for (int i = MyNumberCardClinic.StartRow; ; i++)
					{
						if ("" == ws.Cell(i, 1).GetString())
						{
							break;
						}
						if (IsShika(ws.Cell(i, 2).GetString()))
						{
							MyNumberCardClinic clinic = new MyNumberCardClinic();
							clinic.ExcelFileName = filename;
							clinic.SetData(ws, i);
							clinicList.Add(clinic);
						}
					}
				}
			}
			catch
			{
				throw;
			}
		}
	}
}
