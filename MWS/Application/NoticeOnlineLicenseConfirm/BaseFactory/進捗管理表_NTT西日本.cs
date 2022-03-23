//
// 進捗管理表_NTT西日本.cs
//
// NTT西日本 進捗管理表データ
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
	public class 進捗管理表_NTT西日本
	{
		/// <summary>
		/// NTT西日本 進捗管理表バージョン番号
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
		/// 進捗管理表フィールド番号定義(Ver1.00)
		/// </summary>
		public static readonly Dictionary<string, int> FieldNumber100 = new Dictionary<string, int>()
		{
			{ "受付通番", 1 },
			{ "申込日", 2 },
			{ "担当拠点", 3 },
			{ "担当者", 4 },
			{ "病院ID", 5 },
			{ "医療機関名", 6 },
			{ "工事確定日", 7 },
			{ "工事確定時間", 8 },
			{ "工事結果", 9 },
			{ "工事結果詳細_工事NG時", 10 },
			{ "工事確定日_過去日", 11 },
			{ "完了報告書受領日", 12 },
			{ "備考_工事関連", 13 },
			{ "第1希望日", 14 },
			{ "第1希望時間", 15 },
			{ "第2希望日", 16 },
			{ "第2希望時間", 17 },
			{ "第3希望日", 18 },
			{ "第3希望時間", 19 },
			{ "OK曜日_時間含む", 20 },
			{ "平日夜間土休日", 21 },
			{ "取消", 22 },
			{ "直前リスケ数", 23 },
			{ "直前リスケ内容", 24 },
			{ "BO", 25 },
			{ "フレッツ新規手配", 26 },
			{ "既存回線ID_新設回線ID", 27 },
			{ "回線品目", 28 },
			{ "フレッツ工事_工事確定日", 29 },
			{ "フレッツ工事工事確定時間", 30 },
			{ "フレッツ工事工事結果", 31 },
			{ "フレッツ工事工事確定日_過去日", 32 },
			{ "フレッツ工事備考_工事関連", 33 },
			{ "都道府県", 34 },
			{ "住所", 35 },
			{ "架電先対応者", 36 },
			{ "架電番号", 37 },
			{ "入館調整ステータス", 38 },
			{ "ヒアリングシートチェック結果", 39 },
			{ "ヒアリングシート修正依頼日", 40 },
			{ "連絡票受領日", 41 },
			{ "モバイルディスプレイ", 42 },
			{ "VGA端子", 43 },
			{ "CPU切替器", 44 },
			{ "GW設置確認", 45 },
			{ "HUB台数", 46 },
			{ "オン資ルータ", 47 },
			{ "PCモニタ", 48 },
			{ "既存ルータ型番", 49 },
			{ "V6変更有無", 50 },
			{ "V6オプション開通確認日", 51 },
			{ "新規開通申込み完了_営業", 52 },
			{ "機器到着予定日", 53 },
			{ "到着完了確認日", 54 },
			{ "LAN調査_有無", 55 },
			{ "LAN調査_依頼日", 56 },
			{ "LAN調査_工事日", 57 },
			{ "LAN調査_直前リスト回数", 58 },
			{ "LAN調査_リスケ内容", 59 },
			{ "LAN調査_完了報告受領日", 60 },
			{ "LAN調査_平日夜間土休日", 61 },
			{ "LAN調査_呼び線挿入不可", 62 },
			{ "LAN調査_NG営業連絡日", 63 },
			{ "LAN調査_BO", 64 },
			{ "LAN調査_NG記事", 65 },
			{ "LAN配線_必要可否", 66 },
			{ "LAN配線_依頼日", 67 },
			{ "LAN配線_工事日", 68 },
			{ "LAN配線_取消", 69 },
			{ "LAN配線_直前リスケ回数", 70 },
			{ "LAN配線_リスケ内容", 71 },
			{ "LAN配線_平日夜間土休日", 72 },
			{ "LAN配線_工事完了受領日", 73 },
			{ "LAN配線_延長単位_10m", 74 },
			{ "LAN配線_ワイプロ延長単位_1m", 75 },
			{ "LAN配線_BO", 76 },
			{ "LAN配線_記事", 77 },
			{ "委託業務完成通知書送付日", 78 },
			{ "作業報告書_PDF_送付月25日締め_NTT_ミック", 79 },
			{ "追加費用1", 80 },
			{ "追加費用2", 81 },
			{ "補助金申請書類送付日_NTT_医療機関", 82 },
			{ "機器設定作業料金_プランA_平日日中帯", 83 },
			{ "機器設定作業料金_プランA_夜間土休日", 84 },
			{ "機器設定作業料金_プランB_平日日中帯", 85 },
			{ "機器設定作業料金_プランB_夜間土休日", 86 },
			{ "機器設定作業料金_機器代金のみ", 87 },
			{ "機器設定作業料金_再派遣料金ver1_平日日中帯", 88 },
			{ "機器設定作業料金_再派遣料金ver1_夜間土休日", 89 },
			{ "機器設定作業料金_再派遣料金ver2_平日日中帯", 90 },
			{ "機器設定作業料金_再派遣料金ver2_夜間土休日", 91 },
			{ "機器設定作業料金_再派遣料金ver3_平日日中帯", 92 },
			{ "機器設定作業料金_再派遣料金ver3_夜間土休日", 93 },
			{ "機器設定作業料金_規定後リスケ料金_平日日中帯", 94 },
			{ "機器設定作業料金_規定後リスケ料金_夜間土休日", 95 },
			{ "機器設定作業料金_作業キャンセルA_平日日中帯", 96 },
			{ "機器設定作業料金_作業キャンセルA_夜間土休日", 97 },
			{ "機器設定作業料金_作業キャンセルA1_平日日中帯", 98 },
			{ "機器設定作業料金_作業キャンセルA1_夜間土休日", 99 },
			{ "現地調査_割増料金_夜間土休日", 100 },
			{ "現地調査_再派遣料金_平日日中帯", 101 },
			{ "現地調査_再派遣料金_夜間土休日", 102 },
			{ "現地調査_規定後リスケ料金_平日日中帯", 103 },
			{ "現地調査_規定後リスケ料金_夜間土休日", 104 },
			{ "現地調査_作業キャンセル_平日日中帯", 105 },
			{ "現地調査_作業キャンセル_夜間土休日", 106 },
			{ "LAN配線作業_割増料金_夜間土休日", 107 },
			{ "LAN配線作業_再派遣料金_平日日中帯", 108 },
			{ "LAN配線作業_再派遣料金_夜間土休日", 109 },
			{ "LAN配線作業_規定後リスケ料金_平日日中帯", 110 },
			{ "LAN配線作業_規定後リスケ料金_夜間土休日", 111 },
			{ "LAN配線作業_作業キャンセルB_平日日中帯", 112 },
			{ "LAN配線作業_作業キャンセルB_夜間土休日", 113 },
			{ "LAN配線作業_作業キャンセルB1_平日日中帯", 114 },
			{ "LAN配線作業_作業キャンセルB1_夜間土休日", 115 },
			{ "LAN配線_延長", 116 },
			{ "ワイプロ_延長", 117 },
			{ "その他実費費", 118 },
			{ "備考", 119 },
			{ "請求金額", 120 },
			{ "連絡項目", 121 },
			{ "連絡内容", 122 },
		};

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
		/// <param name="version">進捗管理表バージョン番号</param>
		public void SetWorksheetBy進捗管理表(IXLWorksheet ws, int row, int version)
		{
			ReadWorksheet(ws, row, version, 2);
		}

		/// <summary>
		/// ワークシートの読込(オンライン資格確認通知結果)
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		/// <param name="version">進捗管理表バージョン番号</param>
		public void SetWorksheetByオンライン資格確認通知結果(IXLWorksheet ws, int row, int version)
		{
			Notice.ReadWorksheet(ws, row);
			ReadWorksheet(ws, row, version, Notice.GetColumn);
		}

		/// <summary>
		/// ワークシートの読込(進捗管理表)
		/// </summary>
		/// <param name="ws">ワークシート</param>
		/// <param name="row">行</param>
		/// <param name="version">進捗管理表バージョン番号</param>
		/// <param name="startCol">開始カラム</param>
		private void ReadWorksheet(IXLWorksheet ws, int row, int version, int startCol)
		{
			受付通番 = ws.Cell(row, GetFieldNumber("受付通番", version) + startCol).GetString();
			申込日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("申込日", version) + startCol));
			担当拠点 = ws.Cell(row, GetFieldNumber("担当拠点", version) + startCol).GetString();
			担当者 = ws.Cell(row, GetFieldNumber("担当者", version) + startCol).GetString();
			病院ID = ws.Cell(row, GetFieldNumber("病院ID", version) + startCol).GetString().ToInt();
			医療機関名 = ws.Cell(row, GetFieldNumber("医療機関名", version) + startCol).GetString();
			工事確定日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("工事確定日", version) + startCol));
			工事確定時間 = Program.GetTimeString(ws.Cell(row, GetFieldNumber("工事確定時間", version) + startCol));
			工事結果 = ws.Cell(row, GetFieldNumber("工事結果", version) + startCol).GetString();
			工事結果詳細_工事NG時 = ws.Cell(row, GetFieldNumber("工事結果詳細_工事NG時", version) + startCol).GetString();
			工事確定日_過去日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("工事確定日_過去日", version) + startCol));
			完了報告書受領日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("完了報告書受領日", version) + startCol));
			備考_工事関連 = ws.Cell(row, GetFieldNumber("備考_工事関連", version) + startCol).GetString();
			第1希望日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("第1希望日", version) + startCol));
			第1希望時間 = Program.GetTimeString(ws.Cell(row, GetFieldNumber("第1希望時間", version) + startCol));
			第2希望日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("第2希望日", version) + startCol));
			第2希望時間 = Program.GetTimeString(ws.Cell(row, GetFieldNumber("第2希望時間", version) + startCol));
			第3希望日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("第3希望日", version) + startCol));
			第3希望時間 = Program.GetTimeString(ws.Cell(row, GetFieldNumber("第3希望時間", version) + startCol));
			OK曜日_時間含む = ws.Cell(row, GetFieldNumber("OK曜日_時間含む", version) + startCol).GetString();
			平日夜間土休日 = ws.Cell(row, GetFieldNumber("平日夜間土休日", version) + startCol).GetString();
			取消 = ws.Cell(row, GetFieldNumber("取消", version) + startCol).GetString();
			直前リスケ数 = ws.Cell(row, GetFieldNumber("直前リスケ数", version) + startCol).GetString();
			直前リスケ内容 = ws.Cell(row, GetFieldNumber("直前リスケ内容", version) + startCol).GetString();
			BO = ws.Cell(row, GetFieldNumber("BO", version) + startCol).GetString();
			フレッツ新規手配 = ws.Cell(row, GetFieldNumber("フレッツ新規手配", version) + startCol).GetString();
			既存回線ID_新設回線ID = ws.Cell(row, GetFieldNumber("既存回線ID_新設回線ID", version) + startCol).GetString();
			回線品目 = ws.Cell(row, GetFieldNumber("回線品目", version) + startCol).GetString();
			フレッツ工事_工事確定日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("フレッツ工事_工事確定日", version) + startCol));
			フレッツ工事工事確定時間 = ws.Cell(row, GetFieldNumber("フレッツ工事工事確定時間", version) + startCol).GetString();
			フレッツ工事工事結果 = ws.Cell(row, GetFieldNumber("フレッツ工事工事結果", version) + startCol).GetString();
			フレッツ工事工事確定日_過去日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("フレッツ工事工事確定日_過去日", version) + startCol));
			フレッツ工事備考_工事関連 = ws.Cell(row, GetFieldNumber("フレッツ工事備考_工事関連", version) + startCol).GetString();
			都道府県 = ws.Cell(row, GetFieldNumber("都道府県", version) + startCol).GetString();
			住所 = ws.Cell(row, GetFieldNumber("住所", version) + startCol).GetString();
			架電先対応者 = ws.Cell(row, GetFieldNumber("架電先対応者", version) + startCol).GetString();
			架電番号 = ws.Cell(row, GetFieldNumber("架電番号", version) + startCol).GetString();
			入館調整ステータス = ws.Cell(row, GetFieldNumber("入館調整ステータス", version) + startCol).GetString();
			ヒアリングシートチェック結果 = ws.Cell(row, GetFieldNumber("ヒアリングシートチェック結果", version) + startCol).GetString();
			ヒアリングシート修正依頼日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("ヒアリングシート修正依頼日", version) + startCol));
			連絡票受領日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("連絡票受領日", version) + startCol));
			モバイルディスプレイ = ws.Cell(row, GetFieldNumber("モバイルディスプレイ", version) + startCol).GetString();
			VGA端子 = ws.Cell(row, GetFieldNumber("VGA端子", version) + startCol).GetString();
			CPU切替器 = ws.Cell(row, GetFieldNumber("CPU切替器", version) + startCol).GetString();
			GW設置確認 = ws.Cell(row, GetFieldNumber("GW設置確認", version) + startCol).GetString();
			HUB台数 = ws.Cell(row, GetFieldNumber("HUB台数", version) + startCol).GetString();
			オン資ルータ = ws.Cell(row, GetFieldNumber("オン資ルータ", version) + startCol).GetString();
			PCモニタ = ws.Cell(row, GetFieldNumber("PCモニタ", version) + startCol).GetString();
			既存ルータ型番 = ws.Cell(row, GetFieldNumber("既存ルータ型番", version) + startCol).GetString();
			V6変更有無 = ws.Cell(row, GetFieldNumber("V6変更有無", version) + startCol).GetString();
			V6オプション開通確認日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("V6オプション開通確認日", version) + startCol));
			新規開通申込み完了_営業 = ws.Cell(row, GetFieldNumber("新規開通申込み完了_営業", version) + startCol).GetString();
			機器到着予定日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("機器到着予定日", version) + startCol));
			到着完了確認日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("到着完了確認日", version) + startCol));
			LAN調査_有無 = ws.Cell(row, GetFieldNumber("LAN調査_有無", version) + startCol).GetString();
			LAN調査_依頼日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("LAN調査_依頼日", version) + startCol));
			LAN調査_工事日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("LAN調査_工事日", version) + startCol));
			LAN調査_直前リスト回数 = ws.Cell(row, GetFieldNumber("LAN調査_直前リスト回数", version) + startCol).GetString();
			LAN調査_リスケ内容 = ws.Cell(row, GetFieldNumber("LAN調査_リスケ内容", version) + startCol).GetString();
			LAN調査_完了報告受領日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("LAN調査_完了報告受領日", version) + startCol));
			LAN調査_平日夜間土休日 = ws.Cell(row, GetFieldNumber("LAN調査_平日夜間土休日", version) + startCol).GetString();
			LAN調査_呼び線挿入不可 = ws.Cell(row, GetFieldNumber("LAN調査_呼び線挿入不可", version) + startCol).GetString();
			LAN調査_NG営業連絡日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("LAN調査_NG営業連絡日", version) + startCol));
			LAN調査_BO = ws.Cell(row, GetFieldNumber("LAN調査_BO", version) + startCol).GetString();
			LAN調査_NG記事 = ws.Cell(row, GetFieldNumber("LAN調査_NG記事", version) + startCol).GetString();
			LAN配線_必要可否 = ws.Cell(row, GetFieldNumber("LAN配線_必要可否", version) + startCol).GetString();
			LAN配線_依頼日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("LAN配線_依頼日", version) + startCol));
			LAN配線_工事日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("LAN配線_工事日", version) + startCol));
			LAN配線_取消 = ws.Cell(row, GetFieldNumber("LAN配線_取消", version) + startCol).GetString();
			LAN配線_直前リスケ回数 = ws.Cell(row, GetFieldNumber("LAN配線_直前リスケ回数", version) + startCol).GetString();
			LAN配線_リスケ内容 = ws.Cell(row, GetFieldNumber("LAN配線_リスケ内容", version) + startCol).GetString();
			LAN配線_平日夜間土休日 = ws.Cell(row, GetFieldNumber("LAN配線_平日夜間土休日", version) + startCol).GetString();
			LAN配線_工事完了受領日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("LAN配線_工事完了受領日", version) + startCol));
			LAN配線_延長単位_10m = ws.Cell(row, GetFieldNumber("LAN配線_延長単位_10m", version) + startCol).GetString();
			LAN配線_ワイプロ延長単位_1m = ws.Cell(row, GetFieldNumber("LAN配線_ワイプロ延長単位_1m", version) + startCol).GetString();
			LAN配線_BO = ws.Cell(row, GetFieldNumber("LAN配線_BO", version) + startCol).GetString();
			LAN配線_記事 = ws.Cell(row, GetFieldNumber("LAN配線_記事", version) + startCol).GetString();
			委託業務完成通知書送付日 = Program.GetDateString(ws.Cell(row, GetFieldNumber("委託業務完成通知書送付日", version) + startCol));
			作業報告書_PDF_送付月25日締め_NTT_ミック = ws.Cell(row, GetFieldNumber("作業報告書_PDF_送付月25日締め_NTT_ミック", version) + startCol).GetString();
			追加費用1 = ws.Cell(row, GetFieldNumber("追加費用1", version) + startCol).GetString();
			追加費用2 = ws.Cell(row, GetFieldNumber("追加費用2", version) + startCol).GetString();
			補助金申請書類送付日_NTT_医療機関 = ws.Cell(row, GetFieldNumber("補助金申請書類送付日_NTT_医療機関", version) + startCol).GetString();
			機器設定作業料金_プランA_平日日中帯 = ws.Cell(row, GetFieldNumber("機器設定作業料金_プランA_平日日中帯", version) + startCol).GetString();
			機器設定作業料金_プランA_夜間土休日 = ws.Cell(row, GetFieldNumber("機器設定作業料金_プランA_夜間土休日", version) + startCol).GetString();
			機器設定作業料金_プランB_平日日中帯 = ws.Cell(row, GetFieldNumber("機器設定作業料金_プランB_平日日中帯", version) + startCol).GetString();
			機器設定作業料金_プランB_夜間土休日 = ws.Cell(row, GetFieldNumber("機器設定作業料金_プランB_夜間土休日", version) + startCol).GetString();
			機器設定作業料金_機器代金のみ = ws.Cell(row, GetFieldNumber("機器設定作業料金_機器代金のみ", version) + startCol).GetString();
			機器設定作業料金_再派遣料金ver1_平日日中帯 = ws.Cell(row, GetFieldNumber("機器設定作業料金_再派遣料金ver1_平日日中帯", version) + startCol).GetString();
			機器設定作業料金_再派遣料金ver1_夜間土休日 = ws.Cell(row, GetFieldNumber("機器設定作業料金_再派遣料金ver1_夜間土休日", version) + startCol).GetString();
			機器設定作業料金_再派遣料金ver2_平日日中帯 = ws.Cell(row, GetFieldNumber("機器設定作業料金_再派遣料金ver2_平日日中帯", version) + startCol).GetString();
			機器設定作業料金_再派遣料金ver2_夜間土休日 = ws.Cell(row, GetFieldNumber("機器設定作業料金_再派遣料金ver2_夜間土休日", version) + startCol).GetString();
			機器設定作業料金_再派遣料金ver3_平日日中帯 = ws.Cell(row, GetFieldNumber("機器設定作業料金_再派遣料金ver3_平日日中帯", version) + startCol).GetString();
			機器設定作業料金_再派遣料金ver3_夜間土休日 = ws.Cell(row, GetFieldNumber("機器設定作業料金_再派遣料金ver3_夜間土休日", version) + startCol).GetString();
			機器設定作業料金_規定後リスケ料金_平日日中帯 = ws.Cell(row, GetFieldNumber("機器設定作業料金_規定後リスケ料金_平日日中帯", version) + startCol).GetString();
			機器設定作業料金_規定後リスケ料金_夜間土休日 = ws.Cell(row, GetFieldNumber("機器設定作業料金_規定後リスケ料金_夜間土休日", version) + startCol).GetString();
			機器設定作業料金_作業キャンセルA_平日日中帯 = ws.Cell(row, GetFieldNumber("機器設定作業料金_作業キャンセルA_平日日中帯", version) + startCol).GetString();
			機器設定作業料金_作業キャンセルA_夜間土休日 = ws.Cell(row, GetFieldNumber("機器設定作業料金_作業キャンセルA_夜間土休日", version) + startCol).GetString();
			機器設定作業料金_作業キャンセルA1_平日日中帯 = ws.Cell(row, GetFieldNumber("機器設定作業料金_作業キャンセルA1_平日日中帯", version) + startCol).GetString();
			機器設定作業料金_作業キャンセルA1_夜間土休日 = ws.Cell(row, GetFieldNumber("機器設定作業料金_作業キャンセルA1_夜間土休日", version) + startCol).GetString();
			現地調査_割増料金_夜間土休日 = ws.Cell(row, GetFieldNumber("現地調査_割増料金_夜間土休日", version) + startCol).GetString();
			現地調査_再派遣料金_平日日中帯 = ws.Cell(row, GetFieldNumber("現地調査_再派遣料金_平日日中帯", version) + startCol).GetString();
			現地調査_再派遣料金_夜間土休日 = ws.Cell(row, GetFieldNumber("現地調査_再派遣料金_夜間土休日", version) + startCol).GetString();
			現地調査_規定後リスケ料金_平日日中帯 = ws.Cell(row, GetFieldNumber("現地調査_規定後リスケ料金_平日日中帯", version) + startCol).GetString();
			現地調査_規定後リスケ料金_夜間土休日 = ws.Cell(row, GetFieldNumber("現地調査_規定後リスケ料金_夜間土休日", version) + startCol).GetString();
			現地調査_作業キャンセル_平日日中帯 = ws.Cell(row, GetFieldNumber("現地調査_作業キャンセル_平日日中帯", version) + startCol).GetString();
			現地調査_作業キャンセル_夜間土休日 = ws.Cell(row, GetFieldNumber("現地調査_作業キャンセル_夜間土休日", version) + startCol).GetString();
			LAN配線作業_割増料金_夜間土休日 = ws.Cell(row, GetFieldNumber("LAN配線作業_割増料金_夜間土休日", version) + startCol).GetString();
			LAN配線作業_再派遣料金_平日日中帯 = ws.Cell(row, GetFieldNumber("LAN配線作業_再派遣料金_平日日中帯", version) + startCol).GetString();
			LAN配線作業_再派遣料金_夜間土休日 = ws.Cell(row, GetFieldNumber("LAN配線作業_再派遣料金_夜間土休日", version) + startCol).GetString();
			LAN配線作業_規定後リスケ料金_平日日中帯 = ws.Cell(row, GetFieldNumber("LAN配線作業_規定後リスケ料金_平日日中帯", version) + startCol).GetString();
			LAN配線作業_規定後リスケ料金_夜間土休日 = ws.Cell(row, GetFieldNumber("LAN配線作業_規定後リスケ料金_夜間土休日", version) + startCol).GetString();
			LAN配線作業_作業キャンセルB_平日日中帯 = ws.Cell(row, GetFieldNumber("LAN配線作業_作業キャンセルB_平日日中帯", version) + startCol).GetString();
			LAN配線作業_作業キャンセルB_夜間土休日 = ws.Cell(row, GetFieldNumber("LAN配線作業_作業キャンセルB_夜間土休日", version) + startCol).GetString();
			LAN配線作業_作業キャンセルB1_平日日中帯 = ws.Cell(row, GetFieldNumber("LAN配線作業_作業キャンセルB1_平日日中帯", version) + startCol).GetString();
			LAN配線作業_作業キャンセルB1_夜間土休日 = ws.Cell(row, GetFieldNumber("LAN配線作業_作業キャンセルB1_夜間土休日", version) + startCol).GetString();
			LAN配線_延長 = ws.Cell(row, GetFieldNumber("LAN配線_延長", version) + startCol).GetString();
			ワイプロ_延長 = ws.Cell(row, GetFieldNumber("ワイプロ_延長", version) + startCol).GetString();
			その他実費費 = ws.Cell(row, GetFieldNumber("その他実費費", version) + startCol).GetString();
			備考 = ws.Cell(row, GetFieldNumber("備考", version) + startCol).GetString();
			請求金額 = ws.Cell(row, GetFieldNumber("請求金額", version) + startCol).GetString();
			連絡項目 = ws.Cell(row, GetFieldNumber("連絡項目", version) + startCol).GetString();
			連絡内容 = ws.Cell(row, GetFieldNumber("連絡内容", version) + startCol).GetString();
		}
	}
}
