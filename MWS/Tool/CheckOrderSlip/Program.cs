//
// Program.cs
//
// 伝票確認ツール プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/04/17 勝呂)
// Ver1.01 エラー検出条件にリプレースなしを追加(2020/08/19 勝呂)
// Ver1.10 PC安心サポートPlus対応(2020/10/16 勝呂)
// Ver1.11 PC安心サポートPlus切替対応(2020/10/29 勝呂)
// Ver1.12 SQL Server接続情報を環境設定に移行(2021/09/07 勝呂)
// Ver1.13(2025/07/08 勝呂):palette ES 2025版に対応
// 
using CheckOrderSlip.Settings;
using System;
using System.Windows.Forms;

namespace CheckOrderSlip
{
	static class Program
	{
		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string VersionStr = "Ver1.13 (2025/07/08)";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static CheckOrderSlipSettings gSettings { get; set; }

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// 環境設定の読込
			gSettings = CheckOrderSlipSettingsIF.GetSettings();

			Application.Run(new Forms.MainForm());
		}
	}
}
