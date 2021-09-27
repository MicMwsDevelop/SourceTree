//
// AlmexMainteAccess.cs
//
// アルメックス保守サービス ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/11/24 勝呂)
// 
using CommonLib.BaseFactory.AlmexMainte;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.DB.SqlServer.AlmexMainte
{
	public static class AlmexMainteAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// アプリケーション情報からアルメックス保守サービスの更新対象医院の取得
		/// </summary>
		/// <param name="mainteEndYM">保守終了年月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>アルメックス保守サービス売上情報リスト</returns>
		public static List<AlmexMainteEarningsOut> GetAlmexMainteEarningsOut(YearMonth mainteEndYM, string connectStr)
		{
			DataTable dt = AlmexMainteGetIO.GetAlmexMainteEarningsOut(mainteEndYM, connectStr);
			return AlmexMainteEarningsOut.DataTableToList(dt);
		}

		/// <summary>
		/// 指定期間のアルメックスPCA売上明細情報リストの取得
		/// </summary>
		/// <param name="goods">商品コードリスト</param>
		/// <param name="span">検索期間</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>DataTable</returns>
		public static List<vMicPCA売上明細> GetAlmexMainteEarningsList(string goods, Span span, string connectStr)
		{
			DataTable dt = AlmexMainteGetIO.GetAlmexMainteEarningsList(goods, span, connectStr);
			return vMicPCA売上明細.DataTableToList(dt);
		}

		/// <summary>
		/// アプリケーション情報の更新
		/// </summary>
		/// <param name="sale">アルメックス保守サービス売上情報</param>
		/// <param name="procName">アプリ名</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果行数</returns>
		public static int UpdateSetApplicationInfo(AlmexMainteEarningsOut sale, string procName, string connectStr)
		{
			string updateStr = string.Format(@"UPDATE {0} SET fai保守契約終了 = @1, fai更新日 = @2, fai更新者 = @3"
								+ " WHERE faiCliMicID = {1}"
								, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikアプリケーション情報]
								, sale.f顧客No);

			SqlParameter[] param = {
				new SqlParameter("@1", sale.f保守終了月.Value.GetNormalString()),	// fai保守契約終了
				new SqlParameter("@2", DateTime.Now),			// fai更新日
				new SqlParameter("@3", procName)				// fai更新者
            };
			return DatabaseAccess.UpdateSetDatabase(updateStr, param, connectStr);
		}

		/// <summary>
		/// アプリケーション情報 終了フラグの設定
		/// </summary>
		/// <param name="sale">アルメックス保守サービス売上情報</param>
		/// <param name="procName">アプリ名</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果行数</returns>
		public static int UpdateSetApplicationInfoEndFlag(AlmexMainteEarningsOut sale, string procName, string connectStr)
		{
			string updateStr = string.Format(@"UPDATE {0} SET fai終了フラグ = @1, fai更新日 = @2, fai更新者 = @3"
								+ " WHERE faiCliMicID = {1} AND faiアプリケーションNo = {2} AND faiアプリケーション名 = '{3}'"
								, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikアプリケーション情報]
								, sale.f顧客No
								, sale.fアプリケーションNo
								, sale.fアプリケーション名);

			SqlParameter[] param = {
				new SqlParameter("@1", (sale.f終了フラグ) ? "1" : "0"),	// fai終了フラグ
				new SqlParameter("@2", DateTime.Now),	// fai更新日
				new SqlParameter("@3", procName)		// fai更新者
            };
			return DatabaseAccess.UpdateSetDatabase(updateStr, param, connectStr);
		}
	}
}
