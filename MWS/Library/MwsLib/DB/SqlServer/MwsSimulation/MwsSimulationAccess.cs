//
// MwsSimulationAccess.cs
// 
// MIC WEB SERVICE 課金シミュレーション SQL SERVERデータベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
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
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>サービス情報リスト</returns>
		public static ServiceInfoList GetServiceInfo(bool sqlsv2 = false)
		{
			DataTable table = MwsSimulationGetIO.GetServiceInfo(sqlsv2);
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
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>セット割サービス情報リスト</returns>
		public static List<SetPlan> GetSetPlanList(bool sqlsv2 = false)
		{
			DataTable table = MwsSimulationGetIO.GetSetPlan(sqlsv2);
			List<SetPlan> result = MwsSimulationController.ConvertSetPlan(table);
			table = MwsSimulationGetIO.GetSetPlanElement(sqlsv2);
			return MwsSimulationController.ConvertSetPlanElement(result, table);
		}
	}
}
