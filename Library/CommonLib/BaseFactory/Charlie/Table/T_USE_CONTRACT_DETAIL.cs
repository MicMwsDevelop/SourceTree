//
// T_USE_CONTRACT_HEADER.cs
//
// おまとめプラン契約詳細情報クラス
// [CharlieDB].[dbo].[T_USE_CONTRACT_DETAIL]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2020/12/22 勝呂)
// 
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.Table
{
	public class T_USE_CONTRACT_DETAIL
	{
		/// <summary>
		/// 申込No
		/// </summary>
		public int fContractDetailID { get; set; }

		/// <summary>
		/// おまとめプラン契約ヘッダ情報申込No
		/// </summary>
		public int fContractID { get; set; }

		/// <summary>
		/// サービスID
		/// </summary>
		public int fSERVICE_ID { get; set; }

		/// <summary>
		/// サービス名
		/// </summary>
		public string fSERVICE_NAME { get; set; }

		/// <summary>
		/// 作成日時
		/// </summary>
		public DateTime? fCreateDate { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string fCreatePerson { get; set; }

		/// <summary>
		/// 更新日時
		/// </summary>
		public DateTime? fUpdateDate { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		public string fUpdatePerson { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_USE_CONTRACT_DETAIL()
		{
			fContractDetailID = 0;
			fContractID = 0;
			fSERVICE_ID = 0;
			fSERVICE_NAME = string.Empty;
			fCreateDate = null;
			fCreatePerson = string.Empty;
			fUpdateDate = null;
			fUpdatePerson = string.Empty;
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_CONTRACT_DETAIL]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>T_USE_CONTRACT_DETAIL</returns>
		public static List<T_USE_CONTRACT_DETAIL> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_USE_CONTRACT_DETAIL> result = new List<T_USE_CONTRACT_DETAIL>();
				foreach (DataRow row in table.Rows)
				{
					T_USE_CONTRACT_DETAIL data = new T_USE_CONTRACT_DETAIL
					{
						fContractDetailID = DataBaseValue.ConvObjectToInt(row["fContractDetailID"]),
						fContractID = DataBaseValue.ConvObjectToInt(row["fContractID"]),
						fSERVICE_ID = DataBaseValue.ConvObjectToInt(row["fSERVICE_ID"]),
						fSERVICE_NAME = row["fSERVICE_NAME"].ToString().Trim(),
						fCreateDate = DataBaseValue.ConvObjectToDateTimeNull(row["fCreateDate"]),
						fCreatePerson = row["fCreatePerson"].ToString().Trim(),
						fUpdateDate = DataBaseValue.ConvObjectToDateTimeNull(row["fUpdateDate"]),
						fUpdatePerson = row["fUpdatePerson"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
