//
// tMih送料商品コード.cs
//
// 送料商品コード情報クラス
// [JunpDB].[dbo].[tMih送料商品コード]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.Table
{
	public class tMih送料商品コード
	{
		public string f商品コード { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMih送料商品コード()
		{
			f商品コード = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<string> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<string> result = new List<string>();
				foreach (DataRow row in table.Rows)
				{
					result.Add(row["f商品コード"].ToString().Trim());
				}
				return result;
			}
			return null;
		}
	}
}
