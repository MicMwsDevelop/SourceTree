//
// PcSupportManagerGetIO.cs
//
// PC安心サポート管理 データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/11/19 勝呂)
// 
using MwsLib.BaseFactory.PcSupportManager;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.PcSupportManager
{
	/// <summary>
	/// PC安心サポート管理 データ取得クラス
	/// </summary>
	public static class PcSupportManagerGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 受注情報リストの取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetOrderInfoList(bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT"
									 + " OH.f受注番号 AS 受注番号"
									 + ", fユーザーコード AS 顧客No"
									 + ", fユーザー AS 医院名"
									 + ", f商品コード AS 商品コード"
									 + ", f商品名 AS 商品名"
									 + ", f標準価格 AS 料金"
									 + ", fBshCode3 AS 拠店ID"
									 + ", f担当支店名 AS 拠点名"
									 + ", f担当者コード AS 担当者ID"
									 + ", f担当者名 AS 担当者名"
									 + ", f受注日 AS 受注日"
									 + ", f受注承認日 AS 受注承認日"
									 + ", f備考 AS 備考"
									 + " FROM tMih受注ヘッダ AS OH"
									 + " LEFT JOIN tMih受注詳細 AS OD"
									 + " ON OH.f受注番号 = OD.f受注番号"
									 + " WHERE (OD.f商品コード = '{0}' or OD.f商品コード = '{1}')"
									 + " ORDER BY OH.f受注番号 ASC", PcSupportGoodsInfo.PC_SUPPORT1_GOODS_ID, PcSupportGoodsInfo.PC_SUPPORT3_GOODS_ID);

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
		/// 受注情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetOrderInfo(int customerNo, bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT TOP 1"
									 + " OH.f受注番号 AS 受注番号"
									 + ", fユーザーコード AS 顧客No"
									 + ", fユーザー AS 医院名"
									 + ", f商品コード AS 商品コード"
									 + ", f商品名 AS 商品名"
									 + ", f標準価格 AS 料金"
									 + ", fBshCode3 AS 拠店ID"
									 + ", f担当支店名 AS 拠点名"
									 + ", f担当者コード AS 担当者ID"
									 + ", f担当者名 AS 担当者名"
									 + ", f受注日 AS 受注日"
									 + ", f受注承認日 AS 受注承認日"
									 + ", f備考 AS 備考"
									 + " FROM tMih受注ヘッダ AS OH"
									 + " LEFT JOIN tMih受注詳細 AS OD"
									 + " ON OH.f受注番号 = OD.f受注番号"
									 + " WHERE OH.fユーザーコード = {0} AND (OD.f商品コード = '{1}' or OD.f商品コード = '{2}')"
									 + " ORDER BY OH.f受注番号 DESC", customerNo, PcSupportGoodsInfo.PC_SUPPORT1_GOODS_ID, PcSupportGoodsInfo.PC_SUPPORT3_GOODS_ID);

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
		/// 製品サポート情報ソフト保守情報の取得
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
								+ ", fhsSメンテ契約備考1"
								+ " FROM tMik保守契約";
					if (0 < customerNo)
					{
						strSQL += string.Format(" WHERE fhsCliMicID = {0}", customerNo);
					}
					else
					{
						strSQL += " ORDER BY fhsCliMicID ASC";
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

					string strSQL = string.Format(@"SELECT sms_scd, sms_mei, sms_hyo FROM JunpDB.dbo.vMicPCA商品マスタ"
									+ " WHERE sms_scd = '{0}' OR sms_scd = '{1}'"
									+ " ORDER BY sms_scd ASC", PcSupportGoodsInfo.PC_SUPPORT3_GOODS_ID, PcSupportGoodsInfo.PC_SUPPORT1_GOODS_ID);

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

					string strSQL = @"SELECT * FROM T_PC_SUPPORT_CONTROL";
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
		/// 顧客メールアドレスの取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns></returns>
		public static DataTable GetCustomerMailAddress(bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT 顧客ＩＤ, メールアドレス FROM 顧客マスタ参照ビュー ORDER BY 顧客ＩＤ ASC";
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
		/// 拠店情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns></returns>
		public static DataTable GetBranchInfo(bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT 支店ＩＤ, 支店名, 支店メールアドレス FROM 支店情報参照ビュー WHERE 支店メールアドレス is not null AND 支店ＩＤ <> '32' ORDER BY 支店ＩＤ ASC";
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
