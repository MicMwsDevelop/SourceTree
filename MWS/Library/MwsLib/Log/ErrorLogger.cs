//
// ErrorLogger.cs
// 
// エラーログ出力クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;
using System.IO;
using System.Reflection;

namespace MwsLib.Log
{
	/// <summary>
	/// ERRORログクラス
	/// </summary>
	public static class ErrorLogger
    {
        // ログファイル名
        private static readonly string LOGFILE_NAME = "ERROR.LOG";

        // 日付フォーマット
        private static readonly string DATE_FORMAT = "yyyy/MM/dd,HH:mm:ss";

        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="type">種別</param>
        /// <param name="value">出力内容</param>
        public static void Out(string type, string value)
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
            Logger.Out(fullPath, string.Format("#{0},{1},{2},{3}:{4}", type, dateTime, pcName, programName, value));
        }

        /// <summary>
        /// 警告ログ出力
        /// </summary>
        /// <param name="value">出力内容</param>
        public static void Warning(string value)
        {
            Out("WARNING", value);
        }

        /// <summary>
        /// エラーログ出力
        /// </summary>
        /// <param name="value">出力内容</param>
        public static void Error(string value)
        {
            Out("ERROR", value);
        }
    }
}
