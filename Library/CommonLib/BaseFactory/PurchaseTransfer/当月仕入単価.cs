using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CommonLib.DB;

namespace CommonLib.BaseFactory.PurchaseTransfer
{
	public class 当月仕入単価
	{
		public string 商品コード { get; set; }
		public int 単価 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 当月仕入単価()
		{
			商品コード = string.Empty;
			単価 = 0;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<当月仕入単価> DataTableToList(DataTable table)
		{
			List<当月仕入単価> result = new List<当月仕入単価>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					当月仕入単価 data = new 当月仕入単価
					{
						商品コード = row["商品コード"].ToString().Trim(),
						単価 = DataBaseValue.ConvObjectToInt(row["単価"]),
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
