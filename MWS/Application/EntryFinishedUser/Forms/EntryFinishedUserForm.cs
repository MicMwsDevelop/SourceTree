//
// EntryFinishedUserForm.cs
//
// 終了ユーザー登録画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.EntryFinishedUser;
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace EntryFinishedUser.Forms
{
	/// <summary>
	/// 終了ユーザー登録画面
	/// </summary>
	public partial class EntryFinishedUserForm : Form
	{
		/// <summary>
		/// 終了ユーザー情報
		/// </summary>
		public EntryFinishedUserData FinishedUser;

		/// <summary>
		/// 更新フラグ
		/// </summary>
		public bool ModifyFlag;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public EntryFinishedUserForm()
		{
			InitializeComponent();

			FinishedUser = null;
			ModifyFlag = false;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EntryFinishedUserForm_Load(object sender, EventArgs e)
		{
			// リプレース
			List<string> makerList = new List<string>();
			foreach (tMikコードマスタ code in Program.gReplaceList)
			{
				makerList.Add(code.fcm名称);
			}
			comboBoxReplace.Items.AddRange(makerList.ToArray());

			// 得意先Noの設定
			textBoxTokuisakiID.Text = FinishedUser.TokuisakiNo;

			// 顧客Noの設定
			textBoxCustomerNo.Text = FinishedUser.CustomerID.ToString();

			// 顧客名の設定
			textBoxUserName.Text = FinishedUser.UserName;

			// 終了月の設定
			if (FinishedUser.FinishedYearMonth.HasValue)
			{
				dateTimePickerFinishedYearMonth.Checked = true;
				dateTimePickerFinishedYearMonth.Value = FinishedUser.FinishedYearMonth.Value.First.ToDateTime();
			}
			else
			{
				dateTimePickerFinishedYearMonth.Value = Program.gSystemDate.ToDateTime();
			}
			// 終了事由の設定
			comboBoxFinishedReason.Text = FinishedUser.FinishedReason;

			// リプレースの設定
			comboBoxReplace.Text = FinishedUser.Replace;

			// 理由の設定
			textBoxReason.Text = FinishedUser.Reason;

			// 終了届受領日の設定
			if (FinishedUser.AcceptDate.HasValue)
			{
				dateTimePickerAcceptDate.Checked = true;
				dateTimePickerAcceptDate.Value = FinishedUser.AcceptDate.Value.ToDateTime();
			}
			else
			{
				dateTimePickerAcceptDate.Value = Program.gSystemDate.ToDateTime();
			}
			// 非paletteユーザー
			checkBoxNonPaletteUser.Checked = FinishedUser.NonPaletteUser;
		}

		/// <summary>
		/// 登録
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (false == dateTimePickerFinishedYearMonth.Checked)
			{
				MessageBox.Show("終了月が入力されていません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (false == dateTimePickerAcceptDate.Checked)
			{
				MessageBox.Show("終了届受領日が入力されていません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// 終了月の設定
			FinishedUser.FinishedYearMonth = new Date(dateTimePickerFinishedYearMonth.Value).ToYearMonth();

			// 終了届受領日の設定
			FinishedUser.AcceptDate = new Date(dateTimePickerAcceptDate.Value);

			// 終了事由の設定
			if (-1 != comboBoxFinishedReason.SelectedIndex)
			{
				FinishedUser.FinishedReason = comboBoxFinishedReason.SelectedItem as string;
			}
			else
			{
				FinishedUser.FinishedReason = string.Empty;
			}
			// リプレースの設定
			FinishedUser.Replace = comboBoxReplace.Text;

			// 理由の設定
			FinishedUser.Reason = textBoxReason.Text;

			// 非paletteユーザー
			FinishedUser.NonPaletteUser = checkBoxNonPaletteUser.Checked;

			if (ModifyFlag)
			{
				// 更新
				try
				{
					JunpDatabaseAccess.UpdateSet_tMic終了ユーザーリスト(FinishedUser.To_tMic終了ユーザーリスト(), Program.gSettings.Junp.ConnectionString);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "終了ユーザー情報更新エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return;
				}
			}
			else
			{
				// 新規追加
				try
				{
					JunpDatabaseAccess.InsertInto_tMic終了ユーザーリスト(FinishedUser.To_tMic終了ユーザーリスト(), Program.gSettings.Junp.ConnectionString);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "終了ユーザー情報追加エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return;
				}
			}
			MessageBox.Show("登録しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

			if (DialogResult.Yes == MessageBox.Show(string.Format("メモ登録をしますか\n\n{0}", FinishedUser.GetMemoPlanString()), "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				try
				{
					tMemo memo = FinishedUser.To_tMemo();
					memo.fMemMemo = FinishedUser.GetMemoPlanString();
					JunpDatabaseAccess.InsertInto_tMemo(memo, Program.gSettings.Junp.ConnectionString);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "メモ登録エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return;
				}
				MessageBox.Show("メモ登録をしました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("メモ登録をキャンセルしました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			// 契約中サービス確認
			CheckContractService();

			base.DialogResult = DialogResult.OK;
		}

		/// <summary>
		/// 契約中サービス確認
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCheckContractService_Click(object sender, EventArgs e)
		{
			if (!CheckContractService())
			{
				MessageBox.Show("現在、契約中のサービスはありません。", "契約中サービス", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		/// <summary>
		/// 契約中サービス確認
		/// </summary>
		/// <returns>契約中サービスの有無</returns>
		private bool CheckContractService()
		{
			bool exist = false;

			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			List<int> checkList = new List<int>();
			checkList.Add(FinishedUser.CustomerID);

			// ESET月額版
			List<T_LICENSE_PRODUCT_CONTRACT> esetList = Program.ContractServiceESET(checkList);
			if (null != esetList && 0 < esetList.Count)
			{
				MessageBox.Show(string.Format("{0}（{1}）のサービスが契約中です。", CharlieDatabaseAccess.GetServiceName(esetList[0].SERVICE_ID, Program.gSettings.Charlie.ConnectionString), esetList[0].SERVICE_ID), "契約中サービス", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				exist = true;
			}
			// PC安心サポート
			List<EntryFinishedUserData> finishedList = new List<EntryFinishedUserData>();
			finishedList.Add(FinishedUser);
			List<T_USE_PCCSUPPORT> pcList = Program.ContractServicePcSupport(finishedList);
			if (null != pcList && 0 < pcList.Count)
			{
				MessageBox.Show(string.Format("{0}（{1}）のサービスが契約中です。", CharlieDatabaseAccess.GetServiceName(pcList[0].fServiceId, Program.gSettings.Charlie.ConnectionString), pcList[0].fServiceId), "契約中サービス", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				exist = true;
			}
			// ナルコーム製品
			List<T_CUSSTOMER_USE_INFOMATION> cuiList = Program.ContractServiceNarcohm(checkList);
			if (null != cuiList && 0 < cuiList.Count)
			{
				MessageBox.Show(string.Format("{0}（{1}）のサービスが契約中です。", CharlieDatabaseAccess.GetServiceName(cuiList[0].SERVICE_ID, Program.gSettings.Charlie.ConnectionString), cuiList[0].SERVICE_ID), "契約中サービス", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				exist = true;
			}
			// Microsoft365製品
			cuiList = Program.ContractService365(checkList);
			if (null != cuiList && 0 < cuiList.Count)
			{
				MessageBox.Show(string.Format("{0}（{1}）のサービスが契約中です。", CharlieDatabaseAccess.GetServiceName(cuiList[0].SERVICE_ID, Program.gSettings.Charlie.ConnectionString), cuiList[0].SERVICE_ID), "契約中サービス", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				exist = true;
			}
			// Curlineクラウド
			List<int> noList = Program.ContractServiceCurlineCloud();
			if (null != noList)
			{
				int index = noList.FindIndex(p => p == FinishedUser.CustomerID);
				if (-1 != index)
				{
					MessageBox.Show(string.Format("Curlineクラウド（{0}）のサービスが契約中です。", PcaGoodsIDDefine.MwsCurlineCloud), "契約中サービス", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					exist = true;
				}
			}
			// はなはなし購読
			noList = Program.ContractServiceHanahanashi();
			if (null != noList)
			{
				int index = noList.FindIndex(p => p == FinishedUser.CustomerID);
				if (-1 != index)
				{
					MessageBox.Show(string.Format("はなはなし（{0}）のサービスが契約中です。", PcaGoodsIDDefine.Hanahanashi), "契約中サービス", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					exist = true;
				}
			}
			// 介護連携、介護伝送
			cuiList = Program.ContractServiceKaigo(checkList);
			if (null != cuiList && 0 < cuiList.Count)
			{
				MessageBox.Show(string.Format("{0}（{1}）のサービスが契約中です。", CharlieDatabaseAccess.GetServiceName(cuiList[0].SERVICE_ID, Program.gSettings.Charlie.ConnectionString), cuiList[0].SERVICE_ID), "契約中サービス", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				exist = true;
			}
			// カーソルを元に戻す
			Cursor.Current = preCursor;

			return exist;
		}
	}
}
