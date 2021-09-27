//
// CloudBackupGetIO.cs
//
// クラウドバックアップPCA売上情報 データ取得クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/10/06 勝呂)
// 
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using System.Data;

namespace CommonLib.DB.SqlServer.CloudBackup
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
		/// <param name="settings">SQL Server接続情報</param>
		/// <returns>DataTable</returns>
		public static DataTable GetCloudBackupEarningsList(string goods, Span span, string connectStr)
		{
			string strSQL = string.Format(@"SELECT sykd_jbmn, sykd_jtan, sykd_scd, sykd_mkbn, sykd_tani, sykd_uribi"
										+ ", convert(int, sykd_rate) as 消費税率"
										+ ", convert(int, sum(sykd_suryo)) as 数量"
										+ " FROM {0}"
										+ " WHERE sykd_kingaku <> 0 AND sykd_uribi >= {1} AND sykd_uribi <= {2} AND sykd_scd IN ({3})"
										+ " GROUP BY sykd_jbmn, sykd_jtan, sykd_scd, sykd_mkbn, sykd_tani, sykd_uribi, sykd_rate"
										+ " ORDER BY sykd_jbmn, sykd_uribi, sykd_scd"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, span.Start.ToIntYMD()
										, span.End.ToIntYMD()
										, goods);

			return DatabaseAccess.SelectDatabase(strSQL, connectStr);
		}
	}
}
