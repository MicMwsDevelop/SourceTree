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
		/// <summary>
		/// 申込No
		/// </summary>
		public int fContractID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool fContractFinalized { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int fCustomerID { get; set; }

		/// <summary>
		/// 契約種別
		/// ＶＰ：paletteES、まとめ：おまとめプラン、セット：セット契約
		/// </summary>
		public string fContractType { get; set; }

		/// <summary>
		/// 契約月数
		/// </summary>
		public int? fMonths { get; set; }

		/// <summary>
		/// PCA商品コード
		/// </summary>
		public string fGoodsID { get; set; }

		/// <summary>
		/// 申込日時
		/// </summary>
		public DateTime? fApplyDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? fInvoiceNo { get; set; }

		/// <summary>
		/// 契約金額
		/// </summary>
		public int? fTotalAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? fDueDate { get; set; }

		/// <summary>
		/// 契約開始日
		/// </summary>
		public DateTime? fContractStartDate { get; set; }

		/// <summary>
		/// 契約終了日
		/// </summary>
		public DateTime? fContractEndDate { get; set; }

		/// <summary>
		/// 課金開始日
		/// </summary>
		public DateTime? fBillingStartDate { get; set; }

		/// <summary>
		/// 課金終了日
		/// </summary>
		public DateTime? fBillingEndDate { get; set; }

		/// <summary>
		/// 終了フラグ
		/// </summary>
		public bool fEndFlag { get; set; }

		/// <summary>
		/// 削除フラグ
		/// </summary>
		public bool fDeleteFlag { get; set; }

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
		/// 利用申込が取消が可能かどうか？
		/// </summary>
		public bool IsEnableCancel
		{
			get
			{
				return (fBillingStartDate is null) && (fBillingEndDate is null) ? true : false;
			}
		}

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
