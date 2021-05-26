using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.DecylAnalysis
{
	public static class DecylAnalysisGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 商品マスターの取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetGoodsMasterList(bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT CD.SERVICE_ID AS ServiceID"
									+ ", PCA.商品ID AS GoodsID"
									+ ", PCA.商品名 AS GoodsName"
									+ ", CAST(PCA.標準価格 AS int) AS GoodsPice"
									+ " FROM [charlieDB].[dbo].[PCA商品マスタ参照ビュー] AS PCA"
									+ " LEFT JOIN [charlieDB].[dbo].[M_CODE] AS CD"
									+ " ON PCA.商品ID = CD.GOODS_ID"
									+ " WHERE (PCA.商品区分 = 200 OR PCA.商品区分 = 201 OR PCA.商品区分 = 213) AND CD.DELETE_FLG = 0 AND PCA.商品ID <> '800101' AND PCA.商品ID <> '800151' "
									+ " ORDER BY CD.SERVICE_ID ASC";

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
		/// 月額課金ユーザーの取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetMonthlyUserList(bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT 顧客ID AS CustomerNo"
					+ ", 得意先コード AS TokuisakiCode"
					+ ", 顧客名 AS CustomerName"
					+ " FROM [charlieDB].[dbo].[view_MWSユーザー] AS MWS"
					+ " LEFT JOIN [charlieDB].[dbo].[T_PRODUCT_CONTROL] AS PD"
					+ " ON MWS.顧客ID = PD.CUSTOMER_ID"
					+ " LEFT JOIN [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION] AS CUI"
					+ " ON CUI.CUSTOMER_ID = MWS.顧客ID"
					+ " LEFT JOIN [JunpDB].[dbo].[tClient] AS CL"
					+ " ON MWS.顧客ID = CL.fCliID"
					+ " WHERE MWS.申込種別]= '3' AND CUI.SERVICE_ID = 1001 AND CL.fCliEnd = 0"
					+ " ORDER BY [顧客ID] ASC";

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
