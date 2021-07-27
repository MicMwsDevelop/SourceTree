//
// Program.cs
// 
// WonderWeb見積書CSVファイル EXCEL出力 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2021/03/31):新規作成(勝呂)
// Ver1.02(2021/04/28):新規作成(勝呂)
// Ver1.03(2021/05/19):リース金額が０円でリース期間が設定されている時に月額リース金額の取得でエラー発生(勝呂)
// Ver1.04(2021/05/26):備考内にカンマがあるときにエラー発生(勝呂)
// Ver1.05(2021/06/11):注文書と注文請書の出力機能を追加(勝呂)
//
using System;
using System.Windows.Forms;

namespace WonderEstimateExcel
{
	static class Program
	{
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
