//
// PurchaseTransferController.cs
//
// 仕入振替 データテーブル詰め替えクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/05/27 勝呂)
// 
using MwsLib.BaseFactory.PurchaseTransfer;
using MwsLib.Common;
using System;
using System.Collections.Generic;
using System.Data;
using MwsLib.BaseFactory.Pca;

namespace MwsLib.DB.SqlServer.PurchaseTransfer
{
	/// <summary>
	/// 仕入振替 データテーブル詰め替えクラス
	/// </summary>
	public static class PurchaseTransferController
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/*
				/// <summary>
				/// 製品サポート情報ソフト保守情報の詰め替え
				/// [JunpDB].[dbo].[tMik保守契約]
				/// </summary>
				/// <param name="table">データテーブル</param>
				/// <returns>製品サポート情報ソフト保守情報リスト</returns>
				public static List<SoftMaintenanceContract> ConvertSoftMaintenanceContractList(DataTable table)
				{
					List<SoftMaintenanceContract> result = null;
					if (null != table)
					{
						result = new List<SoftMaintenanceContract>();
						foreach (DataRow row in table.Rows)
						{
							SoftMaintenanceContract contract = new SoftMaintenanceContract();
							contract.CustomerNo = DataBaseValue.ConvObjectToInt(row["fhsCliMicID"]);
							contract.Subscription = ("1" == row["fhsS保守"].ToString()) ? true : false;
							string dateStr = row["fhsS契約書回収年月"].ToString().Trim();
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
							ymStr = row["fhsSメンテ契約終了"].ToString().Trim();
							if (0 < ymStr.Length)
							{
								contract.EndYM = YearMonth.Parse(ymStr);
							}
							contract.Remark1 = row["fhsSメンテ契約備考1"].ToString();
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
				public static List<BranchEmployeeInfo> ConvertBranchEmployeeInfo(DataTable table)
				{
					List<BranchEmployeeInfo> result = null;
					if (null != table)
					{
						result = new List<BranchEmployeeInfo>();
						foreach (DataRow row in table.Rows)
						{
							EmployeeInfo employee = new EmployeeInfo();
							employee.UserID = row["fUsrID"].ToString().Trim();
							employee.UserName = row["fUsrName"].ToString().Trim();
							employee.BranchCode3 = row["fBshCode3"].ToString().Trim();
							if (0 < employee.BranchCode3.Length)
							{
								BranchEmployeeInfo branch = result.Find(p => p.BranchCode3 == employee.BranchCode3);
								if (null == branch)
								{
									branch = new BranchEmployeeInfo();
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
								BranchEmployeeInfo branch = result.Find(p => p.BranchCode2 == branchCode2);
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
							control.OrderNo = row["ORDER_NO"].ToString().Trim();
							control.CustomerNo = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]);
							control.ClinicName = row["CLINIC_NAME"].ToString().Trim();
							control.GoodsID = row["GOODS_ID"].ToString().Trim();
							control.GoodsName = row["GOODS_NAME"].ToString().Trim();
							control.Price = DataBaseValue.ConvObjectToInt(row["PRICE"]);
							control.AgreeYear = DataBaseValue.ConvObjectToInt(row["AGREE_YEAR"]);
							control.StartDate = DataBaseValue.ConvObjectToDateNullByDate(row["START_DATE"]);
							control.EndDate = DataBaseValue.ConvObjectToDateNullByDate(row["END_DATE"]);
							control.PeriodEndDate = DataBaseValue.ConvObjectToDateNullByDate(row["PERIOD_END_DATE"]);
							control.BranchID = row["BRANCH_ID"].ToString().Trim();
							control.BranchName = row["BRANCH_NAME"].ToString().Trim();
							control.SalesmanID = row["SALESMAN_ID"].ToString().Trim();
							control.SalesmanName = row["SALESMAN_NAME"].ToString().Trim();
							control.OrderDate = DataBaseValue.ConvObjectToDateNullByDate(row["ORDER_DATE"]);
							control.OrderReportAccept = ("1" == row["ORDER_REPORT_ACCEPT"].ToString()) ? true : false;
							control.OrderApprovalDate = DataBaseValue.ConvObjectToDateNullByDate(row["ORDER_APPROVAL_DATE"]);
							control.MailAddress = row["MAIL_ADDRESS"].ToString().Trim();
							control.Remark = row["REMARK"].ToString().Trim();
							control.StartMailDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["START_MAIL_DATE"]);
							control.GuideMailDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["GUIDE_MAIL_DATE"]);
							control.UpdateMailDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["UPDATE_MAIL_DATE"]);
							control.CancelDate = DataBaseValue.ConvObjectToDateNullByDate(row["CANCEL_DATE"]);
							control.CancelReportAccept = ("1" == row["CANCEL_REPORT_ACCEPT"].ToString()) ? true : false;
							control.CancelReason = row["CANCEL_REASON"].ToString().Trim();
							control.DisableFlag = ("1" == row["DISABLE_FLAG"].ToString()) ? true : false;
							control.CreateDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["CREATE_DATE"]);
							control.CreatePerson = row["CREATE_PERSON"].ToString().Trim();
							control.UpdateDateTime = DataBaseValue.ConvObjectToDateTimeNull(row["UPDATE_DATE"]);
							control.UpdatePerson = row["UPDATE_PERSON"].ToString().Trim();
							result.Add(control);
						}
					}
					return result;
				}

				/// <summary>
				/// 顧客メールアドレスの取得
				/// </summary>
				/// <param name="table">DataTable</param>
				/// <returns>顧客メールアドレス</returns>
				public static List<Tuple<int, string>> ConvertCustomerMailAddress(DataTable table)
				{
					List<Tuple<int, string>> result = null;
					if (null != table)
					{
						result = new List<Tuple<int, string>>();
						foreach (DataRow row in table.Rows)
						{
							int customerID = DataBaseValue.ConvObjectToInt(row["顧客ＩＤ"]);
							string mailAddress = row["メールアドレス"].ToString().Trim();
							result.Add(new Tuple<int, string>(customerID, mailAddress));
						}
					}
					return result;
				}

				/// <summary>
				/// 拠店情報の取得
				/// </summary>
				/// <param name="table">DataTable</param>
				/// <returns>拠店情報</returns>
				public static List<BranchInfo> ConvertBranchInfo(DataTable table)
				{
					List<BranchInfo> result = null;
					if (null != table)
					{
						result = new List<BranchInfo>();
						foreach (DataRow row in table.Rows)
						{
							BranchInfo branch = new BranchInfo();
							branch.BranchID = row["支店ＩＤ"].ToString();
							branch.BranchName = row["支店名"].ToString();
							branch.MailAddress = row["支店メールアドレス"].ToString().Trim();
							result.Add(branch);
						}
					}
					return result;
				}
		*/
	}
}
