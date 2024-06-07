//
// ServiceInfo.cs
// 
// MIC WEB SERVICE 課金シミュレーション サービス情報
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.000 新規作成(2018/10/24 勝呂)
// Ver2.210 おまとめプランにクラウドバックアップが含まれないように対応(2020/11/20 勝呂)
// Ver2.30(2024/05/20 勝呂):サービス情報マスタにフィールドを追加して、おまとめプランに含めるかどうかの判断するように仕様変更
//
using CommonLib.Common;
using CommonLib.DB.SQLite.MwsSimulation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonLib.BaseFactory.MwsSimulation
{
	/// <summary>
	/// サービス情報クラス
	/// </summary>
	public class ServiceInfo : IEquatable<ServiceInfo>
	{
		/// <summary>
		/// サービスコード
		/// </summary>
		public int ServiceCode { get; set; }

		/// <summary>
		/// サービス名称
		/// </summary>
		public string ServiceName { get; set; }

		/// <summary>
		/// 親サービスコード
		/// </summary>
		public int ParentServiceCode { get; set; }

		/// <summary>
		/// サービス種別
		/// </summary>
		public int ServiceType { get; set; }

		/// <summary>
		/// サービス種別名称
		/// </summary>
		public string ServiceTypeName { get; set; }

		/// <summary>
		/// 価格
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// 商品ID
		/// </summary>
		public string GoodsID { get; set; }

		/// <summary>
		/// 商品名
		/// </summary>
		public string GoodsName { get; set; }

		/// <summary>
		/// 商品区分
		/// </summary>
		public int GoodsKubun { get; set; }

		/// <summary>
		/// おまとめプラン対象サービス
		/// </summary>
		// Ver2.30(2024/05/20 勝呂):サービス情報マスタにフィールドを追加して、おまとめプランに含めるかどうかの判断するように仕様変更
		public bool Matome { get; set; }

		/// <summary>
		/// 選択状態
		/// </summary>
		public bool Select { get; set; }

		/// <summary>
		/// セット割サービスかどうか？
		/// </summary>
		public bool SetService { get; set; }

		/// <summary>
		/// おまとめプラン対象サービスか？
		/// </summary>
		public bool IsGroupPlanService
		{
			get
			{
				// Ver2.30(2024/05/20 勝呂):サービス情報マスタにフィールドを追加して、おまとめプランに含めるかどうかの判断するように仕様変更
				//if (SQLiteMwsSimulationDef.GOODS_KUBUN_GROUP_PLAN_SERVICE == GoodsKubun)
				//{
				//	// Ver2.210 おまとめプランにクラウドバックアップが含まれないように対応(2020/11/20 勝呂)
				//	if ((int)ServiceCodeDefine.ServiceCode.ExCloudBackup != int.Parse(ServiceCode))
				//	{
				//		// 1036260 クラウドバックアップ はおまとめプランに含めない
				//		return true;
				//	}
				//}
				return Matome;
			}
		}

		/// <summary>
		/// 子サービスかどうか？
		/// </summary>
		public bool IsChildService
		{
			get
			{
				if (0 < ParentServiceCode)
				{
					return true;
				}
				return false;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ServiceInfo()
		{
			ServiceCode = 0;
			ServiceName = string.Empty;
			ParentServiceCode = 0;
			ServiceType = 0;
			ServiceTypeName = string.Empty;
			Price = 0;
			GoodsID = string.Empty;
			GoodsName = string.Empty;
			GoodsKubun = 0;
			Select = false;
			SetService = false;

			// Ver2.30(2024/05/20 勝呂):サービス情報マスタにフィールドを追加して、おまとめプランに含めるかどうかの判断するように仕様変更
			Matome = false;
		}

		/// <summary>
		/// 表示情報の取得 
		/// </summary>
		/// <returns>表示情報</returns>
		public string[] GetListViewData()
		{
			string[] array = new string[3];
			array[0] = ServiceTypeName;
			array[1] = ServiceName;
			array[2] = "\\" + StringUtil.CommaEdit(Price);
			return array;
		}

		/// <summary>
		/// 同一かどうかを判断する
		/// </summary>
		/// <param name="other">比較するServiceInfo</param>
		/// <returns>判定</returns>
		public bool Equals(ServiceInfo other)
		{
			if (null != other)
			{
				if (ServiceCode != other.ServiceCode)
					return false;
				if (ServiceName != other.ServiceName)
					return false;
				if (ParentServiceCode != other.ParentServiceCode)
					return false;
				if (ServiceType != other.ServiceType)
					return false;
				if (ServiceTypeName != other.ServiceTypeName)
					return false;
				if (Price != other.Price)
					return false;
				if (GoodsID != other.GoodsID)
					return false;
				if (GoodsName != other.GoodsName)
					return false;
				if (GoodsKubun != other.GoodsKubun)
					return false;
				// Ver2.30(2024/05/20 勝呂):サービス情報マスタにフィールドを追加して、おまとめプランに含めるかどうかの判断するように仕様変更
				if (Matome != other.Matome)
					return false;
				if (Select != other.Select)
					return false;
				if (SetService != other.SetService)
					return false;
				return true;
			}
			return false;
		}

		/// <summary>
		/// このインスタンスと、指定したオブジェクトの値が同一かどうかを判断する
		/// (Object.Equals(Object)をオーバーライドする)
		/// </summary>
		/// <param name="obj">比較するServiceInfoオブジェクト</param>
		/// <returns>判定</returns>
		public override bool Equals(object obj)
		{
			if (obj is GroupPlan)
			{
				return this.Equals((ServiceInfo)obj);
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
			return ServiceCode.ToString() + ServiceName + ParentServiceCode.ToString() + ServiceType.ToString() + ServiceTypeName + Price.ToString() + GoodsID + GoodsName + GoodsKubun.ToString();
		}
	}

	/// <summary>
	/// サービス情報リストクラス
	/// </summary>
	public class ServiceInfoList : List<ServiceInfo>, IEquatable<ServiceInfoList>
	{
		/// <summary>
		/// ＭＩＣ ＷＥＢ ＳＥＲＶＩＣＥ 標準機能 
		/// </summary>
		public ServiceInfo Platform { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ServiceInfoList()
		{
			Platform = null;
		}

		/// <summary>
		/// サービス使用料の取得
		/// </summary>
		/// <returns>サービス使用料</returns>
		public int GetTotalServicePrice()
		{
			int price = 0;
			foreach (ServiceInfo service in this)
			{
				if (service.Select)
				{
					price += service.Price;
				}
			}
			return price;
		}

		/// <summary>
		/// 指定されたサービスコードに対するサービス名称の取得
		/// </summary>
		/// <param name="id">サービスコード</param>
		/// <returns>サービス名称</returns>
		public string GetServiceName(int id)
		{
			foreach (ServiceInfo service in this)
			{
				if (service.ServiceCode == id)
				{
					return service.ServiceName;
				}
			}
			return string.Empty;
		}

		/// <summary>
		/// 指定されたServiceInfoListと内容が並び順も含めて同じかどうかを返す
		/// </summary>
		/// <param name="other">比較するServiceInfoList</param>
		/// <returns> 指定されたGroupPlanListと内容が並び順も含めて同じ場合はtrue、それ以外の場合はfalseを返す</returns>
		public bool Equals(ServiceInfoList other)
		{
			if (null != other)
			{
				if (null != Platform && null != other.Platform)
				{
					if (this.SequenceEqual(other))
						return true;
				}
				else if (null == Platform && null == other.Platform)
				{
					if (this.SequenceEqual(other))
						return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 指定されたServiceInfoListと内容が並び順も含めて同じかどうかを返す
		/// </summary>
		/// <param name="other">比較するServiceInfoList</param>
		/// <returns> 指定されたServiceInfoListと内容が並び順も含めて同じ場合はtrue、それ以外の場合はfalseを返す</returns>
		public override bool Equals(Object other)
		{
			if (other is ServiceInfoList)
			{
				return this.Equals((ServiceInfoList)other);
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
	}

	/// <summary>
	/// おまとめプラン・セット割サービスマスター情報
	/// </summary>
	public class GroupService
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
		/// おまとめプラン・セット割サービス種別
		/// </summary>
		public SQLiteMwsSimulationDef.ServiceMode Mode { get; set; }

		/// <summary>
		/// 価格
		/// </summary>
		public int Price;

		/// <summary>
		/// サービスコード
		/// item1:商品コード、item2:サービス名称
		/// </summary>
		public List<Tuple<string, string>> ServiceCodeList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public GroupService()
		{
			GoodsID = string.Empty;
			GoodsName = string.Empty;
			Mode = SQLiteMwsSimulationDef.ServiceMode.None;
			Price = 0;
			ServiceCodeList = new List<Tuple<string, string>>();
		}
	}
}
