//
// CheckContractServiceForm.cs
//
// 契約中サービスの確認画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 契約中サービスの確認機能の追加(2020/07/17 勝呂)
// 
using ClosedXML.Excel;
using MwsLib.BaseFactory;
using MwsLib.BaseFactory.Charlie.Table;
using MwsLib.BaseFactory.EntryFinishedUser;
using MwsLib.DB.SqlServer.Charlie;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EntryFinishedUser.Forms
{
	public partial class CheckContractServiceForm : Form
	{
		/// <summary>
		/// 契約中サービス確認ユーザーリスト
		/// </summary>
		private List<EntryFinishedUserData> CheckUserList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		private CheckContractServiceForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="list">契約中サービス確認ユーザーリスト</param>
		public CheckContractServiceForm(List<EntryFinishedUserData> list)
		{
			InitializeComponent();

			CheckUserList = list;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CheckContractServiceForm_Load(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			List<int> checkList = (from user in CheckUserList orderby user.CustomerID select user.CustomerID).ToList();
			string userStr = string.Join(",", checkList);

			// ESET月額版
			List<T_LICENSE_PRODUCT_CONTRACT> esetList = Program.ContractServiceESET(checkList);
			if (null != esetList)
			{
				foreach (T_LICENSE_PRODUCT_CONTRACT eset in esetList)
				{
					int index = CheckUserList.FindIndex(p => p.CustomerID == eset.CUSTOMER_ID);
					if (-1 != index)
					{
						ContractServiceUser data = new ContractServiceUser(CheckUserList[index]);
						data.ServiceID = eset.SERVICE_ID.ToString();
						data.ServiceName = CharlieDatabaseAccess.GetServiceName(eset.SERVICE_ID, Program.DATABACE_ACCEPT_CT);
						data.StartDate = eset.START_DATE;
						data.EndDate = eset.END_DATE;
						ListViewItem lvItem = new ListViewItem(data.GetListViewData());
						lvItem.Tag = data;
						listViewESET.Items.Add(lvItem);
					}
				}
			}
			// PC安心サポート
			List<T_USE_PCCSUPPORT> pcList = Program.ContractServicePcSupport(CheckUserList);
			if (null != pcList)
			{
				foreach (T_USE_PCCSUPPORT pc in pcList)
				{
					int index = CheckUserList.FindIndex(p => p.CustomerID == pc.fCustomerID);
					if (-1 != index)
					{
						ContractServiceUser data = new ContractServiceUser(CheckUserList[index]);
						data.ServiceID = pc.fServiceId.ToString();
						data.ServiceName = CharlieDatabaseAccess.GetServiceName(pc.fServiceId, Program.DATABACE_ACCEPT_CT);
						if (pc.fContractStartDate.HasValue)
						{
							data.StartDate = pc.fContractStartDate.Value.ToDateTime();
						}
						if (pc.fContractEndDate.HasValue)
						{
							data.EndDate = pc.fContractEndDate.Value.ToDateTime();
						}
						ListViewItem lvItem = new ListViewItem(data.GetListViewData());
						lvItem.Tag = data;
						listViewPcSupport.Items.Add(lvItem);
					}
				}
			}
			// ナルコーム製品
			List<T_CUSSTOMER_USE_INFOMATION> cuiList = Program.ContractServiceNarcohm(checkList);
			if (null != cuiList)
			{
				foreach (T_CUSSTOMER_USE_INFOMATION cui in cuiList)
				{
					int index = CheckUserList.FindIndex(p => p.CustomerID == cui.CUSTOMER_ID);
					if (-1 != index)
					{
						ContractServiceUser data = new ContractServiceUser(CheckUserList[index]);
						data.ServiceID = cui.SERVICE_ID.ToString();
						data.ServiceName = CharlieDatabaseAccess.GetServiceName(cui.SERVICE_ID, Program.DATABACE_ACCEPT_CT);
						data.StartDate = cui.USE_START_DATE;
						data.EndDate = cui.USE_END_DATE;
						ListViewItem lvItem = new ListViewItem(data.GetListViewData());
						lvItem.Tag = data;
						listViewNarcohm.Items.Add(lvItem);
					}
				}
			}
			// Microsoft365製品
			cuiList = Program.ContractService365(checkList);
			if (null != cuiList)
			{
				foreach (T_CUSSTOMER_USE_INFOMATION cui in cuiList)
				{
					int index = CheckUserList.FindIndex(p => p.CustomerID == cui.CUSTOMER_ID);
					if (-1 != index)
					{
						ContractServiceUser data = new ContractServiceUser(CheckUserList[index]);
						data.ServiceID = cui.SERVICE_ID.ToString();
						data.ServiceName = CharlieDatabaseAccess.GetServiceName(cui.SERVICE_ID, Program.DATABACE_ACCEPT_CT);
						data.StartDate = cui.USE_START_DATE;
						data.EndDate = cui.USE_END_DATE;
						ListViewItem lvItem = new ListViewItem(data.GetListViewData());
						lvItem.Tag = data;
						listViewMicrosoft365.Items.Add(lvItem);
					}
				}
			}
			// Curlineクラウド
			List<int> noList = Program.ContractServiceCurlineCloud();
			if (null != noList)
			{
				foreach (int no in noList)
				{
					int index = CheckUserList.FindIndex(p => p.CustomerID == no);
					if (-1 != index)
					{
						ContractServiceUser data = new ContractServiceUser(CheckUserList[index]);
						data.ServiceID = PcaGoodsIDDefine.MwsCurlineCloud;
						data.ServiceName = "MWS Curline ｸﾗｳﾄﾞ利用料(月額)";
						ListViewItem lvItem = new ListViewItem(data.GetListViewData2());
						lvItem.Tag = data;
						listViewCurline.Items.Add(lvItem);
					}
				}
			}
			// はなはなし購読
			noList = Program.ContractServiceHanahanashi();
			if (null != noList)
			{
				foreach (int no in noList)
				{
					int index = CheckUserList.FindIndex(p => p.CustomerID == no);
					if (-1 != index)
					{
						ContractServiceUser data = new ContractServiceUser(CheckUserList[index]);
						data.ServiceID = PcaGoodsIDDefine.Hanahanashi;
						data.ServiceName = "はなはなし";
						ListViewItem lvItem = new ListViewItem(data.GetListViewData2());
						lvItem.Tag = data;
						listViewStory.Items.Add(lvItem);
					}
				}
			}
			// 介護連携、介護伝送
			cuiList = Program.ContractServiceKaigo(checkList);
			if (null != cuiList)
			{
				foreach (T_CUSSTOMER_USE_INFOMATION cui in cuiList)
				{
					int index = CheckUserList.FindIndex(p => p.CustomerID == cui.CUSTOMER_ID);
					if (-1 != index)
					{
						ContractServiceUser data = new ContractServiceUser(CheckUserList[index]);
						data.ServiceID = cui.SERVICE_ID.ToString();
						data.ServiceName = CharlieDatabaseAccess.GetServiceName(cui.SERVICE_ID, Program.DATABACE_ACCEPT_CT);
						data.StartDate = cui.USE_START_DATE;
						data.EndDate = cui.USE_END_DATE;
						ListViewItem lvItem = new ListViewItem(data.GetListViewData());
						lvItem.Tag = data;
						listViewKaigo.Items.Add(lvItem);
					}
				}
			}
			// カーソルを元に戻す
			Cursor.Current = preCursor;
		}

		/// <summary>
		/// EXCEL出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExcel_Click(object sender, EventArgs e)
		{
			using (XLWorkbook workbook = new XLWorkbook())
			{
				// 元のカーソルを保持
				Cursor preCursor = Cursor.Current;

				// カーソルを待機カーソルに変更
				Cursor.Current = Cursors.WaitCursor;

				// タイトル行
				var titleArray = new[] { "顧客No", "得意先No", "顧客名", "終了月", "拠点コード", "拠点名", "サービスID", "サービス名", "利用開始日", "利用終了日", "ユーザー" };

				// ESET月額版
				IXLWorksheet sheetESET = workbook.Worksheets.Add("ESET月額版");
				sheetESET.Cell("A1").InsertData(new[] { titleArray });
				for (int i = 0; i < listViewESET.Items.Count; i++)
				{
					ContractServiceUser user = listViewESET.Items[i].Tag as ContractServiceUser;
					sheetESET.Cell(i + 2, 1).InsertData(new[] { user.GetExcelData() });
				}
				// PC安心サポート
				IXLWorksheet sheetPC = workbook.Worksheets.Add("PC安心サポート");
				sheetPC.Cell("A1").InsertData(new[] { titleArray });
				for (int i = 0; i < listViewPcSupport.Items.Count; i++)
				{
					ContractServiceUser user = listViewPcSupport.Items[i].Tag as ContractServiceUser;
					sheetPC.Cell(i + 2, 1).InsertData(new[] { user.GetExcelData() });
				}
				// ナルコーム製品
				IXLWorksheet sheetNarcohm = workbook.Worksheets.Add("ナルコーム製品");
				sheetNarcohm.Cell("A1").InsertData(new[] { titleArray });
				for (int i = 0; i < listViewNarcohm.Items.Count; i++)
				{
					ContractServiceUser user = listViewNarcohm.Items[i].Tag as ContractServiceUser;
					sheetNarcohm.Cell(i + 2, 1).InsertData(new[] { user.GetExcelData() });
				}
				// Microsoft365製品
				IXLWorksheet sheet365 = workbook.Worksheets.Add("Microsoft365製品");
				sheet365.Cell("A1").InsertData(new[] { titleArray });
				for (int i = 0; i < listViewMicrosoft365.Items.Count; i++)
				{
					ContractServiceUser user = listViewMicrosoft365.Items[i].Tag as ContractServiceUser;
					sheet365.Cell(i + 2, 1).InsertData(new[] { user.GetExcelData() });
				}
				// Curlineクラウド
				var titleArray2 = new[] { "顧客No", "得意先No", "顧客名", "終了月", "拠点コード", "拠点名", "商品コード", "商品名", "ユーザー" };
				IXLWorksheet sheetCurline = workbook.Worksheets.Add("Curlineクラウド");
				sheetCurline.Cell("A1").InsertData(new[] { titleArray2 });
				for (int i = 0; i < listViewCurline.Items.Count; i++)
				{
					ContractServiceUser user = listViewCurline.Items[i].Tag as ContractServiceUser;
					sheetCurline.Cell(i + 2, 1).InsertData(new[] { user.GetExcelData2() });
				}
				// はなはなし購読
				IXLWorksheet sheetStory = workbook.Worksheets.Add("はなはなし購読");
				sheetStory.Cell("A1").InsertData(new[] { titleArray2 });
				for (int i = 0; i < listViewStory.Items.Count; i++)
				{
					ContractServiceUser user = listViewStory.Items[i].Tag as ContractServiceUser;
					sheetStory.Cell(i + 2, 1).InsertData(new[] { user.GetExcelData2() });
				}
				// 介護連携、介護伝送
				IXLWorksheet sheetKaigo = workbook.Worksheets.Add("介護");
				sheetKaigo.Cell("A1").InsertData(new[] { titleArray });
				for (int i = 0; i < listViewKaigo.Items.Count; i++)
				{
					ContractServiceUser user = listViewKaigo.Items[i].Tag as ContractServiceUser;
					sheetKaigo.Cell(i + 2, 1).InsertData(new[] { user.GetExcelData() });
				}
				// Excelファイルの保存
				workbook.SaveAs(Path.Combine(Directory.GetCurrentDirectory(), @"契約中サービス一覧.xlsx"));

				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("契約中サービス一覧.xlsxを出力しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}
}
