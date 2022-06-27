//
// Program.cs
// 
// 各種書類出力 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// アプリ管理サイト：APPID623 各種書類出力アプリ
// 処理概要：各種書類を出力する社員用のツール
// 入力ファイル：無
// 出力ファイル：各種書類のExcelファイル
// 印刷物：無
// メール送信：無
/////////////////////////////////////////////////////////
// Ver1.00(2021/04/22):新規作成
// Ver1.02(2021/09/01):8-Microsoft365利用申込書のFAX番号を本社から消耗品受注センターに変更
// Ver1.02(2021/09/01):16-アプラス預金口座振替依頼書・自動払込利用申込書の記入例を元に戻す
// Ver1.03(2021/09/28):5 オンライン請求届出 電子証明書発行等依頼内訳に対応
// Ver1.03(2021/09/30):XXXX支部がXXXX支部名と設定されていた
// Ver1.04(2021/10/18):5-オンライン請求届出エクセル出力時に例外エラー(0x800a03ec)が発生する
// Ver1.05(2021/11/12):18-消耗品FAXオーダーシートの新規追加
// Ver1.06(2021/12/13):経部確認後、18-消耗品FAXオーダーシートの修正
// Ver1.07(2021/12/24):19-経理部専用 オンライン資格確認等事業完了報告書の対応
// Ver1.08(2022/01/14):5 オンライン請求届出 電子情報処理組織の使用による費用の請求に関する届出 新用紙対応
// Ver1.09(2022/02/10):14-二次キッティング依頼書 2022/02組織変更対応
// Ver1.10(2022/02/17):18-消耗品FAXオーダーシート印刷枚数不具合対応
// Ver1.11(2022/02/21):14-二次キッティング依頼書 使用廃止によりメニューから削除
// Ver1.12(2022/02/22):19-経理部専用 オンライン資格確認等事業完了報告書 修正依頼対応
// Ver1.13(2022/06/14):8-Microsoft365利用申込書新フォーム対応
// Ver1.13(2022/06/14):16-アプラス預金口座振替依頼書・自動払込利用申込書新フォーム対応
// Ver1.13(2022/06/14):19-経理部専用 オンライン資格確認等事業完了報告書 書類送付状新様式対応
// Ver1.13(2022/06/14):1-ユーザーID＆初期パスワード通知書新フォーム対応
// Ver1.14(2022/06/16):8-Microsoft365利用申込書 拠点FAX番号対応
// Ver1.14(2022/06/27):8-Microsoft365利用申込書 新様式対応
//
using System;
using System.Windows.Forms;
using VariousDocumentOut.Settings;

namespace VariousDocumentOut
{
	static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string ProgramName = "各種書類出力";

		/// <summary>
		/// バージョン番号
		/// </summary>
		public const string VersionStr = "Ver1.14 (2022/06/27)";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static VariousDocumentOutSettings gSettings { get; set; }

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			gSettings = VariousDocumentOutSettingsIF.GetSettings();

			Application.Run(new Forms.MainForm());
		}
	}
}
