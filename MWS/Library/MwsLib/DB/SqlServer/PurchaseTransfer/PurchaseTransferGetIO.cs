//
// PurchaseTransferGetIO.cs
//
// 仕入振替 データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/05/27 勝呂)
// 
using MwsLib.Common;
using MwsLib.DB.SqlServer.Junp;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace MwsLib.DB.SqlServer.PurchaseTransfer
{
	/// <summary>
	/// 仕入振替 データ取得クラス
	/// </summary>
	public static class PurchaseTransferGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 対象月全仕入れ明細の取得
		/// </summary>
		/// <param name="span">検索期間</param>
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetIo対象月全仕入れ明細(Span span, bool ct = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT"
													+ " NYKD.nykd_hoho"
													+ ", NYKD.nykd_flid"
													+ ", NYKD.nykd_denku"
													+ ", NYKD.nykd_uribi"
													+ ", NYKD.nykd_seibi"
													+ ", NYKD.nykd_denno"
													+ ", NYKD.nykd_tcd"
													+ ", RMS.rms_mei1"
													+ ", RMS.rms_tanmei"
													+ ", NYKD.nykd_jbmn"
													+ ", NYKD.nykd_jtan"
													+ ", NYKD.nykd_tekcd"
													+ ", NYKD.nykd_tekmei"
													+ ", NYKD.nykd_scd"
													+ ", NYKD.nykd_mkbn"
													+ ", NYKD.nykd_mei"
													+ ", NYKD.nykd_ku"
													+ ", NYKD.nykd_souko"
													+ ", NYKD.nykd_iri"
													+ ", NYKD.nykd_hako"
													+ ", NYKD.nykd_suryo"
													+ ", NYKD.nykd_tani"
													+ ", NYKD.nykd_tanka"
													+ ", NYKD.nykd_kingaku"
													+ ", NYKD.nykd_zei"
													+ ", NYKD.nykd_uchi"
													+ ", NYKD.nykd_tax"
													+ ", NYKD.nykd_komi"
													+ ", NYKD.nykd_biko"
													+ ", NYKD.nykd_rate"
													+ " FROM {0} AS NYKD"
													+ " INNER JOIN {1} AS RMS ON NYKD.nykd_tcd = RMS.rms_tcd"
													+ " WHERE NYKD.nykd_scd Between '000000' AND '999999' AND NYKD.nykd_souko <> '99' AND NYKD.nykd_uribi Between {2} AND {3} AND NYKD.nykd_flid = 0"
													+ " ORDER BY NYKD.nykd_uribi, NYKD.nykd_denno, NYKD.nykd_eda"
									, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA仕入データ]
									, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA仕入先マスタ]
									, span.Start.ToIntYMD()
									, span.End.ToIntYMD());

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
		/// 対象月社内仕入れ明細
		/// </summary>
		/// <param name="span">検索期間</param>
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetIo対象月社内仕入れ明細(Span span, bool ct = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT DISTINCT"
										+ "  対象月全仕入れ明細.nykd_hoho"
										+ ", 対象月全仕入れ明細.nykd_flid"
										+ ", 対象月全仕入れ明細.nykd_denku"
										+ ", 対象月全仕入れ明細.nykd_uribi"
										+ ", 対象月全仕入れ明細.nykd_seibi"
										+ ", 対象月全仕入れ明細.nykd_denno"
										+ ", 対象月全仕入れ明細.nykd_tcd"
										+ ", 対象月全仕入れ明細.rms_mei1"
										+ ", 対象月全仕入れ明細.rms_tanmei"
										+ ", IIf(fPca倉庫コード is null, convert(smallint, 対象月全仕入れ明細.nykd_souko), fPca部門コード) AS nykd_jbmn"
										+ ", 対象月全仕入れ明細.nykd_jtan"
										+ ", 対象月全仕入れ明細.nykd_tekcd"
										+ ", 対象月全仕入れ明細.nykd_tekmei"
										+ ", 対象月全仕入れ明細.nykd_scd"
										+ ", 対象月全仕入れ明細.nykd_mkbn"
										+ ", 対象月全仕入れ明細.nykd_mei"
										+ ", 対象月全仕入れ明細.nykd_ku"
										+ ", 対象月全仕入れ明細.nykd_souko"
										+ ", 対象月全仕入れ明細.nykd_iri"
										+ ", 対象月全仕入れ明細.nykd_hako"
										+ ", 対象月全仕入れ明細.nykd_suryo"
										+ ", 対象月全仕入れ明細.nykd_tani"
										+ ", 対象月全仕入れ明細.nykd_tanka"
										+ ", 対象月全仕入れ明細.nykd_kingaku"
										+ ", 対象月全仕入れ明細.nykd_zei"
										+ ", 対象月全仕入れ明細.nykd_uchi"
										+ ", 対象月全仕入れ明細.nykd_tax"
										+ ", 対象月全仕入れ明細.nykd_komi"
										+ ", 対象月全仕入れ明細.nykd_biko"
										+ ", 対象月全仕入れ明細.nykd_rate"
										+ " FROM ("
										+ " SELECT"
										+ " NYKD.nykd_hoho"
										+ ", NYKD.nykd_flid"
										+ ", NYKD.nykd_denku"
										+ ", NYKD.nykd_uribi"
										+ ", NYKD.nykd_seibi"
										+ ", NYKD.nykd_denno"
										+ ", NYKD.nykd_tcd"
										+ ", RMS.rms_mei1"
										+ ", RMS.rms_tanmei"
										+ ", NYKD.nykd_jbmn"
										+ ", NYKD.nykd_jtan"
										+ ", NYKD.nykd_tekcd"
										+ ", NYKD.nykd_tekmei"
										+ ", NYKD.nykd_scd"
										+ ", NYKD.nykd_mkbn"
										+ ", NYKD.nykd_mei"
										+ ", NYKD.nykd_ku"
										+ ", NYKD.nykd_souko"
										+ ", NYKD.nykd_iri"
										+ ", NYKD.nykd_hako"
										+ ", NYKD.nykd_suryo"
										+ ", NYKD.nykd_tani"
										+ ", NYKD.nykd_tanka"
										+ ", NYKD.nykd_kingaku"
										+ ", NYKD.nykd_zei"
										+ ", NYKD.nykd_uchi"
										+ ", NYKD.nykd_tax"
										+ ", NYKD.nykd_komi"
										+ ", NYKD.nykd_biko"
										+ ", NYKD.nykd_rate"
										+ " FROM {0} AS NYKD"
										+ " INNER JOIN {1} AS RMS ON NYKD.nykd_tcd = RMS.rms_tcd"
										+ " WHERE NYKD.nykd_scd Between '000000' AND '999999' AND NYKD.nykd_souko <> '99' AND NYKD.nykd_uribi Between {2} AND {3} AND NYKD.nykd_flid = 0"
										+ ") as 対象月全仕入れ明細"
										+ " LEFT JOIN {4} AS 支店情報 ON convert(smallint, 対象月全仕入れ明細.nykd_souko) = 支店情報.fPca倉庫コード"
										+ " WHERE 対象月全仕入れ明細.nykd_denku <> '5' AND 対象月全仕入れ明細.nykd_suryo <> 0 AND (対象月全仕入れ明細.nykd_tcd Between '000201' AND '000250' OR 対象月全仕入れ明細.nykd_tcd = '000275' OR 対象月全仕入れ明細.nykd_tcd = '000262' OR 対象月全仕入れ明細.nykd_tcd = '000261')"
										+ " ORDER BY 対象月全仕入れ明細.nykd_uribi, 対象月全仕入れ明細.nykd_denno, 対象月全仕入れ明細.nykd_tcd"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA仕入データ]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA仕入先マスタ]
										, span.Start.ToIntYMD()
										, span.End.ToIntYMD()
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]);

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
		/// 対象月仕入明細貯蔵品の取得
		/// </summary>
		/// <param name="span">検索期間</param>
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetIo対象月仕入明細貯蔵品(Span span, bool ct = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT"
													+ " NYKD.nykd_hoho"
													+ ", NYKD.nykd_flid"
													+ ", NYKD.nykd_denku"
													+ ", NYKD.nykd_uribi"
													+ ", NYKD.nykd_seibi"
													+ ", NYKD.nykd_denno"
													+ ", NYKD.nykd_tcd"
													+ ", RMS.rms_mei1"
													+ ", RMS.rms_tanmei"
													+ ", NYKD.nykd_jbmn"
													+ ", NYKD.nykd_jtan"
													+ ", NYKD.nykd_tekcd"
													+ ", NYKD.nykd_tekmei"
													+ ", NYKD.nykd_scd"
													+ ", NYKD.nykd_mkbn"
													+ ", NYKD.nykd_mei"
													+ ", NYKD.nykd_ku"
													+ ", NYKD.nykd_souko"
													+ ", NYKD.nykd_iri"
													+ ", NYKD.nykd_hako"
													+ ", NYKD.nykd_suryo"
													+ ", NYKD.nykd_tani"
													+ ", NYKD.nykd_tanka"
													+ ", NYKD.nykd_kingaku"
													+ ", NYKD.nykd_zei"
													+ ", NYKD.nykd_uchi"
													+ ", NYKD.nykd_tax"
													+ ", NYKD.nykd_komi"
													+ ", NYKD.nykd_biko"
													+ ", NYKD.nykd_rate"
													+ " FROM {0} AS NYKD"
													+ " INNER JOIN {1} AS RMS ON NYKD.nykd_tcd = RMS.rms_tcd"
													+ " WHERE NYKD.nykd_uribi Between {2} AND {3} AND NYKD.nykd_scd Like 'A%' OR NYKD.nykd_scd Like 'B%' OR NYKD.nykd_scd Like 'C%' OR NYKD.nykd_scd Like 'D%' OR NYKD.nykd_scd Like 'E%'"
													+ " ORDER BY NYKD.nykd_uribi, NYKD.nykd_denno, NYKD.nykd_eda"
									, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA仕入データ]
									, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA仕入先マスタ]
									, span.Start.ToIntYMD()
									, span.End.ToIntYMD());

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
		/// 売上明細月次の取得
		/// </summary>
		/// <param name="whereGoods">palette商品コード列</param>
		/// <param name="span">検索期間</param>
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetIo売上明細月次(string whereGoods, Span span, bool ct = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT * FROM {0} WHERE sykd_uribi between {1} AND {2} AND sykd_scd IN ({3}) ORDER BY sykd_jbmn, sykd_uribi, sykd_scd"
													, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
													, span.Start.ToIntYMD()
													, span.End.ToIntYMD()
													, whereGoods);

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

		/*
				/// <summary>
				/// 社内使用分出荷明細の取得
				/// </summary>
				/// <param name="customerNo">顧客No</param>
				/// <param name="sqlsv2">CT環境</param>
				/// <returns>レコード数</returns>
				public static DataTable GetIo社内使用分出荷明細(int customerNo, bool sqlsv2 = false)
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
											 + " FROM {0} AS OH"
											 + " LEFT JOIN {1} AS OD ON OH.f受注番号 = OD.f受注番号"
											 + " WHERE OH.fユーザーコード = {2} AND (OD.f商品コード = '{3}' or OD.f商品コード = '{4}')"
											 + " ORDER BY OH.f受注番号 DESC"
											, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA出荷データ]
											, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注詳細]
											, customerNo
											, PcSupportGoodsInfo.PC_SUPPORT1_GOODS_ID
											, PcSupportGoodsInfo.PC_SUPPORT3_GOODS_ID);

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

							string strSQL = string.Format(@"SELECT"
										+ " fhsCliMicID"
										+ ", fhsS保守"
										+ ", fhsS契約書回収年月"
										+ ", fhsS契約年数"
										+ ", fhsSメンテ料金"
										+ ", fhsSメンテ契約開始"
										+ ", fhsSメンテ契約終了"
										+ ", fhsSメンテ契約備考1"
										+ " FROM {0}"
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMik保守契約]);
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

							string strSQL = string.Format(@"SELECT"
											+ " fUsrID"
											+ ", fUsrName"
											+ ", fBshCode2"
											+ ", fBshName2"
											+ ", fBshCode3"
											+ ", fBshName3"
											+ " FROM {0}"
											+ " WHERE 社員フラグ = 1 AND (fBshCode2 = '50' OR fBshCode2 = '60' OR fBshCode2 = '70' OR fBshCode2 = '75' OR fBshCode2 = '80')"
											+ " ORDER BY fBshCode2 ASC, fBshCode3 DESC, fUsrID ASC"
											, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic担当者]);

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

							string strSQL = string.Format(@"SELECT"
											+ " sms_scd"
											+ ", sms_mei"
											+ ", sms_hyo"
											+ " FROM {0}"
											+ " WHERE sms_scd = '{0}' OR sms_scd = '{1}'"
											+ " ORDER BY sms_scd ASC"
											, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]
											, PcSupportGoodsInfo.PC_SUPPORT3_GOODS_ID
											, PcSupportGoodsInfo.PC_SUPPORT1_GOODS_ID);

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
		*/
	}
}
