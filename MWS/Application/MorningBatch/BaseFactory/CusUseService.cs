using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.Common;
using CommonLib.DB;
using CommonLib.DB.SqlServer;
using CommonLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace MorningBatch.BaseFactory
{
	public class CusUseService
	{
		private Environment Variables { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CusUseService()
		{
			Variables = new Environment();
		}

		/// <summary>
		/// WW伝票参照ビュー抽出（顧客が持つ伝票番号の最小のみ抽出）
		///	  ■PCA売上汎用データ作成日がNULL以外の場合
		///	  WW伝票参照ビュー.受注承認日 < PCA売上汎用データ作成日
		///	  ■PCA売上汎用データ作成日がNULLの場合
		///	  WW伝票参照ビュー.受注承認日 IS NOT NULL
		/// </summary>
		/// <returns>V_CHECK</returns>
		private List<V_CHECK> Sel_V_CHECK()
		{
			string sql = "SELECT"
								+ " Z.伝票No"
								+ ",Z.販売先顧客ID"
								+ ",Z.ユーザー顧客ID"
								+ ",Z.担当者ID"
								+ ",Z.担当者名 "
								+ ",Z.担当支店ID"
								+ ",Z.担当支店名"
								+ ",Z.受注年月日"
								+ ",Z.受注承認日"
								+ ",Z.売上承認日"
								+ ",Z.商品コード"
								+ ",Z.商品名"
								+ ",Z.商品区分"
								+ ",Z.数量"
								+ ",Z.販売価格"
								+ ",Z.申込種別"
								+ " FROM [charlieDB].[dbo].[WW伝票参照ビュー] as Z"
								+ " INNER HASH JOIN"
								+ " ("
								+ " SELECT *"
								+ " FROM"
								+ " ( SELECT"
								+ " Y.ユーザー顧客ID"
								+ ", Y.商品コード"
								+ ", SUM(Y.数量) AS sumQUANTITY"
								+ ", COUNT(Y.数量) AS CNT"
								+ ", MIN(伝票No) AS minCHECK_NO"
								+ " FROM [charlieDB].[dbo].[WW伝票参照ビュー] as Y"
								+ " WHERE 受注承認日 is not null"
								+ " GROUP BY ユーザー顧客ID, 商品コード"
								+ ") as tblA"
								+ " WHERE sumQUANTITY > 0"
								+ ") X ON X.minCHECK_NO = Z.伝票No AND X.ユーザー顧客ID = Z.ユーザー顧客ID AND X.商品コード = Z.商品コード"
								+ " WHERE Z.受注承認日 is not null"
								+ " ORDER BY 伝票No, ユーザー顧客ID, 商品コード";
			DataTable table = DatabaseAccess.SelectDatabase(sql, Program.gConnectStr);
			return V_CHECK.DataTableToList(table);
		}

		/// <summary>
		/// WW伝票の商品コードがコードマスタに存在するかチェック
		/// </summary>
		/// <param name="goodID">商品コード</param>
		/// <returns>M_CODE_Chk</returns>
		private M_CODE_Chk Sel_M_CODE_Chk(string goodID)
		{
			string sql = string.Format("SELECT GOODS_ID, C.SERVICE_TYPE_ID, C.SERVICE_ID, SERVICE_NAME"
												+ " FROM [charlieDB].[dbo].[M_CODE] as C"
												+ " LEFT JOIN [charlieDB].[dbo].[M_SERVICE] as M on C.SERVICE_ID = M.SERVICE_ID AND C.SERVICE_TYPE_ID = M.SERVICE_TYPE_ID"
												+ " WHERE GOODS_ID = '{0}' AND DELETE_FLG = '0' AND UMU_FLG = '0'", goodID);
			DataTable table = DatabaseAccess.SelectDatabase(sql, Program.gConnectStr);
			return M_CODE_Chk.DataTableToList(table);
		}

		/// <summary>
		/// WW伝票の顧客が顧客マスタ参照ビューに存在するかチェック
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <returns>V_CUSTOMER_Chk</returns>
		private V_CUSTOMER_Chk Sel_V_CUSTOMER(int customerNo)
		{
			string sql = string.Format("SELECT 顧客ID as CUSTOMER_ID, 顧客名1 as CUSTOMER_NAME1, 顧客名2 as CUSTOMER_NAME2, 登録カード回収日 as RECOVERY_DAY"
												+ " FROM [charlieDB].[dbo].[顧客マスタ参照ビュー]"
												+ " WHERE CUSTOMER_ID = {0}", customerNo);
			DataTable table = DatabaseAccess.SelectDatabase(sql, Program.gConnectStr);
			return V_CUSTOMER_Chk.DataTableToList(table);
		}

		/// <summary>
		/// MWSIDの発行・未発行チェック
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <returns>判定</returns>
		private bool Sel_MwsID_Chk(int customerNo)
		{
			string sql = string.Format("SELECT *  FROM [charlieDB].[dbo].[T_PRODUCT_CONTROL] WHERE CUSTOMER_ID = {0}", customerNo);
			DataTable table = DatabaseAccess.SelectDatabase(sql, Program.gConnectStr);
			return (0 < table.Rows.Count);
		}

		/// <summary>
		/// 伝票の担当者が営業かどうか社員マスタ参照ビューをチェック
		/// </summary>
		/// <param name="chargeID">担当者ID</param>
		/// <returns>営業区分</returns>
		private int GetOperatingClassification(string chargeID)
		{
			string sql = string.Format("SELECT 営業区分  FROM [charlieDB].[dbo].[社員マスタ参照ビュー] WHERE 社員番号 = '{0}'", chargeID);
			DataTable table = DatabaseAccess.SelectDatabase(sql, Program.gConnectStr);
			if (0 < table.Rows.Count)
			{
				return DataBaseValue.ConvObjectToInt(table.Rows[0]["営業区分"]);
			}
			return 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="customerNo">販売先顧客ID</param>
		/// <returns>販売店コード</returns>
		private int? Sel_V_STORE_INFORMATION(int customerNo)
		{
			string sql = string.Format("SELECT B.販売店コード"
													+ " FROM [charlieDB].[dbo].[販売店区分参照ビュー] as A"
													+ " INNER JOIN [charlieDB].[dbo].[販売店情報参照ビュー] as B ON A.区分コード = CONVERT(int, B.販売店ランクコード) AND A.ランク is not null"
													+ " WHERE B.販売店コード = {0}", customerNo);
			DataTable table = DatabaseAccess.SelectDatabase(sql, Program.gConnectStr);
			if (0 < table.Rows.Count)
			{
				return DataBaseValue.ConvObjectToInt(table.Rows[0]["B.販売店コード"]);
			}
			return null;
		}

		/// <summary>
		/// WW伝票のユーザ顧客IDが顧客管理基本に存在するかチェック
		/// </summary>
		/// <param name="customerNo">ユーザー顧客ID</param>
		/// <returns>ユーザー顧客ID</returns>
		private int? Sel_T_CUSTOMER_FOUNDATIONS(int customerNo)
		{
			string sql = string.Format("SELECT CUSTOMER_ID"
														+ " FROM [charlieDB].[dbo].[T_CUSTOMER_FOUNDATIONS]"
														+ " WHERE CUSTOMER_ID = {0}", customerNo);
			DataTable table = DatabaseAccess.SelectDatabase(sql, Program.gConnectStr);
			if (0 < table.Rows.Count)
			{
				return DataBaseValue.ConvObjectToInt(table.Rows[0]["CUSTOMER_ID"]);
			}
			return null;
		}

		/// <summary>
		/// 製品管理テーブルからCoupler IDを抽出する(bindなし)
		/// </summary>
		/// <param name="rdID1"></param>
		/// <param name="customerID">顧客ID</param>
		/// <param name="userCLASSIFICATION">ユーザ種別</param>
		/// <returns>製品管理情報リスト</returns>
		private List<T_PRODUCT_CONTROL> Sel_CouplerID_Get(string rdID1, int customerID, int userCLASSIFICATION)
		{
			string sql = string.Empty;
			if ("1" == rdID1)
			{
				// 紐付き選択時（製品管理.終了フラグ = 0(利用中）AND 製品管理.顧客ID = NULL
				sql = "SELECT PRODUCT_ID FROM [charlieDB].[dbo].[T_PRODUCT_CONTROL]"
							+ " WHERE CUSTOMER_ID = 0 AND USER_CLASSIFICATION IN ('0', '1') AND END_FLG = '0' ORDER BY PRODUCT_ID";
			}
			if ("" == rdID1 || "0" == rdID1)
			{
				// 紐付き解除時（製品管理.顧客ID = 選択されている顧客ID)
				sql = string.Format("SELECT PRODUCT_ID FROM [charlieDB].[dbo].[T_PRODUCT_CONTROL]"
												+ " WHERE CUSTOMER_ID = {0} AND USER_CLASSIFICATION = {1}"
												+ " ORDER BY PRODUCT_ID", customerID, userCLASSIFICATION);
			}
			DataTable table = DatabaseAccess.SelectDatabase(sql, Program.gConnectStr);
			return T_PRODUCT_CONTROL.DataTableToList(table);
		}

		/// <summary>
		/// 製品管理情報の更新
		/// </summary>
		/// <param name="argrdID1">CouplerID紐付き条件</param>
		/// <param name="customerID">顧客ID</param>
		/// <param name="couplerID">CouplerID</param>
		/// <param name="trialStartDate">利用開始日</param>
		/// <param name="trialEndDate">利用終了日</param>
		/// <param name="pause_end_status">課金対象終了フラグ</param>
		/// <param name="strUser">更新者</param>
		/// <returns></returns>
		private int Upd_T_Product_Control(int argrdID1, int customerID, string couplerID, DateTime? trialStartDate, DateTime? trialEndDate, bool pause_end_status, string strUser)
		{
			if (1 == argrdID1)
			{
				string sql1 = string.Format(@"UPDATE {0} SET CUSTOMER_ID = @1, TRIAL_FLG = 0, TRIAL_START_DATE = @2, TRIAL_END_DATE = @3, PAUSE_END_STATUS = @4, UPDATE_DATE = @5, UPDATE_PERSON = @6"
													+ " WHERE PRODUCT_ID = '{1}' AND USER_CLASSIFICATION = '0'", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_PRODUCT_CONTROL], couplerID);

				SqlParameter[] param1 = {
											new SqlParameter("@1", customerID),
											new SqlParameter("@2", trialStartDate.HasValue ? trialStartDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
											new SqlParameter("@3", trialEndDate.HasValue ? trialEndDate.Value.ToString() : System.Data.SqlTypes.SqlString.Null),
											new SqlParameter("@4", pause_end_status ? "1" : "0"),
											new SqlParameter("@5", DateTime.Now.ToString()),
											new SqlParameter("@6", strUser ?? System.Data.SqlTypes.SqlString.Null),
				};
				return DatabaseAccess.UpdateSetDatabase(sql1, param1, Program.gConnectStr);
			}
			// ここの処理を呼び出す箇所はなし
			string sql2 = string.Format(@"UPDATE {0} SET CUSTOMER_ID = @1, UPDATE_DATE = @2, UPDATE_PERSON = @3"
												+ " WHERE PRODUCT_ID = '{1}' AND USER_CLASSIFICATION = '0'", CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_PRODUCT_CONTROL], couplerID);
			SqlParameter[] param2 = {
											new SqlParameter("@1", customerID),
											new SqlParameter("@2", DateTime.Now.ToString()),
											new SqlParameter("@3", strUser ?? System.Data.SqlTypes.SqlString.Null),
			};
			return DatabaseAccess.UpdateSetDatabase(sql2, param2, Program.gConnectStr);
		}

		/// <summary>
		/// WW伝票参照ビューの商品コードがコードマスタに存在するか検証
		/// </summary>
		/// <param name="vCheck">WW伝票参照ビュー</param>
		/// <returns>V_CHECK_Service</returns>
		private List<V_CHECK_Service> Sel_V_CHECK_Service(V_CHECK vCheck)
		{
			string sql = string.Format("SELECT A.商品コード as BRAND_CODE, B.SERVICE_TYPE_ID, T.SERVICE_TYPE_NAME, B.SERVICE_ID, S.SERVICE_NAME"
													+ " FROM [charlieDB].[dbo].[WW伝票参照ビュー] as A"
													+ " LEFT JOIN [charlieDB].[dbo].[M_CODE] as B ON RTRIM(A.商品コード) = RTRIM(B.GOODS_ID)"
													+ " LEFT JOIN [charlieDB].[dbo].[M_SERVICE_TYPE] as T on T.SERVICE_TYPE_ID = B.SERVICE_TYPE_ID AND T.DELETE_FLG = '0'"
													+ " LEFT JOIN [charlieDB].[dbo].[M_SERVICE] as S on S.SERVICE_ID = B.SERVICE_ID AND S.UMU_FLG = '0' AND S.DELETE_FLG = '0'"
													+ " WHERE A.商品コード = '{0}' AND A.ユーザー顧客ID = {1} AND A.伝票No = {2}"
													, vCheck.商品コード, vCheck.ユーザー顧客ID, vCheck.伝票No);
			DataTable table = DatabaseAccess.SelectDatabase(sql, Program.gConnectStr);
			return V_CHECK_Service.DataTableToList(table);
		}

		/// <summary>
		/// 顧客利用情報からサービス情報の取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="serviceTypeID">サービス種別ID</param>
		/// <param name="serviceID">サービスID</param>
		/// <returns>ユーザー顧客ID</returns>
		private List<int> Sel_CUSSTOMER_USE_INFOMATION(int customerNo, int serviceTypeID, int serviceID)
		{
			string sql = string.Format("SELECT CUSTOMER_ID"
													+ " FROM [charlieDB].[dbo].[T_CUSSTOMER_USE_INFOMATION]"
													+ " WHERE CUSTOMER_ID = {0} AND SERVICE_TYPE_ID = {1} AND SERVICE_ID = {2}"
													, customerNo, serviceTypeID, serviceID);
			DataTable table = DatabaseAccess.SelectDatabase(sql, Program.gConnectStr);
			if (0 < table.Rows.Count)
			{
				List<int> result = new List<int>();
				foreach (DataRow row in table.Rows)
				{
					int CUSTOMER_ID = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]);
					result.Add(CUSTOMER_ID);
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// 新規申込のサービスの取得
		/// PCA作成済フラグ=未 かつ 申込日=先月以降
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <param name="serviceTypeID">サービス種別ID</param>
		/// <param name="serviceID">サービスID</param>
		/// <returns>T_APPLICATION_DATA_chk</returns>
		private List<T_APPLICATION_DATA_chk> Sel_T_APPLICATION_DATA_chk(int customerNo, int serviceTypeID, int serviceID)
		{
			string sql = string.Format("SELECT CUSTOMER_ID, SERVICE_TYPE_ID, SERVICE_ID"
													+ " FROM [charlieDB].[dbo].[T_APPLICATION_DATA]"
													+ " WHERE CUSTOMER_ID = {0} AND SERVICE_TYPE_ID = {1} AND SERVICE_ID = {2} AND DELETE_FLG = '0'"
															+ " AND APPLICATION_CANCELLATION_FLG = 0 AND PCA_FINISHING_FLG = '0'"
															+ " AND APPLICATION_DATE  BETWEEN '{3}' AND '{4}'"
													, customerNo, serviceTypeID, serviceID, Variables.sAPPCHK_ST_DATE.Value.ToShortDateString(), Variables.SysDate.Value.ToShortDateString());
			DataTable table = DatabaseAccess.SelectDatabase(sql, Program.gConnectStr);
			return T_APPLICATION_DATA_chk.DataTableToList(table);
		}

		/// <summary>
		/// 顧客利用情報作成（旧Charlie自動作成）処理
		/// (1)顧客マスタ参照ビューを参照し顧客管理基本・顧客管理利用情報に、顧客が存在しない場合は削除フラグを1にする
		/// (2)WW伝票参照ビューを元に顧客管理基本の登録・更新・顧客管理利用情報の削除/登録を行う。
		///     (※顧客マスタ参照ビューに存在しない場合は、処理をスキップする）
		/// </summary>
		/// <param name="strUser">登録・更新者</param>
		/// <param name="logFileNm">ログファイル名</param>
		/// <returns></returns>
		public void Auto_Create_Data(string strUser, string logFileNm)
		{
			Variables.InsDate = DateTime.Now;
			Variables.SysDate = DateTime.Today;
			Variables.sSTART_DATE = Variables.SysDate;  // 開始日はシステム日付
			Variables.SysDate_1st = DateTimeUtil.BeginOfMonth(DateTime.Today);  // システム日時の月初日
			Variables.sEND_DATE = DateTimeUtil.EndOfMonth(Variables.SysDate_1st.Value); // 恐らく当月末日を設定？	ソースファイルからは解読不能
			Variables.sAPPCHK_ST_DATE = DateTimeUtil.BeginOfLastMonth(Variables.SysDate_1st.Value);	// 先月１日

			// ■1-2.WW伝票参照ビュー抽出（WW伝票view.受注承認日が締日の範囲内で、数量>0の、伝票番号が最小の伝票データを抽出）
			List<V_CHECK> queryV_CHECK = Sel_V_CHECK();
			foreach (V_CHECK vCheck in queryV_CHECK)
			{
				// WW伝票の商品コードがコードマスタに存在するかチェック
				M_CODE_Chk queryM_CODE_Chk = Sel_M_CODE_Chk(vCheck.商品コード);

				// WW伝票の顧客が顧客マスタ参照ビューに存在するかチェック
				V_CUSTOMER_Chk queryV_CUSTOMER = Sel_V_CUSTOMER(vCheck.ユーザー顧客ID);

				bool MwsIdChk = Sel_MwsID_Chk(vCheck.ユーザー顧客ID);
				if (false == MwsIdChk)
				{
					// MWSID未発行(WW伝票のユーザー顧客IDがT_PRODUCT_CONTROLに顧客として未登録）
					Trace.WriteLine(string.Format("WW伝票参照ビュー：伝票No【{0}】顧客ID【{1}】※T_PRODUCT_CONTROLに顧客IDが存在しません。", vCheck.伝票No, vCheck.ユーザー顧客ID));
					if (null != queryM_CODE_Chk)
					{
						// MWSIDが未発行のため顧客管理基本・顧客管理利用情報の登録は行わない（※エラーではなく警告）
						if (null != queryV_CUSTOMER)
						{
							Trace.WriteLine(string.Format("COUPLER IDをもたないユーザーに「{0}」のサービス伝票が起票されました。伝票No：{1}、商品コード：{2}（顧客ID：{3} 顧客名：{4}）"
																	, queryM_CODE_Chk.SERVICE_NAME, vCheck.伝票No, vCheck.商品コード, vCheck.ユーザー顧客ID, queryV_CUSTOMER.顧客名));
						}
						else
						{
							Trace.WriteLine(string.Format("COUPLER IDをもたないユーザーに「{0}」のサービス伝票が起票されました。伝票No：{1}、商品コード：{2}（顧客ID：{3} ）"
																	, queryM_CODE_Chk.SERVICE_NAME, vCheck.伝票No, vCheck.商品コード, vCheck.ユーザー顧客ID));
						}
					}
					else
					{
						Trace.WriteLine(string.Format("商品ID【{0}】がコードマスター(M_CODE)に存在しません。", vCheck.商品コード));
					}
				}
				else
				{
					// MWSIDが発行されているので、顧客管理基本と顧客管理利用情報に登録をする
					Trace.WriteLine(string.Format("WW伝票商品ID【{0}】MWSID発行済み", vCheck.商品コード));
					if (null != queryM_CODE_Chk)
					{
						// コードマスタに商品コードが存在するので処理を開始
						// -----------------------------------------------------------------------------------------------------
						// 顧客管理基本登録・更新
						// -----------------------------------------------------------------------------------------------------
						// 顧客マスタ参照ビューの登録カード回収日がnullの場合、ライセンス発行可能フラグに = false、その他はライセンス発行可能フラグ = true
						Variables.LICENSE_FLG = false;
						if (null != queryV_CUSTOMER)
						{
							Variables.LICENSE_FLG = (0 == queryV_CUSTOMER.登録カード回収日.Length) ? false : true;
						}
						Trace.WriteLine(string.Format("ライセンス発行可能フラグ={0}", Variables.LICENSE_FLG));

						if (null != queryV_CUSTOMER)
						{
							// 伝票の担当者が営業かどうか社員マスタ参照ビューをチェック
							int 営業区分 = GetOperatingClassification(vCheck.担当者ID);
							Trace.WriteLine(string.Format("WW伝票担当者ID={0}", vCheck.担当者ID));

							// WW伝票.担当者IDを顧客管理基本.営業担当者IDにセット
							Variables.MARKETING_SPECIALIST_ID = vCheck.担当者ID.Replace('　', ' ');

							if (1 != 営業区分)
							{
								// 社員マスタに存在しない、または営業区分がその他の場合はNULLをセット
								Variables.MARKETING_SPECIALIST_MSG = string.Format("※社員マスタ参照ビューに営業担当者ID({0})が存在しません。", vCheck.担当者ID);
							}
							// ww伝票参照ビュー.販売先顧客ID = ww伝票参照ビュー.ユーザー顧客IDの場合はNULLをセット
							if (vCheck.販売先顧客ID == vCheck.ユーザー顧客ID)
							{
								Variables.CUSTOMER_ID = 0;	// 販売店(使用料請求先）・販売拠点コード
								Variables.CUSTOMER_MSG = string.Format("※WW伝票の販売先顧客IDとユーザー顧客IDが同じです。({0})", vCheck.販売先顧客ID);
								Variables.SALE_TYPE = '1';		// 販売種別:1（直接）
							}
							else
							{
								Variables.SALE_TYPE = '2';      // 販売種別:2（販売店）
								// 販売店情報参照ビュー.販売店コード = ww伝票参照ビューの販売先顧客IDがあるか判定
								// レスポンス改善のため販売店情報参照ビュー.販売店ランクコード = 販売店ランクマスタ.販売店ランクコード

								// Coupler ver2.7 CHARLIEDB.M_STORE_RANKからjunpDB.V_STORE_RANKを参照するように変更
								int? 販売店コード = Sel_V_STORE_INFORMATION(vCheck.販売先顧客ID);
								if (false == 販売店コード.HasValue)
								{
									Variables.CUSTOMER_ID = 0;       // 販売店(使用料請求先）・販売拠点コード
									Variables.CUSTOMER_MSG = string.Format("※WW伝票の販売先顧客ID({0})が販売店情報に存在しませんでした。", vCheck.販売先顧客ID);
								}
								else
								{
									Variables.CUSTOMER_ID = vCheck.販売先顧客ID;     // 販売店(使用料請求先）・販売拠点コード
								}
							}
							// ■■2014 / 07 / 01 オート化対応
							// 申込種別区分をセット ※0の場合は更新しない
							// 1 : バリューパック、2 : アップグレード、3 : 月額課金、0 : 申込種別を更新しない
							Variables.APPLY_TYPE = vCheck.申込種別.ToString()[0];
							Trace.WriteLine(string.Format("WW伝票申込種別={0}", Variables.APPLY_TYPE));

							// WW伝票のユーザ顧客IDが顧客管理基本に存在するかチェック
							int? customerNo = Sel_T_CUSTOMER_FOUNDATIONS(vCheck.ユーザー顧客ID);
							if (false == customerNo.HasValue)
							{
								// 顧客管理基本に顧客IDが存在しないため登録する
								T_CUSTOMER_FOUNDATIONS cf = new T_CUSTOMER_FOUNDATIONS();
								cf.CUSTOMER_ID = vCheck.ユーザー顧客ID;
								cf.MARKETING_SPECIALIST_ID = Variables.MARKETING_SPECIALIST_ID;
								cf.STORE_BILLING_ADDRESS_CODE = Variables.CUSTOMER_ID;
								cf.STORE_CODE = Variables.CUSTOMER_ID;
								cf.LICENSE_FLG = false;
								cf.APPLY_RECOVERY_DAY = null;
								cf.SALE_TYPE = Variables.SALE_TYPE;
								cf.DELETE_FLG = false;
								cf.CREATE_DATE = DateTime.Now;
								cf.CREATE_PERSON = strUser;
								cf.UPDATE_DATE = DateTime.Now;
								cf.UPDATE_PERSON = strUser;
								cf.APPLY_TYPE = Variables.APPLY_TYPE;
								CharlieDatabaseAccess.InsertInto_T_CUSTOMER_FOUNDATIONS(cf, Program.gConnectStr);

								Trace.WriteLine(string.Format("伝票No：{0}、商品コード：{1}のデータを顧客管理基本に登録しました。（顧客ID：{2} 顧客名：{3} 営業担当者ID：{4} 販売店（使用料請求先コード/販売拠点コード）：{5}{6}  {7}"
																				, vCheck.伝票No, vCheck.商品コード, vCheck.ユーザー顧客ID, queryV_CUSTOMER.顧客名, Variables.MARKETING_SPECIALIST_ID
																				, Variables.CUSTOMER_ID, Variables.CUSTOMER_MSG, Variables.MARKETING_SPECIALIST_MSG));

								// 顧客管理基本へ追加した顧客IDをリストへ入れる
								Variables.lstInsCustomerId.Add(vCheck.ユーザー顧客ID);
							}
							// MWSIDが発行されていたら、サービス情報を作成する
							// -----------------------------------------------------------------------------------------------------
							// 顧客管理利用情報登録
							// -----------------------------------------------------------------------------------------------------
							// コードマスタより、商品ID,サービス種別ID、サービスIDを取得
							// WW伝票参照ビューに該当の商品コードがコードマスタにデータがある場合
							// 顧客管理利用情報・申込データに該当の顧客ID・サービス種別ID・サービスIDが存在した場合は物理削除し登録
							List<V_CHECK_Service> queryV_CHECK_Service = Sel_V_CHECK_Service(vCheck);
							if (null != queryV_CHECK_Service && 0 < queryV_CHECK_Service.Count)
							{
								// -----------------------------------------------------------------------------------------------------
								// V_CHECK LOOP処理(サービス毎) START
								// -----------------------------------------------------------------------------------------------------
								foreach (V_CHECK_Service vcs in queryV_CHECK_Service)
								{
									// 顧客管理利用情報の顧客・サービス存在チェック
									List<int> customerNoList = Sel_CUSSTOMER_USE_INFOMATION(vCheck.ユーザー顧客ID, vcs.SERVICE_TYPE_ID, vcs.SERVICE_ID);

									// 申込データに対象の顧客・サービスが存在するかチェック
									// PCA作成済フラグ=未 かつ 申込日=先月以降
									List<T_APPLICATION_DATA_chk> T_APPLICATION_DATA_chk_qry = Sel_T_APPLICATION_DATA_chk(vCheck.ユーザー顧客ID, vcs.SERVICE_TYPE_ID, vcs.SERVICE_ID);
									Variables.SERVICE_NAME = vcs.SERVICE_NAME;
									if (null == customerNoList)
									{
										if (null == T_APPLICATION_DATA_chk_qry)
										{
											// 顧客管理利用情報にデータ登録(WW伝票の商品IDに紐づく顧客ID・サービス種別ID・サービスIDを登録）
											DateTime? startDate = null;
											DateTime? endDate = null;
											if (-1 != Variables.lstInsCustomerId.FindIndex(p => p == vCheck.ユーザー顧客ID))
											{
												// 新規に顧客登録する場合はシステム日付から三ヶ月後をセット
												if (Variables.sSTART_DATE.HasValue)
												{
													startDate = Variables.sSTART_DATE.Value;
												}
												if (Variables.sEND_DATE.HasValue)
												{
													endDate = Variables.sEND_DATE.Value;
												}
											}
											else
											{
												// 既に顧客は登録されている場合は利用期間はNULLをセット
												;
											}
											T_CUSSTOMER_USE_INFOMATION cui = new T_CUSSTOMER_USE_INFOMATION();
											cui.CUSTOMER_ID = vCheck.ユーザー顧客ID;
											cui.SERVICE_TYPE_ID = vcs.SERVICE_TYPE_ID;
											cui.SERVICE_ID = vcs.SERVICE_ID;
											cui.GOODS_ID = vcs.BRAND_CODE;
											cui.USE_START_DATE = startDate;
											cui.USE_END_DATE = endDate;
											cui.CREATE_DATE = Variables.InsDate;
											cui.CREATE_PERSON = strUser;
											cui.UPDATE_DATE = Variables.InsDate;
											cui.UPDATE_PERSON = strUser;
											CharlieDatabaseAccess.InsertInto_T_CUSSTOMER_USE_INFOMATION(cui, Program.gConnectStr);
											if (startDate.HasValue)
											{
												Trace.WriteLine(string.Format("伝票No：{0}、商品コード：{1}のデータを顧客管理利用情報に登録しました。（顧客ID：{2} 顧客名：{3} サービス種別ID：{4} サービス種別名：{5} サービスID：{6} サービス名：{7}）"
																	, vCheck.伝票No, vCheck.商品コード, vCheck.ユーザー顧客ID, queryV_CUSTOMER.顧客名, vcs.SERVICE_TYPE_ID, vcs.SERVICE_TYPE_NAME, vcs.SERVICE_ID, vcs.SERVICE_NAME));
											}
											else
											{
												// 期間が設定されていない場合は警告
												Trace.WriteLine(string.Format("伝票No：{0}、商品コード：{1}のデータを顧客管理利用情報に登録しました。（顧客ID：{2} 顧客名：{3} サービス種別ID：{4} サービス種別名：{5} サービスID：{6} サービス名：{7}) ※警告：登録されましたが利用期間が設定されていません。必ず利用期間を設定して下さい。利用期間が設定されるまではCouplerへの同期はされません。"
																	, vCheck.伝票No, vCheck.商品コード, vCheck.ユーザー顧客ID, queryV_CUSTOMER.顧客名, vcs.SERVICE_TYPE_ID, vcs.SERVICE_TYPE_NAME, vcs.SERVICE_ID, vcs.SERVICE_NAME));
											}
										}
										else
										{
											// 申込データに対象の顧客が存在する
											Trace.WriteLine(string.Format("伝票No：{0}、商品コード：{1}のデータは既に申込情報に存在しているため処理をスキップしました。（顧客ID：{2} 顧客名：{3} サービス種別ID：{4} サービス種別名：{5} サービスID：{6} サービス名：{7}）"
																, vCheck.伝票No, vCheck.商品コード, vCheck.ユーザー顧客ID, queryV_CUSTOMER.顧客名, vcs.SERVICE_TYPE_ID, vcs.SERVICE_TYPE_NAME, vcs.SERVICE_ID, vcs.SERVICE_NAME));
										}
									}
								}
								// -----------------------------------------------------------------------------------------------------
								// V_CHECK LOOP処理(サービス毎) END
								// -----------------------------------------------------------------------------------------------------
							}
						}
					}
					else
					{
						// コードマスタに存在しない商品コードがWW伝票にあった
						;
					}
				}
			}   // foreach:queryV_CHECK

			// ■■■ CouplerDB（顧客管理製品情報・サービス情報・申込情報（システム反映フラグ更新）の差分実行 ■■■
			Trace.WriteLine("CusDataUpdate_Main() Start");

			//< CFSET ret = objcusdataupdate.CusDataUpdate_Main("1", "1", "1", null, null) >

			Trace.WriteLine("CusDataUpdate_Main() End RTN=#ret#");
			Trace.WriteLine("cusdataupdate.CusDataUpdate_Mainの戻り値：#ret#");

			Trace.WriteLine("Auto_Create_Data() End");
		}
	}
}
