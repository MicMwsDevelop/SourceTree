//
// MainForm.cs
//
// サービス申込情報更新処理 メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using AdjustServiceApply.Settings;
using CommonLib.DB.SqlServer.AdjustServiceApply;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLib.BaseFactory.Charlie.View;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.BaseFactory.Charlie.Table;
using System.Collections;
using CommonLib.Common;
using System.Diagnostics;

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
		/// 顧客利用情報作成（旧Charlie自動作成）処理
		/// (1)顧客マスタ参照ビューを参照し顧客管理基本・顧客管理利用情報に、顧客が存在しない場合は削除フラグを1にする
		/// (2)WW伝票参照ビューを元に顧客管理基本の登録・更新・顧客管理利用情報の削除/登録を行う。
		///    	(※顧客マスタ参照ビューに存在しない場合は、処理をスキップする）
		/// </summary>
		private void Auto_Create_Data()
		{
			Trace.WriteLine("Auto_Create_Data() Start");
			try
			{
				List<int> customerIDList = new List<int>();
				List<WW伝票参照ビュー> slipList = AdjustServiceApplyAccess.GetWonderWebSlip(Program.gSettings.ConnectCharlie.ConnectionString);
				if (null != slipList && 0 < slipList.Count)
				{
					foreach (WW伝票参照ビュー slip in slipList)
					{
						string whereStr = string.Format("CUSTOMER_ID = {0}", slip.ユーザー顧客ID);
						List<T_PRODUCT_CONTROL> pcList = CharlieDatabaseAccess.Select_T_PRODUCT_CONTROL(whereStr, "", Program.gSettings.ConnectCharlie.ConnectionString);
						if (null != pcList && 0 < pcList.Count)
						{
							// ログ「WW伝票商品ID【XXXX】MWSID発行済み」
							Trace.WriteLine(string.Format("WW伝票商品ID【{0}】MWSID発行済み", slip.商品コード));

							whereStr = string.Format("GOODS_ID = '{0}'", slip.商品コード);
							List<M_CODE_EX> codeList = CharlieDatabaseAccess.Select_M_CODE_EX(whereStr, string.Empty, Program.gSettings.ConnectCharlie.ConnectionString);
							if (null != codeList && 0 < codeList.Count)
							{
								whereStr = string.Format("CUSTOMER_ID = {0}", slip.ユーザー顧客ID);
								List<V_CUSTOMER> customerList = CharlieDatabaseAccess.Select_V_CUSTOMER(whereStr, "", Program.gSettings.ConnectCharlie.ConnectionString);
								if (null != customerList && 0 < customerList.Count)
								{
									// 顧客マスタ参照ビューの登録カード回収日がnullの場合、ライセンス発行可能フラグに=0、その他はライセンス発行可能フラグ=1
									// ログ「ライセンス発行可能フラグ=XXXX」※登録カード回収日の有無
									Trace.WriteLine(string.Format("ライセンス発行可能フラグ={0}", (0 < customerList[0].RECOVERY_DAY.Length) ? "1" : "0"));

									// ログ「WW伝票担当者ID=XXXX」
									Trace.WriteLine(string.Format("WW伝票担当者ID={0}", slip.担当者ID));

									string specialistMsgLog = string.Empty;
									whereStr = string.Format("[社員番号] = '{0}' AND [営業区分] = 1", slip.担当者ID);
									List<社員マスタ参照ビュー> employeeList = CharlieDatabaseAccess.Select_社員マスタ参照ビュー(whereStr, "", Program.gSettings.ConnectCharlie.ConnectionString);
									if (null == employeeList)
									{
										// 警告「※社員マスタ参照ビューに営業担当者ID(XXXX)が存在しません。」
										specialistMsgLog = string.Format("※社員マスタ参照ビューに営業担当者ID({0}が存在しません。", slip.担当者ID);
										Trace.WriteLine(specialistMsgLog);
									}
									char saleType = ' ';
									string customerMsgLog = string.Empty;
									if (slip.販売先顧客ID == slip.ユーザー顧客ID)
									{
										// ログ「WW伝票の販売先顧客IDとユーザー顧客IDが同じです。(XXXX)」
										customerMsgLog = string.Format("WW伝票の販売先顧客IDとユーザー顧客IDが同じです。({0})", slip.販売先顧客ID);
										Trace.WriteLine(customerMsgLog);
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
											Trace.WriteLine(customerMsgLog);
										}
									}
									// ログ「WW伝票申込種別=XXXX」※WW伝票から申込種別を取得
									Trace.WriteLine(string.Format("WW伝票申込種別={0}", slip.申込種別));

									// WW伝票のユーザ顧客IDが顧客管理基本に存在するかチェック
									whereStr = string.Format("[CUSTOMER_ID] = {0}", slip.ユーザー顧客ID);
									List<T_CUSTOMER_FOUNDATIONS> cfList = CharlieDatabaseAccess.Select_T_CUSTOMER_FOUNDATIONS(whereStr, "", Program.gSettings.ConnectCharlie.ConnectionString);
									if (null == cfList)
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
										//CharlieDatabaseAccess.InsertInto_T_CUSTOMER_FOUNDATIONS(cf, Settings.ConnectCharlie.ConnectionString);

										customerIDList.Add(slip.ユーザー顧客ID);

										// 	ログ「伝票No：XXXX、商品コード：XXXXのデータを顧客管理基本に登録しました。（顧客ID：XXXX 顧客名：XXXX 営業担当者ID：XXXX 販売店（使用料請求先コード / 販売拠点コード）：XXXX XXXX  XXXX」
										Trace.WriteLine(string.Format("伝票No：{0}、商品コード：{1}のデータを顧客管理基本に登録しました。（顧客ID：{2} 顧客名：{3} 営業担当者ID：{4} 販売店（使用料請求先コード / 販売拠点コード）：{5} {6}  {7}"
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
											DateTime? startDate = DateTime.Today;   // システム日付
											DateTime? endDate = DateTime.Today.EndOfNextMonth();       // 多分、翌月末日だと思う？
											if (-1 != customerIDList.FindIndex(p => p == slip.ユーザー顧客ID))
											{
												// 既に顧客は登録されている場合は利用期間はNULLをセット←処理内容不明
												startDate = null;
												endDate = null;
											}
											// ★T_CUSSTOMER_USE_INFOMATIONにサービスを登録する
											T_CUSSTOMER_USE_INFOMATION cui = new T_CUSSTOMER_USE_INFOMATION();
											cui.CUSTOMER_ID = slip.ユーザー顧客ID;
											cui.SERVICE_TYPE_ID = codeList[0].SERVICE_TYPE_ID;
											cui.SERVICE_ID = codeList[0].SERVICE_ID;
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

											//CharlieDatabaseAccess.InsertInto_T_CUSSTOMER_USE_INFOMATION(cui, Settings.ConnectCharlie.ConnectionString);

											if (startDate.HasValue)
											{
												// ログ「伝票No：XXXX、商品コード：XXXXのデータを顧客管理利用情報に登録しました。（顧客ID：XXXX 顧客名：XXXX サービス種別ID：XXXX サービス種別名：XXXX サービスID：XXXX サービス名：XXXX）」
												Trace.WriteLine(string.Format("伝票No：{0}、商品コード：{1}のデータを顧客管理利用情報に登録しました。（顧客ID：{2} 顧客名：{3} サービス種別ID：{4} サービス種別名：{5} サービスID：{6} サービス名：{7}）"
																						, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID, customerList[0].CUSTOMER_NAME, codeList[0].SERVICE_TYPE_ID, codeList[0].SERVICE_TYPE_NAME, codeList[0].SERVICE_ID, codeList[0].SERVICE_NAME));
											}
											else
											{
												// 警告「伝票No：XXXX、商品コード：XXXXのデータを顧客管理利用情報に登録しました。（顧客ID：XXXX 顧客名：XXXX サービス種別ID：XXXX サービス種別名：XXXX サービスID：XXXX サービス名：XXXX)
												//        ※警告：登録されましたが利用期間が設定されていません。必ず利用期間を設定して下さい。利用期間が設定されるまではCouplerへの同期はされません。」
												Trace.WriteLine(string.Format("伝票No：{0}、商品コード：{1}のデータを顧客管理利用情報に登録しました。（顧客ID：{2} 顧客名：{3} サービス種別ID：{4} サービス種別名：{5} サービスID：{6} サービス名：{7})"
																				+ "※警告：登録されましたが利用期間が設定されていません。必ず利用期間を設定して下さい。利用期間が設定されるまではCouplerへの同期はされません。"
																				, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID, customerList[0].CUSTOMER_NAME, codeList[0].SERVICE_TYPE_ID, codeList[0].SERVICE_TYPE_NAME, codeList[0].SERVICE_ID, codeList[0].SERVICE_NAME));
											}
										}
										else
										{
											// ログ「伝票No：XXXX、商品コード：XXXXのデータは既に申込情報に存在しているため処理をスキップしました。（顧客ID：XXXX 顧客名：XXXX サービス種別ID：XXXX サービス種別名：XXXX サービスID：XXXX サービス名：XXXX）」
											Trace.WriteLine(string.Format("伝票No：{0}、商品コード：{1}のデータは既に申込情報に存在しているため処理をスキップしました。（顧客ID：{2} 顧客名：{3} サービス種別ID：{4} サービス種別名：{5} サービスID：{6} サービス名：{7}）"
																				, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID, customerList[0].CUSTOMER_NAME, codeList[0].SERVICE_TYPE_ID, codeList[0].SERVICE_TYPE_NAME, codeList[0].SERVICE_ID, codeList[0].SERVICE_NAME));
										}
									}
								}
								else
								{
									// 警告「COUPLER IDをもたないユーザーに「XXXX」のサービス伝票が起票されました。伝票No：XXXX、商品コード：XXXX（顧客ID：XXXX ）」
									Trace.WriteLine(string.Format("COUPLER IDをもたないユーザーに「{0}」のサービス伝票が起票されました。伝票No：{1}、商品コード：{2}（顧客ID：{3} ）"
																			, codeList[0].SERVICE_ID, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID));
								}
							}
							else
							{
								// 警告「商品ID【XXXX】がコードマスター(M_CODE)に存在しません。」
								Trace.WriteLine(string.Format("商品ID【{0}】がコードマスター(M_CODE)に存在しません。", slip.商品コード));
							}
						}
						else
						{
							// 警告「WW伝票参照ビュー：伝票No【XXXX】顧客ID【XXXX】※T_PRODUCT_CONTROLに顧客IDが存在しません。」
							Trace.WriteLine(string.Format("WW伝票参照ビュー：伝票No【{0}】顧客ID【{1}】※T_PRODUCT_CONTROLに顧客IDが存在しません。", slip.伝票No, slip.ユーザー顧客ID));

							whereStr = string.Format("GOODS_ID = '{0}'", slip.商品コード);
							List<M_CODE> codeList = CharlieDatabaseAccess.Select_M_CODE(whereStr, "", Program.gSettings.ConnectCharlie.ConnectionString);
							if (null != codeList && 0 < codeList.Count)
							{
								whereStr = string.Format("CUSTOMER_ID = {0}", slip.ユーザー顧客ID);
								List<V_CUSTOMER> customerList = CharlieDatabaseAccess.Select_V_CUSTOMER(whereStr, "", Program.gSettings.ConnectCharlie.ConnectionString);
								if (null != customerList && 0 < customerList.Count)
								{
									// 警告「COUPLER IDをもたないユーザーに「XXXX」のサービス伝票が起票されました。伝票No：XXXX、商品コード：XXXX（顧客ID：XXXX 顧客名：XXXX）」
									Trace.WriteLine(string.Format("COUPLER IDをもたないユーザーに「{0}」のサービス伝票が起票されました。伝票No：{1}、商品コード：{2}（顧客ID：{3} 顧客名：{4}）"
																		, codeList[0].SERVICE_ID, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID, customerList[0].CUSTOMER_NAME));
								}
								else
								{
									// 警告「COUPLER IDをもたないユーザーに「XXXX」のサービス伝票が起票されました。伝票No：XXXX、商品コード：XXXX（顧客ID：XXXX ）」
									Trace.WriteLine(string.Format("COUPLER IDをもたないユーザーに「{0}」のサービス伝票が起票されました。伝票No：{1}、商品コード：{2}（顧客ID：{3} ）"
																		, codeList[0].SERVICE_ID, slip.伝票No, slip.商品コード, slip.ユーザー顧客ID));
								}
							}
							else
							{
								// 警告「商品ID【XXXX】がコードマスター(M_CODE)に存在しません。」
								Trace.WriteLine(string.Format("商品ID【{0}】がコードマスター(M_CODE)に存在しません。", slip.商品コード));
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine(string.Format("Auto_Create_Data() エラー：{0}", ex.Message));
				return;
			}
			Trace.WriteLine("Auto_Create_Data() End");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private int CusDataUpdate_Main()
		{
			try
			{
				Trace.WriteLine("CHARLIEDBから基本機能パック取得開始 【PCA商品区分 = 200】");
				M_CODE mwsCodeMaster = AdjustServiceApplyAccess.GetKihonPack(Program.gSettings.ConnectCharlie.ConnectionString);
				if (null != mwsCodeMaster)
				{
					Trace.WriteLine(string.Format("CHARLIEDBから基本機能パック取得終了 【商品ID = {0} / サービス種別ID = {1} / サービスID = {2}】", mwsCodeMaster.GOODS_ID, mwsCodeMaster.SERVICE_TYPE_ID, mwsCodeMaster.SERVICE_ID));
				}
				else
				{
					Trace.WriteLine("CHARLIEDBから基本機能パック取得終了 【商品ID = / サービス種別ID = / サービスID = 】");
				}



			}
			catch (Exception ex)
			{
				Trace.WriteLine(string.Format("Auto_Create_Data() エラー：{0}", ex.Message));
				return 1;
			}
			return 0;
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			Auto_Create_Data();

			Trace.WriteLine("CusDataUpdate_Main() Start");
			int ret = CusDataUpdate_Main();
			Trace.WriteLine(string.Format("CusDataUpdate_Mainの戻り値：{0}", ret));
		}
	}
}
