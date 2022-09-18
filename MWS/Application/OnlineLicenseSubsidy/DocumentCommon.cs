//
// DocumentCommon.cs
// 
// 共通情報クラス
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2022/07/06 勝呂):新規作成
//
using CommonLib.BaseFactory.Junp.View;
using CommonLib.BaseFactory.VariousDocumentOut;
using CommonLib.Common;

namespace OnlineLicenseSubsidy
{
	/// <summary>
	/// 各種書類出力 共通情報
	/// </summary>
	public class DocumentCommon
	{
		/// <summary>
		/// 拠点情報
		/// </summary>
		public SatelliteOffice Satellite { get; set; }

		/// <summary>
		/// 顧客情報
		/// </summary>
		public vMic全ユーザー3 Customer { get; set; }

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
				return Program.gSettings.HeadOffice.CompanyName;
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
					return Program.gSettings.HeadOffice.Zipcode;
				}
				return Satellite.Zipcode;
			}
		}

		/// <summary>
		/// 本社郵便番号
		/// </summary>
		public string 本社郵便番号
		{
			get
			{
				return Program.gSettings.HeadOffice.Zipcode;
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
					return Program.gSettings.HeadOffice.Address1;
				}
				return Satellite.Address1;
			}
		}

		/// <summary>
		/// 本社住所1
		/// </summary>
		public string 本社住所1
		{
			get
			{
				return Program.gSettings.HeadOffice.Address1;
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
					return Program.gSettings.HeadOffice.Address2;
				}
				return Satellite.Address2;
			}
		}

		/// <summary>
		/// 本社住所2
		/// </summary>
		public string 本社住所2
		{
			get
			{
				return Program.gSettings.HeadOffice.Address2;
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
					return Program.gSettings.HeadOffice.住所;
				}
				return Satellite.住所;
			}
		}

		/// <summary>
		/// 本社住所
		/// </summary>
		public string 本社住所
		{
			get
			{
				return Program.gSettings.HeadOffice.住所;
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
					return Program.gSettings.HeadOffice.Tel;
				}
				return Satellite.Tel;
			}
		}

		/// <summary>
		/// 本社電話番号
		/// </summary>
		public string 本社電話番号
		{
			get
			{
				return Program.gSettings.HeadOffice.Tel;
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
					return Program.gSettings.HeadOffice.Fax;
				}
				return Satellite.Fax;
			}
		}

		/// <summary>
		/// 本社FAX番号
		/// </summary>
		public string 本社FAX番号
		{
			get
			{
				return Program.gSettings.HeadOffice.Fax;
			}
		}

		/// <summary>
		/// メールアドレス
		/// </summary>
		public string メールアドレス
		{
			get
			{
				return Program.gSettings.HeadOffice.Email;
			}
		}

		/// <summary>
		/// URL
		/// </summary>
		public string URL
		{
			get
			{
				return Program.gSettings.HeadOffice.Url;
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
					return Program.gSettings.HeadOffice.CompanyName;
				}
				return Program.gSettings.HeadOffice.CompanyName + Satellite.SaleDepartment;
			}
		}

		/// <summary>
		/// 本社送付先
		/// </summary>
		public string 本社送付先
		{
			get
			{
				return Program.gSettings.HeadOffice.CompanyName;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public DocumentCommon()
		{
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
