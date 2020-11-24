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
		/// アプリケーション情報からアルメックス保守サービス料の更新対象ユーザーの取得
		/// </summary>
		/// <param name="saleDate">売上日</param>
		/// <param name="ct">CT環境</param>
		/// <returns>アルメックスサービス保守料売上クラスリスト</returns>
		public static List<AlmexMainteEarningsOut> GetAlmexMainteEarningsOut(Date saleDate, bool ct = false)
		{
			DataTable dt = AlmexMainteEarningsGetIO.GetAlmexMainteEarningsOut(saleDate, ct);
			return AlmexMainteEarningsOut.DataTableToList(dt);
		}

		/// <summary>
		/// アプリケーション情報の更新
		/// </summary>
		/// <param name="sale">アルメックスサービス保守料売上</param>
		/// <param name="procName">アプリ名</param>
		/// <param name="ct">CT環境</param>
		/// <returns>結果行数</returns>
		public static int UpdateSetApplicationInfo(AlmexMainteEarningsOut sale, string procName, bool ct = false)
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
			return JunpDatabaseAccess.UpdateSetJunpDatabase(updateStr, param, ct);
		}
	}
}
