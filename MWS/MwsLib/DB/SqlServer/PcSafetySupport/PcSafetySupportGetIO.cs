using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MwsLib.Common;

namespace MwsLib.DB.SqlServer.PcSafetySupport
{
	public static class PcSafetySupportGetIO
	{
		/// <summary>
		/// ソフト保守情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetSoftMaintenanceContract(bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT fhsCliMicID"
								+ ", fhsS保守"
								+ ", fhsS契約書回収年月"
								+ ", fhsS契約年数"
								+ ", fhsSメンテ料金"
								+ ", fhsSメンテ契約開始"
								+ ", fhsSメンテ契約終了"
								+ " FROM tMik保守契約"
								+ " OREDER BY fhsCliMicID ASC";
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
		/// PC安心サポート管理情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <param name="customerID">顧客ID</param>
		/// <returns>レコード数</returns>
		public static DataTable GetPcSupportControl(string customerID = "", bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT * FROM T_PC_SUPPORT_CONTROL";
					if (0 < customerID.Length)
					{
						strSQL += string.Format("WHERE CUSTOMER_ID = '{0}'", customerID);
					}
					else
					{
						strSQL += " ORDER BY CUSTOMER_ID ASC";
					}
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
