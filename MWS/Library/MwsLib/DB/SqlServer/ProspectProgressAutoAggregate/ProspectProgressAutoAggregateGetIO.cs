﻿//
// ProspectProgressAutoAggregateGetIO.cs
//
// 見込進捗自動集計データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/08/04 勝呂)
// 
using MwsLib.BaseFactory;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Charlie;
using MwsLib.DB.SqlServer.Junp;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.ProspectProgressAutoAggregate
{
	public static class ProspectProgressAutoAggregateGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 売上予想情報の取得
		/// </summary>
		/// <param name="today">当日</param>
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable Select_売上予想(Date today, bool ct = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT"
										+ " choose(SL.区分No,"
													  + " '保守前受月額売上',"
													  + " 'ＰＣＡ－売上計上対象',"
													  + " 'ＷＷ受注残－ＶＰ',"
													  + " 'ＷＷ受注残－ＶＰ以外') as 売上区分"
											  + ", SL.部門コード as 部門コード"
											  + ", RTRIM(BM.emsb_str) as 部門名"
											  + ", SL.商品区分２ as 商品区分コード"
											  + ", KM.ems_str as 商品区分名"
											  + ", CAST(SL.金額 as integer) as 金額"
										+ " FROM ("
											+ "SELECT"
												   + " 2 as 区分No"
												  + ", RTRIM(D.sykd_jbmn) as 部門コード"
												  + ", SUM(D.sykd_kingaku) as 金額"
												  + ", M.sms_skbn2 as 商品区分２"
											+ " FROM {2} as D"
											+ " INNER JOIN {3} as M on M.sms_scd = D.sykd_scd"
											+ " WHERE D.sykd_kingaku <> 0"
											  + " AND D.sykd_uribi >= {0}"
											  + " AND D.sykd_uribi <= {1}"
											  + " AND NOT(M.sms_skbn2 = 5)"
											+ " GROUP BY D.sykd_jbmn, M.sms_skbn2"
										  + " UNION"
											+ " SELECT IIF(H.f販売種別 = 1, 3, 4)"
												  + ", RIGHT('00' + CAST(B.fPca部門コード as varchar), 3)"
												  + ", SUM(D.f提供価格)"
												  + ", D.f商品区分2"
											+ " FROM {4} as H"
											  + " INNER JOIN {5} as D on D.f受注番号 = H.f受注番号"
											  + " LEFT JOIN {6} as B on B.f支店コード = H.fBshCode3"
											+ " WHERE H.f売上計上日 IS NULL"
												+ " AND H.f納期 >= '{9}'"
											  + " AND H.f納期 <= '{10}'"
											  + " AND NOT(D.f商品区分2 = 5)"
											+ " GROUP BY H.f販売種別, B.fPca部門コード, D.f商品区分2"
										+ ") SL"
										+ " LEFT JOIN {7} as BM on BM.emsb_kbn = SL.部門コード"
										+ " LEFT JOIN {8} as KM on KM.ems_kbn = SL.商品区分２ AND KM.ems_id = 22"
										, today.FirstDayOfTheMonth().ToIntYMD()
										, today.LastDayOfTheMonth().ToIntYMD()
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注ヘッダ]
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注詳細]
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA部門マスタ]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA区分マスタ]
										, today.FirstDayOfTheMonth().ToString()
										, today.LastDayOfTheMonth().ToString());

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
		/// ES売上予想情報の取得
		/// </summary>
		/// <param name="start">計上開始月</param>
		/// <param name="end">計上終了月</param>
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable Select_ES売上予想(Date start, Date end, bool ct = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT"
										+ " 部門コード, 営業部名, 拠点コード, 拠点名, 顧客No, 顧客名, 受注番号, 受注承認日, 売上承認日, 納期, 売上金額, 計上月"
										+ " FROM"
										+ " ("
										+ "SELECT 部門コード, 営業部名, 拠点コード, 拠点名, 顧客No, 顧客名, 受注番号, 受注承認日, 売上承認日, 納期, 売上金額, 計上1年目 AS 計上月 FROM {0}"
										+ " UNION SELECT 部門コード, 営業部名, 拠点コード, 拠点名, 顧客No, 顧客名, 受注番号, 受注承認日, 売上承認日, 納期, 売上金額, 計上2年目 AS 計上月 FROM {0}"
										+ " UNION SELECT 部門コード, 営業部名, 拠点コード, 拠点名, 顧客No, 顧客名, 受注番号, 受注承認日, 売上承認日, 納期, 売上金額, 計上3年目 AS 計上月 FROM {0}"
										+ " UNION SELECT 部門コード, 営業部名, 拠点コード, 拠点名, 顧客No, 顧客名, 受注番号, 受注承認日, 売上承認日, 納期, 売上金額, 計上4年目 AS 計上月 FROM {0}"
										+ " UNION SELECT 部門コード, 営業部名, 拠点コード, 拠点名, 顧客No, 顧客名, 受注番号, 受注承認日, 売上承認日, 納期, 売上金額, 計上5年目 AS 計上月 FROM {0}"
										+ " UNION SELECT 部門コード, 営業部名, 拠点コード, 拠点名, 顧客No, 顧客名, 受注番号, 受注承認日, 売上承認日, 納期, 売上金額, 計上6年目 AS 計上月 FROM {0}"
										+ ") AS ES"
										+ " WHERE 計上月 >= '{1}' AND 計上月 <= '{2}'"
										+ " ORDER BY 部門コード, 顧客No, 計上月"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicES売上予想]
										, start.FirstDayOfTheMonth().ToYearMonth().ToString()
										, end.LastDayOfTheMonth().ToYearMonth().ToString());

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
		/// 予測連絡用ESの取得
		/// </summary>
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable Select_予測連絡用ES(bool ct = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT"
										+ " iif(f売上承認日 is null, LEFT(f納期, 7), LEFT(CONVERT(nvarchar, f売上承認日, 111), 7)) as 売上月"
										+ ", H.f受注番号 as 受注番号, CONVERT(nvarchar, f受注日, 111) as 受注日, f販売先コード as 販売先コード"
										+ ", fユーザーコード as ユーザーコード, f販売先 as 販売先, fユーザー as ユーザー, CONVERT(int, f受注金額) as 受注金額"
										+ ", f件名 as 件名, f納期 as 納期, fリプレース区分 as リプレース区分, fリプレース as リプレース, f担当者コード as 担当者コード"
										+ ", f担当者名 as 担当者名, fBshCode2 as BshCode2, fBshCode3 as BshCode3, f担当支店名 as 担当支店名, CONVERT(nvarchar, f受注承認日, 111) as 受注承認日"
										+ ", CONVERT(nvarchar, f売上承認日, 111) as 売上承認日, f請求区分 as 請求区分, f販売店コード as 販売店コード, f販売店 as 販売店"
										+ ", f販売種別 as 販売種別, fSV利用開始年月 as 課金開始日, fSV利用終了年月 as 課金終了日"
										+ " FROM {0} AS H"
										+ " LEFT JOIN {1} AS D ON H.f受注番号 = D.f受注番号"
										+ " LEFT JOIN {2} AS U ON H.fユーザーコード = U.顧客No"
										+ " WHERE f商品コード = '{3}' AND f販売種別 = 1"
										+ " ORDER BY 売上月, H.f受注番号"
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注ヘッダ]
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注詳細]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー4]
										, PcaGoodsIDDefine.PaletteES_2019);

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
		/// 予測連絡用まとめ（WonderWeb起票分）の取得
		/// </summary>
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable Select_予測連絡用まとめ_WW(bool ct = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT"
										+ " iif(f売上承認日 is null, LEFT(f納期, 7), LEFT(CONVERT(nvarchar, f売上承認日, 111), 7)) AS 売上月"
										+ ", U.営業部コード AS 営業部コード, U.営業部名 AS 営業部名, U.拠点コード AS 拠点コード, U.拠点名 AS 拠点名, f担当者コード AS 担当者コード"
										+ ", f担当者名 AS 担当者, H.f受注番号 AS 受注番号, fユーザーコード AS 顧客No, fユーザー AS 顧客名, f商品コード AS 商品コード, f数量 AS 数量"
										+ ", fSV利用開始年月 AS 課金開始日, fSV利用終了年月 AS 課金終了日, CONVERT(int, f標準価格) AS 金額"
										+ " FROM {0} AS H"
										+ " LEFT JOIN {1} AS D ON H.f受注番号 = D.f受注番号"
										+ " LEFT JOIN {2} AS U ON H.fユーザーコード = U.顧客No"
										+ " WHERE f販売種別 = 4 AND (f商品コード = '{3}' OR f商品コード = '{4}' OR f商品コード = '{5}' OR f商品コード = '{6}' OR f商品コード = '{7}')"
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注ヘッダ]
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注詳細]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー4]
										, PcaGoodsIDDefine.Matome12
										, PcaGoodsIDDefine.Matome24
										, PcaGoodsIDDefine.Matome36
										, PcaGoodsIDDefine.Matome48
										, PcaGoodsIDDefine.Matome60);

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
		/// 予測連絡用まとめ（契約情報）の取得
		/// </summary>
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable Select_予測連絡用まとめ_契約情報(bool ct = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT"
										+ " LEFT(CONVERT(NVARCHAR, EOMonth(H.fContractStartDate, -1), 111), 7) As 売上月, U.営業部コード AS 営業部コード, U.営業部名 AS 営業部名"
										+ ", U.拠点コード AS 拠点コード, U.拠点名 AS 拠点名, iif(U.営業担当者コード is null, '', U.営業担当者コード) AS 担当者コード"
										+ ", iif(U.営業担当者名 is null, '', U.営業担当者名) AS 担当者, 0 AS 受注番号, H.fCustomerID AS 顧客No, U.顧客名 AS 顧客名, H.fGoodsID as 商品コード"
										+ ", 1 AS 数量, LEFT(CONVERT(NVARCHAR, H.fContractStartDate, 111), 7) AS 課金開始日, iif(H.fBillingEndDate is null, '', LEFT(CONVERT(NVARCHAR, H.fBillingEndDate, 111), 7)) AS 課金終了日"
										+ ", fTotalAmount AS 金額"
										+ " FROM {0} AS H"
										+ " LEFT JOIN {1} AS U ON H.fCustomerID = U.顧客No"
										+ " WHERE H.fContractType = 'まとめ'"
										, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_CONTRACT_HEADER]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー4]);

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