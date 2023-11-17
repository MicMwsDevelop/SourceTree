using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CommonLib.DB.SqlServer;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory;
using CommonLib.DB;
using System.Data.SqlClient;
using CommonLib.Common;
using CommonLib.BaseFactory.Coupler.Table;

namespace MorningBatch
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// 接続文字列
		/// </summary>
		private string ConnectStr = "Server=SQLSV;Database=charlieDB;User ID=web;Password=02035612;Min Pool Size=1";

		public MainForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			;
		}

		/// <summary>
		/// 顧客情報入出力メイン処理
		/// </summary>
		/// <param name="pmode">1:COUPLER管理.顧客利用情報作成からの実行 2:顧客情報入出力からの実行</param>
		/// <param name="argout1a_1flg">画面.顧客データチェック状態(0:未選択 1:選択中）</param>
		/// <param name="argout1a_2flg">画面.利用データチェック状態(0:未選択 1:選択中）</param>
		/// <param name="argout1a_1">顧客データ最終出力日時</param>
		/// <param name="argout1a_2">利用データ最終出力日時</param>
		/// <returns>結果</returns>
		public bool CusDataUpdate_Main(string pmode, string argout1a_1flg, string argout1a_2flg, string argout1a_1, string argout1a_2)
		{
			bool bCusDataUpdate_Main = false;			// メイン戻り値
			bool bCusDataUpdate_User_All = false;		// 全件顧客データ処理実行戻り値
			bool bCusDataUpdate_User_Diff = false;		// 差分顧客データ実行戻り値
			bool bCusDataUpdate_Service_All = false;	// 全件利用機能データ処理実行戻り値
			bool bCusDataUpdate_Service_Diff = false;	// 差分利用機能データ実行戻り値
			string out1aSwitch = string.Empty;			// 顧客データ処理判定（1:全件 2:差分）
			string out1bSwitch = string.Empty;          // 利用機能データ処理判定（1:全件 2:差分）
			bool bCusDataUpdate_User = false;
			bool bCusDataUpdate_Service = false;

			string result = string.Empty;
			try
			{
				string sLogFileNameManager = string.Format("imp_bat_{0}", DateTime.Now.ToString());
				if (File.Exists("sLockFile"))
				{
					Console.WriteLine("*** インポートバッチ開始失敗（他のバッチ実行中） ***");
					result = "false";
				}
				else
				{
					// ロックファイル作成
					File.Open("sLockFile", FileMode.Append);
				}
				Console.WriteLine("***インポートバッチ開始 ***");

				// Coupler Ver2.7.0 ワークテーブルを操作して、テーブルをSWするため基本は、システム閉塞はしない
				// 顧客データまたは、利用データを全件出力する場合は、 システム閉塞していないとエラー
				if (argout1a_1flg == "1" && argout1a_1flg == "" && argout1a_2flg == "1" && argout1a_2 == "")
				{
					Console.WriteLine("*** システム閉塞中 ***");
					result = "false";
				}
				if (0 == result.Length)
				{
					// CusDataUpdate_Main処理
					string strCustomerLastDate = argout1a_1;	// 顧客データ最終出力日時
					string strCustomerUseLastDate = argout1a_2; // 利用機能データ最終出力日時
					string sReferMsg = string.Empty;
					if ("1" == pmode)
					{
						// 顧客利用情報作成からの実行
						strCustomerLastDate = SEL_DCHFNC_U005_Customer_LastFileCreateDate("1");
						strCustomerUseLastDate = SEL_DCHFNC_U005_Customer_LastFileCreateDate("2");
						sReferMsg = "COUPLER管理（顧客利用情報作成）";
					}
					else
					{
						// 顧客情報入出力からの実行
						sReferMsg = "COUPLER管理（顧客情報入出力）";
					}
					// 基本機能パックの商品ID・サービス種別ＩＤ・サービスＩＤを取得しセッション変数に保存する。（※顧客データ・利用データ取得時の条件に使用）
					Console.WriteLine(string.Format("*** {0}から実行されました。 ***", sReferMsg));
					Console.WriteLine("CHARLIEDBから基本機能パック取得開始 【PCA商品区分=200】");
					DataTable kihonPack = GetKihonPack(200);
					string goodsID = string.Empty;
					int service_type_id = 0;
					int service_id = 0;
					if (null != kihonPack && 0 < kihonPack.Rows.Count)
					{
						DataRow row = kihonPack.Rows[0];
						goodsID = row["GOODS_ID"].ToString().Trim();
						service_type_id = DataBaseValue.ConvObjectToInt(row["SERVICE_TYPE_ID"]);
						service_id = DataBaseValue.ConvObjectToInt(row["SERVICE_ID"]);
					}
					Console.WriteLine(string.Format("CHARLIEDBから基本機能パック取得終了 【商品ID={0}/サービス種別ID={1}/サービスID={2}】", goodsID, service_type_id, service_id));

					// 画面.顧客データのチェック状態がONの場合、または顧客利用情報作成処理から差分実行された場合
					List<stU005_out1a> out1aList = null;
					if ("1" == argout1a_1flg || "1" == pmode)
					{
						// ■■■■■■■■■■■■■■ CHARLIEDBから顧客データ取得 ■■■■■■■■■■■■■■
						Console.WriteLine("*** CHARLIEDB.顧客データ抽出SQL開始 ***");
 						DataTable qryU005_out1a = Sel_DCHFNC_U005_Customer_Datainout("out1a", strCustomerLastDate, strCustomerUseLastDate);
						Console.WriteLine("*** CHARLIEDB.顧客データ抽出SQL終了 ***");

						if (null == qryU005_out1a || 0 == qryU005_out1a.Rows.Count)
						{
							Console.WriteLine(string.Format("*** CHARLIEDB.顧客データ件数：0件 【最終更新日={0}】***", strCustomerUseLastDate));
						}
						else
						{
							if ("" != strCustomerLastDate)
							{
								// 顧客データ処理判定（1:全件 2:差分）
								out1aSwitch = "2";
							}
							else
							{
								// 顧客データ処理判定（1:全件 2:差分）
								out1aSwitch = "1";
							}
							Console.WriteLine(string.Format("*** CHARLIEDB.顧客データ件数：{0} 件 【{1}（1:全件 2:差分）/最終更新日={2}】***", qryU005_out1a.Rows.Count, out1aSwitch, strCustomerLastDate));
							out1aList = stU005_out1a.DataTableToList(qryU005_out1a, out1aSwitch);
						}
						// CHARLIEDB.ファイル出力履歴（顧客データ）の最終出力日を登録（※該当データが0件の場合でも登録）
						Console.WriteLine("CHARLIEDB.ファイル出力履歴(顧客データ）の最終出力日登録開始");
						bool bout1aUp = INS_DCHFNC_U005_Customer_FileCreate("1");
						Console.WriteLine(string.Format("CHARLIEDB.ファイル出力履歴(顧客データ）の最終出力日登録終了【{0}】", bout1aUp.ToString()));
					}
					// 画面.利用機能データのチェック状態がONの場合、または顧客利用情報作成処理から差分実行された場合
					List<stU005_out1b> out1bList = null;
					if ("1" == argout1a_2flg || "1" == pmode)
					{
						Console.WriteLine("*** CHARLIEDB.利用機能データ抽出SQL開始 ***");
						DataTable qryU005_out1b = Sel_DCHFNC_U005_Customer_Datainout("out1b", strCustomerLastDate, strCustomerUseLastDate);
						Console.WriteLine("*** CHARLIEDB.利用機能データ抽出SQL終了 ***");

						if (null == qryU005_out1b || 0 == qryU005_out1b.Rows.Count)
						{
							Console.WriteLine(string.Format("*** CHARLIEDB.利用機能データ件数：0件 【最終更新日={0}】***", strCustomerUseLastDate));
						}
						else
						{
							if ("" != strCustomerUseLastDate)
							{
								// 利用機能データ差分
								out1bSwitch = "2";	// 利用機能データ処理判定（1:全件 2:差分）
							}
							else if ("" == strCustomerUseLastDate)
							{
								// 利用機能データ全件
								out1bSwitch = "1";	// 利用機能データ処理判定（1:全件 2:差分）
							}
							int iServiceNoOut;
							out1bList = stU005_out1b.DataTableToList(qryU005_out1b, out1bSwitch, out iServiceNoOut);
							Console.WriteLine(string.Format("*** CHARLIEDB.利用機能データ件数：{0} 件 / 警告件数：{1} 件【{2}（1:全件 2:差分）/最終更新日={3}】***", out1bList.Count, iServiceNoOut, out1bSwitch, strCustomerUseLastDate));
						}
						// CHARLIEDB.ファイル出力履歴（利用機能データ）の最終出力日を登録（※該当データが0件の場合でも登録）
						Console.WriteLine("CHARLIEDB.ファイル出力履歴(利用機能データ）の最終出力日登録開始");
						bool bout1bUp = INS_DCHFNC_U005_Customer_FileCreate("2");
						Console.WriteLine(string.Format("CHARLIEDB.ファイル出力履歴(利用機能データ）の最終出力日登録終了【{0}】", bout1bUp.ToString()));
					}
					// ■■■■■■■■■■■■■■顧客データ出力処理■■■■■■■■■■■■■■
					if (0 < out1aList.Count && "" != out1aSwitch)
					{
						if ("1" == out1aSwitch)
						{
							// ■■■■■■■■全件処理■■■■■■■■
							Console.WriteLine("顧客データ（全件）登録開始");
							string sbatUserImportAll = batUserImportAll(out1aList, out1aSwitch, argout1a_1flg, argout1a_2flg);
							if ("0" == sbatUserImportAll)
							{
								// ①COUPLERDB.製品顧客管理情報ワークの名前を変更する
								string sProductUserWorkReNmTable = string.Format("PRODUCTUSER_WORK_{0}", DateTime.Now.ToString("yyyyMMdd"));
								Console.WriteLine(string.Format("COUPLERDB.製品顧客管理情報ワーク(PRODUCTUSER_WORK)→COUPLERDB.製品顧客管理情報ワーク({0})テーブル名変更開始", sProductUserWorkReNmTable));
								// EXEC sp_rename 'PRODUCTUSER_WORK', sProductUserWorkReNmTable

								Console.WriteLine(string.Format("COUPLERDB.製品顧客管理情報ワーク(PRODUCTUSER_WORK)→COUPLERDB.製品顧客管理情報ワーク({0})テーブル名変更終了", sProductUserWorkReNmTable));

								// ②COUPLERDB.製品顧客管理情報の名前を製品顧客管理情報ワークの名前に変更する
								Console.WriteLine("COUPLERDB.製品顧客管理情報(PRODUCTUSER)→COUPLERDB.製品顧客管理情報ワーク(PRODUCTUSER_WORK)テーブル名変更開始");
								// EXEC sp_rename 'PRODUCTUSER','PRODUCTUSER_WORK'

								// ①で変更したテーブル名をCOUPLERDB.製品顧客管理情報の名前に変更する
								Console.WriteLine(string.Format("COUPLERDB.製品顧客管理情報ワーク({0})→COUPLERDB.製品顧客管理情報(PRODUCTUSER)テーブル名変更開始", sProductUserWorkReNmTable));
								// EXEC sp_rename '#sProductUserWorkReNmTable#','PRODUCTUSER'

								Console.WriteLine(string.Format("COUPLERDB.製品顧客管理情報ワーク({0})→COUPLERDB.製品顧客管理情報(PRODUCTUSER)テーブル名変更終了", sProductUserWorkReNmTable));

								bCusDataUpdate_User_All = true;
							}
						}
						else if ("2" == out1aSwitch)
						{
							// --- ■■■■■■■■差分処理■■■■■■■■ ---
							Console.WriteLine("顧客データ（差分）登録開始");
							string sCusDataUpdate_User_Diff = CusDataUpdate_User_Diff(out1aList, out1aSwitch, argout1a_1flg, argout1a_2flg);
							Console.WriteLine(string.Format("顧客データ（差分）登録終了({0})", sCusDataUpdate_User_Diff));
							if ("0" == sCusDataUpdate_User_Diff)
							{
								bCusDataUpdate_User_Diff = true;
							}
						}
					}
					// ■■■■■■■■■■■■■■利用機能データ出力処理■■■■■■■■■■■■■■
					if (0 < out1bList.Count && "" != out1bSwitch)
					{
						if ("1" == out1bSwitch)
						{
							// ■■■■■■■■全件処理■■■■■■■■
							Console.WriteLine("サービスデータ（全件）登録開始");
							string sbatServiceImportAll = batServiceImportAll(out1bList, out1bSwitch, DateTime.Now);
							Console.WriteLine(string.Format("サービスデータ（全件）登録終了({0})", sbatServiceImportAll));
							if ("0" == sbatServiceImportAll)
							{
								bCusDataUpdate_Service_All = true;
							}

						}
						else if ("2" == out1bSwitch)
						{
							// ■■■■■■■■差分処理■■■■■■■■
							Console.WriteLine("サービスデータ（差分）登録開始");
							string sCusDataUpdate_Sevice_Diff = CusDataUpdate_Service_Diff(out1bList, out1bSwitch, argout1a_1flg, argout1a_2flg);
							Console.WriteLine(string.Format("サービスデータ（差分）登録終了({0})", sCusDataUpdate_Sevice_Diff));
							if ("0" == sCusDataUpdate_Sevice_Diff)
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
					// 画面.顧客データ・利用データがチェックされている
					if ("1" == argout1a_1flg && "1" == argout1a_2flg)
					{
						// 顧客データ
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
						// 利用機能データ
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
					// 顧客情報・サービス情報ともエラーがなければ正常
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
				Console.WriteLine(string.Format("*** インポートバッチ終了 ({0}) ***", result));
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
				Console.WriteLine(string.Format("*** エラー内容 ({0})***", ex.Message));
				Console.WriteLine(string.Format("*** インポートバッチ終了(catch) ({0}) ***", result));
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
		/// 連携ファイルの最終出力日取得
		/// </summary>
		/// <param name="argFlg">ファイル種類NO（顧客=1,利用=2）</param>
		/// <returns>処理結果（最終出力日）</returns>
		private string SEL_DCHFNC_U005_Customer_LastFileCreateDate(string argFlg)
		{
			try
			{
				string sql = string.Format("SELECT MAX(FILE_CREATEDATE) LASTDATE FROM T_FILE_CREATEDATE WHERE FILE_TYPE = '{0}'", argFlg);
				DataTable table = DatabaseAccess.SelectDatabase(sql, ConnectStr);
				if (null != table && 1 == table.Rows.Count)
				{
					return table.Rows[0]["LASTDATE"].ToString().Trim();
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("SEL_DCHFNC_U005_Customer_LastFileCreateDate() Error({0})", ex.Message));
			}
			return string.Empty;
		}

		/// <summary>
		/// 顧客情報入出力（顧客データ/利用機能データ）
		/// </summary>
		/// <param name="argFlg">処理判断フラグ</param>
		/// <param name="argout1a_1">顧客データ最終出力日時</param>
		/// <param name="argout1a_2">利用データ最終出力日時</param>
		/// <returns>処理結果</returns>
		private DataTable Sel_DCHFNC_U005_Customer_Datainout(string argFlg, string argout1a_1, string argout1a_2)
		{
			try
			{
				string sql = string.Empty;
				string sWhere01 = string.Empty;
				string sWhere02 = string.Empty;
				string sWhere03 = string.Empty;
				string sWhere04 = string.Empty;
				if (argFlg == "out1a")
				{
					// Coupler連携（顧客データ）
					string arg1 = argout1a_1.Trim();
					if ("" != arg1)
					{
						//差分出力の場合は削除レコードも出力対象 & 出力時間以降
						sWhere01 = " AND (T_CUSTOMER_FOUNDATIONS.UPDATE_DATE > '{0}' OR T_PRODUCT_CONTROL.UPDATE_DATE > '{0}' OR V_CUSTOMER.UPDATE_TIME > '{0}')";
						sWhere02 = " AND (T_CUSTOMER_FOUNDATIONS.UPDATE_DATE > '{0}' OR T_PRODUCT_CONTROL.UPDATE_DATE > '{0}' OR V_CUSTOMER.UPDATE_TIME > '{0}')";
						sWhere03 = " AND (T_DEMO_USER.UPDATE_DATE > '{0}' OR T_PRODUCT_CONTROL.UPDATE_DATE > '{0}')";
					}
					// 2014/05/02 差分出力対応(顧客基本と製品情報の更新日をみて抽出)
					sql = string.Format(@"SELECT"
							+ "  T_PRODUCT_CONTROL.PRODUCT_ID"
							+ ", T_PRODUCT_CONTROL.PASSWORD"
							+ ", T_PRODUCT_CONTROL.USER_CLASSIFICATION"
							+ ", T_PRODUCT_CONTROL.TRIAL_FLG"
							+ ", T_PRODUCT_CONTROL.CUSTOMER_ID"
							+ ", (V_CUSTOMER.CUSTOMER_NAME1 + V_CUSTOMER.CUSTOMER_NAME2) as CUSTOMER_NAME"
							+ ", V_BRANCH_INFORMATION.BRANCH_MAIL"
							+ ", (SELECT TOP 1 MAIL_ADDRESS FROM M_MAIL) AS MAIL1"
							+ ", LTRIM(RTRIM(V_CUSTOMER.USE_SYSTEM_CODE)) AS USE_SYSTEM_CODE"
							+ ", V_CUSTOMER.CLIENT_LICENSES"
							+ ", CASE T_PRODUCT_CONTROL.END_FLG"
							+ "    WHEN '2' THEN '1'"
							+ "    ELSE T_PRODUCT_CONTROL.END_FLG"
							+ "  END END_FLG"
							+ ", CONVERT(VARCHAR, T_PRODUCT_CONTROL.TRIAL_START_DATE, 111) AS TRIAL_START_DATE"
							+ ", CONVERT(VARCHAR, T_PRODUCT_CONTROL.PERIOD_END_DATE, 111) AS TRIAL_END_DATE"
							+ ", T_CUSTOMER_FOUNDATIONS.DELETE_FLG"
							+ " FROM T_PRODUCT_CONTROL"
							+ " LEFT OUTER JOIN V_CUSTOMER ON T_PRODUCT_CONTROL.CUSTOMER_ID = V_CUSTOMER.CUSTOMER_ID"
							+ " LEFT OUTER JOIN V_BRANCH_INFORMATION ON RTRIM(V_CUSTOMER.BRANCH_ID) = RTRIM(V_BRANCH_INFORMATION.BRANCH_ID)"
							+ " LEFT OUTER JOIN T_CUSTOMER_FOUNDATIONS ON T_PRODUCT_CONTROL.CUSTOMER_ID = T_CUSTOMER_FOUNDATIONS.CUSTOMER_ID"
							+ " WHERE T_PRODUCT_CONTROL.TRIAL_FLG = 0"  // 通常
							+ " AND V_CUSTOMER.CUSTOMER_ID IS NOT NULL"
							+ " AND T_PRODUCT_CONTROL.TRIAL_START_DATE IS NOT NULL"
							+ "	AND T_PRODUCT_CONTROL.USER_CLASSIFICATION <= 1" //デモユーザ以外
							+ sWhere01
							+ " UNION ALL"
							+ " SELECT"
							+ "  T_PRODUCT_CONTROL.PRODUCT_ID"
							+ ", T_PRODUCT_CONTROL.PASSWORD"
							+ ", T_PRODUCT_CONTROL.USER_CLASSIFICATION"
							+ ", T_PRODUCT_CONTROL.TRIAL_FLG"
							+ ", T_PRODUCT_CONTROL.CUSTOMER_ID"
							+ ", (V_CUSTOMER.CUSTOMER_NAME1 + V_CUSTOMER.CUSTOMER_NAME2) as CUSTOMER_NAME"
							+ ", V_BRANCH_INFORMATION.BRANCH_MAIL"
							+ ", (SELECT TOP 1 MAIL_ADDRESS FROM M_MAIL) AS MAIL1"
							+ ", LTRIM(RTRIM(V_CUSTOMER.USE_SYSTEM_CODE)) AS USE_SYSTEM_CODE"
							+ ", V_CUSTOMER.CLIENT_LICENSES"
							+ ", CASE T_PRODUCT_CONTROL.END_FLG"
							+ "    WHEN '2' THEN '1'"
							+ "    ELSE T_PRODUCT_CONTROL.END_FLG"
							+ "  END END_FLG"
							+ ", CONVERT(VARCHAR, T_PRODUCT_CONTROL.TRIAL_START_DATE, 111) AS TRIAL_START_DATE"
							+ ", CONVERT(VARCHAR, T_PRODUCT_CONTROL.PERIOD_END_DATE, 111) AS TRIAL_END_DATE"
							+ ", T_CUSTOMER_FOUNDATIONS.DELETE_FLG"
							+ " FROM T_PRODUCT_CONTROL"
							+ " LEFT OUTER JOIN V_CUSTOMER ON T_PRODUCT_CONTROL.CUSTOMER_ID = V_CUSTOMER.CUSTOMER_ID"
							+ " LEFT OUTER JOIN V_BRANCH_INFORMATION ON RTRIM(V_CUSTOMER.BRANCH_ID) = RTRIM(V_BRANCH_INFORMATION.BRANCH_ID)"
							+ " LEFT OUTER JOIN T_CUSTOMER_FOUNDATIONS ON T_PRODUCT_CONTROL.CUSTOMER_ID = T_CUSTOMER_FOUNDATIONS.CUSTOMER_ID"
							+ " WHERE T_PRODUCT_CONTROL.TRIAL_FLG = 1"              // 体験版
							+ " AND T_PRODUCT_CONTROL.TRIAL_START_DATE IS NOT NULL"
							+ " AND T_PRODUCT_CONTROL.USER_CLASSIFICATION <= 1"     // デモユーザ以外
							+ sWhere02
							+ " UNION ALL"
							// デモユーザ対応
							+ " SELECT"
							+ "  T_PRODUCT_CONTROL.PRODUCT_ID"
							+ ", T_PRODUCT_CONTROL.PASSWORD"
							+ ", T_PRODUCT_CONTROL.USER_CLASSIFICATION"
							+ ", T_PRODUCT_CONTROL.TRIAL_FLG"
							+ ", T_PRODUCT_CONTROL.CUSTOMER_ID"
							+ ", T_DEMO_USER.NAME as CUSTOMER_NAME"
							+ ", T_DEMO_USER.MAILADDR1 as BRANCH_MAIL"
							+ ", T_DEMO_USER.MAILADDR2 AS MAIL1"
							+ ", '100' AS USE_SYSTEM_CODE"
							+ ", '0' as CLIENT_LICENSES"
							+ ", CASE T_PRODUCT_CONTROL.END_FLG"
							+ "    WHEN '2' THEN '1'"
							+ "    ELSE T_PRODUCT_CONTROL.END_FLG"
							+ "  END END_FLG"
							+ ", CONVERT(VARCHAR, T_PRODUCT_CONTROL.TRIAL_START_DATE, 111) AS TRIAL_START_DATE"
							+ ", CONVERT(VARCHAR, T_PRODUCT_CONTROL.PERIOD_END_DATE, 111) AS TRIAL_END_DATE"
							+ ", T_DEMO_USER.DELETE_FLG"
							+ " FROM T_PRODUCT_CONTROL"
							+ " LEFT OUTER JOIN T_DEMO_USER ON T_PRODUCT_CONTROL.CUSTOMER_ID = T_DEMO_USER.CUSTOMER_ID"
							+ " LEFT OUTER JOIN T_CUSTOMER_FOUNDATIONS ON T_PRODUCT_CONTROL.CUSTOMER_ID = T_CUSTOMER_FOUNDATIONS.CUSTOMER_ID"
							+ " WHERE T_PRODUCT_CONTROL.USER_CLASSIFICATION >= 2"   // デモユーザ
							+ " AND T_DEMO_USER.CUSTOMER_ID IS NOT NULL"
							+ " AND T_DEMO_USER.END_FLG = N'0' AND T_DEMO_USER.DELETE_FLG = N'0'"
							+ "	AND T_PRODUCT_CONTROL.TRIAL_START_DATE IS NOT NULL"
							+ sWhere03
							+ " ORDER BY T_PRODUCT_CONTROL.PRODUCT_ID"
							, arg1);
				}
				else if (argFlg == "out1b")
				{
					// Coupler連携（利用データ）
					string arg2 = argout1a_2.Trim();
					if ("" == arg2)
					{
						// 全件出力の場合は削除されていないレコードを出力
						sWhere01 = " T_CUSSTOMER_USE_INFOMATION.DELETE_FLG = 0 AND";
						sWhere02 = " AND T_DEMO_USER.DELETE_FLG = N'0'";
					}
					else
					{
						// 差分出力の場合は削除レコードも出力対象 & 出力時間以降
						// 2015/03/31 h-chiba 利用開始日終了日が入って無ければ差分に関係なく出力して警告を出すように変更
						sWhere03 = " AND ((CASE WHEN T_CUSSTOMER_USE_INFOMATION.UPDATE_DATE is null THEN T_CUSSTOMER_USE_INFOMATION.CREATE_DATE"
									+ " WHEN T_CUSSTOMER_USE_INFOMATION.UPDATE_DATE is not null THEN T_CUSSTOMER_USE_INFOMATION.UPDATE_DATE"
									+ " END) > '{0}' OR (PRODUCT_ID is not null AND USE_START_DATE is null))";
						sWhere04 = " AND (T_DEMO_USER.UPDATE_DATE > '{0}' OR T_PRODUCT_CONTROL.UPDATE_DATE > '{0}')";
					}
					// 2014/05/02 差分出力対応(利用情報の更新日をみて抽出（※削除フラグを更新した場合が問題なので検討すること）)
					sql = string.Format("SELECT * FROM"
							+ " (SELECT"
							+ "  T_CUSSTOMER_USE_INFOMATION.CUSTOMER_ID"
							+ ", T_PRODUCT_CONTROL.PRODUCT_ID"
							+ ", T_CUSSTOMER_USE_INFOMATION.SERVICE_ID"
							+ ", T_CUSSTOMER_USE_INFOMATION.PAUSE_END_STATUS"
							+ ", '0' as SET_SALE"
							+ ", CONVERT(VARCHAR, T_CUSSTOMER_USE_INFOMATION.USE_START_DATE, 111) AS USE_START_DATE"
							+ ", CONVERT(VARCHAR, T_CUSSTOMER_USE_INFOMATION.PERIOD_END_DATE, 111) AS USE_END_DATE"
							+ ", T_CUSSTOMER_USE_INFOMATION.DELETE_FLG"
							+ " FROM T_CUSSTOMER_USE_INFOMATION"
							+ " LEFT OUTER JOIN T_PRODUCT_CONTROL ON T_CUSSTOMER_USE_INFOMATION.CUSTOMER_ID = T_PRODUCT_CONTROL.CUSTOMER_ID AND T_PRODUCT_CONTROL.USER_CLASSIFICATION IN (0, 1)"
							+ " LEFT OUTER JOIN M_SERVICE ON T_CUSSTOMER_USE_INFOMATION.SERVICE_ID = M_SERVICE.SERVICE_ID"
							+ " WHERE"
							+ sWhere01 
							+ " T_CUSSTOMER_USE_INFOMATION.SERVICE_TYPE_ID <> 1 AND"
							+ " T_CUSSTOMER_USE_INFOMATION.SERVICE_ID <> 1001 AND M_SERVICE.UMU_FLG = 0"
							+ " AND T_PRODUCT_CONTROL.USER_CLASSIFICATION IN(0, 1) AND T_PRODUCT_CONTROL.PRODUCT_ID is not null"
							+ sWhere03
							+ " UNION ALL"
							+ " SELECT"
							+ "  T_DEMO_USER.CUSTOMER_ID"
							+ ", T_PRODUCT_CONTROL.PRODUCT_ID"
							+ ", M_DEMO_SERVICE.SERVICE_ID"
							+ ", 0 AS PAUSE_END_STATUS"
							+ ", 0 AS SET_SALE"
							+ ", CONVERT(VARCHAR, T_PRODUCT_CONTROL.TRIAL_START_DATE, 111) AS USE_START_DATE"
							+ ", CONVERT(VARCHAR, T_PRODUCT_CONTROL.PERIOD_END_DATE, 111) AS USE_END_DATE"
							+ ", T_DEMO_USER.DELETE_FLG"
							+ " FROM T_DEMO_USER"
							+ " INNER JOIN T_PRODUCT_CONTROL ON T_DEMO_USER.CUSTOMER_ID = T_PRODUCT_CONTROL.CUSTOMER_ID CROSS JOIN M_DEMO_SERVICE"
							+ " WHERE T_DEMO_USER.END_FLG = N'0'" 
							+ sWhere02 
							+ " AND M_DEMO_SERVICE.DEMO_USE_FLG = N'1'"
							+ " AND M_DEMO_SERVICE.SERVICE_ID <> 1001"
							+ sWhere04
							+ ") tbl"
							+ " ORDER BY PRODUCT_ID, SERVICE_ID", arg2);	
				}
				return DatabaseAccess.SelectDatabase(sql, ConnectStr);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("Sel_DCHFNC_U005_Customer_Datainout() Error({0})", ex.Message));
			}
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
				string sql = "INSERT INTO T_FILE_CREATEDATE VALUES (@1, @2, @3, @4)";
				SqlParameter[] param = {
										new SqlParameter("@1", DateTime.Today.ToString()),
										new SqlParameter("@2", argFlg),
										new SqlParameter("@3", DateTime.Today.ToString()),
										new SqlParameter("@4", "CFADMIN")
									};
				if (0 < DatabaseAccess.InsertIntoDatabase(sql, param, ConnectStr))
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
		/// 基本機能パックの商品ID・サービス種別ＩＤ・サービスＩＤを取得
		/// </summary>
		/// <param name="argBRAND_CLASSIFICATION">商品ID=200</param>
		/// <returns>処理結果</returns>
		private DataTable GetKihonPack(int argBRAND_CLASSIFICATION)
		{
			try
			{
				string sql = string.Format("SELECT A.GOODS_ID, A.SERVICE_TYPE_ID, A.SERVICE_ID"
										+ " FROM M_CODE A INNER JOIN V_PCA_GOODS B ON A.GOODS_ID = B.GOODS_ID AND B.BRAND_CLASSIFICATION = {0}"
										+ " WHERE A.DELETE_FLG = '0' AND SET_SALE = '1'", argBRAND_CLASSIFICATION);
				return DatabaseAccess.SelectDatabase(sql, ConnectStr);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("GetKihonPack() Error({0})", ex.Message));
			}
		}

		/// <summary>
		/// 顧客情報入出力（全件）顧客データ処理
		/// </summary>
		/// <param name="out1aList">顧客データリスト</param>
		/// <param name="out1aSwitch">顧客データ処理判定（1:全件 2:差分）</param>
		/// <param name="out1a_1flg">顧客データチェック状態(0:未選択 1:選択）</param>
		/// <param name="out1a_2flg">利用機能データチェック状態(0:未選択 1:選択）</param>
		/// <returns>判定</returns>
		private string batUserImportAll(List<stU005_out1a> out1aList, string out1aSwitch, string out1a_1flg, string out1a_2flg)
		{
			string iEndStatus = "";
			int iStatusUser = 0;    // 実行結果ステータス 0 = 正常,1 = 異常,2 = ファイル無し
			bool errorCheck = false;

			Console.WriteLine("ユーザー情報インポート開始 (全件)");
			Console.WriteLine("トランザクション開始");
			try
			{
				// 顧客データの構造体のデータ件数があり、処理モードが全件の場合はStart
				if (0 < out1aList.Count && "1" == out1aSwitch)
				{
					Console.WriteLine("ユーザ情報インポートDB処理開始（全件）");
					string sql = string.Empty;
					try
					{
						// COUPLERDB.製品顧客管理情報ワークを物理削除する。
						sql = "DELETE FROM PRODUCTUSER_WORK";
						DatabaseAccess.DeleteDatabase(sql, ConnectStr);
						Console.WriteLine(sql);

						// COUPLERDB.製品顧客管理情報を製品管理情報にコピーする。
						sql = "INSERT INTO PRODUCTUSER_WORK SELECT * FROM PRODUCTUSER";
						DatabaseAccess.ExecuteNonQuery(sql, ConnectStr);
						Console.WriteLine(sql);

						// COUPLERDB.製品顧客管理情報ワークのCP_IDがCPL、OPE、TRY、TST、XYZ以外を削除
						sql = "DELETE FROM PRODUCTUSER_WORK WHERE (testuser_flg != '1' OR testuser_flg is null) AND LEFT(cp_id, 3) NOT IN ('XYZ','TRY')";
						DatabaseAccess.DeleteDatabase(sql, ConnectStr);
						Console.WriteLine(sql);
					}
					catch (Exception ex)
					{
						Console.Write(string.Format("エラー：{0}/{1}", ex.Message, sql));
						iEndStatus = "1";	// 完了フラグ
					}
					Console.WriteLine(iEndStatus);

					int user_out_cnt = 0;
					if ("" == iEndStatus)
					{
						Console.WriteLine("ユーザ情報インポートデータ読込開始");

						// 製品顧客情報ワークに登録する(PRODUCTUSER_WORK)
						foreach (stU005_out1a out1a in out1aList)
						{
							List<stU005_out1a> work = out1aList.FindAll(p => p.PRODUCT_ID == out1a.PRODUCT_ID);
							if (null != work && 1 < work.Count)
							{
								Console.WriteLine(string.Format("製品ID 重複チェック({0})", out1a.PRODUCT_ID));
								errorCheck = true;
								break;
							}
							// エラーチェック
							string sErr;
							errorCheck = out1a.ErrorCheck(out sErr);
							if (errorCheck)
							{
								Console.WriteLine(string.Format("{0}({1})", sErr, out1a.PRODUCT_ID));
								break;
							}
							if (qInsProductUserWork(out1a))
							{
								user_out_cnt++;
							}
							else
							{
								Console.WriteLine(string.Format("顧客情報ワークの登録に失敗しましたが処理をスキップします。({0})", out1a.PRODUCT_ID));
							}
							// 顧客データに無い項目を製品顧客情報から取得して、製品顧客情報ワークに更新
							if (false == qUpdProductUserWork(out1a))
							{
								Console.WriteLine(string.Format("顧客情報ワークの更新に失敗しましたが処理をスキップします。({0})", out1a.PRODUCT_ID));
							}
						}
					}
					// ユーザ情報インポート成功
					if ("" == iEndStatus && false == errorCheck)
					{
						iEndStatus = "1";	// 完了フラグ1完了
 						iStatusUser = 0;    // 実行結果ステータス 0 = 正常
						Console.WriteLine("ユーザ情報インポートDB処理終了");

						// 顧客データのみ選択された場合は顧客データをコミット
						Console.WriteLine(string.Format("ユーザ情報インポート件数：{0} 件", user_out_cnt));
						if (iStatusUser == 0 && out1a_1flg == "1" && out1a_2flg == "0")
						{
							Console.WriteLine("ユーザ情報をコミットしました。");
						}
					}
					else
					{
						// エラーがあったため異常終了
						iStatusUser = 1;	// 実行結果ステータス 1 = 異常終了
					}
				}
				else
				{
					// インポートファイルが存在しない場合のログレベルを下げる
					Console.WriteLine("ユーザ情報インポートデータがありません");
					iStatusUser = 2;	// 実行結果ステータス 2 = ファイルなし
				}
			}
			catch (TimeoutException ex)
			{
				// タイムアウト
				// *****インポート例外発生 * ****
				Console.WriteLine("インポートタイムアウト発生 ***** 管理画面から解除して下さい *****");
				Console.WriteLine("トランザクションロールバック");
			}
			catch (Exception ex)
			{
				// その他例外エラー
				Console.WriteLine("トランザクションロールバック");
				Console.WriteLine(string.Format("インポート例外エラー発生({0})", ex.Message));
				iEndStatus = "1";	// 完了フラグ1完了
				iStatusUser = 1;	// 実行結果ステータス 1 = 異常終了
			}
			Console.WriteLine("トランザクション終了");
			// ***********トランザクション終了***********

			// インポート完了日時
			// Ver2.19.0
			// ***********ログテーブル記録 * **********
			// ユーザ情報インポート記録終了

			// 終了ログ
			Console.WriteLine("ユーザー情報インポート終了");
			int iUserImportStatus = iStatusUser;	// インポート処理ステータス
			return iUserImportStatus.ToString();
		}

		/// <summary>
		/// 顧客情報入出力（差分）顧客データ出力処理※userdataimp.cfmを流用
		/// </summary>
		/// <param name="out1aList">顧客データリスト</param>
		/// <param name="out1aSwitch">顧客データ処理判定（1:全件 2:差分）</param>
		/// <param name="out1a_1flg">顧客データチェック状態(0:未選択 1:選択）</param>
		/// <param name="out1a_2flg">利用機能データチェック状態(0:未選択 1:選択）</param>
		/// <returns>判定</returns>
		private string CusDataUpdate_User_Diff(List<stU005_out1a> out1aList, string out1aSwitch, string out1a_1flg, string out1a_2flg)
		{
			// 実行結果ステータス 0:正常,1:異常,2:ファイル無し
			string iStatusUser = "1";
			int iPUserStatus = 0;

			// ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼ 顧客データ差分反映開始 ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
			if (0 < out1aList.Count && "2" == out1aSwitch)
			{
				Console.WriteLine("差分インポートバッチ開始 (1)");
				Console.WriteLine("トランザクション開始");
				if (0 < out1aList.Count)
				{
					Console.WriteLine("ユーザ情報インポートDB処理開始");
					// 差分出力の場合（削除フラグあり）
					foreach (stU005_out1a out1a in out1aList)
					{
						// ----------------入力チェック--------------------
						string sErr;
						if (false == out1a.ErrorCheck(out sErr))
						{
							if (out1a.DELETE_FLG)
							{
								if (false == qImpDeleteUserData(out1a.PRODUCT_ID))
								{
									Console.WriteLine(string.Format("ユーザ情報の削除でエラーが発生しました。({0})", out1a.PRODUCT_ID));
									iPUserStatus = 1;   // ユーザー情報異常
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
							Console.WriteLine(string.Format("{0}({1})", sErr, out1a.PRODUCT_ID));
							iPUserStatus = 1;   // ユーザー情報異常
							break;
						}
					}
						Console.WriteLine("ユーザ情報インポートDB処理終了");
					if ("0" == iStatusUser && "1" == out1a_1flg && "0" == out1a_2flg)
					{
						Console.WriteLine("ユーザ情報をコミットしました。");
					}
				}
				else
				{
					// インポートデータが0件の場合のログレベルを下げる
					Console.WriteLine("ユーザ情報インポートデータがありません");
					iStatusUser = "2";      // ユーザ情報インポートデータなし
					Console.WriteLine("ユーザ情報をロールバックしました。");
				}
			}
			else
			{
				iStatusUser = "0";
			}
			// ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲ 顧客データ反映終了 ▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲
			Console.WriteLine("差分インポートバッチ終了");

			// 顧客データ差分反映が正常に終了した、またはインポートデータがない かつ、利用機能データ差分反映が正常に終了した、またはインポートデータがない
			return iStatusUser;
		}

		/// <summary>
		/// 顧客情報入出力（全件）利用機能データ処理
		/// </summary>
		/// <param name="out1bList">利用機能データリスト</param>
		/// <param name="out1bSwitch">利用機能データ処理判定（1:全件 2:差分）</param>
		/// <param name="startTime">タイムアウト測定用開始日時</param>
		/// <returns>判定</returns>
		private string batServiceImportAll(List<stU005_out1b> out1bList, string out1bSwitch, DateTime startTime)
		{
			string sErr = "";	// エラーメッセージ
			string iEndStatus = "";				// サービス情報インポート完了フラグ 1:完了
			string iStatusService = "1";		// 実行結果ステータス 0 = 正常,1 = 異常,2 = ファイル無し
			string iServiceImportStatus = "";	// インポート処理ステータス

			 // インポートモードチェック
			 Console.WriteLine("サービス情報インポートバッチ開始 (全件)");

			// ***********トランザクション開始 * **********
			Console.WriteLine("トランザクション開始");
			try
			{
				// サービス情報インポート
				if (0 < out1bList.Count)
				{
					// COUPLERDB.サービス情報ワークを物理削除する
					DatabaseAccess.DeleteDatabase("DELETE FROM SERVICE_WORK", ConnectStr);

					// ******読み込んだデータを構造体にセット ******
					List<CouplerService> stDbDataList = new List<CouplerService>();
					foreach (stU005_out1b out1b in out1bList)
					{
						CouplerService data = new CouplerService();
						data.cp_id = out1b.PRODUCT_ID;
						data.service_id = out1b.SERVICE_ID;
						data.contrac_type = out1b.PAUSE_END_STATUS;
						data.payment_type = out1b.SET_SALE;
						data.start_date = out1b.USE_START_DATE;
						data.end_date = out1b.USE_END_DATE;
						stDbDataList.Add(data);
					}
					// ******データベース登録 ******
					if ("" == iEndStatus)
					{
						Console.WriteLine("サービス情報インポートDB処理開始");
						// COUPLERDB.サービス情報ワークを物理削除する
						string sSQL = "DELETE FROM SERVICE_WORK";
						DatabaseAccess .DeleteDatabase(sSQL, ConnectStr);
						Console.WriteLine(sSQL);

						// COUPLERDB.サービス情報をサービス情報ワークにコピーする
						sSQL = "INSERT INTO SERVICE_WORK SELECT * FROM SERVICE";
						DatabaseAccess.ExecuteNonQuery(sSQL, ConnectStr);
						Console.WriteLine(sSQL);
		  
						sSQL = "DELETE FROM SERVICE_WORK WHERE LEFT(cp_id,3) NOT IN ('XYZ','TRY')";
						DatabaseAccess.ExecuteNonQuery(sSQL, ConnectStr);
						Console.WriteLine(sSQL);

						// サービス情報に登録
						foreach (CouplerService data in stDbDataList)
						{
							// 次のユーザーのサービス情報を全て削除(コミット後に削除)
							qDelServiceUserWork(data.cp_id);

							qInsServiceWork(data);
						}


					}


				}
			}
			catch (TimeoutException ex)
			{
				// タイムアウト
				// エラーログ出力
				Console.WriteLine("インポートタイムアウト発生 ***** バッチ実行結果画面からエラー回復して下さい *****");
			}
			catch (Exception ex)
			{
				// その他例外エラー
				// サービス情報ワークをサービス情報テーブルから復旧
				Console.WriteLine(string.Format("その他例外エラーが発生しました。{0}", ex.Message));
				iEndStatus = "1";		// 完了フラグ1完了
				iStatusService = "1";	// 実行結果ステータス 1 = 異常終了
			}
			Console.WriteLine("トランザクション終了");
			// ***********トランザクション終了 * **********

			// 終了ログ
			Console.WriteLine(string.Format("インポートバッチ終了(ステータス={0}", iStatusService));
			iServiceImportStatus = iStatusService;
			if ("0" == iStatusService)
			{
				// 正常終了した場合
				// ①COUPLERDB.サービス情報ワークの名前を変更する
				string sServiceWorkReNmTable = string.Format("SERVICE_WORK_{0}", DateTime.Now.ToString("yyyyMMdd"));
				Console.WriteLine(string.Format("COUPLERDB.サービス情報ワーク(SERVICE_WORK)→COUPLERDB.サービス情報ワーク({0})テーブル名変更開始", sServiceWorkReNmTable));
				SqlParameter[] param1 = { 
										new SqlParameter("@1", "SERVICE_WORK"),
										new SqlParameter("@2", sServiceWorkReNmTable)
										};
				DatabaseAccess.ExecuteStoredProcedure("sp_rename", ConnectStr, param1);
				Console.WriteLine(string.Format("COUPLERDB.サービス情報ワーク(SERVICE_WORK)→COUPLERDB.サービス情報ワーク({0})テーブル名変更終了", sServiceWorkReNmTable));

				// ②COUPLERDB.サービス情報の名前をサービス情報ワークの名前に変更する
				Console.WriteLine("COUPLERDB.サービス情報(SERVICE)→COUPLERDB.サービス情報ワーク(SERVICE_WORK)テーブル名変更開始");
				SqlParameter[] param2 = {
										new SqlParameter("@1", "SERVICE"),
										new SqlParameter("@2", "SERVICE_WORK")
										};
				DatabaseAccess.ExecuteStoredProcedure("sp_rename", ConnectStr, param2);
				Console.WriteLine("COUPLERDB.サービス情報(SERVICE)→COUPLERDB.サービス情報ワーク(SERVICE_WORK)テーブル名変更終了");

				// ①で変更したテーブル名をCOUPLERDB.サービス情報の名前に変更する
				Console.WriteLine(string.Format("COUPLERDB.サービス情報ワーク({0})→COUPLERDB.サービス情報(SERVICE)テーブル名変更開始", sServiceWorkReNmTable));
				SqlParameter[] param3 = {
										new SqlParameter("@1", sServiceWorkReNmTable),
										new SqlParameter("@2", "SERVICE")
										};
				DatabaseAccess.ExecuteStoredProcedure("sp_rename", ConnectStr, param3);
				Console.WriteLine(string.Format("COUPLERDB.サービス情報ワーク({0})→COUPLERDB.サービス情報(SERVICE)テーブル名変更終了", sServiceWorkReNmTable));
			}
			if ("0" == iStatusService)
			{
				// 開始ログ
				Console.WriteLine("申込情報更新開始");
				// ***********トランザクション開始 * **********
				Console.WriteLine("トランザクション開始");

				// 申込情報取得（システム反映されていないデータのみ)
				List<CouplerApply> stApply = GetApplyNoSystem();
				//Console.WriteLine(string.Format("データ件数：{0}", stDbData.Count));
				//Console.WriteLine(string.Format("データ件数：{0}", stIchiran.Count));
				Console.WriteLine(string.Format("システム未反映データ件数：{0}", stApply.Count()));

				// サービス情報（全件）処理


				// コミット
				Console.WriteLine("トランザクションコミット");
				Console.WriteLine("トランザクション終了");

				// 終了ログ
				Console.WriteLine("申込情報更新終了");
			}
			return iServiceImportStatus;
		}

		/// <summary>
		/// 顧客情報入出力（差分）利用機能データ出力処理※userdataimp.cfmを流用
		/// </summary>
		/// <param name="out1bList">利用機能データリスト</param>
		/// <param name="out1bSwitch">利用機能データ処理判定（1:全件 2:差分）</param>
		/// <param name="out1a_1flg">顧客データチェック状態(0:未選択 1:選択）</param>
		/// <param name="out1a_2flg">利用機能データチェック状態(0:未選択 1:選択）</param>
		/// <returns>判定</returns>
		private string CusDataUpdate_Service_Diff(List<stU005_out1b> out1bList, string out1bSwitch, string out1a_1flg, string out1a_2flg)
		{
			// システム反映済フラグを設定


			return "";
		}

		/// <summary>
		/// 申込情報取得（システム未反映のみ)
		/// </summary>
		/// <returns>申込情報</returns>
		private List<CouplerApply> GetApplyNoSystem()
		{
			try
			{
				string sql = "SELECT apply_id, cp_id, service_id, apply_type FROM APPLY WHERE CP_ID LIKE 'MWS%' AND SYSTEM_FLG = '0'";
				DataTable table =  DatabaseAccess.SelectDatabase(sql, ConnectStr);
				return CouplerApply.DataTableToList(table);
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("qGetApplyNoSystem() Error({0})", ex.Message));
			}
		}

		/// <summary>
		/// サービス情報ワークの物理削除
		/// </summary>
		/// <param name="cp_id">MWSID</param>
		/// <returns>処理結果</returns>
		private bool qDelServiceUserWork(string cp_id)
		{
			try
			{
				string sql = string.Format("DELETE FROM SERVICE_WORK WHERE CP_ID = '{0}'", cp_id);
				if (-1 != DatabaseAccess.DeleteDatabase(sql, ConnectStr))
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("qDelServiceUserWork() Error({0})", ex.Message));
			}
			return false;
		}

		/// <summary>
		/// サービス情報ワーク登録
		/// </summary>
		/// <param name="data">サービス情報</param>
		/// <returns>処理結果</returns>
		private bool qInsServiceWork(CouplerService data)
		{
			try
			{
				string sql = "INSERT INTO SERVICE_WORK (cp_id, service_id, start_date, end_date, contrac_type, payment_type, create_date, create_user) VALUES (@1, @2, @3, @4, @5, @6)";
				SqlParameter[] param = {
										new SqlParameter("@1", data.cp_id),
										new SqlParameter("@2", data.service_id),
										new SqlParameter("@3", data.start_date.HasValue ? data.start_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
										new SqlParameter("@4", data.end_date.HasValue ? data.end_date.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
										new SqlParameter("@5", data.contrac_type),
										new SqlParameter("@6", data.payment_type),
										new SqlParameter("@7", DateTime.Now.ToString()),
										new SqlParameter("@8", "BAT")
									};
				if (0 < DatabaseAccess.InsertIntoDatabase(sql, param, ConnectStr))
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("qInsServiceWork() Error({0})", ex.Message));
			}
			return false;
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
				if (-1 != DatabaseAccess.DeleteDatabase(sql, ConnectStr))
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
				if (0 < DatabaseAccess.InsertIntoDatabase(sql, param, ConnectStr))
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
		/// 製品顧客情報ワーク更新
		/// </summary>
		/// <param name="out1a"顧客情報></param>
		/// <returns>処理結果</returns>
		private bool qUpdProductUserWork(stU005_out1a out1a)
		{
			try
			{
				string sql = string.Format("SELECT login_paswd, paswd_update, ver_id FROM PRODUCTUSER WHERE cp_id = '{0}'", out1a.PRODUCT_ID);
				DataTable table = DatabaseAccess.SelectDatabase(sql, ConnectStr);
				if (null != table && 0 < table.Rows.Count)
				{
					DataRow row = table.Rows[0];
					int ver_id = DataBaseValue.ConvObjectToInt(row["ver_id"]);
					string login_paswd = row["login_paswd"].ToString().Trim();
					string paswd_update = row["login_paswd"].ToString().Trim();
					sql = string.Format("UPDATE PRODUCTUSER_WORK SET ver_id = {0}, login_paswd = '{1}', ,paswd_update = '{2}' WHERE cp_id = '{3}'"
										, ver_id
										, login_paswd
										, paswd_update
										, out1a.PRODUCT_ID);
					if (0 < DatabaseAccess.ExecuteNonQuery(sql, ConnectStr))
					{
						return true;
					}
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException(string.Format("qUpdProductUserWork() Error({0})", ex.Message));
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
				DataTable table = DatabaseAccess.SelectDatabase(sql, ConnectStr);
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
					if (0 < DatabaseAccess.UpdateSetDatabase(sql, param, ConnectStr))
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
	}
}