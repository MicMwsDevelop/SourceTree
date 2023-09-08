//
// AdjustServiceApplyAccess.cs
//
// サービス申込情報更新処理 データベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Charlie.View;
using CommonLib.DB.SqlServer.Coupler;
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
		/// 前回同期日時の取得（サービス情報）
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>前回同期日時</returns>
		public static T_FILE_CREATEDATE GetLastSynchroTime(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetLastSynchroTime(connectStr);
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






		///// <summary>
		///// 顧客管理利用情報から利用申込サービスの取得
		///// </summary>
		///// <param name="compDateTime">前回取得日時</param>
		///// <param name="connectStr">SQL Server接続文字列</param>
		///// <returns>顧客管理利用情報</returns>
		//public static List<T_CUSSTOMER_USE_INFOMATION> GetCustomerUseInformationUseService(DateTime compDateTime, string connectStr)
		//{
		//	DataTable table = AdjustServiceApplyGetIO.GetCustomerUseInformationUseService(compDateTime, connectStr);
		//	return T_CUSSTOMER_USE_INFOMATION.DataTableToList(table);
		//}

		///// <summary>
		///// 顧客管理利用情報から解約申込サービスの取得
		///// </summary>
		///// <param name="compDateTime">前回取得日時</param>
		///// <param name="connectStr">SQL Server接続文字列</param>
		///// <returns>顧客管理利用情報</returns>
		//public static List<T_CUSSTOMER_USE_INFOMATION> GetCustomerUseInformationCancelService(DateTime compDateTime, string connectStr)
		//{
		//	DataTable table = AdjustServiceApplyGetIO.GetCustomerUseInformationCancelService(compDateTime, connectStr);
		//	return T_CUSSTOMER_USE_INFOMATION.DataTableToList(table);
		//}

		///// <summary>
		///// サービス申込情報から利用申込サービスの取得
		///// </summary>
		///// <param name="connectStr">SQL Server接続文字列</param>
		///// <returns>サービス申込情報</returns>
		//public static List<T_MWS_APPLY> GetMwsApplyUserService(string connectStr)
		//{
		//	DataTable table = AdjustServiceApplyGetIO.GetMwsApplyUserService(connectStr);
		//	return T_MWS_APPLY.DataTableToList(table);
		//}

		///// <summary>
		///// サービス申込情報から解約申込サービスの取得
		///// </summary>
		///// <param name="connectStr">SQL Server接続文字列</param>
		///// <returns>サービス申込情報</returns>
		//public static List<T_MWS_APPLY> GetMwsApplyCancelService(string connectStr)
		//{
		//	DataTable table = AdjustServiceApplyGetIO.GetMwsApplyCancelService(connectStr);
		//	return T_MWS_APPLY.DataTableToList(table);
		//}


		//////////////////////////////
		// UPDATE SET

		/// <summary>
		/// [CouplerDB].[dbo].[APPLY]の更新（申込情報）
		/// </summary>
		/// <param name="list">サービス申込情報リスト</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <param name="databaseName">データベース名</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_T_COUPLER_APPLY(List<V_COUPLER_APPLY> list, string updateUser, string connectStr, string databaseName)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(connectStr))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					//using (SqlTransaction tran = con.BeginTransaction())
					{
						try
						{
							foreach (V_COUPLER_APPLY apply in list)
							{
								string sqlStr = string.Format(@"UPDATE {0} SET system_flg = '1', update_date = getdate(), update_user = '{1}' WHERE apply_id = {2}"
																			, string.Format("{0}.[dbo].{1}", databaseName, CouplerDatabaseDefine.TableName[CouplerDatabaseDefine.TableType.APPLY])
																			, updateUser, apply.apply_id);

								// 実行
								//rowCount = DatabaseController.SqlExecuteNonQueryTran(con, tran, sqlStr);
								rowCount = DatabaseController.SqlExecuteNonQuery(con, sqlStr);
								if (rowCount <= -1)
								{
									throw new ApplicationException("申込情報更新エラー");
								}
							}
							// コミット
							//tran.Commit();
						}
						catch
						{
							// ロールバック
							//tran.Rollback();
							throw;
						}
					}
				}
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("申込情報更新エラー({0})", ex.Message));
				}
				finally
				{
					if (null != con)
					{
						// 切断
						con.Close();
					}
				}
			}
			return rowCount;
		}


		//////////////////////////////
		// INSERT INTO

		/// <summary>
		/// 前回同期日時の新規追加（サービス情報）
		/// </summary>
		/// <param name="userName">作成者</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int SetLastSynchroTime(string userName, string connectStr)
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
