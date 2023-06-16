//
// 送付先リスト.cs
// 
// 送付先リストExcelファイル読込クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// Ver1.18(2023/04/06 勝呂):19-経理部専用 オンライン資格確認等事業完了報告書 送付先リストから受注日を取得
//
using ClosedXML.Excel;
using System;
using System.Collections.Generic;

namespace VariousDocumentOut.BaseFactory
{
	/// <summary>
	/// 送付先リスト情報
	/// </summary>
	public class 送付先リスト
	{
		/// <summary>
		/// 読込対象シート名
		/// </summary>
		public const string TargetSheetName = "送付先リスト";

		/// <summary>
		/// 送付先リストデータ開始行
		/// </summary>
		public const int StartRow = 2;

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
		/// <param name="startCol">開始カラム</param>
		private void SetData(IXLWorksheet ws, int row)
		{
			得意先No = ws.Cell(row, 1).GetString().Trim();
			顧客名 = ws.Cell(row, 2).GetString().Trim();
			金額 = this.GetMoney(ws.Cell(row, 3));
			受注日 = this.GetDateTime(ws.Cell(row, 4));
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

		/// <summary>
		/// 金額の取得
		/// </summary>
		/// <param name="cell"></param>
		/// <returns>金額</returns>
		private int GetMoney(IXLCell cell)
		{
			if (XLDataType.Number == cell.DataType)
			{
				double price = cell.GetDouble();
				return (int)price;
			}
			return 0;
		}

		/// <summary>
		/// 日付の取得
		/// </summary>
		/// <param name="cell"></param>
		/// <returns>日付文字列</returns>
		private DateTime? GetDateTime(IXLCell cell)
		{
			if (XLDataType.DateTime == cell.DataType)
			{
				return cell.GetDateTime();
			}
			//// 和暦で格納しているので、日付型でなくシリアル値で格納されてしまう
			//if (XLDataType.Number == cell.DataType)
			//{
			//	return DateTime.FromOADate(cell.GetDouble());
			//}
			return null;
		}
	}
}
