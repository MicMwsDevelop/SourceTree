//
// PrintEstimateDef.cs
//
// MIC WEB SERVICE見積書・注文書/注文請書印刷定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
// 
using System.IO;
using System.Text.RegularExpressions;

namespace MwsSimulation.Print
{
	/// <summary>
	/// MIC WEB SERVICE見積書 定義クラス
	/// </summary>
	public static class PrintEstimateDef
	{
		/// <summary>
		/// MIC WEB SERVICE見積書 セクション名
		/// </summary>
		public const string ESTIMATE_SECTION_NAME = "MWS見積書";

		/// <summary>
		/// MIC WEB SERVICE注文書 セクション名
		/// </summary>
		public const string PURCHASE_ORDER_SECTION_NAME = "MWS注文書";

		/// <summary>
		/// MIC WEB SERVICE注文請書 セクション名
		/// </summary>
		public const string OERDER_COMFIRM_SECTION_NAME = "MWS注文請書";

		/// <summary>
		/// パラメタファイル名
		/// </summary>
		public const string PARAMETER_FILENAME = "MWS_ORDER_01.PRM";

		/// <summary>
		/// 見積書サービス印刷可能行数
		/// </summary>
		public const int PRINT_SERVICE_COUNT = 68;

		/// <summary>
		/// 備考最大行数
		/// </summary>
		public const int REMARK_MAXLINE = 4;

		/// <summary>
		/// 備考１行最大文字列
		/// </summary>
		public const int REMARK_LINE_MAXLEN = (25 * 2);

		/// <summary>
		/// 用紙種別
		/// </summary>
		public enum MwsPaperType
		{
			/// <summary>見積書</summary>
			Estimate = 0,
			/// <summary>注文書</summary>
			PurchaseOrder,
			/// <summary>注文請書</summary>
			OrderConfirm,
		};

		/// <summary>
		/// パラメタファイル格納フォルダの取得
		/// </summary>
		/// <returns>パラメタファイル格納フォルダ</returns>
		public static string GetParameterFilePath()
		{
			return Directory.GetCurrentDirectory();
		}

		/// <summary>
		/// 文字列から数字のみ抽出
		/// </summary>
		/// <param name="str">文字列</param>
		/// <returns>数字</returns>
		public static int ExtractionNumeral(string str)
		{
			string strDecimal = Regex.Replace(str, @"[^0-9]", "");
			return int.Parse(strDecimal);
		}

		/// <summary>
		/// 印刷ドキュメント名の取得
		/// </summary>
		/// <param name="type">用紙種別</param>
		/// <param name="clinicName">医院名</param>
		/// <returns>印刷ドキュメント名</returns>
		public static string GetDocumentName(MwsPaperType type, string clinicName)
		{
			string paperName = string.Empty;
			switch (type)
			{
				case MwsPaperType.Estimate:
					paperName = "見積書";
					break;
				case MwsPaperType.PurchaseOrder:
					paperName = "注文書";
					break;
				case MwsPaperType.OrderConfirm:
					paperName = "注文請書";
					break;
			}
			return string.Format("{0}-{1}", paperName, clinicName);
		}
	}
}
