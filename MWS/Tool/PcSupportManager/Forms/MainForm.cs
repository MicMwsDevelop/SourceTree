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

namespace PcSupportManager.Forms
{
	/// <summary>
	/// ＰＣ安心サポート管理ツール メイン画面
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// プログラム名
		/// </summary>
		public const string PROGRAM_NAME = "PC安心サポート管理ツール";

		/// <summary>
		/// 拠店営業員情報リスト
		/// </summary>
		public static List<BranchInfo> gBranchList;

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
				gBranchList = PcSupportManagerAccess.GetBranchEmployeeInfo();
				gPcSupportGoodsList = PcSupportManagerAccess.GetPcSupportGoodsInfo();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "読込エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}
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
	}
}
