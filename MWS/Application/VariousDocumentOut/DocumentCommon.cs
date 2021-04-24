//
// DocumentCommon.cs
// 
// 各種書類出力 共通情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00 新規作成(2021/04/22 勝呂)
//
using MwsLib.BaseFactory.Junp.View;
using MwsLib.BaseFactory.VariousDocumentOut;
using MwsLib.Common;
using VariousDocumentOut.Settings;

namespace VariousDocumentOut
{
	/// <summary>
	/// 各種書類出力 共通情報
	/// </summary>
	public class DocumentCommon
	{
		/// <summary>
		/// 本社情報
		/// </summary>
		public HeadOfficeSettings HeadOffice { get; set; }

		/// <summary>
		/// 拠点情報
		/// </summary>
		public SatelliteOffice Satellite { get; set; }

		/// <summary>
		/// 顧客情報
		/// </summary>
		public vMic全ユーザー2 Customer { get; set; }

		/// <summary>
		/// 運用サポート情報
		/// </summary>
		public string 運用サポート情報 { get; set; }

		/// <summary>
		/// 本社所属かどうか？
		/// </summary>
		public bool IsHeadOffice
		{
			get
			{
				return (null == Satellite);
			}
		}

		/// <summary>
		/// 社名
		/// </summary>
		public string 社名
		{
			get
			{
				return HeadOffice.CompanyName;
			}
		}

		/// <summary>
		/// 営業部名
		/// </summary>
		public string 営業部名
		{
			get
			{
				if (false == IsHeadOffice)
				{
					return Satellite.SaleDepartment;
				}
				return this.社名;
			}
		}

		/// <summary>
		/// 郵便番号
		/// </summary>
		public string 郵便番号
		{
			get
			{
				if (IsHeadOffice)
				{
					return HeadOffice.Zipcode;
				}
				return Satellite.Zipcode;
			}
		}

		/// <summary>
		/// 住所1
		/// </summary>
		public string 住所1
		{
			get
			{
				if (IsHeadOffice)
				{
					return HeadOffice.Address1;
				}
				return Satellite.Address1;
			}
		}

		/// <summary>
		/// 住所2
		/// </summary>
		public string 住所2
		{
			get
			{
				if (IsHeadOffice)
				{
					return HeadOffice.Address2;
				}
				return Satellite.Address2;
			}
		}

		/// <summary>
		/// 住所
		/// </summary>
		public string 住所
		{
			get
			{
				if (IsHeadOffice)
				{
					return HeadOffice.住所;
				}
				return Satellite.住所;
			}
		}

		/// <summary>
		/// 電話番号
		/// </summary>
		public string 電話番号
		{
			get
			{
				if (IsHeadOffice)
				{
					return HeadOffice.Tel;
				}
				return Satellite.Tel;
			}
		}

		/// <summary>
		/// FAX番号
		/// </summary>
		public string FAX番号
		{
			get
			{
				if (IsHeadOffice)
				{
					return HeadOffice.Fax;
				}
				return Satellite.Fax;
			}
		}

		/// <summary>
		/// email
		/// </summary>
		public string メールアドレス
		{
			get
			{
				return HeadOffice.Email;
			}
		}

		/// <summary>
		/// URL
		/// </summary>
		public string URL
		{
			get
			{
				return HeadOffice.Url;
			}
		}

		/// <summary>
		/// 送付先
		/// </summary>
		public string 送付先
		{
			get
			{
				if (IsHeadOffice)
				{
					return HeadOffice.CompanyName;
				}
				return HeadOffice.CompanyName + Satellite.SaleDepartment;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public DocumentCommon()
		{
			HeadOffice = null;
			Satellite = null;
			Customer = null;
			運用サポート情報 = string.Empty;
		}

		/// <summary>
		/// 顧客情報クリア
		/// </summary>
		public void ClearCustomer()
		{
			Customer = null;
			運用サポート情報 = string.Empty;
		}

		/// <summary>
		/// 本日文字列の取得
		/// </summary>
		/// <returns>本日文字列</returns>
		public static string DateTodayString()
		{
			return Date.Today.GetJapaneseString(true, '0', true, true);
		}
	}
}
