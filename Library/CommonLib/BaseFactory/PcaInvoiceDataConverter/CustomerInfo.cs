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
		/// 顧客名の取得
		/// </summary>
		public string 顧客名
		{
			get
			{
				string name = 顧客名1;
				if (0 < 顧客名2.Length)
				{
					name += "　" + 顧客名2;
				}
				return name;
			}
		}

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
		/// <param name="table"></param>
		/// <returns>タイトル行</returns>
		public static string GetTitle(DataTable table)
		{
			List<string> result = new List<string>();
			foreach (var column in table.Columns)
			{
				result.Add(column.ToString());
			}
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
		/// 紙請求書かどうか？
		/// </summary>
		/// <returns></returns>
		public bool IsAGREX口振通知書()
		{
			return (レセコン区分 != "2") ? true : false;
		}

		/// <summary>
		/// 得意先名１，２を顧客部課と顧客氏名漢字に振り分け
		/// </summary>
		/// <param name="KokyakuBuka"></param>
		/// <param name="KokyakuShimeiKanji"></param>
		public void SplitCustomerName(out string kokyakuBuka, out string kokyakuShimeiKanji)
		{
			kokyakuBuka = string.Empty;
			kokyakuShimeiKanji = string.Empty;

			// 「得意先名１＋" "＋得意先名２」に連結し半角変換
			string custName = StringUtil.ConvertNarrowForUnicode(顧客名);

			// 2007/11/09 M.IMAMURA追加　～【受注停止！】の文言があった場合削除
			// 但し、前処理にて半角に変換されているので半角での比較をおこなう
			kokyakuShimeiKanji = custName.Replace("【受注停止!】", "");


			// 元のプログラムを見ても処理内容が不明なので「顧客部課」は返さないことにする
			// 元の処理でもほとんど「顧客部課」は返していない
			//if (30 < StringUtil.ByteLength(custName))
			//{
			//	custName.
			//}
			//else
			//{
			//	// 「顧客部課」に空文字列を代入
			//	// 「顧客氏名漢字」に「連結後の得意先名」を代入
			//	KokyakuShimeiKanji = custName;
			//}
		}
	}
}
