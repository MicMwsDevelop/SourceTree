//
// Program.cs
//
// 終了ユーザー管理プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// アプリ管理サイト：APPID605 終了ユーザー登録
// 処理概要：当月初日に先月終了ユーザーに対し終了フラグを設定する
// 入力ファイル：無
// 出力ファイル：無
// 印刷物：無
// メール送信：無
/////////////////////////////////////////////////////////
// Ver1.00 新規作成(2019/06/28 勝呂)
// Ver2.00 契約中サービスの確認機能の追加(2020/07/17 勝呂)
// Ver2.01 SQL Server接続情報を環境設定に移行(2021/09/07 勝呂)
// Ver2.02 paletteESとソフトウェア保守料１年の契約期間のチェックの追加(2022/05/13 勝呂)
// Ver2.03 XMLファイルの変更(2022/06/08 勝呂)
// 
using ClosedXML.Excel;
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.EntryFinishedUser;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.Common;
using CommonLib.DB.SqlServer;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.EntryFinishedUser;
using CommonLib.DB.SqlServer.Junp;
using EntryFinishedUser.BaseFactory;
using EntryFinishedUser.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EntryFinishedUser
{
	static class Program
	{
		/// <summary>
		/// 起動引数
		/// </summary>
		private enum ProgramBootType
		{
			/// <summary>
			/// メイン画面起動
			/// </summary>
			Menu = 0,

			/// <summary>
			/// 当月終了月終了ユーザー処理
			/// タイミング：翌月初日のMWS課金データ作成実行前に行う
			/// ①課金対象外フラグＯＦＦ
			/// ×②終了予定ユーザーリストメール送信
			/// </summary>
			//ThisMonthFiniedUser = 1,

			/// <summary>
			/// 終了ユーザー処理自動実行モード
			/// タイミング：当月初日のMWS課金データ作成実行後に行う
			/// ①終了ユーザー設定
			/// ×②終了ユーザーリストメール送信
			/// </summary>
			PrevMonthFiniedUser = 2,

			/// <summary>
			/// 契約中サービスの確認
			/// タイミング：終了月が翌月のユーザーに対し、他サービス契約中のリストをメールにて注意喚起
			/// ①他サービス契約中の確認
			/// ②契約中ユーザーリストメール送信
			/// </summary>
			//CheckContractService = 3,
		}

		/// <summary>
		/// 起動引数
		/// </summary>
		private static ProgramBootType BootType;

		/// <summary>
		/// 環境設定
		/// </summary>
		public static EntryFinishedUserSettings gSettings { get; set; }

		/// <summary>
		/// バージョン情報
		/// </summary>
		public const string VersionStr = "Ver2.03 (2022/06/08)";

		/// <summary>
		/// 製品名
		/// </summary>
		public static string ProductName = "終了ユーザー管理";

		/// <summary>
		/// リプレース先リスト
		/// </summary>
		public static IEnumerable<tMikコードマスタ> gReplaceList;

		/// <summary>
		/// システム日付
		/// </summary>
		public static Date gSystemDate;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// システム日付の設定
			gSystemDate = Date.Today;

			// 環境設定の読込
			gSettings = EntryFinishedUserSettingsSettingsIF.GetSettings();

			// コマンドライン引数を配列で取得する
			BootType = ProgramBootType.Menu;
			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				//if ("1" == cmds[1])
				//{
				//	BootType = ProgramBootType.ThisMonthFiniedUser;
				//}
				//else 
				if ("2" == cmds[1])
				{
					BootType = ProgramBootType.PrevMonthFiniedUser;
				}
				//else if ("3" == cmds[1])
				//{
				//	BootType = ProgramBootType.CheckContractService;
				//}
				if (3 == cmds.Length)
				{
					// システム日付
					gSystemDate = Date.Parse(int.Parse(cmds[2]));
				}
			}
			try
			{
				// リプレース先リストの取得
				gReplaceList = JunpDatabaseAccess.Select_tMikコードマスタ("fcm名称 Not Like '%不可%' AND fcmコード <> '001' AND fcmコード種別 = '30'", "fcmコード ASC", gSettings.ConnectJunp.ConnectionString);
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("Select_tMikコードマスタ Error!({0})", ex.Message), "データベースエラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}
			switch (BootType)
			{
				// メイン画面起動
				case ProgramBootType.Menu:
					Application.Run(new Forms.MainForm());
					break;
				//// 当月終了月終了ユーザー処理
				//case ProgramBootType.ThisMonthFiniedUser:
				//	Program.ThisMonthFiniedUser(today);
				//	break;
				// 前月終了月終了ユーザー処理
				case ProgramBootType.PrevMonthFiniedUser:
					Program.PrevMonthFiniedUser(gSystemDate);
					break;
				// 契約中サービスの確認
				//case ProgramBootType.CheckContractService:
				//	Program.CheckContractService(gSystemDate);
				//	break;
			}
		}

		///// <summary>
		///// 当月終了月終了ユーザー処理
		///// ①課金対象外フラグＯＦＦ
		///// ②終了予定ユーザーリストメール送信
		///// </summary>
		///// <param name="date"></param>
		//private static void ThisMonthFiniedUser(Date date)
		//{
		//	List<EntryFinishedUserData> work = EntryFinishedUserAccess.GetEntryFinishedUserDataList(gSettings);

		//	YearMonth thisMonth = date.ToYearMonth();
		//	List<EntryFinishedUserData> paletteFinishedList = work.Where(p => true == p.IsNextMonthFinishedUserByPalette(thisMonth)).ToList();
		//	if (0 < paletteFinishedList.Count)
		//	{
		//		// 翌月終了ユーザー（palette）
		//		// ①課金対象外フラグＯＦＦ
		//		List<Tuple<int, int>> list = new List<Tuple<int, int>>();
		//		foreach (EntryFinishedUserData user in paletteFinishedList)
		//		{
		//			List<int> svList = EntryFinishedUserAccess.GetPauseEndStatus(user.CustomerID, gSettings);
		//			foreach (int sv in svList)
		//			{
		//				list.Add(new Tuple<int, int>(user.CustomerID, sv));
		//			}
		//		}
		//		if (!DATABACE_ACCEPT_CT)
		//		{
		//			EntryFinishedUserSetIO.UpdatePauseEndStatus(list, gSettings);
		//		}
		//		if (0 < list.Count)
		//		{
		//			// ②終了予定ユーザーリストメール送信
		//			SendMailControl.SendEigyoKanriMail(paletteFinishedList, false);
		//		}
		//	}
		//	List<EntryFinishedUserData> oldSystemFinishedList = work.Where(p => true == p.IsNextMonthFinishedUserByOldSystem(thisMonth)).ToList();
		//	if (0 < oldSystemFinishedList.Count)
		//	{
		//		// 翌月終了ユーザー（旧システム）
		//		// ①終了ユーザー設定
		//		if (!DATABACE_ACCEPT_CT)
		//		{
		//			EntryFinishedUserSetIO.UpdateClientEndFlag(oldSystemFinishedList, gSettings);
		//		}
		//		// ②終了ユーザーリストメール送信
		//		SendMailControl.SendEigyoKanriMail(oldSystemFinishedList, true);
		//	}
		//}

		/// <summary>
		/// 終了ユーザー処理自動実行モード
		/// ①終了ユーザー設定
		/// ×②終了ユーザーリストメール送信
		/// </summary>
		/// <param name="date"></param>
		private static void PrevMonthFiniedUser(Date date)
		{
			IEnumerable<EntryFinishedUserData> list = EntryFinishedUserAccess.GetEntryFinishedUserDataList(gSettings.ConnectJunp.ConnectionString);
			if (0 < list.Count())
			{
				YearMonth thisMonth = date.ToYearMonth();
				YearMonth nextMonth = thisMonth + 1;
				IEnumerable<EntryFinishedUserData> userList = list.Where(p => true == p.IsPrevMonthFinishedUser(thisMonth));
				if (0 < userList.Count())
				{
					//////////////////////////////////////////
					// palette → 終了 or 非paletteユーザー → 終了

					//List<EntryFinishedUserData> finisherList = new List<EntryFinishedUserData>();
					IEnumerable<EntryFinishedUserData> paletteList = userList.Where(p => false == p.NonPaletteUser);
					foreach (EntryFinishedUserData user in paletteList)
					{
						//finisherList.Add(user);

						try
						{
							// [JunpDB].[dbo].[tClient].[fCliEnd] = 1（終了）
							// [JunpDB].[dbo].[tClient].[fCliUpdate] = 現在
							// [JunpDB].[dbo].[tClient].[fCliUpdateMan] = プログラム名
							JunpDatabaseAccess.UpdateSet_tClient(user.CustomerID, true, ProductName, gSettings.ConnectJunp.ConnectionString);
						}
						catch (Exception ex)
						{
							MessageBox.Show(string.Format("UpdateSet_tClient Error!({0})", ex.Message), "データベースエラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
							return;
						}
						string replace = string.Empty;
						if (0 < user.Replace.Length)
						{
							IEnumerable<tMikコードマスタ> codeList = gReplaceList.Where(p => p.fcm名称 == StringUtil.ConvertUpperCase(user.Replace.Trim()));
							if (0 < codeList.Count())
							{
								replace = codeList.First().fcmコード;
							}
						}
						try
						{
							// [JunpDB].[dbo].[tMikユーザー].[fusユーザー] = 0（非ユーザー）
							// ×[JunpDB].[dbo].[tMikユーザー].[fus前ｼｽﾃﾑ名称] = [JunpDB].[dbo].[tMikユーザー].[fusｼｽﾃﾑ名]
							// ×[JunpDB].[dbo].[tMikユーザー].[fusｼｽﾃﾑ名] = ''（空白）
							// [JunpDB].[dbo].[tMikユーザー].[fusメーカー名] = リプレース
							// [JunpDB].[dbo].[tMikユーザー].[fus更新日] = 現在
							// [JunpDB].[dbo].[tMikユーザー].[fus更新者] = プログラム名
							if (0 < replace.Length)
							{
								string sqlString = string.Format(@"UPDATE {0} SET fusユーザー = @1, fusメーカー名 = @2,  fus更新日 = @3, fus更新者 = @4 WHERE fusCliMicID = {1}", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ], user.CustomerID);
								SqlParameter[] param = { new SqlParameter("@1", "0"),
														new SqlParameter("@2", replace),
														new SqlParameter("@3", DateTime.Now),
														new SqlParameter("@4", ProductName) };
								DatabaseAccess.UpdateSetDatabase(sqlString, param, gSettings.ConnectJunp.ConnectionString);
							}
							else
							{
								string sqlString = string.Format(@"UPDATE {0} SET fusユーザー = @1,  fus更新日 = @2, fus更新者 = @3 WHERE fusCliMicID = {1}", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ], user.CustomerID);
								SqlParameter[] param = { new SqlParameter("@1", "0"),
														new SqlParameter("@2", DateTime.Now),
														new SqlParameter("@3", ProductName) };
								DatabaseAccess.UpdateSetDatabase(sqlString, param, gSettings.ConnectJunp.ConnectionString);
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(string.Format("UpdateSet_tMikユーザ Error!({0})", ex.Message), "データベースエラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						}
						// MWS課金データ作成バッチで間違って設定されているユーザー区分を非paletteユーザーからpaletteユーザーに戻す
						string whereStr = string.Format("CUSTOMER_ID = {0}", user.CustomerID);
						List<T_PRODUCT_CONTROL> pdList = CharlieDatabaseAccess.Select_T_PRODUCT_CONTROL(whereStr, "", gSettings.ConnectCharlie.ConnectionString);
						if (null != pdList && 1 == pdList.Count)
						{
							try
							{
								// [CharlieDB].[dbo].[T_PRODUCT_CONTROL].[USER_CLASSIFICATION] = 0（paletteユーザー）
								pdList[0].USER_CLASSIFICATION = MwsDefine.UserClassification.PaletteUser;
								CharlieDatabaseAccess.UpdateSet_T_PRODUCT_CONTROL(pdList[0], gSettings.ConnectCharlie.ConnectionString);
							}
							catch (Exception ex)
							{
								MessageBox.Show(string.Format("UpdateSet_T_PRODUCT_CONTROL Error!({0})", ex.Message), "データベースエラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
								return;
							}
						}
						// メモを登録
						try
						{
							tMemo memo = user.To_tMemo();
							memo.fMemMemo = user.GetMemoFinishedString();
							JunpDatabaseAccess.InsertInto_tMemo(memo, gSettings.ConnectJunp.ConnectionString);
						}
						catch (Exception ex)
						{
							MessageBox.Show(string.Format("InsertInto_tMemo Error!({0})", ex.Message), "データベースエラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
							return;
						}
					}


					//////////////////////////////////////////
					// palette → 非paletteユーザー

					IEnumerable<EntryFinishedUserData> nonPaletteList = userList.Where(p => true == p.NonPaletteUser);
					foreach (EntryFinishedUserData user in nonPaletteList)
					{
						string replace = string.Empty;
						if (0 < user.Replace.Length)
						{
							IEnumerable<tMikコードマスタ> codeList = gReplaceList.Where(p => p.fcm名称 == StringUtil.ConvertUpperCase(user.Replace.Trim()));
							if (0 < codeList.Count())
							{
								replace = codeList.First().fcmコード;
							}
						}
						try
						{
							// [JunpDB].[dbo].[tMikユーザー].[fusユーザー] = 1（ユーザー）
							// [JunpDB].[dbo].[tMikユーザー].[fus前ｼｽﾃﾑ名称] = [JunpDB].[dbo].[tMikユーザー].[fusｼｽﾃﾑ名]
							// [JunpDB].[dbo].[tMikユーザー].[fusｼｽﾃﾑ名] = '999'（その他）
							// [JunpDB].[dbo].[tMikユーザー].[fusメーカー名] = リプレース
							// [JunpDB].[dbo].[tMikユーザー].[fus更新日] = 現在
							// [JunpDB].[dbo].[tMikユーザー].[fus更新者] = プログラム名
							if (0 < replace.Length)
							{
								string sqlString = string.Format(@"UPDATE {0} SET fusユーザー = @1, fus前ｼｽﾃﾑ名称 = @2, fusシステム名 = @3,  fusメーカー名 = @4,  fus更新日 = @5, fus更新者 = @6 WHERE fusCliMicID = {1}"
																	, JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ], user.CustomerID);

								SqlParameter[] param = { new SqlParameter("@1", "1"),
														new SqlParameter("@2", user.SystemCode),
														new SqlParameter("@3", MwsDefine.SystemCodeEtc),
														new SqlParameter("@4", replace),
														new SqlParameter("@5", DateTime.Now),
														new SqlParameter("@6", ProductName) };
								DatabaseAccess.UpdateSetDatabase(sqlString, param, gSettings.ConnectJunp.ConnectionString);
							}
							else
							{
								string sqlString = string.Format(@"UPDATE {0} SET fusユーザー = @1, fus前ｼｽﾃﾑ名称 = @2, fusシステム名 = @3,  fus更新日 = @4, fus更新者 = @5 WHERE fusCliMicID = {1}", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ], user.CustomerID);
								SqlParameter[] param = { new SqlParameter("@1", "1"),
											new SqlParameter("@2", user.SystemCode),
											new SqlParameter("@3", MwsDefine.SystemCodeEtc),
											new SqlParameter("@4", DateTime.Now),
											new SqlParameter("@5", ProductName) };
								DatabaseAccess.UpdateSetDatabase(sqlString, param, gSettings.ConnectJunp.ConnectionString);
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(string.Format("UpdateSet_tMikユーザ Error!({0})", ex.Message), "データベースエラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
							return;
						}
						// 利用情報に非palette標準サービスの追加及び更新
						string whereStr = string.Format("CUSTOMER_ID = {0} AND SERVICE_ID = {1}", user.CustomerID, (int)ServiceCodeDefine.ServiceCode.StandardNonPalette);
						List<T_CUSSTOMER_USE_INFOMATION> cuiList = CharlieDatabaseAccess.Select_T_CUSSTOMER_USE_INFOMATION(whereStr, "", gSettings.ConnectCharlie.ConnectionString);
						if (null == cuiList)
						{
							try
							{
								// 新規追加
								T_CUSSTOMER_USE_INFOMATION cui = new T_CUSSTOMER_USE_INFOMATION
								{
									CUSTOMER_ID = user.CustomerID,
									SERVICE_TYPE_ID = (int)ServiceCodeDefine.ServiceType.Standard,
									SERVICE_ID = (int)ServiceCodeDefine.ServiceCode.StandardNonPalette,
									USE_START_DATE = thisMonth.ToDate(1).ToDateTime(),
									USE_END_DATE = nextMonth.ToDate(-1).ToDateTime(),
									CREATE_DATE = DateTime.Now,
									CREATE_PERSON = Program.ProductName,
									RENEWAL_FLG = true
								};
								CharlieDatabaseAccess.InsertInto_T_CUSSTOMER_USE_INFOMATION(cui, gSettings.ConnectCharlie.ConnectionString);
							}
							catch (Exception ex)
							{
								MessageBox.Show(string.Format("InsertInto_T_CUSSTOMER_USE_INFOMATION Error!({0})", ex.Message), "データベースエラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
								return;
							}
						}
						else
						{
							try
							{
								// 変更
								T_CUSSTOMER_USE_INFOMATION cui = cuiList[0];
								cui.GOODS_ID = null;
								cui.KAKIN_START_DATE = null;
								cui.USE_START_DATE = thisMonth.ToDate(1).ToDateTime();
								cui.USE_END_DATE = nextMonth.ToDate(-1).ToDateTime();
								cui.CANCELLATION_DAY = null;
								cui.CANCELLATION_PROCESSING_DATE = null;
								cui.PAUSE_END_STATUS = false;
								cui.UPDATE_PERSON = Program.ProductName;
								cui.PERIOD_END_DATE = null;
								cui.RENEWAL_FLG = true;
								CharlieDatabaseAccess.UpdateSet_T_CUSSTOMER_USE_INFOMATION(cui, gSettings.ConnectCharlie.ConnectionString);
							}
							catch (Exception ex)
							{
								MessageBox.Show(string.Format("UpdateSet_T_CUSSTOMER_USE_INFOMATION Error!({0})", ex.Message), "データベースエラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
								return;
							}
						}
					}
					//// ②終了ユーザーリストメール送信
					//// 終了ユーザー連絡メール送信（営業管理部宛て）
					//SendMailControl.SendFinishedUserMail(paletteList, paletteList.Count());
					//if (0 < nonPaletteList.Count())
					//{
					//	// 非paletteユーザー連絡メール送信（営業管理部宛て）
					//	SendMailControl.SendNonPaletteUserMail(nonPaletteList, nonPaletteList.Count());
					//}
				}
				//// 前月終了済ユーザー サービス契約中リスト メール送信（営業管理部宛て）
				//List<EntryFinishedUserData> finisherList = userList.Where(p => false == p.NonPaletteUser).ToList();
				//if (0 < finisherList.Count)
				//{
				//	List<ContractServiceUser> contractUserList = GetContractServiceUserList(finisherList);
				//	SendMailControl.SendContractServiceMailPrevMonth(contractUserList);
				//}
			}
		}

		///// <summary>
		///// 契約中サービスの確認
		///// タイミング：終了月が翌月のユーザーに対し、他サービス契約中のリストをメールにて注意喚起
		///// ①他サービス契約中の確認
		///// ②契約中ユーザーリストメール送信
		///// </summary>
		///// <param name="date">当日</param>
		//private static void CheckContractService(Date date)
		//{
		//	IEnumerable<EntryFinishedUserData> list = EntryFinishedUserAccess.GetEntryFinishedUserDataList(gSettings);
		//	if (0 < list.Count())
		//	{
		//		YearMonth thisMonth = date.ToYearMonth();
		//		List<EntryFinishedUserData> finisherList = list.Where(p => true == p.IsNextMonthFinishedUser(thisMonth)).ToList();
		//		if (0 < finisherList.Count())
		//		{
		//			// サービス契約中リストの取得
		//			List<ContractServiceUser> contractUserList = GetContractServiceUserList(finisherList);

		//			// 翌月終了ユーザー サービス契約中リスト メール送信（営業管理部宛て）
		//			SendMailControl.SendContractServiceMailNextMonth(contractUserList);
		//		}
		//	}
		//}

		///// <summary>
		///// サービス契約中リストの取得
		///// </summary>
		///// <param name="finisherList">終了ユーザーリスト</param>
		///// <returns>サービス契約中リスト</returns>
		//private static List<ContractServiceUser> GetContractServiceUserList(List<EntryFinishedUserData> finisherList)
		//{
		//	List<int> checkList = (from user in finisherList orderby user.CustomerID select user.CustomerID).ToList();
		//	List<ContractServiceUser> ret = new List<ContractServiceUser>();

		//	// ESET月額版
		//	List<T_LICENSE_PRODUCT_CONTRACT> esetList = Program.ContractServiceESET(checkList);
		//	if (null != esetList)
		//	{
		//		foreach (T_LICENSE_PRODUCT_CONTRACT eset in esetList)
		//		{
		//			int index = finisherList.FindIndex(p => p.CustomerID == eset.CUSTOMER_ID);
		//			if (-1 != index)
		//			{
		//				ContractServiceUser contract = new ContractServiceUser(finisherList[index]);
		//				contract.ServiceID = eset.SERVICE_ID.ToString();
		//				contract.ServiceName = CharlieDatabaseAccess.GetServiceName(eset.SERVICE_ID, gSettings);
		//				contract.StartDate = eset.START_DATE;
		//				contract.EndDate = eset.END_DATE;
		//				ret.Add(contract);
		//			}
		//		}
		//	}
		//	// PC安心サポート
		//	List<T_USE_PCCSUPPORT> pcList = Program.ContractServicePcSupport(finisherList);
		//	if (null != pcList)
		//	{
		//		foreach (T_USE_PCCSUPPORT pc in pcList)
		//		{
		//			int index = finisherList.FindIndex(p => p.CustomerID == pc.fCustomerID);
		//			if (-1 != index)
		//			{
		//				ContractServiceUser contract = new ContractServiceUser(finisherList[index]);
		//				contract.ServiceID = pc.fServiceId.ToString();
		//				contract.ServiceName = string.Format("PC安心サポート({0})", CharlieDatabaseAccess.GetServiceName(pc.fServiceId, gSettings));
		//				if (pc.fContractStartDate.HasValue)
		//				{
		//					contract.StartDate = pc.fContractStartDate.Value.ToDateTime();
		//				}
		//				if (pc.fContractEndDate.HasValue)
		//				{
		//					contract.EndDate = pc.fContractEndDate.Value.ToDateTime();
		//				}
		//				ret.Add(contract);
		//			}
		//		}
		//	}
		//	// ナルコーム製品
		//	List<T_CUSSTOMER_USE_INFOMATION> cuiList = Program.ContractServiceNarcohm(checkList);
		//	if (null != cuiList)
		//	{
		//		foreach (T_CUSSTOMER_USE_INFOMATION cui in cuiList)
		//		{
		//			int index = finisherList.FindIndex(p => p.CustomerID == cui.CUSTOMER_ID);
		//			if (-1 != index)
		//			{
		//				ContractServiceUser contract = new ContractServiceUser(finisherList[index]);
		//				contract.ServiceID = cui.SERVICE_ID.ToString();
		//				contract.ServiceName = CharlieDatabaseAccess.GetServiceName(cui.SERVICE_ID, gSettings);
		//				contract.StartDate = cui.USE_START_DATE;
		//				contract.EndDate = cui.USE_END_DATE;
		//				ret.Add(contract);
		//			}
		//		}
		//	}
		//	// Microsoft365製品
		//	cuiList = Program.ContractService365(checkList);
		//	if (null != cuiList)
		//	{
		//		foreach (T_CUSSTOMER_USE_INFOMATION cui in cuiList)
		//		{
		//			int index = finisherList.FindIndex(p => p.CustomerID == cui.CUSTOMER_ID);
		//			if (-1 != index)
		//			{
		//				ContractServiceUser contract = new ContractServiceUser(finisherList[index]);
		//				contract.ServiceID = cui.SERVICE_ID.ToString();
		//				contract.ServiceName = CharlieDatabaseAccess.GetServiceName(cui.SERVICE_ID, Program.DATABACE_ACCEPT_CT);
		//				contract.StartDate = cui.USE_START_DATE;
		//				contract.EndDate = cui.USE_END_DATE;
		//				ret.Add(contract);
		//			}
		//		}
		//	}
		//	// Curlineクラウド
		//	List<int> noList = Program.ContractServiceCurlineCloud();
		//	if (null != noList)
		//	{
		//		foreach (int no in noList)
		//		{
		//			int index = finisherList.FindIndex(p => p.CustomerID == no);
		//			if (-1 != index)
		//			{
		//				ContractServiceUser contract = new ContractServiceUser(finisherList[index]);
		//				contract.ServiceID = PcaGoodsIDDefine.MwsCurlineCloud;
		//				contract.ServiceName = "MWS Curline ｸﾗｳﾄﾞ利用料(月額)";
		//				ret.Add(contract);
		//			}
		//		}
		//	}
		//	// はなはなし購読
		//	noList = Program.ContractServiceHanahanashi();
		//	if (null != noList)
		//	{
		//		foreach (int no in noList)
		//		{
		//			int index = finisherList.FindIndex(p => p.CustomerID == no);
		//			if (-1 != index)
		//			{
		//				ContractServiceUser contract = new ContractServiceUser(finisherList[index]);
		//				contract.ServiceID = PcaGoodsIDDefine.Hanahanashi;
		//				contract.ServiceName = "はなはなし";
		//				ret.Add(contract);
		//			}
		//		}
		//	}
		//	// 介護連携、介護伝送
		//	cuiList = Program.ContractServiceKaigo(checkList);
		//	if (null != cuiList)
		//	{
		//		foreach (T_CUSSTOMER_USE_INFOMATION cui in cuiList)
		//		{
		//			int index = finisherList.FindIndex(p => p.CustomerID == cui.CUSTOMER_ID);
		//			if (-1 != index)
		//			{
		//				ContractServiceUser contract = new ContractServiceUser(finisherList[index]);
		//				contract.ServiceID = cui.SERVICE_ID.ToString();
		//				contract.ServiceName = CharlieDatabaseAccess.GetServiceName(cui.SERVICE_ID, Program.DATABACE_ACCEPT_CT);
		//				contract.StartDate = cui.USE_START_DATE;
		//				contract.EndDate = cui.USE_END_DATE;
		//				ret.Add(contract);
		//			}
		//		}
		//	}
		//	return ret;
		//}

		/// <summary>
		/// 契約中サービス確認 - ESET月額版
		/// </summary>
		/// <param name="usetList">確認ユーザーリスト</param>
		/// <returns>結果</returns>
		public static List<T_LICENSE_PRODUCT_CONTRACT> ContractServiceESET(List<int> usetList)
		{
			string userStr = string.Join(",", usetList);
			//string whereStr = string.Format("CUSTOMER_ID IN ({0})", userStr);
			string whereStr = string.Format("APPLY_STATUS = '0' AND CUSTOMER_ID IN ({0})", userStr);
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_LICENSE_PRODUCT_CONTRACT], whereStr, "CUSTOMER_ID", gSettings.ConnectCharlie.ConnectionString);
			List<T_LICENSE_PRODUCT_CONTRACT> ret = T_LICENSE_PRODUCT_CONTRACT.DataTableToList(table);
			if (null != ret && 0 < ret.Count)
			{
				return ret;
			}
			return null;
		}

		/// <summary>
		/// 契約中サービス確認 - PC安心サポート
		/// </summary>
		/// <param name="usetList">確認ユーザーリスト</param>
		/// <returns>結果</returns>
		public static List<T_USE_PCCSUPPORT> ContractServicePcSupport(List<EntryFinishedUserData> userList)
		{
			List<int> checkList = (from user in userList orderby user.CustomerID select user.CustomerID).ToList();
			string userStr = string.Join(",", checkList);
			string whereStr = string.Format("fEndFlag = '0' AND fCustomerID IN ({0})", userStr);
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_USE_PCCSUPPORT], whereStr, "fCustomerID", gSettings.ConnectCharlie.ConnectionString);
			List<T_USE_PCCSUPPORT> ret = T_USE_PCCSUPPORT.DataTableToList(table);
			if (null != ret && 0 < ret.Count)
			{
				return ret;
			}
			return null;
		}

		/// <summary>
		/// 契約中サービス確認 - ナルコーム製品
		/// </summary>
		/// <param name="usetList">確認ユーザーリスト</param>
		/// <returns>結果</returns>
		public static List<T_CUSSTOMER_USE_INFOMATION> ContractServiceNarcohm(List<int> usetList)
		{
			string userStr = string.Join(",", usetList);
			string whereStr = string.Format("PAUSE_END_STATUS = '0' AND CUSTOMER_ID IN ({0}) AND SERVICE_ID IN ({1})", userStr, string.Join(",", ContractServiceUser.NarcohmSeriveID()));
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION], whereStr, "CUSTOMER_ID, SERVICE_ID", gSettings.ConnectCharlie.ConnectionString);
			List<T_CUSSTOMER_USE_INFOMATION> ret = T_CUSSTOMER_USE_INFOMATION.DataTableToList(table);
			if (null != ret && 0 < ret.Count)
			{
				return ret;
			}
			return null;
		}

		/// <summary>
		/// 契約中サービス確認 - Microsoft365製品
		/// </summary>
		/// <param name="usetList">確認ユーザーリスト</param>
		/// <returns>結果</returns>
		public static List<T_CUSSTOMER_USE_INFOMATION> ContractService365(List<int> usetList)
		{
			string userStr = string.Join(",", usetList);
			string whereStr = string.Format("PAUSE_END_STATUS = '0' AND CUSTOMER_ID IN ({0}) AND SERVICE_ID IN ({1})", userStr, string.Join(",", ContractServiceUser.Microsoft365SeriveID()));
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION], whereStr, "CUSTOMER_ID, SERVICE_ID", gSettings.ConnectCharlie.ConnectionString);
			List<T_CUSSTOMER_USE_INFOMATION> ret = T_CUSSTOMER_USE_INFOMATION.DataTableToList(table);
			if (null != ret && 0 < ret.Count)
			{
				return ret;
			}
			return null;
		}

		/// <summary>
		/// Curlineクラウド利用料請求リストファイルパス名の取得
		/// </summary>
		/// <returns>Curlineクラウド利用料請求リストファイルパス名</returns>
		private static string CurlineCloudListFileName()
		{
			YearMonth ym = gSystemDate.ToYearMonth();
			int i = 1;
			string pathname = string.Empty;
			do
			{
				// 請求リスト_YYYYMM.csv 例:請求リスト_202006.csv
				pathname = Path.Combine(gSettings.CurlineCloudListFolder, string.Format("請求リスト_{0}.csv", ym.ToIntYM()));
				if (12 < i)
				{
					// 過去１年分検索
					return string.Empty;
				}
				ym--;
				i++;
			}
			while (false == File.Exists(pathname));
			return pathname;
		}

		/// <summary>
		/// 契約中サービス確認 - Curlineクラウド
		/// </summary>
		/// <returns>結果</returns>
		public static List<int> ContractServiceCurlineCloud()
		{
			string pathname = CurlineCloudListFileName();
			if (0 < pathname.Length)
			{
				List<int> ret = new List<int>();
				using (var sr = new StreamReader(pathname))
				{
					while (!sr.EndOfStream)
					{
						string line = sr.ReadLine();
						string[] values = line.Split(',');
						if (0 < values[2].ToLong())
						{
							// 0 < 請求金額
							ret.Add(values[0].ToInt());
						}
					}
				}
				if (0 < ret.Count)
				{
					return ret;
				}
			}
			return null;
		}

		/// <summary>
		/// はなはなし月次発送リストファイルパス名の取得
		/// </summary>
		/// <returns>はなはなし月次発送リストファイルパス名</returns>
		private static string HanahanashiListFileName()
		{
			YearMonth ym = gSystemDate.ToYearMonth();
			int i = 1;
			string pathname = string.Empty;
			do
			{
				// YYYYMM.xlsx 例:202006.xlsx
				pathname = Path.Combine(gSettings.HanahashiUserListFolder, string.Format("{0}.xlsx", ym.ToIntYM()));
				if (12 < i)
				{
					// 過去１年分検索
					return string.Empty;
				}
				ym--;
				i++;
			}
			while (false == File.Exists(pathname));
			return pathname;
		}

		/// <summary>
		/// 契約中サービス確認 - はなはなし購読
		/// </summary>
		/// <returns>結果</returns>
		public static List<int> ContractServiceHanahanashi()
		{
			string pathname = HanahanashiListFileName();
			if (0 < pathname.Length)
			{
				List<int> ret = new List<int>();
				using (XLWorkbook workbook = new XLWorkbook(pathname, XLEventTracking.Disabled))
				{
					IXLWorksheet sheet = workbook.Worksheet("月次はなはなしリスト");

					// テーブル作成
					IXLTable tbl = sheet.RangeUsed().AsTable();
					for (int i = 0; i < tbl.DataRange.Rows().Count(); i++)
					{
						if (0 < i)
						{
							IXLTableRow dataRow = tbl.DataRange.Row(i);
							double no = (double)dataRow.Field(0).Value;
							ret.Add((int)no);
						}
					}
				}
				if (0 < ret.Count)
				{
					return ret;
				}
			}
			return null;
		}

		/// <summary>
		/// 契約中サービス確認 - 介護連携、介護伝送
		/// </summary>
		/// <param name="usetList">確認ユーザーリスト</param>
		/// <returns>結果</returns>
		public static List<T_CUSSTOMER_USE_INFOMATION> ContractServiceKaigo(List<int> usetList)
		{
			string userStr = string.Join(",", usetList);
			string whereStr = string.Format("PAUSE_END_STATUS = '0' AND CUSTOMER_ID IN ({0}) AND SERVICE_ID IN ({1})", userStr, string.Join(",", ContractServiceUser.KaigoSeriveID()));
			DataTable table = DatabaseAccess.SelectDatabase(CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION], whereStr, "CUSTOMER_ID, SERVICE_ID", gSettings.ConnectCharlie.ConnectionString);
			List<T_CUSSTOMER_USE_INFOMATION> ret = T_CUSSTOMER_USE_INFOMATION.DataTableToList(table);
			if (null != ret && 0 < ret.Count)
			{
				return ret;
			}
			return null;
		}

		/// <summary>
		/// 契約中サービス確認 - palette ESとソフトウェア保守料１年の契約期間チェック
		/// </summary>
		/// <param name="usetList"></param>
		/// <returns></returns>
		/// Ver2.02 paletteESとソフトウェア保守料１年の契約期間のチェックの追加(2022/05/13 勝呂)
		public static List<CheckSoftwareMainte> ContractSoftwareMainte(List<int> usetList)
		{
			string sql = "SELECT MN.CUSTOMER_ID, ES.USE_END_DATE as ES_USE_END_DATE, MN.USE_END_DATE as MN_USE_END_DATE"
							+ " FROM {0} as MN"
							+ " LEFT JOIN ("
							+ " SELECT CUSTOMER_ID, USE_END_DATE"
							+ " FROM {0}"
							+ " WHERE SERVICE_ID = {1} AND PAUSE_END_STATUS = 0 AND CUSTOMER_ID IN ({2})"
							+ ") as ES on MN.CUSTOMER_ID = ES.CUSTOMER_ID"
							+ " WHERE MN.CUSTOMER_ID = ES.CUSTOMER_ID AND MN.SERVICE_ID = {3} AND MN.PAUSE_END_STATUS = 0 AND ES.USE_END_DATE <> MN.USE_END_DATE"
							+ " ORDER BY MN.CUSTOMER_ID";
			string userStr = string.Join(",", usetList);
			string sqlStr = string.Format(sql, CharlieDatabaseDefine.TableName[CharlieDatabaseDefine.TableType.T_CUSSTOMER_USE_INFOMATION], (int)ServiceCodeDefine.ServiceCode.PaletteES, userStr, (int)ServiceCodeDefine.ServiceCode.SoftwareMainte1);
			DataTable table = DatabaseAccess.SelectDatabase(sqlStr, gSettings.ConnectCharlie.ConnectionString);
			List<CheckSoftwareMainte> ret = CheckSoftwareMainte.DataTableToList(table);
			if (null != ret && 0 < ret.Count)
			{
				return ret;
			}
			return null;
		}
	}
}
