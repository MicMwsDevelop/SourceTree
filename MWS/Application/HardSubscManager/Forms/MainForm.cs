//
// MainForm.cs
//
// メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/10/20 勝呂)
// 
using ClosedXML.Excel;
using CommonLib.BaseFactory.Charlie.Table;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HardSubscManager.Forms
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
			// バージョン情報設定
			labelVersion.Text = Program.ProgramVersion;
		}

		/// <summary>
		/// 契約情報の新規登録
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAddNewContract_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.InitialDirectory = Program.gSettings.InputFolder;
				ofd.Filter = "Excelファイル(*.xlsx)|*.xlsx|すべてのファイル(*.*)|*.*";
				ofd.Title = "顧客情報連絡シートを選択してください";
				if (DialogResult.OK == ofd.ShowDialog())
				{
					try
					{
						using (XLWorkbook book = new XLWorkbook(ofd.FileName))
						{
							IXLWorksheet sheet = book.Worksheet("顧客情報連絡シート");
							if (0 < sheet.Cell(6, 5).Value.ToString().Length)
							{
								MessageBox.Show("既に契約番号が設定されています。顧客情報連絡シートの内容をご確認ください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
							}
							if (0 == sheet.Cell(11, 5).Value.ToString().Length)
							{
								MessageBox.Show("顧客Noが設定されていません。顧客情報連絡シートの内容をご確認ください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
							}
							using (ContractForm form = new ContractForm())
							{
								// 元のカーソルを保持
								Cursor preCursor = Cursor.Current;

								// カーソルを待機カーソルに変更
								Cursor.Current = Cursors.WaitCursor;

								form.ModofyFlag = false;
								form.OrgHeader = new T_HARD_SUBSC_HEADER();
								if (0 < sheet.Cell(11, 5).Value.ToString().Length)
								{
									// 顧客No
									form.OrgHeader.CustomerID = (int)sheet.Cell(11, 5).Value.GetNumber();
								}
								if (0 < sheet.Cell(20, 5).Value.ToString().Length)
								{
									// 受注日
									form.OrgHeader.OrderDate = sheet.Cell(20, 5).Value.GetDateTime();
								}
								if (0 < sheet.Cell(20, 14).Value.ToString().Length)
								{
									// 利用月数
									form.OrgHeader.Months = (short)sheet.Cell(20, 14).Value.GetNumber();
								}
								if (0 < sheet.Cell(20, 23).Value.ToString().Length)
								{
									// 月額利用料
									form.OrgHeader.MonthlyAmount = (int)sheet.Cell(20, 23).Value.GetNumber();
								}
								if (0 < sheet.Cell(22, 5).Value.ToString().Length)
								{
									// 出荷日
									form.OrgHeader.ShippingDate = sheet.Cell(22, 5).Value.GetDateTime();
								}
								if (0 < sheet.Cell(22, 14).Value.ToString().Length)
								{
									// 契約開始日
									form.OrgHeader.ContractStartDate = sheet.Cell(22, 14).Value.GetDateTime();
								}
								if (0 < sheet.Cell(22, 23).Value.ToString().Length)
								{
									// 契約終了日
									form.OrgHeader.ContractEndDate = sheet.Cell(22, 23).Value.GetDateTime();
								}
								form.OrgDetailList = new List<T_HARD_SUBSC_DETAIL>();
								for (int i = 0, j = Program.GoodsStartRow; i < Program.GoodsMaxCount; i++, j += 2)
								{
									if (0 == sheet.Cell(j, 3).Value.ToString().Length)
									{
										break;
									}
									T_HARD_SUBSC_DETAIL detail = new T_HARD_SUBSC_DETAIL();
									if (0 < sheet.Cell(j, 3).Value.ToString().Length)
									{
										// 商品コード
										detail.GoodsCode = sheet.Cell(j, 3).Value.GetText();
									}
									if (0 < sheet.Cell(j, 7).Value.ToString().Length)
									{
										// 機器名
										detail.GoodsName = sheet.Cell(j, 7).Value.GetText();
									}
									if (0 < sheet.Cell(j, 25).Value.ToString().Length)
									{
										// カテゴリ
										detail.CategoryName = sheet.Cell(j, 25).Value.GetText();
									}
									if (0 < sheet.Cell(j, 29).Value.ToString().Length)
									{
										// 数量
										detail.Quantity = (short)sheet.Cell(j, 29).Value.GetNumber();
									}
									if (0 < sheet.Cell(j, 31).Value.ToString().Length)
									{
										// シリアルNo
										detail.SerialNo = sheet.Cell(j, 31).Value.GetText();
									}
									if (0 < sheet.Cell(j, 39).Value.ToString().Length)
									{
										// 保証書スキャンデータファイル名
										detail.ScanFilename = sheet.Cell(j, 39).Value.GetText();
									}
									form.OrgDetailList.Add(detail);
								}
								// カーソルを元に戻す
								Cursor.Current = preCursor;

								if (DialogResult.OK == form.ShowDialog())
								{
									this.Close();
								}
							}
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		/// <summary>
		/// 契約情報の更新
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonModifyContract_Click(object sender, EventArgs e)
		{
			int customerNo = 0;
			using (InputCustomerNoForm form = new InputCustomerNoForm())
			{
				if (DialogResult.Cancel == form.ShowDialog())
				{
					return;
				}
				customerNo = form.CustomerNo;
			}
			using (ContractListForm form = new ContractListForm())
			{
				form.CustomerNo = customerNo;
				if (DialogResult.OK == form.ShowDialog())
				{
					this.Close();
				}
			}
		}
	}
}
