using MwsLib.BaseFactory.EntryFinishedUser;
using MwsLib.Common;
using MwsLib.DB.SqlServer.EntryFinishedUser;
using System;
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
		private EntryFinishedUserForm()
		{
			InitializeComponent();

			FinishedUser = null;
			ModifyFlag = false;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="user">終了ユーザー情報</param>
		/// <param name="modify">更新フラグ</param>
		public EntryFinishedUserForm(EntryFinishedUserData user, bool modify)
		{
			InitializeComponent();

			FinishedUser = user;
			ModifyFlag = modify;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			// リプレース
			comboBoxReplace.Items.AddRange(MainForm.gReplaceList.ToArray());

			// 得意先Noの設定
			textBoxTokuisakiID.Text = FinishedUser.TokuisakiNo;

			// 顧客名の設定
			textBoxUserName.Text = FinishedUser.UserName;

			// 終了月の設定
			if (FinishedUser.FinishedYearMonth.HasValue)
			{
				dateTimePickerFinishedYearMonth.Checked = true;
				dateTimePickerFinishedYearMonth.Value = FinishedUser.FinishedYearMonth.Value.First.ToDateTime();
			}
			// リプレースの設定
			comboBoxReplace.Text = FinishedUser.Replace;

			// 終了事由の設定
			comboBoxFinishedReason.Text = FinishedUser.FinishedReason;

			// 理由の設定
			textBoxReason.Text = FinishedUser.Reason;

			// 終了届受領日の設定
			if (FinishedUser.AcceptDate.HasValue)
			{
				dateTimePickerAcceptDate.Checked = true;
				dateTimePickerAcceptDate.Value = FinishedUser.AcceptDate.Value.ToDateTime();
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
			FinishedUser.FinishedReason = comboBoxFinishedReason.SelectedItem as string;

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
					EntryFinishedUserAccess.UpdateEntryFinishedUser(FinishedUser, Program.DATABACE_ACCEPT_CT);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "終了ユーザー情報更新エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
			}
			else
			{
				// 新規追加
				try
				{
					EntryFinishedUserAccess.InsertIntoEntryFinishedUser(FinishedUser, Program.DATABACE_ACCEPT_CT);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "終了ユーザー情報追加エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				}
			}
			MessageBox.Show("登録しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

			if (DialogResult.Yes == MessageBox.Show("メモ登録をします", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				try
				{
					EntryFinishedUserAccess.InsertIntoMemo(FinishedUser, Program.DATABACE_ACCEPT_CT);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "メモ登録エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
