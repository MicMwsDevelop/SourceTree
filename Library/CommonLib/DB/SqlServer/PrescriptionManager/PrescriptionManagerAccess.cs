//
// PrescriptionManagerAccess.cs
// 
// 電子処方箋契約情報 売上明細データ、仕入明細データ 出力情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/02/14 勝呂):新規作成
//
using CommonLib.BaseFactory.PrescriptionManager;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.DB.SqlServer.PrescriptionManager
{
	public static class PrescriptionManagerAccess
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 電子処方箋契約情報から運用開始日が先月の医院の取得
		/// </summary>
		/// <param name="today">当日</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>電子処方箋契約情報 売上明細データ情報リスト</returns>
		public static List<PrescriptionEarnings> GetPrescriptionEarnings(Date today, string connectStr)
		{
			DataTable dt = PrescriptionManagerGetIO.GetPrescriptionEarnings(today, connectStr);
			return PrescriptionEarnings.DataTableToList(dt);
		}

		/// <summary>
		/// 電子処方箋契約情報の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <param name="where">where文</param>
		/// <returns>電子処方箋契約情報リスト</returns>
		public static List<UsePrescription> GetUsePrescription(string connectStr, string where)
		{
			DataTable dt = PrescriptionManagerGetIO.GetUsePrescriptionList(connectStr, where);
			return UsePrescription.DataTableToList(dt);
		}

		/// <summary>
		/// 電子処方箋契約情報の運用開始日の設定
		/// </summary>
		/// <param name="use">電子処方箋契約情報</param>
		/// <param name="procName">アプリ名</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果行数</returns>
		public static int UpdateSetUsePrescriptionOperationDate(UsePrescription use, string procName, string connectStr)
		{
			string sql = string.Format(@"UPDATE {0} SET [fOperationDate] = @1, [fContractStartDate] = @2, [fContractEndDate] = @3, [fUpdateDate] = @4, [fUpdatePerson] = @5"
								+ " WHERE [fContractID] = {1}"
								, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_PRESCRIPTION_HEADER]
								, use.ContractID);

			SqlParameter[] param = {
									new SqlParameter("@1", use.OperationDate.HasValue ? use.OperationDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
									new SqlParameter("@2", use.ContractStartDate.HasValue ? use.ContractStartDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
									new SqlParameter("@3", use.ContractEndDate.HasValue ? use.ContractEndDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
									new SqlParameter("@4", DateTime.Now),
									new SqlParameter("@5", procName)
            };
			return DatabaseAccess.UpdateSetDatabase(sql, param, connectStr);
		}

		/// <summary>
		/// 電子処方箋契約情報の課金期間の設定
		/// </summary>
		/// <param name="use">電子処方箋契約情報</param>
		/// <param name="procName">アプリ名</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>結果行数</returns>
		public static int UpdateSetUsePrescriptionBilling(UsePrescription use, string procName, string connectStr)
		{
			string sql = string.Format(@"UPDATE {0} SET [fBillingStartDate] = @1, [fBillingEndDate] = @2, [fUpdateDate] = @3, [fUpdatePerson] = @4"
								+ " WHERE [fContractID] = {1}"
								, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_PRESCRIPTION_HEADER]
								, use.ContractID);

			SqlParameter[] param = {
									new SqlParameter("@1", use.BillingStartDate.HasValue ? use.BillingStartDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
									new SqlParameter("@2", use.BillingEndDate.HasValue ? use.BillingEndDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
									new SqlParameter("@3", DateTime.Now),
									new SqlParameter("@4", procName)
			};
			return DatabaseAccess.UpdateSetDatabase(sql, param, connectStr);
		}
	}
}
