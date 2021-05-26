//
// ApplyTypeMatomeSetIO.cs
//
// 申込種別まとめ情報 データ格納クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/02/08 勝呂)
// 
using MwsLib.BaseFactory.ApplyTypeMatome;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.ApplyTypeMatome
{
	public static class ApplyTypeMatomeSetIO
	{
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// 申込種別の更新
		/// [Charlie].[dbo].[T_CUSTOMER_FOUNDATIONS]
		/// </summary>
		/// <param name="list">申込種別まとめ情報リスト</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateApplyTypeMatome(List<ApplyTypeMatomeData> list, bool sqlsv2)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieWebConnectionString(sqlsv2)))
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
							foreach (ApplyTypeMatomeData apply in list)
							{
								string sqlString = string.Format(@"UPDATE T_CUSTOMER_FOUNDATIONS SET"
																	+ " APPLY_TYPE = @1"
																	+ ", RENEWAL_FLG = @2"
																	+ ", UPDATE_DATE = @3"
																	+ ", UPDATE_PERSON = @4"
																	+ " WHERE CUSTOMER_ID = '{0}'", apply.CustomerNo);

								SqlParameter[] param = { new SqlParameter("@1", (int)apply.ApplyType),
														new SqlParameter("@2", 1),
														new SqlParameter("@3", DateTime.Now),
														new SqlParameter("@4", "申込種別変更ツール") };
								// 実行
								rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
								if (rowCount <= -1)
								{
									throw new ApplicationException("UpdateApplyType() Error!");
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
				catch
				{
					throw;
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
	}
}
