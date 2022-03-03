//
// MainForm.cs
// 
// 拠点選択画面 フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/04/22 勝呂)
// Ver1.09(2022/02/10):二次キッティング依頼書 2022/02組織変更対応
// Ver1.11(2022/02/21):二次キッティング依頼書 使用廃止によりメニューから削除
//
using CommonLib.BaseFactory.Junp.Table;
using CommonLib.BaseFactory.VariousDocumentOut;
using CommonLib.DB.SqlServer.Junp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VariousDocumentOut.Forms
{
	/// <summary>
	/// 拠点選択フォーム
	/// </summary>
	public partial class SelectSatelliteForm : Form
	{
		/// <summary>
		/// 部署情報
		/// </summary>
		private List<tBusho> BushoList { get; set; }

		/// <summary>
		/// 拠点情報
		/// </summary>
		public SatelliteOffice Office { get; set; }

		/// <summary>
		/// 選択部署
		/// </summary>
		public tBusho SelectBusho { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SelectSatelliteForm()
		{
			InitializeComponent();

			BushoList = null;
			Office = null;
			SelectBusho = null;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectSateliteForm_Load(object sender, EventArgs e)
		{
			try
			{
				// 部署情報の取得
				BushoList = JunpDatabaseAccess.Select_tBusho("[fBshCode2] > '40' AND [fBshCode2] < '90'", "[fBshCode2], [fBshCode3]", Program.gSettings.Connect.Junp.ConnectionString);

				List<tBusho> officeList = BushoList.FindAll(p => p.fBshType == "3");
				foreach (tBusho office in officeList)
				{
					comboBoxBranch.Items.Add(office.fBshName3);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, string.Format("サーバー通信エラー：{0}", ex.Message), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (null== Office)
			{
				comboBoxBranch.SelectedIndex = 0;
			}
			else
			{
				int index = comboBoxBranch.FindString(Office.Branch);
				if (-1 != index)
				{
					comboBoxBranch.SelectedIndex = index;
				}
			}
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			string branch = comboBoxBranch.Items[comboBoxBranch.SelectedIndex].ToString();
			SelectBusho = BushoList.Find(p =>p.fBshName3 == branch && p.fBshType == "3");
			this.Close();
		}
	}
}
