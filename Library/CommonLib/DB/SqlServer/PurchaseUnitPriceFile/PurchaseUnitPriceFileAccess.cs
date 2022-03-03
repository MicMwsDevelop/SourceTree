//
// PurchaseUnitPriceFileAccess.cs
// 
// 社内使用分振替出力ファイル出力 データベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2022/01/14 勝呂)
//
using CommonLib.BaseFactory.PurchaseUnitPriceFile;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.DB.SqlServer.PurchaseUnitPriceFile
{
	public static class PurchaseUnitPriceFileAccess
	{
		/// <summary>
		/// [charlieDB].[dbo].[TMP_在庫一覧表]の新規追加
		/// </summary>
		/// <param name="list">在庫一覧表</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoList_在庫一覧表(List<在庫一覧表> list, string connectStr)
		{
			List<SqlParameter[]> paramList = new List<SqlParameter[]>();
			foreach (在庫一覧表 data in list)
			{
				paramList.Add(data.GetInsertIntoParameters());
			}
			return DatabaseAccess.InsertIntoListDatabase(在庫一覧表.InsertIntoSqlString, paramList, connectStr);
		}

		/// <summary>
		/// 2-1 社内使用分出荷明細
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<PCA出荷明細> Select_社内使用分出荷明細(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT PCA出荷明細.*"
										+ " FROM"
										+ " ("
											+ " SELECT"
											+ " urid_dkbn AS 出荷方法"
											+ ", urid_uribi AS 出荷日"
											+ ", urid_denno AS 伝票No"
											+ ", urid_tcd AS 出荷先コード"
											+ ", urid_mei1 AS 出荷先名"
											+ ", urid_tanmei AS 先方担当者名"
											+ ", urid_jbmn AS 部門コード"
											+ ", urid_jtan AS 担当者コード"
											+ ", urid_scd AS 商品コード"
											+ ", urid_mei AS 品名"
											+ ", urid_souko AS 倉庫コード"
											+ ", CONVERT(int, urid_iri) AS 入数"
											+ ", CONVERT(int, urid_hako) AS 箱数"
											+ ", CONVERT(int, urid_suryo) AS 数量"
											+ ", urid_tani AS 単位"
											+ ", CONVERT(int, urid_tanka) AS 単価"
											+ ", CONVERT(int, urid_kingaku) AS 出荷金額"
											+ ", urid_tax AS 税区分"
											+ ", urid_komi AS 税込区分"
											+ ", urid_biko AS 備考"
											+ ", '' AS 規格型番"
											+ ", '' AS 色"
											+ ", '' AS サイズ"
											+ ", CONVERT(int, urid_rate) AS 税率"
											+ ", '' AS プロジェクトコード"
											+ ", '' AS 商品名2"
											+ " FROM {0}"
											+ " WHERE urid_scd Between '000000' And '999999'"
										+ ") AS PCA出荷明細"
										+ " WHERE PCA出荷明細.出荷日 Between {1} AND {2} AND PCA出荷明細.出荷先コード Between '000387' AND '000475'"
										+ " ORDER BY 出荷日, 伝票No"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA出荷データ]
										, ym.ToSpan().Start.ToIntYMD()
										, ym.ToSpan().End.ToIntYMD());
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return PCA出荷明細.DataTableToList(dt);
		}

		/// <summary>
		/// 2-2 在庫評価単価
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<在庫評価単価> Select_在庫評価単価(string connectStr)
		{
			string sqlStr = @"SELECT 商品コード, 評価単価"
							+ " FROM [charlieDB].[dbo].TMP_在庫一覧表"
							+ " GROUP BY 商品コード, 評価単価"
							+ " HAVING 商品コード <> '' AND 評価単価 <> 0"
							+ " ORDER BY 商品コード";
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 在庫評価単価.DataTableToList(dt);
		}

		/// <summary>
		/// 2-3 当月仕入単価
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<当月仕入単価> Select_当月仕入単価(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
										  + " 対象月全仕入れ明細.商品コード"
										  + ", CONVERT(int, 対象月全仕入れ明細.単価) AS 単価"
										+ " FROM"
										+ " ("
											+ "SELECT"
											+ " PCA仕入明細.*"
											+ ", CONVERT(varchar, RTrim(倉庫コード)) AS 倉庫code"
											+ " FROM"
											+ " ("
												+ " SELECT"
												+ " NYKD.nykd_hoho AS 入荷方法"
												+ ", NYKD.nykd_flid AS 科目区分"
												+ ", NYKD.nykd_denku AS 伝区"
												+ ", NYKD.nykd_uribi AS 仕入日"
												+ ", NYKD.nykd_seibi AS 精算日"
												+ ", NYKD.nykd_denno AS 伝票No"
												+ ", NYKD.nykd_tcd AS 仕入先コード"
												+ ", RMS.rms_mei1 AS 仕入先名"
												+ ", RMS.rms_tanmei AS 先方担当者名"
												+ ", NYKD.nykd_jbmn AS 部門コード"
												+ ", NYKD.nykd_jtan AS 担当者コード"
												+ ", NYKD.nykd_tekcd AS 摘要コード"
												+ ", NYKD.nykd_tekmei AS 摘要名"
												+ ", NYKD.nykd_scd AS 商品コード"
												+ ", NYKD.nykd_mkbn AS マスター区分"
												+ ", NYKD.nykd_mei AS 品名"
												+ ", NYKD.nykd_ku AS 区"
												+ ", NYKD.nykd_souko AS 倉庫コード"
												+ ", NYKD.nykd_iri AS 入数"
												+ ", NYKD.nykd_hako AS 箱数"
												+ ", NYKD.nykd_suryo AS 数量"
												+ ", NYKD.nykd_tani AS 単位"
												+ ", NYKD.nykd_tanka AS 単価"
												+ ", NYKD.nykd_kingaku AS 金額"
												+ ", NYKD.nykd_zei AS 外税額"
												+ ", NYKD.nykd_uchi AS 内税額"
												+ ", NYKD.nykd_tax AS 税区分"
												+ ", NYKD.nykd_komi AS 税込区分"
												+ ", NYKD.nykd_biko AS 備考"
												+ ", '' AS 規格型番"
												+ ", '' AS 色"
												+ ", '' AS サイズ"
												+ ", 0 AS 計算式コード"
												+ ", 0 AS 商品項目1"
												+ ", 0 AS 商品項目2"
												+ ", 0 AS 商品項目3"
												+ ", 0 AS 仕入項目1"
												+ ", 0 AS 仕入項目2"
												+ ", 0 AS 仕入項目3"
												+ ", NYKD.nykd_rate AS 税率"
												+ ", 0 AS 伝票消費税外税"
												+ ", '' AS プロジェクトコード"
												+ ", '' AS 伝票No2"
												+ ", '0' AS データ区分"
												+ ", '' AS 商品名2"
												+ " FROM {0} AS NYKD"
												+ " INNER JOIN {1} AS RMS ON NYKD.nykd_tcd = RMS.rms_tcd"
												+ " WHERE NYKD.nykd_scd Between '000000' And '999999'"
											+ ") AS PCA仕入明細"
											+ " WHERE CONVERT(varchar, RTrim(倉庫コード)) <> '99' AND PCA仕入明細.仕入日 Between {2} AND {3} AND PCA仕入明細.科目区分 = 0) as 対象月全仕入れ明細"
											+ " LEFT JOIN"
											+ " ("
												+ " SELECT 商品ｺｰﾄﾞ, 評価単価"
												+ " FROM [charlieDB].[dbo].TMP_在庫一覧表"
												+ " GROUP BY 商品ｺｰﾄﾞ, 評価単価"
												+ " HAVING 商品ｺｰﾄﾞ <> '' AND 評価単価 <> 0"
											+ ") AS 在庫評価単価 ON 対象月全仕入れ明細.商品コード = 在庫評価単価.商品ｺｰﾄﾞ"
										+ " GROUP BY 対象月全仕入れ明細.商品コード, 対象月全仕入れ明細.単価, 在庫評価単価.商品ｺｰﾄﾞ, 対象月全仕入れ明細.仕入日"
										+ " HAVING 対象月全仕入れ明細.商品コード <> '' AND 対象月全仕入れ明細.単価 <> 0 AND 在庫評価単価.商品ｺｰﾄﾞ Is Null"
										+ " ORDER BY 対象月全仕入れ明細.商品コード, 対象月全仕入れ明細.仕入日 DESC"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA仕入データ]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA仕入先マスタ]
										, ym.ToSpan().Start.ToIntYMD()
										, ym.ToSpan().End.ToIntYMD());
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 当月仕入単価.DataTableToList(dt);
		}

		/// <summary>
		/// 3-1 貯蔵品社内使用分出荷明細
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<PCA出荷明細貯蔵品> Select_貯蔵品社内使用分出荷明細(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT PCA出荷明細貯蔵品.*"
										+ "FROM"
										+ " ("
											+ " SELECT"
											+ " urid_dkbn AS 出荷方法"
											+ ", urid_uribi AS 出荷日"
											+ ", urid_denno AS 伝票No"
											+ ", urid_tcd AS 出荷先コード"
											+ ", urid_mei1 AS 出荷先名"
											+ ", urid_tanmei AS 先方担当者名"
											+ ", urid_jbmn AS 部門コード"
											+ ", urid_jtan AS 担当者コード"
											+ ", urid_scd AS 商品コード"
											+ ", urid_mei AS 品名"
											+ ", urid_souko AS 倉庫コード"
											+ ", urid_iri AS 入数"
											+ ", urid_hako AS 箱数"
											+ ", urid_suryo AS 数量"
											+ ", urid_tani AS 単位"
											+ ", urid_tanka AS 単価"
											+ ", urid_kingaku AS 出荷金額"
											+ ", urid_tax AS 税区分"
											+ ", urid_komi AS 税込区分"
											+ ", urid_biko AS 備考"
											+ ", '' AS 規格型番"
											+ ", '' AS 色"
											+ ", '' AS サイズ"
											+ ", urid_rate AS 税率"
											+ ", '' AS プロジェクトコード"
											+ " FROM {0}"
											+ " WHERE urid_scd LIKE 'A%' OR urid_scd LIKE 'B%' OR urid_scd LIKE 'C%' OR urid_scd LIKE 'D%' OR urid_scd LIKE 'E%'"
										+ ") AS PCA出荷明細貯蔵品"
										+ " WHERE PCA出荷明細貯蔵品.出荷先コード Between '000387' AND '000475' AND (PCA出荷明細貯蔵品.倉庫コード = '0011' OR PCA出荷明細貯蔵品.倉庫コード = '0050') AND PCA出荷明細貯蔵品.出荷日 Between {1} AND {2}"
										+ " ORDER BY 出荷日, 伝票No, 商品コード"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA出荷データ]
										, ym.ToSpan().Start.ToIntYMD()
										, ym.ToSpan().End.ToIntYMD());
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return PCA出荷明細貯蔵品.DataTableToList(dt);
		}

		/// <summary>
		/// 3-2 当月仕入単価貯蔵品
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<当月仕入単価> Select_当月仕入単価貯蔵品(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
										 + " 対象月仕入明細貯蔵品.商品コード"
										+ ", 対象月仕入明細貯蔵品.単価"
										+ " FROM"
										+ " ("
											+ " SELECT *"
											+ " FROM"
											+ "("
												+ "SELECT"
												+ " NYKD.nykd_hoho AS 入荷方法"
												+ ", NYKD.nykd_flid AS 科目区分"
												+ ", NYKD.nykd_denku AS 伝区"
												+ ", NYKD.nykd_uribi AS 仕入日"
												+ ", NYKD.nykd_seibi AS 精算日"
												+ ", NYKD.nykd_denno AS 伝票No"
												+ ", NYKD.nykd_tcd AS 仕入先コード"
												+ ", RMS.rms_mei1 AS 仕入先名"
												+ ", RMS.rms_tanmei AS 先方担当者名"
												+ ", NYKD.nykd_jbmn AS 部門コード"
												+ ", NYKD.nykd_jtan AS 担当者コード"
												+ ", NYKD.nykd_tekcd AS 摘要コード"
												+ ", NYKD.nykd_tekmei AS 摘要名"
												+ ", NYKD.nykd_scd AS 商品コード"
												+ ", NYKD.nykd_mkbn AS マスター区分"
												+ ", NYKD.nykd_mei AS 品名"
												+ ", NYKD.nykd_ku AS 区"
												+ ", NYKD.nykd_souko AS 倉庫コード"
												+ ", NYKD.nykd_iri AS 入数"
												+ ", NYKD.nykd_hako AS 箱数"
												+ ", NYKD.nykd_suryo AS 数量"
												+ ", NYKD.nykd_tani AS 単位"
												+ ", NYKD.nykd_tanka AS 単価"
												+ ", NYKD.nykd_kingaku AS 金額"
												+ ", NYKD.nykd_zei AS 外税額"
												+ ", NYKD.nykd_uchi AS 内税額"
												+ ", NYKD.nykd_tax AS 税区分"
												+ ", NYKD.nykd_komi AS 税込区分"
												+ ", NYKD.nykd_biko AS 備考"
												+ ", '' AS 規格型番"
												+ ", '' AS 色"
												+ ", '' AS サイズ"
												+ ", 0 AS 計算式コード"
												+ ", 0 AS 商品項目1"
												+ ", 0 AS 商品項目2"
												+ ", 0 AS 商品項目3"
												+ ", 0 AS 仕入項目1"
												+ ", 0 AS 仕入項目2"
												+ ", 0 AS 仕入項目3"
												+ ", NYKD.nykd_rate AS 税率"
												+ ", 0 AS 伝票消費税外税"
												+ ", '' AS プロジェクトコード"
												+ ", '' AS 伝票No2"
												+ " FROM {0} AS NYKD"
												+ " INNER JOIN {1} AS RMS ON NYKD.nykd_tcd = RMS.rms_tcd"
												+ " WHERE NYKD.nykd_scd LIKE 'A%' OR NYKD.nykd_scd LIKE 'B%' OR NYKD.nykd_scd LIKE 'C%' OR NYKD.nykd_scd LIKE 'D%' OR NYKD.nykd_scd LIKE 'E%'"
											+ ") AS PCA仕入明細貯蔵品"
											+ " WHERE PCA仕入明細貯蔵品.仕入日 Between {2} AND {3}"
										+ ") AS 対象月仕入明細貯蔵品"
										+ " LEFT JOIN"
										+ " ("
											+ " SELECT 商品ｺｰﾄﾞ, 評価単価"
											+ " FROM [charlieDB].[dbo].TMP_在庫一覧表"
											+ " GROUP BY 商品ｺｰﾄﾞ, 評価単価"
											+ " HAVING 商品ｺｰﾄﾞ <> '' AND 評価単価 <> 0"
										+ ") AS 在庫評価単価 ON 対象月仕入明細貯蔵品.商品コード = 在庫評価単価.商品ｺｰﾄﾞ"
										+ " GROUP BY 対象月仕入明細貯蔵品.商品コード, 対象月仕入明細貯蔵品.単価, 在庫評価単価.商品ｺｰﾄﾞ, 対象月仕入明細貯蔵品.仕入日"
										+ " HAVING 対象月仕入明細貯蔵品.商品コード <> '' AND 対象月仕入明細貯蔵品.単価 <> 0 AND 在庫評価単価.商品ｺｰﾄﾞ Is Null"
										+ " ORDER BY 対象月仕入明細貯蔵品.商品コード, 対象月仕入明細貯蔵品.仕入日 DESC"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA仕入データ]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA仕入先マスタ]
										, ym.ToSpan().Start.ToIntYMD()
										, ym.ToSpan().End.ToIntYMD());
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 当月仕入単価.DataTableToList(dt);
		}
	}
}
