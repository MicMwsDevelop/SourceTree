//
// CloudBackupGetIO.cs
//
// クラウドバックアップPCA売上情報 データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/10/06 勝呂)
// 
using MwsLib.Common;
using MwsLib.DB.SqlServer.Junp;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.CloudBackup
{
	public static class CloudBackupGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 指定期間のクラウドバックアップPCA売上明細情報リストの取得
		/// </summary>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="span">検索期間</param>
		/// <param name="ct">CT環境かどうか？</param>
		/// <returns>DataTable</returns>
		public static DataTable GetCloudBackupEarningsList(string goods, Span span, bool ct)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(ct)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT sykd_jbmn, sykd_jtan, sykd_scd, sykd_mkbn, sykd_tani, sykd_uribi"
												+ " , convert(int, sykd_rate) as 消費税率"
												+ " , convert(int, sum(sykd_suryo)) as 数量"
												+ " FROM {0}"
												+ " WHERE sykd_kingaku <> 0 AND sykd_uribi >= {1} AND sykd_uribi <= {2} AND sykd_scd IN ({3})"
												+ " GROUP BY sykd_jbmn, sykd_jtan, sykd_scd, sykd_mkbn, sykd_tani, sykd_uribi, sykd_rate"
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
