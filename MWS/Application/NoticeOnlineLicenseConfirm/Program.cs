//
// Program.cs
// 
// オンライン資格確認通知 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// アプリ管理サイト：APPID633 オンライン資格確認通知
// 処理概要：オンライン資格確認の設置作業の進捗状況を管理するため、日々更新されるNTT東西の進捗管理表から得られる情報から
//           進捗状況や資料の不具合をチェックする機能を要し、MIC連絡担当者および営業管理部担当者に対し通知を発信する
// 入力ファイル：
//  NTT東日本【NTT東日本】申込書兼進捗管理表_yyyyMMdd.xlsx
//  NTT西日本【N】申込書兼進捗管理表_yyyyMMdd.xlsx、連絡票_yyyyMMdd.xlsx
// 出力ファイル：オンライン資格確認通知結果.xlsx
// 印刷物：無
// メール送信：有
//  【NTT東日本工事】 NTT東日本より工事確定日の連絡
//  【オンライン資格確認通知】 NTT西日本より工事確定日の連絡
//  【オンライン資格確認通知】 NTT東日本よりヒアリングシート不備の連絡
//  【オンライン資格確認通知】 NTT西日本よりヒアリングシート不備の連絡
//  【オンライン資格確認通知】 工事確定日14日前ヒアリングシート未完成通知
/////////////////////////////////////////////////////////
// Ver1.00 新規作成(2022/03/10 勝呂)
// Ver1.03 メール件名に顧客Noを追加(2022/04/05 勝呂)
// Ver1.03 メール本文に保存先を追加(2022/04/05 勝呂)
// Ver1.04 NTT西日本進捗管理表新フォーム(20220420版)に対応(2022/04/22 勝呂)
// Ver1.05 メール本文にファイル名記載例を追加(2022/05/12 勝呂)
// Ver1.06 通知５の判定を本日以降の工事確定日付のみ検索するように抽出条件を変更(2022/05/17 勝呂)
// Ver1.07 NTT東日本進捗管理表新フォーム(20220613版)に対応(2022/07/21 勝呂)
// Ver1.07 通知１、通知５のメール本文に工事確定時間を追加。東日本SGからの要望(2022/07/21 勝呂)
// Ver1.07 メール文字化け対応のため、メール送信方式とSmtpClientからMailKitに変更(2022/07/22 勝呂)
// Ver1.08 メイン画面 通知名称をメール件名に合わせる(2022/08/01 勝呂)
// Ver1.09 NTT西日本進捗管理表新フォーム(20220822版)に対応(2022/08/08 勝呂)
// Ver1.09 メール件名冒頭部を【オンライン資格確認通知】から【NTT東日本工事】【NTT西日本工事】に変更(2022/08/08 勝呂)
// Ver1.10 NTT西日本進捗管理表新フォーム(20220822版)MIC連絡担当者社員番号対応(2022/08/24 勝呂)
// Ver1.11 NTT現調プランに対応(2022/08/31 勝呂)
// Ver1.12 進捗管理表_作業情報に受付通番、工事結果、工事結果格納日時のフィールド追加に対応(2022/09/13 勝呂)
// Ver1.13 NTT西日本 進捗管理表の現調プランの情報が正しく読み込めていなかった為、NTT西日本の現調通知が機能していなかった(2022/09/29 勝呂)
// Ver1.14 現調及び工事の通知チェック後に設定する連絡用チェックボックスの制御が一部正しくなかった(2022/12/07 勝呂)
// Ver1.14 [進捗管理表_作業情報]に現調情報が登録されている場合、工事通知１(東西)を検出してもエクセル出力されなかった(2022/12/07 勝呂)
// Ver1.15 NTT東日本 現調通知３ 抽出条件の変更(2023/01/27 勝呂)
// Ver1.15 NTT東日本 現調通知３ 自動フローに対応(2023/01/27 勝呂)
/////////////////////////////////////////////////////////////////////////////////
// NTT東日本 申告管理表 変更履歴
// 2022/06/13版：事前調査→現地調査、委託業務完成通知書申請関連欄の追加 Ver1.07で対応
//
// NTT西日本 申告管理表 変更履歴
// 2022/04/20版：機器設定作業料金にプランA(新価格）、プランB（新価格）、機器代金（新価格）、HUB（オプション）、モバイルディスプレイ（オプション）欄の追加 Ver1.04で対応
// 2022/08/22版：NTT西日本進捗管理表新フォーム(20220822版)MIC連絡担当者社員番号対応(2022/08/24 勝呂) Ver1.10で対応
/////////////////////////////////////////////////////////////////////////////////
//
using ClosedXML.Excel;
using NoticeOnlineLicenseConfirm.Settings;
using System;
using System.Windows.Forms;

namespace NoticeOnlineLicenseConfirm
{
	static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string ProgramName = "オンライン資格確認通知";

		/// <summary>
		/// プログラムバージョン
		/// </summary>
		public const string ProgramVersion = "Ver1.15 2023/01/27";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static NoticeOnlineLicenseConfirmSettings gSettings { get; set; }

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// 環境設定の読込
			gSettings = NoticeOnlineLicenseConfirmSettingsIF.GetSettings();

			Application.Run(new Forms.MainForm());
		}

		/// <summary>
		/// 日付文字列の取得
		/// </summary>
		/// <param name="cell"></param>
		/// <returns>日付文字列</returns>
		public static string GetDateString(IXLCell cell)
		{
			if (XLDataType.DateTime == cell.DataType)
			{
				DateTime tm = cell.GetDateTime();
				return tm.ToShortDateString();
			}
			if (XLDataType.Text == cell.DataType)
			{
				return cell.GetString();
			}
			return string.Empty;
		}

		/// <summary>
		/// 時間文字列の取得
		/// </summary>
		/// <param name="cell"></param>
		/// <returns>時間文字列</returns>
		public static string GetTimeString(IXLCell cell)
		{
			if (XLDataType.DateTime == cell.DataType)
			{
				DateTime tm = cell.GetDateTime();
				return tm.ToShortTimeString();
			}
			if (XLDataType.Text == cell.DataType)
			{
				return cell.GetString();
			}
			return string.Empty;
		}
	}
}
