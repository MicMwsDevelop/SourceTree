//
// tBusho.cs
//
// 部署情報クラス
// [JunpDB].[dbo].[tBusho]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.Table
{
	/// <summary>
	/// 部署情報
	/// </summary>
	public class tBusho
	{
		public string fBshCode1 { get; set; }
		
		/// <summary>
		/// 部コード
		/// </summary>
		public string fBshCode2 { get; set; }
		
		/// <summary>
		/// 拠点コード
		/// </summary>
		public string fBshCode3 { get; set; }

		/// <summary>
		/// 所属名
		/// </summary>
		public string fBshName1 { get; set; }

		/// <summary>
		/// 部署名
		/// </summary>
		public string fBshName2 { get; set; }

		/// <summary>
		/// 拠点名
		/// </summary>
		public string fBshName3 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string fBshBumon { get; set; }

		/// <summary>
		/// 部署種別
		/// </summary>
		public string fBshType { get; set; }

		/// <summary>
		/// 部署略称
		/// </summary>
		public string fBshNameRyaku { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tBusho()
		{
			fBshCode1 = string.Empty;
			fBshCode2 = string.Empty;
			fBshCode3 = string.Empty;
			fBshName1 = string.Empty;
			fBshName2 = string.Empty;
			fBshName3 = string.Empty;
			fBshBumon = string.Empty;
			fBshType = string.Empty;
			fBshNameRyaku = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<tBusho> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<tBusho> result = new List<tBusho>();
				foreach (DataRow row in table.Rows)
				{
					tBusho data = new tBusho
					{
						fBshCode1 = row["fBshCode1"].ToString().Trim(),
						fBshCode2 = row["fBshCode2"].ToString().Trim(),
						fBshCode3 = row["fBshCode3"].ToString().Trim(),
						fBshName1 = row["fBshName1"].ToString().Trim(),
						fBshName2 = row["fBshName2"].ToString().Trim(),
						fBshName3 = row["fBshName3"].ToString().Trim(),
						fBshBumon = row["fBshBumon"].ToString().Trim(),
						fBshType = row["fBshType"].ToString().Trim(),
						fBshNameRyaku = row["fBshNameRyaku"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
