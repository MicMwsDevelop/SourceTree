//
// SoftwareMainteSaleDataGetIO.cs
//
// ソフトウェア保守料売上データ作成 データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/03/16 勝呂)
// 
using MwsLib.BaseFactory;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Charlie;
using MwsLib.DB.SqlServer.Junp;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.SoftwareMainteSaleData
{
	/// <summary>
	/// ソフトウェア保守料売上データ作成  データ取得クラス
	/// </summary>
	public static class SoftwareMainteSaleDataGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 顧客Noに対するソフトウェア保守料１年の受注情報の取得
		/// </summary>
		/// <param name="customerID">顧客No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetSoftwareMainteOrderSlip(int customerID, bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT"
						+ " H.[f受注番号]"
						+ ", H.[f受注日]"
						+ ", H.[f受注承認日]"
						+ ", H.[f売上承認日]"
						+ ", H.[f販売種別]"
						+ ", H.[f販売先コード]"
						+ ", H.[f販売先]"
						+ ", H.[fユーザーコード]"
						+ ", H.[fユーザー]"
						+ ", H.[fSV利用開始年月]"
						+ ", H.[fSV利用終了年月]"
						+ ", H.[fBshCode3]"
						+ ", H.[f担当支店名]"
						+ ", H.[f件名]"
						+ ", D.[f商品名]"
						+ ", D.[f商品コード]"
						+ ", D.[f商品名]"
						+ ", D.[f数量]"
						+ ", convert(int, D.[f標準価格]) AS f標準価格"
						+ ", convert(int, D.[f金額]) AS f金額"
						+ ", convert(int, D.[f提供価格]) AS f提供価格"
						+ ", D.[f税区分]"
						+ ", D.[f税率]"
						+ ", D.[f税込区分]"
						+ ", convert(int, D.[f売上原価]) AS f売上原価"
						+ ", B.[fPca部門コード]"
						+ ", B.[f担当者コード] as fPca担当者コード"
						+ ", B.[fPca倉庫コード]"
						+ " FROM {0} AS D"
						+ " LEFT JOIN {1} AS H ON D.[f受注番号] = H.[f受注番号]"
						+ " LEFT JOIN {2} AS B ON B.[fBshCode3] = H.[fBshCode3]"
						+ " WHERE D.[f商品コード] = '{3}' AND H.[f販売種別] = 3 AND H.[f売上承認日] is not null AND H.[fSV利用開始年月] is not null AND [fユーザーコード] = {4}"
						+ " ORDER BY H.[fユーザーコード] ASC, H.[f売上承認日] DESC"
						, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注詳細]
						, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注ヘッダ]
						, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]
						, PcaGoodsIDDefine.PaletteES_Mainte12
						, customerID);

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


		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// ソフトウェア保守料１年 自動更新対象利用情報の取得
		/// </summary>
		/// <param name="today">当日</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetCustomerUseInfoSoftwareMainte1(Date today, bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT SOFT.[CUSTOMER_ID]"
					+ ",SOFT.[SERVICE_ID]"
					+ ",[USE_START_DATE]"
					+ ",SOFT.[USE_END_DATE]"
					+ ",[CANCELLATION_DAY]"
					+ ",[PAUSE_END_STATUS]"
					+ ",[PERIOD_END_DATE]"
					+ ",ES.[USE_END_DATE] AS ES_USE_END_DATE"
					+ " FROM {0} AS SOFT"
					+ " LEFT JOIN"
					+ " ("
					+ "SELECT [CUSTOMER_ID], [USE_END_DATE]"
					+ " FROM {0}"
					+ " WHERE [SERVICE_ID] = {1} AND convert(int, convert(nvarchar, [USE_END_DATE], 112)) > {2}"   // 当月末日
					+ ") AS ES ON SOFT.CUSTOMER_ID = ES.CUSTOMER_ID"
					+ " WHERE SOFT.[SERVICE_ID] = {3} AND SOFT.[USE_END_DATE] <> ES.[USE_END_DATE]"// AND convert(int, convert(nvarchar, SOFT.[USE_END_DATE], 112)) = {2}"	// 当月末日
					+ " ORDER BY [CUSTOMER_ID] ASC"
					, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]
					, (int)ServiceCodeDefine.ServiceCode.PaletteES
					, today.LastDayOfTheMonth().ToIntYMD()
					, (int)ServiceCodeDefine.ServiceCode.SoftwareMaite1);

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
