using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MwsLib.BaseFactory.PcSafetySupport;
using MwsLib.Common;

namespace MwsLib.DB.SqlServer.PcSafetySupport
{
	public static class PcSafetySupportController
	{
		/// <summary>
		/// ソフト保守情報の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>サービス情報リスト</returns>
		public static List<SoftMaintenanceContract> ConvertServiceInfoList(DataTable table)
		{
			List<SoftMaintenanceContract> result = null;
			if (null != table)
			{
				result = new List<SoftMaintenanceContract>();
				foreach (DataRow row in table.Rows)
				{
					SoftMaintenanceContract contract = new SoftMaintenanceContract();
					contract.CustomerID = row["fhsCliMicID"].ToString();
					contract.Subscription = DataBaseValue.ConvObjectToBool(row["fhsS保守"]);
					contract.CollectionDate = DataBaseValue.ConvObjectToDateNull(row["fhsS契約書回収年月"]);
					contract.AgreeYear = DataBaseValue.ConvObjectToInt(row["fhsS契約年数"]);
					contract.Price = DataBaseValue.ConvObjectToInt(row["fhsSメンテ料金"]);
					contract.StartDate = DataBaseValue.ConvObjectToDateNull(row["fhsSメンテ契約開始"]);
					contract.EndDate = DataBaseValue.ConvObjectToDateNull(row["fhsSメンテ契約終了"]);
					result.Add(contract);
				}
			}
			return result;
		}

		/// <summary>
		/// PC安心サポート管理情報の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>PC安心サポート管理情報リスト</returns>
		public static List<PcSupportControl> ConvertPcSupportControl(DataTable table)
		{
			List<PcSupportControl> result = null;
			if (null != table)
			{
				result = new List<PcSupportControl>();
				foreach (DataRow row in table.Rows)
				{
					PcSupportControl control = new PcSupportControl();
					control.CustomerID = row["CUSTOMER_ID"].ToString();
					control.GoodsID = row["GOODS_ID"].ToString();
					control.StartDate = DataBaseValue.ConvObjectToDateNull(row["START_DATE"]);
					control.EndDate = DataBaseValue.ConvObjectToDateNull(row["END_DATE"]);
					control.PeriodEndDate = DataBaseValue.ConvObjectToDateNull(row["PERIOD_END_DATE"]);
					control.AgreeYear = DataBaseValue.ConvObjectToInt(row["AGREE_YEAR"]);
					control.Price = DataBaseValue.ConvObjectToInt(row["PRICE"]);
					control.SalesmanID = row["MARKETING_SPECIALIST_ID"].ToString();
					control.BranchID = row["BRANCH_ID"].ToString();
					control.ApplyDate = DataBaseValue.ConvObjectToDateNull(row["APPLY_DATE"]);
					control.ApplyReportAccept = DataBaseValue.ConvObjectToBool(row["APPLY_REPORT_ACCEPT"]);
					control.MaleAddress = row["MALE_ADDRESS"].ToString();
					control.Remark1 = row["REMARK1"].ToString();
					control.Remark2 = row["REMARK2"].ToString();
					control.StartMaleDate = DataBaseValue.ConvObjectToDateNull(row["START_MALE_DATE"]);
					control.GuideMaleDate = DataBaseValue.ConvObjectToDateNull(row["GUIDE_MALE_DATE"]);
					control.UpdateMaleDate = DataBaseValue.ConvObjectToDateNull(row["UPDATE_MALE_DATE"]);
					control.CancelDate = DataBaseValue.ConvObjectToDateNull(row["CANCEL_DATE"]);
					control.CancelReportAccept = DataBaseValue.ConvObjectToBool(row["CANCEL_REPORT_ACCEPT"]);
					control.CancelReason = row["CANCEL_REASON"].ToString();
					control.CreateDate = DataBaseValue.ConvObjectToDateNull(row["CREATE_DATE"]);
					control.CreaatePerson = row["CREATE_PERSON"].ToString();
					control.UpdateDate = DataBaseValue.ConvObjectToDateNull(row["UPDATE_DATE"]);
					control.UpdatePerson = row["UPDATE_PERSON"].ToString();
					control.WonderWebRenewalFlag = DataBaseValue.ConvObjectToBool(row["WW_RENEWAL_FLAG"]);
					result.Add(control);
				}
			}
			return result;
		}
	}
}
