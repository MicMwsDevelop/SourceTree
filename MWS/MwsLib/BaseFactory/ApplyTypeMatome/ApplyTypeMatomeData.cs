//
// ApplyTypeMatomeData.cs
//
// 申込種別まとめ情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.000 新規作成(2019/02/08 勝呂)
// 
using System;

namespace MwsLib.BaseFactory.ApplyTypeMatome
{
	/// <summary>
	/// 申込種別まとめ情報
	/// </summary>
	[Serializable]
	public class ApplyTypeMatomeData
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string CustomerName { get; set; }

		/// <summary>
		/// 支店名
		/// </summary>
		public string BranchName { get; set; }

		/// <summary>
		/// 営業担当者名
		/// </summary>
		public string SalesmanName { get; set; }

		/// <summary>
		/// 契約金額
		/// </summary>
		public int Price { get; set; }

		/// <summary>
		/// 申込日
		/// </summary>
		public DateTime? ApplyDate { get; set; }

		/// <summary>
		/// 月数
		/// </summary>
		public int AgreeMonths { get; set; }

		/// <summary>
		/// 契約タイプ
		/// </summary>
		public string ContractType { get; set; }

		/// <summary>
		/// 契約開始日
		/// </summary>
		public DateTime? ContractStartDate { get; set; }

		/// <summary>
		/// 申込種別
		/// </summary>
		public MwsDefine.ApplyType ApplyType { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public ApplyTypeMatomeData()
		{
			CustomerNo = 0;
			CustomerName = string.Empty;
			BranchName = string.Empty;
			SalesmanName = string.Empty;
			Price = 0;
			ApplyDate = null;
			AgreeMonths = 0;
			ContractType = string.Empty;
			ContractStartDate = null;
			ApplyType = MwsDefine.ApplyType.Etc;
		}

		/// <summary>
		/// ログ出力文字列の取得
		/// </summary>
		/// <returns>ログ出力文字列</returns>
		public string ToLog()
		{
			string[] log = new string[10];
			log[0] = CustomerNo.ToString();
			log[1] = CustomerName;
			log[2] = BranchName;
			log[3] = SalesmanName;
			log[4] = Price.ToString();
			log[5] = ApplyDate.ToString();
			log[6] = AgreeMonths.ToString();
			log[7] = ContractType;
			log[8] = ContractStartDate.ToString();
			log[9] = MwsDefine.ApplyTypeString[ApplyType];
			return string.Join(",", log);
		}
	}
}
