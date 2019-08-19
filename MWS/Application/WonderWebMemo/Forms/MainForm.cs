using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WonderWebMemo.Forms
{
	public partial class MainForm : Form
	{
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
			if (Program.DATABACE_ACCEPT_CT)
			{
				this.Text = this.Text + "（ＣＴ環境）";
			}

		}

		/// <summary>
		/// 銀行振込請求書発行先
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonBankTransfer_Click(object sender, EventArgs e)
		{
			using (BankTransferForm form = new BankTransferForm())
			{
				form.ShowDialog();
			}
		}
	}
}
