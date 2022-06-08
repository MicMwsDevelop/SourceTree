//
// MemoOnline.cs
//
// オン資格書類発送先エクセルデータ
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
	// 2022年2月25日オン資格書類発送先.xlsx
	// 1:得意先コード	●
	// 2:得意先名１
	// 3:得意先名２
	// 4:入金日			●
	// 5:入金額			●
	// 6:発送			●
	public class MemoOnline
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
		/// 入金日
		/// </summary>
		public Date? 入金日 { get; set; }

		/// <summary>
		/// 入金額
		/// </summary>
		public double 入金額 { get; set; }

		/// <summary>
		/// 発送
		/// </summary>
		public Date? 発送 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MemoOnline()
		{
			顧客No = 0;
			得意先コード = string.Empty;
			入金日 = null;
			入金額 = 0;
			発送 = null;
		}

		/// <summary>
		/// ワークシートの設定
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		public void ReadWorksheet(IXLWorksheet ws, int row)
		{
			得意先コード = ws.Cell(row, 1).GetString();
			入金日 = Program.GetDate(ws.Cell(row, 4));
			入金額 = Program.GetDouble(ws.Cell(row, 5));
			発送 = Program.GetDate(ws.Cell(row, 6));
		}

		/// <summary>
		/// メモ文字列の取得
		/// 【オン資格補助金申請書類】
		/// 2022/01/27 \429,000 口座振替分の領収証・領収書内訳書・オンライン資格確認事業完了報告書発行→2/25 発送
		/// </summary>
		/// <returns>メモ文字列</returns>
		private string GetMemoString()
		{
			return string.Format("【オン資格補助金申請書類】\r\n{0} \\{1} 口座振替分の領収証・領収書内訳書・オンライン資格確認事業完了報告書発行→{2} 発送"
									, 入金日.Value.GetNormalString()
									, StringUtil.CommaEdit(Convert.ToInt32(入金額))
									, string.Format("{0}/{1}", 発送.Value.Month, 発送.Value.Day));
		}

		/// <summary>
		/// メモ情報の取得
		/// </summary>
		/// <returns>メモ情報</returns>
		public tMemo GetMemo()
		{
			tMemo memo = new tMemo();
			memo.fMemKey = 顧客No;
			memo.fMemTable = Program.MemoTableString;
			memo.fMemType = Program.MemoTypeString();
			memo.fMemMemo = this.GetMemoString();
			memo.fMemUpdate = DateTime.Now;
			memo.fMemUpdateMan = Program.MemoUpdateManString;
			return memo;
		}
	}
}
