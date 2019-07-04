//
// EntryFinishedUserForm.cs
//
// 終了ユーザー登録画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/06/28 勝呂)
// 
using MwsLib.BaseFactory.EntryFinishedUser;
using MwsLib.BaseFactory.Junp.Table;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Junp;
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
					JunpDatabaseAccess.UpdateSet_tMic終了ユーザーリスト(FinishedUser.To_tMic終了ユーザーリスト(), Program.DATABACE_ACCEPT_CT);
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
					JunpDatabaseAccess.InsertInto_tMic終了ユーザーリスト(FinishedUser.To_tMic終了ユーザーリスト(), Program.DATABACE_ACCEPT_CT);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "終了ユーザー情報追加エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return;
				}
			}
			MessageBox.Show("登録しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

			if (DialogResult.Yes == MessageBox.Show(string.Format("メモ登録をしますか\n\n{0}", FinishedUser.GetMemoString()), "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				try
				{
					tMemo memo = FinishedUser.To_tMemo();
					JunpDatabaseAccess.InsertInto_tMemo(memo, Program.DATABACE_ACCEPT_CT);
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
			base.DialogResult = DialogResult.OK;
		}
	}
}
