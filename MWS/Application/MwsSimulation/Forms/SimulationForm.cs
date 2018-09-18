//
// SimulationForm.cs
//
// 見積書作成画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
// Ver1.030 電子カルテ標準サービス申込時に、１号カルテ標準サービスと２号カルテ標準サービスの申込をチェックする(2018/08/10 勝呂)
// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
// Ver1.050 おまとめプランが０円から適用できるように修正(2018/09/18 勝呂)
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
	/// 見積書作成画面
	/// </summary>
	public partial class SimulationForm : Form
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
		/// 電子カルテ標準サービス サービスコード
		/// </summary>
		// Ver1.030 電子カルテ標準サービス申込時に、１号カルテ標準サービスと２号カルテ標準サービスの申込をチェックする(2018/08/10 勝呂)
		private const string SERVICE_CODE_CHART_COMPUTE = "1042100";

		/// <summary>
		/// １号カルテ標準サービス サービスコード
		/// </summary>
		// Ver1.030 電子カルテ標準サービス申込時に、１号カルテ標準サービスと２号カルテ標準サービスの申込をチェックする(2018/08/10 勝呂)
		private const string SERVICE_CODE_CHART1_STD = "1012100";

		/// <summary>
		/// ２号カルテ標準サービス サービスコード
		/// </summary>
		// Ver1.030 電子カルテ標準サービス申込時に、１号カルテ標準サービスと２号カルテ標準サービスの申込をチェックする(2018/08/10 勝呂)
		private const string SERVICE_CODE_CHART2_STD = "1014100";

		/// <summary>
		/// おまとめプラン１２ヵ月が存在する
		/// </summary>
		// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
		private bool ExistGroupPlan12 { get; set; }

		/// <summary>
		/// おまとめプラン２４ヵ月が存在する
		/// </summary>
		// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
		private bool ExistGroupPlan24 { get; set; }

		/// <summary>
		/// おまとめプラン３６ヵ月が存在する
		/// </summary>
		// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
		private bool ExistGroupPlan36 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SimulationForm()
		{
			InitializeComponent();

			EstimateData = null;

			PrintInfo = new PrintEstimate();
			PrintDocument = new PrintDocument();
			MaxPage = 0;

			// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
			ExistGroupPlan12 = false;
			ExistGroupPlan24 = false;
			ExistGroupPlan36 = false;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SimulationForm(Estimate est)
		{
			InitializeComponent();

			EstimateData = est;

			PrintInfo = new PrintEstimate();
			PrintDocument = new PrintDocument();
			MaxPage = 0;

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
		private void SimulationForm_Load(object sender, EventArgs e)
		{
#if DEBUG
			// Debug情報表示
			labelDebugNormal.Visible = true;
			labelDebugSet.Visible = true;
			labelDebugGroup.Visible = true;
			textBoxDebugNormal.Visible = true;
			textBoxDebugSet.Visible = true;
			textBoxDebugGroup.Visible = true;
			textBoxDebugTotal.Visible = true;
#endif
			// ウィンドウサイズの変更
			this.Width = MainForm.gSettings.SimulationFormSize.Width;
			this.Height = MainForm.gSettings.SimulationFormSize.Height;

			// サービス情報リストビューの設定
			listViewService.BeginUpdate();
			foreach (ServiceInfo service in MainForm.gServiceList)
			{
				ListViewItem lvItem = new ListViewItem(service.GetListViewData());
				lvItem.Tag = service;
				if (service.IsGroupPlanService)
				{
					// おまとめプラン対象サービスは前景色を青で表示
					lvItem.ForeColor = Color.Blue;
				}
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

			// 契約期間コンボボックスの設定
			comboBoxTerm.Items.Add("１か月");
			comboBoxTerm.Items.Add("１２か月");
			comboBoxTerm.Items.Add("２４か月");
			comboBoxTerm.Items.Add("３６か月");
			comboBoxTerm.SelectedIndex = 0;

			// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
			if (MainForm.gGroupPlanList.IsExistKeiyakuMonth(12))
			{
				// おまとめプラン１２ヵ月が有効
				ExistGroupPlan12 = true;
				radioButtonGroup12.Enabled = true;
				textBoxPrice12.Enabled = true;
				textBoxFree12.Enabled = true;
			}
			// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
			if (MainForm.gGroupPlanList.IsExistKeiyakuMonth(24))
			{
				// おまとめプラン２４ヵ月が有効
				ExistGroupPlan24 = true;
				radioButtonGroup24.Enabled = true;
				textBoxPrice24.Enabled = true;
				textBoxFree24.Enabled = true;
			}
			// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
			if (MainForm.gGroupPlanList.IsExistKeiyakuMonth(36))
			{
				// おまとめプラン３６ヵ月が有効
				ExistGroupPlan36 = true;
				radioButtonGroup36.Enabled = true;
				textBoxPrice36.Enabled = true;
				textBoxFree36.Enabled = true;
			}
			// おススメセットの設定
			if (3 == MainForm.gInitGroupPlanList.Count)
			{
				buttonInitGroupPlan1.Text = MainForm.gInitGroupPlanList[0].GroupName;
				buttonInitGroupPlan1.Tag = MainForm.gInitGroupPlanList[0];
				buttonInitGroupPlan2.Text = MainForm.gInitGroupPlanList[1].GroupName;
				buttonInitGroupPlan2.Tag = MainForm.gInitGroupPlanList[1];
				buttonInitGroupPlan3.Text = MainForm.gInitGroupPlanList[2].GroupName;
				buttonInitGroupPlan3.Tag = MainForm.gInitGroupPlanList[2];
			}
			// セット割サービスリストビューの設定
			foreach (SetPlan set in MainForm.gSetPlanList)
			{
				ListViewItem lvItem = new ListViewItem(set.GetListViewData());
				lvItem.Tag = set;
				listViewSetPlan.Items.Add(lvItem);
			}
			// MIC WEB SERVICEプラットフォーム利用料の設定
			textBoxStandardPrice.Text = "\\" + StringUtil.CommaEdit(MainForm.gServiceList.Standard.Price);

			int groupPlanMonth = 0;
			if (null != EstimateData)
			{
				// 宛先の設定
				textBoxDestination.Text = EstimateData.Destination;

				// 発行日の設定
				dateTimePickerPrintDate.Value = EstimateData.PrintDate.ToDateTime();

				// 契約期間の設定
				dateTimePickerStartDate.Value = EstimateData.AgreeStartDate.ToDateTime();
				switch (EstimateData.AgreeMonthes)
				{
					case 1:
						comboBoxTerm.SelectedIndex = 0;
						break;
					case 12:
						comboBoxTerm.SelectedIndex = 1;
						break;
					case 24:
						comboBoxTerm.SelectedIndex = 2;
						break;
					case 36:
						comboBoxTerm.SelectedIndex = 3;
						break;
				}
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
						// おまとめプラン・セット割サービス
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
								// セット割サービス
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
									if (service.GoodsID == estService.GoodsID)
									{
										item.Checked = true;
										break;
									}
								}

							}
							else
							{
								// おまとめプラン
								groupPlanMonth = MainForm.gGroupPlanList.GetKeiyakuMonth(estService.GoodsID);
							}
						}
					}
				}
			}
			// ご利用のサービスの月額利用額
			this.DrawServicePrice();

			// おまとめプラン チェックボックスの制御
			switch (groupPlanMonth)
			{
				case 12:
					radioButtonGroup12.Checked = true;
					break;
				case 24:
					radioButtonGroup24.Checked = true;
					break;
				case 36:
					radioButtonGroup36.Checked = true;
					break;
			}
		}

		/// <summary>
		/// サービスの選択/解除（設定前）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewService_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (CheckState.Checked == e.CurrentValue)
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
		}

		/// <summary>
		/// サービスの選択/解除（設定後）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewService_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			//if (0 == listViewService.SelectedItems.Count)
			//{
			//	// リストビューの追加時にこのモジュールに来てしまうので、追加時には処理を抜ける
			//	return;
			//}
			ServiceInfo service = (ServiceInfo)e.Item.Tag;
			if (e.Item.Checked)
			{
				service.Select = true;
			}
			else
			{
				service.Select = false;
			}
			// ご利用のサービスの月額利用額の表示
			this.DrawServicePrice();
		}

		/// <summary>
		/// おススメセット１の選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInitGroupPlan1_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in listViewSetPlan.Items)
			{
				if (item.Checked)
				{
					MessageBox.Show("おススメセットを選択する際はセット割サービスの申込を解除してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			InitGroupPlan plan = buttonInitGroupPlan1.Tag as InitGroupPlan;
			this.SetInitGroupPlanCheckBox(plan);
		}

		/// <summary>
		/// おススメセット２の選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInitGroupPlan2_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in listViewSetPlan.Items)
			{
				if (item.Checked)
				{
					MessageBox.Show("おススメセットを選択する際はセット割サービスの申込を解除してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			InitGroupPlan plan = buttonInitGroupPlan2.Tag as InitGroupPlan;
			this.SetInitGroupPlanCheckBox(plan);
		}

		/// <summary>
		/// おススメセット３の選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonInitGroupPlan3_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in listViewSetPlan.Items)
			{
				if (item.Checked)
				{
					MessageBox.Show("おススメセットを選択する際はセット割サービスの申込を解除してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			InitGroupPlan plan = buttonInitGroupPlan3.Tag as InitGroupPlan;
			this.SetInitGroupPlanCheckBox(plan);
		}

		/// <summary>
		/// おまとめプランなし
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonGroupNone_CheckedChanged(object sender, EventArgs e)
		{
			// 契約月数を１か月に変更
			comboBoxTerm.SelectedIndex = 0;

			// サービス使用料の表示
			this.DrawServicePrice();
		}

		/// <summary>
		/// おまとめプラン12ヵ月
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonGroup12_CheckedChanged(object sender, EventArgs e)
		{
			// 契約月数を１２か月に変更
			comboBoxTerm.SelectedIndex = 1;

			// サービス使用料の表示
			this.DrawServicePrice();
		}

		/// <summary>
		/// おまとめプラン24ヵ月
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonGroup24_CheckedChanged(object sender, EventArgs e)
		{
			// 契約月数を２４か月に変更
			comboBoxTerm.SelectedIndex = 2;

			// サービス使用料の表示
			this.DrawServicePrice();
		}

		/// <summary>
		/// おまとめプラン36ヵ月
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonGroup36_CheckedChanged(object sender, EventArgs e)
		{
			// 契約月数を３６か月に変更
			comboBoxTerm.SelectedIndex = 3;

			// サービス使用料の表示
			this.DrawServicePrice();
		}

		/// <summary>
		/// セット割サービスの選択/解除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewSet_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			SetPlan plan = (SetPlan)e.Item.Tag;
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
						//var c = SystemBrushes.Window;
						item.BackColor = Color.White;
					}
				}
			}
			// ご利用のサービスの月額利用額の表示
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
			if (radioButtonGroupNone.Checked)
			{
				// おまとめプランなし
				if (0 != comboBoxTerm.SelectedIndex)
				{
					MessageBox.Show("契約期間は１か月を指定してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					comboBoxTerm.Focus();
					return;
				}
			}
			else if (radioButtonGroup12.Checked)
			{
				// おまとめプラン12ヵ月
				if (1 != comboBoxTerm.SelectedIndex)
				{
					MessageBox.Show("契約期間は１２か月を指定してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					comboBoxTerm.Focus();
					return;
				}
			}
			else if (radioButtonGroup24.Checked)
			{
				// おまとめプラン24ヵ月
				if (2 != comboBoxTerm.SelectedIndex)
				{
					MessageBox.Show("契約期間は２４か月を指定してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					comboBoxTerm.Focus();
					return;
				}
			}
			else if (radioButtonGroup36.Checked)
			{
				// おまとめプラン36ヵ月
				if (3 != comboBoxTerm.SelectedIndex)
				{
					MessageBox.Show("契約期間は３６か月を指定してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					comboBoxTerm.Focus();
					return;
				}
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

				// 発行日
				est.PrintDate = new Date(dateTimePickerPrintDate.Value);

				// 契約期間
				est.AgreeStartDate = new Date(dateTimePickerStartDate.Value);
				switch (comboBoxTerm.SelectedIndex)
				{
					case 0:
						est.AgreeMonthes = 1;
						break;
					case 1:
						est.AgreeMonthes = 12;
						break;
					case 2:
						est.AgreeMonthes = 24;
						break;
					case 3:
						est.AgreeMonthes = 36;
						break;
				}
				// 備考
				est.SetRemark(textBoxRemark.Lines);

				// 見積書情報の設定
				est.SetEstimateData(serviceList, groupList);

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
			if (radioButtonGroupNone.Checked)
			{
				// おまとめプランなし
				if (0 != comboBoxTerm.SelectedIndex)
				{
					MessageBox.Show("契約期間は１か月を指定してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					comboBoxTerm.Focus();
					return;
				}
			}
			else if (radioButtonGroup12.Checked)
			{
				// おまとめプラン12ヵ月
				if (1 != comboBoxTerm.SelectedIndex)
				{
					MessageBox.Show("契約期間は１２か月を指定してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					comboBoxTerm.Focus();
					return;
				}
			}
			else if (radioButtonGroup24.Checked)
			{
				// おまとめプラン24ヵ月
				if (2 != comboBoxTerm.SelectedIndex)
				{
					MessageBox.Show("契約期間は２４か月を指定してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					comboBoxTerm.Focus();
					return;
				}
			}
			else if (radioButtonGroup36.Checked)
			{
				// おまとめプラン36ヵ月
				if (3 != comboBoxTerm.SelectedIndex)
				{
					MessageBox.Show("契約期間は３６か月を指定してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					comboBoxTerm.Focus();
					return;
				}
			}
			// 申込み情報の取得
			List<GroupService> groupList = null;
			List<ServiceInfo> serviceList = null;
			this.GetOrderService(out groupList, out serviceList);

			if (0 < groupList.Count || 0 < serviceList.Count)
			{
				string dataFolder = Program.GetDataFolder();
				if (null == EstimateData)
				{
					// 見積書情報の新規追加
					if (new Date(dateTimePickerStartDate.Value) < new Date(dateTimePickerPrintDate.Value))
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

					// 発行日の設定
					EstimateData.PrintDate = new Date(dateTimePickerPrintDate.Value);

					// 契約期間
					EstimateData.AgreeStartDate = new Date(dateTimePickerStartDate.Value);
					switch (comboBoxTerm.SelectedIndex)
					{
						case 0:
							EstimateData.AgreeMonthes = 1;
							break;
						case 1:
							EstimateData.AgreeMonthes = 12;
							break;
						case 2:
							EstimateData.AgreeMonthes = 24;
							break;
						case 3:
							EstimateData.AgreeMonthes = 36;
							break;
					}
					// 次回見積書情報番号の取得
					EstimateData.EstimateID = SQLiteMwsSimulationAccess.GetLastEstimateNumber(dataFolder);

					// 見積書情報の設定
					EstimateData.SetEstimateData(serviceList, groupList);

					// 備考
					EstimateData.SetRemark(textBoxRemark.Lines);

					// 見積書情報の追加
					if (-1 == SQLiteMwsSimulationSetIO.InsertIntoEstimate(dataFolder, EstimateData))
					{
						return;
					}
				}
				else
				{
					// 見積書情報の更新
					if (new Date(dateTimePickerStartDate.Value) < new Date(dateTimePickerPrintDate.Value))
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

					// 発行日の設定
					EstimateData.PrintDate = new Date(dateTimePickerPrintDate.Value);

					// 契約期間
					EstimateData.AgreeStartDate = new Date(dateTimePickerStartDate.Value);
					switch (comboBoxTerm.SelectedIndex)
					{
						case 0:
							EstimateData.AgreeMonthes = 1;
							break;
						case 1:
							EstimateData.AgreeMonthes = 12;
							break;
						case 2:
							EstimateData.AgreeMonthes = 24;
							break;
						case 3:
							EstimateData.AgreeMonthes = 36;
							break;
					}
					// 見積書情報の設定
					EstimateData.SetEstimateData(serviceList, groupList);

					// 備考
					EstimateData.SetRemark(textBoxRemark.Lines);

					if (-1 == SQLiteMwsSimulationSetIO.UpdateEstimate(dataFolder, EstimateData))
					{
						return;
					}
				}
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
		private void SimulationForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			MainForm.gSettings.SimulationFormSize = new Size(this.Width, this.Height);
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
					if (SERVICE_CODE_CHART_COMPUTE == srcService.ServiceCode)
					{
						// 電子カルテ標準サービス
						orderChartComputeIndex = i;
					}
					else if (SERVICE_CODE_CHART1_STD == srcService.ServiceCode)
					{
						// １号カルテ標準サービス
						orderChart1Std = true;
					}
					else if (SERVICE_CODE_CHART2_STD == srcService.ServiceCode)
					{
						// ２号カルテ標準サービス
						orderChart2Std = true;
					}

					if (0 < srcService.ParentServiceCode.Length)
					{
						// 親サービスが必須
						foreach (ListViewItem dstItem in listViewService.Items)
						{
							ServiceInfo dstService = dstItem.Tag as ServiceInfo;
							if (dstService.ServiceCode == srcService.ParentServiceCode)
							{
								// 子サービスに対する親サービス
								if (false == dstItem.Checked)
								{
									msg = string.Format("[{0}] には親サービスに [{1}] の申込が必要です", srcService.ServiceName, dstService.ServiceName);
									return i;
								}
							}
						}
					}
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
					msg = "[電子カルテ標準サービス] には親サービスに [１号カルテ標準サービス] と [２号カルテ標準サービス] の申込が必要です";
					return orderChartComputeIndex;
				}
			}
			return -1;
		}

		/// <summary>
		/// 通常、セット割、おまとめプラン サービス使用料の取得
		/// </summary>
		/// <param name="normalPrice">通常 サービス使用料</param>
		/// <param name="setPrice">セット割 サービス使用料</param>
		/// <param name="groupPrice">おまとめプラン サービス使用料</param>
		private void GetServicePrice(out int normalPrice, out int setPrice, out int groupPrice)
		{
			normalPrice = setPrice = groupPrice = 0;

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
			foreach (ListViewItem item in listViewService.Items)
			{
				ServiceInfo service = item.Tag as ServiceInfo;
				if (item.Checked)
				{
					if (false == codeList.Contains(service.GoodsID))
					{
						if (service.IsGroupPlanService)
						{
							// おまとめプラン対象サービス
							groupPrice += service.Price;
						}
						else
						{
							normalPrice += service.Price;
						}
					}
				}
			}
		}

		/// <summary>
		/// おススメセット選択後のサービスの設定
		/// </summary>
		/// <param name="plan">おススメセット</param>
		private void SetInitGroupPlanCheckBox(InitGroupPlan plan)
		{
			foreach (ListViewItem item in listViewService.Items)
			{
				ServiceInfo service = item.Tag as ServiceInfo;
				if (service.IsGroupPlanService)
				{
					item.Checked = false;
				}
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
		/// サービス使用料の表示
		/// </summary>
		private void DrawServicePrice()
		{
			int normalPrice;    // 通常のサービス使用料
			int setPrice;       // セット割対象サービス使用料
			int groupPrice;     // おまとめプラン対象サービス使用料
			this.GetServicePrice(out normalPrice, out setPrice, out groupPrice);

			if (MainForm.gMinAmmount <= groupPrice)
			{
				// おまとめプラン適用
				// Ver1.040 おまとめプランのマスターの存在によっておまとめプラン申込みボタンを制御する(2018/08/24 勝呂)
				if (ExistGroupPlan12)
				{
					radioButtonGroup12.Enabled = true;

					// おまとめプラン料金の表示
					GroupPlan targetPlan12 = MainForm.gGroupPlanList.GetMachGroupPlan(12, groupPrice);
					textBoxPrice12.Text = string.Format("\\{0}", StringUtil.CommaEdit(targetPlan12.GetGroupPlanTotalPrice(MainForm.gServiceList.Standard.Price, groupPrice)));
					textBoxPrice12.Tag = string.Format("{0}*12+{1}*(12-{2})", MainForm.gServiceList.Standard.Price, groupPrice, targetPlan12.FreeMonth);

					// おまとめプラン無償利用期間の表示
					int totalGroupPrice12 = targetPlan12.GetGroupPlanTotalPrice(MainForm.gServiceList.Standard.Price, groupPrice);
					textBoxFree12.Text = string.Format("\\{0}", StringUtil.CommaEdit(GroupPlanList.GetGroupPlanMonthlyFreePrice(12, MainForm.gServiceList.Standard.Price, groupPrice, totalGroupPrice12)));
					textBoxFree12.Tag = string.Format("(({0}*12+{1}*12) - ({0}*12+{1}*(12-{2}))) / 12", MainForm.gServiceList.Standard.Price, groupPrice, targetPlan12.FreeMonth);
				}
				if (ExistGroupPlan24)
				{
					radioButtonGroup24.Enabled = true;

					// おまとめプラン料金の表示
					GroupPlan targetPlan24 = MainForm.gGroupPlanList.GetMachGroupPlan(24, groupPrice);
					textBoxPrice24.Text = string.Format("\\{0}", StringUtil.CommaEdit(targetPlan24.GetGroupPlanTotalPrice(MainForm.gServiceList.Standard.Price, groupPrice)));
					textBoxPrice24.Tag = string.Format("{0}*24+{1}*(24-{2})", MainForm.gServiceList.Standard.Price, groupPrice, targetPlan24.FreeMonth);

					// おまとめプラン無償利用期間の表示
					int totalGroupPrice24 = targetPlan24.GetGroupPlanTotalPrice(MainForm.gServiceList.Standard.Price, groupPrice);
					textBoxFree24.Text = string.Format("\\{0}", StringUtil.CommaEdit(GroupPlanList.GetGroupPlanMonthlyFreePrice(24, MainForm.gServiceList.Standard.Price, groupPrice, totalGroupPrice24)));
					textBoxFree24.Tag = string.Format("(({0}*24+{1}*24) - ({0}*24+{1}*(24-{2}))) / 24", MainForm.gServiceList.Standard.Price, groupPrice, targetPlan24.FreeMonth);
				}
				if (ExistGroupPlan36)
				{
					radioButtonGroup36.Enabled = true;

					// おまとめプラン料金の表示
					GroupPlan targetPlan36 = MainForm.gGroupPlanList.GetMachGroupPlan(36, groupPrice);
					textBoxPrice36.Text = string.Format("\\{0}", StringUtil.CommaEdit(targetPlan36.GetGroupPlanTotalPrice(MainForm.gServiceList.Standard.Price, groupPrice)));
					textBoxPrice36.Tag = string.Format("{0}*36+{1}*(36-{2})", MainForm.gServiceList.Standard.Price, groupPrice, targetPlan36.FreeMonth);

					// おまとめプラン無償利用期間の表示
					int totalGroupPrice36 = targetPlan36.GetGroupPlanTotalPrice(MainForm.gServiceList.Standard.Price, groupPrice);
					textBoxFree36.Text = string.Format("\\{0}", StringUtil.CommaEdit(GroupPlanList.GetGroupPlanMonthlyFreePrice(36, MainForm.gServiceList.Standard.Price, groupPrice, totalGroupPrice36)));
					textBoxFree36.Tag = string.Format("(({0}*36+{1}*36) - ({0}*36+{1}*(36-{2}))) / 36", MainForm.gServiceList.Standard.Price, groupPrice, targetPlan36.FreeMonth);
				}
				// ご利用のサービスの月額利用額の表示
				if (radioButtonGroupNone.Checked)
				{
					// おまとめプラン なし
					textBoxTotalPrice.Text = string.Format("\\{0}", StringUtil.CommaEdit(normalPrice + setPrice + groupPrice));
					textBoxTotalPrice.Tag = string.Format("normal:{0}+ set:{1}+ group:{2}", normalPrice, setPrice, groupPrice);
				}
				else if (radioButtonGroup12.Checked)
				{
					// おまとめプラン12ヵ月
					GroupPlan targetPlan12 = MainForm.gGroupPlanList.GetMachGroupPlan(12, groupPrice);
					textBoxTotalPrice.Text = string.Format("\\{0}", StringUtil.CommaEdit(targetPlan12.GetGroupPlanPrice(groupPrice) + normalPrice + setPrice));
					textBoxTotalPrice.Tag = string.Format("group:({0}*(12-{1})) / 12 + normal:{2} + set:{3}", groupPrice, targetPlan12.FreeMonth, normalPrice, setPrice);
				}
				else if (radioButtonGroup24.Checked)
				{
					// おまとめプラン24ヵ月
					GroupPlan targetPlan24 = MainForm.gGroupPlanList.GetMachGroupPlan(24, groupPrice);
					textBoxTotalPrice.Text = string.Format("\\{0}", StringUtil.CommaEdit(targetPlan24.GetGroupPlanPrice(groupPrice) + normalPrice + setPrice));
					textBoxTotalPrice.Tag = string.Format("group:({0}*(24-{1})) / 24 + normal:{2} + set:{3}", groupPrice, targetPlan24.FreeMonth, normalPrice, setPrice);
				}
				else if (radioButtonGroup36.Checked)
				{
					// おまとめプラン36ヵ月
					GroupPlan targetPlan36 = MainForm.gGroupPlanList.GetMachGroupPlan(36, groupPrice);
					textBoxTotalPrice.Text = string.Format("\\{0}", StringUtil.CommaEdit(targetPlan36.GetGroupPlanPrice(groupPrice) + normalPrice + setPrice));
					textBoxTotalPrice.Tag = string.Format("group:({0}*(36-{1})) / 36 + normal:{2} + set:{3}", groupPrice, targetPlan36.FreeMonth, normalPrice, setPrice);
				}
			}
			else
			{
				// おまとめプラン適用外
				radioButtonGroupNone.Checked = true;
				radioButtonGroup12.Enabled = false;
				radioButtonGroup24.Enabled = false;
				radioButtonGroup36.Enabled = false;

				// おまとめプラン料金の表示
				textBoxPrice12.Text = "\\0";
				textBoxPrice24.Text = "\\0";
				textBoxPrice36.Text = "\\0";

				// おまとめプラン無償利用期間の表示
				textBoxFree12.Text = "\\0";
				textBoxFree24.Text = "\\0";
				textBoxFree36.Text = "\\0";

				// ご利用のサービスの月額利用額の表示
				textBoxTotalPrice.Text = string.Format("\\{0}", StringUtil.CommaEdit(normalPrice + setPrice + groupPrice));
				textBoxTotalPrice.Tag = string.Format("normal:{0}+ set:{1}+ group:{2}", normalPrice, setPrice, groupPrice);
			}
			// Ver1.050 おまとめプランが０円から適用できるように修正(2018/09/18 勝呂)
			if (MainForm.gMinFreeMonthMinAmmount <= groupPrice)
			{
				labelGroupPlanMessage.Text = "※おまとめプラン割引が適用できます。";
			}
			else
			{
				labelGroupPlanMessage.Text = string.Format("※あと \\{0} でおまとめプラン割引が適用できます。", StringUtil.CommaEdit(MainForm.gMinFreeMonthMinAmmount - groupPrice));
			}
#if DEBUG
			if (radioButtonGroupNone.Checked)
			{
				textBoxDebugNormal.Text = (normalPrice + groupPrice).ToString();
				textBoxDebugSet.Text = setPrice.ToString();
				textBoxDebugGroup.Text = "0";
				textBoxDebugTotal.Text = (normalPrice + setPrice + groupPrice).ToString();
			}
			else
			{
				textBoxDebugNormal.Text = normalPrice.ToString();
				textBoxDebugSet.Text = setPrice.ToString();
				textBoxDebugGroup.Text = groupPrice.ToString();
				textBoxDebugTotal.Text = (normalPrice + setPrice + groupPrice).ToString();
			}
#endif
		}

		/// <summary>
		/// 申込み情報の取得
		/// </summary>
		/// <param name="groupList"></param>
		/// <param name="serviceList"></param>
		private void GetOrderService(out List<GroupService> groupList, out List<ServiceInfo> serviceList)
		{
			// おまとめプラン→セット割サービス→単品サービスの順に格納(印刷順)
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
			// おまとめプラン
			if (!radioButtonGroupNone.Checked)
			{
				GroupService groupService = null;
				foreach (ListViewItem item in listViewService.Items)
				{
					if (item.Checked)
					{
						ServiceInfo service = item.Tag as ServiceInfo;
						if (service.IsGroupPlanService)
						{
							// おまとめプラン対象サービス
							if (null == groupService)
							{
								groupService = new GroupService();
								groupService.Mode = SQLiteMwsSimulationDef.ServiceMode.Group;

								// MIC WEB SERVICEプラットフォーム利用料を含める
								groupService.ServiceCodeList.Add(new Tuple<string, string>(MainForm.gServiceList.Standard.GoodsID, MainForm.gServiceList.Standard.ServiceName));
							}
							if (-1 == groupList.FindIndex(p => p.ServiceCodeList.Contains(new Tuple<string, string>(service.GoodsID, service.ServiceName))))
							{
								// セット割サービスに含まれるサービスはおまとめプランに含めない
								groupService.ServiceCodeList.Add(new Tuple<string, string>(service.GoodsID, service.ServiceName));
							}
						}
					}
				}
				if (null != groupService)
				{
					int normalPrice;    // 通常のサービス使用料
					int setPrice;       // セット割対象サービス使用料
					int groupPrice;     // おまとめプラン対象サービス使用料
					this.GetServicePrice(out normalPrice, out setPrice, out groupPrice);
					GroupPlan targetPlan = null;
					if (radioButtonGroup12.Checked)
					{
						// おまとめプラン12ヵ月
						targetPlan = MainForm.gGroupPlanList.GetMachGroupPlan(12, groupPrice);
					}
					else if (radioButtonGroup24.Checked)
					{
						// おまとめプラン24ヵ月
						targetPlan = MainForm.gGroupPlanList.GetMachGroupPlan(24, groupPrice);
					}
					else if (radioButtonGroup36.Checked)
					{
						// おまとめプラン36ヵ月
						targetPlan = MainForm.gGroupPlanList.GetMachGroupPlan(36, groupPrice);
					}
					groupService.GoodsID = targetPlan.GoodsID;
					groupService.GoodsName = targetPlan.GoodsName;
					groupService.Price = targetPlan.GetGroupPlanTotalPrice(MainForm.gServiceList.Standard.Price, groupPrice);
					groupList.Insert(0, groupService);
				}
			}
			foreach (ListViewItem item in listViewService.Items)
			{
				ServiceInfo service = item.Tag as ServiceInfo;
				if (item.Checked)
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
					MaxPage = PrintInfo.GetMaxPage();

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
			PrintDocument printDocument = ((PrintPreviewForm)sender).Document;

			if (!printDocument.PrintController.IsPreview)
			{
				if (!e.Cancel)
				{
					// プリンタ発行情報をINIファイルに記憶する
					//ClientPrinterInfoIni pi = new ClientPrinterInfoIni();
					//pi.ReadPrintDocument(printDocument);
					//ClientIniIO.SetClientIntroducePrinterInfo(PrintIntroduce.PaperName, pi);
				}
			}
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

		private void textBoxFree12_MouseDoubleClick(object sender, MouseEventArgs e)
		{
#if DEBUG
			if (null != textBoxFree12.Tag)
			{
				MessageBox.Show(textBoxFree12.Tag as string);
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

		private void textBoxFree24_MouseDoubleClick(object sender, MouseEventArgs e)
		{
#if DEBUG

			if (null != textBoxFree24.Tag)
			{
				MessageBox.Show(textBoxFree24.Tag as string);
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

		private void textBoxFree36_MouseDoubleClick(object sender, MouseEventArgs e)
		{
#if DEBUG
			if (null != textBoxFree36.Tag)
			{
				MessageBox.Show(textBoxFree36.Tag as string);
			}
#endif
		}

		private void textBoxTotalPrice_MouseDoubleClick(object sender, MouseEventArgs e)
		{
#if DEBUG
			if (null != textBoxTotalPrice.Tag)
			{
				MessageBox.Show(textBoxTotalPrice.Tag as string);
			}
#endif
		}
	}
}
