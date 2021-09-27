//
// SQLiteMwsSimulationAccess.cs
// 
// MIC WEB SERVICE 課金シミュレーション SQLiteデータベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
// Ver2.100 おまとめプラン48ヵ月、60ヵ月に対応(2019/01/22 勝呂)
//
using CommonLib.BaseFactory.MwsSimulation;
using CommonLib.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.DB.SQLite.MwsSimulation
{
	public static class SQLiteMwsSimulationAccess
	{
		/////////////////////////////////////////////
		// マスター情報関連

		/// <summary>
		/// バージョン情報の取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>バージョン情報</returns>
		public static Tuple<int, Date> GetVerionInfo(string dbPath)
		{
			DataTable table = SQLiteMwsSimulationGetIO.GetVersionInfo(dbPath);
			return SQLiteMwsSimulationController.ConvertVersionInfo(table);
		}

		/// <summary>
		/// バージョン情報の設定
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="version">バージョン</param>
		/// <param name="updateDate">更新日</param>
		/// <returns>判定</returns>
		public static int SetVersionInfo(string dbPath, int version, Date updateDate)
		{
			return SQLiteMwsSimulationSetIO.SetVersionInfo(dbPath, version, updateDate);
		}

		/// <summary>
		/// 指定日の消費税率の取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="targetDate">指定日</param>
		/// <returns>消費税率</returns>
		public static short GetTaxRate(string dbPath, Date targetDate)
		{
			DataTable table = SQLiteMwsSimulationGetIO.GetTaxRate(dbPath);
			return SQLiteMwsSimulationController.ConvertTaxRate(table, targetDate);
		}

		/// <summary>
		/// サービス情報リストの取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>サービス情報リスト</returns>
		public static ServiceInfoList GetServiceInfo(string dbPath)
		{
			DataTable table = SQLiteMwsSimulationGetIO.GetServiceInfo(dbPath);
			return SQLiteMwsSimulationController.ConvertServiceInfoList(table);
		}

		/// <summary>
		/// おまとめプラン情報リストの取得（旧版）
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="readType">読込種別</param>
		/// <returns>おススメセット情報リスト</returns>
		// Ver2.100 おまとめプラン48ヵ月、60ヵ月に対応(2019/01/22 勝呂)
		public static GroupPlanList GetGroupPlanList(string dbPath, int readType)
		{
			DataTable table = SQLiteMwsSimulationGetIO.GetGroupPlan(dbPath, readType);
			return SQLiteMwsSimulationController.ConvertGroupPlan(table);
		}

		/// <summary>
		/// おススメセット情報リストの取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>おススメセット情報リスト</returns>
		public static List<InitGroupPlan> GetInitGroupPlan(string dbPath)
		{
			DataTable table = SQLiteMwsSimulationGetIO.GetInitGroupPlan(dbPath);
			List<InitGroupPlan> result = SQLiteMwsSimulationController.ConvertInitGroupPlan(table);
			foreach (InitGroupPlan group in result)
			{
				table = SQLiteMwsSimulationGetIO.GetInitGroupPlanElement(dbPath, group.GroupID);
				group.ServiceCodeList = SQLiteMwsSimulationController.ConvertInitGroupPlanElement(table);
			}
			return result;
		}

		/// <summary>
		/// セット割サービス情報リストの取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>セット割サービス情報リスト</returns>
		public static List<SetPlan> GetSetPlanList(string dbPath)
		{
			DataTable table = SQLiteMwsSimulationGetIO.GetSetPlanHeader(dbPath);
			List<SetPlan> result = SQLiteMwsSimulationController.ConvertSetPlanHeader(table);
			foreach (SetPlan plan in result)
			{
				table = SQLiteMwsSimulationGetIO.GetSetPlanElement(dbPath, plan.GoodsID);
				plan.ServiceList = SQLiteMwsSimulationController.ConvertSetPlanElement(table);
			}
			return result;
		}

		/// <summary>
		/// サービス情報の削除
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>判定</returns>
		public static int DeleteAllServiceInfo(string dbPath)
		{
			return SQLiteMwsSimulationSetIO.DeleteAllServiceInfo(dbPath);
		}

		/// <summary>
		/// おまとめプラン情報の削除
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>判定</returns>
		public static int DeleteAllGroupPlan(string dbPath)
		{
			return SQLiteMwsSimulationSetIO.DeleteAllGroupPlan(dbPath);
		}

		/// <summary>
		/// おススメセット情報の削除
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>判定</returns>
		public static int DeleteAllInitGroupPlan(string dbPath)
		{
			return SQLiteMwsSimulationSetIO.DeleteAllInitGroupPlan(dbPath);
		}

		/// <summary>
		/// セット割サービス情報の削除
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>判定</returns>
		public static int DeleteAllSetPlan(string dbPath)
		{
			return SQLiteMwsSimulationSetIO.DeleteAllSetPlan(dbPath);
		}


		/////////////////////////////////////////////
		// ユーザー情報関連

		/// <summary>
		/// 次回見積書情報番号の取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>次回見積書情報番号</returns>
		public static int GetLastEstimateNumber(string dbPath)
		{
			return SQLiteMwsSimulationGetIO.GetLastEstimateNumber(dbPath);
		}

		/// <summary>
		/// 見積書宛先リストの取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>見積書名称リスト</returns>
		public static List<Tuple<int, string>> GetEstimateDestinationList(string dbPath)
		{
			DataTable table = SQLiteMwsSimulationGetIO.GetEstimateDestinationList(dbPath);
			return SQLiteMwsSimulationController.ConvertEstimateDestinationList(table);
		}

		/// <summary>
		/// 見積書情報リストの取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>見積書情報リスト</returns>
		public static List<Estimate> GetEstimateList(string dbPath)
		{
			DataTable table = SQLiteMwsSimulationGetIO.GetEstimateHeader(dbPath);
			//if (9 == table.Columns.Count)
			//{
			//	// 旧バージョンのテーブル構造なのでフィールドを追加
			//	SQLiteMwsSimulationSetIO.AlterTableEstimateHeaderAgreeEndDataAndNotUsedMessrs(dbPath);
			//	table = SQLiteMwsSimulationGetIO.GetEstimateHeader(dbPath);
			//}
			List<Estimate> result = SQLiteMwsSimulationController.ConvertEstimateHeader(table);
			foreach (Estimate est in result)
			{
				table = SQLiteMwsSimulationGetIO.GetEstimateService(dbPath, est.EstimateID);
				est.ServiceList = SQLiteMwsSimulationController.ConvertEstimateService(table);
				foreach (EstimateService service in est.ServiceList)
				{
					if (SQLiteMwsSimulationDef.ServiceMode.None != service.Mode)
					{
						table = SQLiteMwsSimulationGetIO.GetEstimateGroupElement(dbPath, est.EstimateID, service.GoodsID);
						service.GroupServiceList = SQLiteMwsSimulationController.ConvertEstimateGroupElement(table);
					}
				}
			}
			return result;
		}

		/// <summary>
		/// 見積書情報の宛先変更
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="estimateID">見積書番号</param>
		/// <param name="destination">宛先</param>
		/// <param name="notUsedMessrs">様</param>
		/// <returns>判定</returns>
		public static int UpdateEstimateHeaderDestination(string dbPath, int estimateID, string destination, int notUsedMessrs)
		{
			return SQLiteMwsSimulationSetIO.UpdateEstimateHeaderDestination(dbPath, estimateID, destination, notUsedMessrs);
		}

		/// <summary>
		/// 見積書情報の更新
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="est">見積書情報</param>
		/// <returns>判定</returns>
		public static int UpdateEstimate(string dbPath, Estimate est)
		{
			return SQLiteMwsSimulationSetIO.UpdateEstimate(dbPath, est);
		}

		/// <summary>
		/// 見積書情報の削除
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="estimateID">見積書番号</param>
		/// <returns>判定</returns>
		public static int DeleteEstimate(string dbPath, int estimateID)
		{
			return SQLiteMwsSimulationSetIO.DeleteEstimate(dbPath, estimateID);
		}

		/// <summary>
		/// サービス情報リストの更新
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="list">サービス情報リスト</param>
		/// <returns>判定</returns>
		public static int UpdateServiceInfoList(string dbPath, ServiceInfoList list)
		{
			return SQLiteMwsSimulationSetIO.UpdateServiceInfoList(dbPath, list);
		}

		/// <summary>
		/// おまとめプラン情報リストの更新
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="list">おまとめプラン情報リスト</param>
		/// <returns>判定</returns>
		public static int UpdateGroupPlanList(string dbPath, GroupPlanList list)
		{
			return SQLiteMwsSimulationSetIO.UpdateGroupPlanList(dbPath, list);
		}

		/// <summary>
		/// おススメセット情報リストの更新
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="list">おススメセット情報リスト</param>
		/// <returns>判定</returns>
		public static int UpdateInitGroupPlanList(string dbPath, List<InitGroupPlan> list)
		{
			return SQLiteMwsSimulationSetIO.UpdateInitGroupPlanList(dbPath, list);
		}

		/// <summary>
		/// セット割サービス情報リストの更新
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="list">おススメセット情報リスト</param>
		/// <returns>判定</returns>
		public static int UpdateSetPlanList(string dbPath, List<SetPlan> list)
		{
			return SQLiteMwsSimulationSetIO.UpdateSetPlanList(dbPath, list);
		}
	}
}
