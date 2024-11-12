//
// Program.cs
//
// MIC WEB SERVICE 課金シミュレーション
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// アプリ管理サイト：APPID608 MWS課金シミュレーション
// 処理概要：MWSサービスの課金シミュレーションを行い、見積書を発行する
// 入力ファイル：無
// 出力ファイル：無
// 印刷物：見積書、請求書、請求請書
// メール送信：無
/////////////////////////////////////////////////////////
// Ver2.000 新規作成(2018/10/24 勝呂)
// Ver2.100 おまとめプラン48ヵ月、60ヵ月に対応(2019/01/22 勝呂)
// Ver2.101 おまとめプランの選択を12ヵ月、36ヵ月、60ヵ月に変更(2019/07/19 勝呂)
// Ver2.101 消費税率の取得をMwsSimulationMaster.dbから[JunpDB].[dbo].[vMicPCA消費税率]に変更(2019/07/19 勝呂)
// Ver2.210 おまとめプランにクラウドバックアップが含まれないように対応(2020/11/20 勝呂)
// Ver2.220 Web予約受付に対応(2021/09/07 勝呂)
// Ver2.30(2024/10/04 勝呂):サービス情報マスタにフィールドを追加して、おまとめプランに含めるかどうかの判断するように仕様変更
/////////////////////////////////////////////////////////
// 未リリース
// Ver2.31(2024/08/01 勝呂):おまとめプラン60ヵ月販売終了に対応
// 
using MwsSimulation.Forms;
using System;
using System.Windows.Forms;

namespace MwsSimulation
{
	static class Program
	{
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
