//
// AlertCloudBackupPcSupportPlusGetIO.cs
//
// クラウドバックアップPC安心サポートPlus同時申込アラート データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/02/05 勝呂)
// 
using MwsLib.BaseFactory;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Charlie;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.AlertCloudBackupPcSupportPlus
{
	public class AlertCloudBackupPcSupportPlusGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// chralieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// クラウドバックアップとクラウドバックアップ（PC安心サポートPlus）同時契約中リストの取得
		/// </summary>
		/// <param name="date">処理日付</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>DataTable</returns>
		public static DataTable GetCloudBackupPcSupportPlusList(Date date, bool ct)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieWebConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT PC.CUSTOMER_ID, V.顧客名 AS CLINIC_NAME, PC.USE_START_DATE AS PC_START_DATE, PC.USE_END_DATE AS PC_END_DATE"
													+ ", CL.USE_START_DATE AS CL_START_DATE, CL.USE_END_DATE AS CL_END_DATE"
													+ " FROM {0} AS PC"
													+ " INNER JOIN"
													+ " ("
													+ "SELECT CUSTOMER_ID, USE_START_DATE, USE_END_DATE"
													+ " FROM {0}"
													+ " WHERE SERVICE_ID = {1} AND USE_START_DATE <= '{3}' AND USE_END_DATE >= '{3}'"
													+ ") AS CL ON CL.CUSTOMER_ID = PC.CUSTOMER_ID"
													+ " INNER JOIN {4} AS V ON V.顧客No = PC.CUSTOMER_ID"
													+ " WHERE PC.SERVICE_ID = {2} AND PC.USE_START_DATE <= '{3}' AND PC.USE_END_DATE >= '{3}'"
													+ " ORDER BY PC.CUSTOMER_ID"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]
												, (int)ServiceCodeDefine.ServiceCode.ExCloudBackup
												, (int)ServiceCodeDefine.ServiceCode.ExCloudBackupPcSupportPlus
												, date.ToDateTime()
												, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.view_MWS顧客情報]);

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

