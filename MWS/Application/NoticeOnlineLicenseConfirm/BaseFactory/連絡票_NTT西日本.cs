//
// 連絡票_NTT西日本.cs
//
// NTT西日本 連絡票クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/03/10 勝呂)
// 
using ClosedXML.Excel;
using CommonLib.Common;
using System;

namespace NoticeOnlineLicenseConfirm.BaseFactory
{
	public class 連絡票_NTT西日本
	{
		public string 依頼者 { get; set;}
		public string 依頼日 { get; set; }
		public string NTT通番 { get; set; }
		public string 医療機関名 { get; set; }
		public string 連絡種別 { get; set; }
		public string 連絡項目 { get; set; }
		public string 連絡内容 { get; set; }
		public string 回答日 { get; set; }
		public string 回答内容 { get; set; }
		public string ステータス { get; set; }

		/// <summary>
		/// 依頼日付の取得
		/// B列：依頼日
		/// </summary>
		public Date? 依頼日付
		{
			get
			{
				if (0 < 依頼日.Length)
				{
					DateTime work;
					if (DateTime.TryParse(依頼日, out work))
					{
						return new Date(work);
					}
				}
				return null;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 連絡票_NTT西日本()
		{
			依頼者 = string.Empty;
			依頼日 = string.Empty;
			NTT通番 = string.Empty;
			医療機関名 = string.Empty;
			連絡種別 = string.Empty;
			連絡項目 = string.Empty;
			連絡内容 = string.Empty;
			回答日 = string.Empty;
			回答内容 = string.Empty;
			ステータス = string.Empty;
		}

		/// <summary>
		/// ワークシートの読込
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		public void ReadWorksheet(IXLWorksheet ws, int row)
		{
			依頼者 = ws.Cell(row, 1).GetString();
			依頼日 = Program.GetDateString(ws.Cell(row, 2));
			NTT通番 = ws.Cell(row, 3).GetString();
			医療機関名 = ws.Cell(row, 4).GetString();
			連絡種別 = ws.Cell(row, 5).GetString();
			連絡項目 = ws.Cell(row, 6).GetString();
			連絡内容 = ws.Cell(row, 7).GetString();
			回答日 = Program.GetDateString(ws.Cell(row, 8));
			回答内容 = ws.Cell(row, 9).GetString();
			ステータス = ws.Cell(row, 10).GetString();
		}
	}
}
