//
// MwsSimulationAccess.cs
// 
// MIC WEB SERVICE 課金シミュレーション SQL SERVERデータベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using MwsLib.BaseFactory.MwsSimulation;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.MwsSimulation
{
	public class MwsSimulationAccess
	{
		/////////////////////////////////////////////
		// マスター情報関連

		/// <summary>
		/// サービス情報リストの取得
		/// </summary>
		/// <returns>サービス情報リスト</returns>
		public static ServiceInfoList GetServiceInfo()
		{
			DataTable table = MwsSimulationGetIO.GetServiceInfo();
			return MwsSimulationController.ConvertServiceInfoList(table);
		}

		/// <summary>
		/// おススメセット情報リストの取得
		/// </summary>
		/// <returns>おススメセット情報リスト</returns>
		public static List<InitGroupPlan> GetInitGroupPlan()
		{
			DataTable table = MwsSimulationGetIO.GetInitGroupPlan();
			return MwsSimulationController.ConvertInitGroupPlan(table);
		}

		/// <summary>
		/// おまとめプラン情報リストの取得
		/// </summary>
		/// <returns>おススメセット情報リスト</returns>
		public static GroupPlanList GetGroupPlanList()
		{
			DataTable table = MwsSimulationGetIO.GetGroupPlan();
			return MwsSimulationController.ConvertGroupPlan(table);
		}

		/// <summary>
		/// セット割サービス情報リストの取得
		/// </summary>
		/// <returns>セット割サービス情報リスト</returns>
		public static List<SetPlan> GetSetPlanList()
		{
			DataTable table = MwsSimulationGetIO.GetSetPlan();
			List<SetPlan> result = MwsSimulationController.ConvertSetPlan(table);
			table = MwsSimulationGetIO.GetSetPlanElement();
			return MwsSimulationController.ConvertSetPlanElement(result, table);
		}
	}
}
