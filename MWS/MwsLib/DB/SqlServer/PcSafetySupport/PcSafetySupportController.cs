using MwsLib.BaseFactory.PcSafetySupport;
using System.Collections.Generic;
using System.Data;
using MwsLib.Common;

namespace MwsLib.DB.SqlServer.PcSafetySupport
{
	public static class PcSafetySupportController
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// ソフト保守情報の詰め替え
		/// [JunpDB].[dbo].[tMik保守契約]
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>ソフト保守情報リスト</returns>
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
					string dateStr = row["fhsS契約書回収年月"].ToString();
					if (0 < dateStr.Length)
					{
						contract.CollectionDate = Date.Parse(dateStr);
					}
					contract.AgreeYear = DataBaseValue.ConvObjectToInt(row["fhsS契約年数"]);
					contract.Price = DataBaseValue.ConvObjectToInt(row["fhsSメンテ料金"]);
					string ymStr = row["fhsSメンテ契約開始"].ToString();
					if (0 < ymStr.Length)
					{
						contract.StartYM = YearMonth.Parse(ymStr);
					}
					ymStr = row["fhsSメンテ契約終了"].ToString();
					if (0 < ymStr.Length)
					{
						contract.EndYM = YearMonth.Parse(ymStr);
					}
					result.Add(contract);
				}
			}
			return result;
		}

		/// <summary>
		/// 拠店従業員情報の詰め替え
		/// [JunpDB[.[dbo].[vMic担当者]
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>拠店従業員情報リスト</returns>
		public static List<BranchInfo> ConvertBranchEmployeeInfo(DataTable table)
		{
			List<BranchInfo> result = null;
			if (null != table)
			{
				result = new List<BranchInfo>();
				foreach (DataRow row in table.Rows)
				{
					EmployeeInfo employee = new EmployeeInfo();
					employee.UserID = row["fUsrID"].ToString().Trim();
					employee.UserName = row["fUsrName"].ToString().Trim();
					employee.BranchCode3 = row["fBshCode3"].ToString().Trim();
					if (0 < employee.BranchCode3.Length)
					{
						BranchInfo branch = result.Find(p => p.BranchCode3 == employee.BranchCode3);
						if (null == branch)
						{
							branch = new BranchInfo();
							branch.BranchCode2 = row["fBshCode2"].ToString().Trim();
							branch.BranchName2 = row["fBshName2"].ToString().Trim();
							branch.BranchCode3 = employee.BranchCode3;
							branch.BranchName3 = row["fBshName3"].ToString().Trim();
							branch.EmployeeList.Add(employee);
							result.Add(branch);
						}
						else
						{
							branch.EmployeeList.Add(employee);
						}
					}
					else
					{
						string branchCode2 = row["fBshCode2"].ToString().Trim();
						BranchInfo branch = result.Find(p => p.BranchCode2 == branchCode2);
						if (null != branch)
						{
							employee.BranchCode3 = branch.BranchCode2;
							branch.EmployeeList.Add(employee);
						}
					}
				}
			}
			return result;
		}

		/// <summary>
		/// PC安心サポート商品情報の詰め替え
		/// [JunpDB[.[dbo].[vMic担当者]
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>PC安心サポート商品情報リスト</returns>
		public static List<PcSupportGoodsInfo> ConvertPcSupportGoodsInfo(DataTable table)
		{
			List<PcSupportGoodsInfo> result = null;
			if (null != table)
			{
				result = new List<PcSupportGoodsInfo>();
				foreach (DataRow row in table.Rows)
				{
					PcSupportGoodsInfo goods = new PcSupportGoodsInfo();
					goods.GoodsID = row["sms_scd"].ToString().Trim();
					goods.GoodsName = row["sms_mei"].ToString().Trim();
					goods.Price = DataBaseValue.ConvObjectToInt(row["sms_hyo"]);
					result.Add(goods);
				}
			}
			return result;
		}


		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// PC安心サポート管理情報の詰め替え
		/// [Charlie].[dbo].[T_PC_SUPPORT_CONTORL]
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
					control.StartDate = DataBaseValue.ConvObjectToDateNullByDate(row["START_DATE"]);
					control.EndDate = DataBaseValue.ConvObjectToDateNullByDate(row["END_DATE"]);
					control.PeriodEndDate = DataBaseValue.ConvObjectToDateNullByDate(row["PERIOD_END_DATE"]);
					control.AgreeYear = DataBaseValue.ConvObjectToInt(row["AGREE_YEAR"]);
					control.Price = DataBaseValue.ConvObjectToInt(row["PRICE"]);
					control.SalesmanID = row["MARKETING_SPECIALIST_ID"].ToString();
					control.BranchID = row["BRANCH_ID"].ToString();
					control.ApplyDate = DataBaseValue.ConvObjectToDateNull(row["APPLY_DATE"]);
					control.ApplyReportAccept = (1 == DataBaseValue.ConvObjectToInt(row["APPLY_REPORT_ACCEPT"])) ? true : false;
					control.MaleAddress = row["MALE_ADDRESS"].ToString();
					control.Remark1 = row["REMARK1"].ToString();
					control.Remark2 = row["REMARK2"].ToString();
					control.StartMaleDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["START_MALE_DATE"]);
					control.GuideMaleDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["GUIDE_MALE_DATE"]);
					control.UpdateMaleDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["UPDATE_MALE_DATE"]);
					control.CancelDate = DataBaseValue.ConvObjectToDateNullByDate(row["CANCEL_DATE"]);
					control.CancelReportAccept = (1 == DataBaseValue.ConvObjectToInt(row["CANCEL_REPORT_ACCEPT"])) ? true : false;
					control.CancelReason = row["CANCEL_REASON"].ToString();
					control.CreateDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["CREATE_DATE"]);
					control.CreatePerson = row["CREATE_PERSON"].ToString();
					control.UpdateDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["UPDATE_DATE"]);
					control.UpdatePerson = row["UPDATE_PERSON"].ToString();
					control.WonderWebRenewalFlag = (1 == DataBaseValue.ConvObjectToInt(row["WW_RENEWAL_FLAG"])) ? true : false;
					result.Add(control);
				}
			}
			return result;
		}
	}
}
