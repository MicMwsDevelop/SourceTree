using MwsLib.BaseFactory.PcSupportManager;
using System;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.PcSupportManager
{
	public static class PcSupportManagerAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 受注情報リストの取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>受注情報リスト</returns>
		public static List<OrderInfo> GetOrderInfoList(bool sqlsv2 = false)
		{
			DataTable dt = PcSupportManagerGetIO.GetOrderInfoList(sqlsv2);
			return PcSupportManagerController.ConvertOrderInfo(dt);
		}

		/// <summary>
		/// 受注情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>受注情報</returns>
		public static OrderInfo GetOrderInfo(int customerNo, bool sqlsv2 = false)
		{
			DataTable dt = PcSupportManagerGetIO.GetOrderInfo(customerNo, sqlsv2);
			List<OrderInfo> orderInfoList = PcSupportManagerController.ConvertOrderInfo(dt);
			if (1 == orderInfoList.Count)
			{
				return orderInfoList[0];
			}
			return null;
		}

		/// <summary>
		/// 拠店情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>拠店情報</returns>
		public static List<BranchInfo> GetBranchEmployeeInfo(bool sqlsv2 = false)
		{
			DataTable dt = PcSupportManagerGetIO.GetBranchEmployeeInfo(sqlsv2);
			return PcSupportManagerController.ConvertBranchEmployeeInfo(dt);
		}

		/// <summary>
		/// PC安心サポート商品情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>PC安心サポート商品情報</returns>
		public static List<PcSupportGoodsInfo> GetPcSupportGoodsInfo(bool sqlsv2 = false)
		{
			DataTable dt = PcSupportManagerGetIO.GetPcSupportGoodsInfo(sqlsv2);
			return PcSupportManagerController.ConvertPcSupportGoodsInfo(dt);
		}

		/// <summary>
		/// ソフト保守メンテナンス情報の格納
		/// </summary>
		/// <param name="data">ソフト保守メンテナンス情報</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static int SetSoftMaintenanceContract(SoftMaintenanceContract data, bool sqlsv2 = false)
		{
			DataTable dt = PcSupportManagerGetIO.GetSoftMaintenanceContract(data.CustomerNo, sqlsv2);
			if (0 < dt.Rows.Count)
			{
				return PcSupportManagerSetIO.UpdateSoftMaintenanceContract(data, sqlsv2);
			}
			return PcSupportManagerSetIO.InsertIntoSoftMaintenanceContract(data, sqlsv2);
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
			return PcSupportManagerGetIO.GetPcSupportControl(orderNo, sqlsv2);
		}

		/// <summary>
		/// PC安心サポート管理情報の取得
		/// </summary>
		/// <param name="orderNo">受注No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>PC安心サポート管理情報</returns>
		public static List<PcSupportControl> GetPcSupportControl(string orderNo = "", bool sqlsv2 = false)
		{
			DataTable dt = PcSupportManagerGetIO.GetPcSupportControl(orderNo, sqlsv2);
			return PcSupportManagerController.ConvertPcSupportControl(dt);
		}

		/// <summary>
		/// PC安心サポート管理情報の格納
		/// </summary>
		/// <param name="data">PC安心サポート管理情報</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static int SetPcSupportControl(PcSupportControl data, bool sqlsv2 = false)
		{
			DataTable dt = PcSupportManagerGetIO.GetPcSupportControl(data.OrderNo, sqlsv2);
			if (0 < dt.Rows.Count)
			{
				return PcSupportManagerSetIO.UpdatePcSupportControl(data, sqlsv2);
			}
			return PcSupportManagerSetIO.InsertIntoPcSupportControl(data, sqlsv2);
		}

		/// <summary>
		/// メールアドレスの取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>メールアドレス</returns>
		public static List<Tuple<int, string>> GetMailAddress(bool sqlsv2 = false)
		{
			DataTable dt = PcSupportManagerGetIO.GetMailAddress(sqlsv2);
			return PcSupportManagerController.ConvertMailAddress(dt);
		}

		/// <summary>
		/// PC安心サポート送信メール情報リストの追加
		/// [Charlie].[dbo].[T_PC_SUPPORT_MAIL]
		/// </summary>
		/// <param name="list">PC安心サポート送信メール情報リスト</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoPcSupportMailList(List<PcSupportMail> list, bool sqlsv2 = false)
		{
			return PcSupportManagerSetIO.InsertIntoPcSupportMailList(list);
		}
	}
}
