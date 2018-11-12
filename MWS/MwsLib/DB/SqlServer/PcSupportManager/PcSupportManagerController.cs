using MwsLib.BaseFactory.PcSupportManager;
using MwsLib.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.PcSupportManager
{
	public static class PcSupportManagerController
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 受注情報の詰め替え
		/// [JunpDB].[dbo].[tMih受注ヘッダ]
		/// [JunpDB].[dbo].[tMih受注詳細]
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>ソフト保守情報リスト</returns>
		public static List<OrderInfo> ConvertOrderInfo(DataTable table)
		{
			List<OrderInfo> result = null;
			if (null != table)
			{
				result = new List<OrderInfo>();
				foreach (DataRow row in table.Rows)
				{
					OrderInfo order = new OrderInfo();
					order.OrderNo = row["受注番号"].ToString();
					order.CustomerNo = DataBaseValue.ConvObjectToInt(row["顧客No"]);
					order.ClinicName = row["医院名"].ToString();
					order.GoodsID = row["商品コード"].ToString();
					order.GoodsName = row["商品名"].ToString();
					order.PriceStr = row["料金"].ToString();
					order.BranchID = row["拠店ID"].ToString();
					order.BranchName = row["拠点名"].ToString();
					order.SalesmanID = row["担当者ID"].ToString();
					order.SalesmanName = row["担当者名"].ToString();
					order.OrderDate = DataBaseValue.ConvObjectToDateNullByDate(row["受注日"]);
					order.OrderApprovalDate = DataBaseValue.ConvObjectToDateNullByDate(row["受注承認日"]);
					order.Remark = row["備考"].ToString();
					result.Add(order);
				}
			}
			return result;
		}

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
					contract.CustomerNo = DataBaseValue.ConvObjectToInt(row["fhsCliMicID"]);
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
					control.OrderNo = row["ORDER_NO"].ToString();
					control.CustomerNo = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]);
					control.ClinicName = row["CLINIC_NAME"].ToString();
					control.GoodsID = row["GOODS_ID"].ToString();
					control.GoodsName = row["GOODS_NAME"].ToString();
					control.Price = DataBaseValue.ConvObjectToInt(row["PRICE"]);
					control.AgreeYear = DataBaseValue.ConvObjectToInt(row["AGREE_YEAR"]);
					control.StartDate = DataBaseValue.ConvObjectToDateNullByDate(row["START_DATE"]);
					control.EndDate = DataBaseValue.ConvObjectToDateNullByDate(row["END_DATE"]);
					control.PeriodEndDate = DataBaseValue.ConvObjectToDateNullByDate(row["PERIOD_END_DATE"]);
					control.BranchID = row["BRANCH_ID"].ToString();
					control.BranchName = row["BRANCH_NAME"].ToString();
					control.SalesmanID = row["SALESMAN_ID"].ToString();
					control.SalesmanName = row["SALESMAN_NAME"].ToString();
					control.OrderDate = DataBaseValue.ConvObjectToDateNullByDate(row["ORDER_DATE"]);
					control.OrderReportAccept = (1 == DataBaseValue.ConvObjectToInt(row["ORDER_REPORT_ACCEPT"])) ? true : false;
					control.OrderApprovalDate = DataBaseValue.ConvObjectToDateNullByDate(row["ORDER_APPROVAL_DATE"]);
					control.MailAddress = row["MAIL_ADDRESS"].ToString();
					control.Remark = row["REMARK"].ToString();
					control.StartMailDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["START_MAIL_DATE"]);
					control.GuideMailDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["GUIDE_MAIL_DATE"]);
					control.UpdateMailDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["UPDATE_MAIL_DATE"]);
					control.CancelDate = DataBaseValue.ConvObjectToDateNullByDate(row["CANCEL_DATE"]);
					control.CancelReportAccept = (1 == DataBaseValue.ConvObjectToInt(row["CANCEL_REPORT_ACCEPT"])) ? true : false;
					control.CancelReason = row["CANCEL_REASON"].ToString();
					control.DisableFlag = (1 == DataBaseValue.ConvObjectToInt(row["DISABLE_FLAG"])) ? true : false;
					control.WonderWebRenewalFlag = (1 == DataBaseValue.ConvObjectToInt(row["WW_RENEWAL_FLAG"])) ? true : false;
					control.CreateDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["CREATE_DATE"]);
					control.CreatePerson = row["CREATE_PERSON"].ToString();
					control.UpdateDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["UPDATE_DATE"]);
					control.UpdatePerson = row["UPDATE_PERSON"].ToString();
					result.Add(control);
				}
			}
			return result;
		}

		/// <summary>
		/// メールアドレスの取得
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<Tuple<int, string>> ConvertMailAddress(DataTable table)
		{
			List<Tuple<int, string>> result = null;
			if (null != table)
			{
				result = new List<Tuple<int, string>>();
				foreach (DataRow row in table.Rows)
				{
					PcSupportControl control = new PcSupportControl();
					int customerID = DataBaseValue.ConvObjectToInt(row["顧客ＩＤ"]);
					string mailAddress = row["メールアドレス"].ToString();
					result.Add(new Tuple<int, string>(customerID, mailAddress));
				}
			}
			return result;
		}
	}
}
