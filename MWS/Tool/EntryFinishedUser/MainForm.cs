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
			YearMonth month = Date.Today.ToYearMonth();
			for (int i = 0; i < 19; i++)
			{
				comboBoxFinishedMonth.Items.Add(month++);
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
			EntryFinishedUserData data = EntryFinishedUserDataAccess.GetEntryFinishedUserData(textBoxTokuisakiID.Text, true);
			if (null == data)
			{
				MessageBox.Show("該当顧客がいません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				textBoxTokuisakiID.Focus();
				return;
			}
			textBoxUserName.Text = data.UserName;
			comboBoxReplace.Text = data.Replace;
			comboBoxFinishedReason.Text = data.FinishedReason;
			textBoxReason.Text = data.Reason;
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
	}
}
