//
// Program.cs
// 
// 仕入データ作成プログラムファイル
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/01/07 勝呂)
// Ver1.01 新規作成(2022/04/04 勝呂):ナルコーム仕入データ作成時に数量０を除外する
// Ver1.02 汎用データレイアウト 仕入明細データ Version 9(DX-Rev3.00)に対応(2022/05/25 勝呂)
//
using System;
using System.Windows.Forms;

namespace MakePurchaseFile
{
	static class Program
	{
		/// <summary>
		/// プログラムバージョン
		/// </summary>
		public const string gVersionStr = "Ver1.02(2022/05/25)";

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
