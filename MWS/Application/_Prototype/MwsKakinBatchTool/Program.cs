//
// Program.cs
// 
// MWS課金バッチツール
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/08/23 勝呂):新規作成
//
using MwsKakinBatchTool.Settings;
using System;
using System.Windows.Forms;

namespace MwsKakinBatchTool
{
	internal static class Program
	{
		/// <summary>
		/// 環境設定
		/// </summary>
		public static MwsKakinBatchToolSettings gSettings { get; set; }

		/// <summary>
		/// プロシージャ名
		/// </summary>
		public static string ProcName = "課金代替バッチツール";

		/// <summary>
		/// 更新者
		/// </summary>
		public static string SectionName = "システム管理部";

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			gSettings = MwsKakinBatchToolSettingsIF.GetSettings();

			Application.Run(new Forms.MainForm());
		}
	}
}
