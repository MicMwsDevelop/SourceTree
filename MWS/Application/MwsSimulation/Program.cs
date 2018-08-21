//
// Program.cs
//
// MIC WEB SERVICE 課金シミュレーション
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
// Ver1.010 見積書に有効期限を印刷(2018/08/02 勝呂)
// Ver1.020 サービス情報の並びをサービス種別、サービスＩＤの並び順に変更(2018/08/02 勝呂)
// Ver1.030 電子カルテ標準サービス申込時に、１号カルテ標準サービスと２号カルテ標準サービスの申込をチェックする(2018/08/10 勝呂)
// 
using MwsSimulation.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace MwsSimulation
{
	static class Program
	{
		/// <summary>
		/// カレントデータフォルダ
		/// </summary>
		private const string CURRENT_DATA_FOLDER = @"C:\MwsSimulation";

		/// <summary>
		/// サーバーデータフォルダ
		/// </summary>
		public const string SERVER_DATA_FOLDER = @"\\storage\公開データ\サポート課公開用\02_Tools類\その他\MwsSimulation";

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

		/// <summary>
		/// カレントデータフォルダの取得
		/// </summary>
		/// <returns>カレントデータフォルダ</returns>
		public static string GetDataFolder()
		{
			string systemDrive = Environment.GetEnvironmentVariable("SystemDrive");
			return Path.Combine(systemDrive, CURRENT_DATA_FOLDER);
		}
	}
}
