//
// SQLiteMwsSimulationSetIO.cs
// 
// MIC WEB SERVICE 課金シミュレーション SQLiteデータベース書込みI/Oクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using MwsLib.BaseFactory.MwsSimulation;
using MwsLib.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace MwsLib.DB.SQLite.MwsSimulation
{
	/// <summary>
	/// MwsSimulation.dbへの書込みI/O
	/// </summary>
	public static class SQLiteMwsSimulationSetIO
	{
		/////////////////////////////////////////////
		// マスター情報関連

		/// <summary>
		/// バージョン情報の更新
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="version">バージョン</param>
		/// <param name="updateDate">更新日</param>
		/// <returns>影響行数</returns>
		public static int SetVersionInfo(string dbPath, int version, Date updateDate)
		{
			int rowCount = -1;

			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SQLiteTransaction tran = con.BeginTransaction(IsolationLevel.Serializable))
					{
						try
						{
							DeleteAllVersionInfo(con, tran);

							// 追加
							rowCount = InsertIntoVersionInfo(con, tran, version, updateDate);

							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}

		/// <summary>
		/// バージョン情報の削除
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <returns>影響行数、失敗時はException</returns>
		private static int DeleteAllVersionInfo(SQLiteConnection con, SQLiteTransaction tran)
		{
			int result = -1;
			string sqlString = string.Format(@"DELETE FROM {0}", SQLiteMwsSimulationDef.VERSION_INFO_TABLE_NAME);

			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// バージョン情報の追加
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <param name="version">見積書番号</param>
		/// <param name="updateDate">サービス情報</param>
		/// <returns>影響行数</returns>
		private static int InsertIntoVersionInfo(SQLiteConnection con, SQLiteTransaction tran, int version, Date updateDate)
		{
			int result = -1;
			string sqlString = string.Format(@"INSERT INTO {0} VALUES (@1, @2)", SQLiteMwsSimulationDef.VERSION_INFO_TABLE_NAME);
			SQLiteParameter[] param = { new SQLiteParameter("@1", version),
										new SQLiteParameter("@2", updateDate.ToIntYMD()) };
			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString, param);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// サービス情報の削除
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="estimateID">見積書番号</param>
		/// <returns>影響行数</returns>
		public static int DeleteAllServiceInfo(string dbPath)
		{
			int rowCount = -1;

			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SQLiteTransaction tran = con.BeginTransaction(IsolationLevel.Serializable))
					{
						try
						{
							// サービス情報の削除
							rowCount = DeleteAllServiceInfo(con, tran);

							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}

		/// <summary>
		/// サービス情報の削除
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <returns>影響行数、失敗時はException</returns>
		private static int DeleteAllServiceInfo(SQLiteConnection con, SQLiteTransaction tran)
		{
			int result = -1;
			string sqlString = string.Format(@"DELETE FROM {0}", SQLiteMwsSimulationDef.SERVICE_INFO_TABLE_NAME);

			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// おまとめプラン情報の削除
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>影響行数</returns>
		public static int DeleteAllGroupPlan(string dbPath)
		{
			int rowCount = -1;

			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SQLiteTransaction tran = con.BeginTransaction(IsolationLevel.Serializable))
					{
						try
						{
							// サービス情報の削除
							rowCount = DeleteAllGroupPlan(con, tran);

							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}

		/// <summary>
		/// おまとめプラン情報の削除
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <returns>影響行数、失敗時はException</returns>
		private static int DeleteAllGroupPlan(SQLiteConnection con, SQLiteTransaction tran)
		{
			int result = -1;
			string sqlString = string.Format(@"DELETE FROM {0}", SQLiteMwsSimulationDef.GROUP_PLAN_TABLE_NAME);

			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// おススメセット情報の削除
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="estimateID">見積書番号</param>
		/// <returns>影響行数</returns>
		public static int DeleteAllInitGroupPlan(string dbPath)
		{
			int rowCount = -1;

			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SQLiteTransaction tran = con.BeginTransaction(IsolationLevel.Serializable))
					{
						try
						{
							// おススメセット情報の削除
							rowCount = DeleteAllInitGroupPlan(con, tran);
							DeleteAllInitGroupPlanElement(con, tran);

							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}

		/// <summary>
		/// おススメセット情報の削除
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <returns>影響行数、失敗時はException</returns>
		private static int DeleteAllInitGroupPlan(SQLiteConnection con, SQLiteTransaction tran)
		{
			int result = -1;
			string sqlString = string.Format(@"DELETE FROM {0}", SQLiteMwsSimulationDef.INIT_GROUP_PLAN_TABLE_NAME);

			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// おススメセット情報の削除
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <returns>影響行数、失敗時はException</returns>
		private static int DeleteAllInitGroupPlanElement(SQLiteConnection con, SQLiteTransaction tran)
		{
			int result = -1;
			string sqlString = string.Format(@"DELETE FROM {0}", SQLiteMwsSimulationDef.INIT_GROUP_PLAN_ELEMENT_TABLE_NAME);

			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// セット割サービス情報の削除
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="estimateID">見積書番号</param>
		/// <returns>影響行数</returns>
		public static int DeleteAllSetPlan(string dbPath)
		{
			int rowCount = -1;

			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SQLiteTransaction tran = con.BeginTransaction(IsolationLevel.Serializable))
					{
						try
						{
							// おススメセット情報の削除
							rowCount = DeleteAllSetPlanHeader(con, tran);
							DeleteAllSetPlanElement(con, tran);

							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}

		/// <summary>
		/// セット割サービス情報の削除
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <returns>影響行数、失敗時はException</returns>
		private static int DeleteAllSetPlanHeader(SQLiteConnection con, SQLiteTransaction tran)
		{
			int result = -1;
			string sqlString = string.Format(@"DELETE FROM {0}", SQLiteMwsSimulationDef.SET_PLAN_HEADER_TABLE_NAME);

			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// セット割サービス情報の削除
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <returns>影響行数、失敗時はException</returns>
		private static int DeleteAllSetPlanElement(SQLiteConnection con, SQLiteTransaction tran)
		{
			int result = -1;
			string sqlString = string.Format(@"DELETE FROM {0}", SQLiteMwsSimulationDef.SET_PLAN_ELEMENT_TABLE_NAME);

			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}


		/////////////////////////////////////////////
		// ユーザー情報関連

		/// <summary>
		/// 見積書情報の追加
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="est">見積書情報</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoEstimate(string dbPath, Estimate est)
		{
			int rowCount = -1;

			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_USER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SQLiteTransaction tran = con.BeginTransaction(IsolationLevel.Serializable))
					{
						try
						{
							// 見積書ヘッダ情報の追加
							rowCount = InsertIntoEstimateHeader(con, tran, est);

							int i = 0;
							foreach (EstimateService service in est.ServiceList)
							{
								// 見積書サービス情報の追加
								InsertIntoEstimateService(con, tran, est.EstimateID, i, service);

								if (SQLiteMwsSimulationDef.ServiceMode.None != service.Mode)
								{
									int j = 0;
									foreach (Tuple<string, string> group in service.GroupServiceList)
									{
										// おまとめプラン・セット割サービス情報の追加
										InsertIntoEstimateGroupElement(con, tran, est.EstimateID, service.GoodsID, j, group);
										j++;
									}
								}
								i++;
							}
							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}

		/// <summary>
		/// 見積書ヘッダ情報の追加
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <param name="est">見積書情報</param>
		/// <returns>影響行数</returns>
		private static int InsertIntoEstimateHeader(SQLiteConnection con, SQLiteTransaction tran, Estimate est)
		{
			int result = -1;
			string sqlString = string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9)", SQLiteMwsSimulationDef.ESTIMATE_HEADER_TABLE_NAME);
			SQLiteParameter[] param = { new SQLiteParameter("@1", est.EstimateID),
										new SQLiteParameter("@2", est.Destination),
										new SQLiteParameter("@3", est.PrintDate.ToIntYMD()),
										new SQLiteParameter("@4", est.AgreeStartDate.ToIntYMD()),
										new SQLiteParameter("@5", est.AgreeMonthes),
										new SQLiteParameter("@6", (0 < est.Remark.Count) ? est.Remark[0]: ""),
										new SQLiteParameter("@7", (1 < est.Remark.Count) ? est.Remark[1]: ""),
										new SQLiteParameter("@8", (2 < est.Remark.Count) ? est.Remark[2]: ""),
										new SQLiteParameter("@9", (3 < est.Remark.Count) ? est.Remark[3]: "") };
			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString, param);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// 見積書サービス情報の追加
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <param name="estimateID">見積書番号</param>
		/// <param name="seqNo">シーケンス番号</param>
		/// <param name="service">サービス情報</param>
		/// <returns>影響行数</returns>
		private static int InsertIntoEstimateService(SQLiteConnection con, SQLiteTransaction tran, int estimateID, int seqNo, EstimateService service)
		{
			int result = -1;
			string sqlString = string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6)", SQLiteMwsSimulationDef.ESTIMATE_SERVICE_TABLE_NAME);
			SQLiteParameter[] param = { new SQLiteParameter("@1", estimateID),
										new SQLiteParameter("@2", service.GoodsID),
										new SQLiteParameter("@3", seqNo),
										new SQLiteParameter("@4", service.ServiceName),
										new SQLiteParameter("@5", service.Price),
										new SQLiteParameter("@6", service.Mode) };
			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString, param);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// おまとめプラン・セット割サービス情報の追加
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <param name="estimateID">見積書番号</param>
		/// <param name="goodsID">商品ID</param>
		/// <param name="seqNo">シーケンス番号</param>
		/// <param name="group">おまとめプラン・セット割サービス情報</param>
		/// <returns>影響行数</returns>
		private static int InsertIntoEstimateGroupElement(SQLiteConnection con, SQLiteTransaction tran, int estimateID, string goodsID, int seqNo, Tuple<string, string> group)
		{
			int result = -1;
			string sqlString = string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5)", SQLiteMwsSimulationDef.ESTIMATE_GROUP_ELEMENT_TABLE_NAME);
			SQLiteParameter[] param = { new SQLiteParameter("@1", estimateID),
										new SQLiteParameter("@2", goodsID),
										new SQLiteParameter("@3", seqNo),
										new SQLiteParameter("@4", group.Item1),
										new SQLiteParameter("@5", group.Item2) };
			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString, param);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// 見積書情報の宛先変更
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="estimateID">見積書ID</param>
		/// <param name="destination">宛先</param>
		/// <returns>影響行数</returns>
		public static int UpdateEstimateHeaderDestination(string dbPath, int estimateID, string destination)
		{
			int rowCount = -1;

			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_USER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SQLiteTransaction tran = con.BeginTransaction(IsolationLevel.Serializable))
					{
						try
						{
							// 更新
							rowCount = UpdateEstimateHeaderDestination(con, tran, estimateID, destination);

							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}

		/// <summary>
		/// 見積書情報の更新
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="est">見積書情報</param>
		/// <returns>影響行数</returns>
		public static int UpdateEstimate(string dbPath, Estimate est)
		{
			int rowCount = -1;

			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_USER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SQLiteTransaction tran = con.BeginTransaction(IsolationLevel.Serializable))
					{
						try
						{
							// 見積書ヘッダ情報の削除
							DeleteEstimateHeader(con, tran, est.EstimateID);

							// 見積書サービス情報の削除
							DeleteEstimateService(con, tran, est.EstimateID);

							// おまとめプラン・セット割サービス情報の削除
							DeleteEstimateGroupElement(con, tran, est.EstimateID);

							// 見積書ヘッダ情報の追加
							rowCount = InsertIntoEstimateHeader(con, tran, est);

							// 見積書サービス情報の追加
							int i = 0;
							foreach (EstimateService service in est.ServiceList)
							{
								// 見積書サービス情報の追加
								InsertIntoEstimateService(con, tran, est.EstimateID, i, service);

								if (SQLiteMwsSimulationDef.ServiceMode.None != service.Mode)
								{
									int j = 0;
									foreach (Tuple<string, string> group in service.GroupServiceList)
									{
										// おまとめプラン・セット割サービス情報の追加
										InsertIntoEstimateGroupElement(con, tran, est.EstimateID, service.GoodsID, j, group);
										j++;
									}
								}
								i++;
							}
							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}

		/// <summary>
		/// 見積書情報の宛先変更
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <param name="estimateID">見積書番号</param>
		/// <param name="destination">宛先</param>
		/// <returns>影響行数、失敗時はException</returns>
		private static int UpdateEstimateHeaderDestination(SQLiteConnection con, SQLiteTransaction tran, int estimateID, string destination)
		{
			int result = -1;

			string sqlString = string.Format(@"UPDATE {0} SET Destination = @1 WHERE EstimateID = {1}", SQLiteMwsSimulationDef.ESTIMATE_HEADER_TABLE_NAME, estimateID);
			SQLiteParameter[] param = { new SQLiteParameter("@1", destination) };

			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString, param);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// 見積書情報の削除
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="estimateID">見積書番号</param>
		/// <returns>影響行数</returns>
		public static int DeleteEstimate(string dbPath, int estimateID)
		{
			int rowCount = -1;

			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_USER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SQLiteTransaction tran = con.BeginTransaction(IsolationLevel.Serializable))
					{
						try
						{
							// 見積書ヘッダ情報の削除
							rowCount = DeleteEstimateHeader(con, tran, estimateID);

							// 見積書サービス情報の削除
							DeleteEstimateService(con, tran, estimateID);

							// 見積書おまとめプラン・セット割サービス情報の削除
							DeleteEstimateGroupElement(con, tran, estimateID);

							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}

		/// <summary>
		/// 見積書ヘッダ情報の削除
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <param name="estimateID">見積書番号</param>
		/// <returns>影響行数、失敗時はException</returns>
		private static int DeleteEstimateHeader(SQLiteConnection con, SQLiteTransaction tran, int estimateID)
		{
			int result = -1;
			string sqlString = string.Format(@"DELETE FROM {0} WHERE EstimateID = @1", SQLiteMwsSimulationDef.ESTIMATE_HEADER_TABLE_NAME);

			SQLiteParameter[] param = { new SQLiteParameter("@1", estimateID) };

			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString, param);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// 見積書サービス情報の削除
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <param name="estimateID">見積書番号</param>
		/// <returns>影響行数、失敗時はException</returns>
		private static int DeleteEstimateService(SQLiteConnection con, SQLiteTransaction tran, int estimateID)
		{
			int result = -1;
			string sqlString = string.Format(@"DELETE FROM {0} WHERE EstimateID = @1", SQLiteMwsSimulationDef.ESTIMATE_SERVICE_TABLE_NAME);

			SQLiteParameter[] param = { new SQLiteParameter("@1", estimateID) };

			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString, param);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// 見積書おまとめプラン・セット割サービス情報の削除
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <param name="estimateID">見積書番号</param>
		/// <returns>影響行数、失敗時はException</returns>
		private static int DeleteEstimateGroupElement(SQLiteConnection con, SQLiteTransaction tran, int estimateID)
		{
			int result = -1;
			string sqlString = string.Format(@"DELETE FROM {0} WHERE EstimateID = @1", SQLiteMwsSimulationDef.ESTIMATE_GROUP_ELEMENT_TABLE_NAME);

			SQLiteParameter[] param = { new SQLiteParameter("@1", estimateID) };

			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString, param);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// サービス情報リストの更新
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="list">サービス情報リスト</param>
		/// <returns>影響行数</returns>
		public static int UpdateServiceInfoList(string dbPath, ServiceInfoList list)
		{
			int rowCount = -1;

			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SQLiteTransaction tran = con.BeginTransaction(IsolationLevel.Serializable))
					{
						try
						{
							// サービス情報の削除
							rowCount = DeleteAllServiceInfo(con, tran);

							// サービス情報の追加
							if (null != list.Standard)
							{
								// ＭＩＣ ＷＥＢ ＳＥＲＶＩＣＥ 標準機能の追加
								InsertIntoServiceInfo(con, tran, list.Standard);
							}
							foreach (ServiceInfo service in list)
							{
								InsertIntoServiceInfo(con, tran, service);
							}
							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}

		/// <summary>
		/// サービス情報の追加
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <param name="service">サービス情報</param>
		/// <returns>影響行数</returns>
		private static int InsertIntoServiceInfo(SQLiteConnection con, SQLiteTransaction tran, ServiceInfo service)
		{
			int result = -1;
			string sqlString = string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9)", SQLiteMwsSimulationDef.SERVICE_INFO_TABLE_NAME);
			SQLiteParameter[] param = { new SQLiteParameter("@1", service.ServiceCode),
										new SQLiteParameter("@2", service.ServiceName),
										new SQLiteParameter("@3", service.ParentServiceCode),
										new SQLiteParameter("@4", service.ServiceType),
										new SQLiteParameter("@5", service.ServiceTypeName),
										new SQLiteParameter("@6", service.Price),
										new SQLiteParameter("@7", service.GoodsID),
										new SQLiteParameter("@8", service.GoodsName),
										new SQLiteParameter("@9", service.GoodsKubun) };
			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString, param);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// おまとめプラン情報リストの更新
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="list">おまとめプラン情報リスト</param>
		/// <returns>影響行数</returns>
		public static int UpdateGroupPlanList(string dbPath, GroupPlanList list)
		{
			int rowCount = -1;

			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SQLiteTransaction tran = con.BeginTransaction(IsolationLevel.Serializable))
					{
						try
						{
							// おまとめプラン情報の削除
							rowCount = DeleteAllGroupPlan(con, tran);

							// おまとめプラン情報の追加
							int i = 0;
							foreach (GroupPlan plan in list)
							{
								InsertIntoGroupPlan(con, tran, plan, i);
								i++;
							}
							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}

		/// <summary>
		/// おまとめプラン情報の追加
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <param name="plan">おまとめプラン情報</param>
		/// <returns>影響行数</returns>
		private static int InsertIntoGroupPlan(SQLiteConnection con, SQLiteTransaction tran, GroupPlan plan, int seqNo)
		{
			int result = -1;
			string sqlString = string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7)", SQLiteMwsSimulationDef.GROUP_PLAN_TABLE_NAME);
			SQLiteParameter[] param = { new SQLiteParameter("@1", plan.GoodsID),
										new SQLiteParameter("@2", seqNo),
										new SQLiteParameter("@3", plan.GoodsName),
										new SQLiteParameter("@4", plan.KeiyakuMonth),
										new SQLiteParameter("@5", plan.FreeMonth),
										new SQLiteParameter("@6", plan.MinAmmount),
										new SQLiteParameter("@7", plan.MaxAmmount) };
			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString, param);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// おススメセット情報リストの更新
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="list">おススメセット情報リスト</param>
		/// <returns>影響行数</returns>
		public static int UpdateInitGroupPlanList(string dbPath, List<InitGroupPlan> list)
		{
			int rowCount = -1;

			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SQLiteTransaction tran = con.BeginTransaction(IsolationLevel.Serializable))
					{
						try
						{
							// おススメセット情報の削除
							rowCount = DeleteAllInitGroupPlan(con, tran);

							// おススメセットサービス情報の削除
							DeleteAllInitGroupPlanElement(con, tran);

							foreach (InitGroupPlan plan in list)
							{
								// おススメセット情報の追加
								InsertIntoInitGroupPlan(con, tran, plan);
								foreach (string serviceCode in plan.ServiceCodeList)
								{
									// おススメセットサービス情報の追加
									InsertIntoInitGroupPlanElement(con, tran, plan.GroupID, serviceCode);
								}
							}
							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}

		/// <summary>
		/// おススメセット情報の追加
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <param name="plan">おススメセット情報</param>
		/// <returns>影響行数</returns>
		private static int InsertIntoInitGroupPlan(SQLiteConnection con, SQLiteTransaction tran, InitGroupPlan plan)
		{
			int result = -1;
			string sqlString = string.Format(@"INSERT INTO {0} VALUES (@1, @2)", SQLiteMwsSimulationDef.INIT_GROUP_PLAN_TABLE_NAME);
			SQLiteParameter[] param = { new SQLiteParameter("@1", plan.GroupID),
										new SQLiteParameter("@2", plan.GroupName) };
			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString, param);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// おススメセットサービス情報の追加
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <param name="groupID">グループID</param>
		/// <param name="serviceCode">サービスコード</param>
		/// <returns>影響行数</returns>
		private static int InsertIntoInitGroupPlanElement(SQLiteConnection con, SQLiteTransaction tran, int groupID, string serviceCode)
		{
			int result = -1;
			string sqlString = string.Format(@"INSERT INTO {0} VALUES (@1, @2)", SQLiteMwsSimulationDef.INIT_GROUP_PLAN_ELEMENT_TABLE_NAME);
			SQLiteParameter[] param = { new SQLiteParameter("@1", groupID),
										new SQLiteParameter("@2", serviceCode) };
			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString, param);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// セット割サービス情報リストの更新
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="list">おススメセット情報リスト</param>
		/// <returns>影響行数</returns>
		public static int UpdateSetPlanList(string dbPath, List<SetPlan> list)
		{
			int rowCount = -1;

			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SQLiteTransaction tran = con.BeginTransaction(IsolationLevel.Serializable))
					{
						try
						{
							// セット割サービスヘッダ情報の削除
							rowCount = DeleteAllSetPlanHeader(con, tran);

							// セット割サービス情報の削除
							DeleteAllSetPlanElement(con, tran);

							foreach (SetPlan plan in list)
							{
								// セット割サービスヘッダ情報の追加
								InsertIntoSetPlanHeader(con, tran, plan);
								foreach (Tuple<string, string> service in plan.ServiceList)
								{
									// セット割サービス情報の追加
									InsertIntoSetPlanElement(con, tran, plan.GoodsID, service);
								}
							}
							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch
				{
					throw;
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}

		/// <summary>
		/// セット割サービスヘッダ情報の追加
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <param name="plan">セット割サービス情報</param>
		/// <returns>影響行数</returns>
		private static int InsertIntoSetPlanHeader(SQLiteConnection con, SQLiteTransaction tran, SetPlan plan)
		{
			int result = -1;
			string sqlString = string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3)", SQLiteMwsSimulationDef.SET_PLAN_HEADER_TABLE_NAME);
			SQLiteParameter[] param = { new SQLiteParameter("@1", plan.GoodsID),
										new SQLiteParameter("@2", plan.GoodsName),
										new SQLiteParameter("@3", plan.Price) };
			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString, param);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}

		/// <summary>
		/// セット割サービス情報の追加
		/// </summary>
		/// <param name="con">SQLite接続</param>
		/// <param name="tran">トランザクション</param>
		/// <param name="parentGoodsID">親商品ID</param>
		/// <param name="service">サービス情報</param>
		/// <returns>影響行数</returns>
		private static int InsertIntoSetPlanElement(SQLiteConnection con, SQLiteTransaction tran, string parentGoodsID, Tuple<string, string> service)
		{
			int result = -1;
			string sqlString = string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3)", SQLiteMwsSimulationDef.SET_PLAN_ELEMENT_TABLE_NAME);
			SQLiteParameter[] param = { new SQLiteParameter("@1", parentGoodsID),
										new SQLiteParameter("@2", service.Item1),
										new SQLiteParameter("@3", service.Item2) };
			// 実行
			result = SQLiteController.SqlExecuteCommand(con, tran, sqlString, param);
			if (result <= -1)
			{
				throw new ApplicationException("");
			}
			return result;
		}
	}
}
