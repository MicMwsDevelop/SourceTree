//
// MwsSimulationGetIO.cs
// 
// MIC WEB SERVICE 課金シミュレーション SQL SERVER データベース読込みI/Oクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
//
using System;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.MwsSimulation
{
	public static class MwsSimulationGetIO
	{
		/////////////////////////////////////////////
		// マスター情報関連

		/// <summary>
		/// サービス情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>サービス情報レコード数</returns>
		public static DataTable GetServiceInfo(bool sqlsv2)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT  M_SERVICE.SERVICE_ID AS ServiceCode"
								  + ", M_SERVICE.[SERVICE_NAME] AS ServiceName"
								  + ", M_SERVICE.PARENTS_SERVICE_ID AS ParentServiceCode"
								  + ", M_SERVICE_TYPE.SERVICE_TYPE_ID AS ServiceType"
								  + ", M_SERVICE_TYPE.SERVICE_TYPE_NAME AS ServiceTypeName"
								  + ", V_PCA_GOODS.PRICE AS Price"
								  + ", M_CODE.GOODS_ID AS GoodsID"
								  + ", V_PCA_GOODS.GOODS_NAME AS GoodsName"
								  + ", V_PCA_GOODS.BRAND_CLASSIFICATION AS GoodsKubun"
								  + " FROM dbo.M_SERVICE M_SERVICE"
								  + " INNER JOIN dbo.M_SERVICE_TYPE M_SERVICE_TYPE ON M_SERVICE.SERVICE_TYPE_ID = M_SERVICE_TYPE.SERVICE_TYPE_ID"
								  + " INNER JOIN dbo.M_CODE M_CODE ON M_SERVICE.SERVICE_ID = M_CODE.SERVICE_ID"
								  + " INNER JOIN dbo.V_PCA_GOODS V_PCA_GOODS ON M_CODE.GOODS_ID = V_PCA_GOODS.GOODS_ID"
								  + " WHERE (M_SERVICE.DELETE_FLG = N'0') AND (M_SERVICE.UMU_FLG = N'0') AND (M_CODE.DELETE_FLG = N'0') AND (M_CODE.SET_SALE = 1) AND (M_SERVICE.SERVICE_ID <> '1099')"
								  + " AND (V_PCA_GOODS.BRAND_CLASSIFICATION = 200 OR V_PCA_GOODS.BRAND_CLASSIFICATION = 201 OR V_PCA_GOODS.BRAND_CLASSIFICATION = 211 OR V_PCA_GOODS.BRAND_CLASSIFICATION = 212 OR V_PCA_GOODS.BRAND_CLASSIFICATION = 214)"
								  + " ORDER BY M_SERVICE_TYPE.SERVICE_TYPE_ID ASC, M_SERVICE.SERVICE_ID ASC";

					using (SqlCommand cmd = new SqlCommand(strSQL, con))
					{
						using (SqlDataAdapter da = new SqlDataAdapter(cmd))
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
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>セット割サービス情報の取得レコード数</returns>
		public static DataTable GetSetPlan(bool sqlsv2)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT GOODS.GOODS_ID AS GoodsID"
									+ ", GOODS.GOODS_NAME AS GoodsName"
									+ ", GOODS.PRICE AS Price"
									+ ", MCODE.SERVICE_ID AS ServiceCode"
									+ ", MSERVICE.SERVICE_NAME AS ServiceName"
									+ " FROM dbo.V_PCA_GOODS AS GOODS"
									+ " LEFT JOIN dbo.M_CODE AS MCODE ON MCODE.GOODS_ID = GOODS.GOODS_ID"
									+ " LEFT JOIN dbo.M_SERVICE AS MSERVICE ON MSERVICE.SERVICE_ID = MCODE.SERVICE_ID"
									+ " WHERE BRAND_CLASSIFICATION = 206"
									+ " ORDER BY GOODS.GOODS_ID ASC, MCODE.SERVICE_ID ASC";
  
                     using (SqlCommand cmd = new SqlCommand(strSQL, con))
					{
						using (SqlDataAdapter da = new SqlDataAdapter(cmd))
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
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>セット割サービス情報の取得レコード数</returns>
		public static DataTable GetSetPlanElement(bool sqlsv2)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT SERVICE_ID AS ServiceCode, GOODS_ID AS GoodsID"
									+ " FROM dbo.M_CODE"
									+ " WHERE SET_SALE = 1"
									+ " ORDER BY SERVICE_ID ASC";
  
                   using (SqlCommand cmd = new SqlCommand(strSQL, con))
					{
						using (SqlDataAdapter da = new SqlDataAdapter(cmd))
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
		/// <returns>おまとめプラン情報レコード数</returns>
		public static DataTable GetGroupPlan()
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCouplerConnectionString()))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT goods_id AS GoodsID"
									+ ", plan_nm AS GoodsName"
									+ ", keiyaku_month_cnt AS KeiyakuMonth"
									+ ", free_use_month AS FreeMonth"
									+ ", min_amount AS MinAmmount"
									+ ", max_amount AS MaxAmmount"
									+ " FROM dbo.GROUP_PLAN"
									+ " WHERE del_flg = N'0' AND goods_kbn = 204"
									+ " AND ((close_date is null) OR (open_date <= '{0}' AND close_date >= '{0}'))"
									+ " ORDER BY goods_id ASC, keiyaku_month_cnt ASC", DateTime.Now);

					using (SqlCommand cmd = new SqlCommand(strSQL, con))
					{
						using (SqlDataAdapter da = new SqlDataAdapter(cmd))
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
		/// <returns>おススメセット情報レコード数</returns>
		public static DataTable GetInitGroupPlan()
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCouplerConnectionString()))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT MSET.SET_ID AS GroupID"
									+ ", MSET.SET_NM AS GroupName"
									+ ", SET_SERVICE.SERVICE_ID AS ServiceCode"
									+ " FROM dbo.M_SET AS MSET"
									+ " LEFT JOIN dbo.M_SET_SERVICE AS SET_SERVICE ON SET_SERVICE.SET_ID = MSET.SET_ID"
									+ " WHERE MSET.DEL_FLG = N'0' AND (MSET.SET_START_DATE <= '{0}' AND MSET.SET_END_DATE >= '{0}')"
									+ " ORDER BY MSET.SET_ID ASC, SET_SERVICE.SERVICE_ID ASC", DateTime.Now);

					using (SqlCommand cmd = new SqlCommand(strSQL, con))
					{
						using (SqlDataAdapter da = new SqlDataAdapter(cmd))
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
