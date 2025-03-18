//
// Program.cs
//
// AGREX銀行マスタコンバーター プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
//////////////////////////////////////////////////////////////////
// 【作成背景】
// 2025/02/05に経理課で008_AGREXアプリの銀行コード一括更新作業ででエラーが発生した。
// 調査した結果、AGREXが提供している銀行マスタのデータフォーマットが変更されていた。
// AGREXに問い合わせた結果、システムの入替後のデータ入力作業に不備があった。
// 現在、データの再入力を行っているが、作業終了予定は2025/03末であり、それまでは不備のある銀行マスタを
// 使用してほしいとのこと。
// 対策としてAGRXアプリを改修する方法もあるが、一時的なデータの不具合なので、本来のデータに変換するコンバート
// プログラムを作成して対応することにする。
//
// 【処理内容】
// 1. AGREX銀行マスタの読込
// 2. 第２フィールドの銀行名から半角スペースを排除
// 3. コンバートしたAGREX銀行マスタを "NewAgrexBank.txt" で出力
//////////////////////////////////////////////////////////////////
// Ver1.00(2025/02/25 勝呂):新規作成
//
using System;
using System.Windows.Forms;

namespace ConvertAgrexBankMaster
{
	internal static class Program
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
