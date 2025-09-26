//
// HeaderDetailForm.cs
//
// 契約情報入力画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/04/03 勝呂)
// 
using ClosedXML.Excel;
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.Common;
using CommonLib.DB.SqlServer.HardSubsc;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace HardSubscManager.Forms
{
	/// <summary>
	/// 契約情報入力画面クラス
	/// </summary>
	public partial class HeaderDetailForm : Form
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo = 0;

		/// <summary>
		/// 契約情報（保存用）
		/// </summary>
		public T_HARD_SUBSC_HEADER SaveHeader = null;

		/// <summary>
		/// 機器情報リスト（保存用）
		/// </summary>
		private List<T_HARD_SUBSC_DETAIL> SaveDetailList = null;

		/// <summary>
		/// 内部契約番号の取得
		/// </summary>
		private int InternalContractNo
		{
			get
			{
				return (null != SaveHeader) ? SaveHeader.InternalContractNo : 0;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public HeaderDetailForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HeaderDetailForm_Load(object sender, EventArgs e)
		{
			if (null == SaveHeader)
			{
				// 新規申込
				this.Text = "契約情報の新規申込";
			}
			else
			{
				// 入力可能：全て
				this.Text = "契約情報の変更";
				try
				{
					// 契約情報と貸出機器情報の設定
					SaveDetailList = HardSubscAccess.GetHardSubscDetailList(InternalContractNo, Program.gSettings.ConnectCharlie.ConnectionString);
					SetControlData(SaveHeader, SaveDetailList);

					if (SaveHeader.BillingStartDate.HasValue)
					{
						// 課金が開始されているので、解約日、機器回収日、機器廃棄日の入力を許可する
						dateTimePickerCancelDate.Checked = true;
						dateTimePickerCollectDate.Checked = true;
						dateTimePickerDisposalDate.Checked = true;
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		/// <summary>
		/// 顧客情報連絡シートの読込
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonReadSheet_Click(object sender, EventArgs e)
		{
			if (null != SaveHeader)
			{
				if (0 < SaveHeader.ContractNo.Length)
				{
					MessageBox.Show("契約番号が格納されているため、顧客情報連絡シートの読込はできません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (SaveHeader.BillingStartDate.HasValue)
				{
					MessageBox.Show("課金情報が設定されているため、顧客情報連絡シートの読込はできません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.InitialDirectory = Directory.GetCurrentDirectory();
				dlg.Filter = "Excelファイル(*.xlsx)|*.xlsx|すべてのファイル(*.*)|*.*";
				dlg.Title = "顧客情報連絡シートを選択してください";
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					string filename = dlg.FileName;
					try
					{
						using (XLWorkbook book = new XLWorkbook(filename))
						{
							IXLWorksheet sheet = book.Worksheet("顧客情報連絡シート");
							T_HARD_SUBSC_HEADER header = new T_HARD_SUBSC_HEADER();
							List<T_HARD_SUBSC_DETAIL> detailList = new List<T_HARD_SUBSC_DETAIL>();
							header.ContractNo = sheet.Cell(6, 5).Value.GetText();
							header.CustomerID = (int)sheet.Cell(11, 5).Value.GetNumber();
							if (0 < sheet.Cell(20, 5).Value.ToString().Length)
							{
								header.AcceptDate = sheet.Cell(20, 5).Value.GetDateTime();
							}
							header.Months = (short)sheet.Cell(20, 14).Value.GetNumber();
							header.MonthlyAmount = (int)sheet.Cell(20, 23).Value.GetNumber();
							if (0 < sheet.Cell(22, 5).Value.ToString().Length)
							{
								header.DeliveryDate = sheet.Cell(22, 5).Value.GetDateTime();
							}
							if (0 < sheet.Cell(22, 14).Value.ToString().Length)
							{
								header.UseStartDate = sheet.Cell(22, 14).Value.GetDateTime();
							}
							if (0 < sheet.Cell(22, 23).Value.ToString().Length)
							{
								header.UseEndDate = sheet.Cell(22, 23).Value.GetDateTime();
							}
							for (int i = 0, j = 34; i < 20; i++, j += 2)
							{
								if (0 == sheet.Cell(j, 3).Value.ToString().Length)
								{
									break;
								}
								T_HARD_SUBSC_DETAIL detail = new T_HARD_SUBSC_DETAIL();
								detail.GoodsCode = sheet.Cell(j, 3).Value.GetText();
								detail.GoodsName = sheet.Cell(j, 7).Value.GetText();
								detail.CategoryName = sheet.Cell(j, 25).Value.GetText();
								detail.Quantity = (short)sheet.Cell(j, 29).Value.GetNumber();
								detail.Amount = (int)sheet.Cell(j, 31).Value.GetNumber();
								detail.SerialNo = sheet.Cell(j, 35).Value.GetText();
								detail.ScanFilename = sheet.Cell(j, 43).Value.GetText();
								detailList.Add(detail);
							}
							// 契約情報と貸出機器情報のクリア
							ClearControl();

							// 契約情報と貸出機器情報の設定
							SetControlData(header, detailList);
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
		/// 契約情報と貸出機器情報のクリア
		/// </summary>
		private void ClearControl()
		{
			textBoxContractNo.Text = string.Empty;
			dateTimePickerAcceptDate.Value = DateTime.Today;
			numericTextBoxMonths.Text = "0";
			numericTextBoxMonthlyAmount.Text = "0";
			dateTimePickerDeliveryDate.Checked = false;
			labelUseStartDate.Text = string.Empty;
			labelUseEndDate.Text = string.Empty;
			labelBillingStartDate.Text = string.Empty;
			labelBillingEndDate.Text = string.Empty;
			dateTimePickerCancelDate.Checked = false;
			dateTimePickerCollectDate.Checked = false;
			dateTimePickerDisposalDate.Checked = false;
			listViewDetail.Items.Clear();
		}

		/// <summary>
		/// 契約情報と貸出機器情報の設定
		/// </summary>
		/// <param name="header">契約情報</param>
		/// <param name="detailList">貸出機器情報</param>
		private void SetControlData(T_HARD_SUBSC_HEADER header, List<T_HARD_SUBSC_DETAIL> detailList)
		{
			textBoxContractNo.Text = header.ContractNo;
			if (header.AcceptDate.HasValue)
			{
				dateTimePickerAcceptDate.Value = header.AcceptDate.Value.Date;
			}
			numericTextBoxMonths.Text = header.Months.ToString();
			numericTextBoxMonthlyAmount.Text = header.MonthlyAmount.ToString();
			if (header.DeliveryDate.HasValue)
			{
				dateTimePickerDeliveryDate.Checked = true;
				dateTimePickerDeliveryDate.Value = header.DeliveryDate.Value.Date;
			}
			if (header.UseStartDate.HasValue)
			{
				labelUseStartDate.Text = header.UseStartDate.Value.ToShortDateString();
			}
			if (header.UseEndDate.HasValue)
			{
				labelUseEndDate.Text = header.UseEndDate.Value.ToShortDateString();
			}
			if (header.BillingStartDate.HasValue)
			{
				labelBillingStartDate.Text = header.BillingStartDate.Value.ToShortDateString();
			}
			if (header.BillingEndDate.HasValue)
			{
				labelBillingEndDate.Text = header.BillingEndDate.Value.ToShortDateString();
			}
			if (header.CancelDate.HasValue)
			{
				dateTimePickerCancelDate.Checked = true;
				dateTimePickerCancelDate.Value = header.CancelDate.Value.Date;
			}
			if (header.CollectDate.HasValue)
			{
				dateTimePickerCollectDate.Checked = true;
				dateTimePickerCollectDate.Value = header.CollectDate.Value.Date;
			}
			if (header.DisposalDate.HasValue)
			{
				dateTimePickerDisposalDate.Checked = true;
				dateTimePickerDisposalDate.Value = header.DisposalDate.Value.Date;
			}
			if (null != detailList)
			{
				int i = 1;
				foreach (T_HARD_SUBSC_DETAIL detail in detailList)
				{
					ListViewItem item = new ListViewItem(Program.GetDetailListViewItem(i, detail));
					item.Tag = detail;
					listViewDetail.Items.Add(item);
					i++;
				}
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
				labelUseStartDate.Enabled = true;
				labelUseEndDate.Enabled = true;
				DateTime? startDate = T_HARD_SUBSC_HEADER.GetUseStartDate(dateTimePickerDeliveryDate.Value);
				if (startDate.HasValue)
				{
					DateTime? endDate = T_HARD_SUBSC_HEADER.GetUseEndDate(startDate.Value, months);
					if (endDate.HasValue)
					{
						labelUseStartDate.Text = startDate.Value.ToShortDateString();
						labelUseEndDate.Text = endDate.Value.ToShortDateString();
					}
				}
			}
			else
			{
				labelUseStartDate.Enabled = false;
				labelUseEndDate.Enabled = false;
				dateTimePickerCancelDate.Checked = false;
			}
		}

		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSave_Click(object sender, EventArgs e)
		{
			// 機器情報の有無
			if (0 == listViewDetail.Items.Count)
			{
				MessageBox.Show("機器が登録されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// 契約番号の有無
			string contractNo = textBoxContractNo.Text.Trim();
			if (0 == contractNo.Length)
			{
				MessageBox.Show("契約番号が入力されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// 利用月数の正当性
			short months = (short)numericTextBoxMonths.ToInt();
			if (false == T_HARD_SUBSC_HEADER.IsFormalMonths(months))
			{
				MessageBox.Show("利用月数が正しくありません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// 月額利用料の有無
			int amount = numericTextBoxMonthlyAmount.ToInt();
			if (0 == amount)
			{
				MessageBox.Show("月額利用料が入力されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			DateTime? deliveryDate = null;
			DateTime? cancelDate = null;
			DateTime? collectDate = null;
			DateTime? disposalDate = null;
			DateTime? useStartDate = null;
			DateTime? useEndDate = null;
			if (dateTimePickerDeliveryDate.Checked)
			{
				deliveryDate = dateTimePickerDeliveryDate.Value;
			}
			if (dateTimePickerCancelDate.Checked)
			{
				cancelDate = dateTimePickerCancelDate.Value;
			}
			if (dateTimePickerCollectDate.Checked)
			{
				collectDate = dateTimePickerCollectDate.Value;
			}
			if (dateTimePickerDisposalDate.Checked)
			{
				disposalDate = dateTimePickerDisposalDate.Value;
			}
			if (false == deliveryDate.HasValue)
			{
				MessageBox.Show("納品日が入力されていません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// 利用期間
			useStartDate = T_HARD_SUBSC_HEADER.GetUseStartDate(deliveryDate);
			useEndDate = T_HARD_SUBSC_HEADER.GetUseEndDate(useStartDate, months);
			Span useSpan = new Span(useStartDate.Value.ToDate(), useEndDate.Value.ToDate());
			if (cancelDate.HasValue)
			{
				// 解約日の正当性
				if (false == useSpan.IsInside(cancelDate.Value.ToDate()))
				{
					MessageBox.Show("解約日が利用期間内でありません。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (cancelDate.Value.ToDate() != cancelDate.Value.ToDate().LastDayOfTheMonth())
				{
					MessageBox.Show("解約日は末日を設定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				if (SaveHeader.BillingEndDate.HasValue && cancelDate.Value <= SaveHeader.BillingEndDate.Value)
				{
					MessageBox.Show("解約日は課金終了日より未来を設定してください。", Program.ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			// 機器情報を設定
			List<T_HARD_SUBSC_DETAIL> detailList = new List<T_HARD_SUBSC_DETAIL>();
			foreach (ListViewItem lvItem in listViewDetail.Items)
			{
				detailList.Add(lvItem.Tag as T_HARD_SUBSC_DETAIL);
			}
			try
			{
				bool saveFlag = false;
				if (null == SaveHeader)
				{
					// 新規入力
					saveFlag = true;
					T_HARD_SUBSC_HEADER header = new T_HARD_SUBSC_HEADER();
					header.CustomerID = CustomerNo;
					header.ContractNo = contractNo;
					header.AcceptDate = dateTimePickerAcceptDate.Value.Date;
					header.Months = months;
					header.MonthlyAmount = amount;
					header.DeliveryDate = deliveryDate;
					header.UseStartDate = useStartDate;
					header.UseEndDate = useEndDate;
					header.CollectDate = collectDate;
					header.DisposalDate = disposalDate;

					// 契約情報の追加（戻り値は内部契約番号）
					int internalContractNo = HardSubscAccess.InsertIntoHardSubscHeader(header, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
					if (0 < internalContractNo)
					{
						foreach (T_HARD_SUBSC_DETAIL detail in detailList)
						{
							detail.InternalContractNo = internalContractNo;
						}
						// 機器情報の追加
						HardSubscAccess.InsertIntoHardSubscDetailList(detailList, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
					}
				}
				else
				{
					// 更新
					T_HARD_SUBSC_HEADER header = SaveHeader.DeepCopy();
					header.ContractNo = contractNo;
					header.AcceptDate = dateTimePickerAcceptDate.Value.Date;
					header.Months = months;
					header.MonthlyAmount = amount;
					header.DeliveryDate = deliveryDate;
					header.UseStartDate = useStartDate;
					header.UseEndDate = useEndDate;
					header.CollectDate = collectDate;
					header.DisposalDate = disposalDate;
					if (false == SaveHeader.Equals(header))
					{
						// 契約情報の更新
						HardSubscAccess.UpdateSetHardSubscHeader(header, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
						saveFlag = true;
					}
					if (null == SaveDetailList)
					{
						// 新規追加
						if (0 < detailList.Count)
						{
							// 機器情報の追加
							HardSubscAccess.InsertIntoHardSubscDetailList(detailList, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
							saveFlag = true;
						}
					}
					else
					{
						// 更新
						if (false == T_HARD_SUBSC_DETAIL.EqualList(SaveDetailList, detailList))
						{
							// 機器情報の削除
							HardSubscAccess.DeleteHardSubscDetail(header.InternalContractNo, Program.gSettings.ConnectCharlie.ConnectionString);

							// 機器情報の追加
							HardSubscAccess.InsertIntoHardSubscDetailList(detailList, Program.gSettings.ConnectCharlie.ConnectionString, Program.GetPerson());
							saveFlag = true;
						}
					}
				}
				if (saveFlag)
				{
					this.DialogResult = DialogResult.OK;
				}
				else
				{
					this.DialogResult = DialogResult.Cancel;
				}
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
		/// 機器の追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAdd_Click(object sender, EventArgs e)
		{
			using (DetailForm form = new DetailForm())
			{
				form.InternalContractNo = InternalContractNo;
				if (DialogResult.OK == form.ShowDialog())
				{
					ListViewItem item = new ListViewItem(Program.GetDetailListViewItem(listViewDetail.Items.Count + 1, form.Detail));
					item.Tag = form.Detail;
					listViewDetail.Items.Add(item);
				}
			}
		}

		/// <summary>
		/// 機器の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewDetail_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			ListViewItem lvItem = listViewDetail.SelectedItems[0];
			if (null != lvItem.Tag)
			{
				T_HARD_SUBSC_DETAIL detail = (T_HARD_SUBSC_DETAIL)lvItem.Tag;
				using (DetailForm form = new DetailForm())
				{
					form.InternalContractNo = InternalContractNo;
					form.SaveDetail = detail;
					if (DialogResult.OK == form.ShowDialog())
					{
						lvItem.Tag = form.Detail;
						lvItem.SubItems[1].Text = form.Detail.GoodsCode;
						lvItem.SubItems[2].Text = form.Detail.GoodsName;
						lvItem.SubItems[3].Text = detail.CategoryName;
						lvItem.SubItems[4].Text = form.Detail.Amount.ToString();
						lvItem.SubItems[5].Text = form.Detail.Quantity.ToString();
						lvItem.SubItems[6].Text = form.Detail.SerialNo;
						lvItem.SubItems[7].Text = form.Detail.ScanFilename;
						lvItem.SubItems[8].Text = form.Detail.AssetsCode;
					}
				}
			}
		}

		/// <summary>
		/// 機器の削除
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
					if (DialogResult.Yes == MessageBox.Show("機器情報を削除してよろしいですか？", Program.ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					{
						listViewDetail.Items.Remove(lvItem);
					}
				}
			}
		}
	}
}
