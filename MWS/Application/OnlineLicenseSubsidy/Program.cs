//
// Program.cs
// 
// オン資助成金申請書類出力 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// アプリ管理サイト：APPID623 オン資助成金申請書類出力
// 処理概要：経理部がオンライン資格確認業務で使用するツール
// 入力ファイル：
// 出力ファイル：
// 印刷物：無
// メール送信：無
/////////////////////////////////////////////////////////
// Ver1.00(2022/09/16 勝呂):新規作成
/////////////////////////////////////////////////////////
//
using ClosedXML.Excel;
using OnlineLicenseSubsidy.Settings;
using System;
using System.Windows.Forms;

namespace OnlineLicenseSubsidy
{
	static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string ProgramName = "オン資助成金申請書類出力";

		/// <summary>
		/// バージョン番号
		/// </summary>
		public const string VersionStr = "Ver1.00 (2022/09/20)";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static OnlineLicenseSubsidySettings gSettings { get; set; }

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			gSettings = OnlineLicenseSubsidySettingsIF.GetSettings();

			Application.Run(new Forms.MainForm());
		}

		/// <summary>
		/// Double型の取得
		/// </summary>
		/// <param name="cell"></param>
		/// <returns>日付文字列</returns>
		public static double GetDoubleData(IXLCell cell)
		{
			if (XLDataType.Number == cell.DataType)
			{
				return cell.GetDouble();
			}
			if (XLDataType.Text == cell.DataType)
			{
				return 0;
			}
			return 0;
		}

		/// <summary>
		/// 日付型の取得
		/// </summary>
		/// <param name="cell"></param>
		/// <returns>日付文字列</returns>
		public static DateTime? GetDateTimeData(IXLCell cell)
		{
			if (XLDataType.DateTime == cell.DataType)
			{
				return cell.GetDateTime();
			}
			return null;
		}
	}
}
