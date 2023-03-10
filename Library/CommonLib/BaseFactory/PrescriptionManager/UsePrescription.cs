//
// UsePrescription.cs
// 
// 電子処方箋契約情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/02/14 勝呂):新規作成
//
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.PrescriptionManager
{
	public class UsePrescription
	{
		/// <summary>
		/// 申込No
		/// </summary>
		public int ContractID { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerID { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string CustomerName { get; set; }

		/// <summary>
		/// 商品ID
		/// </summary>
		public string GoodsID { get; set; }

		/// <summary>
		/// 商品名
		/// </summary>
		public string GoodsName { get; set; }

		/// <summary>
		/// 注文ID
		/// </summary>
		public int OrderReserveID { get; set; }

		/// <summary>
		/// 申込日時
		/// </summary>
		public DateTime? ApplyDate { get; set; }

		/// <summary>
		/// 運用開始日
		/// </summary>
		public DateTime? OperationDate { get; set; }

		/// <summary>
		/// 契約開始日
		/// </summary>
		public DateTime? ContractStartDate { get; set; }

		/// <summary>
		/// 契約終了日
		/// </summary>
		public DateTime? ContractEndDate { get; set; }

		/// <summary>
		/// 課金開始日
		/// </summary>
		public DateTime? BillingStartDate { get; set; }

		/// <summary>
		/// 課金終了日
		/// </summary>
		public DateTime? BillingEndDate { get; set; }

		/// <summary>
		/// 終了フラグ
		/// </summary>
		public bool EndFlag { get; set; }

		/// <summary>
		/// 削除フラグ
		/// </summary>
		public bool DeleteFlag { get; set; }

		/// <summary>
		/// 作成日
		/// </summary>
		public DateTime? CreateDate { get; set; }

		/// <summary>
		/// 作成者
		/// </summary>
		public string CreatePerson { get; set; }

		/// <summary>
		/// 更新日
		/// </summary>
		public DateTime? UpdateDate { get; set; }

		/// <summary>
		/// 更新者
		/// </summary>
		public string UpdatePerson { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public UsePrescription()
		{
			ContractID = 0;
			CustomerID = 0;
			CustomerName = string.Empty;
			GoodsID = string.Empty;
			GoodsName = string.Empty;
			OrderReserveID = 0;
			ApplyDate = null;
			OperationDate = null;
			ContractStartDate = null;
			ContractEndDate = null;
			BillingStartDate = null;
			BillingEndDate = null;
			EndFlag=false;
			DeleteFlag = false;
			CreateDate = null;
			CreatePerson = string.Empty;
			UpdateDate = null;
			UpdatePerson = string.Empty;
		}

		/// <summary>
		/// DataTable → リスト変換
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static List<UsePrescription> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<UsePrescription> result = new List<UsePrescription>();
				foreach (DataRow row in table.Rows)
				{
					UsePrescription data = new UsePrescription();
					data.ContractID = DataBaseValue.ConvObjectToInt(row["申込No"]);
					data.CustomerID = DataBaseValue.ConvObjectToInt(row["顧客No"]);
					data.CustomerName = row["顧客名"].ToString().Trim();
					data.GoodsID = row["商品ID"].ToString().Trim();
					data.GoodsName = row["商品名"].ToString().Trim();
					data.OrderReserveID = DataBaseValue.ConvObjectToInt(row["注文ID"]);
					data.ApplyDate = DataBaseValue.ConvObjectToDateTimeNull(row["申込日時"]);
					data.OperationDate = DataBaseValue.ConvObjectToDateTimeNull(row["運用開始日"]);
					data.ContractStartDate = DataBaseValue.ConvObjectToDateTimeNull(row["契約開始日"]);
					data.ContractEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["契約終了日"]);
					data.BillingStartDate = DataBaseValue.ConvObjectToDateTimeNull(row["課金開始日"]);
					data.BillingEndDate = DataBaseValue.ConvObjectToDateTimeNull(row["課金終了日"]);
					data.EndFlag = DataBaseValue.ConvObjectToBool(row["終了フラグ"]);
					data.DeleteFlag = DataBaseValue.ConvObjectToBool(row["削除フラグ"]);
					data.CreateDate = DataBaseValue.ConvObjectToDateTimeNull(row["作成日"]);
					data.CreatePerson = row["作成者"].ToString().Trim();
					data.UpdateDate = DataBaseValue.ConvObjectToDateTimeNull(row["更新日"]);
					data.UpdatePerson = row["更新者"].ToString().Trim();
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
