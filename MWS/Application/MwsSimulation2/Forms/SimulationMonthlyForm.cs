//
// SimulationMonthlyForm.cs
//
// 月額課金 御見積書作成画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
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
	/// 月額課金 御見積書作成画面
	/// </summary>
	public partial class SimulationMonthlyForm : Form
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
		/// 見積書情報
		/// </summary>
		public Estimate EstimateData { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SimulationMonthlyForm()
		{
			InitializeComponent();

			PrintInfo = new PrintEstimate();
			PrintDocument = new PrintDocument();
			MaxPage = 0;
			EstimateData = null;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SimulationMonthlyForm(Estimate est)
		{
			InitializeComponent();

			PrintInfo = new PrintEstimate();
			PrintDocument = new PrintDocument();
			MaxPage = 0;
			EstimateData = est;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SimulationMonthlyForm_Load(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			// イベントハンドラ削除
			listViewService.ItemCheck -= new ItemCheckEventHandler(listViewService_ItemCheck);
			listViewService.ItemChecked -= new ItemCheckedEventHandler(listViewService_ItemChecked);
			listViewSetPlan.ItemChecked -= new ItemCheckedEventHandler(listViewSetPlan_ItemChecked);
			dateTimePickerPrintDate.ValueChanged -= new EventHandler(dateTimePickerPrintDate_ValueChanged);

			// ウィンドウサイズの変更
			this.Width = MainForm.gSettings.SimulationMonthlyFormSize.Width;
			this.Height = MainForm.gSettings.SimulationMonthlyFormSize.Height;

			// サービス情報リストビューの設定
			listViewService.BeginUpdate();

			// MIC WEB SERVICE標準サービスを設定
			ListViewItem lvPlatformItem = new ListViewItem(MainForm.gServiceList.Platform.GetListViewData());
			lvPlatformItem.Tag = MainForm.gServiceList.Platform;
			listViewService.Items.Add(lvPlatformItem);

			foreach (ServiceInfo service in MainForm.gServiceList)
			{
				ListViewItem lvItem = new ListViewItem(service.GetListViewData());
				lvItem.Tag = service;
				foreach (SetPlan set in MainForm.gSetPlanList)
				{
					if (-1 != set.ServiceList.FindIndex(p => p.Item1 == service.GoodsID))
					{
						// セット割対象サービス
						service.SetService = true;
					}
				}
				listViewService.Items.Add(lvItem);
			}
			listViewService.EndUpdate();

			// セット割サービスリストビューの設定
			foreach (SetPlan set in MainForm.gSetPlanList)
			{
				ListViewItem lvItem = new ListViewItem(set.GetListViewData());
				lvItem.Tag = set;
				listViewSetPlan.Items.Add(lvItem);
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

				// 備考
				textBoxRemark.Lines = EstimateData.GetRemark();

				// 選択済みのサービスのチェックボックスをチェック
				// セット割サービスのチェックボックスをチェック
				foreach (EstimateService estService in EstimateData.ServiceList)
				{
					if (SQLiteMwsSimulationDef.ServiceMode.None == estService.Mode)
					{
						// 通常のサービス
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
					else
					{
						// セット割サービス
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
							if (SQLiteMwsSimulationDef.ServiceMode.Set == estService.Mode)
							{
								// 該当するセット割サービスを選択状態にする
								foreach (ListViewItem item in listViewSetPlan.Items)
								{
									SetPlan plan = item.Tag as SetPlan;
									if (plan.GoodsID == estService.GoodsID)
									{
										item.Checked = true;
										break;
									}
								}
								foreach (ListViewItem item in listViewService.Items)
								{
									ServiceInfo service = item.Tag as ServiceInfo;
									if (group.Item1 == service.GoodsID)
									{
										// セット割サービスに含まれるサービスを選択状態にする
										item.Checked = true;
										item.BackColor = Color.DarkGray;
										break;
									}
								}
							}
						}
					}
				}
			}
			else
			{
				// 契約期間の設定
				labelAgreeSpan.Tag = Estimate.GetAgreeSapn(false, Date.Today, 1);

				// 有効期限の設定
				dateTimePickerLimitDate.Value = Estimate.GetLimitDate(Date.Today).ToDateTime();
			}
			// サービス利用料及び月額利用料の表示
			this.DrawServicePrice();

			// 契約期間の表示
			this.DrawAgreeSpan((Span)labelAgreeSpan.Tag);

			// イベントハンドラ追加
			listViewService.ItemCheck += new ItemCheckEventHandler(listViewService_ItemCheck);
			listViewService.ItemChecked += new ItemCheckedEventHandler(listViewService_ItemChecked);
			listViewSetPlan.ItemChecked += new ItemCheckedEventHandler(listViewSetPlan_ItemChecked);
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
			else if (CheckState.Checked == e.CurrentValue)
			{
				ServiceInfo service = (ServiceInfo)listViewService.Items[e.Index].Tag;
				foreach (ListViewItem setItem in listViewSetPlan.Items)
				{
					SetPlan set = setItem.Tag as SetPlan;
					if (-1 != set.ServiceList.FindIndex(p => p.Item1 == service.GoodsID))
					{
						if (setItem.Checked)
						{
							e.NewValue = CheckState.Checked;
							MessageBox.Show(string.Format("セット割サービス「{0}」の申込みを解除してください。", set.GoodsName), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
							break;
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
				if (Program.SERVICE_CODE_CHART_COMPUTE == targetService.ServiceCode)
				{
					// 電子カルテ標準サービス選択時には１号カルテ標準サービス、２号カルテ標準サービス、TABLETビューワ、paletteアカウントのサービスを選択状態にする
					foreach (ListViewItem item in listViewService.Items)
					{
						if (false == item.Checked)
						{
							ServiceInfo svr = item.Tag as ServiceInfo;
							if (Program.SERVICE_CODE_CHART1_STD == svr.ServiceCode || Program.SERVICE_CODE_CHART2_STD == svr.ServiceCode || Program.SERVICE_CODE_TABLETVIEWER == svr.ServiceCode || Program.SERVICE_CODE_PALETTE_ACCOUNT == svr.ServiceCode)
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
			labelAgreeSpan.Tag = Estimate.GetAgreeSapn(false, printDate, 1);

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
			using (AgreeSpanForm form = new AgreeSpanForm((Span)labelAgreeSpan.Tag, 1))
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
		/// セット割サービスの選択/解除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewSetPlan_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			SetPlan plan = (SetPlan)e.Item.Tag;

			listViewService.ItemCheck -= new ItemCheckEventHandler(listViewService_ItemCheck);
			listViewService.ItemChecked -= new ItemCheckedEventHandler(listViewService_ItemChecked);
			foreach (ListViewItem item in listViewService.Items)
			{
				ServiceInfo service = item.Tag as ServiceInfo;
				if (-1 != plan.ServiceList.FindIndex(p => p.Item1 == service.GoodsID))
				{
					if (e.Item.Checked)
					{
						item.Checked = true;
						item.BackColor = Color.DarkGray;
					}
					else
					{
						item.Checked = false;
						item.BackColor = Color.White;
					}
				}
			}
			listViewService.ItemCheck += new ItemCheckEventHandler(listViewService_ItemCheck);
			listViewService.ItemChecked += new ItemCheckedEventHandler(listViewService_ItemChecked);

			// サービス利用料及び月額利用料の表示
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
			listViewSetPlan.ItemChecked -= new ItemCheckedEventHandler(listViewSetPlan_ItemChecked);
			foreach (ListViewItem item in listViewService.Items)
			{
				item.Checked = false;
				item.BackColor = Color.White;
			}
			foreach (ListViewItem item in listViewSetPlan.Items)
			{
				item.Checked = false;
			}
			listViewService.ItemCheck += new ItemCheckEventHandler(listViewService_ItemCheck);
			listViewService.ItemChecked += new ItemCheckedEventHandler(listViewService_ItemChecked);
			listViewSetPlan.ItemChecked += new ItemCheckedEventHandler(listViewSetPlan_ItemChecked);

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
			ServiceInfo platform = null;
			this.GetOrderService(out groupList, out serviceList, ref platform);

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
				est.AgreeMonthes = 1;

				// 有効期限の設定
				est.LimitDate = new Date(dateTimePickerLimitDate.Value);

				// 備考
				est.SetRemark(textBoxRemark.Lines);

				// 申込み種別
				est.Apply = Estimate.ApplyType.Monthly;

				// 見積書情報の設定
				est.SetEstimateData(serviceList, groupList, Program.SERVICE_CODE_CHART_COMPUTE, Program.SERVICE_CODE_TABLETVIEWER, platform);

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
			ServiceInfo platform = null;
			this.GetOrderService(out groupList, out serviceList, ref platform);

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
					EstimateData.AgreeMonthes = 1;

					/// 有効期限の設定
					EstimateData.LimitDate = new Date(dateTimePickerLimitDate.Value);

					// 備考
					EstimateData.SetRemark(textBoxRemark.Lines);

					// 申込み種別
					EstimateData.Apply = Estimate.ApplyType.Monthly;

					// 次回見積書情報番号の取得
					EstimateData.EstimateID = SQLiteMwsSimulationAccess.GetLastEstimateNumber(dataFolder);

					// 見積書情報の設定
					EstimateData.SetEstimateData(serviceList, groupList, Program.SERVICE_CODE_CHART_COMPUTE, Program.SERVICE_CODE_TABLETVIEWER, platform);

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
					EstimateData.AgreeMonthes = 1;

					/// 有効期限の設定
					EstimateData.LimitDate = new Date(dateTimePickerLimitDate.Value);

					// 備考
					EstimateData.SetRemark(textBoxRemark.Lines);

					// 申込み種別
					EstimateData.Apply = Estimate.ApplyType.Monthly;

					// 見積書情報の設定
					EstimateData.SetEstimateData(serviceList, groupList, Program.SERVICE_CODE_CHART_COMPUTE, Program.SERVICE_CODE_TABLETVIEWER, platform);

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
		private void SimulationMonthlyForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			MainForm.gSettings.SimulationMonthlyFormSize = new Size(this.Width, this.Height);
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
				}
				i++;
			}
			// 電子カルテ標準サービスには１号カルテ標準サービスおよび２号カルテ標準サービスが必須
			if (-1 != orderChartComputeIndex)
			{
				// 電子カルテ標準サービス申込み有り
				if (false == orderChart1Std || false == orderChart2Std)
				{
					msg = "[電子カルテ標準サービス] には親サービスに [１号カルテ標準サービス] と [２号カルテ標準サービス] の申込が必要です";
					return orderChartComputeIndex;
				}
			}
			return -1;
		}

		/// <summary>
		/// プラットフォーム利用料、通常、セット割、サービス利用料の取得
		/// </summary>
		/// <param name="platformPrice">プラットフォーム利用料</param>
		/// <param name="normalPrice">通常 サービス利用料</param>
		/// <param name="setPrice">セット割 サービス利用料</param>
		private void GetServicePrice(out int platformPrice, out int normalPrice, out int setPrice)
		{
			platformPrice = normalPrice = setPrice = 0;
			List<string> codeList = new List<string>();

			// セット割サービス
			foreach (ListViewItem item in listViewSetPlan.Items)
			{
				if (item.Checked)
				{
					SetPlan plan = item.Tag as SetPlan;
					setPrice += plan.Price;
					foreach (Tuple<string, string> sv in plan.ServiceList)
					{
						codeList.Add(sv.Item1);
					}
				}
			}
			bool isChartCompute = false;
			foreach (ListViewItem item in listViewService.Items)
			{
				ServiceInfo service = item.Tag as ServiceInfo;
				if (Program.SERVICE_CODE_CHART_COMPUTE == service.ServiceCode)
				{
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
					if (false == codeList.Contains(service.GoodsID))
					{
						// セット割サービスに含まれないサービス
						if (MainForm.gServiceList.Platform.ServiceCode == service.ServiceCode)
						{
							// MIC WEB SERVICE標準サービス
							platformPrice = service.Price;
						}
						else
						{
							if (isChartCompute)
							{
								// 電子カルテ標準サービス選択済
								if (Program.SERVICE_CODE_TABLETVIEWER != service.ServiceCode)
								{
									// TABLETビューワ以外
									normalPrice += service.Price;
								}
							}
							else
							{
								// 電子カルテ標準サービス未選択
								normalPrice += service.Price;
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// サービス使用料の表示
		/// </summary>
		private void DrawServicePrice()
		{
			int platformPrice;  // プラットフォーム利用料
			int normalPrice;    // 通常のサービス利用料
			int setPrice;       // セット割対象サービス利用料
			this.GetServicePrice(out platformPrice, out normalPrice, out setPrice);

			// プラットフォーム利用料の設定
			textBoxPlatformPrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(platformPrice));

			// サービス利用料の表示
			textBoxServicePrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(normalPrice + setPrice));
			textBoxServicePrice.Tag = string.Format("normal:{0} set:{1}", normalPrice, setPrice);

			// 月額利用料の表示
			textBoxTotalPrice.Text = string.Format(@"\{0}", StringUtil.CommaEdit(platformPrice + normalPrice + setPrice));
		}

		/// <summary>
		/// 申込み情報の取得
		/// </summary>
		/// <param name="groupList"></param>
		/// <param name="serviceList"></param>
		private void GetOrderService(out List<GroupService> groupList, out List<ServiceInfo> serviceList, ref ServiceInfo pltform)
		{
			// セット割サービス→単品サービスの順に格納(印刷順)
			groupList = new List<GroupService>();
			serviceList = new List<ServiceInfo>();

			// セット割サービス
			foreach (ListViewItem item in listViewSetPlan.Items)
			{
				if (item.Checked)
				{
					SetPlan plan = item.Tag as SetPlan;
					GroupService groupService = new GroupService();
					groupService.GoodsID = plan.GoodsID;
					groupService.GoodsName = plan.GoodsName;
					groupService.Price = plan.Price;
					groupService.Mode = SQLiteMwsSimulationDef.ServiceMode.Set;
					groupService.ServiceCodeList = plan.ServiceList;
					groupList.Add(groupService);
				}
			}
			foreach (ListViewItem item in listViewService.Items)
			{
				ServiceInfo service = item.Tag as ServiceInfo;
				if (item.Checked)
				{
					if (MainForm.gServiceList.Platform.ServiceCode == service.ServiceCode)
					{
						pltform = MainForm.gServiceList.Platform;
					}
					else
					{
						if (0 < groupList.Count)
						{
							if (-1 == groupList.FindIndex(p => p.ServiceCodeList.Contains(new Tuple<string, string>(service.GoodsID, service.ServiceName))))
							{
								// セット割サービス及びおまとめプランに含まれるサービスは追加しない
								serviceList.Add(service);
							}
						}
						else
						{
							// セット割サービス及びおまとめプランの申込みがない
							serviceList.Add(service);
						}
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

		private void textBoxServicePrice_DoubleClick(object sender, EventArgs e)
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
