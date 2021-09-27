//
// SQLiteMwsSimulationGetIO.cs
// 
// MIC WEB SERVICE 課金シミュレーション SQLiteデータベース読込みI/Oクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
// Ver2.100 おまとめプラン48ヵ月、60ヵ月に対応(2019/01/22 勝呂)
//
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace CommonLib.DB.SQLite.MwsSimulation
{
	/// <summary>
	/// MwsSimulation.dbへの読込みI/O
	/// </summary>
	public static class SQLiteMwsSimulationGetIO
	{
		/////////////////////////////////////////////
		// マスター情報関連

		/// <summary>
		/// バージョン情報の取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>バージョン情報</returns>
		public static DataTable GetVersionInfo(string dbPath)
		{
			DataTable result = null;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					string strSql = string.Format(@"SELECT * FROM {0} ORDER BY DataVersion DESC", SQLiteMwsSimulationDef.VERSION_INFO_TABLE_NAME);
					using (SQLiteCommand cmd = new SQLiteCommand(strSql, con))
					{
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
						{
							result = new DataTable();
							da.Fill(result);
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
			return result;
		}

		/// <summary>
		/// 消費税情報の取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>バージョン情報</returns>
		public static DataTable GetTaxRate(string dbPath)
		{
			DataTable result = null;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					string strSql = string.Format(@"SELECT * FROM {0} ORDER BY StartDate ASC", SQLiteMwsSimulationDef.TAXRATE_TABLE_NAME);
					using (SQLiteCommand cmd = new SQLiteCommand(strSql, con))
					{
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
						{
							result = new DataTable();
							da.Fill(result);
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
			return result;
		}

		/// <summary>
		/// サービス情報の取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>サービス情報レコード</returns>
		public static DataTable GetServiceInfo(string dbPath)
		{
			DataTable result = null;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					string strSql = string.Format(@"SELECT * FROM {0} ORDER BY ServiceType ASC, ServiceCode ASC", SQLiteMwsSimulationDef.SERVICE_INFO_TABLE_NAME);
					using (SQLiteCommand cmd = new SQLiteCommand(strSql, con))
					{
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
						{
							result = new DataTable();
							da.Fill(result);
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
			return result;
		}

		/// <summary>
		/// おまとめプラン情報の取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="readType">読込種別</param>
		/// <returns>おまとめプラン情報レコード</returns>
		// Ver2.100 おまとめプラン48ヵ月、60ヵ月に対応(2019/01/22 勝呂)
		public static DataTable GetGroupPlan(string dbPath, int readType)
		{
			DataTable result = null;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					string strSql = string.Empty;
					switch (readType)
					{
						// 全て
						case 0:
							strSql = string.Format(@"SELECT * FROM {0} ORDER BY GoodsID ASC, SeqNo ASC", SQLiteMwsSimulationDef.GROUP_PLAN_TABLE_NAME);
							break;
						// 12ヵ月 or 24ヵ月 or 32ヵ月
						case 1:
							strSql = string.Format(@"SELECT * FROM {0} WHERE GoodsID = '{1}' OR GoodsID = '{2}' OR GoodsID = '{3}' ORDER BY GoodsID ASC, SeqNo ASC", SQLiteMwsSimulationDef.GROUP_PLAN_TABLE_NAME, SQLiteMwsSimulationDef.MWS_MATOME12_GOODSID, SQLiteMwsSimulationDef.MWS_MATOME24_GOODSID, SQLiteMwsSimulationDef.MWS_MATOME36_GOODSID);
							break;
						// 12ヵ月 or 36ヵ月 or 60ヵ月
						case 2:
							strSql = string.Format(@"SELECT * FROM {0} WHERE GoodsID = '{1}' OR GoodsID = '{2}' OR GoodsID = '{3}' ORDER BY GoodsID ASC, SeqNo ASC", SQLiteMwsSimulationDef.GROUP_PLAN_TABLE_NAME, SQLiteMwsSimulationDef.MWS_MATOME12_GOODSID, SQLiteMwsSimulationDef.MWS_MATOME36_GOODSID, SQLiteMwsSimulationDef.MWS_MATOME60_GOODSID);
							break;
					}
					using (SQLiteCommand cmd = new SQLiteCommand(strSql, con))
					{
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
						{
							result = new DataTable();
							da.Fill(result);
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
			return result;
		}

		/// <summary>
		/// おススメセット情報の取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>おススメセット情報レコード</returns>
		public static DataTable GetInitGroupPlan(string dbPath)
		{
			DataTable result = null;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					string strSql = string.Format(@"SELECT * FROM {0} ORDER BY GroupID ASC", SQLiteMwsSimulationDef.INIT_GROUP_PLAN_TABLE_NAME);
					using (SQLiteCommand cmd = new SQLiteCommand(strSql, con))
					{
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
						{
							result = new DataTable();
							da.Fill(result);
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
			return result;
		}

		/// <summary>
		/// おススメセットサービス情報の取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>サービス情報レコード</returns>
		public static DataTable GetInitGroupPlanElement(string dbPath, int groupID)
		{
			DataTable result = null;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					string strSql = string.Format(@"SELECT * FROM {0} WHERE GroupID = {1} ORDER BY ServiceCode ASC", SQLiteMwsSimulationDef.INIT_GROUP_PLAN_ELEMENT_TABLE_NAME, groupID);
					using (SQLiteCommand cmd = new SQLiteCommand(strSql, con))
					{
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
						{
							result = new DataTable();
							da.Fill(result);
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
			return result;
		}

		/// <summary>
		/// セット割サービス情報の取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>セット割サービス情報レコード</returns>
		public static DataTable GetSetPlanHeader(string dbPath)
		{
			DataTable result = null;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					string strSql = string.Format(@"SELECT * FROM {0} ORDER BY GoodsID ASC", SQLiteMwsSimulationDef.SET_PLAN_HEADER_TABLE_NAME);
					using (SQLiteCommand cmd = new SQLiteCommand(strSql, con))
					{
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
						{
							result = new DataTable();
							da.Fill(result);
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
			return result;
		}

		/// <summary>
		/// セット割サービスサービス情報の取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>セット割サービス情報レコード</returns>
		public static DataTable GetSetPlanElement(string dbPath, string parentGoodsID)
		{
			DataTable result = null;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					string strSql = string.Format(@"SELECT GoodsID, ServiceName FROM {0} WHERE ParentGoodsID = '{1}' ORDER BY GoodsID ASC", SQLiteMwsSimulationDef.SET_PLAN_ELEMENT_TABLE_NAME, parentGoodsID);
					using (SQLiteCommand cmd = new SQLiteCommand(strSql, con))
					{
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
						{
							result = new DataTable();
							da.Fill(result);
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
			return result;
		}


		/////////////////////////////////////////////
		// ユーザー情報関連

		/// <summary>
		/// 次回見積書番号の取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>次回見積書番号</returns>
		public static int GetLastEstimateNumber(string dbPath)
		{
			int result = 1;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_USER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					//string strSql = string.Format(@"SELECT Max(EstimateID) AS MaxEstimateID FROM {0}", MwsSimulationDef.ESTIMATE_HEADER_TABLE_NAME);
					string strSql = string.Format(@"SELECT EstimateID AS MaxEstimateID FROM {0} ORDER BY EstimateID DESC", SQLiteMwsSimulationDef.ESTIMATE_HEADER_TABLE_NAME);
					using (SQLiteCommand cmd = new SQLiteCommand(strSql, con))
					{
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
						{
							DataTable table = new DataTable();
							da.Fill(table);
							if (0 < table.Rows.Count)
							{
								result = DataBaseValue.ConvObjectToInt(table.Rows[0]["MaxEstimateID"]) + 1;
							}
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
			return result;
		}

		/// <summary>
		/// 見積書宛先リストの取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>見積書宛先リスト</returns>
		public static DataTable GetEstimateDestinationList(string dbPath)
		{
			DataTable result = null;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_USER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					string strSql = string.Format(@"SELECT EstimateID, Destination FROM {0} ORDER BY EstimateID DESC", SQLiteMwsSimulationDef.ESTIMATE_HEADER_TABLE_NAME);
					using (SQLiteCommand cmd = new SQLiteCommand(strSql, con))
					{
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
						{
							result = new DataTable();
							da.Fill(result);
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
			return result;
		}

		/// <summary>
		/// 見積書ヘッダ情報の取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <returns>見積書ヘッダ情報レコード数</returns>
		public static DataTable GetEstimateHeader(string dbPath)
		{
			DataTable result = null;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_USER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					string strSql = string.Format(@"SELECT * FROM {0} ORDER BY EstimateID ASC", SQLiteMwsSimulationDef.ESTIMATE_HEADER_TABLE_NAME);
					using (SQLiteCommand cmd = new SQLiteCommand(strSql, con))
					{
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
						{
							result = new DataTable();
							da.Fill(result);
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
			return result;
		}

		/// <summary>
		/// 見積書サービス情報の取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="estimateID">見積書番号</param>
		/// <returns>見積書サービス情報レコード数</returns>
		public static DataTable GetEstimateService(string dbPath, int estimateID)
		{
			DataTable result = null;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_USER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					string strSql = string.Format(@"SELECT * FROM {0} WHERE EstimateID = {1} ORDER BY SeqNo ASC", SQLiteMwsSimulationDef.ESTIMATE_SERVICE_TABLE_NAME, estimateID);
					using (SQLiteCommand cmd = new SQLiteCommand(strSql, con))
					{
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
						{
							result = new DataTable();
							da.Fill(result);
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
			return result;
		}

		/// <summary>
		/// 見積書おまとめプラン・セット割サービス情報の取得
		/// </summary>
		/// <param name="dbPath">データベース格納フォルダ</param>
		/// <param name="estimateID">見積書番号</param>
		/// <param name="goodsID">おまとめプラン・セット割サービス商品ID</param>
		/// <returns>見積書おまとめプラン・セット割サービス情報レコード数</returns>
		public static DataTable GetEstimateGroupElement(string dbPath, int estimateID, string parentGoodsID)
		{
			DataTable result = null;
			using (SQLiteConnection con = new SQLiteConnection(SQLiteAccess.CreateConnectionString(Path.Combine(dbPath, SQLiteMwsSimulationDef.MWS_SIMULATION_USER_DATABASE_NAME))))
			{
				try
				{
					// 接続
					con.Open();

					string strSql = string.Format(@"SELECT * FROM {0} WHERE EstimateID = {1} AND ParentGoodsID = '{2}' ORDER BY SeqNo ASC", SQLiteMwsSimulationDef.ESTIMATE_GROUP_ELEMENT_TABLE_NAME, estimateID, parentGoodsID);
					using (SQLiteCommand cmd = new SQLiteCommand(strSql, con))
					{
						using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
						{
							result = new DataTable();
							da.Fill(result);
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
			return result;
		}
	}
}
