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
			DataTable dt = PcSafetySupportGetIO.GetSoftMaintenanceContract(data.CustomerNo, sqlsv2);
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
		/// <param name="orderNo">受注No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetPcSupportControl(string orderNo = "", bool sqlsv2 = false)
		{
			return PcSafetySupportGetIO.GetPcSupportControl(orderNo, sqlsv2);
		}

		/// <summary>
		/// PC安心サポート管理情報の格納
		/// </summary>
		/// <param name="data">PC安心サポート管理情報</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static int SetPcSupportControl(PcSupportControl data, bool sqlsv2 = false)
		{
			DataTable dt = PcSafetySupportGetIO.GetPcSupportControl(data.OrderNo, sqlsv2);
			if (0 < dt.Rows.Count)
			{
				return PcSafetySupportSetIO.UpdatePcSupportControl(data, sqlsv2);
			}
			return PcSafetySupportSetIO.InsertIntoPcSupportControl(data, sqlsv2);
		}

		/// <summary>
		/// PC安心サポート送信メール情報の取得
		/// </summary>
		/// <param name="orderNo">受注No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetPcSupportMail(string orderNo = "", bool sqlsv2 = false)
		{
			return PcSafetySupportGetIO.GetPcSupportMail(orderNo, sqlsv2);
		}

		/// <summary>
		/// PC安心サポート送信メール情報の追加
		/// </summary>
		/// <param name="list">PC安心サポート送信メール情報リスト</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static int SetPcSupportMail(List<PcSupportMail> list, bool sqlsv2 = false)
		{
			return PcSafetySupportSetIO.InsertIntoPcSupportMailList(list, sqlsv2);
		}

		/// <summary>
		/// 医院名の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>医院名</returns>
		public static string GetClinicName(int customerNo, bool sqlsv2 = false)
		{
			DataTable dt = PcSafetySupportGetIO.GetClinicName(customerNo, sqlsv2);
			return PcSafetySupportController.ConvertClinicName(dt);
		}
	}
}
