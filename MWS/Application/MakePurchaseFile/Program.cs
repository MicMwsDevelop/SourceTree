//
// Program.cs
// 
// 仕入データ作成プログラムファイル
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// アプリ管理サイト：APPID631 仕入データ作成
// 処理概要：PCA汎用データレイアウト仕入明細データの出力
// 入力ファイル：無
// 出力ファイル：汎用データレイアウト仕入明細データ
//  1:りすとん月額仕入データファイル
//  2:Microsoft365仕入データファイル
//  3:問心伝月額仕入データファイル
//  4:Curline本体アプリ仕入データファイル
//  5:ナルコーム仕入データファイル
//  6:クラウドバックアップ仕入データファイル
//  7:アルメックス保守仕入データファイル
//  8:オン資格保守サービス仕入データファイル
// 印刷物：無
// メール送信：無
/////////////////////////////////////////////////////////
// Ver1.00 新規作成(2022/01/07 勝呂)
// Ver1.01 新規作成(2022/04/04 勝呂):ナルコーム仕入データ作成時に数量０を除外する
// Ver1.02 汎用データレイアウト 仕入明細データ Version 9(DX-Rev3.00)に対応(2022/05/25 勝呂)
// Ver1.03 経理部の要請により、Microsoft365仕入データを部門毎の集計を止めて、得意先に関する記事データを追加(2023/02/10 勝呂)
// Ver1.04(2023/03/30 勝呂):Microsoft365仕入データの単価が仕入価格でなく、標準価格となっている障害
// Ver1.05(2023/07/10 勝呂):Curline本体アプリ仕入データファイル出力廃止対応
// Ver1.06(2023/11/21 勝呂):002199 ｱﾙﾒｯｸｽ FIT-A 保守(現金2台仕様)1ヶ月の商品追加対応
// Ver1.07(2023/12/01 勝呂):オン資格保守サービスの仕入データ作成に対応
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
		public const string gVersionStr = "Ver1.07(2023/12/01)";

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
