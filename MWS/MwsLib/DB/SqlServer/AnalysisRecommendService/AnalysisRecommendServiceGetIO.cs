using MwsLib.BaseFactory;
using MwsLib.DB.SqlServer.Charlie;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.AnalysisRecommendService
{
	public static class AnalysisRecommendServiceGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// おまとめプランサービス契約情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>DataTable</returns>
		public static DataTable GetRecommendService(int customerNo, bool ct)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieWebConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT fCustomerID, fSERVICE_ID, fSERVICE_NAME"
												+ " FROM {0} as H"
												+ " LEFT JOIN {1} as D on H.fContractID = D.fContractID"
												+ " WHERE fContractType = 'まとめ' and fSERVICE_ID <> {2} and fCustomerID = {3}"
												+ " ORDER BY fSERVICE_ID"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_HEADER]
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_DETAIL]
												, (int)ServiceCodeDefine.ServiceCode.StandardPalette
												, customerNo);

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
		/// おまとめプランサービス契約情報リストの取得
		/// </summary>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>DataTable</returns>
		public static DataTable GetRecommendServiceList(bool ct)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieWebConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT fCustomerID, fSERVICE_ID, fSERVICE_NAME"
												+ " FROM {0} as H"
												+ " LEFT JOIN {1} as D on H.fContractID = D.fContractID"
												+ " WHERE fContractType = 'まとめ' and fSERVICE_ID <> {2}"
												+ " ORDER BY fCustomerID, fSERVICE_ID"
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_HEADER]
												, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_DETAIL]
												, (int)ServiceCodeDefine.ServiceCode.StandardPalette);

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
