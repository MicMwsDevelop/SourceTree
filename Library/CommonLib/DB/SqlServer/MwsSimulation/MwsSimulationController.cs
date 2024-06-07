//
// MwsSimulationController.cs
// 
// MIC WEB SERVICE 課金シミュレーション SQL SERVERデータベース詰め替えクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
//
using CommonLib.BaseFactory.MwsSimulation;
using CommonLib.DB.SQLite.MwsSimulation;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.DB.SqlServer.MwsSimulation
{
	public class MwsSimulationController
	{
		/////////////////////////////////////////////
		// マスター情報関連

		/// <summary>
		/// サービス情報リストの詰め替え
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
					service.ServiceCode = DataBaseValue.ConvObjectToInt(row["ServiceCode"]);
					service.ServiceName = row["ServiceName"].ToString();
					service.ParentServiceCode = DataBaseValue.ConvObjectToInt(row["ParentServiceCode"]);
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
		/// セット割サービス情報の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>セット割サービス情報リスト</returns>
		public static List<SetPlan> ConvertSetPlan(DataTable table)
		{
			List<SetPlan> result = null;
			if (null != table)
			{
				result = new List<SetPlan>();
				foreach (DataRow row in table.Rows)
				{
					string goodsID = row["GoodsID"].ToString();
					SetPlan plan = result.Find(p => p.GoodsID == goodsID);
					if (null == plan)
					{
						plan = new SetPlan();
						plan.GoodsID = goodsID;
						plan.GoodsName = row["GoodsName"].ToString();
						plan.Price = DataBaseValue.ConvObjectToInt(row["Price"]);
						plan.ServiceList = new List<Tuple<string, string>>();
						result.Add(plan);
					}
					string serviceCode = row["ServiceCode"].ToString();
					string serviceName = row["ServiceName"].ToString();
					plan.ServiceList.Add(new Tuple<string, string>(serviceCode, serviceName));
				}
			}
			return result;
		}

		/// <summary>
		/// セット割サービス情報の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>セット割サービス情報リスト</returns>
		public static List<SetPlan> ConvertSetPlanElement(List<SetPlan> result, DataTable table)
		{
			if (null != result && null != table)
			{
				List<Tuple<string, string>> elementList = new List<Tuple<string, string>>();
				foreach (DataRow row in table.Rows)
				{
					string serviceCode = row["ServiceCode"].ToString();
					string goodsID = row["GoodsID"].ToString();
					elementList.Add(new Tuple<string, string>(serviceCode, goodsID));
				}
				foreach (SetPlan plan in result)
				{
					for (int i = 0; i < plan.ServiceList.Count; i++)
					{
						Tuple<string, string> element = elementList.Find(p => p.Item1 == plan.ServiceList[i].Item1);
						if (null != element)
						{
							// ServiceCode → GoodsID に変換
							plan.ServiceList[i] = new Tuple<string, string>(element.Item2, plan.ServiceList[i].Item2);
						}
					}
				}
			}
			return result;
		}

		/// <summary>
		/// おまとめプラン情報の詰め替え
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
		/// おススメセット情報の詰め替え
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
					int groupID = DataBaseValue.ConvObjectToInt(row["GroupID"]);
					InitGroupPlan group = result.Find(p => p.GroupID == groupID);
					if (null == group)
					{
						group = new InitGroupPlan();
						group.GroupID = groupID;
						group.GroupName = row["GroupName"].ToString();
						result.Add(group);
					}
					group.ServiceCodeList.Add(DataBaseValue.ConvObjectToInt(row["ServiceCode"]));
				}
			}
			return result;
		}
	}
}
