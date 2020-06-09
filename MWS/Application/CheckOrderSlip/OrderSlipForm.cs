//
// OrderSlipForm.cs
//
// 伝票情報画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/04/17 勝呂)
// 
using MwsLib.BaseFactory.Junp.CheckOrderSlip;
using System;
using System.Windows.Forms;

namespace CheckOrderSlip
{
	public partial class OrderSlipForm : Form
	{
		/// <summary>
		/// 伝票情報
		/// </summary>
		public OrderSlipData Order {get;set;}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public OrderSlipForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OrderSlipForm_Load(object sender, EventArgs e)
		{
			textBoxOrderNo.Text = Order.受注番号.ToString();
			textBoxCustomerNo.Text = Order.顧客No.ToString();
			textBoxCustomerName.Text = Order.顧客名;
			textBoxTitle.Text = Order.件名;
		}
	}
}
