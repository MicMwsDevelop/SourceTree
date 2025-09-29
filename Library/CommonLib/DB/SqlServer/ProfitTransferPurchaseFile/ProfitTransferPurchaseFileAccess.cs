//
// ProfitTransferPurchaseFileAccess.cs
// 
// 部署間利益付け替え仕入データ作成 データベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/09/22 越田)
//
using CommonLib.BaseFactory.ProfitTransferPurchaseFile;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DB.SqlServer.ProfitTransferPurchaseFile
{
	public class ProfitTransferPurchaseFileAccess
	{
		/// <summary>
		/// PRO営業部→CS事業部 利益付け替え仕入集計
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<営業部_CS事業部_利益付け替え仕入集計> Select_PRO営業部_CS事業部_利益付け替え仕入集計(string goodsCodes, string 部門コード, YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT VMPSD.sykd_uribi AS 売上日 "
										+ "    , VMPSD.sykd_jbmn AS 委託元_部門コード "
										+ "    , VMPSD.sykd_jtan AS 委託元_担当者コード "
										+ "    , VMPSD.sykd_scd AS 商品コード "
										+ "    , VMPSD.sykd_mei AS 商品名 "
										+ "    , VMPSD.sykd_suryo AS 数量 "
										+ "    , VMPSD.sykd_tanka AS 単価 "								// デバッグ用情報
										+ "    , VMPSD.sykd_kingaku AS 売上金額 "                       // デバッグ用情報
										+ "    , VMPSD.sykd_gentan AS 原単価 "                          // デバッグ用情報
										+ "    , VMPSD.sykd_genka AS 原価金額 "                         // PRO営業部の場合は原価(sykd_genka)が利益付け替え対象金額
										+ "    , VMPSD.sykd_tax  AS 税区分 "
										+ "    , VMPSD.sykd_komi AS 税込区分 "
										+ "    , VMPSD.sykd_denno AS 売上伝票No "
										+ "    , JH.fユーザーコード AS 顧客No "
										+ "    , JH.fユーザー AS 顧客名 "
										+ "    , VMPSD.sykd_rate AS 税率 "
										+ "    , RIGHT(CONCAT('000', CONVERT(nvarchar, TMSI.fPca部門コード)), 3) AS 委託先_部門コード "			// 部門コードはPCAでは文字列型でtMih支店情報では数値型のため比較などの処理の都合でPCA側に合わせる
										+ "    , RIGHT(CONCAT('0000', TRIM(TMSI.f担当者コード)), 4) AS 委託先_担当者コード "					// 担当者コードはPCAでは文字列型4桁で0埋めでtMih支店情報では文字列型で0埋めされていないため比較などの処理の都合でPCAの表現に合わせる
										+ ""
										+ "FROM {0} AS VMPSD "
										+ "    LEFT JOIN {1} AS JH ON JH.f受注番号 = VMPSD.sykd_denno "
										+ "    LEFT JOIN {2} AS C ON C.fCliID = JH.fユーザーコード "
										+ "    LEFT JOIN {3} AS VMT on VMT.fUsrID = C.fCliFirstcMan "
										+ "    LEFT JOIN {4} AS TMSI ON TMSI.fBshCode1 = VMT.fBshCode1 AND TMSI.fBshCode2 = VMT.fBshCode2 AND TMSI.fBshCode3 = VMT.fBshCode3 "
										+ ""
										+ "WHERE VMPSD.sykd_scd IN ( {5} )"
										+ "    AND "
										+ "    VMPSD.sykd_genka <> 0 "                                                                          // 利益付け替え対象金額が0円の場合は、付け替える利益がないため利益付け替え対象外とする
										+ "    AND "
										+ "    VMPSD.sykd_jbmn = '{6}' "
										+ "    AND "
										+ "    VMPSD.sykd_uribi / 100 = {7} "
										+ ""
										+ "ORDER BY TMSI.fPca部門コード, VMPSD.sykd_denno "
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注ヘッダ]
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMih担当者]
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]
										, goodsCodes
										, 部門コード
										, ym.ToIntYM());
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 営業部_CS事業部_利益付け替え仕入集計.DataTableToList(dt);
		}

		/// <summary>
		/// SOL営業部→CS事業部 利益付け替え仕入集計
		/// </summary>
		/// <param name="ym">集計月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果</returns>
		public static List<営業部_CS事業部_利益付け替え仕入集計> Select_SOL営業部_CS事業部_利益付け替え仕入集計(string goodsCodes, string 部門コード, YearMonth ym, string connectStr)
		{
			string sqlStr = string.Format("SELECT VMPSD.sykd_uribi AS 売上日 "
										+ "    , VMPSD.sykd_jbmn AS 委託元_部門コード "
										+ "    , VMPSD.sykd_jtan AS 委託元_担当者コード "
										+ "    , VMPSD.sykd_scd AS 商品コード "
										+ "    , VMPSD.sykd_mei AS 商品名 "
										+ "    , VMPSD.sykd_suryo AS 数量 "
										+ "    , VMPSD.sykd_tanka AS 単価 "                             // デバッグ用情報(WW受注伝票→PCA売上伝票作成時には設定されない)
										+ "    , VMPSD.sykd_kingaku AS 売上金額 "                       // SOL営業部は売上価格(sykd_kingaku)(→PCA売上伝票画面では[金額]欄)が利益付け替え対象金額
										+ "    , VMPSD.sykd_gentan AS 原単価 "							// デバッグ用情報
										+ "    , VMPSD.sykd_genka AS 原価金額 "							// デバッグ用情報
										+ "    , VMPSD.sykd_tax  AS 税区分 "
										+ "    , VMPSD.sykd_komi AS 税込区分 "
										+ "    , VMPSD.sykd_denno AS 売上伝票No "
										+ "    , JH.fユーザーコード AS 顧客No "
										+ "    , JH.fユーザー AS 顧客名 "
										+ "    , VMPSD.sykd_rate AS 税率 "
										+ "    , RIGHT(CONCAT('000', CONVERT(nvarchar, TMSI.fPca部門コード)), 3) AS 委託先_部門コード "		// 部門コードはPCAでは文字列型でtMih支店情報では数値型のため比較などの処理の都合でPCA側に合わせる
										+ "    , RIGHT(CONCAT('0000', TRIM(TMSI.f担当者コード)), 4) AS 委託先_担当者コード "				// 担当者コードはPCAでは文字列型4桁で0埋めでtMih支店情報では文字列型で0埋めされていないため比較などの処理の都合でPCAの表現に合わせる
										+ ""
										+ "FROM {0} AS VMPSD "
										+ "    LEFT JOIN {1} AS JH ON JH.f受注番号 = VMPSD.sykd_denno "
										+ "    LEFT JOIN {2} AS C ON C.fCliID = JH.fユーザーコード "
										+ "    LEFT JOIN {3} AS VMT on VMT.fUsrID = C.fCliFirstcMan "
										+ "    LEFT JOIN {4} AS TMSI ON TMSI.fBshCode1 = VMT.fBshCode1 AND TMSI.fBshCode2 = VMT.fBshCode2 AND TMSI.fBshCode3 = VMT.fBshCode3 "
										+ ""
										+ "WHERE VMPSD.sykd_scd IN ( {5} )"
										+ "    AND "
										+ "    VMPSD.sykd_kingaku <> 0 "																	// 利益付け替え対象金額が0円の場合は、付け替える利益がないため利益付け替え対象外とする
										+ "    AND "
										+ "    VMPSD.sykd_jbmn = '{6}' "
										+ "    AND "
										+ "    VMPSD.sykd_uribi / 100 = {7} "
										+ ""
										+ "ORDER BY TMSI.fPca部門コード, VMPSD.sykd_denno "
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMicPCA売上明細]
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih受注ヘッダ]
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tClient]
										, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMih担当者]
										, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]
										, goodsCodes
										, 部門コード
										, ym.ToIntYM());
			DataTable dt = DatabaseAccess.SelectDatabase(sqlStr, connectStr);
			return 営業部_CS事業部_利益付け替え仕入集計.DataTableToList(dt);
		}
	}
}
