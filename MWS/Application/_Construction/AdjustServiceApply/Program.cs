//
// Program.cs
//
// サービス申込情報更新処理 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using AdjustServiceApply.Log;
using AdjustServiceApply.Settings;
using CommonLib.BaseFactory.AdjustServiceApply;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Charlie.View;
using CommonLib.BaseFactory.Coupler.Table;
using CommonLib.Common;
using CommonLib.DB.SqlServer;
using CommonLib.DB.SqlServer.AdjustServiceApply;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.Coupler;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AdjustServiceApply.Mail;

namespace AdjustServiceApply
{
	internal static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string gProcName = "申込情報更新処理";

		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string gVersionStr = "1.00";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static AdjustServiceApplySettings gSettings { get; set; }

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			gSettings = AdjustServiceApplySettingsIF.GetSettings();

			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				switch (cmds[1])
				{
					case "1":
						// 顧客情報更新処理
						ExecCustomerInfo();
						return;
					case "2":
						// 申込情報更新処理
						ExecApplyInfo();
						return;
				}
			}
			Application.Run(new Forms.MainForm());
		}

		/// <summary>
		/// 顧客管理基本情報（T_CUSTOMER_FOUNDATIONS）の登録
		/// </summary>
		/// <param name="slip">伝票データ</param>
		/// <param name="saleType">販売種別</param>
		/// <param name="updateUser">更新者</param>
		/// <returns>影響行数</returns>
		private static int InsertIntoT_CUSTOMER_FOUNDATIONS(WW伝票参照ビュー slip, char saleType, string updateUser)
		{
			// ★顧客管理基本情報に顧客IDが存在しないため登録する
			T_CUSTOMER_FOUNDATIONS cf = new T_CUSTOMER_FOUNDATIONS();
			cf.CUSTOMER_ID = slip.ユーザー顧客ID;
			cf.MARKETING_SPECIALIST_ID = slip.担当者ID;
			cf.STORE_BILLING_ADDRESS_CODE = slip.販売先顧客ID;
			cf.STORE_CODE = slip.販売先顧客ID;
			//cf.LICENSE_FLG = false;
			//cf.APPLY_RECOVERY_DAY = null;
			cf.SALE_TYPE = saleType;
			//cf.DELETE_FLG = false;
			cf.CREATE_DATE = DateTime.Now;
			cf.CREATE_PERSON = updateUser;
			//cf.UPDATE_DATE = DateTime.Now;
			//cf.UPDATE_PERSON = updateUser;
			if (0 < slip.申込種別)
			{
				cf.APPLY_TYPE = slip.申込種別.ToString()[0];
			}
			return CharlieDatabaseAccess.InsertInto_T_CUSTOMER_FOUNDATIONS(cf, gSettings.ConnectCharlie.ConnectionString);
		}

		/// <summary>
		/// 顧客管理利用情報（T_CUSSTOMER_USE_INFOMATION）の登録
		/// </summary>
		/// <param name="slip">伝票データ</param>
		/// <param name="codeMaster">MWSコードマスタ</param>
		/// <param name="startDate">利用開始日</param>
		/// <param name="endDate">課金終了日</param>
		/// <param name="updateUser">更新者</param>
		/// <returns>影響行数</returns>
		private static int InsertIntoT_CUSSTOMER_USE_INFOMATION(WW伝票参照ビュー slip, M_CODE_EX codeMaster, DateTime? startDate, DateTime? endDate, string updateUser)
		{
			T_CUSSTOMER_USE_INFOMATION cui = new T_CUSSTOMER_USE_INFOMATION();
			cui.CUSTOMER_ID = slip.ユーザー顧客ID;
			cui.SERVICE_TYPE_ID = codeMaster.SERVICE_TYPE_ID;
			cui.SERVICE_ID = codeMaster.SERVICE_ID;
			cui.GOODS_ID = slip.商品コード;
			//cui.APPLICATION_NO = NULL
			//cui.KAKIN_START_DATE = NULL
			cui.USE_START_DATE = startDate;
			cui.USE_END_DATE = endDate;
			//cui.CANCELLATION_DAY = NULL
			//cui.CANCELLATION_PROCESSING_DATE = NULL
			//cui.PAUSE_END_STATUS = 0
			//cui.DELETE_FLG = 0
			cui.CREATE_DATE = DateTime.Now;
			cui.CREATE_PERSON = updateUser;
			//cui.UPDATE_DATE = DateTime.Now;
			//cui.UPDATE_PERSON = Program.gProcName;
			//cui.PERIOD_END_DATE = NULL
			//cui.RENEWAL_FLG = 
			return CharlieDatabaseAccess.InsertInto_T_CUSSTOMER_USE_INFOMATION(cui, gSettings.ConnectCharlie.ConnectionString);
		}

		/// <summary>
		/// WW伝票抽出処理
		/// (1)顧客マスタ参照ビューを参照し顧客管理基本・顧客管理利用情報に、顧客が存在しない場合は削除フラグを1にする
		/// (2)WW伝票参照ビューを元に顧客管理基本の登録・更新・顧客管理利用情報の削除/登録を行う。
		///    	(※顧客マスタ参照ビューに存在しない場合は、処理をスキップする）
		/// </summary>
		/// <param name="updateUser">更新者</param>
		/// <param name="mailLogList">ログリスト</param>
		/// <param name="alartCount">警告ログ出力回数</param>
		/// <param name="errorCount">エラーログ出力回数</param>
		private static void Auto_Create_Data(string updateUser, List<string> mailLogList, out int alartCount, out int errorCount)
		{
			alartCount = 0;     // 警告ログ出力回数
			errorCount = 0;     // エラーログ出力回数

			string log = string.Empty;
			try
			{
				log = "WW伝票抽出処理 開始";
				LogOut.Out(log);
				mailLogList.Add(log);

				List<int> customerIDList = new List<int>();

				// 受注承認日が前回同期日時以降の伝票で、数量>0、伝票番号が最小の伝票データの取得
				// 1-1_Sel_V_CHECK.sql
				List<WW伝票参照ビュー> slipList = AdjustServiceApplyAccess.GetWonderWebSlip(gSettings.ConnectCharlie.ConnectionString);
				if (null != slipList && 0 < slipList.Count)
				{
					foreach (WW伝票参照ビュー slip in slipList)
					{
						try
						{
							string whereStr = string.Format("CUSTOMER_ID = {0}", slip.ユーザー顧客ID);
							List<T_PRODUCT_CONTROL> pcList = CharlieDatabaseAccess.Select_T_PRODUCT_CONTROL(whereStr, "", gSettings.ConnectCharlie.ConnectionString);
							if (null != pcList && 0 < pcList.Count)
							{
								// ログ「WW伝票商品ID【XXXX】MWSID発行済み」
								log = string.Format("WW伝票商品ID【{0}】MWSID発行済み", slip.商品コード);
								LogOut.Out(log);
								mailLogList.Add(log);

								whereStr = string.Format("GOODS_ID = '{0}'", slip.商品コード);
								List<M_CODE_EX> codeList = CharlieDatabaseAccess.Select_M_CODE_EX(whereStr, "", gSettings.ConnectCharlie.ConnectionString);
								if (null != codeList && 0 < codeList.Count)
								{
									if ("1" == codeList[0].SET_SALE)
									{
										// 月額課金用サービス
										whereStr = string.Format("CUSTOMER_ID = {0}", slip.ユーザー顧客ID);
										List<V_CUSTOMER> customerList = CharlieDatabaseAccess.Select_V_CUSTOMER(whereStr, "", gSettings.ConnectCharlie.ConnectionString);
										if (null != customerList && 0 < customerList.Count)
										{
											// 顧客マスタ参照ビューの登録カード回収日がnullの場合、ライセンス発行可能フラグに=0、その他はライセンス発行可能フラグ=1
											// ログ「ライセンス発行可能フラグ=XXXX」※登録カード回収日の有無
											log = string.Format("ライセンス発行可能フラグ={0}", (0 < customerList[0].RECOVERY_DAY.Length) ? "1" : "0");
											LogOut.Out(log);
											mailLogList.Add(log);

											// ログ「WW伝票担当者ID=XXXX」
											log = string.Format("WW伝票担当者ID={0}", slip.担当者ID);
											LogOut.Out(log);
											mailLogList.Add(log);

											string specialistMsgLog = string.Empty;
											whereStr = string.Format("社員番号 = '{0}' AND 営業区分 = 1", slip.担当者ID);
											List<社員マスタ参照ビュー> employeeList = CharlieDatabaseAccess.Select_社員マスタ参照ビュー(whereStr, "", gSettings.ConnectCharlie.ConnectionString);
											if (null == employeeList && 0 == employeeList.Count)
											{
												// 警告「※社員マスタ参照ビューに営業担当者ID(XXXX)が存在しません。」
												specialistMsgLog = string.Format("警告「社員マスタ参照ビューに営業担当者ID({0})が存在しません。」", slip.担当者ID);
												LogOut.Out(specialistMsgLog);
												mailLogList.Add(specialistMsgLog);
												alartCount++;
											}
											char saleType = ' ';
											string customerMsgLog = string.Empty;
											if (slip.販売先顧客ID == slip.ユーザー顧客ID)
											{
												// ログ「WW伝票の販売先顧客IDとユーザー顧客IDが同じです。(XXXX)」
												customerMsgLog = string.Format("WW伝票の販売先顧客IDとユーザー顧客IDが同じです。({0})", slip.販売先顧客ID);
												LogOut.Out(customerMsgLog);
												mailLogList.Add(customerMsgLog);
												saleType = '1';   // 販売種別:1（直接）
											}
											else
											{
												saleType = '2';   // 販売種別:2（販売店）
												List<int> storeCodeList = AdjustServiceApplyAccess.GetStoreCode(slip.販売先顧客ID, gSettings.ConnectCharlie.ConnectionString);
												if (null == storeCodeList && 0 == storeCodeList.Count)
												{
													// 警告「※WW伝票の販売先顧客ID(XXXX)が販売店情報に存在しませんでした。」
													customerMsgLog = string.Format("※WW伝票の販売先顧客ID({0})が販売店情報に存在しませんでした。", slip.販売先顧客ID);
													LogOut.Out(string.Format("警告「WW伝票の販売先顧客ID({0})が販売店情報に存在しませんでした。」", slip.販売先顧客ID));
													mailLogList.Add(customerMsgLog);
													alartCount++;
												}
											}
											// ログ「WW伝票申込種別=XXXX」※WW伝票から申込種別を取得
											log = string.Format("WW伝票申込種別={0}", slip.申込種別);
											LogOut.Out(log);
											mailLogList.Add(log);

											// WW伝票のユーザ顧客IDが顧客管理基本に存在するかチェック
											whereStr = string.Format("CUSTOMER_ID = {0}", slip.ユーザー顧客ID);
											List<T_CUSTOMER_FOUNDATIONS> cfList = CharlieDatabaseAccess.Select_T_CUSTOMER_FOUNDATIONS(whereStr, "", gSettings.ConnectCharlie.ConnectionString);
											if (null == cfList && 0 == cfList.Count)
											{
#if WRITE_DATABSE
												// ★顧客管理基本情報に顧客IDが存在しないため登録する
												InsertIntoT_CUSTOMER_FOUNDATIONS(slip, saleType, updateUser);
#endif
												customerIDList.Add(slip.ユーザー顧客ID);

												// 	ログ「伝票No：XXXX、商品コード：XXXXのデータを顧客管理基本に登録しました。（顧客ID：XXXX 顧客名：XXXX 営業担当者ID：XXXX 販売店（使用料請求先コード / 販売拠点コード）：XXXX XXXX  XXXX」
												log = string.Format("伝票No：{0}、商品コード：{1}のデータを顧客管理基本に登録しました。（顧客ID：{2} 顧客名：{3} 営業担当者ID：{4} 販売店（使用料請求先コード / 販売拠点コード）：{5} {6}  {7}"
																						, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID, customerList[0].CUSTOMER_NAME, slip.担当者ID, slip.販売先顧客ID, customerMsgLog, specialistMsgLog);
												LogOut.Out(log);
												mailLogList.Add(log);
											}
											// 顧客管理利用情報の顧客・サービス存在チェック
											whereStr = string.Format("CUSTOMER_ID = {0} AND SERVICE_TYPE_ID = {1} AND SERVICE_ID = {2}", slip.ユーザー顧客ID, codeList[0].SERVICE_TYPE_ID, codeList[0].SERVICE_ID);
											List<T_CUSSTOMER_USE_INFOMATION> cuiList = CharlieDatabaseAccess.Select_T_CUSSTOMER_USE_INFOMATION(whereStr, "", gSettings.ConnectCharlie.ConnectionString);
											if (null == cuiList && 0 == cuiList.Count)
											{
												// 顧客管理利用情報にサービスが登録されていない ※本来、受注承認時に登録されているはず
												whereStr = string.Format("CUSTOMER_ID = {0} AND SERVICE_TYPE_ID = {1} AND SERVICE_ID = {2} AND DELETE_FLG = '0' AND APPLICATION_CANCELLATION_FLG = '0' AND PCA_FINISHING_FLG = '0' AND APPLICATION_DATE >= DATEADD(dd, 1, EOMONTH(getdate(), -2)) AND APPLICATION_DATE <= getdate()"    // 先月初日～当日
																						, slip.ユーザー顧客ID, codeList[0].SERVICE_TYPE_ID, codeList[0].SERVICE_ID);
												List<T_APPLICATION_DATA> aplList = CharlieDatabaseAccess.Select_T_APPLICATION_DATA(whereStr, "", gSettings.ConnectCharlie.ConnectionString);
												if (null == aplList && 0 == aplList.Count)
												{
													// 利用期間：当日～翌月末日
													DateTime? startDate = DateTime.Today;   // システム日付
													DateTime? endDate = DateTime.Today.EndOfNextMonth();       // 多分、翌月末日だと思う？
													if (-1 != customerIDList.FindIndex(p => p == slip.ユーザー顧客ID))
													{
														// ※ただし、本処理で既に顧客管理基本情報[T_CUSTOMER_FOUNDATIONS]に顧客を登録済の場合には利用期間にnullを指定←処理の意図が不明
														startDate = null;
														endDate = null;
													}
#if WRITE_DATABSE
													// ★顧客管理利用情報に顧客・サービスを登録する
													InsertIntoT_CUSSTOMER_USE_INFOMATION(slip, codeList[0], startDate, endDate, updateUser);
#endif
													if (startDate.HasValue)
													{
														// ログ「伝票No：XXXX、商品コード：XXXXのデータを顧客管理利用情報に登録しました。（顧客ID：XXXX 顧客名：XXXX サービス種別ID：XXXX サービス種別名：XXXX サービスID：XXXX サービス名：XXXX）」
														log = string.Format("伝票No：{0}、商品コード：{1}のデータを顧客管理利用情報に登録しました。（顧客ID：{2} 顧客名：{3} サービス種別ID：{4} サービス種別名：{5} サービスID：{6} サービス名：{7}）"
																								, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID, customerList[0].CUSTOMER_NAME, codeList[0].SERVICE_TYPE_ID, codeList[0].SERVICE_TYPE_NAME, codeList[0].SERVICE_ID, codeList[0].SERVICE_NAME);
														LogOut.Out(log);
														mailLogList.Add(log);
													}
													else
													{
														// 警告「伝票No：XXXX、商品コード：XXXXのデータを顧客管理利用情報に登録しました。（顧客ID：XXXX 顧客名：XXXX サービス種別ID：XXXX サービス種別名：XXXX サービスID：XXXX サービス名：XXXX)
														//        ※警告：登録されましたが利用期間が設定されていません。必ず利用期間を設定して下さい。利用期間が設定されるまではCouplerへの同期はされません。」
														log = string.Format("伝票No：{0}、商品コード：{1}のデータを顧客管理利用情報に登録しました。（顧客ID：{2} 顧客名：{3} サービス種別ID：{4} サービス種別名：{5} サービスID：{6} サービス名：{7})"
																						+ "※警告：登録されましたが利用期間が設定されていません。必ず利用期間を設定して下さい。利用期間が設定されるまではCouplerへの同期はされません。"
																						, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID, customerList[0].CUSTOMER_NAME, codeList[0].SERVICE_TYPE_ID, codeList[0].SERVICE_TYPE_NAME, codeList[0].SERVICE_ID, codeList[0].SERVICE_NAME);
														LogOut.Out(log);
														mailLogList.Add(log);
														alartCount++;
													}
												}
												else
												{
													// ログ「伝票No：XXXX、商品コード：XXXXのデータは既に申込情報に存在しているため処理をスキップしました。（顧客ID：XXXX 顧客名：XXXX サービス種別ID：XXXX サービス種別名：XXXX サービスID：XXXX サービス名：XXXX）」
													log = string.Format("伝票No：{0}、商品コード：{1}のデータは既に申込情報に存在しているため処理をスキップしました。（顧客ID：{2} 顧客名：{3} サービス種別ID：{4} サービス種別名：{5} サービスID：{6} サービス名：{7}）"
																						, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID, customerList[0].CUSTOMER_NAME, codeList[0].SERVICE_TYPE_ID, codeList[0].SERVICE_TYPE_NAME, codeList[0].SERVICE_ID, codeList[0].SERVICE_NAME);
													LogOut.Out(log);
													mailLogList.Add(log);
												}
											}
											else
											{
												// 顧客管理利用情報にサービスが登録されているので、無処理
												;
											}
										}
										else
										{
											// 警告「COUPLER IDをもたないユーザーに「XXXX」のサービス伝票が起票されました。伝票No：XXXX、商品コード：XXXX（顧客ID：XXXX ）」
											log = string.Format("警告「COUPLER IDをもたないユーザーに「{0}」のサービス伝票が起票されました。伝票No：{1}、商品コード：{2}（顧客ID：{3}）」"
																					, codeList[0].SERVICE_ID, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID);
											LogOut.Out(log);
											mailLogList.Add(log);
											alartCount++;
										}
									}
									else
									{
										// 処理必要なし  一括販売用 ※PC安心サポートなど
										// 朝バッチでは一括販売用のサービスも顧客利用情報に追加していたので、登録する必要のないサービスが登録されていた
										log = string.Format("伝票No：{0}、商品コード：{1}のデータは一括販売用サービスなので、処理をスキップしました。（顧客ID：{2} サービス種別ID：{3} サービス種別名：{4} サービスID：{5} サービス名：{6}）"
																								, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID, codeList[0].SERVICE_TYPE_ID, codeList[0].SERVICE_TYPE_NAME, codeList[0].SERVICE_ID, codeList[0].SERVICE_NAME);
										LogOut.Out(log);
										mailLogList.Add(log);
									}
								}
								else
								{
									// 警告「商品ID【XXXX】がコードマスター(M_CODE)に存在しません。」
									log = string.Format("警告「商品ID【{0}】がコードマスター(M_CODE)に存在しません。」", slip.商品コード);
									LogOut.Out(log);
									mailLogList.Add(log);
									alartCount++;
								}
							}
							else
							{
								// 警告「WW伝票参照ビュー：伝票No【XXXX】顧客ID【XXXX】※T_PRODUCT_CONTROLに顧客IDが存在しません。」
								log = string.Format("警告「WW伝票参照ビュー：伝票No【{0}】顧客ID【{1}】※T_PRODUCT_CONTROLに顧客IDが存在しません。」", slip.伝票No, slip.ユーザー顧客ID);
								LogOut.Out(log);
								mailLogList.Add(log);
								alartCount++;

								whereStr = string.Format("GOODS_ID = '{0}'", slip.商品コード);
								List<M_CODE> codeList = CharlieDatabaseAccess.Select_M_CODE(whereStr, "", gSettings.ConnectCharlie.ConnectionString);
								if (null != codeList && 0 < codeList.Count)
								{
									whereStr = string.Format("CUSTOMER_ID = {0}", slip.ユーザー顧客ID);
									List<V_CUSTOMER> customerList = CharlieDatabaseAccess.Select_V_CUSTOMER(whereStr, "", gSettings.ConnectCharlie.ConnectionString);
									if (null != customerList && 0 < customerList.Count)
									{
										// 警告「COUPLER IDをもたないユーザーに「XXXX」のサービス伝票が起票されました。伝票No：XXXX、商品コード：XXXX（顧客ID：XXXX 顧客名：XXXX）」
										log = string.Format("警告「COUPLER IDをもたないユーザーに「{0}」のサービス伝票が起票されました。伝票No：{1}、商品コード：{2}（顧客ID：{3} 顧客名：{4}）」"
																			, codeList[0].SERVICE_ID, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID, customerList[0].CUSTOMER_NAME);
										LogOut.Out(log);
										mailLogList.Add(log);
										alartCount++;
									}
									else
									{
										// 警告「COUPLER IDをもたないユーザーに「XXXX」のサービス伝票が起票されました。伝票No：XXXX、商品コード：XXXX（顧客ID：XXXX ）」
										log= string.Format("警告「COUPLER IDをもたないユーザーに「{0}」のサービス伝票が起票されました。伝票No：{1}、商品コード：{2}（顧客ID：{3} ）」"
																			, codeList[0].SERVICE_ID, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID);
										LogOut.Out(log);
										mailLogList.Add(log);
										alartCount++;
									}
								}
								else
								{
									// 警告「商品ID【XXXX】がコードマスター(M_CODE)に存在しません。」
									log = string.Format("警告「商品ID【{0}】がコードマスター(M_CODE)に存在しません。」", slip.商品コード);
									LogOut.Out(log);
									mailLogList.Add(log);
									alartCount++;
								}
							}
						}
						catch (Exception ex)
						{
							log = string.Format("例外エラー「{0} 伝票No：{1} 商品コード：{2} 顧客ID：{3}」 ", ex.Message, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID);
							LogOut.Out(log);
							mailLogList.Add(log);
						}
					}
					log= string.Format("WW伝票抽出処理 終了【取得件数：{0} 件】", slipList.Count);
					LogOut.Out(log);
					mailLogList.Add(log);
				}
				else
				{
					log= "WW伝票抽出処理 終了【取得件数：0 件】";
					LogOut.Out(log);
					mailLogList.Add(log);
				}
			}
			catch (Exception ex)
			{
				log = string.Format("WW伝票抽出処理 異常終了：{0}", ex.Message);
				LogOut.Out(log);
				mailLogList.Add(log);
				return;
			}
			log= "WW伝票抽出処理 終了";
			LogOut.Out(log);
			mailLogList.Add(log);
		}

		/// <summary>
		/// 申込情報の更新
		/// </summary>
		/// <param name="updateUser">更新者</param>
		/// <param name="mailLogList">ログリスト</param>
		private static void CusDataUpdate_Main(string updateUser, List<string> mailLogList)
		{
			try
			{
				string log = "CHARLIEDBから基本機能パック取得開始 【PCA商品区分 = 200】";
				LogOut.Out(log);
				mailLogList.Add(log);

				// 2-1_基本機能パック.sql
				M_CODE mwsCodeMaster = AdjustServiceApplyAccess.GetKihonPack(gSettings.ConnectCharlie.ConnectionString);
				if (null != mwsCodeMaster)
				{
					log = string.Format("CHARLIEDBから基本機能パック取得終了 【商品ID = {0} / サービス種別ID = {1} / サービスID = {2}】", mwsCodeMaster.GOODS_ID, mwsCodeMaster.SERVICE_TYPE_ID, mwsCodeMaster.SERVICE_ID);
				}
				else
				{
					log = "CHARLIEDBから基本機能パック取得終了 【商品ID = / サービス種別ID = / サービスID = 】";
				}
				LogOut.Out(log);
				mailLogList.Add(log);

				// 前回同期日時の取得
				// 2-2_Sel_T_FILE_CREATEDATE_利用情報.sql
				T_FILE_CREATEDATE synchroTime = AdjustServiceApplyAccess.GetLastSynchroTime(gSettings.ConnectCharlie.ConnectionString);
				log = string.Format("前回同期日時：{0}", synchroTime.FILE_CREATEDATE.ToString());
				LogOut.Out(log);
				mailLogList.Add(log);

				List<T_CUSSTOMER_USE_INFOMATION> cuiList = null;

				// 先月分の申込情報の利用申込のシステム反映フラグを設定
				// 2-3_申込情報-先月分利用申込.sql
				// システム反映フラグ=0 AND 申込種別=利用申込 AND 申込日付=先月
				string whereStr = "system_flg = '0' AND apply_type = '0' AND LEFT(cp_id, 3) = 'MWS' AND apply_date >= DATEADD(dd, 1, EOMONTH(getdate(), -2)) AND apply_date <= EOMONTH(getdate(), -1)";		// 先月初日～先月末日
				//string whereStr = "system_flg <> '2' AND apply_type = '0' AND LEFT(cp_id, 3) = 'MWS' AND apply_date >= DATEADD(dd, 1, EOMONTH(getdate(), -2)) AND apply_date <= EOMONTH(getdate(), -1)";    // 先月初日～先月末日
				List<V_COUPLER_APPLY> useApplyList = CharlieDatabaseAccess.Select_V_COUPLER_APPLY(whereStr, "customer_id, service_id, apply_id", gSettings.ConnectCharlie.ConnectionString);
				if (null != useApplyList && 0 < useApplyList.Count)
				{
					//useApplyList = useApplyList.FindAll(p => p.customer_id == 10000071 && p.service_id == 1026100);

					var customerIDList = useApplyList.GroupBy(x => x.customer_id);
					foreach (var id in customerIDList)
					{
						// 2-4_顧客利用情報-利用申込.sql
						// 基本サービス以外 AND 課金対象外フラグ=OFF AND 利用期限日=NULL AND 利用開始日<>NULL AND 利用終了日<>NULL AND 利用開始日が翌月末日以前 AND 利用終了日が翌月末日以降
						whereStr = string.Format("CUSTOMER_ID = {0} AND SERVICE_TYPE_ID <> 1 AND PAUSE_END_STATUS = 0 AND PERIOD_END_DATE is null AND USE_START_DATE is not null AND USE_END_DATE is not null AND USE_START_DATE <= EOMONTH(getdate(), 1) AND USE_END_DATE >= EOMONTH(getdate(), 1)"
															, id.Key);
						cuiList = CharlieDatabaseAccess.Select_T_CUSSTOMER_USE_INFOMATION(whereStr, "SERVICE_ID", gSettings.ConnectCharlie.ConnectionString);
						if (null != cuiList && 0 < cuiList.Count)
						{
							List<V_COUPLER_APPLY> updateUseApplyList = new List<V_COUPLER_APPLY>();
							List<V_COUPLER_APPLY> applyList = useApplyList.FindAll(p => p.customer_id == id.Key);
							foreach (V_COUPLER_APPLY apply in applyList)
							{
								T_CUSSTOMER_USE_INFOMATION cui = cuiList.Find(p => p.SERVICE_ID == apply.service_id);
								if (null != cui)
								{
									apply.system_flg = "1";
									updateUseApplyList.Add(apply);
									log = string.Format("利用申込情報システム反映フラグ設定（CouplerID：{0} サービスID：{1} 申込日時：{2}）", apply.cp_id, apply.service_id, apply.apply_date.Value.ToShortDateString());
									LogOut.Out(log);
									mailLogList.Add(log);
								}
							}
							if (0 < updateUseApplyList.Count)
							{
#if WRITE_DATABSE
								// 申込情報の更新
								UpdateSet_T_COUPLER_APPLY(updateUseApplyList, updateUser, gSettings.ConnectCharlie.ConnectionString, gSettings.ConnectCoupler.DatabaseName);
#endif
							}
						}
					}
				}
				// 先月分の申込情報の解約申込のシステム反映フラグを設定
				// 2-5_申込情報-先月分解約申込.sql
				// システム反映フラグ=0 AND 申込種別=解約申込 AND 申込日付=先月
				whereStr = "system_flg = '0' AND apply_type = '1' AND LEFT(cp_id, 3) = 'MWS' AND apply_date >= DATEADD(dd, 1, EOMONTH(getdate(), -2)) AND apply_date <= EOMONTH(getdate(), -1)";    // 先月初日～先月末日

				// テスト用コード
				//whereStr = "system_flg <> '2' AND apply_type = '1' AND LEFT(cp_id, 3) = 'MWS' AND apply_date >= DATEADD(dd, 1, EOMONTH(getdate(), -2)) AND apply_date <= EOMONTH(getdate(), -1)";   // 先月初日～先月末日

				List<V_COUPLER_APPLY> cancelApplyList = CharlieDatabaseAccess.Select_V_COUPLER_APPLY(whereStr, "customer_id, service_id, apply_id", gSettings.ConnectCharlie.ConnectionString);
				if (null != cancelApplyList && 0 < cancelApplyList.Count)
				{
					// テスト用コード
					//cancelApplyList = cancelApplyList.FindAll(p => p.customer_id == 10000060 && p.service_id == 1020100);

					var customerIDList = cancelApplyList.GroupBy(x => x.customer_id);
					foreach (var id in customerIDList)
					{
						// 2-6_顧客利用情報-解約申込.sql
						// 基本サービス以外 AND 課金対象外フラグ=ON AND 利用期限日<>NULL
						whereStr = string.Format("CUSTOMER_ID = {0} AND SERVICE_TYPE_ID <> 1 AND PAUSE_END_STATUS = 1 AND PERIOD_END_DATE is not null", id.Key);
						cuiList = CharlieDatabaseAccess.Select_T_CUSSTOMER_USE_INFOMATION(whereStr, "SERVICE_ID", gSettings.ConnectCharlie.ConnectionString);
						if (null != cuiList && 0 < cuiList.Count)
						{
							List<V_COUPLER_APPLY> updateCancelApplyList = new List<V_COUPLER_APPLY>();
							List<V_COUPLER_APPLY> applyList = cancelApplyList.FindAll(p => p.customer_id == id.Key);
							foreach (V_COUPLER_APPLY apply in applyList)
							{
								T_CUSSTOMER_USE_INFOMATION cui = cuiList.Find(p => p.SERVICE_ID == apply.service_id);
								if (null != cui)
								{
									apply.system_flg = "1";
									updateCancelApplyList.Add(apply);
									log = string.Format("解約申込情報システム反映フラグ設定（CouplerID：{0} サービスID：{1} 申込日時：{2}）", apply.cp_id, apply.service_id, apply.apply_date.Value.ToShortDateString());
									LogOut.Out(log);
									mailLogList.Add(log);
								}
							}
							if (0 < updateCancelApplyList.Count)
							{
#if WRITE_DATABSE
								// 申込情報の更新
								UpdateSet_T_COUPLER_APPLY(updateCancelApplyList, updateUser, gSettings.ConnectCharlie.ConnectionString, gSettings.ConnectCoupler.DatabaseName);
#endif
							}
						}
					}
				}
				// 前回同期日時以降に追加、更新された顧客利用情報のサービスに該当する申込情報のシステム反映フラグを更新する
				// →上記の処理では前前月以前の申込情報のシステム反映フラグが更新されない  ※達人プラスなどは申込後、しばらくたってからナルコームから連絡があり、その後起票するため
				// 2-8_顧客利用情報-最終出力日時以降.sql
				// 基本サービス以外 AND 利用開始日<>null AND 利用終了日<>null AND (作成日時 > 前回同期日時 OR 更新日時 > 前回同期日時)
				cuiList = AdjustServiceApplyAccess.GetCustomerUseInformationAfterSynchroTime(gSettings.ConnectCharlie.ConnectionString);
				if (null != cuiList && 0 < cuiList.Count)
				{
					// 利用中サービスの抽出
					List<T_CUSSTOMER_USE_INFOMATION> useCuiList = cuiList.FindAll(p => p.PAUSE_END_STATUS == false);
					if (null != useCuiList && 0 < useCuiList.Count)
					{
						// 前月以前、システム反映フラグ=0の利用申込情報を抽出
						// 2-9_申込情報-利用申込.sql
						whereStr = "system_flg = '0' AND apply_type = '0' AND LEFT(cp_id, 3) = 'MWS' AND apply_date < DATEADD(dd, 1, EOMONTH(getdate() , -1))";     // 申込日が前月末日以前
						useApplyList = CharlieDatabaseAccess.Select_V_COUPLER_APPLY(whereStr, "customer_id, service_id, apply_id", gSettings.ConnectCharlie.ConnectionString);
						if (null != useApplyList && 0 < useApplyList.Count)
						{
							List<V_COUPLER_APPLY> updateUseApplyList = new List<V_COUPLER_APPLY>();
							foreach (T_CUSSTOMER_USE_INFOMATION cui in useCuiList)
							{
								List<V_COUPLER_APPLY> applyList = useApplyList.FindAll(p => p.customer_id == cui.CUSTOMER_ID && p.service_id == cui.SERVICE_ID);
								if (null != applyList && 0 < applyList.Count)
								{
									foreach (V_COUPLER_APPLY apply in applyList)
									{
										apply.system_flg = "1";
										updateUseApplyList.Add(apply);
										log = string.Format("利用申込情報システム反映フラグ設定（CouplerID：{0} サービスID：{1} 申込日時：{2}）", apply.cp_id, apply.service_id, apply.apply_date.Value.ToShortDateString());
										LogOut.Out(log);
										mailLogList.Add(log);
									}
								}
							}
							if (0 < updateUseApplyList.Count)
							{
#if WRITE_DATABSE
								// 申込情報の更新
								UpdateSet_T_COUPLER_APPLY(updateUseApplyList, updateUser, gSettings.ConnectCoupler.ConnectionString, gSettings.ConnectCoupler.DatabaseName);
#endif
							}
						}
					}
					// 解約サービスの抽出
					List<T_CUSSTOMER_USE_INFOMATION> cancelCuiList = cuiList.FindAll(p => p.PAUSE_END_STATUS == true);
					if (null != cancelCuiList && 0 < cancelCuiList.Count)
					{
						// 前月以前、システム反映フラグ=0の解約申込情報を抽出
						// 2-10_申込情報-解約申込.sql
						whereStr = "system_flg = '0' AND apply_type = '1' AND LEFT(cp_id, 3) = 'MWS' AND apply_date < DATEADD(dd, 1, EOMONTH(getdate() , -1))";     // 申込日が前月末日以前
						cancelApplyList = CharlieDatabaseAccess.Select_V_COUPLER_APPLY(whereStr, "customer_id, service_id, apply_id", gSettings.ConnectCharlie.ConnectionString);
						if (null != cancelApplyList && 0 < cancelApplyList.Count)
						{
							List<V_COUPLER_APPLY> updateCancelApplyList = new List<V_COUPLER_APPLY>();
							foreach (T_CUSSTOMER_USE_INFOMATION cui in cancelCuiList)
							{
								List<V_COUPLER_APPLY> applyList = useApplyList.FindAll(p => p.customer_id == cui.CUSTOMER_ID && p.service_id == cui.SERVICE_ID);
								if (null != applyList && 0 < applyList.Count)
								{
									foreach (V_COUPLER_APPLY apply in applyList)
									{
										apply.system_flg = "1";
										updateCancelApplyList.Add(apply);
										log = string.Format("解約申込情報システム反映フラグ設定（CouplerID：{0} サービスID：{1} 申込日時：{2}）", apply.cp_id, apply.service_id, apply.apply_date.Value.ToShortDateString());
										LogOut.Out(log);
										mailLogList.Add(log);
									}
								}
							}
							if (0 < updateCancelApplyList.Count)
							{
#if WRITE_DATABSE
								// 申込情報の更新
								UpdateSet_T_COUPLER_APPLY(updateCancelApplyList, updateUser, gSettings.ConnectCoupler.ConnectionString, gSettings.ConnectCoupler.DatabaseName);
#endif
							}
						}
					}
				}
				// サービス利用情報利用開始日および利用終了日のNULLチェック
				List<CheckCuiUseDate> useDateList = AdjustServiceApplyAccess.GetCheckCuiUseDate(gSettings.ConnectCharlie.ConnectionString);
				if (null != useDateList && 0 < useDateList.Count)
				{
					foreach (CheckCuiUseDate useDate in useDateList)
					{
						log = useDate.GetLog();
						LogOut.Out(log);
						mailLogList.Add(log);
					}
				}
#if WRITE_DATABSE
				// 前回同期日時を追加
				// 2-7_InsertInto_T_FILE_CREATEDATE_利用情報.sql
				AdjustServiceApplyAccess.SetLastSynchroTimeForService(updateUser, gSettings.ConnectCharlie.ConnectionString);
#endif
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 申込情報更新処理
		/// </summary>
		public static void ExecApplyInfo()
		{
			// ログファイル名の設定
			string updateUser = "申込情報更新";
			LogOut.SetLogFileName("申込情報更新", Directory.GetCurrentDirectory());

			List<string> mailLogList = new List<string>();

			string log = "申込情報更新処理 開始";
			LogOut.Out(log);
			mailLogList.Add(log);

			// 顧客利用情報作成
			int alartCount;     // 警告ログ出力回数
			int errorCount;     // エラーログ出力回数
			Auto_Create_Data(updateUser, mailLogList, out alartCount, out errorCount);

			try
			{
				//LogOut.Out("CusDataUpdate_Main() Start");
				CusDataUpdate_Main(updateUser, mailLogList);
				//LogOut.Out("CusDataUpdate_Main() End");

				log = string.Format("警告 {0} 件", alartCount);
				LogOut.Out(log);
				mailLogList.Add(log);
				if (0 < errorCount)
				{
					log = string.Format("エラー {0} 件", errorCount);
					LogOut.Out(log);
					mailLogList.Add(log);
				}
			}
			catch (Exception ex)
			{
				log = string.Format("申込情報更新処理 異常終了：{0}\n", ex.Message);
				LogOut.Out(log);
				mailLogList.Add(log);

				// メール送信
				SendMailControl.AdjustServiceApplySendMail(gSettings.ConnectCharlie.InstanceName, false, mailLogList);
				return;
			}
			log = "申込情報更新処理 正常終了\n";
			LogOut.Out(log);
			mailLogList.Add(log);

			// メール送信
			SendMailControl.AdjustServiceApplySendMail(gSettings.ConnectCharlie.InstanceName, true, mailLogList);
		}

		/// <summary>
		/// 1. 顧客情報更新処理
		/// </summary>
		public static void ExecCustomerInfo()
		{
			// ログファイル名の設定
			string updateUser = "顧客情報更新";
			LogOut.SetLogFileName(updateUser, Directory.GetCurrentDirectory());

			LogOut.Out("顧客情報更新処理 開始");
			try
			{
				// MWSユーザー（MWS）
				// ID：T_PRODUCT_CONTROL.PRODUCT_ID
				// ユーザー種別：T_PRODUCT_CONTROL.USER_CLASSIFICATION
				// 体験版フラグ：T_PRODUCT_CONTROL.TRIAL_FLG
				// 終了フラグ：CASE T_PRODUCT_CONTROL.END_FLG WHEN '2' THEN '1' ELSE T_PRODUCT_CONTROL.END_FLG END
				// 顧客No：T_PRODUCT_CONTROL.CUSTOMER_ID
				// 顧客名：tClient.fCliName + tMikユーザ.fkj顧客名２
				// メールアドレス１：tMih支店情報.fメールアドレス ※拠点のメールアドレス
				// メールアドレス２：SELECT TOP 1 MAIL_ADDRESS FROM M_MAIL
				// 利用開始日付：T_PRODUCT_CONTROL.TRIAL_START_DATE
				// 利用終了日付：iif(T_PRODUCT_CONTROL.PERIOD_END_DATE is null, '2999-12-31 23:59:59.000', T_PRODUCT_CONTROL.PERIOD_END_DATE)
				// 初期パスワード：T_PRODUCT_CONTROL.PASSWORD
				// 同時接続クライアント数：tMikユーザ.fus同時接続ｸﾗｲｱﾝﾄ数
				// システムコード：tMikユーザ.fusシステム名

				// 前回同期日時以降の顧客情報の取得（MWSユーザー）
				List<UpdateCouplerProductUser> mwsList = AdjustServiceApplyAccess.GetMwsUserAfterSynchroTime(gSettings.ConnectCharlie.ConnectionString);
				if (null != mwsList && 0 < mwsList.Count)
				{
					foreach (UpdateCouplerProductUser user in mwsList)
					{
						// 同時接続クライアント数を設定
						user.license_count = user.GetClientLicenseCount();
					}
#if WRITE_DATABSE
					// 顧客情報の更新
					Set_T_COUPLER_PRODUCTUSER(mwsList, updateUser, gSettings.ConnectCharlie.ConnectionString, gSettings.ConnectCoupler.DatabaseName);
#endif
				}
				else
				{
					LogOut.Out("MWSユーザーの更新はありませんでした。");
				}
				// MWS体験版ユーザー（MWS）
				// ID：T_PRODUCT_CONTROL.PRODUCT_ID
				// ユーザー種別：T_PRODUCT_CONTROL.USER_CLASSIFICATION
				// 体験版フラグ：T_PRODUCT_CONTROL.TRIAL_FLG
				// 終了フラグ：CASE T_PRODUCT_CONTROL.END_FLG WHEN '2' THEN '1' ELSE T_PRODUCT_CONTROL.END_FLG END
				// 顧客No：T_PRODUCT_CONTROL.CUSTOMER_ID
				// 顧客名：tClient.fCliName + tMikユーザ.fkj顧客名２
				// メールアドレス１：tMih支店情報.fメールアドレス ※拠点のメールアドレス
				// メールアドレス２：SELECT TOP 1 MAIL_ADDRESS FROM M_MAIL
				// 利用開始日付：T_PRODUCT_CONTROL.TRIAL_START_DATE
				// 利用終了日付：iif(T_PRODUCT_CONTROL.PERIOD_END_DATE is null, '2999-12-31 23:59:59.000', T_PRODUCT_CONTROL.PERIOD_END_DATE)
				// 初期パスワード：T_PRODUCT_CONTROL.PASSWORD
				// 同時接続クライアント数：tMikユーザ.fus同時接続ｸﾗｲｱﾝﾄ数
				// システムコード：tMikユーザ.fusシステム名

				// 前回同期日時以降の顧客情報の取得（体験版ユーザー）
				List<UpdateCouplerProductUser> trialList = AdjustServiceApplyAccess.GetTrialUserAfterSynchroTime(gSettings.ConnectCharlie.ConnectionString);
				if (null != trialList && 0 < trialList.Count)
				{
					foreach (UpdateCouplerProductUser user in trialList)
					{
						// 同時接続クライアント数を設定
						user.license_count = user.GetClientLicenseCount();
					}
#if WRITE_DATABSE
					// 顧客情報の更新
					Set_T_COUPLER_PRODUCTUSER(trialList, updateUser, gSettings.ConnectCharlie.ConnectionString, gSettings.ConnectCoupler.DatabaseName);
#endif
				}
				else
				{
					LogOut.Out("体験版ユーザーの更新はありませんでした。");
				}
				// 社員用ユーザー（AAA）、デモ用ユーザー（ADM）
				// ID：T_PRODUCT_CONTROL.PRODUCT_ID
				// ユーザー種別：T_PRODUCT_CONTROL.USER_CLASSIFICATION
				// 体験版フラグ：T_PRODUCT_CONTROL.TRIAL_FLG
				// 終了フラグ：CASE T_DEMO_USER.END_FLG WHEN '2' THEN '1' ELSE T_DEMO_USER.END_FLG END
				// 顧客No：T_PRODUCT_CONTROL.CUSTOMER_ID
				// 顧客名：T_DEMO_USER.NAME
				// メールアドレス１：T_DEMO_USER.MAILADDR1
				// メールアドレス２：T_DEMO_USER.MAILADDR2
				// 利用開始日付：T_PRODUCT_CONTROL.TRIAL_START_DATE
				// 利用終了日付：iif(T_PRODUCT_CONTROL.PERIOD_END_DATE is null, '2999-12-31 23:59:59.000', T_PRODUCT_CONTROL.PERIOD_END_DATE)
				// 初期パスワード：T_PRODUCT_CONTROL.PASSWORD
				// 同時接続クライアント数：10固定
				// システムコード：100固定

				// 前回同期日時以降の顧客情報の取得（社員用ユーザーAAA、デモ用ユーザーADM）
				List<UpdateCouplerProductUser> demoList = AdjustServiceApplyAccess.GetDemoUserAfterSynchroTime(gSettings.ConnectCharlie.ConnectionString);
				if (null != demoList && 0 < demoList.Count)
				{
					foreach (UpdateCouplerProductUser user in demoList)
					{
						// 同時接続クライアント数を設定
						user.license_count = user.GetClientLicenseCount();
					}
#if WRITE_DATABSE
					// 顧客情報の更新
					Set_T_COUPLER_PRODUCTUSER(demoList, updateUser, gSettings.ConnectCharlie.ConnectionString, gSettings.ConnectCoupler.DatabaseName);
#endif
				}
				else
				{
					LogOut.Out("社員用ユーザー、デモ用ユーザーの更新はありませんでした。");
				}

#if WRITE_DATABSE
				// 前回同期日時を追加
				// 2-7_InsertInto_T_FILE_CREATEDATE_利用情報.sql
				AdjustServiceApplyAccess.SetLastSynchroTimeForCustomer(updateUser, gSettings.ConnectCharlie.ConnectionString);
#endif
			}
			catch (Exception ex)
			{
				LogOut.Out(string.Format("顧客情報更新処理 異常終了：{0}\n", ex.Message));
				return;
			}
			LogOut.Out("顧客情報更新処理 正常終了\n");
		}

		/// <summary>
		/// [CouplerDB].[dbo].[APPLY]の更新（申込情報）
		/// </summary>
		/// <param name="applyList">サービス申込情報リスト</param>
		/// <param name="updateUser">更新者</param>
		/// <param name="connectStr">SQL Server接続文字列</param>
		/// <param name="databaseName">データベース名</param>
		/// <returns>影響行数</returns>
		private static int UpdateSet_T_COUPLER_APPLY(List<V_COUPLER_APPLY> applyList, string updateUser, string connectStr, string databaseName)
		{
			int rowCount = -1;
			using (SqlConnection con = new SqlConnection(connectStr))
			{
				try
				{
					// 接続
					con.Open();

					using (SqlCommand cmd = new SqlCommand("SET XACT_ABORT ON", con))
					{
						cmd.ExecuteNonQuery();
					}
					foreach (V_COUPLER_APPLY apply in applyList)
					{
						try
						{
							// トランザクション開始
							using (SqlTransaction tran = con.BeginTransaction())
							{
								string sqlStr = string.Format(@"UPDATE {0} SET system_flg = '1', update_date = getdate(), update_user = '{1}' WHERE apply_id = {2}"
																			, string.Format("{0}.[dbo].{1}", databaseName, CouplerDatabaseDefine.TableName[CouplerDatabaseDefine.TableType.APPLY])
																			, updateUser, apply.apply_id);

								// 実行
								rowCount = DatabaseController.SqlExecuteNonQueryTran(con, tran, sqlStr);
								if (rowCount <= -1)
								{
									throw new ApplicationException("SqlExecuteNonQueryTran");
								}
								// コミット
								tran.Commit();
								LogOut.Out(string.Format("申込情報更新 CouplerID：{0} 顧客No：{1} サービスID：{2}", apply.cp_id, apply.customer_id, apply.service_id));
							}
						}
						catch (Exception ex2)
						{
							LogOut.Out(string.Format("申込情報更新エラー{0}:({1})", apply.cp_id, ex2.Message));
						}
					}
				}
				catch (Exception ex1)
				{
					throw new ApplicationException(string.Format("申込情報更新エラー ({0})", ex1.Message));
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
		/// [CouplerDB].[dbo].[PRODUCTUSER]の新規追加/更新/削除（顧客情報）
		/// </summary>
		/// <param name="userList">顧客情報リスト</param>
		/// <param name="updateUser">更新者</param>
		/// <param name="connectStr">SQL接続文字列</param>
		/// <param name="databaseName">データベース名</param>
		/// <returns></returns>
		/// <exception cref="ApplicationException"></exception>
		private static int Set_T_COUPLER_PRODUCTUSER(List<UpdateCouplerProductUser> userList, string updateUser, string connectStr, string databaseName)
		{
			int rowCount = -1;
			string sqlStr = string.Empty;
			using (SqlConnection con = new SqlConnection(connectStr))
			{
				try
				{
					// 接続
					con.Open();

					using (SqlCommand cmd = new SqlCommand("SET XACT_ABORT ON", con))
					{
						cmd.ExecuteNonQuery();
					}
					foreach (UpdateCouplerProductUser user in userList)
					{
						if (user.delete_flg)
						{
							// 削除
							try
							{
								// トランザクション開始
								using (SqlTransaction tran = con.BeginTransaction())
								{
									sqlStr = string.Format(@"DELETE FROM {0} WHERE [cp_id] = '{1}'"
																				, string.Format("{0}.[dbo].{1}", databaseName, CouplerDatabaseDefine.TableName[CouplerDatabaseDefine.TableType.PRODUCTUSER])
																				, user.cp_id);
									rowCount = DatabaseController.SqlExecuteNonQueryTran(con, tran, sqlStr);
									if (rowCount <= -1)
									{
										throw new ApplicationException("SqlExecuteNonQueryTran() Error!");
									}
									// コミット
									tran.Commit();
									LogOut.Out(string.Format("顧客情報削除 CouplerID：{0} 顧客：{1} {2}", user.cp_id, user.customer_id, user.customer_nm));
								}
							}
							catch (Exception ex2)
							{
								LogOut.Out(string.Format("顧客情報削除エラー CouplerID：{0} 顧客：{1} {2}({3})", user.cp_id, user.customer_id, user.customer_nm, ex2.Message));
							}
						}
						else
						{
							// 新規追加または更新
							try
							{
								sqlStr = string.Format(@"SELECT [cp_id] FROM {0} WHERE [cp_id] = '{1}'"
																			, string.Format("{0}.[dbo].{1}", databaseName, CouplerDatabaseDefine.TableName[CouplerDatabaseDefine.TableType.PRODUCTUSER])
																			, user.cp_id);
								DataTable table = DatabaseController.SqlExcuteDataAdapter(con, sqlStr);

								string cp_id = string.Empty;
								if (null != table && 0 < table.Rows.Count)
								{
									cp_id = table.Rows[0]["cp_id"].ToString().Trim();
								}
								if (user.cp_id == cp_id)
								{
									// 更新
									try
									{
										// トランザクション開始
										using (SqlTransaction tran = con.BeginTransaction())
										{
											sqlStr = string.Format(@"UPDATE {0} SET user_type = @1, trial_flg = @2, end_flg = @3, customer_id = @4, customer_nm = @5, email1 = @6, email2 = @7"
															+ ", login_start_date = @8, login_end_date = @9, default_paswd = @10, license_count = @11, update_date = @12, update_user = @13"
															+ " WHERE cp_id = '{1}'"
															, string.Format("{0}.[dbo].{1}", databaseName, CouplerDatabaseDefine.TableName[CouplerDatabaseDefine.TableType.PRODUCTUSER])
															, user.cp_id);

											SqlParameter[] param = {
																new SqlParameter("@1", user.user_type),
																new SqlParameter("@2", user.trial_flg),
																new SqlParameter("@3", user.end_flg),
																new SqlParameter("@4", user.customer_id),
																new SqlParameter("@5", user.customer_nm),
																new SqlParameter("@6", user.email1),
																new SqlParameter("@7", user.email2),
																new SqlParameter("@8", user.login_start_date),
																new SqlParameter("@9", user.login_end_date),
																new SqlParameter("@10", user.default_paswd),
																new SqlParameter("@11", user.license_count),
																new SqlParameter("@12", DateTime.Now),
																new SqlParameter("@13", updateUser)
															};
											// 実行
											rowCount = DatabaseController.SqlExecuteNonQueryTran(con, tran, sqlStr, param);
											if (rowCount <= -1)
											{
												throw new ApplicationException("SqlExecuteNonQueryTran");
											}
											// コミット
											tran.Commit();
											LogOut.Out(string.Format("顧客情報更新 CouplerID：{0} 顧客：{1} {2}", user.cp_id, user.customer_id, user.customer_nm));
										}
									}
									catch (Exception ex3)
									{
										LogOut.Out(string.Format("顧客情報更新エラー CouplerID：{0} 顧客：{1} {2}({3})", user.cp_id, user.customer_id, user.customer_nm, ex3.Message));
									}
								}
								else
								{
									// 新規追加
									try
									{
										// トランザクション開始
										using (SqlTransaction tran = con.BeginTransaction())
										{
											T_COUPLER_PRODUCTUSER pu = user.SetInsertIntoCouplerProductuser(updateUser);

											sqlStr = string.Format(@"INSERT INTO {0} VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20)"
															, string.Format("{0}.[dbo].{1}", databaseName, CouplerDatabaseDefine.TableName[CouplerDatabaseDefine.TableType.PRODUCTUSER]));

											// 実行
											rowCount = DatabaseController.SqlExecuteNonQueryTran(con, tran, sqlStr, pu.GetInsertIntoParameters());
											if (rowCount <= -1)
											{
												throw new ApplicationException("SqlExecuteNonQueryTran() Error!");
											}
											// コミット
											tran.Commit();
											LogOut.Out(string.Format("顧客情報追加 CouplerID：{0} 顧客：{1} {2}", user.cp_id, user.customer_id, user.customer_nm));
										}
									}
									catch (Exception ex3)
									{
										LogOut.Out(string.Format("顧客情報追加エラー CouplerID：{0} 顧客：{1} {2}({3})", user.cp_id, user.customer_id, user.customer_nm, ex3.Message));
									}
								}
							}
							catch (Exception ex2)
							{
								LogOut.Out(string.Format("顧客情報更新エラー CouplerID：{0} 顧客：{1} {2}({3})", user.cp_id, user.customer_id, user.customer_nm, ex2.Message));
							}
						}
					}
				}
				catch (Exception ex1)
				{
					LogOut.Out(string.Format("顧客情報更新エラー({0})", ex1.Message));
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
