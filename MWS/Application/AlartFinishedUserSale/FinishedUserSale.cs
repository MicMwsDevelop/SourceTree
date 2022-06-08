//
// AlartFinishedUserSale.cs
//
// 終了ユーザー課金情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2021/08/18):新規作成(勝呂)
//
using CommonLib.BaseFactory.Pca;
using System.Collections.Generic;

namespace AlartFinishedUserSale
{
	/// <summary>
	/// 終了ユーザー課金情報
	/// </summary>
	public class FinishedUserSale
	{
		/// <summary>
		/// 得意先No
		/// </summary>
		public string TokuisakiNo { get; set; }

		/// <summary>
		/// 顧客No
		/// </summary>
		public int CustomerNo { get; set; }

		/// <summary>
		/// 顧客名
		/// </summary>
		public string CustomerName { get; set; }

		/// <summary>
		/// 売上情報
		/// </summary>
		public List<汎用データレイアウト売上明細データ> SaleList { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public FinishedUserSale()
		{
			TokuisakiNo = string.Empty;
			CustomerNo = 0;
			CustomerName = string.Empty;
			SaleList = new List<汎用データレイアウト売上明細データ>();
		}
	}
}
