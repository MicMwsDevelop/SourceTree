//
// T_USE_ELECTRIC_PRESCRIPTION.cs
//
// 電子処方箋管理契約情報クラス
// [CharlieDB].[dbo].[T_USE_ELECTRIC_PRESCRIPTION]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2024/07/01 勝呂):新規作成
// 
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.Table
{
	/// <summary>
	/// 電子処方箋管理契約情報
	/// </summary>
	public class T_USE_ELECTRIC_PRESCRIPTION
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
		/// カプラー申込ID
		/// </summary>
		public int CouplerApplyID { get; set; }

		/// <summary>
		/// 注文ID
		/// </summary>
		public int OrderReserveID { get; set; }

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
		public T_USE_ELECTRIC_PRESCRIPTION()
		{
			ApplyNo = 0;
			CustomerID = 0;
			ApplyDate = null;
			GoodsID = string.Empty;
			CouplerApplyID = 0;
			OrderReserveID = 0;
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
		/// [charlieDB].[dbo].[T_USE_ELECTRIC_PRESCRIPTION]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>T_USE_ELECTRIC_PRESCRIPTION</returns>
		public static List<T_USE_ELECTRIC_PRESCRIPTION> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_USE_ELECTRIC_PRESCRIPTION> result = new List<T_USE_ELECTRIC_PRESCRIPTION>();
				foreach (DataRow row in table.Rows)
				{
					T_USE_ELECTRIC_PRESCRIPTION data = new T_USE_ELECTRIC_PRESCRIPTION
					{
						ApplyNo = DataBaseValue.ConvObjectToInt(row["ApplyNo"]),
						CustomerID = DataBaseValue.ConvObjectToInt(row["CustomerID"]),
						ApplyDate = DataBaseValue.ConvObjectToDateTimeNull(row["ApplyDate"]),
						GoodsID = row["GoodsID"].ToString().Trim(),
						CouplerApplyID = DataBaseValue.ConvObjectToInt(row["CouplerApplyID"]),
						OrderReserveID = DataBaseValue.ConvObjectToInt(row["OrderReserveID"]),
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
