//
// Program.cs
//
// デモユーザー palette ESサービス設定プログラム
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/10/18 勝呂)
// 
using System;
using System.Windows.Forms;

namespace DemoUserPaletteES
{
	static class Program
    {
		/// <summary>
		/// データベース接続先 CT環境
		/// </summary>
		public static readonly bool DATABACE_ACCEPT_CT = false;

		/// <summary>
		/// プログラム名
		/// </summary>
		public static readonly string ProgramName = "DemoUserPaletteES";

		/// <summary>
		/// バージョン情報
		/// </summary>
		public static string VersionStr = "バージョン：Ver1.00（2019/10/24）";

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
