//
// 進捗管理表_NTT西日本.cs
//
// NTT西日本 進捗管理表データ
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/03/08 勝呂)
// Ver1.04 NTT西日本進捗管理表新フォームに対応(2022/04/22 勝呂)
// 
using ClosedXML.Excel;
using CommonLib.Common;
using System;
using System.Collections.Generic;

namespace NoticeOnlineLicenseConfirm.BaseFactory
{
	public class 進捗管理表_NTT西日本
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
		public string 担当拠点 { get; set; }
		public string 担当者 { get; set; }
		public int 病院ID { get; set; }
		public string 医療機関名 { get; set; }

		/// <summary>
		/// オン資受託工事
		/// </summary>
		public string 工事確定日 { get; set; }
		public string 工事確定時間 { get; set; }
		public string 工事結果 { get; set; }
		public string 工事結果詳細_工事NG時 { get; set; }
		public string 工事確定日_過去日 { get; set; }
		public string 完了報告書受領日 { get; set; }
		public string 備考_工事関連 { get; set; }

		/// <summary>
		/// オン資受託工事調整
		/// </summary>
		public string 第1希望日 { get; set; }
		public string 第1希望時間 { get; set; }
		public string 第2希望日 { get; set; }
		public string 第2希望時間 { get; set; }
		public string 第3希望日 { get; set; }
		public string 第3希望時間 { get; set; }
		public string OK曜日_時間含む { get; set; }
		public string 平日夜間土休日 { get; set; }
		public string 取消 { get; set; }
		public string 直前リスケ数 { get; set; }
		public string 直前リスケ内容 { get; set; }
		public string BO { get; set; }

		/// <summary>
		/// 基本事項
		/// </summary>
		public string フレッツ新規手配 { get; set; }
		public string 既存回線ID_新設回線ID { get; set; }
		public string 回線品目 { get; set; }

		/// <summary>
		/// フレッツ工事
		/// </summary>
		public string フレッツ工事_工事確定日 { get; set; }
		public string フレッツ工事工事確定時間 { get; set; }
		public string フレッツ工事工事結果 { get; set; }
		public string フレッツ工事工事確定日_過去日 { get; set; }
		public string フレッツ工事備考_工事関連 { get; set; }

		/// <summary>
		/// 拠点情報
		/// </summary>
		public string 都道府県 { get; set; }
		public string 住所 { get; set; }
		public string 架電先対応者 { get; set; }
		public string 架電番号 { get; set; }
		public string 入館調整ステータス { get; set; }

		/// <summary>
		/// ヒアリングシート
		/// </summary>
		public string ヒアリングシートチェック結果 { get; set; }
		public string ヒアリングシート修正依頼日 { get; set; }
		public string 連絡票受領日 { get; set; }

		/// <summary>
		/// 構成情報
		/// </summary>
		public string モバイルディスプレイ { get; set; }
		public string VGA端子 { get; set; }
		public string CPU切替器 { get; set; }
		public string GW設置確認 { get; set; }
		public string HUB台数 { get; set; }
		public string オン資ルータ { get; set; }
		public string PCモニタ { get; set; }
		public string 既存ルータ型番 { get; set; }

		/// <summary>
		/// 回線情報
		/// </summary>
		public string V6変更有無 { get; set; }
		public string V6オプション開通確認日 { get; set; }
		public string 新規開通申込み完了_営業 { get; set; }

		/// <summary>
		/// NTT手配機器
		/// </summary>
		public string 機器到着予定日 { get; set; }
		public string 到着完了確認日 { get; set; }

		/// <summary>
		/// LAN調査
		/// </summary>
		public string LAN調査_有無 { get; set; }
		public string LAN調査_依頼日 { get; set; }
		public string LAN調査_工事日 { get; set; }
		public string LAN調査_直前リスト回数 { get; set; }
		public string LAN調査_リスケ内容 { get; set; }
		public string LAN調査_完了報告受領日 { get; set; }
		public string LAN調査_平日夜間土休日 { get; set; }
		public string LAN調査_呼び線挿入不可 { get; set; }
		public string LAN調査_NG営業連絡日 { get; set; }
		public string LAN調査_BO { get; set; }
		public string LAN調査_NG記事 { get; set; }

		/// <summary>
		/// LAN配線
		/// </summary>
		public string LAN配線_必要可否 { get; set; }
		public string LAN配線_依頼日 { get; set; }
		public string LAN配線_工事日 { get; set; }
		public string LAN配線_取消 { get; set; }
		public string LAN配線_直前リスケ回数 { get; set; }
		public string LAN配線_リスケ内容 { get; set; }
		public string LAN配線_平日夜間土休日 { get; set; }
		public string LAN配線_工事完了受領日 { get; set; }
		public string LAN配線_延長単位_10m { get; set; }
		public string LAN配線_ワイプロ延長単位_1m { get; set; }
		public string LAN配線_BO { get; set; }
		public string LAN配線_記事 { get; set; }

		/// <summary>
		/// 完了関連
		/// </summary>
		public string 委託業務完成通知書送付日 { get; set; }

		/// <summary>
		/// 補助金申請関連
		/// </summary>
		public string 作業報告書_PDF_送付月25日締め_NTT_ミック { get; set; }
		public string 追加費用1 { get; set; }
		public string 追加費用2 { get; set; }
		public string 補助金申請書類送付日_NTT_医療機関 { get; set; }

		/// <summary>
		/// 機器設定作業料金
		/// </summary>
		public string 機器設定作業料金_プランA_平日日中帯 { get; set; }
		public string 機器設定作業料金_プランA_夜間土休日 { get; set; }
		public string 機器設定作業料金_プランB_平日日中帯 { get; set; }
		public string 機器設定作業料金_プランB_夜間土休日 { get; set; }
		public string 機器設定作業料金_機器代金のみ { get; set; }

		// Ver1.04 NTT西日本進捗管理表新フォームに対応(2022/04/22 勝呂)
		public string プランA_新価格_平日日中帯 { get; set; }
		public string プランA_新価格_夜間土休日 { get; set; }
		public string プランB_新価格_平日日中帯 { get; set; }
		public string プランB_新価格_夜間土休日 { get; set; }
		public string 機器代金_新価格 { get; set; }
		public string HUB_オプション { get; set; }
		public string モバイルディスプレイ_オプション { get; set; }

		public string 機器設定作業料金_再派遣料金ver1_平日日中帯 { get; set; }
		public string 機器設定作業料金_再派遣料金ver1_夜間土休日 { get; set; }
		public string 機器設定作業料金_再派遣料金ver2_平日日中帯 { get; set; }
		public string 機器設定作業料金_再派遣料金ver2_夜間土休日 { get; set; }
		public string 機器設定作業料金_再派遣料金ver3_平日日中帯 { get; set; }
		public string 機器設定作業料金_再派遣料金ver3_夜間土休日 { get; set; }
		public string 機器設定作業料金_規定後リスケ料金_平日日中帯 { get; set; }
		public string 機器設定作業料金_規定後リスケ料金_夜間土休日 { get; set; }
		public string 機器設定作業料金_作業キャンセルA_平日日中帯 { get; set; }
		public string 機器設定作業料金_作業キャンセルA_夜間土休日 { get; set; }
		public string 機器設定作業料金_作業キャンセルA1_平日日中帯 { get; set; }
		public string 機器設定作業料金_作業キャンセルA1_夜間土休日 { get; set; }

		/// <summary>
		/// 現地調査(プランBの場合)
		/// </summary>
		public string 現地調査_割増料金_夜間土休日 { get; set; }
		public string 現地調査_再派遣料金_平日日中帯 { get; set; }
		public string 現地調査_再派遣料金_夜間土休日 { get; set; }
		public string 現地調査_規定後リスケ料金_平日日中帯 { get; set; }
		public string 現地調査_規定後リスケ料金_夜間土休日 { get; set; }
		public string 現地調査_作業キャンセル_平日日中帯 { get; set; }
		public string 現地調査_作業キャンセル_夜間土休日 { get; set; }

		/// <summary>
		/// LAN配線作業(プランBの場合)
		/// </summary>
		public string LAN配線作業_割増料金_夜間土休日 { get; set; }
		public string LAN配線作業_再派遣料金_平日日中帯 { get; set; }
		public string LAN配線作業_再派遣料金_夜間土休日 { get; set; }
		public string LAN配線作業_規定後リスケ料金_平日日中帯 { get; set; }
		public string LAN配線作業_規定後リスケ料金_夜間土休日 { get; set; }
		public string LAN配線作業_作業キャンセルB_平日日中帯 { get; set; }
		public string LAN配線作業_作業キャンセルB_夜間土休日 { get; set; }
		public string LAN配線作業_作業キャンセルB1_平日日中帯 { get; set; }
		public string LAN配線作業_作業キャンセルB1_夜間土休日 { get; set; }

		/// <summary>
		/// ケーブル等追加内容
		/// </summary>
		public string LAN配線_延長 { get; set; }
		public string ワイプロ_延長 { get; set; }

		/// <summary>
		/// その他
		/// </summary>
		public string その他実費費 { get; set; }
		public string 備考 { get; set; }
		public string 請求金額 { get; set; }

		/// <summary>
		/// 連絡票
		/// </summary>
		public string 連絡項目 { get; set; }
		public string 連絡内容 { get; set; }

		/// <summary>
		/// 工事確定日付の取得
		/// I列：工事確定日
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
		/// ヒアリングシート修正依頼日付の取得
		/// AP列：ヒアリングシート修正依頼日
		/// </summary>
		public Date? ヒアリングシート修正依頼日付
		{
			get
			{
				if (0 < ヒアリングシート修正依頼日.Length)
				{
					DateTime work;
					if (DateTime.TryParse(ヒアリングシート修正依頼日, out work))
					{
						return new Date(work);
					}
				}
				return null;
			}
		}

		/// <summary>
		/// ヒアリングシートチェック結果がNGかどうか？
		/// AO列：ヒアリングシートチェック結果
		/// </summary>
		public bool ヒアリングシートチェック結果_NG
		{
			get
			{
				return (0 == ヒアリングシートチェック結果.Length) ? true : false;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 進捗管理表_NTT西日本()
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
			担当拠点 = string.Empty;
			担当者 = string.Empty;
			病院ID = 0;
			医療機関名 = string.Empty;
			工事確定日 = string.Empty;
			工事確定時間 = string.Empty;
			工事結果 = string.Empty;
			工事結果詳細_工事NG時 = string.Empty;
			工事確定日_過去日 = string.Empty;
			完了報告書受領日 = string.Empty;
			備考_工事関連 = string.Empty;
			第1希望日 = string.Empty;
			第1希望時間 = string.Empty;
			第2希望日 = string.Empty;
			第2希望時間 = string.Empty;
			第3希望日 = string.Empty;
			第3希望時間 = string.Empty;
			OK曜日_時間含む = string.Empty;
			平日夜間土休日 = string.Empty;
			取消 = string.Empty;
			直前リスケ数 = string.Empty;
			直前リスケ内容 = string.Empty;
			BO = string.Empty;
			フレッツ新規手配 = string.Empty;
			既存回線ID_新設回線ID = string.Empty;
			回線品目 = string.Empty;
			フレッツ工事_工事確定日 = string.Empty;
			フレッツ工事工事確定時間 = string.Empty;
			フレッツ工事工事結果 = string.Empty;
			フレッツ工事工事確定日_過去日 = string.Empty;
			フレッツ工事備考_工事関連 = string.Empty;
			都道府県 = string.Empty;
			住所 = string.Empty;
			架電先対応者 = string.Empty;
			架電番号 = string.Empty;
			入館調整ステータス = string.Empty;
			ヒアリングシートチェック結果 = string.Empty;
			ヒアリングシート修正依頼日 = string.Empty;
			連絡票受領日 = string.Empty;
			モバイルディスプレイ = string.Empty;
			VGA端子 = string.Empty;
			CPU切替器 = string.Empty;
			GW設置確認 = string.Empty;
			HUB台数 = string.Empty;
			オン資ルータ = string.Empty;
			PCモニタ = string.Empty;
			既存ルータ型番 = string.Empty;
			V6変更有無 = string.Empty;
			V6オプション開通確認日 = string.Empty;
			新規開通申込み完了_営業 = string.Empty;
			機器到着予定日 = string.Empty;
			到着完了確認日 = string.Empty;
			LAN調査_有無 = string.Empty;
			LAN調査_依頼日 = string.Empty;
			LAN調査_工事日 = string.Empty;
			LAN調査_直前リスト回数 = string.Empty;
			LAN調査_リスケ内容 = string.Empty;
			LAN調査_完了報告受領日 = string.Empty;
			LAN調査_平日夜間土休日 = string.Empty;
			LAN調査_呼び線挿入不可 = string.Empty;
			LAN調査_NG営業連絡日 = string.Empty;
			LAN調査_BO = string.Empty;
			LAN調査_NG記事 = string.Empty;
			LAN配線_必要可否 = string.Empty;
			LAN配線_依頼日 = string.Empty;
			LAN配線_工事日 = string.Empty;
			LAN配線_取消 = string.Empty;
			LAN配線_直前リスケ回数 = string.Empty;
			LAN配線_リスケ内容 = string.Empty;
			LAN配線_平日夜間土休日 = string.Empty;
			LAN配線_工事完了受領日 = string.Empty;
			LAN配線_延長単位_10m = string.Empty;
			LAN配線_ワイプロ延長単位_1m = string.Empty;
			LAN配線_BO = string.Empty;
			LAN配線_記事 = string.Empty;
			委託業務完成通知書送付日 = string.Empty;
			作業報告書_PDF_送付月25日締め_NTT_ミック = string.Empty;
			追加費用1 = string.Empty;
			追加費用2 = string.Empty;
			補助金申請書類送付日_NTT_医療機関 = string.Empty;
			機器設定作業料金_プランA_平日日中帯 = string.Empty;
			機器設定作業料金_プランA_夜間土休日 = string.Empty;
			機器設定作業料金_プランB_平日日中帯 = string.Empty;
			機器設定作業料金_プランB_夜間土休日 = string.Empty;
			機器設定作業料金_機器代金のみ = string.Empty;

			// Ver1.04 NTT西日本進捗管理表新フォームに対応(2022/04/22 勝呂)
			プランA_新価格_平日日中帯 = string.Empty;
			プランA_新価格_夜間土休日 = string.Empty;
			プランB_新価格_平日日中帯 = string.Empty;
			プランB_新価格_夜間土休日 = string.Empty;
			機器代金_新価格 = string.Empty;
			HUB_オプション = string.Empty;
			モバイルディスプレイ_オプション = string.Empty;

			機器設定作業料金_再派遣料金ver1_平日日中帯 = string.Empty;
			機器設定作業料金_再派遣料金ver1_夜間土休日 = string.Empty;
			機器設定作業料金_再派遣料金ver2_平日日中帯 = string.Empty;
			機器設定作業料金_再派遣料金ver2_夜間土休日 = string.Empty;
			機器設定作業料金_再派遣料金ver3_平日日中帯 = string.Empty;
			機器設定作業料金_再派遣料金ver3_夜間土休日 = string.Empty;
			機器設定作業料金_規定後リスケ料金_平日日中帯 = string.Empty;
			機器設定作業料金_規定後リスケ料金_夜間土休日 = string.Empty;
			機器設定作業料金_作業キャンセルA_平日日中帯 = string.Empty;
			機器設定作業料金_作業キャンセルA_夜間土休日 = string.Empty;
			機器設定作業料金_作業キャンセルA1_平日日中帯 = string.Empty;
			機器設定作業料金_作業キャンセルA1_夜間土休日 = string.Empty;
			現地調査_割増料金_夜間土休日 = string.Empty;
			現地調査_再派遣料金_平日日中帯 = string.Empty;
			現地調査_再派遣料金_夜間土休日 = string.Empty;
			現地調査_規定後リスケ料金_平日日中帯 = string.Empty;
			現地調査_規定後リスケ料金_夜間土休日 = string.Empty;
			現地調査_作業キャンセル_平日日中帯 = string.Empty;
			現地調査_作業キャンセル_夜間土休日 = string.Empty;
			LAN配線作業_割増料金_夜間土休日 = string.Empty;
			LAN配線作業_再派遣料金_平日日中帯 = string.Empty;
			LAN配線作業_再派遣料金_夜間土休日 = string.Empty;
			LAN配線作業_規定後リスケ料金_平日日中帯 = string.Empty;
			LAN配線作業_規定後リスケ料金_夜間土休日 = string.Empty;
			LAN配線作業_作業キャンセルB_平日日中帯 = string.Empty;
			LAN配線作業_作業キャンセルB_夜間土休日 = string.Empty;
			LAN配線作業_作業キャンセルB1_平日日中帯 = string.Empty;
			LAN配線作業_作業キャンセルB1_夜間土休日 = string.Empty;
			LAN配線_延長 = string.Empty;
			ワイプロ_延長 = string.Empty;
			その他実費費 = string.Empty;
			備考 = string.Empty;
			請求金額 = string.Empty;
			連絡項目 = string.Empty;
			連絡内容 = string.Empty;
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
			ret.Add(担当拠点);
			ret.Add(担当者);
			ret.Add(病院ID.ToString());
			ret.Add(医療機関名);
			ret.Add(工事確定日);
			ret.Add(工事確定時間);
			ret.Add(工事結果);
			ret.Add(工事結果詳細_工事NG時);
			ret.Add(工事確定日_過去日);
			ret.Add(完了報告書受領日);
			ret.Add(備考_工事関連);
			ret.Add(第1希望日);
			ret.Add(第1希望時間);
			ret.Add(第2希望日);
			ret.Add(第2希望時間);
			ret.Add(第3希望日);
			ret.Add(第3希望時間);
			ret.Add(OK曜日_時間含む);
			ret.Add(平日夜間土休日);
			ret.Add(取消);
			ret.Add(直前リスケ数);
			ret.Add(直前リスケ内容);
			ret.Add(BO);
			ret.Add(フレッツ新規手配);
			ret.Add(既存回線ID_新設回線ID);
			ret.Add(回線品目);
			ret.Add(フレッツ工事_工事確定日);
			ret.Add(フレッツ工事工事確定時間);
			ret.Add(フレッツ工事工事結果);
			ret.Add(フレッツ工事工事確定日_過去日);
			ret.Add(フレッツ工事備考_工事関連);
			ret.Add(都道府県);
			ret.Add(住所);
			ret.Add(架電先対応者);
			ret.Add(架電番号);
			ret.Add(入館調整ステータス);
			ret.Add(ヒアリングシートチェック結果);
			ret.Add(ヒアリングシート修正依頼日);
			ret.Add(連絡票受領日);
			ret.Add(モバイルディスプレイ);
			ret.Add(VGA端子);
			ret.Add(CPU切替器);
			ret.Add(GW設置確認);
			ret.Add(HUB台数);
			ret.Add(オン資ルータ);
			ret.Add(PCモニタ);
			ret.Add(既存ルータ型番);
			ret.Add(V6変更有無);
			ret.Add(V6オプション開通確認日);
			ret.Add(新規開通申込み完了_営業);
			ret.Add(機器到着予定日);
			ret.Add(到着完了確認日);
			ret.Add(LAN調査_有無);
			ret.Add(LAN調査_依頼日);
			ret.Add(LAN調査_工事日);
			ret.Add(LAN調査_直前リスト回数);
			ret.Add(LAN調査_リスケ内容);
			ret.Add(LAN調査_完了報告受領日);
			ret.Add(LAN調査_平日夜間土休日);
			ret.Add(LAN調査_呼び線挿入不可);
			ret.Add(LAN調査_NG営業連絡日);
			ret.Add(LAN調査_BO);
			ret.Add(LAN調査_NG記事);
			ret.Add(LAN配線_必要可否);
			ret.Add(LAN配線_依頼日);
			ret.Add(LAN配線_工事日);
			ret.Add(LAN配線_取消);
			ret.Add(LAN配線_直前リスケ回数);
			ret.Add(LAN配線_リスケ内容);
			ret.Add(LAN配線_平日夜間土休日);
			ret.Add(LAN配線_工事完了受領日);
			ret.Add(LAN配線_延長単位_10m);
			ret.Add(LAN配線_ワイプロ延長単位_1m);
			ret.Add(LAN配線_BO);
			ret.Add(LAN配線_記事);
			ret.Add(委託業務完成通知書送付日);
			ret.Add(作業報告書_PDF_送付月25日締め_NTT_ミック);
			ret.Add(追加費用1);
			ret.Add(追加費用2);
			ret.Add(補助金申請書類送付日_NTT_医療機関);
			ret.Add(機器設定作業料金_プランA_平日日中帯);
			ret.Add(機器設定作業料金_プランA_夜間土休日);
			ret.Add(機器設定作業料金_プランB_平日日中帯);
			ret.Add(機器設定作業料金_プランB_夜間土休日);
			ret.Add(機器設定作業料金_機器代金のみ);

			// Ver1.04 NTT西日本進捗管理表新フォームに対応(2022/04/22 勝呂)
			ret.Add(プランA_新価格_平日日中帯);
			ret.Add(プランA_新価格_夜間土休日);
			ret.Add(プランB_新価格_平日日中帯);
			ret.Add(プランB_新価格_夜間土休日);
			ret.Add(機器代金_新価格);
			ret.Add(HUB_オプション);
			ret.Add(モバイルディスプレイ_オプション);

			ret.Add(機器設定作業料金_再派遣料金ver1_平日日中帯);
			ret.Add(機器設定作業料金_再派遣料金ver1_夜間土休日);
			ret.Add(機器設定作業料金_再派遣料金ver2_平日日中帯);
			ret.Add(機器設定作業料金_再派遣料金ver2_夜間土休日);
			ret.Add(機器設定作業料金_再派遣料金ver3_平日日中帯);
			ret.Add(機器設定作業料金_再派遣料金ver3_夜間土休日);
			ret.Add(機器設定作業料金_規定後リスケ料金_平日日中帯);
			ret.Add(機器設定作業料金_規定後リスケ料金_夜間土休日);
			ret.Add(機器設定作業料金_作業キャンセルA_平日日中帯);
			ret.Add(機器設定作業料金_作業キャンセルA_夜間土休日);
			ret.Add(機器設定作業料金_作業キャンセルA1_平日日中帯);
			ret.Add(機器設定作業料金_作業キャンセルA1_夜間土休日);
			ret.Add(現地調査_割増料金_夜間土休日);
			ret.Add(現地調査_再派遣料金_平日日中帯);
			ret.Add(現地調査_再派遣料金_夜間土休日);
			ret.Add(現地調査_規定後リスケ料金_平日日中帯);
			ret.Add(現地調査_規定後リスケ料金_夜間土休日);
			ret.Add(現地調査_作業キャンセル_平日日中帯);
			ret.Add(現地調査_作業キャンセル_夜間土休日);
			ret.Add(LAN配線作業_割増料金_夜間土休日);
			ret.Add(LAN配線作業_再派遣料金_平日日中帯);
			ret.Add(LAN配線作業_再派遣料金_夜間土休日);
			ret.Add(LAN配線作業_規定後リスケ料金_平日日中帯);
			ret.Add(LAN配線作業_規定後リスケ料金_夜間土休日);
			ret.Add(LAN配線作業_作業キャンセルB_平日日中帯);
			ret.Add(LAN配線作業_作業キャンセルB_夜間土休日);
			ret.Add(LAN配線作業_作業キャンセルB1_平日日中帯);
			ret.Add(LAN配線作業_作業キャンセルB1_夜間土休日);
			ret.Add(LAN配線_延長);
			ret.Add(ワイプロ_延長);
			ret.Add(その他実費費);
			ret.Add(備考);
			ret.Add(請求金額);
			ret.Add(連絡項目);
			ret.Add(連絡内容);
			return ret.ToArray();
		}

		/// <summary>
		/// ワークシートの読込(進捗管理表)
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		public void SetWorksheetBy進捗管理表(IXLWorksheet ws, int row)
		{
			ReadWorksheet(ws, row, 2);
		}

		/// <summary>
		/// ワークシートの読込(オンライン資格確認通知結果)
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		public void SetWorksheetByオンライン資格確認通知結果(IXLWorksheet ws, int row)
		{
			Notice.ReadWorksheet(ws, row);
			ReadWorksheet(ws, row, Notice.GetColumn);
		}

		/// <summary>
		/// ワークシートの読込(進捗管理表)
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		/// <param name="startCol">開始カラム</param>
		private void ReadWorksheet(IXLWorksheet ws, int row, int startCol)
		{
			受付通番 = ws.Cell(row, 1 + startCol).GetString();
			申込日 = Program.GetDateString(ws.Cell(row, 2 + startCol));
			担当拠点 = ws.Cell(row, 3 + startCol).GetString();
			担当者 = ws.Cell(row, 4 + startCol).GetString();
			病院ID = ws.Cell(row, 5 + startCol).GetString().ToInt();
			医療機関名 = ws.Cell(row, 6 + startCol).GetString();
			工事確定日 = Program.GetDateString(ws.Cell(row, 7 + startCol));
			工事確定時間 = Program.GetTimeString(ws.Cell(row, 8 + startCol));
			工事結果 = ws.Cell(row, 9 + startCol).GetString();
			工事結果詳細_工事NG時 = ws.Cell(row, 10 + startCol).GetString();
			工事確定日_過去日 = Program.GetDateString(ws.Cell(row, 11 + startCol));
			完了報告書受領日 = Program.GetDateString(ws.Cell(row, 12 + startCol));
			備考_工事関連 = ws.Cell(row, 13 + startCol).GetString();
			第1希望日 = Program.GetDateString(ws.Cell(row, 14 + startCol));
			第1希望時間 = Program.GetTimeString(ws.Cell(row, 15 + startCol));
			第2希望日 = Program.GetDateString(ws.Cell(row, 16 + startCol));
			第2希望時間 = Program.GetTimeString(ws.Cell(row, 17 + startCol));
			第3希望日 = Program.GetDateString(ws.Cell(row, 18 + startCol));
			第3希望時間 = Program.GetTimeString(ws.Cell(row, 19 + startCol));
			OK曜日_時間含む = ws.Cell(row, 20 + startCol).GetString();
			平日夜間土休日 = ws.Cell(row, 21 + startCol).GetString();
			取消 = ws.Cell(row, 22 + startCol).GetString();
			直前リスケ数 = ws.Cell(row, 23 + startCol).GetString();
			直前リスケ内容 = ws.Cell(row, 24 + startCol).GetString();
			BO = ws.Cell(row, 25 + startCol).GetString();
			フレッツ新規手配 = ws.Cell(row, 26 + startCol).GetString();
			既存回線ID_新設回線ID = ws.Cell(row, 27 + startCol).GetString();
			回線品目 = ws.Cell(row, 28 + startCol).GetString();
			フレッツ工事_工事確定日 = Program.GetDateString(ws.Cell(row, 29 + startCol));
			フレッツ工事工事確定時間 = ws.Cell(row, 30 + startCol).GetString();
			フレッツ工事工事結果 = ws.Cell(row, 31 + startCol).GetString();
			フレッツ工事工事確定日_過去日 = Program.GetDateString(ws.Cell(row, 32 + startCol));
			フレッツ工事備考_工事関連 = ws.Cell(row, 33 + startCol).GetString();
			都道府県 = ws.Cell(row, 34 + startCol).GetString();
			住所 = ws.Cell(row, 35 + startCol).GetString();
			架電先対応者 = ws.Cell(row, 36 + startCol).GetString();
			架電番号 = ws.Cell(row, 37 + startCol).GetString();
			入館調整ステータス = ws.Cell(row, 38 + startCol).GetString();
			ヒアリングシートチェック結果 = ws.Cell(row, 39 + startCol).GetString();
			ヒアリングシート修正依頼日 = Program.GetDateString(ws.Cell(row, 40 + startCol));
			連絡票受領日 = Program.GetDateString(ws.Cell(row, 41 + startCol));
			モバイルディスプレイ = ws.Cell(row, 42 + startCol).GetString();
			VGA端子 = ws.Cell(row, 43 + startCol).GetString();
			CPU切替器 = ws.Cell(row, 44 + startCol).GetString();
			GW設置確認 = ws.Cell(row, 45 + startCol).GetString();
			HUB台数 = ws.Cell(row, 46 + startCol).GetString();
			オン資ルータ = ws.Cell(row, 47 + startCol).GetString();
			PCモニタ = ws.Cell(row, 48 + startCol).GetString();
			既存ルータ型番 = ws.Cell(row, 49 + startCol).GetString();
			V6変更有無 = ws.Cell(row, 50 + startCol).GetString();
			V6オプション開通確認日 = Program.GetDateString(ws.Cell(row, 51 + startCol));
			新規開通申込み完了_営業 = ws.Cell(row, 52 + startCol).GetString();
			機器到着予定日 = Program.GetDateString(ws.Cell(row, 53 + startCol));
			到着完了確認日 = Program.GetDateString(ws.Cell(row, 54 + startCol));
			LAN調査_有無 = ws.Cell(row, 55 + startCol).GetString();
			LAN調査_依頼日 = Program.GetDateString(ws.Cell(row, 56 + startCol));
			LAN調査_工事日 = Program.GetDateString(ws.Cell(row, 57 + startCol));
			LAN調査_直前リスト回数 = ws.Cell(row, 58 + startCol).GetString();
			LAN調査_リスケ内容 = ws.Cell(row, 59 + startCol).GetString();
			LAN調査_完了報告受領日 = Program.GetDateString(ws.Cell(row, 60 + startCol));
			LAN調査_平日夜間土休日 = ws.Cell(row, 61 + startCol).GetString();
			LAN調査_呼び線挿入不可 = ws.Cell(row, 62 + startCol).GetString();
			LAN調査_NG営業連絡日 = Program.GetDateString(ws.Cell(row, 63 + startCol));
			LAN調査_BO = ws.Cell(row, 64 + startCol).GetString();
			LAN調査_NG記事 = ws.Cell(row, 65 + startCol).GetString();
			LAN配線_必要可否 = ws.Cell(row, 66 + startCol).GetString();
			LAN配線_依頼日 = Program.GetDateString(ws.Cell(row, 67 + startCol));
			LAN配線_工事日 = Program.GetDateString(ws.Cell(row, 68 + startCol));
			LAN配線_取消 = ws.Cell(row, 69 + startCol).GetString();
			LAN配線_直前リスケ回数 = ws.Cell(row, 70 + startCol).GetString();
			LAN配線_リスケ内容 = ws.Cell(row, 71 + startCol).GetString();
			LAN配線_平日夜間土休日 = ws.Cell(row, 72 + startCol).GetString();
			LAN配線_工事完了受領日 = Program.GetDateString(ws.Cell(row, 73 + startCol));
			LAN配線_延長単位_10m = ws.Cell(row, 74 + startCol).GetString();
			LAN配線_ワイプロ延長単位_1m = ws.Cell(row, 75 + startCol).GetString();
			LAN配線_BO = ws.Cell(row, 76 + startCol).GetString();
			LAN配線_記事 = ws.Cell(row, 77 + startCol).GetString();
			委託業務完成通知書送付日 = Program.GetDateString(ws.Cell(row, 78 + startCol));
			作業報告書_PDF_送付月25日締め_NTT_ミック = ws.Cell(row, 79 + startCol).GetString();
			追加費用1 = ws.Cell(row, 80 + startCol).GetString();
			追加費用2 = ws.Cell(row, 81 + startCol).GetString();
			補助金申請書類送付日_NTT_医療機関 = ws.Cell(row, 82 + startCol).GetString();
			機器設定作業料金_プランA_平日日中帯 = ws.Cell(row, 83 + startCol).GetString();
			機器設定作業料金_プランA_夜間土休日 = ws.Cell(row, 84 + startCol).GetString();
			機器設定作業料金_プランB_平日日中帯 = ws.Cell(row, 85 + startCol).GetString();
			機器設定作業料金_プランB_夜間土休日 = ws.Cell(row, 86 + startCol).GetString();
			機器設定作業料金_機器代金のみ = ws.Cell(row, 87 + startCol).GetString();
			プランA_新価格_平日日中帯 = ws.Cell(row, 88 + startCol).GetString();
			プランA_新価格_夜間土休日 = ws.Cell(row, 89 + startCol).GetString();
			プランB_新価格_平日日中帯 = ws.Cell(row, 90 + startCol).GetString();
			プランB_新価格_夜間土休日 = ws.Cell(row, 91 + startCol).GetString();
			機器代金_新価格 = ws.Cell(row, 92 + startCol).GetString();
			HUB_オプション = ws.Cell(row, 93 + startCol).GetString();
			モバイルディスプレイ_オプション = ws.Cell(row, 94 + startCol).GetString();
			機器設定作業料金_再派遣料金ver1_平日日中帯 = ws.Cell(row, 95 + startCol).GetString();
			機器設定作業料金_再派遣料金ver1_夜間土休日 = ws.Cell(row, 96 + startCol).GetString();
			機器設定作業料金_再派遣料金ver2_平日日中帯 = ws.Cell(row, 97 + startCol).GetString();
			機器設定作業料金_再派遣料金ver2_夜間土休日 = ws.Cell(row, 98 + startCol).GetString();
			機器設定作業料金_再派遣料金ver3_平日日中帯 = ws.Cell(row, 99 + startCol).GetString();
			機器設定作業料金_再派遣料金ver3_夜間土休日 = ws.Cell(row, 100 + startCol).GetString();
			機器設定作業料金_規定後リスケ料金_平日日中帯 = ws.Cell(row, 101 + startCol).GetString();
			機器設定作業料金_規定後リスケ料金_夜間土休日 = ws.Cell(row, 102 + startCol).GetString();
			機器設定作業料金_作業キャンセルA_平日日中帯 = ws.Cell(row, 103 + startCol).GetString();
			機器設定作業料金_作業キャンセルA_夜間土休日 = ws.Cell(row, 104 + startCol).GetString();
			機器設定作業料金_作業キャンセルA1_平日日中帯 = ws.Cell(row, 105 + startCol).GetString();
			機器設定作業料金_作業キャンセルA1_夜間土休日 = ws.Cell(row, 106 + startCol).GetString();
			現地調査_割増料金_夜間土休日 = ws.Cell(row, 107 + startCol).GetString();
			現地調査_再派遣料金_平日日中帯 = ws.Cell(row, 108 + startCol).GetString();
			現地調査_再派遣料金_夜間土休日 = ws.Cell(row, 109 + startCol).GetString();
			現地調査_規定後リスケ料金_平日日中帯 = ws.Cell(row, 110 + startCol).GetString();
			現地調査_規定後リスケ料金_夜間土休日 = ws.Cell(row, 111 + startCol).GetString();
			現地調査_作業キャンセル_平日日中帯 = ws.Cell(row, 112 + startCol).GetString();
			現地調査_作業キャンセル_夜間土休日 = ws.Cell(row, 113 + startCol).GetString();
			LAN配線作業_割増料金_夜間土休日 = ws.Cell(row, 114 + startCol).GetString();
			LAN配線作業_再派遣料金_平日日中帯 = ws.Cell(row, 115 + startCol).GetString();
			LAN配線作業_再派遣料金_夜間土休日 = ws.Cell(row, 116 + startCol).GetString();
			LAN配線作業_規定後リスケ料金_平日日中帯 = ws.Cell(row, 117 + startCol).GetString();
			LAN配線作業_規定後リスケ料金_夜間土休日 = ws.Cell(row, 118 + startCol).GetString();
			LAN配線作業_作業キャンセルB_平日日中帯 = ws.Cell(row, 119 + startCol).GetString();
			LAN配線作業_作業キャンセルB_夜間土休日 = ws.Cell(row, 120 + startCol).GetString();
			LAN配線作業_作業キャンセルB1_平日日中帯 = ws.Cell(row, 121 + startCol).GetString();
			LAN配線作業_作業キャンセルB1_夜間土休日 = ws.Cell(row, 122 + startCol).GetString();
			LAN配線_延長 = ws.Cell(row, 123 + startCol).GetString();
			ワイプロ_延長 = ws.Cell(row, 124 + startCol).GetString();
			その他実費費 = ws.Cell(row, 125 + startCol).GetString();
			備考 = ws.Cell(row, 126 + startCol).GetString();
			請求金額 = ws.Cell(row, 127 + startCol).GetString();
			連絡項目 = ws.Cell(row, 128 + startCol).GetString();
			連絡内容 = ws.Cell(row, 129 + startCol).GetString();
		}
	}
}
