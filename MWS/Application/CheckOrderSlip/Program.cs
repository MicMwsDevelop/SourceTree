//
// Program.cs
//
// 伝票確認ツール プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/04/17 勝呂)
// Ver1.01 エラー検出条件にリプレースなしを追加(2020/08/19 勝呂)
// 
using System;
using System.Windows.Forms;

namespace CheckOrderSlip
{
	static class Program
	{
		/// <summary>
		/// データベース接続先
		/// </summary>
		public const bool DATABASE_ACCESS_CT = false;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
	}
}
