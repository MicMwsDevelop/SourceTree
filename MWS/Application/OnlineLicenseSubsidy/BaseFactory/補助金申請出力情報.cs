//
// 補助金申請出力情報.cs
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2022/09/20 勝呂):新規作成
// Ver1.05(2022/12/28 勝呂):経理部要望対応 注文確認書追加対応
//
using CommonLib.BaseFactory;
using System;
using System.Collections.Generic;

namespace OnlineLicenseSubsidy.BaseFactory
{
	/// <summary>
	/// 補助金申請出力情報
	/// </summary>
	public class 補助金申請出力情報
	{
		/// <summary>
		/// 顧客情報
		/// </summary>
		public string 受付通番 { get; set; }
		public string 得意先番号 { get; set; }
		public int 顧客No { get; set; }
		public string 顧客名 { get; set; }
		public string 郵便番号 { get; set; }
		public string 住所 { get; set; }
		public string 電話番号 { get; set; }
		public string 開設者 { get; set; }
		public string 医療機関コード { get; set; }
		public DateTime? 工事完了日 { get; set; }

		/// <summary>
		/// 領収書内訳書
		/// </summary>
		public List<領収内訳情報> 領収内訳情報List { get; set; }

		/// <summary>
		/// 注文確認書
		/// </summary>
		// Ver1.05(2022/12/28 勝呂):経理部要望対応 注文確認書追加対応
		public DateTime? 発送日 { get; set; }
		public DateTime? 受注日 { get; set; }
		public double 金額 { get; set; }

		/// <summary>
		/// ファイル名称の取得
		/// 得意先№_顧客名_領収書内訳書＋完了報告書
		/// </summary>
		public string GetFilename
		{
			get
			{
				// Ver1.05(2022/12/28 勝呂):経理部要望対応 注文確認書追加対応
				//return string.Format("{0}_{1}_領収書内訳書＋完了報告書", 得意先番号, 顧客名);
				return string.Format("{0}_{1}_領収書内訳書＋完了報告書＋注文確認書", 得意先番号, 顧客名);
			}
		}

		/// <summary>
		/// Excelファイル名称の取得
		/// </summary>
		public string GetExcelFilename
		{
			get
			{
				return string.Format("{0}.xlsx", GetFilename);
			}
		}

		/// <summary>
		/// PDFファイル名称の取得
		/// </summary>
		public string GetPdfFilename
		{
			get
			{
				return string.Format("{0}.pdf", GetFilename);
			}
		}

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
		public 補助金申請出力情報()
		{
			受付通番 = string.Empty;
			得意先番号 = string.Empty;
			顧客No = 0;
			顧客名 = string.Empty;
			郵便番号 = string.Empty;
			住所 = string.Empty;
			電話番号 = string.Empty;
			開設者 = string.Empty;
			医療機関コード = string.Empty;
			工事完了日 = null;
			領収内訳情報List = new List<領収内訳情報>();

			// Ver1.05(2022/12/28 勝呂):経理部要望対応 注文確認書追加対応
			発送日 = null;
			受注日 = null;
			金額 = 0;
		}

		/// <summary>
		/// 県番号文字列の取得
		/// </summary>
		/// <returns>県番号文字列</returns>
		public string GetKenNumberString()
		{
			if (0 == 住所.Length)
			{
				return string.Empty;
			}
			foreach (var ken in KenNumDef.KenString)
			{
				if (KenNumDef.KenNumber.None != ken.Key)
				{
					if (-1 != 住所.IndexOf(ken.Value[0]))
					{
						return string.Format("{0:D2}", ((int)(ken.Key)));
					}
				}
			}
			return string.Empty;
		}
	}
}
