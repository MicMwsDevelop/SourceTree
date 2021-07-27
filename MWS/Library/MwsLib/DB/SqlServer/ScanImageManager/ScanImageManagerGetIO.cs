//
// ScanImageManagerGetIO.cs
//
// 文書インデックス管理 データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/07/06 勝呂)
// 
using MwsLib.DB.SqlServer.Junp;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.ScanImageManager
{
	public static class ScanImageManagerGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 得意先番号に対する顧客情報の取得
		/// </summary>
		/// <param name="tokuisakiNo">得意先番号</param>
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetCustomerInfo(string tokuisakiNo, bool ct = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT [fkj得意先情報] as 得意先No, [fkjCliMicID] as 顧客No, [fCliName] AS 顧客名 FROM {0} as B"
													+ " LEFT JOIN {1} as C on B.[fkjCliMicID] = C.[fCliID]"
													+ " WHERE [fkj得意先情報] = '{2}'"
													+ " ORDER BY 得意先No ASC"
													, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik基本情報]
													, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]
													, tokuisakiNo);

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
		/// 顧客情報リストの取得
		/// </summary>
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetCustomerInfoList(bool ct = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT [fkj得意先情報] as 得意先No, [fkjCliMicID] as 顧客No, [fCliName] AS 顧客名 FROM {0} as B"
													+ " LEFT JOIN {1} as C on B.[fkjCliMicID] = C.[fCliID]"
													+ " WHERE LEN([fkj得意先情報]) > 0 AND [fkj得意先情報] is not null"
													+ " ORDER BY 得意先No ASC"
													, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik基本情報]
													, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]);

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
		/// 顧客情報リストの取得
		/// </summary>
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetIndexFileInfoList(bool ct = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT tMikPca得意先.fptCliMicID AS ＷＷ顧客Ｎｏ, tMikPca得意先.fpt得意先No AS 得意先No, fCliName + ' ' + fkj顧客名２ AS 医院名 FROM (tMikPca得意先 INNER JOIN tMik基本情報 ON tMikPca得意先.fptCliMicID = tMik基本情報.fkjCliMicID) INNER JOIN tClient ON tMikPca得意先.fptCliMicID = tClient.fCliID";
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
