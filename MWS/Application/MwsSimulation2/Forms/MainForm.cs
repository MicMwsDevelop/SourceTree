//
// MainForm.cs
//
// MIC WEB SERVICE 課金シミュレーションメイン画面
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
using MwsSimulation.Settings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace MwsSimulation.Forms
{
	/// <summary>
	/// MIC WEB SERVICE 課金シミュレーションメイン画面
	/// </summary>
	public partial class MainForm : Form
	{
		/// <summary>
		/// サービス情報リスト
		/// </summary>
		public static ServiceInfoList gServiceList { get; set; }

		/// <summary>
		/// おススメセット情報リスト
		/// </summary>
		public static List<InitGroupPlan> gInitGroupPlanList { get; set; }

		/// <summary>
		/// おまとめプラン情報リスト
		/// </summary>
		public static GroupPlanList gGroupPlanList { get; set; }

		/// <summary>
		/// セット割サービス情報リスト
		/// </summary>
		public static List<SetPlan> gSetPlanList { get; set; }

		/// <summary>
		/// おまとめプランの中で下限金額の最小値
		/// </summary>
		public static int gMinAmmount { get; set; }

		/// <summary>
		/// おまとめプランの中で無償月数が最小値の下限金額（無償月数が０月は除く）
		/// </summary>
		// Ver1.050 おまとめプランが１円から適用できるように修正(2018/09/18 勝呂)
		public static int gMinFreeMonthMinAmmount { get; set; }

		/// <summary>
		/// TABLETビューワのサービス利用料
		/// </summary>
		public static int gTabletViewerPrice { get; set; }

		/// <summary>
		/// バージョン情報
		/// </summary>
		public static Tuple<int, Date> gVersionInfo { get; set; }

		/// <summary>
		/// 環境設定
		/// </summary>
		public static MwsSimulationSettings gSettings { get; set; }

		/// <summary>
		/// おまとめプラン見積書情報リスト
		/// </summary>
		private List<Estimate> EstimateMatomeList { get; set; }

		/// <summary>
		/// 月額課金見積書情報リスト
		/// </summary>
		private List<Estimate> EstimateMonthlyList { get; set; }

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
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			gServiceList = null;
			gInitGroupPlanList = null;
			gGroupPlanList = null;
			gSetPlanList = null;
			gMinAmmount = 0;
			gMinFreeMonthMinAmmount = 0;
			gTabletViewerPrice = 0;
			gSettings = null;
			EstimateMatomeList = new List<Estimate>();
			EstimateMonthlyList = new List<Estimate>();
			PrintInfo = new PrintEstimate();
			PrintDocument = new PrintDocument();
			MaxPage = 0;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			// データファイルの更新
			string dataFolder = Program.GetDataFolder();

			// @@@ClickOnceマスク
			//if (false == this.UpdateDataFile(dataFolder))
			//{
			//	this.Close();
			//	return;
			//}
			// 環境設定の読み込み
			gSettings = MwsSimulationSettingsIF.GetMwsSimulationSettings();

			// 拠点コンボボックスの設定
			this.SetBranchComboBox();
			if (gSettings.CurrentBranchIndex < comboBoxBranch.Items.Count)
			{
				comboBoxBranch.SelectedIndex = gSettings.CurrentBranchIndex;
			}
			// 担当者コンボボックスの設定
			this.SetStaffComboBox();
			if (gSettings.CurrentStaffIndex < comboBoxStaff.Items.Count)
			{
				comboBoxStaff.SelectedIndex = gSettings.CurrentStaffIndex;
			}
			try
			{
				// サービス情報リストの取得
				gServiceList = SQLiteMwsSimulationAccess.GetServiceInfo(dataFolder);

				// おススメセット情報リストの取得
				gInitGroupPlanList = SQLiteMwsSimulationAccess.GetInitGroupPlan(dataFolder);

				// おまとめプラン情報リストの取得
				gGroupPlanList = SQLiteMwsSimulationAccess.GetGroupPlanList(dataFolder);

				// セット割サービス情報リストの設定
				gSetPlanList = SQLiteMwsSimulationAccess.GetSetPlanList(dataFolder);

				// バージョン情報の取得
				gVersionInfo = SQLiteMwsSimulationAccess.GetVerionInfo(dataFolder);

				// おまとめプランの中で下限金額の最小値
				gMinAmmount = gGroupPlanList.GetMinAmmount();

				// おまとめプランの中で無償月数が最小値の下限金額（無償月数が０月は除く）
				gMinFreeMonthMinAmmount = gGroupPlanList.GetMinFreeMonthMinAmmount();

				ServiceInfo tabletViewer = gServiceList.Find(p => p.ServiceCode == Program.SERVICE_CODE_TABLETVIEWER);
				if (null != tabletViewer)
				{
					gTabletViewerPrice = tabletViewer.Price;
				}
				// 見積書情報リストの取得
				List<Estimate> estimateList = SQLiteMwsSimulationAccess.GetEstimateList(dataFolder);
				foreach (Estimate est in estimateList)
				{
					if (Estimate.ApplyType.Monthly == est.Apply)
					{
						// 月額課金
						EstimateMonthlyList.Add(est);
					}
					else
					{
						// おまとめプラン
						EstimateMatomeList.Add(est);
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "マスター情報読込エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}
			// おまとめプラン見積書情報リストボックスの設定
			this.SetListBoxMatome();
			if (0 < listBoxMatome.Items.Count)
			{
				listBoxMatome.SelectedIndex = 0;
			}
			// 月額課金見積書情報リストボックスの設定
			this.SetListBoxMonthly();
			if (0 < listBoxMonthly.Items.Count)
			{
				listBoxMonthly.SelectedIndex = 0;
			}
			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}

		/// <summary>
		/// おまとめプラン見積書情報リストボックスの設定
		/// </summary>
		private void SetListBoxMatome()
		{
			listBoxMatome.Items.Clear();
			if (null != EstimateMatomeList)
			{
				foreach (Estimate est in EstimateMatomeList)
				{
					listBoxMatome.Items.Add(est.DestinationDispString());
				}
			}
		}

		/// <summary>
		/// 月額課金見積書情報リストボックスの設定
		/// </summary>
		private void SetListBoxMonthly()
		{
			listBoxMonthly.Items.Clear();
			if (null != EstimateMonthlyList)
			{
				foreach (Estimate est in EstimateMonthlyList)
				{
					listBoxMonthly.Items.Add(est.DestinationDispString());
				}
			}
		}

		/// <summary>
		/// 見積書の追加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonNew_Click(object sender, EventArgs e)
		{
			if (0 == tabControlEstimate.SelectedIndex)
			{
				// おまとめプラン
				using (SimulationMatomeForm form = new SimulationMatomeForm())
				{
					if (DialogResult.OK == form.ShowDialog())
					{
						EstimateMatomeList.Add(form.EstimateData);

						// おまとめプラン見積書情報リストボックスの設定
						this.SetListBoxMatome();

						listBoxMatome.SelectedIndex = listBoxMatome.Items.Count - 1;
					}
				}
			}
			else
			{
				// 月額課金
				using (SimulationMonthlyForm form = new SimulationMonthlyForm())
				{
					if (DialogResult.OK == form.ShowDialog())
					{
						EstimateMonthlyList.Add(form.EstimateData);

						// 月額課金見積書情報リストボックスの設定
						this.SetListBoxMonthly();

						listBoxMonthly.SelectedIndex = listBoxMonthly.Items.Count - 1;
					}
				}
			}
		}

		/// <summary>
		/// おまとめプラン見積書の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBoxMatome_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			buttonModify.PerformClick();
		}

		/// <summary>
		/// 月額課金見積書の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listBoxMonthly_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			buttonModify.PerformClick();
		}

		/// <summary>
		/// 見積書の変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonModify_Click(object sender, EventArgs e)
		{
			if (0 == tabControlEstimate.SelectedIndex)
			{
				// おまとめプラン
				if (-1 != listBoxMatome.SelectedIndex)
				{
					int saveIndex = listBoxMatome.SelectedIndex;

					using (SimulationMatomeForm form = new SimulationMatomeForm(EstimateMatomeList[listBoxMatome.SelectedIndex]))
					{
						if (DialogResult.OK == form.ShowDialog())
						{
							EstimateMatomeList[listBoxMatome.SelectedIndex] = form.EstimateData;

							// おまとめプラン見積書情報リストボックスの設定
							this.SetListBoxMatome();

							listBoxMatome.SelectedIndex = saveIndex;
						}
					}
				}
			}
			else
			{
				// 月額課金
				if (-1 != listBoxMonthly.SelectedIndex)
				{
					int saveIndex = listBoxMatome.SelectedIndex;

					using (SimulationMonthlyForm form = new SimulationMonthlyForm(EstimateMonthlyList[listBoxMonthly.SelectedIndex]))
					{
						if (DialogResult.OK == form.ShowDialog())
						{
							EstimateMonthlyList[listBoxMonthly.SelectedIndex] = form.EstimateData;

							// 月額課金見積書情報リストボックスの設定
							this.SetListBoxMonthly();

							listBoxMonthly.SelectedIndex = saveIndex;
						}
					}
				}
			}
		}

		/// <summary>
		/// 見積書の削除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (0 == tabControlEstimate.SelectedIndex)
			{
				// おまとめプラン
				if (-1 != listBoxMatome.SelectedIndex)
				{
					if (DialogResult.Yes == MessageBox.Show("見積書を削除してもよろしいですか？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					{
						try
						{
							if (-1 != SQLiteMwsSimulationAccess.DeleteEstimate(Program.GetDataFolder(), EstimateMatomeList[listBoxMatome.SelectedIndex].EstimateID))
							{
								EstimateMatomeList.RemoveAt(listBoxMatome.SelectedIndex);

								// おまとめプラン見積書情報リストボックスの設定
								this.SetListBoxMatome();
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message, "見積書情報削除エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						}
					}
				}
			}
			else
			{
				// 月額課金
				if (-1 != listBoxMonthly.SelectedIndex)
				{
					if (DialogResult.Yes == MessageBox.Show("見積書を削除してもよろしいですか？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					{
						try
						{
							if (-1 != SQLiteMwsSimulationAccess.DeleteEstimate(Program.GetDataFolder(), EstimateMonthlyList[listBoxMonthly.SelectedIndex].EstimateID))
							{
								EstimateMonthlyList.RemoveAt(listBoxMonthly.SelectedIndex);

								// 月額課金見積書情報リストボックスの設定
								this.SetListBoxMonthly();
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message, "見積書情報削除エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						}
					}
				}
			}
		}

		/// <summary>
		/// 見積書の宛先変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonNameChange_Click(object sender, EventArgs e)
		{
			if (0 == tabControlEstimate.SelectedIndex)
			{
				// おまとめプラン
				if (-1 != listBoxMatome.SelectedIndex)
				{
					int saveIndex = listBoxMatome.SelectedIndex;

					// 宛先の設定
					Estimate est = EstimateMatomeList[listBoxMatome.SelectedIndex];
					using (DestinationForm form = new DestinationForm(est.Destination, est.NotUsedMessrs))
					{
						if (DialogResult.OK == form.ShowDialog())
						{
							if (IsExistDestination(est.EstimateID, form.Destination, true))
							{
								MessageBox.Show("その宛先は既に存在しています。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
							}
							est.Destination = form.Destination;
							est.NotUsedMessrs = form.NotUsedMessrs;

							try
							{
								// 宛先の更新
								SQLiteMwsSimulationAccess.UpdateEstimateHeaderDestination(Program.GetDataFolder(), est.EstimateID, est.Destination, est.NotUsedMessrs);
							}
							catch (Exception ex)
							{
								MessageBox.Show(ex.Message, "宛先更新エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
								return;
							}
							// おまとめプラン見積書情報リストボックスの設定
							this.SetListBoxMatome();

							listBoxMatome.SelectedIndex = saveIndex;
						}
					}
				}
			}
			else
			{
				// 月額課金
				if (-1 != listBoxMonthly.SelectedIndex)
				{
					int saveIndex = listBoxMonthly.SelectedIndex;

					// 宛先の設定
					Estimate est = EstimateMonthlyList[listBoxMonthly.SelectedIndex];
					using (DestinationForm form = new DestinationForm(est.Destination, est.NotUsedMessrs))
					{
						if (DialogResult.OK == form.ShowDialog())
						{
							if (IsExistDestination(est.EstimateID, form.Destination, false))
							{
								MessageBox.Show("その宛先は既に存在しています。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return;
							}
							est.Destination = form.Destination;
							est.NotUsedMessrs = form.NotUsedMessrs;

							try
							{
								// 宛先の更新
								SQLiteMwsSimulationAccess.UpdateEstimateHeaderDestination(Program.GetDataFolder(), est.EstimateID, est.Destination, est.NotUsedMessrs);
							}
							catch (Exception ex)
							{
								MessageBox.Show(ex.Message, "宛先更新エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
								return;
							}
							// 月額課金見積書情報リストボックスの設定
							this.SetListBoxMonthly();

							listBoxMonthly.SelectedIndex = saveIndex;
						}
					}
				}
			}
		}

		/// <summary>
		/// 見積書のコピー
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCopy_Click(object sender, EventArgs e)
		{
			if (0 == tabControlEstimate.SelectedIndex)
			{
				// おまとめプラン
				if (-1 != listBoxMatome.SelectedIndex)
				{
					Estimate src = EstimateMatomeList[listBoxMatome.SelectedIndex];
					if (DialogResult.Yes == MessageBox.Show(string.Format("[{0}] のコピーを作成します。よろしいですか？", src.Destination), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					{
						Estimate copy = src.CloneDeep();
						try
						{
							// 次回見積書情報番号の取得
							copy.EstimateID = SQLiteMwsSimulationAccess.GetLastEstimateNumber(Program.GetDataFolder());

							// 宛先の変更
							copy.Destination = string.Format("{0} - コピー", copy.Destination);
							EstimateMatomeList.Add(copy);

							// 見積書情報の追加
							if (-1 == SQLiteMwsSimulationSetIO.InsertIntoEstimate(Program.GetDataFolder(), copy))
							{
								return;
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message, "見積書情報追加エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						}
						// おまとめプラン見積書情報リストボックスの設定
						this.SetListBoxMatome();

						listBoxMatome.SelectedIndex = EstimateMatomeList.Count - 1;
					}
				}
			}
			else
			{
				// 月額課金
				if (-1 != listBoxMonthly.SelectedIndex)
				{
					Estimate src = EstimateMonthlyList[listBoxMonthly.SelectedIndex];
					if (DialogResult.Yes == MessageBox.Show(string.Format("[{0}] のコピーを作成します。よろしいですか？", src.Destination), this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					{
						Estimate copy = src.CloneDeep();
						try
						{
							// 次回見積書情報番号の取得
							copy.EstimateID = SQLiteMwsSimulationAccess.GetLastEstimateNumber(Program.GetDataFolder());

							// 宛先の変更
							copy.Destination = string.Format("{0} - コピー", copy.Destination);
							EstimateMonthlyList.Add(copy);

							// 見積書情報の追加
							if (-1 == SQLiteMwsSimulationSetIO.InsertIntoEstimate(Program.GetDataFolder(), copy))
							{
								return;
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message, "見積書情報追加エラー", MessageBoxButtons.OK, MessageBoxIcon.Stop);
						}
						// 月額課金見積書情報リストボックスの設定
						this.SetListBoxMonthly();

						listBoxMonthly.SelectedIndex = EstimateMonthlyList.Count - 1;
					}
				}
			}
		}

		/// <summary>
		/// 見積書の印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonPrintEstimate_Click(object sender, EventArgs e)
		{
			if (0 == tabControlEstimate.SelectedIndex)
			{
				// おまとめプラン
				if (-1 != listBoxMatome.SelectedIndex)
				{
					// 見積書印刷
					this.PrintEstimate(PrintEstimateDef.MwsPaperType.Estimate, EstimateMatomeList[listBoxMatome.SelectedIndex], false);
				}
			}
			else
			{
				// 月額課金
				if (-1 != listBoxMonthly.SelectedIndex)
				{
					// 見積書印刷
					this.PrintEstimate(PrintEstimateDef.MwsPaperType.Estimate, EstimateMonthlyList[listBoxMonthly.SelectedIndex], false);
				}
			}
		}

		/// <summary>
		/// 注文請/注文請書の印刷
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonPrintOrder_Click(object sender, EventArgs e)
		{
			if (0 == tabControlEstimate.SelectedIndex)
			{
				// おまとめプラン
				if (-1 != listBoxMatome.SelectedIndex)
				{
					// 注文書印刷
					this.PrintEstimate(PrintEstimateDef.MwsPaperType.PurchaseOrder, EstimateMatomeList[listBoxMatome.SelectedIndex], false);

					if (DialogResult.Yes == MessageBox.Show("注文請書を印刷しますか？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					{
						// 注文請書印刷
						this.PrintEstimate(PrintEstimateDef.MwsPaperType.OrderConfirm, EstimateMatomeList[listBoxMatome.SelectedIndex], false);
					}
				}
			}
			else
			{
				// 月額課金
				if (-1 != listBoxMonthly.SelectedIndex)
				{
					// 注文書印刷
					this.PrintEstimate(PrintEstimateDef.MwsPaperType.PurchaseOrder, EstimateMonthlyList[listBoxMonthly.SelectedIndex], false);

					if (DialogResult.Yes == MessageBox.Show("注文請書を印刷しますか？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
					{
						// 注文請書印刷
						this.PrintEstimate(PrintEstimateDef.MwsPaperType.OrderConfirm, EstimateMonthlyList[listBoxMonthly.SelectedIndex], false);
					}
				}
			}
		}

		/// <summary>
		/// 拠点コンボボックスの変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxBranch_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (-1 != comboBoxBranch.SelectedIndex)
			{
				gSettings.CurrentBranchIndex = comboBoxBranch.SelectedIndex;
			}
		}

		/// <summary>
		/// 担当者コンボボックスの変更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxStaff_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (-1 != comboBoxStaff.SelectedIndex)
			{
				gSettings.CurrentStaffIndex = comboBoxStaff.SelectedIndex;
			}
		}

		/// <summary>
		/// 担当者の登録
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolStripMenuItemEnvStaff_Click(object sender, EventArgs e)
		{
			using (EnvironmentStaffForm form = new EnvironmentStaffForm(gSettings.StaffList))
			{
				if (DialogResult.OK == form.ShowDialog())
				{
					// 担当者コンボボックスの設定
					int index = comboBoxStaff.SelectedIndex;
					this.SetStaffComboBox();
					if (index < comboBoxStaff.Items.Count)
					{
						comboBoxStaff.SelectedIndex = index;
					}
					else
					{
						comboBoxStaff.SelectedIndex = 0;
					}
				}
			}
		}

		/// <summary>
		/// 備考の登録
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolStripMenuItemEnvRemark_Click(object sender, EventArgs e)
		{
			using (EnvironmentRemarkForm form = new EnvironmentRemarkForm(gSettings.RemarkList))
			{
				form.ShowDialog();
			}
		}

		/// <summary>
		/// バージョン情報
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolStripMenuItemVersion_Click(object sender, EventArgs e)
		{
			using (VersionInfoForm form = new VersionInfoForm())
			{
				form.ShowDialog();
			}
		}

		/// <summary>
		/// 閉じる
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Form Closed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
#if false
			// XMLサンプルデータ作成
			this.MakeSampleXmlData();
#endif
			// 環境設定の保存
			MwsSimulationSettingsIF.SetMwsSimulationSettings(gSettings);
		}


		////////////////////////////////////////////////////////////////////
		// 内部メソッド

		/// <summary>
		/// データファイルの更新
		/// </summary>
		/// <param name="dataFolder">データフォルダ</param>
		private bool UpdateDataFile(string dataFolder)
		{
			//if (ApplicationDeployment.IsNetworkDeployed)
			//{
			//	// オンライン
			//	if (Directory.Exists(Program.SERVER_DATA_FOLDER))
			//	{
			//		string srcMasterDB = Path.Combine(Program.SERVER_DATA_FOLDER, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME);
			//		string dstMasterDB = Path.Combine(dataFolder, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME);
			//		if (false == Directory.Exists(dataFolder))
			//		{
			//			// カレントデータフォルダの作成
			//			Directory.CreateDirectory(dataFolder);

			//			// MwsSimulationMaster.dbのコピー
			//			File.Copy(srcMasterDB, dstMasterDB, true);

			//			// MwsSimulationUser.dbのコピー
			//			string srcUserDB = Path.Combine(Program.SERVER_DATA_FOLDER, SQLiteMwsSimulationDef.MWS_SIMULATION_USER_DATABASE_NAME);
			//			string dstUserDB = Path.Combine(dataFolder, SQLiteMwsSimulationDef.MWS_SIMULATION_USER_DATABASE_NAME);
			//			File.Copy(srcUserDB, dstUserDB, true);

			//			// MwsSimulationSettings.xmlのコピー
			//			string srcSettings = Path.Combine(Program.SERVER_DATA_FOLDER, MwsSimulationSettingsIF.SETTINGS_CLIENT_FILE_NAME);
			//			string dstSettings = Path.Combine(dataFolder, MwsSimulationSettingsIF.SETTINGS_CLIENT_FILE_NAME);
			//			File.Copy(srcSettings, dstSettings, true);
			//		}
			//		else
			//		{
			//			DateTime srcUpdateDate = File.GetLastWriteTime(srcMasterDB);
			//			DateTime dstUpdateDate = File.GetLastWriteTime(dstMasterDB);
			//			if (dstUpdateDate < srcUpdateDate)
			//			{
			//				// MwsSimulationMaster.dbの更新
			//				File.Copy(srcMasterDB, dstMasterDB, true);
			//			}
			//		}
			//	}
			//	else if (false == Directory.Exists(dataFolder))
			//	{
			//		MessageBox.Show(string.Format("{0}が見つかりません。", dataFolder), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			//		return false;
			//	}
			//}
			string srcMasterDB = Path.Combine(Program.SERVER_DATA_FOLDER, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME);
			string dstMasterDB = Path.Combine(dataFolder, SQLiteMwsSimulationDef.MWS_SIMULATION_MASTER_DATABASE_NAME);
			DateTime srcUpdateDate = File.GetLastWriteTime(srcMasterDB);
			DateTime dstUpdateDate = File.GetLastWriteTime(dstMasterDB);
			if (dstUpdateDate < srcUpdateDate)
			{
				// MwsSimulationMaster.dbの更新
				File.Copy(srcMasterDB, dstMasterDB, true);
			}
			string srcPara = Path.Combine(Program.SERVER_DATA_FOLDER, PrintEstimateDef.PARAMETER_FILENAME);
			string dstPara = Path.Combine(dataFolder, PrintEstimateDef.PARAMETER_FILENAME);
			srcUpdateDate = File.GetLastWriteTime(srcPara);
			dstUpdateDate = File.GetLastWriteTime(dstPara);
			if (dstUpdateDate < srcUpdateDate)
			{
				// MWS_ORDER_01.PRMの更新
				File.Copy(srcPara, dstPara, true);
			}
			return true;
		}

		/// <summary>
		/// 宛先の存在確認
		/// </summary>
		/// <param name="destination">宛先</param>
		/// <param name="id">見積書番号</param>
		/// <param name="matome">おまとめプランかどうか？</param>
		/// <returns>判定</returns>
		private bool IsExistDestination(int id, string destination, bool matome)
		{
			if (matome)
			{
				// おまとめプラン
				if (-1 != EstimateMatomeList.FindIndex(p => p.EstimateID != id && p.Destination == destination))
				{
					return true;
				}
			}
			else
			{
				// 月額課金
				if (-1 != EstimateMonthlyList.FindIndex(p => p.EstimateID != id && p.Destination == destination))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 拠点コンボボックスの設定
		/// </summary>
		private void SetBranchComboBox()
		{
			comboBoxBranch.Items.Clear();
			foreach (BranchSettings branch in gSettings.BranchList)
			{
				comboBoxBranch.Items.Add(branch);
			}
		}

		/// <summary>
		/// 担当者コンボボックスの設定
		/// </summary>
		private void SetStaffComboBox()
		{
			comboBoxStaff.Items.Clear();
			foreach (string name in gSettings.StaffList)
			{
				comboBoxStaff.Items.Add(name);
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
		/// PrintPreviewForm BeginPrintイベント
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
					if (pdlg.ShowDialog() == DialogResult.Cancel)
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
			PrintInfo.PrintEstimateData(e.Graphics, gSettings.PaperOffset, page, false);
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

		/// <summary>
		/// XMLサンプルデータ作成
		/// </summary>
		private void MakeSampleXmlData()
		{
			gSettings.StaffList.Clear();
			gSettings.StaffList.Add("伊藤 正昭");
			gSettings.StaffList.Add("大谷 純一");
			gSettings.StaffList.Add("木村 剛");
			gSettings.StaffList.Add("佐々木 誠");
			gSettings.StaffList.Add("小河 寛明");

			gSettings.BranchList.Clear();
			BranchSettings branch = new BranchSettings();
			branch.Name = "札幌";
			branch.Zipcode = "060-0061";
			branch.Address1 = "北海道札幌市中央区南一条西 9-1-15";
			branch.Address2 = "井門札幌ビル 6F";
			branch.Tel = "011-251-3936";
			branch.Fax = "011-251-8488";
			gSettings.BranchList.Add(branch);

			branch = new BranchSettings();
			branch.Name = "盛岡";
			branch.Zipcode = "020-0871";
			branch.Address1 = "岩手県盛岡市中ノ橋通 1-4-22";
			branch.Address2 = "中ノ橋 106 ビル 4F";
			branch.Tel = "019-604-6727";
			branch.Fax = "019-604-6728";
			gSettings.BranchList.Add(branch);

			branch = new BranchSettings();
			branch.Name = "仙台";
			branch.Zipcode = "980-0802";
			branch.Address1 = "宮城県仙台市青葉区二日町 14-15";
			branch.Address2 = "アミ・グランデ二日町 2F";
			branch.Tel = "022-262-9361";
			branch.Fax = "022-262-9362";
			gSettings.BranchList.Add(branch);

			branch = new BranchSettings();
			branch.Name = "郡山";
			branch.Zipcode = "963-8874";
			branch.Address1 = "福島県郡山市深沢 1-6-3 8F";
			branch.Address2 = "";
			branch.Tel = "024-927-0484";
			branch.Fax = "024-927-0485";
			gSettings.BranchList.Add(branch);

			branch = new BranchSettings();
			branch.Name = "さいたま";
			branch.Zipcode = "330-0844";
			branch.Address1 = "埼玉県さいたま市大宮区下町 1-45";
			branch.Address2 = "松亀センタービル 4F";
			branch.Tel = "048-658-6025";
			branch.Fax = "048-658-6026";
			gSettings.BranchList.Add(branch);

			branch = new BranchSettings();
			branch.Name = "首都圏";
			branch.Zipcode = "103-0011";
			branch.Address1 = "東京都中央区日本橋大伝馬町 2-7";
			branch.Address2 = "日本橋第一ビル 7F";
			branch.Tel = "03-5651-9930";
			branch.Fax = "03-5651-9921";
			gSettings.BranchList.Add(branch);

			branch = new BranchSettings();
			branch.Name = "横浜";
			branch.Zipcode = "231-0011";
			branch.Address1 = "神奈川県横浜市中区太田町 6-87";
			branch.Address2 = "横浜フコク生命ビル 2F";
			branch.Tel = "045-222-6780";
			branch.Fax = "045-663-0866";
			gSettings.BranchList.Add(branch);

			branch = new BranchSettings();
			branch.Name = "名古屋";
			branch.Zipcode = "461-0001";
			branch.Address1 = "愛知県名古屋市東区泉 1-1-35";
			branch.Address2 = "ハイエスト久屋ビル 7F";
			branch.Tel = "052-950-2525";
			branch.Fax = "052-950-2526";
			gSettings.BranchList.Add(branch);

			branch = new BranchSettings();
			branch.Name = "大阪";
			branch.Zipcode = "532-0011";
			branch.Address1 = "大阪府淀川区西中島 6-1-3";
			branch.Address2 = "アストロ新大阪第２ビル 3F";
			branch.Tel = "06-6304-1044";
			branch.Fax = "06-6304-1706";
			gSettings.BranchList.Add(branch);

			branch = new BranchSettings();
			branch.Name = "金沢";
			branch.Zipcode = "920-0027";
			branch.Address1 = "福井県金沢市駅西新町 3-1-10";
			branch.Address2 = "NEWSビル 3F";
			branch.Tel = "076-265-7203";
			branch.Fax = "076-265-7204";
			gSettings.BranchList.Add(branch);

			branch = new BranchSettings();
			branch.Name = "広島";
			branch.Zipcode = "730-0016";
			branch.Address1 = "広島県広島市中区幟町 13-11";
			branch.Address2 = "明治安田生命広島幟町ビル 8F";
			branch.Tel = "082-511-2167";
			branch.Fax = "082-511-2168";
			gSettings.BranchList.Add(branch);

			branch = new BranchSettings();
			branch.Name = "福岡";
			branch.Zipcode = "812-0022";
			branch.Address1 = "福岡県福岡市博多区博多駅前 1-15-20";
			branch.Address2 = "NMF 博多駅前ビル 5F";
			branch.Tel = "092-472-9645";
			branch.Fax = "092-472-7492";
			gSettings.BranchList.Add(branch);
		}
	}
}
