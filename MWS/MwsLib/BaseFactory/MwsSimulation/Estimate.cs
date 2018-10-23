//
// Estimate.cs
// 
// MIC WEB SERVICE 課金シミュレーション 見積書情報
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
// Ver1.050 見積書および注文書の宛先を「御中」と「様」を変更可能にする(2018/09/26 勝呂)
// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
//
using MwsLib.Common;
using MwsLib.DB.SQLite.MwsSimulation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MwsLib.BaseFactory.MwsSimulation
{
	/// <summary>
	/// 見積書情報
	/// </summary>
	[Serializable]
	public class Estimate : IEquatable<Estimate>
	{
		/// <summary>
		/// 申込み種別
		/// </summary>
		public enum ApplyType
		{
			/// <summary>月額課金</summary>
			Monthly = 0,
			/// <summary>まとめ契約なし</summary>
			MatomeNone,
			/// <summary>まとめ契約あり</summary>
			Matome,
		};

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
		// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
		public Span AgreeSpan { get; set; }

		/// <summary>
		/// 契約月数
		/// </summary>
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
		/// 宛先に御中ではなく様を使用
		/// </summary>
		// Ver1.050 見積書および注文書の宛先を「御中」と「様」を変更可能にする(2018/09/26 勝呂)
		public int NotUsedMessrs { get; set; }

		/// <summary>
		/// 申込み種別
		/// </summary>
		public ApplyType Apply { get; set; }

		/// <summary>
		/// 見積サービス情報リスト
		/// </summary>
		public List<EstimateService> ServiceList { get; set; }

		/// <summary>
		/// サービス使用料の取得
		/// </summary>
		public int GetPrice
		{
			get
			{
				int price = 0;
				foreach (EstimateService service in ServiceList)
				{
					if (null != service.GroupServiceList)
					{
						// おまとめプラン契約あり or セット割サービス
						price += service.Price;
					}
					else
					{
						// おまとめプラン契約なし or 月額課金
						price += (service.Price * AgreeMonthes);
					}
				}
				return price;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public Estimate()
		{
			EstimateID = 0;
			Destination = string.Empty;
			PrintDate = Date.Today;

			// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
			AgreeSpan = Span.Nothing;

			AgreeMonthes = 0;
			LimitDate = Date.MinValue;
			Remark = new List<string>();
			ServiceList = new List<EstimateService>();

			// Ver1.050 見積書および注文書の宛先を「御中」と「様」を変更可能にする(2018/09/26 勝呂)
			NotUsedMessrs = 0;

			Apply = ApplyType.Monthly;
		}

		/// <summary>
		/// 備考の取得
		/// </summary>
		/// <returns>備考</returns>
		public string[] GetRemark()
		{
			return Remark.ToArray();
		}

		/// <summary>
		/// 備考の設定
		/// </summary>
		/// <param name="remark">備考</param>
		public void SetRemark(string[] remark)
		{
			Remark.Clear();
			Remark.AddRange(remark);
		}

		/// <summary>
		/// 宛先の御中または様を取得
		/// </summary>
		/// <param name="notUsedMessrs">宛先に御中ではなく様を使用</param>
		/// <returns>宛先の御中または様</returns>
		// Ver1.050 見積書および注文書の宛先を「御中」と「様」を変更可能にする(2018/09/26 勝呂)
		public static string DestinationTitle(int notUsedMessrs)
		{
			return (0 == notUsedMessrs) ? "御中" : "様";
		}

		/// <summary>
		/// 宛先文字列の取得
		/// </summary>
		/// <returns>宛先文字列</returns>
		// Ver1.050 見積書および注文書の宛先を「御中」と「様」を変更可能にする(2018/09/26 勝呂)
		public string DestinationDispString()
		{
			return string.Format("{0} {1}", Destination, Estimate.DestinationTitle(NotUsedMessrs));
		}

		/// <summary>
		/// 発行日と契約月数から契約期間を取得
		/// まとめ：発行日の翌々月初日
		/// 月額：発行日の翌月初日
		/// </summary>
		/// <param name="matome">まとめ契約かどうか？</param>
		/// <param name="printDate">発行日</param>
		/// <param name="monthes">契約月数</param>
		/// <returns>契約期間</returns>
		// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
		public static Span GetAgreeSapn(bool matome, Date printDate, int monthes)
		{
			if (matome)
			{
				// まとめ契約
				Date startDate = printDate.PlusMonths(2);
				Date endDate = startDate.PlusMonths(monthes - 1);
				return new Span(new Date(startDate.Year, startDate.Month, 1), new Date(endDate.Year, endDate.Month, endDate.ToYearMonth().GetDays()));
			}
			// 月額課金
			Date date = printDate.PlusMonths(1);
			return new Span(new Date(date.Year, date.Month, 1), new Date(date.Year, date.Month, date.ToYearMonth().GetDays()));
		}

		/// <summary>
		/// 契約開始日と契約月数から契約終了日を取得
		/// </summary>
		/// <param name="startDate">契約開始日</param>
		/// <param name="monthes">契約月数</param>
		/// <returns>契約終了日</returns>
		public static Date GetAgreeEndDate(Date startDate, int monthes)
		{
			Date endDate = startDate.PlusMonths(monthes - 1);
			return new Date(endDate.Year, endDate.Month, endDate.ToYearMonth().GetDays());
		}

		/// <summary>
		/// 発行日に対する有効期限を取得
		/// </summary>
		/// <param name="printDate">発行日</param>
		/// <returns>有効期限</returns>
		public static Date GetLimitDate(Date printDate)
		{
			return printDate + 13;
		}

		/// <summary>
		/// 見積書情報の設定
		/// </summary>
		/// <param name="serviceList">申込サービス情報リスト</param>
		/// <param name="groupList">おまとめプラン・セット割サービスリスト</param>
		/// <param name="chartComputeCode">電子カルテ標準サービスサービスコード</param>
		/// <param name="tabletViewerCode">TABLETビューワサービスコード</param>
		/// <param name="platform">プラットフォーム利用料</param>
		// Ver1.050 電子カルテ標準サービス選択時にはTABLETビューワのサービス利用料の500円は加算しない(2018/09/26 勝呂)
		public void SetEstimateData(List<ServiceInfo> serviceList, List<GroupService> groupList, string chartComputeCode, string tabletViewerCode, ServiceInfo platform = null)
		{
			this.ServiceList.Clear();

			// おまとめプラン及びセット割サービスの設定
			foreach (GroupService group in groupList)
			{
				EstimateService estSvr = new EstimateService();
				estSvr.GoodsID = group.GoodsID;
				estSvr.ServiceName = group.GoodsName;
				estSvr.Price = group.Price;
				estSvr.Mode = group.Mode;
				if (SQLiteMwsSimulationDef.ServiceMode.None != group.Mode)
				{
					estSvr.GroupServiceList = group.ServiceCodeList;
				}
				this.ServiceList.Add(estSvr);
			}
			// 電子カルテ標準サービスとTABLETビューワの存在確認
			// Ver1.050 電子カルテ標準サービス選択時にはTABLETビューワのサービス利用料の500円は加算しない(2018/09/26 勝呂)
			bool existChartCompute = false;
			bool existTabletViewer = false;
			foreach (ServiceInfo service in serviceList)
			{
				if (chartComputeCode == service.ServiceCode)
				{
					existChartCompute = true;
				}
				else if (tabletViewerCode == service.ServiceCode)
				{
					existTabletViewer = true;
				}
			}
			// サービスの設定
			foreach (ServiceInfo service in serviceList)
			{
				EstimateService estSvr = new EstimateService();
				estSvr.GoodsID = service.GoodsID;
				estSvr.ServiceName = service.ServiceName;

				// Ver1.050 電子カルテ標準サービス選択時にはTABLETビューワのサービス利用料の500円は加算しない(2018/09/26 勝呂)
				if (tabletViewerCode == service.ServiceCode && true == existChartCompute && true == existTabletViewer)
				{
					estSvr.Price = 0;
				}
				else
				{
					estSvr.Price = service.Price;
				}
				this.ServiceList.Add(estSvr);
			}
			if (null != platform)
			{
				// MIC WEB SERVICE標準サービスを先頭に格納
				EstimateService estSvr = new EstimateService();
				estSvr.GoodsID = platform.GoodsID;
				estSvr.ServiceName = platform.ServiceName;
				estSvr.Price = platform.Price;
				this.ServiceList.Insert(0, estSvr);
			}
		}

		/// <summary>
		/// 同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するEstimate</param>
		/// <returns>判定</returns>
		public bool Equals(Estimate other)
		{
			if (null != other)
			{
				if (EstimateID != other.EstimateID)
					return false;
				if (Destination != other.Destination)
					return false;
				if (PrintDate != other.PrintDate)
					return false;
				// Ver1.050 契約終了日の変更可能に対応(2018/09/27 勝呂)
				if (AgreeSpan != other.AgreeSpan)
					return false;
				if (AgreeMonthes != other.AgreeMonthes)
					return false;
				if (LimitDate != other.LimitDate)
					return false;
				if (false == Remark.SequenceEqual(other.Remark))
					return false;
				// Ver1.050 見積書および注文書の宛先を「御中」と「様」を変更可能にする(2018/09/26 勝呂)
				if (NotUsedMessrs != other.NotUsedMessrs)
					return false;
				if (Apply != other.Apply)
					return false;
				return true;
				if (false == ServiceList.SequenceEqual(other.ServiceList))
					return false;
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するEstimateオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is Estimate)
			{
				return this.Equals((Estimate)obj);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		/// <summary>
		/// ハッシュコードを返す
		/// </summary>
		/// <returns>ハッシュコード</returns>
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		/// <summary>
		/// 出力レコードの取得
		/// </summary>
		/// <returns>出力レコード</returns>
		public override string ToString()
		{
			return EstimateID.ToString() + Destination;
		}
	}

	/// <summary>
	/// 見積サービス情報
	/// </summary>
	[Serializable]
	public class EstimateService : IEquatable<EstimateService>
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
		/// おまとめプラン・セット割サービス情報
		/// </summary>
		public List<Tuple<string, string>> GroupServiceList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public EstimateService()
		{
			GoodsID = string.Empty;
			ServiceName = string.Empty;
			Price = 0;
			Mode = SQLiteMwsSimulationDef.ServiceMode.None;
			GroupServiceList = null;
		}

		/// <summary>
		/// 同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するEstimateService</param>
		/// <returns>判定</returns>
		public bool Equals(EstimateService other)
		{
			if (null != other)
			{
				if (GoodsID != other.GoodsID)
					return false;
				if (ServiceName != other.ServiceName)
					return false;
				if (Price != other.Price)
					return false;
				if (Mode != other.Mode)
					return false;
				if (null != GroupServiceList && null != other.GroupServiceList)
				{
					if (false == GroupServiceList.SequenceEqual(other.GroupServiceList))
						return false;
				}
				else
				{
					return false;
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するEstimateServiceオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is EstimateService)
			{
				return this.Equals((EstimateService)obj);
			}
			else
			{
				return base.Equals(obj);
			}
		}

		/// <summary>
		/// ハッシュコードを返す
		/// </summary>
		/// <returns>ハッシュコード</returns>
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		/// <summary>
		/// 出力レコードの取得
		/// </summary>
		/// <returns>出力レコード</returns>
		public override string ToString()
		{
			return GoodsID + ServiceName + Price.ToString() + Mode.ToString();
		}
	}
}
