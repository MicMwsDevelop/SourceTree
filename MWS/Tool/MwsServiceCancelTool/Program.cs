//
// Program.cs
// 
// MWSサービス利用申込取消ツール
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/06/11 勝呂):新規作成
//
using MwsServiceCancelTool.Settings;
using System;
using System.Windows.Forms;

namespace MwsServiceCancelTool
{
	internal static class Program
	{
		/// <summary>
		/// 環境設定
		/// </summary>
		public static MwsServiceCancelToolSettings gSettings { get; set; }

		/// <summary>
		/// プロシージャ名
		/// </summary>
		public static string ProcName = "MWSサービス利用申込取消ツール";

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

			gSettings = MwsServiceCancelToolSettingsIF.GetSettings();

			Application.Run(new Forms.MainForm());
		}
	}
}
