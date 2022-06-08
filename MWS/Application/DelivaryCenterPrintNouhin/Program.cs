//
// Program.cs
// 
// 配送センター納品書印刷 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// アプリ管理サイト：
// 処理概要：TOP印刷で使用する配送センター納品書印刷プログラム
// 入力ファイル：納品書CSVファイル
// 出力ファイル：
// 印刷物：配送センター納品書
// メール送信：無
/////////////////////////////////////////////////////////
// Ver1.00 新規作成(2021/10/25 勝呂)
// Ver1.01 着日指定の金額が０円で印刷されるので、金額が０円の時は印刷しない(2021/12/22 勝呂)
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
		static public readonly string ProgramVersion = "Ver1.01(2021/12/22)";

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
