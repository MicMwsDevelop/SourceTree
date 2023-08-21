//
// MemoWelfare.cs
//
// 厚生局データファイルエクセルデータ
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.03(2023/08/21 勝呂):厚生局データメモ追加機能の追加
// 
using ClosedXML.Excel;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.Common;
using System;

namespace WonderWebEntryMemo.BaseFactory
{
	// yyyy年m月処理分.xlsx
	// 1:顧客No	●
	// 2:新規/更新	●
	// 3:登録理由	●
	// 4:既得意先No
	// 5:ID
	// 6:医院名
	// 7:電話番号
	// 8:医療機関コード
	// 9:郵便番号
	// 10:都道府県
	// 11:開設者
	// 12:住所
	// 13:代表者
	// 14:科目
	// 15:開始日
	// 16:勤務医
	// 17:支店名
	// 18:診療科目
	public class MemoWelfare
	{
		/// <summary>
		/// tMemo.fMemUpdateMan 格納文字列
		/// </summary>
		private const string MemoUpdateManString = "システム管理部";

		/// <summary>
		/// 顧客No
		/// </summary>
		public int 顧客No { get; set; }

		/// <summary>
		/// 新規/更新
		/// </summary>
		public string 新規_更新 { get; set; }

		/// <summary>
		/// 登録理由
		/// </summary>
		public string 登録理由 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MemoWelfare()
		{
			顧客No = 0;
			新規_更新 = string.Empty;
			登録理由 = string.Empty;
		}

		/// <summary>
		/// ワークシートの設定
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		public void ReadWorksheet(IXLWorksheet ws, int row)
		{
			顧客No = (int)Program.GetDouble(ws.Cell(row, 1));
			新規_更新 = ws.Cell(row, 2).GetString();
			登録理由 = ws.Cell(row, 3).GetString();
		}

		/// <summary>
		/// メモ文字列の追加（追加用）
		/// 【新規指定保険医療機関・保険薬局一覧表】
		/// 顧客情報新規対象先
		/// 登録理由：新規
		/// 更新日:2023/07/10
		/// </summary>
		/// <returns>メモ文字列</returns>
		private string GetAddNewMemoString()
		{
			return string.Format("【新規指定保険医療機関・保険薬局一覧表】\r\n顧客情報新規対象先\r\n登録理由：{0}\r\n更新日：{1}"	, 登録理由, Date.Today.GetNormalString());
		}

		/// <summary>
		/// メモ文字列の追加（更新用）
		/// 【新規指定保険医療機関・保険薬局一覧表】
		/// 顧客情報更新対象先
		/// 登録理由：その他
		/// 更新日:2023/07/10
		/// </summary>
		/// <returns>メモ文字列</returns>
		private string GetModifyMemoString()
		{
			return string.Format("【新規指定保険医療機関・保険薬局一覧表】\r\n顧客情報更新対象先\r\n登録理由：{0}\r\n更新日：{1}", 登録理由, Date.Today.GetNormalString());
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
			memo.fMemType = Program.MemoTypeString(MemoUpdateManString);
			if ("新規" == 新規_更新)
			{
				memo.fMemMemo = GetAddNewMemoString();
			}
			else
			{
				memo.fMemMemo = GetModifyMemoString();
			}
			memo.fMemUpdate = DateTime.Now;
			memo.fMemUpdateMan = MemoUpdateManString;
			return memo;
		}
	}
}
