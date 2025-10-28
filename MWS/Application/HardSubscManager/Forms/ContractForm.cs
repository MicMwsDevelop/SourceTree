//
// ContractForm.cs
//
// 契約情報入力画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/10/20 勝呂)
// 
using ClosedXML.Excel;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.BaseFactory.Junp.View;
using CommonLib.Common;
using CommonLib.DB.SqlServer.HardSubsc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace HardSubscManager.Forms
{
	/// <summary>
	/// 契約情報入力画面クラス
	/// </summary>
	public partial class ContractForm : Form
	{
		/// <summary>
		/// 更新フラグ
		/// </summary>
		public bool ModofyFlag = false;

		/// <summary>
		/// 契約情報
		/// </summary>
		public T_HARD_SUBSC_HEADER OrgHeader = null;

		/// <summary>
		/// 機器情報リスト
		/// </summary>
		public List<T_HARD_SUBSC_DETAIL> OrgDetailList = null;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ContractForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ContractForm_Load(object sender, EventArgs e)
		{
			if (false == ModofyFlag)
			{
				// 新規登録
				this.Text = string.Format("{0}（新規登録）", this.Text);
			}
			else
			{
				// 更新
				this.Text = string.Format("{0}（更新）", this.Text);
				buttonLoadSheet.Enabled = true;
				dateTimePickerDeliveryDate.Enabled = true;
				dateTimePickerCancelDate.Enabled = true;
				dateTimePickerCollectDate.Enabled = true;
				dateTimePickerDisposalDate.Enabled = true;
			}
			try
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// 顧客情報の設定
				vMic全ユーザー2 clinicInfo = HardSubscAccess.GetClinicInfo(OrgHeader.CustomerID, Program.gSettings.ConnectJunp.ConnectionString);
				if (null != clinicInfo)
				{
					// 顧客情報、契約情報と貸出機器情報の設定
					SetControlData(OrgHeader, OrgDetailList, clinicInfo);
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		/// <summary>
		/// 納品日の設定
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dateTimePickerDeliveryDate_ValueChanged(object sender, EventArgs e)
		{
			if (dateTimePickerDeliveryDate.Checked)
			{
				short months = (short)numericTextBoxMonths.ToInt();
				if (false == T_HARD_SUBSC_HEADER.IsFormalMonths(months))
				{
					MessageBox.Show("利用月数が正しくありません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				labelContractStartDate.Enabled = true;
				labelContractEndDate.Enabled = true;
				DateTime? startDate = T_HARD_SUBSC_HEADER.GetContractStartDate(dateTimePickerDeliveryDate.Value);
				if (startDate.HasValue)
				{
					DateTime? endDate = T_HARD_SUBSC_HEADER.GetContractEndDate(startDate.Value, months);
					if (endDate.HasValue)
					{
						labelContractStartDate.Text = startDate.Value.ToShortDateString();
						labelContractEndDate.Text = endDate.Value.ToShortDateString();
					}
				}
			}
			else
			{
				labelContractStartDate.Enabled = false;
				labelContractEndDate.Enabled = false;
				dateTimePickerCancelDate.Checked = false;
			}
		}

		/// <summary>
		/// 顧客情報連絡シートの読込
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonLoadSheet_Click(object sender, EventArgs e)
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
							// 元のカーソルを保持
							Cursor preCursor = Cursor.Current;

							// カーソルを待機カーソルに変更
							Cursor.Current = Cursors.WaitCursor;

							IXLWorksheet sheet = book.Worksheet("顧客情報連絡シート");
							string contractNo = sheet.Cell(6, 5).Value.GetText();
							int customerNo = (int)sheet.Cell(11, 5).Value.GetNumber();
							if (0 == contractNo.Length)
							{
								MessageBox.Show("契約番号が設定されていません。顧客情報連絡シートの内容をご確認ください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
							}
							if (0 == customerNo)
							{
								MessageBox.Show("顧客Noが設定されていません。顧客情報連絡シートの内容をご確認ください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
							}
							if (OrgHeader.ContractNo != contractNo)
							{
								MessageBox.Show("契約番号が一致しません。顧客情報連絡シートの内容をご確認ください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
							}
							if (OrgHeader.CustomerID != customerNo)
							{
								MessageBox.Show("顧客Noが一致しません。顧客情報連絡シートの内容をご確認ください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
							}
							T_HARD_SUBSC_HEADER header = new T_HARD_SUBSC_HEADER();
							header = OrgHeader.DeepCopy();

							// 受注日
							header.OrderDate = null;
							if (0 < sheet.Cell(20, 5).Value.ToString().Length)
							{
								header.OrderDate = sheet.Cell(20, 5).Value.GetDateTime();
							}
							// 利用月数
							header.Months = 0;
							if (0 < sheet.Cell(20, 14).Value.ToString().Length)
							{
								header.Months = (short)sheet.Cell(20, 14).Value.GetNumber();
							}
							// 月額利用料
							header.MonthlyAmount = 0;
							if (0 < sheet.Cell(20, 23).Value.ToString().Length)
							{
								header.MonthlyAmount = (int)sheet.Cell(20, 23).Value.GetNumber();
							}
							// 納品日
							header.DeliveryDate = null;
							if (0 < sheet.Cell(22, 5).Value.ToString().Length)
							{
								header.DeliveryDate = sheet.Cell(22, 5).Value.GetDateTime();
							}
							// 契約開始日
							header.ContractStartDate = null;
							if (0 < sheet.Cell(22, 14).Value.ToString().Length)
							{
								header.ContractStartDate = sheet.Cell(22, 14).Value.GetDateTime();
							}
							// 契約終了日
							header.ContractEndDate = null;
							if (0 < sheet.Cell(22, 23).Value.ToString().Length)
							{
								header.ContractEndDate = sheet.Cell(22, 23).Value.GetDateTime();
							}
							// 解約日
							header.CancelDate = null;
							if (0 < sheet.Cell(24, 5).Value.ToString().Length)
							{
								header.CancelDate = sheet.Cell(24, 5).Value.GetDateTime();
							}
							// 機器回収日
							header.CollectDate = null;
							if (0 < sheet.Cell(24, 14).Value.ToString().Length)
							{
								header.CollectDate = sheet.Cell(24, 14).Value.GetDateTime();
							}
							// 機器廃棄日
							header.DisposalDate = null;
							if (0 < sheet.Cell(24, 23).Value.ToString().Length)
							{
								header.DisposalDate = sheet.Cell(24, 23).Value.GetDateTime();
							}
							List<T_HARD_SUBSC_DETAIL> detailList = new List<T_HARD_SUBSC_DETAIL>();
							for (int i = 0, j = 34; i < 20; i++, j += 2)
							{
								if (0 == sheet.Cell(j, 3).Value.ToString().Length)
								{
									break;
								}
								T_HARD_SUBSC_DETAIL detail = new T_HARD_SUBSC_DETAIL();
								if (i < OrgDetailList.Count)
								{
									detail = OrgDetailList[i].DeepCopy();
								}
								else
								{
									detail.InternalContractNo = header.InternalContractNo;
								}
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
								detailList.Add(detail);
							}
							bool warinigFlag = false;
							if (OrgDetailList.Count != detailList.Count)
							{
								warinigFlag = true;
							}
							else
							{
								for (int i = 0; i < OrgDetailList.Count; i++)
								{
									if (false == OrgDetailList[i].Equals(detailList[i]))
									{
										warinigFlag = true;
									}
								}
							}
							// 顧客情報、契約情報と貸出機器情報の設定
							OrgHeader = header;
							OrgDetailList = detailList;
							SetControlData(OrgHeader, OrgDetailList, null);

							// カーソルを元に戻す
							Cursor.Current = preCursor;

							if (warinigFlag)
							{
								MessageBox.Show("貸出機器情報が更新されています。登録内容を確認してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
							}
							else
							{
								MessageBox.Show("顧客情報連絡シートを読み込みました。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
		/// 保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSave_Click(object sender, EventArgs e)
		{
			// 貸出機器情報の有無
			if (0 == listViewDetail.Items.Count)
			{
				MessageBox.Show("貸出機器が登録されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			short months = (short)numericTextBoxMonths.ToInt();
			if (false == T_HARD_SUBSC_HEADER.IsFormalMonths(months))
			{
				// 利用月数の正当性
				MessageBox.Show("利用月数が正しくありません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			int amount = numericTextBoxMonthlyAmount.ToInt();
			if (0 == amount)
			{
				// 月額利用料の有無
				MessageBox.Show("月額利用料が入力されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			T_HARD_SUBSC_HEADER header = new T_HARD_SUBSC_HEADER();
			header = OrgHeader.DeepCopy();
			header.OrderDate = dateTimePickerOrderDate.Value;
			header.Months = months;
			header.MonthlyAmount = amount;

			// 貸出機器情報を設定
			List<T_HARD_SUBSC_DETAIL> detailList = new List<T_HARD_SUBSC_DETAIL>();
			foreach (ListViewItem lvItem in listViewDetail.Items)
			{
				detailList.Add(lvItem.Tag as T_HARD_SUBSC_DETAIL);
			}
			try
			{
				if (false == ModofyFlag)
				{
					// 契約情報の追加（戻り値は内部契約番号）
					int internalContractNo = HardSubscAccess.InsertIntoHardSubscHeader(header, Program.GetPerson(), Program.gSettings.ConnectCharlie.ConnectionString);
					if (0 < internalContractNo)
					{
						// 契約番号の採番
						header.ContractNo = T_HARD_SUBSC_HEADER.NumberingContractNo(internalContractNo);
						labelContractNo.Text = header.ContractNo;

						// 契約番号の設定
						HardSubscAccess.SetContractNo(internalContractNo, header.ContractNo, Program.GetPerson(), Program.gSettings.ConnectCharlie.ConnectionString);

						// 貸出機器情報の追加
						foreach (T_HARD_SUBSC_DETAIL detail in detailList)
						{
							// 内部契約番号の設定
							detail.InternalContractNo = internalContractNo;
						}
						HardSubscAccess.InsertIntoHardSubscDetailList(detailList, Program.GetPerson(), Program.gSettings.ConnectCharlie.ConnectionString);
					}
				}
				else
				{
					// 更新
					header = OrgHeader.DeepCopy();

					// 納品日
					header.DeliveryDate = null;

					// 契約期間
					header.ContractStartDate = null;
					header.ContractEndDate = null;

					// 解約日
					header.CancelDate = null;

					// 機器回収日
					header.CollectDate = null;

					// 機器廃棄日
					header.DisposalDate = null;
					if (dateTimePickerDeliveryDate.Checked)
					{
						// 納品日
						header.DeliveryDate = dateTimePickerDeliveryDate.Value;

						// 契約期間
						header.ContractStartDate = T_HARD_SUBSC_HEADER.GetContractStartDate(header.DeliveryDate);
						header.ContractEndDate = T_HARD_SUBSC_HEADER.GetContractEndDate(header.ContractStartDate, header.Months);

						Span contractSpan = new Span(header.ContractStartDate.Value.ToDate(), header.ContractEndDate.Value.ToDate());
						if (dateTimePickerCancelDate.Checked)
						{
							// 解約日
							header.CancelDate = dateTimePickerCancelDate.Value;

							// 解約日の正当性チェック
							if (false == contractSpan.IsInside(header.CancelDate.Value.ToDate()))
							{
								MessageBox.Show("解約日が契約期間内でありません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
							}
							if (header.CancelDate.Value.ToDate() != header.CancelDate.Value.ToDate().LastDayOfTheMonth())
							{
								MessageBox.Show("解約日は末日を設定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
							}
							if (header.BillingEndDate.HasValue && header.CancelDate.Value <= header.BillingEndDate.Value)
							{
								MessageBox.Show("解約日は課金終了日より未来を設定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
							}
						}
						// 機器回収日
						if (dateTimePickerCollectDate.Checked)
						{
							header.CollectDate = dateTimePickerCollectDate.Value;
						}
						// 機器廃棄日
						if (dateTimePickerDisposalDate.Checked)
						{
							header.DisposalDate = dateTimePickerDisposalDate.Value;
						}
					}
					// 契約情報の更新
					HardSubscAccess.UpdateSetHardSubscHeader(header, Program.GetPerson(), Program.gSettings.ConnectCharlie.ConnectionString);

					// 貸出機器情報の削除
					HardSubscAccess.DeleteHardSubscDetail(header.InternalContractNo, Program.gSettings.ConnectCharlie.ConnectionString);

					// 貸出機器情報の追加
					HardSubscAccess.InsertIntoHardSubscDetailList(detailList, Program.GetPerson(), Program.gSettings.ConnectCharlie.ConnectionString);
				}
				// 契約管理台帳の出力
				string outputFolder = Path.Combine(Program.gSettings.OutputFolder, header.CustomerID.ToString());
				if (false == File.Exists(outputFolder))
				{
					// 出力フォルダの作成
					Directory.CreateDirectory(outputFolder);
				}
				string filename = string.Format("契約管理台帳_{0}_{1}.xlsx", header.CustomerID, header.ContractNo);
				string pathname = Path.Combine(outputFolder, filename);
				File.Copy(Path.Combine(Directory.GetCurrentDirectory(), "契約管理台帳.xlsx.org"), pathname, true);

				using (XLWorkbook book = new XLWorkbook(pathname))
				{
					IXLWorksheet sheet = book.Worksheet("契約管理台帳");
					sheet.Cell(6, 5).Value = header.ContractNo;
					sheet.Cell(11, 5).Value = header.CustomerID;
					sheet.Cell(11, 14).Value = labelTokuisakiNo.Text;
					sheet.Cell(11, 21).Value = labelOffice.Text;
					sheet.Cell(13, 5).Value = labelClinicName.Text;
					sheet.Cell(13, 29).Value = labelTel.Text;
					sheet.Cell(15, 5).Value = labelAddress.Text;
					sheet.Cell(20, 5).Value = header.OrderDate;
					sheet.Cell(20, 14).Value = header.Months;
					sheet.Cell(20, 23).Value = header.MonthlyAmount;
					sheet.Cell(22, 5).Value = header.DeliveryDate;
					sheet.Cell(22, 14).Value = header.ContractStartDate;
					sheet.Cell(22, 23).Value = header.ContractEndDate;
					sheet.Cell(24, 5).Value = header.CancelDate;
					sheet.Cell(24, 14).Value = header.CollectDate;
					sheet.Cell(24, 23).Value = header.DisposalDate;
					for (int i = 0, j = 31; i < detailList.Count; i++, j += 2)
					{
						T_HARD_SUBSC_DETAIL detail = detailList[i];
						sheet.Cell(j, 3).Value = detail.GoodsCode;
						sheet.Cell(j, 7).Value = detail.GoodsName;
						sheet.Cell(j, 25).Value = detail.CategoryName;
						if (Program.CategoryPC == detail.CategoryName)
						{
							sheet.Cell(j, 25).Style.Font.FontColor = XLColor.Red;
						}
						sheet.Cell(j, 29).Value = detail.Quantity;
						sheet.Cell(j, 31).Value = detail.SerialNo;
						sheet.Cell(j, 39).Value = detail.ScanFilename;
						sheet.Cell(j, 50).Value = detail.AssetsCode;
					}
					book.Save();
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;

				if (false == ModofyFlag)
				{
					MessageBox.Show(string.Format("{0}を出力しました。\n契約番号は {1} となります。", filename, header.ContractNo), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show(string.Format("{0}を出力しました。", filename), Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				// 契約管理台帳の起動
				using (Process process = new Process())
				{
					process.StartInfo.FileName = pathname;
					process.StartInfo.UseShellExecute = true;   // Win32Exceptionを発生させないためのおまじない
					process.Start();
				}
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// キャンセル
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();	
		}

		/// <summary>
		/// 貸出機器の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewDetail_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			buttonModify.PerformClick();
		}

		/// <summary>
		/// 貸出機器変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonModify_Click(object sender, EventArgs e)
		{
			ListViewItem lvItem = listViewDetail.SelectedItems[0];
			if (null != lvItem.Tag)
			{
				T_HARD_SUBSC_DETAIL detail = (T_HARD_SUBSC_DETAIL)lvItem.Tag;
				using (DetailForm form = new DetailForm())
				{
					form.SaveDetail = detail;
					if (DialogResult.OK == form.ShowDialog())
					{
						lvItem.Tag = form.Detail;
						lvItem.SubItems[1].Text = form.Detail.GoodsCode;
						lvItem.SubItems[2].Text = form.Detail.GoodsName;
						lvItem.SubItems[3].Text = detail.CategoryName;
						lvItem.SubItems[4].Text = form.Detail.Quantity.ToString();
						lvItem.SubItems[5].Text = form.Detail.SerialNo;
						lvItem.SubItems[6].Text = form.Detail.ScanFilename;
						lvItem.SubItems[7].Text = form.Detail.AssetsCode;
						if (Program.CategoryPC == lvItem.SubItems[3].Text)
						{
							// 資産コードを格納する機器のカテゴリ名はテキストカラーを赤にする
							lvItem.UseItemStyleForSubItems = false;
							lvItem.SubItems[3].ForeColor = System.Drawing.Color.Red;
						}
						else
						{
							lvItem.UseItemStyleForSubItems = true;
							lvItem.SubItems[3].ForeColor = System.Drawing.Color.Black;
						}
					}
				}
			}
		}

		/// <summary>
		/// 貸出機器の追加
		/// 現機能は安全運用を考慮してマスク中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAdd_Click(object sender, EventArgs e)
		{
			using (DetailForm form = new DetailForm())
			{
				if (DialogResult.OK == form.ShowDialog())
				{
					ListViewItem item = new ListViewItem(Program.GetDetailListViewItem(listViewDetail.Items.Count + 1, form.Detail));
					if (Program.CategoryPC == item.SubItems[3].Text)
					{
						// 資産コードを格納する機器のカテゴリ名はテキストカラーを赤にする
						item.UseItemStyleForSubItems = false;
						item.SubItems[3].ForeColor = System.Drawing.Color.Red;
					}
					item.Tag = form.Detail;
					listViewDetail.Items.Add(item);
				}
			}
		}

		/// <summary>
		/// 貸出機器の削除
		/// 現機能は安全運用を考慮してマスク中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (0 < listViewDetail.SelectedItems.Count)
			{
				ListViewItem lvItem = listViewDetail.SelectedItems[0];
				if (null != lvItem)
				{
					if (DialogResult.Yes == MessageBox.Show("貸出機器情報を削除してよろしいですか？", Program.ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					{
						listViewDetail.Items.Remove(lvItem);
					}
				}
			}
		}

		/// <summary>
		/// 顧客情報、契約情報と貸出機器情報の設定
		/// </summary>
		/// <param name="header">契約情報</param>
		/// <param name="detailList">貸出機器情報</param>
		/// <param name="clinicInfo">顧客情報</param>
		private void SetControlData(T_HARD_SUBSC_HEADER header, List<T_HARD_SUBSC_DETAIL> detailList, vMic全ユーザー2 clinicInfo)
		{
			if (null != clinicInfo)
			{
				// 顧客情報の設定
				labelCustomerNo.Text = clinicInfo.顧客No.ToString();
				labelTokuisakiNo.Text = clinicInfo.得意先No;
				labelOffice.Text = clinicInfo.支店名;
				labelClinicName.Text = clinicInfo.顧客名;
				labelClinickKana.Text = clinicInfo.フリガナ;
				labelAddress.Text = clinicInfo.住所;
				labelTel.Text = clinicInfo.電話番号;
				labelEndFlag.Text = (clinicInfo.終了フラグ) ? "終了" : "";
			}
			// 契約情報の設定
			labelContractNo.Text = header.ContractNo;
			if (header.OrderDate.HasValue)
			{
				dateTimePickerOrderDate.Value = header.OrderDate.Value.Date;
			}
			numericTextBoxMonths.Text = header.Months.ToString();
			numericTextBoxMonthlyAmount.Text = header.MonthlyAmount.ToString();
			dateTimePickerDeliveryDate.Checked = false;
			if (header.DeliveryDate.HasValue)
			{
				dateTimePickerDeliveryDate.Checked = true;
				dateTimePickerDeliveryDate.Value = header.DeliveryDate.Value.Date;
			}
			labelContractStartDate.Text = string.Empty;
			if (header.ContractStartDate.HasValue)
			{
				labelContractStartDate.Text = header.ContractStartDate.Value.ToShortDateString();
			}
			labelContractEndDate.Text = string.Empty;
			if (header.ContractEndDate.HasValue)
			{
				labelContractEndDate.Text = header.ContractEndDate.Value.ToShortDateString();
			}
			labelBillingStartDate.Text = string.Empty;
			if (header.BillingStartDate.HasValue)
			{
				labelBillingStartDate.Text = header.BillingStartDate.Value.ToShortDateString();
			}
			labelBillingEndDate.Text = string.Empty;
			if (header.BillingEndDate.HasValue)
			{
				labelBillingEndDate.Text = header.BillingEndDate.Value.ToShortDateString();
			}
			dateTimePickerCancelDate.Checked = false;
			if (header.CancelDate.HasValue)
			{
				dateTimePickerCancelDate.Checked = true;
				dateTimePickerCancelDate.Value = header.CancelDate.Value.Date;
			}
			dateTimePickerCollectDate.Checked = false;
			if (header.CollectDate.HasValue)
			{
				dateTimePickerCollectDate.Checked = true;
				dateTimePickerCollectDate.Value = header.CollectDate.Value.Date;
			}
			dateTimePickerDisposalDate.Checked = false;
			if (header.DisposalDate.HasValue)
			{
				dateTimePickerDisposalDate.Checked = true;
				dateTimePickerDisposalDate.Value = header.DisposalDate.Value.Date;
			}
			// 貸出機器情報の設定
			listViewDetail.Items.Clear();
			if (null != detailList)
			{
				int i = 1;
				foreach (T_HARD_SUBSC_DETAIL detail in detailList)
				{
					ListViewItem item = new ListViewItem(Program.GetDetailListViewItem(i, detail));
					if (Program.CategoryPC == item.SubItems[3].Text)
					{
						// 資産コードを格納する機器のカテゴリ名はテキストカラーを赤にする
						item.UseItemStyleForSubItems = false;
						item.SubItems[3].ForeColor = System.Drawing.Color.Red;
					}
					item.Tag = detail;
					listViewDetail.Items.Add(item);
					i++;
				}
			}
		}
	}
}
