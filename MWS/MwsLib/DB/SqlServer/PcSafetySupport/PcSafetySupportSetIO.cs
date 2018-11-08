using MwsLib.BaseFactory.PcSafetySupport;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace MwsLib.DB.SqlServer.PcSafetySupport
{
	public static class PcSafetySupportSetIO
	{
		//////////////////////////////////////////////////////////////////
		/// JunpDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// ソフト保守メンテナンス情報の追加
		/// [JunpDB].[dbo].[tMik保守契約]
		/// </summary>
		/// <param name="data">ソフト保守メンテナンス情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoSoftMaintenanceContract(SoftMaintenanceContract data, bool sqlsv2)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(sqlsv2)))
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
							string sqlString = @"INSERT INTO tMik保守契約 VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20, @21, @22, @23, @24, @25, @26, @27, @28, @29, @30, @31, @32, @33)";
							SqlParameter[] param = { new SqlParameter("@1", data.CustomerNo),						// fhsCliMicID
													new SqlParameter("@2", (data.Subscription) ? '1': '0'),			// fhsS保守
													new SqlParameter("@3", (data.CollectionDate.HasValue) ? data.CollectionDate.Value.ToString() : string.Empty),	// fhsS契約書回収年月
													new SqlParameter("@4", null),									// fhsS売計上
													new SqlParameter("@5", data.AgreeYear),							// fhsS契約年数
													new SqlParameter("@6", data.Price),								// fhsSメンテ料金
													new SqlParameter("@7", (data.StartYM.HasValue) ? data.StartYM.Value.ToString() : string.Empty),		// fhsSメンテ契約開始
													new SqlParameter("@8", (data.EndYM.HasValue) ? data.EndYM.Value.ToString() : string.Empty),		// fhsSメンテ契約終了
													new SqlParameter("@9", data.Remark1),							// fhsSメンテ契約備考1
													new SqlParameter("@10", data.Remark2),							// fhsSメンテ契約備考2
													new SqlParameter("@11", null),									// fhsS契約名義
													new SqlParameter("@12", null),									// fhsSメンテ請求先コード
													new SqlParameter("@13", null),									// fhsSメンテ請求先名
													new SqlParameter("@14", 0),										// fhsSメンテ区分
													new SqlParameter("@15", null),									// fhsS卸BM先名
													new SqlParameter("@16", 0),										// fhsS金額
													new SqlParameter("@17", '0'),									// fhsH保守
													new SqlParameter("@18", null),									// fhsH契約書回収年月
													new SqlParameter("@19", '0'),									// fhsH売計上
													new SqlParameter("@20", null),									// fhsH契約年数
													new SqlParameter("@21", null),									// fhsHメンテ料金
													new SqlParameter("@22", null),									// fhsHメンテ契約開始
													new SqlParameter("@23", null),									// fhsHメンテ契約終了
													new SqlParameter("@24", null),									// fhsHメンテ契約備考1
													new SqlParameter("@25", null),									// fhsHメンテ契約備考2
													new SqlParameter("@26", null),									// fhsH契約名義
													new SqlParameter("@27", null),									// fhsHメンテ請求先コード
													new SqlParameter("@28", null),									// fhsHメンテ請求先名
													new SqlParameter("@29", '0'),									// fhsHメンテ区分
													new SqlParameter("@30", null),									// fhsH卸BM先名
													new SqlParameter("@31", null),									// fhsH金額
													new SqlParameter("@32", DateTime.Now),							// fhs更新日
													new SqlParameter("@33", "PcSafetySupport") };					// fhs更新者

							// 実行
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
							if (rowCount <= -1)
							{
								throw new ApplicationException("InsertIntoSoftMaintenanceContract() Error!");
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
		/// ソフト保守メンテナンス情報の更新
		/// [JunpDB].[dbo].[tMik保守契約]
		/// </summary>
		/// <param name="data">ソフト保守メンテナンス情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdateSoftMaintenanceContract(SoftMaintenanceContract data, bool sqlsv2)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(DataBaseAccess.CreateJunpWebConnectionString(sqlsv2)))
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
							string sqlString = string.Format(@"UPDATE tMik保守契約 SET fhsS保守 = @1, fhsS契約書回収年月 = @2, fhsS契約年数 = @3, fhsSメンテ料金 = @4, fhsSメンテ契約開始 = @5, fhsSメンテ契約終了 = @6, fhsSメンテ契約備考1 = @7, fhsSメンテ契約備考2 = @8, fhs更新日 = @9, fhs更新者 = @10 WHERE fhsCliMicID = {0}", data.CustomerNo);
							SqlParameter[] param = { new SqlParameter("@1", (data.Subscription) ? '1': '0'),
													new SqlParameter("@2", (data.CollectionDate.HasValue) ? data.CollectionDate.Value.ToString() : string.Empty),
													new SqlParameter("@3", data.AgreeYear),
													new SqlParameter("@4", data.Price),
													new SqlParameter("@5", (data.StartYM.HasValue) ? data.StartYM.Value.ToString() : string.Empty),
													new SqlParameter("@6", (data.EndYM.HasValue) ? data.EndYM.Value.ToString() : string.Empty),
													new SqlParameter("@7", data.Remark1),
													new SqlParameter("@8", data.Remark2),
													new SqlParameter("@9", DateTime.Now),
													new SqlParameter("@10", "PcSafetySupport") };
							// 実行
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
							if (rowCount <= -1)
							{
								throw new ApplicationException("UpdateSoftMaintenanceContract() Error!");
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


		//////////////////////////////////////////////////////////////////
		/// CharlieDB
		//////////////////////////////////////////////////////////////////

		/// <summary>
		/// PC安心サポート管理情報の追加
		/// [Charlie].[dbo].[T_PC_SUPPORT_CONTORL]
		/// </summary>
		/// <param name="data">PC安心サポート管理情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoPcSupportControl(PcSupportControl data, bool sqlsv2)
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
							string sqlString = @"INSERT INTO T_PC_SUPPORT_CONTROL VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20, @21, @22, @23, @24, @25, @26, @27, @28)";
							SqlParameter[] param = { new SqlParameter("@1", data.OrderNo),								// ORDER_NO
													new SqlParameter("@2", data.CustomerNo),							// CUSTOMER_ID
													new SqlParameter("@3", data.GoodsID),								// GOODS_ID
													new SqlParameter("@4", (data.StartDate.HasValue) ? data.StartDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),			// START_DATE
													new SqlParameter("@5", (data.EndDate.HasValue) ? data.EndDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),				// END_DATE
													new SqlParameter("@6", (data.PeriodEndDate.HasValue) ? data.PeriodEndDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	// PERIOD_END_DATE
													new SqlParameter("@7", data.AgreeYear),								// AGREE_YEAR
													new SqlParameter("@8", data.Price),									// PRICE
													new SqlParameter("@9", data.SalesmanID),							// MARKETING_SPECIALIST_ID
													new SqlParameter("@10", data.BranchID),								// BRANCH_ID
													new SqlParameter("@11", (data.OrderDate.HasValue) ? data.OrderDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),			// ORDER_DATE
													new SqlParameter("@12", (data.OrderReportAccept) ? '1' : '0'),		// ORDER_REPORT_ACCEPT
													new SqlParameter("@13", (data.OrderApprovalDate.HasValue) ? data.OrderApprovalDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	// ORDER_APPROVAL_DATE
													new SqlParameter("@14", data.MailAddress),							// MAIL_ADDRESS
													new SqlParameter("@15", data.Remark1),								// REMARK1
													new SqlParameter("@16", data.Remark2),								// REMARK2
													new SqlParameter("@17", (data.StartMailDateTime.HasValue) ? data.StartMailDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),		// START_MAIL_DATE
													new SqlParameter("@18", (data.GuideMailDateTime.HasValue) ? data.GuideMailDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),		// GUIDE_MAIL_DATE
													new SqlParameter("@19", (data.UpdateMailDateTime.HasValue) ? data.UpdateMailDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),	// UPDATE_MAIL_DATE
													new SqlParameter("@20", (data.CancelDate.HasValue) ? data.CancelDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),		// CANCEL_DATE
													new SqlParameter("@21", (data.CancelReportAccept) ? '1' : '0'),		// CANCEL_REPORT_ACCEPT
													new SqlParameter("@22", data.CancelReason),							// CANCEL_REASON
													new SqlParameter("@23", (data.DisableFlag) ? '1' : '0'),			// DISABLE_FLAG
													new SqlParameter("@24", (data.WonderWebRenewalFlag) ? '1' : '0'),	// WW_RENEWAL_FLAG
													new SqlParameter("@25", (data.CreateDateTime.HasValue) ? data.CreateDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),			// CREATE_DATE
													new SqlParameter("@26", data.CreatePerson),							// CREATE_PERSON
													new SqlParameter("@27", (data.UpdateDateTime.HasValue) ? data.UpdateDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),			// UPDATE_DATE
													new SqlParameter("@28", data.UpdatePerson) };						// UPDATE_PERSON

							// 実行
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
							if (rowCount <= -1)
							{
								throw new ApplicationException("InsertIntoPcSupportControl() Error!");
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
		/// PC安心サポート管理情報の更新
		/// [Charlie].[dbo].[T_PC_SUPPORT_CONTORL]
		/// </summary>
		/// <param name="data">PC安心サポート管理情報</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int UpdatePcSupportControl(PcSupportControl data, bool sqlsv2) 
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
							string sqlString = string.Format(@"UPDATE T_PC_SUPPORT_CONTROL SET"
																+ " CUSTOMER_ID = @1"
																+ ", GOODS_ID = @2"
																+ ", START_DATE = @3"
																+ ", END_DATE = @4"
																+ ", PERIOD_END_DATE = @5"
																+ ", AGREE_YEAR = @6"
																+ ", PRICE = @7"
																+ ", MARKETING_SPECIALIST_ID = @8"
																+ ", BRANCH_ID = @9"
																+ ", ORDER_DATE = @10"
																+ ", ORDER_REPORT_ACCEPT = @11"
																+ ", ORDER_APPROVAL_DATE = @12"
																+ ", MAIL_ADDRESS = @13"
																+ ", REMARK1 = @14"
																+ ", REMARK2 = @15"
																+ ", START_MAIL_DATE = @16"
																+ ", GUIDE_MAIL_DATE = @17"
																+ ", UPDATE_MAIL_DATE = @18"
																+ ", CANCEL_DATE = @19"
																+ ", CANCEL_REPORT_ACCEPT = @20"
																+ ", CANCEL_REASON = @21"
																+ ", DISABLE_FLAG = @22"
																+ ", WW_RENEWAL_FLAG = @23"
																+ ", CREATE_DATE = @24"
																+ ", CREATE_PERSON = @25"
																+ ", UPDATE_DATE = @26"
																+ ", UPDATE_PERSON = @27"
																+ " WHERE ORDER_NO = '{0}'", data.OrderNo);
							SqlParameter[] param = { new SqlParameter("@1", data.CustomerNo),
													new SqlParameter("@2", data.GoodsID),
													new SqlParameter("@3", (data.StartDate.HasValue) ? data.StartDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@4", (data.EndDate.HasValue) ? data.EndDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@5", (data.PeriodEndDate.HasValue) ? data.PeriodEndDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@6", data.AgreeYear),
													new SqlParameter("@7", data.Price),
													new SqlParameter("@8", data.SalesmanID),
													new SqlParameter("@9", data.BranchID),
													new SqlParameter("@10", (data.OrderDate.HasValue) ? data.OrderDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@11", (data.OrderReportAccept) ? '1' : '0'),
													new SqlParameter("@12", (data.OrderApprovalDate.HasValue) ? data.OrderApprovalDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@13", data.MailAddress),
													new SqlParameter("@14", data.Remark1),
													new SqlParameter("@15", data.Remark2),
													new SqlParameter("@16", (data.StartMailDateTime.HasValue) ? data.StartMailDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@17", (data.GuideMailDateTime.HasValue) ? data.GuideMailDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@18", (data.UpdateMailDateTime.HasValue) ? data.UpdateMailDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@19", (data.CancelDate.HasValue) ? data.CancelDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@20", (data.CancelReportAccept) ? '1' : '0'),
													new SqlParameter("@21", data.CancelReason),
													new SqlParameter("@22", (data.DisableFlag) ? '1' : '0'),
													new SqlParameter("@23", (data.WonderWebRenewalFlag) ? '1' : '0'),
													new SqlParameter("@24", (data.CreateDateTime.HasValue) ? data.CreateDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@25", data.CreatePerson),
													new SqlParameter("@26", (data.UpdateDateTime.HasValue) ? data.UpdateDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@27", data.UpdatePerson) };
							// 実行
							rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
							if (rowCount <= -1)
							{
								throw new ApplicationException("UpdatePcSupportControl() Error!");
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
		/// PC安心サポート送信メール情報リストの追加
		/// [Charlie].[dbo].[T_PC_SUPPORT_MAIL]
		/// </summary>
		/// <param name="list">PC安心サポート送信メール情報リスト</param>
		/// <param name="sqlsv2">CT環境かどうか？</param>
		/// <returns>影響行数</returns>
		public static int InsertIntoPcSupportMailList(List<PcSupportMail> list, bool sqlsv2)
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
							string sqlString = @"INSERT INTO T_PC_SUPPORT_MAIL VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13)";
							foreach (PcSupportMail data in list)
							{
								SqlParameter[] param = { new SqlParameter("@1", data.OrderNo),			// ORDER_NO
														new SqlParameter("@2", data.CustomerNo),		// CUSTOMER_ID
														new SqlParameter("@3", data.SendMailType),		// SEND_MAIL_TYPE
														new SqlParameter("@4", data.GoodsID),			// GOODS_ID
														new SqlParameter("@5", (data.StartDate.HasValue) ? data.StartDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	// START_DATE
														new SqlParameter("@6", (data.EndDate.HasValue) ? data.EndDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),		// END_DATE
														new SqlParameter("@7", data.AgreeYear),			// AGREE_YEAR
														new SqlParameter("@8", data.Price),				// PRICE
														new SqlParameter("@9", data.SalesmanID),		// MARKETING_SPECIALIST_ID
														new SqlParameter("@10", data.BranchID),			// BRANCH_ID
														new SqlParameter("@11", (data.OrderDate.HasValue) ? data.OrderDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	// ORDER_DATE
														new SqlParameter("@12", data.MailAddress),		// MAIL_ADDRESS
														new SqlParameter("@13", (data.SendDateTime.HasValue) ? data.SendDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null) };		// SEND_DATE

								// 実行
								rowCount = DataBaseController.SqlExecuteCommand(con, tran, sqlString, param);
								if (rowCount <= -1)
								{
									throw new ApplicationException("InsertIntoPcSupportMail() Error!");
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
