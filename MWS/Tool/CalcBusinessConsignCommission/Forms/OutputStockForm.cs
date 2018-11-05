//
// OutputStockForm.cs
//
// PCA仕入データ出力画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/11/05 勝呂)
// 
using CalcBusinessConsignCommission.BaseFactory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CalcBusinessConsignCommission.Forms
{
	/// <summary>
	/// PCA仕入データ出力画面
	/// </summary>
	public partial class OutputStockForm : Form
	{
		/// <summary>
		/// PCA出力用仕入データ
		/// </summary>
		public List<OutputStockRecord> OutputList;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		private OutputStockForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="outputList"></param>
		public OutputStockForm(List<OutputStockRecord> outputList)
		{
			InitializeComponent();

			OutputList = outputList;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OutputStockForm_Load(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			// PCA出力用仕入データの表示
			dataGridViewStock.DataSource = null;
			dataGridViewStock.Rows.Clear();
			dataGridViewStock.Columns.Clear();
			dataGridViewStock.DataSource = OutputList;
			dataGridViewStock.Columns["SlipCode"].HeaderText = "伝票番号";
			dataGridViewStock.Columns["VenderCode"].HeaderText = "仕入先コード";
			dataGridViewStock.Columns["GoodsCode"].HeaderText = "商品コード";
			dataGridViewStock.Columns["GoodsName"].HeaderText = "商品名";
			dataGridViewStock.Columns["UnitPrice"].HeaderText = "単価";
			dataGridViewStock.Columns["Price"].HeaderText = "金額";
			dataGridViewStock.Columns["TargetPrice"].HeaderText = "業務委託手数料対象金額";
			dataGridViewStock.Columns["CommissionRate"].HeaderText = "手数料率";
			dataGridViewStock.Columns["CalcUnitPrice"].HeaderText = "単価（再計算）";
			dataGridViewStock.Columns["CaclPrice"].HeaderText = "金額（再計算）";
			dataGridViewStock.Columns["Record"].HeaderText = "レコード";
			dataGridViewStock.Columns["Record"].Visible = false;
			dataGridViewStock.Columns["RecalcRecord"].Visible = false;
			dataGridViewStock.ResumeLayout();

			foreach (DataGridViewRow row in dataGridViewStock.Rows)
			{
				if ((bool)row.Cells["RecalcRecord"].Value)
				{
					// 単価と単価（再計算）の金額が違う場合は背景色を赤で表示
					row.Cells["CalcUnitPrice"].Style.BackColor = Color.Red;
					row.Cells["CaclPrice"].Style.BackColor = Color.Red;
				}
			}
			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}

		/// <summary>
		/// PCA仕入データ出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOutput_Click(object sender, EventArgs e)
		{
			if (0 < OutputList.Count)
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// PCA仕入データの書き込み
				try
				{
					using (var wr = new StreamWriter("仕入データ-修正後.csv", false, Encoding.GetEncoding("Shift_JIS")))
					{
						foreach (OutputStockRecord rec in OutputList)
						{
							wr.WriteLine(rec.Output());
						}
						wr.Close();
					}
				}
				catch (System.Exception ex)
				{
					MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
					return;
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("仕入データ-修正後.csvを出力しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}
}
