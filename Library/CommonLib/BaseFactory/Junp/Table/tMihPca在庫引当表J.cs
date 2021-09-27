//
// tMihPca在庫引当表J.cs
//
// PCA在庫引当表情報クラス
// [JunpDB].[dbo].[tMihPca在庫引当表J]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
//
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.Table
{
	public class tMihPca在庫引当表J
	{
		public int f部門コード { get; set; }
		public string f部門名 { get; set; }
		public short f商品区分 { get; set; }
		public string f商品区分名 { get; set; }
		public string f商品コード { get; set; }
		public string f商品名 { get; set; }
		public int? fPCA現在庫数 { get; set; }
		public int? f引当数 { get; set; }
		public int? f引当在庫数 { get; set; }
		public int? f入荷予定数 { get; set; }
		public DateTime? f作成日 { get; set; }

		/// <summary>
		/// 有効在庫数の取得
		/// 有効在庫数 = 現在庫数 - 引当済数
		/// </summary>
		public int 有効在庫数
		{
			get
			{
				if (fPCA現在庫数.HasValue && f引当数.HasValue)
				{
					return fPCA現在庫数.Value - f引当数.Value;
				}
				return 0;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMihPca在庫引当表J()
		{
			f部門コード = 0;
			f部門名 = string.Empty;
			f商品区分 = 0;
			f商品区分名 = string.Empty;
			f商品コード = string.Empty;
			f商品名 = string.Empty;
			fPCA現在庫数 = null;
			f引当数 = null;
			f引当在庫数 = null;
			f入荷予定数 = null;
			f作成日 = null;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<tMihPca在庫引当表J> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<tMihPca在庫引当表J> result = new List<tMihPca在庫引当表J>();
				foreach (DataRow row in table.Rows)
				{
					tMihPca在庫引当表J data = new tMihPca在庫引当表J
					{
						f部門コード = DataBaseValue.ConvObjectToInt(row["f部門コード"]),
						f部門名 = row["f部門名"].ToString().Trim(),
						f商品区分 = DataBaseValue.ConvObjectToShort(row["f商品区分"]),
						f商品区分名 = row["f商品区分名"].ToString().Trim(),
						f商品コード = row["f商品コード"].ToString().Trim(),
						f商品名 = row["f商品名"].ToString().Trim(),
						fPCA現在庫数 = DataBaseValue.ConvObjectToIntNull(row["fPCA現在庫数"]),
						f引当数 = DataBaseValue.ConvObjectToIntNull(row["f引当数"]),
						f引当在庫数 = DataBaseValue.ConvObjectToIntNull(row["f引当在庫数"]),
						f入荷予定数 = DataBaseValue.ConvObjectToIntNull(row["f入荷予定数"]),
						f作成日 = DataBaseValue.ConvObjectToDateTimeNull(row["f作成日"])
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
