//
// HardSubscManagerAccess.cs
//
// ハードサブスク情報管理 データアクセスクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/04/03 勝呂)
// 
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CommonLib.DB.SqlServer.HardSubscManager
{
	/// <summary>
	/// ハードサブスク情報管理アクセスクラス
	/// </summary>
	public static  class HardSubscManagerAccess
	{
		/// <summary>
		/// 顧客情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>顧客情報</returns>
		public static vMic全ユーザー2 GetUserInfo(int customerNo, string connectStr)
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
				throw new Exception(string.Format("{0}(GetUserInfo)", ex.Message));
			}
		}

		/// <summary>
		/// 契約情報リストの取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>契約情報リスト</returns>
		public static List<T_HARDSUBSC_HEADER> GetHardSubscHeaderList(int customerNo, string connectStr)
		{
			try
			{
				string whereStr = string.Format("[CustomerID] = {0}", customerNo);
				return CharlieDatabaseAccess.Select_T_HARDSUBSC_HEADER(whereStr, "[RentalNo] ASC", connectStr);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}(GetHardSubscHeaderList)", ex.Message));
			}
		}

		/// <summary>
		/// 機器情報リストの取得
		/// </summary>
		/// <param name="rentalNo">貸出番号</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>機器情報リスト</returns>
		public static List<T_HARDSUBSC_DETAIL> GetHardSubscDetailList(int rentalNo, string connectStr)
		{
			try
			{
				string whereStr = string.Format("[RentalNo] = {0}", rentalNo);
				return CharlieDatabaseAccess.Select_T_HARDSUBSC_DETAIL(whereStr, "[RentalDetailNo] ASC", connectStr);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}(GetHardSubscDetailList)", ex.Message));
			}
		}

		/// <summary>
		/// 契約情報の新規追加
		/// </summary>
		/// <param name="header">契約情報</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <param name="createPerson">作成者</param>
		/// <returns>貸出番号</returns>
		public static int InsertIntoHardSubscHeader(T_HARDSUBSC_HEADER header, string connectStr, string createPerson)
		{
			try
			{
				object iNewRowIdentity = DatabaseAccess.InsertIntoDatabaseScopeIdentity(T_HARDSUBSC_HEADER.InsertIntoSqlString, header.GetInsertIntoParameters(createPerson), connectStr);
				if (null != iNewRowIdentity)
				{
					// オートナンバーの取得
					return Convert.ToInt32(iNewRowIdentity);
				}
				return 0;
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}(InsertIntoHardSubscHeader)", ex.Message));
			}
		}

		/// <summary>
		/// 機器情報リストの新規追加
		/// </summary>
		/// <param name="list">機器情報リスト</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <param name="createPerson">作成者</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoHardSubscDetailList(List<T_HARDSUBSC_DETAIL> list, string connectStr, string createPerson)
		{
			try
			{
				List<SqlParameter[]> paramList = new List<SqlParameter[]>();
				foreach (T_HARDSUBSC_DETAIL detail in list)
				{
					paramList.Add(detail.GetInsertIntoParameters(createPerson));
				}
				return DatabaseAccess.InsertIntoListDatabase(T_HARDSUBSC_DETAIL.InsertIntoSqlString, paramList, connectStr);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}(InsertIntoHardSubscDetailList)", ex.Message));
			}
		}

		/// <summary>
		/// 契約情報の更新
		/// </summary>
		/// <param name="header">契約情報</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <param name="updatePerson">更新者</param>
		/// <returns>影響行数</returns>
		public static int UpdateSetHardSubscHeader(T_HARDSUBSC_HEADER header, string connectStr, string updatePerson)
		{
			try
			{
				return DatabaseAccess.UpdateSetDatabase(header.UpdateSetSqlString, header.GetUpdateSetParameters(updatePerson), connectStr);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}(UpdateSetHardSubscHeader)", ex.Message));
			}
		}

		/// <summary>
		/// 契約情報の削除
		/// </summary>
		/// <param name="rentalNo">貸出番号</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>影響行数</returns>
		public static int DeleteHardSubscHeader(int rentalNo, string connectStr)
		{
			try
			{
				string sqlStr = string.Format("DELETE FROM {0} WHERE [RentalNo] = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_HARDSUBSC_HEADER], rentalNo);
				return DatabaseAccess.DeleteDatabase(sqlStr, connectStr);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}(DeleteHardSubscHeader)", ex.Message));
			}
		}

		/// <summary>
		/// 機器情報の削除
		/// </summary>
		/// <param name="rentalNo">貸出番号</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <returns>影響行数</returns>
		public static int DeleteHardSubscDetail(int rentalNo, string connectStr)
		{
			try
			{
				string sqlStr = string.Format("DELETE FROM {0} WHERE [RentalNo] = {1}", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_HARDSUBSC_DETAIL], rentalNo);
				return DatabaseAccess.DeleteDatabase(sqlStr, connectStr);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("{0}(DeleteHardSubscDetail)", ex.Message));
			}
		}
	}
}
