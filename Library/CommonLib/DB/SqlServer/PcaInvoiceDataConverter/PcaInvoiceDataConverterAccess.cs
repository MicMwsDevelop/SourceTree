//
// PcaInvoiceDataConverterAccess.cs
//
// PCA請求データコンバータ データベースアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/27 勝呂):新規作成
// 
using CommonLib.BaseFactory.PcaInvoiceDataConverter;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.DB.SqlServer.PcaInvoiceDataConverter
{
	public static class PcaInvoiceDataConverterAccess
	{

		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		//////////////////////////////
		// SELECT

		/// <summary>
		/// 顧客情報の抽出
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>顧客情報リスト</returns>
		public static List<CustomerInfo> GetCustomerInfo(string connectStr)
		{
			DataTable table = PcaInvoiceDataConverterGetIO.GetCustomerInfo(connectStr);
			return CustomerInfo.DataTableToList(table);
		}
/*







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
		/// 顧客管理利用情報から利用申込サービスの取得
		/// </summary>
		/// <param name="compDateTime">前回取得日時</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>顧客管理利用情報</returns>
		public static List<T_CUSSTOMER_USE_INFOMATION> GetCustomerUseInformationUseService(DateTime compDateTime, string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetCustomerUseInformationUseService(compDateTime, connectStr);
			return T_CUSSTOMER_USE_INFOMATION.DataTableToList(table);
		}

		/// <summary>
		/// 顧客管理利用情報から解約申込サービスの取得
		/// </summary>
		/// <param name="compDateTime">前回取得日時</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>顧客管理利用情報</returns>
		public static List<T_CUSSTOMER_USE_INFOMATION> GetCustomerUseInformationCancelService(DateTime compDateTime, string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetCustomerUseInformationCancelService(compDateTime, connectStr);
			return T_CUSSTOMER_USE_INFOMATION.DataTableToList(table);
		}

		/// <summary>
		/// 最終同期日時の取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>最終同期日時</returns>
		public static T_FILE_CREATEDATE GetLastSynchroTime(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetLastSynchroTime(connectStr);
			return T_FILE_CREATEDATE.DataTableToData(table);
		}

		/// <summary>
		/// サービス申込情報から利用申込サービスの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>サービス申込情報</returns>
		public static List<T_MWS_APPLY> GetMwsApplyUserService(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetMwsApplyUserService(connectStr);
			return T_MWS_APPLY.DataTableToList(table);
		}

		/// <summary>
		/// サービス申込情報から解約申込サービスの取得
		/// </summary>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>サービス申込情報</returns>
		public static List<T_MWS_APPLY> GetMwsApplyCancelService(string connectStr)
		{
			DataTable table = AdjustServiceApplyGetIO.GetMwsApplyCancelService(connectStr);
			return T_MWS_APPLY.DataTableToList(table);
		}


		//////////////////////////////
		// UPDATE SET

		/// <summary>
		/// [charlieDB].[dbo].[T_MWS_APPLY]の更新（サービス申込情報）
		/// </summary>
		/// <param name="list">サービス申込情報リスト</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>影響行数</returns>
		public static int UpdateSet_T_MWS_APPLY(List<T_MWS_APPLY> list, string updateUser, string connectStr)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(connectStr))
			{
				try
				{
					// 接続
					con.Open();

					// トランザクション開始
					using (SqlTransaction tran = con.BeginTransaction())
					{
						try
						{
							foreach (T_MWS_APPLY apply in list)
							{
								string sqlStr = string.Format(@"UPDATE {0} SET system_flg = '1', update_date = {1}, update_user = {2} WHERE apply_id = {3}"
																			, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_MWS_APPLY]
																			, DateTime.Now, updateUser, apply.apply_id);
								// 実行
								rowCount = DatabaseController.SqlExecuteNonQueryTran(con, tran, sqlStr);
								if (rowCount <= -1)
								{
									throw new ApplicationException("サービス申込情報更新エラー");
								}
							}
							// コミット
							tran.Commit();
						}
						catch
						{
							// ロールバック
							tran.Rollback();
							throw;
						}
					}
				}
				catch (Exception ex)
				{
					throw new ApplicationException(string.Format("サービス申込情報更新エラー({0})", ex.Message));
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
		/// 最終同期日時の新規追加
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
*/
	}
}
