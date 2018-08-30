using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MwsLib.Common;
using MwsLib.DB.SqlServer.EntryFinishedUser;
using MwsLib.BaseFactory.EntryFinishedUser;

namespace EntryFinishedUser
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// データベース接続先 CT環境
		/// </summary>
		private const bool DATABACE_ACCEPT_CT = true;

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
			// 終了月コンボボックスの設定
			YearMonth month = Date.Today.ToYearMonth();
			for (int i = 0; i < 19; i++)
			{
				comboBoxFinishedMonth.Items.Add(month++);
			}
			// リプレース
			List<string> replace = EntryFinishedUserDataAccess.GetReplaceMakerList(DATABACE_ACCEPT_CT);
			if (null != replace)
			{
				comboBoxReplace.Items.AddRange(replace.ToArray());
			}
		}

		/// <summary>
		/// 検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSearch_Click(object sender, EventArgs e)
		{
			if (0 == textBoxTokuisakiID.Text.Length)
			{
				MessageBox.Show("得意先Noを入力してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				textBoxTokuisakiID.Focus();
				return;
			}
			EntryFinishedUserData data = EntryFinishedUserDataAccess.GetEntryFinishedUserData(textBoxTokuisakiID.Text, DATABACE_ACCEPT_CT);
			if (null == data)
			{
				MessageBox.Show("該当顧客がいません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				textBoxTokuisakiID.Focus();
				return;
			}
			// 顧客名の設定
			textBoxUserName.Text = data.UserName;

			// 終了月の設定
			if (data.FinishedYearMonth.HasValue)
			{
				comboBoxFinishedMonth.Text = data.FinishedYearMonth.Value.ToString();
			}
			// リプレースの設定
			comboBoxReplace.Text = data.Replace;

			// 終了事由の設定
			comboBoxFinishedReason.Text = data.FinishedReason;

			// 理由の設定
			textBoxReason.Text = data.Reason;

			// 終了届受領日の設定
			if (data.AcceptDate.HasValue)
			{
				dateTimePickerAcceptDate.Value = data.AcceptDate.Value.ToDateTime();
			}
		}

		/// <summary>
		/// クリア
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonClear_Click(object sender, EventArgs e)
		{
			textBoxTokuisakiID.Text = string.Empty;
			textBoxUserName.Text = string.Empty;
			comboBoxFinishedMonth.SelectedIndex = -1;
			dateTimePickerAcceptDate.Value = DateTime.Today;
			comboBoxReplace.SelectedIndex = -1;
			comboBoxReplace.Text = string.Empty;
			comboBoxFinishedReason.SelectedIndex = -1;
			comboBoxFinishedReason.Text = string.Empty;
			textBoxReason.Text = string.Empty;
			checkBoxNotPaletteUser.Checked = false;
		}

		/// <summary>
		/// 得意先No 入力制限
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxTokuisakiID_KeyPress(object sender, KeyPressEventArgs e)
		{
			// 0～9と、バックスペース以外の時は、イベントをキャンセルする
			if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
			{
				e.Handled = true;
			}
		}

		/// <summary>
		/// 登録
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (0 == textBoxTokuisakiID.Text.Length)
			{
				MessageBox.Show("得意先Noを入力してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				textBoxTokuisakiID.Focus();
				return;
			}
			YearMonth finishedYearMonth;
			if (null != comboBoxFinishedMonth.SelectedItem)
			{
				finishedYearMonth = (YearMonth)comboBoxFinishedMonth.SelectedItem;
			}
			else
			{
				if (false == YearMonth.TryParse(comboBoxFinishedMonth.Text, out finishedYearMonth))
				{
					MessageBox.Show("終了月が正しく入力されていません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			EntryFinishedUserData data = EntryFinishedUserDataAccess.GetEntryFinishedUserData(textBoxTokuisakiID.Text, DATABACE_ACCEPT_CT);
			if (null == data)
			{
				// 新規追加
				data = new EntryFinishedUserData();
				data.TokuisakiNo = textBoxTokuisakiID.Text;
				data.FinishedYearMonth = finishedYearMonth;
				data.AcceptDate = new Date(dateTimePickerAcceptDate.Value);
				data.FinishedReason = comboBoxFinishedReason.SelectedItem as string;
				data.Replace = comboBoxReplace.Text;
				data.Reason = textBoxReason.Text;
				EntryFinishedUserDataAccess.InsertIntoEntryFinishedUserData(DATABACE_ACCEPT_CT, data);
			}
			else
			{
				// 更新
				data.TokuisakiNo = textBoxTokuisakiID.Text;
				data.FinishedYearMonth = finishedYearMonth;
				data.AcceptDate = new Date(dateTimePickerAcceptDate.Value);
				data.FinishedReason = comboBoxFinishedReason.SelectedItem as string;
				data.Replace = comboBoxReplace.Text;
				data.Reason = textBoxReason.Text;
				EntryFinishedUserDataAccess.UpdateEntryFinishedUserData(DATABACE_ACCEPT_CT, data);
			}
			MessageBox.Show("登録しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			if (DialogResult.Yes == MessageBox.Show("メモ登録をします", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
			{
				EntryFinishedUserDataAccess.InsertIntoMemo(DATABACE_ACCEPT_CT, data);

				MessageBox.Show("メモ登録をしました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("メモ登録をキャンセルしました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// リスト参照
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonList_Click(object sender, EventArgs e)
		{
			List<EntryFinishedUserData> list = EntryFinishedUserDataAccess.GetEntryFinishedUserDataList(DATABACE_ACCEPT_CT);
			if (null != list && 0 < list.Count)
			{
				using (ShowListForm form = new ShowListForm(list))
				{
					form.ShowDialog();
				}
			}
		}

		/// <summary>
		/// 閉じる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
