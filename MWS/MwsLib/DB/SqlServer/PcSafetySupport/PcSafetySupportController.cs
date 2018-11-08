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
					control.StartDate = DataBaseValue.ConvObjectToDateNullByDate(row["START_DATE"]);
					control.EndDate = DataBaseValue.ConvObjectToDateNullByDate(row["END_DATE"]);
					control.PeriodEndDate = DataBaseValue.ConvObjectToDateNullByDate(row["PERIOD_END_DATE"]);
					control.AgreeYear = DataBaseValue.ConvObjectToInt(row["AGREE_YEAR"]);
					control.Price = DataBaseValue.ConvObjectToInt(row["PRICE"]);
					control.SalesmanID = row["MARKETING_SPECIALIST_ID"].ToString();
					control.BranchID = row["BRANCH_ID"].ToString();
					control.OrderDate = DataBaseValue.ConvObjectToDateNullByDate(row["ORDER_DATE"]);
					control.OrderReportAccept = (1 == DataBaseValue.ConvObjectToInt(row["ORDER_REPORT_ACCEPT"])) ? true : false;
					control.OrderApprovalDate = DataBaseValue.ConvObjectToDateNullByDate(row["ORDER_APPROVAL_DATE"]);
					control.MailAddress = row["MAIL_ADDRESS"].ToString();
					control.Remark1 = row["REMARK1"].ToString();
					control.Remark2 = row["REMARK2"].ToString();
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
		/// PC安心サポートメール送信情報の詰め替え
		/// [Charlie].[dbo].[T_PC_SUPPORT_MAIL]
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>PC安心サポートメール送信情報リスト</returns>
		public static List<PcSupportMail> ConvertPcSupportMail(DataTable table)
		{
			List<PcSupportMail> result = null;
			if (null != table)
			{
				result = new List<PcSupportMail>();
				foreach (DataRow row in table.Rows)
				{
					PcSupportMail mail = new PcSupportMail();
					mail.OrderNo = row["ORDER_NO"].ToString();
					mail.CustomerNo = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]);
					string sendMailType = row["SEND_MAIL_TYPE"].ToString();
					if ("1" == sendMailType)
					{
						mail.SendMailType = PcSupportMail.MailType.Start;
					}
					else if ("2" == sendMailType)
					{
						mail.SendMailType = PcSupportMail.MailType.Guide;
					}
					else if ("3" == sendMailType)
					{
						mail.SendMailType = PcSupportMail.MailType.Update;
					}
					mail.GoodsID = row["GOODS_ID"].ToString();
					mail.StartDate = DataBaseValue.ConvObjectToDateNullByDate(row["START_DATE"]);
					mail.EndDate = DataBaseValue.ConvObjectToDateNullByDate(row["END_DATE"]);
					mail.AgreeYear = DataBaseValue.ConvObjectToInt(row["AGREE_YEAR"]);
					mail.Price = DataBaseValue.ConvObjectToInt(row["PRICE"]);
					mail.SalesmanID = row["MARKETING_SPECIALIST_ID"].ToString();
					mail.BranchID = row["BRANCH_ID"].ToString();
					mail.OrderDate = DataBaseValue.ConvObjectToDateNullByDate(row["ORDER_DATE"]);
					mail.MailAddress = row["MAIL_ADDRESS"].ToString();
					mail.SendDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["SEND_DATE"]);
					result.Add(mail);
				}
			}
			return result;
		}

		/// <summary>
		/// 医院名の取得
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>医院名</returns>
		public static string ConvertClinicName(DataTable table)
		{
			if (null != table)
			{
				if (0 < table.Rows.Count)
				{
					return table.Rows[0]["CLINIC_NAME"].ToString();
				}
			}
			return string.Empty;
		}
	}
}
