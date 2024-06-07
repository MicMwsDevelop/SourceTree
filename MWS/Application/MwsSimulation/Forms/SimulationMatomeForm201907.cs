//
// SimulationMatomeForm201907.cs
//
// おまとめプラン御見積書作成画面（2019/07～12ヵ月、36ヵ月、60ヵ月）
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
// Ver2.100 おまとめプラン48ヵ月、60ヵ月に対応(2019/01/22 勝呂)
// Ver2.101 おまとめプランの選択を12ヵ月、36ヵ月、60ヵ月に変更(2019/07/19 勝呂)
// Ver2.30(2024/05/20 勝呂):サービス情報マスタにフィールドを追加して、おまとめプランに含めるかどうかの判断するように仕様変更
// 
using CommonDialog.PrintPreview;
using CommonLib.BaseFactory;
using CommonLib.BaseFactory.MwsSimulation;
using CommonLib.Common;
using CommonLib.DB.SQLite.MwsSimulation;
using MwsSimulation.Print;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace MwsSimulation.Forms
{
	/// <summary>
	/// おまとめプラン 御見積書作成画面（2019/07～12ヵ月、36ヵ月、60ヵ月）
	/// </summary>
	public partial class SimulationMatomeForm201907 : Form
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
		/// おまとめプラン12ヵ月がマスターに存在する
		/// </summary>
		private bool ExistMatome12 { get; set; }

		/// <summary>
		/// おまとめプラン36ヵ月プランがマスターに存在する
		/// </summary>
		private bool ExistMatome36 { get; set; }

		/// <summary>
		/// おまとめプラン60ヵ月がマスターに存在する
		/// </summary>
		private bool ExistMatome60 { get; set; }

		/// <summary>
		/// 見積書情報
		/// </summary>
		public Estimate EstimateData { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SimulationMatomeForm201907()
		{
			InitializeComponent();

			PrintInfo = new PrintEstimate();
			PrintDocument = new PrintDocument();
			MaxPage = 0;
			ExistMatome12 = false;
			ExistMatome36 = false;
			ExistMatome60 = false;
			EstimateData = null;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SimulationMatomeForm201907(Estimate est)
		{
			InitializeComponent();

			PrintInfo = new PrintEstimate();
			PrintDocument = new PrintDocument();
			MaxPage = 0;
			ExistMatome12 = false;
			ExistMatome36 = false;
			ExistMatome60 = false;
			EstimateData = est;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SimulationMatomeNewForm_Load(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			// イベントハンドラ削除
			listViewService.ItemCheck -= new ItemCheckEventHandler(listViewService_ItemCheck);
			listViewService.ItemChecked -= new ItemCheckedEventHandler(listViewService_ItemChecked);
			radioButtonNormal12.CheckedChanged -= new EventHandler(radioButtonNormal12_CheckedChanged);
			radioButtonNormal36.CheckedChanged -= new EventHandler(radioButtonNormal36_CheckedChanged);
			radioButtonNormal60.CheckedChanged -= new EventHandler(radioButtonNormal60_CheckedChanged);
			radioButtonMatome12.CheckedChanged -= new EventHandler(radioButtonMatome12_CheckedChanged);
			radioButtonMatome36.CheckedChanged -= new EventHandler(radioButtonMatome36_CheckedChanged);
			radioButtonMatome60.CheckedChanged -= new EventHandler(radioButtonMatome60_CheckedChanged);
			dateTimePickerPrintDate.ValueChanged -= new EventHandler(dateTimePickerPrintDate_ValueChanged);

			// ウィンドウサイズの変更
			this.Width = MainForm.gSettings.SimulationMatomeFormSize.Width;
			this.Height = MainForm.gSettings.SimulationMatomeFormSize.Height;

			// サービス情報リストビューの設定
			listViewService.BeginUpdate();
			foreach (ServiceInfo service in MainForm.gServiceList)
			{
				// Ver2.30(2024/05/20 勝呂):サービス情報マスタにフィールドを追加して、おまとめプランに含めるかどうかの判断するように仕様変更
				//if (Program.SERVICE_CODE_REMOTE != service.ServiceCode && true == service.IsGroupPlanService)
				if (service.IsGroupPlanService)
				{
					// おまとめプラン対象サービス
					ListViewItem lvItem = new ListViewItem(service.GetListViewData());
					lvItem.Tag = service;
					listViewService.Items.Add(lvItem);
				}
			}
			listViewService.EndUpdate();

			if (MainForm.gGroupPlanList201907.IsExistKeiyakuMonth(12))
			{
				// おまとめプラン12ヵ月プランが有効
				ExistMatome12 = true;
				radioButtonNormal12.Enabled = true;
				textBoxNormalTotalPrice12.Enabled = true;
				textBoxMatomeTotalPrice12.Enabled = true;
				textBoxMatomeFree12.Enabled = true;
			}
			if (MainForm.gGroupPlanList201907.IsExistKeiyakuMonth(36))
			{
				// おまとめプラン36ヵ月プランが有効
				ExistMatome36 = true;
				radioButtonNormal36.Enabled = true;
				textBoxNormalTotalPrice36.Enabled = true;
				textBoxMatomeTotalPrice36.Enabled = true;
				textBoxMatomeFree36.Enabled = true;
			}
			if (MainForm.gGroupPlanList201907.IsExistKeiyakuMonth(60))
			{
				// おまとめプラン60ヵ月が有効
				ExistMatome60 = true;
				radioButtonNormal60.Enabled = true;
				textBoxNormalTotalPrice60.Enabled = true;
				textBoxMatomeTotalPrice60.Enabled = true;
				textBoxMatomeFree60.Enabled = true;
			}
			// オススメセットの設定
			if (3 == MainForm.gInitGroupPlanList.Count)
			{
				buttonInitMatomePlan1.Text = MainForm.gInitGroupPlanList[0].GroupName;
				buttonInitMatomePlan1.Tag = MainForm.gInitGroupPlanList[0];
				buttonInitMatomePlan2.Text = MainForm.gInitGroupPlanList[1].GroupName;
				buttonInitMatomePlan2.Tag = MainForm.gInitGroupPlanList[1];
				buttonInitMatomePlan3.Text = MainForm.gInitGroupPlanList[2].GroupName;
				buttonInitMatomePlan3.Tag = MainForm.gInitGroupPlanList[2];
			}
			if (null != EstimateData)
			{
				// 宛先の設定
				textBoxDestination.Text = EstimateData.Destination;

				// 御中/様
				if (0 != EstimateData.NotUsedMessrs)
				{
					radioSama.Checked = true;
				}
				// 発行日の設定
				dateTimePickerPrintDate.Value = EstimateData.PrintDate.ToDateTime();

				// 契約期間の設定
				labelAgreeSpan.Tag = EstimateData.AgreeSpan;

				// 有効期限の設定
				dateTimePickerLimitDate.Value = EstimateData.LimitDate.ToDateTime();

				// 備考の設定
				textBoxRemark.Lines = EstimateData.GetRemark();

				if (Estimate.ApplyType.Matome == EstimateData.Apply)
				{
					// おまとめプラン契約あり
					switch (EstimateData.AgreeMonthes)
					{
						case 12:
							radioButtonMatome12.Checked = true;
							break;
						case 36:
							radioButtonMatome36.Checked = true;
							break;
						case 60:
							radioButtonMatome60.Checked = true;
							break;
					}
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
				}
				else
				{
					// おまとめプラン契約なし
					switch (EstimateData.AgreeMonthes)
					{
						case 12:
							radioButtonNormal12.Checked = true;
							break;
						case 36:
							radioButtonNormal36.Checked = true;
							break;
						case 60:
							radioButtonNormal60.Checked = true;
							break;
					}
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
				// 契約月数を36ヵ月で設定
				radioButtonNormal36.Checked = true;

				// 契約期間の設定
				labelAgreeSpan.Tag = Estimate.GetAgreeSapn(true, Date.Today, 36);

				// 有効期限の設定
				dateTimePickerLimitDate.Value  = Estimate.GetLimitDate(Date.Today).ToDateTime();
			}
			// プラットフォーム利用料の設定
			textBoxPlatformPrice.Text = @"\" + StringUtil.CommaEdit(MainForm.gServiceList.Platform.Price);

			// 契約期間の表示
			this.DrawAgreeSpan((Span)labelAgreeSpan.Tag);

			// サービス利用料及び月額利用料の表示
			this.DrawServicePrice();

			// イベントハンドラ追加
			listViewService.ItemCheck += new ItemCheckEventHandler(listViewService_ItemCheck);
			listViewService.ItemChecked += new ItemCheckedEventHandler(listViewService_ItemChecked);
			radioButtonNormal12.CheckedChanged += new EventHandler(radioButtonNormal12_CheckedChanged);
			radioButtonNormal36.CheckedChanged += new EventHandler(radioButtonNormal36_CheckedChanged);
			radioButtonNormal60.CheckedChanged += new EventHandler(radioButtonNormal60_CheckedChanged);
			radioButtonMatome12.CheckedChanged += new EventHandler(radioButtonMatome12_CheckedChanged);
			radioButtonMatome36.CheckedChanged += new EventHandler(radioButtonMatome36_CheckedChanged);
			radioButtonMatome60.CheckedChanged += new EventHandler(radioButtonMatome60_CheckedChanged);
			dateTimePickerPrintDate.ValueChanged += new EventHandler(dateTimePickerPrintDate_ValueChanged);

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
				if ((int)ServiceCodeDefine.ServiceCode.ElectricChartStandard == targetService.ServiceCode)
				{
					// 電子カルテ標準サービス選択時には１号カルテ標準サービス、２号カルテ標準サービス、TABLETビューワ、paletteアカウントのサービスを選択状態にする
					foreach (ListViewItem item in listViewService.Items)
					{
						if (false == item.Checked)
						{
							ServiceInfo svr = item.Tag as ServiceInfo;
							if ((int)ServiceCodeDefine.ServiceCode.Chart1Standard == svr.ServiceCode || (int)ServiceCodeDefine.ServiceCode.Chart2Standard == svr.ServiceCode || (int)ServiceCodeDefine.ServiceCode.TabletViewer == svr.ServiceCode || (int)ServiceCodeDefine.ServiceCode.ExPaletteAccount == svr.ServiceCode)
							{
								// １号カルテ標準サービス、２号カルテ標準サービス、TABLETビューワ、paletteアカウント
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
			labelAgreeSpan.Tag = Estimate.GetAgreeSapn(true, printDate, this.GetAgreeMonthes());

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
		private void buttonChangeAgreeSpan_Click(object sender, EventArgs e)
		{
			using (AgreeSpanForm form = new AgreeSpanForm((Span)labelAgreeSpan.Tag, this.GetAgreeMonthes()))
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
		/// オススメセット１の選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInitMatomePlan1_Click(object sender, EventArgs e)
		{
			InitGroupPlan plan = buttonInitMatomePlan1.Tag as InitGroupPlan;
			this.SetInitMatomePlanCheckBox(plan);
		}

		/// <summary>
		/// オススメセット２の選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInitMatomePlan2_Click(object sender, EventArgs e)
		{
			InitGroupPlan plan = buttonInitMatomePlan2.Tag as InitGroupPlan;
			this.SetInitMatomePlanCheckBox(plan);
		}

		/// <summary>
		/// オススメセット３の選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInitMatomePlan3_Click(object sender, EventArgs e)
		{
			InitGroupPlan plan = buttonInitMatomePlan3.Tag as InitGroupPlan;
			this.SetInitMatomePlanCheckBox(plan);
		}

		/// <summary>
		/// 12ヵ月
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonNormal12_CheckedChanged(object sender, EventArgs e)
		{
			if (null != labelAgreeSpan.Tag)
			{
				// 契約期間の設定
				Span agreeSpan = labelAgreeSpan.Tag as Span;
				labelAgreeSpan.Tag = new Span(agreeSpan.Start, Estimate.GetAgreeEndDate(agreeSpan.Start, this.GetAgreeMonthes()));

				// 契約期間の表示
				DrawAgreeSpan((Span)labelAgreeSpan.Tag);
			}
			// サービス利用料の表示
			this.DrawServicePrice();
		}

		/// <summary>
		/// 36ヵ月
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonNormal36_CheckedChanged(object sender, EventArgs e)
		{
			if (null != labelAgreeSpan.Tag)
			{
				// 契約期間の設定
				Span agreeSpan = labelAgreeSpan.Tag as Span;
				labelAgreeSpan.Tag = new Span(agreeSpan.Start, Estimate.GetAgreeEndDate(agreeSpan.Start, this.GetAgreeMonthes()));

				// 契約期間の表示
				DrawAgreeSpan((Span)labelAgreeSpan.Tag);
			}
			// サービス利用料の表示
			this.DrawServicePrice();
		}

		/// <summary>
		/// 60ヵ月
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonNormal60_CheckedChanged(object sender, EventArgs e)
		{
			if (null != labelAgreeSpan.Tag)
			{
				// 契約期間の設定
				Span agreeSpan = labelAgreeSpan.Tag as Span;
				labelAgreeSpan.Tag = new Span(agreeSpan.Start, Estimate.GetAgreeEndDate(agreeSpan.Start, this.GetAgreeMonthes()));

				// 契約期間の表示
				DrawAgreeSpan((Span)labelAgreeSpan.Tag);
			}
			// サービス利用料の表示
			this.DrawServicePrice();
		}

		/// <summary>
		/// 12ヵ月プラン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonMatome12_CheckedChanged(object sender, EventArgs e)
		{
			if (null != labelAgreeSpan.Tag)
			{
				// 契約期間の設定
				Span agreeSpan = labelAgreeSpan.Tag as Span;
				labelAgreeSpan.Tag = new Span(agreeSpan.Start, Estimate.GetAgreeEndDate(agreeSpan.Start, this.GetAgreeMonthes()));

				// 契約期間の表示
				DrawAgreeSpan((Span)labelAgreeSpan.Tag);
			}
			// サービス利用料の表示
			this.DrawServicePrice();
		}

		/// <summary>
		/// 36ヵ月プラン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonMatome36_CheckedChanged(object sender, EventArgs e)
		{
			if (null != labelAgreeSpan.Tag)
			{
				// 契約期間の設定
				Span agreeSpan = labelAgreeSpan.Tag as Span;
				labelAgreeSpan.Tag = new Span(agreeSpan.Start, Estimate.GetAgreeEndDate(agreeSpan.Start, this.GetAgreeMonthes()));

				// 契約期間の表示
				DrawAgreeSpan((Span)labelAgreeSpan.Tag);
			}
			// サービス利用料の表示
			this.DrawServicePrice();
		}

		/// <summary>
		/// 60ヵ月プラン
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonMatome60_CheckedChanged(object sender, EventArgs e)
		{
			if (null != labelAgreeSpan.Tag)
			{
				// 契約期間の設定
				Span agreeSpan = labelAgreeSpan.Tag as Span;
				labelAgreeSpan.Tag = new Span(agreeSpan.Start, Estimate.GetAgreeEndDate(agreeSpan.Start, this.GetAgreeMonthes()));

				// 契約期間の表示
				DrawAgreeSpan((Span)labelAgreeSpan.Tag);
			}
			// サービス利用料の表示
			this.DrawServicePrice();
		}

		/// <summary>
		/// 定型文からの備考入力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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
			if (DialogResult.Yes == MessageBox.Show("すべてのサービスを選択します。よろしいですか？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
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
		}

		/// <summary>
		/// 全解除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonAllOff_Click(object sender, EventArgs e)
		{
			if (DialogResult.Yes == MessageBox.Show("すべてのサービスを解除します。よろしいですか？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
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

				// 御中/様
				if (radioSama.Checked)
				{
					est.NotUsedMessrs = 1;
				}
				// 発行日
				est.PrintDate = new Date(dateTimePickerPrintDate.Value);

				// 契約期間の設定
				est.AgreeSpan = labelAgreeSpan.Tag as Span;

				// 契約月数の設定
				est.AgreeMonthes = this.GetAgreeMonthes();

				// 有効期限の設定
				est.LimitDate = new Date(dateTimePickerLimitDate.Value);

				// 備考
				est.SetRemark(textBoxRemark.Lines);

				// 申込み種別
				est.Apply = (radioButtonNormal12.Checked || radioButtonNormal36.Checked || radioButtonNormal60.Checked) ? Estimate.ApplyType.MatomeNone: Estimate.ApplyType.Matome;

				// 見積書情報の設定
				est.SetEstimateData(serviceList, groupList, ServiceCodeDefine.ServiceCode.ElectricChartStandard, ServiceCodeDefine.ServiceCode.TabletViewer);

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
					if (new Date(dateTimePickerLimitDate.Value) < new Date(dateTimePickerPrintDate.Value))
					{
						MessageBox.Show("有効期限が発行日より前の日付になっています。ご確認ください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
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

					// 御中/様
					if (radioSama.Checked)
					{
						EstimateData.NotUsedMessrs = 1;
					}
					// 発行日の設定
					EstimateData.PrintDate = new Date(dateTimePickerPrintDate.Value);

					// 契約期間
					EstimateData.AgreeSpan = agreeSpan;

					// 契約月数の設定
					EstimateData.AgreeMonthes = this.GetAgreeMonthes();

					/// 有効期限の設定
					EstimateData.LimitDate = new Date(dateTimePickerLimitDate.Value);

					// 備考
					EstimateData.SetRemark(textBoxRemark.Lines);

					// 申込み種別
					EstimateData.Apply = (radioButtonNormal12.Checked || radioButtonNormal36.Checked || radioButtonNormal60.Checked) ? Estimate.ApplyType.MatomeNone : Estimate.ApplyType.Matome;

					// 次回見積書情報番号の取得
					EstimateData.EstimateID = SQLiteMwsSimulationAccess.GetLastEstimateNumber(dataFolder);

					// 見積書情報の設定
					EstimateData.SetEstimateData(serviceList, groupList, ServiceCodeDefine.ServiceCode.ElectricChartStandard, ServiceCodeDefine.ServiceCode.TabletViewer);

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
					if (new Date(dateTimePickerLimitDate.Value) < new Date(dateTimePickerPrintDate.Value))
					{
						MessageBox.Show("有効期限が発行日より前の日付になっています。ご確認ください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
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

					// 御中/様
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
					EstimateData.AgreeSpan = agreeSpan;

					// 契約月数の設定
					EstimateData.AgreeMonthes = this.GetAgreeMonthes();

					/// 有効期限の設定
					EstimateData.LimitDate = new Date(dateTimePickerLimitDate.Value);

					// 備考
					EstimateData.SetRemark(textBoxRemark.Lines);

					// 申込み種別
					EstimateData.Apply = (radioButtonNormal12.Checked || radioButtonNormal36.Checked || radioButtonNormal60.Checked) ? Estimate.ApplyType.MatomeNone : Estimate.ApplyType.Matome;

					// 見積書情報の設定
					EstimateData.SetEstimateData(serviceList, groupList, ServiceCodeDefine.ServiceCode.ElectricChartStandard, ServiceCodeDefine.ServiceCode.TabletViewer);

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
		private void SimulationMatomeNewForm_FormClosed(object sender, FormClosedEventArgs e)
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

			int orderElectricChartIndex = -1;
			bool orderChart1Std = false;
			bool orderChart2Std = false;
			int i = 0;
			foreach (ListViewItem srcItem in listViewService.Items)
			{
				if (srcItem.Checked)
				{
					ServiceInfo srcService = srcItem.Tag as ServiceInfo;
					if ((int)ServiceCodeDefine.ServiceCode.ElectricChartStandard == srcService.ServiceCode)
					{
						// 電子カルテ標準サービス
						orderElectricChartIndex = i;
					}
					else if ((int)ServiceCodeDefine.ServiceCode.Chart1Standard == srcService.ServiceCode)
					{
						// １号カルテ標準サービス
						orderChart1Std = true;
					}
					else if ((int)ServiceCodeDefine.ServiceCode.Chart2Standard == srcService.ServiceCode)
					{
						// ２号カルテ標準サービス
						orderChart2Std = true;
					}
				}
				i++;
			}
			// 電子カルテ標準サービスには１号カルテ標準サービスおよび２号カルテ標準サービスが必須
			if (-1 != orderElectricChartIndex)
			{
				// 電子カルテ標準サービス申込み有り
				if (false == orderChart1Std || false == orderChart2Std)
				{
					msg = "[電子カルテ標準サービス] には、[１号カルテ標準サービス] と [２号カルテ標準サービス] の申込が必要です";
					return orderElectricChartIndex;
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
			bool isElectricChart = false;
			foreach (ListViewItem item in listViewService.Items)
			{
				ServiceInfo service = item.Tag as ServiceInfo;
				if ((int)ServiceCodeDefine.ServiceCode.ElectricChartStandard == service.ServiceCode)
				{
					// 電子カルテ標準サービス
					if (item.Checked)
					{
						isElectricChart = true;
					}
					break;
				}
			}
			foreach (ListViewItem item in listViewService.Items)
			{
				ServiceInfo service = item.Tag as ServiceInfo;
				if (item.Checked)
				{
					if (isElectricChart)
					{
						// 電子カルテ標準サービス選択済
						if ((int)ServiceCodeDefine.ServiceCode.TabletViewer != service.ServiceCode)
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
		private void SetInitMatomePlanCheckBox(InitGroupPlan plan)
		{
			foreach (ListViewItem item in listViewService.Items)
			{
				item.Checked = false;
			}
			foreach (int code in plan.ServiceCodeList)
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
			if (0 == servicePrice)
			{
				// おまとめプラン契約なし
				textBoxNormalTotalPrice12.Text = @"\0";
				textBoxNormalTotalPrice36.Text = @"\0";
				textBoxNormalTotalPrice60.Text = @"\0";
				textBoxNormalMonthlyPrice.Text = @"\0";

				// おまとめプラン契約あり
				textBoxMatomeTotalPrice12.Text = @"\0";
				textBoxMatomeTotalPrice36.Text = @"\0";
				textBoxMatomeTotalPrice60.Text = @"\0";
				textBoxMatomeMonthlyPrice12.Text = @"\0";
				textBoxMatomeMonthlyPrice36.Text = @"\0";
				textBoxMatomeMonthlyPrice60.Text = @"\0";
				textBoxMatomeFree12.Text = @"\0";
				textBoxMatomeFree36.Text = @"\0";
				textBoxMatomeFree60.Text = @"\0";

				// プラットフォーム利用料
				textBoxPlatformPrice.Text = @"\0";

				// サービス利用料
				textBoxServicePrice.Text = @"\0";

				// 月額利用料
				textBoxMonthlyPrice.Text = @"\0";

				// おまとめプラン未適用金額
				radioButtonMatome12.Enabled = false;
				radioButtonMatome36.Enabled = false;
				radioButtonMatome60.Enabled = false;
				radioButtonNormal36.Checked = true;

				labelMatomeMessage.Text = string.Format(@"※あと \{0} でおまとめプラン割引が適用できます。", StringUtil.CommaEdit(MainForm.gServiceList.Platform.Price + MainForm.gMinFreeMonthMinAmmount201907));
			}
			else
			{
				int agreeMonthes = this.GetAgreeMonthes();

				// おまとめプラン契約なし
				int normalTotalPrice12 = GroupPlanList.GetNormalTotalPrice(12, MainForm.gServiceList.Platform.Price, servicePrice);
				int normalTotalPrice36 = GroupPlanList.GetNormalTotalPrice(36, MainForm.gServiceList.Platform.Price, servicePrice);
				int normalTotalPrice60 = GroupPlanList.GetNormalTotalPrice(60, MainForm.gServiceList.Platform.Price, servicePrice);
				textBoxNormalTotalPrice12.Text = string.Format(@"\{0}", StringUtil.CommaEdit(normalTotalPrice12));
				textBoxNormalTotalPrice36.Text = string.Format(@"\{0}", StringUtil.CommaEdit(normalTotalPrice36));
				textBoxNormalTotalPrice60.Text = string.Format(@"\{0}", StringUtil.CommaEdit(normalTotalPrice60));
				textBoxNormalMonthlyPrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(MainForm.gServiceList.Platform.Price + servicePrice));

				// おまとめプラン契約あり
				GroupPlan plan12 = MainForm.gGroupPlanList201907.GetMachGroupPlan(12, servicePrice);
				GroupPlan plan36 = MainForm.gGroupPlanList201907.GetMachGroupPlan(36, servicePrice);
				GroupPlan plan60 = MainForm.gGroupPlanList201907.GetMachGroupPlan(60, servicePrice);
				int matomeTotalPrice12 = plan12.GetGroupPlanTotalPrice(MainForm.gServiceList.Platform.Price, servicePrice);
				int matomeTotalPrice36 = plan36.GetGroupPlanTotalPrice(MainForm.gServiceList.Platform.Price, servicePrice);
				int matomeTotalPrice60 = plan60.GetGroupPlanTotalPrice(MainForm.gServiceList.Platform.Price, servicePrice);
				textBoxMatomeTotalPrice12.Text = string.Format(@"\{0}", StringUtil.CommaEdit(matomeTotalPrice12));
				textBoxMatomeTotalPrice36.Text = string.Format(@"\{0}", StringUtil.CommaEdit(matomeTotalPrice36));
				textBoxMatomeTotalPrice60.Text = string.Format(@"\{0}", StringUtil.CommaEdit(matomeTotalPrice60));
				int matomeMonthlyPrice12 = plan12.GetGroupPlanPrice(servicePrice);
				int matomeMonthlyPrice36 = plan36.GetGroupPlanPrice(servicePrice);
				int matomeMonthlyPrice60 = plan60.GetGroupPlanPrice(servicePrice);
				textBoxMatomeMonthlyPrice12.Text = string.Format(@"\{0}", StringUtil.CommaEdit(MainForm.gServiceList.Platform.Price + matomeMonthlyPrice12));
				textBoxMatomeMonthlyPrice36.Text = string.Format(@"\{0}", StringUtil.CommaEdit(MainForm.gServiceList.Platform.Price + matomeMonthlyPrice36));
				textBoxMatomeMonthlyPrice60.Text = string.Format(@"\{0}", StringUtil.CommaEdit(MainForm.gServiceList.Platform.Price + matomeMonthlyPrice60));

				if (0 < plan12.FreeMonth)
				{
					textBoxMatomeTotalPrice12.Tag = string.Format("{0}x12+{1}x{2}", MainForm.gServiceList.Platform.Price, servicePrice, 12 - plan12.FreeMonth);
					textBoxMatomeFree12.Text = string.Format(@"\{0} ({1}ヵ月)", StringUtil.CommaEdit(normalTotalPrice12 - matomeTotalPrice12), plan12.FreeMonth);
				}
				else
				{
					textBoxMatomeTotalPrice12.Tag = string.Format("({0}+{1})x12", MainForm.gServiceList.Platform.Price, servicePrice);
					textBoxMatomeFree12.Text = @"\0";
				}
				if (0 < plan36.FreeMonth)
				{
					textBoxMatomeTotalPrice36.Tag = string.Format("{0}x36+{1}x{2}", MainForm.gServiceList.Platform.Price, servicePrice, 36 - plan36.FreeMonth);
					textBoxMatomeFree36.Text = string.Format(@"\{0} ({1}ヵ月)", StringUtil.CommaEdit(normalTotalPrice36 - matomeTotalPrice36), plan36.FreeMonth);
				}
				else
				{
					textBoxMatomeTotalPrice36.Tag = string.Format("({0}+{1})x36", MainForm.gServiceList.Platform.Price, servicePrice);
					textBoxMatomeFree36.Text = @"\0";
				}
				if (0 < plan60.FreeMonth)
				{
					textBoxMatomeTotalPrice60.Tag = string.Format("{0}x60+{1}x{2}", MainForm.gServiceList.Platform.Price, servicePrice, 60 - plan60.FreeMonth);
					textBoxMatomeFree60.Text = string.Format(@"\{0} ({1}ヵ月)", StringUtil.CommaEdit(normalTotalPrice60 - matomeTotalPrice60), plan60.FreeMonth);
				}
				else
				{
					textBoxMatomeTotalPrice60.Tag = string.Format("({0}+{1})x60", MainForm.gServiceList.Platform.Price, servicePrice);
					textBoxMatomeFree60.Text = @"\0";
				}
				if (MainForm.gMinFreeMonthMinAmmount201907 <= servicePrice)
				{
					labelMatomeMessage.Text = "※おまとめプラン割引が適用できます。";
				}
				else
				{
					labelMatomeMessage.Text = string.Format(@"※あと \{0} でおまとめプラン割引が適用できます。", StringUtil.CommaEdit(MainForm.gMinFreeMonthMinAmmount201907 - servicePrice));
				}
				if (MainForm.gMinAmmount201907 <= servicePrice)
				{
					// おまとめプラン適用金額
					radioButtonMatome12.Enabled = true;
					radioButtonMatome36.Enabled = true;
					radioButtonMatome60.Enabled = true;
				}
				else
				{
					// おまとめプラン未適用金額
					radioButtonMatome12.Enabled = false;
					radioButtonMatome36.Enabled = false;
					radioButtonMatome60.Enabled = false;
					radioButtonNormal36.Checked = true;
				}
				// プラットフォーム利用料
				textBoxPlatformPrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(MainForm.gServiceList.Platform.Price));

				if (radioButtonNormal12.Checked || radioButtonNormal36.Checked || radioButtonNormal60.Checked)
				{
					// おまとめプラン契約なし

					// サービス利用料
					textBoxServicePrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(servicePrice));

					// 月額利用料
					textBoxMonthlyPrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(MainForm.gServiceList.Platform.Price + servicePrice));
				}
				else
				{
					// おまとめプラン契約あり
					if (radioButtonMatome12.Checked)
					{
						// サービス利用料
						textBoxServicePrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(matomeMonthlyPrice12));

						// 月額利用料
						textBoxMonthlyPrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(MainForm.gServiceList.Platform.Price + matomeMonthlyPrice12));
					}
					else if (radioButtonMatome36.Checked)
					{
						// サービス利用料
						textBoxServicePrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(matomeMonthlyPrice36));

						// 月額利用料
						textBoxMonthlyPrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(MainForm.gServiceList.Platform.Price + matomeMonthlyPrice36));
					}
					else
					{
						// サービス利用料
						textBoxServicePrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(matomeMonthlyPrice60));

						// 月額利用料
						textBoxMonthlyPrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(MainForm.gServiceList.Platform.Price + matomeMonthlyPrice60));
					}
				}
			}
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

			if (radioButtonMatome12.Checked || radioButtonMatome36.Checked || radioButtonMatome60.Checked)
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
					GroupPlan targetPlan = MainForm.gGroupPlanList201907.GetMachGroupPlan(this.GetAgreeMonthes(), price);
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
		private void DrawAgreeSpan(Span agreeSpan)
		{
			labelAgreeSpan.Text = agreeSpan.GetJapaneseANString("～", true, '0', true);
		}

		/// <summary>
		/// 契約月数の取得
		/// </summary>
		/// <returns>契約月数</returns>
		private int GetAgreeMonthes()
		{
			if (radioButtonNormal12.Checked || radioButtonMatome12.Checked)
			{
				return 12;
			}
			if (radioButtonNormal36.Checked || radioButtonMatome36.Checked)
			{
				return 36;
			}
			return 60;
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
				short taxRate = SQLiteMwsSimulationAccess.GetTaxRate(Program.GetDataFolder(), est.AgreeStartDate);

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

		private void textBoxMatomeTotalPrice12_MouseDoubleClick(object sender, MouseEventArgs e)
		{
#if DEBUG
			if (null != textBoxMatomeTotalPrice12.Tag)
			{
				MessageBox.Show(textBoxMatomeTotalPrice12.Tag as string);
			}
#endif
		}

		private void textBoxMatomeTotalPrice36_MouseDoubleClick(object sender, MouseEventArgs e)
		{
#if DEBUG
			if (null != textBoxMatomeTotalPrice36.Tag)
			{
				MessageBox.Show(textBoxMatomeTotalPrice36.Tag as string);
			}
#endif
		}

		private void textBoxMatomeTotalPrice60_MouseDoubleClick(object sender, MouseEventArgs e)
		{
#if DEBUG
			if (null != textBoxMatomeTotalPrice60.Tag)
			{
				MessageBox.Show(textBoxMatomeTotalPrice60.Tag as string);
			}
#endif
		}
	}
}
