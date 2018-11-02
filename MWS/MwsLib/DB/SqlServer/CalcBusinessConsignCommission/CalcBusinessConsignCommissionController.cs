using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MwsLib.DB.SqlServer.CalcBusinessConsignCommission
{
	public static class CalcBusinessConsignCommissionController
	{
		/// <summary>
		/// 販売店情報の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>販売店情報リスト</returns>
		public static List<Tuple<string, int>> ConvertSalesOutletInfo(DataTable table)
		{
			List<Tuple<string, int>> result = null;
			if (null != table)
			{
				result = new List<Tuple<string, int>>();
				foreach (DataRow row in table.Rows)
				{
					string code = row["仕入先コード"].ToString();
					int rate = DataBaseValue.ConvObjectToInt(row["手数料率"]);
					result.Add(new Tuple<string, int>(code, rate));
				}
			}
			return result;
		}
	}
}
