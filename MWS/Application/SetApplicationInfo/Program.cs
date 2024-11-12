//
// Program.cs
// 
// アプリケーション情報設定 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/01/22 勝呂):新規作成
// Ver1.01(2024/11/12 勝呂):ライセンスキー追加対応 MICオンライン資格確認保守サービス DX推進課依頼
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
		public static string VersionStr = "Ver1.01";

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
