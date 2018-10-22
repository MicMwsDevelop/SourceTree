//
// EnvironmentStaffForm.cs
//
// 備考登録画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.050 備考の定型文登録機能を追加(2018/09/27 勝呂)
// 
using System;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace MwsSimulation.Forms
{
	/// <summary>
	/// 備考登録画面
	/// </summary>
	public partial class EnvironmentRemarkForm : Form
	{
		/// <summary>
		/// 備考リスト
		/// </summary>
		public StringCollection RemarkList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		private EnvironmentRemarkForm()
		{
			InitializeComponent();

			RemarkList = null;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="remark">備考リスト</param>
		public EnvironmentRemarkForm(StringCollection remark)
		{
			InitializeComponent();

			RemarkList = remark;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EnvironmentForm_Load(object sender, EventArgs e)
		{
			foreach (string name in RemarkList)
			{
				listBoxRemark.Items.Add(name);	
			}
			if (0 < listBoxRemark.Items.Count)
			{
				listBoxRemark.SelectedIndex = 0;
			}
		}

		/// <summary>
		/// 指定された備考は登録済みかどうか？
		/// </summary>
		/// <param name="name">担当者名</param>
		/// <returns>判定</returns>
		private bool IsExsixtRemark(string str)
		{
			foreach (string remark in listBoxRemark.Items)
			{
				if (remark == str)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAdd_Click(object sender, EventArgs e)
		{
			using (SetRemarkForm form = new SetRemarkForm())
			{
				if (DialogResult.OK == form.ShowDialog())
				{
					if (false == this.IsExsixtRemark(form.Remark))
					{
						int index = listBoxRemark.Items.Add(form.Remark);
						listBoxRemark.SelectedIndex = index;
					}
					else
					{
						MessageBox.Show("その備考は既に存在しています。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
		}

		/// <summary>
		/// 変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBoxRemark_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			buttonModify.PerformClick();
		}

		/// <summary>
		/// 変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonModify_Click(object sender, EventArgs e)
		{
			if (-1 != listBoxRemark.SelectedIndex)
			{
				string remark = listBoxRemark.Items[listBoxRemark.SelectedIndex].ToString();
				using (SetRemarkForm form = new SetRemarkForm(remark))
				{
					if (DialogResult.OK == form.ShowDialog())
					{
						if (remark != form.Remark)
						{
							if (false == this.IsExsixtRemark(form.Remark))
							{
								listBoxRemark.Items[listBoxRemark.SelectedIndex] = form.Remark;
							}
							else
							{
								MessageBox.Show("その備考は既に存在しています。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// 削除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (-1 != listBoxRemark.SelectedIndex)
			{
				listBoxRemark.Items.RemoveAt(listBoxRemark.SelectedIndex);
			}
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			RemarkList.Clear();
			foreach (string name in listBoxRemark.Items)
			{
				RemarkList.Add(name);
			}
		}
	}
}
