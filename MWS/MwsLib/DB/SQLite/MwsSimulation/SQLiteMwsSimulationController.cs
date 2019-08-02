//
// SQLiteMwsSimulationController.cs
// 
// MIC WEB SERVICE 課金シミュレーション SQLiteデータベース詰め替えクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
// Ver2.101 消費税率の取得をMwsSimulationMaster.dbから[JunpDB].[dbo].[vMicPCA消費税率]に変更(2019/07/19 勝呂)
//
using MwsLib.BaseFactory.MwsSimulation;
using MwsLib.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SQLite.MwsSimulation
{
	public static class SQLiteMwsSimulationController
	{
		/// <summary>
		/// バージョン情報の詰め替え
		/// VERSION_INFO
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>バージョン情報</returns>
		public static Tuple<int, Date> ConvertVersionInfo(DataTable table)
		{
			Tuple<int, Date> result = null;
			if (null != table)
			{
				if (0 < table.Rows.Count)
				{
					int version = DataBaseValue.ConvObjectToInt(table.Rows[0]["DataVersion"]);
					Date date = DataBaseValue.ConvObjectToDate(table.Rows[0]["UpdateDate"]);
					result = new Tuple<int, Date>(version, date);
				}
			}
			return result;
		}

		// Ver2.101 消費税率の取得をMwsSimulationMaster.dbから[JunpDB].[dbo].[vMicPCA消費税率]に変更(2019/07/19 勝呂)
		///// <summary>
		///// 指定日の消費税率の取得
		///// TAX_RATE
		///// </summary>
		///// <param name="table">データテーブル</param>
		///// <param name="targetDate">指定日</param>
		///// <returns>消費税率</returns>
		//public static int ConvertTaxRate(DataTable table, Date targetDate)
		//{
		//	ServiceInfoList result = null;
		//	if (null != table)
		//	{
		//		result = new ServiceInfoList();
		//		foreach (DataRow row in table.Rows)
		//		{
		//			Date start = DataBaseValue.ConvObjectToDate(row["StartDate"]);
		//			Date end = DataBaseValue.ConvObjectToDate(row["EndDate"]);
		//			Span span = new Span(start, end);
		//			if (span.IsInside(targetDate))
		//			{
		//				return DataBaseValue.ConvObjectToInt(row["TaxRate"]);
		//			}
		//		}
		//	}
		//	return 0;
		//}

		/// <summary>
		/// サービス情報リストの詰め替え
		/// SERVICE_INFO
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>サービス情報リスト</returns>
		public static ServiceInfoList ConvertServiceInfoList(DataTable table)
		{
			ServiceInfoList result = null;
			if (null != table)
			{
				result = new ServiceInfoList();
				foreach (DataRow row in table.Rows)
				{
					ServiceInfo service = new ServiceInfo();
					service.ServiceCode = row["ServiceCode"].ToString();
					service.ServiceName = row["ServiceName"].ToString();
					service.ParentServiceCode = row["ParentServiceCode"].ToString();
					service.ServiceType = DataBaseValue.ConvObjectToInt(row["ServiceType"]);
					service.ServiceTypeName = row["ServiceTypeName"].ToString();
					service.Price = DataBaseValue.ConvObjectToInt(row["Price"]);
					service.GoodsID = row["GoodsID"].ToString();
					service.GoodsName = row["GoodsName"].ToString();
					service.GoodsKubun = DataBaseValue.ConvObjectToInt(row["GoodsKubun"]);
					if (SQLiteMwsSimulationDef.MWS_STANDARD_GOODSID == service.GoodsID)
					{
						result.Platform = service;
					}
					else
					{
						result.Add(service);
					}
				}
			}
			return result;
		}

		/// <summary>
		/// 見積書宛先リストの詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>見積書名称リスト</returns>
		public static List<Tuple<int, string>> ConvertEstimateDestinationList(DataTable table)
		{
			List<Tuple<int, string>> result = null;
			if (null != table)
			{
				result = new List<Tuple<int, string>>();
				foreach (DataRow row in table.Rows)
				{
					int id = DataBaseValue.ConvObjectToInt(row["EstimateID"]);
					string destinaion = row["Destination"].ToString();
					result.Add(new Tuple<int, string>(id, destinaion));
				}
			}
			return result;
		}

		/// <summary>
		/// 見積書ヘッダ情報リストの詰め替え
		/// ESTIMATE_HEADER
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>見積書ヘッダ情報リスト</returns>
		public static List<Estimate> ConvertEstimateHeader(DataTable table)
		{
			List<Estimate> result = null;
			if (null != table)
			{
				result = new List<Estimate>();
				foreach (DataRow row in table.Rows)
				{
					Estimate est = new Estimate();
					est.EstimateID = DataBaseValue.ConvObjectToInt(row["EstimateID"]);
					est.Destination = row["Destination"].ToString();
					est.PrintDate = DataBaseValue.ConvObjectToDate(row["PrintDate"]);
					Date startDate = DataBaseValue.ConvObjectToDate(row["AgreeStartDate"]);
					Date endDate = DataBaseValue.ConvObjectToDate(row["AgreeEndDate"]);
					est.AgreeSpan = new Span(startDate, endDate);
					est.AgreeMonthes = DataBaseValue.ConvObjectToInt(row["AgreeMonthes"]);
					est.LimitDate = DataBaseValue.ConvObjectToDate(row["LimitDate"]);
					est.Remark.Add(row["Remark1"].ToString());
					est.Remark.Add(row["Remark2"].ToString());
					est.Remark.Add(row["Remark3"].ToString());
					est.Remark.Add(row["Remark4"].ToString());
					est.NotUsedMessrs = DataBaseValue.ConvObjectToInt(row["NotUsedMessrs"]);
					est.Apply = (Estimate.ApplyType)DataBaseValue.ConvObjectToInt(row["ApplyType"]);
					result.Add(est);
				}
			}
			return result;
		}

		/// <summary>
		/// 見積書サービス情報リストの詰め替え
		/// ESTIMATE_SERVICE
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>見積書サービス情報リスト</returns>
		public static List<EstimateService> ConvertEstimateService(DataTable table)
		{
			List<EstimateService> result = null;
			if (null != table)
			{
				result = new List<EstimateService>();
				foreach (DataRow row in table.Rows)
				{
					EstimateService service = new EstimateService();
					service.GoodsID = row["GoodsID"].ToString();
					service.ServiceName = row["ServiceName"].ToString();
					service.Price = DataBaseValue.ConvObjectToInt(row["Price"]);
					service.Mode = (SQLiteMwsSimulationDef.ServiceMode)DataBaseValue.ConvObjectToInt(row["Mode"]);
					result.Add(service);
				}
			}
			return result;
		}

		/// <summary>
		/// 見積書サービス情報リストの詰め替え
		/// ESTIMATE_GROUP_ELEMENT
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>見積書サービス情報リスト</returns>
		public static List<Tuple<string, string>> ConvertEstimateGroupElement(DataTable table)
		{
			List<Tuple<string, string>> result = null;
			if (null != table)
			{
				result = new List<Tuple<string, string>>();
				foreach (DataRow row in table.Rows)
				{
					string goodsID = row["GoodsID"].ToString();
					string serviceName = row["ServiceName"].ToString();
					result.Add(new Tuple<string, string>(goodsID, serviceName));
				}
			}
			return result;
		}

		/// <summary>
		/// おススメセット情報の詰め替え
		/// INIT_GROUP_PLAN
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>おススメセット情報リスト</returns>
		public static List<InitGroupPlan> ConvertInitGroupPlan(DataTable table)
		{
			List<InitGroupPlan> result = null;
			if (null != table)
			{
				result = new List<InitGroupPlan>();
				foreach (DataRow row in table.Rows)
				{
					InitGroupPlan group = new InitGroupPlan();
					group.GroupID = DataBaseValue.ConvObjectToInt(row["GroupID"]);
					group.GroupName = row["GroupName"].ToString();
					result.Add(group);
				}
			}
			return result;
		}

		/// <summary>
		/// おススメセットサービス情報の詰め替え
		/// INIT_GROUP_PLAN_ELEMENT
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>サービス情報リスト</returns>
		public static List<string> ConvertInitGroupPlanElement(DataTable table)
		{
			List<string> result = null;
			if (null != table)
			{
				result = new List<string>();
				foreach (DataRow row in table.Rows)
				{
					result.Add(row["ServiceCode"].ToString());
				}
			}
			return result;
		}

		/// <summary>
		/// おまとめプラン情報の詰め替え
		/// GROUP_PLAN
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>おまとめプラン情報リスト</returns>
		public static GroupPlanList ConvertGroupPlan(DataTable table)
		{
			GroupPlanList result = null;
			if (null != table)
			{
				result = new GroupPlanList();
				foreach (DataRow row in table.Rows)
				{
					GroupPlan plan = new GroupPlan();
					plan.GoodsID = row["GoodsID"].ToString();
					plan.GoodsName = row["GoodsName"].ToString();
					plan.KeiyakuMonth = DataBaseValue.ConvObjectToInt(row["KeiyakuMonth"]);
					plan.FreeMonth = DataBaseValue.ConvObjectToInt(row["FreeMonth"]);
					plan.MinAmmount = DataBaseValue.ConvObjectToInt(row["MinAmmount"]);
					plan.MaxAmmount = DataBaseValue.ConvObjectToInt(row["MaxAmmount"]);
					if (0 == plan.MaxAmmount)
					{
						plan.MaxAmmount = null;
					}
					result.Add(plan);
				}
			}
			return result;
		}

		/// <summary>
		/// セット割サービス情報の詰め替え
		/// SET_PLAN_HEADER
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>セット割サービス情報リスト</returns>
		public static List<SetPlan> ConvertSetPlanHeader(DataTable table)
		{
			List<SetPlan> result = null;
			if (null != table)
			{
				result = new List<SetPlan>();
				foreach (DataRow row in table.Rows)
				{
					SetPlan plan = new SetPlan();
					plan.GoodsID = row["GoodsID"].ToString();
					plan.GoodsName = row["GoodsName"].ToString();
					plan.Price = DataBaseValue.ConvObjectToInt(row["Price"]);
					result.Add(plan);
				}
			}
			return result;
		}

		/// <summary>
		/// セット割サービス情報の詰め替え
		/// SET_PLAN_ELEMENT
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>セット割サービス情報リスト</returns>
		public static List<Tuple<string, string>> ConvertSetPlanElement(DataTable table)
		{
			List<Tuple<string, string>> result = null;
			if (null != table)
			{
				result = new List<Tuple<string, string>>();
				foreach (DataRow row in table.Rows)
				{
					string goodsID = row["GoodsID"].ToString();
					string serviceName = row["ServiceName"].ToString();
					result.Add(new Tuple<string, string>(goodsID, serviceName));
				}
			}
			return result;
		}
	}
}
