//
// CouplerDatabaseAccess.cs
//
// CouplerDB データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/10/18 勝呂)
// 
using CommonLib.BaseFactory.Coupler.Table;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.DB.SqlServer.Coupler
{
	public static class CouplerDatabaseAccess
	{
		////////////////////////////////////////////////////////////////
		// テーブル関連
		////////////////////////////////////////////////////////////////

		/// <summary>
		/// [Coupler].[dbo].[SERVICE]の取得
		/// </summary>
		/// <param name="dbConnect">DB接続情報</param>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>サービス利用情報</returns>
		public static List<T_COUPLER_SERVICE> Select_T_COUPLER_SERVICE(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CouplerDatabaseDefine.TableName[CouplerDatabaseDefine.TableType.T_COUPLER_SERVICE], whereStr, orderStr, connectStr);
			return T_COUPLER_SERVICE.DataTableToList(table);
		}

		/// <summary>
		/// [Coupler].[dbo].[PRODUCTUSER]の取得
		/// </summary>
		/// <param name="dbConnect">DB接続情報</param>
		/// <param name="whereStr">Where句</param>
		/// <param name="orderStr">Order句</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>製品顧客管理情報</returns>
		public static List<T_COUPLER_PRODUCTUSER> Select_T_COUPLER_PRODUCTUSER(string whereStr, string orderStr, string connectStr)
		{
			DataTable table = DatabaseAccess.SelectDatabase(CouplerDatabaseDefine.TableName[CouplerDatabaseDefine.TableType.T_COUPLER_PRODUCTUSER], whereStr, orderStr, connectStr);
			return T_COUPLER_PRODUCTUSER.DataTableToList(table);
		}
	}
}
