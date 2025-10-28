//
// InputCustomerNoForm.cs
//
// 顧客No入力画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/10/20 勝呂)
// 
using System;
using System.Windows.Forms;

namespace HardSubscManager.Forms
{
	public partial class InputCustomerNoForm : Form
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo { get; private set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public InputCustomerNoForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			int customerNo = numericTextBoxCustomerNo.ToInt();
			if (customerNo < 1 || 99999999 < customerNo)
			{
				MessageBox.Show("顧客Noが正しく入力されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			CustomerNo = customerNo;
			DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
