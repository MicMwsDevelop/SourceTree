//
// Program.cs
//
// サービス申込情報更新処理 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using AdjustServiceApply.Settings;
using System;
using System.Windows.Forms;
using AdjustServiceApply.Log;
using System.IO;

namespace AdjustServiceApply
{
	internal static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string ProcName = "サービス申込情報更新処理";

		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string VersionStr = "1.00";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static AdjustServiceApplySettings gSettings { get; set; }

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			gSettings = AdjustServiceApplySettingsIF.GetSettings();

			Application.Run(new Forms.MainForm());
		}
	}
}
