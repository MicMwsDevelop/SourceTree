//
// CloudDataBankSaleGetIO.cs
//
// クラウドデータバンクPCA売上情報 データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/03/06 勝呂)
// 
using MwsLib.Common;
using MwsLib.DB.SqlServer.Junp;
using System.Data;
using System.Data.SqlClient;
using MwsLib.DB.SqlServer.Charlie;

namespace MwsLib.DB.SqlServer.CloudDataBankStockDataOutput
{
	public static class CloudDataBankSaleGetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 先月のクラウドデータバンクPCA売上情報リストの取得
		/// </summary>
		/// <param name="date">処理日付</param>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>DataTable</returns>
		public static DataTable GetCloudDataBankSaleList(Date date, bool sqlsv2)
		{
			DataTable result = null;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(sqlsv2)))
			{
				try
				{
					// 接続
					con.Open();

					string strSQL = string.Format(@"SELECT"
					+ " 仕入先 AS 仕入先コード"
					+ ", sykd_jbmn AS 部門コード"
					+ ", sykd_jtan AS 担当者コード"
					+ ", 仕入商品コード"
					+ ", sms_mei AS 商品名"
					+ ", SUM(convert(int, sykd_suryo)) AS 数量"
					+ ", sykd_tani AS 単位"
					+ ", 仕入価格"
					+ ", sykd_uribi AS 売上日"
					+ ", 仕入フラグ"
					+ ", convert(int, sykd_rate) AS 消費税率"
					+ " FROM {0} as D"
					+ " INNER JOIN {1} AS G ON D.sykd_scd = G.商品コード"
					+ " INNER JOIN {2} AS M ON G.仕入商品コード = M.sms_scd"
					+ " WHERE sykd_kingaku <> 0 AND sykd_uribi >= {3} AND sykd_uribi <= {4}"
					+ " GROUP BY 仕入先, sykd_jbmn, sykd_jtan, 仕入商品コード, sykd_mkbn, sms_mei, sykd_tani, 仕入価格, sykd_uribi, 仕入フラグ, sykd_rate"
					+ " ORDER BY sykd_jbmn, sykd_uribi, 仕入フラグ, 仕入商品コード"
					, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
					, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.M_クラウドデータバンク商品]
					, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]
					, SqlServerHelper.FirstDayOfLasMonthToIntYMD(date)
					, SqlServerHelper.LastDayOfLasMonthToIntYMD(date));

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
