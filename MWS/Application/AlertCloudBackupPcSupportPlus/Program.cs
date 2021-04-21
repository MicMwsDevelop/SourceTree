//
// Program.cs
//
// クラウドバックアップPC安心サポートPlus同時申込アラート プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/02/05 勝呂)
//
using ClosedXML.Excel;
using AlertCloudBackupPcSupportPlus.Mail;
using AlertCloudBackupPcSupportPlus.Settings;
using MwsLib.BaseFactory;
using MwsLib.BaseFactory.AlertCloudBackupPcSupportPlus;
using MwsLib.BaseFactory.Charlie.Table;
using MwsLib.BaseFactory.Charlie.View;
using MwsLib.Common;
using MwsLib.DB.SqlServer.AlertCloudBackupPcSupportPlus;
using MwsLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace AlertCloudBackupPcSupportPlus
{
	static class Program
	{
		/// <summary>
		/// データベース接続先 CT環境
		/// </summary>
		public static bool DATABACE_ACCEPT_CT = false;

		/// <summary>
		/// システム日付
		/// </summary>
		public static Date gSystemDate;

		/// <summary>
		/// プログラム名
		/// </summary>
		public const string PROC_NAME = "ｸﾗｳﾄﾞﾊﾞｯｸｱｯﾌﾟPC安心ｻﾎﾟｰﾄPlus申込ｱﾗｰﾄ";

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
			List<CloudBackupPcSupportPlus> formalChecktList1 = new List<CloudBackupPcSupportPlus>();
			List<CloudBackupPcSupportPlus> formalChecktList2 = new List<CloudBackupPcSupportPlus>();
			List<CloudBackupPcSupportPlus> formalChecktList3 = new List<CloudBackupPcSupportPlus>();
			try
			{
				// 1. 同時契約中チェック
				// PC安心サポートPlus契約期間中にクラウドバックアップサービスも契約している
				// (1) 当日の利用情報にクラウドバックアップとクラウドバックアップ(PC安心サポートPlus)が存在する
				// (2)PC安心サポート契約情報がPC安心サポートPlusで利用期間中である
				// (3)クラウドバックアップが解約中でない
				List<CloudBackupPcSupportPlus> list = AlertCloudBackupPcSupportPlusAccess.GetCloudBackupPcSupportPlusList(gSystemDate, DATABACE_ACCEPT_CT);
				foreach (CloudBackupPcSupportPlus data in list)
				{
					//string whereStr1 = string.Format("fCustomerID = {0} AND (fServiceId = {1} OR fServiceId = {2} OR fServiceId = {3}) AND fContractStartDate <= '{4}' AND fContractEndDate >= '{4}'"
					//								, data.CustomerNo
					//								, (int)ServiceCodeDefine.ServiceCode.PcSafetySupportPlus3
					//								, (int)ServiceCodeDefine.ServiceCode.PcSafetySupportPlus1
					//								, (int)ServiceCodeDefine.ServiceCode.PcSafetySupportPlusContinue
					//								, gSystemDate.ToDateTime());
					//List<T_USE_PCCSUPPORT> pc1List = CharlieDatabaseAccess.Select_T_USE_PCCSUPPORT(whereStr1, "fCustomerID ASC", DATABACE_ACCEPT_CT);
					//if (null != pc1List && 0 < pc1List.Count)
					//{
					//	string whereStr2 = string.Format("customer_id = {0} AND service_id = {1} AND apply_type = '1' AND system_flg = '0'"
					//							, data.CustomerNo
					//							, (int)ServiceCodeDefine.ServiceCode.ExCloudBackup);
					//	List<V_COUPLER_APPLY> apply1List = CharlieDatabaseAccess.Select_V_COUPLER_APPLY(whereStr2, "", DATABACE_ACCEPT_CT);
					//	if (null == apply1List)
					//	{
					//		checktList1.Add(data);
					//	}
					//}
					checktList1.Add(data);
				}
				// 2.クラウドバックアップ申込中チェック
				// PC安心サポートPlus契約期間中にクラウドバックアップサービスがMWSサイトで申し込まれた
				// (1)申込情報でクラウドバックアップが申込中である
				// (2)翌月初日はPC安心サポート契約情報がPC安心サポートPlusで利用期間中である
				string whereStr3 = string.Format("service_id = {0} AND apply_type = '0' AND system_flg = '0' AND cp_id LIKE 'MWS%'"
													, (int)ServiceCodeDefine.ServiceCode.ExCloudBackup);
				List<V_COUPLER_APPLY> apply2List = CharlieDatabaseAccess.Select_V_COUPLER_APPLY(whereStr3, "customer_id ASC", DATABACE_ACCEPT_CT);
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
						List<T_USE_PCCSUPPORT> pc2List = CharlieDatabaseAccess.Select_T_USE_PCCSUPPORT(whereStr4, "", DATABACE_ACCEPT_CT);
						if (null != pc2List && 0 < pc2List.Count)
						{
							CloudBackupPcSupportPlus data = new CloudBackupPcSupportPlus();
							data.CustomerNo = apply.customer_id;
							data.PcStartDate = new DateTime(pc2List[0].fContractStartDate.Value.Year, pc2List[0].fContractStartDate.Value.Month, pc2List[0].fContractStartDate.Value.Day);
							data.PcEndDate = new DateTime(pc2List[0].fContractEndDate.Value.Year, pc2List[0].fContractEndDate.Value.Month, pc2List[0].fContractEndDate.Value.Day);

							string whereStr5 = string.Format("顧客No = {0}", apply.customer_id);
							List<view_MWS顧客情報> nameList = CharlieDatabaseAccess.Select_view_MWS顧客情報(whereStr5, "", DATABACE_ACCEPT_CT);
							if (null != nameList && 0 < nameList.Count)
							{
								data.ClinicName = nameList[0].顧客名;
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
				List<V_COUPLER_APPLY> apply3List = CharlieDatabaseAccess.Select_V_COUPLER_APPLY(whereStr6, "customer_id ASC", DATABACE_ACCEPT_CT);
				if (null != apply3List)
				{
					foreach (V_COUPLER_APPLY apply in apply3List)
					{
						string whereStr7 = string.Format("CUSTOMER_ID = {0} AND SERVICE_ID = {1} AND USE_START_DATE <= '{2}' AND USE_END_DATE >= '{2}'"
															, apply.customer_id
															, (int)ServiceCodeDefine.ServiceCode.ExCloudBackup
															, gSystemDate.FirstDayOfNextMonth().ToDateTime());
						List<T_CUSSTOMER_USE_INFOMATION> cuiList = CharlieDatabaseAccess.Select_T_CUSSTOMER_USE_INFOMATION(whereStr7, "", DATABACE_ACCEPT_CT);
						if (null != cuiList && 1 == cuiList.Count)
						{
							string whereStr8 = string.Format("customer_id = {0} AND service_id = {1} AND apply_type = '1' AND system_flg = '0'"
																, apply.customer_id
																, (int)ServiceCodeDefine.ServiceCode.ExCloudBackup);
							List<V_COUPLER_APPLY> apply4List = CharlieDatabaseAccess.Select_V_COUPLER_APPLY(whereStr8, "cp_id ASC", DATABACE_ACCEPT_CT);
							if (null == apply4List)
							{
								CloudBackupPcSupportPlus data = new CloudBackupPcSupportPlus();
								data.CustomerNo = apply.customer_id;
								data.ClStartDate = cuiList[0].USE_START_DATE;
								data.ClEndDate = cuiList[0].USE_END_DATE;
								string whereStr9 = string.Format("顧客No = {0}", apply.customer_id);
								List<view_MWS顧客情報> nameList = CharlieDatabaseAccess.Select_view_MWS顧客情報(whereStr9, "", DATABACE_ACCEPT_CT);
								if (null != nameList && 0 < nameList.Count)
								{
									data.ClinicName = nameList[0].顧客名;
								}
								checktList3.Add(data);
							}
						}
					}
				}
				if (0 < checktList1.Count + checktList2.Count + checktList3.Count)
				{
					using (XLWorkbook wb = new XLWorkbook(Path.Combine(Directory.GetCurrentDirectory(), "アラート情報.xlsx"), XLEventTracking.Disabled))
					{
						IXLWorksheet ws = wb.Worksheet("アラート");
						List<CloudBackupPcSupportPlus> compList = new List<CloudBackupPcSupportPlus>();


						IXLRange rgn = ws.RangeUsed().AsTable();
						for (int i = 1; i < rgn.RowCount(); i++)
						{
							CloudBackupPcSupportPlus plus = new CloudBackupPcSupportPlus();
							string csv = string.Format("{0},{1},{2},{3},{4},{5},{6}", rgn.Cell(i + 1, 1).Value, rgn.Cell(i + 1, 2).Value, rgn.Cell(i + 1, 3).Value, rgn.Cell(i + 1, 4).Value, rgn.Cell(i + 1, 5).Value, rgn.Cell(i + 1, 6).Value, rgn.Cell(i + 1, 7).Value);
							plus.SetRecord(csv);
							compList.Add(plus);
						}






						//foreach(IXLTableRow dataRow in table.DataRange.Rows())
						//{
						//	if (firstLine)
						//	{
						//		firstLine = false;
						//		continue;
						//	}
						//	CloudBackupPcSupportPlus plus = new CloudBackupPcSupportPlus();
						//	string csv = string.Format("{0},{1},{2},{3},{4},{5},{6}", dataRow.Cell(1).Value, dataRow.Cell(2).Value, dataRow.Cell(3).Value, dataRow.Cell(4).Value, dataRow.Cell(5).Value, dataRow.Cell(6).Value, dataRow.Cell(7).Value);
						//	plus.SetRecord(csv);
						//	compList.Add(plus);
						//}
						foreach (CloudBackupPcSupportPlus check in checktList1)
						{
							if (-1 != compList.FindIndex(p => p.IsMatch(check)))
							{
								formalChecktList1.Add(check);
							}
						}
						foreach (CloudBackupPcSupportPlus check in checktList2)
						{
							if (-1 != compList.FindIndex(p => p.IsMatch(check)))
							{
								formalChecktList2.Add(check);
							}
						}
						foreach (CloudBackupPcSupportPlus check in checktList3)
						{
							if (-1 != compList.FindIndex(p => p.IsMatch(check)))
							{
								formalChecktList3.Add(check);
							}
						}
						if (0 < formalChecktList1.Count + formalChecktList2.Count + formalChecktList3.Count)
						{
#if !DEBUG
							// 営業管理部宛にアラートメール送信
							SendMailControl.AlartSendMail(formalChecktList1, formalChecktList2, formalChecktList3);
#endif
							// アラート情報に追加
							formalChecktList1.AddRange(formalChecktList2);
							formalChecktList1.AddRange(formalChecktList3);
							foreach (CloudBackupPcSupportPlus check in formalChecktList1)
							{
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				msg = ex.Message;
				return -1;
			}
			return formalChecktList1.Count + formalChecktList2.Count + formalChecktList3.Count;
		}
	}
}
