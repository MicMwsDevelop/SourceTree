using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MwsLib.DB.SqlServer.CalcBusinessConsignCommission
{
	public static class CalcBusinessConsignCommissionAccess
	{
		/// <summary>
		/// 販売店情報の取得
		/// </summary>
		/// <returns>レコード数</returns>
		public static List<Tuple<string, int>> GetSalesOutletInfo()
		{
			DataTable dt = CalcBusinessConsignCommissionGetIO.GetSalesOutletInfo();
			return CalcBusinessConsignCommissionController.ConvertSalesOutletInfo(dt);
		}
	}
}
