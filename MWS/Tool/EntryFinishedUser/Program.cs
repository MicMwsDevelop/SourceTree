//
// Program.cs
//
// 終了ユーザー管理プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using EntryFinishedUser.Mail;
using MwsLib.BaseFactory;
using MwsLib.BaseFactory.Charlie.Table;
using MwsLib.BaseFactory.EntryFinishedUser;
using MwsLib.BaseFactory.Junp.Table;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Charlie;
using MwsLib.DB.SqlServer.EntryFinishedUser;
using MwsLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
			/// ②終了予定ユーザーリストメール送信
			/// </summary>
			ThisMonthFiniedUser = 1,

			/// <summary>
			/// 終了ユーザー処理自動実行モード
			/// タイミング：当月初日のMWS課金データ作成実行後に行う
			/// ①終了ユーザー設定
			/// ②終了ユーザーリストメール送信
			/// </summary>
			PrevMonthFiniedUser = 2,
		}

		/// <summary>
		/// 起動引数
		/// </summary>
		private static ProgramBootType BootType;

		/// <summary>
		/// データベース接続先 CT環境
		/// </summary>
		public static bool DATABACE_ACCEPT_CT = true;

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

			gSystemDate = Date.Today;

			// コマンドライン引数を配列で取得する
			BootType = ProgramBootType.Menu;
			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("1" == cmds[1])
				{
					//BootType = ProgramBootType.ThisMonthFiniedUser;
				}
				else if ("2" == cmds[1])
				{
					BootType = ProgramBootType.PrevMonthFiniedUser;
				}
				if (3 == cmds.Length)
				{
					// システム日付
					gSystemDate = Date.Parse(int.Parse(cmds[2]));
				}
			}
			try
			{
				// リプレース先リストの取得
				gReplaceList = JunpDatabaseAccess.Select_tMikコードマスタ("fcm名称 Not Like '%不可%' AND fcmコード <> '001' AND fcmコード種別 = '30'", "fcmコード ASC", DATABACE_ACCEPT_CT);
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
					Application.Run(new Forms.FinishedUserListForm());
					break;
				//// 当月終了月終了ユーザー処理
				//case ProgramBootType.ThisMonthFiniedUser:
				//	Program.ThisMonthFiniedUser(today);
				//	break;
				// 前月終了月終了ユーザー処理
				case ProgramBootType.PrevMonthFiniedUser:
					Program.PrevMonthFiniedUser(gSystemDate);
					break;
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
		//	List<EntryFinishedUserData> work = EntryFinishedUserAccess.GetEntryFinishedUserDataList(DATABACE_ACCEPT_CT);

		//	YearMonth thisMonth = date.ToYearMonth();
		//	List<EntryFinishedUserData> paletteFinishedList = work.Where(p => true == p.IsNextMonthFinishedUserByPalette(thisMonth)).ToList();
		//	if (0 < paletteFinishedList.Count)
		//	{
		//		// 翌月終了ユーザー（palette）
		//		// ①課金対象外フラグＯＦＦ
		//		List<Tuple<int, int>> list = new List<Tuple<int, int>>();
		//		foreach (EntryFinishedUserData user in paletteFinishedList)
		//		{
		//			List<int> svList = EntryFinishedUserAccess.GetPauseEndStatus(user.CustomerID, DATABACE_ACCEPT_CT);
		//			foreach (int sv in svList)
		//			{
		//				list.Add(new Tuple<int, int>(user.CustomerID, sv));
		//			}
		//		}
		//		if (!DATABACE_ACCEPT_CT)
		//		{
		//			EntryFinishedUserSetIO.UpdatePauseEndStatus(list, DATABACE_ACCEPT_CT);
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
		//			EntryFinishedUserSetIO.UpdateClientEndFlag(oldSystemFinishedList, DATABACE_ACCEPT_CT);
		//		}
		//		// ②終了ユーザーリストメール送信
		//		SendMailControl.SendEigyoKanriMail(oldSystemFinishedList, true);
		//	}
		//}

		/// <summary>
		/// 終了ユーザー処理自動実行モード
		/// ①終了ユーザー設定
		/// ②終了ユーザーリストメール送信
		/// </summary>
		/// <param name="date"></param>
		private static void PrevMonthFiniedUser(Date date)
		{
			IEnumerable<EntryFinishedUserData> list = EntryFinishedUserAccess.GetEntryFinishedUserDataList(DATABACE_ACCEPT_CT);
			if (0 < list.Count())
			{
				YearMonth thisMonth = date.ToYearMonth();
				YearMonth nextMonth = thisMonth + 1;
				IEnumerable<EntryFinishedUserData> userList = list.Where(p => true == p.IsPrevMonthFinishedUser(thisMonth));
				if (0 < userList.Count())
				{
					//////////////////////////////////////////
					// palette → 終了 or 非paletteユーザー → 終了

					IEnumerable<EntryFinishedUserData> paletteList = userList.Where(p => false == p.NonPaletteUser);
					foreach (EntryFinishedUserData user in paletteList)
					{
						try
						{
							// [JunpDB].[dbo].[tClient].[fCliEnd] = 1（終了）
							// [JunpDB].[dbo].[tClient].[fCliUpdate] = 現在
							// [JunpDB].[dbo].[tClient].[fCliUpdateMan] = プログラム名
							JunpDatabaseAccess.UpdateSet_tClient(user.CustomerID, true, ProductName, DATABACE_ACCEPT_CT);
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
								JunpDatabaseAccess.UpdateSetJunpDatabase(sqlString, param, DATABACE_ACCEPT_CT);
							}
							else
							{
								string sqlString = string.Format(@"UPDATE {0} SET fusユーザー = @1,  fus更新日 = @2, fus更新者 = @3 WHERE fusCliMicID = {1}", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ], user.CustomerID);
								SqlParameter[] param = { new SqlParameter("@1", "0"),
														new SqlParameter("@2", DateTime.Now),
														new SqlParameter("@3", ProductName) };
								JunpDatabaseAccess.UpdateSetJunpDatabase(sqlString, param, DATABACE_ACCEPT_CT);
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(string.Format("UpdateSet_tMikユーザ Error!({0})", ex.Message), "データベースエラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						}
						// MWS課金データ作成バッチで間違って設定されているユーザー区分を非paletteユーザーからpaletteユーザーに戻す
						string whereStr = string.Format("CUSTOMER_ID = {0}", user.CustomerID);
						List<T_PRODUCT_CONTROL> pdList = CharlieDatabaseAccess.Select_T_PRODUCT_CONTROL(whereStr, "", DATABACE_ACCEPT_CT);
						if (null != pdList && 1 == pdList.Count)
						{
							try
							{
								// [CharlieDB].[dbo].[T_PRODUCT_CONTROL].[USER_CLASSIFICATION] = 0（paletteユーザー）
								pdList[0].USER_CLASSIFICATION = MwsDefine.UserClassification.PaletteUser;
								CharlieDatabaseAccess.UpdateSet_T_PRODUCT_CONTROL(pdList[0], DATABACE_ACCEPT_CT);
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
							JunpDatabaseAccess.InsertInto_tMemo(memo, DATABACE_ACCEPT_CT);
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
								string sqlString = string.Format(@"UPDATE {0} SET fusユーザー = @1, fus前ｼｽﾃﾑ名称 = @2, fusシステム名 = @3,  fusメーカー名 = @4,  fus更新日 = @5, fus更新者 = @6 WHERE fusCliMicID = {1}", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ], user.CustomerID);
								SqlParameter[] param = { new SqlParameter("@1", "1"),
														new SqlParameter("@2", user.SystemCode),
														new SqlParameter("@3", MwsDefine.SystemCodeEtc),
														new SqlParameter("@4", replace),
														new SqlParameter("@5", DateTime.Now),
														new SqlParameter("@6", ProductName) };
								JunpDatabaseAccess.UpdateSetJunpDatabase(sqlString, param, DATABACE_ACCEPT_CT);
							}
							else
							{
								string sqlString = string.Format(@"UPDATE {0} SET fusユーザー = @1, fus前ｼｽﾃﾑ名称 = @2, fusシステム名 = @3,  fus更新日 = @4, fus更新者 = @5 WHERE fusCliMicID = {1}", JunpDatabaseDefine.TableName[JunpDatabaseDefine.TableType.tMikユーザ], user.CustomerID);
								SqlParameter[] param = { new SqlParameter("@1", "1"),
											new SqlParameter("@2", user.SystemCode),
											new SqlParameter("@3", MwsDefine.SystemCodeEtc),
											new SqlParameter("@4", DateTime.Now),
											new SqlParameter("@5", ProductName) };
								JunpDatabaseAccess.UpdateSetJunpDatabase(sqlString, param, DATABACE_ACCEPT_CT);
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(string.Format("UpdateSet_tMikユーザ Error!({0})", ex.Message), "データベースエラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
							return;
						}
						// 利用情報に非palette標準サービスの追加及び更新
						string whereStr = string.Format("CUSTOMER_ID = {0} AND SERVICE_ID = {1}", user.CustomerID, (int)MwsDefine.ServiceCode.StandardNonPalette);
						List<T_CUSSTOMER_USE_INFOMATION> cuiList = CharlieDatabaseAccess.Select_T_CUSSTOMER_USE_INFOMATION(whereStr, "", DATABACE_ACCEPT_CT);
						if (null == cuiList)
						{
							try
							{
								// 新規追加
								T_CUSSTOMER_USE_INFOMATION cui = new T_CUSSTOMER_USE_INFOMATION
								{
									CUSTOMER_ID = user.CustomerID,
									SERVICE_TYPE_ID = (int)MwsDefine.ServiceType.Standard,
									SERVICE_ID = (int)MwsDefine.ServiceCode.StandardNonPalette,
									USE_START_DATE = thisMonth.ToDate(1).ToDateTime(),
									USE_END_DATE = nextMonth.ToDate(-1).ToDateTime(),
									CREATE_DATE = DateTime.Now,
									CREATE_PERSON = Program.ProductName,
									RENEWAL_FLG = true
								};
								CharlieDatabaseAccess.InsertInto_T_CUSSTOMER_USE_INFOMATION(cui, DATABACE_ACCEPT_CT);
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
								CharlieDatabaseAccess.UpdateSet_T_CUSSTOMER_USE_INFOMATION(cui, DATABACE_ACCEPT_CT);
							}
							catch (Exception ex)
							{
								MessageBox.Show(string.Format("UpdateSet_T_CUSSTOMER_USE_INFOMATION Error!({0})", ex.Message), "データベースエラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
								return;
							}
						}
					}
					// ②終了ユーザーリストメール送信
					// 終了ユーザー連絡メール送信（営業管理部宛て）
					SendMailControl.SendFinishedUserMail(paletteList, paletteList.Count());
					if (0 < nonPaletteList.Count())
					{
						// 非paletteユーザー連絡メール送信（営業管理部宛て）
						SendMailControl.SendNonPaletteUserMail(nonPaletteList, nonPaletteList.Count());
					}
				}
			}
		}
	}
}
