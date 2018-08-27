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
				comboBoxEndMonth.Items.Add(month++);
			}
		}

		/// <summary>
		/// 検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSearch_Click(object sender, EventArgs e)
		{

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
			comboBoxEndMonth.SelectedIndex = -1;
			dateTimePickerAcceptDate.Value = DateTime.Today;
			comboBoxReplace.SelectedIndex = -1;
			comboBoxReplace.Text = string.Empty;
			comboBoxEndReason.SelectedIndex = -1;
			comboBoxEndReason.Text = string.Empty;
			textBoxReason.Text = string.Empty;
			checkBoxNotPaletteUser.Checked = false;
		}
	}
}
