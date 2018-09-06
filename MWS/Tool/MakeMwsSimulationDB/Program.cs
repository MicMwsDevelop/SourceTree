//
// Program.cs
// 
// MWS課金シミュレーションデータファイル更新コンソールアプリケーション
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using MwsLib.BaseFactory.MwsSimulation;
using MwsLib.Common;
using MwsLib.DB.SQLite.MwsSimulation;
using MwsLib.DB.SqlServer.MwsSimulation;
using MwsLib.Log;
using System;
using System.Collections.Generic;

namespace MakeMwsSimulationDB
{
	class Program
	{
		/// <summary>
		/// サーバーデータフォルダ
		/// </summary>
#if DEBUG
		private const string SERVER_DATA_FOLDER = @"C:\_AAA\MakeMwsSimulationDB";
#else
		private const string SERVER_DATA_FOLDER = @"\\storage\公開データ\サポート課公開用\02_Tools類\その他\MwsSimulation";
#endif

		static void Main(string[] args)
		{
			// メンテナンス開始ログ出力
			MainteLogger.MainLine("MWS課金シミュレーションデータファイル更新：開始");

			// サービス情報
			ServiceInfoList oldServiceInfoList = null;
			ServiceInfoList newServiceInfoList = null;
			try
			{
				oldServiceInfoList = SQLiteMwsSimulationAccess.GetServiceInfo(SERVER_DATA_FOLDER);
				newServiceInfoList = MwsSimulationAccess.GetServiceInfo();

				// 下記のサービスは削除
				// 1030120 ３ＤｅｎｔＭＯＶＩＥ
				// 2510100 Office365 Small Business Premium
				// 2510101 Office365 Small Business Premium 2ﾗｲｾﾝｽ
				// 2510103 Office365 BizPremium 3
				// 2510120 SmaBiz!Office365 ProPLus
				// 2510140 Office365 Business
				// 2520100 Office365 Small Biz Premium
				// 2520120 Office365 Business
				// 2520140 Office365 Business 2L
				// 3510100 達人プラスVersion5 月額版
				// 4510100 プロセシアVersion2Web版
				// 5410120 クラウドバックアップサービス 10GB
				newServiceInfoList.RemoveAll(p => 
					(p.ServiceCode == "1030120" || p.ServiceCode == "2510100" || p.ServiceCode == "2510101" || p.ServiceCode == "2510103"
					|| p.ServiceCode == "2510120" || p.ServiceCode == "2510140" || p.ServiceCode == "2520100" || p.ServiceCode == "2520120"
					|| p.ServiceCode == "2520140" || p.ServiceCode == "3510100" || p.ServiceCode == "4510100" || p.ServiceCode == "5410120"));
			}
			catch (Exception ex)
			{
				MainteLogger.SubLine(string.Format("サービス情報読込エラー：{0}", ex.Message));
				MainteLogger.MainLine("MWS課金シミュレーションデータファイル更新：異常終了");
				return;
			}
			// おススメセット情報
			List<InitGroupPlan> oldInitGroupPlanList = null;
			List<InitGroupPlan> newInitGroupPlanList = null;
			try
			{
				oldInitGroupPlanList = SQLiteMwsSimulationAccess.GetInitGroupPlan(SERVER_DATA_FOLDER);
				newInitGroupPlanList = MwsSimulationAccess.GetInitGroupPlan();
			}
			catch (Exception ex)
			{
				MainteLogger.SubLine(string.Format("おススメセット情報読込エラー：{0}", ex.Message));
				MainteLogger.MainLine("MWS課金シミュレーションデータファイル更新：異常終了");
				return;
			}
			// おまとめプラン情報
			GroupPlanList oldGroupPlanList = null;
			GroupPlanList newGroupPlanList = null;
			try
			{
				oldGroupPlanList = SQLiteMwsSimulationAccess.GetGroupPlanList(SERVER_DATA_FOLDER);
				newGroupPlanList = MwsSimulationAccess.GetGroupPlanList();
			}
			catch (Exception ex)
			{
				MainteLogger.SubLine(string.Format("おまとめプラン情報読込エラー：{0}", ex.Message));
				MainteLogger.MainLine("MWS課金シミュレーションデータファイル更新：異常終了");
				return;
			}
			// セット割サービス情報
			List<SetPlan> oldSetPlanList = null;
			List<SetPlan> newSetPlanList = null;
			try
			{
				oldSetPlanList = SQLiteMwsSimulationAccess.GetSetPlanList(SERVER_DATA_FOLDER);
				newSetPlanList = MwsSimulationAccess.GetSetPlanList();
			}
			catch (Exception ex)
			{
				MainteLogger.SubLine(string.Format("セット割サービス情報読込エラー：{0}", ex.Message));
				MainteLogger.MainLine("MWS課金シミュレーションデータファイル更新：異常終了");
				return;
			}
			// サービス情報の比較
			bool serviceInfoDef = false;
			if (null != oldServiceInfoList && null != newServiceInfoList)
			{
				if (false == oldServiceInfoList.Equals(newServiceInfoList))
				{
					serviceInfoDef = true;
				}
			}
			else
			{
				serviceInfoDef = true;
			}
			// おススメセット情報の比較
			bool initGroupPlanDef = false;
			if (null != oldInitGroupPlanList && null != newInitGroupPlanList)
			{
				if (oldInitGroupPlanList.Count == newInitGroupPlanList.Count)
				{
					for (int i = 0; i < oldInitGroupPlanList.Count; i++)
					{
						if (false == oldInitGroupPlanList[i].Equals(newInitGroupPlanList[i]))
						{
							initGroupPlanDef = true;
							break;
						}
					}
				}
				else
				{
					initGroupPlanDef = true;
				}
			}
			else
			{
				initGroupPlanDef = true;
			}
			// おまとめプラン情報の比較
			bool groupPlanDef = false;
			if (null != oldGroupPlanList && null != newGroupPlanList)
			{
				if (false == oldGroupPlanList.Equals(newGroupPlanList))
				{
					groupPlanDef = true;
				}
			}
			else
			{
				groupPlanDef = true;
			}
			// セット割サービス情報の比較
			bool setPlanDef = false;
			if (null != oldSetPlanList && null != newSetPlanList)
			{
				if (oldSetPlanList.Count == newSetPlanList.Count)
				{
					for (int i = 0; i < oldSetPlanList.Count; i++)
					{
						if (false == oldSetPlanList[i].Equals(newSetPlanList[i]))
						{
							setPlanDef = true;
							break;
						}
					}
				}
				else
				{
					setPlanDef = true;
				}
			}
			else
			{
				setPlanDef = true;
			}
			if (serviceInfoDef)
			{
				try
				{
					// サービス情報の再作成
					SQLiteMwsSimulationAccess.UpdateServiceInfoList(SERVER_DATA_FOLDER, newServiceInfoList);
					MainteLogger.SubLine("サービス情報更新");
				}
				catch (Exception ex)
				{
					MainteLogger.SubLine(string.Format("サービス情報更新エラー：{0}", ex.Message));
					MainteLogger.MainLine("MWS課金シミュレーションデータファイル更新：異常終了");
					return;
				}
			}
			if (initGroupPlanDef)
			{
				try
				{
					// おススメセット情報の再作成
					SQLiteMwsSimulationAccess.UpdateInitGroupPlanList(SERVER_DATA_FOLDER, newInitGroupPlanList);
					MainteLogger.SubLine("おススメセット情報更新");
				}
				catch (Exception ex)
				{
					MainteLogger.SubLine(string.Format("おススメセット情報更新エラー：{0}", ex.Message));
					MainteLogger.MainLine("MWS課金シミュレーションデータファイル更新：異常終了");
					return;
				}
			}
			if (groupPlanDef)
			{
				try
				{
					// おまとめプラン情報の再作成
					SQLiteMwsSimulationAccess.UpdateGroupPlanList(SERVER_DATA_FOLDER, newGroupPlanList);
					MainteLogger.SubLine("おまとめプラン情報更新");
				}
				catch (Exception ex)
				{
					MainteLogger.SubLine(string.Format("おまとめプラン情報更新エラー：{0}", ex.Message));
					MainteLogger.MainLine("MWS課金シミュレーションデータファイル更新：異常終了");
					return;
				}
			}
			if (setPlanDef)
			{
				try
				{
					// セット割サービス情報の再作成
					SQLiteMwsSimulationAccess.UpdateSetPlanList(SERVER_DATA_FOLDER, newSetPlanList);
					MainteLogger.SubLine("セット割サービス情報更新");
				}
				catch (Exception ex)
				{
					MainteLogger.SubLine(string.Format("セット割サービス情報更新エラー：{0}", ex.Message));
					MainteLogger.MainLine("MWS課金シミュレーションデータファイル更新：異常終了");
					return;
				}
			}
			if (serviceInfoDef || initGroupPlanDef || groupPlanDef || setPlanDef)
			{
				Tuple<int, Date> version = SQLiteMwsSimulationAccess.GetVerionInfo(SERVER_DATA_FOLDER);
				if (null != version)
				{
					try
					{
						// バージョン情報をインクリメント
						SQLiteMwsSimulationAccess.SetVersionInfo(SERVER_DATA_FOLDER, version.Item1 + 1, Date.Today);
						MainteLogger.SubLine(string.Format("バージョン情報更新：{0}", version.Item1 + 1));
					}
					catch (Exception ex)
					{
						MainteLogger.SubLine(string.Format("バージョン情報更新エラー：{0}", ex.Message));
						MainteLogger.MainLine("MWS課金シミュレーションデータファイル更新：異常終了");
						return;
					}
				}
			}
			// メンテナンス正常終了ログ出力
			MainteLogger.MainLine("MWS課金シミュレーションデータファイル更新：正常終了");
		}
	}
}
