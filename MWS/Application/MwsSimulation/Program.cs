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
// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
// Ver1.050 おまとめプランが１円から適用できるように修正(2018/09/18 勝呂)
// Ver1.050 プラン割引、月額利用額をＭＷＳサイトに合わせ、100円未満切り捨てる(2018/09/21 勝呂)
// Ver1.050 見積書および注文書の宛先を「御中」と「様」を変更可能にする(2018/09/26 勝呂)
// Ver1.050 電子カルテ標準サービス選択時に１号カルテ標準サービスと２号カルテ標準サービスを選択状態にする(2018/09/26 勝呂)
// Ver1.050 電子カルテ標準サービス選択時にはTABLETビューワのサービス利用料の500円は加算しない(2018/09/26 勝呂)
// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
// Ver1.050 月額利用料の表示の追加(2018/09/27 勝呂)
// Ver1.050 見積書のコピー機能を追加(2018/09/27 勝呂)
// Ver1.050 備考の定型文登録機能を追加(2018/09/27 勝呂)
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
		// Ver1.030 電子カルテ標準サービス申込時に、１号カルテ標準サービスと２号カルテ標準サービスの申込をチェックする(2018/08/10 勝呂)
		public const string SERVICE_CODE_CHART_COMPUTE = "1042100";

		/// <summary>
		/// １号カルテ標準サービス サービスコード
		/// </summary>
		// Ver1.030 電子カルテ標準サービス申込時に、１号カルテ標準サービスと２号カルテ標準サービスの申込をチェックする(2018/08/10 勝呂)
		public const string SERVICE_CODE_CHART1_STD = "1012100";

		/// <summary>
		/// ２号カルテ標準サービス サービスコード
		/// </summary>
		// Ver1.030 電子カルテ標準サービス申込時に、１号カルテ標準サービスと２号カルテ標準サービスの申込をチェックする(2018/08/10 勝呂)
		public const string SERVICE_CODE_CHART2_STD = "1014100";

		/// <summary>
		/// TABLETビューワ サービスコード
		/// </summary>
		// Ver1.050 電子カルテ標準サービス選択時にはTABLETビューワのサービス利用料の500円は加算しない(2018/09/26 勝呂)
		public const string SERVICE_CODE_TABLETVIEWER = "1036240";

		/// <summary>
		/// カレントデータフォルダ
		/// </summary>
		private const string CURRENT_DATA_FOLDER = @"MwsSimulation";

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
			return string.Format("{0}\\{1}", systemDrive, CURRENT_DATA_FOLDER);
		}
	}
}
