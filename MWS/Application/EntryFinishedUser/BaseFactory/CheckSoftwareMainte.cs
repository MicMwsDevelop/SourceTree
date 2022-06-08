//
// CheckSoftwareMainte.cs
//
// palette ESとソフトウェア保守料１年の契約期間チェック情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver2.02 paletteESとソフトウェア保守料１年の契約期間のチェックの追加(2022/05/13 勝呂)
// 
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace EntryFinishedUser.BaseFactory
{
	/// <summary>
	/// palette ESとソフトウェア保守料１年の契約期間チェック
	/// </summary>
	public class CheckSoftwareMainte
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerID { get; set; }

		/// <summary>
		/// paletteES 利用終了日
		/// </summary>
		public DateTime? EsUseEndDate { get; set; }

		/// <summary>
		/// ソフトウェア保守料１年 利用終了日
		/// </summary>
		public DateTime? MainteUseEndDate { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public CheckSoftwareMainte()
		{
			CustomerID = 0;
			EsUseEndDate = null;
			MainteUseEndDate = null;
		}

		/// <summary>
		/// CheckSoftwareMainteの詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>利用情報</returns>
		public static List<CheckSoftwareMainte> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<CheckSoftwareMainte> result = new List<CheckSoftwareMainte>();
				foreach (DataRow row in table.Rows)
				{
					CheckSoftwareMainte data = new CheckSoftwareMainte
					{
						CustomerID = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]),
						EsUseEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["ES_USE_END_DATE"]),
						MainteUseEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["MN_USE_END_DATE"]),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
