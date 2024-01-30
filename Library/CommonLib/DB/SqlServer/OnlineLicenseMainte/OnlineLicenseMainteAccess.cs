//
// OnlineLicenseMainteAccess.cs
//
// オンライン資格保守サービス ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/01/23 勝呂):新規作成
// 
using CommonLib.BaseFactory.OnlineLicenseMainte;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.DB.SqlServer.OnlineLicenseMainte
{
	static public class OnlineLicenseMainteAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// アプリケーション情報からオン資格保守サービス売上情報の取得
		/// </summary>
		/// <param name="mainteYM">保守終了年月</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>オン資格保守サービス売上情報リスト</returns>
		public static List<OnlineLicenseMainteEarningsOut> GetOnlineLicenseMainteEarningsOut(YearMonth mainteYM, string connectStr)
		{
			DataTable dt = OnlineLicenseMainteGetIO.GetOnlineLicenseMainteEarningsOut(mainteYM, connectStr);
			return OnlineLicenseMainteEarningsOut.DataTableToList(dt);
		}

		/// <summary>
		/// アプリケーション情報の更新
		/// </summary>
		/// <param name="sale">オンライン資格保守サービス売上情報</param>
		/// <param name="procName">アプリ名</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果行数</returns>
		public static int UpdateSetApplicationInfo(OnlineLicenseMainteEarningsOut sale, string procName, string connectStr)
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
		/// <param name="sale">オンライン資格保守サービス売上情報</param>
		/// <param name="procName">アプリ名</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果行数</returns>
		public static int UpdateSetApplicationInfoEndFlag(OnlineLicenseMainteEarningsOut sale, string procName, string connectStr)
		{
			string updateStr = string.Format(@"UPDATE {0} SET fai終了フラグ = @1, fai更新日 = @2, fai更新者 = @3"
								+ " WHERE faiCliMicID = {1} AND faiアプリケーションNo = {2} AND faiアプリケーション名 = '{3}'"
								, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikアプリケーション情報]
								, sale.f顧客No
								, sale.fアプリケーションNo
								, sale.fアプリケーション名);

			SqlParameter[] param = {
				new SqlParameter("@1", (sale.f終了フラグ) ? "1" : "0"),	// fai終了フラグ
				new SqlParameter("@2", DateTime.Now),						// fai更新日
				new SqlParameter("@3", procName)							// fai更新者
            };
			return DatabaseAccess.UpdateSetDatabase(updateStr, param, connectStr);
		}
	}
}
