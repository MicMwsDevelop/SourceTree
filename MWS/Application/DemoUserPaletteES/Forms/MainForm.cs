//
// MainForm.cs
//
// デモユーザー palette ESサービス設定 メイン画面
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/10/18 勝呂)
// 
using MwsLib.BaseFactory;
using MwsLib.BaseFactory.Coupler.Table;
using MwsLib.DB.SqlServer.Coupler;
using MwsLib.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DemoUserPaletteES.Forms
{
	public partial class MainForm : Form
    {
		/// <summary>
		/// データベース接続情報
		/// </summary>
		private DatabaseConnect CouplerConnect;

		/// <summary>
		/// MWS-IDリスト.txt読み込みフォルダ
		/// </summary>
		private string CurrentFolder;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
        {
            InitializeComponent();

			CurrentFolder = @"C:\Public";
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
        {
			this.Text += "  " + Program.Version;

			SqlServerConnectSettings settings = SqlServerConnectSettingsIF.GetSettingsXML();
			if (Program.DATABACE_ACCEPT_CT)
			{
				CouplerConnect = settings.CouplerCT;
			}
			else
			{
				CouplerConnect = settings.Coupler;
			}
		}

		/// <summary>
		/// MWS-IDリストの読み込み
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonFolder_Click(object sender, EventArgs e)
        {
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.FileName = "MWS-IDリスト.txt";
				ofd.InitialDirectory = CurrentFolder;
				ofd.Filter = "テキストLファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
				ofd.Title = "MWS-IDリストファイルを選択してください";
				ofd.RestoreDirectory = true;
				ofd.CheckFileExists = true;
				ofd.CheckPathExists = true;
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					// 元のカーソルを保持
					Cursor preCursor = Cursor.Current;

					// カーソルを待機カーソルに変更
					Cursor.Current = Cursors.WaitCursor;

					labelPathname.Text = ofd.FileName;
					CurrentFolder = Path.GetDirectoryName(ofd.FileName);

					// データ初期化
					listViewDemoUser.Items.Clear();
					listBoxError.Items.Clear();

					List<string> demoUserList = new List<string>();
					try
					{
						using (var sr = new StreamReader(ofd.FileName))
						{
							while (!sr.EndOfStream)
							{
								var line = sr.ReadLine();
								var values = line.Split(',');
								foreach (var value in values)
								{
									if (';' == value[0])
									{
										continue;
									}
									string id = value.Trim();
									if (11 != id.Length)
									{
										MessageBox.Show("MWS-IDが設定されていません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
										return;
									}
									demoUserList.Add(id);
								}
							}
						}
					}
					catch (System.Exception ex)
					{
						// ファイルを開くのに失敗したとき
						System.Console.WriteLine(ex.Message);
					}
					foreach (string id in demoUserList)
					{
						List<T_COUPLER_PRODUCTUSER> userList = CouplerDatabaseAccess.Select_T_COUPLER_PRODUCTUSER(CouplerConnect, string.Format("cp_id = '{0}'", id));
						if (null != userList && 0 < userList.Count)
						{
							T_COUPLER_PRODUCTUSER user = userList.First();
							string[] data = new string[3];
							data[0] = user.cp_id;
							data[1] = user.customer_id.ToString();
							data[2] = user.customer_nm;
							ListViewItem lvItem = new ListViewItem(data);
							lvItem.Tag = user;
							listViewDemoUser.Items.Add(lvItem);
						}
						else
						{
							// カプラー未登録リストに追加
							listBoxError.Items.Add(id);
						}
					}
					// カーソルを元に戻す
					Cursor.Current = preCursor;

					MessageBox.Show("準備ができました。実行ボタンを押下してください。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
        {
			if (0 < listViewDemoUser.Items.Count)
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// [Coupler].[dbo].[SERVICE]に palette ESの下記のサービスを追加
				// 1016180 自費見積書発行
				// 1016200 インプラント管理
				// 1018280 治療費等請求書
				// 1024220 保険処置頻度表
				// 1024240 摘要欄文頻度表
				// 1024260 病名頻度表
				// 1024280 薬価頻度表
				// 1024300 カルテ文頻度表
				// 1024320 登録患者一覧表
				// 1024340 保険証確認患者一覧表
				// 1024360 点数一覧表
				// 1024380 薬価一覧表
				// 1024400 算定ルール一覧表
				// 1024420 保険請求額概算集計表
				// 1036140 補綴状況管理(ｺﾝﾃﾞｨｼｮﾝﾋﾞｭｰ)
				// 1036200 電子レセプトカルテビューワ
				// 1036220 paletteアカウント
				// 1040100 口腔ケアノート
				// 1042100 電子カルテ標準サービス
				// 9910100 palette ES
				List<int> addSV = new List<int>();
				addSV.Add((int)ServiceCodeDefine.ServiceCode.JihiEstimate);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.JihiImplant);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.JihiSeikyusho);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.ReportHindoHoken);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.ReportHindoTekiyo);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.ReportHindoByomei);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.ReportHindoYakka);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.ReportHindoChart);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.ReportPatient);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.ReportComfirmInsurance);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.ReportPoint);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.ReportYakka);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.ReportRule);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.ReportHokenSeikyu);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.ExConditionView);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.ExRezeptComputeChartViewer);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.ExPaletteAccount);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.MouthCareNote);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.ElectricChart);
				addSV.Add((int)ServiceCodeDefine.ServiceCode.PaletteES);

				// [Coupler].[dbo].[SERVICE]に palette ESの下記のサービスを削除
				// 1020100 予約管理標準サービス
				// 1020120 予約キャンセル管理
				// 1028120 地図分析
				// 1030100 ＰＬＭ
				// 1030120 ３ＤｅｎｔＭＯＶＩＥ
				// 1030140 患者情報出力
				// 1032100 訪問診療標準サービス
				// 1036100 保険証ＯＣＲ
				// 1510100 シカハコ
				// 2010100 りすとん
				List<int> delSV = new List<int>();
				delSV.Add((int)ServiceCodeDefine.ServiceCode.AppointStandard);
				delSV.Add((int)ServiceCodeDefine.ServiceCode.AppointCancel);
				delSV.Add((int)ServiceCodeDefine.ServiceCode.AnalyzeMap);
				delSV.Add((int)ServiceCodeDefine.ServiceCode.LinkPlm);
				delSV.Add((int)ServiceCodeDefine.ServiceCode.LinkThreeDentMovie);
				delSV.Add((int)ServiceCodeDefine.ServiceCode.LinkPatientOut);
				delSV.Add((int)ServiceCodeDefine.ServiceCode.HomonStadard);
				delSV.Add((int)ServiceCodeDefine.ServiceCode.ExHokenshoOcr);
				delSV.Add((int)ServiceCodeDefine.ServiceCode.Shikahako);
				delSV.Add((int)ServiceCodeDefine.ServiceCode.Liston);

				foreach (ListViewItem item in listViewDemoUser.Items)
				{
					if (null != item.Tag)
					{
						T_COUPLER_PRODUCTUSER user = item.Tag as T_COUPLER_PRODUCTUSER;

						// 1010100 レセプト標準サービスの利用開始日を取得
						DateTime startDate = DateTime.Today;
						try
						{
							List<T_COUPLER_SERVICE> work = CouplerDatabaseAccess.Select_T_COUPLER_SERVICE(CouplerConnect, string.Format("cp_id = '{0}' AND service_id = {1}", user.cp_id, (int)ServiceCodeDefine.ServiceCode.RecieptStandard));
							if (null != work && 0 < work.Count)
							{
								if (work.First().start_date.HasValue)
								{
									startDate = work.First().start_date.Value;
								}
							}
						}
						catch
						{
							;
						}
						// [Coupler].[dbo].[SERVICE]にサービス追加
						foreach (int code in addSV)
						{
							try
							{
								List<T_COUPLER_SERVICE> work = CouplerDatabaseAccess.Select_T_COUPLER_SERVICE(CouplerConnect, string.Format("cp_id = '{0}' AND service_id = {1}", user.cp_id, code));
								if (null != work && 0 < work.Count)
								{
									// 更新
									T_COUPLER_SERVICE service = work.First();
									service.start_date = startDate;
									service.end_date = new DateTime(2999, 12, 31);
									service.contrac_type = 0;
									service.update_date = DateTime.Today;
									service.update_user = "DemoUserPaletteES";
									try
									{
										CouplerDatabaseAccess.UpdateSet_T_COUPLER_SERVICE(CouplerConnect, service);
									}
									catch (Exception ex)
									{
										MessageBox.Show(string.Format("UpdateSet_T_COUPLER_SERVICE({0})", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
									}
								}
								else
								{
									// 新規追加
									T_COUPLER_SERVICE service = new T_COUPLER_SERVICE
									{
										cp_id = user.cp_id,
										service_id = code,
										start_date = startDate,
										end_date = new DateTime(2999, 12, 31),
										contrac_type = 0,
										payment_type = 0,
										create_date = DateTime.Today,
										create_user = "DemoUserPaletteES"
									};
									try
									{
										CouplerDatabaseAccess.InsertInto_T_COUPLER_SERVICE(CouplerConnect, service);
									}
									catch (Exception ex)
									{
										MessageBox.Show(string.Format("InsertInto_T_COUPLER_SERVICE({0})", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
									}
								}
							}
							catch (Exception ex)
							{
								MessageBox.Show(string.Format("Select_T_COUPLER_SERVICE({0})", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
							}
						}
						// [Coupler].[dbo].[SERVICE]にサービスの解約
						foreach (int code in delSV)
						{
							List<T_COUPLER_SERVICE> work = CouplerDatabaseAccess.Select_T_COUPLER_SERVICE(CouplerConnect, string.Format("cp_id = '{0}' AND service_id = {1}", user.cp_id, code));
							if (null != work && 0 < work.Count)
							{
								// 更新
								T_COUPLER_SERVICE service = work.First();
								service.contrac_type = 1;
								service.update_date = DateTime.Today;
								service.update_user = "DemoUserPaletteES";
								try
								{
									CouplerDatabaseAccess.UpdateSet_T_COUPLER_SERVICE(CouplerConnect, service);
								}
								catch (Exception ex)
								{
									MessageBox.Show(string.Format("UpdateSet_T_COUPLER_SERVICE({0})", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								}
							}
						}
					}
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("デモユーザーにpalette ES サービスを設定しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}
}
