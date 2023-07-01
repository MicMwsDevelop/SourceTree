//
// Program.cs
// 
// オン資補助金申請書類出力 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// Ver1.00(2022/09/20 勝呂):新規作成
// Ver1.01(2022/11/10 勝呂):経理部動作確認後、要望対応
// Ver1.02(2022/11/14 勝呂):動作環境の違いにより事業完了報告書の印刷範囲の不具合の対処のため、経理部にてエクセルファイルのフォーマットを作成
// Ver1.03(2022/11/16 勝呂):経理部にてエクセルファイルのフォーマットを作成(再調整)
// Ver1.04(2022/12/13 勝呂):経理部要望対応 顧客情報（出力用）のチェックに顧客名の追加と開設者が未設定時の時には院長名を使用する
// Ver1.05(2022/12/28 勝呂):経理部要望対応 注文確認書追加対応
// Ver1.06(2023/02/07 勝呂):経理部要望対応 NTT以外の案件も作業リストから補助金申請書類を出力できるようにする
// Ver1.07(2023/02/17 勝呂):経理部要望対応 受注日を伝票でなく、[SalesDB].[dbo].[オンライン資格確認進捗管理情報]の契約日から取得
// Ver1.08(2023/04/07 勝呂):経理部要望対応 注文確認書の金額欄に\マークを付加
// Ver1.08(2023/04/07 勝呂):障害対応  vMicオンライン資格確認ソフト改修費で赤伝が抽出されている(vMicオンライン資格確認ソフト改修費の修正)
// Ver1.08(2023/04/07 勝呂):経理部要望対応  受注日は送付先リストから取得する
// Ver1.09(2023/04/13 勝呂):送付先リストのシート名の修正
// Ver1.10(2023/04/17 勝呂):送付先リストの受注日のフィールドを日付型でなく、日付シリアル型で読み込んでいた為、注文確認書に出力できていない医院があった
// Ver1.11(2023/06/16 勝呂):事業完了報告書、領収内訳書、送付先リストの読込時のエラーメッセージにファイル名を表示
/////////////////////////////////////////////////////////
//
using OnlineLicenseSubsidy.Settings;
using System;
using System.Windows.Forms;

namespace OnlineLicenseSubsidy
{
	static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string ProgramName = "オン資補助金申請書類出力";

		/// <summary>
		/// バージョン番号
		/// </summary>
		public const string VersionStr = "Ver1.11 (2023/06/16)";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static OnlineLicenseSubsidySettings gSettings { get; set; }

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			gSettings = OnlineLicenseSubsidySettingsIF.GetSettings();

			Application.Run(new Forms.MainForm());
		}
	}
}
