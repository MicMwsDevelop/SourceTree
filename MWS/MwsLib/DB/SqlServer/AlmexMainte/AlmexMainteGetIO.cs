//
// AlmexMainteGetIO.cs
//
// アルメックス保守サービス データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/11/24 勝呂)
// 
using MwsLib.BaseFactory.Junp.Table;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Junp;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.AlmexMainte
{
	public static class AlmexMainteGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// アプリケーション情報からアルメックス保守サービスの更新対象医院の取得
		/// </summary>
		/// <param name="saleDate">売上日</param>
		/// <param name="ct">CT環境</param>
		/// <returns>レコード数</returns>
		public static DataTable GetAlmexMainteEarningsOut(Date saleDate, bool ct = false)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpConnectionString(ct)))
			{
				string ym = saleDate.ToYearMonth().GetNormalString();

				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT"
							+ " U.顧客No as f顧客No"
							+ ", U.顧客名１ + U.顧客名２ as f顧客名"
							+ ", U.得意先No as f得意先コード"
							+ ", U.請求先コード as f請求先コード"
							+ ", A.fai保守契約開始 as f保守開始月"
							+ ", A.fai保守契約終了 as f保守終了月"
							+ ", A.faiアプリケーションNo as fアプリケーションNo"
							+ ", A.faiアプリケーション名 as fアプリケーション名"
							+ ", C.fcm名称 as fcm名称"
							+ ", '月' as f更新単位"
							+ ", S.sms_scd as f商品コード"
							+ ", S.sms_mei as f商品名"
							+ ", convert(int, S.sms_hyo) as f標準価格"
							+ ", convert(int, S.sms_gen) as f原単価"
							+ ", S.sms_tani as f単位"
							+ ", B.fPca部門コード as fPCA部門コード"
							+ ", B.fPca倉庫コード as fPCA倉庫コード"
							+ ", B.f担当者コード as fPCA担当者コード"
							+ " FROM {0} as U"
							+ " INNER JOIN {1} as A on A.faiCliMicID = U.顧客No"
							+ " INNER JOIN {2} as C on C.fcmコード = A.faiアプリケーション名"
							+ " INNER JOIN {3} as S on S.sms_scd = C.fcmサブコード"
							+ " INNER JOIN {4} as B on B.fBshCode3 = U.支店コード"
							+ " WHERE U.終了フラグ = '0' AND A.fai終了フラグ = '0' AND C.fcmコード種別 = '{5}' AND (C.fcmコード = '{6}' OR C.fcmコード = '{7}') AND A.fai保守契約終了 = '{8}'"
							+ " ORDER BY U.顧客No, S.sms_scd"
						, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー２]
						, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikアプリケーション情報]
						, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikコードマスタ]
						, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]
						, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]
						, tMikコードマスタ.fcmコード種別_ApplicationName
						, tMikコードマスタ.fcmコード_AlmexMainteCash
						, tMikコードマスタ.fcmコード_AlmexMainteCredit
						, ym);

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
		/// 指定期間のアルメックスPCA売上明細情報リストの取得
		/// </summary>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="span">検索期間</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>DataTable</returns>
		public static DataTable GetAlmexMainteEarningsList(string goods, Span span, bool ct)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT * FROM {0}"
												+ " WHERE sykd_kingaku <> 0 AND sykd_uribi >= {1} AND sykd_uribi <= {2} AND sykd_scd IN ({3})"
												+ " ORDER BY sykd_jbmn, sykd_uribi, sykd_scd"
												, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
												, span.Start.ToIntYMD()
												, span.End.ToIntYMD()
												, goods);

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
