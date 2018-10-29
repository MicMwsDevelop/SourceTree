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

namespace PcSafetySupport.Forms
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// ＰＣ安心サポート管理ツール
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
			dateTimePickerSystemDate.Value = Program.SystemDate.ToDateTime();
		}

		/// <summary>
		/// システム日付変更
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
		private void buttonControl_Click(object sender, EventArgs e)
		{
			using (PcSupportControlListForm form = new PcSupportControlListForm())
			{
				form.ShowDialog();
			}
		}

		/// <summary>
		/// 送信メール情報
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonMale_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 日時処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonDaily_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 月次処理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonMonthly_Click(object sender, EventArgs e)
		{

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
