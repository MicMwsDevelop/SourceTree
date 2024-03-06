//
// V_SERVICE.cs
//
// [CharlieDB].[dbo].[V_SERVICE] サービス利用情報ビュー
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/07/26 勝呂):新規作成
// 
using CommonLib.BaseFactory.Charlie.Table;
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.View
{
	/// <summary>
	/// サービス利用情報取得ビュー
	/// </summary>
	public class V_SERVICE
	{
		/// <summary>
		/// サービスステータス
		/// </summary>
		public enum ServiceStatus
		{
			利用申込可能 = 0,
			利用申込受付中 = 1,
			契約中 = 2,
			契約終了予定 = 3,
			解約申込受付中 = 4,
		}

		/// <summary>
		/// MWSID
		/// </summary>
		public string cp_id { get; set; }
			
		/// <summary>
		/// サービスID
		/// </summary>
		public int service_id { get; set; }

		/// <summary>
		/// 利用開始日
		/// </summary>
		public DateTime? start_date { get; set; }

		/// <summary>
		/// 利用終了日
		/// </summary>
		public DateTime? end_date { get; set; }

		/// <summary>
		/// 契約種別（0:契約、1:解約）
		/// </summary>
		public bool contrac_type { get; set; }

		/// <summary>
		/// 支払い方法（未使用）
		/// </summary>
		public bool payment_type { get; set; }

		/// <summary>
		/// 作成日
		/// </summary>
		public DateTime? create_date { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string create_user { get; set; }

		/// <summary>
		/// 更新日
		/// </summary>
		public DateTime? update_date { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		public string update_user { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int customer_id { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public V_SERVICE()
		{
			cp_id = string.Empty;
			service_id=0;
			start_date = null;
			end_date = null;
			contrac_type = false;
			payment_type = false;
			create_date = null;
			create_user = string.Empty;
			update_date = null;
			update_user = string.Empty;
			customer_id =0;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns>サービス利用情報リスト</returns>
		public static List<V_SERVICE> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<V_SERVICE> result = new List<V_SERVICE>();
				foreach (DataRow row in table.Rows)
				{
					V_SERVICE data = new V_SERVICE
					{
						cp_id = row["cp_id"].ToString().Trim(),
						service_id = DataBaseValue.ConvObjectToInt(row["service_id"]),
						start_date = DataBaseValue.ConvObjectToDateTimeNull(row["start_date"]),
						end_date = DataBaseValue.ConvObjectToDateTimeNull(row["end_date"]),
						contrac_type = DataBaseValue.ConvObjectToBool(row["contrac_type"]),
						payment_type = DataBaseValue.ConvObjectToBool(row["payment_type"]),
						create_date = DataBaseValue.ConvObjectToDateTimeNull(row["create_date"]),
						create_user = row["create_user"].ToString().Trim(),
						update_date = DataBaseValue.ConvObjectToDateTimeNull(row["update_date"]),
						update_user = row["update_user"].ToString().Trim(),
						customer_id = DataBaseValue.ConvObjectToInt(row["customer_id"])
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="service"></param>
		/// <param name="apply"></param>
		/// <param name="today"></param>
		/// <returns></returns>
		public static ServiceStatus GetServiceStatus(V_SERVICE service, T_MWS_APPLY apply, DateTime today)
		{
			if (null == service)
			{
				// サービス情報なし
				if (null == apply || "2" == apply.system_flg)
				{
					// 申込情報なし OR 申込情報.システム反映済フラグ=取消
					return ServiceStatus.利用申込可能;
				}
				if (null != apply && "0" == apply.apply_type && "0" == apply.system_flg)
				{
					// 申込情報.申込種別 = 利用申込 AND 申込情報.システム反映済フラグ = 未反映
					return ServiceStatus.利用申込受付中;
				}
				return ServiceStatus.利用申込可能;
			}
			// サービス情報あり
			if (service.end_date.HasValue)
			{
				if (today.Year == service.end_date.Value.Year && today.Month == service.end_date.Value.Month)
				{
					// 利用終了月が当月
					if (null == apply || "2" == apply.system_flg)
					{
						// 申込情報なし OR 申込情報.システム反映済フラグ=取消
						return ServiceStatus.契約終了予定;
					}
					if (null != apply && "0" == apply.apply_type)
					{
						// 申込情報.申込種別=利用申込
						if ("0" == apply.system_flg)
						{
							// 申込情報.システム反映済フラグ=未反映
							return ServiceStatus.利用申込受付中;
						}
					}
					return ServiceStatus.契約終了予定;
				}
				else if (today <= service.end_date.Value)
				{
					// 利用終了日が有効期間内
					if (null == apply || "2" == apply.system_flg)
					{
						// 申込情報なし OR 申込情報.システム反映済フラグ=取消
						return ServiceStatus.契約中;
					}
					if (null != apply && "1" == apply.apply_type)
					{
						// 申込情報.申込種別=解約申込
						if ("0" == apply.system_flg)
						{
							// 申込情報.システム反映済フラグ=未反映
							return ServiceStatus.解約申込受付中;
						}
					}
					return ServiceStatus.契約中;
				}
				else
				{
					// 有効期間外
					if (null == apply || "2" == apply.system_flg)
					{
						// 申込情報なし OR 申込情報.システム反映済フラグ=取消
						return ServiceStatus.利用申込可能;
					}
					if (null != apply && "0" == apply.apply_type)
					{
						// 申込情報.申込種別=利用申込
						if ("0" == apply.system_flg)
						{
							// 申込情報.システム反映済フラグ=未反映
							return ServiceStatus.利用申込受付中;
						}
					}
				}
			}
			return ServiceStatus.利用申込可能;
		}
	}
}
