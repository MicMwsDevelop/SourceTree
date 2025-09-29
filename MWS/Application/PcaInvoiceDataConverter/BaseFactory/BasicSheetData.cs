//
// BasicSheetData.cs
// 
// 「基本データ」設定値クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
///////////////////////////////////////////////////////////////////////////////////////////////////////
// Ver2.00(2023/06/27 勝呂):新規作成
//
using System;

namespace PcaInvoiceDataConverter.BaseFactory
{
	/// <summary>
	/// 「基本データ」設定値
	/// </summary>
	public class BasicSheetData
	{
		///////////////////////////////////////////////////////
		// 口座振替関連基本データ

		public DateTime? 口座振替日 { get; set; }
		public string PCA請求一覧10読込みファイル { get; set; }
		public string APLUS送信ファイル出力フォルダ { get; set; }
		public string APLUS送信ファイル { get; set; }
		public int 口座振替請求一覧件数 { get; set; }
		public int 口座振替請求一覧請求金額 { get; set; }
		public int 口座振替不可件数 { get; set; }
		public int 口座振替不可請求額 { get; set; }
		public int 口座振替不要件数 { get; set; }
		public int 口座振替不要請求額 { get; set; }

		/// <summary>
		/// 口座振替請求件数の取得
		/// </summary>
		public int 口座振替請求件数
		{
			get
			{
				return 口座振替請求一覧件数 - 口座振替不可件数 - 口座振替不要件数;
			}
		}

		/// <summary>
		/// 口座振替請求金額の取得
		/// </summary>
		public int 口座振替請求金額
		{
			get
			{
				return 口座振替請求一覧請求金額 - 口座振替不可請求額 - 口座振替不要請求額;
			}
		}

		///////////////////////////////////////////////////////
		// WEB請求書発行関連基本データ

		public int WEB請求書番号基数 { get; set; }
		public DateTime? 口座振替請求日 { get; set; }
		public DateTime? 口座振替請求期間開始日 { get; set; }
		public DateTime? 口座振替請求期間終了日 { get; set; }
		public string PCA請求明細10読込みファイル { get; set; }
		public string WEB請求書ファイル出力フォルダ { get; set; }
		public string WEB請求書ヘッダファイル { get; set; }
		public string WEB請求書明細売上行ファイル { get; set; }
		public string WEB請求書明細消費税行ファイル { get; set; }
		public string WEB請求書明細記事行ファイル { get; set; }
		public string AGREX口振通知書ファイル出力フォルダ { get; set; }
		public string AGREX口振通知書ファイル { get; set; }
		public int WEB請求書件数 { get; set; }
		public int AGREX口振通知書件数 { get; set; }
		public int 口振請求なし件数 { get; set; }
		public int 請求金額あり件数 { get; set; }
		public int WEB請求書請求金額 { get; set; }

		///////////////////////////////////////////////////////
		// 銀行振込請求書発行関連基本データ

		public int 請求書番号基数 { get; set; }
		public DateTime? 銀行振込請求書請求日 { get; set; }
		public DateTime? 銀行振込請求期間開始日 { get; set; }
		public DateTime? 銀行振込請求期間終了日 { get; set; }
		public DateTime? 銀行振込入金期限日 { get; set; }
		public string PCA請求一覧11読込みファイル { get; set; }
		public string PCA請求明細11読込みファイル { get; set; }
		public string AGREX請求書ファイル出力フォルダ { get; set; }
		public string AGREX請求書ファイル { get; set; }
		public int 銀行振込請求一覧件数 { get; set; }
		public int 銀行振込請求一覧請求金額 { get; set; }
		public int 銀行振込請求書件数 { get; set; }
		public int 銀行振込請求金額 { get; set; }
		public int 銀行振込マイナス請求件数 { get; set; }
		public int 銀行振込マイナス請求金額 { get; set; }
		public int 銀行振込0円請求件数 { get; set; }

		/// <summary>
		/// 銀行振込請求書発行件数の取得
		/// </summary>
		public int 銀行振込請求書発行件数
		{
			get
			{
				return 銀行振込請求書件数 + 銀行振込マイナス請求件数 + 銀行振込0円請求件数;
			}
		}

		/// <summary>
		/// 銀行振込請求書発行金額の取得
		/// </summary>
		public int 銀行振込請求書発行金額
		{
			get
			{
				return 銀行振込請求金額 + 銀行振込マイナス請求金額;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public BasicSheetData()
		{
			口座振替日 = null;
			PCA請求一覧10読込みファイル = string.Empty;
			APLUS送信ファイル出力フォルダ = string.Empty;
			APLUS送信ファイル = string.Empty;
			口座振替請求一覧件数 = 0;
			口座振替請求一覧請求金額 = 0;
			口座振替不可件数 = 0;
			口座振替不可請求額 = 0;
			口座振替不要件数 = 0;
			口座振替不要請求額 = 0;
			WEB請求書番号基数 = 0;
			口座振替請求日 = null;
			口座振替請求期間開始日 = null;
			口座振替請求期間終了日 = null;
			PCA請求明細10読込みファイル = string.Empty;
			WEB請求書ファイル出力フォルダ = string.Empty;
			WEB請求書ヘッダファイル = string.Empty;
			WEB請求書明細売上行ファイル = string.Empty;
			WEB請求書明細消費税行ファイル = string.Empty;
			WEB請求書明細記事行ファイル = string.Empty;
			AGREX口振通知書ファイル出力フォルダ = string.Empty;
			AGREX口振通知書ファイル = string.Empty;
			WEB請求書件数 = 0;
			AGREX口振通知書件数 = 0;
			口振請求なし件数 = 0;
			請求金額あり件数 = 0;
			WEB請求書請求金額 = 0;
			請求書番号基数 = 0;
			銀行振込請求書請求日 = null;
			銀行振込請求期間開始日 = null;
			銀行振込請求期間終了日 = null;
			銀行振込入金期限日 = null;
			PCA請求一覧11読込みファイル = string.Empty;
			PCA請求明細11読込みファイル = string.Empty;
			AGREX請求書ファイル出力フォルダ = string.Empty;
			AGREX請求書ファイル = string.Empty;
			銀行振込請求一覧件数 = 0;
			銀行振込請求一覧請求金額 = 0;
			銀行振込請求書件数 = 0;
			銀行振込請求金額 = 0;
			銀行振込マイナス請求件数 = 0;
			銀行振込マイナス請求金額 = 0;
			銀行振込0円請求件数 = 0;
		}

	/// <summary>
	/// 振替日の取得（口座振替日、口座振替請求日）
	/// </summary>
	/// <returns></returns>
	public DateTime 振替日()
		{
			DateTime transferDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 27);
			if (DayOfWeek.Sunday == transferDate.DayOfWeek)
			{
				// 日曜日は翌日に移動（意味あるのか？）
				transferDate.AddDays(1);
			}
			return transferDate;
		}
	}
}
