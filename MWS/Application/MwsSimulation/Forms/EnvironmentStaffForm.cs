//
// EnvironmentStaffForm.cs
//
// 担当者登録画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
// 
using System;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace MwsSimulation.Forms
{
	/// <summary>
	/// 担当者登録画面
	/// </summary>
	public partial class EnvironmentStaffForm : Form
	{
		/// <summary>
		/// 担当者リスト
		/// </summary>
		public StringCollection StaffList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		private EnvironmentStaffForm()
		{
			InitializeComponent();

			StaffList = null;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="staff">担当者リスト</param>
		public EnvironmentStaffForm(StringCollection staff)
		{
			InitializeComponent();

			StaffList = staff;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EnvironmentForm_Load(object sender, EventArgs e)
		{
			foreach (string name in StaffList)
			{
				listBoxStaff.Items.Add(name);	
			}
			if (0 < listBoxStaff.Items.Count)
			{
				listBoxStaff.SelectedIndex = 0;
			}
		}

		/// <summary>
		/// 指定された担当者は登録済みかどうか？
		/// </summary>
		/// <param name="name">担当者名</param>
		/// <returns>判定</returns>
		private bool IsExsixtStaffName(string name)
		{
			foreach (string staff in listBoxStaff.Items)
			{
				if (staff == name)
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
			using (SetStaffForm form = new SetStaffForm())
			{
				if (DialogResult.OK == form.ShowDialog())
				{
					if (false == this.IsExsixtStaffName(form.StaffName))
					{
						int index = listBoxStaff.Items.Add(form.StaffName);
						listBoxStaff.SelectedIndex = index;
					}
					else
					{
						MessageBox.Show("その担当者は既に存在しています。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
			}
		}

		/// <summary>
		/// 変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBoxStaff_MouseDoubleClick(object sender, MouseEventArgs e)
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
			if (-1 != listBoxStaff.SelectedIndex)
			{
				string name = listBoxStaff.Items[listBoxStaff.SelectedIndex].ToString();
				using (SetStaffForm form = new SetStaffForm(name))
				{
					if (DialogResult.OK == form.ShowDialog())
					{
						if (name != form.StaffName)
						{
							if (false == this.IsExsixtStaffName(form.StaffName))
							{
								listBoxStaff.Items[listBoxStaff.SelectedIndex] = form.StaffName;
							}
							else
							{
								MessageBox.Show("その担当者は既に存在しています。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
			if (-1 != listBoxStaff.SelectedIndex)
			{
				listBoxStaff.Items.RemoveAt(listBoxStaff.SelectedIndex);
			}
		}

		/// <summary>
		/// OK
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			StaffList.Clear();
			foreach (string name in listBoxStaff.Items)
			{
				StaffList.Add(name);
			}
		}
	}
}
