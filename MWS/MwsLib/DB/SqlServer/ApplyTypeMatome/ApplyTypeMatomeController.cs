//
// ApplyTypeMatomeController.cs
//
// 申込種別まとめ情報 データ詰め替えクラス
//
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/02/08 勝呂)
// 
using MwsLib.BaseFactory;
using MwsLib.BaseFactory.ApplyTypeMatome;
using System.Collections.Generic;
using System.Data;

namespace MwsLib.DB.SqlServer.ApplyTypeMatome
{
	public static class ApplyTypeMatomeController
	{
		/// <summary>
		/// 申込種別まとめ情報リストの詰め替え
		/// </summary>
		/// <param name="table">DataTable</param>
		/// <returns>申込種別まとめ情報リスト</returns>
		public static List<ApplyTypeMatomeData> ConvertApplyMatomeList(DataTable table)
		{
			List<ApplyTypeMatomeData> result = null;
			if (null != table)
			{
				result = new List<ApplyTypeMatomeData>();
				foreach (DataRow row in table.Rows)
				{
					ApplyTypeMatomeData apply = new ApplyTypeMatomeData();
					apply.CustomerNo = DataBaseValue.ConvObjectToInt(row["fCustomerID"]);
					apply.CustomerName = row["顧客名"].ToString();
					apply.BranchName = row["支店名"].ToString();
					apply.SalesmanName = row["営業担当者名"].ToString();
					apply.Price = DataBaseValue.ConvObjectToInt(row["fTotalAmount"]);
					apply.ApplyDate = DataBaseValue.ConvObjectToDateTimeNull(row["fApplyDate"]);
					apply.AgreeMonths = DataBaseValue.ConvObjectToInt(row["fMonths"]);
					apply.ContractType = row["fContractType"].ToString();
					apply.ContractStartDate = DataBaseValue.ConvObjectToDateTimeNull(row["fContractStartDate"]);
					apply.ApplyType = (MwsDefine.ApplyType)DataBaseValue.ConvObjectToInt(row["APPLY_TYPE"]);
					result.Add(apply);
				}
			}
			return result;
		}
	}
}
