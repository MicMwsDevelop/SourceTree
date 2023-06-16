//
// オン資補助金申請書類顧客情報.cs
// 
// オン資補助金申請書類顧客情報 ExcelファイルI/Oクラス
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
	public class オン資補助金申請書類顧客情報
	{
		/// <summary>
		/// 読込対象シート名
		/// </summary>
		private const string TargetSheetName = "顧客情報";

		/// <summary>
		/// 送付先リストデータ開始行
		/// </summary>
		private const int StartRow = 3;

		/// <summary>
		/// 得意先No
		/// </summary>
		public string 得意先No { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string 顧客名 { get; set; }

		/// <summary>
		/// 医療機関コード
		/// </summary>
		public string 医療機関コード { get; set; }

		/// <summary>
		/// 開設者
		/// </summary>
		public string 開設者 { get; set; }

		/// <summary>
		/// 郵便番号
		/// </summary>
		public string 郵便番号 { get; set; }

		/// <summary>
		/// 住所
		/// </summary>
		public string 住所 { get; set; }

		/// <summary>
		/// 電話番号
		/// </summary>
		public string 電話番号 { get; set; }

		/// <summary>
		/// 受注日
		/// </summary>
		public DateTime? 受注日 { get; set; }

		/// <summary>
		/// 金額
		/// </summary>
		public int 金額 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public オン資補助金申請書類顧客情報()
		{
			得意先No = string.Empty;
			顧客名 = string.Empty;
			医療機関コード = string.Empty;
			開設者 = string.Empty;
			郵便番号 = string.Empty;
			住所 = string.Empty;
			電話番号 = string.Empty;
			受注日 = null;
			金額 = 0;
		}

		/// <summary>
		/// データの格納
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		private void SetData(IXLWorksheet ws, int row)
		{
			得意先No = ws.Cell(row, 1).GetString().Trim();
		}

		/// <summary>
		/// オン資補助金申請書類顧客情報.xlsxの読込
		/// </summary>
		/// <param name="pathname">オン資補助金申請書類顧客情報ファイルパス名</param>
		/// <returns>オン資補助金申請書類顧客情報</returns>
		public static List<オン資補助金申請書類顧客情報> ReadExcelオン資補助金申請書類顧客情報(string pathname)
		{
			List<オン資補助金申請書類顧客情報> list = new List<オン資補助金申請書類顧客情報>();
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					IXLWorksheet ws = wb.Worksheet(オン資補助金申請書類顧客情報.TargetSheetName);
					for (int i = オン資補助金申請書類顧客情報.StartRow; ; i++)
					{
						if ("" == ws.Cell(i, 1).GetString())
						{
							break;
						}
						オン資補助金申請書類顧客情報 data = new オン資補助金申請書類顧客情報();
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
		/// オン資補助金申請書類顧客情報.xlsxの出力
		/// </summary>
		/// <param name="customerList">オン資補助金申請書類顧客情報</param>
		/// <param name="pathname">オン資補助金申請書類顧客情報.xlsx</param>
		public static void WriteExcelオン資補助金申請書類顧客情報(List<オン資補助金申請書類顧客情報> customerList, string pathname)
		{
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					IXLWorksheet ws = wb.Worksheet(オン資補助金申請書類顧客情報.TargetSheetName);
					for (int i = オン資補助金申請書類顧客情報.StartRow; ; i++)
					{
						string target = ws.Cell(i, 1).GetString();
						if (0 == target.Length)
						{
							break;
						}
						オン資補助金申請書類顧客情報 cust = customerList.Find(p => p.得意先No == target);
						if (null != cust)
						{
							ws.Cell(i, 2).SetValue(cust.顧客名);
							ws.Cell(i, 3).SetValue(cust.医療機関コード);
							ws.Cell(i, 4).SetValue(cust.開設者);
							ws.Cell(i, 5).SetValue(cust.郵便番号);
							ws.Cell(i, 6).SetValue(cust.住所);
							ws.Cell(i, 7).SetValue(cust.電話番号);
							ws.Cell(i, 8).SetValue(cust.受注日);
							ws.Cell(i, 9).SetValue(cust.金額);
						}
					}
					// Excelファイルの保存
					wb.Save();
				}
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("WriteExcelオン資補助金申請書類顧客情報({0})", ex.Message));
			}
		}

		/// <summary>
		/// 得意先No Where文文字列の取得
		/// </summary>
		/// <param name="list">オン資補助金申請書類顧客情報リスト</param>
		/// <returns>Where文文字列</returns>
		public static string GetNumberString(List<オン資補助金申請書類顧客情報> list)
		{
			List<string> ret = new List<string>();
			foreach (オン資補助金申請書類顧客情報 cust in list)
			{
				ret.Add(string.Format("'{0}'", cust.得意先No));
			}
			string[] array = ret.ToArray();
			return String.Join(",", array);
		}
	}
}
