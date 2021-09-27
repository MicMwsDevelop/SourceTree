//
// Program.cs
//
// MIC WEB SERVICE 課金シミュレーション
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
// Ver2.100 おまとめプラン48ヵ月、60ヵ月に対応(2019/01/22 勝呂)
// Ver2.101 おまとめプランの選択を12ヵ月、36ヵ月、60ヵ月に変更(2019/07/19 勝呂)
// Ver2.101 消費税率の取得をMwsSimulationMaster.dbから[JunpDB].[dbo].[vMicPCA消費税率]に変更(2019/07/19 勝呂)
// Ver2.210 おまとめプランにクラウドバックアップが含まれないように対応(2020/11/20 勝呂)
// Ver2.220 Web予約受付に対応(2021/09/07 勝呂)
// 
using MwsSimulation.Forms;
using System;
using System.Windows.Forms;

namespace MwsSimulation
{
	static class Program
	{
		/// <summary>
		/// 電子カルテ標準サービス サービスコード
		/// </summary>
		public const string SERVICE_CODE_CHART_COMPUTE = "1042100";

		/// <summary>
		/// １号カルテ標準サービス サービスコード
		/// </summary>
		public const string SERVICE_CODE_CHART1_STD = "1012100";

		/// <summary>
		/// ２号カルテ標準サービス サービスコード
		/// </summary>
		public const string SERVICE_CODE_CHART2_STD = "1014100";

		/// <summary>
		/// TABLETビューワ サービスコード
		/// </summary>
		public const string SERVICE_CODE_TABLETVIEWER = "1036240";

		/// <summary>
		/// paletteアカウント サービスコード
		/// </summary>
		public const string SERVICE_CODE_PALETTE_ACCOUNT = "1036220";

		/// <summary>
		/// リモートサービス サービスコード
		/// </summary>
		public const string SERVICE_CODE_REMOTE = "1038100";

		/// <summary>
		/// カレントデータフォルダ
		/// </summary>
		private const string CURRENT_DATA_FOLDER = @"MwsSimulation";

		/// <summary>
		/// サーバーデータフォルダ
		/// </summary>
		//public const string SERVER_DATA_FOLDER = @"\\storage\公開データ\サポート課公開用\02_Tools類\その他\MwsSimulation";

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
			return System.IO.Directory.GetCurrentDirectory();
		}
	}
}
