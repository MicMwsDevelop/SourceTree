//
// DemoUserPaletteESLogger.cs
// 
// デモユーザー palette ESサービス設定プログラム ログ出力クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/10/18 勝呂)
//
using MwsLib.Log;
using System;
using System.IO;

namespace DemoUserPaletteES.Log
{
	/// <summary>
	/// デモユーザー palette ESサービス設定ログクラス
	/// </summary>
	public static class DemoUserPaletteESLogger
	{
        // ログファイル名
        private static readonly string LOGFILE_NAME = "DemoUserPaletteES.LOG";

        // 日付フォーマット
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
        /// メインログ出力
        /// </summary>
        /// <param name="value">出力内容</param>
        public static void MainLine(string value)
        {
			Logger.Out(Pathname, string.Format("palette ESサービス設定追加:{0},{1}", DateTime.Now.ToString(DATE_FORMAT), value));
        }

		/// <summary>
		/// サブログ出力（インデント付き）
		/// </summary>
		/// <param name="mwsID">MWS-ID</param>
		/// <param name="name">ユーザー名</param>
		/// <param name="result">結果</param>
		public static void SubLine(string mwsID, string name, string result)
        {
			Logger.Out(Pathname, string.Format("  #ESサービス設定ユーザー,{0},{1},{2},{3}", DateTime.Now.ToString(DATE_FORMAT), mwsID, name, result));
		}
	}
}
