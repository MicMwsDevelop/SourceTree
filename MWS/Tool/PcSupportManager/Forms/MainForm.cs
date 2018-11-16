using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MwsLib.DB.SqlServer.PcSupportManager;
using MwsLib.BaseFactory.PcSupportManager;
using MwsLib.Common;

namespace PcSupportManager.Forms
{
	/// <summary>
	/// ＰＣ安心サポート管理ツール メイン画面
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// 拠店営業員情報リスト
		/// </summary>
		public static List<BranchEmployeeInfo> gBranchEmployeeList;

		/// <summary>
		/// 商品情報リスト
		/// </summary>
		public static List<PcSupportGoodsInfo> gPcSupportGoodsList;

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
			try
			{
				gBranchEmployeeList = PcSupportManagerAccess.GetBranchEmployeeInfo();
				gPcSupportGoodsList = PcSupportManagerAccess.GetPcSupportGoodsInfo();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "読込エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}
		}

		/// <summary>
		/// システム日付の変更
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
		private void buttonManagement_Click(object sender, EventArgs e)
		{
			using (ManagementForm form = new ManagementForm())
			{
				form.ShowDialog();
			}
		}

		/// <summary>
		/// メール送信
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSendMail_Click(object sender, EventArgs e)
		{
			using (SendMailForm form = new SendMailForm())
			{
				form.ShowDialog();
			}
		}

		/// <summary>
		/// ソフト保守メンテナンス情報更新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSoftMainte_Click(object sender, EventArgs e)
		{
			using (SoftMainteForm form = new SoftMainteForm())
			{
				form.ShowDialog();
			}
		}

		/// <summary>
		/// 閉じる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonClose_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
