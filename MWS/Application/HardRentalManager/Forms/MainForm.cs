using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HardRentalManager.Forms
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// ハードサブスク契約管理
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonManager_Click(object sender, EventArgs e)
		{
			using (ManagerForm form = new ManagerForm())
			{
				form.ShowDialog();
			}
		}

		/// <summary>
		/// 利用期限通知
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonNotify_Click(object sender, EventArgs e)
		{
			
		}
	}
}
