//
// AlmexMainteEarningsAccess.cs
//
// アルメックスサービス保守料売上更新データ作成 ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/11/24 勝呂)
// 
using MwsLib.BaseFactory.AlmexMainteEarnings;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.AlmexMainteEarnings
{
	public static class AlmexMainteEarningsAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// アプリケーション情報からアルメックス保守サービスの更新対象医院の取得
		/// </summary>
		/// <param name="saleDate">売上日</param>
		/// <param name="ct">CT環境</param>
		/// <returns>アルメックス保守サービス売上情報リスト</returns>
		public static List<AlmexMainteEarningsOut> GetAlmexMainteEarningsOut(Date saleDate, bool ct = false)
		{
			DataTable dt = AlmexMainteEarningsGetIO.GetAlmexMainteEarningsOut(saleDate, ct);
			return AlmexMainteEarningsOut.DataTableToList(dt);
		}

		/// <summary>
		/// アプリケーション情報の更新
		/// </summary>
		/// <param name="sale">アルメックス保守サービス売上情報</param>
		/// <param name="procName">アプリ名</param>
		/// <param name="ct">CT環境</param>
		/// <returns>結果行数</returns>
		public static int UpdateSetApplicationInfo(AlmexMainteEarningsOut sale, string procName, bool ct = false)
		{
			string updateStr = string.Format(@"UPDATE {0} SET fai保守契約終了 = @1, fai保守契約備考 = @2, fai更新日 = @3, fai更新者 = @4"
								+ " WHERE faiCliMicID = {1}"
								, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikアプリケーション情報]
								, sale.f顧客No);

			SqlParameter[] param = {
				new SqlParameter("@1", sale.f保守終了月.Value.GetNormalString()),	// fai保守契約終了
				new SqlParameter("@2", sale.保守契約備考),	// fai保守契約備考
				new SqlParameter("@3", DateTime.Now),			// fai更新日
				new SqlParameter("@4", procName)				// fai更新者
            };
			return JunpDatabaseAccess.UpdateSetJunpDatabase(updateStr, param, ct);
		}
	}
}
