//
// Program.cs
// 
// 各種書類出力 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/04/22 勝呂)
//
using System;
using System.Windows.Forms;

namespace VariousDocumentOut
{
	static class Program
	{
		/// <summary>
		/// データベース接続先
		/// </summary>
		public const bool DATABASE_ACCESS_CT = false;

		/// <summary>
		/// プログラム名
		/// </summary>
		public const string ProgramName = "各種書類出力";

		/// <summary>
		/// バージョン番号
		/// </summary>
		public const string VersionStr = "Ver1.01 (2021/08/06)";

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
