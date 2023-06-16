//
// 送付先リスト.cs
// 
// 送付先リストExcelファイル読込クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/04/05 勝呂):新規作成
//
using ClosedXML.Excel;
using System;
using System.Collections.Generic;

namespace OnlineLicenseSubsidyCustomerList.BaseFactory
{
	/// <summary>
	/// 送付先リスト情報
	/// </summary>
	public class 送付先リスト
	{
		/// <summary>
		/// 読込対象シート名
		/// </summary>
		private const string TargetSheetName = "送付先リスト";

		/// <summary>
		/// 送付先リストデータ開始行
		/// </summary>
		private const int StartRow = 2;

		/// <summary>
		/// 得意先No
		/// </summary>
		public string 得意先No { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string 顧客名 { get; set; }

		/// <summary>
		/// 金額
		/// </summary>
		public int 金額 { get; set; }

		/// <summary>
		/// 受注日
		/// </summary>
		public DateTime? 受注日 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 送付先リスト()
		{
			得意先No = string.Empty;
			顧客名 = string.Empty;
			金額 = 0;
			受注日 = null;
		}

		/// <summary>
		/// データの格納
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		private void SetData(IXLWorksheet ws, int row)
		{
			得意先No = ws.Cell(row, 1).GetString().Trim();
			顧客名 = ws.Cell(row, 2).GetString().Trim();
			金額 = Program.GetMoney(ws.Cell(row, 3));
			受注日 = Program.GetDateTime(ws.Cell(row, 4));
		}

		/// <summary>
		/// 送付先リストの読込
		/// </summary>
		/// <param name="pathname">送付先リストファイルパス名</param>
		/// <returns>送付先リスト</returns>
		public static List<送付先リスト> ReadExcel送付先リスト(string pathname)
		{
			List<送付先リスト> list = new List<送付先リスト>();
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					IXLWorksheet ws = wb.Worksheet(送付先リスト.TargetSheetName);
					for (int i = 送付先リスト.StartRow; ; i++)
					{
						if ("" == ws.Cell(i, 1).GetString())
						{
							break;
						}
						送付先リスト data = new 送付先リスト();
						data.SetData(ws, i);
						list.Add(data);
					}
				}
			}
			catch
			{
				throw;
			}
			return list;
		}
	}
}
