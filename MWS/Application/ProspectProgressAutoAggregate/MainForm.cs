//
// MainForm.cs
// 
// 見込進捗自動集計 メイン画面フォームクラス（Debug用）
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/08/04 勝呂)
//
using MwsLib.Common;
using System;
using System.Windows.Forms;

namespace ProspectProgressAutoAggregate
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// FormLoad
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			this.Text = string.Format("{0}  {1}", this.Text,Program.VersionStr);
		}

		/// <summary>
		/// 見込進捗エクセル出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			Date bootDate = new Date(dateTimePickerBootDate.Value);
			string msg;
			if (0 == Program.AutoAggregate(bootDate, out msg))
			{
				this.Close();
				return;
			}
			MessageBox.Show(this, msg, "Excelファイル書込エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
