using MwsLib.BaseFactory.PcSafetySupport;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.PcSafetySupport
{
	public static class PcSafetySupportAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 拠店情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static List<BranchInfo> GetBranchEmployeeInfo(bool sqlsv2 = false)
		{
			DataTable dt = PcSafetySupportGetIO.GetBranchEmployeeInfo(sqlsv2);
			return PcSafetySupportController.ConvertBranchEmployeeInfo(dt);
		}

		/// <summary>
		/// PC安心サポート商品情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static List<PcSupportGoodsInfo> GetPcSupportGoodsInfo(bool sqlsv2 = false)
		{
			DataTable dt = PcSafetySupportGetIO.GetPcSupportGoodsInfo(sqlsv2);
			return PcSafetySupportController.ConvertPcSupportGoodsInfo(dt);
		}

		/// <summary>
		/// ソフト保守メンテナンス情報の格納
		/// </summary>
		/// <param name="data"></param>
		/// <param name="sqlsv2"></param>
		/// <returns>レコード数</returns>
		public static int SetSoftMaintenanceContract(SoftMaintenanceContract data, bool sqlsv2 = false)
		{
			DataTable dt = PcSafetySupportGetIO.GetSoftMaintenanceContract(data.CustomerID, sqlsv2);
			if (0 < dt.Rows.Count)
			{
				return PcSafetySupportSetIO.UpdateSoftMaintenanceContract(data, sqlsv2);
			}
			return PcSafetySupportSetIO.InsertIntoSoftMaintenanceContract(data, sqlsv2);
		}


		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// PC安心サポート管理情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <param name="customerID">顧客ID</param>
		/// <returns>レコード数</returns>
		public static DataTable GetPcSupportControl(string customerID = "", bool sqlsv2 = false)
		{
			return PcSafetySupportGetIO.GetPcSupportControl(customerID, sqlsv2);
		}

		/// <summary>
		/// PC安心サポート管理情報の格納
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
