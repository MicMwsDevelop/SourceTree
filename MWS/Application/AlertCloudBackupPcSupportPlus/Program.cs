//
// Program.cs
//
// クラウドバックアップPC安心サポートPlus同時申込アラート プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
/////////////////////////////////////////////////////////
// アプリ管理サイト：APPID615 クラウドバックアップPC安心サポートPlus申込アラート
// 処理概要：クラウドバックアップのサービスとPC安心サポートPlusの両方を申し込んでいるもしくは利用している医院の検出後、
//           eigyo_kanri@mic.jpに対しアラートメールを送信する。検出されなかった時はメール送信は行わない
// 入力ファイル：無
// 出力ファイル：無
// 印刷物：無
// メール送信：クラウドバックアップPC安心サポートPlus申込アラート
/////////////////////////////////////////////////////////
// Ver1.00 新規作成(2021/02/05 勝呂)
// Ver1.01 アラートに引っかかったユーザーがしばらくの間、毎日、アラートさせるので、チェックから除外するユーザーを登録できるように改修(2021/10/03 勝呂)
// Ver1.02(2023/09/14 勝呂):組織変更対応 営業管理部→システム管理部
/////////////////////////////////////////////////////////
//
using AlertCloudBackupPcSupportPlus.Mail;
using AlertCloudBackupPcSupportPlus.Settings;
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.AlertCloudBackupPcSupportPlus;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Charlie.View;
using CommonLib.Common;
using CommonLib.DB.SqlServer.AlertCloudBackupPcSupportPlus;
using CommonLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AlertCloudBackupPcSupportPlus
{
	static class Program
	{
		/// <summary>
		/// システム日付
		/// </summary>
		public static Date gSystemDate;

		/// <summary>
		/// プログラム名
		/// </summary>
		public const string PROC_NAME = "ｸﾗｳﾄﾞﾊﾞｯｸｱｯﾌﾟPC安心ｻﾎﾟｰﾄPlus申込ｱﾗｰﾄ";

		/// <summary>
		/// プログラムバージョン
		/// </summary>
		public const string VersionStr = "Ver1.02";

		/// <summary>
		/// 環境設定
		/// </summary>
		public static AlertCloudBackupPcSupportPlusSettings gSettings;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static int Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			gSystemDate = Date.Today;

			gSettings = AlertCloudBackupPcSupportPlusSettingsIF.GetSettings();

			string[] cmds = Environment.GetCommandLineArgs();
			if (2 <= cmds.Length)
			{
				if ("AUTO" == cmds[1].ToUpper())
				{
					// サイレントモード
					string msg;
					if (-1 == AlartCheck(out msg))
					{
						return 1;
					}
					return 0;
				}
			}
			Application.Run(new Forms.MainForm());
			return 0;
		}

		/// <summary>
		/// アラートチェック
		/// </summary>
		/// <param name="msg">エラーメッセージ</param>
		/// <returns>判定</returns>
		public static int AlartCheck(out string msg)
		{
			msg = string.Empty;

			List<CloudBackupPcSupportPlus> checktList1 = new List<CloudBackupPcSupportPlus>();
			List<CloudBackupPcSupportPlus> checktList2 = new List<CloudBackupPcSupportPlus>();
			List<CloudBackupPcSupportPlus> checktList3 = new List<CloudBackupPcSupportPlus>();
			try
			{
				// Ver1.01 アラートに引っかかったユーザーがしばらくの間、毎日、アラートさせるので、チェックから除外するユーザーを登録できるように改修(2021/10/03 勝呂)
				// 除外ユーザーの取得
				List<string> excludeUserList = gSettings.GetExcludeUserList();

				// 1. 同時契約中チェック
				// PC安心サポートPlus契約期間中にクラウドバックアップサービスも契約している
				// (1) 当日の利用情報にクラウドバックアップとクラウドバックアップ(PC安心サポートPlus)が存在する
				// (2)PC安心サポート契約情報がPC安心サポートPlusで利用期間中である
				// (3)クラウドバックアップが解約中でない
				List<CloudBackupPcSupportPlus> list = AlertCloudBackupPcSupportPlusAccess.GetCloudBackupPcSupportPlusList(gSystemDate, gSettings.Connect.Charlie.ConnectionString);
				if (null != list)
				{
					foreach (CloudBackupPcSupportPlus data in list)
					{
						// Ver1.01 アラートに引っかかったユーザーがしばらくの間、毎日、アラートさせるので、チェックから除外するユーザーを登録できるように改修(2021/10/03 勝呂)
						if (-1 != excludeUserList.FindIndex(p => p == data.CustomerNo.ToString()))
						{
							continue;
						}
						checktList1.Add(data);
					}
				}
				// 2.クラウドバックアップ申込中チェック
				// PC安心サポートPlus契約期間中にクラウドバックアップサービスがMWSサイトで申し込まれた
				// (1)申込情報でクラウドバックアップが申込中である
				// (2)翌月初日はPC安心サポート契約情報がPC安心サポートPlusで利用期間中である
				string whereStr3 = string.Format("service_id = {0} AND apply_type = '0' AND system_flg = '0' AND cp_id LIKE 'MWS%'"
													, (int)ServiceCodeDefine.ServiceCode.ExCloudBackup);
				List<V_COUPLER_APPLY> apply2List = CharlieDatabaseAccess.Select_V_COUPLER_APPLY(whereStr3, "customer_id ASC", gSettings.Connect.Charlie.ConnectionString);
				if (null != apply2List)
				{
					foreach (V_COUPLER_APPLY apply in apply2List)
					{
						string whereStr4 = string.Format("fCustomerID = {0} AND (fServiceId = {1} OR fServiceId = {2} OR fServiceId = {3}) AND fContractStartDate <= '{4}' AND fContractEndDate >= '{4}'"
												, apply.customer_id
												, (int)ServiceCodeDefine.ServiceCode.PcSafetySupportPlus3
												, (int)ServiceCodeDefine.ServiceCode.PcSafetySupportPlus1
												, (int)ServiceCodeDefine.ServiceCode.PcSafetySupportPlusContinue
												, gSystemDate.FirstDayOfNextMonth().ToDateTime());
						List<T_USE_PCCSUPPORT> pc2List = CharlieDatabaseAccess.Select_T_USE_PCCSUPPORT(whereStr4, "", gSettings.Connect.Charlie.ConnectionString);
						if (null != pc2List && 0 < pc2List.Count)
						{
							CloudBackupPcSupportPlus data = new CloudBackupPcSupportPlus();
							data.CustomerNo = apply.customer_id;
							data.PcStartDate = new DateTime(pc2List[0].fContractStartDate.Value.Year, pc2List[0].fContractStartDate.Value.Month, pc2List[0].fContractStartDate.Value.Day);
							data.PcEndDate = new DateTime(pc2List[0].fContractEndDate.Value.Year, pc2List[0].fContractEndDate.Value.Month, pc2List[0].fContractEndDate.Value.Day);

							string whereStr5 = string.Format("顧客No = {0}", apply.customer_id);
							List<view_MWS顧客情報> nameList = CharlieDatabaseAccess.Select_view_MWS顧客情報(whereStr5, "", gSettings.Connect.Charlie.ConnectionString);
							if (null != nameList && 0 < nameList.Count)
							{
								data.ClinicName = nameList[0].顧客名;
							}
							// Ver1.01 アラートに引っかかったユーザーがしばらくの間、毎日、アラートさせるので、チェックから除外するユーザーを登録できるように改修(2021/10/03 勝呂)
							if (-1 != excludeUserList.FindIndex(p => p == data.CustomerNo.ToString()))
							{
								continue;
							}
							checktList2.Add(data);
						}
					}
				}
				// 3.PC安心サポートPlus申込中チェック
				// (1)申込情報でクラウドバックアップ(PC安心サポートPlus)が申込中である
				// (2)当日の利用情報にクラウドバックアップが存在する
				// (3)クラウドバックアップが解約中でない
				string whereStr6 = string.Format("service_id = {0} AND apply_type = '0' AND system_flg = '0' AND cp_id LIKE 'MWS%'"
													, (int)ServiceCodeDefine.ServiceCode.ExCloudBackupPcSupportPlus);
				List<V_COUPLER_APPLY> apply3List = CharlieDatabaseAccess.Select_V_COUPLER_APPLY(whereStr6, "customer_id ASC", gSettings.Connect.Charlie.ConnectionString);
				if (null != apply3List)
				{
					foreach (V_COUPLER_APPLY apply in apply3List)
					{
						string whereStr7 = string.Format("CUSTOMER_ID = {0} AND SERVICE_ID = {1} AND USE_START_DATE <= '{2}' AND USE_END_DATE >= '{2}'"
															, apply.customer_id
															, (int)ServiceCodeDefine.ServiceCode.ExCloudBackup
															, gSystemDate.FirstDayOfNextMonth().ToDateTime());
						List<T_CUSSTOMER_USE_INFOMATION> cuiList = CharlieDatabaseAccess.Select_T_CUSSTOMER_USE_INFOMATION(whereStr7, "", gSettings.Connect.Charlie.ConnectionString);
						if (null != cuiList && 1 == cuiList.Count)
						{
							string whereStr8 = string.Format("customer_id = {0} AND service_id = {1} AND apply_type = '1' AND system_flg = '0'"
																, apply.customer_id
																, (int)ServiceCodeDefine.ServiceCode.ExCloudBackup);
							List<V_COUPLER_APPLY> apply4List = CharlieDatabaseAccess.Select_V_COUPLER_APPLY(whereStr8, "cp_id ASC", gSettings.Connect.Charlie.ConnectionString);
							if (null == apply4List)
							{
								CloudBackupPcSupportPlus data = new CloudBackupPcSupportPlus();
								data.CustomerNo = apply.customer_id;
								data.ClStartDate = cuiList[0].USE_START_DATE;
								data.ClEndDate = cuiList[0].USE_END_DATE;
								string whereStr9 = string.Format("顧客No = {0}", apply.customer_id);
								List<view_MWS顧客情報> nameList = CharlieDatabaseAccess.Select_view_MWS顧客情報(whereStr9, "", gSettings.Connect.Charlie.ConnectionString);
								if (null != nameList && 0 < nameList.Count)
								{
									data.ClinicName = nameList[0].顧客名;
								}
								// Ver1.01 アラートに引っかかったユーザーがしばらくの間、毎日、アラートさせるので、チェックから除外するユーザーを登録できるように改修(2021/10/03 勝呂)
								if (-1 != excludeUserList.FindIndex(p => p == data.CustomerNo.ToString()))
								{
									continue;
								}
								checktList3.Add(data);
							}
						}
					}
				}
				if (0 < checktList1.Count + checktList2.Count + checktList3.Count)
				{
					// 営業管理部宛にアラートメール送信
					SendMailControl.AlartSendMail(checktList1, checktList2, checktList3);
				}
			}
			catch (Exception ex)
			{
				msg = ex.Message;
				return -1;
			}
			return checktList1.Count + checktList2.Count + checktList3.Count;
		}
	}
}
