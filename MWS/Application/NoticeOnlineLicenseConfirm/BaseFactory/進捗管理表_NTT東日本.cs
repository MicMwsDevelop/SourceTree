//
// 進捗管理表_NTT東日本.cs
//
// NTT東日本 進捗管理表データ
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/03/08 勝呂)
// 
using ClosedXML.Excel;
using CommonLib.Common;
using System;
using System.Collections.Generic;

namespace NoticeOnlineLicenseConfirm.BaseFactory
{
	public class 進捗管理表_NTT東日本
	{
		/// <summary>
		/// NTT東日本 進捗管理表バージョン番号
		/// </summary>
		public static int Verion = 100;

		/// <summary>
		/// 通知情報
		/// </summary>
		public NoticeInfo Notice { get; set; }

		/// <summary>
		/// ミック様 記入欄
		/// </summary>
		public string 受付通番 { get; set; }
		public string 申込日 { get; set; }
		public int 病院ID { get; set; }
		public string 医療機関名 { get; set; }

		/// <summary>
		/// NTT基本事項
		/// </summary>
		public string 更新日_NTT { get; set; }
		public string 受付業務開始日 { get; set; }
		public string 一元受付ステータス { get; set; }
		public string 回線調査ステータス { get; set; }
		public string 日程調整ステータス { get; set; }
		public string 入館調整ステータス { get; set; }
		public string 進捗管理ステータス { get; set; }
		public string フレッツ新規手配 { get; set; }

		/// <summary>
		/// NTTオン資調査 NTT都合
		/// </summary>
		public string 事前調査確定日 { get; set; }
		public string 事前調査確定時間 { get; set; }
		public string 事前調査結果 { get; set; }
		public string 事前調査結果詳細_調査NG時 { get; set; }
		public string 事前調査確定日_過去日 { get; set; }
		public string 備考_調査関連 { get; set; }

		/// <summary>
		/// NTTオン資受諾工事
		/// </summary>
		public string 工事確定日 { get; set; }
		public string 工事確定時間 { get; set; }
		public string 工事結果 { get; set; }
		public string 工事結果詳細_工事NG時 { get; set; }
		public string 工事確定日_過去日 { get; set; }
		public string 備考_工事関連 { get; set; }

		/// <summary>
		/// NTT補助金申請関連
		/// </summary>
		public string 作業報告書_PDF_送付月25日締め_NTT_ミック { get; set; }
		public string 工事基本額単価 { get; set; }
		public string 工事基本額数量 { get; set; }
		public string 工事基本額小計 { get; set; }
		public string 平日夜間等割増料金単価 { get; set; }
		public string 平日夜間等割増料金数量 { get; set; }
		public string 平日夜間等割増料金小計 { get; set; }
		public string 再派遣料金単価 { get; set; }
		public string 再派遣料金数量 { get; set; }
		public string 再派遣料金小計 { get; set; }
		public string 平日夜間等再派遣料金単価 { get; set; }
		public string 平日夜間等再派遣料金数量 { get; set; }
		public string 平日夜間等再派遣料金小計 { get; set; }
		public string 規定後リスケ料金単価 { get; set; }
		public string 規定後リスケ料金数量 { get; set; }
		public string 規定後リスケ料金小計 { get; set; }
		public string 平日夜間等規定後リスケ料金単価 { get; set; }
		public string 平日夜間等規定後リスケ料金数量 { get; set; }
		public string 平日夜間等規定後リスケ料金小計 { get; set; }
		public string 作業キャンセル料単価 { get; set; }
		public string 作業キャンセル料数量 { get; set; }
		public string 作業キャンセル料小計 { get; set; }
		public string 平日夜間等作業キャンセル料単価 { get; set; }
		public string 平日夜間等作業キャンセル料数量 { get; set; }
		public string 平日夜間等作業キャンセル料小計 { get; set; }
		public string 作業延長料金_30分毎_単価 { get; set; }
		public string 作業延長料金_30分毎_数量 { get; set; }
		public string 作業延長料金_30分毎_小計 { get; set; }
		public string 離島における交通費小計 { get; set; }
		public string 機器料金小計 { get; set; }
		public string 小計 { get; set; }
		public string 消費税額 { get; set; }
		public string 合計_税込 { get; set; }
		public string 補助金申請書類送付日_NTT_ミック { get; set; }

		/// <summary>
		/// NTT基本事項
		/// </summary>
		public string 完了フラグ_拠点毎 { get; set; }
		public string NTT備考欄 { get; set; }

		/// <summary>
		/// NTT(ヒアリングシート修正連絡)
		/// </summary>
		public string 本日の更新分 { get; set; }
		public string 回答結果1 { get; set; }
		public string 修正箇所1 { get; set; }
		public string 回答結果2 { get; set; }
		public string 修正箇所2 { get; set; }

		/// <summary>
		/// 進捗管理表フィールド番号定義(Ver1.00)
		/// </summary>
		public static readonly Dictionary<string, int> FieldNumber100 = new Dictionary<string, int>()
		{
			{ "受付通番", 1 },
			{ "申込日", 2 },
			{ "病院ID", 3 },
			{ "医療機関名", 4 },
			{ "更新日_NTT", 5 },
			{ "受付業務開始日", 6 },
			{ "一元受付ステータス", 7 },
			{ "回線調査ステータス", 8 },
			{ "日程調整ステータス", 9 },
			{ "入館調整ステータス", 10 },
			{ "進捗管理ステータス", 11 },
			{ "フレッツ新規手配", 12 },
			{ "事前調査確定日", 13 },
			{ "事前調査確定時間", 14 },
			{ "事前調査結果", 15 },
			{ "事前調査結果詳細_調査NG時", 16 },
			{ "事前調査確定日_過去日", 17 },
			{ "備考_調査関連", 18 },
			{ "工事確定日", 19 },
			{ "工事確定時間", 20 },
			{ "工事結果", 21 },
			{ "工事結果詳細_工事NG時", 22 },
			{ "工事確定日_過去日", 23 },
			{ "備考_工事関連", 24 },
			{ "作業報告書_PDF_送付月25日締め_NTT_ミック", 25 },
			{ "工事基本額単価", 26 },
			{ "工事基本額数量", 27 },
			{ "工事基本額小計", 28 },
			{ "平日夜間等割増料金単価", 29 },
			{ "平日夜間等割増料金数量", 30 },
			{ "平日夜間等割増料金小計", 31 },
			{ "再派遣料金単価", 32 },
			{ "再派遣料金数量", 33 },
			{ "再派遣料金小計", 34 },
			{ "平日夜間等再派遣料金単価", 35 },
			{ "平日夜間等再派遣料金数量", 36 },
			{ "平日夜間等再派遣料金小計", 37 },
			{ "規定後リスケ料金単価", 38 },
			{ "規定後リスケ料金数量", 39 },
			{ "規定後リスケ料金小計", 40 },
			{ "平日夜間等規定後リスケ料金単価", 41 },
			{ "平日夜間等規定後リスケ料金数量", 42 },
			{ "平日夜間等規定後リスケ料金小計", 43 },
			{ "作業キャンセル料単価", 44 },
			{ "作業キャンセル料数量", 45 },
			{ "作業キャンセル料小計", 46 },
			{ "平日夜間等作業キャンセル料単価", 47 },
			{ "平日夜間等作業キャンセル料数量", 48 },
			{ "平日夜間等作業キャンセル料小計", 49 },
			{ "作業延長料金_30分毎_単価", 50 },
			{ "作業延長料金_30分毎_数量", 51 },
			{ "作業延長料金_30分毎_小計", 52 },
			{ "離島における交通費小計", 53 },
			{ "機器料金小計", 54 },
			{ "小計", 55 },
			{ "消費税額", 56 },
			{ "合計_税込", 57 },
			{ "補助金申請書類送付日_NTT_ミック", 58 },
			{ "完了フラグ_拠点毎", 59 },
			{ "NTT備考欄", 60 },
			{ "本日の更新分", 61 },
			{ "回答結果1", 62 },
			{ "修正箇所1", 63 },
			{ "回答結果2", 64 },
			{ "修正箇所2", 65 },
		};

		/// <summary>
		/// 工事確定日付の取得
		/// S列：工事確定日
		/// </summary>
		public Date? 工事確定日付
		{
			get
			{
				if (0 < 工事確定日.Length)
				{
					DateTime work;
					if (DateTime.TryParse(工事確定日, out work))
					{
						return new Date(work);
					}
				}
				return null;
			}
		}

		/// <summary>
		/// 本日の更新分日付の取得
		/// BI列：本日の更新分
		/// </summary>
		public Date? 本日の更新分日付
		{
			get
			{
				if (0 < 本日の更新分.Length)
				{
					DateTime work;
					if (DateTime.TryParse(本日の更新分, out work))
					{
						return new Date(work);
					}
				}
				return null;
			}
		}

		/// <summary>
		/// 回答結果1がNGかどうか？
		/// BJ列：回答結果1
		/// </summary>
		public bool 回答結果1_NG
		{
			get
			{
				return ("NG" == 回答結果1) ? true : false;
			}
		}

		/// <summary>
		/// 回答結果2がNGかどうか？
		/// BL列：回答結果1
		/// </summary>
		public bool 回答結果2_NG
		{
			get
			{
				return ("NG" == 回答結果2) ? true : false;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 進捗管理表_NTT東日本()
		{
			Notice = new NoticeInfo();
			Clear();
		}

		/// <summary>
		/// 進捗管理表バージョン番号に対応するフィールド番号を取得
		/// </summary>
		/// <param name="fieldName">フィールド名</param>
		/// <param name="version">進捗管理表バージョン番号</param>
		/// <returns></returns>
		public int GetFieldNumber(string fieldName, int version)
		{
			switch (version)
			{
				case 100: return FieldNumber100[fieldName];
			}
			return 0;
		}

		/// <summary>
		/// クリア
		/// </summary>
		public void Clear()
		{
			Notice.Clear();
			受付通番 = string.Empty;
			申込日 = string.Empty;
			病院ID = 0;
			医療機関名 = string.Empty;
			更新日_NTT = string.Empty;
			受付業務開始日 = string.Empty;
			一元受付ステータス = string.Empty;
			回線調査ステータス = string.Empty;
			日程調整ステータス = string.Empty;
			入館調整ステータス = string.Empty;
			進捗管理ステータス = string.Empty;
			フレッツ新規手配 = string.Empty;
			事前調査確定日 = string.Empty;
			事前調査確定時間 = string.Empty;
			事前調査結果 = string.Empty;
			事前調査結果詳細_調査NG時 = string.Empty;
			事前調査確定日_過去日 = string.Empty;
			備考_調査関連 = string.Empty;
			工事確定日 = string.Empty;
			工事確定時間 = string.Empty;
			工事結果 = string.Empty;
			工事結果詳細_工事NG時 = string.Empty;
			工事確定日_過去日 = string.Empty;
			備考_工事関連 = string.Empty;
			作業報告書_PDF_送付月25日締め_NTT_ミック = string.Empty;
			工事基本額単価 = string.Empty;
			工事基本額数量 = string.Empty;
			工事基本額小計 = string.Empty;
			平日夜間等割増料金単価 = string.Empty;
			平日夜間等割増料金数量 = string.Empty;
			平日夜間等割増料金小計 = string.Empty;
			再派遣料金単価 = string.Empty;
			再派遣料金数量 = string.Empty;
			再派遣料金小計 = string.Empty;
			平日夜間等再派遣料金単価 = string.Empty;
			平日夜間等再派遣料金数量 = string.Empty;
			平日夜間等再派遣料金小計 = string.Empty;
			規定後リスケ料金単価 = string.Empty;
			規定後リスケ料金数量 = string.Empty;
			規定後リスケ料金小計 = string.Empty;
			平日夜間等規定後リスケ料金単価 = string.Empty;
			平日夜間等規定後リスケ料金数量 = string.Empty;
			平日夜間等規定後リスケ料金小計 = string.Empty;
			作業キャンセル料単価 = string.Empty;
			作業キャンセル料数量 = string.Empty;
			作業キャンセル料小計 = string.Empty;
			平日夜間等作業キャンセル料単価 = string.Empty;
			平日夜間等作業キャンセル料数量 = string.Empty;
			平日夜間等作業キャンセル料小計 = string.Empty;
			作業延長料金_30分毎_単価 = string.Empty;
			作業延長料金_30分毎_数量 = string.Empty;
			作業延長料金_30分毎_小計 = string.Empty;
			離島における交通費小計 = string.Empty;
			機器料金小計 = string.Empty;
			小計 = string.Empty;
			消費税額 = string.Empty;
			合計_税込 = string.Empty;
			補助金申請書類送付日_NTT_ミック = string.Empty;
			完了フラグ_拠点毎 = string.Empty;
			NTT備考欄 = string.Empty;
			本日の更新分 = string.Empty;
			回答結果1 = string.Empty;
			修正箇所1 = string.Empty;
			回答結果2 = string.Empty;
			修正箇所2 = string.Empty;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string[] GetData()
		{
			List<string> ret = new List<string>();
			ret.AddRange(Notice.GetData());
			ret.Add(受付通番);
			ret.Add(申込日);
			ret.Add(病院ID.ToString());
			ret.Add(医療機関名);
			ret.Add(更新日_NTT);
			ret.Add(受付業務開始日);
			ret.Add(一元受付ステータス);
			ret.Add(回線調査ステータス);
			ret.Add(日程調整ステータス);
			ret.Add(入館調整ステータス);
			ret.Add(進捗管理ステータス);
			ret.Add(フレッツ新規手配);
			ret.Add(事前調査確定日);
			ret.Add(事前調査確定時間);
			ret.Add(事前調査結果);
			ret.Add(事前調査結果詳細_調査NG時);
			ret.Add(事前調査確定日_過去日);
			ret.Add(備考_調査関連);
			ret.Add(工事確定日);
			ret.Add(工事確定時間);
			ret.Add(工事結果);
			ret.Add(工事結果詳細_工事NG時);
			ret.Add(工事確定日_過去日);
			ret.Add(備考_工事関連);
			ret.Add(作業報告書_PDF_送付月25日締め_NTT_ミック);
			ret.Add(工事基本額単価);
			ret.Add(工事基本額数量);
			ret.Add(工事基本額小計);
			ret.Add(平日夜間等割増料金単価);
			ret.Add(平日夜間等割増料金数量);
			ret.Add(平日夜間等割増料金小計);
			ret.Add(再派遣料金単価);
			ret.Add(再派遣料金数量);
			ret.Add(再派遣料金小計);
			ret.Add(平日夜間等再派遣料金単価);
			ret.Add(平日夜間等再派遣料金数量);
			ret.Add(平日夜間等再派遣料金小計);
			ret.Add(規定後リスケ料金単価);
			ret.Add(規定後リスケ料金数量);
			ret.Add(規定後リスケ料金小計);
			ret.Add(平日夜間等規定後リスケ料金単価);
			ret.Add(平日夜間等規定後リスケ料金数量);
			ret.Add(平日夜間等規定後リスケ料金小計);
			ret.Add(作業キャンセル料単価);
			ret.Add(作業キャンセル料数量);
			ret.Add(作業キャンセル料小計);
			ret.Add(平日夜間等作業キャンセル料単価);
			ret.Add(平日夜間等作業キャンセル料数量);
			ret.Add(平日夜間等作業キャンセル料小計);
			ret.Add(作業延長料金_30分毎_単価);
			ret.Add(作業延長料金_30分毎_数量);
			ret.Add(作業延長料金_30分毎_小計);
			ret.Add(離島における交通費小計);
			ret.Add(機器料金小計);
			ret.Add(小計);
			ret.Add(消費税額);
			ret.Add(合計_税込);
			ret.Add(補助金申請書類送付日_NTT_ミック);
			ret.Add(完了フラグ_拠点毎);
			ret.Add(NTT備考欄);
			ret.Add(本日の更新分);
			ret.Add(回答結果1);
			ret.Add(修正箇所1);
			ret.Add(回答結果2);
			ret.Add(修正箇所2);
			return ret.ToArray();
		}

		/// <summary>
		/// ワークシートの読込定(進捗管理表)
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		/// <param name="version">進捗管理表バージョン番号</param>
		public void SetWorksheetBy進捗管理表(IXLWorksheet ws, int row, int version)
		{
			ReadWorksheet(ws, row, version, 0);
		}

		/// <summary>
		/// ワークシートの読込(オンライン資格確認通知結果)
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		/// <param name="version">進捗管理表バージョン番号</param>
		public void ReadWorksheetByオンライン資格確認通知結果(IXLWorksheet ws, int row, int version)
		{
			Notice.ReadWorksheet(ws, row);
			ReadWorksheet(ws, row, version, Notice.GetColumn); 
		}

		/// <summary>
		/// ワークシートの読込
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		/// <param name="version">進捗管理表バージョン番号</param>
		/// <param name="startCol">開始カラム</param>
		private void ReadWorksheet(IXLWorksheet ws, int row, int version, int startCol)
		{
			受付通番 = ws.Cell(row, GetFieldNumber("受付通番", version) + startCol).GetString();
			申込日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("申込日", version) + startCol));
			病院ID = ws.Cell(row, GetFieldNumber("病院ID", version) + startCol).GetString().ToInt();
			医療機関名 = ws.Cell(row, GetFieldNumber("医療機関名", version) + startCol).GetString();
			更新日_NTT = Program.GetDateString(ws.Cell(row, GetFieldNumber("更新日_NTT", version) + startCol));
			受付業務開始日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("受付業務開始日", version) + startCol));
			一元受付ステータス = ws.Cell(row, GetFieldNumber("一元受付ステータス", version) + startCol).GetString();
			回線調査ステータス = ws.Cell(row, GetFieldNumber("回線調査ステータス", version) + startCol).GetString();
			日程調整ステータス = ws.Cell(row, GetFieldNumber("日程調整ステータス", version) + startCol).GetString();
			入館調整ステータス = ws.Cell(row, GetFieldNumber("入館調整ステータス", version) + startCol).GetString();
			進捗管理ステータス = ws.Cell(row, GetFieldNumber("進捗管理ステータス", version) + startCol).GetString();
			フレッツ新規手配 = ws.Cell(row, GetFieldNumber("フレッツ新規手配", version) + startCol).GetString();
			事前調査確定日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("事前調査確定日", version) + startCol));
			事前調査確定時間 = Program.GetTimeString(ws.Cell(row, GetFieldNumber("事前調査確定時間", version) + startCol));
			事前調査結果 = ws.Cell(row, GetFieldNumber("事前調査結果", version) + startCol).GetString();
			事前調査結果詳細_調査NG時 = ws.Cell(row, GetFieldNumber("事前調査結果詳細_調査NG時", version) + startCol).GetString();
			事前調査確定日_過去日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("事前調査確定日_過去日", version) + startCol));
			備考_調査関連 = ws.Cell(row, GetFieldNumber("備考_調査関連", version) + startCol).GetString();
			工事確定日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("工事確定日", version) + startCol));
			工事確定時間 = Program.GetTimeString(ws.Cell(row, GetFieldNumber("工事確定時間", version) + startCol));
			工事結果 = ws.Cell(row, GetFieldNumber("工事結果", version) + startCol).GetString();
			工事結果詳細_工事NG時 = ws.Cell(row, GetFieldNumber("工事結果詳細_工事NG時", version) + startCol).GetString();
			工事確定日_過去日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("工事確定日_過去日", version) + startCol));
			備考_工事関連 = ws.Cell(row, GetFieldNumber("備考_工事関連", version) + startCol).GetString();
			作業報告書_PDF_送付月25日締め_NTT_ミック = ws.Cell(row, GetFieldNumber("作業報告書_PDF_送付月25日締め_NTT_ミック", version) + startCol).GetString();
			工事基本額単価 = ws.Cell(row, GetFieldNumber("工事基本額単価", version) + startCol).GetString();
			工事基本額数量 = ws.Cell(row, GetFieldNumber("工事基本額数量", version) + startCol).GetString();
			工事基本額小計 = ws.Cell(row, GetFieldNumber("工事基本額小計", version) + startCol).GetString();
			平日夜間等割増料金単価 = ws.Cell(row, GetFieldNumber("平日夜間等割増料金単価", version) + startCol).GetString();
			平日夜間等割増料金数量 = ws.Cell(row, GetFieldNumber("平日夜間等割増料金数量", version) + startCol).GetString();
			平日夜間等割増料金小計 = ws.Cell(row, GetFieldNumber("平日夜間等割増料金小計", version) + startCol).GetString();
			再派遣料金単価 = ws.Cell(row, GetFieldNumber("再派遣料金単価", version) + startCol).GetString();
			再派遣料金数量 = ws.Cell(row, GetFieldNumber("再派遣料金数量", version) + startCol).GetString();
			再派遣料金小計 = ws.Cell(row, GetFieldNumber("再派遣料金小計", version) + startCol).GetString();
			平日夜間等再派遣料金単価 = ws.Cell(row, GetFieldNumber("平日夜間等再派遣料金単価", version) + startCol).GetString();
			平日夜間等再派遣料金数量 = ws.Cell(row, GetFieldNumber("平日夜間等再派遣料金数量", version) + startCol).GetString();
			平日夜間等再派遣料金小計 = ws.Cell(row, GetFieldNumber("平日夜間等再派遣料金小計", version) + startCol).GetString();
			規定後リスケ料金単価 = ws.Cell(row, GetFieldNumber("規定後リスケ料金単価", version) + startCol).GetString();
			規定後リスケ料金数量 = ws.Cell(row, GetFieldNumber("規定後リスケ料金数量", version) + startCol).GetString();
			規定後リスケ料金小計 = ws.Cell(row, GetFieldNumber("規定後リスケ料金小計", version) + startCol).GetString();
			平日夜間等規定後リスケ料金単価 = ws.Cell(row, GetFieldNumber("平日夜間等規定後リスケ料金単価", version) + startCol).GetString();
			平日夜間等規定後リスケ料金数量 = ws.Cell(row, GetFieldNumber("平日夜間等規定後リスケ料金数量", version) + startCol).GetString();
			平日夜間等規定後リスケ料金小計 = ws.Cell(row, GetFieldNumber("平日夜間等規定後リスケ料金小計", version) + startCol).GetString();
			作業キャンセル料単価 = ws.Cell(row, GetFieldNumber("作業キャンセル料単価", version) + startCol).GetString();
			作業キャンセル料数量 = ws.Cell(row, GetFieldNumber("作業キャンセル料数量", version) + startCol).GetString();
			作業キャンセル料小計 = ws.Cell(row, GetFieldNumber("作業キャンセル料小計", version) + startCol).GetString();
			平日夜間等作業キャンセル料単価 = ws.Cell(row, GetFieldNumber("平日夜間等作業キャンセル料単価", version) + startCol).GetString();
			平日夜間等作業キャンセル料数量 = ws.Cell(row, GetFieldNumber("平日夜間等作業キャンセル料数量", version) + startCol).GetString();
			平日夜間等作業キャンセル料小計 = ws.Cell(row, GetFieldNumber("平日夜間等作業キャンセル料小計", version) + startCol).GetString();
			作業延長料金_30分毎_単価 = ws.Cell(row, GetFieldNumber("作業延長料金_30分毎_単価", version) + startCol).GetString();
			作業延長料金_30分毎_数量 = ws.Cell(row, GetFieldNumber("作業延長料金_30分毎_数量", version) + startCol).GetString();
			作業延長料金_30分毎_小計 = ws.Cell(row, GetFieldNumber("作業延長料金_30分毎_小計", version) + startCol).GetString();
			離島における交通費小計 = ws.Cell(row, GetFieldNumber("離島における交通費小計", version) + startCol).GetString();
			機器料金小計 = ws.Cell(row, GetFieldNumber("機器料金小計", version) + startCol).GetString();
			小計 = ws.Cell(row, GetFieldNumber("小計", version) + startCol).GetString();
			消費税額 = ws.Cell(row, GetFieldNumber("消費税額", version) + startCol).GetString();
			合計_税込 = ws.Cell(row, GetFieldNumber("合計_税込", version) + startCol).GetString();
			補助金申請書類送付日_NTT_ミック = Program.GetDateString(ws.Cell(row, GetFieldNumber("補助金申請書類送付日_NTT_ミック", version) + startCol));
			完了フラグ_拠点毎 = ws.Cell(row, GetFieldNumber("完了フラグ_拠点毎", version) + startCol).GetString();
			NTT備考欄 = ws.Cell(row, GetFieldNumber("NTT備考欄", version) + startCol).GetString();
			本日の更新分 = Program.GetDateString(ws.Cell(row, GetFieldNumber("本日の更新分", version) + startCol));
			回答結果1 = ws.Cell(row, GetFieldNumber("回答結果1", version) + startCol).GetString();
			修正箇所1 = ws.Cell(row, GetFieldNumber("修正箇所1", version) + startCol).GetString();
			回答結果2 = ws.Cell(row, GetFieldNumber("回答結果2", version) + startCol).GetString();
			修正箇所2 = ws.Cell(row, GetFieldNumber("修正箇所2", version) + startCol).GetString();
		}
	}
}
