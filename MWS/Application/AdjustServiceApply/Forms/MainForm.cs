//
// MainForm.cs
//
// サービス申込情報更新処理 メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using AdjustServiceApply.Log;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Charlie.View;
using CommonLib.Common;
using CommonLib.DB.SqlServer.AdjustServiceApply;
using CommonLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

namespace AdjustServiceApply.Forms
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Load Form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			this.Text = string.Format("{0} Ver{1}", Program.ProcName, Program.VersionStr);
		}

		/// <summary>
		/// 顧客管理基本情報（T_CUSTOMER_FOUNDATIONS）の登録
		/// </summary>
		/// <param name="slip">伝票データ</param>
		/// <param name="saleType">販売種別</param>
		/// <returns>影響行数</returns>
		private int InsertIntoT_CUSTOMER_FOUNDATIONS(WW伝票参照ビュー slip, char saleType)
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
			cf.CREATE_PERSON = Program.ProcName;
			cf.UPDATE_DATE = DateTime.Now;
			cf.UPDATE_PERSON = Program.ProcName;
			if (0 < slip.申込種別)
			{
				cf.APPLY_TYPE = slip.申込種別.ToString()[0];
			}
			return CharlieDatabaseAccess.InsertInto_T_CUSTOMER_FOUNDATIONS(cf, Program.gSettings.ConnectCharlie.ConnectionString);
		}

		/// <summary>
		/// 顧客管理利用情報（T_CUSSTOMER_USE_INFOMATION）の登録
		/// </summary>
		/// <param name="slip">伝票データ</param>
		/// <param name="codeMaster">MWSコードマスタ</param>
		/// <param name="startDate">利用開始日</param>
		/// <param name="endDate">課金終了日</param>
		/// <returns>影響行数</returns>
		private int InsertIntoT_CUSSTOMER_USE_INFOMATION(WW伝票参照ビュー slip, M_CODE_EX codeMaster, DateTime? startDate, DateTime? endDate)
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
			cui.CREATE_PERSON = Program.ProcName;
			cui.UPDATE_DATE = DateTime.Now;
			cui.UPDATE_PERSON = Program.ProcName;
			//cui.PERIOD_END_DATE = NULL
			//cui.RENEWAL_FLG = 
			return CharlieDatabaseAccess.InsertInto_T_CUSSTOMER_USE_INFOMATION(cui, Program.gSettings.ConnectCharlie.ConnectionString);
		}

		/// <summary>
		/// 顧客利用情報作成（旧Charlie自動作成）処理
		/// (1)顧客マスタ参照ビューを参照し顧客管理基本・顧客管理利用情報に、顧客が存在しない場合は削除フラグを1にする
		/// (2)WW伝票参照ビューを元に顧客管理基本の登録・更新・顧客管理利用情報の削除/登録を行う。
		///    	(※顧客マスタ参照ビューに存在しない場合は、処理をスキップする）
		/// </summary>
		/// <param name="alartCount">警告ログ出力回数</param>
		/// <param name="errorCount">エラーログ出力回数</param>
		private void Auto_Create_Data(out int alartCount, out int errorCount)
		{
			alartCount = 0;     // 警告ログ出力回数
			errorCount = 0;     // エラーログ出力回数

			LogOut.Out("Auto_Create_Data() Start");
			try
			{
				// WW伝票参照ビュー抽出から受注承認日が締日の範囲内で、数量>0の、伝票番号が最小の伝票データの取得
				LogOut.Out("WW伝票抽出処理開始");
				List<int> customerIDList = new List<int>();
				List<WW伝票参照ビュー> slipList = AdjustServiceApplyAccess.GetWonderWebSlip(Program.gSettings.ConnectCharlie.ConnectionString);
				if (null != slipList && 0 < slipList.Count)
				{
					foreach (WW伝票参照ビュー slip in slipList)
					{
						try
						{
							string whereStr = string.Format("CUSTOMER_ID = {0}", slip.ユーザー顧客ID);
							List<T_PRODUCT_CONTROL> pcList = CharlieDatabaseAccess.Select_T_PRODUCT_CONTROL(whereStr, "", Program.gSettings.ConnectCharlie.ConnectionString);
							if (null != pcList && 0 < pcList.Count)
							{
								// ログ「WW伝票商品ID【XXXX】MWSID発行済み」
								LogOut.Out(string.Format("WW伝票商品ID【{0}】MWSID発行済み", slip.商品コード));

								whereStr = string.Format("GOODS_ID = '{0}'", slip.商品コード);
								List<M_CODE_EX> codeList = CharlieDatabaseAccess.Select_M_CODE_EX(whereStr, string.Empty, Program.gSettings.ConnectCharlie.ConnectionString);
								if (null != codeList && 0 < codeList.Count)
								{
									if ("1" == codeList[0].SET_SALE)
									{
										// 月額課金用サービス
										whereStr = string.Format("CUSTOMER_ID = {0}", slip.ユーザー顧客ID);
										List<V_CUSTOMER> customerList = CharlieDatabaseAccess.Select_V_CUSTOMER(whereStr, "", Program.gSettings.ConnectCharlie.ConnectionString);
										if (null != customerList && 0 < customerList.Count)
										{
											// 顧客マスタ参照ビューの登録カード回収日がnullの場合、ライセンス発行可能フラグに=0、その他はライセンス発行可能フラグ=1
											// ログ「ライセンス発行可能フラグ=XXXX」※登録カード回収日の有無
											LogOut.Out(string.Format("ライセンス発行可能フラグ={0}", (0 < customerList[0].RECOVERY_DAY.Length) ? "1" : "0"));

											// ログ「WW伝票担当者ID=XXXX」
											LogOut.Out(string.Format("WW伝票担当者ID={0}", slip.担当者ID));

											string specialistMsgLog = string.Empty;
											whereStr = string.Format("[社員番号] = '{0}' AND [営業区分] = 1", slip.担当者ID);
											List<社員マスタ参照ビュー> employeeList = CharlieDatabaseAccess.Select_社員マスタ参照ビュー(whereStr, "", Program.gSettings.ConnectCharlie.ConnectionString);
											if (null == employeeList)
											{
												// 警告「※社員マスタ参照ビューに営業担当者ID(XXXX)が存在しません。」
												specialistMsgLog = string.Format("警告「社員マスタ参照ビューに営業担当者ID({0})が存在しません。」", slip.担当者ID);
												LogOut.Out(specialistMsgLog);
												alartCount++;
											}
											char saleType = ' ';
											string customerMsgLog = string.Empty;
											if (slip.販売先顧客ID == slip.ユーザー顧客ID)
											{
												// ログ「WW伝票の販売先顧客IDとユーザー顧客IDが同じです。(XXXX)」
												customerMsgLog = string.Format("WW伝票の販売先顧客IDとユーザー顧客IDが同じです。({0})", slip.販売先顧客ID);
												LogOut.Out(customerMsgLog);
												saleType = '1';   // 販売種別:1（直接）
											}
											else
											{
												saleType = '2';   // 販売種別:2（販売店）
												List<int> storeCodeList = AdjustServiceApplyAccess.GetStoreCode(slip.販売先顧客ID, Program.gSettings.ConnectCharlie.ConnectionString);
												if (null == storeCodeList)
												{
													// 警告「※WW伝票の販売先顧客ID(XXXX)が販売店情報に存在しませんでした。」
													customerMsgLog = string.Format("※WW伝票の販売先顧客ID({0})が販売店情報に存在しませんでした。", slip.販売先顧客ID);
													LogOut.Out(string.Format("警告「WW伝票の販売先顧客ID({0})が販売店情報に存在しませんでした。」", slip.販売先顧客ID));
													alartCount++;
												}
											}
											// ログ「WW伝票申込種別=XXXX」※WW伝票から申込種別を取得
											LogOut.Out(string.Format("WW伝票申込種別={0}", slip.申込種別));

											// WW伝票のユーザ顧客IDが顧客管理基本に存在するかチェック
											whereStr = string.Format("[CUSTOMER_ID] = {0}", slip.ユーザー顧客ID);
											List<T_CUSTOMER_FOUNDATIONS> cfList = CharlieDatabaseAccess.Select_T_CUSTOMER_FOUNDATIONS(whereStr, "", Program.gSettings.ConnectCharlie.ConnectionString);
											if (null == cfList)
											{
												// ★顧客管理基本情報に顧客IDが存在しないため登録する
												//this.InsertIntoT_CUSTOMER_FOUNDATIONS(slip, saleType);

												customerIDList.Add(slip.ユーザー顧客ID);

												// 	ログ「伝票No：XXXX、商品コード：XXXXのデータを顧客管理基本に登録しました。（顧客ID：XXXX 顧客名：XXXX 営業担当者ID：XXXX 販売店（使用料請求先コード / 販売拠点コード）：XXXX XXXX  XXXX」
												LogOut.Out(string.Format("伝票No：{0}、商品コード：{1}のデータを顧客管理基本に登録しました。（顧客ID：{2} 顧客名：{3} 営業担当者ID：{4} 販売店（使用料請求先コード / 販売拠点コード）：{5} {6}  {7}"
																						, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID, customerList[0].CUSTOMER_NAME, slip.担当者ID, slip.販売先顧客ID, customerMsgLog, specialistMsgLog));
											}
											// 顧客管理利用情報の顧客・サービス存在チェック
											whereStr = string.Format("[CUSTOMER_ID] = {0} AND [SERVICE_TYPE_ID] = {1} AND [SERVICE_ID] = {2}", slip.ユーザー顧客ID, codeList[0].SERVICE_TYPE_ID, codeList[0].SERVICE_ID);
											List<T_CUSSTOMER_USE_INFOMATION> cuiList = CharlieDatabaseAccess.Select_T_CUSSTOMER_USE_INFOMATION(whereStr, "", Program.gSettings.ConnectCharlie.ConnectionString);
											if (null == cuiList)
											{
												// 申込データに対象の顧客・サービスが存在するかチェック
												whereStr = string.Format("CUSTOMER_ID = {0} AND SERVICE_TYPE_ID = {1} AND SERVICE_ID = {2} AND DELETE_FLG = '0' AND APPLICATION_CANCELLATION_FLG = '0' AND PCA_FINISHING_FLG = '0' AND CONVERT(int, CONVERT(nvarchar, APPLICATION_DATE, 112))  BETWEEN {3} AND {4}"
																						, slip.ユーザー顧客ID, codeList[0].SERVICE_TYPE_ID, codeList[0].SERVICE_ID
																						, DateTime.Today.BeginOfLastMonth().ToDate().ToIntYMD() // 先月初日
																						, DateTime.Today.ToDate().ToIntYMD());  // 当日
												List<T_APPLICATION_DATA> aplList = CharlieDatabaseAccess.Select_T_APPLICATION_DATA(whereStr, "", Program.gSettings.ConnectCharlie.ConnectionString);
												if (null == aplList)
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
													// ★顧客管理利用情報に顧客・サービスを登録する
													//InsertIntoT_CUSSTOMER_USE_INFOMATION(slip, codeList[0], startDate, endDate);

													if (startDate.HasValue)
													{
														// ログ「伝票No：XXXX、商品コード：XXXXのデータを顧客管理利用情報に登録しました。（顧客ID：XXXX 顧客名：XXXX サービス種別ID：XXXX サービス種別名：XXXX サービスID：XXXX サービス名：XXXX）」
														LogOut.Out(string.Format("伝票No：{0}、商品コード：{1}のデータを顧客管理利用情報に登録しました。（顧客ID：{2} 顧客名：{3} サービス種別ID：{4} サービス種別名：{5} サービスID：{6} サービス名：{7}）"
																								, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID, customerList[0].CUSTOMER_NAME, codeList[0].SERVICE_TYPE_ID, codeList[0].SERVICE_TYPE_NAME, codeList[0].SERVICE_ID, codeList[0].SERVICE_NAME));
													}
													else
													{
														// 警告「伝票No：XXXX、商品コード：XXXXのデータを顧客管理利用情報に登録しました。（顧客ID：XXXX 顧客名：XXXX サービス種別ID：XXXX サービス種別名：XXXX サービスID：XXXX サービス名：XXXX)
														//        ※警告：登録されましたが利用期間が設定されていません。必ず利用期間を設定して下さい。利用期間が設定されるまではCouplerへの同期はされません。」
														LogOut.Out(string.Format("伝票No：{0}、商品コード：{1}のデータを顧客管理利用情報に登録しました。（顧客ID：{2} 顧客名：{3} サービス種別ID：{4} サービス種別名：{5} サービスID：{6} サービス名：{7})"
																						+ "※警告：登録されましたが利用期間が設定されていません。必ず利用期間を設定して下さい。利用期間が設定されるまではCouplerへの同期はされません。"
																						, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID, customerList[0].CUSTOMER_NAME, codeList[0].SERVICE_TYPE_ID, codeList[0].SERVICE_TYPE_NAME, codeList[0].SERVICE_ID, codeList[0].SERVICE_NAME));
														alartCount++;
													}
												}
												else
												{
													// ログ「伝票No：XXXX、商品コード：XXXXのデータは既に申込情報に存在しているため処理をスキップしました。（顧客ID：XXXX 顧客名：XXXX サービス種別ID：XXXX サービス種別名：XXXX サービスID：XXXX サービス名：XXXX）」
													LogOut.Out(string.Format("伝票No：{0}、商品コード：{1}のデータは既に申込情報に存在しているため処理をスキップしました。（顧客ID：{2} 顧客名：{3} サービス種別ID：{4} サービス種別名：{5} サービスID：{6} サービス名：{7}）"
																						, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID, customerList[0].CUSTOMER_NAME, codeList[0].SERVICE_TYPE_ID, codeList[0].SERVICE_TYPE_NAME, codeList[0].SERVICE_ID, codeList[0].SERVICE_NAME));
												}
											}
										}
										else
										{
											// 警告「COUPLER IDをもたないユーザーに「XXXX」のサービス伝票が起票されました。伝票No：XXXX、商品コード：XXXX（顧客ID：XXXX ）」
											LogOut.Out(string.Format("警告「COUPLER IDをもたないユーザーに「{0}」のサービス伝票が起票されました。伝票No：{1}、商品コード：{2}（顧客ID：{3}）」"
																					, codeList[0].SERVICE_ID, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID));
											alartCount++;
										}
									}
									else
									{
										// 処理必要なし  一括販売用 ※PC安心サポートなど
										// 朝バッチでは一括販売用のサービスも顧客利用情報に追加していたので、登録する必要のないサービスが登録されていた
										LogOut.Out(string.Format("伝票No：{0}、商品コード：{1}のデータは一括販売用サービスなので、処理をスキップしました。（顧客ID：{2} サービス種別ID：{3} サービス種別名：{4} サービスID：{5} サービス名：{6}）"
																								, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID, codeList[0].SERVICE_TYPE_ID, codeList[0].SERVICE_TYPE_NAME, codeList[0].SERVICE_ID, codeList[0].SERVICE_NAME));
									}
								}
								else
								{
									// 警告「商品ID【XXXX】がコードマスター(M_CODE)に存在しません。」
									LogOut.Out(string.Format("警告「商品ID【{0}】がコードマスター(M_CODE)に存在しません。」", slip.商品コード));
									alartCount++;
								}
							}
							else
							{
								// 警告「WW伝票参照ビュー：伝票No【XXXX】顧客ID【XXXX】※T_PRODUCT_CONTROLに顧客IDが存在しません。」
								LogOut.Out(string.Format("警告「WW伝票参照ビュー：伝票No【{0}】顧客ID【{1}】※T_PRODUCT_CONTROLに顧客IDが存在しません。」", slip.伝票No, slip.ユーザー顧客ID));
								alartCount++;

								whereStr = string.Format("GOODS_ID = '{0}'", slip.商品コード);
								List<M_CODE> codeList = CharlieDatabaseAccess.Select_M_CODE(whereStr, "", Program.gSettings.ConnectCharlie.ConnectionString);
								if (null != codeList && 0 < codeList.Count)
								{
									whereStr = string.Format("CUSTOMER_ID = {0}", slip.ユーザー顧客ID);
									List<V_CUSTOMER> customerList = CharlieDatabaseAccess.Select_V_CUSTOMER(whereStr, "", Program.gSettings.ConnectCharlie.ConnectionString);
									if (null != customerList && 0 < customerList.Count)
									{
										// 警告「COUPLER IDをもたないユーザーに「XXXX」のサービス伝票が起票されました。伝票No：XXXX、商品コード：XXXX（顧客ID：XXXX 顧客名：XXXX）」
										LogOut.Out(string.Format("警告「COUPLER IDをもたないユーザーに「{0}」のサービス伝票が起票されました。伝票No：{1}、商品コード：{2}（顧客ID：{3} 顧客名：{4}）」"
																			, codeList[0].SERVICE_ID, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID, customerList[0].CUSTOMER_NAME));
										alartCount++;
									}
									else
									{
										// 警告「COUPLER IDをもたないユーザーに「XXXX」のサービス伝票が起票されました。伝票No：XXXX、商品コード：XXXX（顧客ID：XXXX ）」
										LogOut.Out(string.Format("警告「COUPLER IDをもたないユーザーに「{0}」のサービス伝票が起票されました。伝票No：{1}、商品コード：{2}（顧客ID：{3} ）」"
																			, codeList[0].SERVICE_ID, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID));
										alartCount++;
									}
								}
								else
								{
									// 警告「商品ID【XXXX】がコードマスター(M_CODE)に存在しません。」
									LogOut.Out(string.Format("警告「商品ID【{0}】がコードマスター(M_CODE)に存在しません。」", slip.商品コード));
									alartCount++;
								}
							}
						}
						catch (Exception ex)
						{
							LogOut.Out(string.Format("例外エラー「{0} 伝票No：{1}、商品コード：{2} 顧客ID：{3}」 ", ex.Message, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID));
						}
					}
					LogOut.Out(string.Format("WW伝票抽出処理終了【取得件数：{0} 件】", slipList.Count));
				}
				else
				{
					LogOut.Out("WW伝票抽出処理終了【取得件数：0 件】");
				}
			}
			catch (Exception ex)
			{
				LogOut.Out(string.Format("エラー「Auto_Create_Data()：{0}」", ex.Message));
				return;
			}
			LogOut.Out("Auto_Create_Data() End");
		}

		/// <summary>
		/// サービス申込情報の更新
		/// </summary>
		/// <returns></returns>
		private void CusDataUpdate_Main()
		{
			try
			{
				LogOut.Out("CHARLIEDBから基本機能パック取得開始 【PCA商品区分 = 200】");
				M_CODE mwsCodeMaster = AdjustServiceApplyAccess.GetKihonPack(Program.gSettings.ConnectCharlie.ConnectionString);
				if (null != mwsCodeMaster)
				{
					LogOut.Out(string.Format("CHARLIEDBから基本機能パック取得終了 【商品ID = {0} / サービス種別ID = {1} / サービスID = {2}】", mwsCodeMaster.GOODS_ID, mwsCodeMaster.SERVICE_TYPE_ID, mwsCodeMaster.SERVICE_ID));
				}
				else
				{
					LogOut.Out("CHARLIEDBから基本機能パック取得終了 【商品ID = / サービス種別ID = / サービスID = 】");
				}
				// 前回同期日時の取得
				T_FILE_CREATEDATE synchroTime = AdjustServiceApplyAccess.GetLastSynchroTime(Program.gSettings.ConnectCharlie.ConnectionString);
				LogOut.Out(string.Format("前回同期日時：{0}", synchroTime.FILE_CREATEDATE.ToString()));

				// サービス申込情報の利用申込のシステム反映フラグを設定
				List<T_MWS_APPLY> applyUseList = AdjustServiceApplyAccess.GetMwsApplyUserService(Program.gSettings.ConnectCharlie.ConnectionString);
				if (null != applyUseList && 0 < applyUseList.Count)
				{
					List<T_CUSSTOMER_USE_INFOMATION> cuiUseList = AdjustServiceApplyAccess.GetCustomerUseInformationUseService(synchroTime.FILE_CREATEDATE, Program.gSettings.ConnectCharlie.ConnectionString);
					if (null != cuiUseList && 0 < cuiUseList.Count)
					{
						foreach (T_CUSSTOMER_USE_INFOMATION cui in cuiUseList)
						{
							List<T_MWS_APPLY> useList = applyUseList.FindAll(p => p.customer_id == cui.CUSTOMER_ID && p.service_id == cui.SERVICE_ID);
							if (0 < useList.Count)
							{
								foreach (T_MWS_APPLY apply in useList)
								{
									// システム反映フラグを設定
									apply.system_flg = true;
									LogOut.Out(string.Format("利用申込情報システム反映フラグ設定（顧客ID：{0} サービスID：{1} 申込日時：{2}）", apply.customer_id, apply.service_id, apply.apply_date.Value.ToShortDateString()));
								}
								// サービス申込情報の更新
								//AdjustServiceApplyAccess.UpdateSet_T_MWS_APPLY(useList, Program.ProcName, Program.gSettings.ConnectCharlie.ConnectionString);
							}
						}
					}
				}
				// サービス申込情報の解約申込のシステム反映フラグを設定
				List<T_MWS_APPLY> applyCancelList = AdjustServiceApplyAccess.GetMwsApplyCancelService(Program.gSettings.ConnectCharlie.ConnectionString);
				if (null != applyCancelList && 0 < applyCancelList.Count)
				{
					List<T_CUSSTOMER_USE_INFOMATION> cuiCancelList = AdjustServiceApplyAccess.GetCustomerUseInformationCancelService(synchroTime.FILE_CREATEDATE, Program.gSettings.ConnectCharlie.ConnectionString);
					if (null != cuiCancelList && 0 < cuiCancelList.Count)
					{
						foreach (T_CUSSTOMER_USE_INFOMATION cui in cuiCancelList)
						{
							List<T_MWS_APPLY> cancelList = applyCancelList.FindAll(p => p.customer_id == cui.CUSTOMER_ID && p.service_id == cui.SERVICE_ID);
							if (0 < cancelList.Count)
							{
								foreach (T_MWS_APPLY apply in cancelList)
								{
									// システム反映フラグを設定
									apply.system_flg = true;
									LogOut.Out(string.Format("解約申込情報システム反映フラグ設定（顧客ID：{0} サービスID：{1} 申込日時：{2}）", apply.customer_id, apply.service_id, apply.apply_date.Value.ToShortDateString()));
								}
								// サービス申込情報の更新
								//AdjustServiceApplyAccess.UpdateSet_T_MWS_APPLY(useList, Program.ProcName, Program.gSettings.ConnectCharlie.ConnectionString);
							}
						}
					}
				}
				// 前回同期日時を追加
				AdjustServiceApplyAccess.SetLastSynchroTime(Program.ProcName, Program.gSettings.ConnectCharlie.ConnectionString);
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			// ログファイル名の設定
			LogOut.SetLogFileName(Directory.GetCurrentDirectory());

			List<string> mailLogList = new List<string>();

			string log = "サービス申込情報更新処理 開始";
			LogOut.Out(log);
			mailLogList.Add(log);

			// 顧客利用情報作成
			int alartCount;     // 警告ログ出力回数
			int errorCount;     // エラーログ出力回数
			Auto_Create_Data(out alartCount, out errorCount);

			try
			{
				LogOut.Out("CusDataUpdate_Main() Start");
				CusDataUpdate_Main();
				LogOut.Out("CusDataUpdate_Main() End");

				LogOut.Out(string.Format("警告 {0} 件", alartCount));
				if (0 < errorCount)
				{
					LogOut.Out(string.Format("エラー {0} 件", errorCount));
				}
			}
			catch (Exception ex)
			{
				LogOut.Out(string.Format("CusDataUpdate_Main エラー：{0}", ex.Message));
				return;
			}

			LogOut.Out("サービス申込情報更新処理 終了");
		}
	}
}
