using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.DB;
using CommonLib.DB.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace MorningBatch.BaseFactory
{
	public class CusDataUpdate
	{
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CusDataUpdate()
		{
		}

		/// <summary>
		/// 顧客情報入出力メイン処理
		/// </summary>
		/// <param name="pmode">1:COUPLER管理.顧客利用情報作成からの実行 2:顧客情報入出力からの実行→設定値：1</param>
		/// <param name="argout1a_1flg">画面.顧客情報チェック状態(0:未選択 1:選択中）→設定値：1</param>
		/// <param name="argout1a_2flg">画面.サービス利用情報チェック状態(0:未選択 1:選択中）→設定値：1</param>
		/// <param name="argout1a_1">顧客情報最終出力日時→設定値：null</param>
		/// <param name="argout1a_2">サービス利用情報最終出力日時→設定値：null</param>
		/// <returns>結果</returns>
		public bool CusDataUpdate_Main(string pmode, string argout1a_1flg, string argout1a_2flg, DateTime? argout1a_1, DateTime? argout1a_2)
		{
			bool bCusDataUpdate_Main = false;           // メイン戻り値
			bool bCusDataUpdate_User_All = false;       // 全件顧客データ処理実行戻り値
			bool bCusDataUpdate_User_Diff = false;      // 差分顧客データ実行戻り値
			bool bCusDataUpdate_Service_All = false;    // 全件利用機能データ処理実行戻り値
			bool bCusDataUpdate_Service_Diff = false;   // 差分利用機能データ実行戻り値
			string out1aSwitch = string.Empty;          // 顧客データ処理判定（1:全件 2:差分）
			string out1bSwitch = string.Empty;          // 利用機能データ処理判定（1:全件 2:差分）
			bool bCusDataUpdate_User = false;
			bool bCusDataUpdate_Service = false;

			string result = string.Empty;
			try
			{
				string sLogFileNameManager = string.Format("imp_bat_{0}", DateTime.Now.ToString());
				if (File.Exists("sLockFile"))
				{
					Trace.WriteLine("*** インポートバッチ開始失敗（他のバッチ実行中） ***");
					result = "false";
				}
				else
				{
					// ロックファイル作成
					File.Open("sLockFile", FileMode.Append);
				}
				Trace.WriteLine("***インポートバッチ開始 ***");

				// Coupler Ver2.7.0 ワークテーブルを操作して、テーブルをSWするため基本は、システム閉塞はしない
				// 顧客データまたは、利用データを全件出力する場合は、 システム閉塞していないとエラー
				if (argout1a_1flg == "1" && argout1a_1flg == "" && argout1a_2flg == "1" && false == argout1a_2.HasValue)
				{
					Trace.WriteLine("*** システム閉塞中 ***");
					result = "false";
				}
				if (0 == result.Length)
				{
					// CusDataUpdate_Main処理
					DateTime? customerLastDate = argout1a_1;	// 顧客情報最終出力日時
					DateTime? serviceUseLastDate = argout1a_2;	// サービス利用情報最終出力日時
					if ("1" == pmode)
					{
						// 顧客利用情報作成からの実行
						customerLastDate = SEL_DCHFNC_U005_Customer_LastFileCreateDate("1");
						serviceUseLastDate = SEL_DCHFNC_U005_Customer_LastFileCreateDate("2");
						Trace.WriteLine("*** COUPLER管理（顧客利用情報作成）から実行されました。 ***");
					}
					else
					{
						// 顧客情報入出力からの実行
						Trace.WriteLine("*** COUPLER管理（顧客情報入出力）から実行されました。 ***");
					}
					// 基本機能パックの商品ID・サービス種別ＩＤ・サービスＩＤを取得しセッション変数に保存する。（※顧客情報・サービス利用情報取得時の条件に使用）
					Trace.WriteLine("CHARLIEDBから基本機能パック取得開始 【PCA商品区分=200】");
					DataTable kihonPack = GetKihonPack(200);
					if (null != kihonPack && 0 < kihonPack.Rows.Count)
					{
						DataRow row = kihonPack.Rows[0];
						Trace.WriteLine(string.Format("CHARLIEDBから基本機能パック取得終了 【商品ID={0}/サービス種別ID={1}/サービスID={2}】"
																		, row["GOODS_ID"].ToString().Trim()
																		, DataBaseValue.ConvObjectToInt(row["SERVICE_TYPE_ID"])
																		, DataBaseValue.ConvObjectToInt(row["SERVICE_ID"])));
					}
					else
					{
						Trace.WriteLine("CHARLIEDBから基本機能パック取得終了 【商品ID= /サービス種別ID=/サービスID=】");
					}
					// 画面.顧客情報のチェック状態がONの場合、または顧客利用情報作成処理から差分実行された場合
					List<stU005_out1a> out1aList = null;
					if ("1" == argout1a_1flg || "1" == pmode)
					{
						// ■■■■■■■■■■■■■■ CHARLIEDBから顧客情報取得 ■■■■■■■■■■■■■■
						Trace.WriteLine("*** CHARLIEDB.顧客データ抽出SQL開始 ***");
						DataTable qryU005_out1a = Sel_DCHFNC_U005_Customer_Datainout(true, customerLastDate, serviceUseLastDate);
						Trace.WriteLine("*** CHARLIEDB.顧客データ抽出SQL終了 ***");

						if (null == qryU005_out1a || 0 == qryU005_out1a.Rows.Count)
						{
							Trace.WriteLine(string.Format("*** CHARLIEDB.顧客データ件数：0件 【最終更新日={0}】***", customerLastDate.HasValue ? customerLastDate.Value.ToString() : ""));
						}
						else
						{
							if (customerLastDate.HasValue)
							{
								// 顧客情報処理判定（1:全件 2:差分）
								out1aSwitch = "2";
							}
							else
							{
								// 顧客情報処理判定（1:全件 2:差分）
								out1aSwitch = "1";
							}
							out1aList = stU005_out1a.DataTableToList(qryU005_out1a, customerLastDate);
							Trace.WriteLine(string.Format("*** CHARLIEDB.顧客データ件数：{0} 件 【{1}（1:全件 2:差分）/最終更新日={2}】***"
																			, qryU005_out1a.Rows.Count
																			, customerLastDate.HasValue ? "2" : "1"
																			, customerLastDate.HasValue ? customerLastDate.Value.ToString() : ""));
						}
						// CHARLIEDB.ファイル出力履歴（顧客データ）の最終出力日を登録（※該当データが0件の場合でも登録）
						Trace.WriteLine("CHARLIEDB.ファイル出力履歴(顧客データ）の最終出力日登録開始");
						bool bout1aUp = INS_DCHFNC_U005_Customer_FileCreate("1");
						Trace.WriteLine(string.Format("CHARLIEDB.ファイル出力履歴(顧客データ）の最終出力日登録終了【{0}】", bout1aUp.ToString()));
					}
					// 画面.サービス利用情報のチェック状態がONの場合、または顧客利用情報作成処理から差分実行された場合
					List<stU005_out1b> out1bList = null;
					if ("1" == argout1a_2flg || "1" == pmode)
					{
						Trace.WriteLine("*** CHARLIEDB.利用機能データ抽出SQL開始 ***");
						DataTable qryU005_out1b = Sel_DCHFNC_U005_Customer_Datainout(false, customerLastDate, serviceUseLastDate);
						Trace.WriteLine("*** CHARLIEDB.利用機能データ抽出SQL終了 ***");

						if (null == qryU005_out1b || 0 == qryU005_out1b.Rows.Count)
						{
							Trace.WriteLine(string.Format("*** CHARLIEDB.利用機能データ件数：0件 【最終更新日={0}】***", serviceUseLastDate.HasValue ? serviceUseLastDate.Value.ToString() : ""));
						}
						else
						{
							if (serviceUseLastDate.HasValue)
							{
								// サービス利用情報処理判定
								out1bSwitch = "2";  // 利用機能データ処理判定（1:全件 2:差分）
							}
							else
							{
								// サービス利用情報処理判定
								out1bSwitch = "1";  // 利用機能データ処理判定（1:全件 2:差分）
							}
							int iServiceNoOut;
							out1bList = stU005_out1b.DataTableToList(qryU005_out1b, serviceUseLastDate, out iServiceNoOut);
							Trace.WriteLine(string.Format("*** CHARLIEDB.利用機能データ件数：{0} 件 / 警告件数：{1} 件【{2}（1:全件 2:差分）/最終更新日={3}】***"
													, out1bList.Count
													, iServiceNoOut
													, serviceUseLastDate.HasValue ? "2" : "1"
													, serviceUseLastDate.HasValue ? serviceUseLastDate.Value.ToString() : ""));
						}
						// CHARLIEDB.ファイル出力履歴（利用機能データ）の最終出力日を登録（※該当データが0件の場合でも登録）
						Trace.WriteLine("CHARLIEDB.ファイル出力履歴(利用機能データ）の最終出力日登録開始");
						bool bout1bUp = INS_DCHFNC_U005_Customer_FileCreate("2");
						Trace.WriteLine(string.Format("CHARLIEDB.ファイル出力履歴(利用機能データ）の最終出力日登録終了【{0}】", bout1bUp.ToString()));
					}
					// ■■■■■■■■■■■■■■顧客情報出力処理■■■■■■■■■■■■■■
					if (0 < out1aList.Count && "" != out1aSwitch)
					{
						if ("1" == out1aSwitch)
						{
							/*
							// 全件処理は行わないので、解析作業の必要なし
							// ■■■■■■■■全件処理■■■■■■■■
							Trace.WriteLine("顧客データ（全件）登録開始");
							string sbatUserImportAll = batUserImportAll(out1aList, out1aSwitch, argout1a_1flg, argout1a_2flg);
							if ("0" == sbatUserImportAll)
							{
								// ①COUPLERDB.製品顧客管理情報ワークの名前を変更する
								string sProductUserWorkReNmTable = string.Format("PRODUCTUSER_WORK_{0}", DateTime.Now.ToString("yyyyMMdd"));
								Trace.WriteLine(string.Format("COUPLERDB.製品顧客管理情報ワーク(PRODUCTUSER_WORK)→COUPLERDB.製品顧客管理情報ワーク({0})テーブル名変更開始", sProductUserWorkReNmTable));
								// EXEC sp_rename 'PRODUCTUSER_WORK', sProductUserWorkReNmTable

								Trace.WriteLine(string.Format("COUPLERDB.製品顧客管理情報ワーク(PRODUCTUSER_WORK)→COUPLERDB.製品顧客管理情報ワーク({0})テーブル名変更終了", sProductUserWorkReNmTable));

								// ②COUPLERDB.製品顧客管理情報の名前を製品顧客管理情報ワークの名前に変更する
								Trace.WriteLine("COUPLERDB.製品顧客管理情報(PRODUCTUSER)→COUPLERDB.製品顧客管理情報ワーク(PRODUCTUSER_WORK)テーブル名変更開始");
								// EXEC sp_rename 'PRODUCTUSER','PRODUCTUSER_WORK'

								// ①で変更したテーブル名をCOUPLERDB.製品顧客管理情報の名前に変更する
								Trace.WriteLine(string.Format("COUPLERDB.製品顧客管理情報ワーク({0})→COUPLERDB.製品顧客管理情報(PRODUCTUSER)テーブル名変更開始", sProductUserWorkReNmTable));
								// EXEC sp_rename '#sProductUserWorkReNmTable#','PRODUCTUSER'

								Trace.WriteLine(string.Format("COUPLERDB.製品顧客管理情報ワーク({0})→COUPLERDB.製品顧客管理情報(PRODUCTUSER)テーブル名変更終了", sProductUserWorkReNmTable));

								bCusDataUpdate_User_All = true;
							}
							*/
						}
						else if ("2" == out1aSwitch)
						{
							// --- ■■■■■■■■差分処理■■■■■■■■ ---
							Trace.WriteLine("顧客データ（差分）登録開始");
							int sCusDataUpdate_User_Diff = CusDataUpdate_User_Diff(out1aList, out1aSwitch, argout1a_1flg, argout1a_2flg);
							Trace.WriteLine(string.Format("顧客データ（差分）登録終了({0})", sCusDataUpdate_User_Diff));
							if (0 == sCusDataUpdate_User_Diff)
							{
								// 実行結果ステータス 0:正常,1:異常,2:ファイル無し
								bCusDataUpdate_User_Diff = true;
							}
						}
					}
					// ■■■■■■■■■■■■■■サービス利用情報出力処理■■■■■■■■■■■■■■
					if (0 < out1bList.Count && "" != out1bSwitch)
					{
						if ("1" == out1bSwitch)
						{
							/*
							// 全件処理は行わないので、解析作業の必要なし
							// ■■■■■■■■全件処理■■■■■■■■
							Trace.WriteLine("サービスデータ（全件）登録開始");
							string sbatServiceImportAll = batServiceImportAll(out1bList, out1bSwitch, DateTime.Now);
							Trace.WriteLine(string.Format("サービスデータ（全件）登録終了({0})", sbatServiceImportAll));
							if ("0" == sbatServiceImportAll)
							{
								bCusDataUpdate_Service_All = true;
							}
							*/
						}
						else if ("2" == out1bSwitch)
						{
							// ■■■■■■■■差分処理■■■■■■■■
							Trace.WriteLine("サービスデータ（差分）登録開始");
							int sCusDataUpdate_Sevice_Diff = CusDataUpdate_Service_Diff(out1bList, out1bSwitch, argout1a_1flg, argout1a_2flg);
							Trace.WriteLine(string.Format("サービスデータ（差分）登録終了({0})", sCusDataUpdate_Sevice_Diff));
							if (0 == sCusDataUpdate_Sevice_Diff)
							{
								bCusDataUpdate_Service_Diff = true;
							}
						}
					}
					else if (0 == out1bList.Count)
					{
						if ("1" == out1bSwitch)
						{
							bCusDataUpdate_Service_All = true;
						}
						else if ("2" == out1bSwitch)
						{
							bCusDataUpdate_Service_Diff = true;
						}
					}
					// 画面.顧客情報・サービス利用情報がチェックされている
					if ("1" == argout1a_1flg && "1" == argout1a_2flg)
					{
						// 顧客情報
						if ("1" == out1aSwitch)
						{
							bCusDataUpdate_User = bCusDataUpdate_User_All;
						}
						else if ("2" == out1aSwitch)
						{
							bCusDataUpdate_User = bCusDataUpdate_User_Diff;
						}
						else if ("" == out1aSwitch)
						{
							bCusDataUpdate_User = true;
						}
						// サービス利用情報
						if ("1" == out1bSwitch)
						{
							bCusDataUpdate_Service = bCusDataUpdate_Service_All;
						}
						else if ("2" == out1bSwitch)
						{
							bCusDataUpdate_Service = bCusDataUpdate_Service_Diff;
						}
						else if ("" == out1bSwitch)
						{
							bCusDataUpdate_Service = true;
						}
					}
					// 顧客情報・サービス利用情報ともエラーがなければ正常
					if (true == bCusDataUpdate_User && true == bCusDataUpdate_Service)
					{
						bCusDataUpdate_Main = true;
					}
				}
				else
				{
					// 閉塞
					bCusDataUpdate_Main = false;
				}
				if (true == bCusDataUpdate_Main)
				{
					result = "OK";
				}
				else
				{
					result = "NG";
				}
				Trace.WriteLine(string.Format("*** インポートバッチ終了 ({0}) ***", result));
			}
			catch (Exception ex)
			{
				if (bCusDataUpdate_Main)
				{
					result = "OK";
				}
				else
				{
					result = "NG";
				}
				Trace.WriteLine(string.Format("*** エラー内容 ({0})***", ex.Message));
				Trace.WriteLine(string.Format("*** インポートバッチ終了(catch) ({0}) ***", result));
			}
			finally
			{
				if (File.Exists("sLockFile"))
				{
					// 処理が終了したためロックファイルを削除
					File.Delete("sLockFile");
				}
			}
			return bCusDataUpdate_Main;
		}

		/// <summary>
		/// 基本機能パックの商品ID・サービス種別ＩＤ・サービスＩＤを取得
		/// </summary>
		/// <param name="argBRAND_CLASSIFICATION">商品ID=200</param>
		/// <returns>処理結果</returns>
		private DataTable GetKihonPack(int argBRAND_CLASSIFICATION)
		{
			try
			{
				string sql = string.Format("SELECT A.GOODS_ID, A.SERVICE_TYPE_ID, A.SERVICE_ID"
										+ " FROM [charlieDB].[dbo].[M_CODE] as  A"
										+ " INNER JOIN [charlieDB].[dbo].[V_PCA_GOODS] B ON A.GOODS_ID = B.GOODS_ID AND B.BRAND_CLASSIFICATION = {0}"
										+ " WHERE A.DELETE_FLG = '0' AND SET_SALE = '1'"
										, argBRAND_CLASSIFICATION);
				return DatabaseAccess.SelectDatabase(sql, Program.gConnectStr);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("GetKihonPack() Error({0})", ex.Message));
			}
		}

		/// <summary>
		/// 連携ファイルの最終出力日取得
		/// </summary>
		/// <param name="argFlg">ファイル種類NO（顧客情報=1,サービス利用情報=2）</param>
		/// <returns>処理結果（最終出力日）</returns>
		private DateTime? SEL_DCHFNC_U005_Customer_LastFileCreateDate(string argFlg)
		{
			try
			{
				string sql = string.Format("SELECT MAX(FILE_CREATEDATE) LASTDATE FROM [charlieDB].[dbo].[T_FILE_CREATEDATE] WHERE FILE_TYPE = '{0}'", argFlg);
				DataTable table = DatabaseAccess.SelectDatabase(sql, Program.gConnectStr);
				if (null != table && 1 == table.Rows.Count)
				{
					return DataBaseValue.ConvObjectToDateTimeNull(table.Rows[0]["LASTDATE"]);
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("SEL_DCHFNC_U005_Customer_LastFileCreateDate() Error({0})", ex.Message));
			}
			return null;
		}

		/// <summary>
		/// 連携ファイルの出力日格納
		/// </summary>
		/// <param name="argFlg">ファイル種類NO（顧客=1,利用=2）</param>
		/// <returns>処理結果</returns>
		private bool INS_DCHFNC_U005_Customer_FileCreate(string argFlg)
		{
			try
			{
				string sql = "INSERT INTO [charlieDB].[dbo].[T_FILE_CREATEDATE] VALUES (@1, @2, @3, @4)";
				SqlParameter[] param = {
										new SqlParameter("@1", DateTime.Today.ToString()),
										new SqlParameter("@2", argFlg),
										new SqlParameter("@3", DateTime.Today.ToString()),
										new SqlParameter("@4", "CFADMIN")
									};
				if (0 < DatabaseAccess.InsertIntoDatabase(sql, param, Program.gConnectStr))
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("INS_DCHFNC_U005_Customer_FileCreate() Error({0})", ex.Message));
			}
			return false;
		}

		/// <summary>
		/// 顧客情報入出力（顧客情報/サービス利用情報）
		/// </summary>
		/// <param name="out1a">処理判断フラグ true:顧客情報、false:サービス利用情報</param>
		/// <param name="customerLastDate">顧客情報最終出力日時</param>
		/// <param name="serviceUseLastDate">サービス利用情報最終出力日時</param>
		/// <returns>処理結果</returns>
		private DataTable Sel_DCHFNC_U005_Customer_Datainout(bool out1a, DateTime? customerLastDate, DateTime? serviceUseLastDate)
		{
			try
			{
				string sql = string.Empty;
				string sWhere01 = string.Empty;
				string sWhere02 = string.Empty;
				string sWhere03 = string.Empty;
				string sWhere04 = string.Empty;
				if (out1a)
				{
					// Coupler連携（顧客情報）
					if (customerLastDate.HasValue)
					{
						//差分出力の場合は削除レコードも出力対象 & 出力時間以降
						sWhere01 = string.Format(" AND (CF.UPDATE_DATE > '{0}' OR PC.UPDATE_DATE > '{0}' OR VC.UPDATE_TIME > '{0}')", customerLastDate.Value.ToString());
						sWhere02 = string.Format(" AND (CF.UPDATE_DATE > '{0}' OR PC.UPDATE_DATE > '{0}' OR VC.UPDATE_TIME > '{0}')", customerLastDate.Value.ToString());
						sWhere03 = string.Format(" AND (DU.UPDATE_DATE > '{0}' OR PC.UPDATE_DATE > '{0}')", customerLastDate.Value.ToString());
					}
					// 2014/05/02 差分出力対応(顧客基本と製品情報の更新日をみて抽出)
					sql = @"SELECT"
							+ "  PC.PRODUCT_ID"
							+ ", PC.PASSWORD"
							+ ", PC.USER_CLASSIFICATION"
							+ ", PC.TRIAL_FLG"
							+ ", PC.CUSTOMER_ID"
							+ ", VC.CUSTOMER_NAME1 + VC.CUSTOMER_NAME2 as CUSTOMER_NAME"
							+ ", VB.BRANCH_MAIL"
							+ ", (SELECT TOP 1 MAIL_ADDRESS FROM [charlieDB].[dbo].[M_MAIL]) as MAIL1"
							+ ", LTRIM(RTRIM(VC.USE_SYSTEM_CODE)) as USE_SYSTEM_CODE"
							+ ", VC.CLIENT_LICENSES"
							+ ", CASE PC.END_FLG"
							+ "    WHEN '2' THEN '1'"
							+ "    ELSE PC.END_FLG"
							+ " END as END_FLG"
							+ ", CONVERT(varchar, PC.TRIAL_START_DATE, 111) as TRIAL_START_DATE"
							+ ", CONVERT(varchar, PC.PERIOD_END_DATE, 111) as TRIAL_END_DATE"
							+ ", CF.DELETE_FLG"
							+ " FROM [charlieDB].[dbo].[T_PRODUCT_CONTROL] as PC"
							+ " LEFT JOIN [charlieDB].[dbo].[V_CUSTOMER] as VC on PC.CUSTOMER_ID = VC.CUSTOMER_ID"
							+ " LEFT JOIN [charlieDB].[dbo].[V_BRANCH_INFORMATION] as VB on RTRIM(VC.BRANCH_ID) = RTRIM(VB.BRANCH_ID)"
							+ " LEFT JOIN [charlieDB].[dbo].[T_CUSTOMER_FOUNDATIONS] as CF on PC.CUSTOMER_ID = CF.CUSTOMER_ID"
							+ " WHERE PC.TRIAL_FLG = 0"	// 通常
							+ " AND VC.CUSTOMER_ID is not null"
							+ " AND PC.TRIAL_START_DATE is not null"
							+ " AND PC.USER_CLASSIFICATION <= 1"	// デモユーザ以外
							+ sWhere01

							+ " UNION ALL"
							+ " SELECT"
							+ " PC.PRODUCT_ID"
							+ ", PC.PASSWORD"
							+ ", PC.USER_CLASSIFICATION"
							+ ", PC.TRIAL_FLG"
							+ ", PC.CUSTOMER_ID"
							+ ", VC.CUSTOMER_NAME1 + VC.CUSTOMER_NAME2 as CUSTOMER_NAME"
							+ ", VB.BRANCH_MAIL"
							+ ", (SELECT TOP 1 MAIL_ADDRESS FROM [charlieDB].[dbo].[M_MAIL]) as MAIL1"
							+ ", LTRIM(RTRIM(VC.USE_SYSTEM_CODE)) as USE_SYSTEM_CODE"
							+ ", VC.CLIENT_LICENSES"
							+ ", CASE PC.END_FLG"
							+ "    WHEN '2' THEN '1'"
							+ "    ELSE PC.END_FLG"
							+ "  END as END_FLG"
							+ ", CONVERT(varchar, PC.TRIAL_START_DATE, 111) as TRIAL_START_DATE"
							+ ", CONVERT(varchar, PC.PERIOD_END_DATE, 111) as TRIAL_END_DATE"
							+ ", CF.DELETE_FLG"
							+ " FROM [charlieDB].[dbo].[T_PRODUCT_CONTROL] as PC"
							+ " LEFT JOIN [charlieDB].[dbo].[V_CUSTOMER] as VC on PC.CUSTOMER_ID = VC.CUSTOMER_ID"
							+ " LEFT JOIN [charlieDB].[dbo].[V_BRANCH_INFORMATION] as VB on RTRIM(VC.BRANCH_ID) = RTRIM(VB.BRANCH_ID)"
							+ " LEFT JOIN [charlieDB].[dbo].[T_CUSTOMER_FOUNDATIONS] as CF on PC.CUSTOMER_ID = CF.CUSTOMER_ID"
							+ " WHERE PC.TRIAL_FLG = 1"	// 体験版
							+ " AND PC.TRIAL_START_DATE is not null"
							+ " AND PC.USER_CLASSIFICATION <= 1"	// デモユーザ以外
							+ sWhere02
							+ " UNION ALL"
							// デモユーザ対応
							+ " SELECT"
							+ " PC.PRODUCT_ID"
							+ ", PC.PASSWORD"
							+ ", PC.USER_CLASSIFICATION"
							+ ", PC.TRIAL_FLG"
							+ ", PC.CUSTOMER_ID"
							+ ", DU.NAME as CUSTOMER_NAME"
							+ ", DU.MAILADDR1 as BRANCH_MAIL"
							+ ", DU.MAILADDR2 as MAIL1"
							+ ", '100' AS USE_SYSTEM_CODE"
							+ ", '0' as CLIENT_LICENSES"
							+ ", CASE PC.END_FLG"
							+ "    WHEN '2' THEN '1'"
							+ "    ELSE PC.END_FLG"
							+ "  END as END_FLG"
							+ ", CONVERT(varchar, PC.TRIAL_START_DATE, 111) as TRIAL_START_DATE"
							+ ", CONVERT(varchar, PC.PERIOD_END_DATE, 111) as TRIAL_END_DATE"
							+ ", DU.DELETE_FLG"
							+ " FROM [charlieDB].[dbo].[T_PRODUCT_CONTROL] as PC"
							+ " LEFT JOIN [charlieDB].[dbo].[T_DEMO_USER] as DU on PC.CUSTOMER_ID = DU.CUSTOMER_ID"
							+ " WHERE PC.USER_CLASSIFICATION >= 2"	// デモユーザ
							+ " AND DU.CUSTOMER_ID is not null"
							+ " AND DU.END_FLG = N'0' AND DU.DELETE_FLG = N'0'"
							+ " AND PC.TRIAL_START_DATE is not null"
							+ sWhere03
							+ " ORDER BY PC.PRODUCT_ID";
				}
				else
				{
					// Coupler連携（サービス利用情報）
					if (false == serviceUseLastDate.HasValue)
					{
						// 全件出力の場合は削除されていないレコードを出力
						sWhere01 = " CUI.DELETE_FLG = 0 AND ";
						sWhere02 = " AND DU.DELETE_FLG = N'0'";
					}
					else
					{
						// 差分出力の場合は削除レコードも出力対象 & 出力時間以降
						// 2015/03/31 h-chiba 利用開始日終了日が入って無ければ差分に関係なく出力して警告を出すように変更
						sWhere03 = string.Format(" AND ((CASE WHEN CUI.UPDATE_DATE is null THEN CUI.CREATE_DATE"
																+ " WHEN CUI.UPDATE_DATE is not null THEN CUI.UPDATE_DATE"
																+ " END) > '{0}' OR (PC.PRODUCT_ID is not null AND CUI.USE_START_DATE is null))", serviceUseLastDate.Value.ToString());
						sWhere04 = string.Format(" AND (DU.UPDATE_DATE > '{0}' OR PC.UPDATE_DATE > '{0}')", serviceUseLastDate.Value.ToString());
					}
					// 2014/05/02 差分出力対応(利用情報の更新日をみて抽出（※削除フラグを更新した場合が問題なので検討すること）)
					sql = @"SELECT * FROM"
							+ " (SELECT"
							+ "  CUI.CUSTOMER_ID"
							+ ", PC.PRODUCT_ID"
							+ ", CUI.SERVICE_ID"
							+ ", CUI.PAUSE_END_STATUS"
							+ ", '0' as SET_SALE"
							+ ", CONVERT(varchar, CUI.USE_START_DATE, 111) as USE_START_DATE"
							+ ", CONVERT(varchar, CUI.PERIOD_END_DATE, 111) as USE_END_DATE"
							+ ", CUI.DELETE_FLG"
							+ " FROM [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION] as CUI"
							+ " LEFT JOIN [charlieDB].[dbo].[T_PRODUCT_CONTROL] as PC ON CUI.CUSTOMER_ID = PC.CUSTOMER_ID AND PC.USER_CLASSIFICATION IN (0, 1)"
							+ " LEFT JOIN [charlieDB].[dbo].[M_SERVICE] as M ON CUI.SERVICE_ID = M.SERVICE_ID"
							+ " WHERE"
							+ sWhere01
							+ " CUI.SERVICE_TYPE_ID <> 1 AND"
							+ " CUI.SERVICE_ID <> 1001 AND M.UMU_FLG = 0"
							+ " AND PC.USER_CLASSIFICATION IN (0, 1) AND PC.PRODUCT_ID is not null"
							+ sWhere03
							+ " UNION ALL"
							+ " SELECT"
							+ "  DU.CUSTOMER_ID"
							+ ", PC.PRODUCT_ID"
							+ ", DU.SERVICE_ID"
							+ ", 0 as PAUSE_END_STATUS"
							+ ", 0 as SET_SALE"
							+ ", CONVERT(varchar, PC.TRIAL_START_DATE, 111) as USE_START_DATE"
							+ ", CONVERT(varchar, PC.PERIOD_END_DATE, 111) as USE_END_DATE"
							+ ", DU.DELETE_FLG"
							+ " FROM [charlieDB].[dbo].[T_DEMO_USER] as DU"
							+ " INNER JOIN [charlieDB].[dbo].[T_PRODUCT_CONTROL] as PC ON DU.CUSTOMER_ID = PC.CUSTOMER_ID CROSS JOIN [charlieDB].[dbo].[M_DEMO_SERVICE] as DM"
							+ " WHERE DU.END_FLG = N'0'"
							+ sWhere02
							+ " AND DM.DEMO_USE_FLG = N'1'"
							+ " AND DM.SERVICE_ID <> 1001"
							+ sWhere04
							+ ") tbl"
							+ " ORDER BY PRODUCT_ID, SERVICE_ID";
				}
				return DatabaseAccess.SelectDatabase(sql, Program.gConnectStr);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("Sel_DCHFNC_U005_Customer_Datainout() Error({0})", ex.Message));
			}
		}

		/// <summary>
		/// 顧客情報入出力（差分）顧客データ出力処理※userdataimp.cfmを流用
		/// </summary>
		/// <param name="out1aList">顧客データリスト</param>
		/// <param name="out1aSwitch">顧客データ処理判定（1:全件 2:差分）</param>
		/// <param name="out1a_1flg">顧客データチェック状態(0:未選択 1:選択）</param>
		/// <param name="out1a_2flg">利用機能データチェック状態(0:未選択 1:選択）</param>
		/// <returns>判定</returns>
		private int CusDataUpdate_User_Diff(List<stU005_out1a> out1aList, string out1aSwitch, string out1a_1flg, string out1a_2flg)
		{
			// 実行結果ステータス 0:正常,1:異常,2:ファイル無し
			int iStatusUser = 0;

			// ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼ 顧客データ差分反映開始 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
			if (0 < out1aList.Count && "2" == out1aSwitch)
			{
				Trace.WriteLine("差分インポートバッチ開始 (1)");
				Trace.WriteLine("トランザクション開始");
				if (0 < out1aList.Count)
				{
					Trace.WriteLine("ユーザ情報インポートDB処理開始");
					// 差分出力の場合（削除フラグあり）
					foreach (stU005_out1a out1a in out1aList)
					{
						// ----------------入力チェック--------------------
						string sErr;
						if (false == out1a.ErrorCheck(out sErr))
						{
							if (out1a.DELETE_FLG)
							{
								// PRODUCTUSERの削除
								if (false == qImpDeleteUserData(out1a.PRODUCT_ID))
								{
									Trace.WriteLine(string.Format("ユーザ情報の削除でエラーが発生しました。({0})", out1a.PRODUCT_ID));
									iStatusUser = 1;  // ユーザー情報異常
									break;
								}
							}
							else
							{
								// 登録・更新
								qImpProductUser(out1a);
							}
						}
						else
						{
							Trace.WriteLine(string.Format("{0}({1})", sErr, out1a.PRODUCT_ID));
							iStatusUser = 1;  // ユーザー情報異常
							break;
						}
					}
					Trace.WriteLine("ユーザ情報インポートDB処理終了");
					if (0 == iStatusUser && "1" == out1a_1flg && "0" == out1a_2flg)
					{
						Trace.WriteLine("ユーザ情報をコミットしました。");
					}
				}
				else
				{
					// インポートデータが0件の場合のログレベルを下げる
					Trace.WriteLine("ユーザ情報インポートデータがありません");
					iStatusUser = 2;      // ユーザ情報インポートデータなし
					Trace.WriteLine("ユーザ情報をロールバックしました。");
				}
			}
			else
			{
				iStatusUser = 0;
			}
			// ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲ 顧客データ反映終了 ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
			Trace.WriteLine("差分インポートバッチ終了");

			// 顧客データ差分反映が正常に終了した、またはインポートデータがない かつ、利用機能データ差分反映が正常に終了した、またはインポートデータがない
			return iStatusUser;
		}

		/// <summary>
		/// 顧客データの物理削除
		/// </summary>
		/// <param name="cp_id">MWSID</param>
		/// <returns>処理結果</returns>
		private bool qImpDeleteUserData(string cp_id)
		{
			try
			{
				string sql = string.Format("DELETE FROM PRODUCTUSER WHERE CP_ID = '{0}'", cp_id);
				if (-1 != DatabaseAccess.DeleteDatabase(sql, Program.gConnectStr))
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("qImpDeleteUserData() Error({0})", ex.Message));
			}
			return false;
		}

		/// <summary>
		/// 顧客データの登録/更新
		/// </summary>
		/// <param name="out1a">顧客情報</param>
		/// <returns>処理結果</returns>
		private bool qImpProductUser(stU005_out1a out1a)
		{
			try
			{
				// 更新
				string sql = string.Format("SELECT count(*) FROM PRODUCTUSER WHERE cp_id = '{0}'", out1a.PRODUCT_ID);
				DataTable table = DatabaseAccess.SelectDatabase(sql, Program.gConnectStr);
				if (null != table && 0 < table.Rows.Count)
				{
					sql = string.Format(@"UPDATE {0} SET user_type = @1, trial_flg = @2, end_flg = @3, customer_id = @4, customer_nm = @5, email1 = @6"
										+ ", email2 = @7, login_start_date = @8, login_end_date = @9, default_paswd = @10, license_count = @11"
										+ ", update_date = @12, update_user = @13"
										+ " WHERE cp_id = '{1}'", out1a.PRODUCT_ID);
					SqlParameter[] param = {
											new SqlParameter("@1", (int)out1a.USER_CLASSIFICATION),
											new SqlParameter("@2", (out1a.TRIAL_FLG) ? 0 : 1),
											new SqlParameter("@3", (out1a.END_FLG) ? 0 : 1),
											new SqlParameter("@4", out1a.CUSTOMER_ID),
											new SqlParameter("@5", out1a.CUSTOMER_NAME),
											new SqlParameter("@6", out1a.BRANCH_MAIL),
											new SqlParameter("@7", out1a.MAIL1),
											new SqlParameter("@8", out1a.TRIAL_START_DATE.HasValue ? out1a.TRIAL_START_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
											new SqlParameter("@9", out1a.TRIAL_END_DATE.HasValue ? out1a.TRIAL_END_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
											new SqlParameter("@10", out1a.PASSWORD),
											new SqlParameter("@11", out1a.CLIENT_LICENSES),
											new SqlParameter("@12", DateTime.Now.ToString()),
											new SqlParameter("@13", "BAT")
										};
					if (0 < DatabaseAccess.UpdateSetDatabase(sql, param, Program.gConnectStr))
					{
						return true;
					}
				}
				else
				{
					// 登録
					return qInsProductUserWork(out1a);
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("qImpProductUser() Error({0})", ex.Message));
			}
			return false;
		}

		/// <summary>
		/// 顧客データの追加
		/// </summary>
		/// <param name="out1a"></param>
		/// <returns>処理結果</returns>
		private bool qInsProductUserWork(stU005_out1a out1a)
		{
			try
			{
				string sql = "INSERT INTO PRODUCTUSER (cp_id, user_type, trial_flg, end_flg, customer_id, customer_nm, email1, email2, login_start_date, login_end_date, default_paswd, license_count, create_date, create_user) VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14)";
				SqlParameter[] param = {
											new SqlParameter("@1", out1a.PRODUCT_ID),
											new SqlParameter("@2", (int)out1a.USER_CLASSIFICATION),
											new SqlParameter("@3", (out1a.TRIAL_FLG) ? 0 : 1),
											new SqlParameter("@4", (out1a.END_FLG) ? 0 : 1),
											new SqlParameter("@5", out1a.CUSTOMER_ID),
											new SqlParameter("@6", out1a.CUSTOMER_NAME),
											new SqlParameter("@7", out1a.BRANCH_MAIL),
											new SqlParameter("@8", out1a.MAIL1),
											new SqlParameter("@9", out1a.TRIAL_START_DATE.HasValue ? out1a.TRIAL_START_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
											new SqlParameter("@10", out1a.TRIAL_END_DATE.HasValue ? out1a.TRIAL_END_DATE.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
											new SqlParameter("@11", out1a.PASSWORD),
											new SqlParameter("@12", out1a.CLIENT_LICENSES),
											new SqlParameter("@13", DateTime.Now.ToString()),
											new SqlParameter("@14", "BAT")
										};
				if (0 < DatabaseAccess.InsertIntoDatabase(sql, param, Program.gConnectStr))
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("qInsProductUserWork() Error({0})", ex.Message));
			}
			return false;
		}

		/// <summary>
		/// 顧客情報入出力（差分）サービス利用情報出力処理※userdataimp.cfmを流用
		/// </summary>
		/// <param name="out1bList">サービス利用情報リスト</param>
		/// <param name="out1bSwitch">利用機能データ処理判定（1:全件 2:差分）</param>
		/// <param name="out1a_1flg">顧客情報チェック状態(0:未選択 1:選択）</param>
		/// <param name="out1a_2flg">サービス利用情報チェック状態(0:未選択 1:選択）</param>
		/// <returns>判定</returns>
		private int CusDataUpdate_Service_Diff(List<stU005_out1b> out1bList, string out1bSwitch, string out1a_1flg, string out1a_2flg)
		{
			int iServiceStatus  = 0;

			// ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼ 利用機能データ反映開始 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
			// 利用データが選択されている、かつユーザ情報インポートが正常に終了した場合
			if (0 < out1bList.Count)
			{
				Trace.WriteLine("差分インポートバッチ開始 (2)");
				Trace.WriteLine("トランザクション開始");

				// サービス情報インポート開始日時セット
				DateTime dStartDateTime = DateTime.Now;

				// サービス情報インポート
				Trace.WriteLine("サービス情報読み込み開始");
				List<CouplerService> csList = new List<CouplerService>();
				foreach (stU005_out1b out1b in out1bList)
				{
					CouplerService cs = new CouplerService();
					cs.cp_id = out1b.PRODUCT_ID;
					cs.service_id = out1b.SERVICE_ID;
					cs.contrac_type = out1b.PAUSE_END_STATUS;
					cs.payment_type = out1b.SET_SALE;
					cs.start_date = out1b.USE_START_DATE;
					cs.end_date = out1b.USE_END_DATE;
					cs.imp_type = out1b.DELETE_FLG;
					csList.Add(cs);
				}
				// 2013/11/11 kondo システム未反映の申込み情報をＤＢから読み込み配列に格納
				List<CouplerApply> applyList = qGetApplyNoSystem();

				// ******データベース登録 * *****
				if (0 < csList.Count)
				{
					Trace.WriteLine("サービス情報インポートDB処理開始");
					foreach (CouplerService cs in csList)
					{
						if (cs.imp_type)
						{
							// 削除
							// SERVICEからレコードの削除
							qImpDeleteService(cs.cp_id, cs.service_id);
						}
						else
						{
							// 登録・更新
							// SERVICEにレコードの追加または更新
							qImpService(cs);

							// APPLYのシステム未反映フラグの更新
							if (null != applyList && 0 < applyList.Count)
							{
								List<CouplerApply> applys = applyList.FindAll(p => p.cp_id == cs.cp_id && p.service_id == cs.service_id && p.apply_type == cs.contrac_type);
								if (null != applys && 0 < applys.Count)
								{
									qUpdApply(applys);
								}
							}
						}
					}
					Trace.WriteLine("サービス情報インポートDB処理終了");
				}
			}
			return 0;
		}

		/// <summary>
		/// 申込情報取得（システム未反映のみ)
		/// </summary>
		/// <returns>申込情報</returns>
		private List<CouplerApply> qGetApplyNoSystem()
		{
			try
			{
				string sql = "SELECT apply_id, cp_id, service_id, apply_type"
									+ " FROM APPLY"
									+ " WHERE cp_id LIKE 'MWS%' AND system_flg = '0' AND apply_type <> 2"
									+ " ORDER BY cp_id, service_id, apply_type";
				DataTable table = DatabaseAccess.SelectDatabase(sql, Program.gConnectStr);
				return CouplerApply.DataTableToList(table);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("qGetApplyNoSystem() Error({0})", ex.Message));
			}
		}

		/// <summary>
		/// カプラーサービス情報の削除
		/// </summary>
		/// <param name="cp_id">MWSID</param>
		/// <param name="serviceID">サービスID</param>
		/// <returns>処理結果</returns>
		private bool qImpDeleteService(string cp_id, int serviceID)
		{
			try
			{
				string sql = string.Format("DELETE FROM SERVICE WHERE cp_ic = '{0}' AND service_id = {1}", cp_id, serviceID);
				if (-1 != DatabaseAccess.DeleteDatabase(sql, Program.gConnectStr))
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("qImpDeleteService() Error({0})", ex.Message));
			}
			return false;
		}

		/// <summary>
		/// カプラーサービス情報の追加/更新
		/// </summary>
		/// <param name="cs"></param>
		/// <returns></returns>
		/// <exception cref="ApplicationException"></exception>
		private bool qImpService(CouplerService cs)
		{
			if (cs.imp_type)
			{
				// 削除
				return false;
			}
			try
			{
				string sql = string.Format("SELECT cp_id, service_id FROM SERVICE"
														+ " WHERE cp_id = '{0}' AND service_id = {1}", cs.cp_id, cs.service_id);
				DataTable table = DatabaseAccess.SelectDatabase(sql, Program.gConnectStr);
				if (null != table && 0 < table.Rows.Count)
				{
					// レコード更新
					sql = string.Format(@"UPDATE SERVICE SET start_date = @1, end_date = @2, contrac_type = @3, update_date = @4, update_user = @5"
										+ " WHERE cp_id = '{0}' AND service_id = {1}", cs.cp_id, cs.service_id);
					SqlParameter[] param = {
															new SqlParameter("@1", cs.start_date.HasValue ? cs.start_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
															new SqlParameter("@2", cs.end_date.HasValue ? cs.end_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
															new SqlParameter("@3", cs.contrac_type),
															new SqlParameter("@4", DateTime.Now.ToString()),
															new SqlParameter("@5", "BAT")
														};
					if (0 < DatabaseAccess.UpdateSetDatabase(sql, param, Program.gConnectStr))
					{
						return true;
					}
				}
				else
				{
					// レコード追加
					sql = @"INSERT INTO SERVICE VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10)";
					SqlParameter[] param = {
															new SqlParameter("@1", cs.cp_id),
															new SqlParameter("@2", cs.service_id),
															new SqlParameter("@3", cs.start_date.HasValue ? cs.start_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
															new SqlParameter("@4", cs.end_date.HasValue ? cs.end_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
															new SqlParameter("@5", cs.contrac_type),
															new SqlParameter("@6", 0),
															new SqlParameter("@7", DateTime.Now.ToString()),
															new SqlParameter("@8", "BAT"),
															new SqlParameter("@9", DateTime.Now.ToString()),
															new SqlParameter("@10", "BAT")
														};
					if (0 < DatabaseAccess.InsertIntoDatabase(sql, param, Program.gConnectStr))
					{
						return true;
					}
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("qImpService() Error({0})", ex.Message));
			}
			return false;
		}

		/// <summary>
		/// カプラー申込情報 システム反映フラグの更新
		/// </summary>
		/// <param name="applyList">カプラー申込情報リスト</param>
		/// <returns>判定</returns>
		/// <exception cref="ApplicationException"></exception>
		private bool qUpdApply(List<CouplerApply> applyList)
		{
			try
			{
				if (null != applyList && 0 < applyList.Count)
				{
					foreach (CouplerApply apply in applyList)
					{
						string sql = string.Format(@"UPDATE APPLY SET system_flg = @1, update_date = @2, update_user = @3"
																	+ " WHERE apply_id = {0}", apply.apply_id);
						SqlParameter[] param = {
																new SqlParameter("@1", '1'),
																new SqlParameter("@2", DateTime.Now.ToString()),
																new SqlParameter("@3", "BAT")
															};
						DatabaseAccess.UpdateSetDatabase(sql, param, Program.gConnectStr);
					}
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("qUpdApply() Error({0})", ex.Message));
			}
			return true;
		}
	}
}
