//
// CheckVoucherPaletteESLogger.cs
// 
// ログ出力クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2019/11/15 勝呂)
//
using MwsLib.Log;
using System;
using System.Collections.Generic;
using System.IO;

namespace CheckVoucherPaletteES
{
	/// <summary>
	/// ログ出力クラス
	/// </summary>
	public static class CheckVoucherPaletteESLogger
	{
		/// <summary>
		/// ログファイル名
		/// </summary>
		private static readonly string LOGFILE_NAME = "CheckVoucherPaletteES.LOG";

		/// <summary>
		/// 日付フォーマット
		/// </summary>
		private static readonly string DATE_FORMAT = "yyyy/MM/dd,HH:mm:ss";

		/// <summary>
		/// ログファイルパス名の取得
		/// </summary>
		private static string Pathname
		{
			get
			{
				return Path.Combine(Directory.GetCurrentDirectory(), LOGFILE_NAME);
			}
		}

		/// <summary>
		/// ログ出力開始
		/// </summary>
		public static void LogStart()
		{
			Logger.Out(Pathname, string.Format("paletteES 起票確認 開始:{0}", DateTime.Now.ToString(DATE_FORMAT)));
		}

		/// <summary>
		/// ログ出力終了
		/// </summary>
		public static void LogEnd()
		{
			Logger.Out(Pathname, string.Format("paletteES 起票確認 終了:{0}", DateTime.Now.ToString(DATE_FORMAT)));
		}

		/// <summary>
		/// メインログ出力
		/// </summary>
		/// <param name="value">出力内容</param>
		public static void MainLine(string value)
        {
			Logger.Out(Pathname, value);
        }

		/// <summary>
		/// サブログ出力（インデント付き）
		/// </summary>
		/// <param name="list">ログリスト</param>
		public static void SubLine(List<string> list)
        {
			foreach (string str in list)
			{
				Logger.Out(Pathname, string.Format("  →{0}", str));
			}
		}
	}
}
