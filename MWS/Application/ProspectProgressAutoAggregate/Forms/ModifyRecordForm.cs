//
// ModifyRecordForm.cs
// 
// 見込進捗自動集計 売上実績設定画面フォームクラス（管理者用）
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.04 来期追加と売上実績設定機能を追加（管理者用）(2021/10/20 勝呂)
//
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.Common;
using CommonLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProspectProgressAutoAggregate.Forms
{
	public partial class ModifyRecordForm : Form
	{
		/// <summary>
		/// 期
		/// </summary>
		public int Period { get; set; }
		 
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ModifyRecordForm()
		{
			InitializeComponent();

			Period = 0;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ModifyRecordForm_Load(object sender, EventArgs e)
		{
			Date date = Program.PeriodToDate(Period);
			List<Tuple<Date, string>> list = new List<Tuple<Date, string>>();
			for (int i = 0; i < 12; i++)
			{
				list.Add(new Tuple<Date, string>(date, string.Format("{0:D4}/{1:D2}", date.Year, date.Month)));
				date = date.PlusMonths(1);
			}
			comboBoxYearMonth.DataSource = list;
			comboBoxYearMonth.DisplayMember = "Item2";
			comboBoxYearMonth.ValueMember = "Item1";
			comboBoxYearMonth.SelectedIndex = 0;

			// DataGridViewの設定
			SetDataGridView((Date)((Tuple<Date, string>)comboBoxYearMonth.SelectedItem).Item1);
		}

		/// <summary>
		/// DataGridViewの設定
		/// </summary>
		/// <param name="date"></param>
		private void SetDataGridView(Date date)
		{
			// クリア
			dataGridViewRecord.DataSource = null;
			dataGridViewRecord.Rows.Clear();

			try
			{
				List<売上実績> list = CharlieDatabaseAccess.Select_売上実績(string.Format("実績日 = {0}", date.ToIntYMD()), "営業部コード", Program.gSettings.Charlie.ConnectionString);
				if (null != list)
				{
					dataGridViewRecord.AutoGenerateColumns = false;
					dataGridViewRecord.DataSource = list;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 指定月の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxYearMonth_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (-1 != comboBoxYearMonth.SelectedIndex)
			{
				// DataGridViewの設定
				SetDataGridView((Date)((Tuple<Date, string>)comboBoxYearMonth.SelectedItem).Item1);
			}
		}

		/// <summary>
		/// 更新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonUpdateSet_Click(object sender, EventArgs e)
		{
			if (null != dataGridViewRecord.DataSource)
			{
				List<売上実績> list = dataGridViewRecord.DataSource as List<売上実績>;
				try
				{
					foreach (売上実績 sale in list)
					{
						CharlieDatabaseAccess.UpdateSet_売上実績(sale, Program.gSettings.Charlie.ConnectionString);
					}
					MessageBox.Show("更新しました。");
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}
	}
}
