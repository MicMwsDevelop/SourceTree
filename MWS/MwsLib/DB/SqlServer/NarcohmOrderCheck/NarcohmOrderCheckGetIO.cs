using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.NarcohmOrderCheck
{
    public static class NarcohmOrderCheckGetIO
    {
        //////////////////////////////////////////////////////////////////
        /// JunpDB
        //////////////////////////////////////////////////////////////////

        /// <summary>
        /// 医院情報の取得
        /// </summary>
        /// <param name="tel">電話番号</param>
        /// <param name="sqlsv2">CT環境</param>
        /// <returns>レコード数</returns>
        public static DataTable GetCustomerInfo(string tel, bool sqlsv2 = false)
        {
            DataTable result = null;
            using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(sqlsv2)))
            {
                try
                {
                    // 接続
                    con.Open();

                    string strSQL = string.Format(@"SELECT"
                                                + " 顧客No"
                                                + ", 得意先No"
                                                + ", MWS_ID"
                                                + ", 顧客名１ + 顧客名２ AS 顧客名"
                                                + ", 郵便番号"
                                                + ", 住所１ + 住所２ AS 住所"
                                                + ", 電話番号"
                                                + ", メールアドレス"
                                                + ", 営業部コード"
                                                + ", 営業部名"
                                                + ", 拠点コード"
                                                + ", 拠点名"
                                                + ", 営業担当者コード"
                                                + ", 営業担当者名"
                                                + ", 終了フラグ"
                                                + " FROM JunpDB.dbo.vMic全ユーザー３"
                                                + " WHERE 電話番号 = '{0}'", tel);

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
		/// ナルコーム製品受注情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="goodsID">商品コード</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetNarcohmOrderInfo(int customerNo, string goodsID, bool sqlsv2 = false)
        {
            DataTable result = null;
            using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(sqlsv2)))
            {
                try
                {
                    // 接続
                    con.Open();

                    string strSQL = string.Format(@"SELECT"
                                                + " ヘッダ.f受注番号 AS 受注番号"
                                                + ", CAST(f受注日 AS date) AS 受注日"
                                                //+ ", fユーザーコード AS 顧客No"
                                                //+ ", fユーザー AS 医院名"
                                                //+ ", CAST(f受注金額 AS int) AS 受注金額"
                                                + ", f件名 AS 件名"
                                                //+ ", f担当支店名 AS 担当支店名"
                                                //+ ", f担当者コード AS 担当者コード"
                                                //+ ", f担当者名 AS 担当者名"
                                                //+ ", 詳細.f表示順 AS 表示順"
                                                + ", 詳細.f商品コード AS 商品コード"
                                                + ", 詳細.f商品名 AS 商品名"
												+ ", CAST(詳細.[f標準価格] AS int) as 標準価格"
												+ ", 詳細.f数量 AS 数量"
                                                + ", CAST(詳細.f金額 AS int) AS 金額"
												+ " FROM JunpDB.dbo.tMih受注ヘッダ AS ヘッダ"
                                                + " LEFT JOIN JunpDB.dbo.tMih受注詳細 AS 詳細"
                                                + " ON ヘッダ.f受注番号 = 詳細.f受注番号"
                                                + " WHERE fユーザーコード = {0} AND 詳細.f商品コード = '{1}'"
                                                + " ORDER BY 詳細.f受注番号 DESC, 詳細.f表示順 ASC", customerNo, goodsID);

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
		/// ナルコーム製品申込番号の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetNarcohmApplicateID(int customerNo, bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT TOP (1) ApplicateID"
												+ " FROM CharlieDB.dbo.T_NARCOHM_APPLICATE_HEADER"
												+ " WHERE CustomerNo = {0} ORDER BY ApplicateID DESC", customerNo);

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
		/// ナルコーム製品申込ヘッダ情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetNarcohmApplicateHeader(int customerNo, bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT * FROM CharlieDB.dbo.T_NARCOHM_APPLICATE_HEADER"
												+ " WHERE CustomerNo = {0} ORDER BY ApplicateID DESC", customerNo);

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
		/// ナルコーム製品申込詳細情報の取得
		/// </summary>
		/// <param name="applicateID">顧客No</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetNarcohmApplicateDetail(int applicateID, bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT * FROM CharlieDB.dbo.T_NARCOHM_APPLICATE_DETAIL"
												+ " WHERE ApplicateNo = {0} ORDER BY ApplicateDetailID ASC", applicateID);

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
		/// ナルコーム製品申込ヘッダ情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetNarcohmApplicateHeaderList(bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT * FROM CharlieDB.dbo.T_NARCOHM_APPLICATE_HEADER ORDER BY ApplicateID ASC";

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
		/// ナルコーム製品申込詳細情報の取得
		/// </summary>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetNarcohmApplicateDetailList(bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = @"SELECT * FROM CharlieDB.dbo.T_NARCOHM_APPLICATE_DETAIL ORDER BY ApplicateID ASC, SeqNo ASC";

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
		/// ナルコーム製品標準価格の取得
		/// </summary>
		/// <param name="goodsCode">商品ID</param>
		/// <param name="sqlsv2">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetNarcohmProductPrice(string goodsCode, bool sqlsv2 = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT 標準価格"
												+ " FROM CharlieDB.dbo.PCA商品マスタ参照ビュー"
												+ " WHERE 商品ID = '{0}'", goodsCode);

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
