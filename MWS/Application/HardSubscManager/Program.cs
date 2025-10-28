//
// Program.cs
//
// ハードサブスク管理 プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/10/20 勝呂)
// 
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.Common;
using CommonLib.DB.SqlServer.HardSubsc;
using CommonLib.DB.SqlServer.Junp;
using HardSubscManager.Settings;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HardSubscManager
{
	internal static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string ProgramName = "ハードサブスク管理";

		/// <summary>
		/// プログラムバージョン
		/// </summary>
		public const string ProgramVersion = "Ver1.00 2025/10/20";

		/// <summary>
		/// [JunpDB].[dbo].[tUser]
		/// </summary>
		public static tUser UserInfo { get; set; }

		/// <summary>
		/// 環境設定
		/// </summary>
		public static HardSubscManagerSettings gSettings { get; set; }

		/// <summary>
		/// カテゴリリスト
		/// </summary>
		private static List<Tuple<int, string>> gCategoryList { get; set; }

		/// <summary>
		/// カテゴリ名：本体
		/// </summary>
		public const string CategoryPC = "本体";

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// 環境設定の読込
			gSettings = HardSubscManagerSettingsIF.GetSettings();

			// ユーザー情報の設定
			UserInfo = HardSubscAccess.GetLoginUser(Environment.UserName, gSettings.ConnectCharlie.ConnectionString);

			Application.Run(new Forms.MainForm());
		}

		/// <summary>
		/// 使用者の取得
		/// </summary>
		/// <returns>使用者</returns>
		public static string GetPerson()
		{
			if (null != UserInfo)
			{
				return UserInfo.fUsrName;
			}
			return ProgramVersion;
		}

		/// <summary>
		/// 契約情報ListView設定値の取得
		/// </summary>
		/// <returns>ListView設定値</returns>
		public static string[] GetHeaderListViewItem(T_HARD_SUBSC_HEADER header)
		{
			string[] item = new string[10];
			item[0] = header.ContractNo;
			item[1] = (header.OrderDate.HasValue) ? header.OrderDate.Value.ToShortDateString() : "";
			item[2] = header.Months.ToString();
			item[3] = string.Format("\\{0}", StringUtil.CommaEdit(header.MonthlyAmount));
			item[4] = (header.ContractStartDate.HasValue) ? header.ContractStartDate.Value.ToShortDateString() : "";
			item[5] = (header.ContractEndDate.HasValue) ? header.ContractEndDate.Value.ToShortDateString() : "";
			item[6] = (header.BillingStartDate.HasValue) ? header.BillingStartDate.Value.ToShortDateString() : "";
			item[7] = (header.BillingEndDate.HasValue) ? header.BillingEndDate.Value.ToShortDateString() : "";
			item[8] = (header.CancelDate.HasValue) ? header.CancelDate.Value.ToShortDateString() : "";
			item[9] = (header.ServiceEndFlag) ? "終了" : "";
			return item;
		}

		/// <summary>
		/// 機器情報ListView設定値の取得
		/// </summary>
		/// <param name="line">行番号</param>
		/// <param name="detail">機器情報</param>
		/// <returns>ListView設定値</returns>
		public static string[] GetDetailListViewItem(int line, T_HARD_SUBSC_DETAIL detail)
		{
			string[] item = new string[8];
			item[0] = line.ToString();
			item[1] = detail.GoodsCode;
			item[2] = detail.GoodsName;
			item[3] = detail.CategoryName;
			item[4] = detail.Quantity.ToString();
			item[5] = detail.SerialNo;
			item[6] = detail.ScanFilename;
			item[7] = detail.AssetsCode;
			return item;
		}

		/// <summary>
		/// カテゴリの取得
		/// </summary>
		/// <param name="CategoryNo">カテゴリ番号</param>
		/// <returns>カテゴリ</returns>
		public static string GetCategoryName(int CategoryNo)
		{
			if (null == gCategoryList)
			{
				try
				{
					gCategoryList = new List<Tuple<int, string>>();
					List<vMicPCA区分マスタ> list = JunpDatabaseAccess.Select_vMicPCA区分マスタ("[ems_id] = 23", "ems_kbn", gSettings.ConnectJunp.ConnectionString);
					if (null != list && 0 < list.Count)
					{
						foreach (vMicPCA区分マスタ pca in list)
						{
							gCategoryList.Add(new Tuple<int, string>(pca.ems_kbn, pca.ems_str));
						}
					}
				}
				catch
				{
					return string.Empty;
				}
			}
			return gCategoryList.Find(p => p.Item1 == CategoryNo).Item2;
		}
	}
}
