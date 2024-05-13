//
// V_CUSTOMER.cs
//
// [CharlieDB].[dbo].[V_CUSTOMER]
// 
// Copyright (C) MIC All Rights Reserved.
// 
// Ver1.00(2023/06/07 勝呂):新規作成
// 
using CommonLib.DB;
using System;
using System.Collections.Generic;
using System.Data;

namespace CommonLib.BaseFactory.Charlie.View
{
	public class V_CUSTOMER
	{
		/// <summary>
		/// 顧客No
		/// </summary>
		public int CUSTOMER_ID { get; set; }

		/// <summary>
		/// 顧客名1
		/// </summary>
		public string CUSTOMER_NAME1 { get; set; }

		/// <summary>
		/// 顧客名2
		/// </summary>
		public string CUSTOMER_NAME2 { get; set; }

		/// <summary>
		/// 顧客名フリガナ
		/// </summary>
		public string CUSTOMER_KANA { get; set; }

		/// <summary>
		/// 得意先No
		/// </summary>
		public string CUSTOMER_NO { get; set; }

		/// <summary>
		/// 請求先コード
		/// </summary>
		public string BILLING_CODE { get; set; }

		/// <summary>
		/// 支店ＩＤ
		/// </summary>
		public string BRANCH_ID { get; set; }

		/// <summary>
		/// 支店名
		/// </summary>
		public string BRANCH_NAME { get; set; }

		/// <summary>
		/// 県番号
		/// </summary>
		public string PREFECTURAL_NUMBER { get; set; }

		/// <summary>
		/// 都道府県名
		/// </summary>
		public string PREFECTURAL_NAME { get; set; }

		/// <summary>
		/// 郵便番号
		/// </summary>
		public string POSTCODE { get; set; }

		/// <summary>
		/// 住所1
		/// </summary>
		public string ADDRESS1 { get; set; }

		/// <summary>
		/// 住所2
		/// </summary>
		public string ADDRESS2 { get; set; }

		/// <summary>
		/// 住所フリガナ
		/// </summary>
		public string ADDRESS_KANA { get; set; }

		/// <summary>
		/// 電話番号
		/// </summary>
		public string TELEPHONE_NUMBER { get; set; }

		/// <summary>
		/// FAX番号
		/// </summary>
		public string FAX_NUMBER { get; set; }

		/// <summary>
		/// 院長名
		/// </summary>
		public string DIRECTOR_NAME { get; set; }

		/// <summary>
		/// 院長名フリガナ
		/// </summary>
		public string DIRECTOR_NAME_KANA { get; set; }

		/// <summary>
		/// 利用システムコード
		/// </summary>
		public string USE_SYSTEM_CODE { get; set; }

		/// <summary>
		/// 利用システム名
		/// </summary>
		public string USE_SYSTEM_NAME { get; set; }

		/// <summary>
		/// サーバー
		/// </summary>
		public int SERVER { get; set; }

		/// <summary>
		/// クライアントライセンス数
		/// </summary>
		public int CLIENT_LICENSES { get; set; }

		/// <summary>
		/// 登録カード回収日
		/// </summary>
		public string RECOVERY_DAY { get; set; }

		/// <summary>
		/// 改正時情報
		/// </summary>
		public string REVISION_INFORMATION { get; set; }

		/// <summary>
		/// 終了フラグ
		/// </summary>
		public bool END_FLG { get; set; }

		/// <summary>
		/// 保守契約加入
		/// </summary>
		public bool CONTRACT_FLG { get; set; }

		/// <summary>
		/// 保守契約開始年月
		/// </summary>
		public string CONTRACT_FROM { get; set; }

		/// <summary>
		/// 保守契約終了年月
		/// </summary>
		public string CONTRACT_TO { get; set; }

		/// <summary>
		/// 医療機関コード
		/// </summary>
		public string MEDICAL_INSTITUTION_CODE { get; set; }

		/// <summary>
		/// ＯＳ
		/// </summary>
		public string OPERATING_SYSTEM { get; set; }

		/// <summary>
		/// 請求書発行区分
		/// </summary>
		public string PAYMENT_METHOD { get; set; }

		/// <summary>
		/// 最終更新日時
		/// </summary>
		public DateTime? UPDATE_TIME { get; set; }

		/// <summary>
		/// 更新対象フラグ
		/// </summary>
		public int UPDATE_FLAG { get; set; }

		/// <summary>
		/// 顧客名の取得
		/// </summary>
		public string CUSTOMER_NAME
		{
			get
			{
				return CUSTOMER_NAME1 + CUSTOMER_NAME2;
			}
		}

		/// <summary>
		/// 住所の取得
		/// </summary>
		public string ADDRESS
		{
			get
			{
				return ADDRESS1 + ADDRESS2;
			}
		}

		/// <summary>
		/// デフォルトコンストラクタ
		/// </summary>
		public V_CUSTOMER()
		{
			CUSTOMER_ID = 0;
			CUSTOMER_NAME1 = string.Empty;
			CUSTOMER_NAME2 = string.Empty;
			CUSTOMER_KANA = string.Empty;
			CUSTOMER_NO = string.Empty;
			BILLING_CODE = string.Empty;
			BRANCH_ID = string.Empty;
			BRANCH_NAME = string.Empty;
			PREFECTURAL_NUMBER = string.Empty;
			PREFECTURAL_NAME = string.Empty;
			POSTCODE = string.Empty;
			ADDRESS1 = string.Empty;
			ADDRESS2 = string.Empty;
			ADDRESS_KANA = string.Empty;
			TELEPHONE_NUMBER = string.Empty;
			FAX_NUMBER = string.Empty;
			DIRECTOR_NAME = string.Empty;
			DIRECTOR_NAME_KANA = string.Empty;
			USE_SYSTEM_CODE = string.Empty;
			USE_SYSTEM_NAME = string.Empty;
			SERVER = 0;
			CLIENT_LICENSES = 0;
			RECOVERY_DAY = string.Empty;
			REVISION_INFORMATION = string.Empty;
			END_FLG = false;
			CONTRACT_FLG = false;
			CONTRACT_FROM = string.Empty;
			CONTRACT_TO = string.Empty;
			MEDICAL_INSTITUTION_CODE = string.Empty;
			OPERATING_SYSTEM = string.Empty;
			PAYMENT_METHOD = string.Empty;
			UPDATE_TIME = null;
			UPDATE_FLAG = 0;
		}

		/// <summary>
		/// [charlieDB].[dbo].[V_CUSTOMER]の詰め替え
		/// </summary>
		/// <param name="table">データテーブル</param>
		/// <returns>V_CUSTOMER</returns>
		public static List<V_CUSTOMER> DataTableToList(DataTable table)
		{
			if (null != table && 0 < table.Rows.Count)
			{
				List<V_CUSTOMER> result = new List<V_CUSTOMER>();
				foreach (DataRow row in table.Rows)
				{
					V_CUSTOMER data = new V_CUSTOMER
					{
						CUSTOMER_ID = DataBaseValue.ConvObjectToInt(row["CUSTOMER_ID"]),
						CUSTOMER_NAME1 = row["CUSTOMER_NAME1"].ToString().Trim(),
						CUSTOMER_NAME2 = row["CUSTOMER_NAME2"].ToString().Trim(),
						CUSTOMER_KANA = row["CUSTOMER_KANA"].ToString().Trim(),
						CUSTOMER_NO = row["CUSTOMER_NO"].ToString().Trim(),
						BILLING_CODE = row["BILLING_CODE"].ToString().Trim(),
						BRANCH_ID = row["BRANCH_ID"].ToString().Trim(),
						BRANCH_NAME = row["BRANCH_NAME"].ToString().Trim(),
						PREFECTURAL_NUMBER = row["PREFECTURAL_NUMBER"].ToString().Trim(),
						PREFECTURAL_NAME = row["PREFECTURAL_NAME"].ToString().Trim(),
						POSTCODE = row["POSTCODE"].ToString().Trim(),
						ADDRESS1 = row["ADDRESS1"].ToString().Trim(),
						ADDRESS2 = row["ADDRESS2"].ToString().Trim(),
						ADDRESS_KANA = row["ADDRESS_KANA"].ToString().Trim(),
						TELEPHONE_NUMBER = row["TELEPHONE_NUMBER"].ToString().Trim(),
						FAX_NUMBER = row["FAX_NUMBER"].ToString().Trim(),
						DIRECTOR_NAME = row["DIRECTOR_NAME"].ToString().Trim(),
						DIRECTOR_NAME_KANA = row["DIRECTOR_NAME_KANA"].ToString().Trim(),
						USE_SYSTEM_CODE = row["USE_SYSTEM_CODE"].ToString().Trim(),
						USE_SYSTEM_NAME = row["USE_SYSTEM_NAME"].ToString().Trim(),
						SERVER = DataBaseValue.ConvObjectToInt(row["SERVER"]),
						CLIENT_LICENSES = DataBaseValue.ConvObjectToInt(row["CLIENT_LICENSES"]),
						RECOVERY_DAY = row["RECOVERY_DAY"].ToString().Trim(),
						REVISION_INFORMATION = row["REVISION_INFORMATION"].ToString().Trim(),
						END_FLG = DataBaseValue.ConvObjectToBool(row["END_FLG"]),
						CONTRACT_FLG = DataBaseValue.ConvObjectToBool(row["CONTRACT_FLG"]),
						CONTRACT_FROM = row["CONTRACT_FROM"].ToString().Trim(),
						CONTRACT_TO = row["CONTRACT_TO"].ToString().Trim(),
						MEDICAL_INSTITUTION_CODE = row["MEDICAL_INSTITUTION_CODE"].ToString().Trim(),
						OPERATING_SYSTEM = row["OPERATING_SYSTEM"].ToString().Trim(),
						PAYMENT_METHOD = row["PAYMENT_METHOD"].ToString().Trim(),
						UPDATE_TIME = DataBaseValue.ConvObjectToDateTimeNull(row["UPDATE_TIME"]),
						UPDATE_FLAG = DataBaseValue.ConvObjectToInt(row["UPDATE_FLAG"]),
					};
					result.Add(data);
				}
				return result;
			}
			return null;
		}
	}
}
