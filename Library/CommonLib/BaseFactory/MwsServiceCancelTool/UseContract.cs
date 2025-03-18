//
// UseContractHeader.cs
//
// 契約情報クラス
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
	/// <summary>
	/// 契約ヘッダ情報
	/// </summary>
	public class UseContractHeader
	{
		/// <summary>
		/// 申込No
		/// </summary>
		public int fContractID { get; set; }

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
		/// 契約金額
		/// </summary>
		public int? fTotalAmount { get; set; }

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
		public UseContractHeader()
		{
			fContractID = 0;
			fCustomerID = 0;
			fContractType = string.Empty;
			fMonths = null;
			fGoodsID = string.Empty;
			fApplyDate = null;
			fTotalAmount = null;
			fContractStartDate = null;
			fContractEndDate = null;
			fBillingStartDate = null;
			fBillingEndDate = null;
		}

		/// <summary>
		/// UseContractHeaderの詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>UseContractHeader</returns>
		public static List<UseContractHeader> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<UseContractHeader> result = new List<UseContractHeader>();
				foreach (DataRow row in table.Rows)
				{
					UseContractHeader data = new UseContractHeader
					{
						fContractID = DataBaseValue.ConvObjectToInt(row["fContractID"]),
						fCustomerID = DataBaseValue.ConvObjectToInt(row["fCustomerID"]),
						fContractType = row["fContractType"].ToString().Trim(),
						fMonths = DataBaseValue.ConvObjectToIntNull(row["fMonths"]),
						fGoodsID = row["fGoodsID"].ToString().Trim(),
						fApplyDate = DataBaseValue.ConvObjectToDateTimeNull(row["fApplyDate"]),
						fTotalAmount = DataBaseValue.ConvObjectToIntNull(row["fTotalAmount"]),
						fContractStartDate = DataBaseValue.ConvObjectToDateTimeNull(row["fContractStartDate"]),
						fContractEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["fContractEndDate"]),
						fBillingStartDate = DataBaseValue.ConvObjectToDateTimeNull(row["fBillingStartDate"]),
						fBillingEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["fBillingEndDate"]),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}

	/// <summary>
	/// 契約詳細情報
	/// </summary>
	public class UseContractDetail
	{
		/// <summary>
		/// 申込No
		/// </summary>
		public int fContractDetailID { get; set; }

		/// <summary>
		/// 契約ヘッダ情報申込No
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
		/// デフォルトコンストラクタ
		/// </summary>
		public UseContractDetail()
		{
			fContractDetailID = 0;
			fContractID = 0;
			fSERVICE_ID = 0;
			fSERVICE_NAME = string.Empty;
		}

		/// <summary>
		/// UseContractDetailの詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>UseContractDetail</returns>
		public static List<UseContractDetail> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<UseContractDetail> result = new List<UseContractDetail>();
				foreach (DataRow row in table.Rows)
				{
					UseContractDetail data = new UseContractDetail
					{
						fContractDetailID = DataBaseValue.ConvObjectToInt(row["fContractDetailID"]),
						fContractID = DataBaseValue.ConvObjectToInt(row["fContractID"]),
						fSERVICE_ID = DataBaseValue.ConvObjectToInt(row["fSERVICE_ID"]),
						fSERVICE_NAME = row["fSERVICE_NAME"].ToString().Trim(),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
