using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.PcSafetySupport
{
	public static class PcSafetySupportGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// ソフト保守情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetSoftMaintenanceContract(int customerNo = 0, bool sqlsv2 = false)
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
								+ " FROM tMik保守契約";
					if (0 < customerNo)
					{
						strSQL += string.Format(" WHERE fhsCliMicID = {0}", customerNo);
					}
					else
					{
						strSQL += " OREDER BY fhsCliMicID ASC";
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

		/// <summary>
		/// 拠店従業員情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetBranchEmployeeInfo(bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT fUsrID, fUsrName, fBshCode2, fBshName2, fBshCode3, fBshName3 FROM JunpDB.dbo.vMic担当者"
									+ " WHERE 社員フラグ = 1 AND (fBshCode2 = '50' OR fBshCode2 = '60' OR fBshCode2 = '70' OR fBshCode2 = '75' OR fBshCode2 = '80')"
									+ " ORDER BY fBshCode2 ASC, fBshCode3 DESC, fUsrID ASC";

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
		/// PC安心サポート商品情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetPcSupportGoodsInfo(bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT sms_scd, sms_mei, sms_hyo FROM JunpDB.dbo.vMicPCA商品マスタ"
									+ " WHERE sms_scd = '001871' OR sms_scd = '001872'"
									+ " ORDER BY sms_scd ASC";
							
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
		/// PC安心サポート管理情報の取得
		/// </summary>
		/// <param name="orderNo">受注No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetPcSupportControl(string orderNo = "", bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT PSC.*, CS.顧客名１ + CS.顧客名２ AS CLINIC_NAME FROM T_PC_SUPPORT_CONTROL AS PSC LEFT JOIN 顧客マスタ参照ビュー AS CS ON PSC.CUSTOMER_ID = CS.顧客ＩＤ";
					if (0 < orderNo.Length)
					{
						strSQL += string.Format(" WHERE ORDER_NO = '{0}'", orderNo);
					}
					else
					{
						strSQL += " ORDER BY ORDER_NO ASC";
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

		/// <summary>
		/// PC安心サポート送信メール情報の取得
		/// </summary>
		/// <param name="orderNo">受注No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetPcSupportMail(string orderNo = "", bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT * FROM T_PC_SUPPORT_MAIL";
					if (0 < orderNo.Length)
					{
						strSQL += string.Format(" WHERE ORDER_NO = '{0}'", orderNo);
					}
					else
					{
						strSQL += " ORDER BY ORDER_NO ASC";
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

		/// <summary>
		/// 医院名の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetClinicName(int customerNo, bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT 顧客名１ + 顧客名２ AS CLINIC_NAME FROM 顧客マスタ参照ビュー WHERE 顧客ＩＤ = {0}", customerNo);
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
