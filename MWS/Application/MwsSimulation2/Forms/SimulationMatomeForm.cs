//
// SimulationMatomeForm.cs
//
// おまとめプラン 御見積書作成画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
// Ver1.030 電子カルテ標準サービス申込時に、１号カルテ標準サービスと２号カルテ標準サービスの申込をチェックする(2018/08/10 勝呂)
// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
// Ver1.050 おまとめプランが１円から適用できるように修正(2018/09/18 勝呂)
// Ver1.050 電子カルテ標準サービス選択時にはTABLETビューワのサービス利用料の500円は加算しない(2018/09/26 勝呂)
// Ver1.050 見積書および注文書の宛先を「御中」と「様」を変更可能にする(2018/09/26 勝呂)
// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
// Ver1.050 月額利用料の表示の追加(2018/09/27 勝呂)
// Ver1.050 備考の定型文登録機能を追加(2018/09/27 勝呂)
// 
using CommonDialog.PrintPreview;
using MwsLib.BaseFactory.MwsSimulation;
using MwsLib.Common;
using MwsLib.DB.SQLite.MwsSimulation;
using MwsSimulation.Print;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace MwsSimulation.Forms
{
	/// <summary>
	/// おまとめプラン 御見積書作成画面
	/// </summary>
	public partial class SimulationMatomeForm : Form
	{
		/// <summary>
		/// 印刷設定保持用
		/// </summary>
		private PrintDocument PrintDocument { get; set; }

		/// <summary>
		/// 見積書印刷クラス
		/// </summary>
		private PrintEstimate PrintInfo { get; set; } 

		/// <summary>
		/// 印刷 総ページ数
		/// </summary>
		private int MaxPage { get; set; }

		/// <summary>
		/// 契約月数リスト
		/// </summary>
		private List<Tuple<int, string>> AgreeMonthesList { get; set; }

		/// <summary>
		/// おまとめプラン12ヵ月プランがマスターに存在する
		/// </summary>
		// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
		private bool ExistGroupPlan12 { get; set; }

		/// <summary>
		/// おまとめプラン24ヵ月がマスターに存在する
		/// </summary>
		// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
		private bool ExistGroupPlan24 { get; set; }

		/// <summary>
		/// おまとめプラン36ヵ月がマスターに存在する
		/// </summary>
		// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
		private bool ExistGroupPlan36 { get; set; }

		/// <summary>
		/// 見積書情報
		/// </summary>
		public Estimate EstimateData { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SimulationMatomeForm()
		{
			InitializeComponent();

			EstimateData = null;

			PrintInfo = new PrintEstimate();
			PrintDocument = new PrintDocument();
			MaxPage = 0;
			AgreeMonthesList = new List<Tuple<int, string>>();

			// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
			ExistGroupPlan12 = false;
			ExistGroupPlan24 = false;
			ExistGroupPlan36 = false;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SimulationMatomeForm(Estimate est)
		{
			InitializeComponent();

			EstimateData = est;

			PrintInfo = new PrintEstimate();
			PrintDocument = new PrintDocument();
			MaxPage = 0;
			AgreeMonthesList = new List<Tuple<int, string>>();

			// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
			ExistGroupPlan12 = false;
			ExistGroupPlan24 = false;
			ExistGroupPlan36 = false;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SimulationMatomeForm_Load(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			// ウィンドウサイズの変更
			this.Width = MainForm.gSettings.SimulationMatomeFormSize.Width;
			this.Height = MainForm.gSettings.SimulationMatomeFormSize.Height;

			// サービス情報リストビューの設定
			listViewService.ItemCheck -= new ItemCheckEventHandler(listViewService_ItemCheck);
			listViewService.ItemChecked -= new ItemCheckedEventHandler(listViewService_ItemChecked);
			listViewService.BeginUpdate();
			foreach (ServiceInfo service in MainForm.gServiceList)
			{
				if (Program.SERVICE_CODE_REMOTE != service.ServiceCode && true == service.IsGroupPlanService)
				{
					// おまとめプラン対象サービス
					ListViewItem lvItem = new ListViewItem(service.GetListViewData());
					lvItem.Tag = service;
					listViewService.Items.Add(lvItem);
				}
			}
			listViewService.EndUpdate();

			// 契約期間コンボボックスの設定
			AgreeMonthesList.Add(new Tuple<int, string>(12, "12ヵ月"));
			AgreeMonthesList.Add(new Tuple<int, string>(24, "24ヵ月"));
			AgreeMonthesList.Add(new Tuple<int, string>(36, "36ヵ月"));
			comboBoxTerm.DisplayMember = "Item2";
			comboBoxTerm.ValueMember = "Item1";
			comboBoxTerm.DataSource = AgreeMonthesList;

			// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
			if (MainForm.gGroupPlanList.IsExistKeiyakuMonth(12))
			{
				// おまとめプラン12ヵ月プランが有効
				ExistGroupPlan12 = true;
				textBoxPrice12.Enabled = true;
				textBoxFree12.Enabled = true;
			}
			// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
			if (MainForm.gGroupPlanList.IsExistKeiyakuMonth(24))
			{
				// おまとめプラン24ヵ月プランが有効
				ExistGroupPlan24 = true;
				textBoxPrice24.Enabled = true;
				textBoxFree24.Enabled = true;
			}
			// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
			if (MainForm.gGroupPlanList.IsExistKeiyakuMonth(36))
			{
				// おまとめプラン36ヵ月が有効
				ExistGroupPlan36 = true;
				textBoxPrice36.Enabled = true;
				textBoxFree36.Enabled = true;
			}
			// オススメセットの設定
			if (3 == MainForm.gInitGroupPlanList.Count)
			{
				buttonInitGroupPlan1.Text = MainForm.gInitGroupPlanList[0].GroupName;
				buttonInitGroupPlan1.Tag = MainForm.gInitGroupPlanList[0];
				buttonInitGroupPlan2.Text = MainForm.gInitGroupPlanList[1].GroupName;
				buttonInitGroupPlan2.Tag = MainForm.gInitGroupPlanList[1];
				buttonInitGroupPlan3.Text = MainForm.gInitGroupPlanList[2].GroupName;
				buttonInitGroupPlan3.Tag = MainForm.gInitGroupPlanList[2];
			}
			if (null != EstimateData)
			{
				// 宛先の設定
				textBoxDestination.Text = EstimateData.Destination;

				// Ver1.050 見積書および注文書の宛先を「御中」と「様」を変更可能にする(2018/09/26 勝呂)
				if (0 != EstimateData.NotUsedMessrs)
				{
					radioSama.Checked = true;
				}
				// 発行日の設定
				dateTimePickerPrintDate.ValueChanged -= new System.EventHandler(dateTimePickerPrintDate_ValueChanged);
				dateTimePickerPrintDate.Value = EstimateData.PrintDate.ToDateTime();
				dateTimePickerPrintDate.ValueChanged += new System.EventHandler(dateTimePickerPrintDate_ValueChanged);

				// 契約期間の設定
				labelAgreeSpan.Tag = EstimateData.AgreeSpan;

				// 契約月数の設定
				comboBoxTerm.SelectedValue = EstimateData.AgreeMonthes;

				// 有効期限の設定
				dateTimePickerLimitDate.Value = EstimateData.LimitDate.ToDateTime();

				// 備考の設定
				textBoxRemark.Lines = EstimateData.GetRemark();

				if (Estimate.ApplyType.Matome == EstimateData.Apply)
				{
					// 選択済みのサービスのチェックボックスをON
					foreach (EstimateService estService in EstimateData.ServiceList)
					{
						foreach (Tuple<string, string> group in estService.GroupServiceList)
						{
							foreach (ListViewItem item in listViewService.Items)
							{
								ServiceInfo service = item.Tag as ServiceInfo;
								if (service.GoodsID == group.Item1)
								{
									item.Checked = true;
									break;
								}
							}
						}
					}
					// おまとめプラン契約あり
					radioButtonGroupEnable.Checked = true;
				}
				else
				{
					// おまとめプラン契約なし
					// 選択済みのサービスのチェックボックスをON
					foreach (EstimateService estService in EstimateData.ServiceList)
					{
						foreach (ListViewItem item in listViewService.Items)
						{
							ServiceInfo service = item.Tag as ServiceInfo;
							if (service.GoodsID == estService.GoodsID)
							{
								item.Checked = true;
								break;
							}
						}
					}
				}
			}
			else
			{
				// 契約期間の設定
				labelAgreeSpan.Tag = Estimate.GetAgreeSapn(Date.Today, 36);

				// 契約月数の設定
				comboBoxTerm.SelectedValue = 36;

				// 有効期限の設定
				dateTimePickerLimitDate.Value  = Estimate.GetLimitDate(Date.Today).ToDateTime();

				// サービス利用料及び月額利用料の表示
				this.DrawServicePrice();
			}
			// プラットフォーム利用料の設定
			textBoxPlatformPrice.Text = @"\" + StringUtil.CommaEdit(MainForm.gServiceList.Platform.Price);

			// 契約期間の表示
			// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
			this.DrawAgreeSpan((Span)labelAgreeSpan.Tag);

			listViewService.ItemCheck += new ItemCheckEventHandler(listViewService_ItemCheck);
			listViewService.ItemChecked += new ItemCheckedEventHandler(listViewService_ItemChecked);

			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}

		/// <summary>
		/// サービスの選択/解除（設定前）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewService_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			listViewService.ItemCheck -= new ItemCheckEventHandler(listViewService_ItemCheck);
			if (CheckState.Unchecked == e.CurrentValue)
			{
				// 選択状態
				ListViewItem targetItem = listViewService.Items[e.Index];
				ServiceInfo targetService = (ServiceInfo)targetItem.Tag;
				if (targetService.IsChildService)
				{
					// 子サービス
					foreach (ListViewItem item in listViewService.Items)
					{
						if (false == item.Checked)
						{
							ServiceInfo service = item.Tag as ServiceInfo;
							if (targetService.ParentServiceCode == service.ServiceCode)
							{
								// 親サービスが未選択なので、子サービスは未選択のまま
								MessageBox.Show(string.Format("[{0}] には、[{1}] の申込が必要です", targetService.ServiceName, service.ServiceName), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								e.NewValue = CheckState.Unchecked;
								break;
							}
						}
					}
				}
			}
			listViewService.ItemCheck += new ItemCheckEventHandler(listViewService_ItemCheck);
		}

		/// <summary>
		/// サービスの選択/解除（設定後）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewService_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			listViewService.ItemChecked -= new ItemCheckedEventHandler(listViewService_ItemChecked);

			ServiceInfo targetService = (ServiceInfo)e.Item.Tag;
			if (e.Item.Checked)
			{
				targetService.Select = true;

				// Ver1.050 電子カルテ標準サービス選択時に１号カルテ標準サービスと２号カルテ標準サービスを選択状態にする(2018/09/26 勝呂)
				if (Program.SERVICE_CODE_CHART_COMPUTE == targetService.ServiceCode)
				{
					// 電子カルテ標準サービス
					foreach (ListViewItem item in listViewService.Items)
					{
						if (false == item.Checked)
						{
							ServiceInfo svr = item.Tag as ServiceInfo;
							if (Program.SERVICE_CODE_CHART1_STD == svr.ServiceCode || Program.SERVICE_CODE_CHART2_STD == svr.ServiceCode)
							{
								// １号カルテ標準サービス or ２号カルテ標準サービス
								item.Checked = true;
							}
						}
					}
				}
			}
			else
			{
				targetService.Select = false;
				foreach (ListViewItem item in listViewService.Items)
				{
					if (item.Checked)
					{
						ServiceInfo service = item.Tag as ServiceInfo;
						if (service.ParentServiceCode == targetService.ServiceCode)
						{
							// 親サービスが未選択になったので、子サービスも未選択にする
							item.Checked = false;
							service.Select = false;
						}
					}
				}
			}
			listViewService.ItemChecked += new ItemCheckedEventHandler(listViewService_ItemChecked);

			// サービス利用料及び月額利用料の表示
			this.DrawServicePrice();
		}

		/// <summary>
		/// 発行日の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dateTimePickerPrintDate_ValueChanged(object sender, EventArgs e)
		{
			Date printDate = new Date(dateTimePickerPrintDate.Value);

			// 発行日に対する契約期間の変更
			labelAgreeSpan.Tag = Estimate.GetAgreeSapn(printDate, (int)comboBoxTerm.SelectedValue);

			// 契約期間の表示
			DrawAgreeSpan((Span)labelAgreeSpan.Tag);

			// 有効期限の設定
			dateTimePickerLimitDate.Value = Estimate.GetLimitDate(printDate).ToDateTime();
		}

		/// <summary>
		/// 契約期間の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
		private void buttonChangeAgreeSpan_Click(object sender, EventArgs e)
		{
			using (AgreeSpanForm form = new AgreeSpanForm((Span)labelAgreeSpan.Tag, (int)comboBoxTerm.SelectedValue))
			{
				if (DialogResult.OK == form.ShowDialog())
				{
					labelAgreeSpan.Tag = form.AgreeSpan;

					// 契約期間の表示
					this.DrawAgreeSpan((Span)labelAgreeSpan.Tag);
				}
			}
		}

		/// <summary>
		/// 契約月数の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
		private void comboBoxTerm_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (null != labelAgreeSpan.Tag)
			{
				// 契約期間の設定
				Span agreeSpan = labelAgreeSpan.Tag as Span;
				labelAgreeSpan.Tag = new Span(agreeSpan.Start, Estimate.GetAgreeEndDate(agreeSpan.Start, (int)comboBoxTerm.SelectedValue));

				// 契約期間の表示
				DrawAgreeSpan((Span)labelAgreeSpan.Tag);

				// サービス利用料の表示
				this.DrawServicePrice();
			}
		}

		/// <summary>
		/// オススメセット１の選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInitGroupPlan1_Click(object sender, EventArgs e)
		{
			InitGroupPlan plan = buttonInitGroupPlan1.Tag as InitGroupPlan;
			this.SetInitGroupPlanCheckBox(plan);
		}

		/// <summary>
		/// オススメセット２の選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInitGroupPlan2_Click(object sender, EventArgs e)
		{
			InitGroupPlan plan = buttonInitGroupPlan2.Tag as InitGroupPlan;
			this.SetInitGroupPlanCheckBox(plan);
		}

		/// <summary>
		/// オススメセット３の選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInitGroupPlan3_Click(object sender, EventArgs e)
		{
			InitGroupPlan plan = buttonInitGroupPlan3.Tag as InitGroupPlan;
			this.SetInitGroupPlanCheckBox(plan);
		}

		/// <summary>
		/// おまとめプラン契約なし
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonGroupDisable_CheckedChanged(object sender, EventArgs e)
		{
			// サービス利用料の表示
			this.DrawServicePrice();
		}

		/// <summary>
		/// おまとめプラン契約あり
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonGroupEnable_CheckedChanged(object sender, EventArgs e)
		{
			// サービス利用料の表示
			this.DrawServicePrice();
		}

		/// <summary>
		/// 定型文からの備考入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		// Ver1.050 備考の定型文登録機能を追加(2018/09/27 勝呂)
		private void buttonRemarkTemplate_Click(object sender, EventArgs e)
		{
			using (SelectRemarkForm form = new SelectRemarkForm())
			{
				if (DialogResult.OK == form.ShowDialog())
				{
					textBoxRemark.Text += form.Remark;
				}
			}
		}

		/// <summary>
		/// 全選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAllOn_Click(object sender, EventArgs e)
		{
			listViewService.ItemCheck -= new ItemCheckEventHandler(listViewService_ItemCheck);
			listViewService.ItemChecked -= new ItemCheckedEventHandler(listViewService_ItemChecked);
			foreach (ListViewItem item in listViewService.Items)
			{
				item.Checked = true;
			}
			listViewService.ItemCheck += new ItemCheckEventHandler(listViewService_ItemCheck);
			listViewService.ItemChecked += new ItemCheckedEventHandler(listViewService_ItemChecked);

			// サービス利用料及び月額利用料の表示
			this.DrawServicePrice();
		}

		/// <summary>
		/// 全解除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAllOff_Click(object sender, EventArgs e)
		{
			listViewService.ItemCheck -= new ItemCheckEventHandler(listViewService_ItemCheck);
			listViewService.ItemChecked -= new ItemCheckedEventHandler(listViewService_ItemChecked);
			foreach (ListViewItem item in listViewService.Items)
			{
				item.Checked = false;
			}
			listViewService.ItemCheck += new ItemCheckEventHandler(listViewService_ItemCheck);
			listViewService.ItemChecked += new ItemCheckedEventHandler(listViewService_ItemChecked);

			// サービス利用料及び月額利用料の表示
			this.DrawServicePrice();
		}

		/// <summary>
		/// 見積書印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonPrint_Click(object sender, EventArgs e)
		{
			// 子サービスの申込に対して親サービスが申し込まれているか判定
			string msg;
			int errIndex = this.IsOrderParentSrvice(out msg);
			if (-1 != errIndex)
			{
				MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				listViewService.Items[errIndex].Selected = true;

				// Ver1.030 電子カルテ標準サービス申込時に、１号カルテ標準サービスと２号カルテ標準サービスの申込をチェックする(2018/08/10 勝呂)
				listViewService.EnsureVisible(errIndex);
				return;
			}
			if (0 == textBoxDestination.Text.Length)
			{
				MessageBox.Show("宛先を入力してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				textBoxDestination.Focus();
				return;
			}
			// 申込み情報の取得
			List<GroupService> groupList = null;
			List<ServiceInfo> serviceList = null;
			this.GetOrderService(out groupList, out serviceList);

			if (0 < groupList.Count || 0 < serviceList.Count)
			{
				Estimate est = new Estimate();

				// 宛先
				est.Destination = textBoxDestination.Text;

				// Ver1.050 見積書および注文書の宛先を「御中」と「様」を変更可能にする(2018/09/26 勝呂)
				if (radioSama.Checked)
				{
					est.NotUsedMessrs = 1;
				}

				// 発行日
				est.PrintDate = new Date(dateTimePickerPrintDate.Value);

				// 契約期間の設定
				// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
				est.AgreeSpan = labelAgreeSpan.Tag as Span;

				// 契約月数の設定
				est.AgreeMonthes = (int)comboBoxTerm.SelectedValue;

				// 有効期限の設定
				est.LimitDate = new Date(dateTimePickerLimitDate.Value);

				// 備考
				est.SetRemark(textBoxRemark.Lines);

				// 申込み種別
				est.Apply = (radioButtonGroupDisable.Checked) ? Estimate.ApplyType.MatomeNone: Estimate.ApplyType.Matome;

				// 見積書情報の設定
				// Ver1.050 電子カルテ標準サービス選択時にはTABLETビューワのサービス利用料の500円は加算しない(2018/09/26 勝呂)
				//est.SetEstimateData(serviceList, groupList);
				est.SetEstimateData(serviceList, groupList, Program.SERVICE_CODE_CHART_COMPUTE, Program.SERVICE_CODE_TABLETVIEWER);

				// 見積書印刷
				this.PrintEstimate(PrintEstimateDef.MwsPaperType.Estimate, est, false);
			}
			else
			{
				MessageBox.Show("サービスの申込みを行ってください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			string msg;
			int errIndex = this.IsOrderParentSrvice(out msg);
			if (-1 != errIndex)
			{
				MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				listViewService.Items[errIndex].Selected = true;

				// Ver1.030 電子カルテ標準サービス申込時に、１号カルテ標準サービスと２号カルテ標準サービスの申込をチェックする(2018/08/10 勝呂)
				listViewService.EnsureVisible(errIndex);
				return;
			}
			// 申込み情報の取得
			List<GroupService> groupList = null;
			List<ServiceInfo> serviceList = null;
			this.GetOrderService(out groupList, out serviceList);

			if (0 < groupList.Count || 0 < serviceList.Count)
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				string dataFolder = Program.GetDataFolder();

				Span agreeSpan = labelAgreeSpan.Tag as Span;
				if (null == EstimateData)
				{
					// 見積書情報の新規追加
					if (agreeSpan.Start < new Date(dateTimePickerPrintDate.Value))
					{
						MessageBox.Show("契約開始日が発行日より前の日付になっています。ご確認ください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					if (0 == textBoxDestination.Text.Length)
					{
						MessageBox.Show("宛先を入力してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						textBoxDestination.Focus();
						return;
					}
					else
					{
						if (this.IsExistDestination(textBoxDestination.Text))
						{
							// 既存の宛先
							MessageBox.Show("その宛先は既に存在しています。宛先を修正してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
							textBoxDestination.Focus();
							return;
						}
					}
					EstimateData = new Estimate();

					// 宛先の設定
					EstimateData.Destination = textBoxDestination.Text;

					// Ver1.050 見積書および注文書の宛先を「御中」と「様」を変更可能にする(2018/09/26 勝呂)
					if (radioSama.Checked)
					{
						EstimateData.NotUsedMessrs = 1;
					}
					// 発行日の設定
					EstimateData.PrintDate = new Date(dateTimePickerPrintDate.Value);

					// 契約期間
					// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
					EstimateData.AgreeSpan = agreeSpan;

					// 契約月数の設定
					EstimateData.AgreeMonthes = (int)comboBoxTerm.SelectedValue;

					/// 有効期限の設定
					EstimateData.LimitDate = new Date(dateTimePickerLimitDate.Value);

					// 備考
					EstimateData.SetRemark(textBoxRemark.Lines);

					// 申込み種別
					EstimateData.Apply = (radioButtonGroupDisable.Checked) ? Estimate.ApplyType.MatomeNone : Estimate.ApplyType.Matome;

					// 次回見積書情報番号の取得
					EstimateData.EstimateID = SQLiteMwsSimulationAccess.GetLastEstimateNumber(dataFolder);

					// 見積書情報の設定
					// Ver1.050 電子カルテ標準サービス選択時にはTABLETビューワのサービス利用料の500円は加算しない(2018/09/26 勝呂)
					//EstimateData.SetEstimateData(serviceList, groupList);
					EstimateData.SetEstimateData(serviceList, groupList, Program.SERVICE_CODE_CHART_COMPUTE, Program.SERVICE_CODE_TABLETVIEWER);

					try
					{
						// 見積書情報の追加
						if (-1 == SQLiteMwsSimulationSetIO.InsertIntoEstimate(dataFolder, EstimateData))
						{
							return;
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "見積書情報追加エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					}
				}
				else
				{
					// 見積書情報の更新
					if (agreeSpan.Start < new Date(dateTimePickerPrintDate.Value))
					{
						MessageBox.Show("契約開始日が発行日より前の日付になっています。ご確認ください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					if (this.IsExistDestination(textBoxDestination.Text, EstimateData.EstimateID))
					{
						// 既存の宛先
						MessageBox.Show("その宛先は既に存在しています。宛先を修正してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						textBoxDestination.Focus();
						return;
					}
					// 宛先の設定
					EstimateData.Destination = textBoxDestination.Text;

					// Ver1.050 見積書および注文書の宛先を「御中」と「様」を変更可能にする(2018/09/26 勝呂)
					if (radioSama.Checked)
					{
						EstimateData.NotUsedMessrs = 1;
					}
					else
					{
						EstimateData.NotUsedMessrs = 0;
					}
					// 発行日の設定
					EstimateData.PrintDate = new Date(dateTimePickerPrintDate.Value);

					// 契約期間
					// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
					EstimateData.AgreeSpan = agreeSpan;

					// 契約月数の設定
					EstimateData.AgreeMonthes = (int)comboBoxTerm.SelectedValue;

					/// 有効期限の設定
					EstimateData.LimitDate = new Date(dateTimePickerLimitDate.Value);

					// 備考
					EstimateData.SetRemark(textBoxRemark.Lines);

					// 申込み種別
					EstimateData.Apply = (radioButtonGroupDisable.Checked) ? Estimate.ApplyType.MatomeNone : Estimate.ApplyType.Matome;

					// 見積書情報の設定
					// Ver1.050 電子カルテ標準サービス選択時にはTABLETビューワのサービス利用料の500円は加算しない(2018/09/26 勝呂)
					//EstimateData.SetEstimateData(serviceList, groupList);
					EstimateData.SetEstimateData(serviceList, groupList, Program.SERVICE_CODE_CHART_COMPUTE, Program.SERVICE_CODE_TABLETVIEWER);

					try
					{
						if (-1 == SQLiteMwsSimulationSetIO.UpdateEstimate(dataFolder, EstimateData))
						{
							return;
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "見積書情報更新エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
					}
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;

				this.DialogResult = DialogResult.OK;
			}
			else
			{
				this.DialogResult = DialogResult.Cancel;
			}
			this.Close();
		}

		/// <summary>
		/// 破棄
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		/// <summary>
		/// Form Cloesd
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SimulationMatomeForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			MainForm.gSettings.SimulationMatomeFormSize = new Size(this.Width, this.Height);
		}


		////////////////////////////////////////////////////////////////////
		// 内部メソッド

		/// <summary>
		/// 宛先の存在確認
		/// </summary>
		/// <param name="destination">宛先</param>
		/// <param name="id">見積書番号</param>
		/// <returns>判定</returns>
		private bool IsExistDestination(string destination, int id = 0)
		{
			List<Tuple<int, string>> nameList = SQLiteMwsSimulationAccess.GetEstimateDestinationList(Program.GetDataFolder());
			if (0 == id)
			{
				// 新規追加
				if (-1 != nameList.FindIndex(p => p.Item2 == destination))
				{
					return true;
				}
			}
			else
			{
				// 更新
				if (-1 != nameList.FindIndex(p => p.Item1 != id && p.Item2 == destination))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 子サービスの申込に対して親サービスが申し込まれているか判定
		/// </summary>
		/// <param name="msg">エラーメッセージ</param>
		/// <returns>エラー行</returns>
		private int IsOrderParentSrvice(out string msg)
		{
			msg = string.Empty;

			int orderChartComputeIndex = -1;
			bool orderChart1Std = false;
			bool orderChart2Std = false;
			int i = 0;
			foreach (ListViewItem srcItem in listViewService.Items)
			{
				if (srcItem.Checked)
				{
					ServiceInfo srcService = srcItem.Tag as ServiceInfo;

					// Ver1.030 電子カルテ標準サービス申込時に、１号カルテ標準サービスと２号カルテ標準サービスの申込をチェックする(2018/08/10 勝呂)
					if (Program.SERVICE_CODE_CHART_COMPUTE == srcService.ServiceCode)
					{
						// 電子カルテ標準サービス
						orderChartComputeIndex = i;
					}
					else if (Program.SERVICE_CODE_CHART1_STD == srcService.ServiceCode)
					{
						// １号カルテ標準サービス
						orderChart1Std = true;
					}
					else if (Program.SERVICE_CODE_CHART2_STD == srcService.ServiceCode)
					{
						// ２号カルテ標準サービス
						orderChart2Std = true;
					}
					//if (srcService.IsChildService)
					//{
					//	// 親サービスが必須
					//	foreach (ListViewItem dstItem in listViewService.Items)
					//	{
					//		ServiceInfo dstService = dstItem.Tag as ServiceInfo;
					//		if (dstService.ServiceCode == srcService.ParentServiceCode)
					//		{
					//			// 子サービスに対する親サービス
					//			if (false == dstItem.Checked)
					//			{
					//				msg = string.Format("[{0}] には、[{1}] の申込が必要です", srcService.ServiceName, dstService.ServiceName);
					//				return i;
					//			}
					//		}
					//	}
					//}
				}
				i++;
			}
			// 電子カルテ標準サービスには１号カルテ標準サービスおよび２号カルテ標準サービスが必須
			// Ver1.030 電子カルテ標準サービス申込時に、１号カルテ標準サービスと２号カルテ標準サービスの申込をチェックする(2018/08/10 勝呂)
			if (-1 != orderChartComputeIndex)
			{
				// 電子カルテ標準サービス申込み有り
				if (false == orderChart1Std || false == orderChart2Std)
				{
					msg = "[電子カルテ標準サービス] には、[１号カルテ標準サービス] と [２号カルテ標準サービス] の申込が必要です";
					return orderChartComputeIndex;
				}
			}
			return -1;
		}

		/// <summary>
		/// サービス利用料の取得
		/// </summary>
		/// <returns>サービス利用料</returns>
		private int GetServicePrice()
		{
			int price = 0;

			// Ver1.050 電子カルテ標準サービス選択時にはTABLETビューワのサービス利用料の500円は加算しない(2018/09/26 勝呂)
			bool isChartCompute = false;
			foreach (ListViewItem item in listViewService.Items)
			{
				ServiceInfo service = item.Tag as ServiceInfo;
				if (Program.SERVICE_CODE_CHART_COMPUTE == service.ServiceCode)
				{
					// 電子カルテ標準サービス
					if (item.Checked)
					{
						isChartCompute = true;
					}
					break;
				}
			}
			foreach (ListViewItem item in listViewService.Items)
			{
				ServiceInfo service = item.Tag as ServiceInfo;
				if (item.Checked)
				{
					// Ver1.050 電子カルテ標準サービス選択時にはTABLETビューワのサービス利用料の500円は加算しない(2018/09/26 勝呂)
					if (isChartCompute)
					{
						// 電子カルテ標準サービス選択済
						if (Program.SERVICE_CODE_TABLETVIEWER != service.ServiceCode)
						{
							// TABLETビューワ以外
							price += service.Price;
						}
					}
					else
					{
						// 電子カルテ標準サービス未選択
						price += service.Price;
					}
				}
			}
			return price;
		}

		/// <summary>
		/// オススメセット選択後のサービスの設定
		/// </summary>
		/// <param name="plan">オススメセット</param>
		private void SetInitGroupPlanCheckBox(InitGroupPlan plan)
		{
			foreach (ListViewItem item in listViewService.Items)
			{
				item.Checked = false;
			}
			foreach (string code in plan.ServiceCodeList)
			{
				foreach (ListViewItem item in listViewService.Items)
				{
					ServiceInfo service = item.Tag as ServiceInfo;
					if (code == service.ServiceCode)
					{
						item.Checked = true;
						break;
					}
				}
			}
		}

		/// <summary>
		/// サービス利用料の表示
		/// </summary>
		private void DrawServicePrice()
		{
			int servicePrice = this.GetServicePrice();
			if (MainForm.gMinAmmount <= servicePrice)
			{
				// おまとめプラン適用
				radioButtonGroupEnable.Enabled = true;

				// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
				// サービス利用料の表示
				if (radioButtonGroupDisable.Checked)
				{
					// おまとめプラン契約なし

					// 料金と割引額の表示
					if (ExistGroupPlan12)
					{
						textBoxPrice12.Text = string.Format(@"\{0}", StringUtil.CommaEdit(GroupPlanList.GetNormalTotalPrice(12, MainForm.gServiceList.Platform.Price, servicePrice)));
						textBoxPrice12.Tag = string.Format("({0}+{1})x12)", MainForm.gServiceList.Platform.Price, servicePrice);
						textBoxFree12.Text = @"\0";
					}
					if (ExistGroupPlan24)
					{
						textBoxPrice24.Text = string.Format(@"\{0}", StringUtil.CommaEdit(GroupPlanList.GetNormalTotalPrice(24, MainForm.gServiceList.Platform.Price, servicePrice)));
						textBoxPrice24.Tag = string.Format("({0}+{1})x24)", MainForm.gServiceList.Platform.Price, servicePrice);
						textBoxFree24.Text = @"\0";
					}
					if (ExistGroupPlan36)
					{
						textBoxPrice36.Text = string.Format(@"\{0}", StringUtil.CommaEdit(GroupPlanList.GetNormalTotalPrice(36, MainForm.gServiceList.Platform.Price, servicePrice)));
						textBoxPrice36.Tag = string.Format("({0}+{1})x36)", MainForm.gServiceList.Platform.Price, servicePrice);
						textBoxFree36.Text = @"\0";
					}
					// サービス利用料の表示
					textBoxServicePrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(servicePrice));
					textBoxServicePrice.Tag = servicePrice.ToString();

					// 月額利用料の表示
					// Ver1.050 月額利用料の表示の追加(2018/09/27 勝呂)
					textBoxTotalPrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(MainForm.gServiceList.Platform.Price + servicePrice));
				}
				else 
				{
					// おまとめプラン契約あり

					// 料金と割引額の表示
					if (ExistGroupPlan12)
					{
						// 料金の表示
						GroupPlan plan = MainForm.gGroupPlanList.GetMachGroupPlan(12, servicePrice);
						int groupTotalPrice = plan.GetGroupPlanTotalPrice(MainForm.gServiceList.Platform.Price, servicePrice);
						textBoxPrice12.Text = string.Format(@"\{0}", StringUtil.CommaEdit(groupTotalPrice));

						// 割引額の表示
						int normalTotalPrice = GroupPlanList.GetNormalTotalPrice(12, MainForm.gServiceList.Platform.Price, servicePrice);
						if (0 < plan.FreeMonth)
						{
							textBoxPrice12.Tag = string.Format("{0}x12+{1}x{2}", MainForm.gServiceList.Platform.Price, servicePrice, 12 - plan.FreeMonth);
							textBoxFree12.Text = string.Format(@"\{0} ({1}ヵ月)", StringUtil.CommaEdit(normalTotalPrice - groupTotalPrice), plan.FreeMonth);
						}
						else
						{
							textBoxPrice12.Tag = string.Format("({0}+{1})x12)", MainForm.gServiceList.Platform.Price, servicePrice);
							textBoxFree12.Text = string.Format(@"\{0}", StringUtil.CommaEdit(normalTotalPrice - groupTotalPrice));
						}
					}
					if (ExistGroupPlan24)
					{
						// 料金の表示
						GroupPlan plan = MainForm.gGroupPlanList.GetMachGroupPlan(24, servicePrice);
						int groupTotalPrice = plan.GetGroupPlanTotalPrice(MainForm.gServiceList.Platform.Price, servicePrice);
						textBoxPrice24.Text = string.Format(@"\{0}", StringUtil.CommaEdit(groupTotalPrice));

						// 割引額の表示
						int normalTotalPrice = GroupPlanList.GetNormalTotalPrice(24, MainForm.gServiceList.Platform.Price, servicePrice);
						if (0 < plan.FreeMonth)
						{
							textBoxPrice24.Tag = string.Format("{0}x24+{1}x{2}", MainForm.gServiceList.Platform.Price, servicePrice, 24 - plan.FreeMonth);
							textBoxFree24.Text = string.Format(@"\{0} ({1}ヵ月)", StringUtil.CommaEdit(normalTotalPrice - groupTotalPrice), plan.FreeMonth);
						}
						else
						{
							textBoxPrice24.Tag = string.Format("({0}+{1})x24)", MainForm.gServiceList.Platform.Price, servicePrice);
							textBoxFree24.Text = string.Format(@"\{0}", StringUtil.CommaEdit(normalTotalPrice - groupTotalPrice));
						}
					}
					if (ExistGroupPlan36)
					{
						// 料金の表示
						GroupPlan plan = MainForm.gGroupPlanList.GetMachGroupPlan(36, servicePrice);
						int groupTotalPrice = plan.GetGroupPlanTotalPrice(MainForm.gServiceList.Platform.Price, servicePrice);
						textBoxPrice36.Text = string.Format(@"\{0}", StringUtil.CommaEdit(groupTotalPrice));

						// 割引額の表示
						int normalTotalPrice = GroupPlanList.GetNormalTotalPrice(36, MainForm.gServiceList.Platform.Price, servicePrice);
						if (0 < plan.FreeMonth)
						{
							textBoxPrice36.Tag = string.Format("{0}x36+{1}x{2}", MainForm.gServiceList.Platform.Price, servicePrice, 36 - plan.FreeMonth);
							textBoxFree36.Text = string.Format(@"\{0} ({1}ヵ月)", StringUtil.CommaEdit(normalTotalPrice - groupTotalPrice), plan.FreeMonth);
						}
						else
						{
							textBoxPrice36.Tag = string.Format("({0}+{1})x36)", MainForm.gServiceList.Platform.Price, servicePrice);
							textBoxFree36.Text = string.Format(@"\{0}", StringUtil.CommaEdit(normalTotalPrice - groupTotalPrice));
						}
					}
					// サービス利用料の表示
					GroupPlan targetPlan = MainForm.gGroupPlanList.GetMachGroupPlan((int)comboBoxTerm.SelectedValue, servicePrice);
					int groupPrice = targetPlan.GetGroupPlanPrice(servicePrice);
					textBoxServicePrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(groupPrice));
					textBoxServicePrice.Tag = string.Format("({0}x{1})/{2}", servicePrice, (int)comboBoxTerm.SelectedValue - targetPlan.FreeMonth, (int)comboBoxTerm.SelectedValue);

					// 月額利用料の表示
					// Ver1.050 月額利用料の表示の追加(2018/09/27 勝呂)
					textBoxTotalPrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(groupPrice + MainForm.gServiceList.Platform.Price));
				}
			}
			else
			{
				// おまとめプラン適用外
				radioButtonGroupDisable.Checked = true;
				radioButtonGroupEnable.Enabled = false;

				// 料金と割引額の表示
				if (ExistGroupPlan12)
				{
					textBoxPrice12.Text = string.Format(@"\{0}", StringUtil.CommaEdit(GroupPlanList.GetNormalTotalPrice(12, MainForm.gServiceList.Platform.Price, servicePrice)));
					textBoxPrice12.Tag = string.Format("({0}+{1})x12", MainForm.gServiceList.Platform.Price, servicePrice);
					textBoxFree12.Text = @"\0";
				}
				if (ExistGroupPlan24)
				{
					textBoxPrice24.Text = string.Format(@"\{0}", StringUtil.CommaEdit(GroupPlanList.GetNormalTotalPrice(24, MainForm.gServiceList.Platform.Price, servicePrice))); ;
					textBoxPrice24.Tag = string.Format("({0}+{1})x24", MainForm.gServiceList.Platform.Price, servicePrice);
					textBoxFree24.Text = @"\0";
				}
				if (ExistGroupPlan36)
				{
					textBoxPrice36.Text = string.Format(@"\{0}", StringUtil.CommaEdit(GroupPlanList.GetNormalTotalPrice(36, MainForm.gServiceList.Platform.Price, servicePrice))); ;
					textBoxPrice36.Tag = string.Format("({0}+{1})x36", MainForm.gServiceList.Platform.Price, servicePrice);
					textBoxFree36.Text = @"\0";
				}
				// サービス利用料の表示
				textBoxServicePrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(servicePrice));
				textBoxServicePrice.Tag = servicePrice.ToString();

				// 月額利用料の表示
				// Ver1.050 月額利用料の表示の追加(2018/09/27 勝呂)
				textBoxTotalPrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(MainForm.gServiceList.Platform.Price + servicePrice));
			}
			// Ver1.050 おまとめプランが１円から適用できるように修正(2018/09/18 勝呂)
			if (MainForm.gMinFreeMonthMinAmmount <= servicePrice)
			{
				labelGroupPlanMessage.Text = "※おまとめプラン割引が適用できます。";
			}
			else
			{
				labelGroupPlanMessage.Text = string.Format(@"※あと \{0} でおまとめプラン割引が適用できます。", StringUtil.CommaEdit(MainForm.gMinFreeMonthMinAmmount - servicePrice));
			}
			// おまとめプラン契約なしの料金表示
			textBoxNormalPlanTotalPrice12.Text = string.Format(@"\{0}", StringUtil.CommaEdit(GroupPlanList.GetNormalTotalPrice(12, MainForm.gServiceList.Platform.Price, servicePrice)));
			textBoxNormalPlanTotalPrice24.Text = string.Format(@"\{0}", StringUtil.CommaEdit(GroupPlanList.GetNormalTotalPrice(24, MainForm.gServiceList.Platform.Price, servicePrice)));
			textBoxNormalPlanTotalPrice36.Text = string.Format(@"\{0}", StringUtil.CommaEdit(GroupPlanList.GetNormalTotalPrice(36, MainForm.gServiceList.Platform.Price, servicePrice)));
		}

		/// <summary>
		/// 申込み情報の取得
		/// </summary>
		/// <param name="groupList">おまとめプラン</param>
		/// <param name="serviceList"></param>
		private void GetOrderService(out List<GroupService> groupList, out List<ServiceInfo> serviceList)
		{
			groupList = new List<GroupService>();
			serviceList = new List<ServiceInfo>();

			if (radioButtonGroupEnable.Checked)
			{
				// おまとめプラン契約あり
				GroupService groupService = null;
				foreach (ListViewItem item in listViewService.Items)
				{
					if (item.Checked)
					{
						ServiceInfo service = item.Tag as ServiceInfo;

						// おまとめプラン対象サービス
						if (null == groupService)
						{
							groupService = new GroupService();
							groupService.Mode = SQLiteMwsSimulationDef.ServiceMode.Group;

							// MIC WEB SERVICE 標準機能(1001)を含める
							groupService.ServiceCodeList.Add(new Tuple<string, string>(MainForm.gServiceList.Platform.GoodsID, MainForm.gServiceList.Platform.ServiceName));
						}
						groupService.ServiceCodeList.Add(new Tuple<string, string>(service.GoodsID, service.ServiceName));
					}
				}
				if (null != groupService)
				{
					int price = this.GetServicePrice();
					GroupPlan targetPlan = MainForm.gGroupPlanList.GetMachGroupPlan((int)comboBoxTerm.SelectedValue, price);
					groupService.GoodsID = targetPlan.GoodsID;
					groupService.GoodsName = targetPlan.GoodsName;
					groupService.Price = targetPlan.GetGroupPlanTotalPrice(MainForm.gServiceList.Platform.Price, price);
					groupList.Insert(0, groupService);
				}
			}
			else
			{
				// おまとめプラン契約なし
				foreach (ListViewItem item in listViewService.Items)
				{
					ServiceInfo service = item.Tag as ServiceInfo;
					if (item.Checked)
					{
						if (0 == serviceList.Count)
						{
							// MIC WEB SERVICE 標準機能(1001)を含める
							serviceList.Add(MainForm.gServiceList.Platform);
						}
						serviceList.Add(service);
					}
				}
			}
		}

		/// <summary>
		/// 契約期間の表示
		/// </summary>
		/// <param name="agreeSpan">契約期間</param>
		// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
		private void DrawAgreeSpan(Span agreeSpan)
		{
			labelAgreeSpan.Text = agreeSpan.GetJapaneseANString("～", true, '0', true);
		}


		////////////////////////////////////////////////////////////////////
		// 印刷メソッド

		/// <summary>
		/// 見積書の印刷
		/// </summary>
		/// <param name="type">用紙種別</param>
		/// <param name="est">見積書情報</param>
		/// <param name="isPrint">印刷かどうか？</param>
		private void PrintEstimate(PrintEstimateDef.MwsPaperType type, Estimate est, bool isPrint)
		{
			string message;
			if (-1 != PrintInfo.ReadEstimateParameterFile(type, out message))
			{
				// 消費税率の取得
				int taxRate = SQLiteMwsSimulationAccess.GetTaxRate(Program.GetDataFolder(), est.PrintDate);

				// 見積ページ情報の設定
				PrintInfo.SetData(type, est, taxRate);

				// 印刷プレビューダイアログ生成
				using (PrintPreviewForm pf = new PrintPreviewForm())
				{
					MaxPage = PrintInfo.GetMaxPage;

					// 印刷処理開始イベントハンドラの追加
					pf.BeginPrint += new PrintPreviewForm.PrintEventHandler(PrintPreviewForm_BeginPrint);
					// ページ枚の印刷処理イベントハンドラの追加
					pf.PrintPage += new PrintPreviewForm.PrintPageEventHandler(PrintPreviewForm_PrintPage);
					// 印刷終了イベントハンドラの追加
					pf.EndPrint += new PrintPreviewForm.PrintEventHandler(PrintPreviewForm_EndPrint);
					// 印刷ドキュメント                        
					pf.Document = PrintDocument;
					// ページ数
					pf.MaxPage = MaxPage;
					// 表示を最大化
					pf.WindowState = FormWindowState.Maximized;

					if (isPrint)
					{
						// 印刷
						if (pf.Document.PrinterSettings.IsValid)
						{
							pf.Print();
						}
						else
						{
							MessageBox.Show(this, "有効なプリンタが指定されていません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
					}
					else
					{
						// プレビュー
						if (pf.Document.PrinterSettings.IsValid)
						{
							// 印刷プレビューダイアログを表示する
							if (pf.ShowDialog() == DialogResult.OK)
							{
								// PrintPreviewFormで印刷を実行
								;
							}
						}
						else
						{
							MessageBox.Show(this, "有効なプリンタが指定されていません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
					}
				}
			}
			else
			{
				MessageBox.Show(this, message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}

		/// <summary>
		/// PalettePrintPreviewForm BeginPrintイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PrintPreviewForm_BeginPrint(object sender, PrintEventArgs e)
		{
			PrintDocument printDocument = ((PrintPreviewForm)sender).Document;

			if (!printDocument.PrintController.IsPreview)
			{
				// 印刷の選択ダイアログを表示
				using (PrintDialog pdlg = new PrintDialog())
				{
					PrintDocument pd = ((PrintPreviewForm)sender).Document;
					pdlg.Document = pd;
					pdlg.AllowSomePages = true;
					pdlg.PrinterSettings.MinimumPage = 1;
					pdlg.PrinterSettings.MaximumPage = MaxPage;
					pdlg.PrinterSettings.FromPage = pdlg.PrinterSettings.MinimumPage;
					pdlg.PrinterSettings.ToPage = pdlg.PrinterSettings.MaximumPage;
					if (DialogResult.Cancel == pdlg.ShowDialog())
					{
						// 印刷のキャンセル
						e.Cancel = true;
						return;
					}
					pd.PrinterSettings = pdlg.PrinterSettings;
					MaxPage = pdlg.PrinterSettings.ToPage;
				}
			}
			printDocument.DocumentName = PrintEstimateDef.GetDocumentName(PrintInfo.PaperType, PrintInfo.PrintData.Destination);
		}

		/// <summary>
		/// PrintPreviewForm PrintPageイベント
		/// </summary>
		private void PrintPreviewForm_PrintPage(object sender, PrintPageEventArgs e, int page)
		{
			PrintDocument printDocument = ((PrintPreviewForm)sender).Document;

			e.Graphics.PageUnit = GraphicsUnit.Display;

			//Point offset = ClientIniIO.GetClientIntroduceOffset(PrintIntroduce.PaperName);

			//if (printDocument.PrintController.IsPreview)
			//{
			//	// プレビューのハードマージン分のずれを補正する

			//	// 印字不可領域を、1/100inchから0.1mm単位に変換する
			//	float x = PrintPara.ToMillimeter(printDocument.DefaultPageSettings.HardMarginX);
			//	float y = PrintPara.ToMillimeter(printDocument.DefaultPageSettings.HardMarginY);

			//	offset.X += (int)x;
			//	offset.Y += (int)y;
			//}

			//// 基底引数
			//var args = this.PaletteSystemInfo as VisitArgs;

			// 印刷処理
			PrintInfo.PrintEstimateData(e.Graphics, MainForm.gSettings.PaperOffset, page, false);
		}

		/// <summary>
		/// PrintPreviewForm EndPrintイベント
		/// </summary>
		private void PrintPreviewForm_EndPrint(object sender, PrintEventArgs e)
		{
			//PrintDocument printDocument = ((PrintPreviewForm)sender).Document;

			//if (!printDocument.PrintController.IsPreview)
			//{
			//	if (!e.Cancel)
			//	{
			//		// プリンタ発行情報をINIファイルに記憶する
			//		ClientPrinterInfoIni pi = new ClientPrinterInfoIni();
			//		pi.ReadPrintDocument(printDocument);
			//		ClientIniIO.SetClientIntroducePrinterInfo(PrintIntroduce.PaperName, pi);
			//	}
			//}
		}


		////////////////////////////////////////////////////////////////////
		// Debugメソッド

		private void textBoxPrice12_MouseDoubleClick(object sender, MouseEventArgs e)
		{
#if DEBUG
			if (null != textBoxPrice12.Tag)
			{
				MessageBox.Show(textBoxPrice12.Tag as string);
			}
#endif
		}

		private void textBoxPrice24_MouseDoubleClick(object sender, MouseEventArgs e)
		{
#if DEBUG
			if (null != textBoxPrice24.Tag)
			{
				MessageBox.Show(textBoxPrice24.Tag as string);
			}
#endif
		}

		private void textBoxPrice36_MouseDoubleClick(object sender, MouseEventArgs e)
		{
#if DEBUG
			if (null != textBoxPrice36.Tag)
			{
				MessageBox.Show(textBoxPrice36.Tag as string);
			}
#endif
		}

		private void textBoxTotalPrice_MouseDoubleClick(object sender, MouseEventArgs e)
		{
#if DEBUG
			if (null != textBoxServicePrice.Tag)
			{
				MessageBox.Show(textBoxServicePrice.Tag as string);
			}
#endif
		}
	}
}
