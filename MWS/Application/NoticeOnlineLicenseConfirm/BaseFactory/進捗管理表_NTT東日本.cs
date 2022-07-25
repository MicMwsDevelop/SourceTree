//
// 進捗管理表_NTT東日本.cs
//
// NTT東日本 進捗管理表データ
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/03/08 勝呂)
// Ver1.07 NTT東日本進捗管理表新フォーム(20220613版)に対応(2022/07/21 勝呂)
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
		/// NTTオン資調査
		/// </summary>
		public string 現地調査確定日 { get; set; }
		public string 現地調査確定時間 { get; set; }
		public string 現地調査結果 { get; set; }
		public string 現地調査結果詳細_調査NG時 { get; set; }
		public string 現地調査確定日_過去日 { get; set; }
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
		public string 補助金_工事基本額単価 { get; set; }
		public string 補助金_工事基本額数量 { get; set; }
		public string 補助金_工事基本額小計 { get; set; }
		public string 補助金_平日夜間等割増料金単価 { get; set; }
		public string 補助金_平日夜間等割増料金数量 { get; set; }
		public string 補助金_平日夜間等割増料金小計 { get; set; }
		public string 補助金_再派遣料金単価 { get; set; }
		public string 補助金_再派遣料金数量 { get; set; }
		public string 補助金_再派遣料金小計 { get; set; }
		public string 補助金_平日夜間等再派遣料金単価 { get; set; }
		public string 補助金_平日夜間等再派遣料金数量 { get; set; }
		public string 補助金_平日夜間等再派遣料金小計 { get; set; }
		public string 補助金_規定後リスケ料金単価 { get; set; }
		public string 補助金_規定後リスケ料金数量 { get; set; }
		public string 補助金_規定後リスケ料金小計 { get; set; }
		public string 補助金_平日夜間等規定後リスケ料金単価 { get; set; }
		public string 補助金_平日夜間等規定後リスケ料金数量 { get; set; }
		public string 補助金_平日夜間等規定後リスケ料金小計 { get; set; }
		public string 補助金_作業キャンセル料単価 { get; set; }
		public string 補助金_作業キャンセル料数量 { get; set; }
		public string 補助金_作業キャンセル料小計 { get; set; }
		public string 補助金_平日夜間等作業キャンセル料単価 { get; set; }
		public string 補助金_平日夜間等作業キャンセル料数量 { get; set; }
		public string 補助金_平日夜間等作業キャンセル料小計 { get; set; }
		public string 補助金_作業延長料金_30分毎_単価 { get; set; }
		public string 補助金_作業延長料金_30分毎_数量 { get; set; }
		public string 補助金_作業延長料金_30分毎_小計 { get; set; }
		public string 補助金_離島における交通費小計 { get; set; }
		public string 補助金_機器料金小計 { get; set; }
		public string 補助金_小計 { get; set; }
		public string 補助金_消費税額 { get; set; }
		public string 補助金_合計税込 { get; set; }
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
		/// NTT委託業務完成通知書申請関連
		/// Ver1.07 NTT東日本進捗管理表新フォーム(20220613版)に対応(2022/07/21 勝呂)
		/// </summary>
		public string 委託業務完成通知書_現地調査基本額単価 { get; set; }
		public string 委託業務完成通知書_現地調査基本額数量 { get; set; }
		public string 委託業務完成通知書_現地調査基本額小計 { get; set; }
		public string 委託業務完成通知書_平日夜間等割増料金単価 { get; set; }
		public string 委託業務完成通知書_平日夜間等割増料金数量 { get; set; }
		public string 委託業務完成通知書_平日夜間等割増料金小計 { get; set; }
		public string 委託業務完成通知書_再派遣料金単価 { get; set; }
		public string 委託業務完成通知書_再派遣料金数量 { get; set; }
		public string 委託業務完成通知書_再派遣料金小計 { get; set; }
		public string 委託業務完成通知書_平日夜間等再派遣料金単価 { get; set; }
		public string 委託業務完成通知書_平日夜間等再派遣料金数量 { get; set; }
		public string 委託業務完成通知書_平日夜間等再派遣料金小計 { get; set; }
		public string 委託業務完成通知書_規定後リスケ料金単価 { get; set; }
		public string 委託業務完成通知書_規定後リスケ料金数量 { get; set; }
		public string 委託業務完成通知書_規定後リスケ料金小計 { get; set; }
		public string 委託業務完成通知書_平日夜間等規定後リスケ料金単価 { get; set; }
		public string 委託業務完成通知書_平日夜間等規定後リスケ料金数量 { get; set; }
		public string 委託業務完成通知書_平日夜間等規定後リスケ料金小計 { get; set; }
		public string 委託業務完成通知書_作業キャンセル料単価 { get; set; }
		public string 委託業務完成通知書_作業キャンセル料数量 { get; set; }
		public string 委託業務完成通知書_作業キャンセル料小計 { get; set; }
		public string 委託業務完成通知書_平日夜間等作業キャンセル料単価 { get; set; }
		public string 委託業務完成通知書_平日夜間等作業キャンセル料数量 { get; set; }
		public string 委託業務完成通知書_平日夜間等作業キャンセル料小計 { get; set; }
		public string 委託業務完成通知書_作業延長料金30分毎単価 { get; set; }
		public string 委託業務完成通知書_作業延長料金30分毎数量 { get; set; }
		public string 委託業務完成通知書_作業延長料金30分毎小計 { get; set; }
		public string 委託業務完成通知書_離島における交通費小計 { get; set; }
		public string 委託業務完成通知書_小計 { get; set; }
		public string 委託業務完成通知書_消費税額 { get; set; }
		public string 委託業務完成通知書_合計税込 { get; set; }

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
			現地調査確定日 = string.Empty;
			現地調査確定時間 = string.Empty;
			現地調査結果 = string.Empty;
			現地調査結果詳細_調査NG時 = string.Empty;
			現地調査確定日_過去日 = string.Empty;
			備考_調査関連 = string.Empty;
			工事確定日 = string.Empty;
			工事確定時間 = string.Empty;
			工事結果 = string.Empty;
			工事結果詳細_工事NG時 = string.Empty;
			工事確定日_過去日 = string.Empty;
			備考_工事関連 = string.Empty;
			作業報告書_PDF_送付月25日締め_NTT_ミック = string.Empty;
			補助金_工事基本額単価 = string.Empty;
			補助金_工事基本額数量 = string.Empty;
			補助金_工事基本額小計 = string.Empty;
			補助金_平日夜間等割増料金単価 = string.Empty;
			補助金_平日夜間等割増料金数量 = string.Empty;
			補助金_平日夜間等割増料金小計 = string.Empty;
			補助金_再派遣料金単価 = string.Empty;
			補助金_再派遣料金数量 = string.Empty;
			補助金_再派遣料金小計 = string.Empty;
			補助金_平日夜間等再派遣料金単価 = string.Empty;
			補助金_平日夜間等再派遣料金数量 = string.Empty;
			補助金_平日夜間等再派遣料金小計 = string.Empty;
			補助金_規定後リスケ料金単価 = string.Empty;
			補助金_規定後リスケ料金数量 = string.Empty;
			補助金_規定後リスケ料金小計 = string.Empty;
			補助金_平日夜間等規定後リスケ料金単価 = string.Empty;
			補助金_平日夜間等規定後リスケ料金数量 = string.Empty;
			補助金_平日夜間等規定後リスケ料金小計 = string.Empty;
			補助金_作業キャンセル料単価 = string.Empty;
			補助金_作業キャンセル料数量 = string.Empty;
			補助金_作業キャンセル料小計 = string.Empty;
			補助金_平日夜間等作業キャンセル料単価 = string.Empty;
			補助金_平日夜間等作業キャンセル料数量 = string.Empty;
			補助金_平日夜間等作業キャンセル料小計 = string.Empty;
			補助金_作業延長料金_30分毎_単価 = string.Empty;
			補助金_作業延長料金_30分毎_数量 = string.Empty;
			補助金_作業延長料金_30分毎_小計 = string.Empty;
			補助金_離島における交通費小計 = string.Empty;
			補助金_機器料金小計 = string.Empty;
			補助金_小計 = string.Empty;
			補助金_消費税額 = string.Empty;
			補助金_合計税込 = string.Empty;
			補助金申請書類送付日_NTT_ミック = string.Empty;
			完了フラグ_拠点毎 = string.Empty;
			NTT備考欄 = string.Empty;
			本日の更新分 = string.Empty;
			回答結果1 = string.Empty;
			修正箇所1 = string.Empty;
			回答結果2 = string.Empty;
			修正箇所2 = string.Empty;

			// Ver1.07 NTT東日本進捗管理表新フォーム(20220613版)に対応(2022/07/21 勝呂)
			委託業務完成通知書_現地調査基本額単価 = string.Empty;
			委託業務完成通知書_現地調査基本額数量 = string.Empty;
			委託業務完成通知書_現地調査基本額小計 = string.Empty;
			委託業務完成通知書_平日夜間等割増料金単価 = string.Empty;
			委託業務完成通知書_平日夜間等割増料金数量 = string.Empty;
			委託業務完成通知書_平日夜間等割増料金小計 = string.Empty;
			委託業務完成通知書_再派遣料金単価 = string.Empty;
			委託業務完成通知書_再派遣料金数量 = string.Empty;
			委託業務完成通知書_再派遣料金小計 = string.Empty;
			委託業務完成通知書_平日夜間等再派遣料金単価 = string.Empty;
			委託業務完成通知書_平日夜間等再派遣料金数量 = string.Empty;
			委託業務完成通知書_平日夜間等再派遣料金小計 = string.Empty;
			委託業務完成通知書_規定後リスケ料金単価 = string.Empty;
			委託業務完成通知書_規定後リスケ料金数量 = string.Empty;
			委託業務完成通知書_規定後リスケ料金小計 = string.Empty;
			委託業務完成通知書_平日夜間等規定後リスケ料金単価 = string.Empty;
			委託業務完成通知書_平日夜間等規定後リスケ料金数量 = string.Empty;
			委託業務完成通知書_平日夜間等規定後リスケ料金小計 = string.Empty;
			委託業務完成通知書_作業キャンセル料単価 = string.Empty;
			委託業務完成通知書_作業キャンセル料数量 = string.Empty;
			委託業務完成通知書_作業キャンセル料小計 = string.Empty;
			委託業務完成通知書_平日夜間等作業キャンセル料単価 = string.Empty;
			委託業務完成通知書_平日夜間等作業キャンセル料数量 = string.Empty;
			委託業務完成通知書_平日夜間等作業キャンセル料小計 = string.Empty;
			委託業務完成通知書_作業延長料金30分毎単価 = string.Empty;
			委託業務完成通知書_作業延長料金30分毎数量 = string.Empty;
			委託業務完成通知書_作業延長料金30分毎小計 = string.Empty;
			委託業務完成通知書_離島における交通費小計 = string.Empty;
			委託業務完成通知書_小計 = string.Empty;
			委託業務完成通知書_消費税額 = string.Empty;
			委託業務完成通知書_合計税込 = string.Empty;
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
			ret.Add(現地調査確定日);
			ret.Add(現地調査確定時間);
			ret.Add(現地調査結果);
			ret.Add(現地調査結果詳細_調査NG時);
			ret.Add(現地調査確定日_過去日);
			ret.Add(備考_調査関連);
			ret.Add(工事確定日);
			ret.Add(工事確定時間);
			ret.Add(工事結果);
			ret.Add(工事結果詳細_工事NG時);
			ret.Add(工事確定日_過去日);
			ret.Add(備考_工事関連);
			ret.Add(作業報告書_PDF_送付月25日締め_NTT_ミック);
			ret.Add(補助金_工事基本額単価);
			ret.Add(補助金_工事基本額数量);
			ret.Add(補助金_工事基本額小計);
			ret.Add(補助金_平日夜間等割増料金単価);
			ret.Add(補助金_平日夜間等割増料金数量);
			ret.Add(補助金_平日夜間等割増料金小計);
			ret.Add(補助金_再派遣料金単価);
			ret.Add(補助金_再派遣料金数量);
			ret.Add(補助金_再派遣料金小計);
			ret.Add(補助金_平日夜間等再派遣料金単価);
			ret.Add(補助金_平日夜間等再派遣料金数量);
			ret.Add(補助金_平日夜間等再派遣料金小計);
			ret.Add(補助金_規定後リスケ料金単価);
			ret.Add(補助金_規定後リスケ料金数量);
			ret.Add(補助金_規定後リスケ料金小計);
			ret.Add(補助金_平日夜間等規定後リスケ料金単価);
			ret.Add(補助金_平日夜間等規定後リスケ料金数量);
			ret.Add(補助金_平日夜間等規定後リスケ料金小計);
			ret.Add(補助金_作業キャンセル料単価);
			ret.Add(補助金_作業キャンセル料数量);
			ret.Add(補助金_作業キャンセル料小計);
			ret.Add(補助金_平日夜間等作業キャンセル料単価);
			ret.Add(補助金_平日夜間等作業キャンセル料数量);
			ret.Add(補助金_平日夜間等作業キャンセル料小計);
			ret.Add(補助金_作業延長料金_30分毎_単価);
			ret.Add(補助金_作業延長料金_30分毎_数量);
			ret.Add(補助金_作業延長料金_30分毎_小計);
			ret.Add(補助金_離島における交通費小計);
			ret.Add(補助金_機器料金小計);
			ret.Add(補助金_小計);
			ret.Add(補助金_消費税額);
			ret.Add(補助金_合計税込);
			ret.Add(補助金申請書類送付日_NTT_ミック);
			ret.Add(完了フラグ_拠点毎);
			ret.Add(NTT備考欄);
			ret.Add(本日の更新分);
			ret.Add(回答結果1);
			ret.Add(修正箇所1);
			ret.Add(回答結果2);
			ret.Add(修正箇所2);

			// Ver1.07 NTT東日本進捗管理表新フォーム(20220613版)に対応(2022/07/21 勝呂)
			ret.Add(委託業務完成通知書_現地調査基本額単価);
			ret.Add(委託業務完成通知書_現地調査基本額数量);
			ret.Add(委託業務完成通知書_現地調査基本額小計);
			ret.Add(委託業務完成通知書_平日夜間等割増料金単価);
			ret.Add(委託業務完成通知書_平日夜間等割増料金数量);
			ret.Add(委託業務完成通知書_平日夜間等割増料金小計);
			ret.Add(委託業務完成通知書_再派遣料金単価);
			ret.Add(委託業務完成通知書_再派遣料金数量);
			ret.Add(委託業務完成通知書_再派遣料金小計);
			ret.Add(委託業務完成通知書_平日夜間等再派遣料金単価);
			ret.Add(委託業務完成通知書_平日夜間等再派遣料金数量);
			ret.Add(委託業務完成通知書_平日夜間等再派遣料金小計);
			ret.Add(委託業務完成通知書_規定後リスケ料金単価);
			ret.Add(委託業務完成通知書_規定後リスケ料金数量);
			ret.Add(委託業務完成通知書_規定後リスケ料金小計);
			ret.Add(委託業務完成通知書_平日夜間等規定後リスケ料金単価);
			ret.Add(委託業務完成通知書_平日夜間等規定後リスケ料金数量);
			ret.Add(委託業務完成通知書_平日夜間等規定後リスケ料金小計);
			ret.Add(委託業務完成通知書_作業キャンセル料単価);
			ret.Add(委託業務完成通知書_作業キャンセル料数量);
			ret.Add(委託業務完成通知書_作業キャンセル料小計);
			ret.Add(委託業務完成通知書_平日夜間等作業キャンセル料単価);
			ret.Add(委託業務完成通知書_平日夜間等作業キャンセル料数量);
			ret.Add(委託業務完成通知書_平日夜間等作業キャンセル料小計);
			ret.Add(委託業務完成通知書_作業延長料金30分毎単価);
			ret.Add(委託業務完成通知書_作業延長料金30分毎数量);
			ret.Add(委託業務完成通知書_作業延長料金30分毎小計);
			ret.Add(委託業務完成通知書_離島における交通費小計);
			ret.Add(委託業務完成通知書_小計);
			ret.Add(委託業務完成通知書_消費税額);
			ret.Add(委託業務完成通知書_合計税込);
			return ret.ToArray();
		}

		/// <summary>
		/// ワークシートの読込定(進捗管理表)
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		/// <param name="version">進捗管理表バージョン番号</param>
		public void SetWorksheetBy進捗管理表(IXLWorksheet ws, int row)
		{
			ReadWorksheet(ws, row, 0);
		}

		/// <summary>
		/// ワークシートの読込(オンライン資格確認通知結果)
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		public void ReadWorksheetByオンライン資格確認通知結果(IXLWorksheet ws, int row)
		{
			Notice.ReadWorksheet(ws, row);
			ReadWorksheet(ws, row, Notice.GetColumn); 
		}

		/// <summary>
		/// ワークシートの読込
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		/// <param name="startCol">開始カラム</param>
		private void ReadWorksheet(IXLWorksheet ws, int row, int startCol)
		{
			受付通番 = ws.Cell(row, 1 + startCol).GetString();
			申込日 = Program.GetDateString(ws.Cell(row, 2 + startCol));
			病院ID = ws.Cell(row, 3 + startCol).GetString().ToInt();
			医療機関名 = ws.Cell(row, 4 + startCol).GetString();
			更新日_NTT = Program.GetDateString(ws.Cell(row, 5 + startCol));
			受付業務開始日 = Program.GetDateString(ws.Cell(row, 6 + startCol));
			一元受付ステータス = ws.Cell(row, 7 + startCol).GetString();
			回線調査ステータス = ws.Cell(row, 8 + startCol).GetString();
			日程調整ステータス = ws.Cell(row, 9 + startCol).GetString();
			入館調整ステータス = ws.Cell(row, 10 + startCol).GetString();
			進捗管理ステータス = ws.Cell(row, 11 + startCol).GetString();
			フレッツ新規手配 = ws.Cell(row, 12 + startCol).GetString();
			現地調査確定日 = Program.GetDateString(ws.Cell(row, 13 + startCol));
			現地調査確定時間 = Program.GetTimeString(ws.Cell(row, 14 + startCol));
			現地調査結果 = ws.Cell(row, 15 + startCol).GetString();
			現地調査結果詳細_調査NG時 = ws.Cell(row, 16 + startCol).GetString();
			現地調査確定日_過去日 = Program.GetDateString(ws.Cell(row, 17 + startCol));
			備考_調査関連 = ws.Cell(row, 18 + startCol).GetString();
			工事確定日 = Program.GetDateString(ws.Cell(row, 19 + startCol));
			工事確定時間 = Program.GetTimeString(ws.Cell(row, 20 + startCol));
			工事結果 = ws.Cell(row, 21 + startCol).GetString();
			工事結果詳細_工事NG時 = ws.Cell(row, 22 + startCol).GetString();
			工事確定日_過去日 = Program.GetDateString(ws.Cell(row, 23 + startCol));
			備考_工事関連 = ws.Cell(row, 24 + startCol).GetString();
			作業報告書_PDF_送付月25日締め_NTT_ミック = ws.Cell(row, 25 + startCol).GetString();
			補助金_工事基本額単価 = ws.Cell(row, 26 + startCol).GetString();
			補助金_工事基本額数量 = ws.Cell(row, 27 + startCol).GetString();
			補助金_工事基本額小計 = ws.Cell(row, 28 + startCol).GetString();
			補助金_平日夜間等割増料金単価 = ws.Cell(row, 29 + startCol).GetString();
			補助金_平日夜間等割増料金数量 = ws.Cell(row, 30 + startCol).GetString();
			補助金_平日夜間等割増料金小計 = ws.Cell(row, 31 + startCol).GetString();
			補助金_再派遣料金単価 = ws.Cell(row, 32 + startCol).GetString();
			補助金_再派遣料金数量 = ws.Cell(row, 33 + startCol).GetString();
			補助金_再派遣料金小計 = ws.Cell(row, 34 + startCol).GetString();
			補助金_平日夜間等再派遣料金単価 = ws.Cell(row, 35 + startCol).GetString();
			補助金_平日夜間等再派遣料金数量 = ws.Cell(row, 36 + startCol).GetString();
			補助金_平日夜間等再派遣料金小計 = ws.Cell(row, 37 + startCol).GetString();
			補助金_規定後リスケ料金単価 = ws.Cell(row, 38 + startCol).GetString();
			補助金_規定後リスケ料金数量 = ws.Cell(row, 39 + startCol).GetString();
			補助金_規定後リスケ料金小計 = ws.Cell(row, 40 + startCol).GetString();
			補助金_平日夜間等規定後リスケ料金単価 = ws.Cell(row, 41 + startCol).GetString();
			補助金_平日夜間等規定後リスケ料金数量 = ws.Cell(row, 42 + startCol).GetString();
			補助金_平日夜間等規定後リスケ料金小計 = ws.Cell(row, 43 + startCol).GetString();
			補助金_作業キャンセル料単価 = ws.Cell(row, 44 + startCol).GetString();
			補助金_作業キャンセル料数量 = ws.Cell(row, 45 + startCol).GetString();
			補助金_作業キャンセル料小計 = ws.Cell(row, 46 + startCol).GetString();
			補助金_平日夜間等作業キャンセル料単価 = ws.Cell(row, 47 + startCol).GetString();
			補助金_平日夜間等作業キャンセル料数量 = ws.Cell(row, 48 + startCol).GetString();
			補助金_平日夜間等作業キャンセル料小計 = ws.Cell(row, 49 + startCol).GetString();
			補助金_作業延長料金_30分毎_単価 = ws.Cell(row, 50 + startCol).GetString();
			補助金_作業延長料金_30分毎_数量 = ws.Cell(row, 51 + startCol).GetString();
			補助金_作業延長料金_30分毎_小計 = ws.Cell(row, 52 + startCol).GetString();
			補助金_離島における交通費小計 = ws.Cell(row, 53 + startCol).GetString();
			補助金_機器料金小計 = ws.Cell(row, 54 + startCol).GetString();
			補助金_小計 = ws.Cell(row, 55 + startCol).GetString();
			補助金_消費税額 = ws.Cell(row, 56 + startCol).GetString();
			補助金_合計税込 = ws.Cell(row, 57 + startCol).GetString();
			補助金申請書類送付日_NTT_ミック = Program.GetDateString(ws.Cell(row, 58 + startCol));
			完了フラグ_拠点毎 = ws.Cell(row, 59 + startCol).GetString();
			NTT備考欄 = ws.Cell(row, 60 + startCol).GetString();
			本日の更新分 = Program.GetDateString(ws.Cell(row, 61 + startCol));
			回答結果1 = ws.Cell(row, 62 + startCol).GetString();
			修正箇所1 = ws.Cell(row, 63 + startCol).GetString();
			回答結果2 = ws.Cell(row, 64 + startCol).GetString();
			修正箇所2 = ws.Cell(row, 65 + startCol).GetString();

			// Ver1.07 NTT東日本進捗管理表新フォーム(20220613版)に対応(2022/07/21 勝呂)
			委託業務完成通知書_現地調査基本額単価 = ws.Cell(row, 66 + startCol).GetString();
			委託業務完成通知書_現地調査基本額数量 = ws.Cell(row, 67 + startCol).GetString();
			委託業務完成通知書_現地調査基本額小計 = ws.Cell(row, 68 + startCol).GetString();
			委託業務完成通知書_平日夜間等割増料金単価 = ws.Cell(row, 69 + startCol).GetString();
			委託業務完成通知書_平日夜間等割増料金数量 = ws.Cell(row, 70 + startCol).GetString();
			委託業務完成通知書_平日夜間等割増料金小計 = ws.Cell(row, 71 + startCol).GetString();
			委託業務完成通知書_再派遣料金単価 = ws.Cell(row, 72 + startCol).GetString();
			委託業務完成通知書_再派遣料金数量 = ws.Cell(row, 73 + startCol).GetString();
			委託業務完成通知書_再派遣料金小計 = ws.Cell(row, 74 + startCol).GetString();
			委託業務完成通知書_平日夜間等再派遣料金単価 = ws.Cell(row, 75 + startCol).GetString();
			委託業務完成通知書_平日夜間等再派遣料金数量 = ws.Cell(row, 76 + startCol).GetString();
			委託業務完成通知書_平日夜間等再派遣料金小計 = ws.Cell(row, 77 + startCol).GetString();
			委託業務完成通知書_規定後リスケ料金単価 = ws.Cell(row, 78 + startCol).GetString();
			委託業務完成通知書_規定後リスケ料金数量 = ws.Cell(row, 79 + startCol).GetString();
			委託業務完成通知書_規定後リスケ料金小計 = ws.Cell(row, 80 + startCol).GetString();
			委託業務完成通知書_平日夜間等規定後リスケ料金単価 = ws.Cell(row, 81 + startCol).GetString();
			委託業務完成通知書_平日夜間等規定後リスケ料金数量 = ws.Cell(row, 82 + startCol).GetString();
			委託業務完成通知書_平日夜間等規定後リスケ料金小計 = ws.Cell(row, 83 + startCol).GetString();
			委託業務完成通知書_作業キャンセル料単価 = ws.Cell(row, 84 + startCol).GetString();
			委託業務完成通知書_作業キャンセル料数量 = ws.Cell(row, 85 + startCol).GetString();
			委託業務完成通知書_作業キャンセル料小計 = ws.Cell(row, 86 + startCol).GetString();
			委託業務完成通知書_平日夜間等作業キャンセル料単価 = ws.Cell(row, 87 + startCol).GetString();
			委託業務完成通知書_平日夜間等作業キャンセル料数量 = ws.Cell(row, 88 + startCol).GetString();
			委託業務完成通知書_平日夜間等作業キャンセル料小計 = ws.Cell(row, 89 + startCol).GetString();
			委託業務完成通知書_作業延長料金30分毎単価 = ws.Cell(row, 90 + startCol).GetString();
			委託業務完成通知書_作業延長料金30分毎数量 = ws.Cell(row, 91 + startCol).GetString();
			委託業務完成通知書_作業延長料金30分毎小計 = ws.Cell(row, 92 + startCol).GetString();
			委託業務完成通知書_離島における交通費小計 = ws.Cell(row, 93 + startCol).GetString();
			委託業務完成通知書_小計 = ws.Cell(row, 94 + startCol).GetString();
			委託業務完成通知書_消費税額 = ws.Cell(row, 95 + startCol).GetString();
			委託業務完成通知書_合計税込 = ws.Cell(row, 96 + startCol).GetString();
		}
	}
}
