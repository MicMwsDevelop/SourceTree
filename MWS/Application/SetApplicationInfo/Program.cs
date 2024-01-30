//
// Program.cs
// 
// アプリケーション情報設定 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/01/22 勝呂):新規作成
//
using System;
using System.Windows.Forms;

namespace SetApplicationInfo
{
	internal static class Program
	{
		/// <summary>
		/// プロシージャ名
		/// </summary>
		public static string ProcName = "アプリケーション情報設定";

		/// <summary>
		/// バージョン番号
		/// </summary>
		public static string VersionStr = "Ver1.00";

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
	}
}
