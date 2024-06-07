//
// SetPlan.cs
// 
// MIC WEB SERVICE 課金シミュレーション セット割サービス情報
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
//
using CommonLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLib.BaseFactory.MwsSimulation
{
	/// <summary>
	/// セット割サービス情報
	/// </summary>
	public class SetPlan : IEquatable<SetPlan>
	{
		/// <summary>
		/// 商品ID
		/// </summary>
		public string GoodsID { get; set; }

		/// <summary>
		/// 商品名
		/// </summary>
		public string GoodsName { get; set; }

		/// <summary>
		/// 価格
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// サービス情報リスト（商品コード、サービス名）
		/// </summary>
		public List<Tuple<string, string>> ServiceList;

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public SetPlan()
		{
			GoodsID = string.Empty;
			GoodsName = string.Empty;
			Price = 0;
			ServiceList = null;
		}

		/// <summary>
		/// 表示情報の取得 
		/// </summary>
		/// <returns>表示情報</returns>
		public string[] GetListViewData()
		{
			string[] array = new string[2];
			array[0] = GoodsName;
			array[1] = "\\" + StringUtil.CommaEdit(Price);
			return array;
		}

		/// <summary>
		/// 同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するSetPlan</param>
		/// <returns>判定</returns>
		public bool Equals(SetPlan other)
		{
			if (null != other)
			{
				if (GoodsID != other.GoodsID)
					return false;
				if (GoodsName != other.GoodsName)
					return false;
				if (Price != other.Price)
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
		/// <param name="obj">比較するSetPlanオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is SetPlan)
			{
				return this.Equals((SetPlan)obj);
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
			return GoodsID + GoodsName + Price.ToString();
		}
	}
}
