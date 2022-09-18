//
// vMicユーザーオン資用.cs
//
// vMicユーザーオン資用ビュークラス
// [JunpDB].[dbo].[vMicユーザーオン資用]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/09/16 勝呂)
//
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace CommonLib.BaseFactory.Junp.View
{
	/// <summary>
	/// 
	/// </summary>
	public class vMicユーザーオン資用
	{
		public int 顧客No { get; set; }
		public string 終了フラグ { get; set; }
		public string 削除フラグ { get; set; }
		public string 得意先No { get; set; }
		public string 顧客名 { get; set; }
		public string 郵便番号 { get; set; }
		public string 住所 { get; set; }
		public string 電話番号 { get; set; }
		public string FAX番号 { get; set; }
		public string 医保医療コード { get; set; }
		public string 院長名 { get; set; }
		public string 開設者 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public vMicユーザーオン資用()
		{
			顧客No = 0;
			終了フラグ = string.Empty;
			削除フラグ = string.Empty;
			得意先No = string.Empty;
			顧客名 = string.Empty;
			郵便番号 = string.Empty;
			住所 = string.Empty;
			電話番号 = string.Empty;
			FAX番号 = string.Empty;
			医保医療コード = string.Empty;
			院長名 = string.Empty;
			開設者 = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<vMicユーザーオン資用> DataTableToList(DataTable table)
		{
			List<vMicユーザーオン資用> result = new List<vMicユーザーオン資用>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					vMicユーザーオン資用 data = new vMicユーザーオン資用
					{
						顧客No = DataBaseValue.ConvObjectToInt(row["顧客No"]),
						終了フラグ = row["終了フラグ"].ToString().Trim(),
						削除フラグ = row["削除フラグ"].ToString().Trim(),
						得意先No = row["得意先No"].ToString().Trim(),
						顧客名 = row["顧客名"].ToString().Trim(),
						郵便番号 = row["郵便番号"].ToString().Trim(),
						住所 = row["住所"].ToString().Trim(),
						電話番号 = row["電話番号"].ToString().Trim(),
						FAX番号 = row["FAX番号"].ToString().Trim(),
						医保医療コード = row["医保医療コード"].ToString().Trim(),
						院長名 = row["院長名"].ToString().Trim(),
						開設者 = row["開設者"].ToString().Trim(),
					};
					result.Add(data);
				}
			}
			return result;
		}

		/// <summary>
		/// 医療機関コードから数字のみ出力
		/// </summary>
		/// <returns></returns>
		public string GetClinicCodeNumeric()
		{
			return Regex.Replace(医保医療コード, @"[^0-9]", "");
		}
	}
}
