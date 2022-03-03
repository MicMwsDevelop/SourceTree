//
// Program.cs
// 
// 仕入データ作成プログラムファイル
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/01/07 勝呂)
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
		public const string gVersionStr = "Ver1.00(2022/02/18)";

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
