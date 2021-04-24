//
// MainForm.cs
// 
// 拠点選択画面 フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/04/22 勝呂)
//
using System;
using System.Windows.Forms;

namespace VariousDocumentOut.Forms
{
	/// <summary>
	/// 拠点選択フォーム
	/// </summary>
	public partial class SelectSatelliteForm : Form
	{
		/// <summary>
		/// 営業部名
		/// </summary>
		public string SaleDepartment { get; set; }

		/// <summary>
		/// 拠点名
		/// </summary>
		public string Branch { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SelectSatelliteForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectSateliteForm_Load(object sender, EventArgs e)
		{
			if (0 == SaleDepartment.Length)
			{
				comboBoxSaleDepartent.SelectedIndex = 0;
			}
			else
			{
				int index = comboBoxSaleDepartent.FindString(SaleDepartment);
				if (-1 != index)
				{
					comboBoxSaleDepartent.SelectedIndex = index;
				}
			}
			if (0 == Branch.Length)
			{
				comboBoxBranch.SelectedIndex = 0;
			}
			else
			{
				int index = comboBoxBranch.FindString(Branch);
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
			SaleDepartment = comboBoxSaleDepartent.Items[comboBoxSaleDepartent.SelectedIndex].ToString();
			Branch = comboBoxBranch.Items[comboBoxBranch.SelectedIndex].ToString();
			this.Close();
		}
	}
}
