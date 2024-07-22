//
// MainForm.cs
// 
// メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/06/11 勝呂):新規作成
//
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MwsServiceCancelTool.Forms
{
	/// <summary>
	/// メイン画面クラス
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// 顧客情報
		/// </summary>
		public vMic顧客情報 CustomerInfo { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			CustomerInfo = null;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			this.Text = string.Format("{0}  ({1})", Program.ProcName, Program.gSettings.ConnectCharlie.InstanceName);

#if DEBUG
			// おまとめプラン 利用申込取消：10000082

			// PC安心サポート 利用申込取消
			// PC安心サポート：10037770
			// PC安心サポートPlus：10000082

			// PC安心サポート 自動更新後の終了処理
			// 1年				2024/04～2025/03 ✖：20024179
			// 1年+更新		2023/07～2025/06 1年：10025484
			// 1年+更新x2	2022/07～2025/06 更新：20005682
			// 3年				2022/04～2025/03 ✖：10027230
			// 3年+更新		2021/07～2025/06 3年：20009855
			// 3年+更新x2	2020/07～2025/06 更新：20018105

			// PC安心サポートPlus 自動更新後の終了処理
			// 1年				2024/04～2025/03 ✖：20015174
			// 1年+更新		2023/07～2025/06 1年：20023024
			// 1年+更新x2	2022/07～2025/06 更新：10022369
			// 3年				2022/04～2025/03 ✖：20021256
			// 3年+更新		2021/07～2025/06 3年：20009601
			// 3年+更新x2	2020/07～2025/06 更新：10002207

			// オンライン請求作業済申請 利用申込取消：10075375

			// セット割サービス 利用申込取消
			// 〇：10000081
			// ✖：10039002

			textBoxCustomerNo.Text = "10000081";
#endif
		}

		/// <summary>
		/// 検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSearch_Click(object sender, EventArgs e)
		{
			int customerNo = textBoxCustomerNo.Text.ToInt();
			if (MwsDefine.CustomerNoLength != customerNo.ToString().Length)
			{
				CustomerInfo = null;
				labelCustomerName.Text = string.Empty;
				return;
			}
			// 顧客名の取得
			List<vMic顧客情報> userList = JunpDatabaseAccess.Select_vMic顧客情報(string.Format("顧客No = {0}", customerNo), "", Program.gSettings.ConnectJunp.ConnectionString);
			if (null != userList && 0 < userList.Count)
			{
				CustomerInfo = userList[0];
				labelCustomerName.Text = userList[0].顧客名;
			}
			else
			{
				CustomerInfo = null;
				labelCustomerName.Text = string.Empty;
			}
		}

		/// <summary>
		/// おまとめプラン 利用申込取消
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonMatomeCancel_Click(object sender, EventArgs e)
		{
			if (null != CustomerInfo)
			{
				using (MatomeCancelForm dlg = new MatomeCancelForm())
				{
					dlg.SetCustomerInfo(CustomerInfo);
					dlg.ShowDialog();
				}
			}
		}

		/// <summary>
		/// PC安心サポート 利用申込取消
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonPcSupportCancel_Click(object sender, EventArgs e)
		{
			if (null != CustomerInfo)
			{
				using (PcSupportCancelForm dlg = new PcSupportCancelForm())
				{
					dlg.SetCustomerInfo(CustomerInfo);
					dlg.ShowDialog();
				}
			}
		}

		/// <summary>
		/// PC安心サポート 自動更新後の終了処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonPcSupportEnd_Click(object sender, EventArgs e)
		{
			if (null != CustomerInfo)
			{
				using (PcSupportEndForm dlg = new PcSupportEndForm())
				{
					dlg.SetCustomerInfo(CustomerInfo);
					dlg.ShowDialog();
				}
			}
		}

		/// <summary>
		/// オンライン請求作業済申請 利用申込取消
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOnlineDemandCancel_Click(object sender, EventArgs e)
		{
			if (null != CustomerInfo)
			{
				using (OnlineDemandCancelForm dlg = new OnlineDemandCancelForm())
				{
					dlg.SetCustomerInfo(CustomerInfo);
					dlg.ShowDialog();
				}
			}
		}

		/// <summary>
		/// セット割サービス 利用申込取消
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSetServiceCancel_Click(object sender, EventArgs e)
		{
			if (null != CustomerInfo)
			{
				using (SetServiceCancelForm dlg = new SetServiceCancelForm())
				{
					dlg.SetCustomerInfo(CustomerInfo);
					dlg.ShowDialog();
				}
			}
		}
	}
}
