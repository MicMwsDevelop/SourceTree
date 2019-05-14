//
// Program.cs
//
// 申込種別まとめ更新コンソールアプリケーション
//
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/02/08 勝呂)
// 
using MwsLib.BaseFactory.ApplyTypeMatome;
using MwsLib.Common;
using MwsLib.DB.SqlServer.ApplyTypeMatome;
using MwsLib.Log;
using System;
using System.Collections.Generic;
using System.IO;

namespace UpdateApplyTypeMatome
{
	class Program
	{
		/// <summary>
		/// デバッグモード
		/// </summary>
		private static bool DebugMode = true;

		/// <summary>
		/// プログラム名
		/// </summary>
		public static readonly string PROGRAM_NAME = "申込種別まとめ更新ツール";

		/// <summary>
		/// ログファイル名
		/// </summary>
		public static readonly string LOG_FILENAME = "UpdateApplyTypeMatome-{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}.log";

		/// <summary>
		/// メイン処理
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			string logPathname = Program.GetLogPathname();
			Logger.Out(logPathname, string.Format("{0} {1}:申込種別まとめ更新 開始", Program.PROGRAM_NAME, DateTime.Now.ToString()));

			// 申込種別の取得
			List<ApplyTypeMatomeData> list = ApplyTypeMatomeAccess.GetApplyTypeMatomeList(Date.Today, DebugMode);

			List<ApplyTypeMatomeData> org = new List<ApplyTypeMatomeData>();
			foreach (ApplyTypeMatomeData apply in list)
			{
				org.Add(apply.CloneDeep());

				// 申込種別をまとめに変更
				apply.ApplyType = MwsLib.BaseFactory.MwsDefine.ApplyType.Matome;
			}
			try
			{
				// 申込種別の格納
				ApplyTypeMatomeAccess.SetApplyTypeMatome(list, DebugMode);
				foreach (ApplyTypeMatomeData apply in org)
				{
					// ログ出力
					Logger.Out(logPathname, apply.ToLog());
				}
			}
			catch (Exception ex)
			{
				Logger.Out(logPathname, string.Format("#ERROR:ApplyTypeMatomeAccess.SetApplyTypeMatome ({0})", ex.Message));
				Logger.Out(logPathname, string.Format("{0} {1}:申込種別まとめ更新 異常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
				return;
			}
			Logger.Out(logPathname, string.Format("{0} {1}:申込種別まとめ更新 正常終了", Program.PROGRAM_NAME, DateTime.Now.ToString()));
		}

		/// <summary>
		/// ログファイルのパス名を取得
		/// </summary>
		/// <returns>ログファイルパス名</returns>
		private static string GetLogPathname()
		{
			return Path.Combine(Directory.GetCurrentDirectory(), string.Format(LOG_FILENAME, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute));
		}
	}
}
