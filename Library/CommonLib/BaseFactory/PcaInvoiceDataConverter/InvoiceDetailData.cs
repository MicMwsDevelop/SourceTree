//
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
using System.Data;

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
		/// DataRow → オブジェクト
		/// </summary>
		/// <param name="row"></param>
		/// <returns>請求明細データ</returns>
		public static InvoiceDetailData DataTableToObject(DataRow row)
		{
			InvoiceDetailData data = new InvoiceDetailData();
			data.請求実績請求先コード = row["請求実績請求先コード"].ToString().Trim();
			data.請求先名1 = row["請求先名1"].ToString().Trim();
			data.請求先名2 = row["請求先名2"].ToString().Trim();
			data.請求先郵便番号 = row["請求先郵便番号"].ToString().Trim();
			data.請求先住所1 = row["請求先住所1"].ToString().Trim();
			data.請求先住所2 = row["請求先住所2"].ToString().Trim();
			data.請求先会社TEL = row["請求先会社TEL"].ToString().Trim();
			data.請求期間開始日 = ConvObjectDateTimeNull(row["請求期間開始日"].ToString().Trim());
			data.請求期間終了日 = ConvObjectDateTimeNull(row["請求期間終了日"].ToString().Trim());
			data.前回請求額 = row["前回請求額"].ToString().Trim().ToInt();
			data.期間入金額 = row["期間入金額"].ToString().Trim().ToInt();
			data.期間調整額 = row["期間調整額"].ToString().Trim().ToInt();
			data.繰越金額 = row["繰越金額"].ToString().Trim().ToInt();
			data.期間売上額 = row["期間売上額"].ToString().Trim().ToInt();
			data.期間外税額 = row["期間外税額"].ToString().Trim().ToInt();
			data.期間内税額 = row["期間内税額"].ToString().Trim().ToInt();
			data.伝票No = row["伝票No"].ToString().Trim().ToInt();
			data.売上日 = ConvObjectDateTimeNull(row["売上日"].ToString().Trim());
			data.請求日 = ConvObjectDateTimeNull(row["請求日"].ToString().Trim());
			data.得意先コード = row["得意先コード"].ToString().Trim();
			data.摘要名 = row["摘要名"].ToString().Trim();
			data.売上金額合計 = row["売上金額合計"].ToString().Trim().ToInt();
			data.外税合計 = row["外税合計"].ToString().Trim().ToInt();
			data.内税合計 = row["内税合計"].ToString().Trim().ToInt();
			data.商品コード = row["商品コード"].ToString().Trim();
			data.マスター区分 = row["マスター区分"].ToString().Trim().ToInt();
			data.商品名 = row["商品名"].ToString().Trim();
			data.数量 = row["数量"].ToString().Trim().ToInt();
			data.単位 = row["単位"].ToString().Trim();
			data.単価 = row["単価"].ToString().Trim().ToInt();
			data.売上金額 = row["売上金額"].ToString().Trim().ToInt();
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
