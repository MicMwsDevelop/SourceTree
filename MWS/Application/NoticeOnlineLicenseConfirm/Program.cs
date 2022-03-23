//
// Program.cs
// 
// オンライン資格確認通知 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/03/10 勝呂)
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
		public const string ProgramVersion = "Ver1.01 2022/03/23";

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
