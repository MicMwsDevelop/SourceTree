//
// PcSupportEarningsAccess.cs
//
// PC安心サポート継続売上データ作成 ファイルアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2020/10/09 勝呂)
// 
using MwsLib.BaseFactory.Charlie.Table;
using MwsLib.BaseFactory.PcSupportEarnings;
using MwsLib.DB.SqlServer.Charlie;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.PcSupportEarnings
{
	public static class PcSupportEarningsAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 顧客Noに売上データ必須情報の取得
		/// </summary>
		/// <param name="customerID">顧客No</param>
		/// <param name="ct">CT環境</param>
		/// <returns>受注情報リスト</returns>
		public static PcSupportEarningsOut GetPcSupportEarningsOut(int customerID, bool ct = false)
		{
			DataTable dt = PcSupportEarningsGetIO.GetPcSupportEarningsOut(customerID, ct);
			return PcSupportEarningsController.ConvertPcSupportEarningsOut(dt);
		}


		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// PC安心サポート契約情報の更新
		/// </summary>
		/// <param name="pc">PC安心サポート契約情報</param>
		/// <param name="updateUser">更新者</param>
		/// <param name="ct">CT環境</param>
		/// <returns>結果行数</returns>
		public static int UpdatePcSupportInfo(T_USE_PCCSUPPORT pc, string updateUser, bool ct = false)
		{
			string updateStr = string.Format(@"UPDATE {0} SET fContractEndDate = @1, fBillingEndDate = @2, fGoodsID = @3, fUpdateDate = @4, fUpdatePerson = @5"
								+ " WHERE fCustomerID = {1}"
								, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_PCCSUPPORT]
								, pc.fCustomerID);
			
			//System.DateTime
								
			SqlParameter[] param = {
				new SqlParameter("@1", pc.fContractEndDate.Value.ToDateTime()),	// fContractEndDate
                new SqlParameter("@2", pc.fBillingEndDate.Value.ToDateTime()),	// fBillingEndDate
				new SqlParameter("@3", pc.fGoodsID),			// fGoodsID
				new SqlParameter("@4", DateTime.Now),			// fUpdateDate
				new SqlParameter("@5", updateUser)				// fUpdatePerson
            };

			return CharlieDatabaseAccess.UpdateSetCharlieDatabase(updateStr, param, ct);
		}
	}
}
