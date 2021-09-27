//
// MainteLogger.cs
// 
// メンテナンスログ出力クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using CommonLib.Common;
using System;
using System.IO;
using System.Reflection;

namespace MwsLib.Log
{
	/// <summary>
	/// メンテナンスログクラス
	/// </summary>
	public static class MainteLogger
    {
        // ログファイル名
        private static readonly string LOGFILE_NAME = "MAINTE.LOG";

        // 日付フォーマット
        private static readonly string DATE_FORMAT = "yyyy/MM/dd,HH:mm:ss";

        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="value">出力内容</param>
        private static void Out(string value)
        {
            // 日時取得
            string dateTime = DateTime.Now.ToString(DATE_FORMAT);

            // PC名取得
            string pcName = Environment.MachineName;

            // プログラム名取得
            Assembly asm = Assembly.GetEntryAssembly();
            if (asm == null)
            {
                asm = Assembly.GetCallingAssembly();
            }
            string appPath = asm.Location;
            string programName = Path.GetFileName(appPath);

            // ログ出力
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), LOGFILE_NAME);
            Logger.Out(fullPath, string.Format("#MakeMwsSimulationDB,{0},{1},{2}", dateTime, pcName, value));
        }

        /// <summary>
        /// メインログ出力
        /// </summary>
        /// <param name="programName">プログラム名</param>
        /// <param name="value">出力内容</param>
        public static void MainLine(string value)
        {
            Out(string.Format("MakeMwsSimulationDB:{0}", value));
        }

        /// <summary>
        /// サブログ出力（インデント付き）
        /// </summary>
        /// <param name="programName">プログラム名</param>
        /// <param name="value">出力内容</param>
        public static void SubLine(string value)
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), LOGFILE_NAME);

            string s = value;
            while (s != "")
            {
                string outStr = string.Empty;
                int p = s.IndexOf("\r\n");
                if (p >= 0)
                {
                    outStr = StringUtil.Left(s, p);
                    s = StringUtil.Mid(s, p + 2);
                }
                else
                {
                    outStr = s;
                    s = "";
                }
                Logger.Out(fullPath, string.Format("  {0}", outStr));
            }
        }
    }
}
