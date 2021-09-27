//
// MainForm.cs
// 
// 終了ユーザー課金アラート メイン画面フォームクラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2021/08/18):新規作成(勝呂)
//
using CommonLib.Common;
using System;
using System.Windows.Forms;

namespace AlartFinishedUserSale.Forms
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
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			this.Text = Program.Title;
			dateTimePickerFinishedDate.Value = DateTime.Today;
		}

		/// <summary>
		/// DragEnter
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxPathname_DragEnter(object sender, DragEventArgs e)
		{
			//コントロール内にドラッグされたとき実行される
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				//ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
				e.Effect = DragDropEffects.Copy;
			else
				//ファイル以外は受け付けない
				e.Effect = DragDropEffects.None;
		}

		/// <summary>
		/// Drag&Drop
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxPathname_DragDrop(object sender, DragEventArgs e)
		{
			//コントロール内にドロップされたとき実行される
			//ドロップされたすべてのファイル名を取得する
			string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			if (0 < fileName.Length)
			{
				textBoxPathname.Text = fileName[0];
			}
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			if (0 == textBoxPathname.Text.Length)
			{
				MessageBox.Show(this, "売上データファイルを指定してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			string msg;
			if (0 != Program.Alart(new Date(dateTimePickerFinishedDate.Value), textBoxPathname.Text, out msg))
			{
				MessageBox.Show(msg, Program.gProgramName);
				return;
			}
			MessageBox.Show("終了しました。", Program.gProgramName);
		}
	}
}
