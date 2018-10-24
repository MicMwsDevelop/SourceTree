//
// DestinationForm.cs
//
// 宛先設定画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
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
		/// 宛先に御中ではなく様を使用
		/// </summary>
		public int NotUsedMessrs { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		private DestinationForm()
		{
			InitializeComponent();

			Destination = string.Empty;
			NotUsedMessrs = 0;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="destination">宛先</param>
		/// <param name="notUsedMessrs">様</param>
		public DestinationForm(string destination, int notUsedMessrs)
		{
			InitializeComponent();

			Destination = destination;
			NotUsedMessrs = notUsedMessrs;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EstimateNameForm_Load(object sender, EventArgs e)
		{
			textBoxDestination.Text = Destination;

			if (0 != NotUsedMessrs)
			{
				radioSama.Checked = true;
			}
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			Destination = textBoxDestination.Text;
			if (radioSama.Checked)
			{
				NotUsedMessrs = 1;
			}
			else
			{
				NotUsedMessrs = 0;
			}
		}
	}
}
