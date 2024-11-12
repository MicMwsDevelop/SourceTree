//
// Program.cs
//
// MWSサービス価格改定売上データ作成ツール プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
//////////////////////////////////////////////////////////////////
// 【処理内容】
// 1. 課金データ作成バッチが出力した売上データの読込
//     ※月額課金サービス 利用申込初月分のみ集計
// 2. PCA商品マスタのエクスポートファイルの読み込み（旧価格）
// 3. 赤伝を新価格、黒伝を旧価格で出力
//     ※価格変更がサービスも赤伝、黒伝の出力対象とする（請求書や請求日の変更作業を簡易化するため）
//////////////////////////////////////////////////////////////////
// Ver1.00(2024/10/16 勝呂):新規作成
//
using PriceUpEarningsFile.Settings;
using System;
using System.Threading;
using System.Windows.Forms;

namespace PriceUpEarningsFile
{
	internal static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string gProcName = "MWSサービス価格改定売上データ作成ツール";

		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string gVersionStr = "1.00";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static PriceUpEarningsFileSettings gSettings { get; set; }

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static int Main()
		{
			int ret = 0;

			// Mutexオブジェクトを作成する ※多重起動禁止
			Mutex mutex = new Mutex(false, gProcName);

			bool hasHandle = false;
			try
			{
				try
				{
					// ミューテックスの所有権を要求する
					hasHandle = mutex.WaitOne(0, false);
				}
				//.NET Framework 2.0以降の場合
				catch (AbandonedMutexException)
				{
					// 別のアプリケーションがミューテックスを解放しないで終了した時
					hasHandle = true;
				}
				// ミューテックスを得られたか調べる
				if (hasHandle == false)
				{
					// 得られなかった場合は、すでに起動していると判断して終了
					//MessageBox.Show("多重起動はできません。");
					return 0;
				}
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				gSettings = PriceUpEarningsFileSettingsIF.GetSettings();
				Application.Run(new Forms.MainForm());
			}
			finally
			{
				if (hasHandle)
				{
					// ミューテックスを解放する
					mutex.ReleaseMutex();
				}
				mutex.Close();
			}
			return ret;
		}
	}
}
