//
// AgrexDefine.cs
// 
// AGREX定義クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/27 勝呂):新規作成
//
using CommonLib.BaseFactory.PcaInvoiceDataConverter;
using CommonLib.Common;
using System;

namespace PcaInvoiceDataConverter.BaseFactory
{
	public class AgrexDefine
	{
		/// <summary>
		/// AGREX口座振替通知書コード
		/// </summary>
		public const string AccountTransferCode = "132001";

		/// <summary>
		/// AGREX銀行振込請求書コード
		/// </summary>
		public const string BankTransferCode = "296001";

		/// <summary>
		/// 契約先コード：132002
		/// </summary>
		public const string ContactCode = "132002";

		/// <summary>
		/// 委託先コード：ミックは 206271 固定
		/// </summary>
		public const string ConsignmentCode = "206271";

		/// <summary>
		/// 委託者区分
		/// </summary>
		public const string ConsignmentKubun = "00";

		/// <summary>
		/// 顧客指定口座番号（三菱UFJ）
		/// </summary>
		public const string AccountNumber = "0501846259";

		/// <summary>
		/// APLUS送信ヘッダ文字列（120文字）
		///  "19112062710000ｶ)ﾐﾂｸ                                   yymm0005ﾐﾂﾋﾞｼﾕｰｴﾌｼﾞｴｲ  050ｼﾝｼﾞﾕｸﾄﾞｵﾘ     21846259                 "
		/// ※yymmの箇所に振替日の年月を設定
		/// </summary>
		public const string AplusHeaderStr = "19112062710000ｶ)ﾐﾂｸ                                   {0:D2}{1:D2}0005ﾐﾂﾋﾞｼﾕｰｴﾌｼﾞｴｲ  050ｼﾝｼﾞﾕｸﾄﾞｵﾘ     21846259                 ";

		/// <summary>
		/// APLUS送信データフォーマット
		/// </summary>
		public const string AplusSendDataFormat = "2{0}{1,-15}{2}{3,-15}{4,-4}{5:-1}{6}{7,-30}{8:D10}1{9:D20}0{4,-8}";

		// 送信データシートにトレーラレコード（合計データ）を記録
		public const string AplusTotalRecordFormat = "8{0:D6}{1:D12}{2:D6}{2:D12}{2:D6}{2:D12}{3,-65}";

		// 送信データシートにエンドレコード（終端データ）を記録
		public const string AplusEndRecordFormat = "9{0,-119}";

		/// <summary>
		/// APLUS送信ファイル名の取得（本日日付）
		/// </summary>
		public static string GetAplusSendDataFilename
		{
			get
			{
				return string.Format("Sosin{0:D2}{1:D2}{2:D2}.txt", DateTime.Today.Year - 2000, DateTime.Today.Month, DateTime.Today.Day);
			}
		}

		/// <summary>
		/// AGREX口座振替通知書ファイル名の取得（本日日付）
		/// </summary>
		public static string GetAccountTransferFilename
		{
			get
			{
				return string.Format("{0}{1:D2}{2:D2}{3:D2}F.csv", AccountTransferCode, DateTime.Today.Year - 2000, DateTime.Today.Month, DateTime.Today.Day);
			}
		}

		/// <summary>
		/// AGREX銀行振込請求書ファイル名の取得（本日日付）
		/// </summary>
		public static string GetBankTransferFilename
		{
			get
			{
				return string.Format("{0}{1:D2}{2:D2}{3:D2}.csv", BankTransferCode, DateTime.Today.Year - 2000, DateTime.Today.Month, DateTime.Today.Day);
			}
		}

		/// <summary>
		/// APLUS送信ヘッダ文字列の取得
		/// </summary>
		/// <param name="transferDate">振替日</param>
		/// <returns>APLUS送信ヘッダ文字列</returns>
		public static string AplusHeaderRecord(DateTime transferDate)
		{
			return string.Format(AplusHeaderStr, transferDate.Month, transferDate.Day);
		}

		/// <summary>
		/// APLUS送信データ文字列の取得
		/// </summary>
		/// <param name="invoice">振替金額</param>
		/// <returns>APLUS送信データ文字列</returns>
		public static string AplusSendDataRecord(int invoice, CustomerInfo cust)
		{
			// データ区分:'2'
			// 振替銀行番号:x(4)
			// 振替銀行名:x(15)
			// 振替銀行支店番号:x(3)
			// 振替銀行支店名:x(15)
			// ダミー:x(4)
			// 預金種別:x(1)
			// 口座番号:x(7)
			// 預金者名:x(30)
			// 振替金額:9(10)
			// 新規コード:'1'
			// 顧客番号:9(20)
			// 振替結果コード:'0'
			// ダミー:x(8)
			string depositor = StringUtil.ConvertNarrowForUnicode(cust.預金者名);
			return string.Format(AplusSendDataFormat, cust.銀行コード.PadLeft(4, '0'), cust.銀行名カナ, cust.支店コード.PadLeft(3, '0'), cust.支店名カナ, " ", cust.預金種別, cust.口座番号.PadLeft(7, '0'), StringUtil.ByteLeft(depositor, 30), invoice, cust.APLUSコード);
		}

		/// <summary>
		/// トレーラレコード（合計データ）文字列の取得
		/// </summary>
		/// <param name="invoiceCount">口座振替請求件数</param>
		/// <param name="invoiceTotal">口座振替請求金額</param>
		/// <returns>文字列</returns>
		public static string AplusTotalRecord(int invoiceCount, int invoiceTotal)
		{
			return string.Format(AplusTotalRecordFormat, invoiceCount, invoiceTotal, 0, " ");
		}

		/// <summary>
		/// エンドレコード（終端データ）文字列の取得
		/// </summary>
		/// <returns>文字列</returns>
		public static string AplusEndRecord()
		{
			return string.Format(AplusEndRecordFormat, " ");
		}
	}
}
