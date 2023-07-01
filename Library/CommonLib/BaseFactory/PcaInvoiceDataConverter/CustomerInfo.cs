//
// CustomerInfo.cs
// 
// 顧客情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/27 勝呂):新規作成
//
using CommonLib.Common;
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.PcaInvoiceDataConverter
{
	/// <summary>
	/// 顧客情報
	/// </summary>
	public class CustomerInfo
	{
		public string 得意先No { get; set; }
		public int 顧客No { get; set; }
		public string 顧客名1 { get; set; }
		public string 顧客名2 { get; set; }
		public string APLUSコード { get; set; }
		public string 銀行コード { get; set; }
		public string 銀行名カナ { get; set; }
		public string 支店コード { get; set; }
		public string 支店名カナ { get; set; }
		public string 預金種別 { get; set; }
		public string 口座番号 { get; set; }
		public string 預金者名 { get; set; }
		public string レセコン区分 { get; set; }

		/// <summary>
		/// APLUS送信ヘッダ文字列（120文字）
		///  "19112062710000ｶ)ﾐﾂｸ                                   yymm0005ﾐﾂﾋﾞｼﾕｰｴﾌｼﾞｴｲ  050ｼﾝｼﾞﾕｸﾄﾞｵﾘ     21846259                 "
		/// ※yymmの箇所に振替日の年月を設定
		/// </summary>
		public const string AplusHeaderStr = "19112062710000ｶ)ﾐﾂｸ                                   {0:D2}{1:D2}0005ﾐﾂﾋﾞｼﾕｰｴﾌｼﾞｴｲ  050ｼﾝｼﾞﾕｸﾄﾞｵﾘ     21846259                 ";

		/// <summary>
		/// APLUS送信データフォーマット
		/// </summary>
		public const string AplusSendDataFormat = "2{0:D4}{1,-15}{2:D3}{3,-15}{4,-4}{5:-1}{6:D9}{7,-30}{8:D10}1{9:D20}0{4,-8}";

		// 送信データシートにトレーラレコード（合計データ）を記録
		public const string AplusTotalRecordFormat = "'8{0:D6}{1:D12}{2:D6}{2:D12}{2:D6}{2:D12}{3,-65}";

		// 送信データシートにエンドレコード（終端データ）を記録
		public const string AplusEndRecordFormat = "'9{0,-119}";

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CustomerInfo()
		{
			得意先No = string.Empty;
			顧客No = 0;
			顧客名1 = string.Empty;
			顧客名2 = string.Empty;
			APLUSコード = string.Empty;
			銀行コード = string.Empty;
			銀行名カナ = string.Empty;
			支店コード = string.Empty;
			支店名カナ = string.Empty;
			預金種別 = string.Empty;
			口座番号 = string.Empty;
			預金者名 = string.Empty;
			レセコン区分 = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns>MWSコードマスタリスト</returns>
		public static List<CustomerInfo> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<CustomerInfo> result = new List<CustomerInfo>();
				foreach (DataRow row in table.Rows)
				{
					CustomerInfo data = new CustomerInfo
					{
						得意先No = row["得意先No"].ToString().Trim(),
						顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]),
						顧客名1 = row["顧客名1"].ToString().Trim(),
						顧客名2 = row["顧客名2"].ToString().Trim(),
						APLUSコード = row["APLUSコード"].ToString().Trim(),
						銀行コード = row["銀行コード"].ToString().Trim(),
						銀行名カナ = row["銀行名カナ"].ToString().Trim(),
						支店コード = row["支店コード"].ToString().Trim(),
						支店名カナ = row["支店名カナ"].ToString().Trim(),
						預金種別 = row["預金種別"].ToString().Trim(),
						口座番号 = row["口座番号"].ToString().Trim(),
						預金者名 = row["預金者名"].ToString().Trim(),
						レセコン区分 = row["レセコン区分"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// タイトル行の取得
		/// </summary>
		/// <returns></returns>
		public static string GetTitle()
		{
			List<string> result = new List<string>
			{
				";得意先No",
				"顧客No",
				"顧客名1",
				"顧客名2",
				"APLUSコード",
				"銀行コード",
				"銀行名カナ",
				"支店コード",
				"支店名カナ",
				"預金種別",
				"口座番号",
				"預金者名",
				"レセコン区分"
			};
			return string.Join(",", result.ToArray());
		}

		/// <summary>
		/// 顧客情報の取得
		/// </summary>
		/// <returns></returns>
		public string GetData()
		{
			List<string> result = new List<string>
			{
				得意先No,
				顧客No.ToString(),
				顧客名1,
				顧客名2,
				APLUSコード,
				銀行コード,
				銀行名カナ,
				支店コード,
				支店名カナ,
				預金種別,
				口座番号,
				預金者名,
				レセコン区分
			};
			return string.Join(",", result.ToArray());
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
		public string AplusSendDataRecord(int invoice)
		{
			// データ区分:'2'
			// 振替銀行番号:9(4)
			// 振替銀行名:x(15)
			// 振替銀行支店番号:9(3)
			// 振替銀行支店名:x(15)
			// ダミー:x(4)
			// 預金種別:x(1)
			// 口座番号:9(7)
			// 預金者名:x(30)
			// 振替金額:9(10)
			// 新規コード:'1'
			// 顧客番号:9(20)
			// 振替結果コード:'0'
			// ダミー:x(8)
			return string.Format(AplusSendDataFormat, 銀行コード, 銀行名カナ, 支店コード, 支店名カナ, " ", 預金種別, 口座番号, StringUtil.ByteLeft(預金者名, 30), invoice, APLUSコード);
		}

		/// <summary>
		/// トレーラレコード（合計データ）文字列の取得
		/// </summary>
		/// <param name="invoiceCount">請求一覧件数</param>
		/// <param name="invoiceTotal">請求一覧請求金額</param>
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

		/// <summary>
		/// 紙請求書かどうか？
		/// </summary>
		/// <returns></returns>
		public bool IsAGREX口振通知書()
		{
			return (レセコン区分 != "2") ? true : false;
		}
	}
}
