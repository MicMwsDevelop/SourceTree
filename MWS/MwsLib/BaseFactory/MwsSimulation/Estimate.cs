//
// Estimate.cs
// 
// MIC WEB SERVICE 課金シミュレーション 見積書情報
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
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
	public class Estimate : IEquatable<Estimate>
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
		/// 契約開始日
		/// </summary>
		public Date AgreeStartDate { get; set; }

		/// <summary>
		/// 契約月数
		/// </summary>
		public int AgreeMonthes { get; set; }

		/// <summary>
		/// 備考
		/// </summary>
		public List<string> Remark { get; set; }

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
					price += service.Price;
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
			AgreeStartDate = Date.MinValue;
			AgreeMonthes = 1;
			Remark = new List<string>();
			ServiceList = new List<EstimateService>();
		}

		/// <summary>
		/// 備考の取得
		/// </summary>
		/// <returns></returns>
		public string[] GetRemark()
		{
			return Remark.ToArray();
		}

		/// <summary>
		/// 備考の設定
		/// </summary>
		/// <param name="remark"></param>
		public void SetRemark(string[] remark)
		{
			Remark.Clear();
			Remark.AddRange(remark);
		}

		/// <summary>
		/// 見積書情報の設定
		/// </summary>
		/// <param name="serviceList">申込サービス情報リスト</param>
		/// <param name="groupList">おまとめプラン・セット割サービスリスト</param>
		public void SetEstimateData(List<ServiceInfo> serviceList, List<GroupService> groupList)
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
			// サービスの設定
			foreach (ServiceInfo service in serviceList)
			{
				EstimateService estSvr = new EstimateService();
				estSvr.GoodsID = service.GoodsID;
				estSvr.ServiceName = service.ServiceName;
				estSvr.Price = service.Price;
				this.ServiceList.Add(estSvr);
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
				if (AgreeStartDate != other.AgreeStartDate)
					return false;
				if (AgreeMonthes != other.AgreeMonthes)
					return false;
				if (false == Remark.SequenceEqual(other.Remark))
					return false;
				if (false == ServiceList.SequenceEqual(other.ServiceList))
					return false;
				return true;
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
