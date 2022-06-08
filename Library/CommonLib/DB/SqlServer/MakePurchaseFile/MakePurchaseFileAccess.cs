//
// MakePurchaseFileAccess.cs
// 
// 仕入データ作成 データベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/01/07 勝呂)
// Ver1.01 新規作成(2022/04/04 勝呂):ナルコーム仕入データ作成時に数量０を除外する
//
using CommonLib.BaseFactory.Junp.View;
using CommonLib.BaseFactory.MakePurchaseFile;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.DB.SqlServer.MakePurchaseFile
{
	public static class MakePurchaseFileAccess
	{
		/// <summary>
		/// [charlieDB].[dbo].[TMP_Curline本体アプリ商品]の新規追加
		/// </summary>
		/// <param name="list">仕入商品情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoList_Curline本体アプリ商品(List<仕入商品情報> list, string connectStr)
		{
			List<SqlParameter[]> paramList = new List<SqlParameter[]>();
			foreach (仕入商品情報 data in list)
			{
				paramList.Add(data.GetInsertIntoParameters());
			}
			return DatabaseAccess.InsertIntoListDatabase(仕入商品情報.InsertIntoSqlString_Curline本体アプリ商品, paramList, connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[TMP_Microsoft365商品]の新規追加
		/// </summary>
		/// <param name="list">仕入商品情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoList_Microsoft365商品(List<仕入商品情報> list, string connectStr)
		{
			List<SqlParameter[]> paramList = new List<SqlParameter[]>();
			foreach (仕入商品情報 data in list)
			{
				paramList.Add(data.GetInsertIntoParameters());
			}
			return DatabaseAccess.InsertIntoListDatabase(仕入商品情報.InsertIntoSqlString_Microsoft365商品, paramList, connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[TMP_りすとん月額商品]の新規追加
		/// </summary>
		/// <param name="list">仕入商品情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoList_りすとん月額商品(List<仕入商品情報> list, string connectStr)
		{
			List<SqlParameter[]> paramList = new List<SqlParameter[]>();
			foreach (仕入商品情報 data in list)
			{
				paramList.Add(data.GetInsertIntoParameters());
			}
			return DatabaseAccess.InsertIntoListDatabase(仕入商品情報.InsertIntoSqlString_りすとん月額商品, paramList, connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[TMP_問心伝月額商品]の新規追加
		/// </summary>
		/// <param name="list">仕入商品情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoList_問心伝月額商品(List<仕入商品情報> list, string connectStr)
		{
			List<SqlParameter[]> paramList = new List<SqlParameter[]>();
			foreach (仕入商品情報 data in list)
			{
				paramList.Add(data.GetInsertIntoParameters());
			}
			return DatabaseAccess.InsertIntoListDatabase(仕入商品情報.InsertIntoSqlString_問心伝月額商品, paramList, connectStr);
		}

		/// <summary>
		/// [charlieDB].[dbo].[TMP_ナルコーム商品]の新規追加
		/// </summary>
		/// <param name="list">ナルコーム仕入商品情報</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoList_ナルコーム商品(List<ナルコーム仕入商品情報> list, string connectStr)
		{
			List<SqlParameter[]> paramList = new List<SqlParameter[]>();
			foreach (ナルコーム仕入商品情報 data in list)
			{
				paramList.Add(data.GetInsertIntoParameters());
			}
			return DatabaseAccess.InsertIntoListDatabase(ナルコーム仕入商品情報.InsertIntoSqlString, paramList, connectStr);
		}

		/// <summary>
		/// 7 りすとん月額仕入集計
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<仕入集計> Select_りすとん月額仕入集計(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
											+ " りすとん月額商品.仕入先"
											+ ", 売上明細月次.sykd_jbmn"
											+ ", 売上明細月次.sykd_jtan"
											+ ", りすとん月額商品.仕入商品コード AS sykd_scd"
											+ ", 売上明細月次.sykd_mkbn"
											+ ", SMS.sms_mei AS sykd_mei"
											+ ", SUM(CONVERT(int, sykd_suryo)) AS 数量"
											+ ", 売上明細月次.sykd_tani"
											+ ", りすとん月額商品.仕入価格 AS 評価単価"
											+ ", CONVERT(int, 売上明細月次.sykd_rate) AS sykd_rate"
											+ " FROM ("
											+ "("
												+ " SELECT"
												+ " sykd_jbmn"
												+ ", sykd_jtan"
												+ ", sykd_scd"
												+ ", sykd_mkbn"
												+ ", sykd_suryo"
												+ ", iif(sykd_tani = '', '月', sykd_tani) AS sykd_tani"
												+ ", sykd_rate"
												+ " FROM {0}"
												+ " WHERE sykd_uribi / 100 = {1}"
											+ ") AS 売上明細月次"
											+ " INNER JOIN"
											+ " ("
												+ " SELECT * FROM [charlieDB].[dbo].[TMP_りすとん月額商品]"
											+ ") AS りすとん月額商品 ON 売上明細月次.sykd_scd = りすとん月額商品.商品コード)"
											+ " INNER JOIN {2} AS SMS ON りすとん月額商品.仕入商品コード = SMS.sms_scd"
											+ " GROUP BY りすとん月額商品.仕入先, 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, りすとん月額商品.仕入商品コード, 売上明細月次.sykd_mkbn, SMS.sms_mei, 売上明細月次.sykd_tani, りすとん月額商品.仕入価格, 売上明細月次.sykd_rate"
											+ " HAVING SUM(CONVERT(int, sykd_suryo)) <> 0"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, ym.ToIntYM()
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]);
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 仕入集計.DataTableToList(dt);
		}

		/// <summary>
		/// 8 Microsoft365仕入集計
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<仕入集計> Select_Microsoft365仕入集計(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
											+ " Microsoft365商品.仕入先"
											+ ", 売上明細月次.sykd_jbmn"
											+ ", 売上明細月次.sykd_jtan"
											+ ", Microsoft365商品.仕入商品コード AS sykd_scd"
											+ ", 売上明細月次.sykd_mkbn"
											+ ", SMS.sms_mei AS sykd_mei"
											+ ", SUM(CONVERT(int, sykd_suryo)) AS 数量"
											+ ", 売上明細月次.sykd_tani"
											+ ", Microsoft365商品.仕入価格 AS 評価単価"
											+ ", CONVERT(int, 売上明細月次.sykd_rate) AS sykd_rate"
											+ " FROM ("
											+ "("
												+ " SELECT *"
												+ " FROM {0}"
												+ " WHERE sykd_uribi / 100 = {1}"
											+ ") AS 売上明細月次"
											+ " INNER JOIN"
											+ " ("
												+ " SELECT * FROM [charlieDB].[dbo].[TMP_Microsoft365商品]"
											+ ") AS Microsoft365商品 ON 売上明細月次.sykd_scd = Microsoft365商品.商品コード)"
											+ " INNER JOIN {2} AS SMS ON Microsoft365商品.仕入商品コード = SMS.sms_scd"
											+ " GROUP BY Microsoft365商品.仕入先, 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, Microsoft365商品.仕入商品コード, 売上明細月次.sykd_mkbn, SMS.sms_mei, 売上明細月次.sykd_tani, Microsoft365商品.仕入価格, 売上明細月次.sykd_rate"
											+ " HAVING SUM(CONVERT(int, sykd_suryo)) <> 0"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, ym.ToIntYM()
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]);
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 仕入集計.DataTableToList(dt);
		}

		/// <summary>
		/// 9 問心伝月額仕入集計
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<仕入集計> Select_問心伝月額仕入集計(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
											+ " 問心伝月額商品.仕入先"
											+ ", 売上明細月次.sykd_jbmn"
											+ ", 売上明細月次.sykd_jtan"
											+ ", 問心伝月額商品.仕入商品コード AS sykd_scd"
											+ ", 売上明細月次.sykd_mkbn"
											+ ", SMS.sms_mei AS sykd_mei"
											+ ", SUM(CONVERT(int, sykd_suryo)) AS 数量"
											+ ", 売上明細月次.sykd_tani"
											+ ", 問心伝月額商品.仕入価格 AS 評価単価"
											+ ", CONVERT(int, 売上明細月次.sykd_rate) AS sykd_rate"
											+ " FROM ("
											+ "("
												+ " SELECT *"
												+ " FROM {0}"
												+ " WHERE sykd_uribi / 100 = {1}"
											+ ") AS 売上明細月次"
											+ " INNER JOIN"
											+ " ("
												+ " SELECT * FROM [charlieDB].[dbo].[TMP_問心伝月額商品]"
											+ ") AS 問心伝月額商品 ON 売上明細月次.sykd_scd = 問心伝月額商品.商品コード)"
											+ " INNER JOIN {2} AS SMS ON 問心伝月額商品.仕入商品コード = SMS.sms_scd"
											+ " GROUP BY 問心伝月額商品.仕入先, 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, 問心伝月額商品.仕入商品コード, 売上明細月次.sykd_mkbn, SMS.sms_mei, 売上明細月次.sykd_tani, 問心伝月額商品.仕入価格, 売上明細月次.sykd_rate"
											+ " HAVING SUM(CONVERT(int, sykd_suryo)) <> 0"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, ym.ToIntYM()
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]);
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 仕入集計.DataTableToList(dt);
		}

		/// <summary>
		/// 11 Curline本体アプリ仕入集計
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<仕入集計> Select_Curline本体アプリ仕入集計(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
											+ " Curline本体アプリ商品.仕入先"
											+ ", '075' AS sykd_jbmn"
											+ ", 売上明細月次.sykd_jtan"
											+ ", Curline本体アプリ商品.仕入商品コード AS sykd_scd"
											+ ", 売上明細月次.sykd_mkbn"
											+ ", SMS.sms_mei AS sykd_mei"
											+ ", SUM(CONVERT(int, sykd_suryo)) AS 数量"
											+ ", 売上明細月次.sykd_tani"
											+ ", Curline本体アプリ商品.仕入価格 AS 評価単価"
											+ ", CONVERT(int, 売上明細月次.sykd_rate) AS sykd_rate"
											+ " FROM ("
											+ "("
												+ " SELECT *"
												+ " FROM {0}"
												+ " WHERE sykd_uribi / 100 = {1}"
											+ ") AS 売上明細月次"
											+ " INNER JOIN"
											+ " ("
												+ " SELECT * FROM [charlieDB].[dbo].[TMP_Curline本体アプリ商品]"
											+ ") AS Curline本体アプリ商品 ON 売上明細月次.sykd_scd = Curline本体アプリ商品.商品コード)"
											+ " INNER JOIN {2} AS SMS ON Curline本体アプリ商品.仕入商品コード = SMS.sms_scd"
											+ " GROUP BY Curline本体アプリ商品.仕入先, sykd_jbmn, 売上明細月次.sykd_jtan, Curline本体アプリ商品.仕入商品コード, 売上明細月次.sykd_mkbn, SMS.sms_mei, 売上明細月次.sykd_tani, Curline本体アプリ商品.仕入価格, 売上明細月次.sykd_rate"
											+ " HAVING SUM(CONVERT(int, sykd_suryo)) <> 0"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, ym.ToIntYM()
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]);
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 仕入集計.DataTableToList(dt);
		}

		/// <summary>
		/// 12 ナルコーム仕入集計
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<ナルコーム仕入集計> Select_ナルコーム仕入集計(YearMonth ym, string connectStr)
		{
			// Ver1.01 新規作成(2022/04/04 勝呂):ナルコーム仕入データ作成時に数量０を除外する
			string sqlStr = string.Format("SELECT" 
										+ " ナルコーム商品売上伝票.仕入先"
										+ ", ナルコーム商品売上伝票.sykd_jbmn"
										+ ", ナルコーム商品売上伝票.sykd_jtan"
										+ ", ナルコーム商品売上伝票.仕入商品コード AS sykd_scd"
										+ ", ナルコーム商品売上伝票.sykd_mkbn"
										+ ", 商品マスタ.sms_mei AS sykd_mei"
										+ ", SUM(CONVERT(int, sykd_suryo)) AS 数量"
										+ ", ナルコーム商品売上伝票.sykd_tani"
										+ ", ナルコーム商品売上伝票.仕入価格 AS 評価単価"
										+ ", ナルコーム商品売上伝票.sykd_uribi"
										+ ", ナルコーム商品売上伝票.仕入フラグ"
										+ ", ナルコーム商品売上伝票.sykd_rate"
										+ " FROM"
										+ " ("
											+ " SELECT"
											+ " 売上明細.*"
											+ ", ナルコーム商品.仕入商品コード"
											+ ", ナルコーム商品.仕入価格"
											+ ", ナルコーム商品.仕入先"
											+ ", ナルコーム商品.仕入フラグ"
											+ " FROM ("
											+ " ("
												+ " SELECT *"
												+ " FROM {0}"
												+ " WHERE sykh_uribi >= {1} AND sykh_uribi <= {2}"
											+ ") AS 売上伝票"
											+ " INNER JOIN {3} AS 売上明細 ON 売上伝票.sykh_uribi = 売上明細.sykd_uribi AND 売上伝票.sykh_denno = 売上明細.sykd_denno AND 売上伝票.sykh_seibi = 売上明細.sykd_seibi)"
											+ " INNER JOIN"
											+ " ("
												+ " SELECT * FROM [charlieDB].[dbo].[TMP_ナルコーム商品]"
											+ ") AS ナルコーム商品 ON 売上明細.sykd_scd = ナルコーム商品.商品コード"
											+ " WHERE 売上明細.sykd_kingaku <> 0"
										+ ") AS ナルコーム商品売上伝票"
										+ " INNER JOIN {4} AS 商品マスタ ON ナルコーム商品売上伝票.仕入商品コード = 商品マスタ.sms_scd"
										+ " GROUP BY ナルコーム商品売上伝票.仕入先, ナルコーム商品売上伝票.sykd_jbmn, ナルコーム商品売上伝票.sykd_jtan, ナルコーム商品売上伝票.仕入商品コード, ナルコーム商品売上伝票.sykd_mkbn, 商品マスタ.sms_mei, ナルコーム商品売上伝票.sykd_tani, ナルコーム商品売上伝票.仕入価格, ナルコーム商品売上伝票.sykd_uribi, ナルコーム商品売上伝票.仕入フラグ, ナルコーム商品売上伝票.sykd_rate"
										+ " HAVING SUM(CONVERT(int, sykd_suryo)) <> 0"
										+ " ORDER BY ナルコーム商品売上伝票.sykd_jbmn, ナルコーム商品売上伝票.仕入フラグ, ナルコーム商品売上伝票.sykd_uribi, ナルコーム商品売上伝票.仕入商品コード"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上ヘッダ]
										, ym.ToSpan().Start.ToIntYMD()
										, ym.ToSpan().End.ToIntYMD()
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]);
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return ナルコーム仕入集計.DataTableToList(dt);
		}

		/// <summary>
		/// 13. クラウドバックアップ仕入集計
		/// </summary>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="ym">集計月</param>
		/// <param name="settings">SQL Server接続情報</param>
		/// <returns>PCA売上明細情報リスト</returns>
		public static List<GroupMicPCA売上明細> Select_クラウドバックアップ仕入集計(string goods, YearMonth ym, string connectStr)
		{
			string strSQL = string.Format(@"SELECT sykd_jbmn, sykd_jtan, sykd_scd, sykd_mkbn, sykd_tani, sykd_uribi"
										+ ", convert(int, sykd_rate) as 消費税率"
										+ ", convert(int, sum(sykd_suryo)) as 数量"
										+ " FROM {0}"
										+ " WHERE sykd_kingaku <> 0 AND sykd_uribi >= {1} AND sykd_uribi <= {2} AND sykd_scd IN ({3})"
										+ " GROUP BY sykd_jbmn, sykd_jtan, sykd_scd, sykd_mkbn, sykd_tani, sykd_uribi, sykd_rate"
										+ " ORDER BY sykd_jbmn, sykd_uribi, sykd_scd"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, ym.ToSpan().Start.ToIntYMD()
										, ym.ToSpan().End.ToIntYMD()
										, goods);
			DataTable dt = DatabaseAccess.SelectDatabase(strSQL, connectStr);
			return GroupMicPCA売上明細.DataTableToList(dt);
		}

		/// <summary>
		/// 14. アルメックス仕入集計
		/// </summary>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="span">検索期間</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static List<vMicPCA売上明細> Select_アルメックス仕入集計(string goods, YearMonth ym, string connectStr)
		{
			string strSQL = string.Format(@"SELECT * FROM {0}"
										+ " WHERE sykd_kingaku <> 0 AND sykd_uribi >= {1} AND sykd_uribi <= {2} AND sykd_scd IN ({3})"
										+ " ORDER BY sykd_jbmn, sykd_uribi, sykd_scd"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, ym.ToSpan().Start.ToIntYMD()
										, ym.ToSpan().End.ToIntYMD()
										, goods);
			DataTable dt = DatabaseAccess.SelectDatabase(strSQL, connectStr);
			return vMicPCA売上明細.DataTableToList(dt);
		}
	}
}
