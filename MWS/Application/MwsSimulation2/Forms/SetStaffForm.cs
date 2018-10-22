//
// SetStaffForm.cs
//
// 担当者入力画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
// 
using System;
using System.Windows.Forms;

namespace MwsSimulation.Forms
{
	public partial class SetStaffForm : Form
	{
		/// <summary>
		/// 担当者名
		/// </summary>
		public string StaffName { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SetStaffForm()
		{
			InitializeComponent();

			StaffName = string.Empty;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SetStaffForm(string name)
		{
			InitializeComponent();

			StaffName = name;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SetStaffForm_Load(object sender, EventArgs e)
		{
			textBoxName.Text = StaffName;
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (0 == textBoxName.Text.Length)
			{
				MessageBox.Show(this, "担当者を入力してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				textBoxName.Focus();
				return;
			}
			StaffName = textBoxName.Text;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
