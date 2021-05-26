using System;
using System.Windows.Forms;

namespace ProspectProgressAutoAggregate
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
		/// FormLoad
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			this.Text = string.Format("{0}  {1}", this.Text,Program.VersionStr);
		}

		/// <summary>
		/// 自動集計
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			string msg;
			if (0 != Program.AutoAggregate(out msg))
			{
				MessageBox.Show(this, msg, "Excelファイル書込エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
