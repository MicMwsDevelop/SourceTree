using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MwsLib.BaseFactory.PcSafetySupport;

namespace MwsLib.DB.SqlServer.PcSafetySupport
{
	public static class PcSafetySupportAccess
	{
		/// <summary>
		/// ＰＣ安心サポート管理情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <param name="customerID">顧客ID</param>
		/// <returns>レコード数</returns>
		public static DataTable GetPcSupportControl(string customerID = "", bool sqlsv2 = false)
		{
			return PcSafetySupportGetIO.GetPcSupportControl(customerID, sqlsv2);
		}

		/// <summary>
		/// ＰＣ安心サポート管理情報の格納
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static int SetPcSupportControl(PcSupportControl data, bool sqlsv2)
		{
			DataTable dt = PcSafetySupportGetIO.GetPcSupportControl(data.CustomerID, sqlsv2);
			if (0 < dt.Rows.Count)
			{
				return PcSafetySupportSetIO.UpdatePcSupportControl(data, sqlsv2);
			}
			return PcSafetySupportSetIO.InsertIntoPcSupportControl(data, sqlsv2);
		}
	}
}
