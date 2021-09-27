//
// MwsSimulationAccess.cs
// 
// MIC WEB SERVICE 課金シミュレーション SQL SERVERデータベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
//
using CommonLib.BaseFactory.MwsSimulation;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.DB.SqlServer.MwsSimulation
{
	public class MwsSimulationAccess
	{
		/////////////////////////////////////////////
		// マスター情報関連

		/// <summary>
		/// サービス情報リストの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>サービス情報リスト</returns>
		public static ServiceInfoList GetServiceInfo(string connectStr)
		{
			DataTable table = MwsSimulationGetIO.GetServiceInfo(connectStr);
			return MwsSimulationController.ConvertServiceInfoList(table);
		}

		/// <summary>
		/// おススメセット情報リストの取得
		/// </summary>
		/// <returns>おススメセット情報リスト</returns>
		public static List<InitGroupPlan> GetInitGroupPlan(string connectStr)
		{
			DataTable table = MwsSimulationGetIO.GetInitGroupPlan(connectStr);
			return MwsSimulationController.ConvertInitGroupPlan(table);
		}

		/// <summary>
		/// おまとめプラン情報リストの取得
		/// </summary>
		/// <returns>おススメセット情報リスト</returns>
		public static GroupPlanList GetGroupPlanList(string connectStr)
		{
			DataTable table = MwsSimulationGetIO.GetGroupPlan(connectStr);
			return MwsSimulationController.ConvertGroupPlan(table);
		}

		/// <summary>
		/// セット割サービス情報リストの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>セット割サービス情報リスト</returns>
		public static List<SetPlan> GetSetPlanList(string connectStr)
		{
			DataTable table = MwsSimulationGetIO.GetSetPlan(connectStr);
			List<SetPlan> result = MwsSimulationController.ConvertSetPlan(table);
			table = MwsSimulationGetIO.GetSetPlanElement(connectStr);
			return MwsSimulationController.ConvertSetPlanElement(result, table);
		}
	}
}
