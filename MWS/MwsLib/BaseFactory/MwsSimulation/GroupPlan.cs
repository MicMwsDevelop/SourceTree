//
// GroupPlan.cs
// 
// MIC WEB SERVICE 課金シミュレーション おまとめプラン情報
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2018/08/01 勝呂)
//
using System;
using System.Collections.Generic;
using System.Linq;

namespace MwsLib.BaseFactory.MwsSimulation
{

	/// <summary>
	/// おまとめプラン情報
	/// </summary>
	public class GroupPlan : IEquatable<GroupPlan>
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
		/// 契約月数
		/// </summary>
		public int KeiyakuMonth { get; set; }

		/// <summary>
		/// 無償月数
		/// </summary>
		public int FreeMonth { get; set; }

		/// <summary>
		/// 下限金額
		/// </summary>
		public int MinAmmount { get; set; }

		/// <summary>
		/// 上限金額
		/// </summary>
		public int? MaxAmmount { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public GroupPlan()
		{
			GoodsID = string.Empty;
			GoodsName = string.Empty;
			KeiyakuMonth = 0;
			FreeMonth = 0;
			MinAmmount = 0;
			MaxAmmount = null;
		}

		/// <summary>
		/// おまとめプラン月額料金の取得
		/// </summary>
		/// <param name="price">おまとめプラン対象利用料</param>
		/// <returns>おまとめプラン月額料金</returns>
		public int GetGroupPlanPrice(int price)
		{
			return (price * (KeiyakuMonth - FreeMonth)) / KeiyakuMonth;
		}

		/// <summary>
		/// おまとめプラン料金の取得
		/// </summary>
		/// <param name="standardPrice">MWS WEB SERVICE プラットフォーム利用料</param>
		/// <param name="price">おまとめプラン対象利用料</param>
		/// <returns>おまとめプラン料金</returns>
		public int GetGroupPlanTotalPrice(int standardPrice, int price)
		{
			return (standardPrice * KeiyakuMonth) + (price * (KeiyakuMonth - FreeMonth));
		}

		/// <summary>
		/// 同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するGroupPlan</param>
		/// <returns>判定</returns>
		public bool Equals(GroupPlan other)
		{
			if (null != other)
			{
				if (GoodsID != other.GoodsID)
					return false;
				if (GoodsName != other.GoodsName)
					return false;
				if (KeiyakuMonth != other.KeiyakuMonth)
					return false;
				if (FreeMonth != other.FreeMonth)
					return false;
				if (MinAmmount != other.MinAmmount)
					return false;
				if (MaxAmmount != other.MaxAmmount)
					return false;
				return true;
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するGroupPlanオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is GroupPlan)
			{
				return this.Equals((GroupPlan)obj);
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
			return GoodsID + GoodsName + KeiyakuMonth.ToString() + FreeMonth.ToString() + MinAmmount.ToString() + MaxAmmount.ToString();
		}
	}

	/// <summary>
	/// おまとめプラン情報リスト
	/// </summary>
	public class GroupPlanList : List<GroupPlan>, IEquatable<GroupPlanList>
	{
		public GroupPlanList()
		{
		}

		/// <summary>
		/// おまとめプランの中で下限金額の最小値の取得
		/// </summary>
		/// <returns>おまとめプランの中で下限金額の最小値</returns>
		public int GetMinAmmount()
		{
			int ret = 0;
			foreach (GroupPlan plan in this)
			{
				if (0 == ret)
				{
					ret = plan.MinAmmount;
				} else if (plan.MinAmmount < ret)
				{
					ret = plan.MinAmmount;
				}
			}
			return ret;
		}

		/// <summary>
		/// 商品IDに対する契約月数を取得
		/// </summary>
		/// <param name="goodsID">商品ID</param>
		/// <returns>契約月数</returns>
		public int GetKeiyakuMonth(string goodsID)
		{
			foreach (GroupPlan plan in this)
			{
				if (plan.GoodsID == goodsID)
				{
					return plan.KeiyakuMonth;
				}
			}
			return 0;
		}

		/// <summary>
		/// おまとめプランの中で契約月数と金額に合致したおまとめプランを取得
		/// </summary>
		/// <param name="month"></param>
		/// <param name="price"></param>
		/// <returns>おまとめプラン情報</returns>
		public GroupPlan GetMachGroupPlan(int month, int price)
		{
			foreach (GroupPlan plan in this)
			{
				if (month == plan.KeiyakuMonth)
				{
					if (plan.MinAmmount <= price)
					{
						if (false == plan.MaxAmmount.HasValue || price <= plan.MaxAmmount.Value)
						{
							return plan;
						}
					}
				}
			}
			return null;
		}

		/// <summary>
		/// 通常のサービス利用料の取得
		/// </summary>
		/// <param name="month">契約月数</param>
		/// <param name="standardPrice">MWS WEB SERVICE プラットフォーム利用料</param>
		/// <param name="normalPrice">通常のサービス利用料</param>
		/// <returns>通常のサービス利用料</returns>
		public static int GetNormalTotalPrice(int month, int standardPrice, int normalPrice)
		{
			return (standardPrice + normalPrice) * month;
		}

		/// <summary>
		/// 無償利用期間の取得
		/// </summary>
		/// <param name="month">契約月数</param>
		/// <param name="standardPrice">MWS WEB SERVICE プラットフォーム利用料</param>
		/// <param name="normalPrice">通常のサービス利用料</param>
		/// <param name="groupPrice">おまとめプランサービス利用料</param>
		/// <returns>無償利用期間</returns>
		public static int GetGroupPlanMonthlyFreePrice(int month, int standardPrice, int normalPrice, int groupPrice)
		{
			return (GetNormalTotalPrice(month, standardPrice, normalPrice) - groupPrice) / month;
		}

		/// <summary>
		/// 指定されたGroupPlanListと内容が並び順も含めて同じかどうかを返す
		/// </summary>
		/// <param name="other">比較するGroupPlanList</param>
		/// <returns> 指定されたGroupPlanListと内容が並び順も含めて同じ場合はtrue、それ以外の場合はfalseを返す</returns>
		public bool Equals(GroupPlanList other)
		{
			if (this.SequenceEqual(other))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 指定されたGroupPlanListと内容が並び順も含めて同じかどうかを返す
		/// </summary>
		/// <param name="other">比較するGroupPlanList</param>
		/// <returns> 指定されたGroupPlanListと内容が並び順も含めて同じ場合はtrue、それ以外の場合はfalseを返す</returns>
		public override bool Equals(Object other)
		{
			if (other is GroupPlanList)
			{
				return this.Equals((GroupPlanList)other);
			}
			else
			{
				return base.Equals(other);
			}
		}

		/// <summary>
		///  ハッシュコードを返す
		/// </summary>
		/// <returns>ハッシュコード</returns>
		public override int GetHashCode()
		{
			int hashCode = this.Count.GetHashCode();
			foreach (var x in this)
			{
				hashCode ^= x.GetHashCode();
			}
			return hashCode;
		}

		/// <summary>
		/// 指定された契約期間が存在するかどうか？
		/// </summary>
		/// <param name="month"></param>
		/// <returns></returns>
		public bool IsExistKeiyakuMonth(int month)
		{
			foreach (GroupPlan plan in this)
			{
				if (month == plan.KeiyakuMonth)
				{
					return true;
				}
			}
			return false;
		}
	}


	/// <summary>
	/// おススメセット情報
	/// </summary>
	public class InitGroupPlan : IEquatable<InitGroupPlan>
	{
		/// <summary>
		/// グループ番号
		/// </summary>
		public int GroupID { get; set; }

		/// <summary>
		/// おススメセット名称
		/// </summary>
		public string GroupName { get; set; }

		/// <summary>
		/// サービスコードリスト
		/// </summary>
		public List<string> ServiceCodeList { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public InitGroupPlan()
		{
			GroupID = 0;
			GroupName = string.Empty;
			ServiceCodeList = new List<string>();
		}

		/// <summary>
		/// 同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するInitGroupPlan</param>
		/// <returns>判定</returns>
		public bool Equals(InitGroupPlan other)
		{
			if (null != other)
			{
				if (GroupID != other.GroupID)
					return false;
				if (GroupName != other.GroupName)
					return false;
				if (false == ServiceCodeList.SequenceEqual(other.ServiceCodeList))
					return false;
				return true;
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するInitGroupPlanオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is InitGroupPlan)
			{
				return this.Equals((InitGroupPlan)obj);
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
			return GroupID.ToString() + GroupName;
		}
	}
}
