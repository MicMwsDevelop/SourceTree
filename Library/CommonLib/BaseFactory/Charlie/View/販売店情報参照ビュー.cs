//
// 販売店情報参照ビュー.cs
//
// [CharlieDB].[dbo].[販売店情報参照ビュー]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.View
{
	public class 販売店情報参照ビュー
	{
		public int 販売店コード { get; set; }
		public string 得意先コード { get; set; }
		public string 仕入先コード { get; set; }
		public string 販売店ランクコード { get; set; }
		public string 販売店ランク名 { get; set; }
		public string 販売店名 { get; set; }
		public string 販売店グループコード { get; set; }
		public string 販売店グループ名 { get; set; }
		public string 契約締結日 { get; set; }
		public string 担当支店名 { get; set; }
		public string 販売店名フリガナ { get; set; }
		public string 電話番号 { get; set; }
		public string 住所 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 販売店情報参照ビュー()
		{
			販売店コード = 0;
			得意先コード = string.Empty;
			仕入先コード = string.Empty;
			販売店ランクコード = string.Empty;
			販売店ランク名 = string.Empty;
			販売店名 = string.Empty;
			販売店グループコード = string.Empty;
			販売店グループ名 = string.Empty;
			契約締結日 = string.Empty;
			担当支店名 = string.Empty;
			販売店名フリガナ = string.Empty;
			電話番号 = string.Empty;
			住所 = string.Empty;
		}

		/// <summary>
		/// [charlieDB].[dbo].[販売店情報参照ビュー]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>販売店情報参照ビュー</returns>
		public static List<販売店情報参照ビュー> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<販売店情報参照ビュー> result = new List<販売店情報参照ビュー>();
				foreach (DataRow row in table.Rows)
				{
					販売店情報参照ビュー data = new 販売店情報参照ビュー
					{
						販売店コード = DataBaseValue.ConvObjectToInt(row["販売店コード"]),
						得意先コード = row["得意先コード"].ToString().Trim(),
						仕入先コード = row["仕入先コード"].ToString().Trim(),
						販売店ランクコード = row["販売店ランクコード"].ToString().Trim(),
						販売店ランク名 = row["販売店ランク名"].ToString().Trim(),
						販売店名 = row["販売店名"].ToString().Trim(),
						販売店グループコード = row["販売店グループコード"].ToString().Trim(),
						販売店グループ名 = row["販売店グループ名"].ToString().Trim(),
						契約締結日 = row["契約締結日"].ToString().Trim(),
						担当支店名 = row["担当支店名"].ToString().Trim(),
						販売店名フリガナ = row["販売店名フリガナ"].ToString().Trim(),
						電話番号 = row["電話番号"].ToString().Trim(),
						住所 = row["住所"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
