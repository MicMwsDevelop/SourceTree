//
// Program.cs
// 
// オンライン資格確認導入状況 プログラムファイル
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/08/29 勝呂)
// Ver1.01 総計の東日本営業部と西日本営業部に対応(2022/09/05 勝呂)
// Ver1.01 進捗管理情報を[SalesDB].[dbo].[オン資導入状況]に登録(2022/09/05 勝呂)
//
using System;
using System.Windows.Forms;

namespace OnlineLicenseIntroductionStatus
{
	static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string ProgramName = "オンライン資格確認導入状況";

		/// <summary>
		/// プログラムバージョン
		/// </summary>
		public const string ProgramVersion = "Ver1.00 2022/09/09";

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
