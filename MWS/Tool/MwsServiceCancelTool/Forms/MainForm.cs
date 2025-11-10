//
// MainForm.cs
// 
// メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2025/01/23 勝呂):新規作成
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
			/////////////////////
			// TESTSV2

			// おまとめプラン
			//textBoxCustomerNo.Text = "10000082";

			// PC安心サポート
			//textBoxCustomerNo.Text = "10037770";

			// PC安心サポートPlus
			//textBoxCustomerNo.Text = "10000082";

			// PC安心サポート 自動更新後の終了処理
			// ✖1年 2024/04～2025/03
			//textBoxCustomerNo.Text = "20024179";
			// 〇1年+更新 2023/07～2025/06
			//textBoxCustomerNo.Text = "10025484";
			// 〇1年+更新x2 2022/07～2025/06
			//textBoxCustomerNo.Text = "20005682";
			// ✖3年 2022/04～2025/03
			//textBoxCustomerNo.Text = "10027230";
			// 〇3年+更新 2021/07～2025/06
			//textBoxCustomerNo.Text = "20009855";
			// 〇3年+更新x2 2020/07～2025/06
			//textBoxCustomerNo.Text = "20018105";

			// PC安心サポートPlus 自動更新後の終了処理
			// ✖1年 2024/04～2025/03
			//textBoxCustomerNo.Text = "20015174";
			// 〇1年+更新 2023/07～2025/06
			//textBoxCustomerNo.Text = "20023024";
			// 〇1年+更新x2 2022/07～2025/06
			//textBoxCustomerNo.Text = "10022369";
			// ✖3年 2022/04～2025/03
			//textBoxCustomerNo.Text = "20021256";
			// 〇3年+更新 2021/07～2025/06
			//textBoxCustomerNo.Text = "20009601";
			// 〇3年+更新x2 2020/07～2025/06
			//textBoxCustomerNo.Text = "10002207";

			// オンライン請求作業済申請 利用申込取消
			textBoxCustomerNo.Text = "10075375";

			// セット割サービス
			// 〇
			//textBoxCustomerNo.Text = "10000081";
			// ✖
			//textBoxCustomerNo.Text = "10039002";


			/////////////////////
			// SQLSV

			// おまとめプラン
			//textBoxCustomerNo.Text = "20008604";

			// PC安心サポート
			//textBoxCustomerNo.Text = "10000822";

			// PC安心サポートPlus
			//textBoxCustomerNo.Text = "20011827";

			// オンライン請求作業済申請
			// 〇
			//textBoxCustomerNo.Text = "10001330";
			// ×
			//textBoxCustomerNo.Text = "10003080";

			// PC安心サポート 自動更新後の終了処理
			// ✖1年 2024/04～2025/03
			//textBoxCustomerNo.Text = "20024179";
			// 〇1年+更新 2024/02～2026/01
			//textBoxCustomerNo.Text = "20023972";
			// 〇1年+更新x2 2022/07～2025/06
			//textBoxCustomerNo.Text = "10022369";
			// ✖3年 2022/04～2025/03
			//textBoxCustomerNo.Text = "20021256";
			// 〇3年+更新 2021/07～2025/06
			//textBoxCustomerNo.Text = "20009601";
			// 〇3年+更新x2 2020/07～2025/06
			//textBoxCustomerNo.Text = "10002207";

			// セット割サービス
			// ✖
			//textBoxCustomerNo.Text = "10003170";
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
			else
			{
				MessageBox.Show("顧客が設定されていません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
			else
			{
				MessageBox.Show("顧客が設定されていません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
			else
			{
				MessageBox.Show("顧客が設定されていません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
			else
			{
				MessageBox.Show("顧客が設定されていません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
			else
			{
				MessageBox.Show("顧客が設定されていません。", Program.ProcName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}
	}
}
