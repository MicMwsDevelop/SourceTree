//
// T_USE_ONLINE_DEMAND.cs
//
// オンライン請求作業済申請情報クラス
// [CharlieDB].[dbo].[T_USE_ONLINE_DEMAND]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/12/01 勝呂):新規作成
// 
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.Table
{
	/// <summary>
	/// オンライン請求作業済申請情報
	/// </summary>
	public class T_USE_ONLINE_DEMAND
	{
		/// <summary>
		/// 受付番号
		/// </summary>
		public int ApplyNo { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerID { get; set; }

		/// <summary>
		/// オンライン請求設定（リモート対応）かどうか？
		/// </summary>
		public bool RemoteFlag { get; set; }

		/// <summary>
		/// 商品ID
		/// </summary>
		public string GoodsID { get; set; }

		/// <summary>
		/// オンライン請求作業済日時
		/// </summary>
		public DateTime? ApplyDate { get; set; }

		/// <summary>
		/// 削除フラグ
		/// </summary>
		public bool DeleteFlag { get; set; }

		/// <summary>
		/// 売上データ作成日時
		/// </summary>
		public DateTime? SalesDate { get; set; }

		/// <summary>
		/// 作成日時
		/// </summary>
		public DateTime? CreateDate { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string CreatePerson { get; set; }

		/// <summary>
		/// 更新日時
		/// </summary>
		public DateTime? UpdateDate { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		public string UpdatePerson { get; set; }

		/// <summary>
		/// 利用申込が取消が可能かどうか？
		/// </summary>
		public bool IsEnableCancel
		{
			get
			{
				return (SalesDate is null) ? true : false;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_USE_ONLINE_DEMAND()
		{
			ApplyNo = 0;
			CustomerID = 0;
			RemoteFlag = false;
			GoodsID = string.Empty;
			ApplyDate = null;
			DeleteFlag = false;
			SalesDate = null;
			CreateDate = null;
			CreatePerson = string.Empty;
			UpdateDate = null;
			UpdatePerson = string.Empty;
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_ONLINE_DEMAND]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>T_USE_ONLINE_DEMAND</returns>
		public static List<T_USE_ONLINE_DEMAND> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_USE_ONLINE_DEMAND> result = new List<T_USE_ONLINE_DEMAND>();
				foreach (DataRow row in table.Rows)
				{
					T_USE_ONLINE_DEMAND data = new T_USE_ONLINE_DEMAND
					{
						ApplyNo = DataBaseValue.ConvObjectToInt(row["ApplyNo"]),
						CustomerID = DataBaseValue.ConvObjectToInt(row["CustomerID"]),
						RemoteFlag = ("0" == row["RemoteFlag"].ToString()) ? false : true,
						GoodsID = row["GoodsID"].ToString().Trim(),
						ApplyDate = DataBaseValue.ConvObjectToDateTimeNull(row["ApplyDate"]),
						DeleteFlag = ("0" == row["DeleteFlag"].ToString()) ? false : true,
						SalesDate = DataBaseValue.ConvObjectToDateTimeNull(row["SalesDate"]),
						CreateDate = DataBaseValue.ConvObjectToDateTimeNull(row["CreateDate"]),
						CreatePerson = row["CreatePerson"].ToString().Trim(),
						UpdateDate = DataBaseValue.ConvObjectToDateTimeNull(row["UpdateDate"]),
						UpdatePerson = row["UpdatePerson"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
