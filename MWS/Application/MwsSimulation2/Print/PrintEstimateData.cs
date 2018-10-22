//
// MIC WEB SERVICE見積書・注文書/注文請書印刷情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
// Ver1.050 見積書および注文書の宛先を「御中」と「様」を変更可能にする(2018/09/26 勝呂)
// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
// 
using MwsLib.BaseFactory.MwsSimulation;
using MwsLib.Common;
using MwsLib.DB.SQLite.MwsSimulation;
using System;
using System.Collections.Generic;

namespace MwsSimulation.Print
{
	/// <summary>
	/// MIC WEB SERVICE見積書・注文書/注文請書印刷情報
	/// </summary>
	public class PrintEstimateData
	{
		/// <summary>
		/// 見積書番号
		/// </summary>
		public int EstimateID { get; set; }

		/// <summary>
		/// 宛先
		/// </summary>
		public string Destination { get; set; }

		/// <summary>
		/// 発行日
		/// </summary>
		public Date PrintDate { get; set; }

		/// <summary>
		/// 契約期間
		/// </summary>
		public Span AgreeSpan { get; set; }

		/// <summary>
		/// 契約月数
		/// </summary>
		// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
		public int AgreeMonthes { get; set; }

		/// <summary>
		/// 有効期限
		/// </summary>
		public Date LimitDate { get; set; }

		/// <summary>
		/// 備考
		/// </summary>
		public List<string> Remark { get; set; }

		/// <summary>
		/// 宛先に様を使用
		/// </summary>
		// Ver1.050 見積書および注文書の宛先を「御中」と「様」を変更可能にする(2018/09/26 勝呂)
		public int NotUsedMessrs { get; set; }

		/// <summary>
		/// 申込み種別
		/// </summary>
		public Estimate.ApplyType Apply { get; set; }

		/// <summary>
		/// 見積ページ情報リスト
		/// </summary>
		List<List<PrintEstimateLine>> PageList { get; set; }

		/// <summary>
		/// 印刷枚数の取得
		/// </summary>
		/// <returns>印刷枚数</returns>
		public int GetMaxPage
		{
			get
			{
				return PageList.Count;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public PrintEstimateData()
		{
			EstimateID = 0;
			Destination = string.Empty;
			PrintDate = Date.Today;
			Remark = new List<string>();
			PageList = new List<List<PrintEstimateLine>>();

			// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
			AgreeSpan = Span.Nothing;
			AgreeMonthes = 0;

			// Ver1.050 見積書および注文書の宛先を「御中」と「様」を変更可能にする(2018/09/26 勝呂)
			NotUsedMessrs = 0;

			Apply = Estimate.ApplyType.Monthly;
			LimitDate = Date.MinValue;
		}

		/// <summary>
		/// 見積ページ情報の設定
		/// </summary>
		/// <param name="est">見積書情報</param>
		/// <returns>見積ページ情報数</returns>
		public int SetEstimateData(Estimate est)
		{
			PageList.Clear();

			EstimateID = est.EstimateID;
			Destination = est.Destination;
			PrintDate = est.PrintDate;

			// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
			//Date endDate = est.AgreeStartDate.PlusMonths(est.AgreeMonthes - 1);
			AgreeSpan = est.AgreeSpan;
			AgreeMonthes = est.AgreeMonthes;

			// Ver1.050 見積書および注文書の宛先を「御中」と「様」を変更可能にする(2018/09/26 勝呂)
			NotUsedMessrs = est.NotUsedMessrs;

			Remark = est.Remark;

			Apply = est.Apply;
			LimitDate = est.LimitDate;

			if (0 < est.ServiceList.Count)
			{
				List<PrintEstimateLine> page = new List<PrintEstimateLine>();
				PageList.Add(page);
				int line = 0;
				foreach (EstimateService sv in est.ServiceList)
				{
					PrintEstimateLine order = new PrintEstimateLine();
					order.GoodsID = sv.GoodsID;
					order.ServiceName = sv.ServiceName;
					order.Price = sv.Price;
					order.Mode = sv.Mode;
					if (PrintEstimateDef.PRINT_SERVICE_COUNT < line + 1)
					{
						page = new List<PrintEstimateLine>();
						PageList.Add(page);
						line = 0;
					}
					line++;
					page.Add(order);
					if (SQLiteMwsSimulationDef.ServiceMode.None != sv.Mode)
					{
						// おまとめプラン or セット割サービス
						foreach (Tuple<string, string> child in sv.GroupServiceList)
						{
							order = new PrintEstimateLine();
							order.GoodsID = child.Item1;
							order.ServiceName = "    " + child.Item2;
							order.Mode = sv.Mode;
							order.ChildServide = true;
							if (PrintEstimateDef.PRINT_SERVICE_COUNT < line + 1)
							{
								page = new List<PrintEstimateLine>();
								PageList.Add(page);
								line = 0;
							}
							line++;
							page.Add(order);
						}
					}
				}
			}
			return PageList.Count;
		}

		/// <summary>
		/// ページと行に対応する見積書印刷行情報の取得
		/// </summary>
		/// <param name="curPage">カレントページ</param>
		/// <param name="curLine">カレント行</param>
		/// <returns>見積書印刷行情報</returns>
		public PrintEstimateLine GetPrintEstimateLine(int curPage, int curLine)
		{
			List<PrintEstimateLine> page = PageList[curPage - 1];
			if (null != page)
			{
				if (curLine - 1 < page.Count)
				{
					return page[curLine - 1];
				}
			}
			return null;
		}
	}

	/// <summary>
	/// 見積書印刷行情報
	/// </summary>
	public class PrintEstimateLine
	{
		/// <summary>
		/// 商品ＩＤ
		/// </summary>
		public string GoodsID { get; set; }

		/// <summary>
		/// サービス名称
		/// </summary>
		public string ServiceName { get; set; }

		/// <summary>
		/// 価格
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// おまとめプラン・セット割サービス種別
		/// </summary>
		public SQLiteMwsSimulationDef.ServiceMode Mode { get; set; }

		/// <summary>
		/// おまとめプラン or セット割サービスの子処置
		/// </summary>
		public bool ChildServide { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PrintEstimateLine()
		{
			GoodsID = string.Empty;
			ServiceName = string.Empty;
			Price = 0;
			Mode = SQLiteMwsSimulationDef.ServiceMode.None;
			ChildServide = false;
		}
	}
}
