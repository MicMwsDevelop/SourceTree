using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Junp.Table
{
	public class tMikOS明細印字
	{
		public string fmsコード種別 { get; set; }

		public string fmsコード { get; set; }

		public string fms名称 { get; set; }

		public string fms印字必要 { get; set; }

		public string fms請求書印字必要 { get; set; }

		public string fms発注単位 { get; set; }

		public string fms商品コード1 { get; set; }

		public string fms商品コード2 { get; set; }

		public string fms商品コード3 { get; set; }

		public string fms商品コード4 { get; set; }

		public string fms商品コード5 { get; set; }

		public string fms商品コード6 { get; set; }

		public string fms商品コード7 { get; set; }

		public string fms商品コード8 { get; set; }

		public DateTime? fms更新日 { get; set; }

		public string fms更新者 { get; set; }

		public int fmsID { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public tMikOS明細印字()
		{
			fmsコード種別 = string.Empty;
			fmsコード = string.Empty;
			fms名称 = string.Empty;
			fms印字必要 = string.Empty;
			fms請求書印字必要 = string.Empty;
			fms発注単位 = string.Empty;
			fms商品コード1 = string.Empty;
			fms商品コード2 = string.Empty;
			fms商品コード3 = string.Empty;
			fms商品コード4 = string.Empty;
			fms商品コード5 = string.Empty;
			fms商品コード6 = string.Empty;
			fms商品コード7 = string.Empty;
			fms商品コード8 = string.Empty;
			fms更新日 = null;
			fms更新者 = string.Empty;
			fmsID = 0;
		}

		/// <summary>
		/// 商品コード群文字列の取得
		/// </summary>
		/// <returns></returns>
		public string GetGoodsCodes()
		{
			List<string> ret = new List<string>();
			if (0 < fms商品コード1.Length)
			{
				ret.Add("'" + fms商品コード1 + "'");
			}
			if (0 < fms商品コード2.Length)
			{
				ret.Add("'" + fms商品コード2 + "'");
			}
			if (0 < fms商品コード3.Length)
			{
				ret.Add("'" + fms商品コード3 + "'");
			}
			if (0 < fms商品コード4.Length)
			{
				ret.Add("'" + fms商品コード4 + "'");
			}
			if (0 < fms商品コード5.Length)
			{
				ret.Add("'" + fms商品コード5 + "'");
			}
			if (0 < fms商品コード6.Length)
			{
				ret.Add("'" + fms商品コード6 + "'");
			}
			if (0 < fms商品コード7.Length)
			{
				ret.Add("'" + fms商品コード7 + "'");
			}
			if (0 < fms商品コード8.Length)
			{
				ret.Add("'" + fms商品コード8 + "'");
			}
			return string.Join(",", ret);
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<tMikOS明細印字> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<tMikOS明細印字> result = new List<tMikOS明細印字>();
				foreach (DataRow row in table.Rows)
				{
					tMikOS明細印字 data = new tMikOS明細印字
					{
						fmsコード種別 = row["fmsコード種別"].ToString().Trim(),
						fmsコード = row["fmsコード"].ToString().Trim(),
						fms名称 = row["fms名称"].ToString().Trim(),
						fms印字必要 = row["fms印字必要"].ToString().Trim(),
						fms請求書印字必要 = row["fms請求書印字必要"].ToString().Trim(),
						fms発注単位 = row["fms発注単位"].ToString().Trim(),
						fms商品コード1 = row["fms商品コード1"].ToString().Trim(),
						fms商品コード2 = row["fms商品コード2"].ToString().Trim(),
						fms商品コード3 = row["fms商品コード3"].ToString().Trim(),
						fms商品コード4 = row["fms商品コード4"].ToString().Trim(),
						fms商品コード5 = row["fms商品コード5"].ToString().Trim(),
						fms商品コード6 = row["fms商品コード6"].ToString().Trim(),
						fms商品コード7 = row["fms商品コード7"].ToString().Trim(),
						fms商品コード8 = row["fms商品コード8"].ToString().Trim(),
						fms更新日 = DataBaseValue.ConvObjectToDateTimeNull(row["fms更新日"]),
						fms更新者 = row["fms更新者"].ToString().Trim(),
						fmsID = DataBaseValue.ConvObjectToInt(row["fmsID"]),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
