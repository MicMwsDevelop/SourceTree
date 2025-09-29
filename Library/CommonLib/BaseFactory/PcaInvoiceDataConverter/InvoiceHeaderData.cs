//
// InvoiceHeaderData.cs
// 
// 請求一覧表クラス
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
	/// 請求一覧表
	/// PCA商魂・商管 販売管理→請求→請求一覧表
	/// </summary>
	public class InvoiceHeaderData
	{
		public int 請求締日 { get; set; }
		public DateTime? 請求期間開始 { get; set; }
		public DateTime? 請求期間終了 { get; set; }
		public int データ区分 { get; set; }
		public string 得意先コード { get; set; }
		public string 得意先名1 { get; set; }
		public string 得意先名2 { get; set; }
		public int 前回請求額 { get; set; }
		public int 入金額 { get; set; }
		public int 繰越金額 { get; set; }
		public int 税込売上高 { get; set; }
		public int 請求残高 { get; set; }
		public DateTime? 回収予定日 { get; set; }

		/// <summary>
		/// 顧客情報
		/// </summary>
		public CustomerInfo Customer { get; set; }

		/// <summary>
		/// 請求明細データリスト
		/// </summary>
		public List<InvoiceDetailData> InvoiceDetailDataList { get; set; }

		/// <summary>
		/// オリジナルデータ
		/// </summary>
		public string OrgData { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public InvoiceHeaderData()
		{
			請求締日 = 0;
			請求期間開始 = null;
			請求期間終了 = null;
			データ区分 = 0;
			得意先コード = string.Empty;
			得意先名1 = string.Empty;
			得意先名2 = string.Empty;
			前回請求額 = 0;
			入金額 = 0;
			繰越金額 = 0;
			税込売上高 = 0;
			請求残高 = 0;
			回収予定日 = null;
			OrgData = string.Empty;
			Customer = null;
			InvoiceDetailDataList = null;
		}

		/// <summary>
		/// データの格納
		/// </summary>
		/// <param name="values"></param>
		public void SetData(string line, string[] values)
		{
			請求締日 = values[0].ToInt();
			if (0 < values[1].Length)
			{
				請求期間開始 = DateConversion.YMDToDate(values[1].ToInt()).ToDateTime();
			}
			if (0 < values[2].Length)
			{
				請求期間終了 = DateConversion.YMDToDate(values[2].ToInt()).ToDateTime();
			}
			データ区分 = values[3].ToInt();
			得意先コード = values[4];
			得意先名1 = values[5];
			得意先名2 = values[6];
			前回請求額 = values[7].ToInt();
			入金額 = values[8].ToInt();
			繰越金額 = values[9].ToInt();
			税込売上高 = values[10].ToInt();
			請求残高 = values[11].ToInt();
			if (0 < values[12].Length)
			{
				回収予定日 = DateConversion.YMDToDate(values[12].ToInt()).ToDateTime();
			}
			OrgData = line;
		}

		/// <summary>
		/// 前回請求締日付の取得
		/// </summary>
		/// <returns></returns>
		public DateTime? 前回請求締日付()
		{
			if (請求期間開始.HasValue)
			{
				// 請求期間開始の前日
				return 請求期間開始.Value.AddDays(-1);
			}
			return DateTime.Today;
		}

		/// <summary>
		/// 「口振請求なし」タイトル行の取得
		/// </summary>
		/// <returns></returns>
		public static string[] GetInvoiceNothingTitle()
		{
			string[] title = { "得意先コード", "得意先名１", "得意先名２", "前回請求額", "入金額", "繰越金額", "税込売上高", "請求残高", "回収予定日" };
			return title;
		}

		/// <summary>
		/// 「口振請求なし」、 「銀行振込０円請求」のデータ行の取得
		/// </summary>
		/// <returns></returns>
		public string[] GetInvoiceNothingRecord()
		{
			string[] result =
			{
				得意先コード,
				得意先名1,
				得意先名2,
				前回請求額.ToString(),
				入金額.ToString(),
				繰越金額.ToString(),
				税込売上高.ToString(),
				請求残高.ToString(),
				(回収予定日.HasValue) ? 回収予定日.Value.ToString("yyyyMMdd") : ""
			};
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool Is銀行振込請求書送付()
		{
			return (0 < 請求残高) ? true : false;
		}

		///// <summary>
		///// 「請求一覧10.txt」の内容をDataTableに変換
		///// </summary>
		///// <param name="list">請求一覧リスト</param>
		///// <returns>DataTable</returns>
		//public static DataTable ConvertDataTable(List<InvoiceHeaderData> list)
		//{
		//	DataTable table = new DataTable();
		//	table.Columns.Add("請求締日", typeof(string));
		//	table.Columns.Add("請求期間開始", typeof(string));
		//	table.Columns.Add("請求期間終了", typeof(string));
		//	table.Columns.Add("データ区分", typeof(string));
		//	table.Columns.Add("得意先コード", typeof(string));
		//	table.Columns.Add("得意先名1", typeof(string));
		//	table.Columns.Add("得意先名2", typeof(string));
		//	table.Columns.Add("前回請求額", typeof(string));
		//	table.Columns.Add("入金額", typeof(string));
		//	table.Columns.Add("繰越金額", typeof(string));
		//	table.Columns.Add("税込売上高", typeof(string));
		//	table.Columns.Add("請求残高", typeof(string));
		//	table.Columns.Add("回収予定日", typeof(string));
		//	foreach (InvoiceHeaderData header in list)
		//	{
		//		DataRow row = table.NewRow();
		//		row["請求締日"] = header.請求締日.ToString();
		//		row["請求期間開始"] = (header.請求期間開始.HasValue) ? header.請求期間開始.Value.ToString("yyyyMMdd") : "";
		//		row["請求期間終了"] = (header.請求期間終了.HasValue) ? header.請求期間終了.Value.ToString("yyyyMMdd") : "";
		//		row["データ区分"] = header.データ区分.ToString();
		//		row["得意先コード"] = header.得意先コード;
		//		row["得意先名1"] = header.得意先名1;
		//		row["得意先名2"] = header.得意先名2;
		//		row["前回請求額"] = header.前回請求額.ToString();
		//		row["入金額"] = header.入金額.ToString();
		//		row["繰越金額"] = header.繰越金額.ToString();
		//		row["税込売上高"] = header.税込売上高.ToString();
		//		row["請求残高"] = header.請求残高.ToString();
		//		row["回収予定日"] = (header.回収予定日.HasValue) ? header.回収予定日.Value.ToString("yyyyMMdd") : "";
		//		table.Rows.Add(row);
		//	}
		//	return table;
		//}

		/// <summary>
		/// DataRow → オブジェクト
		/// </summary>
		/// <param name="row"></param>
		/// <returns>請求一覧データ</returns>
		public static InvoiceHeaderData DataTableToObject(DataRow row)
		{
			InvoiceHeaderData data = new InvoiceHeaderData();
			data.請求締日 = row["請求締日"].ToString().Trim().ToInt();
			data.請求期間開始 = ConvObjectDateTimeNull(row["請求期間開始"].ToString().Trim());
			data.請求期間終了 = ConvObjectDateTimeNull(row["請求期間終了"].ToString().Trim());
			data.データ区分 = DataBaseValue.ConvObjectToInt(row["データ区分"]);
			data.得意先コード = row["得意先コード"].ToString().Trim();
			data.得意先名1 = row["得意先名1"].ToString().Trim();
			data.得意先名2 = row["得意先名2"].ToString().Trim();
			data.前回請求額 = row["前回請求額"].ToString().Trim().ToInt();
			data.入金額 = row["入金額"].ToString().Trim().ToInt();
			data.繰越金額 = row["繰越金額"].ToString().Trim().ToInt();
			data.税込売上高 = row["税込売上高"].ToString().Trim().ToInt();
			data.請求残高 = row["請求残高"].ToString().Trim().ToInt();
			data.回収予定日 = ConvObjectDateTimeNull(row["回収予定日"].ToString().Trim());
			return data;
		}

		/// <summary>
		/// yyyyMMdd文字列からDateTime?に変換
		/// </summary>
		/// <param name="str">yyyyMMdd文字列</param>
		/// <returns>DateTime?</returns>
		public static DateTime? ConvObjectDateTimeNull(string str)
		{
			if (null != str && 0 < str.Length)
			{
				return Convert.ToInt32(str).YMDToDate().ToDateTime();
			}
			return null;
		}
	}
}
