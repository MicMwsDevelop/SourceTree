//
// T_USE_CONTRACT_HEADER.cs
//
// おまとめプラン契約ヘッダ情報クラス
// [CharlieDB].[dbo].[T_USE_CONTRACT_HEADER]
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
	public class T_USE_CONTRACT_HEADER
	{
		public int fContractID { get; set; }
		public bool fContractFinalized { get; set; }
		public int fCustomerID { get; set; }
		public string fContractType { get; set; }
		public int? fMonths { get; set; }
		public string fGoodsID { get; set; }
		public DateTime? fApplyDate { get; set; }
		public int? fInvoiceNo { get; set; }
		public int? fTotalAmount { get; set; }
		public DateTime? fDueDate { get; set; }
		public DateTime? fContractStartDate { get; set; }
		public DateTime? fContractEndDate { get; set; }
		public DateTime? fBillingStartDate { get; set; }
		public DateTime? fBillingEndDate { get; set; }
		public bool fEndFlag { get; set; }
		public bool fDeleteFlag { get; set; }
		public DateTime? fCreateDate { get; set; }
		public string fCreatePerson { get; set; }
		public DateTime? fUpdateDate { get; set; }
		public string fUpdatePerson { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public T_USE_CONTRACT_HEADER()
		{
			fContractID = 0;
			fContractFinalized = false;
			fCustomerID = 0;
			fContractType = string.Empty;
			fMonths = null;
			fGoodsID = string.Empty;
			fApplyDate = null;
			fInvoiceNo = null;
			fTotalAmount = null;
			fDueDate = null;
			fContractStartDate = null;
			fContractEndDate = null;
			fBillingStartDate = null;
			fBillingEndDate = null;
			fEndFlag = false;
			fDeleteFlag = false;
			fCreateDate = null;
			fCreatePerson = string.Empty;
			fUpdateDate = null;
			fUpdatePerson = string.Empty;
		}

		/// <summary>
		/// [charlieDB].[dbo].[T_USE_CONTRACT_HEADER]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>T_USE_CONTRACT_HEADER</returns>
		public static List<T_USE_CONTRACT_HEADER> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<T_USE_CONTRACT_HEADER> result = new List<T_USE_CONTRACT_HEADER>();
				foreach (DataRow row in table.Rows)
				{
					T_USE_CONTRACT_HEADER data = new T_USE_CONTRACT_HEADER
					{
						fContractID = DataBaseValue.ConvObjectToInt(row["fContractID"]),
						fContractFinalized = ("0" == row["fContractFinalized"].ToString()) ? false : true,
						fCustomerID = DataBaseValue.ConvObjectToInt(row["fCustomerID"]),
						fContractType = row["fContractType"].ToString().Trim(),
						fMonths = DataBaseValue.ConvObjectToIntNull(row["fMonths"]),
						fGoodsID = row["fGoodsID"].ToString().Trim(),
						fApplyDate = DataBaseValue.ConvObjectToDateTimeNull(row["fApplyDate"]),
						fInvoiceNo = DataBaseValue.ConvObjectToIntNull(row["fInvoiceNo"]),
						fTotalAmount = DataBaseValue.ConvObjectToIntNull(row["fTotalAmount"]),
						fDueDate = DataBaseValue.ConvObjectToDateTimeNull(row["fDueDate"]),
						fContractStartDate = DataBaseValue.ConvObjectToDateTimeNull(row["fContractStartDate"]),
						fContractEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["fContractEndDate"]),
						fBillingStartDate = DataBaseValue.ConvObjectToDateTimeNull(row["fBillingStartDate"]),
						fBillingEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["fBillingEndDate"]),
						fEndFlag = ("0" == row["fEndFlag"].ToString()) ? false : true,
						fDeleteFlag = ("0" == row["fDeleteFlag"].ToString()) ? false : true,
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
