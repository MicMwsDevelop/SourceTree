//
// Program.cs
// 
// オンライン資格確認通知 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/03/10 勝呂)
// Ver1.03 メール件名に顧客Noを追加(2022/04/05 勝呂)
// Ver1.03 メール本文に保存先を追加(2022/04/05 勝呂)
// Ver1.04 NTT西日本進捗管理表新フォームに対応(2022/04/22 勝呂)
// Ver1.05 メール本文にファイル名記載例を追加(2022/05/12 勝呂)
// Ver1.06 通知５の判定を本日以降の工事確定日付のみ検索するように抽出条件を変更(2022/05/17 勝呂)
//
/////////////////////////////////////////////////////////////////////////////////
// NTT東日本 申告管理表 変更履歴
//
//
// NTT西日本 申告管理表 変更履歴
// 2022/04/20版：機器設定作業料金にプランA(新価格）、プランB（新価格）、機器代金（新価格）、HUB（オプション）、モバイルディスプレイ（オプション）欄の追加→Ver1.04で対応
//
// NTT西日本 連絡表 変更履歴
//
//
/////////////////////////////////////////////////////////////////////////////////
//
using ClosedXML.Excel;
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
		public const string ProgramVersion = "Ver1.06 2022/05/17";

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
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
