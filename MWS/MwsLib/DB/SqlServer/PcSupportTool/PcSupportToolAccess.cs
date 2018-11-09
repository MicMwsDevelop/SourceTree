using MwsLib.BaseFactory.PcSupportTool;
using System;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.PcSupportTool
{
	public static class PcSupportToolAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 受注情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static List<OrderInfo> GetOrderInfo(bool sqlsv2 = false)
		{
			DataTable dt = PcSupportToolGetIO.GetOrderInfo(sqlsv2);
			return PcSupportToolController.ConvertOrderInfo(dt);
		}

		/// <summary>
		/// 拠店情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static List<BranchInfo> GetBranchEmployeeInfo(bool sqlsv2 = false)
		{
			DataTable dt = PcSupportToolGetIO.GetBranchEmployeeInfo(sqlsv2);
			return PcSupportToolController.ConvertBranchEmployeeInfo(dt);
		}

		/// <summary>
		/// PC安心サポート商品情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static List<PcSupportGoodsInfo> GetPcSupportGoodsInfo(bool sqlsv2 = false)
		{
			DataTable dt = PcSupportToolGetIO.GetPcSupportGoodsInfo(sqlsv2);
			return PcSupportToolController.ConvertPcSupportGoodsInfo(dt);
		}

		/// <summary>
		/// ソフト保守メンテナンス情報の格納
		/// </summary>
		/// <param name="data"></param>
		/// <param name="sqlsv2"></param>
		/// <returns>レコード数</returns>
		public static int SetSoftMaintenanceContract(SoftMaintenanceContract data, bool sqlsv2 = false)
		{
			DataTable dt = PcSupportToolGetIO.GetSoftMaintenanceContract(data.CustomerNo, sqlsv2);
			if (0 < dt.Rows.Count)
			{
				return PcSupportToolSetIO.UpdateSoftMaintenanceContract(data, sqlsv2);
			}
			return PcSupportToolSetIO.InsertIntoSoftMaintenanceContract(data, sqlsv2);
		}


		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// PC安心サポート管理情報の取得
		/// </summary>
		/// <param name="orderNo">受注No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetDataTablePcSupportControl(string orderNo = "", bool sqlsv2 = false)
		{
			return PcSupportToolGetIO.GetPcSupportControl(orderNo, sqlsv2);
		}

		/// <summary>
		/// PC安心サポート管理情報の取得
		/// </summary>
		/// <param name="orderNo">受注No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static List<PcSupportControl> GetPcSupportControl(string orderNo = "", bool sqlsv2 = false)
		{
			DataTable dt = PcSupportToolGetIO.GetPcSupportControl(orderNo, sqlsv2);
			return PcSupportToolController.ConvertPcSupportControl(dt);
		}

		/// <summary>
		/// PC安心サポート管理情報の格納
		/// </summary>
		/// <param name="data">PC安心サポート管理情報</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static int SetPcSupportControl(PcSupportControl data, bool sqlsv2 = false)
		{
			DataTable dt = PcSupportToolGetIO.GetPcSupportControl(data.OrderNo, sqlsv2);
			if (0 < dt.Rows.Count)
			{
				return PcSupportToolSetIO.UpdatePcSupportControl(data, sqlsv2);
			}
			return PcSupportToolSetIO.InsertIntoPcSupportControl(data, sqlsv2);
		}

		/// <summary>
		/// メールアドレスの取得
		/// </summary>
		/// <param name="sqlsv2"></param>
		/// <returns></returns>
		public static List<Tuple<int, string>> GetMailAddress(bool sqlsv2 = false)
		{
			DataTable dt = PcSupportToolGetIO.GetMailAddress(sqlsv2);
			return PcSupportToolController.ConvertMailAddress(dt);
		}
	}
}
