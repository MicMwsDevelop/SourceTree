//
// MainForm.cs
// 
// 見込進捗自動集計 メイン画面フォームクラス（Debug用）
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/08/04 勝呂)
// Ver1.04 来期追加と売上実績設定機能を追加（管理者用）(2021/10/20 勝呂)
//
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProspectProgressAutoAggregate.Forms
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
			// バージョン情報表示
			this.Text = string.Format("{0}  {1}", this.Text,Program.VersionStr);

#if DEBUG
			// 決算期の設定
			List<Tuple<int, string>> list = new List<Tuple<int, string>>();
			//list.Add(new Tuple<int, string>(46, "46期"));
			//list.Add(new Tuple<int, string>(47, "47期"));
			list.Add(new Tuple<int, string>(48, "48期"));
			list.Add(new Tuple<int, string>(49, "49期"));
			comboBoxPeriod.DataSource = list;
			comboBoxPeriod.DisplayMember = "Item2";
			comboBoxPeriod.ValueMember = "Item1";
			comboBoxPeriod.SelectedIndex = 1;

			// 来期追加、実績設定
			buttonAddNew.Enabled = true;
			buttonModify.Enabled = true;
#else
			// 決算期の設定
			List<Tuple<int, string>> list = new List<Tuple<int, string>>();
			//list.Add(new Tuple<int, string>(46, "46期"));
			list.Add(new Tuple<int, string>(47, "47期"));
			//list.Add(new Tuple<int, string>(48, "48期"));
			comboBoxPeriod.DataSource = list;
			comboBoxPeriod.DisplayMember = "Item2";
			comboBoxPeriod.ValueMember = "Item1";
			comboBoxPeriod.SelectedIndex = 0;

			//ウィンドウサイズの変更
			this.Size = new System.Drawing.Size(433, 290);
#endif
		}

		/// <summary>
		/// 見込進捗エクセル出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			if (-1 != comboBoxPeriod.SelectedIndex)
			{
				Tuple<int, string> item = (Tuple<int, string>)comboBoxPeriod.SelectedItem;
				string msg;
				if (0 == Program.AutoAggregate(item.Item1, out msg))
				{
					this.Close();
					return;
				}
				MessageBox.Show(this, msg, "Excelファイル書込エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// 来期追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAddNew_Click(object sender, EventArgs e)
		{
			using (AddNewRecordForm dlg = new AddNewRecordForm())
			{
				dlg.ShowDialog();
			}
		}

		/// <summary>
		/// 売上実績設定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonModify_Click(object sender, EventArgs e)
		{
			if (-1 != comboBoxPeriod.SelectedIndex)
			{
				Tuple<int, string> item = (Tuple<int, string>)comboBoxPeriod.SelectedItem;
				using (ModifyRecordForm dlg = new ModifyRecordForm())
				{
					dlg.Period = item.Item1;
					dlg.ShowDialog();
				}
			}
		}
	}
}
