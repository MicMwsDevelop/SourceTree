//
// LogOut.cs
// 
// ログ出力クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace AdjustServiceApply.Log
{
	/// <summary>
	/// ログ出力クラス
	/// </summary>
	public static class LogOut
    {
		/// <summary>
		/// リトライ回数：20回 
		/// </summary>
		private static readonly int RETRY_COUNT = 20;

		/// <summary>
		/// 待ち時間：0.1秒（100ミリ秒）
		/// </summary>
		private static readonly int WAIT_TIME = 100;

		/// <summary>
		/// エンコーディング 
		/// </summary>
		private static Encoding Encoding = Encoding.GetEncoding("shift_jis");

		/// <summary>
		/// ログファイルパス名
		/// </summary>
		private static string LogPathname = Program.ProcName + ".log";

		/// <summary>
		/// ログファイルパス名の設定
		/// </summary>
		/// <param name="folder">パス</param>
		public static void SetLogFileName(string folder)
		{
			string filename = string.Format("{0}_{1}.log", Program.ProcName, DateTime.Now.ToString("yyyyMMddHHmmss"));
			LogPathname = Path.Combine(folder, filename);
		}

		/// <summary>
		/// ログ出力
		/// </summary>
		/// <param name="fullPath">ログファイルフルパス</param>
		/// <param name="value">出力内容</param>
		public static void Out(string value)
		{
			for (int i = 0; i < RETRY_COUNT; i++)
			{
				if (Writting(value))
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
		/// <param name="value">出力内容</param>
		/// <returns>true:ログ書き込みが成功した場合, false:ログファイルが他プロセスで使用中の場合</returns>
		private static bool Writting(string value)
		{
			try
			{
				// 後続の処理で読み取り専用で開く場合のみ許可する
				// ※書き込みで開かれた場合はIOException）
				using (FileStream fileStream = new FileStream(LogPathname, FileMode.Append, FileAccess.Write, FileShare.Read))
				{
					using (StreamWriter writer = new StreamWriter(fileStream, Encoding))
					{
						string buf = string.Format("{0}:{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), value);
						writer.WriteLine(buf);
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
		/// <returns></returns>
		public static List<string> Read(string fullPath)
		{
			List<string> result = new List<string>();

			// ファイル存在確認
			if (File.Exists(LogPathname))
			{
				string line = "";
				using (StreamReader sr = new StreamReader(LogPathname, Encoding))
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
