using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MwsLib.BaseFactory.Junp.Table;
using MwsLib.DB.SqlServer.Junp;

namespace UserDataManager.Forms
{
	/// <summary>
	/// メイン画面
	/// </summary>
	public partial class MainForm : Form
	{
		private List<tMic社内データ管理ヘッダ> HeaderList;
		private List<tMic社内データ管理利用部署情報> BranchList;


		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			HeaderList = null;
			BranchList = null;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			BranchList = JunpDatabaseAccess.Select_tMic社内データ管理利用部署情報("", "ID", Program.DATABACE_ACCEPT_CT);
			HeaderList = JunpDatabaseAccess.Select_tMic社内データ管理ヘッダ("fステータス Not Like '削除済' And fステータス Not Like '終了'", "f作業No", Program.DATABACE_ACCEPT_CT);

			var table = HeaderList.Join(BranchList, Busho1 => f部署コード1, f部署コード2, f部署コード3)
						join b in BranchList on new { h.f部署コード1, h.f部署コード2, h.f部署コード3 } equals new { b.Busho1, b.Busho2, b.Busho3 }
						select new { 利用部署名 = b.利用部署名 };

		/// <summary>
		/// 抽出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button抽出_Click(object sender, EventArgs e)
		{
			textBox検索文字列.Text = string.Empty;
		}

		/// <summary>
		/// 検索
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button検索_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// クリア
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonクリア_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 新規登録
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button新規登録_Click(object sender, EventArgs e)
		{
			using(AddNewForm dlg = new AddNewForm())
			{
				dlg.ShowDialog();
			}
		}

		/// <summary>
		/// 編集
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button編集_Click(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// 終了
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button終了_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
