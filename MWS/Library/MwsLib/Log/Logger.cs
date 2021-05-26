//
// Logger.cs
// 
// ログ出力クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace MwsLib.Log
{
	/// <summary>
	/// ログクラス
	/// </summary>
	public static class Logger
    {
        // デフォルトの出力ファイルフルパス
        private static readonly string DEFAULT_FULLPATH = @".\ERROR.log";

        // リトライ回数：20回
        private static readonly int RETRY_COUNT = 20;

        // 待ち時間：0.1秒（100ミリ秒）
        private static readonly int WAIT_TIME = 100;

        // エンコーディング
        private static Encoding Encoding = Encoding.GetEncoding("shift_jis");

        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="fullPath">出力ファイルフルパス</param>
        /// <param name="value">出力内容</param>
        public static void Out(string fullPath, string value)
        {
            if (string.IsNullOrEmpty(fullPath))
            {
                fullPath = DEFAULT_FULLPATH;
            }

            for (int i = 0; i < RETRY_COUNT; i++)
            {
                if (Writting(fullPath, value))
                {
                    // 書き込み成功時は終了
                    break;
                }
                // 書き込み失敗時は、0.1秒待機してからリトライ
                Thread.Sleep(WAIT_TIME);
            }
        }

        /// <summary>
        /// ログファイルへの書き込み
        /// </summary>
        /// <param name="fullPath">出力ファイルフルパス</param>
        /// <param name="value">出力内容</param>
        /// <returns>true:ログ書き込みが成功した場合, false:ログファイルが他プロセスで使用中の場合</returns>
        private static bool Writting(string fullPath, string value)
        {
            try
            {
                // 後続の処理で読み取り専用で開く場合のみ許可する
                // ※書き込みで開かれた場合はIOException）
                using (FileStream fileStream = new FileStream(fullPath, FileMode.Append, FileAccess.Write, FileShare.Read))
                {
                    using (StreamWriter writer = new StreamWriter(fileStream, Encoding))
                    {
                        writer.WriteLine(value);
                        writer.Close();
                    }
                }
                return true;
            }
            catch (IOException)
            {
                // 他プロセスで使用中の場合はIOExceptionが発生する
                // このエラーの場合のみ、falseとして返す（それ以外の例外はエラーとして上位に返す）
                return false;
            }
        }

        /// <summary>
        /// ログファイルの読込み
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static List<string> Read(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath))
            {
                fullPath = DEFAULT_FULLPATH;
            }

            List<string> result = new List<string>();

            // ファイル存在確認
            if (File.Exists(fullPath))
            {
                string line = "";
                using (StreamReader sr = new StreamReader(fullPath, Encoding))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        result.Add(line);
                    }
                }
            }

            return result;
        }

    }
}
