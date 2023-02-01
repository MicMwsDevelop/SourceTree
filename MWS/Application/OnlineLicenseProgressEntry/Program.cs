//
// Program.cs
// 
// オンライン資格確認進捗管理情報登録 プログラムファイル
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/09/28 勝呂)
// Ver1.01 マイナンバーカードの健康保険証利用対応の医療機関リスト（都道府県別）の運用開始日に対応(2022/12/12 勝呂)
// Ver1.02 猶予理由の追加、ステータス設定値の追加(2023/02/01 勝呂)
//
using ClosedXML.Excel;
using System;
using System.Windows.Forms;

namespace OnlineLicenseProgressEntry
{
	static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string ProgramName = "オンライン資格確認進捗管理情報登録";

		/// <summary>
		/// プログラムバージョン
		/// </summary>
		public const string ProgramVersion = "Ver1.02 2023/02/01";

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
	}
}
