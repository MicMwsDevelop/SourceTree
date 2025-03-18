//
// UseOnlineDemand.cs
//
// 各種作業料作業済申請情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/01/23 勝呂)
// 
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.MwsServiceCancelTool
{
	public class UseOnlineDemand
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
		/// 商品名
		/// </summary>
		public string GoodsName { get; set; }

		/// <summary>
		/// オンライン請求作業済日時
		/// </summary>
		public DateTime? ApplyDate { get; set; }

		/// <summary>
		/// 売上データ作成日時
		/// </summary>
		public DateTime? SalesDate { get; set; }

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
		public UseOnlineDemand()
		{
			ApplyNo = 0;
			CustomerID = 0;
			RemoteFlag = false;
			GoodsID = string.Empty;
			GoodsName = string.Empty;
			ApplyDate = null;
			SalesDate = null;
		}

		/// <summary>
		/// UseOnlineDemandの詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>T_USE_ONLINE_DEMAND</returns>
		public static List<UseOnlineDemand> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<UseOnlineDemand> result = new List<UseOnlineDemand>();
				foreach (DataRow row in table.Rows)
				{
					UseOnlineDemand data = new UseOnlineDemand
					{
						ApplyNo = DataBaseValue.ConvObjectToInt(row["ApplyNo"]),
						CustomerID = DataBaseValue.ConvObjectToInt(row["CustomerID"]),
						RemoteFlag = ("0" == row["RemoteFlag"].ToString()) ? false : true,
						GoodsID = row["GoodsID"].ToString().Trim(),
						GoodsName = row["GoodsName"].ToString().Trim(),
						ApplyDate = DataBaseValue.ConvObjectToDateTimeNull(row["ApplyDate"]),
						SalesDate = DataBaseValue.ConvObjectToDateTimeNull(row["SalesDate"]),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}

