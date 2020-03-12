//
// MainForm.cs
//
// 受注伝票チェックツール メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/03/02 勝呂)
// 
using ClosedXML.Excel;
using MwsLib.BaseFactory;
using MwsLib.BaseFactory.Junp;
using MwsLib.Common;
using MwsLib.DB.SqlServer.OrderSlip;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CheckOrderSlip
{
	public partial class MainForm : Form
	{
		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public MainForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Form Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
			// 検索日に
			Date firstDate = new Date(Date.Today.Year, Date.Today.Month, 1);
			dateTimePickerSearchDate.Value = firstDate.ToDateTime();
		}

		/// <summary>
		/// 起票確認
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExec_Click(object sender, EventArgs e)
		{
			// 元のカーソルを保持
			Cursor preCursor = Cursor.Current;

			// カーソルを待機カーソルに変更
			Cursor.Current = Cursors.WaitCursor;

			listViewES.Items.Clear();
			listBoxError.Items.Clear();

			// 検索日
			Date checkDate = new Date(dateTimePickerSearchDate.Value.Year, dateTimePickerSearchDate.Value.Month, dateTimePickerSearchDate.Value.Day);

			// チェック対象の商品の設定
			List<string> goods = new List<string>();
			goods.Add(PcaGoodsIDDefine.PaletteES_2019);
			goods.Add(PcaGoodsIDDefine.PaletteES_Mainte72);
			goods.Add(PcaGoodsIDDefine.PaletteES_Mainte12);
			goods.Add(PcaGoodsIDDefine.PcSafetySupport3);
			goods.Add(PcaGoodsIDDefine.PcSafetySupport1);
			goods.Add(PcaGoodsIDDefine.Matome12);
			goods.Add(PcaGoodsIDDefine.Matome24);
			goods.Add(PcaGoodsIDDefine.Matome36);
			goods.Add(PcaGoodsIDDefine.Matome48);
			goods.Add(PcaGoodsIDDefine.Matome60);

			try
			{
				// 受注伝票情報リストの取得
				List<OrderSlipData> list = OrderSlipAccess.GetOrderSlipList(checkDate, goods, Program.DATABASE_ACCESS_CT);
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
							if (es.受注日.HasValue && Date.MinValue != es.利用期間.Start)
							{
								if (6 < new Span(new Date(es.受注日.Value), es.利用期間.Start).GetMonthCount())
								{
									es.ErrorList.Add("paletteESの利用開始日が受注日の半年以降");
								}
							}
							// 同伝票にｿﾌﾄｳｪｱ保守料72ケ月が存在するか？
							OrderSlipData mainte72 = OrderSlipData.GetSameMainte72(list, es);
							if (null == mainte72)
							{
								// 別伝票にｿﾌﾄｳｪｱ保守料72ケ月が存在するか？
								mainte72 = OrderSlipData.GetAnotherMainte72(list, es);
								if (null != mainte72)
								{
									// ｿﾌﾄｳｪｱ保守料72ケ月の販売種別が月額課金かどうか？
									if (MwsDefine.ApplyType.Monthly != mainte72.販売種別)
									{
										mainte72.ErrorList.Add("ｿﾌﾄｳｪｱ保守料72ケ月の販売種別が月額課金でない");
									}
									// ｿﾌﾄｳｪｱ保守料72ケ月の利用期間が72ヵ月かどうか？
									if (72 != mainte72.利用期間.GetMonthCount())
									{
										mainte72.ErrorList.Add("ｿﾌﾄｳｪｱ保守料72ケ月の利用期間が72ヵ月でない");
									}
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
										// ｿﾌﾄｳｪｱ保守料12ケ月の販売種別が月額課金かどうか？
										if (MwsDefine.ApplyType.Monthly != mainte12.販売種別)
										{
											mainte12.ErrorList.Add("ｿﾌﾄｳｪｱ保守料12ケ月の販売種別が月額課金でない");
										}
										// ｿﾌﾄｳｪｱ保守料12ケ月の利用期間が12ヵ月かどうか？
										if (12 != mainte12.利用期間.GetMonthCount())
										{
											mainte12.ErrorList.Add("ｿﾌﾄｳｪｱ保守料12ケ月の利用期間が12ヵ月でない");
										}
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
						}
					}
					// PC安心サポートのみ抽出
					List<OrderSlipData> listPC = OrderSlipData.SelectPcSupport(list);
					if (null != listPC)
					{
						foreach (OrderSlipData slip in listPC)
						{
							if (MwsDefine.ApplyType.PcSupport != slip.販売種別)
							{
								slip.ErrorList.Add("PC安心サポートの販売種別がPC安心でない");
							}
							switch (slip.商品コード)
							{
								case PcaGoodsIDDefine.PcSafetySupport3:
									if (36 != slip.利用期間.GetMonthCount())
									{
										slip.ErrorList.Add("PC安心サポート３年契約の利用期間が36ヵ月でない");
									}
									break;
								case PcaGoodsIDDefine.PcSafetySupport1:
									if (12 != slip.利用期間.GetMonthCount())
									{
										slip.ErrorList.Add("PC安心サポート１年契約の利用期間が12ヵ月でない");
									}
									break;
							}
							if (slip.受注日.HasValue && Date.MinValue != slip.利用期間.Start)
							{
								if (6 < new Span(new Date(slip.受注日.Value), slip.利用期間.Start).GetMonthCount())
								{
									slip.ErrorList.Add("PC安心サポートの利用開始日が受注日の半年以降");
								}
							}
						}
					}
					// おまとめプランのみ抽出
					List<OrderSlipData> listMatome = OrderSlipData.SelectMatome(list);
					if (null != listMatome)
					{
						foreach (OrderSlipData slip in listMatome)
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
							if (slip.受注日.HasValue && Date.MinValue != slip.利用期間.Start)
							{
								if (6 < new Span(new Date(slip.受注日.Value), slip.利用期間.Start).GetMonthCount())
								{
									slip.ErrorList.Add("おまとめプランの利用開始日が受注日の半年以降");
								}
							}
						}
					}
					foreach (OrderSlipData data in list)
					{
						if (radioButtonJuchu.Checked)
						{
							if (data.Is受注承認)
							{
								ListViewItem item = new ListViewItem(data.GetListViewItem());
								item.Tag = data;
								listViewES.Items.Add(item);
							}
						}
						else if (radioButtonUriage.Checked)
						{
							if (data.Is売上承認)
							{
								ListViewItem item = new ListViewItem(data.GetListViewItem());
								item.Tag = data;
								listViewES.Items.Add(item);
							}
						}
						else
						{
							ListViewItem item = new ListViewItem(data.GetListViewItem());
							item.Tag = data;
							listViewES.Items.Add(item);
						}
					}
				}
				else
				{
					// paletteESの伝票が存在しない
					;
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
		/// palette ESのエラー情報の表示
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void listViewES_SelectedIndexChanged(object sender, EventArgs e)
		{
			listBoxError.Items.Clear();

			if(0 < listViewES.SelectedIndices.Count)
			{
				ListViewItem lvItem = listViewES.SelectedItems[0];
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
		/// EXCEL出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonExcel_Click(object sender, EventArgs e)
		{
			if (0 < listViewES.Items.Count)
			{
				var workbook = new XLWorkbook();
				var worksheet = workbook.Worksheets.Add("Sheet");

				string[] titles = OrderSlipData.GetExcelTitle();
				for (int i = 0; i < titles.Length; i++)
				{
					worksheet.Cell(1, i + 1).Value = titles[i];
				}
				for (int i = 0; i < listViewES.Items.Count; i++)
				{
					OrderSlipData slip = listViewES.Items[i].Tag as OrderSlipData;
					List<string> row = slip.GetExcelRow();
					for (int j = 0; j < row.Count; j++)
					{
						worksheet.Cell(i + 2, j + 1).Style.NumberFormat.Format = "@";
						worksheet.Cell(i + 2, j + 1).Value = row[j];
					}
				}
				try
				{
					workbook.SaveAs("受注伝票チェック内容.xlsx");
					MessageBox.Show("EXCELファイルを出力しました。", "出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "ワーニング", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
		}

		/// <summary>
		/// 全て
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonAll_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonAll.Checked)
			{
				buttonExec.PerformClick();
			}
		}

		/// <summary>
		/// 受注承認済
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonJuchu_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonJuchu.Checked)
			{
				buttonExec.PerformClick();
			}
		}

		/// <summary>
		/// 売上承認済
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void radioButtonUriage_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonUriage.Checked)
			{
				buttonExec.PerformClick();
			}
		}
	}
}
