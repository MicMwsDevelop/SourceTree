﻿//
// ProgressExcelRecord.cs
// 
// 進捗管理全顧客情報
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/09/28 勝呂)
//
using ClosedXML.Excel;
using CommonLib.BaseFactory.Sales.Table;
using System;
using System.Collections.Generic;

namespace OnlineLicenseProgressEntry.BaseFactory
{
	/// <summary>
	/// 進捗管理全顧客情報
	/// </summary>
	public class ProgressExcelRecord
	{
		/// <summary>
		/// オンライン資格確認進捗管理ファイル「全顧客」読込対象シート名
		/// </summary>
		public const string TargetSheetName = "全顧客";

		/// <summary>
		/// オンライン資格確認進捗管理ファイル「全顧客」データ開始行
		/// </summary>
		public const int StartRow = 2;

		/// <summary>
		/// 拠点名
		/// </summary>
		public string 拠点名 { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int 顧客No { get; set; }

		/// <summary>
		/// 導入意思
		/// </summary>
		public string 導入意思 { get; set; }

		/// <summary>
		/// オン資担当
		/// </summary>
		public string オン資担当 { get; set; }

		/// <summary>
		/// 都道府県
		/// </summary>
		public string 都道府県 { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string 顧客名 { get; set; }

		/// <summary>
		/// 工事種別
		/// </summary>
		public string 工事種別 { get; set; }

		/// <summary>
		/// ステータス
		/// </summary>
		public string ステータス { get; set; }

		/// <summary>
		/// 現調完了月
		/// </summary>
		public DateTime? 現調完了月 { get; set; }

		/// <summary>
		/// 導入月
		/// </summary>
		public DateTime? 導入月 { get; set; }

		/// <summary>
		/// 部署
		/// </summary>
		public string 部署 { get; set; }

		/// <summary>
		/// 価格帯
		/// </summary>
		public string 価格帯 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ProgressExcelRecord()
		{
			拠点名 = string.Empty;
			顧客No = 0;
			導入意思 = string.Empty;
			オン資担当 = string.Empty;
			都道府県 = string.Empty;
			顧客名 = string.Empty;
			工事種別 = string.Empty;
			ステータス = string.Empty;
			現調完了月 = null;
			導入月 = null;
			部署 = string.Empty;
			価格帯 = string.Empty;
		}

		/// <summary>
		/// データの格納
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		/// <param name="startCol">開始カラム</param>
		private void SetData(IXLWorksheet ws, int row)
		{
			拠点名 = ws.Cell(row, 1).GetString().Trim();
			顧客No = int.Parse(ws.Cell(row, 2).GetString().Trim());
			導入意思 = ws.Cell(row, 3).GetString().Trim();
			オン資担当 = ws.Cell(row, 4).GetString().Trim();
			都道府県 = ws.Cell(row, 5).GetString().Trim();
			顧客名 = ws.Cell(row, 6).GetString().Trim();
			工事種別 = ws.Cell(row, 7).GetString().Trim();
			ステータス = ws.Cell(row, 8).GetString().Trim();
			if ("済" == ws.Cell(row, 9).GetString())
			{
				現調完了月 = new DateTime(9999, 12, 31);
			}
			else
			{
				string dateStr = Program.GetDateString(ws.Cell(row, 9));
				if (0 < dateStr.Length)
				{
					DateTime work;
					if (DateTime.TryParse(dateStr, out work))
					{
						現調完了月 = work;
					}
				}
			}
			if ("済" == ws.Cell(row, 10).GetString())
			{
				導入月 = new DateTime(9999, 12, 31);
			}
			else
			{
				string dateStr = Program.GetDateString(ws.Cell(row, 10));
				if (0 < dateStr.Length)
				{
					DateTime work;
					if (DateTime.TryParse(dateStr, out work))
					{
						導入月 = work;
					}
				}
			}
			部署 = ws.Cell(row, 11).GetString().Trim();
			価格帯 = ws.Cell(row, 12).GetString().Trim();
		}

		/// <summary>
		/// 進捗管理ファイルの読込
		/// </summary>
		/// <param name="pathname">進捗管理ファイルパス名</param>
		/// <returns>NTT東日本進捗管理表リスト</returns>
		public static List<ProgressExcelRecord> ReadProgressExcelFile(string pathname)
		{
			List<ProgressExcelRecord> list = new List<ProgressExcelRecord>();
			try
			{
				using (XLWorkbook wb = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					IXLWorksheet ws = wb.Worksheet(ProgressExcelRecord.TargetSheetName);
					for (int i = ProgressExcelRecord.StartRow; ; i++)
					{
						if ("" == ws.Cell(i, 1).GetString())
						{
							break;
						}
						ProgressExcelRecord data = new ProgressExcelRecord();
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
		/// 進捗管理全顧客情報を[SalesDB].[dbo].[オンライン資格確認進捗管理情報]に変換する
		/// </summary>
		/// <param name="progressList">顧客進捗管理リスト</param>
		/// <returns>[SalesDB].[dbo].[オンライン資格確認進捗管理情報]</returns>
		public static List<オンライン資格確認進捗管理情報> GetOnlineIntroductionStatusList(List<ProgressExcelRecord> progressList)
		{
			List<オンライン資格確認進捗管理情報> ret = new List<オンライン資格確認進捗管理情報>();
			foreach (ProgressExcelRecord progress in progressList)
			{
				オンライン資格確認進捗管理情報 online = new オンライン資格確認進捗管理情報();
				online.顧客No = progress.顧客No;
				online.拠点名 = progress.拠点名;
				online.顧客名 = progress.顧客名;
				online.オン資担当 = progress.オン資担当;
				online.導入意思 = progress.導入意思;
				online.工事種別 = progress.工事種別;
				online.ステータス = progress.ステータス;
				online.現調完了月 = progress.現調完了月;
				online.導入月 = progress.導入月;
				online.都道府県 = progress.都道府県;
				online.部署 = progress.部署;
				online.価格帯 = progress.価格帯;
				ret.Add(online);
			}
			return ret;
		}
	}
}