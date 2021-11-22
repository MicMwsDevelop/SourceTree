//
// Program.cs
// 
// 配送センター納品書印刷 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/10/25 勝呂)
//
using System;
using System.Windows.Forms;

namespace DeliveryCenterPrintNouhin
{
	static class Program
	{
		/// <summary>
		/// プログラム名称
		/// </summary>
		static public readonly string ProgramName = "Mic 納品書印刷";

		/// <summary>
		/// バージョン情報
		/// </summary>
		static public readonly string ProgramVersion = "Ver1.00(2021/11/22)";

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
