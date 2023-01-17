//
// 補助金申請情報.cs
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2022/09/20 勝呂):新規作成
// Ver1.05(2022/12/28 勝呂):経理部要望対応 注文確認書追加対応
//
using CommonLib.BaseFactory.Junp.View;
using System;
using System.Collections.Generic;

namespace OnlineLicenseSubsidy.BaseFactory
{
	/// <summary>
	/// 領収内訳情報
	/// </summary>
	public class 領収内訳情報
	{
		public string 項目 { get; set; }
		public string 内訳 { get; set; }
		public double 補助対象金額 { get; set; }
		public double 補助対象外金額 { get; set; }

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 領収内訳情報()
		{
			項目 = string.Empty;
			内訳 = string.Empty;
			補助対象金額 = 0;
			補助対象外金額 = 0;
		}
	}

	/// <summary>
	/// 補助金申請情報
	/// </summary>
	public class 補助金申請情報
	{
		public vMicユーザーオン資用 顧客情報WW { get; set; }
		public string 受付通番 { get; set; }
		public string 顧客名 { get; set; }
		public string 郵便番号 { get; set; }
		public string 住所 { get; set; }
		public string 電話番号 { get; set; }
		public string 開設者 { get; set; }
		public string 医療機関コード { get; set; }
		public DateTime? 工事完了日 { get; set; }
		public List<領収内訳情報> 領収内訳情報List { get; set; }

		// Ver1.05(2022/12/28 勝呂):経理部要望対応 注文確認書追加対応
		public DateTime? 発送日 { get; set; }
		public DateTime? 受注日 { get; set; }
		public double 金額 { get; set; }

		/// <summary>
		/// 注文確認書の設定があるかどうか？
		/// </summary>
		// Ver1.05(2022/12/28 勝呂):経理部要望対応 注文確認書追加対応
		public bool IsExist注文確認書
		{
			get
			{
				return 受注日.HasValue;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public 補助金申請情報()
		{
			顧客情報WW = null;
			受付通番 = string.Empty;
			顧客名 = string.Empty;
			郵便番号 = string.Empty;
			住所 = string.Empty;
			電話番号 = string.Empty;
			開設者 = string.Empty;
			医療機関コード = string.Empty;
			工事完了日 = null;
			領収内訳情報List = null;

			// Ver1.05(2022/12/28 勝呂):経理部要望対応 注文確認書追加対応
			発送日 = null;
			受注日 = null;
			金額 = 0;
		}

		/// <summary>
		/// 補助対象金額小計
		/// </summary>
		/// <returns>金額</returns>
		public double 補助対象金額小計()
		{
			if (null != 領収内訳情報List)
			{
				double price = 0;
				foreach (領収内訳情報 data in 領収内訳情報List)
				{
					price += data.補助対象金額;
				}
				return price;
			}
			return 0;
		}

		/// <summary>
		/// 補助対象外金額小計
		/// </summary>
		/// <returns>金額</returns>
		public double 補助対象外金額小計()
		{
			if (null != 領収内訳情報List)
			{
				double price = 0;
				foreach (領収内訳情報 data in 領収内訳情報List)
				{
					price += data.補助対象外金額;
				}
				return price;
			}
			return 0;
		}

		/// <summary>
		/// 総額
		/// </summary>
		/// <returns>金額</returns>
		public double 総額()
		{
			if (null != 領収内訳情報List)
			{
				double price = 0;
				foreach (領収内訳情報 data in 領収内訳情報List)
				{
					price += data.補助対象金額 + data.補助対象外金額;
				}
				return price;
			}
			return 0;
		}
	}
}
