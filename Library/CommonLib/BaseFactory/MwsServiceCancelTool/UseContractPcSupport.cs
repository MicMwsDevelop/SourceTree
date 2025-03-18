//
// UseContractPcSupport.cs
//
// PC安心サポート契約情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2025/01/23 勝呂)
// 
using CommonLib.Common;
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.MwsServiceCancelTool
{

	/// <summary>
	/// PC安心サポート契約情報
	/// <summary>
	public class UseContractPcSupport
	{
		/// <summary>
		/// 申込No
		/// </summary>
		public int fApplyNo { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int fCustomerID { get; set; }

		/// <summary>
		/// サービスコード
		/// </summary>
		public int fServiceId { get; set; }

		/// <summary>
		/// 契約年数
		/// </summary>
		public int fYears { get; set; }

		/// <summary>
		/// 商品ID
		/// </summary>
		public string fGoodsID { get; set; }

		/// <summary>
		/// 申込日付
		/// </summary>
		public DateTime? fApplyDate { get; set; }

		/// <summary>
		/// 契約開始日
		/// </summary>
		public Date? fContractStartDate { get; set; }

		/// <summary>
		/// 契約終了日
		/// </summary>
		public Date? fContractEndDate { get; set; }

		/// <summary>
		/// 課金開始日
		/// </summary>
		public Date? fBillingStartDate { get; set; }

		/// <summary>
		/// 課金終了日
		/// </summary>
		public Date? fBillingEndDate { get; set; }

		/// <summary>
		/// 終了フラグ
		/// </summary>
		public bool fEndFlag { get; set; }

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
		/// 商品コードが更新かどうか？
		/// PC安心サポート１年契約（更新用） or PC安心サポートPlus１年契約（更新用）
		/// </summary>
		public bool IsContinueGoods
		{
			get
			{
				return (PcaGoodsIDDefine.PcSupport1Continue == fGoodsID || PcaGoodsIDDefine.PcSupportPlus1Continue == fGoodsID) ? true : false;
			}
		}

		/// <summary>
		/// 商品コードがPC安心サポートPlusかどうか？
		/// </summary>
		public bool IsPcSupportPlusGoods
		{
			get
			{
				switch (fGoodsID)
				{
					case PcaGoodsIDDefine.PcSupportPlus1:
					case PcaGoodsIDDefine.PcSupportPlus3:
					case PcaGoodsIDDefine.PcSupportPlus1Continue:
						return true;
				}
				return false;
			}
		}

		/// <summary>
		/// サービスコードがPC安心サポート３年契約またはPC安心サポートPlus３年契約かどうか？
		/// </summary>
		public bool IsThreeYearService
		{
			get
			{
				return ((int)ServiceCodeDefine.ServiceCode.PcSafetySupport3 == fServiceId || (int)ServiceCodeDefine.ServiceCode.PcSafetySupportPlus3 == fServiceId) ? true : false;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public UseContractPcSupport()
		{
			fApplyNo = 0;
			fCustomerID = 0;
			fServiceId = 0;
			fYears = 0;
			fGoodsID = null;
			fApplyDate = null;
			fContractStartDate = null;
			fContractEndDate = null;
			fBillingStartDate = null;
			fBillingEndDate = null;
			fEndFlag = false;
		}

		/// <summary>
		/// UseContractPcSupportの詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>PC安心サポート契約情報</returns>
		public static List<UseContractPcSupport> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<UseContractPcSupport> result = new List<UseContractPcSupport>();
				foreach (DataRow row in table.Rows)
				{
					UseContractPcSupport data = new UseContractPcSupport
					{
						fApplyNo = DataBaseValue.ConvObjectToInt(row["fApplyNo"]),
						fCustomerID = DataBaseValue.ConvObjectToInt(row["fCustomerID"]),
						fServiceId = DataBaseValue.ConvObjectToInt(row["fServiceId"]),
						fYears = DataBaseValue.ConvObjectToInt(row["fYears"]),
						fGoodsID = row["fGoodsID"].ToString().Trim(),
						fApplyDate = DataBaseValue.ConvObjectToDateTimeNull(row["fApplyDate"]),
						fContractStartDate = DataBaseValue.ConvObjectToDateNullByDate(row["fContractStartDate"]),
						fContractEndDate = DataBaseValue.ConvObjectToDateNullByDate(row["fContractEndDate"]),
						fBillingStartDate = DataBaseValue.ConvObjectToDateNullByDate(row["fBillingStartDate"]),
						fBillingEndDate = DataBaseValue.ConvObjectToDateNullByDate(row["fBillingEndDate"]),
						fEndFlag = ("0" == row["fEndFlag"].ToString()) ? false : true,
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
