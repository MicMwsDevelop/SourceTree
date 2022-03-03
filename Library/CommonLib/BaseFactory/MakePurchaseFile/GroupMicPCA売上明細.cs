//
// GroupMicPCA売上明細.cs
// 
// GroupMicPCA売上明細情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/01/07 勝呂)
//
using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.MakePurchaseFile
{
	public class GroupMicPCA売上明細
	{
		public string sykd_jbmn { get; set; }
		public string sykd_jtan { get; set; }
		public string sykd_scd { get; set; }
		public short sykd_mkbn { get; set; }
		public string sykd_tani { get; set; }
		public int sykd_uribi { get; set; }
		public int 消費税率 { get; set; }
		public int 数量 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public GroupMicPCA売上明細()
		{
			sykd_jbmn = string.Empty;
			sykd_jtan = string.Empty;
			sykd_scd = string.Empty;
			sykd_mkbn = 0;
			sykd_tani = string.Empty;
			sykd_uribi = 0;
			消費税率 = 0;
			数量 = 0;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<GroupMicPCA売上明細> DataTableToList(DataTable table)
		{
			List<GroupMicPCA売上明細> result = new List<GroupMicPCA売上明細>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					GroupMicPCA売上明細 data = new GroupMicPCA売上明細
					{
						sykd_jbmn = row["sykd_jbmn"].ToString().Trim(),
						sykd_jtan = row["sykd_jtan"].ToString().Trim(),
						sykd_scd = row["sykd_scd"].ToString().Trim(),
						sykd_mkbn = DataBaseValue.ConvObjectToShort(row["sykd_mkbn"]),
						sykd_tani = row["sykd_tani"].ToString().Trim(),
						sykd_uribi = DataBaseValue.ConvObjectToInt(row["sykd_uribi"]),
						消費税率 = DataBaseValue.ConvObjectToInt(row["消費税率"]),
						数量 = DataBaseValue.ConvObjectToInt(row["数量"]),
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
