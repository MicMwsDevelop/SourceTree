//
// MainForm.cs
//
// 伝票確認ツール メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/04/17 勝呂)
// Ver1.10 PC安心サポートPlus対応(2020/10/16 勝呂)
// Ver1.11 PC安心サポートPlus切替対応(2020/10/29 勝呂)
// 
using ClosedXML.Excel;
using MwsLib.BaseFactory;
using MwsLib.BaseFactory.Charlie.Table;
using MwsLib.BaseFactory.Junp.CheckOrderSlip;
using MwsLib.Common;
using MwsLib.DB.SqlServer.Charlie;
using MwsLib.DB.SqlServer.CheckOrderSlip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CheckOrderSlip
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// 伝票チェック種別
		/// </summary>
		private enum CheckType
		{
			/// <summary>
			/// palette ES
			/// </summary>
			PaletteES = 0,

			/// <summary>
			/// おまとめプラン
			/// </summary>
			Matome = 1,

			/// <summary>
			/// PC安心サポート
			/// </summary>
			PcSupport = 2,

			/// <summary>
			/// MWSプラットフォーム利用料
			/// </summary>
			Platform = 3,

			/// <summary>
			/// PC安心サポートPlus切替
			/// </summary>
			/// Ver1.11 PC安心サポートPlus切替対応(2020/10/29 勝呂)
			ChangePcSupportPlus,
		}

		/// <summary>
		/// Excelファイル名
		/// </summary>
		private readonly string ExcelFileName = "伝票確認-{0}.xlsx";

		/// <summary>
		/// 伝票チェック種別
		/// </summary>
		private CheckType Mode { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();

			Mode = CheckType.PaletteES;
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			// チェック対象コンボボックスの設定
			comboBoxMode.Items.Add("paletteES");
			comboBoxMode.Items.Add("おまとめプラン");
			comboBoxMode.Items.Add("PC安心サポート");
			comboBoxMode.Items.Add("MWSプラットフォーム利用料");

			// Ver1.11 PC安心サポートPlus切替対応(2020/10/29 勝呂)
			comboBoxMode.Items.Add("PC安心サポートPlus切替");
			comboBoxMode.SelectedIndex = 0;

			// 検索日に当月初日を設定
			Date firstDate = new Date(Date.Today.Year, Date.Today.Month, 1);
			dateTimePickerSearchDate.Value = firstDate.ToDateTime();
		}

		/// <summary>
		/// 確認対象コンボボックスの選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void comboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (-1 != comboBoxMode.SelectedIndex)
			{
				Mode = (CheckType)comboBoxMode.SelectedIndex;

				listViewSlip.Items.Clear();
				listBoxError.Items.Clear();
			}
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			listViewSlip.Items.Clear();
			listBoxError.Items.Clear();

			// 検索日
			Date checkDate = new Date(dateTimePickerSearchDate.Value.Year, dateTimePickerSearchDate.Value.Month, dateTimePickerSearchDate.Value.Day);

			int ret = 0;
			try
			{
				switch (Mode)
				{
					case CheckType.PaletteES:
						ret = CheckPaletteES(checkDate);
						break;
					case CheckType.Matome:
						ret = CheckMatome(checkDate);
						break;
					case CheckType.PcSupport:
						ret = CheckPcSupport(checkDate);
						break;
					case CheckType.Platform:
						ret = CheckPlatform(checkDate);
						break;
					// Ver1.11 PC安心サポートPlus切替対応(2020/10/29 勝呂)
					case CheckType.ChangePcSupportPlus:
						ret = CheckChangePcSupportPlus(checkDate);
						break;
				}
				// カーソルを元に戻す
				Cursor.Current = preCursor;

				MessageBox.Show("確認終了", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}

		/// <summary>
		/// 受注日
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonOrder_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonOrder.Checked)
			{
				buttonExec.PerformClick();
			}
		}

		/// <summary>
		/// 受注承認日
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonOrderAccept_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonOrderAccept.Checked)
			{
				buttonExec.PerformClick();
			}
		}

		/// <summary>
		/// 売上承認日
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonSale_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonSale.Checked)
			{
				buttonExec.PerformClick();
			}
		}

		/// <summary>
		/// エラー行のみ表示
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void checkBoxOnlyError_CheckedChanged(object sender, EventArgs e)
		{
			buttonExec.PerformClick();
		}

		/// <summary>
		/// エラー情報の表示
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewSlip_SelectedIndexChanged(object sender, EventArgs e)
		{
			listBoxError.Items.Clear();

			if (0 < listViewSlip.SelectedIndices.Count)
			{
				ListViewItem lvItem = listViewSlip.SelectedItems[0];
				if (null != lvItem)
				{
					OrderSlipData slip = lvItem.Tag as OrderSlipData;
					if (0 < slip.ErrorList.Count)
					{
						listBoxError.Items.AddRange(slip.ErrorList.ToArray());
					}
				}
			}
		}

		/// <summary>
		/// 伝票情報の表示
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewSlip_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (0 < listViewSlip.SelectedIndices.Count)
			{
				ListViewItem lvItem = listViewSlip.SelectedItems[0];
				if (null != lvItem)
				{
					OrderSlipForm form = new OrderSlipForm();
					form.Order = lvItem.Tag as OrderSlipData;
					form.ShowDialog();
				}
			}
		}

		/// <summary>
		/// EXCEL出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExcel_Click(object sender, EventArgs e)
		{
			if (0 < listViewSlip.Items.Count)
			{
				var workbook = new XLWorkbook();
				var worksheet = workbook.Worksheets.Add("Sheet");

				string[] titles = OrderSlipData.GetExcelTitle();
				for (int i = 0; i < titles.Length; i++)
				{
					worksheet.Cell(1, i + 1).Value = titles[i];
				}
				for (int i = 0; i < listViewSlip.Items.Count; i++)
				{
					OrderSlipData slip = listViewSlip.Items[i].Tag as OrderSlipData;
					List<string> row = slip.GetExcelRow();
					for (int j = 0; j < row.Count; j++)
					{
						worksheet.Cell(i + 2, j + 1).Style.NumberFormat.Format = "@";
						worksheet.Cell(i + 2, j + 1).Value = row[j];
					}
				}
				try
				{
					string chkStr = string.Empty;
					switch (Mode)
					{
						case CheckType.PaletteES:
							chkStr = "paletteES";
							break;
						case CheckType.Matome:
							chkStr = "Matome";
							break;
						case CheckType.PcSupport:
							chkStr = "PcSupport";
							break;
						case CheckType.Platform:
							chkStr = "Platform";
							break;
						// Ver1.11 PC安心サポートPlus切替対応(2020/10/29 勝呂)
						case CheckType.ChangePcSupportPlus:
							chkStr = "ChangePcSupportPlus";
							break;
					}
					string pathName = Path.Combine(Directory.GetCurrentDirectory(), string.Format(ExcelFileName, chkStr));
					workbook.SaveAs(pathName);
					MessageBox.Show(string.Format("{0} を出力しました。", pathName), "出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "ワーニング", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
		}

		/// <summary>
		/// 伝票リストビューの設定
		/// </summary>
		/// <param name="list">受注伝票リスト</param>
		private void SetListViewSlip(List<OrderSlipData> list)
		{
			foreach (OrderSlipData slip in list)
			{
				if (checkBoxOnlyError.Checked)
				{
					if (slip.IsError)
					{
						if (radioButtonOrderAccept.Checked)
						{
							if (slip.Is受注承認)
							{
								ListViewItem item = new ListViewItem(slip.GetListViewItem());
								item.Tag = slip;
								listViewSlip.Items.Add(item);
							}
						}
						else if (radioButtonSale.Checked)
						{
							if (slip.Is売上承認)
							{
								ListViewItem item = new ListViewItem(slip.GetListViewItem());
								item.Tag = slip;
								listViewSlip.Items.Add(item);
							}
						}
						else
						{
							ListViewItem item = new ListViewItem(slip.GetListViewItem());
							item.Tag = slip;
							listViewSlip.Items.Add(item);
						}
					}
				}
				else
				{
					if (radioButtonOrderAccept.Checked)
					{
						if (slip.Is受注承認)
						{
							ListViewItem item = new ListViewItem(slip.GetListViewItem());
							item.Tag = slip;
							listViewSlip.Items.Add(item);
						}
					}
					else if (radioButtonSale.Checked)
					{
						if (slip.Is売上承認)
						{
							ListViewItem item = new ListViewItem(slip.GetListViewItem());
							item.Tag = slip;
							listViewSlip.Items.Add(item);
						}
					}
					else
					{
						ListViewItem item = new ListViewItem(slip.GetListViewItem());
						item.Tag = slip;
						listViewSlip.Items.Add(item);
					}
				}
			}
		}

		/// <summary>
		/// paletteES 伝票チェック
		/// </summary>
		/// <param name="checkDate">検索開始日</param>
		/// <returns>検索数</returns>
		private int CheckPaletteES(Date checkDate)
		{
			// チェック対象の商品の設定
			List<string> goods = new List<string>();
			goods.Add(PcaGoodsIDDefine.PaletteES_2019);
			goods.Add(PcaGoodsIDDefine.PaletteES_Mainte72);
			goods.Add(PcaGoodsIDDefine.PaletteES_Mainte12);

			// 受注伝票情報リストの取得
			List<OrderSlipData> list = null;
			if (radioButtonOrder.Checked)
			{
				list = CheckOrderSlipAccess.GetOrderSlipList(checkDate, goods, Program.DATABASE_ACCESS_CT);
			}
			else if (radioButtonOrderAccept.Checked)
			{
				list = CheckOrderSlipAccess.GetOrderAcceptSlipList(checkDate, goods, Program.DATABASE_ACCESS_CT);
			}
			else
			{
				list = CheckOrderSlipAccess.GetSaleSlipList(checkDate, goods, Program.DATABASE_ACCESS_CT);
			}
			if (null != list)
			{
				// paletteESのみ抽出
				List<OrderSlipData> listES = OrderSlipData.SelectPaletteES(list);
				if (null != listES)
				{
					foreach (OrderSlipData es in listES)
					{
						// paletteESの販売種別がＶＰかどうか？
						if (MwsDefine.ApplyType.ValuePack != es.販売種別)
						{
							es.ErrorList.Add("paletteESの販売種別がＶＰでない");
						}
						// paletteESの利用期間が72ヵ月かどうか？
						if (72 != es.利用期間.GetMonthCount())
						{
							es.ErrorList.Add("paletteESの利用期間が72ヵ月でない");
						}
						//if (es.受注日.HasValue && Date.MinValue != es.利用期間.Start)
						//{
						//	if (6 < new Span(new Date(es.受注日.Value), es.利用期間.Start).GetMonthCount())
						//	{
						//		es.ErrorList.Add("paletteESの利用開始日が受注日の半年以降");
						//	}
						//}
						// 同伝票にｿﾌﾄｳｪｱ保守料72ケ月が存在するか？
						OrderSlipData mainte72 = OrderSlipData.GetSameMainte72(list, es);
						if (null == mainte72)
						{
							// 別伝票にｿﾌﾄｳｪｱ保守料72ケ月が存在するか？
							mainte72 = OrderSlipData.GetAnotherMainte72(list, es);
							if (null != mainte72)
							{
								if (es.利用期間 != mainte72.利用期間)
								{
									mainte72.ErrorList.Add("paletteESの利用期間とｿﾌﾄｳｪｱ保守料の利用期間が違う");
								}
							}
							else
							{
								// 別伝票にｿﾌﾄｳｪｱ保守料12ケ月が存在するか？
								OrderSlipData mainte12 = OrderSlipData.GetAnotherMainte12(list, es);
								if (null != mainte12)
								{
									if (es.利用期間.Start != mainte12.利用期間.Start)
									{
										mainte12.ErrorList.Add("paletteESの利用開始日とｿﾌﾄｳｪｱ保守料の利用開始日が違う");
									}
								}
								else
								{
									es.ErrorList.Add("ｿﾌﾄｳｪｱ保守料の伝票が存在しない");
								}
							}
						}
						if (es.NoReplace)
						{
							es.ErrorList.Add("リプレースなし");
						}
					}
				}
				// ｿﾌﾄｳｪｱ保守料72ケ月のみ抽出
				List<OrderSlipData> listMainte72 = OrderSlipData.SelectMainte72(list);
				if (null != listMainte72)
				{
					foreach (OrderSlipData mainte in listMainte72)
					{
						OrderSlipData es = OrderSlipData.GetSamePaletteES(list, mainte);
						if (null == es)
						{
							// 同伝票にpaletteESが存在しない

							// ｿﾌﾄｳｪｱ保守料72ケ月の販売種別が月額課金かどうか？
							if (MwsDefine.ApplyType.Monthly != mainte.販売種別)
							{
								mainte.ErrorList.Add("ｿﾌﾄｳｪｱ保守料72ケ月の販売種別が月額課金でない");
							}
							// ｿﾌﾄｳｪｱ保守料72ケ月の利用期間が72ヵ月かどうか？
							if (72 != mainte.利用期間.GetMonthCount())
							{
								mainte.ErrorList.Add("ｿﾌﾄｳｪｱ保守料72ケ月の利用期間が72ヵ月でない");
							}
						}
					}
				}
				// ｿﾌﾄｳｪｱ保守料12ケ月のみ抽出
				List<OrderSlipData> listMainte12 = OrderSlipData.SelectMainte12(list);
				if (null != listMainte12)
				{
					foreach (OrderSlipData mainte in listMainte12)
					{
						OrderSlipData es = OrderSlipData.GetSamePaletteES(list, mainte);
						if (null == es)
						{
							// 同伝票にpaletteESが存在しない

							// ｿﾌﾄｳｪｱ保守料12ケ月の販売種別が月額課金かどうか？
							if (MwsDefine.ApplyType.Monthly != mainte.販売種別)
							{
								mainte.ErrorList.Add("ｿﾌﾄｳｪｱ保守料12ケ月の販売種別が月額課金でない");
							}
							// ｿﾌﾄｳｪｱ保守料12ケ月の利用期間が12ヵ月かどうか？
							if (12 != mainte.利用期間.GetMonthCount())
							{
								mainte.ErrorList.Add("ｿﾌﾄｳｪｱ保守料12ケ月の利用期間が12ヵ月でない");
							}
						}
					}
				}
				// 受注伝票リストビューの設定
				SetListViewSlip(list);

				return list.Count;
			}
			return 0;
		}

		/// <summary>
		/// おまとめプラン 伝票チェック
		/// </summary>
		/// <param name="checkDate">検索開始日</param>
		/// <returns>検索数</returns>
		private int CheckMatome(Date checkDate)
		{
			// チェック対象の商品の設定
			List<string> goods = new List<string>();
			goods.Add(PcaGoodsIDDefine.Matome12);
			goods.Add(PcaGoodsIDDefine.Matome24);
			goods.Add(PcaGoodsIDDefine.Matome36);
			goods.Add(PcaGoodsIDDefine.Matome48);
			goods.Add(PcaGoodsIDDefine.Matome60);

			// 受注伝票情報リストの取得
			List<OrderSlipData> list = null;
			if (radioButtonOrder.Checked)
			{
				list = CheckOrderSlipAccess.GetOrderSlipList(checkDate, goods, Program.DATABASE_ACCESS_CT);
			}
			else if (radioButtonOrderAccept.Checked)
			{
				list = CheckOrderSlipAccess.GetOrderAcceptSlipList(checkDate, goods, Program.DATABASE_ACCESS_CT);
			}
			else
			{
				list = CheckOrderSlipAccess.GetSaleSlipList(checkDate, goods, Program.DATABASE_ACCESS_CT);
			}
			if (null != list)
			{
				foreach (OrderSlipData slip in list)
				{
					if (MwsDefine.ApplyType.Matome != slip.販売種別)
					{
						slip.ErrorList.Add("おまとめプランの販売種別がまとめでない");
					}
					switch (slip.商品コード)
					{
						case PcaGoodsIDDefine.Matome12:
							if (12 != slip.利用期間.GetMonthCount())
							{
								slip.ErrorList.Add("おまとめプラン12ケ月の利用期間が12ヵ月でない");
							}
							break;
						case PcaGoodsIDDefine.Matome24:
							if (24 != slip.利用期間.GetMonthCount())
							{
								slip.ErrorList.Add("おまとめプラン24ケ月の利用期間が24ヵ月でない");
							}
							break;
						case PcaGoodsIDDefine.Matome36:
							if (36 != slip.利用期間.GetMonthCount())
							{
								slip.ErrorList.Add("おまとめプラン36ケ月の利用期間が36ヵ月でない");
							}
							break;
						case PcaGoodsIDDefine.Matome48:
							if (48 != slip.利用期間.GetMonthCount())
							{
								slip.ErrorList.Add("おまとめプラン48ケ月の利用期間が48ヵ月でない");
							}
							break;
						case PcaGoodsIDDefine.Matome60:
							if (60 != slip.利用期間.GetMonthCount())
							{
								slip.ErrorList.Add("おまとめプラン60ケ月の利用期間が60ヵ月でない");
							}
							break;
					}
					//if (slip.受注日.HasValue && Date.MinValue != slip.利用期間.Start)
					//{
					//	if (6 < new Span(new Date(slip.受注日.Value), slip.利用期間.Start).GetMonthCount())
					//	{
					//		slip.ErrorList.Add("おまとめプランの利用開始日が受注日の半年以降");
					//	}
					//}
					if (slip.NoReplace)
					{
						slip.ErrorList.Add("リプレースなし");
					}
				}
				// 受注伝票リストビューの設定
				SetListViewSlip(list);

				return list.Count;
			}
			return 0;
		}

		/// <summary>
		/// PC安心サポート 伝票チェック
		/// </summary>
		/// <param name="checkDate">検索開始日</param>
		/// <returns>検索数</returns>
		/// Ver1.10 PC安心サポートPlus対応(2020/10/16 勝呂)
		private int CheckPcSupport(Date checkDate)
		{
			// チェック対象の商品の設定
			List<string> goods = new List<string>();
			goods.Add(PcaGoodsIDDefine.PcSupport3);
			goods.Add(PcaGoodsIDDefine.PcSupport1);

			// Ver1.10 PC安心サポートPlus対応(2020/10/16 勝呂)
			goods.Add(PcaGoodsIDDefine.PcSupportPlus3);
			goods.Add(PcaGoodsIDDefine.PcSupportPlus1);

			// 受注伝票情報リストの取得
			List<OrderSlipData> list = null;
			if (radioButtonOrder.Checked)
			{
				list = CheckOrderSlipAccess.GetOrderSlipList(checkDate, goods, Program.DATABASE_ACCESS_CT);
			}
			else if (radioButtonOrderAccept.Checked)
			{
				list = CheckOrderSlipAccess.GetOrderAcceptSlipList(checkDate, goods, Program.DATABASE_ACCESS_CT);
			}
			else
			{
				list = CheckOrderSlipAccess.GetSaleSlipList(checkDate, goods, Program.DATABASE_ACCESS_CT);
			}
			if (null != list)
			{
				foreach (OrderSlipData slip in list)
				{
					if (MwsDefine.ApplyType.PcSupport != slip.販売種別)
					{
						slip.ErrorList.Add("PC安心サポートの販売種別がPC安心でない");
					}
					switch (slip.商品コード)
					{
						case PcaGoodsIDDefine.PcSupport3:
							if (36 != slip.利用期間.GetMonthCount())
							{
								slip.ErrorList.Add("PC安心サポート３年契約の利用期間が36ヵ月でない");
							}
							break;
						case PcaGoodsIDDefine.PcSupport1:
							if (12 != slip.利用期間.GetMonthCount())
							{
								slip.ErrorList.Add("PC安心サポート１年契約の利用期間が12ヵ月でない");
							}
							break;
						// Ver1.10 PC安心サポートPlus対応(2020/10/16 勝呂)
						case PcaGoodsIDDefine.PcSupportPlus3:
							if (36 != slip.利用期間.GetMonthCount())
							{
								slip.ErrorList.Add("PC安心サポートPlus３年契約の利用期間が36ヵ月でない");
							}
							break;
						// Ver1.10 PC安心サポートPlus対応(2020/10/16 勝呂)
						case PcaGoodsIDDefine.PcSupportPlus1:
							if (12 != slip.利用期間.GetMonthCount())
							{
								slip.ErrorList.Add("PC安心サポートPlus１年契約の利用期間が12ヵ月でない");
							}
							break;
					}
					//if (slip.受注日.HasValue && Date.MinValue != slip.利用期間.Start)
					//{
					//	if (6 < new Span(new Date(slip.受注日.Value), slip.利用期間.Start).GetMonthCount())
					//	{
					//		slip.ErrorList.Add("PC安心サポートの利用開始日が受注日の半年以降");
					//	}
					//}
				}
				// 受注伝票リストビューの設定
				SetListViewSlip(list);

				return list.Count;
			}
			return 0;
		}

		/// <summary>
		/// MWSプラットフォーム利用料 伝票チェック
		/// </summary>
		/// <param name="checkDate">検索開始日</param>
		/// <returns>検索数</returns>
		private int CheckPlatform(Date checkDate)
		{
			// チェック対象の商品の設定
			List<string> goods = new List<string>();
			goods.Add(PcaGoodsIDDefine.MwsPlatform);

			// 受注伝票情報リストの取得
			List<OrderSlipData> list = CheckOrderSlipAccess.GetOrderSlipList(checkDate, goods, Program.DATABASE_ACCESS_CT);
			if (null != list)
			{
				List<OrderSlipData> listPlat = list.FindAll(p =>p.販売種別 == MwsDefine.ApplyType.Monthly);
				if (null != listPlat)
				{
					foreach (OrderSlipData slip in list)
					{
						if (MwsDefine.ApplyType.Monthly != slip.販売種別)
						{
							slip.ErrorList.Add("販売種別が月額課金でない");
						}
						if (slip.利用期間.IsNothing())
						{
							slip.ErrorList.Add("サービス利用期間が未設定");
						}
						else if (24 <= slip.利用期間.GetMonthCount())
						{
							slip.ErrorList.Add("サービス利用期間が２年以上");
						}
						if (slip.NoReplace)
						{
							slip.ErrorList.Add("リプレースなし");
						}
					}
					// 受注伝票リストビューの設定
					SetListViewSlip(listPlat);

					return listPlat.Count;
				}
			}
			return 0;
		}

		/// <summary>
		/// PC安心サポートPlus切替 伝票チェック
		/// </summary>
		/// <param name="checkDate">検索開始日</param>
		/// <returns>検索数</returns>
		/// Ver1.11 PC安心サポートPlus切替対応(2020/10/29 勝呂)
		private int CheckChangePcSupportPlus(Date checkDate)
		{
			// チェック対象の商品の設定
			List<string> goods = new List<string>();
			goods.Add(PcaGoodsIDDefine.CloudBackupPcSupport);

			// 受注伝票情報リストの取得
			List<OrderSlipData> list = null;
			if (radioButtonOrder.Checked)
			{
				list = CheckOrderSlipAccess.GetOrderSlipList(checkDate, goods, Program.DATABASE_ACCESS_CT);
			}
			else if (radioButtonOrderAccept.Checked)
			{
				list = CheckOrderSlipAccess.GetOrderAcceptSlipList(checkDate, goods, Program.DATABASE_ACCESS_CT);
			}
			else
			{
				list = CheckOrderSlipAccess.GetSaleSlipList(checkDate, goods, Program.DATABASE_ACCESS_CT);
			}
			if (null != list)
			{
				foreach (OrderSlipData slip in list)
				{
					if (MwsDefine.ApplyType.Monthly != slip.販売種別)
					{
						slip.ErrorList.Add("販売種別が月額課金でない");
					}
					if (800 != slip.標準価格)
					{
						slip.ErrorList.Add("価格が800円でない");
					}
					if (slip.Is受注承認 || slip.Is売上承認)
					{
						if (slip.利用期間.IsNothing())
						{
							slip.ErrorList.Add("サービス利用期間が未設定");
						}
						List<T_USE_PCCSUPPORT> pcList = CharlieDatabaseAccess.Select_T_USE_PCCSUPPORT(string.Format("[fCustomerID] = {0}", slip.顧客No), "", Program.DATABASE_ACCESS_CT);
						if (null != pcList)
						{
							T_USE_PCCSUPPORT pc = pcList.First();
							if (!pc.fContractEndDate.HasValue || pc.fContractEndDate.Value != slip.利用期間.End)
							{
								slip.ErrorList.Add("サービス利用期間の終了月がPC安心サポート契約情報の契約終了月と一致しない");
							}
						}
						else
						{
							slip.ErrorList.Add("PC安心サポート契約情報が登録されていない");
						}
					}
				}
				// 受注伝票リストビューの設定
				SetListViewSlip(list);

				return list.Count;
			}
			return 0;
		}
	}
}
