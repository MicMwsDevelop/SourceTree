//
// MakePurchaseFileAccess.cs
// 
// 仕入データ作成 データベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/01/07 勝呂)
// Ver1.01 新規作成(2022/04/04 勝呂):ナルコーム仕入データ作成時に数量０を除外する
// Ver1.03 経理部の要請により、Microsoft365仕入データを部門毎の集計を止めて、得意先に関する記事データを追加(2023/02/10 勝呂)
// Ver1.04(2023/03/30 勝呂):Microsoft365仕入データの単価が仕入価格でなく、標準価格となっている障害
// Ver1.07(2023/12/01 勝呂):オン資格保守サービスの仕入データ作成に対応
//
using CommonLib.BaseFactory.Junp.View;
using CommonLib.BaseFactory.MakePurchaseFile;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using CommonLib.DB.SqlServer.Charlie;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using CommonLib.BaseFactory;

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
		/// 8 Microsoft365売上明細
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		/// Ver1.03 経理部の要請により、Microsoft365仕入データを部門毎の集計を止めて、得意先に関する記事データを追加(2023/02/02 勝呂)
		public static List<売上明細> Select_Microsoft365売上明細(YearMonth ym, string connectStr)
		{
			//string sqlStr = string.Format("SELECT"
			//									+ " [sykd_denno] as 伝票No"
			//									+ ", Microsoft365商品.[仕入先] as 仕入先"
			//									+ ",[sykd_tcd] as 得意先番号"
			//									+ ",M.[顧客名１] + M.[顧客名２] as 得意先名"
			//									+ ",[sykd_uribi] as 売上日"
			//									+ ",[sykd_jbmn]"
			//									+ ",[sykd_jtan]"
			//									+ ",[sykd_tekmei] as 摘要"
			//									+ ",[sykd_scd] as 商品コード"
			//									+ ",[sykd_mkbn]"
			//									+ ",[sykd_mei] as 商品名"
			//									+ ",[sykd_suryo] as 数量"
			//									+ ",[sykd_tani] as 単位"
			//									+ ",[sykd_tanka] as 単価"
			//									+ ",[sykd_kingaku] as 金額"
			//									+ ",[sykd_rate] as 消費税率"
			//									+ " FROM {0} as D"
			//									+ " LEFT JOIN {1} as M on D.[sykd_tcd] = M.[得意先No]"
			//									+ " INNER JOIN (SELECT * FROM [charlieDB].[dbo].[TMP_Microsoft365商品]) AS Microsoft365商品 ON D.sykd_scd = Microsoft365商品.商品コード"
			//									+ " WHERE sykd_uribi / 100 = {2}"
			//									+ " ORDER BY sykd_jbmn, 伝票No"
			//									, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
			//									, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.顧客マスタ参照ビュー]
			//									, ym.ToIntYM());

			// Ver1.04(2023/03/30 勝呂):Microsoft365仕入データの単価が仕入価格でなく、標準価格となっている障害
			string sqlStr = string.Format("SELECT"
												+ " [sykd_denno] as 伝票No"
												+ ", Microsoft365商品.[仕入先] as 仕入先"
												+ ",[sykd_tcd] as 得意先番号"
												+ ",M.[顧客名１] + M.[顧客名２] as 得意先名"
												+ ",[sykd_uribi] as 売上日"
												+ ",[sykd_jbmn]"
												+ ",[sykd_jtan]"
												+ ",[sykd_tekmei] as 摘要"
												+ ",[sykd_scd] as 商品コード"
												+ ",[sykd_mkbn]"
												+ ",[sykd_mei] as 商品名"
												+ ",[sykd_suryo] as 数量"
												+ ",[sykd_tani] as 単位"

												//+ ",[sykd_tanka] as 単価"
												//+ ",[sykd_kingaku] as 金額"
												+ ",Microsoft365商品.仕入価格"

												+ ",[sykd_rate] as 消費税率"
												+ " FROM {0} as D"
												+ " LEFT JOIN {1} as M on D.[sykd_tcd] = M.[得意先No]"
												+ " INNER JOIN (SELECT * FROM [charlieDB].[dbo].[TMP_Microsoft365商品]) AS Microsoft365商品 ON D.sykd_scd = Microsoft365商品.商品コード"
												+ " WHERE sykd_uribi / 100 = {2}"
												+ " ORDER BY sykd_jbmn, 伝票No"
												, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
												, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.顧客マスタ参照ビュー]
												, ym.ToIntYM());
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 売上明細.DataTableToList(dt);
		}

		/// <summary>
		/// 8 Microsoft365売上明細記事データ
		/// </summary>
		/// <param name="denNo">伝票No</param>
		/// <param name="ymd">売上日</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		/// Ver1.03 経理部の要請により、Microsoft365仕入データを部門毎の集計を止めて、得意先に関する記事データを追加(2023/02/02 勝呂)
		public static 売上明細記事データ Select_Microsoft365売上明細記事データ(int denNo, int ymd, string connectStr)
		{
			string sqlStr = string.Format("SELECT TOP 1"
												+ " [sykd_denno] as 伝票No"
												+ ",[sykd_tcd] as 請求先"
												+ ",[sykd_uribi] as 売上日"
												+ ",[sykd_scd] as 商品コード"
												+ ",[sykd_mei] as 商品名"
												+ ", SUBSTRING([sykd_mei], 8, 6) as 得意先番号"
												+ ", M.[顧客名１] +M.[顧客名２] as 得意先名"
												+ " FROM {0} as D"
												+ " LEFT JOIN {1} as M on M.[得意先No] = substring(D.[sykd_mei], 8, 6)"
												+ " WHERE [sykd_denno] = {2} AND [sykd_uribi] = {3} and [sykd_scd] = '{4}' AND [sykd_mei] LIKE '得意先No.%'"
												+ " ORDER BY [sykd_denno]"
												, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
												, CharlieDatabaseDefine.ViewName[CharlieDatabaseDefine.ViewType.顧客マスタ参照ビュー]
												, denNo
												, ymd
												, PcaGoodsIDDefine.ArticleCode);
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 売上明細記事データ.DataTableToData(dt);
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

		/// <summary>
		/// オン資格保守サービス仕入集計
		/// </summary>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="span">検索期間</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		// Ver1.07(2023/12/01 勝呂):オン資格保守サービスの仕入データ作成に対応
		public static List<vMicPCA売上明細> Select_オン資格保守サービス仕入集計(string goods, YearMonth ym, string connectStr)
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
