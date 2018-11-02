using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CalcBusinessConsignCommission.BaseFactory;

namespace CalcBusinessConsignCommission.Forms
{
	public partial class OutputStockForm : Form
	{
		List<OutputStockRecord> OutputList;

		private OutputStockForm()
		{
			InitializeComponent();
		}

		public OutputStockForm(List<OutputStockRecord> outputList)
		{
			InitializeComponent();

			OutputList = outputList;
		}

		private void OutputStockForm_Load(object sender, EventArgs e)
		{
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
			dataGridViewStock.ResumeLayout();

			foreach (DataGridViewRow row in dataGridViewStock.Rows)
			{
				int unitPrice = (int)row.Cells["UnitPrice"].Value;
				int calcUnitPrice = (int)row.Cells["CalcUnitPrice"].Value;
				if (unitPrice != calcUnitPrice)
				{
					// 単価と単価（再計算）の金額が違う場合は背景色を赤で表示
					row.Cells["CalcUnitPrice"].Style.BackColor = Color.Red;
					row.Cells["CaclPrice"].Style.BackColor = Color.Red;
				}
			}
		}
	}
}
