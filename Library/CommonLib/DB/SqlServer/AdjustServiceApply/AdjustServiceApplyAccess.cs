﻿//
// AdjustServiceApplyAccess.cs
//
// サービス申込情報更新処理 データベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// Ver1.02(2024/01/24 勝呂):販売店情報参照ビューから販売店コードを取得処理で例外エラー
// 
using CommonLib.BaseFactory.AdjustServiceApply;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Charlie.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CommonLib.DB.SqlServer.AdjustServiceApply
{
	public static class AdjustServiceApplyAccess
	{
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		//////////////////////////////
		// SELECT

		/// <summary>
		/// WW伝票参照ビュー抽出から受注承認日が締日の範囲内で、数量>0の、伝票番号が最小の伝票データの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>WW伝票参照ビューリスト</returns>
		public static List<WW伝票参照ビュー> GetWonderWebSlip(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetWonderWebSlip(connectStr);
			return WW伝票参照ビュー.DataTableToList(table);
		}

		/// <summary>
		/// WW伝票参照ビュー抽出から受注承認日が締日の範囲内で、数量>0の、伝票番号が最小の伝票データの取得（DEBUG用）
		/// </summary>
		/// <param name="denNo">伝票No</param>
		/// <param name="customerNo">顧客No</param>
		/// <param name="goodsCode">商品コード</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>WW伝票参照ビューリスト</returns>
		public static List<WW伝票参照ビュー> GetWonderWebSlipByDebug(int denNo, int customerNo, string goodsCode, string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetWonderWebSlipByDebug(denNo, customerNo, goodsCode, connectStr);
			return WW伝票参照ビュー.DataTableToList(table);
		}

		/// <summary>
		/// 販売店情報参照ビューから販売店コードを取得
		/// </summary>
		/// <param name="storeCode">販売店コード</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>WW伝票参照ビューリスト</returns>
		public static List<int> GetStoreCode(int storeCode, string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetStoreCode(storeCode, connectStr);
			if (null != table && 0 < table.Rows.Count)
			{
				List<int> result = new List<int>();
				foreach (DataRow row in table.Rows)
				{
					result.Add(DataBaseValue.ConvObjectToInt(row["販売店コード"]));
				}
				// Ver1.02(2024/01/24 勝呂):販売店情報参照ビューから販売店コードを取得処理で例外エラー
				return result;
			}
			return null;
		}

		/// <summary>
		/// 基本機能パック 商品コード、サービス種別ID、サービスIDの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>MWSコードマスタ</returns>
		public static M_CODE GetKihonPack(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetKihonPack(connectStr);
			return M_CODE.DataTableToData(table);
		}

		/// <summary>
		/// 前回同期日時の取得（顧客情報）
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>前回同期日時</returns>
		public static T_FILE_CREATEDATE GetLastSynchroTimeForCustomer(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetLastSynchroTime("1", connectStr);
			return T_FILE_CREATEDATE.DataTableToData(table);
		}

		/// <summary>
		/// 前回同期日時の取得（サービス情報）
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>前回同期日時</returns>
		public static T_FILE_CREATEDATE GetLastSynchroTimeForService(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetLastSynchroTime("2", connectStr);
			return T_FILE_CREATEDATE.DataTableToData(table);
		}

		/// <summary>
		/// 前回同期日時以降の顧客管理利用情報の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>顧客管理利用情報</returns>
		public static List<T_CUSSTOMER_USE_INFOMATION> GetCustomerUseInformationAfterSynchroTime(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetCustomerUseInformationAfterSynchroTime(connectStr);
			return T_CUSSTOMER_USE_INFOMATION.DataTableToList(table);
		}

		/// <summary>
		/// 前回同期日時以降の顧客情報の取得（MWSユーザー）
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>顧客管理利用情報</returns>
		public static List<UpdateCouplerProductUser> GetMwsUserAfterSynchroTime(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetMwsUserAfterSynchroTime(connectStr);
			return UpdateCouplerProductUser.DataTableToList(table);
		}

		/// <summary>
		/// 前回同期日時以降の顧客情報の取得（体験版ユーザー）
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>顧客管理利用情報</returns>
		public static List<UpdateCouplerProductUser> GetTrialUserAfterSynchroTime(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetTrialUserAfterSynchroTime(connectStr);
			return UpdateCouplerProductUser.DataTableToList(table);
		}

		/// <summary>
		/// 前回同期日時以降の顧客情報の取得（社員用ユーザーAAA、デモ用ユーザーADM）
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>顧客管理利用情報</returns>
		public static List<UpdateCouplerProductUser> GetDemoUserAfterSynchroTime(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetDemoUserAfterSynchroTime(connectStr);
			return UpdateCouplerProductUser.DataTableToList(table);
		}

		/// <summary>
		/// 全顧客情報の取得（MWSユーザー）
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>顧客管理利用情報</returns>
		public static List<UpdateCouplerProductUser> GetAllMwsUser(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetAllMwsUser(connectStr);
			return UpdateCouplerProductUser.DataTableToList(table);
		}

		/// <summary>
		/// 利用情報利用日確認情報の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>利用情報利用日確認情報</returns>
		public static List<CheckCuiUseDate> GetCheckCuiUseDate(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetCheckCuiUseDate(connectStr);
			return CheckCuiUseDate.DataTableToList(table);
		}

		/// <summary>
		/// 利用申込データの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>利用情報利用日確認情報</returns>
		public static List<T_APPLICATION_DATA> GetUseApplicationData(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetUseApplicationData(connectStr);
			return T_APPLICATION_DATA.DataTableToList(table);
		}

		/// <summary>
		/// 解約申込データの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>利用情報利用日確認情報</returns>
		public static List<T_APPLICATION_DATA> GetCancelApplicationData(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetCancelApplicationData(connectStr);
			return T_APPLICATION_DATA.DataTableToList(table);
		}

		/// <summary>
		/// 申込データ（利用中）の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>利用情報利用日確認情報</returns>
		public static List<T_APPLICATION_DATA> GetUsedApplicationData(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetUsedApplicationData(connectStr);
			return T_APPLICATION_DATA.DataTableToList(table);
		}

		/// <summary>
		/// 申込データ（解約済）の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>利用情報利用日確認情報</returns>
		public static List<T_APPLICATION_DATA> GetCanceledApplicationData(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetCanceledApplicationData(connectStr);
			return T_APPLICATION_DATA.DataTableToList(table);
		}


		//////////////////////////////
		// INSERT INTO

		/// <summary>
		/// 前回同期日時の新規追加（顧客情報）
		/// </summary>
		/// <param name="userName">作成者</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int SetLastSynchroTimeForCustomer(string userName, string connectStr)
		{
			SqlParameter[] param = {
													new SqlParameter("@1", DateTime.Now),
													new SqlParameter("@2", "1"),
													new SqlParameter("@3", DateTime.Now),
													new SqlParameter("@4", userName)
												};
			return DatabaseAccess.InsertIntoDatabase(T_FILE_CREATEDATE.InsertIntoSqlString, param, connectStr);
		}

		/// <summary>
		/// 前回同期日時の新規追加（サービス利用情報）
		/// </summary>
		/// <param name="userName">作成者</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int SetLastSynchroTimeForService(string userName, string connectStr)
		{
			SqlParameter[] param = {
													new SqlParameter("@1", DateTime.Now),
													new SqlParameter("@2", "2"),
													new SqlParameter("@3", DateTime.Now),
													new SqlParameter("@4", userName)
												};
			return DatabaseAccess.InsertIntoDatabase(T_FILE_CREATEDATE.InsertIntoSqlString, param, connectStr);
		}
	}
}
