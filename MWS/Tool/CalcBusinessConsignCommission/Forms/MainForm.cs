//
// MainForm.cs
//
// PCA仕入データ業務委託手数料再計算ツール メイン画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/11/05 勝呂)
// 
using CalcBusinessConsignCommission.BaseFactory;
using MwsLib.DB.SqlServer.CalcBusinessConsignCommission;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CalcBusinessConsignCommission.Forms
{
	/// <summary>
	/// メイン画面
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// 売上データ
		/// </summary>
		private List<SaleData> SaleDataList;

		/// <summary>
		/// 仕入データ
		/// </summary>
		private List<StockData> StockDataList;

		/// <summary>
		/// DataSource用売上データ
		/// </summary>
		private List<SaleRecord> DataSourceSaleRecord;

		/// <summary>
		/// DataSource用仕入データ
		/// </summary>
		private List<StockRecord> DataSourceStockRecord;

		/// <summary>
		/// 販売店情報
		/// </summary>
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
			try
			{
				// 販売店情報の読込み
				SaleStockList = CalcBusinessConsignCommissionAccess.GetSalesOutletInfo();
			}
			catch (System.Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
		}

		/// <summary>
		/// PCA売上データファイルの読込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonReadSaleFile_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.FileName = SaleData.SALE_FILENAME;
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";				dlg.Title = "PCA売上データファイルを選択してください";				dlg.RestoreDirectory = true;				dlg.CheckFileExists = true;				dlg.CheckPathExists = true;
				if (DialogResult.OK == dlg.ShowDialog())
				{
					labelSaleFilename.Text = dlg.FileName;

					SaleDataList.Clear();

					// 元のカーソルを保持
					Cursor preCursor = Cursor.Current;

					// カーソルを待機カーソルに変更
					Cursor.Current = Cursors.WaitCursor;

					// PCA売上データの読込み
					try
					{
						using (var sr = new StreamReader(dlg.FileName, Encoding.GetEncoding("Shift_JIS")))
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
						MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
					// カーソルを元に戻す
					Cursor.Current = preCursor;
				}
			}
		}

		/// <summary>
		/// PCA仕入データファイルの読込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonReadStockFile_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.FileName = StockData.STOCK_FILENAME;
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";				dlg.Title = "PCA仕入データファイルを選択してください";				dlg.RestoreDirectory = true;				dlg.CheckFileExists = true;				dlg.CheckPathExists = true;
				if (DialogResult.OK == dlg.ShowDialog())
				{
					labelStockFilename.Text = dlg.FileName;

					StockDataList.Clear();

					// 元のカーソルを保持
					Cursor preCursor = Cursor.Current;

					// カーソルを待機カーソルに変更
					Cursor.Current = Cursors.WaitCursor;

					// PCA仕入データの読込み
					try
					{
						using (var sr = new StreamReader(dlg.FileName/*StockData.STOCK_FILENAME*/, Encoding.GetEncoding("Shift_JIS")))
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
						MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
			}
		}

		/// <summary>
		/// PCA仕入データ 業務委託手数料再計算
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonRecalcResult_Click(object sender, EventArgs e)
		{
			if (0 == SaleDataList.Count)
			{
				MessageBox.Show("PCA売上データを読み込んで下さい。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (0 == StockDataList.Count)
			{
				MessageBox.Show("PCA仕入データを読み込んで下さい。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			bool existRecalRecord = false;
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
							SaleData sale = SaleDataList.Find(p => p.SlipCode == record.SlipCode);
							if (null != sale)
							{
								// 業務委託手数料の再計算
								output.Recalc(sale.GetSalePrice(), saleStock.Item2);
								outputList.Add(output);
								existRecalRecord = true;
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
			// カーソルを元に戻す
			Cursor.Current = preCursor;

			if (existRecalRecord)
			{
				using (OutputStockForm form = new OutputStockForm(outputList))
				{
					form.ShowDialog();
				}
			}
			else
			{
				MessageBox.Show("業務委託手数料 再計算対象レコードが存在しませんでした。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}
}
