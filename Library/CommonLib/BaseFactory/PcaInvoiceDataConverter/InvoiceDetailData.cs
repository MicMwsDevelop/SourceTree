﻿//
// InvoiceDetailData.cs
// 
// 請求明細データクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/27 勝呂):新規作成
//
using CommonLib.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CommonLib.BaseFactory.PcaInvoiceDataConverter
{
	/// <summary>
	/// 請求明細データ
	/// PCA商魂・商管 随時→拡張汎用データの作成→請求明細データ
	/// </summary>
	public class InvoiceDetailData
	{
		public string 請求実績請求先コード { get; set; }
		public string 請求先名1 { get; set; }
		public string 請求先名2 { get; set; }
		public string 請求先郵便番号 { get; set; }
		public string 請求先住所1 { get; set; }
		public string 請求先住所2 { get; set; }
		public string 請求先会社TEL { get; set; }
		public DateTime? 請求期間開始日 { get; set; }
		public DateTime? 請求期間終了日 { get; set; }
		public int 前回請求額 { get; set; }
		public int 期間入金額 { get; set; }
		public int 期間調整額 { get; set; }
		public int 繰越金額 { get; set; }
		public int 期間売上額 { get; set; }
		public int 期間外税額 { get; set; }
		public int 期間内税額 { get; set; }
		public int 伝票No { get; set; }
		public DateTime? 売上日 { get; set; }
		public DateTime? 請求日 { get; set; }
		public string 得意先コード { get; set; }
		public string 摘要名 { get; set; }
		public int 売上金額合計 { get; set; }
		public int 外税合計 { get; set; }
		public int 内税合計 { get; set; }
		public string 商品コード { get; set; }
		public int マスター区分 { get; set; }
		public string 商品名 { get; set; }
		public int 数量 { get; set; }
		public string 単位 { get; set; }
		public int 単価 { get; set; }
		public int 売上金額 { get; set; }

		/// <summary>
		/// 送料かどうか？
		/// </summary>
		public bool IsShipping
		{
			get
			{
				return ("000600" == 商品コード) ? true : false;
			}
		}
    
		/// <summary>
		/// 着日指定かどうか？
		/// </summary>
		public bool IsArraivalDate
		{
			get
			{
				return ("000020" == 商品コード) ? true : false;
			}
		}

		/// <summary>
		/// 記事行かどうか？
		/// </summary>
		public bool IsComment
		{
			get
			{
				return ("000014" == 商品コード) ? true : false;
			}
		}

		/// <summary>
		/// 売上合計の取得
		/// </summary>
		/// <returns>売上合計</returns>
		public int 売上合計
		{
			get { return 売上金額合計 + 外税合計; }
		}

		/// <summary>
		/// 請求先郵便番号数字のみの取得
		/// </summary>
		public string 請求先郵便番号数字のみ
		{
			get
			{
				return 請求先郵便番号.Replace("-", "");
			}
		}

		/// <summary>
		/// 請求先住所
		/// </summary>
		public string 請求先住所
		{
			get
			{
				// 住所文字列を１７＋１７＋１６文字形式に変換？
				string address = 請求先住所1;
				if (0 < 請求先住所2.Length)
				{
					address += "　" + 請求先住所2;
				}
				// 半角→全角
				return StringUtil.ConvertWideForUnicode(address);
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public InvoiceDetailData()
		{
			請求実績請求先コード = string.Empty;
			請求先名1 = string.Empty;
			請求先名2 = string.Empty;
			請求先郵便番号 = string.Empty;
			請求先住所1 = string.Empty;
			請求先住所2 = string.Empty;
			請求先会社TEL = string.Empty;
			請求期間開始日 = null;
			請求期間終了日 = null;
			前回請求額 = 0;
			期間入金額 = 0;
			期間調整額 = 0;
			繰越金額 = 0;
			期間売上額 = 0;
			期間外税額 = 0;
			期間内税額 = 0;
			伝票No = 0;
			売上日 = null;
			請求日 = null;
			得意先コード = string.Empty;
			摘要名 = string.Empty;
			売上金額合計 = 0;
			外税合計 = 0;
			内税合計 = 0;
			商品コード = string.Empty;
			マスター区分 = 0;
			商品名 = string.Empty;
			数量 = 0;
			単位 = string.Empty;
			単価 = 0;
			売上金額 = 0;
		}

		/// <summary>
		/// データの格納
		/// </summary>
		/// <param name="values"></param>
		public void SetData(string[] values)
		{
			請求実績請求先コード = values[0];
			請求先名1 = values[1];
			請求先名2 = values[2];
			請求先郵便番号 = values[3];
			請求先住所1 = values[4];
			請求先住所2 = values[5];
			請求先会社TEL = values[6];
			請求期間開始日 = Date.Parse(values[7].ToInt()).ToDateTime();
			請求期間終了日 = Date.Parse(values[8].ToInt()).ToDateTime();
			前回請求額 = values[9].ToInt();
			期間入金額 = values[10].ToInt();
			期間調整額 = values[11].ToInt();
			繰越金額 = values[12].ToInt();
			期間売上額 = values[13].ToInt();
			期間外税額 = values[14].ToInt(); ;
			期間内税額 = values[15].ToInt();
			伝票No = values[16].ToInt();
			売上日 = Date.Parse(values[17].ToInt()).ToDateTime();
			請求日 = Date.Parse(values[18].ToInt()).ToDateTime();
			得意先コード = values[19];
			摘要名 = values[20];
			売上金額合計 = values[21].ToInt();
			外税合計 = values[22].ToInt();
			内税合計 = values[23].ToInt();
			商品コード = values[24];
			マスター区分 = values[25].ToInt();
			商品名 = values[26];
			数量 = values[27].ToInt();
			単位 = values[28];
			単価 = values[29].ToInt();
			売上金額 = values[30].ToInt();
		}

		/// <summary>
		/// 摘要名文字列の取得
		/// </summary>
		/// <returns></returns>
		public string 摘要名文字列()
		{
			// Ver.1.61 2021/04/13 医院名 L’est Kid's dental clinic でWEB請求書取込みエラー by 勝呂
			if (0 < 摘要名.Length)
			{
				string work = "■ " + 摘要名.Replace("'", "");
				return work.Replace("’", "");
			}
			return string.Empty;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public static DataTable SetColumns()
		{
			DataTable table = new DataTable();
			table.Columns.Add("請求実績請求先コード", typeof(string));
			table.Columns.Add("請求先名1", typeof(string));
			table.Columns.Add("請求先名2", typeof(string));
			table.Columns.Add("請求先郵便番号", typeof(string));
			table.Columns.Add("請求先住所1", typeof(string));
			table.Columns.Add("請求先住所2", typeof(string));
			table.Columns.Add("請求先会社TEL", typeof(string));
			table.Columns.Add("請求期間開始日", typeof(string));
			table.Columns.Add("請求期間終了日", typeof(string));
			table.Columns.Add("前回請求額", typeof(int));
			table.Columns.Add("期間入金額", typeof(int));
			table.Columns.Add("期間調整額", typeof(int));
			table.Columns.Add("繰越金額", typeof(int));
			table.Columns.Add("期間売上額", typeof(int));
			table.Columns.Add("期間外税額", typeof(int));
			table.Columns.Add("期間内税額", typeof(int));
			table.Columns.Add("伝票No", typeof(int));
			table.Columns.Add("売上日", typeof(string));
			table.Columns.Add("請求日", typeof(string));
			table.Columns.Add("得意先コード", typeof(string));
			table.Columns.Add("摘要名", typeof(string));
			table.Columns.Add("売上金額合計", typeof(int));
			table.Columns.Add("外税合計", typeof(int));
			table.Columns.Add("内税合計", typeof(int));
			table.Columns.Add("商品コード", typeof(string));
			table.Columns.Add("マスター区分", typeof(int));
			table.Columns.Add("商品名", typeof(string));
			table.Columns.Add("数量", typeof(int));
			table.Columns.Add("単位", typeof(string));
			table.Columns.Add("単価", typeof(int));
			table.Columns.Add("売上金額", typeof(int));
			return table;

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="table">DataTable</param>
		/// <returns></returns>
		public DataRow GetDataRow(DataRow row)
		{
			row["請求実績請求先コード"] = 請求実績請求先コード;
			row["請求先名1"] = 請求先名1;
			row["請求先名2"] = 請求先名2;
			row["請求先郵便番号"] = 請求先郵便番号;
			row["請求先住所1"] = 請求先住所1;
			row["請求先住所2"] = 請求先住所2;
			row["請求先会社TEL"] = 請求先会社TEL;
			row["請求期間開始日"] = (請求期間開始日.HasValue) ? 請求期間開始日.Value.ToString("yyyyMMdd") : ""; ;
			row["請求期間終了日"] = (請求期間終了日.HasValue) ? 請求期間終了日.Value.ToString("yyyyMMdd") : ""; ;
			row["前回請求額"] = 前回請求額;
			row["期間入金額"] = 期間入金額;
			row["期間調整額"] = 期間調整額;
			row["繰越金額"] = 繰越金額;
			row["期間売上額"] = 期間売上額;
			row["期間外税額"] = 期間外税額;
			row["期間内税額"] = 期間内税額;
			row["伝票No"] = 伝票No;
			row["売上日"] = (売上日.HasValue) ? 売上日.Value.ToString("yyyyMMdd") : ""; ;
			row["請求日"] = (請求日.HasValue) ? 請求日.Value.ToString("yyyyMMdd") : ""; ;
			row["得意先コード"] = 得意先コード;
			row["摘要名"] = 摘要名;
			row["売上金額合計"] = 売上金額合計;
			row["外税合計"] = 外税合計;
			row["内税合計"] = 内税合計;
			row["商品コード"] = 商品コード;
			row["マスター区分"] = マスター区分;
			row["商品名"] = 商品名;
			row["数量"] = 数量;
			row["単位"] = 単位;
			row["単価"] = 単価;
			row["売上金額"] = 売上金額;
			return row;
		}
	}
}
