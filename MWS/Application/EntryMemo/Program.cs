//
// Program.cs
// 
// WonderWebメモ追加 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// アプリ管理サイト：APPID632 WonderWebメモ追加
// 処理概要：WonderWebのメモ欄にExcelのリストからメモを追加する
//  1:銀行振込請求書発行メモ追加
//  2:オン資格補助金申請書類メモ追加
// 入力ファイル：Excelファイル
//  1:yyyy年mm月dd日締 請求書発行先.xlsx
//  2:yyyy年mm月dd日オン資格書類発送先.xlsx
// 出力ファイル：無
// 印刷物：無
// メール送信：無
/////////////////////////////////////////////////////////
// Ver1.00(2022/03/17 勝呂):新規作成
// Ver1.01(2022/03/25 勝呂):メモ種別と更新者を経理部に変更
// Ver1.02(2023/02/17 勝呂):メモの文言の変更「口座振替分の領収証・領収書内訳書・オンライン資格確認事業完了報告書発行」 →「口座振替分の領収証・領収書内訳書・オンライン資格確認事業完了報告書・注文確認書発行」
/////////////////////////////////////////////////////////
//
using ClosedXML.Excel;
using CommonLib.Common;
using EntryMemo.Settings;
using System;
using System.Windows.Forms;

namespace EntryMemo
{
	static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string ProgramName = "WonderWebメモ追加";

		/// <summary>
		/// プログラムバージョン
		/// </summary>
		public const string ProgramVersion = "Ver1.02 2023/02/17";

		/// <summary>
		/// tMemo.fMemTable 格納文字列
		/// </summary>
		public const string MemoTableString = "tClient";

		/// <summary>
		/// tMemo.fMemUpdateMan 格納文字列
		/// </summary>
		public const string MemoUpdateManString = "経理部";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static EntryMemoSettings gSettings { get; set; }

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
		/// 時間文字列の取得
		/// </summary>
		/// <param name="cell"></param>
		/// <returns>時間文字列</returns>
		public static double GetDouble(IXLCell cell)
		{
			if (XLDataType.Number == cell.DataType)
			{
				return cell.GetDouble();
			}
			if (null != cell.Value)
			{
				return 0;
			}
			return 0;
		}

		/// <summary>
		/// 日付文字列の取得
		/// </summary>
		/// <param name="cell"></param>
		/// <returns>時間文字列</returns>
		public static Date? GetDate(IXLCell cell)
		{
			if (XLDataType.DateTime == cell.DataType)
			{
				DateTime tm = cell.GetDateTime();
				return tm.ToDate();
			}
			else if (XLDataType.Text == cell.DataType)
			{
				string str = cell.GetString();
				Date date;
				if (Date.TryParse(str, out date))
				{
					return date;
				}
				return null;
			}
			return null;
		}

		/// <summary>
		/// tMemo.fMemType 格納文字列の取得
		/// </summary>
		/// <returns></returns>
		public static string MemoTypeString()
		{
			return string.Format("{0} {1} {2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), MemoUpdateManString);
		}
	}
}
