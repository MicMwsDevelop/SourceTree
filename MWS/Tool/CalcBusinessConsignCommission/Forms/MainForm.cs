using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MwsLib.DB.SqlServer.CalcBusinessConsignCommission;
using CalcBusinessConsignCommission.BaseFactory;

namespace CalcBusinessConsignCommission.Forms
{
	public partial class MainForm : Form
	{
		private List<SaleData> SaleDataList;

		private List<StockData> StockDataList;

		private List<SaleRecord> DataSourceSaleRecord;

		private List<StockRecord> DataSourceStockRecord;

		List<Tuple<string, int>> SaleStockList;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			SaleDataList = new List<SaleData>();
			StockDataList = new List<StockData>();
			DataSourceSaleRecord = new List<SaleRecord>();
			DataSourceStockRecord = new List<StockRecord>();
			SaleStockList = null;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			// 販売店情報の読込み
			SaleStockList = CalcBusinessConsignCommissionAccess.GetSalesOutletInfo();
		}

		/// <summary>
		/// ファイル読込
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonReadFile_Click(object sender, EventArgs e)
		{
			SaleDataList.Clear();
			StockDataList.Clear();

			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			// PCA売上データの読込み
			try
			{
				using (var sr = new StreamReader(SaleData.SALE_FILENAME, Encoding.GetEncoding("Shift_JIS")))
				{
					while (!sr.EndOfStream)
					{
						var record = sr.ReadLine();

						// 伝票番号の抽出
						int slipCode = SaleData.ExtractionSlipCode(record);
						SaleData sale = SaleDataList.Find(p => p.SlipCode == slipCode);
						if (null != sale)
						{
							sale.RecordList.Add(new SaleRecord(record));
						}
						else
						{
							SaleDataList.Add(new SaleData(record));
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (0 < SaleDataList.Count)
			{
				// データソースの作成
				DataSourceSaleRecord.Clear();
				foreach (SaleData data in SaleDataList)
				{
					DataSourceSaleRecord.AddRange(data.RecordList);
				}
				dataGridViewSale.DataSource = null;
				dataGridViewSale.Rows.Clear();
				dataGridViewSale.Columns.Clear();
				dataGridViewSale.DataSource = DataSourceSaleRecord;
				dataGridViewSale.Columns["SlipCode"].HeaderText = "伝票番号";
				dataGridViewSale.Columns["TokuisakiCode"].HeaderText = "得意先コード";
				dataGridViewSale.Columns["GoodsCode"].HeaderText = "商品コード";
				dataGridViewSale.Columns["GoodsName"].HeaderText = "商品名";
				dataGridViewSale.Columns["UnitPrice"].HeaderText = "単価";
				dataGridViewSale.Columns["SalePrice"].HeaderText = "売上金額";
				dataGridViewSale.Columns["OrgUnitPrice"].HeaderText = "原単価";
				dataGridViewSale.Columns["OrgPrice"].HeaderText = "原価金額";
				dataGridViewSale.Columns["Record"].HeaderText = "レコード";
				dataGridViewSale.Columns["Record"].Visible = false;
				dataGridViewSale.ResumeLayout();
			}

			// PCA仕入データの読込み
			try
			{
				using (var sr = new StreamReader(StockData.STOCK_FILENAME, Encoding.GetEncoding("Shift_JIS")))
				{
					while (!sr.EndOfStream)
					{
						var record = sr.ReadLine();

						// 伝票番号の抽出
						int slipCode = StockData.ExtractionSlipCode(record);
						StockData stock = StockDataList.Find(p => p.SlipCode == slipCode);
						if (null != stock)
						{
							stock.RecordList.Add(new StockRecord(record));
						}
						else
						{
							StockDataList.Add(new StockData(record));
						}
					}
				}
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (0 < StockDataList.Count)
			{
				// データソースの作成
				DataSourceStockRecord.Clear();
				foreach (StockData data in StockDataList)
				{
					DataSourceStockRecord.AddRange(data.RecordList);
				}
				dataGridViewStock.DataSource = null;
				dataGridViewStock.Rows.Clear();
				dataGridViewStock.Columns.Clear();
				dataGridViewStock.DataSource = DataSourceStockRecord;
				dataGridViewStock.Columns["SlipCode"].HeaderText = "伝票番号";
				dataGridViewStock.Columns["VenderCode"].HeaderText = "仕入先コード";
				dataGridViewStock.Columns["GoodsCode"].HeaderText = "商品コード";
				dataGridViewStock.Columns["GoodsName"].HeaderText = "商品名";
				dataGridViewStock.Columns["UnitPrice"].HeaderText = "単価";
				dataGridViewStock.Columns["Price"].HeaderText = "金額";
				dataGridViewStock.Columns["Record"].HeaderText = "レコード";
				dataGridViewStock.Columns["Record"].Visible = false;
				dataGridViewStock.ResumeLayout();
			}
			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}

		/// <summary>
		/// 再計算
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCalc_Click(object sender, EventArgs e)
		{
			if (0 < SaleDataList.Count && 0 < StockDataList.Count)
			{
				List<OutputStockRecord> outputList = new List<OutputStockRecord>();
				foreach (StockData stock in StockDataList)
				{
					foreach (StockRecord record in stock.RecordList)
					{
						if ("000084" == record.GoodsCode)
						{
							// MWS)月額用販売手数料
							Tuple<string, int> saleStock = SaleStockList.Find(p => p.Item1 == record.VenderCode);
							if (null != saleStock)
							{
								// 手数料率
								OutputStockRecord output = new OutputStockRecord(record);
								output.CommissionRate = saleStock.Item2;
								SaleData sale = SaleDataList.Find(p => p.SlipCode == record.SlipCode);
								if (null != sale)
								{
									output.TargetPrice = sale.GetSalePrice();
									output.Recalc();
									outputList.Add(output);
								}
								else
								{
									outputList.Add(new OutputStockRecord(record));
								}
							}
							else
							{
								outputList.Add(new OutputStockRecord(record));
							}
						}
						else
						{
							outputList.Add(new OutputStockRecord(record));
						}
					}
				}
				if (0 < outputList.Count)
				{
					using (OutputStockForm form = new OutputStockForm(outputList))
					{
						form.ShowDialog();
					}
				}
			}
		}
	}
}
