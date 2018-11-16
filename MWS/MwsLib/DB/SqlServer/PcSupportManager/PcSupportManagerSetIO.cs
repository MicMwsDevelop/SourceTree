using MwsLib.BaseFactory.PcSupportManager;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MwsLib.DB.SqlServer.PcSupportManager
{
	public static class PcSupportManagerSetIO
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
													new SqlParameter("@3", (data.CollectionDate.HasValue) ? data.CollectionDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),	// fhsS契約書回収年月
													new SqlParameter("@4", '0'),									// fhsS売計上
													new SqlParameter("@5", data.AgreeYear),							// fhsS契約年数
													new SqlParameter("@6", data.Price),								// fhsSメンテ料金
													new SqlParameter("@7", (data.StartYM.HasValue) ? data.StartYM.Value.ToString() : System.Data.SqlTypes.SqlString.Null),	// fhsSメンテ契約開始
													new SqlParameter("@8", (data.EndYM.HasValue) ? data.EndYM.Value.ToString() : System.Data.SqlTypes.SqlString.Null),		// fhsSメンテ契約終了
													new SqlParameter("@9", data.Remark1),							// fhsSメンテ契約備考1
													new SqlParameter("@10", System.Data.SqlTypes.SqlString.Null),	// fhsSメンテ契約備考2
													new SqlParameter("@11", System.Data.SqlTypes.SqlString.Null),	// fhsS契約名義
													new SqlParameter("@12", System.Data.SqlTypes.SqlString.Null),	// fhsSメンテ請求先コード
													new SqlParameter("@13", System.Data.SqlTypes.SqlString.Null),	// fhsSメンテ請求先名
													new SqlParameter("@14", '0'),									// fhsSメンテ区分
													new SqlParameter("@15", System.Data.SqlTypes.SqlString.Null),	// fhsS卸BM先名
													new SqlParameter("@16", (System.Data.SqlTypes.SqlInt32)0),		// fhsS金額
													new SqlParameter("@17", '0'),									// fhsH保守
													new SqlParameter("@18", System.Data.SqlTypes.SqlString.Null),	// fhsH契約書回収年月
													new SqlParameter("@19", '0'),									// fhsH売計上
													new SqlParameter("@20", (System.Data.SqlTypes.SqlInt32)0),		// fhsH契約年数
													new SqlParameter("@21", (System.Data.SqlTypes.SqlInt32)0),		// fhsHメンテ料金
													new SqlParameter("@22", System.Data.SqlTypes.SqlString.Null),	// fhsHメンテ契約開始
													new SqlParameter("@23", System.Data.SqlTypes.SqlString.Null),	// fhsHメンテ契約終了
													new SqlParameter("@24", System.Data.SqlTypes.SqlString.Null),	// fhsHメンテ契約備考1
													new SqlParameter("@25", System.Data.SqlTypes.SqlString.Null),	// fhsHメンテ契約備考2
													new SqlParameter("@26", System.Data.SqlTypes.SqlString.Null),	// fhsH契約名義
													new SqlParameter("@27", System.Data.SqlTypes.SqlString.Null),	// fhsHメンテ請求先コード
													new SqlParameter("@28", System.Data.SqlTypes.SqlString.Null),	// fhsHメンテ請求先名
													new SqlParameter("@29", '0'),									// fhsHメンテ区分
													new SqlParameter("@30", System.Data.SqlTypes.SqlString.Null),	// fhsH卸BM先名
													new SqlParameter("@31", (System.Data.SqlTypes.SqlInt32)0),		// fhsH金額
													new SqlParameter("@32", DateTime.Now),							// fhs更新日
													new SqlParameter("@33", PcSupportControl.PERSON_NAME) };		// fhs更新者

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
							string sqlString = string.Format(@"UPDATE tMik保守契約 SET fhsS保守 = @1, fhsS契約書回収年月 = @2, fhsS契約年数 = @3, fhsSメンテ料金 = @4, fhsSメンテ契約開始 = @5, fhsSメンテ契約終了 = @6, fhsSメンテ契約備考1 = @7, fhs更新日 = @8, fhs更新者 = @9 WHERE fhsCliMicID = {0}", data.CustomerNo);
							SqlParameter[] param = { new SqlParameter("@1", (data.Subscription) ? '1': '0'),
													new SqlParameter("@2", (data.CollectionDate.HasValue) ? data.CollectionDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
													new SqlParameter("@3", data.AgreeYear),
													new SqlParameter("@4", data.Price),
													new SqlParameter("@5", (data.StartYM.HasValue) ? data.StartYM.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
													new SqlParameter("@6", (data.EndYM.HasValue) ? data.EndYM.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
													new SqlParameter("@7", data.Remark1),
													new SqlParameter("@8", DateTime.Now),
													new SqlParameter("@9", PcSupportControl.PERSON_NAME) };
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
							string sqlString = @"INSERT INTO T_PC_SUPPORT_CONTROL VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20, @21, @22, @23, @24, @25, @26, @27, @28, @29, @30, @31)";
							SqlParameter[] param = { new SqlParameter("@1", data.OrderNo),								// ORDER_NO
													new SqlParameter("@2", data.CustomerNo),							// CUSTOMER_ID
													new SqlParameter("@3", data.ClinicName),							// CLINIC_NAME
													new SqlParameter("@4", data.GoodsID),								// GOODS_ID
													new SqlParameter("@5", data.GoodsName),								// GOODS_NAME
													new SqlParameter("@6", data.Price),									// PRICE
													new SqlParameter("@7", data.AgreeYear),								// AGREE_YEAR
													new SqlParameter("@8", (data.StartDate.HasValue) ? data.StartDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),			// START_DATE
													new SqlParameter("@9", (data.EndDate.HasValue) ? data.EndDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),				// END_DATE
													new SqlParameter("@10", (data.PeriodEndDate.HasValue) ? data.PeriodEndDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	// PERIOD_END_DATE
													new SqlParameter("@11", data.BranchID),								// BRANCH_ID
													new SqlParameter("@12", data.BranchName),							// BRANCH_NAME
													new SqlParameter("@13", data.SalesmanID),							// SALESMAN_ID
													new SqlParameter("@14", data.SalesmanName),							// SALESMAN_NAME
													new SqlParameter("@15", (data.OrderDate.HasValue) ? data.OrderDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),			// ORDER_DATE
													new SqlParameter("@16", (data.OrderReportAccept) ? '1' : '0'),		// ORDER_REPORT_ACCEPT
													new SqlParameter("@17", (data.OrderApprovalDate.HasValue) ? data.OrderApprovalDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	// ORDER_APPROVAL_DATE
													new SqlParameter("@18", data.MailAddress),							// MAIL_ADDRESS
													new SqlParameter("@19", data.Remark),								// REMARK
													new SqlParameter("@20", (data.StartMailDateTime.HasValue) ? data.StartMailDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),		// START_MAIL_DATE
													new SqlParameter("@21", (data.GuideMailDateTime.HasValue) ? data.GuideMailDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),		// GUIDE_MAIL_DATE
													new SqlParameter("@22", (data.UpdateMailDateTime.HasValue) ? data.UpdateMailDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),	// UPDATE_MAIL_DATE
													new SqlParameter("@23", (data.CancelDate.HasValue) ? data.CancelDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),		// CANCEL_DATE
													new SqlParameter("@24", (data.CancelReportAccept) ? '1' : '0'),		// CANCEL_REPORT_ACCEPT
													new SqlParameter("@25", data.CancelReason),							// CANCEL_REASON
													new SqlParameter("@26", (data.DisableFlag) ? '1' : '0'),			// DISABLE_FLAG
													new SqlParameter("@27", (data.WonderWebRenewalFlag) ? '1' : '0'),	// WW_RENEWAL_FLAG
													new SqlParameter("@28", (data.CreateDateTime.HasValue) ? data.CreateDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),			// CREATE_DATE
													new SqlParameter("@29", data.CreatePerson),							// CREATE_PERSON
													new SqlParameter("@30", (data.UpdateDateTime.HasValue) ? data.UpdateDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),			// UPDATE_DATE
													new SqlParameter("@31", data.UpdatePerson) };						// UPDATE_PERSON

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
																+ ", CLINIC_NAME = @2"
																+ ", GOODS_ID = @3"
																+ ", GOODS_NAME = @4"
																+ ", PRICE = @5"
																+ ", AGREE_YEAR = @6"
																+ ", START_DATE = @7"
																+ ", END_DATE = @8"
																+ ", PERIOD_END_DATE = @9"
																+ ", BRANCH_ID = @10"
																+ ", BRANCH_NAME = @11"
																+ ", SALESMAN_ID = @12"
																+ ", SALESMAN_NAME = @13"
																+ ", ORDER_DATE = @14"
																+ ", ORDER_REPORT_ACCEPT = @15"
																+ ", ORDER_APPROVAL_DATE = @16"
																+ ", MAIL_ADDRESS = @17"
																+ ", REMARK = @18"
																+ ", START_MAIL_DATE = @19"
																+ ", GUIDE_MAIL_DATE = @20"
																+ ", UPDATE_MAIL_DATE = @21"
																+ ", CANCEL_DATE = @22"
																+ ", CANCEL_REPORT_ACCEPT = @23"
																+ ", CANCEL_REASON = @24"
																+ ", DISABLE_FLAG = @25"
																+ ", WW_RENEWAL_FLAG = @26"
																+ ", CREATE_DATE = @27"
																+ ", CREATE_PERSON = @28"
																+ ", UPDATE_DATE = @29"
																+ ", UPDATE_PERSON = @30"
																+ " WHERE ORDER_NO = '{0}'", data.OrderNo);

							SqlParameter[] param = { new SqlParameter("@1", data.CustomerNo),
													new SqlParameter("@2", data.ClinicName),
													new SqlParameter("@3", data.GoodsID),
													new SqlParameter("@4", data.GoodsName),
													new SqlParameter("@5", data.Price),
													new SqlParameter("@6", data.AgreeYear),
													new SqlParameter("@7", (data.StartDate.HasValue) ? data.StartDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@8", (data.EndDate.HasValue) ? data.EndDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@9", (data.PeriodEndDate.HasValue) ? data.PeriodEndDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@10", data.BranchID),
													new SqlParameter("@11", data.BranchName),
													new SqlParameter("@12", data.SalesmanID),
													new SqlParameter("@13", data.SalesmanName),
													new SqlParameter("@14", (data.OrderDate.HasValue) ? data.OrderDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@15", (data.OrderReportAccept) ? '1' : '0'),
													new SqlParameter("@16", (data.OrderApprovalDate.HasValue) ? data.OrderApprovalDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@17", data.MailAddress),
													new SqlParameter("@18", data.Remark),
													new SqlParameter("@19", (data.StartMailDateTime.HasValue) ? data.StartMailDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@20", (data.GuideMailDateTime.HasValue) ? data.GuideMailDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@21", (data.UpdateMailDateTime.HasValue) ? data.UpdateMailDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@22", (data.CancelDate.HasValue) ? data.CancelDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@23", (data.CancelReportAccept) ? '1' : '0'),
													new SqlParameter("@24", data.CancelReason),
													new SqlParameter("@25", (data.DisableFlag) ? '1' : '0'),
													new SqlParameter("@26", (data.WonderWebRenewalFlag) ? '1' : '0'),
													new SqlParameter("@27", (data.CreateDateTime.HasValue) ? data.CreateDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@28", data.CreatePerson),
													new SqlParameter("@29", (data.UpdateDateTime.HasValue) ? data.UpdateDateTime.Value : System.Data.SqlTypes.SqlDateTime.Null),
													new SqlParameter("@30", data.UpdatePerson) };
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
		public static int InsertIntoPcSupportMailList(List<PcSupportMail> list, bool sqlsv2 = false)
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
														new SqlParameter("@5", data.Price),				// PRICE
														new SqlParameter("@6", data.AgreeYear),			// AGREE_YEAR
														new SqlParameter("@7", (data.StartDate.HasValue) ? data.StartDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),	// START_DATE
														new SqlParameter("@8", (data.EndDate.HasValue) ? data.EndDate.Value.ToDateTime() : System.Data.SqlTypes.SqlDateTime.Null),		// END_DATE
														new SqlParameter("@9", data.BranchID),			// BRANCH_ID
														new SqlParameter("@10", data.SalesmanID),		// SALESMAN_ID
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
