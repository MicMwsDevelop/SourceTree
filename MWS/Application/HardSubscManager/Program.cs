//
// Program.cs
//
// ハードサブスク情報管理プログラムクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/04/03 勝呂)
// 
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Junp.Table;
using HardSubscManager.Settings;
using System;
using System.Windows.Forms;

namespace HardSubscManager
{
	internal static class Program
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string ProgramName = "ハードサブスク情報管理";

		/// <summary>
		/// プログラムバージョン
		/// </summary>
		public const string ProgramVersion = "Ver1.00 2025/04/03";

		/// <summary>
		/// [JunpDB].[dbo].[tUser]
		/// </summary>
		public static tUser UserInfo { get; set; }

		/// <summary>
		/// 環境設定
		/// </summary>
		public static HardSubscManagerSettings gSettings { get; set; }

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			UserInfo = null;

			Application.Run(new MainForm());
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
		public static string[] GetHeaderListViewItem(T_HARDSUBSC_HEADER header)
		{
			string[] item = new string[11];
			item[0] = header.RentalNo.ToString();
			item[1] = (header.ApplyDate.HasValue) ? header.ApplyDate.Value.ToString() : "";
			item[2] = header.Months.ToString();
			item[3] = header.TotalAmount.ToString();
			item[4] = header.MonthlyAmount.ToString();
			item[5] = (header.ContractStartDate.HasValue) ? header.ContractStartDate.Value.ToShortDateString() : "";
			item[6] = (header.ContractEndDate.HasValue) ? header.ContractEndDate.Value.ToShortDateString() : "";
			item[7] = (header.BillingStartDate.HasValue) ? header.BillingStartDate.Value.ToShortDateString() : "";
			item[8] = (header.BillingEndDate.HasValue) ? header.BillingEndDate.Value.ToShortDateString() : "";
			item[9] = (header.CancelDate.HasValue) ? header.CancelDate.Value.ToShortDateString() : "";
			item[10] = (header.CancelApplyDate.HasValue) ? header.CancelApplyDate.Value.ToString() : "";
			return item;
		}

		/// <summary>
		/// 機器情報ListView設定値の取得
		/// </summary>
		/// <param name="line">行番号</param>
		/// <param name="detail">機器情報</param>
		/// <returns>ListView設定値</returns>
		public static string[] GetDetailListViewItem(int line, T_HARDSUBSC_DETAIL detail)
		{
			string[] item = new string[7];
			item[0] = line.ToString();
			item[1] = detail.GoodsCode;
			item[2] = detail.GoodsName;
			item[3] = detail.GetCategoryName();
			item[4] = detail.Quantity.ToString();
			item[5] = detail.AssetsCode;
			item[6] = detail.SerialNo;
			return item;
		}
	}
}
