using CommonLib.DB;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.PurchaseTransfer
{
	/// <summary>
	/// 5-2 りすとん仕入振替月次合計行追加
	/// 5-3 りすとん仕入振替月次追加
	/// 6-2 問心伝仕入振替月次合計行追加
	/// 6-3 問心伝仕入振替月次追加
	/// </summary>
	public class 仕入振替月次追加
	{
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
		public 仕入振替月次追加()
		{
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
		public static List<仕入振替月次追加> DataTableToList(DataTable table)
		{
			List<仕入振替月次追加> result = new List<仕入振替月次追加>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					仕入振替月次追加 data = new 仕入振替月次追加
					{
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
