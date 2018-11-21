//
// MainForm.cs
//
// PC安心サポート管理 メイン画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/11/19 勝呂)
// 
using MwsLib.BaseFactory.PcSupportManager;
using MwsLib.Common;
using MwsLib.DB.SqlServer.PcSupportManager;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PcSupportManager.Settings;

namespace PcSupportManager.Forms
{
	/// <summary>
	/// ＰＣ安心サポート管理 メイン画面
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// 拠店営業員情報リスト
		/// </summary>
		public static List<BranchEmployeeInfo> gBranchEmployeeList;

		/// <summary>
		/// 商品情報リスト
		/// </summary>
		public static List<PcSupportGoodsInfo> gPcSupportGoodsList;

		/// <summary>
		/// メールアドレスリスト
		/// </summary>
		public static List<Tuple<int, string>> gMailAddressList;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			try
			{
				gBranchEmployeeList = PcSupportManagerAccess.GetBranchEmployeeInfo();
				gPcSupportGoodsList = PcSupportManagerAccess.GetPcSupportGoodsInfo();
				gMailAddressList = PcSupportManagerAccess.GetCustomerMailAddress();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "読込エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}
		}

		/// <summary>
		/// システム日付の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dateTimePickerSystemDate_ValueChanged(object sender, EventArgs e)
		{
			Program.SystemDate = new Date(dateTimePickerSystemDate.Value);
		}

		/// <summary>
		/// 管理情報登録
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonManagement_Click(object sender, EventArgs e)
		{
			using (ManagementForm form = new ManagementForm())
			{
				form.ShowDialog();
			}
		}

		/// <summary>
		/// メール送信
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSendMail_Click(object sender, EventArgs e)
		{
			using (SendMailForm form = new SendMailForm())
			{
				form.ShowDialog();
			}
		}

		/// <summary>
		/// 製品サポート情報ソフト保守更新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSoftMainte_Click(object sender, EventArgs e)
		{
			using (SoftMainteForm form = new SoftMainteForm())
			{
				form.ShowDialog();
			}
		}

		/// <summary>
		/// 閉じる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonClose_Click(object sender, EventArgs e)
		{
#if false
			PcSupportManagerSettings settings = new PcSupportManagerSettings();
			settings.NationalHoliday.Add("2018,0101,0211,0429,0503,0504,0505,0923,1103,1123,1223");
			settings.NationalHoliday.Add("2019,0101,0211,0429,0430,0501,0502,0503,0504,0505,0923,1103,1123,1223");
			settings.NationalHoliday.Add("2020,0101,0211,0429,0503,0504,0505,0923,1103,1123,1223");
			settings.HappyMonday.Add("2003,0102,0703,0903,1002");
			settings.SpecialHoliday.Add("2018,0102,0103,0104,0813,0814,0815,0816,0817,1230,1231");
			settings.SpecialHoliday.Add("2019,0102,0103,0104,0812,0813,0814,0815,0816,1230,1231");
			settings.SpecialHoliday.Add("2020,0102,0103,0104,1230,1231");
			settings.WeeklyHoliday = "1,0,0,0,0,0,1";
			settings.ExecDay = 10;
			PcSupportManagerSettingsIF.SetPcSupportManagerSettings(settings);
#endif
			base.Close();
		}

		/// <summary>
		/// 顧客Noからメールアドレスの取得
		/// </summary>
		/// <param name="customerNo">顧客No</param>
		/// <returns>メールアドレス</returns>
		public static string GetCustomerMailAddress(int customerNo)
		{
			Tuple<int, string> mail = gMailAddressList.Find(p => p.Item1 == customerNo);
			if (null != mail)
			{
				return mail.Item2;
			}
			return string.Empty;
		}
	}
}
