//
// DestinationForm.cs
//
// 宛先設定画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
// 
using System;
using System.Windows.Forms;

namespace MwsSimulation.Forms
{
	/// <summary>
	/// 宛先設定画面
	/// </summary>
	public partial class DestinationForm : Form
	{
		/// <summary>
		/// 宛先
		/// </summary>
		public string Destination { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public DestinationForm()
		{
			InitializeComponent();

			Destination = string.Empty;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="destination">宛先</param>
		public DestinationForm(string destination)
		{
			InitializeComponent();

			Destination = destination;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EstimateNameForm_Load(object sender, EventArgs e)
		{
			textBoxDestination.Text = Destination;
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			Destination = textBoxDestination.Text;
		}
	}
}
