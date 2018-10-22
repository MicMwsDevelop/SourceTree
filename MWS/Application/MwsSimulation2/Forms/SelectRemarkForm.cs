//
// SelectRemarkForm.cs
//
// 備考選択画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.050 備考の定型文登録機能を追加(2018/09/27 勝呂)
// 
using System;
using System.Windows.Forms;

namespace MwsSimulation.Forms
{
	/// <summary>
	/// 備考選択画面
	/// </summary>
	public partial class SelectRemarkForm : Form
	{
		/// <summary>
		/// 備考リスト
		/// </summary>
		public string Remark { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SelectRemarkForm()
		{
			InitializeComponent();

			Remark = string.Empty;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SelectRemarkForm_Load(object sender, EventArgs e)
		{
			foreach (string name in MainForm.gSettings.RemarkList)
			{
				listBoxRemark.Items.Add(name);	
			}
		}

		/// <summary>
		/// 選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBoxRemark_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (-1 != listBoxRemark.SelectedIndex)
			{
				textBoxRemark.Text = listBoxRemark.SelectedItem as string;
			}
		}

		/// <summary>
		/// 選択して終了
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBoxRemark_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (-1 != listBoxRemark.SelectedIndex)
			{
				Remark = listBoxRemark.SelectedItem as string;
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		/// <summary>
		/// 登録
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonRegist_Click(object sender, EventArgs e)
		{
			if (0 < textBoxRemark.Text.Length)
			{
				foreach (string remark in MainForm.gSettings.RemarkList)
				{
					if (textBoxRemark.Text == remark)
					{
						return;
					}
				}
				MainForm.gSettings.RemarkList.Add(textBoxRemark.Text);
				foreach (string name in MainForm.gSettings.RemarkList)
				{
					listBoxRemark.Items.Add(name);
				}
				listBoxRemark.SelectedIndex = MainForm.gSettings.RemarkList.Count - 1;
			}
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (0 < textBoxRemark.Text.Length)
			{
				Remark = textBoxRemark.Text;
				this.DialogResult = DialogResult.OK;
			}
			else
			{
				this.DialogResult = DialogResult.Cancel;
			}
			this.Close();
		}
	}
}
