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
//
//  1:銀行振込請求書発行メモ追加
//    目的：未入金情報をメモに登録する
//    使用者：経理部
//    使用時期：毎月１回 10日締作業以降
//    入力ファイル：yyyy年mm月dd日締 請求書発行先.xlsx
//    出力ファイル：無
//    印刷物：無
//    メール送信：無
//    メモ文字列：【銀行振込請求書発行】
//                    2022 / 03 / 10締 請求額 \13,750
//
//  2:オン資格補助金申請書類メモ追加
//    目的：助成金申請書類の発送情報をメモに登録する
//    使用者：経理部
//    使用時期：毎月１回 助成金申請書類の発送時
//    入力ファイル：yyyy年mm月dd日オン資格書類発送先.xlsx
//    出力ファイル：無
//    印刷物：無
//    メール送信：無
//    メモ文字列：【オン資格補助金申請書類】
//                    2022 / 01 / 27 \429,000 口座振替分の領収証・領収書内訳書・オンライン資格確認事業完了報告書・注文確認書発行→2/25 発送
//
//  3:厚生局データメモ追加
//    目的：毎月厚生局が更新する新規保険医療機関データを元にWWの顧客情報を追加・更新する
//    使用者：システム管理部
//    使用時期：毎月１回 
//    入力ファイル：yyyy年m月処理分.xlsx
//    出力ファイル：無
//    印刷物：無
//    メール送信：無
//    メモ文字列：【新規指定保険医療機関・保険薬局一覧表】
//                    顧客情報新規対象先
//                    登録理由：新規
 //                   更新日:2023/07/10
/////////////////////////////////////////////////////////
// Ver1.00(2022/03/17 勝呂):新規作成
// Ver1.01(2022/03/25 勝呂):メモ種別と更新者を経理部に変更
// Ver1.02(2023/02/17 勝呂):メモの文言の変更「口座振替分の領収証・領収書内訳書・オンライン資格確認事業完了報告書発行」 →「口座振替分の領収証・領収書内訳書・オンライン資格確認事業完了報告書・注文確認書発行」
// Ver1.03(2023/08/21 勝呂):厚生局データメモ追加機能の追加
/////////////////////////////////////////////////////////
//
using ClosedXML.Excel;
using CommonLib.Common;
using System;
using System.Windows.Forms;
using WonderWebEntryMemo.Settings;

namespace WonderWebEntryMemo
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
		public const string ProgramVersion = "Ver1.03 2023/08/21";

		/// <summary>
		/// tMemo.fMemTable 格納文字列
		/// </summary>
		public const string MemoTableString = "tClient";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static WonderWebEntryMemoSettings gSettings { get; set; }

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
		/// <param name="updateMan">更新者</param>
		/// <returns></returns>
		public static string MemoTypeString(string updateMan)
		{
			return string.Format("{0} {1} {2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), updateMan);
		}
	}
}
