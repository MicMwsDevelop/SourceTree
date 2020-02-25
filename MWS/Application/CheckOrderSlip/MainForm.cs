//
// MainForm.cs
//
// paletteES 起票不備連絡ツール メイン画面クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/11/15 勝呂)
// 
using MwsLib.BaseFactory.Junp;
using MwsLib.Common;
using MwsLib.DB.SqlServer.OrderSlip;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MwsLib.BaseFactory;

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
			listViewES.Items.Clear();
			listBoxError.Items.Clear();

			// 検索日
			Date checkDate = new Date(dateTimePickerSearchDate.Value.Year, dateTimePickerSearchDate.Value.Month, dateTimePickerSearchDate.Value.Day);

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
						if (72 == es.利用期間.GetMonthCount())
						{
							es.ErrorList.Add("paletteESの利用期間が72ヵ月でない");
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
								if (72 == mainte72.利用期間.GetMonthCount())
								{
									mainte72.ErrorList.Add("ｿﾌﾄｳｪｱ保守料72ケ月の利用期間が72ヵ月でない");
								}
								if (es.利用期間.Start != mainte72.利用期間.Start)
								{
									mainte72.ErrorList.Add("paletteESの利用開始日とｿﾌﾄｳｪｱ保守料の利用開始日が違う");
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
									if (12 == mainte12.利用期間.GetMonthCount())
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
						}
					}
				}
				foreach (OrderSlipData data in list)
				{
					ListViewItem item = new ListViewItem(data.GetListViewItem());
					item.Tag = data;
					listViewES.Items.Add(item);
				}
			}
			else
			{
				// paletteESの伝票が存在しない
				;
			}
			MessageBox.Show("確認終了");
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
					OrderSlipData es = lvItem.Tag as OrderSlipData;
					if (0 < es.ErrorList.Count)
					{
						listBoxError.Items.AddRange(es.ErrorList.ToArray());
					}
				}
			}
		}

		/// <summary>
		/// ログ出力
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOutputLog_Click(object sender, EventArgs e)
		{
			//if (0 < listViewES.Items.Count)
			//{
			//	CheckVoucherPaletteESLogger.LogStart();
			//	foreach (ListViewItem lvItem in listViewES.Items)
			//	{
			//		OrderVoucher es = lvItem.Tag as OrderVoucher;
			//		CheckVoucherPaletteESLogger.MainLine(es.GetLogString());
			//		if (0 < es.ErrorList.Count)
			//		{
			//			CheckVoucherPaletteESLogger.SubLine(es.ErrorList);
			//		}
			//	}
			//	CheckVoucherPaletteESLogger.LogEnd();

			//	MessageBox.Show("ログを出力しました。");
			//}
		}
	}
}
