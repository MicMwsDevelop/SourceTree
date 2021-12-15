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
	/// 4-1 対象月社内仕入伝票
	/// </summary>
	public class 対象月社内仕入伝票
	{
		public int 仕入日 { get; set; }
		public int 伝票No { get; set; }
		public string 仕入先コード { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 対象月社内仕入伝票()
		{
			仕入日 = 0;
			伝票No = 0;
			仕入先コード = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<対象月社内仕入伝票> DataTableToList(DataTable table)
		{
			List<対象月社内仕入伝票> result = new List<対象月社内仕入伝票>();
			if (null != table && 0 < table.Rows.Count)
			{
				foreach (DataRow row in table.Rows)
				{
					対象月社内仕入伝票 data = new 対象月社内仕入伝票
					{
						仕入日 = DataBaseValue.ConvObjectToInt(row["仕入日"]),
						伝票No = DataBaseValue.ConvObjectToInt(row["伝票No"]),
						仕入先コード = row["仕入先コード"].ToString().Trim(),
					};
					result.Add(data);
				}
			}
			return result;
		}
	}
}
