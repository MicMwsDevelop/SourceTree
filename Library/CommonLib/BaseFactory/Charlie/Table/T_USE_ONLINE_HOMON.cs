//
// T_USE_ONLINE_HOMON.cs
//
// オン資格確認訪問診療連携契約情報クラス
// [CharlieDB].[dbo].[T_USE_ONLINE_HOMON]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/03/21 勝呂):新規作成
// 
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.Table
{
	public class T_USE_ONLINE_HOMON
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
		/// 申込日時
		/// </summary>
		public DateTime? ApplyDate { get; set; }

		/// <summary>
		/// 商品コード
		/// </summary>
		public string GoodsID { get; set; }

		/// <summary>
		/// 契約開始日
		/// </summary>
		public DateTime? ContractStartDate { get; set; }

		/// <summary>
		/// 契約終了日
		/// </summary>
		public DateTime? ContractEndDate { get; set; }

		/// <summary>
		/// 売上作成日時
		/// </summary>
		public DateTime? SalesDate { get; set; }

		/// <summary>
		/// 削除フラグ
		/// </summary>
		public bool DeleteFlag { get; set; }

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
		/// デフォルトコンストラクタ
		/// </summary>
		public T_USE_ONLINE_HOMON()
		{
			ApplyNo = 0;
			CustomerID = 0;
			ApplyDate = null;
			GoodsID = string.Empty;
			ContractStartDate = null;
			ContractEndDate = null;
			SalesDate = null;
			DeleteFlag = false;
			CreateDate = null;
			CreatePerson = string.Empty;
			UpdateDate = null;
			UpdatePerson = string.Empty;
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_ONLINE_HOMON]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>T_USE_ONLINE_HOMON</returns>
		public static List<T_USE_ONLINE_HOMON> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_USE_ONLINE_HOMON> result = new List<T_USE_ONLINE_HOMON>();
				foreach (DataRow row in table.Rows)
				{
					T_USE_ONLINE_HOMON data = new T_USE_ONLINE_HOMON
					{
						ApplyNo = DataBaseValue.ConvObjectToInt(row["ApplyNo"]),
						CustomerID = DataBaseValue.ConvObjectToInt(row["CustomerID"]),
						GoodsID = row["GoodsID"].ToString().Trim(),
						ContractStartDate = DataBaseValue.ConvObjectToDateTimeNull(row["ContractStartDate"]),
						ContractEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["ContractEndDate"]),
						SalesDate = DataBaseValue.ConvObjectToDateTimeNull(row["SalesDate"]),
						DeleteFlag = ("0" == row["DeleteFlag"].ToString()) ? false : true,
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
