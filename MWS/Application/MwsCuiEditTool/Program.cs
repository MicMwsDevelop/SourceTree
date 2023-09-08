//
// Program.cs
// 
// 顧客利用情報編集ツール
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/08/24 勝呂):新規作成
//
using MwsCuiEditTool.Settings;
using System;
using System.Windows.Forms;

namespace MwsCuiEditTool
{
	internal static class Program
	{
		/// <summary>
		/// 環境設定
		/// </summary>
		public static MwsCuiEditToolSettings gSettings { get; set; }

		/// <summary>
		/// プロシージャ名
		/// </summary>
		public static string ProcName = "顧客利用情報編集ツール";

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

			gSettings = MwsCuiEditToolSettingsIF.GetSettings();

			Application.Run(new Forms.MainForm());
		}
	}
}
