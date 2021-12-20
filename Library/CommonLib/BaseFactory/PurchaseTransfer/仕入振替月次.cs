using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CommonLib.DB;

namespace CommonLib.BaseFactory.PurchaseTransfer
{
	/// <summary>
	/// 7 りすとん月額仕入振替月次
	/// 8 office365仕入振替月次
	/// 9 問心伝月額仕入振替月次
	/// 10 ソフトバンク仕入振替月次
	/// 11 Curline本体アプリ仕入作成月次
	/// </summary>
	public class 仕入振替月次
	{
		public string 仕入先 { get; set; }
		public string sykd_jbmn { get; set; }
		public string sykd_jtan { get; set; }
		public string sykd_scd { get; set; }
		public short sykd_mkbn { get; set; }
		public string sykd_mei { get; set; }
		public int 数量 { get; set; }
		public string sykd_tani { get; set; }
		public int 評価単価 { get; set; }
		public int sykd_rate { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 仕入振替月次()
		{
			仕入先 = string.Empty;
			sykd_jbmn = string.Empty;
			sykd_jtan = string.Empty;
			sykd_scd = string.Empty;
			sykd_mkbn = 0;
			sykd_mei = string.Empty;
			数量 = 0;
			sykd_tani = string.Empty;
			評価単価 = 0;
			sykd_rate = 0;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<仕入振替月次> DataTableToList(DataTable table)
		{
			List<仕入振替月次> result = new List<仕入振替月次>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					仕入振替月次 data = new 仕入振替月次
					{
						仕入先 = row["仕入先"].ToString().Trim(),
						sykd_jbmn = row["sykd_jbmn"].ToString().Trim(),
						sykd_jtan = row["sykd_jtan"].ToString().Trim(),
						sykd_scd = row["sykd_scd"].ToString().Trim(),
						sykd_mkbn = DataBaseValue.ConvObjectToShort(row["sykd_mkbn"]),
						sykd_mei = row["sykd_mei"].ToString().Trim(),
						数量 = DataBaseValue.ConvObjectToInt(row["数量"]),
						sykd_tani = row["sykd_tani"].ToString().Trim(),
						評価単価 = DataBaseValue.ConvObjectToInt(row["評価単価"]),
						sykd_rate = DataBaseValue.ConvObjectToInt(row["sykd_rate"]),
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
