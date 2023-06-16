//
// Program.cs
// 
// オン資補助金申請書類顧客情報抽出 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/04/05 勝呂):新規作成
// Ver1.01(2023/04/13 勝呂):開設者が未設定の場合には院長名を出力する
//
using ClosedXML.Excel;
using OnlineLicenseSubsidyCustomerList.Settings;
using System;
using System.Windows.Forms;

namespace OnlineLicenseSubsidyCustomerList
{
	internal static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string ProgramName = "オン資補助金申請書類顧客情報抽出";

		/// <summary>
		/// プログラムバージョン
		/// </summary>
		public const string ProgramVersion = "Ver1.01 2023/04/13";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static OnlineLicenseSubsidyCustomerListSettings gSettings { get; set; }

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			gSettings = OnlineLicenseSubsidyCustomerListSettingsIF.GetSettings();

			Application.Run(new Forms.MainForm());
		}

		/// <summary>
		/// 金額の取得
		/// </summary>
		/// <param name="cell"></param>
		/// <returns>金額</returns>
		public static int GetMoney(IXLCell cell)
		{
			if (XLDataType.Number == cell.DataType)
			{
				double price = cell.GetDouble();
				return (int)price;
			}
			return 0;
		}

		/// <summary>
		/// 日付の取得
		/// </summary>
		/// <param name="cell"></param>
		/// <returns>日付文字列</returns>
		public static DateTime? GetDateTime(IXLCell cell)
		{
			if (XLDataType.DateTime == cell.DataType)
			{
				return cell.GetDateTime();
			}
			//// 和暦で格納しているので、日付型でなくシリアル値で格納されてしまう
			//if (XLDataType.Number == cell.DataType)
			//{
			//	return DateTime.FromOADate(cell.GetDouble());
			//}
			return null;
		}
	}
}
