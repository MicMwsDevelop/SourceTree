//
// HardSubscriptAccess.cs
//
// ハードサブスク データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/04/03 勝呂)
// 
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.HardSubscript;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;

namespace CommonLib.DB.SqlServer.HardSubscript
{
	/// <summary>
	/// ハードサブスク データアクセスクラス
	/// </summary>
	public static  class HardSubscriptAccess
	{
		/// <summary>
		/// ログインユーザー情報の取得
		/// </summary>
		/// <param name="userName">ログインユーザー名</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns> ログインユーザー情報</returns>
		public static tUser GetLoginUser(string userName, string connectStr)
		{
			try
			{
				string whereStr = string.Format("[fUsrLoginID] = '{0}'", userName);
				List<tUser> list = JunpDatabaseAccess.Select_tUser(whereStr, "", connectStr);
				if (null != list && 0 < list.Count)
				{
					return list[0];
				}
				return null;
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}(GetLoginUser)", ex.Message));
			}
		}

		/// <summary>
		/// 顧客情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>顧客情報</returns>
		public static vMic全ユーザー2 GetClinicInfo(int customerNo, string connectStr)
		{
			try
			{
				string whereStr = string.Format("[顧客No] = {0}", customerNo);
				List<vMic全ユーザー2> list = JunpDatabaseAccess.Select_vMic全ユーザー2(whereStr, "", connectStr);
				if (null != list && 0 < list.Count)
				{
					return list[0];
				}
				return null;
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}(GetClinicInfo)", ex.Message));
			}
		}

		/// <summary>
		/// ハードサブスク契約情報リストの取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>ハードサブスク契約情報リスト</returns>
		public static List<T_HARD_SUBSCRIPT_HEADER> GetHardSubscriptHeaderList(int customerNo, string connectStr)
		{
			try
			{
				string whereStr = string.Format("[CustomerID] = {0}", customerNo);
				return CharlieDatabaseAccess.Select_T_HARD_SUBSCRIPT_HEADER(whereStr, "[InternalRentalNo] DESC", connectStr);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}(GetHardSubscriptHeaderList)", ex.Message));
			}
		}

		/// <summary>
		/// ハードサブスク機器情報リストの取得
		/// </summary>
		/// <param name="InternalRentalNo">内部契約番号</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>ハードサブスク機器情報リスト</returns>
		public static List<T_HARD_SUBSCRIPT_DETAIL> GetHardSubscriptDetailList(int InternalRentalNo, string connectStr)
		{
			try
			{
				string whereStr = string.Format("[InternalRentalNo] = {0}", InternalRentalNo);
				return CharlieDatabaseAccess.Select_T_HARD_SUBSCRIPT_DETAIL(whereStr, "[RentalDetailNo] ASC", connectStr);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}(GetHardSubscriptDetailList)", ex.Message));
			}
		}

		/// <summary>
		/// ハードサブスク契約情報の新規追加
		/// </summary>
		/// <param name="header">ハードサブスク契約情報</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <param name="createPerson">作成者</param>
		/// <returns>内部契約番号</returns>
		public static int InsertIntoHardSubscriptHeader(T_HARD_SUBSCRIPT_HEADER header, string connectStr, string createPerson)
		{
			try
			{
				object iNewRowIdentity = DatabaseAccess.InsertIntoDatabaseScopeIdentity(T_HARD_SUBSCRIPT_HEADER.InsertIntoSqlString, header.GetInsertIntoParameters(createPerson), connectStr);
				if (null != iNewRowIdentity)
				{
					// オートナンバーの取得
					return Convert.ToInt32(iNewRowIdentity);
				}
				return 0;
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}(InsertIntoHardSubscriptHeader)", ex.Message));
			}
		}

		/// <summary>
		/// ハードサブスク機器情報リストの新規追加
		/// </summary>
		/// <param name="list">ハードサブスク機器情報リスト</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <param name="createPerson">作成者</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoHardSubscriptDetailList(List<T_HARD_SUBSCRIPT_DETAIL> list, string connectStr, string createPerson)
		{
			try
			{
				List<SqlParameter[]> paramList = new List<SqlParameter[]>();
				foreach (T_HARD_SUBSCRIPT_DETAIL detail in list)
				{
					paramList.Add(detail.GetInsertIntoParameters(createPerson));
				}
				return DatabaseAccess.InsertIntoListDatabase(T_HARD_SUBSCRIPT_DETAIL.InsertIntoSqlString, paramList, connectStr);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}(InsertIntoHardSubscriptDetailList)", ex.Message));
			}
		}

		/// <summary>
		/// ハードサブスク契約情報の更新
		/// </summary>
		/// <param name="header">ハードサブスク契約情報</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <param name="updatePerson">更新者</param>
		/// <returns>影響行数</returns>
		public static int UpdateSetHardSubscriptHeader(T_HARD_SUBSCRIPT_HEADER header, string connectStr, string updatePerson)
		{
			try
			{
				return DatabaseAccess.UpdateSetDatabase(header.UpdateSetSqlString, header.GetUpdateSetParameters(updatePerson), connectStr);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}(UpdateSetHardSubscriptHeader)", ex.Message));
			}
		}

		/// <summary>
		/// ハードサブスク契約情報の削除
		/// </summary>
		/// <param name="InternalRentalNo">内部契約番号</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>影響行数</returns>
		public static int DeleteHardSubscriptHeader(int InternalRentalNo, string connectStr)
		{
			try
			{
				string sqlStr = string.Format("DELETE FROM {0} WHERE [InternalRentalNo] = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_HARD_SUBSCRIPT_HEADER], InternalRentalNo);
				return DatabaseAccess.DeleteDatabase(sqlStr, connectStr);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}(DeleteHardSubscriptHeader)", ex.Message));
			}
		}

		/// <summary>
		/// ハードサブスク機器情報の削除
		/// </summary>
		/// <param name="InternalRentalNo">内部契約番号</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>影響行数</returns>
		public static int DeleteHardSubscriptDetail(int InternalRentalNo, string connectStr)
		{
			try
			{
				string sqlStr = string.Format("DELETE FROM {0} WHERE [InternalRentalNo] = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_HARD_SUBSCRIPT_DETAIL], InternalRentalNo);
				return DatabaseAccess.DeleteDatabase(sqlStr, connectStr);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}(DeleteHardSubscriptDetail)", ex.Message));
			}
		}

		/// <summary>
		/// ハードサブスク契約情報から売上対象医院の取得
		/// </summary>
		/// <param name="firstDate">当月初日</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>ハードサブスク売上情報リスト</returns>
		public static List<HardSubscriptEarningsOut> GetHardSubscriptEarningsOut(Date firstDate, string connectStr)
		{
			// 抽出条件
			// 終了フラグ=OFF (1)
			// サービス終了フラグ=OFF (2)
			// 利用開始日と利用終了日が設定済(3)
			// (課金開始日==null && 当日が利用開始日の翌月)(4) || (課金終了日 <= iif(解約日 is not null, 解約日, 利用終了日))(5)
			string strSQL = string.Format(@"SELECT"
									+ " U.[顧客No] as 顧客No"
									+ ", U.[顧客名１] + U.[顧客名２] as 顧客名"
									+ ", U.[得意先No] as 得意先コード"
									+ ", U.[請求先コード] as 請求先コード"
									+ ", B.[fPca部門コード] as PCA部門コード"
									+ ", B.[fPca倉庫コード] as PCA倉庫コード"
									+ ", B.[f担当者コード] as PCA担当者コード"
									+ ", '012345' as 商品コード"
									+ ", 'ハードサブスク月額' as 商品名"
									+ ", H.[MonthlyAmount] as 標準価格"
									+ ", H.[MonthlyAmount] as 原単価"
									+ ", '月' as 単位"
									+ ", U.[終了フラグ]"
									+ ", H.[InternalRentalNo] as 内部契約番号"
									+ ", H.[RentalNo] as 契約番号"
									+ ", H.[ContractDate] as 契約日"
									+ ", H.[MonthlyAmount] as 月額利用料"
									+ ", H.[Months] as 利用月数"
									+ ", H.[UseStartDate] as 利用開始日"
									+ ", H.[UseEndDate] as 利用終了日"
									+ ", H.[BillingStartDate] as 課金開始日"
									+ ", H.[BillingEndDate] as 課金終了日"
									+ ", H.[CancelDate] as 解約日"
									+ ", H.[ServiceEndFlag] as サービス終了フラグ"
									+ " FROM {0} as H"
									+ " INNER JOIN {1} as U on U.[顧客No] = H.[CustomerID]"
									+ " INNER JOIN {2} as B on B.[fBshCode3] = U.[支店コード]"
									+ " WHERE U.[終了フラグ] = '0'"	// (1)
									+ " AND H.[ServiceEndFlag] = '0'"			// (2)
									+ " AND H.[UseStartDate] is not null AND H.[UseEndDate] is not null"		// (3)
									+ " AND (H.[BillingEndDate] is null AND DATEADD(dd, 1, EOMONTH(H.[UseStartDate])) = DATEADD(dd, 1, EOMONTH('{3}' , -1))"		// (4)
									+ " OR (H.[BillingEndDate] <= iif(H.[CancelDate] is not null, H.[CancelDate], H.[UseEndDate])))"    // (5)
									+ " ORDER BY 顧客No, 内部契約番号"
									, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_HARD_SUBSCRIPT_HEADER]   // 0
									, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー2]   // 1
									, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]   // 2
									, firstDate.ToDateTime().ToShortDateString());   // 3

			DataTable dt = DatabaseAccess.SelectDatabase(strSQL, connectStr);
			return HardSubscriptEarningsOut.DataTableToList(dt);
		}

		/// <summary>
		/// ハードサブスク契約情報の更新
		/// </summary>
		/// <param name="sale">ハードサブスク売上情報</param>
		/// <param name="serviceEndFlag">サービス終了フラグ</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <param name="updatePerson">更新者</param>
		/// <returns>影響行数</returns>
		public static int UpdateSetHardSubscriptHeader(HardSubscriptEarningsOut sale, bool serviceEndFlag, string connectStr, string updatePerson)
		{
			try
			{
				string sqlStr = string.Format(@"UPDATE {0} SET BillingStartDate = @1, BillingEndDate = @2, ServiceEndFlag = @3, UpdateDate = @4, UpdatePerson = @5 WHERE InternalRentalNo = {1}"
											, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_HARD_SUBSCRIPT_HEADER]		// 0
											, sale.内部契約番号);   // 1
				SqlParameter[] param = {
															new SqlParameter("@1", sale.課金開始日.HasValue ? sale.課金開始日.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
															new SqlParameter("@2", sale.課金終了日.HasValue ? sale.課金終了日.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
															new SqlParameter("@3", serviceEndFlag ? "1" : "0"),
															new SqlParameter("@4", DateTime.Now),
															new SqlParameter("@5", updatePerson)
														};
				return DatabaseAccess.UpdateSetDatabase(sqlStr, param, connectStr);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}(UpdateSetHardSubscriptHeader)", ex.Message));
			}
		}

		/// <summary>
		/// ハードサブスク契約情報から利用期限通知医院の取得
		/// </summary>
		/// <param name="useEndDate">利用終了日</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <returns>ハードサブスク利用期限通知医院リスト</returns>
		public static List<HardSubscriptNotify> GetHardSubscriptNotify(Date useEndDate, string connectStr)
		{
			// 抽出条件
			// 終了フラグ = OFF(1)
			// サービス終了フラグ = OFF(2)
			// 利用開始日と利用終了日が設定済(3)
			// 当日から半年後の末日が利用終了日または解約日(4)
			string strSQL = string.Format(@"SELECT"
														+ " H.[InternalRentalNo] as 内部契約番号"
														+ ", H.[RentalNo] as 契約番号"
														+ ", H.[CustomerID] as 顧客No"
														+ ", H.[ContractDate] as 契約日"
														+ ", H.[MonthlyAmount] as 月額利用料"
														+ ", H.[Months] as 利用月数"
														+ ", H.[UseStartDate] as 利用開始日"
														+ ", H.[UseEndDate] as 利用終了日"
														+ ", H.[CancelDate] as 解約日"
														+ ", H.[ServiceEndFlag] as サービス終了フラグ"
														+ ", U1.[顧客名１] + U1.[顧客名２] as 顧客名"
														+ ", U1.[電話番号] as 電話番号"
														+ ", U1.[得意先No] as 得意先コード"
														+ ", U1.[請求先コード] as 請求先コード"
														+ ", U2.[顧客No] as 請求先No"
														+ ", U2.[顧客名１] + U1.[顧客名２] as 請求先名"
														+ ", U1.[支店コード] as 支店コード"
														+ ", U1.[支店名] as オフィス名"
														+ ", B.[fメールアドレス] as オフィスメールアドレス"
														+ " FROM {0} as H"
														+ " INNER JOIN {1}  as U1 on U1.[顧客No] = H.[CustomerID]"
														+ " LEFT JOIN {1}  as U2 on U2.[得意先No] = U1.[請求先コード]"
														+ " LEFT JOIN {2} as B on B.[fBshCode3] = U1.[支店コード]"
														+ " WHERE U1.[終了フラグ] = '0'"	// (1)
														+ " AND H.[ServiceEndFlag] = '0'"		// (2)
														+ " AND H.[UseStartDate] is not null AND H.[UseEndDate] is not null"		// (3)
														+ " AND {3} = iif(H.[CancelDate] is null, H.[UseEndDate], H.[CancelDate])"		// (4)
														+ " ORDER BY 支店コード, 顧客No, 契約番号 ASC"
														, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_HARD_SUBSCRIPT_HEADER]   // 0
														, JunpDatabaseDefine.ViewName[JunpDatabaseDefine.ViewType.vMic全ユーザー2]   // 1
														, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMih支店情報]   // 2
														, useEndDate.ToDateTime().ToShortDateString());   // 3
			DataTable dt = DatabaseAccess.SelectDatabase(strSQL, connectStr);
			return HardSubscriptNotify.DataTableToList(dt);
		}
	}
}
