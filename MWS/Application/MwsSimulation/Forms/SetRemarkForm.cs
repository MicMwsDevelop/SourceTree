//
// SetRemarkForm.cs
//
// 備考入力画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
// 
using System;
using System.Windows.Forms;

namespace MwsSimulation.Forms
{
	public partial class SetRemarkForm : Form
	{
		/// <summary>
		/// 備考
		/// </summary>
		public string Remark { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SetRemarkForm()
		{
			InitializeComponent();

			Remark = string.Empty;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="str">備考</param>
		public SetRemarkForm(string str)
		{
			InitializeComponent();

			Remark = str;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SetRemarkForm_Load(object sender, EventArgs e)
		{
			textBoxRemark.Text = Remark;
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (0 == textBoxRemark.Text.Length)
			{
				MessageBox.Show(this, "備考を入力してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				textBoxRemark.Focus();
				return;
			}
			Remark = textBoxRemark.Text;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
