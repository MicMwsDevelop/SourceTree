//
// MemoBank.cs
//
// 請求書発行先エクセルデータ
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/03/17 勝呂)
// 
using ClosedXML.Excel;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.Common;
using System;

namespace EntryMemo.BaseFactory
{
	// 2022年3月10日締 請求書発行先.xlsx
	// 1:担当支店
	// 2:営業担当者
	// 3:得意先コード	●
	// 4:得意先名
	// 5:繰越金額
	// 6:税込売上高
	// 7:請求金額		●
	// 8:備考			●
	public class MemoBank
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int 顧客No { get; set; }

		/// <summary>
		/// 得意先コード
		/// </summary>
		public string 得意先コード { get; set; }

		/// <summary>
		/// 請求金額
		/// </summary>
		public double 請求金額 { get; set; }

		/// <summary>
		/// 備考
		/// </summary>
		public string 備考 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MemoBank()
		{
			顧客No = 0;
			得意先コード = string.Empty;
			請求金額 = 0;
			備考 = string.Empty;
		}

		/// <summary>
		/// ワークシートの設定
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		public void ReadWorksheet(IXLWorksheet ws, int row)
		{
			得意先コード = ws.Cell(row, 3).GetString();
			請求金額 = Program.GetDouble(ws.Cell(row, 7));
			備考 = ws.Cell(row, 8).GetString();
		}

		/// <summary>
		/// メモ文字列の取得
		/// 【銀行振込請求書発行】
		/// 2022/03/10締  請求額 \15,180
		/// </summary>
		/// <param name="date">締日</param>
		/// <returns>メモ文字列</returns>
		private string GetMemoString(Date date)
		{
			return string.Format("【銀行振込請求書発行】\r\n{0}締  請求額 \\{1}", date.GetNormalString(), StringUtil.CommaEdit(Convert.ToInt32(請求金額)));
		}

		/// <summary>
		/// メモ情報の取得
		/// </summary>
		/// <param name="date">締日</param>
		/// <returns>メモ情報</returns>
		public tMemo GetMemo(Date date)
		{
			tMemo memo = new tMemo();
			memo.fMemKey = 顧客No;
			memo.fMemTable = Program.MemoTableString;
			memo.fMemType = Program.MemoTypeString();
			memo.fMemMemo = this.GetMemoString(date);
			memo.fMemUpdate = DateTime.Now;
			memo.fMemUpdateMan = Program.MemoUpdateManString;
			return memo;
		}
	}
}
