using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLib.BaseFactory.PurchaseTransfer;
using System.Data.SqlClient;
using CommonLib.Common;
using System.Data;
using CommonLib.DB.SqlServer.Junp;

namespace CommonLib.DB.SqlServer.PurchaseTransfer
{
	public static class PurchaseTransferAccess
	{
		/// <summary>
		/// [charlieDB].[dbo].[TEST_在庫一覧表]の新規追加
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
							+ " FROM [charlieDB].[dbo].TEST_在庫一覧表"
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
									+ " WHERE CONVERT(varchar, RTrim(倉庫コード)) <> '99' AND PCA仕入明細.仕入日 Between {2} And {3} AND PCA仕入明細.科目区分 = 0) as 対象月全仕入れ明細"
									+ " LEFT JOIN"
									+ " ("
										+ " SELECT 商品ｺｰﾄﾞ, 評価単価"
										+ " FROM [charlieDB].[dbo].TEST_在庫一覧表"
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
										+ " WHERE PCA出荷明細貯蔵品.出荷先コード Between '000387' AND '000475' AND(PCA出荷明細貯蔵品.倉庫コード = '0011' OR PCA出荷明細貯蔵品.倉庫コード = '0050') AND PCA出荷明細貯蔵品.出荷日 Between {1} AND {2}"
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
											+ " FROM [charlieDB].[dbo].TEST_在庫一覧表"
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

		/// <summary>
		/// 4-1 対象月社内仕入伝票
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<対象月社内仕入伝票> Select_対象月社内仕入伝票(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
										+ " 対象月全仕入れ伝票.仕入日"
										+ ", 対象月全仕入れ伝票.伝票No"
										+ ", 対象月全仕入れ伝票.仕入先コード"
										+ " FROM"
										+ " ("
											+ " SELECT"
											+ " 対象月全仕入れ明細.仕入日 AS 仕入日"
											+ ", 対象月全仕入れ明細.伝票No AS 伝票No"
											+ ", 対象月全仕入れ明細.仕入先コード AS 仕入先コード"
											+ " FROM"
											+ " ("
												+ " SELECT"
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
													+ " WHERE NYKD.nykd_scd Between '000000' AND '999999'"
												+ ") AS PCA仕入明細"
												+ " WHERE CONVERT(varchar, RTrim(倉庫コード)) <> '99' AND PCA仕入明細.仕入日 Between {2} AND {3} AND PCA仕入明細.科目区分 = 0"
											+ ") AS 対象月全仕入れ明細"
											+ " GROUP BY 対象月全仕入れ明細.仕入日, 対象月全仕入れ明細.伝票No, 対象月全仕入れ明細.仕入先コード, 対象月全仕入れ明細.仕入日, 対象月全仕入れ明細.伝票No, 対象月全仕入れ明細.仕入先コード, 対象月全仕入れ明細.伝区"
											+ " HAVING 対象月全仕入れ明細.伝区 <> '5'"
										+ ") AS 対象月全仕入れ伝票"
										+ " GROUP BY 対象月全仕入れ伝票.仕入日, 対象月全仕入れ伝票.伝票No, 対象月全仕入れ伝票.仕入先コード, 対象月全仕入れ伝票.仕入日, 対象月全仕入れ伝票.伝票No, 対象月全仕入れ伝票.仕入先コード"
										+ " HAVING 対象月全仕入れ伝票.仕入先コード Between '000201' AND '000250' OR 対象月全仕入れ伝票.仕入先コード = '000275' OR 対象月全仕入れ伝票.仕入先コード = '000262' OR 対象月全仕入れ伝票.仕入先コード = '000261'"
										+ " ORDER BY 対象月全仕入れ伝票.仕入日, 対象月全仕入れ伝票.伝票No, 対象月全仕入れ伝票.仕入先コード"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA仕入データ]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA仕入先マスタ]
										, ym.ToSpan().Start.ToIntYMD()
										, ym.ToSpan().End.ToIntYMD());
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 対象月社内仕入伝票.DataTableToList(dt);
		}

		/// <summary>
		/// 4-2 対象月社内仕入明細
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<PCA仕入明細> Select_対象月社内仕入明細(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT DISTINCT"
										+ " 対象月全仕入れ明細.入荷方法"
										+ ", 対象月全仕入れ明細.科目区分"
										+ ", 対象月全仕入れ明細.伝区"
										+ ", 対象月全仕入れ明細.仕入日"
										+ ", 対象月全仕入れ明細.精算日"
										+ ", 対象月全仕入れ明細.伝票No"
										+ ", 対象月全仕入れ明細.仕入先コード"
										+ ", 対象月全仕入れ明細.仕入先名"
										+ ", 対象月全仕入れ明細.先方担当者名"
										+ ", IIf(fPca倉庫コード is null, 倉庫code, fPca部門コード) AS 部門コード"
										+ ", 対象月全仕入れ明細.担当者コード"
										+ ", 対象月全仕入れ明細.摘要コード"
										+ ", 対象月全仕入れ明細.摘要名"
										+ ", 対象月全仕入れ明細.商品コード"
										+ ", 対象月全仕入れ明細.マスター区分"
										+ ", 対象月全仕入れ明細.品名"
										+ ", 対象月全仕入れ明細.区"
										+ ", 対象月全仕入れ明細.倉庫コード"
										+ ", 対象月全仕入れ明細.入数"
										+ ", 対象月全仕入れ明細.箱数"
										+ ", 対象月全仕入れ明細.数量"
										+ ", 対象月全仕入れ明細.単位"
										+ ", 対象月全仕入れ明細.単価"
										+ ", 対象月全仕入れ明細.金額"
										+ ", 対象月全仕入れ明細.外税額"
										+ ", 対象月全仕入れ明細.内税額"
										+ ", 対象月全仕入れ明細.税区分"
										+ ", 対象月全仕入れ明細.税込区分"
										+ ", 対象月全仕入れ明細.備考"
										+ ", 対象月全仕入れ明細.規格型番"
										+ ", 対象月全仕入れ明細.色"
										+ ", 対象月全仕入れ明細.サイズ"
										+ ", 対象月全仕入れ明細.計算式コード"
										+ ", 対象月全仕入れ明細.商品項目1"
										+ ", 対象月全仕入れ明細.商品項目2"
										+ ", 対象月全仕入れ明細.商品項目3"
										+ ", 対象月全仕入れ明細.仕入項目1"
										+ ", 対象月全仕入れ明細.仕入項目2"
										+ ", 対象月全仕入れ明細.仕入項目3"
										+ ", 対象月全仕入れ明細.税率"
										+ ", 対象月全仕入れ明細.伝票消費税外税"
										+ ", 対象月全仕入れ明細.プロジェクトコード"
										+ ", 対象月全仕入れ明細.伝票No2"
										+ ", 対象月全仕入れ明細.データ区分"
										+ ", 対象月全仕入れ明細.商品名2"
										+ " FROM"
										+ " ("
											+ " SELECT"
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
												+ " WHERE NYKD.nykd_scd Between '000000' AND '999999'"
											+ ") AS PCA仕入明細"
											+ " WHERE convert(varchar, RTrim(倉庫コード)) <> '99' AND PCA仕入明細.仕入日 Between {2} AND {3} AND PCA仕入明細.科目区分 = 0"
										+ ") AS 対象月全仕入れ明細"
										+ " LEFT JOIN {4} AS BSH ON 対象月全仕入れ明細.倉庫code = BSH.fPca倉庫コード"
										+ " WHERE (対象月全仕入れ明細.伝区 <> '5' AND(対象月全仕入れ明細.仕入先コード Between '000201' AND '000250' OR 対象月全仕入れ明細.仕入先コード = '000275') AND 対象月全仕入れ明細.数量 <> 0)"
										+ " OR"
										+ " ((対象月全仕入れ明細.仕入先コード = '000262' OR 対象月全仕入れ明細.仕入先コード = '000261') AND 対象月全仕入れ明細.数量 <> 0)"
										+ " ORDER BY 対象月全仕入れ明細.仕入日, 対象月全仕入れ明細.伝票No, 対象月全仕入れ明細.仕入先コード"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA仕入データ]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA仕入先マスタ]
										, ym.ToSpan().Start.ToIntYMD()
										, ym.ToSpan().End.ToIntYMD()
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]);
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return PCA仕入明細.DataTableToList(dt);
		}

		/// <summary>
		/// 5-2 りすとん仕入振替月次合計行追加
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<仕入振替月次追加> Select_りすとん仕入振替月次合計行追加(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
										+ "'011' AS sykd_jbmn"
										+ ", '0099' AS sykd_jtan"
										+ ", りすとん仕入振替月次_準備.sykd_scd"
										+ ", りすとん仕入振替月次_準備.sykd_mkbn"
										+ ", りすとん仕入振替月次_準備.sykd_mei"
										+ ", SUM(りすとん仕入振替月次_準備.数量計) AS 数量"
										+ ", りすとん仕入振替月次_準備.sykd_tani"
										+ ", りすとん仕入振替月次_準備.評価単価"
										+ ", CONVERT(int, りすとん仕入振替月次_準備.sykd_rate) AS sykd_rate"
										+ " FROM"
										+ " ("
											+ " SELECT"
											+ " 売上明細月次.sykd_jbmn"
											+ ", 売上明細月次.sykd_jtan"
											+ ", 在庫評価単価.商品ｺｰﾄﾞ AS sykd_scd"
											+ ", 売上明細月次.sykd_mkbn"
											+ ", SMS.sms_mei AS sykd_mei"
											+ ", SUM(CONVERT(int, sykd_suryo)) AS 数量計"
											+ ", 売上明細月次.sykd_tani"
											+ ", 在庫評価単価.評価単価"
											+ ", 売上明細月次.sykd_rate"
											+ " FROM {0} AS SMS"
											+ " INNER JOIN (("
											+ "("
												+ " SELECT *"
												+ " FROM {1}"
												+ " WHERE sykd_uribi / 100 = {2}"
											+ ") as 売上明細月次"
											+ " INNER JOIN"
											+ " ("
												+ " SELECT *"
												+ " FROM [charlieDB].[dbo].[TEST_りすとん商品コード]"
												+ ") AS りすとん商品コード ON 売上明細月次.sykd_scd = りすとん商品コード.Palette商品コード)"
												+ " INNER JOIN"
												+ "("
													+ " SELECT 商品ｺｰﾄﾞ, 評価単価"
													+ " FROM [charlieDB].[dbo].TEST_在庫一覧表"
													+ " GROUP BY 商品ｺｰﾄﾞ, 評価単価"
													+ " HAVING 商品ｺｰﾄﾞ <> '' AND 評価単価 <> 0"
												+ ") AS 在庫評価単価 ON りすとん商品コード.りすとん商品コード = 在庫評価単価.商品ｺｰﾄﾞ) ON SMS.sms_scd = 在庫評価単価.商品ｺｰﾄﾞ"
												+ " GROUP BY 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, 在庫評価単価.商品ｺｰﾄﾞ, 売上明細月次.sykd_mkbn, SMS.sms_mei, 売上明細月次.sykd_tani, 在庫評価単価.評価単価, 売上明細月次.sykd_scd, 売上明細月次.sykd_rate"
												+ " HAVING 売上明細月次.sykd_scd = '800102' OR 売上明細月次.sykd_scd = '800103' OR 売上明細月次.sykd_scd = '800104' OR 売上明細月次.sykd_scd = '800105' OR 売上明細月次.sykd_scd = '800106' OR 売上明細月次.sykd_scd = '800107'"
										+ ") AS りすとん仕入振替月次_準備"
										+ " GROUP BY りすとん仕入振替月次_準備.sykd_jbmn, りすとん仕入振替月次_準備.sykd_jtan, りすとん仕入振替月次_準備.sykd_scd, りすとん仕入振替月次_準備.sykd_mkbn, りすとん仕入振替月次_準備.sykd_mei, りすとん仕入振替月次_準備.sykd_tani, りすとん仕入振替月次_準備.評価単価, りすとん仕入振替月次_準備.sykd_rate"
										+ " HAVING SUM(りすとん仕入振替月次_準備.数量計) <> 0"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, ym.ToIntYM());
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 仕入振替月次追加.DataTableToList(dt);
		}

		/// <summary>
		/// 5-2 りすとん仕入振替月次追加
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<仕入振替月次追加> Select_りすとん仕入振替月次追加(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
										+ " りすとん仕入振替月次_準備.sykd_jbmn"
										+ ", りすとん仕入振替月次_準備.sykd_jtan"
										+ ", りすとん仕入振替月次_準備.sykd_scd"
										+ ", りすとん仕入振替月次_準備.sykd_mkbn"
										+ ", りすとん仕入振替月次_準備.sykd_mei"
										+ ", SUM(りすとん仕入振替月次_準備.数量計) AS 数量"
										+ ", りすとん仕入振替月次_準備.sykd_tani"
										+ ", りすとん仕入振替月次_準備.評価単価"
										+ ", CONVERT(int, りすとん仕入振替月次_準備.sykd_rate) AS sykd_rate"
										+ " FROM"
										+ " ("
											+ " SELECT"
											+ " 売上明細月次.sykd_jbmn"
											+ ", 売上明細月次.sykd_jtan"
											+ ", 在庫評価単価.商品ｺｰﾄﾞ AS sykd_scd"
											+ ", 売上明細月次.sykd_mkbn"
											+ ", SMS.sms_mei AS sykd_mei"
											+ ", SUM(CONVERT(int, sykd_suryo)) AS 数量計"
											+ ", 売上明細月次.sykd_tani"
											+ ", 在庫評価単価.評価単価"
											+ ", 売上明細月次.sykd_rate"
											+ " FROM {0} AS SMS"
											+ " INNER JOIN (("
											+ "("
												+ " SELECT *"
												+ " FROM {1}"
												+ " WHERE sykd_uribi / 100 = {2}"
											+ ") AS 売上明細月次"
											+ " INNER JOIN"
											+ " ("
												+ " SELECT *"
												+ " FROM [charlieDB].[dbo].[TEST_りすとん商品コード]"
											+ ") AS りすとん商品コード ON 売上明細月次.sykd_scd = りすとん商品コード.Palette商品コード)"
											+ " INNER JOIN"
											+ " ("
												+ " SELECT 商品ｺｰﾄﾞ, 評価単価"
												+ " FROM [charlieDB].[dbo].TEST_在庫一覧表"
												+ " GROUP BY 商品ｺｰﾄﾞ, 評価単価"
												+ " HAVING 商品ｺｰﾄﾞ <> '' AND 評価単価 <> 0"
											+ ") AS 在庫評価単価 ON りすとん商品コード.りすとん商品コード = 在庫評価単価.商品ｺｰﾄﾞ"
											+ ") ON SMS.sms_scd = 在庫評価単価.商品ｺｰﾄﾞ"
											+ " GROUP BY 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, 在庫評価単価.商品ｺｰﾄﾞ, 売上明細月次.sykd_mkbn, SMS.sms_mei, 売上明細月次.sykd_tani, 在庫評価単価.評価単価, 売上明細月次.sykd_scd, 売上明細月次.sykd_rate"
											+ " HAVING 売上明細月次.sykd_scd = '800102' OR 売上明細月次.sykd_scd = '800103' OR 売上明細月次.sykd_scd = '800104' OR 売上明細月次.sykd_scd = '800105' OR 売上明細月次.sykd_scd = '800106' OR 売上明細月次.sykd_scd = '800107'"
										+ ") AS りすとん仕入振替月次_準備"
										+ " GROUP BY りすとん仕入振替月次_準備.sykd_jbmn, りすとん仕入振替月次_準備.sykd_jtan, りすとん仕入振替月次_準備.sykd_scd, りすとん仕入振替月次_準備.sykd_mkbn, りすとん仕入振替月次_準備.sykd_mei, りすとん仕入振替月次_準備.sykd_tani, りすとん仕入振替月次_準備.評価単価, りすとん仕入振替月次_準備.sykd_rate"
										+ " HAVING SUM(りすとん仕入振替月次_準備.数量計) <> 0"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, ym.ToIntYM());
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 仕入振替月次追加.DataTableToList(dt);
		}

		/// <summary>
		/// 6-2 問心伝仕入振替月次合計行追加
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<仕入振替月次追加> Select_問心伝仕入振替月次合計行追加(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
										+ " '011' AS sykd_jbmn"
										+ ", '0099' AS sykd_jtan"
										+ ", 問心伝仕入振替月次_準備.sykd_scd"
										+ ", 問心伝仕入振替月次_準備.sykd_mkbn"
										+ ", 問心伝仕入振替月次_準備.sykd_mei"
										+ ", SUM(問心伝仕入振替月次_準備.数量計) AS 数量"
										+ ", 問心伝仕入振替月次_準備.sykd_tani"
										+ ", 問心伝仕入振替月次_準備.評価単価"
										+ ", CONVERT(int, 問心伝仕入振替月次_準備.sykd_rate) AS sykd_rate"
										+ " FROM"
										+ " ("
											+ " SELECT"
											+ " BSH.fPca倉庫コード"
											+ ", 売上明細月次.sykd_jbmn"
											+ ", 売上明細月次.sykd_jtan"
											+ ", 在庫評価単価.商品ｺｰﾄﾞ AS sykd_scd"
											+ ", 売上明細月次.sykd_mkbn"
											+ ", SMS.sms_mei AS sykd_mei"
											+ ", SUM(CONVERT(int, sykd_suryo)) AS 数量計"
											+ ", 売上明細月次.sykd_tani"
											+ ", 在庫評価単価.評価単価"
											+ ", JH.fBshCode1"
											+ ", JH.fBshCode2"
											+ ", JH.fBshCode3"
											+ ", 売上明細月次.sykd_rate"
											+ " FROM {0} AS BSH"
											+ " INNER JOIN ({1} AS JH"
											+ " INNER JOIN ({2} AS SMS"
											+ " INNER JOIN (("
											+ "("
												+ " SELECT *"
												+ " FROM {3}"
												+ " WHERE sykd_uribi / 100 = {4}"
											+ ") AS 売上明細月次"
											+ " INNER JOIN"
											+ " ("
												+ " SELECT * FROM [charlieDB].[dbo].[TEST_問心伝商品コード]"
											+ ") AS 問心伝商品コード ON 売上明細月次.sykd_scd = 問心伝商品コード.Palette商品コード"
											+ ") INNER JOIN"
											+ " ("
												+ " SELECT 商品ｺｰﾄﾞ, 評価単価"
												+ " FROM [charlieDB].[dbo].TEST_在庫一覧表"
												+ " GROUP BY 商品ｺｰﾄﾞ, 評価単価"
												+ " HAVING 商品ｺｰﾄﾞ <> '' AND 評価単価 <> 0"
											+ ") AS 在庫評価単価 ON 問心伝商品コード.問心伝商品コード = 在庫評価単価.商品ｺｰﾄﾞ"
											+ ") ON SMS.sms_scd = 在庫評価単価.商品ｺｰﾄﾞ"
											+ ") ON JH.f受注番号 = 売上明細月次.sykd_denno"
											+ ") ON BSH.fBshCode1 = JH.fBshCode1 AND BSH.fBshCode2 = JH.fBshCode2 AND BSH.fBshCode3 = JH.fBshCode3"
											+ " GROUP BY BSH.fPca倉庫コード, 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, 在庫評価単価.商品ｺｰﾄﾞ, 売上明細月次.sykd_mkbn, SMS.sms_mei, 売上明細月次.sykd_tani, 在庫評価単価.評価単価, JH.fBshCode1, JH.fBshCode2, JH.fBshCode3, 売上明細月次.sykd_scd, 売上明細月次.sykd_rate"
											+ " HAVING 売上明細月次.sykd_scd = '800104' OR 売上明細月次.sykd_scd = '800105' OR 売上明細月次.sykd_scd = '800106' OR 売上明細月次.sykd_scd = '800107'"
										+ ") AS 問心伝仕入振替月次_準備"
										+ " GROUP BY 問心伝仕入振替月次_準備.sykd_jbmn, 問心伝仕入振替月次_準備.sykd_jtan, 問心伝仕入振替月次_準備.sykd_scd, 問心伝仕入振替月次_準備.sykd_mkbn, 問心伝仕入振替月次_準備.sykd_mei, 問心伝仕入振替月次_準備.sykd_tani, 問心伝仕入振替月次_準備.評価単価, 問心伝仕入振替月次_準備.sykd_rate"
										+ " HAVING SUM(問心伝仕入振替月次_準備.数量計) <> 0"
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注ヘッダ]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, ym.ToIntYM());
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 仕入振替月次追加.DataTableToList(dt);
		}

		/// <summary>
		/// 6-3 問心伝仕入振替月次追加
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<仕入振替月次追加> Select_問心伝仕入振替月次追加(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
										+ " 問心伝仕入振替月次_準備.sykd_jbmn"
										+ ", 問心伝仕入振替月次_準備.sykd_jtan"
										+ ", 問心伝仕入振替月次_準備.sykd_scd"
										+ ", 問心伝仕入振替月次_準備.sykd_mkbn"
										+ ", 問心伝仕入振替月次_準備.sykd_mei"
										+ ", SUM(問心伝仕入振替月次_準備.数量計) AS 数量"
										+ ", 問心伝仕入振替月次_準備.sykd_tani"
										+ ", 問心伝仕入振替月次_準備.評価単価"
										+ ", CONVERT(int, 問心伝仕入振替月次_準備.sykd_rate) AS sykd_rate"
										+ " FROM"
										+ " ("
											+ " SELECT"
											+ " BSH.fPca倉庫コード"
											+ ", 売上明細月次.sykd_jbmn"
											+ ", 売上明細月次.sykd_jtan"
											+ ", 在庫評価単価.商品ｺｰﾄﾞ AS sykd_scd"
											+ ", 売上明細月次.sykd_mkbn"
											+ ", SMS.sms_mei AS sykd_mei"
											+ ", SUM(CONVERT(int, sykd_suryo)) AS 数量計"
											+ ", 売上明細月次.sykd_tani"
											+ ", 在庫評価単価.評価単価"
											+ ", JH.fBshCode1"
											+ ", JH.fBshCode2"
											+ ", JH.fBshCode3"
											+ ", 売上明細月次.sykd_rate"
											+ " FROM {0} AS BSH"
											+ " INNER JOIN ({1} AS JH"
											+ " INNER JOIN ({2} AS SMS"
											+ " INNER JOIN (("
											+ "("
												+ " SELECT *"
												+ " FROM {3}"
												+ " WHERE sykd_uribi / 100 = {4}"
											+ ") AS 売上明細月次"
											+ " INNER JOIN"
											+ " ("
												+ " SELECT * FROM[charlieDB].[dbo].[TEST_問心伝商品コード]"
											+ ") AS 問心伝商品コード ON 売上明細月次.sykd_scd = 問心伝商品コード.Palette商品コード)"
											+ " INNER JOIN"
											+ " ("
												+ " SELECT 商品ｺｰﾄﾞ, 評価単価"
												+ " FROM [charlieDB].[dbo].TEST_在庫一覧表"
												+ " GROUP BY 商品ｺｰﾄﾞ, 評価単価"
												+ " HAVING 商品ｺｰﾄﾞ <> '' AND 評価単価 <> 0"
											+ ") AS 在庫評価単価 ON 問心伝商品コード.問心伝商品コード = 在庫評価単価.商品ｺｰﾄﾞ"
											+ ") ON SMS.sms_scd = 在庫評価単価.商品ｺｰﾄﾞ"
											+ ") ON JH.f受注番号 = 売上明細月次.sykd_denno"
											+ ") ON BSH.fBshCode1 = JH.fBshCode1 AND BSH.fBshCode2 = JH.fBshCode2 AND BSH.fBshCode3 = JH.fBshCode3"
											+ " GROUP BY BSH.fPca倉庫コード, 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, 在庫評価単価.商品ｺｰﾄﾞ, 売上明細月次.sykd_mkbn, SMS.sms_mei, 売上明細月次.sykd_tani, 在庫評価単価.評価単価, JH.fBshCode1, JH.fBshCode2, JH.fBshCode3, 売上明細月次.sykd_scd, 売上明細月次.sykd_rate"
											+ " HAVING 売上明細月次.sykd_scd = '800104' OR 売上明細月次.sykd_scd = '800105' OR 売上明細月次.sykd_scd = '800106' OR 売上明細月次.sykd_scd = '800107'"
										+ ") AS 問心伝仕入振替月次_準備"
										+ " GROUP BY 問心伝仕入振替月次_準備.sykd_jbmn, 問心伝仕入振替月次_準備.sykd_jtan, 問心伝仕入振替月次_準備.sykd_scd, 問心伝仕入振替月次_準備.sykd_mkbn, 問心伝仕入振替月次_準備.sykd_mei, 問心伝仕入振替月次_準備.sykd_tani, 問心伝仕入振替月次_準備.評価単価, 問心伝仕入振替月次_準備.sykd_rate"
										+ " HAVING SUM(問心伝仕入振替月次_準備.数量計) <> 0"
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注ヘッダ]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, ym.ToIntYM());
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 仕入振替月次追加.DataTableToList(dt);
		}

		/// <summary>
		/// 7 りすとん月額仕入振替月次
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<仕入振替月次> Select_りすとん月額仕入振替月次(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
											+ " りすとん月額商品コード.仕入先"
											+ ", 売上明細月次.sykd_jbmn"
											+ ", 売上明細月次.sykd_jtan"
											+ ", りすとん月額商品コード.仕入商品コード AS sykd_scd"
											+ ", 売上明細月次.sykd_mkbn"
											+ ", SMS.sms_mei AS sykd_mei"
											+ ", SUM(CONVERT(int, sykd_suryo)) AS 数量"
											+ ", 売上明細月次.sykd_tani"
											+ ", りすとん月額商品コード.仕入価格 AS 評価単価"
											+ ", CONVERT(int, 売上明細月次.sykd_rate) AS sykd_rate"
											+ " FROM ("
											+ "("
												+ " SELECT *"
												+ " FROM {0}"
												+ " WHERE sykd_uribi / 100 = {1}"
											+ ") AS 売上明細月次"
											+ " INNER JOIN"
											+ " ("
												+ " SELECT * FROM [charlieDB].[dbo].[TEST_りすとん月額商品コード]"
											+ ") AS りすとん月額商品コード ON 売上明細月次.sykd_scd = りすとん月額商品コード.商品コード)"
											+ " INNER JOIN {2} AS SMS ON りすとん月額商品コード.仕入商品コード = SMS.sms_scd"
											+ " GROUP BY りすとん月額商品コード.仕入先, 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, りすとん月額商品コード.仕入商品コード, 売上明細月次.sykd_mkbn, SMS.sms_mei, 売上明細月次.sykd_tani, りすとん月額商品コード.仕入価格, 売上明細月次.sykd_rate"
											+ " HAVING SUM(CONVERT(int, sykd_suryo)) <> 0"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, ym.ToIntYM()
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]);
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 仕入振替月次.DataTableToList(dt);
		}

		/// <summary>
		/// 8 Office365仕入振替月次
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<仕入振替月次> Select_Office365仕入振替月次(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
											+ " Office365商品コード.仕入先"
											+ ", 売上明細月次.sykd_jbmn"
											+ ", 売上明細月次.sykd_jtan"
											+ ", Office365商品コード.仕入商品コード AS sykd_scd"
											+ ", 売上明細月次.sykd_mkbn"
											+ ", SMS.sms_mei AS sykd_mei"
											+ ", SUM(CONVERT(int, sykd_suryo)) AS 数量"
											+ ", 売上明細月次.sykd_tani"
											+ ", Office365商品コード.仕入価格 AS 評価単価"
											+ ", CONVERT(int, 売上明細月次.sykd_rate) AS sykd_rate"
											+ " FROM ("
											+ "("
												+ " SELECT *"
												+ " FROM {0}"
												+ " WHERE sykd_uribi / 100 = {1}"
											+ ") AS 売上明細月次"
											+ " INNER JOIN"
											+ " ("
												+ " SELECT * FROM [charlieDB].[dbo].[TEST_Office365商品コード]"
											+ ") AS Office365商品コード ON 売上明細月次.sykd_scd = Office365商品コード.商品コード)"
											+ " INNER JOIN {2} AS SMS ON Office365商品コード.仕入商品コード = SMS.sms_scd"
											+ " GROUP BY Office365商品コード.仕入先, 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, Office365商品コード.仕入商品コード, 売上明細月次.sykd_mkbn, SMS.sms_mei, 売上明細月次.sykd_tani, Office365商品コード.仕入価格, 売上明細月次.sykd_rate"
											+ " HAVING SUM(CONVERT(int, sykd_suryo)) <> 0"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, ym.ToIntYM()
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]);
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 仕入振替月次.DataTableToList(dt);
		}

		/// <summary>
		/// 9 問心伝月額仕入振替月次
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<仕入振替月次> Select_問心伝月額仕入振替月次(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
											+ " 問心伝月額商品コード.仕入先"
											+ ", 売上明細月次.sykd_jbmn"
											+ ", 売上明細月次.sykd_jtan"
											+ ", 問心伝月額商品コード.仕入商品コード AS sykd_scd"
											+ ", 売上明細月次.sykd_mkbn"
											+ ", SMS.sms_mei AS sykd_mei"
											+ ", SUM(CONVERT(int, sykd_suryo)) AS 数量"
											+ ", 売上明細月次.sykd_tani"
											+ ", 問心伝月額商品コード.仕入価格 AS 評価単価"
											+ ", CONVERT(int, 売上明細月次.sykd_rate) AS sykd_rate"
											+ " FROM ("
											+ "("
												+ " SELECT *"
												+ " FROM {0}"
												+ " WHERE sykd_uribi / 100 = {1}"
											+ ") as 売上明細月次"
											+ " INNER JOIN"
											+ " ("
												+ " SELECT * FROM [charlieDB].[dbo].[TEST_問心伝月額商品コード]"
											+ ") as 問心伝月額商品コード ON 売上明細月次.sykd_scd = 問心伝月額商品コード.商品コード)"
											+ " INNER JOIN {2} AS SMS ON 問心伝月額商品コード.仕入商品コード = SMS.sms_scd"
											+ " GROUP BY 問心伝月額商品コード.仕入先, 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, 問心伝月額商品コード.仕入商品コード, 売上明細月次.sykd_mkbn, SMS.sms_mei, 売上明細月次.sykd_tani, 問心伝月額商品コード.仕入価格, 売上明細月次.sykd_rate"
											+ " HAVING SUM(CONVERT(int, sykd_suryo)) <> 0"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, ym.ToIntYM()
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]);
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 仕入振替月次.DataTableToList(dt);
		}

		/// <summary>
		/// 10 ソフトバンク仕入振替月次
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<仕入振替月次> Select_ソフトバンク仕入振替月次(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
											+ " ソフトバンク商品コード.仕入先"
											+ ", 売上明細月次.sykd_jbmn"
											+ ", 売上明細月次.sykd_jtan"
											+ ", ソフトバンク商品コード.仕入商品コード AS sykd_scd"
											+ ", 売上明細月次.sykd_mkbn"
											+ ", SMS.sms_mei AS sykd_mei"
											+ ", SUM(CONVERT(int, sykd_suryo)) AS 数量"
											+ ", 売上明細月次.sykd_tani"
											+ ", ソフトバンク商品コード.仕入価格 AS 評価単価"
											+ ", CONVERT(int, 売上明細月次.sykd_rate) AS sykd_rate"
											+ " FROM ("
											+ "("
												+ " SELECT *"
												+ " FROM {0}"
												+ " WHERE sykd_uribi / 100 = {1}"
											+ ") AS 売上明細月次"
											+ " INNER JOIN"
											+ " ("
												+ " SELECT * FROM [charlieDB].[dbo].[TEST_ソフトバンク商品コード]"
											+ ") AS ソフトバンク商品コード ON 売上明細月次.sykd_scd = ソフトバンク商品コード.商品コード)"
											+ " INNER JOIN {2} AS SMS ON ソフトバンク商品コード.仕入商品コード = SMS.sms_scd"
											+ " GROUP BY ソフトバンク商品コード.仕入先, 売上明細月次.sykd_jbmn, 売上明細月次.sykd_jtan, ソフトバンク商品コード.仕入商品コード, 売上明細月次.sykd_mkbn, SMS.sms_mei, 売上明細月次.sykd_tani, ソフトバンク商品コード.仕入価格, 売上明細月次.sykd_rate"
											+ " HAVING SUM(CONVERT(int, sykd_suryo)) <> 0"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, ym.ToIntYM()
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]);
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 仕入振替月次.DataTableToList(dt);
		}

		/// <summary>
		/// 11 Curline本体アプリ仕入作成月次
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<仕入振替月次> Select_Curline本体アプリ仕入作成月次(YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT"
											+ " Curline本体アプリ商品コード.仕入先"
											+ ", '075' AS sykd_jbmn"
											+ ", 売上明細月次.sykd_jtan"
											+ ", Curline本体アプリ商品コード.仕入商品コード AS sykd_scd"
											+ ", 売上明細月次.sykd_mkbn"
											+ ", SMS.sms_mei AS sykd_mei"
											+ ", SUM(CONVERT(int, sykd_suryo)) AS 数量"
											+ ", 売上明細月次.sykd_tani"
											+ ", Curline本体アプリ商品コード.仕入価格 AS 評価単価"
											+ ", CONVERT(int, 売上明細月次.sykd_rate) AS sykd_rate"
											+ " FROM ("
											+ "("
												+ " SELECT *"
												+ " FROM {0}"
												+ " WHERE sykd_uribi / 100 = {1}"
											+ ") AS 売上明細月次"
											+ " INNER JOIN"
											+ " ("
												+ " SELECT * FROM [charlieDB].[dbo].[TEST_Curline本体アプリ商品コード]"
											+ ") AS Curline本体アプリ商品コード ON 売上明細月次.sykd_scd = Curline本体アプリ商品コード.商品コード)"
											+ " INNER JOIN {2} AS SMS ON Curline本体アプリ商品コード.仕入商品コード = SMS.sms_scd"
											+ " GROUP BY Curline本体アプリ商品コード.仕入先, sykd_jbmn, 売上明細月次.sykd_jtan, Curline本体アプリ商品コード.仕入商品コード, 売上明細月次.sykd_mkbn, SMS.sms_mei, 売上明細月次.sykd_tani, Curline本体アプリ商品コード.仕入価格, 売上明細月次.sykd_rate"
											+ " HAVING SUM(CONVERT(int, sykd_suryo)) <> 0"
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, ym.ToIntYM()
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA商品マスタ]);
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 仕入振替月次.DataTableToList(dt);
		}
	}
}
