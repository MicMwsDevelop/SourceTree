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
					string strSQL = string.Format(@"SELECT PC.fCustomerID as CUSTOMER_ID, V.顧客名 AS CLINIC_NAME, PC.fBillingStartDate AS PC_START_DATE, PC.fBillingEndDate AS PC_END_DATE"
													+ ", CUI.USE_START_DATE AS CL_START_DATE, CUI.USE_END_DATE AS CL_END_DATE"
													+ " FROM {0} as PC"
													+ " INNER JOIN {1} as CUI on PC.fCustomerID = CUI.CUSTOMER_ID"
													+ " INNER JOIN {2} as V on PC.fCustomerID = V.顧客No"
													+ " WHERE (PC.fServiceId = {3} OR PC.fServiceId = {4}) AND PC.fBillingStartDate <= '{5}' AND PC.fBillingEndDate >= '{5}'"
													+ " AND CUI.SERVICE_ID = {6} AND CUI.USE_START_DATE <= '{5}' AND CUI.USE_END_DATE >= '{5}' AND CUI.PAUSE_END_STATUS = '0'"
													+ " ORDER BY CUSTOMER_ID"
													, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_PCCSUPPORT]
													, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION]
													, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.view_MWS顧客情報]
													, (int)ServiceCodeDefine.ServiceCode.PcSafetySupportPlus3
													, (int)ServiceCodeDefine.ServiceCode.PcSafetySupportPlus1
													, date.ToDateTime()
													, (int)ServiceCodeDefine.ServiceCode.ExCloudBackup);

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

