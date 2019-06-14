using MwsLib.BaseFactory.NarcohmOrderCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MwsLib.Common;

namespace MwsLib.DB.SqlServer.NarcohmOrderCheck
{
    public static class NarcohmOrderCheckSetIO
    {
		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// ナルコーム製品申込情報の追加
		/// [Charlie].[dbo].[T_NARCOHM_APPLICATE_HEADER]
		/// [Charlie].[dbo].[T_NARCOHM_APPLICATE_DETAIL]
		/// </summary>
		/// <param name="data">ナルコーム製品申込情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoNarcohmApplicate(NarcohmApplicate data, bool sqlsv2)
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
							string sqlString = @"INSERT INTO T_NARCOHM_APPLICATE_HEADER VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18);"
													+ " SELECT SCOPE_IDENTITY()";
							SqlParameter[] param = {    new SqlParameter("@1", data.CustomerNo),	    // [CustomerNo]
														new SqlParameter("@2", data.TokuisakiNo),	    // [TokuisakiNo] 
														new SqlParameter("@3", data.ClinicName),	    // [ClinicName]
														new SqlParameter("@4", data.Telephone),		    // [Telephone]
														new SqlParameter("@5", data.Subject),			// [Subject] 
														new SqlParameter("@6", data.SectionCode),		// [SectionCode] 
														new SqlParameter("@7", data.SectionName),	    // [SectionName] 
														new SqlParameter("@8", data.BranchCode),	    // [BranchCode] 
														new SqlParameter("@9", data.BranchName),	    // [BranchName] 
														new SqlParameter("@10", data.SalesmanCode),		// [SalesmanCode] 
														new SqlParameter("@11", data.SalesmanName),	    // [SalesmanName] 
														new SqlParameter("@12", (data.ServiceStartDate.HasValue) ? data.ServiceStartDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	// [ServiceStartDate] 
														new SqlParameter("@13", data.SaleType),			// [SaleType] 
														new SqlParameter("@14", (data.MailSendDateTime.HasValue) ? data.MailSendDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),	            // [MailSendDate] 
														new SqlParameter("@15", (data.CreateDateTime.HasValue) ? data.CreateDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),	// [CreateDate] 
														new SqlParameter("@16", data.CreatePerson),	    // [CreatePerson]
														new SqlParameter("@17", (data.UpdateDateTime.HasValue) ? data.UpdateDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),    // [UpdateDate] 
														new SqlParameter("@18", data.UpdatePerson) };   // [UpdatePerson] 

							// 実行
							object iNewRowIdentity = DataBaseController.SqlExecuteScalar(con, tran, sqlString, param);
							if (null == iNewRowIdentity)
							{
								throw new ApplicationException("InsertIntoNarcohmApplicate() Error!");
							}
							// オートナンバーの取得
							data.ApplicateID = Convert.ToInt32(iNewRowIdentity);

							// ナルコーム製品申込詳細情報の追加
							sqlString = @"INSERT INTO T_NARCOHM_APPLICATE_DETAIL VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)";
							int i = 0;
							foreach (NarcohmApplicateDetail detail in data.DetailList)
							{
								detail.ApplicateID = data.ApplicateID;
								SqlParameter[] param2 = {   new SqlParameter("@1", detail.ApplicateID),		// [ApplicateID]
															new SqlParameter("@2", i),						// [SeqNo]
															new SqlParameter("@3", (detail.OrderNo.HasValue) ? detail.OrderNo.Value : System.Data.SqlTypes.SqlInt32.Null),	// [OrderNo] 
															new SqlParameter("@4", (detail.OrderDate.HasValue) ? detail.OrderDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	    // [OrderDate] 
															new SqlParameter("@5", detail.GoodsCode),		// [GoodsCode] 
															new SqlParameter("@6", detail.GoodsName),		// [GoodsName] 
															new SqlParameter("@7", detail.Price),			// [Price] 
															new SqlParameter("@8", detail.Count),			// [Count] 
															new SqlParameter("@9", detail.Total),			// [Total] 
															new SqlParameter("@10", (detail.UseStartDate.HasValue) ? detail.UseStartDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	// [UseStartDate] 
															new SqlParameter("@11", (detail.UseEndDate.HasValue) ? detail.UseEndDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null) };     // [UseEndtDate] 

								// 実行
								rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param2);
								if (rowCount <= -1)
								{
									throw new ApplicationException("InsertIntoNarcohmApplicate() Error!");
								}
								i++;
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

		/// <summary>
		/// ナルコーム製品申込情報の更新
		/// [Charlie].[dbo].[T_NARCOHM_APPLICATE_HEADER]
		/// [Charlie].[dbo].[T_NARCOHM_APPLICATE_DETAIL]
		/// </summary>
		/// <param name="data">ナルコーム製品申込情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateNarcohmApplicate(NarcohmApplicate data, bool sqlsv2)
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
							// ナルコーム製品申込情報の更新
							string sqlString = string.Format(@"UPDATE T_NARCOHM_APPLICATE_HEADER SET"
																+ " CustomerNo = @1"
                                                                + ", TokuisakiNo = @2"
                                                                + ", ClinicName = @3"
                                                                + ", Telephone = @4"
																+ ", Subject = @5"
                                                                + ", SectionCode = @6"
                                                                + ", SectionName = @7"
                                                                + ", BranchCode = @8"
                                                                + ", BranchName = @9"
                                                                + ", SalesmanCode = @10"
                                                                + ", SalesmanName = @11"
																+ ", ServiceStartDate = @12"
																+ ", SaleType = @13"
                                                                + ", MailSendDate = @14"
                                                                + ", CreateDate = @15"
                                                                + ", CreatePerson = @16"
                                                                + ", UpdateDate = @17"
                                                                + ", UpdatePerson = @18"
                                                                + " WHERE ApplicateID = {0}", data.ApplicateID);

                            SqlParameter[] param1 = {	new SqlParameter("@1", data.CustomerNo),	    // [CustomerNo]
														new SqlParameter("@2", data.TokuisakiNo),	    // [TokuisakiNo] 
														new SqlParameter("@3", data.ClinicName),	    // [ClinicName]
														new SqlParameter("@4", data.Telephone),		    // [Telephone]
														new SqlParameter("@5", data.Subject),			// [Subject] 
														new SqlParameter("@6", data.SectionCode),		// [SectionCode] 
														new SqlParameter("@7", data.SectionName),	    // [SectionName] 
														new SqlParameter("@8", data.BranchCode),	    // [BranchCode] 
														new SqlParameter("@9", data.BranchName),	    // [BranchName] 
														new SqlParameter("@10", data.SalesmanCode),		// [SalesmanCode] 
														new SqlParameter("@11", data.SalesmanName),	    // [SalesmanName] 
														new SqlParameter("@12", (data.ServiceStartDate.HasValue) ? data.ServiceStartDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),   // [ServiceStartDate] 
														new SqlParameter("@13", data.SaleType),			// [SaleType] 
														new SqlParameter("@14", (data.MailSendDateTime.HasValue) ? data.MailSendDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),				// [MailSendDate] 
														new SqlParameter("@15", (data.CreateDateTime.HasValue) ? data.CreateDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),	// [CreateDate] 
														new SqlParameter("@16", data.CreatePerson),	    // [CreatePerson]
														new SqlParameter("@17", (data.UpdateDateTime.HasValue) ? data.UpdateDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),    // [UpdateDate] 
														new SqlParameter("@18", data.UpdatePerson) };   // [UpdatePerson] 
							// 実行
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param1);
                            if (rowCount <= -1)
                            {
                                throw new ApplicationException("UpdateNarcohmApplicate() Error!");
                            }
							// ナルコーム製品申込詳細情報の削除
							sqlString = string.Format(@"DELETE FROM T_NARCOHM_APPLICATE_DETAIL WHERE ApplicateID = {0}", data.ApplicateID);
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString);
							if (rowCount <= -1)
							{
								throw new ApplicationException("UpdateNarcohmApplicate() Error!");
							}
							// ナルコーム製品申込詳細情報の追加
							sqlString = @"INSERT INTO T_NARCOHM_APPLICATE_DETAIL VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)";
							int i = 0;
							foreach (NarcohmApplicateDetail detail in data.DetailList)
							{
								SqlParameter[] param2 = {   new SqlParameter("@1", detail.ApplicateID),		// [ApplicateID]
															new SqlParameter("@2", i),						// [SeqNo]
															new SqlParameter("@3", (detail.OrderNo.HasValue) ? detail.OrderNo.Value : System.Data.SqlTypes.SqlInt32.Null),	// [OrderNo] 
															new SqlParameter("@4", (detail.OrderDate.HasValue) ? detail.OrderDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	    // [OrderDate] 
															new SqlParameter("@5", detail.GoodsCode),		// [GoodsCode] 
															new SqlParameter("@6", detail.GoodsName),		// [GoodsName] 
															new SqlParameter("@7", detail.Price),			// [Price] 
															new SqlParameter("@8", detail.Count),			// [Count] 
															new SqlParameter("@9", detail.Total),			// [Total] 
															new SqlParameter("@10", (detail.UseStartDate.HasValue) ? detail.UseStartDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	// [UseStartDate] 
															new SqlParameter("@11", (detail.UseEndDate.HasValue) ? detail.UseEndDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null) };		// [UseEndtDate] 
								// 実行
								rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param2);
								if (rowCount <= -1)
								{
									throw new ApplicationException("UpdateNarcohmApplicate() Error!");
								}
								i++;
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

		///// <summary>
		///// ナルコーム製品申込詳細情報リストの追加
		///// [Charlie].[dbo].[T_NARCOHM_APPLICATE_DETAIL]
		///// </summary>
		///// <param name="list">ナルコーム製品申込詳細情報リスト</param>
		///// <param name="sqlsv2">CT環境かどうか？</param>
		///// <returns>影響行数</returns>
		//public static int InsertIntoNarcohmApplicateDetailList(List<NarcohmApplicateDetail> list, bool sqlsv2)
		//{
		//	int rowCount = -1;
		//	using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateCharlieWebConnectionString(sqlsv2)))
		//	{
		//		try
		//		{
		//			// 接続
		//			con.Open();

		//			// トランザクション開始
		//			using (SqlTransaction tran = con.BeginTransaction())
		//			{
		//				try
		//				{
		//					string sqlString = @"INSERT INTO T_NARCOHM_APPLICATE_DETAIL VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11)";
		//					int i = 0;
		//					foreach (NarcohmApplicateDetail detail in list)
		//					{
		//						SqlParameter[] param = {    new SqlParameter("@1", detail.ApplicateID),	// [ApplicateID]
		//													new SqlParameter("@2", i),					// [SeqNo]
		//													new SqlParameter("@3", (detail.OrderNo.HasValue) ? detail.OrderNo.Value : System.Data.SqlTypes.SqlInt32.Null),	// [OrderNo] 
		//													new SqlParameter("@4", (detail.OrderDate.HasValue) ? detail.OrderDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	    // [OrderDate] 
		//													new SqlParameter("@5", detail.GoodsCode),	// [GoodsCode] 
		//													new SqlParameter("@6", detail.GoodsName),	// [GoodsName] 
		//													new SqlParameter("@7", detail.Price),		// [Price] 
		//													new SqlParameter("@8", detail.Count),		// [Count] 
		//													new SqlParameter("@9", detail.Total),		// [Total] 
		//													new SqlParameter("@10", (detail.UseStartDate.HasValue) ? detail.UseStartDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	// [UseStartDate] 
		//													new SqlParameter("@11", (detail.UseEndDate.HasValue) ? detail.UseEndDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null) };		// [UseEndtDate] 

		//						// 実行
		//						rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
		//						if (rowCount <= -1)
		//						{
		//							throw new ApplicationException("InsertIntoNarcohmApplicateDetailList() Error!");
		//						}
		//						i++;
		//					}
		//					// コミット
		//					tran.Commit();
		//				}
		//				catch
		//				{
		//					// ロールバック
		//					tran.Rollback();
		//					throw;
		//				}
		//			}
		//		}
		//		catch
		//		{
		//			throw;
		//		}
		//		finally
		//		{
		//			if (null != con)
		//			{
		//				// 切断
		//				con.Close();
		//			}
		//		}
		//	}
		//	return rowCount;
		//}
	}
}
